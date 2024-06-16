using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using static MapAreaStruc;
using static System.Diagnostics.DebuggableAttribute;

public partial class OverlayForm : Form
{
    public int Scale = 9;

    public Form1 Form1_0;

    //#######################################
    //#######################################
    //SETTINGS CONFIGURATION
    public bool ShowMobs = true;
    public bool ShowWPs = true;
    public bool ShowGoodChests = true;
    public bool ShowLogs = true;
    public bool ShowBotInfos = true;
    public bool ShowNPC = true;
    public bool ShowPathFinding = true;
    public bool ShowExits = true;
    public bool ShowMapHackShowLines = true;
    public bool ShowUnitsScanCount = false;     //false = only on debug menu open
    //#######################################
    //#######################################


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
    public List<int> ExitIDs = new List<int>();
    public Point ExitPointDuriel = new Point();
    public Point ExitPointSummoner = new Point();
    public System.Drawing.Point MoveToPoint = new System.Drawing.Point(0, 0);

    public List<System.Drawing.Point> MobsPoints = new List<System.Drawing.Point>();
    public List<int> MobsIDs = new List<int>();
    public List<int> MobsTypes = new List<int>();

    public List<System.Drawing.Point> NPCPoints = new List<System.Drawing.Point>();
    public List<int> NPCIDs = new List<int>();

    Font drawFont = new Font("Arial", 12, FontStyle.Regular);
    Font drawFontBold = new Font("Arial", 14, FontStyle.Bold);
    Font drawFontBold10 = new Font("Arial", 12, FontStyle.Bold);

    SolidBrush drawBrushYellow = new SolidBrush(Color.FromArgb(255, 255, 255, 0));
    SolidBrush drawBrushOrange = new SolidBrush(Color.FromArgb(255, 255, 95, 0));
    SolidBrush drawBrushPurple = new SolidBrush(Color.FromArgb(255, 172, 19, 224));
    SolidBrush drawBrushCyan = new SolidBrush(Color.FromArgb(255, 0, 255, 255));
    SolidBrush drawBrushWhite = new SolidBrush(Color.FromArgb(150, 255, 255, 255));
    SolidBrush drawBrushRed = new SolidBrush(Color.LightPink);
    SolidBrush drawBrushBlue = new SolidBrush(Color.LightBlue);
    SolidBrush drawBrushGreen = new SolidBrush(Color.FromArgb(150, 0, 255, 0));
    SolidBrush drawBrushDark = new SolidBrush(Color.FromArgb(250, 0, 0, 0));


    public bool ScanningOverlayItems = true;
    public bool CanDisplayOverlay = false;

    public List<string> LogsTexts = new List<string>();
    public List<Color> LogsTextColor = new List<Color>();
    public List<DateTime> LogsTextsTimeSinceSpawned = new List<DateTime>();

    public float ScaleScreenSize = 0f;
    public float ScaleScreenSizeInverted = 0f;

