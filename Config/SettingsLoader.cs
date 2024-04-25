using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace app
{
    public class SettingsLoader
    {
        Form1 Form1_0;

        public string File_PaladinHammer = Application.StartupPath + @"\Settings\Char\PaladinHammer.txt";
        public string File_SorceressBlizzard = Application.StartupPath + @"\Settings\Char\SorceressBlizzard.txt";

        public string File_CharSettings = Application.StartupPath + @"\Settings\CharSettings.txt";
        public string File_BotSettings = Application.StartupPath + @"\Settings\BotSettings.txt";
        public string File_ItemsSettings = Application.StartupPath + @"\Settings\ItemsSettings.txt";
        public string File_CubingSettings = Application.StartupPath + @"\Settings\CubingRecipes.txt";
        public string File_Settings = Application.StartupPath + @"\Settings\Settings.txt";
        public string[] AllLines = new string[] { };

        public void SetForm1(Form1 form1_1)
        {
            Form1_0 = form1_1;
        }

        public void LoadSettings()
        {
            LoadThisFileSettings(File_CharSettings);
            //###################
            ReloadCharSettings();
            //###################
            //#####
            LoadThisFileSettings(File_BotSettings);
            LoadThisFileSettings(File_ItemsSettings);
            LoadThisFileSettings(File_CubingSettings);
            //#####
            if (File.Exists(File_Settings))
            {
                AllLines = File.ReadAllLines(File_Settings);
                LoadOthersSettings();
            }
            else
            {
                SaveOthersSettings();
            }
        }

        public void LoadThisFileSettings(string ThisFilePath)
        {
            if (File.Exists(ThisFilePath))
            {
                AllLines = File.ReadAllLines(ThisFilePath);
                
                if (Path.GetFileName(ThisFilePath) == "CharSettings.txt") LoadCharSettings();
                if (Path.GetFileName(ThisFilePath) == "BotSettings.txt") LoadBotSettings();
                if (Path.GetFileName(ThisFilePath) == "ItemsSettings.txt") LoadCubingSettings();
                if (Path.GetFileName(ThisFilePath) == "CubingRecipes.txt") LoadCubingSettings();

                if (Path.GetFileName(ThisFilePath) == "PaladinHammer.txt") LoadCurrentCharSettings();
                if (Path.GetFileName(ThisFilePath) == "SorceressBlizzard.txt") LoadCurrentCharSettings();
            }
            else
            {
                Form1_0.method_1("Unable to find '" + Path.GetFileName(ThisFilePath) + "' file!", Color.Red);
            }
        }

        public void LoadOthersSettings()
        {
            try
            {
                for (int i = 0; i < AllLines.Length; i++)
                {
                    if (AllLines[i].Length > 0)
                    {
                        if (AllLines[i][0] != '/' && AllLines[i][0] != '#')
                        {
                            if (AllLines[i].Contains("="))
                            {
                                string[] Params = AllLines[i].Split('=');

                                if (Params[0].Contains("RunNumber"))
                                {
                                    Form1_0.CurrentGameNumber = int.Parse(Params[1]);
                                }
                                if (Params[0].Contains("D2_LOD_113C_Path"))
                                {
                                    Form1_0.D2_LOD_113C_Path = Params[1];
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
                Form1_0.method_1("Unable to load 'Settings.txt' file!", Color.Red);
            }
        }

        public void ReloadCharSettings()
        {
            if (CharConfig.RunningOnChar == "PaladinHammer") LoadThisFileSettings(File_PaladinHammer);
            else if (CharConfig.RunningOnChar == "SorceressBlizzard") LoadThisFileSettings(File_SorceressBlizzard);
        }

        public void ReloadCharSettingsFromThisFile(string ThisFilePath)
        {
            LoadThisFileSettings(ThisFilePath);
        }

        public void SaveCharSettings()
        {
            string ThisFilePath = "";
            if (CharConfig.RunningOnChar == "PaladinHammer") ThisFilePath = File_PaladinHammer;
            if (CharConfig.RunningOnChar == "SorceressBlizzard") ThisFilePath = File_SorceressBlizzard;

            string[] AllLines = File.ReadAllLines(ThisFilePath);
            for (int i = 0; i < AllLines.Length; i++)
            {
                if (AllLines[i].Contains("="))
                {
                    string[] Splitted = AllLines[i].Split('=');
                    if (Splitted[0] == "KeySkillAttack") AllLines[i] = "KeySkillAttack=" + CharConfig.KeySkillAttack;
                    if (Splitted[0] == "KeySkillAura") AllLines[i] = "KeySkillAura=" + CharConfig.KeySkillAura;
                    if (Splitted[0] == "KeySkillfastMoveAtTown") AllLines[i] = "KeySkillfastMoveAtTown=" + CharConfig.KeySkillfastMoveAtTown;
                    if (Splitted[0] == "KeySkillfastMoveOutsideTown") AllLines[i] = "KeySkillfastMoveOutsideTown=" + CharConfig.KeySkillfastMoveOutsideTown;
                    if (Splitted[0] == "KeySkillDefenseAura") AllLines[i] = "KeySkillDefenseAura=" + CharConfig.KeySkillDefenseAura;
                    if (Splitted[0] == "KeySkillCastDefense") AllLines[i] = "KeySkillCastDefense=" + CharConfig.KeySkillCastDefense;
                    if (Splitted[0] == "KeySkillLifeAura") AllLines[i] = "KeySkillLifeAura=" + CharConfig.KeySkillLifeAura;

                    if (Splitted[0] == "BeltPotTypeToHave") AllLines[i] = "BeltPotTypeToHave=" + CharConfig.BeltPotTypeToHave[0] + "," + CharConfig.BeltPotTypeToHave[1] + "," + CharConfig.BeltPotTypeToHave[2] + "," + CharConfig.BeltPotTypeToHave[3];

                    /*InventoryDontCheckItem=
                    {
                    0,0,0,0,0,0,0,1,1,1,
                    0,0,0,0,0,0,0,1,1,1,
                    0,0,0,0,0,0,0,1,1,1,
                    0,0,0,0,0,0,0,1,1,1
                    }*/
                    if (Splitted[0] == "InventoryDontCheckItem")
                    {
                        string InventoryTxtt = "";
                        InventoryTxtt += Environment.NewLine + "{" + Environment.NewLine;
                        for (int w = 0; w < 40; w++)
                        {
                            if (w == 10) InventoryTxtt += Environment.NewLine;
                            if (w == 20) InventoryTxtt += Environment.NewLine;
                            if (w == 30) InventoryTxtt += Environment.NewLine;

                            InventoryTxtt += CharConfig.InventoryDontCheckItem[w];

                            if (w < 40 - 1) InventoryTxtt += ",";
                        }
                        AllLines[i] = "InventoryDontCheckItem=" + InventoryTxtt;
                    }

                    if (Splitted[0] == "PlayerCharName") AllLines[i] = "PlayerCharName=" + CharConfig.PlayerCharName;
                    if (Splitted[0] == "UseTeleport") AllLines[i] = "UseTeleport=" + CharConfig.UseTeleport;
                    if (Splitted[0] == "UseBO") AllLines[i] = "UseBO=" + CharConfig.UseBO;
                    if (Splitted[0] == "IDAtShop") AllLines[i] = "IDAtShop=" + CharConfig.IDAtShop;
                    if (Splitted[0] == "GrabForGold") AllLines[i] = "GrabForGold=" + CharConfig.GrabForGold;
                    if (Splitted[0] == "LeaveDiabloClone") AllLines[i] = "LeaveDiabloClone=" + CharConfig.LeaveDiabloClone;
                    if (Splitted[0] == "GambleGold") AllLines[i] = "GambleGold=" + CharConfig.GambleGold;
                    if (Splitted[0] == "ChickenHP") AllLines[i] = "ChickenHP=" + CharConfig.ChickenHP;
                    if (Splitted[0] == "TakeHPPotUnder" && !Splitted[0].Contains("MercTakeHPPotUnder")) AllLines[i] = "TakeHPPotUnder=" + CharConfig.TakeHPPotUnder;
                    if (Splitted[0] == "TakeRVPotUnder") AllLines[i] = "TakeRVPotUnder=" + CharConfig.TakeRVPotUnder;
                    if (Splitted[0] == "TakeManaPotUnder") AllLines[i] = "TakeManaPotUnder=" + CharConfig.TakeManaPotUnder;
                    if (Splitted[0] == "GambleAboveGoldAmount") AllLines[i] = "GambleAboveGoldAmount=" + CharConfig.GambleAboveGoldAmount;
                    if (Splitted[0] == "GambleUntilGoldAmount") AllLines[i] = "GambleUntilGoldAmount=" + CharConfig.GambleUntilGoldAmount;
                    if (Splitted[0] == "PlayerAttackWithRightHand") AllLines[i] = "PlayerAttackWithRightHand=" + CharConfig.PlayerAttackWithRightHand;

                    if (Splitted[0] == "KeysLocationInInventory") AllLines[i] = "KeysLocationInInventory=" + CharConfig.KeysLocationInInventory.Item1 + "," + CharConfig.KeysLocationInInventory.Item2;

                    if (Splitted[0] == "UsingMerc") AllLines[i] = "UsingMerc=" + CharConfig.UsingMerc;
                    if (Splitted[0] == "MercTakeHPPotUnder") AllLines[i] = "MercTakeHPPotUnder=" + CharConfig.MercTakeHPPotUnder;
                }
            }

            File.Create(ThisFilePath).Dispose();
            File.WriteAllLines(ThisFilePath, AllLines);

            Form1_0.method_1("Saved '" + Path.GetFileName(ThisFilePath) + "' file!", Color.DarkGreen);

            SaveCurrentCharSettings();
        }

        public void SaveCurrentSettings()
        {
            string[] AllLines = File.ReadAllLines(File_BotSettings);

            for (int i = 0; i < AllLines.Length; i++)
            {
                if (AllLines[i].Contains("="))
                {
                    string[] Splitted = AllLines[i].Split('=');
                    if (Splitted[0] == "MaxGameTime") AllLines[i] = "MaxGameTime=" + CharConfig.MaxGameTime;

                    if (Splitted[0] == "IsRushing") AllLines[i] = "IsRushing=" + CharConfig.IsRushing;
                    if (Splitted[0] == "RushLeecherName") AllLines[i] = "RushLeecherName=" + CharConfig.RushLeecherName;

                    //#########
                    if (Splitted[0] == "KillBaal") AllLines[i] = "KillBaal=" + Form1_0.Baal_0.KillBaal;
                    if (Splitted[0] == "LeaveIfMobsIsPresent_ID")
                    {
                        AllLines[i] = "LeaveIfMobsIsPresent_ID=";
                        for (int j = 0; j < Form1_0.Baal_0.LeaveIfMobsIsPresent_ID.Count; j++)
                        {
                            if (j > 0) AllLines[i] += ",";
                            AllLines[i] += Form1_0.Baal_0.LeaveIfMobsIsPresent_ID[j];
                        }
                    }
                    if (Splitted[0] == "LeaveIfMobsIsPresent_Count")
                    {
                        AllLines[i] = "LeaveIfMobsIsPresent_Count=";
                        for (int j = 0; j < Form1_0.Baal_0.LeaveIfMobsIsPresent_Count.Count; j++)
                        {
                            if (j > 0) AllLines[i] += ",";
                            AllLines[i] += Form1_0.Baal_0.LeaveIfMobsIsPresent_Count[j];
                        }
                    }
                    if (Splitted[0] == "LeaveIfMobsCountIsAbove") AllLines[i] = "LeaveIfMobsCountIsAbove=" + Form1_0.Baal_0.LeaveIfMobsCountIsAbove;
                    if (Splitted[0] == "SafeHealingStrat") AllLines[i] = "SafeHealingStrat=" + Form1_0.Baal_0.SafeYoloStrat;
                    //#########

                    if (Splitted[0] == "RunMapHackOnly") AllLines[i] = "RunMapHackOnly=" + CharConfig.RunMapHackOnly;
                    if (Splitted[0] == "RunDarkWoodRush") AllLines[i] = "RunDarkWoodRush=" + CharConfig.RunDarkWoodRush;
                    if (Splitted[0] == "RunTristramRush") AllLines[i] = "RunTristramRush=" + CharConfig.RunTristramRush;
                    if (Splitted[0] == "RunAndarielRush") AllLines[i] = "RunAndarielRush=" + CharConfig.RunAndarielRush;
                    if (Splitted[0] == "RunRadamentRush") AllLines[i] = "RunRadamentRush=" + CharConfig.RunRadamentRush;
                    if (Splitted[0] == "RunHallOfDeadRush") AllLines[i] = "RunHallOfDeadRush=" + CharConfig.RunHallOfDeadRush;
                    if (Splitted[0] == "RunFarOasisRush") AllLines[i] = "RunFarOasisRush=" + CharConfig.RunFarOasisRush;
                    if (Splitted[0] == "RunLostCityRush") AllLines[i] = "RunLostCityRush=" + CharConfig.RunLostCityRush;
                    if (Splitted[0] == "RunSummonerRush") AllLines[i] = "RunSummonerRush=" + CharConfig.RunSummonerRush;
                    if (Splitted[0] == "RunDurielRush") AllLines[i] = "RunDurielRush=" + CharConfig.RunDurielRush;
                    if (Splitted[0] == "RunKahlimEyeRush") AllLines[i] = "RunKahlimEyeRush=" + CharConfig.RunKahlimEyeRush;
                    if (Splitted[0] == "RunKahlimBrainRush") AllLines[i] = "RunKahlimBrainRush=" + CharConfig.RunKahlimBrainRush;
                    if (Splitted[0] == "RunKahlimHeartRush") AllLines[i] = "RunKahlimHeartRush=" + CharConfig.RunKahlimHeartRush;
                    if (Splitted[0] == "RunTravincalRush") AllLines[i] = "RunTravincalRush=" + CharConfig.RunTravincalRush;
                    if (Splitted[0] == "RunMephistoRush") AllLines[i] = "RunMephistoRush=" + CharConfig.RunMephistoRush;
                    if (Splitted[0] == "RunChaosRush") AllLines[i] = "RunChaosRush=" + CharConfig.RunChaosRush;
                    if (Splitted[0] == "RunAncientsRush") AllLines[i] = "RunAncientsRush=" + CharConfig.RunAncientsRush;
                    if (Splitted[0] == "RunBaalRush") AllLines[i] = "RunBaalRush=" + CharConfig.RunBaalRush;

                    if (Splitted[0] == "ShowOverlay") AllLines[i] = "ShowOverlay=" + CharConfig.ShowOverlay;

                    if (Splitted[0] == "RunWPTaker") AllLines[i] = "RunWPTaker=" + CharConfig.RunWPTaker;
                    if (Splitted[0] == "RunTravincalScript") AllLines[i] = "RunTravincalScript=" + CharConfig.RunTravincalScript;
                    if (Splitted[0] == "RunPindleskinScript") AllLines[i] = "RunPindleskinScript=" + CharConfig.RunPindleskinScript;
                    if (Splitted[0] == "RunDurielScript") AllLines[i] = "RunDurielScript=" + CharConfig.RunDurielScript;
                    if (Splitted[0] == "RunSummonerScript") AllLines[i] = "RunSummonerScript=" + CharConfig.RunSummonerScript;
                    if (Splitted[0] == "RunMephistoScript") AllLines[i] = "RunMephistoScript=" + CharConfig.RunMephistoScript;
                    if (Splitted[0] == "RunAndarielScript") AllLines[i] = "RunAndarielScript=" + CharConfig.RunAndarielScript;
                    if (Splitted[0] == "RunCountessScript") AllLines[i] = "RunCountessScript=" + CharConfig.RunCountessScript;
                    if (Splitted[0] == "RunChaosScript") AllLines[i] = "RunChaosScript=" + CharConfig.RunChaosScript;
                    if (Splitted[0] == "RunChaosLeechScript") AllLines[i] = "RunChaosLeechScript=" + CharConfig.RunChaosLeechScript;
                    if (Splitted[0] == "RunLowerKurastScript") AllLines[i] = "RunLowerKurastScript=" + CharConfig.RunLowerKurastScript;
                    if (Splitted[0] == "RunBaalScript") AllLines[i] = "RunBaalScript=" + CharConfig.RunBaalScript;
                    if (Splitted[0] == "RunBaalLeechScript") AllLines[i] = "RunBaalLeechScript=" + CharConfig.RunBaalLeechScript;
                    if (Splitted[0] == "RunItemGrabScriptOnly") AllLines[i] = "RunItemGrabScriptOnly=" + CharConfig.RunItemGrabScriptOnly;
                    if (Splitted[0] == "RunCowsScript") AllLines[i] = "RunCowsScript=" + CharConfig.RunCowsScript;
                    if (Splitted[0] == "RunEldritchScript") AllLines[i] = "RunEldritchScript=" + CharConfig.RunEldritchScript;
                    if (Splitted[0] == "RunShenkScript") AllLines[i] = "RunShenkScript=" + CharConfig.RunShenkScript;
                    if (Splitted[0] == "RunNihlatakScript") AllLines[i] = "RunNihlatakScript=" + CharConfig.RunNihlatakScript;

                    if (Splitted[0] == "RunChaosSearchGameScript") AllLines[i] = "RunChaosSearchGameScript=" + CharConfig.RunChaosSearchGameScript;
                    if (Splitted[0] == "RunBaalSearchGameScript") AllLines[i] = "RunBaalSearchGameScript=" + CharConfig.RunBaalSearchGameScript;
                    if (Splitted[0] == "RunGameMakerScript") AllLines[i] = "RunGameMakerScript=" + CharConfig.RunGameMakerScript;
                    if (Splitted[0] == "GameName") AllLines[i] = "GameName=" + CharConfig.GameName;
                    if (Splitted[0] == "GameDifficulty") AllLines[i] = "GameDifficulty=" + CharConfig.GameDifficulty;
                    if (Splitted[0] == "GamePass") AllLines[i] = "GamePass=" + CharConfig.GamePass;

                    if (Splitted[0] == "StartStopKey") AllLines[i] = "StartStopKey=" + CharConfig.StartStopKey;
                }
            }

            File.Create(File_BotSettings).Dispose();
            File.WriteAllLines(File_BotSettings, AllLines);

            Form1_0.method_1("Saved '" + Path.GetFileName(File_BotSettings) + "' file!", Color.DarkGreen);
        }

        public void SaveOthersSettings()
        {
            string SaveTxtt = "";
            SaveTxtt += "RunNumber=" + Form1_0.CurrentGameNumber + Environment.NewLine;
            SaveTxtt += "D2_LOD_113C_Path=" + Form1_0.D2_LOD_113C_Path + Environment.NewLine;

            File.Create(File_Settings).Dispose();
            File.WriteAllText(File_Settings, SaveTxtt);

            Form1_0.method_1("Saved '" + Path.GetFileName(File_Settings) + "' file!", Color.DarkGreen);
        }

        public void LoadItemsSettings()
        {
            try
            {
                bool DoingKeysRune = false;
                bool DoingNormal = false;

                Dictionary<string, bool> AllKeys = new Dictionary<string, bool>();
                Dictionary<string, bool> AllPotion = new Dictionary<string, bool>();

                //######
                Dictionary<string, bool> AllNormall_ByName = new Dictionary<string, bool>();
                Dictionary<string, Dictionary<uint, string>> AllNormall_ByName_Flags = new Dictionary<string, Dictionary<uint, string>>();
                Dictionary<string, int> AllNormall_ByName_Quality = new Dictionary<string, int>();
                Dictionary<string, Dictionary<string, int>> AllNormall_ByName_Stats = new Dictionary<string, Dictionary<string, int>>();
                Dictionary<string, Dictionary<string, string>> AllNormall_ByName_Operators = new Dictionary<string, Dictionary<string, string>>();
                Dictionary<string, string> NormalNameDesc = new Dictionary<string, string>();

                Dictionary<string, bool> AllNormall_ByType = new Dictionary<string, bool>();
                Dictionary<string, Dictionary<uint, string>> AllNormall_ByType_Flags = new Dictionary<string, Dictionary<uint, string>>();
                Dictionary<string, int> AllNormall_ByType_Quality = new Dictionary<string, int>();
                Dictionary<string, Dictionary<string, int>> AllNormall_ByType_Stats = new Dictionary<string, Dictionary<string, int>>();
                Dictionary<string, Dictionary<string, string>> AllNormal_ByType_Operators = new Dictionary<string, Dictionary<string, string>>();
                Dictionary<string, string> NormalTypeDesc = new Dictionary<string, string>();
                //######


                int CurrentNameIndex = 1;
                int CurrentTypeIndex = 1;
                for (int i = 0; i < AllLines.Length; i++)
                {
                    try
                    {
                        if (AllLines[i].Length > 0)
                        {
                            if (AllLines[i][2] != '#')
                            {
                                string ThisItem = AllLines[i];
                                bool PickItem = true;
                                string Desc = "";
                                if (AllLines[i][0] == '/')
                                {
                                    PickItem = false;
                                    ThisItem = AllLines[i].Substring(2);
                                }

                                if (ThisItem.Contains("/"))
                                {
                                    Desc = ThisItem.Substring(ThisItem.IndexOf('/'));
                                    ThisItem = ThisItem.Substring(0, ThisItem.IndexOf('/'));    //remove description '//'
                                }

                                if (DoingNormal)
                                {
                                    ThisItem = "";
                                    Desc = "";
                                    if (AllLines[i].Contains("/"))
                                    {
                                        string ThisLineeee = AllLines[i];
                                        if (ThisLineeee[1] == '/') ThisLineeee = ThisLineeee.Substring(2);
                                        if (ThisLineeee.Contains("/")) Desc = "//" + ThisLineeee.Substring(ThisLineeee.LastIndexOf('/') + 1);
                                    }
                                    string ThisLine = AllLines[i];
                                    ThisLine = ThisLine.Replace(" ", "");

                                    bool GoingByName = true;
                                    string currentName = "";

                                    Dictionary<uint, string> AllFlags_ByName = new Dictionary<uint, string>();
                                    Dictionary<string, int> AllStats_ByName = new Dictionary<string, int>();
                                    Dictionary<string, string> AllOperators_ByName = new Dictionary<string, string>();

                                    Dictionary<uint, string> AllFlags_ByType = new Dictionary<uint, string>();
                                    Dictionary<string, int> AllStats_ByType = new Dictionary<string, int>();
                                    Dictionary<string, string> AllOperators_ByType = new Dictionary<string, string>();
                                    while (ThisLine != "")
                                    {
                                        if (ThisLine[0] == '#' || ThisLine[1] == '#' || ThisLine[2] == '#' || ThisLine[3] == '#')
                                        {
                                            ThisLine = "";
                                            break;
                                        }
                                        //Console.WriteLine(ThisLine);

                                        if (ThisLine[0] == '/') ThisLine = ThisLine.Substring(2);

                                        if (ThisLine[0] == '[')
                                        {
                                            //[Type] == primalhelm && [Class] == elite && [Quality] <= superior && [Flag] != ethereal # [Sockets] == 3 && [SkillBattleOrders]+[SkillShout] >= 5 // Delirium
                                            //[Name] == BerserkerAxe
                                            string ThisParam = ThisLine.Substring(1, ThisLine.IndexOf("]") - 1);
                                            int IndexBracket = ThisLine.IndexOf("]");
                                            if (ThisLine.Contains("+"))
                                            {
                                                //[SkillBattleOrders]+[SkillShout]
                                                int AddedCount = 0;
                                                while (true)
                                                {
                                                    //Console.WriteLine(ThisLine.Substring(1).IndexOf("+") + " != " + IndexBracket);
                                                    if (ThisLine.Substring(1).IndexOf("+") == IndexBracket)
                                                    {
                                                        //Console.WriteLine(ThisLine);
                                                        if (ThisLine[1] == '=') break;
                                                        if (AddedCount > 0)
                                                        {
                                                            string MidNames = ThisLine.Substring(2, ThisLine.IndexOf("]") - 2);
                                                            ThisParam += "+" + MidNames;
                                                        }
                                                        ThisLine = ThisLine.Substring(ThisLine.IndexOf("]") + 3);
                                                        string OtherNames = ThisLine.Substring(0, ThisLine.IndexOf("]"));
                                                        ThisParam += "+" + OtherNames;
                                                        //Console.WriteLine(OtherNames);
                                                        //Console.WriteLine(ThisLine);

                                                        if (ThisLine[1] == '=') break;
                                                        ThisLine = ThisLine.Substring(ThisLine.IndexOf("]") + 1);
                                                        //Console.WriteLine(ThisLine);
                                                        if (ThisLine[1] == '=') break;
                                                        else if (ThisLine.Contains("+")) IndexBracket = ThisLine.IndexOf("]");
                                                        else break;
                                                        AddedCount++;
                                                    }
                                                    else
                                                    {
                                                        break;
                                                    }
                                                }
                                            }
                                            //Console.WriteLine("--------");
                                            //Console.WriteLine(ThisLine);

                                            string ThisOperator = "";
                                            string ThisValue = "";
                                            if (ThisLine[1] == '=')
                                            {
                                                ThisOperator = ThisLine.Substring(0, 2);
                                                if (ThisLine.Contains("&")) ThisValue = ThisLine.Substring(2, ThisLine.IndexOf("&") - 2);
                                                else ThisValue = ThisLine.Substring(2);
                                                //Console.WriteLine(ThisOperator + ThisValue);
                                            }
                                            else
                                            {
                                                ThisOperator = ThisLine.Substring(ThisLine.IndexOf("]") + 1, 2);
                                                ThisValue = ThisLine.Substring(ThisLine.IndexOf("]") + 3);
                                            }
                                            if (ThisValue.Contains("&")) ThisValue = ThisValue.Substring(0, ThisValue.IndexOf("&"));
                                            if (ThisValue.Contains("/")) ThisValue = ThisValue.Substring(0, ThisValue.IndexOf("/"));

                                            //Console.WriteLine(ThisOperator + ThisValue);

                                            if (ThisLine.Contains("&"))
                                            {
                                                ThisLine = ThisLine.Substring(ThisLine.IndexOf("&") + 2);
                                            }
                                            else
                                            {
                                                //if (ThisLine.Contains("/")) Desc = ThisLine.Substring(ThisLine.IndexOf('/'));
                                                ThisLine = "";
                                            }

                                            if (ThisParam.ToLower() == "name")
                                            {
                                                currentName = ThisValue + CurrentNameIndex;
                                                AllNormall_ByName.Add(currentName, PickItem);
                                                NormalNameDesc.Add(currentName, Desc);
                                                CurrentNameIndex++;
                                                GoingByName = true;
                                            }
                                            else if (ThisParam.ToLower() == "type")
                                            {
                                                currentName = ThisValue + CurrentTypeIndex;
                                                AllNormall_ByType.Add(currentName, PickItem);
                                                NormalTypeDesc.Add(currentName, Desc);
                                                CurrentTypeIndex++;
                                                GoingByName = false;
                                            }
                                            else if (ThisParam.ToLower() == "quality")
                                            {
                                                int QualityValue = 0;
                                                if (ThisValue.ToLower() == "inferior") QualityValue = 1;
                                                if (ThisValue.ToLower() == "normal") QualityValue = 2;
                                                if (ThisValue.ToLower() == "superior") QualityValue = 3;
                                                if (ThisValue.ToLower() == "magic") QualityValue = 4;
                                                if (ThisValue.ToLower() == "set") QualityValue = 5;
                                                if (ThisValue.ToLower() == "rare") QualityValue = 6;
                                                if (ThisValue.ToLower() == "unique") QualityValue = 7;
                                                if (ThisValue.ToLower() == "crafted") QualityValue = 8;
                                                if (ThisValue.ToLower() == "tempered") QualityValue = 9;

                                                if (GoingByName) AllNormall_ByName_Quality.Add(currentName, QualityValue);
                                                else AllNormall_ByType_Quality.Add(currentName, QualityValue);
                                            }
                                            else if (ThisParam.ToLower() == "flag")
                                            {
                                                uint ThisFlagss = 0;
                                                if (ThisValue.ToLower() == "identified") ThisFlagss += 0x00000010;
                                                if (ThisValue.ToLower() == "socketed") ThisFlagss += 0x00000800;
                                                if (ThisValue.ToLower() == "ethereal") ThisFlagss += 0x00400000;

                                                if (GoingByName) AllFlags_ByName.Add(ThisFlagss, ThisOperator);
                                                else AllFlags_ByType.Add(ThisFlagss, ThisOperator);
                                            }
                                            else if (ThisParam.ToLower() == "class")
                                            {
                                                //NOT IMPLEMENTED YET
                                            }
                                            else
                                            {
                                                if (GoingByName)
                                                {
                                                    //Console.WriteLine("Added:" + ThisParam + ThisOperator + ThisValue);
                                                    AllStats_ByName.Add(ThisParam, int.Parse(ThisValue));
                                                    AllOperators_ByName.Add(ThisParam, ThisOperator);
                                                }
                                                else
                                                {
                                                    //Console.WriteLine("Added:" + ThisParam + " " + ThisOperator + " " + ThisValue + " for " + currentName);
                                                    AllStats_ByType.Add(ThisParam, int.Parse(ThisValue));
                                                    AllOperators_ByType.Add(ThisParam, ThisOperator);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            ThisLine = "";
                                        }
                                    }

                                    if (AllStats_ByName.Count > 0)
                                    {
                                        AllNormall_ByName_Stats.Add(currentName, AllStats_ByName);
                                        AllNormall_ByName_Operators.Add(currentName, AllOperators_ByName);
                                    }
                                    if (AllStats_ByType.Count > 0)
                                    {
                                        AllNormall_ByType_Stats.Add(currentName, AllStats_ByType);
                                        AllNormal_ByType_Operators.Add(currentName, AllOperators_ByType);
                                    }
                                    if (AllFlags_ByName.Count > 0)
                                    {
                                        AllNormall_ByName_Flags.Add(currentName, AllFlags_ByName);
                                    }
                                    if (AllFlags_ByType.Count > 0)
                                    {
                                        AllNormall_ByType_Flags.Add(currentName, AllFlags_ByType);
                                    }
                                }

                                //##########################################################################################################################

                                if (DoingKeysRune) AllKeys.Add(ThisItem, PickItem);
                            }

                            if (AllLines[i].Contains("KEYS/GEMS/RUNES ITEMS"))
                            {
                                DoingKeysRune = true;
                                DoingNormal = false;
                            }
                            if (AllLines[i].Contains("NORMAL ITEMS"))
                            {
                                DoingKeysRune = false;
                                DoingNormal = true;
                            }
                        }
                    }
                    catch
                    {
                        Form1_0.method_1("Error encountered in 'ItemsSettings.txt' file at line" + i + "!", Color.Red);
                    }
                }

                Form1_0.ItemsAlert_0.PickItemsRunesKeyGems.Clear();
                foreach (var ThisDir in AllKeys) Form1_0.ItemsAlert_0.PickItemsRunesKeyGems.Add(ThisDir.Key, ThisDir.Value);

                Form1_0.ItemsAlert_0.PickItemsPotions.Clear();

                //#####
                Form1_0.ItemsAlert_0.PickItemsNormal_ByName.Clear();
                Form1_0.ItemsAlert_0.PickItemsNormal_ByName_Flags.Clear();
                Form1_0.ItemsAlert_0.PickItemsNormal_ByName_Quality.Clear();
                Form1_0.ItemsAlert_0.PickItemsNormal_ByName_Stats.Clear();
                Form1_0.ItemsAlert_0.PickItemsNormal_ByName_Operators.Clear();
                Form1_0.ItemsAlert_0.PickItemsNormal_ByNameDesc.Clear();
                foreach (var ThisDir in AllNormall_ByName)
                {
                    if (ThisDir.Key.Contains("Potion"))
                    {
                        Form1_0.ItemsAlert_0.PickItemsPotions.Add(ThisDir.Key, ThisDir.Value);
                    }
                    else
                    {
                        //Console.WriteLine("Key:" + ThisDir.Key + ", value:" + ThisDir.Value);
                        Form1_0.ItemsAlert_0.PickItemsNormal_ByName.Add(ThisDir.Key, ThisDir.Value);
                        if (AllNormall_ByName_Flags.ContainsKey(ThisDir.Key)) Form1_0.ItemsAlert_0.PickItemsNormal_ByName_Flags.Add(ThisDir.Key, AllNormall_ByName_Flags[ThisDir.Key]);
                        if (AllNormall_ByName_Quality.ContainsKey(ThisDir.Key)) Form1_0.ItemsAlert_0.PickItemsNormal_ByName_Quality.Add(ThisDir.Key, AllNormall_ByName_Quality[ThisDir.Key]);
                        if (AllNormall_ByName_Stats.ContainsKey(ThisDir.Key)) Form1_0.ItemsAlert_0.PickItemsNormal_ByName_Stats.Add(ThisDir.Key, AllNormall_ByName_Stats[ThisDir.Key]);
                        if (AllNormall_ByName_Operators.ContainsKey(ThisDir.Key)) Form1_0.ItemsAlert_0.PickItemsNormal_ByName_Operators.Add(ThisDir.Key, AllNormall_ByName_Operators[ThisDir.Key]);
                        Form1_0.ItemsAlert_0.PickItemsNormal_ByNameDesc.Add(ThisDir.Key, NormalNameDesc[ThisDir.Key]);
                    }
                }
                //#####

                //#####
                Form1_0.ItemsAlert_0.PickItemsNormal_ByType.Clear();
                Form1_0.ItemsAlert_0.PickItemsNormal_ByType_Flags.Clear();
                Form1_0.ItemsAlert_0.PickItemsNormal_ByType_Quality.Clear();
                Form1_0.ItemsAlert_0.PickItemsNormal_ByType_Stats.Clear();
                Form1_0.ItemsAlert_0.PickItemsNormal_ByType_Operators.Clear();
                Form1_0.ItemsAlert_0.PickItemsNormal_ByTypeDesc.Clear();
                foreach (var ThisDir in AllNormall_ByType)
                {
                    //Console.WriteLine("Key:" + ThisDir.Key + ", value:" + ThisDir.Value);
                    Form1_0.ItemsAlert_0.PickItemsNormal_ByType.Add(ThisDir.Key, ThisDir.Value);
                    if (AllNormall_ByType_Flags.ContainsKey(ThisDir.Key)) Form1_0.ItemsAlert_0.PickItemsNormal_ByType_Flags.Add(ThisDir.Key, AllNormall_ByType_Flags[ThisDir.Key]);
                    if (AllNormall_ByType_Quality.ContainsKey(ThisDir.Key)) Form1_0.ItemsAlert_0.PickItemsNormal_ByType_Quality.Add(ThisDir.Key, AllNormall_ByType_Quality[ThisDir.Key]);
                    if (AllNormall_ByType_Stats.ContainsKey(ThisDir.Key)) Form1_0.ItemsAlert_0.PickItemsNormal_ByType_Stats.Add(ThisDir.Key, AllNormall_ByType_Stats[ThisDir.Key]);
                    if (AllNormal_ByType_Operators.ContainsKey(ThisDir.Key)) Form1_0.ItemsAlert_0.PickItemsNormal_ByType_Operators.Add(ThisDir.Key, AllNormal_ByType_Operators[ThisDir.Key]);
                    Form1_0.ItemsAlert_0.PickItemsNormal_ByTypeDesc.Add(ThisDir.Key, NormalTypeDesc[ThisDir.Key]);
                }
                //#####

                //Console.WriteLine("Done normal loading");

                //#####

                //Form1_0.ItemsAlert_0.PickItemsUnique = new string[AllNormal.Count];
                //for (int i = 0; i < AllNormal.Count; i++) Form1_0.ItemsAlert_0.PickItemsUnique[i] = AllNormal[i];
            }
            catch
            {
                Form1_0.method_1("Unable to load 'ItemsSettings.txt' file!", Color.Red);
            }
        }

        public void SaveCubingSettings()
        {
            try
            {
                AllLines = File.ReadAllLines(File_CubingSettings);
                for (int i = 0; i < AllLines.Length; i++)
                {
                    if (AllLines[i].Length > 0)
                    {
                        if (AllLines[i][2] != '#')
                        {
                            string ThisItem = AllLines[i];
                            bool PickItem = true;
                            if (AllLines[i][0] == '/')
                            {
                                PickItem = false;
                                ThisItem = AllLines[i].Substring(2);
                            }

                            for (int k = 0; k < Form1_0.Cubing_0.CubingRecipes.Count; k++)
                            {
                                if (Form1_0.Cubing_0.CubingRecipes[k] == ThisItem)
                                {
                                    if (!PickItem && Form1_0.Cubing_0.CubingRecipesEnabled[k]) AllLines[i] = ThisItem;
                                    if (PickItem && !Form1_0.Cubing_0.CubingRecipesEnabled[k]) AllLines[i] = "//" + ThisItem;
                                }
                            }
                        }
                    }
                }

                File.WriteAllLines(File_CubingSettings, AllLines);
                Form1_0.method_1("Saved 'CubingRecipes.txt' file!", Color.DarkGreen);
            }
            catch
            {
                Form1_0.method_1("Unable to save 'CubingRecipes.txt' file!", Color.Red);
            }
        }

        public void SaveItemsSettings()
        {
            try
            {
                bool DoingKeysRune = false;
                bool DoingNormal = false;

                bool DoingName = true;
                int NormalStartAt = 0;

                AllLines = File.ReadAllLines(File_ItemsSettings);
                for (int i = 0; i < AllLines.Length; i++)
                {
                    if (AllLines[i].Length > 0)
                    {
                        if (AllLines[i][2] != '#')
                        {
                            string ThisItem = AllLines[i];
                            bool PickItem = true;
                            string ThisDesc = "";
                            if (AllLines[i][0] == '/')
                            {
                                PickItem = false;
                                ThisItem = AllLines[i].Substring(2);
                            }

                            if (ThisItem.Contains("/"))
                            {
                                ThisDesc = ThisItem.Substring(ThisItem.IndexOf('/'));
                                ThisItem = ThisItem.Substring(0, ThisItem.IndexOf('/'));    //remove description '//'
                            }

                            string FullLine = ThisItem;
                            //##########
                            if (DoingNormal)
                            {
                                if (ThisItem.Replace(" ", "").ToLower().Contains("[type]=="))
                                {
                                    DoingName = false;
                                    ThisItem = ThisItem.Replace(" ", "").Substring(ThisItem.IndexOf('=') + 1, ThisItem.IndexOf('&'));
                                    ThisItem = ThisItem.Substring(0, ThisItem.IndexOf('&'));

                                    //Get the index count
                                    int Count = 1;
                                    for (int k = NormalStartAt; k < i; k++)
                                    {
                                        if (AllLines[k].Length > 0)
                                        {
                                            if (AllLines[k][3] == '#') continue;
                                            string ThisItemBuf = AllLines[k];
                                            if (AllLines[k][0] == '/') ThisItemBuf = AllLines[k].Substring(2);

                                            if (ThisItemBuf.Contains('&')) ThisItemBuf = ThisItemBuf.Replace(" ", "").Substring(ThisItemBuf.IndexOf('=') + 2, ThisItemBuf.IndexOf('&'));
                                            else ThisItemBuf = ThisItemBuf.Replace(" ", "").Substring(ThisItemBuf.IndexOf('=') + 2);
                                            if (ThisItemBuf == ThisItem) Count++;
                                        }
                                    }
                                    ThisItem += Count;
                                }
                                if (ThisItem.Replace(" ", "").ToLower().Contains("[name]=="))
                                {
                                    DoingName = true;

                                    if (ThisItem.Contains('&'))
                                    {
                                        ThisItem = ThisItem.Replace(" ", "").Substring(ThisItem.IndexOf('=') + 1, ThisItem.IndexOf('&'));
                                        ThisItem = ThisItem.Substring(0, ThisItem.IndexOf('&'));

                                        //Get the index count
                                        int Count = 1;
                                        for (int k = NormalStartAt; k < i; k++)
                                        {
                                            if (AllLines[k].Length > 0)
                                            {
                                                if (AllLines[k][3] == '#') continue;
                                                string ThisItemBuf = AllLines[k];
                                                if (AllLines[k][0] == '/') ThisItemBuf = AllLines[k].Substring(2);

                                                if (ThisItemBuf.Contains('&')) ThisItemBuf = ThisItemBuf.Replace(" ", "").Substring(ThisItemBuf.IndexOf('=') + 2, ThisItemBuf.IndexOf('&'));
                                                else ThisItemBuf = ThisItemBuf.Replace(" ", "").Substring(ThisItemBuf.IndexOf('=') + 2);
                                                if (ThisItemBuf == ThisItem) Count++;
                                            }
                                        }
                                        ThisItem += Count;
                                    }
                                    else
                                    {
                                        ThisItem = ThisItem.Replace(" ", "").Substring(ThisItem.IndexOf('=') + 1);
                                        if (ThisItem.Contains('/')) ThisItem = ThisItem.Substring(0, ThisItem.IndexOf('/'));
                                    }
                                }
                            }
                            //##########

                            if (DoingKeysRune)
                            {
                                foreach (var ThisDir in Form1_0.ItemsAlert_0.PickItemsRunesKeyGems)
                                {
                                    if (ThisDir.Key == ThisItem)
                                    {
                                        if (!PickItem && ThisDir.Value) AllLines[i] = ThisItem + ThisDesc;
                                        if (PickItem && !ThisDir.Value) AllLines[i] = "//" + ThisItem + ThisDesc;
                                    }
                                }
                            }
                            if (DoingNormal)
                            {
                                if (DoingName)
                                {
                                    foreach (var ThisDir in Form1_0.ItemsAlert_0.PickItemsNormal_ByName)
                                    {
                                        if (ThisDir.Key == ThisItem)
                                        {
                                            if (!PickItem && ThisDir.Value) AllLines[i] = FullLine + ThisDesc;
                                            if (PickItem && !ThisDir.Value) AllLines[i] = "//" + FullLine + ThisDesc;
                                        }
                                    }
                                }
                                else
                                {
                                    foreach (var ThisDir in Form1_0.ItemsAlert_0.PickItemsNormal_ByType)
                                    {
                                        if (ThisDir.Key == ThisItem)
                                        {
                                            if (!PickItem && ThisDir.Value) AllLines[i] = FullLine + ThisDesc;
                                            if (PickItem && !ThisDir.Value) AllLines[i] = "//" + FullLine + ThisDesc;
                                        }
                                    }
                                }
                            }
                        }

                        if (AllLines[i].Contains("KEYS/GEMS/RUNES ITEMS"))
                        {
                            DoingKeysRune = true;
                            DoingNormal = false;
                        }
                        if (AllLines[i].Contains("NORMAL ITEMS"))
                        {
                            DoingKeysRune = false;
                            DoingNormal = true;
                            NormalStartAt = i;
                        }
                    }
                }

                File.WriteAllLines(File_ItemsSettings, AllLines);
                Form1_0.method_1("Saved 'ItemsSettings.txt' file!", Color.DarkGreen);
            }
            catch
            {
                Form1_0.method_1("Unable to save 'ItemsSettings.txt' file!", Color.Red);
            }
        }

        public void LoadBotSettings()
        {
            try
            {
                for (int i = 0; i < AllLines.Length; i++)
                {
                    if (AllLines[i].Length > 0)
                    {
                        if (AllLines[i][0] != '/' && AllLines[i][0] != '#')
                        {
                            if (AllLines[i].Contains("="))
                            {
                                string[] Params = AllLines[i].Split('=');

                                if (Params[0].Contains("MaxGameTime"))
                                {
                                    CharConfig.MaxGameTime = int.Parse(Params[1]);
                                }
                                if (Params[0].Contains("IsRushing"))
                                {
                                    CharConfig.IsRushing = bool.Parse(Params[1].ToLower());
                                }
                                if (Params[0].Contains("RushLeecherName"))
                                {
                                    CharConfig.RushLeecherName = Params[1];
                                }

                                //#########
                                if (Params[0].Contains("KillBaal"))
                                {
                                    Form1_0.Baal_0.KillBaal = bool.Parse(Params[1].ToLower());
                                }
                                if (Params[0].Contains("LeaveIfMobsIsPresent_ID"))
                                {
                                    Form1_0.Baal_0.LeaveIfMobsIsPresent_ID.Clear();
                                    if (Params[1].Contains(","))
                                    {
                                        string[] Splitted = Params[1].Split(',');
                                        Form1_0.Baal_0.LeaveIfMobsIsPresent_ID.Add(uint.Parse(Splitted[i]));
                                    }
                                    else if(Params[1] != "")
                                    {
                                        Form1_0.Baal_0.LeaveIfMobsIsPresent_ID.Add(uint.Parse(Params[1]));
                                    }
                                }
                                if (Params[0].Contains("LeaveIfMobsIsPresent_Count"))
                                {
                                    Form1_0.Baal_0.LeaveIfMobsIsPresent_Count.Clear();
                                    if (Params[1].Contains(","))
                                    {
                                        string[] Splitted = Params[1].Split(',');
                                        Form1_0.Baal_0.LeaveIfMobsIsPresent_Count.Add(int.Parse(Splitted[i]));
                                    }
                                    else if (Params[1] != "")
                                    {
                                        Form1_0.Baal_0.LeaveIfMobsIsPresent_Count.Add(int.Parse(Params[1]));
                                    }
                                }
                                if (Params[0].Contains("LeaveIfMobsCountIsAbove"))
                                {
                                    Form1_0.Baal_0.LeaveIfMobsCountIsAbove = int.Parse(Params[1].ToLower());
                                }
                                if (Params[0].Contains("SafeHealingStrat"))
                                {
                                    Form1_0.Baal_0.SafeYoloStrat = bool.Parse(Params[1].ToLower());
                                }
                                //#########


                                if (Params[0].Contains("RunMapHackOnly"))
                                {
                                    CharConfig.RunMapHackOnly = bool.Parse(Params[1].ToLower());
                                }

                                if (Params[0].Contains("RunDarkWoodRush"))
                                {
                                    CharConfig.RunDarkWoodRush = bool.Parse(Params[1].ToLower());
                                }
                                if (Params[0].Contains("RunTristramRush"))
                                {
                                    CharConfig.RunTristramRush = bool.Parse(Params[1].ToLower());
                                }
                                if (Params[0].Contains("RunAndarielRush"))
                                {
                                    CharConfig.RunAndarielRush = bool.Parse(Params[1].ToLower());
                                }
                                if (Params[0].Contains("RunRadamentRush"))
                                {
                                    CharConfig.RunRadamentRush = bool.Parse(Params[1].ToLower());
                                }
                                if (Params[0].Contains("RunHallOfDeadRush"))
                                {
                                    CharConfig.RunHallOfDeadRush = bool.Parse(Params[1].ToLower());
                                }
                                if (Params[0].Contains("RunFarOasisRush"))
                                {
                                    CharConfig.RunFarOasisRush = bool.Parse(Params[1].ToLower());
                                }
                                if (Params[0].Contains("RunLostCityRush"))
                                {
                                    CharConfig.RunLostCityRush = bool.Parse(Params[1].ToLower());
                                }
                                if (Params[0].Contains("RunSummonerRush"))
                                {
                                    CharConfig.RunSummonerRush = bool.Parse(Params[1].ToLower());
                                }
                                if (Params[0].Contains("RunDurielRush"))
                                {
                                    CharConfig.RunDurielRush = bool.Parse(Params[1].ToLower());
                                }
                                if (Params[0].Contains("RunKahlimEyeRush"))
                                {
                                    CharConfig.RunKahlimEyeRush = bool.Parse(Params[1].ToLower());
                                }
                                if (Params[0].Contains("RunKahlimBrainRush"))
                                {
                                    CharConfig.RunKahlimBrainRush = bool.Parse(Params[1].ToLower());
                                }
                                if (Params[0].Contains("RunKahlimHeartRush"))
                                {
                                    CharConfig.RunKahlimHeartRush = bool.Parse(Params[1].ToLower());
                                }
                                if (Params[0].Contains("RunTravincalRush"))
                                {
                                    CharConfig.RunTravincalRush = bool.Parse(Params[1].ToLower());
                                }
                                if (Params[0].Contains("RunMephistoRush"))
                                {
                                    CharConfig.RunMephistoRush = bool.Parse(Params[1].ToLower());
                                }
                                if (Params[0].Contains("RunChaosRush"))
                                {
                                    CharConfig.RunChaosRush = bool.Parse(Params[1].ToLower());
                                }
                                if (Params[0].Contains("RunAncientsRush"))
                                {
                                    CharConfig.RunAncientsRush = bool.Parse(Params[1].ToLower());
                                }
                                if (Params[0].Contains("RunBaalRush"))
                                {
                                    CharConfig.RunBaalRush = bool.Parse(Params[1].ToLower());
                                }

                                //##########
                                if (Params[0].Contains("ShowOverlay"))
                                {
                                    CharConfig.ShowOverlay = bool.Parse(Params[1].ToLower());
                                }
                                //##########

                                if (Params[0].Contains("RunWPTaker"))
                                {
                                    CharConfig.RunWPTaker = bool.Parse(Params[1].ToLower());
                                }
                                if (Params[0].Contains("RunPindleskinScript"))
                                {
                                    CharConfig.RunPindleskinScript = bool.Parse(Params[1].ToLower());
                                }
                                if (Params[0].Contains("RunDurielScript"))
                                {
                                    CharConfig.RunDurielScript = bool.Parse(Params[1].ToLower());
                                }
                                if (Params[0].Contains("RunSummonerScript"))
                                {
                                    CharConfig.RunSummonerScript = bool.Parse(Params[1].ToLower());
                                }
                                if (Params[0].Contains("RunMephistoScript"))
                                {
                                    CharConfig.RunMephistoScript = bool.Parse(Params[1].ToLower());
                                }
                                if (Params[0].Contains("RunAndarielScript"))
                                {
                                    CharConfig.RunAndarielScript = bool.Parse(Params[1].ToLower());
                                }
                                if (Params[0].Contains("RunChaosScript"))
                                {
                                    CharConfig.RunChaosScript = bool.Parse(Params[1].ToLower());
                                }
                                if (Params[0].Contains("RunChaosLeechScript"))
                                {
                                    CharConfig.RunChaosLeechScript = bool.Parse(Params[1].ToLower());
                                }
                                if (Params[0].Contains("RunCountessScript"))
                                {
                                    CharConfig.RunCountessScript = bool.Parse(Params[1].ToLower());
                                }
                                if (Params[0].Contains("RunLowerKurastScript"))
                                {
                                    CharConfig.RunLowerKurastScript = bool.Parse(Params[1].ToLower());
                                }
                                if (Params[0].Contains("RunBaalScript"))
                                {
                                    CharConfig.RunBaalScript = bool.Parse(Params[1].ToLower());
                                }
                                if (Params[0].Contains("RunBaalLeechScript"))
                                {
                                    CharConfig.RunBaalLeechScript = bool.Parse(Params[1].ToLower());
                                }
                                if (Params[0].Contains("RunCowsScript"))
                                {
                                    CharConfig.RunCowsScript = bool.Parse(Params[1].ToLower());
                                }
                                if (Params[0].Contains("RunEldritchScript"))
                                {
                                    CharConfig.RunEldritchScript = bool.Parse(Params[1].ToLower());
                                }
                                if (Params[0].Contains("RunShenkScript"))
                                {
                                    CharConfig.RunShenkScript = bool.Parse(Params[1].ToLower());
                                }
                                if (Params[0].Contains("RunNihlatakScript"))
                                {
                                    CharConfig.RunNihlatakScript = bool.Parse(Params[1].ToLower());
                                }
                                //########

                                if (Params[0].Contains("RunItemGrabScriptOnly"))
                                {
                                    CharConfig.RunItemGrabScriptOnly = bool.Parse(Params[1].ToLower());
                                }
                                if (Params[0].Contains("RunChaosSearchGameScript"))
                                {
                                    CharConfig.RunChaosSearchGameScript = bool.Parse(Params[1].ToLower());
                                }
                                if (Params[0].Contains("RunBaalSearchGameScript"))
                                {
                                    CharConfig.RunBaalSearchGameScript = bool.Parse(Params[1].ToLower());
                                }
                                if (Params[0].Contains("RunGameMakerScript"))
                                {
                                    CharConfig.RunGameMakerScript = bool.Parse(Params[1].ToLower());
                                }
                                if (Params[0].Contains("GameName"))
                                {
                                    CharConfig.GameName = Params[1];
                                }
                                if (Params[0].Contains("GamePass"))
                                {
                                    CharConfig.GamePass = Params[1];
                                }
                                if (Params[0].Contains("GameDifficulty"))
                                {
                                    CharConfig.GameDifficulty = int.Parse(Params[1]);
                                }
                                //#####
                                if (Params[0].Contains("StartStopKey"))
                                {
                                    Enum.TryParse(Params[1], out CharConfig.StartStopKey);
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
                Form1_0.method_1("Unable to load 'BotSettings.txt' file!", Color.Red);
            }
        }

        public void LoadCubingSettings()
        {
            try
            {
                for (int i = 0; i < AllLines.Length; i++)
                {
                    if (AllLines[i].Length > 0)
                    {
                        if (AllLines[i][0] != '/' && AllLines[i][0] != '#')
                        {
                            if (AllLines[i].Contains("="))
                            {
                                Form1_0.Cubing_0.CubingRecipes.Add(AllLines[i]);
                                Form1_0.Cubing_0.CubingRecipesEnabled.Add(true);
                            }
                        }
                        else
                        {
                            if (AllLines[i][0] != '/' && AllLines[i][2] != '#')
                            {
                                AllLines[i] = AllLines[i].Substring(2);
                                if (AllLines[i].Contains("="))
                                {
                                    Form1_0.Cubing_0.CubingRecipes.Add(AllLines[i]);
                                    Form1_0.Cubing_0.CubingRecipesEnabled.Add(false);
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
                Form1_0.method_1("Unable to load 'CubingRecipes.txt' file!", Color.Red);
            }
        }

        public void SaveCurrentCharSettings()
        {
            string[] AllLines = File.ReadAllLines(File_CharSettings);

            for (int i = 0; i < AllLines.Length; i++)
            {
                if (AllLines[i].Contains("="))
                {
                    string[] Splitted = AllLines[i].Split('=');
                    if (Splitted[0] == "RunOnChar") AllLines[i] = "RunOnChar=" + CharConfig.RunningOnChar;
                }
            }

            File.Create(File_CharSettings).Dispose();
            File.WriteAllLines(File_CharSettings, AllLines);

            Form1_0.method_1("Saved '" + Path.GetFileName(File_CharSettings) + "' file!", Color.DarkGreen);
        }

        public void LoadCharSettings()
        {
            try
            {
                for (int i = 0; i < AllLines.Length; i++)
                {
                    if (AllLines[i].Length > 0)
                    {
                        if (AllLines[i][0] != '/' && AllLines[i][0] != '#')
                        {
                            if (AllLines[i].Contains("="))
                            {
                                string[] Params = AllLines[i].Split('=');

                                if (Params[0].Contains("RunOnChar"))
                                {
                                    CharConfig.RunningOnChar = Params[1];
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
                Form1_0.method_1("Unable to load 'CharSettings.txt' file!", Color.Red);
            }
        }

        public void LoadCurrentCharSettings()
        {
            try
            {
                for (int i = 0; i < AllLines.Length; i++)
                {
                    if (AllLines[i].Length > 0)
                    {
                        if (AllLines[i][0] != '/' && AllLines[i][0] != '#')
                        {
                            if (AllLines[i].Contains("="))
                            {
                                string[] Params = AllLines[i].Split('=');

                                if (Params[0].Contains("KeySkillAttack"))
                                {
                                    Enum.TryParse(Params[1], out CharConfig.KeySkillAttack);
                                }
                                if (Params[0].Contains("KeySkillAura"))
                                {
                                    Enum.TryParse(Params[1], out CharConfig.KeySkillAura);
                                }
                                if (Params[0].Contains("KeySkillfastMoveAtTown"))
                                {
                                    Enum.TryParse(Params[1], out CharConfig.KeySkillfastMoveAtTown);
                                }
                                if (Params[0].Contains("KeySkillfastMoveOutsideTown"))
                                {
                                    Enum.TryParse(Params[1], out CharConfig.KeySkillfastMoveOutsideTown);
                                }
                                if (Params[0].Contains("KeySkillDefenseAura"))
                                {
                                    Enum.TryParse(Params[1], out CharConfig.KeySkillDefenseAura);
                                }
                                if (Params[0].Contains("KeySkillCastDefense"))
                                {
                                    Enum.TryParse(Params[1], out CharConfig.KeySkillCastDefense);
                                }
                                if (Params[0].Contains("KeySkillLifeAura"))
                                {
                                    Enum.TryParse(Params[1], out CharConfig.KeySkillLifeAura);
                                }
                                //######
                                if (Params[0].Contains("BeltPotTypeToHave") && Params[1].Contains(","))
                                {
                                    string[] NewParams = Params[1].Split(',');
                                    if (NewParams.Length >= 4)
                                    {
                                        CharConfig.BeltPotTypeToHave = new int[4] { int.Parse(NewParams[0]),
                                                                                int.Parse(NewParams[1]),
                                                                                int.Parse(NewParams[2]),
                                                                                int.Parse(NewParams[3]) };
                                    }
                                }
                                if (Params[0].Contains("InventoryDontCheckItem"))
                                {
                                    string[] NewParams1 = AllLines[i + 2].Split(',');
                                    string[] NewParams2 = AllLines[i + 3].Split(',');
                                    string[] NewParams3 = AllLines[i + 4].Split(',');
                                    string[] NewParams4 = AllLines[i + 5].Split(',');

                                    if (NewParams1.Length >= 10 && NewParams2.Length >= 10 && NewParams3.Length >= 10 && NewParams4.Length >= 10)
                                    {
                                        CharConfig.InventoryDontCheckItem = new int[40];
                                        for (int k = 0; k < 10; k++)
                                        {
                                            CharConfig.InventoryDontCheckItem[k] = int.Parse(NewParams1[k]);
                                        }
                                        for (int k = 0; k < 10; k++)
                                        {
                                            CharConfig.InventoryDontCheckItem[k + 10] = int.Parse(NewParams2[k]);
                                        }
                                        for (int k = 0; k < 10; k++)
                                        {
                                            CharConfig.InventoryDontCheckItem[k + 20] = int.Parse(NewParams3[k]);
                                        }
                                        for (int k = 0; k < 10; k++)
                                        {
                                            CharConfig.InventoryDontCheckItem[k + 30] = int.Parse(NewParams4[k]);
                                        }
                                    }
                                }
                                //#####
                                if (Params[0].Contains("DummyItemSharedStash1"))
                                {
                                    CharConfig.DummyItemSharedStash1 = Params[1];
                                }
                                if (Params[0].Contains("DummyItemSharedStash2"))
                                {
                                    CharConfig.DummyItemSharedStash2 = Params[1];
                                }
                                if (Params[0].Contains("DummyItemSharedStash3"))
                                {
                                    CharConfig.DummyItemSharedStash3 = Params[1];
                                }
                                //#####
                                if (Params[0].Contains("PlayerCharName"))
                                {
                                    CharConfig.PlayerCharName = Params[1];
                                }
                                if (Params[0].Contains("UseTeleport"))
                                {
                                    CharConfig.UseTeleport = bool.Parse(Params[1].ToLower());
                                }
                                if (Params[0].Contains("UseBO"))
                                {
                                    CharConfig.UseBO = bool.Parse(Params[1].ToLower());
                                }
                                if (Params[0].Contains("IDAtShop"))
                                {
                                    CharConfig.IDAtShop = bool.Parse(Params[1].ToLower());
                                }
                                if (Params[0].Contains("GrabForGold"))
                                {
                                    CharConfig.GrabForGold = bool.Parse(Params[1].ToLower());
                                }
                                if (Params[0].Contains("LeaveDiabloClone"))
                                {
                                    CharConfig.LeaveDiabloClone = bool.Parse(Params[1].ToLower());
                                }
                                if (Params[0].Contains("GambleGold"))
                                {
                                    CharConfig.GambleGold = bool.Parse(Params[1].ToLower());
                                }
                                if (Params[0].Contains("ChickenHP"))
                                {
                                    CharConfig.ChickenHP = int.Parse(Params[1]);
                                }
                                if (Params[0].Contains("TakeHPPotUnder") && !Params[0].Contains("MercTakeHPPotUnder"))
                                {
                                    CharConfig.TakeHPPotUnder = int.Parse(Params[1]);
                                }
                                if (Params[0].Contains("TakeRVPotUnder"))
                                {
                                    CharConfig.TakeRVPotUnder = int.Parse(Params[1]);
                                }
                                if (Params[0].Contains("TakeManaPotUnder"))
                                {
                                    CharConfig.TakeManaPotUnder = int.Parse(Params[1]);
                                }
                                if (Params[0].Contains("GambleAboveGoldAmount"))
                                {
                                    CharConfig.GambleAboveGoldAmount = int.Parse(Params[1]);
                                }
                                if (Params[0].Contains("GambleUntilGoldAmount"))
                                {
                                    CharConfig.GambleUntilGoldAmount = int.Parse(Params[1]);
                                }
                                if (Params[0].Contains("PlayerAttackWithRightHand"))
                                {
                                    CharConfig.PlayerAttackWithRightHand = bool.Parse(Params[1].ToLower());
                                }
                                if (Params[0].Contains("KeysLocationInInventory"))
                                {
                                    //8,0
                                    string KeyValue1 = Params[1].ToLower().Substring(0, Params[1].IndexOf(","));
                                    string KeyValue2 = Params[1].ToLower().Substring(Params[1].IndexOf(",") + 1);
                                    CharConfig.KeysLocationInInventory.Item1 = int.Parse(KeyValue1);
                                    CharConfig.KeysLocationInInventory.Item2 = int.Parse(KeyValue2);
                                }
                                //#####
                                if (Params[0].Contains("UsingMerc"))
                                {
                                    CharConfig.UsingMerc = bool.Parse(Params[1].ToLower());
                                }
                                if (Params[0].Contains("MercTakeHPPotUnder"))
                                {
                                    CharConfig.MercTakeHPPotUnder = int.Parse(Params[1]);
                                }
                                //#####

                                //#####
                            }
                        }
                    }
                }
            }
            catch
            {
                Form1_0.method_1("Unable to load the settings file for the Current Char!", Color.Red);
            }
        }
    }
}
