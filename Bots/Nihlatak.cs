using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MapAreaStruc;

public class Nihlatak
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
        if ((Enums.Area)Form1_0.PlayerScan_0.levelNo == Enums.Area.HallsOfPain) CurrentStep = 1;
        if ((Enums.Area)Form1_0.PlayerScan_0.levelNo == Enums.Area.HallsOfVaught) CurrentStep = 2;
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

            Form1_0.Town_0.GoToWPArea(5, 5);
        }
        else
        {
            if (CurrentStep == 0)
            {
                Form1_0.SetGameStatus("DOING NIHLATAK");
                Form1_0.Battle_0.CastDefense();
                Form1_0.WaitDelay(15);

                if ((Enums.Area)Form1_0.PlayerScan_0.levelNo == Enums.Area.HallsOfPain)
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
                //####
                if (Form1_0.PlayerScan_0.levelNo == (int)Enums.Area.HallsOfVaught)
                {
                    CurrentStep++;
                    return;
                }
                //####

                Form1_0.PathFinding_0.MoveToExit(Enums.Area.HallsOfVaught);
                CurrentStep++;
            }

            if (CurrentStep == 2)
            {
                //####
                if ((Enums.Area)Form1_0.PlayerScan_0.levelNo == Enums.Area.HallsOfPain)
                {
                    CurrentStep = 1;
                    return;
                }
                //####

                //Form1_0.PathFinding_0.MoveToNPC("Nihlathak");
                Form1_0.PathFinding_0.MoveToObject("NihlathakWildernessStartPosition");
                CurrentStep++;
            }

            if (CurrentStep == 3)
            {
                Form1_0.Potions_0.CanUseSkillForRegen = false;
                Form1_0.SetGameStatus("KILLING NIHLATAK");
                Form1_0.MobsStruc_0.DetectThisMob("getBossName", "Nihlathak", false, 200, new List<long>());
                if (Form1_0.MobsStruc_0.GetMobs("getBossName", "Nihlathak", false, 200, new List<long>()))
                {
                    if (Form1_0.MobsStruc_0.MobsHP > 0)
                    {
                        Form1_0.Battle_0.RunBattleScriptOnThisMob("getBossName", "Nihlathak", new List<long>());
                    }
                    else
                    {
                        if (Form1_0.Battle_0.EndBossBattle()) ScriptDone = true;
                        return;
                    }
                }
                else
                {
                    Form1_0.method_1("Nihlatak not detected!", Color.Red);

                    //baal not detected...
                    Form1_0.ItemsStruc_0.GetItems(true);
                    if (Form1_0.MobsStruc_0.GetMobs("getBossName", "Nihlathak", false, 200, new List<long>())) return; //redetect baal?
                    Form1_0.ItemsStruc_0.GrabAllItemsForGold();
                    if (Form1_0.MobsStruc_0.GetMobs("getBossName", "Nihlathak", false, 200, new List<long>())) return; //redetect baal?
                    Form1_0.Potions_0.CanUseSkillForRegen = true;

                    if (Form1_0.Battle_0.EndBossBattle()) ScriptDone = true;
                    return;
                }
            }
        }
    }
}
