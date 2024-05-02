using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

public class Andariel
{
    Form1 Form1_0;

    public int CurrentStep = 0;
    public bool ScriptDone = false;
    public bool DetectedBoss = false;

    public void SetForm1(Form1 form1_1)
    {
        Form1_0 = form1_1;
    }

    public void ResetVars()
    {
        CurrentStep = 0;
        ScriptDone = false;
        DetectedBoss = false;
    }

    public void DetectCurrentStep()
    {
        if ((Enums.Area)Form1_0.PlayerScan_0.levelNo == Enums.Area.CatacombsLevel2) CurrentStep = 1;
        if ((Enums.Area)Form1_0.PlayerScan_0.levelNo == Enums.Area.CatacombsLevel3) CurrentStep = 2;
        if ((Enums.Area)Form1_0.PlayerScan_0.levelNo == Enums.Area.CatacombsLevel4) CurrentStep = 3;
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

            Form1_0.Town_0.GoToWPArea(1, 8);
        }
        else
        {
            if (CurrentStep == 0)
            {
                Form1_0.SetGameStatus("DOING ANDARIEL");
                Form1_0.Battle_0.CastDefense();
                Form1_0.WaitDelay(15);

                if ((Enums.Area)Form1_0.PlayerScan_0.levelNo == Enums.Area.CatacombsLevel2)
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
                if (Form1_0.PlayerScan_0.levelNo == (int)Enums.Area.CatacombsLevel3)
                {
                    CurrentStep++;
                    return;
                }
                //####

                Form1_0.PathFinding_0.MoveToExit(Enums.Area.CatacombsLevel3);
                CurrentStep++;
            }

            if (CurrentStep == 2)
            {
                //####
                if (Form1_0.PlayerScan_0.levelNo == (int)Enums.Area.CatacombsLevel4)
                {
                    CurrentStep++;
                    return;
                }
                if ((Enums.Area)Form1_0.PlayerScan_0.levelNo == Enums.Area.CatacombsLevel2)
                {
                    CurrentStep--;
                    return;
                }
                //####

                Form1_0.PathFinding_0.MoveToExit(Enums.Area.CatacombsLevel4);
                CurrentStep++;
            }

            if (CurrentStep == 3)
            {
                //####
                if ((Enums.Area)Form1_0.PlayerScan_0.levelNo == Enums.Area.CatacombsLevel3)
                {
                    CurrentStep--;
                    return;
                }
                //####

                /*X: 22561,
                Y: 9553,*/
                if (Form1_0.Mover_0.MoveToLocation(22556, 9544))
                {
                    DetectedBoss = false;
                    CurrentStep++;
                }
            }

            if (CurrentStep == 4)
            {
                Form1_0.Potions_0.CanUseSkillForRegen = false;
                Form1_0.SetGameStatus("KILLING ANDARIEL");

                //#############
                Form1_0.MobsStruc_0.DetectThisMob("getBossName", "Andariel", false, 200, new List<long>());
                bool DetectedAndy = Form1_0.MobsStruc_0.GetMobs("getBossName", "Andariel", false, 200, new List<long>());
                DateTime StartTime = DateTime.Now;
                TimeSpan TimeSinceDetecting = DateTime.Now - StartTime;
                while (!DetectedAndy && TimeSinceDetecting.TotalSeconds < 5)
                {
                    Form1_0.SetGameStatus("WAITING DETECTING ANDARIEL");
                    DetectedAndy = Form1_0.MobsStruc_0.GetMobs("getBossName", "Andariel", false, 200, new List<long>());
                    TimeSinceDetecting = DateTime.Now - StartTime;

                    //cast attack during this waiting time
                    Form1_0.Battle_0.SetSkills();
                    Form1_0.Battle_0.CastSkillsNoMove();
                    Form1_0.ItemsStruc_0.GetItems(true);      //#############
                    Form1_0.Potions_0.CheckIfWeUsePotion();

                    if (!Form1_0.GameStruc_0.IsInGame() || !Form1_0.Running)
                    {
                        Form1_0.overlayForm.ResetMoveToLocation();
                        return;
                    }
                }
                //#############

                if (Form1_0.MobsStruc_0.GetMobs("getBossName", "Andariel", false, 200, new List<long>()))
                {
                    Form1_0.SetGameStatus("KILLING ANDARIEL");
                    if (Form1_0.MobsStruc_0.MobsHP > 0)
                    {
                        DetectedBoss = true;
                        Form1_0.Battle_0.RunBattleScriptOnThisMob("getBossName", "Andariel", new List<long>());
                    }
                    else
                    {
                        if (!DetectedBoss)
                        {
                            Form1_0.method_1("Andariel not detected!", Color.Red);
                            Form1_0.Battle_0.DoBattleScript(15);
                        }

                        if (Form1_0.Battle_0.EndBossBattle()) ScriptDone = true;
                        return;
                    }
                }
                else
                {
                    Form1_0.method_1("Andariel not detected!", Color.Red);

                    Form1_0.Battle_0.DoBattleScript(15);

                    //baal not detected...
                    Form1_0.ItemsStruc_0.GetItems(true);
                    if (Form1_0.MobsStruc_0.GetMobs("getBossName", "Andariel", false, 200, new List<long>())) return; //redetect baal?
                    Form1_0.ItemsStruc_0.GrabAllItemsForGold();
                    if (Form1_0.MobsStruc_0.GetMobs("getBossName", "Andariel", false, 200, new List<long>())) return; //redetect baal?
                    Form1_0.Potions_0.CanUseSkillForRegen = true;

                    if (Form1_0.Battle_0.EndBossBattle()) ScriptDone = true;
                    return;
                }
            }
        }
    }
}
