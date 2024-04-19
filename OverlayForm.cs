using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static app.MapAreaStruc;
using static System.Diagnostics.DebuggableAttribute;

namespace app
{
    public partial class OverlayForm : Form
    {
        public int Scale = 9;

        public Form1 Form1_0;

        private Pen redPen = new Pen(Color.FromArgb(150, 255, 0, 0), 2);
        private Pen yellowPen = new Pen(Color.FromArgb(150, 255, 255, 0), 2);
        private Pen greenPen = new Pen(Color.FromArgb(150, 0, 255, 0), 2);
        private Pen orangePen = new Pen(Color.FromArgb(150, 255, 95, 0), 2);
        private Pen bluePen = new Pen(Color.FromArgb(150, 0, 0, 255), 2);
        private Pen cyanPen = new Pen(Color.FromArgb(150, 0, 255, 255), 2);
        private Pen purplePen = new Pen(Color.FromArgb(150, 172, 19, 224), 2);
        private Pen whitePen = new Pen(Color.FromArgb(150, 255, 255, 255), 2);
        private Pen darkPen = new Pen(Color.FromArgb(50, 0, 0, 0), 2);

        public List<System.Drawing.Point> PathFindingPoints = new List<System.Drawing.Point>();
        public List<System.Drawing.Point> GoodChestsPoints = new List<System.Drawing.Point>();
        public List<System.Drawing.Point> WPPoints = new List<System.Drawing.Point>();
        public List<System.Drawing.Point> ExitPoints = new List<System.Drawing.Point>();
        public System.Drawing.Point MoveToPoint = new System.Drawing.Point(0, 0);

        public List<System.Drawing.Point> MobsPoints = new List<System.Drawing.Point>();
        public List<int> MobsIDs = new List<int>();

        public List<System.Drawing.Point> NPCPoints = new List<System.Drawing.Point>();
        public List<int> NPCIDs = new List<int>();

        Font drawFont = new Font("Arial", 12, FontStyle.Regular);
        Font drawFontBold = new Font("Arial", 14, FontStyle.Bold);
        Font drawFontBold10 = new Font("Arial", 12, FontStyle.Bold);

        SolidBrush drawBrushYellow = new SolidBrush(Color.FromArgb(150, 255, 255, 0));
        SolidBrush drawBrushWhite = new SolidBrush(Color.FromArgb(150, 255, 255, 255));
        SolidBrush drawBrushRed = new SolidBrush(Color.FromArgb(150, 255, 0, 0));
        SolidBrush drawBrushBlue = new SolidBrush(Color.FromArgb(150, 0, 0, 255));
        SolidBrush drawBrushGreen = new SolidBrush(Color.FromArgb(150, 0, 255, 0));
        SolidBrush drawBrushDark = new SolidBrush(Color.FromArgb(250, 0, 0, 0));


        public bool ScanningOverlayItems = true;
        public bool CanDisplayOverlay = false;

        public List<string> LogsTexts = new List<string>();
        public List<Color> LogsTextColor = new List<Color>();
        public List<DateTime> LogsTextsTimeSinceSpawned = new List<DateTime>();


        public OverlayForm(Form1 form1_1)
        {
            Form1_0 = form1_1;

            InitializeComponent();

            DoubleBuffered = true;

            // Set up the form as a transparent overlay
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.FromArgb(255, 245, 245, 245);
            this.TransparencyKey = Color.FromArgb(255, 245, 245, 245);
            this.TopMost = true;
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new System.Drawing.Point(0, 0);
            this.ShowInTaskbar = false;
            this.Size = new System.Drawing.Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
        }

        public void AddLogs(string string_3, Color ThisColor)
        {
            LogsTexts.Add(string_3);
            LogsTextColor.Add(ThisColor);
            LogsTextsTimeSinceSpawned.Add(DateTime.Now);
        }

        public void SetPathPoints(List<PathFinding.Point> ThisPathFind, int CurrentIndex, Position Offsets, Position PlayerOffsetInCollisiongrid)
        {
            PathFindingPoints = new List<System.Drawing.Point>();

            for (int i = CurrentIndex; i < ThisPathFind.Count; i++)
            {
                PathFindingPoints.Add(new System.Drawing.Point(ThisPathFind[i].X + Offsets.X - PlayerOffsetInCollisiongrid.X, ThisPathFind[i].Y + Offsets.Y - PlayerOffsetInCollisiongrid.Y));
            }

            UpdateOverlay();
        }

