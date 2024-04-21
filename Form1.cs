using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Remoting;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Text.Json;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Xml.Linq;
using static app.Enums;
using static app.Form1;
using static System.Collections.Specialized.BitVector32;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;
using static app.MapAreaStruc;

namespace app
{
    public partial class Form1 : Form
    {

        public string BotVersion = "V2.0";

        public string D2_LOD_113C_Path = "";

        public Process process;
        public string ThisEndPath = Application.StartupPath + @"\Extracted\";
        public Dictionary<string, IntPtr> offsets = new Dictionary<string, IntPtr>();
        public IntPtr BaseAddress = (IntPtr)0;
        public IntPtr processHandle = (IntPtr)0;
        public byte[] buffer = new byte[0x3FFFFFF];
        public byte[] bufferRead = new byte[0];
        public System.Timers.Timer LoopTimer;
        public bool Running = false;
        public bool RunFinished = false;
        public bool HasPointers = false;
        public int UnitStrucOffset = -32;
        public int hWnd = 0;
        public DateTime CheckTime = new DateTime();
        public int LoopDone = 0;
        public DateTime GameStartedTime = new DateTime();
        public bool CharDied = false;
        public bool PrintedGameTime = false;
        public DateTime TimeSinceSearchingForGames = new DateTime();

        public Rectangle D2Rect = new Rectangle();
        public int ScreenX = 1920;
        public int ScreenY = 1080;
        public int CenterX = 0;
        public int CenterY = 0;
        public int D2Widht = 0;
        public int D2Height = 0;
        public int ScreenXOffset = 0;
        public int ScreenYOffset = 0;
        public double centerModeScale = 2.262;
        public int renderScale = 3;
        public int ScreenYMenu = 180;

        public int CurrentGameNumber = 1;
        public int CurrentGameNumberFullyDone = 0;
        public bool SetGameDone = false;
        public int FoundPlayerPointerTryCount = 0;
        public int FoundPlayerPointerRetryTimes = 0;
        public int TriedToCreateNewGameCount = 0;

        public int CurrentGameNumberSinceStart = 1;

        public bool ForceSwitch2ndPlayer = false;
        public bool BadPlayerPointerFound = false;

        public bool PublicGame = false;
        public int DebugMenuStyle = 0;

        public bool BotJustStarted = true;
        public bool SetDeadCount = false;

        public double FPS = 0;
        public string mS = "";

        public int TotalChickenCount = 0;
        public int TotalDeadCount = 0;
        public int TotalChickenByTimeCount = 0;

        public ItemsStruc ItemsStruc_0;
        public Mem Mem_0;
        public Form1 Form1_0;
        public PatternsScan PatternsScan_0;
        public GameStruc GameStruc_0;
        public PlayerScan PlayerScan_0;
        public ItemsAlert ItemsAlert_0;
        public UIScan UIScan_0;
        public BeltStruc BeltStruc_0;
        public ItemsFlags ItemsFlags_0;
        public ItemsNames ItemsNames_0;
        public InventoryStruc InventoryStruc_0;
        public MobsStruc MobsStruc_0;
        public NPCStruc NPCStruc_0;
        public HoverStruc HoverStruc_0;
        public Town Town_0;
        public Potions Potions_0;
        public SkillsStruc SkillsStruc_0;
        public ObjectsStruc ObjectsStruc_0;
        public Mover Mover_0;
        public Stash Stash_0;
        public Shop Shop_0;
        public Repair Repair_0;
        public ChaosLeech ChaosLeech_0;
        public Chaos Chaos_0;
        public Duriel Duriel_0;
        public Pindleskin Pindleskin_0;
        public Battle Battle_0;
        public KeyMouse KeyMouse_0;
        public Summoner Summoner_0;
        public Baal Baal_0;
        public BaalLeech BaalLeech_0;
        public Travincal Travincal_0;
        public Mephisto Mephisto_0;
        public Andariel Andariel_0;
        public Countess Countess_0;
        public MercStruc MercStruc_0;
        public StashStruc StashStruc_0;
        public Cubing Cubing_0;
        public Gamble Gamble_0;
        public LowerKurast LowerKurast_0;
        public SettingsLoader SettingsLoader_0;
        public MapAreaStruc MapAreaStruc_0;
        public PathFinding PathFinding_0;
        public WPTaker WPTaker_0;
        public Cows Cows_0;
        public Eldritch Eldritch_0;
        public Shenk Shenk_0;
        public Nihlatak Nihlatak_0;

        public AndarielRush AndarielRush_0;
        public DarkWoodRush DarkWoodRush_0;
        public DurielRush DurielRush_0;
        public FarOasisRush FarOasisRush_0;
        public HallOfDeadRushCube HallOfDeadRushCube_0;
        public KahlimBrainRush KahlimBrainRush_0;
        public KahlimEyeRush KahlimEyeRush_0;
        public KahlimHeartRush KahlimHeartRush_0;
        public LostCityRush LostCityRush_0;
        public MephistoRush MephistoRush_0;
        public RadamentRush RadamentRush_0;
        public SummonerRush SummonerRush_0;
        public TravincalRush TravincalRush_0;
        public TristramRush TristramRush_0;
        public AncientsRush AncientsRush_0;
        public ChaosRush ChaosRush_0;
        public BaalRush BaalRush_0;

        public OverlayForm overlayForm;

        // REQUIRED CONSTS
        const int PROCESS_QUERY_INFORMATION = 0x0400;
        const int MEM_COMMIT = 0x00001000;
        const int PROCESS_VM_OPERATION = 0x0008;
        const int PROCESS_VM_READ = 0x0010;
        const int PROCESS_VM_WRITE = 0x0020;
        const int SYNCHRONIZE = 0x00100000;

        // REQUIRED METHODS
        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll")]
        static extern void GetSystemInfo(out SYSTEM_INFO lpSystemInfo);

        [DllImport("user32.dll")]
        private static extern int FindWindow(string ClassName, string WindowName);

        [DllImport("user32.dll")]
        private static extern int GetWindowRect(int hwnd, out Rectangle rect);

        [DllImport("user32.dll")]
        private static extern bool GetClientRect(int hwnd, out Rectangle lpRect);

        [DllImport("user32.dll")]
        static extern bool ClientToScreen(int hWnd, out Point lpPoint);


        // REQUIRED STRUCTS
        public struct MEMORY_BASIC_INFORMATION
        {
            public int BaseAddress;
            public int AllocationBase;
            public int AllocationProtect;
            public int RegionSize;
            public int State;
            public int Protect;
            public int lType;
        }

        public struct SYSTEM_INFO
        {
            public ushort processorArchitecture;
            ushort reserved;
            public uint pageSize;
            public IntPtr minimumApplicationAddress;
            public IntPtr maximumApplicationAddress;
            public IntPtr activeProcessorMask;
            public uint numberOfProcessors;
            public uint processorType;
            public uint allocationGranularity;
            public ushort processorLevel;
            public ushort processorRevision;
        }