    public OverlayForm(Form1 form1_1)
    {
        Form1_0 = form1_1;

        InitializeComponent();
        this.TopMost = true;

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

    public void ResetScaleForDisplay()
    {
        drawFont = new Font("Arial", 12 * ScaleScreenSize, FontStyle.Regular);
        drawFontBold = new Font("Arial", 14 * ScaleScreenSize, FontStyle.Bold);
        drawFontBold10 = new Font("Arial", 12 * ScaleScreenSize, FontStyle.Bold);
    }

    public void AddLogs(string string_3, Color ThisColor)
    {
        LogsTexts.Add(string_3);
        LogsTextColor.Add(ThisColor);
        LogsTextsTimeSinceSpawned.Add(DateTime.Now);
    }

    public void ClearAllLogsExceptLast()
    {
        for (int i = LogsTexts.Count - 2; i >= 0; i--)
        {
            LogsTexts.RemoveAt(i);
            LogsTextColor.RemoveAt(i);
            LogsTextsTimeSinceSpawned.RemoveAt(i);
        }
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
            if (ShowGoodChests) SetAllGoodChestNearby();
            if (ShowMobs)
            {
                if (!Form1_0.MobsStruc_0.DebuggingMobs) SetAllMonsterNearby();
                if (Form1_0.MobsStruc_0.DebuggingMobs) SetAllMonsterNearbyDebug();
            }
            if (ShowNPC) SetAllNPCNearby();
            if (ShowWPs) SetAllWPNearby();
            if (ShowExits) SetAllExitNearby();

            UpdateOverlay();

            TimeSpan UpdatingDisplayTime = DateTime.Now - StartScanTime;

            //stop scanning too much lags!!
            if (UpdatingDisplayTime.TotalMilliseconds > 180)
            {
                Form1_0.method_1("Overlay creating too much slowdown, disabling Overlay!", Color.OrangeRed);
                ScanningOverlayItems = false;
            }
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

        List<Position> AllChestPos = Form1_0.MapAreaStruc_0.GetPositionOfAllObject("object", "GoodChest", (int)Form1_0.PlayerScan_0.levelNo, new List<int>());
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
        ExitIDs = new List<int>();

        List<Position> AllPos = Form1_0.MapAreaStruc_0.GetPositionOfAllObject("exit", "", (int)Form1_0.PlayerScan_0.levelNo, new List<int>(), true);
        for (int i = 0; i < AllPos.Count; i++)
        {
            ExitPoints.Add(new System.Drawing.Point(AllPos[i].X, AllPos[i].Y));
            ExitIDs.Add(Form1_0.MapAreaStruc_0.AllExitsIDs[i]);
        }

        //Set duriel tomb exit
        Position OrificePos = Form1_0.MapAreaStruc_0.GetAreaOfObject("object", "HoradricOrifice", new List<int>(), 65, 72, true);
        if (OrificePos.X != 0 && OrificePos.Y != 0)
        {
            //"id":71, "type":"exit", "x":214, "y":25, "isGoodExit":true}
            //Form1_0.method_1("Moving to: " + ((Enums.Area)(Form1_0.MapAreaStruc_0.CurrentObjectAreaIndex + 1)), Color.Red);
            Position ThisFinalPosition = Form1_0.MapAreaStruc_0.GetPositionOfObject("exit", Form1_0.Town_0.getAreaName((int)Form1_0.MapAreaStruc_0.CurrentObjectAreaIndex + 1), (int)Form1_0.PlayerScan_0.levelNo, new List<int>() { }, true);
            ExitPointDuriel.X = ThisFinalPosition.X;
            ExitPointDuriel.Y = ThisFinalPosition.Y;
        }

        //Set Summoner Pos
        Position ThisFinalPositionArcane = Form1_0.MapAreaStruc_0.GetPositionOfObject("npc", "Summoner", (int)Enums.Area.ArcaneSanctuary, new List<int>() { }, true);
        ExitPointSummoner.X = ThisFinalPositionArcane.X;
        ExitPointSummoner.Y = ThisFinalPositionArcane.Y;
        if (ExitPointSummoner.X != 0 && ExitPointSummoner.Y != 0)
        {
            ExitPoints.Add(new System.Drawing.Point(ThisFinalPositionArcane.X, ThisFinalPositionArcane.Y));
            ExitIDs.Add((int)Enums.Area.CanyonOfTheMagi);
        }
    }

    public void SetAllMonsterNearby()
    {
        MobsPoints = new List<System.Drawing.Point>();
        MobsIDs = new List<int>();
        MobsTypes = new List<int>();

        try
        {
            List<int[]> monsterPositions = Form1_0.MobsStruc_0.GetAllMobsNearby();
            for (int i = 0; i < monsterPositions.Count; i++)
            {
                MobsPoints.Add(new System.Drawing.Point(monsterPositions[i][0], monsterPositions[i][1]));
                MobsIDs.Add(Form1_0.MobsStruc_0.monsterIDs[i]);
                MobsTypes.Add(Form1_0.MobsStruc_0.monsterTypes[i]);
            }
        }
        catch { }
    }

    public void SetAllNPCNearby()
    {
        NPCPoints = new List<System.Drawing.Point>();
        NPCIDs = new List<int>();

        try
        {
            List<int[]> AllPositions = Form1_0.NPCStruc_0.GetAllNPCNearby();
            for (int i = 0; i < AllPositions.Count; i++)
            {
                NPCPoints.Add(new System.Drawing.Point(AllPositions[i][0], AllPositions[i][1]));
                NPCIDs.Add(Form1_0.NPCStruc_0.NPC_IDs[i]);
            }
        }
        catch { }
    }

    public void SetAllMonsterNearbyDebug()
    {
        Form1_0.ClearDebugMobs();
        MobsPoints = new List<System.Drawing.Point>();
        MobsIDs = new List<int>();
        MobsTypes = new List<int>();

        try
        {
            List<int[]> monsterPositions = Form1_0.MobsStruc_0.GetAllMobsNearby();
            for (int i = 0; i < monsterPositions.Count; i++)
            {
                MobsPoints.Add(new System.Drawing.Point(monsterPositions[i][0], monsterPositions[i][1]));
                MobsIDs.Add(Form1_0.MobsStruc_0.monsterIDs[i]);
                MobsTypes.Add(Form1_0.MobsStruc_0.monsterTypes[i]);
            }
        }
        catch { }
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

        if (CharConfig.ShowOverlay)
        {
            e.Graphics.CompositingQuality = CompositingQuality.HighQuality;
            e.Graphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

            //################################################
            //################################################
            //Print Logs
            if (ShowLogs)
            {
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
                    if (LogsTextColor[ThisIndexInverted] == Color.DarkMagenta || LogsTextColor[ThisIndexInverted] == Color.Magenta) LogsTextColor[ThisIndexInverted] = Color.FromArgb(255, 255, 120, 255);
                    if (LogsTextColor[ThisIndexInverted] == Color.OrangeRed) LogsTextColor[ThisIndexInverted] = Color.Orange;
                    if (LogsTextColor[ThisIndexInverted] == Color.Red) LogsTextColor[ThisIndexInverted] = Color.LightPink;
                    if (LogsTextColor[ThisIndexInverted] == System.Drawing.ColorTranslator.FromHtml("#0005ff")) LogsTextColor[ThisIndexInverted] = Color.LightBlue;
                    if (LogsTextColor[ThisIndexInverted] == System.Drawing.ColorTranslator.FromHtml("#6a6a6a")) LogsTextColor[ThisIndexInverted] = Color.LightGray;

                    SolidBrush drawBrushCustom = new SolidBrush(Color.FromArgb(200, LogsTextColor[ThisIndexInverted].R, LogsTextColor[ThisIndexInverted].G, LogsTextColor[ThisIndexInverted].B));
                    string ThisLogTxt = LogsTexts[ThisIndexInverted];
                    SizeF ThisS = e.Graphics.MeasureString(ThisLogTxt, drawFontBold10);
                    //FillRectangle(e, drawBrushDark, 1500, 840 - (i * 20), 410, 20, true);
                    FillRectangle(e, drawBrushDark, 1890 - (ThisS.Width * ScaleScreenSizeInverted), 840 - (i * 20), ThisS.Width, 20, true);
                    DrawString(e, ThisLogTxt, drawFontBold10, drawBrushCustom, 1890 - (ThisS.Width * ScaleScreenSizeInverted) + 2, 840 - (i * 20), true);
                }
            }

            //################################################
            //################################################
            if (Form1_0.GameStruc_0.IsInGame() && Form1_0.HasPointers)
            {
                if (ShowBotInfos)
                {
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
                    DrawString(e, Qty1.ToString(), drawFontBold, drawBrushWhite, 1091, 1018, true);
                    DrawString(e, Qty2.ToString(), drawFontBold, drawBrushWhite, 1091 + 62, 1018, true);
                    DrawString(e, Qty3.ToString(), drawFontBold, drawBrushWhite, 1091 + (62 * 2), 1018, true);
                    DrawString(e, Qty4.ToString(), drawFontBold, drawBrushWhite, 1091 + (62 * 3), 1018, true);

                    //Print HP/Mana
                    int Percent = (int)((Form1_0.PlayerScan_0.PlayerHP * 100.0) / Form1_0.PlayerScan_0.PlayerMaxHP);
                    if (Form1_0.PlayerScan_0.PlayerHP == 0 && Form1_0.PlayerScan_0.PlayerMaxHP == 0) Percent = 0;
                    string HPTxt = Form1_0.PlayerScan_0.PlayerHP.ToString() + "/" + Form1_0.PlayerScan_0.PlayerMaxHP.ToString() + " (" + Percent + "%)";
                    SizeF ThisS2 = e.Graphics.MeasureString(HPTxt, drawFontBold);
                    FillRectangle(e, drawBrushDark, 560, 960, ThisS2.Width, 22, true);
                    DrawString(e, HPTxt, drawFontBold, drawBrushRed, 560, 960, true);

                    int PercentMana = (int)((Form1_0.PlayerScan_0.PlayerMana * 100.0) / Form1_0.PlayerScan_0.PlayerMaxMana);
                    if (Form1_0.PlayerScan_0.PlayerMana == 0 && Form1_0.PlayerScan_0.PlayerMaxMana == 0) PercentMana = 0;
                    string ManaTxt = Form1_0.PlayerScan_0.PlayerMana.ToString() + "/" + Form1_0.PlayerScan_0.PlayerMaxMana.ToString() + " (" + PercentMana + "%)";
                    ThisS2 = e.Graphics.MeasureString(ManaTxt, drawFontBold);
                    FillRectangle(e, drawBrushDark, 1360 - (ThisS2.Width * ScaleScreenSizeInverted), 960, ThisS2.Width, 22, true);
                    DrawString(e, ManaTxt, drawFontBold, drawBrushBlue, 1360 - (ThisS2.Width * ScaleScreenSizeInverted), 960, true);

                    //Print Player Pos
                    string CordsTxt = Form1_0.PlayerScan_0.xPosFinal.ToString() + ", " + Form1_0.PlayerScan_0.yPosFinal.ToString();
                    ThisS2 = e.Graphics.MeasureString(CordsTxt, drawFontBold);
                    //DrawString(e, CordsTxt, drawFontBold, drawBrushWhite, Form1_0.CenterX - (ThisS2.Width / 2), 960);
                    DrawString(e, CordsTxt, drawFontBold, drawBrushWhite, 990, 960, true);

                    //Print Infos
                    if (ShowMobs) DrawString(e, "Mobs:" + MobsPoints.Count, drawFontBold, drawBrushWhite, 790, 960, true);
                    string MapTxt = "Map Level:" + Form1_0.PlayerScan_0.levelNo;
                    ThisS2 = e.Graphics.MeasureString(MapTxt, drawFontBold);
                    DrawString(e, MapTxt, drawFontBold, drawBrushWhite, 1360 - (ThisS2.Width * ScaleScreenSizeInverted), 935, true);

                    //Print Items Grab/Battle Infos
                    if (!CharConfig.RunMapHackOnly && !CharConfig.RunMapHackPickitOnly && !CharConfig.RunItemGrabScriptOnly)
                    {
                        if (Form1_0.ItemsStruc_0.IsGrabbingItemOnGround)
                        {
                            string ItemTxt = Form1_0.ItemsStruc_0.ItemNAAME + "(" + Form1_0.ItemsStruc_0.txtFileNo + "), Pos:" + Form1_0.ItemsStruc_0.itemx + ", " + Form1_0.ItemsStruc_0.itemy + " (tries:" + (Form1_0.ItemsStruc_0.TriesToPickItemCount + 1) + "/" + CharConfig.MaxItemGrabTries + ")";
                            DrawString(e, ItemTxt, drawFontBold, drawBrushWhite, 560, 910, true);
                        }
                        else if (Form1_0.Battle_0.DoingBattle || Form1_0.Battle_0.ClearingArea)
                        {
                            string MobsTxt = (EnumsMobsNPC.MonsterType)((int)Form1_0.MobsStruc_0.txtFileNo) + "(" + Form1_0.MobsStruc_0.txtFileNo + "), HP:" + Form1_0.MobsStruc_0.MobsHP + ", Pos:" + Form1_0.MobsStruc_0.xPosFinal + ", " + Form1_0.MobsStruc_0.yPosFinal;
                            DrawString(e, MobsTxt, drawFontBold, drawBrushWhite, 560, 910, true);
                        }
                    }

                    //Print Status
                    DrawString(e, "Status: " + Form1_0.CurrentStatus, drawFontBold, drawBrushWhite, 560, 935, true);
                    ThisS2 = e.Graphics.MeasureString(Form1_0.CurrentGameTime, drawFontBold);
                    //DrawString(e, Form1_0.CurrentGameTime, drawFontBold, drawBrushYellow, Form1_0.CenterX, 935, true);
                    DrawString(e, Form1_0.CurrentGameTime, drawFontBold, drawBrushYellow, 990, 935, true);

                    //Print mS Delay
                    string ThisMSStr = "~" + Form1_0.Average_mS + "ms(" + Form1_0.mS.Replace("ms", "") + ")";
                    DrawString(e, ThisMSStr, drawFontBold, drawBrushYellow, 1090, 910, true);

                    //Print FPS Delay
                    string ThisFPSStr = "~" + Form1_0.Average_FPS.ToString("00") + "Fps(" + Form1_0.FPS.ToString("00") + ")";
                    DrawString(e, ThisFPSStr, drawFontBold, drawBrushYellow, 1090, 935, true);

                    if (!CharConfig.RunMapHackOnly && !CharConfig.RunMapHackPickitOnly && !CharConfig.RunItemGrabScriptOnly)
                    {
                        string OtherInfosTxt = Form1_0.TotalChickenCount + " ChickensByHP, " + Form1_0.TotalChickenByTimeCount + " ChickensByTime";
                        ThisS2 = e.Graphics.MeasureString(OtherInfosTxt, drawFontBold);
                        DrawString(e, OtherInfosTxt, drawFontBold, drawBrushWhite, 1360 - (ThisS2.Width * ScaleScreenSizeInverted), 885, true);

                        string OtherInfosTxt2 = Form1_0.CurrentGameNumberFullyDone.ToString() + " Done, " + Form1_0.TotalDeadCount + " Dead";
                        ThisS2 = e.Graphics.MeasureString(OtherInfosTxt2, drawFontBold);
                        DrawString(e, OtherInfosTxt2, drawFontBold, drawBrushWhite, 1360 - (ThisS2.Width * ScaleScreenSizeInverted), 910, true);
                    }

                    //Print Merc
                    if (CharConfig.UsingMerc && !CharConfig.RunMapHackOnly && !CharConfig.RunMapHackPickitOnly && !CharConfig.RunItemGrabScriptOnly)
                    {
                        string ThisMercTxt = "Merc not alive";
                        if (Form1_0.MercStruc_0.MercAlive)
                        {

                            int PercentMerc = (int)((Form1_0.MercStruc_0.MercHP * 100.0) / Form1_0.MercStruc_0.MercMaxHP);
                            if (Form1_0.MercStruc_0.MercHP == 0 && Form1_0.MercStruc_0.MercMaxHP == 0) PercentMerc = 0;
                            ThisMercTxt = "Merc:" + Form1_0.MercStruc_0.MercHP.ToString() + "/" + Form1_0.MercStruc_0.MercMaxHP.ToString() + " (" + PercentMerc + "%)";
                        }
                        ThisS2 = e.Graphics.MeasureString(ThisMercTxt, drawFontBold);
                        DrawString(e, ThisMercTxt, drawFontBold, drawBrushGreen, 1360 - (ThisS2.Width * ScaleScreenSizeInverted), 860, true);
                    }

                    //Print Units Scanned Count
                    if (ShowUnitsScanCount || (Form1_0.DebugMenuStyle > 0 && !ShowUnitsScanCount))
                    {
                        //Show values of the V3 Units Scan
                        /*string UnitsStr = "Units Scan V3:" + Form1_0.PatternsScan_0.GetUnitsScannedCount(3).ToString();
                        UnitsStr += " (Items:" + Form1_0.PatternsScan_0.ScannedItemsCount;
                        UnitsStr += ", Player:" + Form1_0.PatternsScan_0.ScannedPlayerCount;
                        UnitsStr += ", Objects:" + Form1_0.PatternsScan_0.ScannedObjectsCount;
                        UnitsStr += ", NPC:" + Form1_0.PatternsScan_0.ScannedNPCCount;
                        UnitsStr += ")";
                        DrawString(e, UnitsStr, drawFontBold10, drawBrushGreen, 560, 860, true);

                        //Show values of the V2 Units Scan
                        string UnitsStr = "Units Scan V2:" + Form1_0.PatternsScan_0.GetUnitsScannedCount(2).ToString();
                        UnitsStr += " (Items:" + Form1_0.PatternsScan_0.ScannedItemsCount;
                        UnitsStr += ", Player:" + Form1_0.PatternsScan_0.ScannedPlayerCount;
                        UnitsStr += ", Objects:" + Form1_0.PatternsScan_0.ScannedObjectsCount;
                        UnitsStr += ", NPC:" + Form1_0.PatternsScan_0.ScannedNPCCount;
                        UnitsStr += ")";
                        DrawString(e, UnitsStr, drawFontBold10, drawBrushGreen, 560, 835, true);*/

                        //Show values of the V1 Units Scan
                        string UnitsStr = "Units Scan V1:" + Form1_0.PatternsScan_0.GetUnitsScannedCount(1).ToString();
                        UnitsStr += " (Items:" + Form1_0.PatternsScan_0.ScannedItemsCount;
                        UnitsStr += ", Player:" + Form1_0.PatternsScan_0.ScannedPlayerCount;
                        UnitsStr += ", Objects:" + Form1_0.PatternsScan_0.ScannedObjectsCount;
                        UnitsStr += ", NPC:" + Form1_0.PatternsScan_0.ScannedNPCCount;
                        UnitsStr += ")";
                        DrawString(e, UnitsStr, drawFontBold10, drawBrushGreen, 560, 810, true);
                    }

                    //Print Pressed Keys
                    /*string UnitsStr2 = "CTRL Key:" + Form1_0.KeyMouse_0.HoldingCTRL.ToString();
                    DrawString(e, UnitsStr2, drawFontBold10, drawBrushGreen, 560, 860, true);

                    //Show values of the V2 Units Scan
                    UnitsStr2 = "Shift Key:" + Form1_0.KeyMouse_0.HoldingShift.ToString();
                    DrawString(e, UnitsStr2, drawFontBold10, drawBrushGreen, 560, 835, true);

                    //Show values of the V1 Units Scan
                    UnitsStr2 = "ForceMove Key:" + Form1_0.KeyMouse_0.HoldingForceMove.ToString();
                    DrawString(e, UnitsStr2, drawFontBold10, drawBrushGreen, 560, 810, true);*/
                }

                if (CanDisplayOverlay)
                {
                    if (ShowPathFinding)
                    {
                        for (int i = 0; i < PathFindingPoints.Count - 1; i++)
                        {
                            Position itemScreenPosStart = Form1_0.GameStruc_0.World2ScreenDisplay(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, PathFindingPoints[i].X, PathFindingPoints[i].Y);
                            Position itemScreenPosEnd = Form1_0.GameStruc_0.World2ScreenDisplay(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, PathFindingPoints[i + 1].X, PathFindingPoints[i + 1].Y);

                            System.Drawing.Point StartPoint = new System.Drawing.Point(itemScreenPosStart.X, itemScreenPosStart.Y);
                            System.Drawing.Point EndPoint = new System.Drawing.Point(itemScreenPosEnd.X, itemScreenPosEnd.Y);
                            StartPoint = RescaleThisPoint(StartPoint);
                            EndPoint = RescaleThisPoint(EndPoint);

                            System.Drawing.Point MidPoint = new System.Drawing.Point(Form1_0.CenterX, Form1_0.CenterY);

                            //Console.WriteLine("line: " + StartPoint.X + ", " + StartPoint.Y + " to " + EndPoint.X + ", " + EndPoint.Y);
                            if (i == 0) DrawLine(e, redPen, MidPoint, StartPoint, false);

                            DrawLine(e, redPen, StartPoint, EndPoint, false);

                            if (i != 0) DrawCrossAtPoint(e, StartPoint, orangePen, false);
                        }

                        if (MoveToPoint.X != 0 && MoveToPoint.Y != 0)
                        {
                            Position itemScreenPosStart = Form1_0.GameStruc_0.World2ScreenDisplay(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, MoveToPoint.X, MoveToPoint.Y);
                            System.Drawing.Point StartPoint = new System.Drawing.Point(itemScreenPosStart.X, itemScreenPosStart.Y);
                            StartPoint = RescaleThisPoint(StartPoint);
                            if (PathFindingPoints.Count == 0)
                            {
                                System.Drawing.Point MidPoint = new System.Drawing.Point(Form1_0.CenterX, Form1_0.CenterY);
                                DrawLine(e, redPen, MidPoint, StartPoint, false);
                            }
                            DrawCrossAtPoint(e, StartPoint, redPen, false);
                        }
                    }

                    if (ShowMobs)
                    {
                        for (int i = 0; i < MobsPoints.Count; i++)
                        {
                            Pen ThisPenMobs = yellowPen;
                            Brush ThisBrushMobs = drawBrushYellow;
                            if (MobsTypes[i] == 1 || MobsTypes[i] == 2 || MobsTypes[i] == 3)
                            {
                                ThisPenMobs = orangePen;
                                ThisBrushMobs = drawBrushOrange;
                            }

                            Position itemScreenPosStart = Form1_0.GameStruc_0.World2ScreenDisplay(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, MobsPoints[i].X, MobsPoints[i].Y);
                            System.Drawing.Point StartPoint = new System.Drawing.Point(itemScreenPosStart.X, itemScreenPosStart.Y);
                            StartPoint = RescaleThisPoint(StartPoint);
                            DrawCrossAtPoint(e, StartPoint, ThisPenMobs, false);

                            string ThisTxt = "ID:" + MobsIDs[i].ToString();
                            SizeF ThisS3 = e.Graphics.MeasureString(ThisTxt, drawFont);
                            DrawString(e, ThisTxt, drawFont, ThisBrushMobs, StartPoint.X - (ThisS3.Width / 2), StartPoint.Y + 9, false);
                        }
                    }

                    if (ShowNPC)
                    {
                        for (int i = 0; i < NPCPoints.Count; i++)
                        {
                            Position itemScreenPosStart = Form1_0.GameStruc_0.World2ScreenDisplay(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, NPCPoints[i].X, NPCPoints[i].Y);
                            System.Drawing.Point StartPoint = new System.Drawing.Point(itemScreenPosStart.X, itemScreenPosStart.Y);
                            StartPoint = RescaleThisPoint(StartPoint);
                            DrawCrossAtPoint(e, StartPoint, purplePen, false);

                            string ThisTxt = "ID:" + NPCIDs[i].ToString();
                            SizeF ThisS3 = e.Graphics.MeasureString(ThisTxt, drawFont);
                            DrawString(e, ThisTxt, drawFont, drawBrushWhite, StartPoint.X - (ThisS3.Width / 2), StartPoint.Y + 9, false);
                        }
                    }

                    if (ShowGoodChests)
                    {
                        for (int i = 0; i < GoodChestsPoints.Count; i++)
                        {
                            Position itemScreenPosStart = Form1_0.GameStruc_0.World2ScreenDisplay(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, GoodChestsPoints[i].X, GoodChestsPoints[i].Y);
                            System.Drawing.Point StartPoint = new System.Drawing.Point(itemScreenPosStart.X, itemScreenPosStart.Y);
                            StartPoint = RescaleThisPoint(StartPoint);
                            DrawCrossAtPoint(e, StartPoint, greenPen, false);

                            if ((CharConfig.RunMapHackOnly || CharConfig.RunMapHackPickitOnly) && ShowMapHackShowLines)
                            {
                                System.Drawing.Point PlayerPoint = new System.Drawing.Point(Form1_0.CenterX, Form1_0.CenterY);
                                PlayerPoint = RescaleThisPoint(PlayerPoint);
                                DrawLine(e, greenPen, StartPoint, PlayerPoint, false);
                            }
                        }
                    }

                    if (ShowWPs)
                    {
                        for (int i = 0; i < WPPoints.Count; i++)
                        {
                            Position itemScreenPosStart = Form1_0.GameStruc_0.World2ScreenDisplay(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, WPPoints[i].X, WPPoints[i].Y);
                            System.Drawing.Point StartPoint = new System.Drawing.Point(itemScreenPosStart.X, itemScreenPosStart.Y);
                            StartPoint = RescaleThisPoint(StartPoint);
                            DrawCrossAtPoint(e, StartPoint, bluePen, false);

                            if ((CharConfig.RunMapHackOnly || CharConfig.RunMapHackPickitOnly) && ShowMapHackShowLines)
                            {
                                System.Drawing.Point PlayerPoint = new System.Drawing.Point(Form1_0.CenterX, Form1_0.CenterY);
                                PlayerPoint = RescaleThisPoint(PlayerPoint);
                                DrawLine(e, bluePen, StartPoint, PlayerPoint, false);
                            }
                        }
                    }


                    if ((CharConfig.RunMapHackOnly || CharConfig.RunMapHackPickitOnly) && ShowMapHackShowLines)
                    {
                        FillRectangle(e, drawBrushDark, 1398, 5, 270, 119, true);
                        DrawString(e, "Green: Good Chest", drawFontBold10, drawBrushGreen, 1400, 5, true);
                        DrawString(e, "Blue: Waypoint", drawFontBold10, drawBrushBlue, 1400, 25, true);
                        DrawString(e, "Red: Next Area Exit", drawFontBold10, drawBrushRed, 1400, 45, true);
                        DrawString(e, "Yellow: Next Area Exit (Special)", drawFontBold10, drawBrushOrange, 1400, 65, true);
                        DrawString(e, "Purple: Previous Area Exit", drawFontBold10, drawBrushPurple, 1400, 85, true);
                        DrawString(e, "Cyan: Previous Area Exit (Special)", drawFontBold10, drawBrushCyan, 1400, 105, true);
                    }

                    //DrawItemDesc(e);

                    if (ShowExits)
                    {
                        for (int i = 0; i < ExitPoints.Count; i++)
                        {
                            Position itemScreenPosStart = Form1_0.GameStruc_0.World2ScreenDisplay(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, ExitPoints[i].X, ExitPoints[i].Y);
                            System.Drawing.Point StartPoint = new System.Drawing.Point(itemScreenPosStart.X, itemScreenPosStart.Y);
                            StartPoint = RescaleThisPoint(StartPoint);
                            DrawCrossAtPoint(e, StartPoint, cyanPen, false);

                            if ((CharConfig.RunMapHackOnly || CharConfig.RunMapHackPickitOnly) && ShowMapHackShowLines)
                            {
                                System.Drawing.Point PlayerPoint = new System.Drawing.Point(Form1_0.CenterX, Form1_0.CenterY);
                                PlayerPoint = RescaleThisPoint(PlayerPoint);

                                if ((Enums.Area)Form1_0.PlayerScan_0.levelNo == Enums.Area.CanyonOfTheMagi
                                    && ExitPointDuriel.X != 0 && ExitPointDuriel.Y != 0
                                    && ExitPoints[i].X == ExitPointDuriel.X && ExitPoints[i].Y == ExitPointDuriel.Y)
                                {
                                    DrawLine(e, redPen, StartPoint, PlayerPoint, false);
                                }
                                else if ((Enums.Area)Form1_0.PlayerScan_0.levelNo == Enums.Area.ArcaneSanctuary
                                    && ExitPointSummoner.X != 0 && ExitPointSummoner.Y != 0
                                    && ExitPoints[i].X == ExitPointSummoner.X && ExitPoints[i].Y == ExitPointSummoner.Y)
                                {
                                    DrawLine(e, redPen, StartPoint, PlayerPoint, false);
                                }
                                else
                                {
                                    if ((Enums.Area)Form1_0.PlayerScan_0.levelNo == Enums.Area.CanyonOfTheMagi)
                                    {
                                        DrawLine(e, yellowPen, StartPoint, PlayerPoint, false);
                                    }
                                    else
                                    {
                                        if (ExitIDs[i] > Form1_0.PlayerScan_0.levelNo)
                                        {

                                            if (ExitIDs[i] == Form1_0.PlayerScan_0.levelNo + 1)
                                            {
                                                DrawLine(e, redPen, StartPoint, PlayerPoint, false);
                                            }
                                            else
                                            {
                                                DrawLine(e, yellowPen, StartPoint, PlayerPoint, false);
                                            }
                                        }
                                        else
                                        {
                                            if (ExitIDs[i] == Form1_0.PlayerScan_0.levelNo - 1)
                                            {
                                                DrawLine(e, purplePen, StartPoint, PlayerPoint, false);
                                            }
                                            else
                                            {
                                                DrawLine(e, cyanPen, StartPoint, PlayerPoint, false);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    public void DrawItemDesc(PaintEventArgs e)
    {
        string FullString ="Sold Item:Giant Axe (ID:9) && [Type] == axe && [Quality] == Rare && [Flag] == identified && [SecondMinDamage] == 22 && [SecMaxDamage] == 45 && [AttackRate] == -10 && [Durability] == 47 && [MaxDurability] == 50 && [AttackRating] == 93 && [SecondMinDamage] == 29 && [SecMaxDamage] == 59 && [ColdMinDamage] == 1 && [ColdMaxDamage] == 3 && [ColdLength] == 3 && [AttackRate] == -10 && [Durability] == 47 && [MaxDurability] == 50, StashItem:false, ItemToID:false  (07:21:49)";
        string ItemName = "";
        string ItemQuality = "";
        string ItemText = "";
        string ItemDesc = "";

        bool Identified = false;
        bool Socketed = false;
        bool Etheral = false;

        //string FullString = "";
        FullString = FullString.Replace(" ", "");
        FullString = FullString.Replace("&&", "&");
        if (FullString.Contains("&"))
        {
            string[] CmdSplitted = FullString.Split('&');
            for (int i = 0; i < CmdSplitted.Length; i++)
            {
                //Name
                if (CmdSplitted[i].Contains(":"))
                {
                    ItemName = CmdSplitted[i].Substring(CmdSplitted[i].IndexOf(":") + 1);
                }
                //Quality
                else if (CmdSplitted[i].ToLower().Contains("[quality]"))
                {
                    ItemQuality = CmdSplitted[i].Substring(CmdSplitted[i].LastIndexOf("=") + 1);
                }
                //Flag
                else if (CmdSplitted[i].ToLower().Contains("[flag]==identified")) Identified = true;
                else if (CmdSplitted[i].ToLower().Contains("[flag]==socketed")) Socketed = true;
                else if (CmdSplitted[i].ToLower().Contains("[flag]==etheral")) Etheral = true;
                //Stats (Normals)
                //defense
                else if (CmdSplitted[i].ToLower().Contains("[defense]"))
                {
                    if (ItemText != "") ItemText += Environment.NewLine;
                    ItemText += "Defense : " + CmdSplitted[i].Substring(CmdSplitted[i].LastIndexOf("=") + 1);
                }
                //attack damage
                else if (CmdSplitted[i].ToLower().Contains("[secondmindamage]"))
                {
                    if (ItemText != "") ItemText += Environment.NewLine;
                    ItemText += "Damage : " + CmdSplitted[i].Substring(CmdSplitted[i].LastIndexOf("=") + 1);
                }
                else if (CmdSplitted[i].ToLower().Contains("[secmaxdamage]"))
                {
                    if (ItemText != "") ItemText += "-";
                    ItemText += CmdSplitted[i].Substring(CmdSplitted[i].LastIndexOf("=") + 1);
                }
                //durability
                else if (CmdSplitted[i].ToLower().Contains("[durability]"))
                {
                    if (ItemText != "") ItemText += Environment.NewLine;
                    ItemText += "Durability : " + CmdSplitted[i].Substring(CmdSplitted[i].LastIndexOf("=") + 1);
                }
                //max durability
                else if (CmdSplitted[i].ToLower().Contains("[maxdurability]"))
                {
                    if (ItemText != "") ItemText += "/";
                    ItemText += CmdSplitted[i].Substring(CmdSplitted[i].LastIndexOf("=") + 1);
                }
                //strenght required
                //dexterity required
                //lvl required
                else if (CmdSplitted[i].ToLower().Contains("[levelrequire]"))
                {
                    if (ItemText != "") ItemText += Environment.NewLine;
                    ItemText += "Level required : " + CmdSplitted[i].Substring(CmdSplitted[i].LastIndexOf("=") + 1);
                }
                //block rate
                //shield damage
                else
                {
                    if (ItemDesc != "") ItemDesc += Environment.NewLine;
                    ItemDesc += CmdSplitted[i].Replace("[", "").Replace("]", "").Replace("==", " : ");
                }
            }
        }

        SizeF ThisS2 = e.Graphics.MeasureString(ItemName, drawFontBold);
        SizeF ThisS3 = e.Graphics.MeasureString(ItemText, drawFontBold);
        SizeF ThisS4 = e.Graphics.MeasureString(ItemDesc, drawFontBold);
        int ThisWidht = (int) ThisS2.Width;
        if (ThisS3.Width > ThisWidht) ThisWidht = (int)ThisS3.Width;
        if (ThisS4.Width > ThisWidht) ThisWidht = (int)ThisS4.Width;
        int ThisHeigh = (int)ThisS2.Height;
        ThisHeigh += (int)ThisS3.Height;
        ThisHeigh += (int)ThisS4.Height;

        FillRectangle(e, drawBrushDark, Cursor.Position.X + 1, Cursor.Position.Y - (ThisHeigh / 2), ThisWidht, ThisHeigh, true);
        DrawString(e, ItemName, drawFontBold, drawBrushBlue, Cursor.Position.X + (ThisWidht) - ((ThisS2.Width / 2) * ScaleScreenSizeInverted), Cursor.Position.Y - (ThisHeigh / 2), true);
        DrawString(e, ItemText, drawFontBold, drawBrushBlue, Cursor.Position.X + (ThisWidht) - ((ThisS3.Width / 2) * ScaleScreenSizeInverted), Cursor.Position.Y + ThisS2.Height - (ThisHeigh / 2), true);
        DrawString(e, ItemDesc, drawFontBold, drawBrushBlue, Cursor.Position.X + (ThisWidht) - ((ThisS4.Width / 2) * ScaleScreenSizeInverted), Cursor.Position.Y + ThisS2.Height + ThisS3.Height - (ThisHeigh / 2), true);
    }

    public void DrawString(PaintEventArgs e, string ThisTxt, Font ThisFont, Brush ThisBrush, float PosX, float PosY, bool FixPos)
    {
        if (FixPos)
        {
            PosX = Form1_0.KeyMouse_0.CorrectXPos((int)PosX);
            PosY = Form1_0.KeyMouse_0.CorrectYPos((int)PosY);
        }
        e.Graphics.DrawString(ThisTxt, ThisFont, ThisBrush, PosX, PosY);
    }

    public void FillRectangle(PaintEventArgs e, Brush ThisBrush, float PosX, float PosY, float Width, float Height, bool FixPos)
    {
        if (FixPos)
        {
            PosX = Form1_0.KeyMouse_0.CorrectXPos((int)PosX);
            PosY = Form1_0.KeyMouse_0.CorrectYPos((int)PosY);
        }
        int Remover = 0;
        if (ScaleScreenSize != 1) Remover = 1;
        e.Graphics.FillRectangle(ThisBrush, PosX, PosY, Width, Height * ScaleScreenSize - Remover);
    }

    public void DrawLine(PaintEventArgs e, Pen ThisPen, Point StartP, Point EndP, bool FixPos)
    {
        if (FixPos)
        {
            StartP.X = Form1_0.KeyMouse_0.CorrectXPos(StartP.X);
            StartP.Y = Form1_0.KeyMouse_0.CorrectYPos(StartP.Y);
            EndP.X = Form1_0.KeyMouse_0.CorrectXPos(EndP.X);
            EndP.Y = Form1_0.KeyMouse_0.CorrectYPos(EndP.Y);
        }
        e.Graphics.DrawLine(ThisPen, StartP, EndP);
    }

    public void DrawCrossAtPoint(PaintEventArgs e, System.Drawing.Point ThisP, Pen ThisPenColor, bool FixPos)
    {
        System.Drawing.Point ThisPoint1 = new System.Drawing.Point(ThisP.X - 5, ThisP.Y - 5);
        System.Drawing.Point ThisPoint2 = new System.Drawing.Point(ThisP.X + 5, ThisP.Y + 5);

        System.Drawing.Point ThisPoint3 = new System.Drawing.Point(ThisP.X - 5, ThisP.Y + 5);
        System.Drawing.Point ThisPoint4 = new System.Drawing.Point(ThisP.X + 5, ThisP.Y - 5);

        // Draw a X red cross
        DrawLine(e, ThisPenColor, ThisPoint1, ThisPoint2, FixPos);
        DrawLine(e, ThisPenColor, ThisPoint3, ThisPoint4, FixPos);
    }

    public System.Drawing.Point RescaleThisPoint(System.Drawing.Point ThisssPoint)
    {
        ThisssPoint.X = ((ThisssPoint.X - Form1_0.CenterX) / Scale) + Form1_0.CenterX;
        ThisssPoint.Y = ((ThisssPoint.Y - Form1_0.CenterY) / Scale) + Form1_0.CenterY;

        return ThisssPoint;
    }

    private void OverlayForm_Load(object sender, EventArgs e)
    {

    }
}
