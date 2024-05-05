using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MapAreaStruc;

public class Mephisto
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
        if ((Enums.Area)Form1_0.PlayerScan_0.levelNo == Enums.Area.DuranceOfHateLevel2) CurrentStep = 1;
        if ((Enums.Area)Form1_0.PlayerScan_0.levelNo == Enums.Area.DuranceOfHateLevel3) CurrentStep = 2;
    }

    public void RunScript()
    {
        Form1_0.Town_0.ScriptTownAct = 5; //set to town act 5 when running this script

        if (!Form1_0.Running || !Form1_0.GameStruc_0.IsInGame())
        {
            ScriptDone = true;
            return;
        }

        if (Form1_0.Town_0.GetInTown())
        {
            Form1_0.SetGameStatus("GO TO WP");
            CurrentStep = 0;

            Form1_0.Town_0.GoToWPArea(3, 8);
        }
        else
        {
            if (CurrentStep == 0)
            {
                Form1_0.SetGameStatus("DOING MEPHISTO");
                Form1_0.Battle_0.CastDefense();
                Form1_0.WaitDelay(15);

                if ((Enums.Area)Form1_0.PlayerScan_0.levelNo == Enums.Area.DuranceOfHateLevel2)
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
                if (Form1_0.PlayerScan_0.levelNo == (int)Enums.Area.DuranceOfHateLevel3)
                {
                    CurrentStep++;
                    return;
                }
                //####

                Form1_0.PathFinding_0.MoveToExit(Enums.Area.DuranceOfHateLevel3);
                CurrentStep++;
            }

            if (CurrentStep == 2)
            {
                //####
                if ((Enums.Area)Form1_0.PlayerScan_0.levelNo == Enums.Area.DuranceOfHateLevel2)
                {
                    CurrentStep = 1;
                    return;
                }
                //####

                /*X: 22561,
                Y: 9553,*/
                if (Form1_0.Mover_0.MoveToLocation(17568, 8069))
                {
                    CurrentStep++;
                }
            }

            if (CurrentStep == 3)
            {
                //####
                if ((Enums.Area)Form1_0.PlayerScan_0.levelNo == Enums.Area.DuranceOfHateLevel2)
                {
                    CurrentStep = 1;
                    return;
                }
                //####

                Form1_0.Potions_0.CanUseSkillForRegen = false;
                Form1_0.SetGameStatus("KILLING MEPHISTO");
                Form1_0.MobsStruc_0.DetectThisMob("getBossName", "Mephisto", false, 200, new List<long>());
                if (Form1_0.MobsStruc_0.GetMobs("getBossName", "Mephisto", false, 200, new List<long>()))
                {
                    if (Form1_0.MobsStruc_0.MobsHP > 0)
                    {
                        Form1_0.Battle_0.RunBattleScriptOnThisMob("getBossName", "Mephisto", new List<long>());
                    }
                    else
                    {
                        if (Form1_0.Battle_0.EndBossBattle())
                        {
                            Form1_0.Town_0.FastTowning = false;
                            Form1_0.Town_0.UseLastTP = false;
                            ScriptDone = true;

                            //Position ThisFinalPosition = Form1_0.MapAreaStruc_0.GetPositionOfObject("object", "portal", 102 - 1, new List<int>() { });
                            //if (Form1_0.Mover_0.MoveToLocation(ThisFinalPosition.X, ThisFinalPosition.Y))
                            /*while (Form1_0.PlayerScan_0.levelNo == (int)Enums.Area.DuranceOfHateLevel3)
                            {
                                Form1_0.ItemsStruc_0.GetItems(true);
                                if (Form1_0.Mover_0.MoveToLocation(17601, 8070))
                                {
                                    Position itemScreenPos = Form1_0.GameStruc_0.World2Screen(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, 17601, 8070);
                                    //Position itemScreenPos = Form1_0.GameStruc_0.World2Screen(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, ThisFinalPosition.X, ThisFinalPosition.Y);

                                    Form1_0.KeyMouse_0.MouseClicc_RealPos(itemScreenPos.X, itemScreenPos.Y - 15);

                                    Form1_0.PlayerScan_0.GetPositions();
                                }
                            }

                            Form1_0.WaitDelay(CharConfig.MephistoRedPortalEnterDelay);

                            Form1_0.Town_0.FastTowning = false;
                            Form1_0.Town_0.UseLastTP = false;
                            ScriptDone = true;*/
                            return;
                            //Form1_0.LeaveGame(true);
                        }
                    }
                }
                else
                {
                    Form1_0.method_1("Mephisto not detected!", Color.Red);

                    //baal not detected...
                    Form1_0.ItemsStruc_0.GetItems(true);
                    if (Form1_0.MobsStruc_0.GetMobs("getBossName", "Mephisto", false, 200, new List<long>())) return; //redetect baal?
                    Form1_0.ItemsStruc_0.GrabAllItemsForGold();
                    if (Form1_0.MobsStruc_0.GetMobs("getBossName", "Mephisto", false, 200, new List<long>())) return; //redetect baal?
                    Form1_0.Potions_0.CanUseSkillForRegen = true;

                    if (Form1_0.Battle_0.EndBossBattle()) ScriptDone = true;
                    return;
                }
            }
        }
    }
}