        public void ResetPathPoints()
        {
            PathFindingPoints = new List<System.Drawing.Point>();
            //UpdateOverlay();
        }

        public void SetMoveToPoint(System.Drawing.Point ThisPoint)
        {
            MoveToPoint.X = ThisPoint.X;
            MoveToPoint.Y = ThisPoint.Y;
            UpdateOverlay();
        }

        public void SetAllOverlay()
        {
            if (ScanningOverlayItems)
            {
                CanDisplayOverlay = true;

                DateTime StartScanTime = DateTime.Now;
                SetAllGoodChestNearby();
                if (!Form1_0.MobsStruc_0.DebuggingMobs) SetAllMonsterNearby();
                if (Form1_0.MobsStruc_0.DebuggingMobs) SetAllMonsterNearbyDebug();
                SetAllNPCNearby();
                SetAllWPNearby();
                SetAllExitNearby();

                UpdateOverlay();

                TimeSpan UpdatingDisplayTime = DateTime.Now - StartScanTime;

                //stop scanning too much lags!! (->> issue fixed!)
                if (UpdatingDisplayTime.TotalMilliseconds > 160) ScanningOverlayItems = false;
            }
            else
            {
                CanDisplayOverlay = false;

                GoodChestsPoints = new List<System.Drawing.Point>();
                MobsPoints = new List<System.Drawing.Point>();
                NPCPoints = new List<System.Drawing.Point>();
                WPPoints = new List<System.Drawing.Point>();
                ExitPoints = new List<System.Drawing.Point>();

                UpdateOverlay();
            }
        }

        public void SetAllGoodChestNearby()
        {
            GoodChestsPoints = new List<System.Drawing.Point>();

            List<Position> AllChestPos = Form1_0.MapAreaStruc_0.GetPositionOfAllObject("object", "GoodChest", (int) Form1_0.PlayerScan_0.levelNo, new List<int>());
            foreach (var objectPos in AllChestPos)
            {
                GoodChestsPoints.Add(new System.Drawing.Point(objectPos.X, objectPos.Y));
            }
        }

        public void SetAllWPNearby()
        {
            WPPoints = new List<System.Drawing.Point>();

            List<Position> AllPos = Form1_0.MapAreaStruc_0.GetPositionOfAllObject("object", "WaypointPortal", (int)Form1_0.PlayerScan_0.levelNo, new List<int>());
            foreach (var objectPos in AllPos)
            {
                WPPoints.Add(new System.Drawing.Point(objectPos.X, objectPos.Y));
            }
        }

        public void SetAllExitNearby()
        {
            ExitPoints = new List<System.Drawing.Point>();

            List<Position> AllPos = Form1_0.MapAreaStruc_0.GetPositionOfAllObject("exit", "", (int)Form1_0.PlayerScan_0.levelNo, new List<int>(), true);
            foreach (var objectPos in AllPos)
            {
                ExitPoints.Add(new System.Drawing.Point(objectPos.X, objectPos.Y));
            }
        }

        public void SetAllMonsterNearby()
        {
            MobsPoints = new List<System.Drawing.Point>();
            MobsIDs = new List<int>();

            List<int[]> monsterPositions = Form1_0.MobsStruc_0.GetAllMobsNearby();
            for (int i = 0; i < monsterPositions.Count; i++)
            {
                MobsPoints.Add(new System.Drawing.Point(monsterPositions[i][0], monsterPositions[i][1]));
                MobsIDs.Add(Form1_0.MobsStruc_0.monsterIDs[i]);
            }
        }

        public void SetAllNPCNearby()
        {
            NPCPoints = new List<System.Drawing.Point>();
            NPCIDs = new List<int>();

            List<int[]> AllPositions = Form1_0.NPCStruc_0.GetAllNPCNearby();
            for (int i = 0; i < AllPositions.Count; i++)
            {
                NPCPoints.Add(new System.Drawing.Point(AllPositions[i][0], AllPositions[i][1]));
                NPCIDs.Add(Form1_0.NPCStruc_0.NPC_IDs[i]);
            }
        }

