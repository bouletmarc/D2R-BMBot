using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace app
{
    public class Baal
    {

        Form1 Form1_0;

        public int CurrentStep = 0;
        public int MaxGameTimeToEnter = (4 * 60); //6mins
        public int MaxTimeWaitedForTP = (2 * 60) * 2; //2mins
        public int TimeWaitedForTP = 0;
        public bool PrintedInfos = false;
        public int SameGameRetry = 0;

        public List<uint> IgnoredTPList = new List<uint>();
        public uint LastUsedTP_ID = 0;

        public void SetForm1(Form1 form1_1)
        {
            Form1_0 = form1_1;
        }

        public void RunScriptNOTInGame()
        {
            TimeWaitedForTP = 0;
            CurrentStep = 0;
            PrintedInfos = false;
            Form1_0.PlayerScan_0.LeechPlayerUnitID = 0;
            Form1_0.PlayerScan_0.LeechPlayerPointer = 0;
            Form1_0.Town_0.ScriptTownAct = 5; //set to town act 5 when running this script
            Form1_0.GameStruc_0.GetAllGamesNames();
            IgnoredTPList = new List<uint>();
            LastUsedTP_ID = 0;
            bool EnteredGammme = false;

            string LastGameName = Form1_0.GameStruc_0.GameName;
            string SearchSameGame = "";
            if (LastGameName != "" && SameGameRetry < 20)
            {
                SearchSameGame = LastGameName.Substring(0, LastGameName.Length - 2);
            }

            if (Form1_0.GameStruc_0.AllGamesNames.Count > 0)
            {
                List<int> PossibleGamesIndex = new List<int>();

                for (int i = 0; i < Form1_0.GameStruc_0.AllGamesNames.Count; i++)
                {
                    if (SearchSameGame != "")
                    {
                        if (Form1_0.GameStruc_0.AllGamesNames[i].ToLower().Contains(SearchSameGame.ToLower())
                            && Form1_0.GameStruc_0.AllGamesNames[i] != LastGameName)
                        {
                            PossibleGamesIndex.Add(i);
                        }
                    }
                    else
                    {
                        if (Form1_0.GameStruc_0.AllGamesNames[i].ToLower().Contains("baal")
                            && !Form1_0.GameStruc_0.AllGamesNames[i].ToLower().Contains("umf")
                            && !Form1_0.GameStruc_0.AllGamesNames[i].ToLower().Contains("u-mf")
                            && !Form1_0.GameStruc_0.AllGamesNames[i].ToLower().Contains("u mf")
                            && !Form1_0.GameStruc_0.AllGamesNames[i].ToLower().Contains("chaos")
                            && !Form1_0.GameStruc_0.AllGamesNames[i].ToLower().Contains("cbaal")
                            && !Form1_0.GameStruc_0.AllGamesNames[i].ToLower().Contains("help")
                            && !Form1_0.GameStruc_0.AllGamesNames[i].ToLower().Contains("walk")
                            && Form1_0.GameStruc_0.AllGamesNames[i] != LastGameName) //not equal last gamename
                        {
                            if (!Form1_0.GameStruc_0.TriedThisGame(Form1_0.GameStruc_0.AllGamesNames[i]))
                            {
                                PossibleGamesIndex.Add(i);
                            }
                        }
                    }
                }

                //##
                if (PossibleGamesIndex.Count > 0)
                {
                    for (int i = 0; i < PossibleGamesIndex.Count; i++)
                    {
                        Form1_0.SetGameStatus("SEARCHING:" + Form1_0.GameStruc_0.AllGamesNames[PossibleGamesIndex[i]]);

                        Form1_0.GameStruc_0.SelectGame(PossibleGamesIndex[i], false);
                        Form1_0.GameStruc_0.GetSelectedGameInfo();

                        if (Form1_0.GameStruc_0.SelectedGameTime < MaxGameTimeToEnter)
                        {
                            Form1_0.GameStruc_0.SelectGame(PossibleGamesIndex[i], true);
                            EnteredGammme = true;
                            break;
                        }
                        else
                        {
                            Form1_0.GameStruc_0.AllGamesTriedNames.Add(Form1_0.GameStruc_0.AllGamesNames[PossibleGamesIndex[i]]);
                        }
                    }
                }

                if (!EnteredGammme)
                {
                    if (SearchSameGame != "")
                    {
                        SameGameRetry++;
                    }
                }
            }
        }

        public void RunScript()
        {
            SameGameRetry = 0;
            Form1_0.Town_0.ScriptTownAct = 5; //set to town act 5 when running this script
            GetLeechInfo();
            if (Form1_0.Town_0.GetInTown())
            {
                Form1_0.SetGameStatus("WAITING TP");
                CurrentStep = 0;
                Form1_0.Mover_0.MoveToLocation(5103, 5029); //move to wp spot

                //use tp
                //if (Form1_0.ObjectsStruc_0.GetObjects("TownPortal", Form1_0.PlayerScan_0.LeechPlayerUnitID))
                if (Form1_0.ObjectsStruc_0.GetObjects("TownPortal", true, IgnoredTPList))
                {
                    //CastDefense();
                    Dictionary<string, int> itemScreenPos = Form1_0.GameStruc_0.World2Screen(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, Form1_0.ObjectsStruc_0.itemx, Form1_0.ObjectsStruc_0.itemy);
                    //Console.WriteLine("Clicking: " + itemScreenPos["x"] + "," + itemScreenPos["y"]);
                    Form1_0.KeyMouse_0.MouseClicc(itemScreenPos["x"], itemScreenPos["y"] - 15);
                    Form1_0.WaitDelay(100);
                    LastUsedTP_ID = Form1_0.ObjectsStruc_0.ObjectUnitID;

                    Form1_0.UIScan_0.readUI();
                    if (Form1_0.UIScan_0.tradeMenu)
                    {
                        Form1_0.UIScan_0.CloseUIMenu("tradeMenu");
                    }
                    if (Form1_0.UIScan_0.quitMenu)
                    {
                        Form1_0.UIScan_0.CloseUIMenu("quitMenu");
                    }
                    //Form1_0.Mover_0.FinishMoving();
                }
                else
                {
                    //Form1_0.method_1("NO TP FOUND NEAR IN TOWN");
                    if (TimeWaitedForTP >= MaxTimeWaitedForTP)
                    {
                        Form1_0.method_1("Leaving reason: Waited too long for tp", Color.Red);
                        Form1_0.LeaveGame();
                    }
                    else
                    {
                        Form1_0.WaitDelay(450);
                        TimeWaitedForTP++;
                    }

                    //check if we are about to do baal atleast when waiting
                    if (TimeWaitedForTP >= (MaxTimeWaitedForTP / 3))
                    {
                        if (!Form1_0.PlayerScan_0.HasAnyPlayerInArea(129)    //worldstone lvl2
                            && !Form1_0.PlayerScan_0.HasAnyPlayerInArea(130) //worldstone lvl3
                            && !Form1_0.PlayerScan_0.HasAnyPlayerInArea(131))//throne chamber
                        {
                            Form1_0.method_1("Leaving reason: Nobody seem to baal run", Color.Red);
                            Form1_0.LeaveGame();
                        }
                    }
                }
            }
            else
            {
                if (CurrentStep == 0)
                {
                    CastDefense();
                    CastDefense();
                    CastDefense();


                    //not correct location check
                    if (Form1_0.PlayerScan_0.levelNo != 131)
                    {
                        IgnoredTPList.Add(LastUsedTP_ID);
                        Form1_0.Town_0.UseLastTP = false;
                        Form1_0.Town_0.GoToTown();
                    }
                    else
                    {
                        CurrentStep++;
                    }
                }
                if(CurrentStep == 1)
                {
                    Form1_0.SetGameStatus("LEECHING");
                    Form1_0.PlayerScan_0.GetLeechPositions();

                    //Form1_0.Battle_0.DoBattleScript();

                    if (Form1_0.PlayerScan_0.xPosFinal < 15105 - 8
                        || Form1_0.PlayerScan_0.xPosFinal > 15105 + 8
                        || Form1_0.PlayerScan_0.yPosFinal < 5026 - 8
                        || Form1_0.PlayerScan_0.yPosFinal > 5026 + 8)
                    {
                        Form1_0.Mover_0.MoveToLocation(15105, 5026); //move to safe spot
                    }
                    else
                    {
                        Form1_0.Battle_0.DoBattleScript();
                    }

                    //detect last wave
                    if (Form1_0.MobsStruc_0.GetMobs("getSuperUniqueName", "Baal Subject 4", false, 99, new List<long>()))
                    {
                        if (Form1_0.MobsStruc_0.MobsHP > 0)
                        {
                            CurrentStep++;
                        }
                    }
                    if (Form1_0.MobsStruc_0.GetMobs("getSuperUniqueName", "Baal Subject 5", false, 99, new List<long>()))
                    {
                        if (Form1_0.MobsStruc_0.MobsHP > 0)
                        {
                            CurrentStep++;
                        }
                    }
                }
                if (CurrentStep == 2)
                {
                    Form1_0.SetGameStatus("WAITING PORTAL");
                    //move to baal red portal
                    if (Form1_0.PlayerScan_0.xPosFinal >= 15170 - 40
                        && Form1_0.PlayerScan_0.xPosFinal <= 15170 + 40
                        && Form1_0.PlayerScan_0.yPosFinal >= 5880 - 40
                        && Form1_0.PlayerScan_0.yPosFinal <= 5880 + 40)
                    {
                        CastDefense();
                        CurrentStep++;
                    }
                    else
                    {
                        if (Form1_0.PlayerScan_0.xPosFinal < 15090 - 3
                            || Form1_0.PlayerScan_0.xPosFinal > 15090 + 3
                            || Form1_0.PlayerScan_0.yPosFinal < 5008 - 3
                            || Form1_0.PlayerScan_0.yPosFinal > 5008 + 3)
                        {
                            if (!CharConfig.UseTeleport)
                            {
                                Form1_0.Mover_0.MoveAcceptOffset = 1;
                            }
                            else
                            {
                                Form1_0.Mover_0.MoveAcceptOffset = 3;
                            }
                            if (Form1_0.Mover_0.MoveToLocation(15095, 5023))
                            {
                                if (Form1_0.Mover_0.MoveToLocation(15090, 5008))
                                {
                                    Form1_0.Mover_0.MoveAcceptOffset = 4;
                                }
                            }
                        }
                        else
                        {
                            /*if (Form1_0.ObjectsStruc_0.GetObjects("BaalsPortal", 0, false))
                            {
                                Dictionary<string, int> itemScreenPos = Form1_0.GameStruc_0.World2Screen(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, Form1_0.ObjectsStruc_0.itemx, Form1_0.ObjectsStruc_0.itemy);
                                Form1_0.KeyMouse_0.MouseClicc(itemScreenPos["x"], itemScreenPos["y"] - 15);
                                Form1_0.WaitDelay(10);
                            }*/
                            Dictionary<string, int> itemScreenPos = Form1_0.GameStruc_0.World2Screen(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, 15091, 5005);
                            Form1_0.KeyMouse_0.MouseClicc(itemScreenPos["x"] - 5, itemScreenPos["y"] - 20);
                            Form1_0.WaitDelay(10);
                        }
                    }
                }
                if (CurrentStep == 3)
                {
                    Form1_0.SetGameStatus("MOVING TO BAAL");
                    //in throne, move close to baal
                    if (Form1_0.Mover_0.MoveToLocation(15166, 5934))
                    {
                        if (Form1_0.Mover_0.MoveToLocation(15140, 5940))
                        {
                            CurrentStep++;
                        }
                    }
                }
                if (CurrentStep == 4)
                {
                    Form1_0.SetGameStatus("KILLING BAAL");
                    if (Form1_0.MobsStruc_0.GetMobs("getBossName", "Baal", false, 120, new List<long>()))
                    {
                        if (Form1_0.MobsStruc_0.MobsHP > 0)
                        {
                            Form1_0.Battle_0.RunBattleScriptOnThisMob("getBossName", "Baal");
                        }
                        else
                        {
                            Form1_0.ItemsStruc_0.GetItems(true);
                            Form1_0.ItemsStruc_0.GetItems(true);
                            Form1_0.ItemsStruc_0.GetItems(true);
                            Form1_0.ItemsStruc_0.GetItems(true);
                            Form1_0.ItemsStruc_0.GetItems(true);
                            Form1_0.ItemsStruc_0.GetItems(true);
                            Form1_0.ItemsStruc_0.GetItems(true);
                            Form1_0.ItemsStruc_0.GetItems(true);
                            Form1_0.ItemsStruc_0.GetItems(true);
                            Form1_0.ItemsStruc_0.GetItems(true);
                            Form1_0.ItemsStruc_0.GrabAllItemsForGold();
                            Form1_0.LeaveGame();
                        }
                    }
                }
            }
        }

        public void CastDefense()
        {
            //cast sacred shield
            Form1_0.KeyMouse_0.PressKey(CharConfig.KeySkillCastDefense);
            Form1_0.WaitDelay(5);
            Form1_0.KeyMouse_0.MouseCliccRight(CharConfig.ScreenX / 2, CharConfig.ScreenY / 2);
            Form1_0.WaitDelay(5);
        }

        public void GetLeechInfo()
        {
            Form1_0.PlayerScan_0.ScanForLeecher();

            if (!PrintedInfos)
            {
                Form1_0.method_1("Leecher name: " + Form1_0.GameStruc_0.GameOwnerName, Color.DarkViolet);
                //Form1_0.method_1("Leecher pointer: 0x" + Form1_0.PlayerScan_0.LeechPlayerPointer.ToString("X"), Color.DarkViolet);
                //Form1_0.method_1("Leecher unitID: 0x" + Form1_0.PlayerScan_0.LeechPlayerUnitID.ToString("X"), Color.DarkViolet);
                PrintedInfos = true;
            }

            //LEECHER NOT IN GAME
            if (Form1_0.PlayerScan_0.LeechPlayerPointer == 0 || Form1_0.PlayerScan_0.LeechPlayerUnitID == 0)
            {
                //if (CurrentStep < 2) //kill baal manually
                //{
                    Form1_0.ItemsStruc_0.GrabAllItemsForGold();
                    Form1_0.LeaveGame();
                //}
            }
        }
    }
}
