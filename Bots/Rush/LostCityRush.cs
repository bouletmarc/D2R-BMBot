using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MapAreaStruc;

public class LostCityRush
{
    Form1 Form1_0;

    public int CurrentStep = 0;
    public bool ScriptDone = false;
    public Position ChestPos = new Position { X = 0, Y = 0 };


    public void SetForm1(Form1 form1_1)
    {
        Form1_0 = form1_1;
    }

    public void ResetVars()
    {
        CurrentStep = 0;
        ScriptDone = false;
    }

    public void DetectCurrentStep()
    {
        if ((Enums.Area)Form1_0.PlayerScan_0.levelNo == Enums.Area.LostCity) CurrentStep = 1;
        if ((Enums.Area)Form1_0.PlayerScan_0.levelNo == Enums.Area.ValleyOfSnakes) CurrentStep = 2;
        if ((Enums.Area)Form1_0.PlayerScan_0.levelNo == Enums.Area.ClawViperTempleLevel1) CurrentStep = 3;
        if ((Enums.Area)Form1_0.PlayerScan_0.levelNo == Enums.Area.ClawViperTempleLevel2) CurrentStep = 4;
    }

    public void RunScript()
    {
        Form1_0.Town_0.ScriptTownAct = 2; //set to town act 5 when running this script

        if (!Form1_0.Running || !Form1_0.GameStruc_0.IsInGame())
        {
            ScriptDone = true;
            return;
        }

        if (Form1_0.Town_0.GetInTown())
        {
            Form1_0.SetGameStatus("GO TO WP");
            CurrentStep = 0;

            Form1_0.Town_0.GoToWPArea(2, 5);
        }
        else
        {
            if (CurrentStep == 0)
            {
                Form1_0.SetGameStatus("DOING LOST CITY (AMMY)");
                //Form1_0.Battle_0.CastDefense();
                //Form1_0.WaitDelay(15);

                if ((Enums.Area)Form1_0.PlayerScan_0.levelNo == Enums.Area.LostCity)
                {
                    Form1_0.Town_0.SpawnTP();
                    Form1_0.WaitDelay(15);
                    Form1_0.Battle_0.CastDefense();
                    CurrentStep++;
                }
                else
                {
                    DetectCurrentStep();
                    if (CurrentStep == 0)
                    {
                        Form1_0.Town_0.FastTowning = false;
                        Form1_0.Town_0.GoToTown();
                    }
                }
            }

            if (CurrentStep == 1)
            {
                //####
                if (Form1_0.PlayerScan_0.levelNo == (int)Enums.Area.ValleyOfSnakes)
                {
                    CurrentStep++;
                    return;
                }
                //####

                Form1_0.PathFinding_0.MoveToNextArea(Enums.Area.ValleyOfSnakes);
                CurrentStep++;
            }

            if (CurrentStep == 2)
            {
                //####
                if (Form1_0.PlayerScan_0.levelNo == (int)Enums.Area.ClawViperTempleLevel1)
                {
                    CurrentStep++;
                    return;
                }
                if (Form1_0.PlayerScan_0.levelNo != (int)Enums.Area.ValleyOfSnakes)
                {
                    CurrentStep--;
                    return;
                }
                //####

                Form1_0.PathFinding_0.MoveToExit(Enums.Area.ClawViperTempleLevel1);
                CurrentStep++;
            }

            if (CurrentStep == 3)
            {
                //####
                if (Form1_0.PlayerScan_0.levelNo == (int)Enums.Area.ClawViperTempleLevel2)
                {
                    CurrentStep++;
                    return;
                }
                if (Form1_0.PlayerScan_0.levelNo != (int)Enums.Area.ClawViperTempleLevel1)
                {
                    CurrentStep--;
                    return;
                }
                //####

                Form1_0.PathFinding_0.MoveToExit(Enums.Area.ClawViperTempleLevel2);
                CurrentStep++;
            }

            if (CurrentStep == 4)
            {
                //####
                if (Form1_0.PlayerScan_0.levelNo != (int)Enums.Area.ClawViperTempleLevel2)
                {
                    CurrentStep--;
                    return;
                }
                //####

                ChestPos = Form1_0.MapAreaStruc_0.GetPositionOfObject("object", "TaintedSunAltar", (int)Enums.Area.ClawViperTempleLevel2, new List<int>());
                if (ChestPos.X != 0 && ChestPos.Y != 0)
                {
                    Form1_0.PathFinding_0.MoveToThisPos(ChestPos);

                    //repeat clic on chest
                    /*int tryyy = 0;
                    while (tryyy <= 25)
                    {
                        Dictionary<string, int> itemScreenPos = Form1_0.GameStruc_0.World2Screen(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, ChestPos.X, ChestPos.Y);
                        Form1_0.KeyMouse_0.MouseClicc(itemScreenPos["x"], itemScreenPos["y"]);
                        Form1_0.PlayerScan_0.GetPositions();
                        tryyy++;
                    }*/


                    Form1_0.Town_0.TPSpawned = false;
                    CurrentStep++;
                }
                else
                {
                    Form1_0.method_1("Ammy location not detected!", Color.Red);

                    Form1_0.Town_0.TPSpawned = false;
                    CurrentStep++;
                    //Form1_0.Town_0.FastTowning = false;
                    //ScriptDone = true;
                    //return;
                }
            }

            if (CurrentStep == 5)
            {
                if (!Form1_0.Town_0.TPSpawned) Form1_0.Town_0.SpawnTP();

                CurrentStep++;
            }

            if (CurrentStep == 6)
            {
                Form1_0.SetGameStatus("Ammy waiting on leecher");

                Form1_0.Battle_0.DoBattleScript(25);

                //get leecher infos
                Form1_0.PlayerScan_0.GetLeechPositions();

                if (Form1_0.PlayerScan_0.LeechlevelNo == (int)Enums.Area.ClawViperTempleLevel2)
                {
                    CurrentStep++;
                }
            }

            if (CurrentStep == 7)
            {
                Form1_0.SetGameStatus("Ammy waiting on leecher #2");

                Form1_0.Battle_0.DoBattleScript(25);

                //get leecher infos
                Form1_0.PlayerScan_0.GetLeechPositions();

                if (Form1_0.PlayerScan_0.LeechlevelNo == (int)Enums.Area.LutGholein)
                {
                    Form1_0.Town_0.FastTowning = false;
                    Form1_0.Town_0.UseLastTP = false;
                    ScriptDone = true;
                }
            }
        }
    }
}