        public void SetAllMonsterNearbyDebug()
        {
            Form1_0.ClearDebugMobs();
            MobsPoints = new List<System.Drawing.Point>();
            MobsIDs = new List<int>();

            List<int[]> monsterPositions = Form1_0.MobsStruc_0.GetAllMobsNearby();
            for (int i = 0; i < monsterPositions.Count; i++)
            {
                MobsPoints.Add(new System.Drawing.Point(monsterPositions[i][0], monsterPositions[i][1]));
                MobsIDs.Add(Form1_0.MobsStruc_0.monsterIDs[i]);
            }
        }

        public void ResetMoveToLocation()
        {
            ResetPathPoints();
            MoveToPoint = new System.Drawing.Point(0, 0);
            UpdateOverlay();
        }

        public void ClearAllOverlay()
        {
            ClearAllOverlayWithoutUpdating();
            UpdateOverlay();
        }

        public void ClearAllOverlayWithoutUpdating()
        {
            CanDisplayOverlay = false;
            MoveToPoint = new System.Drawing.Point(0, 0);

            PathFindingPoints = new List<System.Drawing.Point>();
            MobsPoints = new List<System.Drawing.Point>();
            NPCPoints = new List<System.Drawing.Point>();
            GoodChestsPoints = new List<System.Drawing.Point>();
            WPPoints = new List<System.Drawing.Point>();
            ExitPoints = new List<System.Drawing.Point>();
        }

