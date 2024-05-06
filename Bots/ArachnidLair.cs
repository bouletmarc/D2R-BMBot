using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Enums;
using static MapAreaStruc;

public class ArachnidLair
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
        if ((Enums.Area)Form1_0.PlayerScan_0.levelNo == Enums.Area.SpiderForest) CurrentStep = 1;
        if ((Enums.Area)Form1_0.PlayerScan_0.levelNo == Enums.Area.SpiderCavern) CurrentStep = 2;
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

            Form1_0.Town_0.GoToWPArea(3, 1);
        }
        else
        {
            if (CurrentStep == 0)
            {
                Form1_0.SetGameStatus("DOING ARACHNID LAIR");
                Form1_0.Battle_0.CastDefense();
                Form1_0.WaitDelay(15);

                if ((Enums.Area)Form1_0.PlayerScan_0.levelNo == Enums.Area.SpiderForest)
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
                if ((Enums.Area)Form1_0.PlayerScan_0.levelNo == Enums.Area.SpiderCavern)
                {
                    CurrentStep++;
                    return;
                }

                Form1_0.PathFinding_0.MoveToExit(Enums.Area.SpiderCavern);
                CurrentStep++;
            }

            if (CurrentStep == 2)
            {
                //####
                if ((Enums.Area)Form1_0.PlayerScan_0.levelNo == Enums.Area.SpiderForest)
                {
                    CurrentStep--;
                    return;
                }
                //####

                Form1_0.SetGameStatus("CLEARING ARACHNID LAIR");

                if ((Enums.Area)Form1_0.Battle_0.AreaIDFullyCleared != Enums.Area.SpiderCavern)
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