        public Form1()
        {
            InitializeComponent();

            this.Text = "D2R - BMBot (" + BotVersion + ")";
            labelGames.Text = "";//CurrentGameNumber.ToString();
            SetGameStatus("STOPPED");
            Form1_0 = this;
            richTextBox1.HideSelection = false;//Hide selection so that AppendText will auto scroll to the end
            richTextBox2.HideSelection = false;//Hide selection so that AppendText will auto scroll to the end
            //richTextBox2.Visible = false;

            ModifyMonsterList();

            LabelChickenCount.Text = TotalChickenCount.ToString();
            LabelDeadCount.Text = TotalDeadCount.ToString();

            SetDebugMenu();

            labelGameName.Text = "";
            labelGameTime.Text = "";

            LoopTimer = new System.Timers.Timer(1);
            LoopTimer.Elapsed += new ElapsedEventHandler(timer_Elapsed);

            ScreenX = Screen.PrimaryScreen.Bounds.Width;
            ScreenY = Screen.PrimaryScreen.Bounds.Height;

            //CenterX = CharConfig.ScreenX / 2;
            //CenterY = CharConfig.ScreenY / 2;

            ItemsStruc_0 = new ItemsStruc();
            Mem_0 = new Mem();
            PatternsScan_0 = new PatternsScan();
            GameStruc_0 = new GameStruc();
            PlayerScan_0 = new PlayerScan();
            ItemsAlert_0 = new ItemsAlert();
            UIScan_0 = new UIScan();
            BeltStruc_0 = new BeltStruc();
            ItemsFlags_0 = new ItemsFlags();
            ItemsNames_0 = new ItemsNames();
            InventoryStruc_0 = new InventoryStruc();
            MobsStruc_0 = new MobsStruc();
            NPCStruc_0 = new NPCStruc();
            HoverStruc_0 = new HoverStruc();
            Town_0 = new Town();
            Potions_0 = new Potions();
            SkillsStruc_0 = new SkillsStruc();
            ObjectsStruc_0 = new ObjectsStruc();
            Mover_0 = new Mover();
            Stash_0 = new Stash();
            Shop_0 = new Shop();
            Repair_0 = new Repair();
            Summoner_0 = new Summoner();
            ChaosLeech_0 = new ChaosLeech();
            Chaos_0 = new Chaos();
            Battle_0 = new Battle();
            KeyMouse_0 = new KeyMouse();
            Duriel_0 = new Duriel();
            Pindleskin_0 = new Pindleskin();
            BaalLeech_0 = new BaalLeech();
            Baal_0 = new Baal();
            Travincal_0 = new Travincal();
            Mephisto_0 = new Mephisto();
            Andariel_0 = new Andariel();
            Countess_0 = new Countess();
            MercStruc_0 = new MercStruc();
            StashStruc_0 = new StashStruc();
            Cubing_0 = new Cubing();
            Gamble_0 = new Gamble();
            LowerKurast_0 = new LowerKurast();
            SettingsLoader_0 = new SettingsLoader();
            MapAreaStruc_0 = new MapAreaStruc();
            PathFinding_0 = new PathFinding();
            WPTaker_0 = new WPTaker();
            Cows_0 = new Cows();
            Eldritch_0 = new Eldritch();
            Shenk_0 = new Shenk();
            Nihlatak_0 = new Nihlatak();

            AndarielRush_0 = new AndarielRush();
            DarkWoodRush_0 = new DarkWoodRush();
            DurielRush_0 = new DurielRush();
            FarOasisRush_0 = new FarOasisRush();
            HallOfDeadRushCube_0 = new HallOfDeadRushCube();
            KahlimBrainRush_0 = new KahlimBrainRush();
            KahlimEyeRush_0 = new KahlimEyeRush();
            KahlimHeartRush_0 = new KahlimHeartRush();
            LostCityRush_0 = new LostCityRush();
            MephistoRush_0 = new MephistoRush();
            RadamentRush_0 = new RadamentRush();
            SummonerRush_0 = new SummonerRush();
            TravincalRush_0 = new TravincalRush();
            TristramRush_0 = new TristramRush();
            AncientsRush_0 = new AncientsRush();
            ChaosRush_0 = new ChaosRush();
            BaalRush_0 = new BaalRush();

            ItemsStruc_0.SetForm1(Form1_0);
            Mem_0.SetForm1(Form1_0);
            PatternsScan_0.SetForm1(Form1_0);
            GameStruc_0.SetForm1(Form1_0);
            PlayerScan_0.SetForm1(Form1_0);
            ItemsAlert_0.SetForm1(Form1_0);
            UIScan_0.SetForm1(Form1_0);
            BeltStruc_0.SetForm1(Form1_0);
            ItemsFlags_0.SetForm1(Form1_0);
            InventoryStruc_0.SetForm1(Form1_0);
            MobsStruc_0.SetForm1(Form1_0);
            NPCStruc_0.SetForm1(Form1_0);
            HoverStruc_0.SetForm1(Form1_0);
            Town_0.SetForm1(Form1_0);
            Potions_0.SetForm1(Form1_0);
            SkillsStruc_0.SetForm1(Form1_0);
            ObjectsStruc_0.SetForm1(Form1_0);
            Mover_0.SetForm1(Form1_0);
            Stash_0.SetForm1(Form1_0);
            Shop_0.SetForm1(Form1_0);
            Repair_0.SetForm1(Form1_0);
            Summoner_0.SetForm1(Form1_0);
            ChaosLeech_0.SetForm1(Form1_0);
            Chaos_0.SetForm1(Form1_0);
            Duriel_0.SetForm1(Form1_0);
            Travincal_0.SetForm1(Form1_0);
            Battle_0.SetForm1(Form1_0);
            KeyMouse_0.SetForm1(Form1_0);
            Mephisto_0.SetForm1(Form1_0);
            Pindleskin_0.SetForm1(Form1_0);
            Baal_0.SetForm1(Form1_0);
            BaalLeech_0.SetForm1(Form1_0);
            Andariel_0.SetForm1(Form1_0);
            Countess_0.SetForm1(Form1_0);
            MercStruc_0.SetForm1(Form1_0);
            StashStruc_0.SetForm1(Form1_0);
            Cubing_0.SetForm1(Form1_0);
            Gamble_0.SetForm1(Form1_0);
            LowerKurast_0.SetForm1(Form1_0);
            SettingsLoader_0.SetForm1(Form1_0);
            MapAreaStruc_0.SetForm1(Form1_0);
            PathFinding_0.SetForm1(Form1_0);
            WPTaker_0.SetForm1(Form1_0);
            Cows_0.SetForm1(Form1_0);
            Eldritch_0.SetForm1(Form1_0);
            Shenk_0.SetForm1(Form1_0);
            Nihlatak_0.SetForm1(Form1_0);

            AndarielRush_0.SetForm1(Form1_0);
            DarkWoodRush_0.SetForm1(Form1_0);
            DurielRush_0.SetForm1(Form1_0);
            FarOasisRush_0.SetForm1(Form1_0);
            HallOfDeadRushCube_0.SetForm1(Form1_0);
            KahlimBrainRush_0.SetForm1(Form1_0);
            KahlimEyeRush_0.SetForm1(Form1_0);
            KahlimHeartRush_0.SetForm1(Form1_0);
            LostCityRush_0.SetForm1(Form1_0);
            MephistoRush_0.SetForm1(Form1_0);
            RadamentRush_0.SetForm1(Form1_0);
            SummonerRush_0.SetForm1(Form1_0);
            TravincalRush_0.SetForm1(Form1_0);
            TristramRush_0.SetForm1(Form1_0);
            AncientsRush_0.SetForm1(Form1_0);
            ChaosRush_0.SetForm1(Form1_0);
            BaalRush_0.SetForm1(Form1_0);

            //overlay graphics
            if (overlayForm == null || overlayForm.IsDisposed)
            {
                overlayForm = new OverlayForm(Form1_0);
            }
            overlayForm.Show();

            SettingsLoader_0.LoadSettings();

            if (Form1_0.D2_LOD_113C_Path == "")
            {
                method_1("ERROR: Diablo2 LOD 1.13C Path NOT SET CORRECTLY!", Color.Red);
                method_1("Clic on the settings button and set the path where Diablo2 1.13c (the old legacy diablo2) is located!", Color.Red);
                method_1("Make sure the path don't contain any whitespace!", Color.Red);
            }

            KeyMouse_0.proc = KeyMouse_0.HookCallback;
            KeyMouse_0.hookID = KeyMouse_0.SetHook(KeyMouse_0.proc);
            //ItemsAlert_0.SetParams();

            dataGridView1.Rows.Add("Processing Time", "Unknown");
            //dataGridView1.Rows.Add("---Player---", "---------");
            dataGridView1.Rows.Add("Pointer", "Unknown");
            dataGridView1.Rows.Add("Cords", "Unknown"); //
            dataGridView1.Rows.Add("LeechCords", "Unknown"); //
            dataGridView1.Rows.Add("Life", "Unknown"); //
            dataGridView1.Rows.Add("Mana", "Unknown"); //
            dataGridView1.Rows.Add("Map Level", "Unknown"); //
            //dataGridView1.Rows.Add("Room Exit", "Unknown"); //
            //dataGridView1.Rows.Add("Difficulty", "Unknown"); //
            dataGridView1.Rows.Add("Merc", "Unknown"); //
            //dataGridView1.Rows.Add("Seed", "Unknown"); //
            //dataGridView1.Rows.Add("Belt qty", "Unknown"); //
            //dataGridView1.Rows.Add("---Items---", "---------");
            dataGridView1.Rows.Add("Scanned", "Unknown");
            dataGridView1.Rows.Add("On ground", "Unknown");
            dataGridView1.Rows.Add("Equipped", "Unknown");
            dataGridView1.Rows.Add("InInventory", "Unknown");
            dataGridView1.Rows.Add("InBelt", "Unknown");
            //dataGridView1.Rows.Add("---Menu---", "---------");
            dataGridView1.Rows.Add("Left Open", "Unknown");
            dataGridView1.Rows.Add("Right Open", "Unknown");
            dataGridView1.Rows.Add("Full Open", "Unknown");
        }

