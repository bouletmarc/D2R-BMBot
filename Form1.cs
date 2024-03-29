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

        public string BotVersion = "V1.2";

        public string D2_LOD_113C_Path = @"C:\Users\boule\OneDrive\Bureau\JEUX\DiabloIICOCO\";

        public Process process;
        public string ThisEndPath = Application.StartupPath + @"\Extracted\";
        public Dictionary<string, IntPtr> offsets = new Dictionary<string, IntPtr>();
        public IntPtr BaseAddress = (IntPtr)0;
        public IntPtr processHandle = (IntPtr)0;
        public byte[] buffer = new byte[0x3FFFFFF];
        public byte[] bufferRead = new byte[0];
        public System.Timers.Timer LoopTimer;
        public bool Running = false;
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

        public int CurrentGameNumberSinceStart = 1;

        public bool ForceSwitch2ndPlayer = false;

        public double FPS = 0;

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
        public Chaos Chaos_0;
        public Duriel Duriel_0;
        public Battle Battle_0;
        public KeyMouse KeyMouse_0;
        public Summoner Summoner_0;
        public Baal Baal_0;
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
        public MoveToPath MoveToPath_0;

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
            richTextBox2.Visible = false;

            LoopTimer = new System.Timers.Timer(1);
            LoopTimer.Elapsed += new ElapsedEventHandler(timer_Elapsed);

            ScreenX = Screen.PrimaryScreen.Bounds.Width;
            ScreenY = Screen.PrimaryScreen.Bounds.Height;

            if (Form1_0.D2_LOD_113C_Path == "")
            {
                method_1("ERROR: Diablo2 LOD 1.13C Path NOT SET CORRECTLY!", Color.Red);
                method_1("Clic on the settings button and set the path where Diablo2 1.13c (the old legacy diablo2) is located!", Color.Red);
                method_1("Make sure the path don't contain any whitespace!", Color.Red);
            }

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
            Chaos_0 = new Chaos();
            Battle_0 = new Battle();
            KeyMouse_0 = new KeyMouse();
            Duriel_0 = new Duriel();
            Baal_0 = new Baal();
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
            MoveToPath_0 = new MoveToPath();

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
            Chaos_0.SetForm1(Form1_0);
            Duriel_0.SetForm1(Form1_0);
            Battle_0.SetForm1(Form1_0);
            KeyMouse_0.SetForm1(Form1_0);
            Mephisto_0.SetForm1(Form1_0);
            Baal_0.SetForm1(Form1_0);
            Andariel_0.SetForm1(Form1_0);
            Countess_0.SetForm1(Form1_0);
            MercStruc_0.SetForm1(Form1_0);
            StashStruc_0.SetForm1(Form1_0);
            Cubing_0.SetForm1(Form1_0);
            Gamble_0.SetForm1(Form1_0);
            LowerKurast_0.SetForm1(Form1_0);
            SettingsLoader_0.SetForm1(Form1_0);
            MapAreaStruc_0.SetForm1(Form1_0);
            MoveToPath_0.SetForm1(Form1_0);

            SettingsLoader_0.LoadSettings();

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
            if (labelGames.InvokeRequired)
            {
                // Call this same method but append THREAD2 to the text
                Action safeWrite = delegate { SetGamesText(); };
                labelGames.Invoke(safeWrite);
            }
            else
            {
                labelGames.Text = CurrentGameNumberSinceStart.ToString() + " entered. " + CurrentGameNumberFullyDone.ToString() + " fully done";
            }
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
                    GetWindowRect(hWnd, out D2Rect);
                    //ScreenX = Screen.PrimaryScreen.Bounds.Width;
                    //ScreenY = Screen.PrimaryScreen.Bounds.Height;
                    CenterX = ScreenX / 2;
                    CenterY = ScreenY / 2;
                    D2Widht = D2Rect.Width;
                    D2Height = D2Rect.Height;
                    ScreenXOffset = D2Rect.Location.X;
                    ScreenYOffset = D2Rect.Location.Y;

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

                    //PlayerScan_0.scanForPlayer(false);
                    //PlayerScan_0.GetPositions();
                    //method_1("Player pos: " + PlayerScan_0.xPosFinal + "," + PlayerScan_0.yPosFinal, Color.Black);
                    //UIScan_0.readUI();
                    //Console.WriteLine(UIScan_0.npcInteract);
                    //MobsStruc_0.GetMobs("", "", true, 50, new List<long>() { });
                    //Thread.Sleep(3000);
                    //HoverStruc_0.GetHovering(); //only when item is on ground not on hands
                    //method_1("hover: " + HoverStruc_0.lastHoveredType + ", " + HoverStruc_0.lastHoveredUnitId + ", " + HoverStruc_0.isHovered, Color.BlueViolet);
                    //ItemsStruc_0.GetItems(false);
                    //ItemsStruc_0.GetItems(true);
                    //BeltStruc_0.CheckForMissingPotions();
                    //Potions_0.CheckIfWeUsePotion();
                    //ObjectsStruc_0.GetObjects("", false);
                    //NPCStruc_0.GetNPC("noname");
                    /*if (ObjectsStruc_0.GetObjects("Act3TownWaypoint", false))
                    {
                        method_1("WP: " + ObjectsStruc_0.itemx + ", " + ObjectsStruc_0.itemy, Color.DarkOrchid);
                    }
                    ObjectsStruc_0.GetObjects("", false);*/
                    //Form1_0.ObjectsStruc_0.GetObjects("AllChests", true, new List<uint>(), 300);

                    //##############################
                    //GRAB AND KEEP ITEM SHOULD NOT BE IDENTICAL
                    //ignored tp
                    //baal pos not detected
                    //BO script
                    //gamble script
                    //merc hp not correct
                    //merc potion sending not correct (shift key)
                    //##############################


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

        public void RunScriptNOTInGame()
        {
            Form1_0.GameStruc_0.CreateNewGame();
        }

        public void SetNewGame()
        {
            Form1_0.SetGameStatus("NEW GAME STARTED");

            PatternsScan_0.StartIndexItemLast = long.MaxValue;
            PatternsScan_0.ScanUnitsNumber = 2600;
            Town_0.TriedToShopCount = 0;
            Town_0.TriedToShopCount2 = 0;
            Town_0.TriedToMercCount = 0;
            FoundPlayerPointerTryCount = 0;
            Town_0.Towning = true;
            Town_0.IsInTown = true;
            Town_0.ForcedTowning = false;
            Town_0.FastTowning = false;
            PlayerScan_0.GetPositions();
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
            Town_0.IgnoredTPList.Clear();
            Baal_0.IgnoredTPList.Clear();
            Town_0.FirstTown = true;
            ForceSwitch2ndPlayer = false;
            PlayerScan_0.PlayerGoldInventory = 0;
            SetGameDone = false;
            BeltStruc_0.ForceMANAPotionQty = 0;
            BeltStruc_0.ForceHPPotionQty = 0;
            SetGamesText();
            CurrentGameNumber++;
            CurrentGameNumberSinceStart++;

            //##############################
            MapAreaStruc_0.ScanMapStruc();
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            LoopTimer.Stop();
            CheckTime = DateTime.Now;

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
                            WaitDelay(400); //wait here because 'loading' menu is not correct
                            Town_0.GetCorpse();
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
                                WaitDelay(400); //wait here because 'loading' menu is not correct
                                Town_0.GetCorpse();
                                HasPointers = true;
                            }
                            else
                            {
                                FoundPlayerPointerTryCount++;

                                if (FoundPlayerPointerTryCount >= 300)
                                {
                                    method_1("Leaving Player pointer not found!", Color.Red);
                                    Form1_0.Potions_0.ForceLeave = true;
                                    ForceSwitch2ndPlayer = true;
                                    Form1_0.Baal_0.SearchSameGamesAsLastOne = false;
                                    Form1_0.Chaos_0.SearchSameGamesAsLastOne = false;
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
                                /*if (UIScan_0.deadMenu || PlayerScan_0.PlayerHP == 0)
                                {
                                    method_1("PLAYER DIED!!!", Color.Red);
                                    CharDied = true;
                                    LeaveGame();
                                }*/

                                //HoverStruc_0.GetHovering();
                                bool runnn = true;
                                if (runnn)
                                {
                                    //#####
                                    /*if (CharConfig.RunLowerKurastScript && LowerKurast_0.ScriptDone)
                                    {
                                        LeaveGame(true);
                                    }*/
                                    //#####

                                    //LowerKurast_0.TakeChest();
                                    //Form1_0.ObjectsStruc_0.GetObjects("JungleMediumChestLeft", true, new List<uint>() { }, 400);

                                    //MoveToPath_0.MoveToArea(20);
                                    //return;

                                    if (!ItemsStruc_0.GetItems(true))
                                    {
                                        //BeltStruc_0.CheckForMissingPotions();
                                        if (Town_0.Towning)
                                        {
                                            ItemsStruc_0.GetItems(false);
                                            Town_0.RunTownScript();
                                        }
                                        else
                                        {
                                            if (Battle_0.ClearingArea && !CharConfig.RunItemGrabScriptOnly)
                                            {
                                                Battle_0.RunBattleScript();
                                            }
                                            else
                                            {
                                                if (CharConfig.RunAndarielScript && !Andariel_0.ScriptDone)
                                                {
                                                    Andariel_0.RunScript();
                                                }
                                                else
                                                {
                                                    if (CharConfig.RunCountessScript && !Countess_0.ScriptDone)
                                                    {
                                                        Countess_0.RunScript();
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
                                                                    if (CharConfig.RunMephistoScript && !Mephisto_0.ScriptDone)
                                                                    {
                                                                        Mephisto_0.RunScript();
                                                                    }
                                                                    else
                                                                    {
                                                                        /*if (CharConfig.RunLowerKurastScript && LowerKurast_0.ScriptDone)
                                                                        {
                                                                            LeaveGame(true); //#####
                                                                        }*/
                                                                        if (CharConfig.RunChaosScript && !Chaos_0.ScriptDone)
                                                                        {
                                                                            Chaos_0.RunScript();
                                                                        }
                                                                        else
                                                                        {
                                                                            if (CharConfig.RunBaalLeechScript && !Baal_0.ScriptDone)
                                                                            {
                                                                                Baal_0.RunScript();
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
                                else
                                {
                                    ObjectsStruc_0.GetObjects("TownPortal", true);
                                }
                                Potions_0.CheckIfWeUsePotion();
                                if (!CharConfig.RunItemGrabScriptOnly) GameStruc_0.CheckChickenGameTime();


                                Grid_SetInfos("Scanned", ItemsStruc_0.ItemsScanned.ToString());
                                Grid_SetInfos("On ground", ItemsStruc_0.ItemsOnGround.ToString());
                                Grid_SetInfos("Equipped", ItemsStruc_0.ItemsEquiped.ToString());
                                Grid_SetInfos("InInventory", ItemsStruc_0.ItemsInInventory.ToString());
                                Grid_SetInfos("InBelt", ItemsStruc_0.ItemsInBelt.ToString());
                            }
                        }
                    }
                }
                else
                {
                    Potions_0.ForceLeave = false;
                    FoundPlayerPointerTryCount = 0;
                    HasPointers = false;

                    if (!PrintedGameTime)
                    {
                        GameStruc_0.LogGameTime();
                        TimeSinceSearchingForGames = DateTime.Now;
                        PrintedGameTime = true;
                    }

                    ChangeCharScript();

                    if (CharConfig.RunGameMakerScript)
                    {
                        Form1_0.SetGameStatus("CREATING GAME");
                        RunScriptNOTInGame();
                    }
                    else
                    {
                        //Chaos_0.RunScriptNOTInGame();
                        if (CharConfig.RunBaalSearchGameScript && !CharConfig.RunItemGrabScriptOnly)
                        {
                            Form1_0.SetGameStatus("SEARCHING GAMES");
                            Baal_0.RunScriptNOTInGame();

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
                            Chaos_0.RunScriptNOTInGame();

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

            SetProcessingTime();

            if (Running) LoopTimer.Start();
            //if (Running && LoopDone < 1) LoopTimer.Start();
            //LoopDone++;
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

            if (name != CharConfig.PlayerCharName || ForceSwitch2ndPlayer)
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


            Grid_SetInfos("Processing Time", TimeStr + "-" + FPS.ToString("00") + "FPS");
            CheckTime = DateTime.Now;
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

        public void StopBot()
        {
            //SetGameStatus("STOPPED");
            button1.Text = "START";
            Running = false;
            HasPointers = false;
            PlayerScan_0.FoundPlayer = false;
            LoopDone = 0;
            Stash_0.StashFull = false;
            button3.Enabled = true;
            LoopTimer.Stop();
            SetGameStatus("STOPPED");
        }

        public void button1_Click(object sender, EventArgs e)
        {
            if (!Running)
            {
                button3.Enabled = false;
                button1.Text = "STOP";
                Running = true;
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

        private void button2_Click(object sender, EventArgs e)
        {
            if (!richTextBox2.Visible)
            {
                richTextBox2.Visible = true;
                richTextBox1.Visible = false;
            }
            else
            {
                richTextBox2.Visible = false;
                richTextBox1.Visible = true;
            }
            Application.DoEvents();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FormSettings FormSettings_0 = new FormSettings(Form1_0);
            FormSettings_0.ShowDialog();
        }
    }
}
