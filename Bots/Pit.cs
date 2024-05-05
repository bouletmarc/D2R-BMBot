using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Enums;
using static MapAreaStruc;

public class Pit
{
    Form1 Form1_0;

    public int CurrentStep = 0;
    public bool ScriptDone = false;


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
        if ((Enums.Area)Form1_0.PlayerScan_0.levelNo == Enums.Area.OuterCloister) CurrentStep = 1;
        if ((Enums.Area)Form1_0.PlayerScan_0.levelNo == Enums.Area.MonasteryGate) CurrentStep = 2;
        if ((Enums.Area)Form1_0.PlayerScan_0.levelNo == Enums.Area.TamoeHighland) CurrentStep = 3;
        if ((Enums.Area)Form1_0.PlayerScan_0.levelNo == Enums.Area.PitLevel1) CurrentStep = 4;
        if ((Enums.Area)Form1_0.PlayerScan_0.levelNo == Enums.Area.PitLevel2) CurrentStep = 5;
    }

    public void RunScript()
    {
        Form1_0.Town_0.ScriptTownAct = 1; //set to town act 5 when running this script

        if (!Form1_0.Running || !Form1_0.GameStruc_0.IsInGame())
        {
            ScriptDone = true;
            return;
        }

        if (Form1_0.Town_0.GetInTown())
        {
            Form1_0.SetGameStatus("GO TO WP");
            CurrentStep = 0;

            Form1_0.Town_0.GoToWPArea(1, 5);
        }
        else
        {
            if (CurrentStep == 0)
            {
                Form1_0.SetGameStatus("DOING CRYPT");
                Form1_0.Battle_0.CastDefense();
                Form1_0.WaitDelay(15);

                if ((Enums.Area)Form1_0.PlayerScan_0.levelNo == Enums.Area.OuterCloister)
                {
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
                if ((Enums.Area)Form1_0.PlayerScan_0.levelNo == Enums.Area.BurialGrounds)
                {
                    CurrentStep++;
                    return;
                }

                Form1_0.PathFinding_0.MoveToNextArea(Enums.Area.MonasteryGate);
                CurrentStep++;
            }

            if (CurrentStep == 2)
            {
                //####
                if (Form1_0.PlayerScan_0.levelNo == (int)Enums.Area.TamoeHighland)
                {
                    CurrentStep++;
                    return;
                }
                if ((Enums.Area)Form1_0.PlayerScan_0.levelNo == Enums.Area.OuterCloister)
                {
                    CurrentStep--;
                    return;
                }
                //####

                Form1_0.PathFinding_0.MoveToNextArea(Enums.Area.TamoeHighland);
                CurrentStep++;
            }

            if (CurrentStep == 3)
            {
                //####
                if (Form1_0.PlayerScan_0.levelNo == (int)Enums.Area.PitLevel1)
                {
                    CurrentStep++;
                    return;
                }
                if ((Enums.Area)Form1_0.PlayerScan_0.levelNo == Enums.Area.MonasteryGate)
                {
                    CurrentStep--;
                    return;
                }
                //####


                Form1_0.PathFinding_0.MoveToExit(Enums.Area.PitLevel1);
                CurrentStep++;
            }

            if (CurrentStep == 4) 
            {
                Form1_0.SetGameStatus("CLEARING PIT LVL1");
                if ((Enums.Area)Form1_0.Battle_0.AreaIDFullyCleared != Enums.Area.PitLevel1)
                {
                    Form1_0.Battle_0.ClearFullAreaOfMobs();

                    if (!Form1_0.Battle_0.ClearingArea)
                    {
                        Form1_0.PathFinding_0.MoveToExit(Enums.Area.PitLevel2);
                        CurrentStep++;
                    }
                }
                else
                {
                    Form1_0.PathFinding_0.MoveToExit(Enums.Area.PitLevel2);
                    CurrentStep++;
                }
            }

            if (CurrentStep == 5)
            {
                //####
                if (Form1_0.PlayerScan_0.levelNo == (int)Enums.Area.PitLevel1)
                {
                    Form1_0.PathFinding_0.MoveToExit(Enums.Area.PitLevel2);
                    return;
                }
                //####

                Form1_0.SetGameStatus("CLEARING PIT LVL2");
                if ((Enums.Area)Form1_0.Battle_0.AreaIDFullyCleared != Enums.Area.PitLevel2)
                {
                    Form1_0.Battle_0.ClearFullAreaOfMobs();

                    if (!Form1_0.Battle_0.ClearingArea)
                    {
                        Form1_0.Town_0.FastTowning = false;
                        Form1_0.Town_0.UseLastTP = false;
                        ScriptDone = true;
                    }
                }
                else
                {
                    Form1_0.Town_0.FastTowning = false;
                    Form1_0.Town_0.UseLastTP = false;
                    ScriptDone = true;
                }
            }
        }
    }
}