        public void LeaveGame(bool BotCompletlyDone)
        {
            if (CharConfig.RunItemGrabScriptOnly) return;

            SetGameStatus("LEAVING");

            if (BotCompletlyDone && !SetGameDone)
            {
                Form1_0.CurrentGameNumberFullyDone++;
                SetGameDone = true;
            }

            if (UIScan_0.OpenUIMenu("quitMenu"))
            {
                KeyMouse_0.MouseClicc(960, 480);
                WaitDelay(5);
                KeyMouse_0.MouseClicc(960, 480);
                WaitDelay(200);
            }
        }

        void RemovePastDump()
        {
            string[] FileList = Directory.GetFiles(ThisEndPath, "Dump*");
            if (FileList.Length > 0)
            {
                for (int i = 0; i < FileList.Length; i++)
                {
                    File.Delete(FileList[i]);
                }
            }
        }

        public void method_1(string string_3, Color ThisColor)
        {
            //try
            //{
                if (richTextBox1.InvokeRequired)
                {
                    // Call this same method but append THREAD2 to the text
                    Action safeWrite = delegate { method_1(string_3, ThisColor); };
                    richTextBox1.Invoke(safeWrite);
                }
                else
                {
                    Console.WriteLine(string_3);
                    richTextBox1.SelectionColor = ThisColor;
                    richTextBox1.AppendText(string_3 + Environment.NewLine);
                    overlayForm.AddLogs(string_3, ThisColor);

                    if (ThisColor == Color.Red || ThisColor == Color.Orange || ThisColor == Color.DarkOrange || ThisColor == Color.OrangeRed) AppendTextErrorLogs(string_3, ThisColor);
                    if (ThisColor == Color.DarkBlue) AppendTextGameLogs(string_3, ThisColor);
                    Application.DoEvents();
                }
            //}
            //catch { }
        }

        public void method_1_Items(string string_3, Color ThisColor)
        {
            //try
            //{
                if (richTextBox2.InvokeRequired)
                {
                    // Call this same method but append THREAD2 to the text
                    Action safeWrite = delegate { method_1_Items(string_3, ThisColor); };
                    richTextBox2.Invoke(safeWrite);
                }
                else
                {
                    string LogThis = string_3+ " in " + Town_0.getAreaName((int) PlayerScan_0.levelNo) + " " + GameStruc_0.GetTimeNow();
                    richTextBox2.SelectionColor = ThisColor;
                    richTextBox2.AppendText(LogThis + Environment.NewLine);
                    method_1(LogThis, ThisColor);
                    Application.DoEvents();
                }
            //}
            //catch { }
        }

        public string PreviousStatus = "IDLE";
        public string CurrentStatus = "IDLE";
        public void SetGameStatus(string string_3)
        {
            //try
            //{
                if (this.InvokeRequired)
                {
                    // Call this same method but append THREAD2 to the text
                    Action safeWrite = delegate { SetGameStatus(string_3); };
                    this.Invoke(safeWrite);
                }
                else
                {
                    string RunText = "STOPPED";
                    if (Running) RunText = "RUNNING";

                    if (string_3 == "STOPPED") string_3 = PreviousStatus;
                    else PreviousStatus = string_3;

                    CurrentStatus = string_3;

                    this.Text = "D2R - BMBot " + BotVersion + " | " + RunText + " | " + string_3;
                    Application.DoEvents();
                }
            //}
            //catch { }
        }

        public void Grid_SetInfos(string RowName, string ThisInfos)
        {
            //try
            //{
                if (dataGridView1.InvokeRequired)
                {
                    // Call this same method but append THREAD2 to the text
                    Action safeWrite = delegate { Grid_SetInfos(RowName, ThisInfos); };
                    dataGridView1.Invoke(safeWrite);
                }
                else
                {
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        if (dataGridView1.Rows[i].Cells[0].Value.ToString() == RowName)
                        {
                            dataGridView1.Rows[i].Cells[1].Value = ThisInfos;
                            return;
                        }
                    }
                }
            //}
            //catch { }
        }

        public void SetGamesText()
        {
            /*if (labelGames.InvokeRequired)
            {
                // Call this same method but append THREAD2 to the text
                Action safeWrite = delegate { SetGamesText(); };
                labelGames.Invoke(safeWrite);
            }
            else
            {
                labelGames.Text = CurrentGameNumberSinceStart.ToString() + " entered. " + CurrentGameNumberFullyDone.ToString() + " fully done";
            }*/
            labelGames.Text = CurrentGameNumberSinceStart.ToString() + " entered. " + CurrentGameNumberFullyDone.ToString() + " fully done";
        }

        public void Startt()
        {
            try
            {
                SYSTEM_INFO sys_info = new SYSTEM_INFO();
                GetSystemInfo(out sys_info);

                if (!Directory.Exists(ThisEndPath)) Directory.CreateDirectory(ThisEndPath);
                RemovePastDump();

                method_1("------------------------------------------", Color.DarkBlue);
                method_1("Extracting Infos...", Color.Black);

                Process[] ProcList = Process.GetProcessesByName("D2R");
                if (!GameStruc_0.IsGameRunning())
                {
                    method_1("D2R is not running!", Color.Red);
                    return;
                }
                else
                {
                    SetGameStatus("LOADING");
                    method_1("D2R is running...", Color.DarkGreen);

                    hWnd = FindWindow(null, "Diablo II: Resurrected");
                    //GetWindowRect(hWnd, out D2Rect);
                    GetClientRect(hWnd, out D2Rect);
                    Point thiP = new Point();
                    ClientToScreen(hWnd, out thiP);

                    D2Widht = D2Rect.Width;
                    D2Height = D2Rect.Height;
                    ScreenXOffset = thiP.X;
                    ScreenYOffset = thiP.Y;

                    CenterX = (D2Widht / 2) + ScreenXOffset;
                    CenterY = (D2Height / 2) + ScreenYOffset;

                    method_1("Screen Specs:", Color.DarkBlue);
                    method_1("-> Screen size: " + ScreenX + ", " + ScreenY, Color.DarkBlue);
                    method_1("-> D2R rect Size: " + D2Widht + ", " + D2Height, Color.DarkBlue);
                    method_1("-> D2R rect offset: " + ScreenXOffset + ", " + ScreenYOffset, Color.DarkBlue);
                    method_1("-> D2R Center Position: " + CenterX + ", " + CenterY, Color.DarkBlue);

                    double screenRatio = (double)D2Widht / D2Height;

                    if (Math.Abs(screenRatio - (16.0 / 9.0)) > 0.01)
                    {
                        method_1("D2R rect Size ratio is not 16:9!", Color.Red);
                    }
                    if (CenterX >= ((ScreenX / 2) - 15)
                        && CenterX <= ((ScreenX / 2) + 15)
                        && CenterY >= ((ScreenY / 2) - 15)
                        && CenterY <= ((ScreenY / 2) + 15))
                    {
                        method_1("D2R rect Position is not in the center of screen, might have some issues!", Color.OrangeRed);
                    }


                    process = Process.GetProcessesByName("D2R")[0];
                    processHandle = OpenProcess(PROCESS_QUERY_INFORMATION | PROCESS_VM_READ, false, process.Id);
                    //processHandle = OpenProcess(PROCESS_QUERY_INFORMATION | PROCESS_VM_OPERATION | PROCESS_VM_READ | PROCESS_VM_WRITE | SYNCHRONIZE, false, process.Id);

                    foreach (ProcessModule module in process.Modules)
                    {
                        if (module.ModuleName == "D2R.exe")
                        {
                            this.BaseAddress = module.BaseAddress;
                            method_1("D2R module BaseAddress: 0x" + this.BaseAddress.ToString("X"), Color.Black);
                        }
                        //Console.WriteLine("Module: " + module.FileName + ", Name2: " + module.ModuleName + ", BaseAddress: " + module.BaseAddress);
                    }

                    int bytesRead = 0;
                    buffer = new byte[0x3FFFFFF];
                    Mem_0.ReadMemory(BaseAddress, ref buffer, buffer.Length, ref bytesRead);
                    if (bytesRead > 0)
                    {
                        string SavePathh = ThisEndPath + "DumpHex1";
                        File.Create(SavePathh).Dispose();
                        File.WriteAllBytes(SavePathh, buffer);
                    }

                    PatternsScan_0.PatternScan();

                    buffer = null;
                    buffer = new byte[0];

                    method_1("Starting loop timer!", Color.Black);
                    method_1("------------------------------------------", Color.DarkBlue);
                    method_1("Bot started for: " + CharConfig.RunningOnChar + " - " + CharConfig.PlayerCharName, Color.DarkBlue);
                    SetGameStatus("IDLE");
                    LoopTimer.Start();
                }
            }
            catch (Exception message)
            {
                method_1("Error:" + Environment.NewLine + message, Color.Red);
                return;

            }
        }

