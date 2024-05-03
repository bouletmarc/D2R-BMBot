using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static MapAreaStruc;

public class BaalRush
{
    Form1 Form1_0;

    public int CurrentStep = 0;
    public bool ScriptDone = false;
    public bool DetectedBaal = false;

    public List<long> IgnoredMobs = new List<long>();

    public Position ThronePos = new Position { X = 15095, Y = 5029 };
    public Position PortalPos = new Position { X = 15116, Y = 5071 };

    public bool Wave5Detected = false;
    public bool Wave5Cleared = false;

    public Position ThroneCorner1Pos = new Position { X = 15104, Y = 5062 };
    public Position ThroneCorner2Pos = new Position { X = 15082, Y = 5063 };
    public Position ThroneCorner3Pos = new Position { X = 15081, Y = 5016 };
    public Position ThroneCorner4Pos = new Position { X = 15112, Y = 5013 };

    public int CornerClearedIndex = 0;
    public int BufferPathFindingMoveSize = 0;

    public DateTime TimeSinceLastWaveDone = DateTime.Now;
    public bool TimeSinceLastWaveSet = false;

    public void SetForm1(Form1 form1_1)
    {
        Form1_0 = form1_1;
    }

    public void ResetVars()
    {
        CurrentStep = 0;
        ScriptDone = false;
        DetectedBaal = false;
        Wave5Detected = false;
        Wave5Cleared = false;
        IgnoredMobs = new List<long>();
        CornerClearedIndex = 0;
        TimeSinceLastWaveDone = DateTime.Now;
        TimeSinceLastWaveSet = false;
    }

    public void DetectCurrentStep()
    {
        if ((Enums.Area)Form1_0.PlayerScan_0.levelNo == Enums.Area.TheWorldStoneKeepLevel2) CurrentStep = 1;
        if ((Enums.Area)Form1_0.PlayerScan_0.levelNo == Enums.Area.TheWorldStoneKeepLevel3) CurrentStep = 2;
        if ((Enums.Area)Form1_0.PlayerScan_0.levelNo == Enums.Area.ThroneOfDestruction) CurrentStep = 3;
    }

