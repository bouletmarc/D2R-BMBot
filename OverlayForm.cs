using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
        public List<System.Drawing.Point> PathFindingPoints = new List<System.Drawing.Point>();
        public List<System.Drawing.Point> MobsPoints = new List<System.Drawing.Point>();
        public List<System.Drawing.Point> GoodChestsPoints = new List<System.Drawing.Point>();
        public List<System.Drawing.Point> WPPoints = new List<System.Drawing.Point>();
        public List<System.Drawing.Point> ExitPoints = new List<System.Drawing.Point>();
        public System.Drawing.Point MoveToPoint = new System.Drawing.Point(0, 0);

        Font drawFont = new Font("Arial", 10);
        SolidBrush drawBrush = new SolidBrush(Color.FromArgb(150, 255, 255, 0));

        public List<int> MobsIDs = new List<int>();

        public bool ScanningOverlayItems = true;


        public OverlayForm(Form1 form1_1)
        {
            Form1_0 = form1_1;

            InitializeComponent();

            // Set up the form as a transparent overlay
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.LimeGreen;
            this.TransparencyKey = Color.LimeGreen;
            this.TopMost = true;
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new System.Drawing.Point(0, 0);
            this.ShowInTaskbar = false;
            this.Size = new System.Drawing.Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
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
                DateTime StartScanTime = DateTime.Now;
                SetAllGoodChestNearby();
                if (!Form1_0.MobsStruc_0.DebuggingMobs) SetAllMonsterNearby();
                if (Form1_0.MobsStruc_0.DebuggingMobs) SetAllMonsterNearbyDebug();
                SetAllWPNearby();
                SetAllExitNearby();

                UpdateOverlay();

                TimeSpan UpdatingDisplayTime = DateTime.Now - StartScanTime;

                //stop scanning too much lags!! (->> issue fixed!)
                if (UpdatingDisplayTime.TotalMilliseconds > 160) ScanningOverlayItems = false;
            }
            else
            {
                GoodChestsPoints = new List<System.Drawing.Point>();
                MobsPoints = new List<System.Drawing.Point>();
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

            List<int[]> monsterPositions = Form1_0.MobsStruc_0.GetAllMobsNearby();
            foreach (var monsterPosition in monsterPositions)
            {
                MobsPoints.Add(new System.Drawing.Point(monsterPosition[0], monsterPosition[1]));
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
            MoveToPoint = new System.Drawing.Point(0, 0);

            PathFindingPoints = new List<System.Drawing.Point>();
            MobsPoints = new List<System.Drawing.Point>();
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

            if (CharConfig.ShowOverlay)
            {
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

                    if (Form1_0.MobsStruc_0.DebuggingMobs)
                    {
                        string ThisTxt = "ID:" + MobsIDs[i].ToString();
                        e.Graphics.DrawString(ThisTxt, drawFont, drawBrush, StartPoint.X - (ThisTxt.Length * 3), StartPoint.Y + 10);
                    }
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
