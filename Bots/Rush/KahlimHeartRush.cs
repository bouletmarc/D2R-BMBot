using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MapAreaStruc;

public class KahlimHeartRush
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
        if ((Enums.Area)Form1_0.PlayerScan_0.levelNo == Enums.Area.KurastBazaar) CurrentStep = 1;
        if ((Enums.Area)Form1_0.PlayerScan_0.levelNo == Enums.Area.SewersLevel1Act3) CurrentStep = 2;
        if ((Enums.Area)Form1_0.PlayerScan_0.levelNo == Enums.Area.SewersLevel2Act3) CurrentStep = 3;
    }

    public void RunScript()
    {
        Form1_0.Town_0.ScriptTownAct = 3; //set to town act 5 when running this script

        if (!Form1_0.Running || !Form1_0.GameStruc_0.IsInGame())
        {
            ScriptDone = true;
            return;
        }

        if (Form1_0.Town_0.GetInTown())
        {
            Form1_0.SetGameStatus("GO TO WP");
            CurrentStep = 0;

            Form1_0.Town_0.GoToWPArea(3, 5);
        }
        else
        {
            if (CurrentStep == 0)
            {
                Form1_0.SetGameStatus("DOING KAHLIM HEART");
                //Form1_0.Battle_0.CastDefense();
                //Form1_0.WaitDelay(15);

                if ((Enums.Area)Form1_0.PlayerScan_0.levelNo == Enums.Area.KurastBazaar)
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
                if (Form1_0.PlayerScan_0.levelNo == (int)Enums.Area.SewersLevel1Act3)
                {
                    CurrentStep++;
                    return;
                }
                //####

                Form1_0.PathFinding_0.MoveToExit(Enums.Area.SewersLevel1Act3);
                CurrentStep++;
            }

            if (CurrentStep == 2)
            {
                //####
                if (Form1_0.PlayerScan_0.levelNo == (int)Enums.Area.SewersLevel2Act3)
                {
                    CurrentStep++;
                    return;
                }
                if (Form1_0.PlayerScan_0.levelNo != (int)Enums.Area.SewersLevel1Act3)
                {
                    CurrentStep--;
                    return;
                }
                //####

                Form1_0.PathFinding_0.MoveToExit(Enums.Area.SewersLevel2Act3);

                ChestPos = Form1_0.MapAreaStruc_0.GetPositionOfObject("object", "Act3SewerStairsToLevel3", (int)Enums.Area.SewersLevel1Act3, new List<int>());
                if (ChestPos.X != 0 && ChestPos.Y != 0)
                {
                    Form1_0.PathFinding_0.MoveToThisPos(ChestPos);

                    Form1_0.Battle_0.SetSkills();
                    Form1_0.Battle_0.CastSkillsNoMove();

                    //repeat clic on leverfor stair
                    int tryyy = 0;
                    while (tryyy <= 25)
                    {
                        Position itemScreenPos = Form1_0.GameStruc_0.World2Screen(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, ChestPos.X, ChestPos.Y);

                        Form1_0.KeyMouse_0.MouseClicc_RealPos(itemScreenPos.X, itemScreenPos.Y);
                        Form1_0.PlayerScan_0.GetPositions();
                        Form1_0.WaitDelay(2);
                        tryyy++;
                    }

                    CurrentStep++;
                }
                else
                {
                    Form1_0.method_1("Lever location not detected!", Color.Red);
                    Form1_0.Town_0.FastTowning = false;
                    Form1_0.Town_0.UseLastTP = false;
                    ScriptDone = true;
                    return;
                }

                Form1_0.PathFinding_0.MoveToExit(Enums.Area.SewersLevel2Act3);

                CurrentStep++;
            }

            if (CurrentStep == 3)
            {
                //####
                if (Form1_0.PlayerScan_0.levelNo != (int)Enums.Area.SewersLevel2Act3)
                {
                    CurrentStep--;
                    return;
                }
                //####

                ChestPos = Form1_0.MapAreaStruc_0.GetPositionOfObject("object", "KhalimChest1", (int)Enums.Area.SewersLevel2Act3, new List<int>());
                if (ChestPos.X != 0 && ChestPos.Y != 0)
                {
                    Form1_0.PathFinding_0.MoveToThisPos(ChestPos);

                    //repeat clic on chest
                    int tryyy = 0;
                    while (tryyy <= 25)
                    {
                        Position itemScreenPos = Form1_0.GameStruc_0.World2Screen(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, ChestPos.X, ChestPos.Y);

                        Form1_0.KeyMouse_0.MouseClicc_RealPos(itemScreenPos.X, itemScreenPos.Y);
                        Form1_0.PlayerScan_0.GetPositions();
                        tryyy++;
                    }

                    CurrentStep++;
                }
                else
                {
                    Form1_0.method_1("Kahlim Heart Chest location not detected!", Color.Red);
                    Form1_0.Town_0.FastTowning = false;
                    Form1_0.Town_0.UseLastTP = false;
                    ScriptDone = true;
                    return;
                }
            }

            if (CurrentStep == 4)
            {
                if (!Form1_0.Battle_0.DoBattleScript(10))
                {
                    Position ThisTPPos = new Position { X = ChestPos.X - 10, Y = ChestPos.Y + 5 };
                    Form1_0.PathFinding_0.MoveToThisPos(ThisTPPos);

                    Form1_0.Town_0.TPSpawned = false;

                    CurrentStep++;
                }
            }

            if (CurrentStep == 5)
            {
                Form1_0.SetGameStatus("Kahlim Heart waiting on leecher");

                if (!Form1_0.Town_0.TPSpawned) Form1_0.Town_0.SpawnTP();

                Form1_0.Battle_0.DoBattleScript(10);

                //get leecher infos
                Form1_0.PlayerScan_0.GetLeechPositions();

                if (Form1_0.PlayerScan_0.LeechlevelNo == (int)Enums.Area.SewersLevel2Act3)
                {
                    CurrentStep++;
                }
            }

            if (CurrentStep == 6)
            {
                Form1_0.SetGameStatus("Kahlim Heart waiting on leecher #2");

                Form1_0.Battle_0.DoBattleScript(10);

                //get leecher infos
                Form1_0.PlayerScan_0.GetLeechPositions();

                if (Form1_0.PlayerScan_0.LeechlevelNo == (int)Enums.Area.KurastDocks)
                {
                    Form1_0.Town_0.FastTowning = false;
                    Form1_0.Town_0.UseLastTP = false;
                    ScriptDone = true;
                }
            }
        }
    }
}