    public void RunScript()
    {
        Form1_0.Town_0.ScriptTownAct = 4; //set to town act 4 when running this script

        if (!Form1_0.Running || !Form1_0.GameStruc_0.IsInGame())
        {
            ScriptDone = true;
            return;
        }

        if (Form1_0.Town_0.GetInTown())
        {
            Form1_0.SetGameStatus("GO TO WP");
            CurrentStep = 0;

            Form1_0.Town_0.GoToWPArea(5, 8);
        }
        else
        {
            if (CurrentStep == 0)
            {
                Form1_0.SetGameStatus("DOING BAAL");
                //Form1_0.Battle_0.CastDefense();
                //Form1_0.WaitDelay(15);

                if (Form1_0.PlayerScan_0.levelNo == (int)Enums.Area.TheWorldStoneKeepLevel2)
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
                if (Form1_0.PlayerScan_0.levelNo == (int)Enums.Area.TheWorldStoneKeepLevel3)
                {
                    CurrentStep++;
                    return;
                }
                //####

                Form1_0.PathFinding_0.MoveToExit(Enums.Area.TheWorldStoneKeepLevel3);
                CurrentStep++;
            }

            if (CurrentStep == 2)
            {
                //####
                if (Form1_0.PlayerScan_0.levelNo == (int)Enums.Area.ThroneOfDestruction)
                {
                    CurrentStep++;
                    return;
                }
                if ((Enums.Area)Form1_0.PlayerScan_0.levelNo == Enums.Area.TheWorldStoneKeepLevel2)
                {
                    CurrentStep--;
                    return;
                }
                //####

                Form1_0.PathFinding_0.MoveToExit(Enums.Area.ThroneOfDestruction);
                Form1_0.Town_0.TPSpawned = false;
                CurrentStep++;
            }

            if (CurrentStep == 3)
            {
                //####
                if (Form1_0.PlayerScan_0.levelNo == (int)Enums.Area.TheWorldStoneKeepLevel3)
                {
                    CurrentStep--;
                    return;
                }
                //####

                if (!Form1_0.Town_0.TPSpawned)
                {
                    Form1_0.PathFinding_0.MoveToThisPos(PortalPos);
                    Form1_0.Town_0.SpawnTP();
                }

                Form1_0.PathFinding_0.MoveToThisPos(ThronePos);
                CurrentStep++;
            }

            if (CurrentStep == 4)
            {
                //clear throne area of mobs
                if (CornerClearedIndex == 0)
                {
                    Form1_0.PathFinding_0.MoveToThisPos(ThroneCorner1Pos, 4, true);
                    CornerClearedIndex++;
                }
                else if (CornerClearedIndex == 1)
                {
                    Form1_0.PathFinding_0.MoveToThisPos(ThroneCorner2Pos, 4, true);
                    CornerClearedIndex++;
                }
                else if (CornerClearedIndex == 2)
                {
                    Form1_0.PathFinding_0.MoveToThisPos(ThroneCorner4Pos, 4, true);
                    CornerClearedIndex++;
                }
                else if (CornerClearedIndex == 3)
                {
                    Form1_0.PathFinding_0.MoveToThisPos(ThroneCorner3Pos, 4, true);
                    CornerClearedIndex++;
                }
                if (CornerClearedIndex == 4)
                {
                    //Form1_0.PathFinding_0.MoveToThisPos(ThroneCorner4Pos, 4, true);

                    Form1_0.PathFinding_0.MoveToThisPos(ThroneCorner1Pos, 4, true);
                    CurrentStep++;
                }
            }

            if (CurrentStep == 5)
            {
                Form1_0.SetGameStatus("Baal waiting on leecher");

                Form1_0.Battle_0.DoBattleScript(15);

                //get leecher infos
                Form1_0.PlayerScan_0.GetLeechPositions();

                if (Form1_0.PlayerScan_0.LeechlevelNo == (int)Enums.Area.ThroneOfDestruction)
                {
                    CurrentStep++;
                }
            }

            if (CurrentStep == 6)
            {
                //clear waves
                if (Form1_0.PlayerScan_0.xPosFinal < ThronePos.X - 3
                    || Form1_0.PlayerScan_0.xPosFinal > ThronePos.X + 3
                    || Form1_0.PlayerScan_0.yPosFinal < ThronePos.Y - 3
                    || Form1_0.PlayerScan_0.yPosFinal > ThronePos.Y + 3)
                {
                    Form1_0.PathFinding_0.MoveToThisPos(ThronePos, 4, true);
                }
                else
                {
                    Form1_0.Battle_0.DoBattleScript(30);
                }

                if (!Wave5Cleared)
                {
                    //DETECT OTHERS WAVES FOR CASTING
                    if (!TimeSinceLastWaveSet && !Form1_0.MobsStruc_0.GetMobs("", "", true, 25, IgnoredMobs))
                    {
                        TimeSinceLastWaveDone = DateTime.Now;
                        TimeSinceLastWaveSet = true;
                    }

                    //START CASTING IN ADVANCE
                    if ((DateTime.Now - TimeSinceLastWaveDone).TotalSeconds > CharConfig.BaalWavesCastDelay)
                    {
                        Form1_0.Battle_0.SetSkills();
                        Form1_0.Battle_0.CastSkillsNoMove();
                    }

                    //STOP CASTING
                    if (Form1_0.MobsStruc_0.GetMobs("", "", true, 25, IgnoredMobs))
                    {
                        TimeSinceLastWaveDone = DateTime.Now;
                        TimeSinceLastWaveSet = false;
                    }

                    //#### DETECT WAVE 5
                    if (Form1_0.MobsStruc_0.GetMobs("getSuperUniqueName", "Baal Subject 5", false, 99, IgnoredMobs))
                    {
                        if (Form1_0.MobsStruc_0.MobsHP > 0)
                        {
                            Wave5Detected = true;
                        }
                        else
                        {
                            if (Wave5Detected)
                            {
                                if (!Form1_0.MobsStruc_0.GetMobs("", "", true, 25, IgnoredMobs))
                                {
                                    Wave5Cleared = true;
                                }
                            }
                        }
                    }
                    //####

                    //leecher already in baal chamber.. move to baal chamber then
                    if (Form1_0.PlayerScan_0.LeechlevelNo == (int)Enums.Area.TheWorldstoneChamber)
                    {
                        CurrentStep++;
                    }
                }
                else
                {
                    CurrentStep++;
                }
            }



            if (CurrentStep == 7)
            {
                Form1_0.SetGameStatus("WAITING PORTAL");

                //move to baal red portal
                if (Form1_0.PlayerScan_0.xPosFinal >= 15170 - 40
                    && Form1_0.PlayerScan_0.xPosFinal <= 15170 + 40
                    && Form1_0.PlayerScan_0.yPosFinal >= 5880 - 40
                    && Form1_0.PlayerScan_0.yPosFinal <= 5880 + 40)
                {
                    Form1_0.Battle_0.CastDefense();
                    CurrentStep++;
                }
                else
                {
                    if (Form1_0.PlayerScan_0.xPosFinal < 15090 - 3
                        || Form1_0.PlayerScan_0.xPosFinal > 15090 + 3
                        || Form1_0.PlayerScan_0.yPosFinal < 5008 - 3
                        || Form1_0.PlayerScan_0.yPosFinal > 5008 + 3)
                    {
                        if (!CharConfig.UseTeleport)
                        {
                            Form1_0.Mover_0.MoveAcceptOffset = 1;
                        }
                        else
                        {
                            Form1_0.Mover_0.MoveAcceptOffset = 3;
                        }
                        if (Form1_0.Mover_0.MoveToLocation(15095, 5023))
                        {
                            if (Form1_0.Mover_0.MoveToLocation(15090, 5008))
                            {
                                Form1_0.Battle_0.CastDefense();
                                Form1_0.Mover_0.MoveAcceptOffset = 4;
                            }
                        }
                    }
                    else
                    {
                        Position itemScreenPos = Form1_0.GameStruc_0.World2Screen(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, 15091, 5005);

                        Form1_0.KeyMouse_0.MouseClicc_RealPos(itemScreenPos.X - 5, itemScreenPos.Y - 20);
                        Form1_0.WaitDelay(10);
                    }
                }
            }

            if (CurrentStep == 8)
            {
                Form1_0.SetGameStatus("Baal waiting on leecher #2");

                //get leecher infos
                Form1_0.PlayerScan_0.GetLeechPositions();

                if (Form1_0.PlayerScan_0.LeechlevelNo == (int)Enums.Area.TheWorldstoneChamber)
                {
                    CurrentStep++;
                }
            }

            if (CurrentStep == 9)
            {
                Form1_0.SetGameStatus("MOVING TO BAAL");
                Form1_0.PathFinding_0.MoveToThisPos(new Position { X = 15134, Y = 5927 });
                //Form1_0.WaitDelay(50); //wait a bit to detect baal
                CurrentStep++;
            }

            if (CurrentStep == 10)
            {
                Form1_0.Potions_0.CanUseSkillForRegen = false;
                Form1_0.SetGameStatus("KILLING BAAL");
                Form1_0.MobsStruc_0.DetectThisMob("getBossName", "Baal", false, 200, new List<long>());
                if (Form1_0.MobsStruc_0.GetMobs("getBossName", "Baal", false, 200, new List<long>()))
                {
                    if (Form1_0.MobsStruc_0.MobsHP > 0)
                    {
                        DetectedBaal = true;
                        Form1_0.Battle_0.RunBattleScriptOnThisMob("getBossName", "Baal", new List<long>());
                    }
                    else
                    {

                        if (Form1_0.Battle_0.EndBossBattle())
                        {
                            ScriptDone = true;
                        }
                    }
                }
                else
                {
                    Form1_0.method_1("Baal not detected!", Color.Red);

                    //baal not detected...
                    Form1_0.ItemsStruc_0.GetItems(true);
                    if (Form1_0.MobsStruc_0.GetMobs("getBossName", "Baal", false, 200, new List<long>())) return; //redetect baal?
                    Form1_0.ItemsStruc_0.GrabAllItemsForGold();
                    if (Form1_0.MobsStruc_0.GetMobs("getBossName", "Baal", false, 200, new List<long>())) return; //redetect baal?

                    Form1_0.Potions_0.CanUseSkillForRegen = true;
                    //Form1_0.LeaveGame(true);
                    Form1_0.Town_0.UseLastTP = false;
                    Form1_0.Town_0.FastTowning = false;
                    ScriptDone = true;
                }
            }

        }
    }

}
