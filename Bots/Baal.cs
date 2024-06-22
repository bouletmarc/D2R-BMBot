using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static MapAreaStruc;

public class Baal
{
    Form1 Form1_0;

    //#####################################################
    //#####################################################
    //Special Run Variable
    public bool KillBaal = false;
    public List<uint> LeaveIfMobsIsPresent_ID = new List<uint>();
    public List<int> LeaveIfMobsIsPresent_Count = new List<int>();
    public int LeaveIfMobsCountIsAbove = 0;
    public bool SafeYoloStrat = false;
    //#####################################################
    //#####################################################

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

    public int TeleportToBaalTry = 0;
    public bool TryMovingAwayOnLeftSide = true;
    public int MaxMoveAwayTry = 2;

    public int CheckingThroneBackMode = 0;
    public int PortalYOffset = 0;

    public int SafeYoloStratIndex = 0;
    public int DefaultTakeRVPot = 0;

    public void SetForm1(Form1 form1_1)
    {
        Form1_0 = form1_1;
    }

    public void ResetVars()
    {
        if (DefaultTakeRVPot != 0) CharConfig.TakeRVPotUnder = DefaultTakeRVPot;

        CurrentStep = 0;
        ScriptDone = false;
        DetectedBaal = false;
        Wave5Detected = false;
        Wave5Cleared = false;
        IgnoredMobs = new List<long>();
        CornerClearedIndex = 0;
        TimeSinceLastWaveDone = DateTime.MaxValue;
        TimeSinceLastWaveSet = false;
        TeleportToBaalTry = 0;
        TryMovingAwayOnLeftSide = true;
        CheckingThroneBackMode = 0;
        PortalYOffset = 0;
        SafeYoloStratIndex = 0;
        DefaultTakeRVPot = CharConfig.TakeRVPotUnder;
    }

    public void DetectCurrentStep()
    {
        if ((Enums.Area)Form1_0.PlayerScan_0.levelNo == Enums.Area.TheWorldStoneKeepLevel2) CurrentStep = 1;
        if ((Enums.Area)Form1_0.PlayerScan_0.levelNo == Enums.Area.TheWorldStoneKeepLevel3) CurrentStep = 2;
        if ((Enums.Area)Form1_0.PlayerScan_0.levelNo == Enums.Area.ThroneOfDestruction) CurrentStep = 3;
        if ((Enums.Area)Form1_0.PlayerScan_0.levelNo == Enums.Area.TheWorldstoneChamber) CurrentStep = 7;
    }

    public bool LeaveOnMobs()
    {
        bool IsLeaving = false;

        //Check by mobs count
        if (LeaveIfMobsCountIsAbove > 0)
        {
            if (Form1_0.MobsStruc_0.GetMobsCount(0) >= LeaveIfMobsCountIsAbove) IsLeaving = true;
        }

        //Check by mobs ID and count
        if (!IsLeaving)
        {
            if (LeaveIfMobsIsPresent_ID.Count > 0)
            {
                for (int i = 0; i < LeaveIfMobsIsPresent_ID.Count; i++)
                {
                    if (Form1_0.MobsStruc_0.GetMobsCount(LeaveIfMobsIsPresent_ID[i]) >= LeaveIfMobsIsPresent_Count[i])
                    {
                        IsLeaving = true;
                        break;
                    }
                }
            }
        }

        if (IsLeaving)
        {
            Form1_0.method_1("Leaving game (Baal leaving mobs condition)!", Color.Red);
            Form1_0.LeaveGame(false);
        }

        //Yolo Strat
        if (SafeYoloStrat)
        {
            if (Form1_0.MobsStruc_0.GetMobsCount(0) >= 45 && CharConfig.TakeRVPotUnder < 45) CharConfig.TakeRVPotUnder = DefaultTakeRVPot + 10;
            if (Form1_0.MobsStruc_0.GetMobsCount(0) >= 25 && Form1_0.MobsStruc_0.GetMobsCount(0) < 45 && CharConfig.TakeRVPotUnder < 45) CharConfig.TakeRVPotUnder = DefaultTakeRVPot + 5;
            if (Form1_0.MobsStruc_0.GetMobsCount(0) < 25) CharConfig.TakeRVPotUnder = DefaultTakeRVPot;

            int PlayerHPPercent = (int)((Form1_0.PlayerScan_0.PlayerHP * 100) / Form1_0.PlayerScan_0.PlayerMaxHP);
            if (Form1_0.MobsStruc_0.GetMobsCount(0) >= 45
                && PlayerHPPercent < DefaultTakeRVPot - 5
                && Form1_0.BeltStruc_0.GetPotionQuantityInBelt(3) == 0)
            {
                Form1_0.method_1("Leaving game (Baal Yolo Strat)!", Color.Red);
                Form1_0.LeaveGame(false);
            }
        }

        return IsLeaving;
    }