        public void SetNewGame()
        {
            Form1_0.SetGameStatus("NEW GAME STARTED");

            PublicGame = (CharConfig.GamePass == "");
            if (!PublicGame && CharConfig.IsRushing) PublicGame = true;
            if (!PublicGame && !CharConfig.RunGameMakerScript) PublicGame = true;
            if (PublicGame) KeyMouse_0.ProcessingDelay = 5;
            else KeyMouse_0.ProcessingDelay = 2;
            GameStruc_0.AlreadyChickening = false;
            PatternsScan_0.StartIndexItemLast = long.MaxValue;
            //PatternsScan_0.ScanUnitsNumber = 2600;
            PatternsScan_0.ScanUnitsNumber = 2400;
            //PatternsScan_0.ScanUnitsNumber = 2048;
            Town_0.TriedToShopCount = 0;
            Town_0.TriedToShopCount2 = 0;
            Town_0.TriedToMercCount = 0;
            FoundPlayerPointerTryCount = 0;
            FoundPlayerPointerRetryTimes = 0;
            TriedToCreateNewGameCount = 0;
            Town_0.Towning = true;
            Town_0.IsInTown = true;
            Town_0.ForcedTowning = false;
            Town_0.FastTowning = false;
            PlayerScan_0.GetPositions();
            Town_0.LoadFirstTownAct();
            overlayForm.ScanningOverlayItems = true;
            ItemsStruc_0.GetItems(false);
            PlayerScan_0.PlayerHP = PlayerScan_0.PlayerMaxHP;
            if (PlayerScan_0.PlayerHP == 0) PlayerScan_0.PlayerHP = 100;
            PlayerScan_0.SetMaxHPAndMana();
            Shop_0.FirstShopping = true;
            ItemsStruc_0.dwOwnerId_Shared1 = 0;
            ItemsStruc_0.dwOwnerId_Shared2 = 0;
            ItemsStruc_0.dwOwnerId_Shared3 = 0;
            Potions_0.CanUseSkillForRegen = true;
            LowerKurast_0.ResetVars();
            Countess_0.ResetVars();
            Andariel_0.ResetVars();
            Mephisto_0.ResetVars();
            Summoner_0.ResetVars();
            Duriel_0.ResetVars();
            Travincal_0.ResetVars();
            Pindleskin_0.ResetVars();
            WPTaker_0.ResetVars();
            Chaos_0.ResetVars();
            Baal_0.ResetVars();
            Cows_0.ResetVars();
            Eldritch_0.ResetVars();
            Shenk_0.ResetVars();
            Nihlatak_0.ResetVars();

            AndarielRush_0.ResetVars();
            DarkWoodRush_0.ResetVars();
            DurielRush_0.ResetVars();
            FarOasisRush_0.ResetVars();
            HallOfDeadRushCube_0.ResetVars();
            KahlimBrainRush_0.ResetVars();
            KahlimEyeRush_0.ResetVars();
            KahlimHeartRush_0.ResetVars();
            LostCityRush_0.ResetVars();
            MephistoRush_0.ResetVars();
            RadamentRush_0.ResetVars();
            SummonerRush_0.ResetVars();
            TravincalRush_0.ResetVars();
            TristramRush_0.ResetVars();
            AncientsRush_0.ResetVars();
            ChaosRush_0.ResetVars();
            BaalRush_0.ResetVars();

            Battle_0.DoingBattle = false;
            Battle_0.ClearingArea = false;
            Battle_0.MoveTryCount = 0;

            Town_0.IgnoredTPList.Clear();
            Town_0.IgnoredWPList.Clear();
            BaalLeech_0.IgnoredTPList.Clear();
            Town_0.FirstTown = true;
            ForceSwitch2ndPlayer = false;
            PlayerScan_0.PlayerGoldInventory = 0;
            SetGameDone = false;
            BadPlayerPointerFound = false;
            BeltStruc_0.ForceMANAPotionQty = 0;
            BeltStruc_0.ForceHPPotionQty = 0;
            SetGamesText();
            if (CharConfig.RunGameMakerScript && !BotJustStarted) CurrentGameNumber++;
            CurrentGameNumberSinceStart++;
            SettingsLoader_0.SaveOthersSettings();
            ItemsStruc_0.BadItemsOnCursorIDList = new List<long>();
            ItemsStruc_0.BadItemsOnGroundPointerList = new List<long>();
            SetDeadCount = false;
            GameStruc_0.ChickenTry = 0;

            //##############################
            MapAreaStruc_0.ScanMapStruc();
        }

