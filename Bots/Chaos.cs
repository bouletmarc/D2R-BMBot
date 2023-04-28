using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace app
{
    public class Chaos
    {
        Form1 Form1_0;

        public int CurrentStep = 0;
        public int MaxGameTimeToEnter = (6 * 60); //6mins
        public int MaxTimeWaitedForTP = (2 * 60) * 2; //2mins
        public int TimeWaitedForTP = 0;
        public bool PrintedInfos = false;

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
            Form1_0.GameStruc_0.GetAllGamesNames();

            if (Form1_0.GameStruc_0.AllGamesNames.Count > 0)
            {
                List<int> PossibleGamesIndex = new List<int>();

                for (int i = 0; i < Form1_0.GameStruc_0.AllGamesNames.Count; i++)
                {
                    if (Form1_0.GameStruc_0.AllGamesNames[i].ToLower().Contains("chaos")
                        && !Form1_0.GameStruc_0.AllGamesNames[i].ToLower().Contains("umf")
                        && !Form1_0.GameStruc_0.AllGamesNames[i].ToLower().Contains("u-mf")
                        && !Form1_0.GameStruc_0.AllGamesNames[i].ToLower().Contains("u mf")
                        && !Form1_0.GameStruc_0.AllGamesNames[i].ToLower().Contains("baal"))
                    {
                        PossibleGamesIndex.Add(i);
                    }
                }

                //##
                if (PossibleGamesIndex.Count > 0)
                {
                    for (int i = 0; i < PossibleGamesIndex.Count; i++)
                    {
                        Form1_0.SetGameStatus("SEARCHING:" + Form1_0.GameStruc_0.AllGamesNames[PossibleGamesIndex[i]]);

                        Form1_0.GameStruc_0.SelectGame(PossibleGamesIndex[i], false);
                        if (Form1_0.GameStruc_0.SelectedGameTime < MaxGameTimeToEnter)
                        {
                            Form1_0.GameStruc_0.SelectGame(PossibleGamesIndex[i], true);
                            break;
                        }
                    }
                }
            }
        }

        public void RunScript()
        {
            GetLeechInfo();
            if (Form1_0.Town_0.GetInTown())
            {
                Form1_0.SetGameStatus("WAITING TP");
                CurrentStep = 0;
                Form1_0.Mover_0.MoveToLocation(5055, 5039); //move to wp spot

                //use tp
                //if (Form1_0.ObjectsStruc_0.GetObjects("TownPortal", Form1_0.PlayerScan_0.LeechPlayerUnitID))
                if (Form1_0.ObjectsStruc_0.GetObjects("TownPortal", true))
                {
                    //CastDefense();
                    Dictionary<string, int> itemScreenPos = Form1_0.GameStruc_0.World2Screen(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, Form1_0.ObjectsStruc_0.itemx, Form1_0.ObjectsStruc_0.itemy);
                    Form1_0.KeyMouse_0.MouseClicc(itemScreenPos["x"], itemScreenPos["y"] - 15);
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
                        Form1_0.WaitDelay(450);
                        TimeWaitedForTP++;
                    }
                }
            }
            else
            {
                if (CurrentStep == 0)
                {
                    CastDefense();
                    CurrentStep++;
                }
                else
                {
                    Form1_0.SetGameStatus("LEECHING");
                    Form1_0.PlayerScan_0.GetLeechPositions();


                    //Form1_0.method_1("Leecher: " + Form1_0.PlayerScan_0.LeechPosX + ", " + Form1_0.PlayerScan_0.LeechPosY);
                    if (Form1_0.PlayerScan_0.LeechPosX == 0 && Form1_0.PlayerScan_0.LeechPosY == 0)
                    {
                        //Form1_0.LeaveGame();
                        return;
                    }

                    int DiffXPlayer = Form1_0.PlayerScan_0.LeechPosX - Form1_0.PlayerScan_0.xPos;
                    int DiffYPlayer = Form1_0.PlayerScan_0.LeechPosY - Form1_0.PlayerScan_0.yPos;
                    if (DiffXPlayer < 0) DiffXPlayer = -DiffXPlayer;
                    if (DiffYPlayer < 0) DiffYPlayer = -DiffYPlayer;

                    if (DiffXPlayer > 5 || DiffYPlayer > 5)
                    {
                        Form1_0.SetGameStatus("LEECHING-MOVE");
                        Form1_0.Mover_0.MoveToLocation(Form1_0.PlayerScan_0.LeechPosX, Form1_0.PlayerScan_0.LeechPosY);
                    }
                }
            }
        }

        public void CastDefense()
        {
            //cast sacred shield
            Form1_0.KeyMouse_0.PressKey(CharConfig.KeySkillCastDefense);
            Form1_0.WaitDelay(5);
            Form1_0.KeyMouse_0.MouseCliccRight(Form1_0.CenterX, Form1_0.CenterY);
            Form1_0.WaitDelay(5);
        }

        public void GetLeechInfo()
        {
            Form1_0.PlayerScan_0.ScanForLeecher();

            if (!PrintedInfos)
            {
                Form1_0.method_1("Leecher name: " + Form1_0.GameStruc_0.GameOwnerName, Color.Violet);
                Form1_0.method_1("Leecher pointer: 0x" + Form1_0.PlayerScan_0.LeechPlayerPointer.ToString("X"), Color.Violet);
                Form1_0.method_1("Leecher unitID: 0x" + Form1_0.PlayerScan_0.LeechPlayerUnitID.ToString("X"), Color.Violet);
                PrintedInfos = true;
            }

            //LEECHER NOT IN GAME
            if (Form1_0.PlayerScan_0.LeechPlayerPointer == 0 || Form1_0.PlayerScan_0.LeechPlayerUnitID == 0)
            {
                Form1_0.LeaveGame(true);
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
                    Dictionary<string, int> itemScreenPos = Form1_0.GameStruc_0.World2Screen(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, Form1_0.ObjectsStruc_0.itemx, Form1_0.ObjectsStruc_0.itemy);
                    Form1_0.KeyMouse_0.MouseClicc(itemScreenPos["x"], itemScreenPos["y"] - 15);
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
}
