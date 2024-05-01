using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Countess
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
        if ((Enums.Area)Form1_0.PlayerScan_0.levelNo == Enums.Area.ForgottenTower) CurrentStep = 1;
        if ((Enums.Area)Form1_0.PlayerScan_0.levelNo == Enums.Area.TowerCellarLevel1) CurrentStep = 2;
        if ((Enums.Area)Form1_0.PlayerScan_0.levelNo == Enums.Area.TowerCellarLevel2) CurrentStep = 3;
        if ((Enums.Area)Form1_0.PlayerScan_0.levelNo == Enums.Area.TowerCellarLevel3) CurrentStep = 4;
        if ((Enums.Area)Form1_0.PlayerScan_0.levelNo == Enums.Area.TowerCellarLevel4) CurrentStep = 5;
        if ((Enums.Area)Form1_0.PlayerScan_0.levelNo == Enums.Area.TowerCellarLevel5) CurrentStep = 6;
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

            Form1_0.Town_0.GoToWPArea(1, 4);
        }
        else
        {
            if (CurrentStep == 0)
            {
                Form1_0.SetGameStatus("DOING COUNTESS");
                Form1_0.Battle_0.CastDefense();
                Form1_0.WaitDelay(15);

                if ((Enums.Area)Form1_0.PlayerScan_0.levelNo == Enums.Area.BlackMarsh)
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
                if (Form1_0.PlayerScan_0.levelNo == (int)Enums.Area.ForgottenTower)
                {
                    CurrentStep++;
                    return;
                }
                //####

                Form1_0.PathFinding_0.MoveToExit(Enums.Area.ForgottenTower);
                CurrentStep++;
            }

            if (CurrentStep == 2)
            {
                //####
                if (Form1_0.PlayerScan_0.levelNo == (int)Enums.Area.TowerCellarLevel1)
                {
                    CurrentStep++;
                    return;
                }
                if ((Enums.Area)Form1_0.PlayerScan_0.levelNo == Enums.Area.BlackMarsh)
                {
                    CurrentStep--;
                    return;
                }
                //####

                Form1_0.PathFinding_0.MoveToExit(Enums.Area.TowerCellarLevel1);
                CurrentStep++;
            }

            if (CurrentStep == 3)
            {
                //####
                if (Form1_0.PlayerScan_0.levelNo == (int)Enums.Area.TowerCellarLevel2)
                {
                    CurrentStep++;
                    return;
                }
                if ((Enums.Area)Form1_0.PlayerScan_0.levelNo == Enums.Area.ForgottenTower)
                {
                    CurrentStep--;
                    return;
                }
                //####

                Form1_0.PathFinding_0.MoveToExit(Enums.Area.TowerCellarLevel2);
                CurrentStep++;
            }

            if (CurrentStep == 4)
            {
                //####
                if (Form1_0.PlayerScan_0.levelNo == (int)Enums.Area.TowerCellarLevel3)
                {
                    CurrentStep++;
                    return;
                }
                if ((Enums.Area)Form1_0.PlayerScan_0.levelNo == Enums.Area.TowerCellarLevel1)
                {
                    CurrentStep--;
                    return;
                }
                //####

                Form1_0.PathFinding_0.MoveToExit(Enums.Area.TowerCellarLevel3);
                CurrentStep++;
            }

            if (CurrentStep == 5)
            {
                //####
                if (Form1_0.PlayerScan_0.levelNo == (int)Enums.Area.TowerCellarLevel4)
                {
                    CurrentStep++;
                    return;
                }
                if ((Enums.Area)Form1_0.PlayerScan_0.levelNo == Enums.Area.TowerCellarLevel2)
                {
                    CurrentStep--;
                    return;
                }
                //####

                Form1_0.PathFinding_0.MoveToExit(Enums.Area.TowerCellarLevel4);
                CurrentStep++;
            }

            if (CurrentStep == 6)
            {
                //####
                if (Form1_0.PlayerScan_0.levelNo == (int)Enums.Area.TowerCellarLevel5)
                {
                    CurrentStep++;
                    return;
                }
                if ((Enums.Area)Form1_0.PlayerScan_0.levelNo == Enums.Area.TowerCellarLevel3)
                {
                    CurrentStep--;
                    return;
                }
                //####

                Form1_0.PathFinding_0.MoveToExit(Enums.Area.TowerCellarLevel5);
                CurrentStep++;
            }

            if (CurrentStep == 7)
            {
                //####
                if ((Enums.Area)Form1_0.PlayerScan_0.levelNo == Enums.Area.TowerCellarLevel4)
                {
                    CurrentStep--;
                    return;
                }
                //####

                MapAreaStruc.Position ThisChestPos = Form1_0.MapAreaStruc_0.GetPositionOfObject("object", "GoodChest", (int)Enums.Area.TowerCellarLevel5, new List<int>());

                //Form1_0.ItemsStruc_0.GrabAllItemsForGold();
                if (Form1_0.Mover_0.MoveToLocation(ThisChestPos.X, ThisChestPos.Y))
                {
                    CurrentStep++;
                }
            }

            if (CurrentStep == 8)
            {
                Form1_0.Potions_0.CanUseSkillForRegen = false;
                Form1_0.SetGameStatus("KILLING COUNTESS");
                Form1_0.MobsStruc_0.DetectThisMob("getSuperUniqueName", "The Countess", false, 200, new List<long>());
                if (Form1_0.MobsStruc_0.GetMobs("getSuperUniqueName", "The Countess", false, 200, new List<long>()))
                {
                    if (Form1_0.MobsStruc_0.MobsHP > 0)
                    {
                        Form1_0.Battle_0.RunBattleScriptOnThisMob("getSuperUniqueName", "The Countess", new List<long>());
                    }
                    else
                    {
                        if (Form1_0.Battle_0.EndBossBattle()) ScriptDone = true;
                        return;
                    }
                }
                else
                {
                    Form1_0.method_1("Countess not detected!", Color.Red);

                    //baal not detected...
                    Form1_0.ItemsStruc_0.GetItems(true);
                    if (Form1_0.MobsStruc_0.GetMobs("getSuperUniqueName", "The Countess", false, 200, new List<long>())) return; //redetect baal?
                    Form1_0.ItemsStruc_0.GrabAllItemsForGold();
                    if (Form1_0.MobsStruc_0.GetMobs("getSuperUniqueName", "The Countess", false, 200, new List<long>())) return; //redetect baal?
                    Form1_0.Potions_0.CanUseSkillForRegen = true;
                    if (Form1_0.Battle_0.EndBossBattle()) ScriptDone = true;
                    return;
                }
            }
        }
    }
}