    public void RunScript()
    {
        Form1_0.Town_0.ScriptTownAct = 5; //set to town act 4 when running this script

        if (!Form1_0.Running || !Form1_0.GameStruc_0.IsInGame())
        {
            ScriptDone = true;
            return;
        }

        if (Form1_0.Town_0.GetInTown())
        {
            //stop doing baal if we are in town, and we was actually killing baaal and we dont detect the TP,
            //else it will go back in the WP and redo the whole Baal script again. (for Public Games)
            if (CharConfig.RunBaalScript
                && !ScriptDone
                && CurrentStep >= 7
                && Form1_0.PublicGame)
            {
                Form1_0.Town_0.FastTowning = false;
                Form1_0.Town_0.UseLastTP = false;
                ScriptDone = true;
                return;
            }

            Form1_0.SetGameStatus("GO TO WP");
            CurrentStep = 0;

            Form1_0.Town_0.GoToWPArea(5, 8);
        }
        else
        {
            if (CurrentStep == 0)
            {
                Form1_0.SetGameStatus("DOING BAAL");
                Form1_0.Battle_0.CastDefense();
                //Form1_0.WaitDelay(15);

                if (Form1_0.PlayerScan_0.levelNo == (int)Enums.Area.TheWorldStoneKeepLevel2)
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

                if (Form1_0.PublicGame && !Form1_0.Town_0.TPSpawned)
                {
                    PortalPos.Y += PortalYOffset;
                    Form1_0.PathFinding_0.MoveToThisPos(PortalPos);
                    Form1_0.Town_0.SpawnTP();

                    PortalYOffset -= 6;
                }

                if (!Form1_0.Town_0.TPSpawned)
                {
                    Form1_0.PathFinding_0.MoveToThisPos(PortalPos);
                    Form1_0.PathFinding_0.MoveToThisPos(PortalPos);
                    Form1_0.Town_0.SpawnTP();

                    PortalYOffset -= 6;
                }

                if (LeaveOnMobs()) return;

                Form1_0.PathFinding_0.MoveToThisPos(ThronePos);
                CurrentStep++;
            }

            if (CurrentStep == 4)
            {
                //clear throne area of mobs
                if (CornerClearedIndex == 0)
                {
                    if (LeaveOnMobs()) return;
                    if (Form1_0.PathFinding_0.MoveToThisPos(ThroneCorner1Pos, 4, true)) CornerClearedIndex++;
                    if (LeaveOnMobs()) return;
                }
                else if (CornerClearedIndex == 1)
                {
                    if (LeaveOnMobs()) return;
                    if (Form1_0.PathFinding_0.MoveToThisPos(ThroneCorner1Pos, 4, true)) CornerClearedIndex++;
                    if (LeaveOnMobs()) return;
                    //CornerClearedIndex++;
                }
                else if (CornerClearedIndex == 2)
                {
                    if (LeaveOnMobs()) return;
                    if (Form1_0.PathFinding_0.MoveToThisPos(ThroneCorner1Pos, 4, true)) CornerClearedIndex++;
                    if (LeaveOnMobs()) return;
                    //CornerClearedIndex++;
                }
                else if (CornerClearedIndex == 3)
                {
                    if (LeaveOnMobs()) return;
                    if (Form1_0.PathFinding_0.MoveToThisPos(ThroneCorner1Pos, 4, true)) CornerClearedIndex++;
                    if (LeaveOnMobs()) return;
                    //CornerClearedIndex++;
                }
                if (CornerClearedIndex == 4)
                {
                    if (LeaveOnMobs()) return;
                    //Form1_0.PathFinding_0.MoveToThisPos(ThroneCorner4Pos, 4, true);
                    CurrentStep++;
                }
            }

            if (CurrentStep == 5)
            {
                //clear waves
                if (Form1_0.PlayerScan_0.xPosFinal < ThronePos.X - 3
                    || Form1_0.PlayerScan_0.xPosFinal > ThronePos.X + 3
                    || Form1_0.PlayerScan_0.yPosFinal < ThronePos.Y - 3
                    || Form1_0.PlayerScan_0.yPosFinal > ThronePos.Y + 3)
                {
                    if (!Form1_0.Battle_0.ClearingArea && !Form1_0.Battle_0.DoingBattle)
                    {
                        Form1_0.PathFinding_0.MoveToThisPos(ThronePos, 2, true);
                    }
                    else
                    {
                        TimeSinceLastWaveDone = DateTime.MaxValue;
                        TimeSinceLastWaveSet = false;
                        return;
                    }
                }
                else
                {
                    Form1_0.Battle_0.DoBattleScript(30);

                    if (Form1_0.Battle_0.ClearingArea || Form1_0.Battle_0.DoingBattle)
                    {
                        TimeSinceLastWaveDone = DateTime.MaxValue;
                        TimeSinceLastWaveSet = false;
                        return;
                    }
                }

                if (!Wave5Cleared)
                {
                    if (Form1_0.PublicGame && Form1_0.PlayerScan_0.HasAnyPlayerInArea((int)Enums.Area.TheWorldstoneChamber))
                    {
                        Form1_0.method_1("People detected in Worldstone chamber, switching to baal script!", Color.OrangeRed);

                        Form1_0.KeyMouse_0.MouseClicc_RealPos(Form1_0.CenterX, Form1_0.ScreenYOffset); //drop possible items on curson to ground
                        CurrentStep++;
                    }

                    //DETECT OTHERS WAVES FOR CASTING
                    if (!TimeSinceLastWaveSet && !Form1_0.MobsStruc_0.GetMobs("", "", true, 25, IgnoredMobs))
                    {
                        if (!Form1_0.PublicGame || !Form1_0.PlayerScan_0.HasBattleOrderState) Form1_0.Battle_0.CastDefense();
                        TimeSinceLastWaveDone = DateTime.Now;
                        TimeSinceLastWaveSet = true;
                        Form1_0.InventoryStruc_0.DumpBadItemsOnGround();
                    }

                    //START CASTING IN ADVANCE
                    if ((DateTime.Now - TimeSinceLastWaveDone).TotalSeconds > CharConfig.BaalWavesCastDelay)
                    {
                        Form1_0.Battle_0.SetSkills();
                        Form1_0.Battle_0.CastSkillsNoMove();
                    }

                    //STOP CASTING ERROR DETECTING MOBS/BAAL MOVED
                    if ((DateTime.Now - TimeSinceLastWaveDone).TotalSeconds > 25)
                    {
                        TimeSinceLastWaveDone = DateTime.MaxValue;
                        TimeSinceLastWaveSet = false;

                        if (CheckingThroneBackMode == 0)
                        {
                            Form1_0.method_1("Mobs undetected, moving back to clear Throne!", Color.OrangeRed);
                            CornerClearedIndex = 0;
                            CurrentStep--;
                            CheckingThroneBackMode = 1;
                            return;
                        }
                        else if (CheckingThroneBackMode == 1)
                        {
                            Form1_0.method_1("Mobs undetected, moving forward to Kill Baal!", Color.OrangeRed);
                            CurrentStep++;
                            CheckingThroneBackMode = 0;
                            return;
                        }
                    }

                    //STOP CASTING
                    if (Form1_0.MobsStruc_0.GetMobs("", "", true, 30, IgnoredMobs))
                    {
                        TimeSinceLastWaveDone = DateTime.MaxValue;
                        TimeSinceLastWaveSet = false;
                        Form1_0.Battle_0.DoBattleScript(30);
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
                                if (!Form1_0.MobsStruc_0.GetMobs("", "", true, 30, IgnoredMobs))
                                {
                                    Wave5Cleared = true;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (Wave5Detected)
                        {
                            if (!Form1_0.MobsStruc_0.GetMobs("", "", true, 30, IgnoredMobs))
                            {
                                Wave5Cleared = true;
                            }
                        }
                    }
                    //####
                }
                else
                {
                    Form1_0.KeyMouse_0.MouseClicc_RealPos(Form1_0.CenterX, Form1_0.ScreenYOffset); //drop possible items on curson to ground
                    CurrentStep++;
                }
            }


            if (CurrentStep == 6)
            {
                if (!KillBaal)
                {

                    Form1_0.ItemsStruc_0.GrabAllItemsForGold();
                    Form1_0.Battle_0.ClearingArea = false;
                    Form1_0.Battle_0.DoingBattle = false;
                    Form1_0.Potions_0.CanUseSkillForRegen = true;
                    //Form1_0.LeaveGame(true);
                    Form1_0.Town_0.FastTowning = false;
                    Form1_0.Town_0.UseLastTP = false;
                    ScriptDone = true;
                    KillBaal = true;
                    return;
                }

                Form1_0.SetGameStatus("WAITING PORTAL");

                //move to baal red portal
                if (Form1_0.PlayerScan_0.xPosFinal >= 15170 - 40
                    && Form1_0.PlayerScan_0.xPosFinal <= 15170 + 40
                    && Form1_0.PlayerScan_0.yPosFinal >= 5880 - 40
                    && Form1_0.PlayerScan_0.yPosFinal <= 5880 + 40)
                {
                    //Form1_0.Battle_0.CastDefense();
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
                        //Form1_0.PathFinding_0.MoveToThisPos(new Position { X = 15090, Y = 5008 });
                        if (Form1_0.Mover_0.MoveToLocation(15095, 5023))
                        {
                            if (Form1_0.Mover_0.MoveToLocation(15090, 5008))
                            {
                                Form1_0.InventoryStruc_0.DumpBadItemsOnGround();
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

            if (CurrentStep == 7)
            {
                Form1_0.SetGameStatus("MOVING TO BAAL");
                Form1_0.PathFinding_0.MoveToThisPos(new Position { X = 15134, Y = 5920 });
                //Form1_0.WaitDelay(50); //wait a bit to detect baal
                CurrentStep++;
                //15065,5891
            }

            if (CurrentStep == 8)
            {
                Form1_0.Potions_0.CanUseSkillForRegen = false;
                Form1_0.SetGameStatus("KILLING BAAL");
                Form1_0.MobsStruc_0.DetectThisMob("getBossName", "Baal", false, 200, new List<long>());
                if (Form1_0.MobsStruc_0.GetMobs("getBossName", "Baal", false, 200, new List<long>()))
                {
                    TeleportToBaalTry = 0;
                    if (Form1_0.MobsStruc_0.MobsHP > 0)
                    {
                        DetectedBaal = true;
                        Form1_0.Battle_0.RunBattleScriptOnThisMob("getBossName", "Baal", new List<long>());
                    }
                    else
                    {
                        if (Form1_0.Battle_0.EndBossBattle()) ScriptDone = true;
                    }
                }
                else
                {
                    Form1_0.method_1("Baal not detected!", Color.Red);

                    if (TeleportToBaalTry < MaxMoveAwayTry && TryMovingAwayOnLeftSide)
                    {
                        if (Form1_0.Mover_0.MoveToLocation(15062, 5891))
                        {
                            //if (Form1_0.Mover_0.MoveToLocation(15106, 5901))
                            if (Form1_0.Mover_0.MoveToLocation(15134, 5920))
                            {
                                TeleportToBaalTry++;
                                if (Form1_0.MobsStruc_0.GetMobs("getBossName", "Baal", false, 200, new List<long>())) return; //redetect baal?
                            }
                        }
                    }
                    else if (TeleportToBaalTry >= MaxMoveAwayTry && TryMovingAwayOnLeftSide)
                    {
                        TeleportToBaalTry = 1;
                        TryMovingAwayOnLeftSide = false; //now try moving away to the right to try detect baal
                    }
                    else if (TeleportToBaalTry < MaxMoveAwayTry && !TryMovingAwayOnLeftSide)
                    {
                        if (Form1_0.Mover_0.MoveToLocation(15214, 5890))
                        {
                            //if (Form1_0.Mover_0.MoveToLocation(15166, 5908))
                            if (Form1_0.Mover_0.MoveToLocation(15134, 5920))
                            {
                                TeleportToBaalTry++;
                                if (Form1_0.MobsStruc_0.GetMobs("getBossName", "Baal", false, 200, new List<long>())) return; //redetect baal?
                            }
                        }
                    }
                    else if (TeleportToBaalTry >= MaxMoveAwayTry && !TryMovingAwayOnLeftSide)
                    {
                        if (Form1_0.Mover_0.MoveToLocation(15134, 5920))
                        {
                            for (int i = 0; i < 30; i++) //140
                            {
                                Form1_0.PlayerScan_0.GetPositions();

                                Form1_0.Battle_0.SetSkills();
                                Form1_0.Battle_0.CastSkillsNoMove();

                                Form1_0.ItemsStruc_0.GetItems(true);
                                Form1_0.Potions_0.CheckIfWeUsePotion();
                                Form1_0.ItemsStruc_0.GetItems(false);
                                Form1_0.overlayForm.UpdateOverlay();

                                if (Form1_0.MobsStruc_0.GetMobs("getBossName", "Baal", false, 200, new List<long>())) return; //redetect baal?
                            }

                            //baal not detected...
                            Form1_0.ItemsStruc_0.GetItems(true);
                            if (Form1_0.MobsStruc_0.GetMobs("getBossName", "Baal", false, 200, new List<long>())) return; //redetect baal?
                                                                                                                          //if (!Form1_0.PublicGame) Form1_0.ItemsStruc_0.GrabAllItemsForGold();
                            Form1_0.ItemsStruc_0.GrabAllItemsForGold();
                            if (Form1_0.MobsStruc_0.GetMobs("getBossName", "Baal", false, 200, new List<long>())) return; //redetect baal?
                                                                                                                          //if (!Form1_0.PublicGame) Form1_0.ItemsStruc_0.GrabAllItemsForGold();

                            if (Form1_0.Battle_0.EndBossBattle()) ScriptDone = true;
                        }
                    }
                }
            }

        }
    }

}
