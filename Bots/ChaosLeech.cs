using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static MapAreaStruc;

public class ChaosLeech
{
    Form1 Form1_0;

    public int CurrentStep = 0;
    public int MaxGameTimeToEnter = CharConfig.MaxTimeEnterGame; //6mins
    public int MaxTimeWaitedForTP = (2 * 60) * 2; //2mins
    public int TimeWaitedForTP = 0;
    public bool PrintedInfos = false;
    public bool ScriptDone = false;

    public bool SearchSameGamesAsLastOne = false;
    public int SameGameRetry = 0;

    public List<uint> IgnoredTPList = new List<uint>();
    public uint LastUsedTP_ID = 0;
    public bool AddingIgnoredTP_ID = false;

    public bool LeechDetectedCorrectly = false;

    //List<Tuple<int, int>> GoodPositions = new List<Tuple<int, int>>();

    public int LastLeechPosX = 0;
    public int LastLeechPosY = 0;

    List<int[]> LastLeechPositions = new List<int[]>();
    public int LastLeechPosXNEW = 0;
    public int LastLeechPosYNEW = 0;

    public bool CastedDefense = false;

    public void SetForm1(Form1 form1_1)
    {
        Form1_0 = form1_1;
    }

    public void RunScriptNOTInGame()
    {
        TimeWaitedForTP = 0;
        CurrentStep = 0;
        LastLeechPosX = 0;
        LastLeechPosY = 0;
        LastLeechPosXNEW = 0;
        LastLeechPosYNEW = 0;
        PrintedInfos = false;
        CastedDefense = false;
        //GoodPositions = new List<Tuple<int, int>>();
        LastLeechPositions = new List<int[]>();
        Form1_0.PlayerScan_0.LeechPlayerUnitID = 0;
        Form1_0.PlayerScan_0.LeechPlayerPointer = 0;
        Form1_0.GameStruc_0.GetAllGamesNames();
        IgnoredTPList = new List<uint>();
        LastUsedTP_ID = 0;
        bool EnteredGammme = false;
        LeechDetectedCorrectly = false;
        ScriptDone = false;

        string LastGameName = Form1_0.GameStruc_0.GameName;
        string SearchSameGame = "";
        if (LastGameName != "" && SameGameRetry < 20 && SearchSameGamesAsLastOne)
        {
            SearchSameGame = LastGameName.Substring(0, LastGameName.Length - 2);
        }

        if (Form1_0.GameStruc_0.AllGamesNames.Count > 0)
        {
            List<int> PossibleGamesIndex = new List<int>();

            for (int i = 0; i < Form1_0.GameStruc_0.AllGamesNames.Count; i++)
            {
                if (!Form1_0.Running)
                {
                    break;
                }

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
                    if (Form1_0.GameStruc_0.AllGamesNames[i].ToLower().Contains("chaos")
                        && !Form1_0.GameStruc_0.IsIncludedInListString(CharConfig.ChaosSearchAvoidWords, Form1_0.GameStruc_0.AllGamesNames[i].ToLower())
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
                    if (!Form1_0.Running)
                    {
                        break;
                    }

                    Form1_0.SetGameStatus("SEARCHING:" + Form1_0.GameStruc_0.AllGamesNames[PossibleGamesIndex[i]]);
                    Form1_0.method_1("Checking Game: " + PossibleGamesIndex[i], Color.Black);

                    Form1_0.GameStruc_0.SelectGame(PossibleGamesIndex[i], false);
                    if (!Form1_0.GameStruc_0.SelectedGameName.Contains(Form1_0.GameStruc_0.AllGamesNames[PossibleGamesIndex[i]]))
                    {
                        continue;
                    }
                    if (Form1_0.GameStruc_0.SelectedGameTime < MaxGameTimeToEnter)
                    {
                        Form1_0.GameStruc_0.SelectGame(PossibleGamesIndex[i], true);
                        Form1_0.WaitDelay(300);
                        EnteredGammme = true;
                        break;
                    }
                    else
                    {
                        Form1_0.method_1("Game: " + Form1_0.GameStruc_0.AllGamesNames[PossibleGamesIndex[i]] + " exceed MaxGameTime of " + (MaxGameTimeToEnter / 60) + "mins", Color.DarkOrange);
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
        Form1_0.Town_0.ScriptTownAct = 4; //set to town act 4 when running this script
        SameGameRetry = 0;
        GetLeechInfo();
        if (Form1_0.PlayerScan_0.LeechPlayerPointer == 0 || Form1_0.PlayerScan_0.LeechPlayerUnitID == 0)
        {
            ScriptDone = true;
            return;
        }

        if (!Form1_0.Running || !Form1_0.GameStruc_0.IsInGame())
        {
            ScriptDone = true;
            return;
        }

        if (Form1_0.Town_0.GetInTown())
        {
            Form1_0.SetGameStatus("WAITING TP");
            CurrentStep = 0;
            Form1_0.Mover_0.MoveToLocation(5055, 5039); //move to wp spot

            //use tp
            if (Form1_0.ObjectsStruc_0.GetObjects("TownPortal", true, IgnoredTPList, 999, "", (int)Enums.Area.ChaosSanctuary))
            {
                if (!CastedDefense)
                {
                    Form1_0.Battle_0.CastDefense();
                    CastedDefense = true;
                    Form1_0.WaitDelay(30);
                }

                Position itemScreenPos = Form1_0.GameStruc_0.World2Screen(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, Form1_0.ObjectsStruc_0.itemx, Form1_0.ObjectsStruc_0.itemy);

                Form1_0.KeyMouse_0.MouseClicc_RealPos(itemScreenPos.X, itemScreenPos.Y - 15);
                Form1_0.WaitDelay(100);
                //Form1_0.Mover_0.FinishMoving();
            }
            else
            {
                //Form1_0.method_1("NO TP FOUND NEAR IN TOWN");
                if (TimeWaitedForTP >= MaxTimeWaitedForTP)
                {
                    Form1_0.method_1("Leaving reason: Waited too long for tp", Color.Black);
                    Form1_0.LeaveGame(false);
                }
                else
                {
                    Form1_0.WaitDelay(CharConfig.LeechEnterTPDelay);
                    TimeWaitedForTP++;
                }
            }
        }
        else
        {
            if (CurrentStep == 0)
            {
                Form1_0.Battle_0.CastDefense();
                CurrentStep++;
            }
            else
            {
                Form1_0.SetGameStatus("LEECHING");

                //not correct location check
                if (Form1_0.PlayerScan_0.levelNo != (int)Enums.Area.ChaosSanctuary)
                {

                    if (Form1_0.ObjectsStruc_0.GetObjects("TownPortal", true, IgnoredTPList, 999, "", (int)Enums.Area.ThePandemoniumFortress))
                    {
                        Position itemScreenPos = Form1_0.GameStruc_0.World2Screen(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, Form1_0.ObjectsStruc_0.itemx, Form1_0.ObjectsStruc_0.itemy);

                        Form1_0.KeyMouse_0.MouseClicc_RealPos(itemScreenPos.X, itemScreenPos.Y - 15);
                        Form1_0.WaitDelay(100);
                    }

                    Form1_0.method_1("Added ignored TP ID: " + LastUsedTP_ID, Color.OrangeRed);

                    IgnoredTPList.Add(LastUsedTP_ID);
                    Form1_0.Town_0.UseLastTP = false;
                    AddingIgnoredTP_ID = true;
                    Form1_0.Town_0.FastTowning = false;
                    Form1_0.Town_0.GoToTown();
                    return;
                }

                Form1_0.PlayerScan_0.GetLeechPositions();

                while (Form1_0.PlayerScan_0.LeechlevelNo != 108 && !LeechDetectedCorrectly)
                {
                    if (!Form1_0.Running || !Form1_0.GameStruc_0.IsInGame())
                    {
                        ScriptDone = true;
                        return;
                    }

                    int PlayerLeechIndex = 1;

                    while (true)
                    {
                        Form1_0.GameStruc_0.GameOwnerName = Form1_0.GameStruc_0.AllPlayersNames[PlayerLeechIndex];
                        GetLeechInfo();
                        if (Form1_0.PlayerScan_0.LeechPlayerPointer == 0 || Form1_0.PlayerScan_0.LeechPlayerUnitID == 0) return;
                        Form1_0.PlayerScan_0.GetLeechPositions();

                        if (!Form1_0.Running || !Form1_0.GameStruc_0.IsInGame())
                        {
                            ScriptDone = true;
                            return;
                        }

                        if (Form1_0.PlayerScan_0.LeechlevelNo != 108)
                        {
                            PlayerLeechIndex++;
                            if (PlayerLeechIndex > Form1_0.GameStruc_0.AllPlayersNames.Count - 1)
                            {
                                Form1_0.Town_0.FastTowning = false;
                                Form1_0.Town_0.GoToTown();
                                break;
                            }
                        }
                        else
                        {
                            Form1_0.method_1("Leecher Changed to: " + Form1_0.GameStruc_0.GameOwnerName, Color.DarkGreen);
                            break;
                        }
                    }
                }

                LeechDetectedCorrectly = true;
                SearchSameGamesAsLastOne = false;

                //Form1_0.method_1("Leecher: " + Form1_0.PlayerScan_0.LeechPosX + ", " + Form1_0.PlayerScan_0.LeechPosY);
                /*if (Form1_0.PlayerScan_0.LeechPosX == 0 && Form1_0.PlayerScan_0.LeechPosY == 0)
                {
                    //Form1_0.LeaveGame();
                    return;
                }*/

                List<int[]> monsterPositions = Form1_0.MobsStruc_0.GetAllMobsNearby();

                if (IsMonsterPosition(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, monsterPositions))
                {
                    int IndexToCheck = LastLeechPositions.Count - 2;
                    if (IndexToCheck >= 0)
                    {

                        bool MonsterNearby = IsMonsterPosition(LastLeechPositions[IndexToCheck][0], LastLeechPositions[IndexToCheck][1], monsterPositions);
                        while (MonsterNearby)
                        {
                            if (!Form1_0.Running || !Form1_0.GameStruc_0.IsInGame())
                            {
                                ScriptDone = true;
                                return;
                            }

                            IndexToCheck--;

                            if (IndexToCheck >= 0)
                            {
                                //monsterPositions = Form1_0.MobsStruc_0.GetAllMobsNearby();
                                MonsterNearby = IsMonsterPosition(LastLeechPositions[IndexToCheck][0], LastLeechPositions[IndexToCheck][1], monsterPositions);
                            }
                            else
                            {
                                break;
                            }
                        }

                        if (!MonsterNearby)
                        {
                            Form1_0.method_1("Move to Safe Pos: " + LastLeechPositions[IndexToCheck][0] + ", " + LastLeechPositions[IndexToCheck][1], Color.DarkGreen);
                            Form1_0.Mover_0.MoveToLocation(LastLeechPositions[IndexToCheck][0], LastLeechPositions[IndexToCheck][1], true);
                        }
                    }
                }

                Form1_0.PlayerScan_0.GetLeechPositions();
                if (Form1_0.PlayerScan_0.LeechlevelNo != 108) return;
                if (Form1_0.PlayerScan_0.LeechPosX == 0 && Form1_0.PlayerScan_0.LeechPosY == 0)
                {
                    //Form1_0.LeaveGame();
                    return;
                }

                //Move to leecher
                if (LastLeechPosXNEW != Form1_0.PlayerScan_0.LeechPosX && LastLeechPosYNEW != Form1_0.PlayerScan_0.LeechPosY)
                {
                    //LastLeechPositions.Add(Tuple.Create(Form1_0.PlayerScan_0.LeechPosX, Form1_0.PlayerScan_0.LeechPosY));
                    LastLeechPositions.Add(new int[2] { Form1_0.PlayerScan_0.LeechPosX, Form1_0.PlayerScan_0.LeechPosY });

                    LastLeechPosXNEW = Form1_0.PlayerScan_0.LeechPosX;
                    LastLeechPosYNEW = Form1_0.PlayerScan_0.LeechPosY;
                }

                /*Tuple<int, int> bestPosition = FindBestPosition(Form1_0.PlayerScan_0.LeechPosX, Form1_0.PlayerScan_0.LeechPosY, monsterPositions, 5);

                if (bestPosition[0] != 0 && bestPosition[1] != 0)
                {
                    if (bestPosition[0] != LastLeechPosX && bestPosition[1] != LastLeechPosY)
                    {
                        if (Form1_0.PlayerScan_0.LeechlevelNo != 108) return;

                        GoodPositions.Add(Tuple.Create(bestPosition[0], bestPosition[1]));
                        Form1_0.method_1("Move to Leecher Pos: " + bestPosition[0] + ", " + bestPosition[1], Color.DarkGreen);
                        Form1_0.Mover_0.MoveToLocation(bestPosition[0], bestPosition[1]);

                        LastLeechPosX = bestPosition[0];
                        LastLeechPosY = bestPosition[1];
                    }
                    else
                    {*/
                int ThisCheckIndex = LastLeechPositions.Count - 1;
                while (true)
                {
                    if (!Form1_0.Running || !Form1_0.GameStruc_0.IsInGame())
                    {
                        ScriptDone = true;
                        return;
                    }

                    int[] bestPosition = FindBestPosition(LastLeechPositions[ThisCheckIndex][0], LastLeechPositions[ThisCheckIndex][1], monsterPositions, 4);
                    if (bestPosition[0] != 0 && bestPosition[1] != 0)
                    {
                        if (bestPosition[0] != LastLeechPosX && bestPosition[1] != LastLeechPosY)
                        {
                            if (IsMonsterPosition(bestPosition[0], bestPosition[1], monsterPositions))
                            {
                                ThisCheckIndex--;
                                if (ThisCheckIndex < 0)
                                {
                                    break;
                                }
                                continue;
                            }
                            if (bestPosition[0] - Form1_0.PlayerScan_0.xPosFinal > 300
                                || bestPosition[0] - Form1_0.PlayerScan_0.xPosFinal < -300
                                || bestPosition[1] - Form1_0.PlayerScan_0.yPosFinal > 300
                                || bestPosition[1] - Form1_0.PlayerScan_0.yPosFinal < -300)
                            {
                                break;
                            }

                            if (Form1_0.PlayerScan_0.LeechlevelNo != 108) return;

                            //GoodPositions.Add(Tuple.Create(bestPosition[0], bestPosition[1]));
                            Form1_0.method_1("Move to Leecher Pos #2: " + bestPosition[0] + ", " + bestPosition[1], Color.DarkGreen);
                            Form1_0.Mover_0.MoveToLocation(bestPosition[0], bestPosition[1], true);

                            LastLeechPosX = bestPosition[0];
                            LastLeechPosY = bestPosition[1];
                            break;
                        }
                        else
                        {
                            if (IsMonsterPosition(LastLeechPosX, LastLeechPosY, monsterPositions))
                            {
                                ThisCheckIndex--;
                                if (ThisCheckIndex < 0)
                                {
                                    break;
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }
                //    }
                //}


            }
        }
    }


    static int[] FindBestPosition(int playerX, int playerY, List<int[]> monsterPositions, int maxDisplacement)
    {
        // Create a list to store all possible positions around the player
        List<int[]> possiblePositions = new List<int[]>();

        // Generate all possible positions within the maximum displacement range
        for (int x = playerX - maxDisplacement; x <= playerX + maxDisplacement; x++)
        {
            for (int y = playerY - maxDisplacement; y <= playerY + maxDisplacement; y++)
            {
                // Calculate the distance between the player and the current position
                double distance = Math.Sqrt(Math.Pow(playerX - x, 2) + Math.Pow(playerY - y, 2));

                // Check if the distance is within the maximum displacement and the position is not occupied by a monster
                if (distance <= maxDisplacement && !IsMonsterPosition(x, y, monsterPositions))
                {
                    //possiblePositions.Add(Tuple.Create(x, y));
                    possiblePositions.Add(new int[2] { x, y });
                }
            }
        }

        // Find the closest position among the possible positions
        //int[] bestPosition = Tuple.Create(playerX, playerY);
        int[] bestPosition = new int[2] { playerX, playerY };
        double closestDistance = double.MaxValue;
        foreach (var position in possiblePositions)
        {
            double distance = Math.Sqrt(Math.Pow(playerX - position[0], 2) + Math.Pow(playerY - position[1], 2));
            if (distance < closestDistance)
            {
                closestDistance = distance;
                bestPosition = position;
            }
        }

        return bestPosition;
    }

    static bool IsMonsterPosition(int x, int y, List<int[]> monsterPositions)
    {
        foreach (var monsterPosition in monsterPositions)
        {
            if (monsterPosition[0] >= x - 8
                && monsterPosition[0] <= x + 8
                && monsterPosition[1] >= y - 8
                && monsterPosition[1] <= y + 8)
            {
                return true;
            }
        }
        return false;
    }

    public void GetLeechInfo()
    {
        Form1_0.PlayerScan_0.ScanForLeecher();

        if (!PrintedInfos)
        {
            Form1_0.method_1("Leecher name: " + Form1_0.GameStruc_0.GameOwnerName, Color.DarkTurquoise);
            Form1_0.method_1("Leecher pointer: 0x" + Form1_0.PlayerScan_0.LeechPlayerPointer.ToString("X"), Color.DarkTurquoise);
            Form1_0.method_1("Leecher unitID: 0x" + Form1_0.PlayerScan_0.LeechPlayerUnitID.ToString("X"), Color.DarkTurquoise);
            PrintedInfos = true;
        }

        //LEECHER NOT IN GAME
        if (Form1_0.PlayerScan_0.LeechPlayerPointer == 0 || Form1_0.PlayerScan_0.LeechPlayerUnitID == 0)
        {
            if (Form1_0.Running && Form1_0.GameStruc_0.IsInGame())
            {
                Form1_0.ItemsStruc_0.GrabAllItemsForGold();
                SearchSameGamesAsLastOne = true;
                Form1_0.LeaveGame(true);
            }
        }
    }

    /*public void RunScript()
    {
        if (Form1_0.Town_0.GetInTown())
        {
            Form1_0.Mover_0.MoveToLocation(5055, 5039); //move to wp spot

            //use wp
            if (Form1_0.ObjectsStruc_0.GetObjects("PandamoniumFortressWaypoint"))
            {
                Position itemScreenPos = Form1_0.GameStruc_0.World2Screen(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, Form1_0.ObjectsStruc_0.itemx, Form1_0.ObjectsStruc_0.itemy);
                        itemScreenPos = Form1_0.Mover_0.FixMousePositionWithScreenSize(itemScreenPos);
                Form1_0.KeyMouse_0.MouseClicc(itemScreenPos.X, itemScreenPos.Y - 15);
                Form1_0.Mover_0.FinishMoving();
                if (Form1_0.UIScan_0.WaitTilUIOpen("waypointMenu"))
                {
                    Form1_0.KeyMouse_0.MouseClicc(450, 390);
                    Form1_0.WaitDelay(50);
                    Form1_0.UIScan_0.WaitTilUIClose("waypointMenu");
                    Form1_0.UIScan_0.WaitTilUIClose("loading");
                }
                else
                {
                    Form1_0.method_1("WP MENU NOT OPENED");
                }
            }
            else
            {
                Form1_0.method_1("NO WP FOUND NEAR IN TOWN");
            }
        }
        else
        {
            if (CurrentStep == 0)
            {
                //cast sacred shield
                Form1_0.KeyMouse_0.PressKey(KeySkillCastDefense);
                Form1_0.WaitDelay(5);
                Form1_0.KeyMouse_0.MouseCliccRight(Form1_0.ScreenX / 2, Form1_0.ScreenY / 2);
                //start moving to chaos
                if (Form1_0.Mover_0.MoveToLocation(7794, 5868))
                {
                    CurrentStep++;
                    Form1_0.PlayerScan_0.GetPositions();
                }
            }
            else if (CurrentStep == 1)
            {
                if (Form1_0.Mover_0.MoveToLocation(7800, 5826))
                {
                    CurrentStep++;
                    Form1_0.PlayerScan_0.GetPositions();
                }
            }
            else if (CurrentStep == 2)
            {
                Form1_0.Battle_0.ClearAreaOfMobs(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, 15);
                CurrentStep++;
            }
            else if (CurrentStep == 3)
            {
                //7800,5815 - spot1
                if (Form1_0.Mover_0.MoveToLocation(7800, 5815))
                {
                    CurrentStep++;
                    Form1_0.PlayerScan_0.GetPositions();
                }
            }
            if (CurrentStep == 4)
            {
                //try right
                bool TryingLeft = false;
                if (Form1_0.Mover_0.MoveToLocation(7820, 5815))
                {
                    Form1_0.PlayerScan_0.GetPositions();
                    Form1_0.Battle_0.ClearAreaOfMobs(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, 15);

                    if (Form1_0.Mover_0.MoveToLocation(7840, 5810))
                    {
                        Form1_0.PlayerScan_0.GetPositions();
                        Form1_0.Battle_0.ClearAreaOfMobs(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, 15);

                        if (Form1_0.Mover_0.MoveToLocation(7840, 5775))
                        {
                            Form1_0.PlayerScan_0.GetPositions();
                            Form1_0.Battle_0.ClearAreaOfMobs(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, 15);

                            if (Form1_0.Mover_0.MoveToLocation(7840, 5740))
                            {
                                Form1_0.PlayerScan_0.GetPositions();
                                Form1_0.Battle_0.ClearAreaOfMobs(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, 15);

                                if (Form1_0.Mover_0.MoveToLocation(7840, 5730))
                                {
                                    Form1_0.PlayerScan_0.GetPositions();
                                    Form1_0.Battle_0.ClearAreaOfMobs(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, 15);

                                    CurrentStep++;
                                }
                                else
                                {
                                    Form1_0.Mover_0.MoveToLocation(7840, 5810); //go back
                                    Form1_0.Mover_0.MoveToLocation(7820, 5815); //go back
                                    TryingLeft = true;
                                }
                            }
                            else
                            {
                                Form1_0.Mover_0.MoveToLocation(7840, 5810); //go back
                                Form1_0.Mover_0.MoveToLocation(7820, 5815); //go back
                                TryingLeft = true;
                            }
                        }
                        else
                        {
                            Form1_0.Mover_0.MoveToLocation(7840, 5810); //go back
                            Form1_0.Mover_0.MoveToLocation(7820, 5815); //go back
                            TryingLeft = true;
                        } 
                    }
                    else
                    {
                        Form1_0.Mover_0.MoveToLocation(7820, 5815); //go back
                        TryingLeft = true;
                    }
                }

                if (TryingLeft)
                {
                    if (Form1_0.Mover_0.MoveToLocation(7780, 5815))
                    {
                        Form1_0.PlayerScan_0.GetPositions();
                        Form1_0.Battle_0.ClearAreaOfMobs(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, 15);

                        if (Form1_0.Mover_0.MoveToLocation(7780, 5790))
                        {
                            Form1_0.PlayerScan_0.GetPositions();
                            Form1_0.Battle_0.ClearAreaOfMobs(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, 15);

                            if (Form1_0.Mover_0.MoveToLocation(7760, 5790))
                            {
                                Form1_0.PlayerScan_0.GetPositions();
                                Form1_0.Battle_0.ClearAreaOfMobs(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, 15);

                                if (Form1_0.Mover_0.MoveToLocation(7760, 5760))
                                {
                                    Form1_0.PlayerScan_0.GetPositions();
                                    Form1_0.Battle_0.ClearAreaOfMobs(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, 15);

                                    if (Form1_0.Mover_0.MoveToLocation(7760, 5740))
                                    {
                                        Form1_0.PlayerScan_0.GetPositions();
                                        Form1_0.Battle_0.ClearAreaOfMobs(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, 15);

                                        if (Form1_0.Mover_0.MoveToLocation(7780, 5735))
                                        {
                                            Form1_0.PlayerScan_0.GetPositions();
                                            Form1_0.Battle_0.ClearAreaOfMobs(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, 15);
                                            Form1_0.Mover_0.MoveToLocation(7780, 5730); //###

                                            if (Form1_0.Mover_0.MoveToLocation(7800, 5730))
                                            {
                                                Form1_0.PlayerScan_0.GetPositions();
                                                Form1_0.Battle_0.ClearAreaOfMobs(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, 15);

                                                if (Form1_0.Mover_0.MoveToLocation(7800, 5705))
                                                {
                                                    Form1_0.PlayerScan_0.GetPositions();
                                                    Form1_0.Battle_0.ClearAreaOfMobs(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, 15);
                                                    CurrentStep++;
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
            if (CurrentStep == 5)
            {

            }
        }
    }*/


}