        public void IncreaseDeadCount()
        {
            if (!SetDeadCount)
            {
                Form1_0.TotalDeadCount++;
                Form1_0.LabelDeadCount.Text = Form1_0.TotalDeadCount.ToString();
                SetDeadCount = true;
            }
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            LoopTimer.Stop();
            CheckTime = DateTime.Now;

            //Form1_0.MobsStruc_0.GetMobs("getBossName", "Andariel", true, 200, new List<long>());
            //overlayForm.DoSomething();
            //if (Running) LoopTimer.Start();
            //return;

            if (GameStruc_0.IsGameRunning())
            {
                if (Stash_0.StashFull)
                {
                    StopBot();
                    return;
                }

                bool isInGame = GameStruc_0.IsInGame();
                if (isInGame)
                {
                    if (!HasPointers)
                    {
                        PrintedGameTime = false;
                        PlayerScan_0.scanForPlayer(true);
                        if (PlayerScan_0.FoundPlayer)
                        {
                            GameStruc_0.SetNewGame();
                            SetNewGame();
                            if (!CharConfig.IsRushing) WaitDelay(400); //wait here because 'loading' menu is not correct
                            if (CharConfig.IsRushing) PlayerScan_0.ScanForLeecher();
                            Town_0.GetCorpse();
                            ItemsStruc_0.GetBadItemsOnCursor();
                            HasPointers = true;
                        }
                        else
                        {
                            //didn't found player pointer
                            PlayerScan_0.scanForPlayer(false);
                            if (PlayerScan_0.FoundPlayer)
                            {
                                GameStruc_0.SetNewGame();
                                SetNewGame();
                                if (!CharConfig.IsRushing) WaitDelay(400); //wait here because 'loading' menu is not correct
                                if (CharConfig.IsRushing) PlayerScan_0.ScanForLeecher();
                                Town_0.GetCorpse();
                                ItemsStruc_0.GetBadItemsOnCursor();
                                HasPointers = true;
                            }
                            else
                            {
                                FoundPlayerPointerTryCount++;

                                if (FoundPlayerPointerTryCount >= 300)
                                {
                                    method_1("Leaving Player pointer not found!", Color.Red);
                                    Form1_0.Potions_0.ForceLeave = true;
                                    BadPlayerPointerFound = true;

                                    if (FoundPlayerPointerRetryTimes > 0) ForceSwitch2ndPlayer = true;
                                    FoundPlayerPointerRetryTimes++;

                                    Form1_0.BaalLeech_0.SearchSameGamesAsLastOne = false;
                                    Form1_0.ChaosLeech_0.SearchSameGamesAsLastOne = false;
                                    Form1_0.LeaveGame(false);

                                    SetProcessingTime();
                                    if (Running) LoopTimer.Start();

                                    return;
                                }
                            }
                        }
                    }
                    if (HasPointers)
                    {
                        PlayerScan_0.GetPositions();
                        if (HasPointers)
                        {
                            UIScan_0.readUI();
                            if (!UIScan_0.loading)
                            {
                                //HoverStruc_0.GetHovering();
                                bool runnn = true;
                                if (runnn)
                                {
                                    //MobsStruc_0.GetMobs("", "", true, 200, new List<long>());
                                    //MercStruc_0.GetMercInfos();
                                    //PlayerScan_0.ScanForLeecher();
                                    //Battle_0.SetSkills();
                                    //Battle_0.CastSkills();
                                    ItemsStruc_0.GetItems(true);
                                    //if (Running) LoopTimer.Start();
                                    return;

                                    if (!ItemsStruc_0.GetItems(true))
                                    {
                                        if (!CharConfig.RunItemGrabScriptOnly)
                                        {
                                            if (Town_0.Towning)
                                            {
                                                ItemsStruc_0.GetItems(false);
                                                Town_0.RunTownScript();
                                            }
                                            else
                                            {
                                                Town_0.FastTowning = false;
                                                Town_0.UseLastTP = false;
                                                Town_0.TPSpawned = false;

                                                if (!Town_0.GetInTown() && Form1_0.ItemsStruc_0.ItemsEquiped <= 2)
                                                {
                                                    method_1("Going to town, body not grabbed!", Color.OrangeRed);
                                                    Form1_0.Town_0.GoToTown();
                                                }
                                                else
                                                {
                                                    if (!CharConfig.IsRushing)
                                                    {
                                                        if (Battle_0.ClearingArea || Battle_0.DoingBattle)
                                                        {
                                                            Battle_0.RunBattleScript();
                                                        }
                                                        else
                                                        {
                                                            if (CharConfig.RunWPTaker && !WPTaker_0.ScriptDone)
                                                            {
                                                                WPTaker_0.RunScript();
                                                            }
                                                            else
                                                            {
                                                                if (CharConfig.RunCowsScript && !Cows_0.ScriptDone)
                                                                {
                                                                    Cows_0.RunScript();
                                                                }
                                                                else
                                                                {
                                                                    if (CharConfig.RunCountessScript && !Countess_0.ScriptDone)
                                                                    {
                                                                        Countess_0.RunScript();
                                                                    }
                                                                    else
                                                                    {
                                                                        if (CharConfig.RunAndarielScript && !Andariel_0.ScriptDone)
                                                                        {
                                                                            Andariel_0.RunScript();
                                                                        }
                                                                        else
                                                                        {
                                                                            if (CharConfig.RunSummonerScript && !Summoner_0.ScriptDone)
                                                                            {
                                                                                Summoner_0.RunScript();
                                                                            }
                                                                            else
                                                                            {
                                                                                if (CharConfig.RunDurielScript && !Duriel_0.ScriptDone)
                                                                                {
                                                                                    Duriel_0.RunScript();
                                                                                }
                                                                                else
                                                                                {
                                                                                    if (CharConfig.RunLowerKurastScript && !LowerKurast_0.ScriptDone)
                                                                                    {
                                                                                        LowerKurast_0.RunScript();
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        if (CharConfig.RunTravincalScript && !Travincal_0.ScriptDone)
                                                                                        {
                                                                                            Travincal_0.RunScript();
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            if (CharConfig.RunMephistoScript && !Mephisto_0.ScriptDone)
                                                                                            {
                                                                                                Mephisto_0.RunScript();
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                if (CharConfig.RunChaosScript && !Chaos_0.ScriptDone)
                                                                                                {
                                                                                                    Chaos_0.RunScript();
                                                                                                }
                                                                                                else
                                                                                                {
                                                                                                    if (CharConfig.RunChaosLeechScript && !ChaosLeech_0.ScriptDone)
                                                                                                    {
                                                                                                        ChaosLeech_0.RunScript();
                                                                                                    }
                                                                                                    else
                                                                                                    {
                                                                                                        if (CharConfig.RunEldritchScript && !Eldritch_0.ScriptDone)
                                                                                                        {
                                                                                                            Eldritch_0.RunScript();
                                                                                                        }
                                                                                                        else
                                                                                                        {
                                                                                                            if (CharConfig.RunShenkScript && !Shenk_0.ScriptDone)
                                                                                                            {
                                                                                                                Shenk_0.RunScript();
                                                                                                            }
                                                                                                            else
                                                                                                            {
                                                                                                                if (CharConfig.RunPindleskinScript && !Pindleskin_0.ScriptDone)
                                                                                                                {
                                                                                                                    Pindleskin_0.RunScript();
                                                                                                                }
                                                                                                                else
                                                                                                                {
                                                                                                                    if (CharConfig.RunNihlatakScript && !Nihlatak_0.ScriptDone)
                                                                                                                    {
                                                                                                                        Nihlatak_0.RunScript();
                                                                                                                    }
                                                                                                                    else
                                                                                                                    {
                                                                                                                        if (CharConfig.RunBaalScript && !Baal_0.ScriptDone)
                                                                                                                        {
                                                                                                                            Baal_0.RunScript();
                                                                                                                        }
                                                                                                                        else
                                                                                                                        {
                                                                                                                            if (CharConfig.RunBaalLeechScript && !BaalLeech_0.ScriptDone)
                                                                                                                            {
                                                                                                                                BaalLeech_0.RunScript();
                                                                                                                            }
                                                                                                                            else
                                                                                                                            {
                                                                                                                                Form1_0.LeaveGame(true);
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
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (Battle_0.ClearingArea)
                                                        {
                                                            Battle_0.RunBattleScript();
                                                        }
                                                        else
                                                        {
                                                            if (CharConfig.RunDarkWoodRush && !DarkWoodRush_0.ScriptDone)
                                                            {
                                                                DarkWoodRush_0.RunScript();
                                                            }
                                                            else
                                                            {
                                                                if (CharConfig.RunTristramRush && !TristramRush_0.ScriptDone)
                                                                {
                                                                    TristramRush_0.RunScript();
                                                                }
                                                                else
                                                                {
                                                                    if (CharConfig.RunAndarielRush && !AndarielRush_0.ScriptDone)
                                                                    {
                                                                        AndarielRush_0.RunScript();
                                                                    }
                                                                    else
                                                                    {
                                                                        if (CharConfig.RunRadamentRush && !RadamentRush_0.ScriptDone)
                                                                        {
                                                                            RadamentRush_0.RunScript();
                                                                        }
                                                                        else
                                                                        {
                                                                            if (CharConfig.RunHallOfDeadRush && !HallOfDeadRushCube_0.ScriptDone)
                                                                            {
                                                                                HallOfDeadRushCube_0.RunScript();
                                                                            }
                                                                            else
                                                                            {
                                                                                if (CharConfig.RunFarOasisRush && !FarOasisRush_0.ScriptDone)
                                                                                {
                                                                                    FarOasisRush_0.RunScript();
                                                                                }
                                                                                else
                                                                                {
                                                                                    if (CharConfig.RunLostCityRush && !LostCityRush_0.ScriptDone)
                                                                                    {
                                                                                        LostCityRush_0.RunScript();
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        if (CharConfig.RunSummonerRush && !SummonerRush_0.ScriptDone)
                                                                                        {
                                                                                            SummonerRush_0.RunScript();
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            if (CharConfig.RunDurielRush && !DurielRush_0.ScriptDone)
                                                                                            {
                                                                                                DurielRush_0.RunScript();
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                if (CharConfig.RunKahlimEyeRush && !KahlimEyeRush_0.ScriptDone)
                                                                                                {
                                                                                                    KahlimEyeRush_0.RunScript();
                                                                                                }
                                                                                                else
                                                                                                {
                                                                                                    if (CharConfig.RunKahlimBrainRush && !KahlimBrainRush_0.ScriptDone)
                                                                                                    {
                                                                                                        KahlimBrainRush_0.RunScript();
                                                                                                    }
                                                                                                    else
                                                                                                    {
                                                                                                        if (CharConfig.RunKahlimHeartRush && !KahlimHeartRush_0.ScriptDone)
                                                                                                        {
                                                                                                            KahlimHeartRush_0.RunScript();
                                                                                                        }
                                                                                                        else
                                                                                                        {
                                                                                                            if (CharConfig.RunTravincalRush && !TravincalRush_0.ScriptDone)
                                                                                                            {
                                                                                                                TravincalRush_0.RunScript();
                                                                                                            }
                                                                                                            else
                                                                                                            {
                                                                                                                if (CharConfig.RunMephistoRush && !MephistoRush_0.ScriptDone)
                                                                                                                {
                                                                                                                    MephistoRush_0.RunScript();
                                                                                                                }
                                                                                                                else
                                                                                                                {
                                                                                                                    if (CharConfig.RunChaosRush && !ChaosRush_0.ScriptDone)
                                                                                                                    {
                                                                                                                        ChaosRush_0.RunScript();
                                                                                                                    }
                                                                                                                    else
                                                                                                                    {
                                                                                                                        if (CharConfig.RunAncientsRush && !AncientsRush_0.ScriptDone)
                                                                                                                        {
                                                                                                                            AncientsRush_0.RunScript();
                                                                                                                        }
                                                                                                                        else
                                                                                                                        {
                                                                                                                            if (CharConfig.RunBaalRush && !BaalRush_0.ScriptDone)
                                                                                                                            {
                                                                                                                                BaalRush_0.RunScript();
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
                                    }
                                }
                                else
                                {
                                    ObjectsStruc_0.GetObjects("TownPortal", true);
                                }
                                Potions_0.CheckIfWeUsePotion();
                                GameStruc_0.CheckChickenGameTime();


                                Grid_SetInfos("Scanned", ItemsStruc_0.ItemsScanned.ToString());
                                Grid_SetInfos("On ground", ItemsStruc_0.ItemsOnGround.ToString());
                                Grid_SetInfos("Equipped", ItemsStruc_0.ItemsEquiped.ToString());
                                Grid_SetInfos("InInventory", ItemsStruc_0.ItemsInInventory.ToString());
                                Grid_SetInfos("InBelt", ItemsStruc_0.ItemsInBelt.ToString());
                            }
                            else
                            {
                                overlayForm.ClearAllOverlay();
                            }
                        }
                    }
                }
                else
                {
                    GameStruc_0.method_GameLabel("");
                    method_GameTimeLabel("");
                    PlayerScan_0.PrintedLeechFoundInfo = false;
                    Potions_0.ForceLeave = false;
                    FoundPlayerPointerTryCount = 0;
                    HasPointers = false;
                    BotJustStarted = false;

                    if (!PrintedGameTime)
                    {
                        MapAreaStruc_0.AllMapData.Clear();
                        overlayForm.ClearAllOverlay();
                        GameStruc_0.LogGameTime();
                        TimeSinceSearchingForGames = DateTime.Now;
                        PrintedGameTime = true;
                    }

                    if (!GameStruc_0.IsPlayerConnectedToBnet())
                    {
                        Form1_0.SetGameStatus("CONNECTING TO BNET!");
                        GameStruc_0.ClicCreateNewChar();
                    }
                    else
                    {
                        ChangeCharScript();

                        if (CharConfig.IsRushing)
                        {
                            CharConfig.RunGameMakerScript = false;
                            CharConfig.RunItemGrabScriptOnly = false;
                            CharConfig.RunChaosSearchGameScript = false;
                            CharConfig.RunBaalSearchGameScript = false;
                        }

                        if (CharConfig.RunGameMakerScript)
                        {
                            Form1_0.SetGameStatus("CREATING GAME");

                            if (BadPlayerPointerFound)
                            {
                                CurrentGameNumber++;
                                CurrentGameNumberSinceStart++;
                                BadPlayerPointerFound = false;
                            }
                            if (TriedToCreateNewGameCount >= 4)
                            {
                                CurrentGameNumber++;
                                CurrentGameNumberSinceStart++;
                                TriedToCreateNewGameCount = 0;
                            }
                            Form1_0.GameStruc_0.CreateNewGame(CurrentGameNumber);
                        }
                        else
                        {
                            if (CharConfig.RunBaalSearchGameScript && !CharConfig.RunItemGrabScriptOnly)
                            {
                                Form1_0.SetGameStatus("SEARCHING GAMES");
                                BaalLeech_0.RunScriptNOTInGame();

                                TimeSpan ThisTimeCheckk = DateTime.Now - TimeSinceSearchingForGames;
                                if (ThisTimeCheckk.TotalMinutes > 8)
                                {
                                    LeaveGame(false);
                                    TimeSinceSearchingForGames = DateTime.Now;
                                }
                            }
                            else if (CharConfig.RunChaosSearchGameScript && !CharConfig.RunItemGrabScriptOnly)
                            {
                                Form1_0.SetGameStatus("SEARCHING GAMES");
                                ChaosLeech_0.RunScriptNOTInGame();

                                TimeSpan ThisTimeCheckk = DateTime.Now - TimeSinceSearchingForGames;
                                if (ThisTimeCheckk.TotalMinutes > 8)
                                {
                                    LeaveGame(false);
                                    TimeSinceSearchingForGames = DateTime.Now;
                                }
                            }
                            else
                            {
                                Form1_0.SetGameStatus("IDLE");
                            }
                        }
                    }
                }
            }

            SetProcessingTime();

            if (Running) LoopTimer.Start();
            //if (!Running) SetStartButtonEnable(true);
            //if (Running && LoopDone < 1) LoopTimer.Start();
            //LoopDone++;
        }

        public void GoToNextScript()
        {
            if (!CharConfig.IsRushing && !Town_0.GetInTown())
            {
                if (CharConfig.RunWPTaker && !WPTaker_0.ScriptDone) WPTaker_0.ScriptDone = true;
                else if (CharConfig.RunCowsScript && !Cows_0.ScriptDone) Cows_0.ScriptDone = true;
                else if (CharConfig.RunAndarielScript && !Andariel_0.ScriptDone) Andariel_0.ScriptDone = true;
                else if (CharConfig.RunCountessScript && !Countess_0.ScriptDone) Countess_0.ScriptDone = true;
                else if (CharConfig.RunSummonerScript && !Summoner_0.ScriptDone) Summoner_0.ScriptDone = true;
                else if (CharConfig.RunDurielScript && !Duriel_0.ScriptDone) Duriel_0.ScriptDone = true;
                else if (CharConfig.RunLowerKurastScript && !LowerKurast_0.ScriptDone) LowerKurast_0.ScriptDone = true;
                else if (CharConfig.RunTravincalScript && !Travincal_0.ScriptDone) Travincal_0.ScriptDone = true;
                else if (CharConfig.RunMephistoScript && !Mephisto_0.ScriptDone) Mephisto_0.ScriptDone = true;
                else if (CharConfig.RunChaosScript && !Chaos_0.ScriptDone) Chaos_0.ScriptDone = true;
                else if (CharConfig.RunChaosLeechScript && !ChaosLeech_0.ScriptDone) ChaosLeech_0.ScriptDone = true;
                else if (CharConfig.RunEldritchScript && !Eldritch_0.ScriptDone) Eldritch_0.ScriptDone = true;
                else if (CharConfig.RunShenkScript && !Shenk_0.ScriptDone) Shenk_0.ScriptDone = true;
                else if (CharConfig.RunPindleskinScript && !Pindleskin_0.ScriptDone) Pindleskin_0.ScriptDone = true;
                else if (CharConfig.RunNihlatakScript && !Nihlatak_0.ScriptDone) Nihlatak_0.ScriptDone = true;
                else if (CharConfig.RunBaalScript && !Baal_0.ScriptDone) Baal_0.ScriptDone = true;
                else if (CharConfig.RunBaalLeechScript && !BaalLeech_0.ScriptDone) BaalLeech_0.ScriptDone = true;
            }
            else
            {
                if (CharConfig.RunDarkWoodRush && !DarkWoodRush_0.ScriptDone) DarkWoodRush_0.ScriptDone = true;
                else if (CharConfig.RunTristramRush && !TristramRush_0.ScriptDone) TristramRush_0.ScriptDone = true;
                else if (CharConfig.RunAndarielRush && !AndarielRush_0.ScriptDone) AndarielRush_0.ScriptDone = true;
                else if (CharConfig.RunHallOfDeadRush && !HallOfDeadRushCube_0.ScriptDone) HallOfDeadRushCube_0.ScriptDone = true;
                else if (CharConfig.RunFarOasisRush && !FarOasisRush_0.ScriptDone) FarOasisRush_0.ScriptDone = true;
                else if (CharConfig.RunLostCityRush && !LostCityRush_0.ScriptDone) LostCityRush_0.ScriptDone = true;
                else if (CharConfig.RunSummonerRush && !SummonerRush_0.ScriptDone) SummonerRush_0.ScriptDone = true;
                else if (CharConfig.RunDurielRush && !DurielRush_0.ScriptDone) DurielRush_0.ScriptDone = true;
                else if (CharConfig.RunKahlimEyeRush && !KahlimEyeRush_0.ScriptDone) KahlimEyeRush_0.ScriptDone = true;
                else if (CharConfig.RunKahlimBrainRush && !KahlimBrainRush_0.ScriptDone) KahlimBrainRush_0.ScriptDone = true;
                else if (CharConfig.RunKahlimHeartRush && !KahlimHeartRush_0.ScriptDone) KahlimHeartRush_0.ScriptDone = true;
                else if (CharConfig.RunTravincalRush && !TravincalRush_0.ScriptDone) TravincalRush_0.ScriptDone = true;
                else if (CharConfig.RunMephistoRush && !MephistoRush_0.ScriptDone) MephistoRush_0.ScriptDone = true;
                else if (CharConfig.RunChaosRush && !ChaosRush_0.ScriptDone) ChaosRush_0.ScriptDone = true;
                else if (CharConfig.RunAncientsRush && !AncientsRush_0.ScriptDone) AncientsRush_0.ScriptDone = true;
                else if (CharConfig.RunBaalRush && !BaalRush_0.ScriptDone) BaalRush_0.ScriptDone = true;
            }
        }

        public void ChangeCharScript()
        {
            long baseAddr = (long)Form1_0.BaseAddress + (long)Form1_0.offsets["SelectedChar"];
            byte[] buffer = new byte[16];
            Form1_0.Mem_0.ReadRawMemory(baseAddr, ref buffer, 16);

            string name = "";
            for (int i2 = 0; i2 < 16; i2++)
            {
                if (buffer[i2] != 0x00)
                {
                    name += (char)buffer[i2];
                }
            }
            
            if (!name.Contains(CharConfig.PlayerCharName) || ForceSwitch2ndPlayer)
            {
                method_1("Changing Char...", Color.Red);

                //Esc
                Form1_0.KeyMouse_0.PressKey(Keys.Escape);
                Form1_0.WaitDelay(120);
                Application.DoEvents();

                //Select Top Char
                if (!ForceSwitch2ndPlayer)
                {
                    Form1_0.KeyMouse_0.MouseClicc(1700, 85);
                    Form1_0.WaitDelay(10);
                }
                else
                {
                    Form1_0.KeyMouse_0.MouseClicc(1700, 200);
                    Form1_0.WaitDelay(10);
                }

                Form1_0.KeyMouse_0.MouseClicc(1190, 990); //clic 'salon' if not in server
                Form1_0.WaitDelay(10);

                Form1_0.KeyMouse_0.MouseClicc(1415, 65);  //clic 'join game' if not in game list area
                Form1_0.WaitDelay(10);

                Form1_0.KeyMouse_0.MouseClicc(1720, 210); //clic refresh
                Form1_0.WaitDelay(60);

                ForceSwitch2ndPlayer = false;
            }
        }

        public void SetProcessingTime()
        {
            //Get processing time (ex: 1.125s)
            DateTime CompareTime = DateTime.Now;
            TimeSpan testtime = (CompareTime - CheckTime);
            string TimeStr = "";
            if (testtime.Seconds > 0)
            {
                TimeStr += testtime.Seconds + ".";
            }
            if (testtime.Milliseconds > 0)
            {
                //TimeStr += testtime.Milliseconds.ToString("000");
                TimeStr += testtime.Milliseconds.ToString();
            }
            else
            {
                TimeStr += "0";
            }
            TimeStr += "ms";

            //convert to FPS
            long TimeMS = testtime.Milliseconds + (testtime.Seconds * 1000);
            FPS = 1000.0 / (double)TimeMS;

            overlayForm.SetAllOverlay();

            mS = TimeStr;
            Grid_SetInfos("Processing Time", TimeStr + "-" + FPS.ToString("00") + "FPS");
            CheckTime = DateTime.Now;

            if (GameStruc_0.IsInGame())
            {
                TimeSpan Checkkt = (DateTime.Now - Form1_0.GameStartedTime);
                method_GameTimeLabel(Checkkt.Minutes.ToString("00") + ":" + Checkkt.Seconds.ToString("00") + ":" + Checkkt.Milliseconds.ToString("0"));
            }
            /*else
            {
                method_GameTimeLabel("");
            }*/
            Grid_SetInfos("Scanned", ItemsStruc_0.ItemsScanned.ToString());
            Grid_SetInfos("On ground", ItemsStruc_0.ItemsOnGround.ToString());
            Grid_SetInfos("Equipped", ItemsStruc_0.ItemsEquiped.ToString());
            Grid_SetInfos("InInventory", ItemsStruc_0.ItemsInInventory.ToString());
            Grid_SetInfos("InBelt", ItemsStruc_0.ItemsInBelt.ToString());
        }

        public string CurrentGameTime = "";

        public void method_GameTimeLabel(string string_3)
        {
            //try
            //{
            /*if (Form1_0.labelGameTime.InvokeRequired)
            {
                // Call this same method but append THREAD2 to the text
                Action safeWrite = delegate { method_GameTimeLabel(string_3); };
                Form1_0.labelGameTime.Invoke(safeWrite);
            }
            else
            {
                Form1_0.labelGameTime.Text = string_3;
                Application.DoEvents();
            }*/
            try 
            {
                CurrentGameTime = string_3;
                Form1_0.labelGameTime.Text = string_3;
            }
            catch { }
        }

        public void WaitDelay(int DelayTime)
        {
            DateTime TimeStart = DateTime.Now;
            TimeSpan ThisTime = DateTime.Now - TimeStart;

            int CurrentWait = 0;
            //while (CurrentWait < DelayTime)
            while (ThisTime.TotalMilliseconds < (DelayTime * 10))
            {
                SetProcessingTime();
                Thread.Sleep(1);
                Application.DoEvents();
                ThisTime = DateTime.Now - TimeStart;
                CurrentWait++;
            }
        }

        public void SetPlayButtonText(string string_3)
        {
            //try
            //{
            if (button1.InvokeRequired)
            {
                // Call this same method but append THREAD2 to the text
                Action safeWrite = delegate { SetPlayButtonText(string_3); };
                button1.Invoke(safeWrite);
            }
            else
            {
                button1.Text = string_3;
                Application.DoEvents();
            }
            //}
            //catch { }
        }

        public void SetSettingButton(bool Enabledd)
        {
            //try
            //{
            if (button3.InvokeRequired)
            {
                // Call this same method but append THREAD2 to the text
                Action safeWrite = delegate { SetSettingButton(Enabledd); };
                button3.Invoke(safeWrite);
            }
            else
            {
                button3.Enabled = Enabledd;
                Application.DoEvents();
            }

            SetItemsButton(Enabledd);
            SetCharButtonEnable(Enabledd);
            //}
            //catch { }
        }
        public void SetItemsButton(bool Enabledd)
        {
            //try
            //{
            if (button4.InvokeRequired)
            {
                // Call this same method but append THREAD2 to the text
                Action safeWrite = delegate { SetItemsButton(Enabledd); };
                button4.Invoke(safeWrite);
            }
            else
            {
                button4.Enabled = Enabledd;
                Application.DoEvents();
            }
            //}
            //catch { }
        }

        public void StopBot()
        {
            SetPlayButtonText("START");
            Running = false;
            HasPointers = false;
            PlayerScan_0.FoundPlayer = false;
            LoopDone = 0;
            Stash_0.StashFull = false;
            SetSettingButton(true);
            LoopTimer.Stop();
            //MapAreaStruc_0.AllMapData.Clear();
            overlayForm.ClearAllOverlay();
            SetGameStatus("STOPPED");

            //SetStartButtonEnable(false);
        }

        public void button1_Click(object sender, EventArgs e)
        {
            if (!Running && button1.Enabled)
            {
                SetSettingButton(false);
                SetPlayButtonText("STOP");
                Running = true;
                BotJustStarted = true;
                Startt();
            }
            else if (Running)
            {
                StopBot();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SettingsLoader_0.SaveCurrentSettings();
            SettingsLoader_0.SaveOthersSettings();
            KeyMouse.UnhookWindowsHookEx(KeyMouse_0.hookID);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FormSettings FormSettings_0 = new FormSettings(Form1_0);
            FormSettings_0.ShowDialog();
        }

        private void charSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormCharSettings FormCharSettings_0 = new FormCharSettings(Form1_0);
            FormCharSettings_0.ShowDialog();
        }

        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl2.SelectedIndex == 0) ItemsStruc_0.DebugItems();
            if (tabControl2.SelectedIndex == 1) Form1_0.MobsStruc_0.DebuggingMobs = true;
            if (tabControl2.SelectedIndex == 2) ObjectsStruc_0.DebugObjects();
            if (tabControl2.SelectedIndex == 3) MapAreaStruc_0.DebugMapData();
            if (tabControl2.SelectedIndex == 4) PathFinding_0.DebugMapCollision();

            if (tabControl2.SelectedIndex != 1) Form1_0.MobsStruc_0.DebuggingMobs = false;
        }

        public void SetDebugMenu()
        {
            if (DebugMenuStyle == 0)
            {
                this.Size = new System.Drawing.Size(357, 446);
            }
            else if (DebugMenuStyle == 1)
            {
                this.Size = new System.Drawing.Size(570, 446);
            }
            else if(DebugMenuStyle == 2)
            {
                this.Size = new System.Drawing.Size(570, 678);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (DebugMenuStyle < 2) DebugMenuStyle++;
            else DebugMenuStyle = 0;
            SetDebugMenu();

            if (DebugMenuStyle == 2) tabControl2_SelectedIndexChanged(null, null);
        }


        public void AppendTextDebugItems(string ThisT)
        {
            if (richTextBoxDebugItems.InvokeRequired)
            {
                // Call this same method but append THREAD2 to the text
                Action safeWrite = delegate { AppendTextDebugItems(ThisT); };
                richTextBoxDebugItems.Invoke(safeWrite);
            }
            else
            {
                richTextBoxDebugItems.AppendText(ThisT);
                Application.DoEvents();
            }
        }

        public void ClearDebugItems()
        {
            if (richTextBoxDebugItems.InvokeRequired)
            {
                // Call this same method but append THREAD2 to the text
                Action safeWrite = delegate { ClearDebugItems(); };
                richTextBoxDebugItems.Invoke(safeWrite);
            }
            else
            {
                richTextBoxDebugItems.Text = "";
                Application.DoEvents();
            }
        }

        public void AppendTextDebugObjects(string ThisT)
        {
            if (richTextBoxDebugObjects.InvokeRequired)
            {
                // Call this same method but append THREAD2 to the text
                Action safeWrite = delegate { AppendTextDebugObjects(ThisT); };
                richTextBoxDebugObjects.Invoke(safeWrite);
            }
            else
            {
                richTextBoxDebugObjects.AppendText(ThisT);
                Application.DoEvents();
            }
        }

        public void ClearDebugobjects()
        {
            if (richTextBoxDebugObjects.InvokeRequired)
            {
                // Call this same method but append THREAD2 to the text
                Action safeWrite = delegate { ClearDebugobjects(); };
                richTextBoxDebugObjects.Invoke(safeWrite);
            }
            else
            {
                richTextBoxDebugObjects.Text = "";
                Application.DoEvents();
            }
        }

        public void AppendTextDebugMobs(string ThisT)
        {
            if (richTextBoxDebugMobs.InvokeRequired)
            {
                // Call this same method but append THREAD2 to the text
                Action safeWrite = delegate { AppendTextDebugMobs(ThisT); };
                richTextBoxDebugMobs.Invoke(safeWrite);
            }
            else
            {
                richTextBoxDebugMobs.AppendText(ThisT);
                Application.DoEvents();
            }
        }

        public void ClearDebugMobs()
        {
            if (richTextBoxDebugMobs.InvokeRequired)
            {
                // Call this same method but append THREAD2 to the text
                Action safeWrite = delegate { ClearDebugMobs(); };
                richTextBoxDebugMobs.Invoke(safeWrite);
            }
            else
            {
                richTextBoxDebugMobs.Text = "";
                Application.DoEvents();
            }
        }

        public void AppendTextDebugMapData(string ThisT)
        {
            if (richTextBoxDebugMapData.InvokeRequired)
            {
                // Call this same method but append THREAD2 to the text
                Action safeWrite = delegate { AppendTextDebugMapData(ThisT); };
                richTextBoxDebugMapData.Invoke(safeWrite);
            }
            else
            {
                richTextBoxDebugMapData.AppendText(ThisT);
                Application.DoEvents();
            }
        }

        public void ClearDebugMapData()
        {
            if (richTextBoxDebugMapData.InvokeRequired)
            {
                // Call this same method but append THREAD2 to the text
                Action safeWrite = delegate { ClearDebugMapData(); };
                richTextBoxDebugMapData.Invoke(safeWrite);
            }
            else
            {
                richTextBoxDebugMapData.Text = "";
                Application.DoEvents();
            }
        }

        public void AppendTextDebugCollision(string ThisT)
        {
            if (richTextBoxDebugMapCollision.InvokeRequired)
            {
                // Call this same method but append THREAD2 to the text
                Action safeWrite = delegate { AppendTextDebugCollision(ThisT); };
                richTextBoxDebugMapCollision.Invoke(safeWrite);
            }
            else
            {
                richTextBoxDebugMapCollision.AppendText(ThisT);
                Application.DoEvents();
            }
        }

        public void ClearDebugCollision()
        {
            if (richTextBoxDebugMapCollision.InvokeRequired)
            {
                // Call this same method but append THREAD2 to the text
                Action safeWrite = delegate { ClearDebugCollision(); };
                richTextBoxDebugMapCollision.Invoke(safeWrite);
            }
            else
            {
                richTextBoxDebugMapCollision.Text = "";
                Application.DoEvents();
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            FormSettings FormSettings_0 = new FormSettings(Form1_0);
            FormSettings_0.ShowDialog();
        }

        public void AppendTextErrorLogs(string ThisT, Color ThisColor)
        {
            if (richTextBoxErrorLogs.InvokeRequired)
            {
                // Call this same method but append THREAD2 to the text
                Action safeWrite = delegate { AppendTextErrorLogs(ThisT, ThisColor); };
                richTextBoxErrorLogs.Invoke(safeWrite);
            }
            else
            {
                richTextBoxErrorLogs.SelectionColor = ThisColor;
                richTextBoxErrorLogs.AppendText(ThisT + Environment.NewLine);
                Application.DoEvents();
            }
        }
        public void AppendTextGameLogs(string ThisT, Color ThisColor)
        {
            if (richTextBoxGamesLogs.InvokeRequired)
            {
                // Call this same method but append THREAD2 to the text
                Action safeWrite = delegate { AppendTextGameLogs(ThisT, ThisColor); };
                richTextBoxGamesLogs.Invoke(safeWrite);
            }
            else
            {
                richTextBoxGamesLogs.SelectionColor = ThisColor;
                richTextBoxGamesLogs.AppendText(ThisT + Environment.NewLine);
                Application.DoEvents();
            }
        }


        public void SetStartButtonEnable(bool Enabled)
        {
            if (button1.InvokeRequired)
            {
                // Call this same method but append THREAD2 to the text
                Action safeWrite = delegate { SetStartButtonEnable(Enabled); };
                button1.Invoke(safeWrite);
            }
            else
            {
                button1.Enabled = Enabled;
                Application.DoEvents();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FormItems FormItems_0 = new FormItems(Form1_0);
            FormItems_0.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FormCharSettings FormCharSettings_0 = new FormCharSettings(Form1_0);
            FormCharSettings_0.ShowDialog();
        }

        public void SetCharButtonEnable(bool Enabled)
        {
            if (button5.InvokeRequired)
            {
                // Call this same method but append THREAD2 to the text
                Action safeWrite = delegate { SetCharButtonEnable(Enabled); };
                button5.Invoke(safeWrite);
            }
            else
            {
                button5.Enabled = Enabled;
                Application.DoEvents();
            }
        }

        public void ModifyMonsterList()
        {
            string[] AllLines = File.ReadAllLines(Application.StartupPath + @"\List.txt");
            string EndTxt = "";
            EndTxt += "public enum MonsterType" + Environment.NewLine;
            EndTxt += "{" + Environment.NewLine;

            for (int i = 0; i < AllLines.Length; i++)
            {
                if (AllLines[i].Length > 0)
                {
                    //EndTxt += AllLines[i].Substring(0, AllLines[i].IndexOf('\t'));
                    AllLines[i] = AllLines[i].Substring(AllLines[i].IndexOf('\t') + 1);
                    string ThidID = AllLines[i].Substring(0, AllLines[i].IndexOf('\t'));

                    AllLines[i] = AllLines[i].Substring(AllLines[i].IndexOf('\t') + 1);
                    AllLines[i] = AllLines[i].Substring(AllLines[i].IndexOf('\t') + 1);
                    AllLines[i] = AllLines[i].Substring(AllLines[i].IndexOf('\t') + 1);
                    AllLines[i] = AllLines[i].Substring(AllLines[i].IndexOf('\t') + 1);
                    string ThidName = AllLines[i].Substring(0, AllLines[i].IndexOf('\t'));

                    if (ThidName == "dummy" ||  ThidName == "Dummy" || ThidName == "unused" || ThidName == "Unused" || ThidName == "")
                    {
                        AllLines[i] = AllLines[i].Substring(AllLines[i].IndexOf('\t') + 1);
                        ThidName = AllLines[i].Substring(0, AllLines[i].IndexOf('\t'));
                    }

                    EndTxt += "\t" + ThidName.Replace(" ", "") + " = " + ThidID + "," + Environment.NewLine;
                }
            }
            EndTxt += "}";

            File.Create(Application.StartupPath + @"\List2.txt").Dispose();
            File.WriteAllText(Application.StartupPath + @"\List2.txt", EndTxt);
        }
    }
}