        public void UpdateOverlay()
        {
            if (!Form1_0.Running)
            {
                ClearAllOverlayWithoutUpdating();
            }

            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(updateGUI));
            }
            else
            {
                updateGUI();
            }
        }

        void updateGUI()
        {

            this.Refresh();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (CharConfig.ShowOverlay && CanDisplayOverlay)
            {
                e.Graphics.CompositingQuality = CompositingQuality.HighQuality;
                e.Graphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

                //Print Potions Qty
                int Qty1 = 0;
                int Qty2 = 0;
                int Qty3 = 0;
                int Qty4 = 0;
                for (int i = 0; i < Form1_0.BeltStruc_0.BeltHaveItems.Length; i++)
                {
                    if ((i == 0 || i == 4 || i == 8 || i == 12) && Form1_0.BeltStruc_0.BeltHaveItems[i] > 0) Qty1++;
                    if ((i == 1 || i == 5 || i == 9 || i == 13) && Form1_0.BeltStruc_0.BeltHaveItems[i] > 0) Qty2++;
                    if ((i == 2 || i == 6 || i == 10 || i == 14) && Form1_0.BeltStruc_0.BeltHaveItems[i] > 0) Qty3++;
                    if ((i == 3 || i == 7 || i == 11 || i == 15) && Form1_0.BeltStruc_0.BeltHaveItems[i] > 0) Qty4++;
                }
                e.Graphics.DrawString(Qty1.ToString(), drawFontBold, drawBrushWhite, 1092, 1019);
                e.Graphics.DrawString(Qty2.ToString(), drawFontBold, drawBrushWhite, 1092 + 62, 1019);
                e.Graphics.DrawString(Qty3.ToString(), drawFontBold, drawBrushWhite, 1092 + (62 * 2), 1019);
                e.Graphics.DrawString(Qty4.ToString(), drawFontBold, drawBrushWhite, 1092 + (62 * 3), 1019);

                //Print HP/Mana
                int Percent = (int)((Form1_0.PlayerScan_0.PlayerHP * 100.0) / Form1_0.PlayerScan_0.PlayerMaxHP);
                e.Graphics.DrawString(Form1_0.PlayerScan_0.PlayerHP.ToString() + "/" + Form1_0.PlayerScan_0.PlayerMaxHP.ToString() + " (" + Percent + "%)", drawFontBold, drawBrushRed, 560, 960);

                int PercentMana = (int)((Form1_0.PlayerScan_0.PlayerMana * 100.0) / Form1_0.PlayerScan_0.PlayerMaxMana);
                string ManaTxt = Form1_0.PlayerScan_0.PlayerMana.ToString() + "/" + Form1_0.PlayerScan_0.PlayerMaxMana.ToString() + " (" + PercentMana + "%)";
                SizeF ThisS2 = e.Graphics.MeasureString(ManaTxt, drawFontBold);
                e.Graphics.DrawString(ManaTxt, drawFontBold, drawBrushBlue, 1360 - ThisS2.Width, 960);

                //Print Player Pos
                string CordsTxt = Form1_0.PlayerScan_0.xPosFinal.ToString() + ", " + Form1_0.PlayerScan_0.yPosFinal.ToString();
                ThisS2 = e.Graphics.MeasureString(CordsTxt, drawFontBold);
                e.Graphics.DrawString(CordsTxt, drawFontBold, drawBrushWhite, Form1_0.CenterX - (ThisS2.Width / 2), 960);

                //Print Infos
                e.Graphics.DrawString("Mobs:" + MobsPoints.Count, drawFontBold, drawBrushWhite, 760, 960);
                string MapTxt = "Map Level:" + Form1_0.PlayerScan_0.levelNo;
                ThisS2 = e.Graphics.MeasureString(MapTxt, drawFontBold);
                e.Graphics.DrawString(MapTxt, drawFontBold, drawBrushWhite, 1360 - ThisS2.Width, 935);

                //Print Battle Infos
                if (Form1_0.Battle_0.DoingBattle || Form1_0.Battle_0.ClearingArea)
                {
                    string MobsTxt = Form1_0.NPCStruc_0.getNPC_ID((int)Form1_0.MobsStruc_0.txtFileNo) + " (" + Form1_0.MobsStruc_0.txtFileNo + ") - HP:" + Form1_0.MobsStruc_0.MobsHP + " - Pos:" + Form1_0.MobsStruc_0.xPosFinal + ", " + Form1_0.MobsStruc_0.yPosFinal;
                    e.Graphics.DrawString(MobsTxt, drawFontBold, drawBrushWhite, 560, 910);
                }

                //Print Logs
                List<int> ToRemoveIndex = new List<int>();
                for (int i = 0; i < LogsTexts.Count; i++) if ((DateTime.Now - LogsTextsTimeSinceSpawned[i]).TotalSeconds >= 15) ToRemoveIndex.Add(i);
                for (int i = ToRemoveIndex.Count - 4; i >= 0; i--)
                {
                    LogsTextsTimeSinceSpawned.RemoveAt(ToRemoveIndex[i]);
                    LogsTexts.RemoveAt(ToRemoveIndex[i]);
                    LogsTextColor.RemoveAt(ToRemoveIndex[i]);
                }
                for (int i = 0; i < LogsTexts.Count; i++)
                {
                    //invert index
                    int ThisIndexInverted = LogsTexts.Count - 1 - i;

                    if (LogsTextColor[ThisIndexInverted] == Color.Black) LogsTextColor[ThisIndexInverted] = Color.White;
                    if (LogsTextColor[ThisIndexInverted] == Color.DarkBlue) LogsTextColor[ThisIndexInverted] = Color.LightBlue;
                    if (LogsTextColor[ThisIndexInverted] == Color.DarkGreen) LogsTextColor[ThisIndexInverted] = Color.LightGreen;

                    SolidBrush drawBrushCustom = new SolidBrush(Color.FromArgb(200, LogsTextColor[ThisIndexInverted].R, LogsTextColor[ThisIndexInverted].G, LogsTextColor[ThisIndexInverted].B));
                    string ThisLogTxt = LogsTexts[ThisIndexInverted];
                    SizeF ThisS = e.Graphics.MeasureString(ThisLogTxt, drawFontBold10);
                    e.Graphics.FillRectangle(drawBrushDark, 1500, 840 - (i * 20), 410, 20);
                    e.Graphics.DrawString(ThisLogTxt, drawFontBold10, drawBrushCustom, 1890 - ThisS.Width, 840 - (i * 20));
                }

                //Print Status
                e.Graphics.DrawString("Status: " + Form1_0.CurrentStatus, drawFontBold, drawBrushWhite, 560, 935);
                ThisS2 = e.Graphics.MeasureString(Form1_0.CurrentGameTime, drawFontBold);
                e.Graphics.DrawString(Form1_0.CurrentGameTime, drawFontBold, drawBrushYellow, Form1_0.CenterX, 935);

                string OtherInfosTxt = Form1_0.TotalChickenCount + " ChickensByHP, " + Form1_0.TotalChickenByTimeCount + " ChickensByTime";
                ThisS2 = e.Graphics.MeasureString(OtherInfosTxt, drawFontBold);
                e.Graphics.DrawString(OtherInfosTxt, drawFontBold, drawBrushWhite, 1360 - ThisS2.Width, 910);

                string OtherInfosTxt2 = Form1_0.CurrentGameNumberFullyDone.ToString() + " Done, " + Form1_0.TotalDeadCount + " Dead";
                ThisS2 = e.Graphics.MeasureString(OtherInfosTxt2, drawFontBold);
                e.Graphics.DrawString(OtherInfosTxt2, drawFontBold, drawBrushWhite, 1360 - ThisS2.Width, 885);

                for (int i = 0; i < PathFindingPoints.Count - 1; i++)
                {
                    Dictionary<string, int> itemScreenPosStart = Form1_0.GameStruc_0.World2Screen(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, PathFindingPoints[i].X, PathFindingPoints[i].Y);
                    Dictionary<string, int> itemScreenPosEnd = Form1_0.GameStruc_0.World2Screen(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, PathFindingPoints[i + 1].X, PathFindingPoints[i + 1].Y);

                    System.Drawing.Point StartPoint = new System.Drawing.Point(itemScreenPosStart["x"], itemScreenPosStart["y"]);
                    System.Drawing.Point EndPoint = new System.Drawing.Point(itemScreenPosEnd["x"], itemScreenPosEnd["y"]);
                    StartPoint = RescaleThisPoint(StartPoint);
                    EndPoint = RescaleThisPoint(EndPoint);

                    System.Drawing.Point MidPoint = new System.Drawing.Point(Form1_0.CenterX, Form1_0.CenterY);

                    //Console.WriteLine("line: " + StartPoint.X + ", " + StartPoint.Y + " to " + EndPoint.X + ", " + EndPoint.Y);
                    if (i == 0) e.Graphics.DrawLine(redPen, MidPoint, StartPoint);

                    e.Graphics.DrawLine(redPen, StartPoint, EndPoint);

                    if (i != 0) DrawCrossAtPoint(e, StartPoint, orangePen);
                }

                if (MoveToPoint.X != 0 && MoveToPoint.Y != 0)
                {
                    Dictionary<string, int> itemScreenPosStart = Form1_0.GameStruc_0.World2Screen(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, MoveToPoint.X, MoveToPoint.Y);
                    System.Drawing.Point StartPoint = new System.Drawing.Point(itemScreenPosStart["x"], itemScreenPosStart["y"]);
                    StartPoint = RescaleThisPoint(StartPoint);
                    if (PathFindingPoints.Count == 0)
                    {
                        System.Drawing.Point MidPoint = new System.Drawing.Point(Form1_0.CenterX, Form1_0.CenterY);
                        e.Graphics.DrawLine(redPen, MidPoint, StartPoint);
                    }
                    DrawCrossAtPoint(e, StartPoint, redPen);
                }

                for (int i = 0; i < MobsPoints.Count; i++)
                {
                    Dictionary<string, int> itemScreenPosStart = Form1_0.GameStruc_0.World2Screen(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, MobsPoints[i].X, MobsPoints[i].Y);
                    System.Drawing.Point StartPoint = new System.Drawing.Point(itemScreenPosStart["x"], itemScreenPosStart["y"]);
                    StartPoint = RescaleThisPoint(StartPoint);
                    DrawCrossAtPoint(e, StartPoint, yellowPen);

                    //if (Form1_0.MobsStruc_0.DebuggingMobs)
                    //{
                        string ThisTxt = "ID:" + MobsIDs[i].ToString();
                        SizeF ThisS3 = e.Graphics.MeasureString(ThisTxt, drawFont);
                        e.Graphics.DrawString(ThisTxt, drawFont, drawBrushYellow, StartPoint.X - (ThisS3.Width / 2), StartPoint.Y + 9);
                    //}
                }

                for (int i = 0; i < NPCPoints.Count; i++)
                {
                    Dictionary<string, int> itemScreenPosStart = Form1_0.GameStruc_0.World2Screen(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, NPCPoints[i].X, NPCPoints[i].Y);
                    System.Drawing.Point StartPoint = new System.Drawing.Point(itemScreenPosStart["x"], itemScreenPosStart["y"]);
                    StartPoint = RescaleThisPoint(StartPoint);
                    DrawCrossAtPoint(e, StartPoint, purplePen);

                    //if (Form1_0.MobsStruc_0.DebuggingMobs)
                    //{
                        string ThisTxt = "ID:" + NPCIDs[i].ToString();
                        SizeF ThisS3 = e.Graphics.MeasureString(ThisTxt, drawFont);
                        e.Graphics.DrawString(ThisTxt, drawFont, drawBrushWhite, StartPoint.X - (ThisS3.Width / 2), StartPoint.Y + 9);
                    //}
                }

                for (int i = 0; i < GoodChestsPoints.Count; i++)
                {
                    Dictionary<string, int> itemScreenPosStart = Form1_0.GameStruc_0.World2Screen(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, GoodChestsPoints[i].X, GoodChestsPoints[i].Y);
                    System.Drawing.Point StartPoint = new System.Drawing.Point(itemScreenPosStart["x"], itemScreenPosStart["y"]);
                    StartPoint = RescaleThisPoint(StartPoint);
                    DrawCrossAtPoint(e, StartPoint, greenPen);
                }

                for (int i = 0; i < WPPoints.Count; i++)
                {
                    Dictionary<string, int> itemScreenPosStart = Form1_0.GameStruc_0.World2Screen(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, WPPoints[i].X, WPPoints[i].Y);
                    System.Drawing.Point StartPoint = new System.Drawing.Point(itemScreenPosStart["x"], itemScreenPosStart["y"]);
                    StartPoint = RescaleThisPoint(StartPoint);
                    DrawCrossAtPoint(e, StartPoint, bluePen);
                }

                for (int i = 0; i < ExitPoints.Count; i++)
                {
                    Dictionary<string, int> itemScreenPosStart = Form1_0.GameStruc_0.World2Screen(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, ExitPoints[i].X, ExitPoints[i].Y);
                    System.Drawing.Point StartPoint = new System.Drawing.Point(itemScreenPosStart["x"], itemScreenPosStart["y"]);
                    StartPoint = RescaleThisPoint(StartPoint);
                    DrawCrossAtPoint(e, StartPoint, cyanPen);
                }
            }
        }

        public void DrawCrossAtPoint(PaintEventArgs e, System.Drawing.Point ThisP, Pen ThisPenColor)
        {
            System.Drawing.Point ThisPoint1 = new System.Drawing.Point(ThisP.X - 5, ThisP.Y - 5);
            System.Drawing.Point ThisPoint2 = new System.Drawing.Point(ThisP.X + 5, ThisP.Y + 5);

            System.Drawing.Point ThisPoint3 = new System.Drawing.Point(ThisP.X - 5, ThisP.Y + 5);
            System.Drawing.Point ThisPoint4 = new System.Drawing.Point(ThisP.X + 5, ThisP.Y - 5);

            // Draw a X red cross
            e.Graphics.DrawLine(ThisPenColor, ThisPoint1, ThisPoint2);
            e.Graphics.DrawLine(ThisPenColor, ThisPoint3, ThisPoint4);
        }

        public System.Drawing.Point RescaleThisPoint(System.Drawing.Point ThisssPoint)
        {
            ThisssPoint.X = ((ThisssPoint.X - Form1_0.CenterX) / Scale) + Form1_0.CenterX;
            ThisssPoint.Y = ((ThisssPoint.Y - Form1_0.CenterY) / Scale) + Form1_0.CenterY;

            return ThisssPoint;
        }
    }
}
