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

        public string File_CharSettings = Application.StartupPath + @"\CharSettings.txt";
        public string File_BotSettings = Application.StartupPath + @"\BotSettings.txt";
        public string File_ItemsSettings = Application.StartupPath + @"\ItemsSettings.txt";
        public string File_Settings = Application.StartupPath + @"\Settings.txt";
        string[] AllLines = new string[] { };

        public void SetForm1(Form1 form1_1)
        {
            Form1_0 = form1_1;
        }

        public void LoadSettings()
        {
            if (File.Exists(File_CharSettings))
            {
                AllLines = File.ReadAllLines(File_CharSettings);
                LoadCharSettings();
            }
            else
            {
                Form1_0.method_1("UNABLE TO FIND 'CharSettings.txt' FILE!", Color.Red);
            }
            //#####
            if (File.Exists(File_BotSettings))
            {
                AllLines = File.ReadAllLines(File_BotSettings);
                LoadBotSettings();
            }
            else
            {
                Form1_0.method_1("UNABLE TO FIND 'BotSettings.txt' FILE!", Color.Red);
            }
            //#####
            if (File.Exists(File_ItemsSettings))
            {
                AllLines = File.ReadAllLines(File_ItemsSettings);
                LoadItemsSettings();
            }
            else
            {
                Form1_0.method_1("UNABLE TO FIND 'ItemsSettings.txt' FILE!", Color.Red);
            }
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
                Form1_0.method_1("UNABLE TO LOAD 'Settings.txt' FILE!", Color.Red);
            }
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

                    if (Splitted[0] == "RunDurielScript") AllLines[i] = "RunDurielScript=" + CharConfig.RunDurielScript;
                    if (Splitted[0] == "RunSummonerScript") AllLines[i] = "RunSummonerScript=" + CharConfig.RunSummonerScript;
                    if (Splitted[0] == "RunMephistoScript") AllLines[i] = "RunMephistoScript=" + CharConfig.RunMephistoScript;
                    if (Splitted[0] == "RunAndarielScript") AllLines[i] = "RunAndarielScript=" + CharConfig.RunAndarielScript;
                    if (Splitted[0] == "RunCountessScript") AllLines[i] = "RunCountessScript=" + CharConfig.RunCountessScript;
                    if (Splitted[0] == "RunChaosScript") AllLines[i] = "RunChaosScript=" + CharConfig.RunChaosScript;
                    if (Splitted[0] == "RunLowerKurastScript") AllLines[i] = "RunLowerKurastScript=" + CharConfig.RunLowerKurastScript;
                    if (Splitted[0] == "RunBaalLeechScript") AllLines[i] = "RunBaalLeechScript=" + CharConfig.RunBaalLeechScript;
                    if (Splitted[0] == "RunItemGrabScriptOnly") AllLines[i] = "RunItemGrabScriptOnly=" + CharConfig.RunItemGrabScriptOnly;

                    if (Splitted[0] == "RunChaosSearchGameScript") AllLines[i] = "RunChaosSearchGameScript=" + CharConfig.RunChaosSearchGameScript;
                    if (Splitted[0] == "RunBaalSearchGameScript") AllLines[i] = "RunBaalSearchGameScript=" + CharConfig.RunBaalSearchGameScript;
                    if (Splitted[0] == "RunGameMakerScript") AllLines[i] = "RunGameMakerScript=" + CharConfig.RunGameMakerScript;
                    if (Splitted[0] == "GameName") AllLines[i] = "GameName=" + CharConfig.GameName;
                    if (Splitted[0] == "GameDifficulty") AllLines[i] = "GameDifficulty=" + CharConfig.GameDifficulty;
                    if (Splitted[0] == "GamePass") AllLines[i] = "GamePass=" + CharConfig.GamePass;
                }
            }

            File.Create(File_BotSettings).Dispose();
            File.WriteAllLines(File_BotSettings, AllLines);


        }

        public void SaveOthersSettings()
        {
            string SaveTxtt = "";
            SaveTxtt += "RunNumber=" + Form1_0.CurrentGameNumber + Environment.NewLine;
            SaveTxtt += "D2_LOD_113C_Path=" + Form1_0.D2_LOD_113C_Path + Environment.NewLine;

            File.Create(File_Settings).Dispose();
            File.WriteAllText(File_Settings, SaveTxtt);
        }

        public void LoadItemsSettings()
        {
            try
            {
                bool DoingUnique = false;
                bool DoingKeysRune = false;
                bool DoingSet = false;
                bool DoingNormal = false;

                List<string> AllUnique = new List<string>();
                List<string> AllKeys = new List<string>();
                List<string> AllSet = new List<string>();
                List<string> AllNormal = new List<string>();

                for (int i = 0; i < AllLines.Length; i++)
                {
                    if (AllLines[i].Length > 0)
                    {
                        if (AllLines[i][0] != '/' && AllLines[i][0] != '#')
                        {
                            string ThisItem = AllLines[i];
                            if (ThisItem.Contains("/"))
                            {
                                ThisItem = ThisItem.Substring(0, ThisItem.IndexOf('/'));    //remove description '//'
                            }

                            if (DoingUnique) AllUnique.Add(ThisItem);
                            if (DoingKeysRune) AllKeys.Add(ThisItem);
                            if (DoingSet) AllSet.Add(ThisItem);
                            if (DoingNormal) AllNormal.Add(ThisItem);
                        }

                        if (AllLines[i].Contains("UNIQUE ITEMS"))
                        {
                            DoingUnique = true;
                            DoingKeysRune = false;
                            DoingSet = false;
                            DoingNormal = false;
                        }
                        if (AllLines[i].Contains("KEYS/GEMS/RUNES ITEMS"))
                        {
                            DoingUnique = false;
                            DoingKeysRune = true;
                            DoingSet = false;
                            DoingNormal = false;
                        }
                        if (AllLines[i].Contains("SET ITEMS"))
                        {
                            DoingUnique = false;
                            DoingKeysRune = false;
                            DoingSet = true;
                            DoingNormal = false;
                        }
                        if (AllLines[i].Contains("NORMAL ITEMS"))
                        {
                            DoingUnique = false;
                            DoingKeysRune = false;
                            DoingSet = false;
                            DoingNormal = true;
                        }
                    }
                }

                Form1_0.ItemsAlert_0.PickItemsUnique = new string[AllUnique.Count];
                for (int i = 0; i < AllUnique.Count; i++) Form1_0.ItemsAlert_0.PickItemsUnique[i] = AllUnique[i];

                Form1_0.ItemsAlert_0.PickItemsRunesKeyGems = new string[AllKeys.Count];
                for (int i = 0; i < AllKeys.Count; i++) Form1_0.ItemsAlert_0.PickItemsRunesKeyGems[i] = AllKeys[i];

                Form1_0.ItemsAlert_0.PickItemsSet = new string[AllSet.Count];
                for (int i = 0; i < AllSet.Count; i++) Form1_0.ItemsAlert_0.PickItemsSet[i] = AllSet[i];

                //Form1_0.ItemsAlert_0.PickItemsUnique = new string[AllNormal.Count];
                //for (int i = 0; i < AllNormal.Count; i++) Form1_0.ItemsAlert_0.PickItemsUnique[i] = AllNormal[i];
            }
            catch
            {
                Form1_0.method_1("UNABLE TO LOAD 'ItemsSettings.txt' FILE!", Color.Red);
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
                                if (Params[0].Contains("RunCountessScript"))
                                {
                                    CharConfig.RunCountessScript = bool.Parse(Params[1].ToLower());
                                }
                                if (Params[0].Contains("RunLowerKurastScript"))
                                {
                                    CharConfig.RunLowerKurastScript = bool.Parse(Params[1].ToLower());
                                }
                                if (Params[0].Contains("RunBaalLeechScript"))
                                {
                                    CharConfig.RunBaalLeechScript = bool.Parse(Params[1].ToLower());
                                }
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
                                    CharConfig.StartStopKey = int.Parse(Params[1]);
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
                Form1_0.method_1("UNABLE TO LOAD 'BotSettings.txt' FILE!", Color.Red);
            }
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
                Form1_0.method_1("UNABLE TO LOAD 'CharSettings.txt' FILE!", Color.Red);
            }
        }
    }
}
