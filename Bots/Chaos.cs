using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static MapAreaStruc;

public class Chaos
{
    Form1 Form1_0;

    //#####################################################
    //#####################################################
    //Special Run Variable
    public bool FastChaos = false;
    //#####################################################
    //#####################################################

    public int CurrentStep = 0;
    public bool ScriptDone = false;
    public bool DetectedBoss = false;

    public Position EntrancePos = new Position { X = 7796, Y = 5561 };
    public Position DiabloSpawnPos = new Position { X = 7794, Y = 5294 }; //7800,5286

    public Position TPPos = new Position { X = 7760, Y = 5305 };

    public Position CurrentSealPos = new Position { X = 0, Y = 0 };

    public DateTime StartTimeUniqueBossWaiting = DateTime.Now;
    public bool TimeSetForWaitingUniqueBoss = false;
    public int TryCountWaitingUniqueBoss = 0;

    public int BufferPathFindingMoveSize = 0;
    public int SealType = 0;

    public bool MovedToTPPos = false;
    public bool CastedAtSeis = false;

    public bool FastChaosPopingSeals = false;

    public void SetForm1(Form1 form1_1)
    {
        Form1_0 = form1_1;
    }

    public void ResetVars()
    {
        CurrentStep = 0;
        ScriptDone = false;
        DetectedBoss = false;
        TimeSetForWaitingUniqueBoss = false;
        TryCountWaitingUniqueBoss = 0;
        StartTimeUniqueBossWaiting = DateTime.Now;
        MovedToTPPos = false;
        CastedAtSeis = false;
        FastChaosPopingSeals = false;
    }

    public void DetectCurrentStep()
    {
        if ((Enums.Area)Form1_0.PlayerScan_0.levelNo == Enums.Area.RiverOfFlame) CurrentStep = 1;
        if ((Enums.Area)Form1_0.PlayerScan_0.levelNo == Enums.Area.ChaosSanctuary) CurrentStep = 3;
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

            Form1_0.Town_0.GoToWPArea(4, 2);
        }
        else
        {
            if (CurrentStep == 0)
            {
                Form1_0.SetGameStatus("DOING CHAOS");
                Form1_0.Battle_0.CastDefense();
                //Form1_0.WaitDelay(15);

                if (Form1_0.PlayerScan_0.levelNo == (int)Enums.Area.RiverOfFlame)
                {
                    CurrentStep++;
                }
                else if (Form1_0.PlayerScan_0.levelNo == (int)Enums.Area.ChaosSanctuary)
                {
                    CurrentStep = 3;
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
                if (Form1_0.PlayerScan_0.levelNo == (int)Enums.Area.ChaosSanctuary)
                {
                    CurrentStep++;
                    return;
                }
                //####

                //Form1_0.PathFinding_0.MoveToNextArea(Enums.Area.ChaosSanctuary);
                //Form1_0.PathFinding_0.MoveToThisPos(EntrancePos);
                //CurrentStep++;

                Position MidPos = new Position { X = 7800, Y = 5761 };
                if (Form1_0.Mover_0.MoveToLocation(MidPos.X, MidPos.Y))
                {
                    Form1_0.Town_0.TPSpawned = false;
                    CurrentStep++;
                }
            }

            if (CurrentStep == 2)
            {
                //####
                if (Form1_0.PlayerScan_0.levelNo == (int)Enums.Area.ChaosSanctuary)
                {
                    CurrentStep++;
                    return;
                }
                //####

                if (Form1_0.Mover_0.MoveToLocation(EntrancePos.X, EntrancePos.Y))
                {
                    if (Form1_0.PublicGame && !Form1_0.Town_0.TPSpawned) Form1_0.Town_0.SpawnTP();
                    if (!Form1_0.Town_0.TPSpawned) Form1_0.Battle_0.CastDefense();
                    Form1_0.Town_0.TPSpawned = false;

                    BufferPathFindingMoveSize = Form1_0.PathFinding_0.AcceptMoveOffset;
                    Form1_0.PathFinding_0.AcceptMoveOffset = 15;

                    CurrentStep++;
                }
            }

            if (CurrentStep == 3)
            {
                if (Form1_0.PlayerScan_0.levelNo != (int)Enums.Area.ChaosSanctuary)
                {
                    CurrentStep--;
                    return;
                }

                CurrentSealPos = Form1_0.MapAreaStruc_0.GetPositionOfObject("object", "DiabloSeal5", (int)Enums.Area.ChaosSanctuary, new List<int>());

                if (Form1_0.PlayerScan_0.xPosFinal >= (CurrentSealPos.X - 5)
                    && Form1_0.PlayerScan_0.xPosFinal <= (CurrentSealPos.X + 5)
                    && Form1_0.PlayerScan_0.yPosFinal >= (CurrentSealPos.Y - 5)
                    && Form1_0.PlayerScan_0.yPosFinal <= (CurrentSealPos.Y + 5))
                {
                    int InteractCount = 0;
                    while (InteractCount < 3)
                    {
                        Form1_0.PathFinding_0.MoveToObject("DiabloSeal5");
                        Form1_0.WaitDelay(10);
                        InteractCount++;
                    }

                    //######
                    //KILL VIZIER
                    if (!TimeSetForWaitingUniqueBoss)
                    {
                        StartTimeUniqueBossWaiting = DateTime.Now;
                        TimeSetForWaitingUniqueBoss = true;
                    }

                    if (CurrentSealPos.Y == 5275) SealType = 1;
                    else SealType = 2;

                    if (SealType == 1) Form1_0.PathFinding_0.MoveToThisPos(new Position { X = 7691, Y = 5292 }, 4, true);
                    else Form1_0.PathFinding_0.MoveToThisPos(new Position { X = 7695, Y = 5316 }, 4, true);

                    Form1_0.SetGameStatus("WAITING VIZIER " + (TryCountWaitingUniqueBoss + 1) + "/1");

                    bool UniqueDetected = Form1_0.MobsStruc_0.GetMobs("getSuperUniqueName", "Grand Vizier of Chaos", false, 200, new List<long>());

                    while (!UniqueDetected && (DateTime.Now - StartTimeUniqueBossWaiting).TotalSeconds < CharConfig.ChaosWaitingSealBossDelay)
                    {
                        UniqueDetected = Form1_0.MobsStruc_0.GetMobs("getSuperUniqueName", "Grand Vizier of Chaos", false, 200, new List<long>());

                        Form1_0.PlayerScan_0.GetPositions();
                        Form1_0.overlayForm.UpdateOverlay();
                        Form1_0.GameStruc_0.CheckChickenGameTime();
                        Form1_0.ItemsStruc_0.GetItems(true);
                        Form1_0.Potions_0.CheckIfWeUsePotion();
                        Form1_0.Battle_0.DoBattleScript(10);
                        //###
                        Form1_0.Battle_0.SetSkills();
                        Form1_0.Battle_0.CastSkillsNoMove();
                        //###
                        Application.DoEvents();
                    }

                    if (!UniqueDetected)
                    {
                        TimeSetForWaitingUniqueBoss = false;
                        CurrentStep++;
                    }
                    else
                    {
                        Form1_0.SetGameStatus("KILLING VIZIER");

                        if (Form1_0.MobsStruc_0.GetMobs("getSuperUniqueName", "Grand Vizier of Chaos", false, 200, new List<long>()))
                        {
                            if (Form1_0.MobsStruc_0.MobsHP > 0)
                            {
                                Form1_0.Battle_0.RunBattleScriptOnThisMob("getSuperUniqueName", "Grand Vizier of Chaos", new List<long>());
                            }
                            else
                            {
                                TimeSetForWaitingUniqueBoss = false;
                                CurrentStep++;
                            }
                        }
                        else
                        {
                            TimeSetForWaitingUniqueBoss = false;
                            CurrentStep++;
                        }
                    }
                }
                else
                {
                    if (Form1_0.PlayerScan_0.xPosFinal >= (TPPos.X - 5)
                        && Form1_0.PlayerScan_0.xPosFinal <= (TPPos.X + 5)
                        && Form1_0.PlayerScan_0.yPosFinal >= (TPPos.Y - 5)
                        && Form1_0.PlayerScan_0.yPosFinal <= (TPPos.Y + 5))
                    {
                        if (Form1_0.PublicGame && !Form1_0.Town_0.TPSpawned) Form1_0.Town_0.SpawnTP();
                        if (!FastChaos && !FastChaosPopingSeals) Form1_0.Battle_0.CastDefense();
                        Form1_0.InventoryStruc_0.DumpBadItemsOnGround();
                        MovedToTPPos = true;
                        Form1_0.PathFinding_0.MoveToObject("DiabloSeal5", 4, true);
                    }
                    else
                    {
                        if (!MovedToTPPos) Form1_0.PathFinding_0.MoveToThisPos(TPPos, 4, true);
                        else Form1_0.PathFinding_0.MoveToObject("DiabloSeal5", 4, true);
                    }
                }
            }

            if (CurrentStep == 4)
            {
                CurrentSealPos = Form1_0.MapAreaStruc_0.GetPositionOfObject("object", "DiabloSeal4", (int)Enums.Area.ChaosSanctuary, new List<int>());

                if (Form1_0.PlayerScan_0.xPosFinal >= (CurrentSealPos.X - 5)
                    && Form1_0.PlayerScan_0.xPosFinal <= (CurrentSealPos.X + 5)
                    && Form1_0.PlayerScan_0.yPosFinal >= (CurrentSealPos.Y - 5)
                    && Form1_0.PlayerScan_0.yPosFinal <= (CurrentSealPos.Y + 5))
                {
                    int InteractCount = 0;
                    while (InteractCount < 3)
                    {
                        Form1_0.PathFinding_0.MoveToObject("DiabloSeal4");
                        Form1_0.WaitDelay(10);
                        InteractCount++;
                    }

                    CurrentStep++;
                }
                else
                {
                    Form1_0.PathFinding_0.MoveToObject("DiabloSeal4", 4, true);
                }
            }

            if (CurrentStep == 5)
            {
                CurrentSealPos = Form1_0.MapAreaStruc_0.GetPositionOfObject("object", "DiabloSeal3", (int)Enums.Area.ChaosSanctuary, new List<int>());

                if (Form1_0.PlayerScan_0.xPosFinal >= (CurrentSealPos.X - 5)
                    && Form1_0.PlayerScan_0.xPosFinal <= (CurrentSealPos.X + 5)
                    && Form1_0.PlayerScan_0.yPosFinal >= (CurrentSealPos.Y - 5)
                    && Form1_0.PlayerScan_0.yPosFinal <= (CurrentSealPos.Y + 5))
                {
                    int InteractCount = 0;
                    while (InteractCount < 3)
                    {
                        Form1_0.PathFinding_0.MoveToObject("DiabloSeal3");
                        Form1_0.WaitDelay(10);
                        InteractCount++;
                    }
                    if (!CastedAtSeis)
                    {
                        CastedAtSeis = true;
                        Form1_0.Battle_0.CastDefense();
                    }

                    //######
                    //KILL LORD DE SEIS
                    if (!TimeSetForWaitingUniqueBoss)
                    {
                        StartTimeUniqueBossWaiting = DateTime.Now;
                        TimeSetForWaitingUniqueBoss = true;
                    }

                    if (CurrentSealPos.X == 7773) SealType = 1;
                    else SealType = 2;

                    if (SealType == 1)
                    {
                        Form1_0.PathFinding_0.MoveToThisPos(new Position { X = 7794, Y = 5227 }, 4, true);
                        //NTM_MoveTo(108, 7797, 5201);
                        //for (int i = 0; i < 3; i += 1) NTM_TeleportTo(7794, 5227);
                    }
                    else Form1_0.PathFinding_0.MoveToThisPos(new Position { X = 7798, Y = 5186 }, 4, true);

                    Form1_0.SetGameStatus("WAITING LORD DE SEIS " + (TryCountWaitingUniqueBoss + 1) + "/1");

                    bool UniqueDetected = Form1_0.MobsStruc_0.GetMobs("getSuperUniqueName", "Lord De Seis", false, 200, new List<long>());

                    while (!UniqueDetected && (DateTime.Now - StartTimeUniqueBossWaiting).TotalSeconds < CharConfig.ChaosWaitingSealBossDelay)
                    {
                        UniqueDetected = Form1_0.MobsStruc_0.GetMobs("getSuperUniqueName", "Lord De Seis", false, 200, new List<long>());

                        Form1_0.PlayerScan_0.GetPositions();
                        Form1_0.overlayForm.UpdateOverlay();
                        Form1_0.GameStruc_0.CheckChickenGameTime();
                        Form1_0.ItemsStruc_0.GetItems(true);
                        Form1_0.Potions_0.CheckIfWeUsePotion();
                        Form1_0.Battle_0.DoBattleScript(10);
                        //###
                        Form1_0.Battle_0.SetSkills();
                        Form1_0.Battle_0.CastSkillsNoMove();
                        //###
                        Application.DoEvents();
                    }

                    if (!UniqueDetected)
                    {
                        //Form1_0.Battle_0.CastDefense();
                        Form1_0.InventoryStruc_0.DumpBadItemsOnGround();
                        TimeSetForWaitingUniqueBoss = false;
                        CurrentStep++;
                    }
                    else
                    {
                        Form1_0.SetGameStatus("KILLING LORD DE SEIS");

                        if (Form1_0.MobsStruc_0.GetMobs("getSuperUniqueName", "Lord De Seis", false, 200, new List<long>()))
                        {
                            if (Form1_0.MobsStruc_0.MobsHP > 0)
                            {
                                Form1_0.Battle_0.RunBattleScriptOnThisMob("getSuperUniqueName", "Lord De Seis", new List<long>());
                            }
                            else
                            {
                                //Form1_0.Battle_0.CastDefense();
                                Form1_0.InventoryStruc_0.DumpBadItemsOnGround();
                                TimeSetForWaitingUniqueBoss = false;
                                CurrentStep++;
                            }
                        }
                        else
                        {
                            TimeSetForWaitingUniqueBoss = false;
                            CurrentStep++;
                        }
                    }
                    //######
                }
                else
                {
                    Form1_0.PathFinding_0.MoveToObject("DiabloSeal3", 4, true);
                }
            }

            if (CurrentStep == 6)
            {
                CurrentSealPos = Form1_0.MapAreaStruc_0.GetPositionOfObject("object", "DiabloSeal2", (int)Enums.Area.ChaosSanctuary, new List<int>());

                if (Form1_0.PlayerScan_0.xPosFinal >= (CurrentSealPos.X - 5)
                    && Form1_0.PlayerScan_0.xPosFinal <= (CurrentSealPos.X + 5)
                    && Form1_0.PlayerScan_0.yPosFinal >= (CurrentSealPos.Y - 5)
                    && Form1_0.PlayerScan_0.yPosFinal <= (CurrentSealPos.Y + 5))
                {
                    int InteractCount = 0;
                    while (InteractCount < 3)
                    {
                        Form1_0.PathFinding_0.MoveToObject("DiabloSeal2");
                        Form1_0.WaitDelay(10);
                        InteractCount++;
                    }

                    CurrentStep++;
                }
                else
                {
                    Form1_0.PathFinding_0.MoveToObject("DiabloSeal2", 4, true);
                }
            }

            if (CurrentStep == 7)
            {
                CurrentSealPos = Form1_0.MapAreaStruc_0.GetPositionOfObject("object", "DiabloSeal1", (int)Enums.Area.ChaosSanctuary, new List<int>());

                if (Form1_0.PlayerScan_0.xPosFinal >= (CurrentSealPos.X - 5)
                    && Form1_0.PlayerScan_0.xPosFinal <= (CurrentSealPos.X + 5)
                    && Form1_0.PlayerScan_0.yPosFinal >= (CurrentSealPos.Y - 5)
                    && Form1_0.PlayerScan_0.yPosFinal <= (CurrentSealPos.Y + 5))
                {
                    int InteractCount = 0;
                    while (InteractCount < 3)
                    {
                        Form1_0.PathFinding_0.MoveToObject("DiabloSeal1");
                        Form1_0.WaitDelay(10);
                        InteractCount++;
                    }

                    //######
                    //KILL INFECTOR
                    if (!TimeSetForWaitingUniqueBoss)
                    {
                        StartTimeUniqueBossWaiting = DateTime.Now;
                        TimeSetForWaitingUniqueBoss = true;
                    }

                    if (CurrentSealPos.X == 7893) SealType = 1;
                    else SealType = 2;

                    if (SealType == 1) SealType = 1; // temp
                    else Form1_0.PathFinding_0.MoveToThisPos(new Position { X = 7933, Y = 5299 }, 4, true);

                    Form1_0.SetGameStatus("WAITING INFECTOR " + (TryCountWaitingUniqueBoss + 1) + "/1");

                    bool UniqueDetected = Form1_0.MobsStruc_0.GetMobs("getSuperUniqueName", "Winged Death", false, 200, new List<long>());

                    while (!UniqueDetected && (DateTime.Now - StartTimeUniqueBossWaiting).TotalSeconds < CharConfig.ChaosWaitingSealBossDelay)
                    {
                        UniqueDetected = Form1_0.MobsStruc_0.GetMobs("getSuperUniqueName", "Winged Death", false, 200, new List<long>());

                        Form1_0.PlayerScan_0.GetPositions();
                        Form1_0.overlayForm.UpdateOverlay();
                        Form1_0.GameStruc_0.CheckChickenGameTime();
                        Form1_0.ItemsStruc_0.GetItems(true);
                        Form1_0.Potions_0.CheckIfWeUsePotion();
                        Form1_0.Battle_0.DoBattleScript(10);
                        //###
                        Form1_0.Battle_0.SetSkills();
                        Form1_0.Battle_0.CastSkillsNoMove();
                        //###
                        Application.DoEvents();
                    }

                    if (!UniqueDetected)
                    {
                        Form1_0.InventoryStruc_0.DumpBadItemsOnGround();
                        TimeSetForWaitingUniqueBoss = false;
                        CurrentStep++;
                    }
                    else
                    {
                        Form1_0.SetGameStatus("KILLING INFECTOR");

                        if (Form1_0.MobsStruc_0.GetMobs("getSuperUniqueName", "Winged Death", false, 200, new List<long>()))
                        {
                            if (Form1_0.MobsStruc_0.MobsHP > 0)
                            {
                                Form1_0.Battle_0.RunBattleScriptOnThisMob("getSuperUniqueName", "Winged Death", new List<long>());
                            }
                            else
                            {
                                Form1_0.InventoryStruc_0.DumpBadItemsOnGround();
                                TimeSetForWaitingUniqueBoss = false;
                                CurrentStep++;
                            }
                        }
                        else
                        {
                            Form1_0.InventoryStruc_0.DumpBadItemsOnGround();
                            TimeSetForWaitingUniqueBoss = false;
                            CurrentStep++;
                        }
                    }
                    //######
                }
                else
                {
                    Form1_0.PathFinding_0.MoveToObject("DiabloSeal1", 4, true);
                }
            }

            if (CurrentStep == 8)
            {
                if (Form1_0.PathFinding_0.MoveToThisPos(DiabloSpawnPos, 4, true))
                {
                    //Form1_0.PathFinding_0.MoveToThisPos(DiabloSpawnPos, 4, true);
                    if (!FastChaos) Form1_0.Battle_0.CastDefense();
                    CurrentStep++;
                }
            }

            if (CurrentStep == 9)
            {
                Form1_0.Potions_0.CanUseSkillForRegen = false;
                Form1_0.SetGameStatus("KILLING DIABLO");

                //#############
                bool DetectedDiablo = Form1_0.MobsStruc_0.GetMobs("getBossName", "Diablo", false, 200, new List<long>());
                DateTime StartTime = DateTime.Now;
                TimeSpan TimeSinceDetecting = DateTime.Now - StartTime;
                while (!DetectedDiablo && TimeSinceDetecting.TotalSeconds < 13)
                {
                    Form1_0.SetGameStatus("WAITING DETECTING DIABLO");
                    DetectedDiablo = Form1_0.MobsStruc_0.GetMobs("getBossName", "Diablo", false, 200, new List<long>());
                    TimeSinceDetecting = DateTime.Now - StartTime;

                    //cast attack during this waiting time
                    /*Form1_0.Battle_0.SetSkills();
                    Form1_0.Battle_0.CastSkills();*/
                    Form1_0.ItemsStruc_0.GetItems(true);      //#############
                    Form1_0.Potions_0.CheckIfWeUsePotion();

                    if (!Form1_0.GameStruc_0.IsInGame() || !Form1_0.Running)
                    {
                        Form1_0.overlayForm.ResetMoveToLocation();
                        return;
                    }
                }

                if (TimeSinceDetecting.TotalSeconds >= 13)
                {
                    Form1_0.MobsStruc_0.DetectThisMob("getBossName", "Diablo", false, 200, new List<long>());
                    Form1_0.method_1("Waited too long for Diablo repoping the seals!", Color.OrangeRed);
                    FastChaosPopingSeals = true;
                    CurrentStep = 3;
                    return;
                }
                //#############

                if (Form1_0.MobsStruc_0.GetMobs("getBossName", "Diablo", false, 200, new List<long>()))
                {
                    Form1_0.SetGameStatus("KILLING DIABLO");
                    if (Form1_0.MobsStruc_0.MobsHP > 0)
                    {
                        DetectedBoss = true;
                        Form1_0.Battle_0.RunBattleScriptOnThisMob("getBossName", "Diablo", new List<long>());
                    }
                    else
                    {
                        if (!DetectedBoss)
                        {
                            Form1_0.method_1("Diablo not detected!", Color.Red);
                            Form1_0.Battle_0.DoBattleScript(15);
                        }

                        Form1_0.PathFinding_0.AcceptMoveOffset = BufferPathFindingMoveSize;
                        if (Form1_0.Battle_0.EndBossBattle()) ScriptDone = true;
                        return;
                        //Form1_0.LeaveGame(true);
                    }
                }
                else
                {
                    Form1_0.method_1("Diablo not detected!", Color.Red);

                    Form1_0.Battle_0.DoBattleScript(15);

                    //baal not detected...
                    Form1_0.ItemsStruc_0.GetItems(true);
                    if (Form1_0.MobsStruc_0.GetMobs("getBossName", "Diablo", false, 200, new List<long>())) return; //redetect baal?
                    Form1_0.ItemsStruc_0.GrabAllItemsForGold();
                    if (Form1_0.MobsStruc_0.GetMobs("getBossName", "Diablo", false, 200, new List<long>())) return; //redetect baal?
                    Form1_0.Potions_0.CanUseSkillForRegen = true;

                    Form1_0.PathFinding_0.AcceptMoveOffset = BufferPathFindingMoveSize;
                    if (Form1_0.Battle_0.EndBossBattle()) ScriptDone = true;
                    return;
                    //Form1_0.LeaveGame(true);
                }
            }

        }
    }


    static int[] FindBestPosition(int playerX, int playerY, List<int[]> monsterPositions, int maxDisplacement)
    {
        // Create a list to store all possible positions around the player
        List<int[]> possiblePositions = new List<int[]>();

        // Generate all possible positions within the maximum displacement range
        for (int x = playerX - maxDisplacement; x <= playerX + maxDisplacement; x++)
        {
            for (int y = playerY - maxDisplacement; y <= playerY + maxDisplacement; y++)
            {
                // Calculate the distance between the player and the current position
                double distance = Math.Sqrt(Math.Pow(playerX - x, 2) + Math.Pow(playerY - y, 2));

                // Check if the distance is within the maximum displacement and the position is not occupied by a monster
                if (distance <= maxDisplacement && !IsMonsterPosition(x, y, monsterPositions))
                {
                    //possiblePositions.Add(Tuple.Create(x, y));
                    possiblePositions.Add(new int[2] { x, y });
                }
            }
        }

        // Find the closest position among the possible positions
        //int[] bestPosition = Tuple.Create(playerX, playerY);
        int[] bestPosition = new int[2] { playerX, playerY };
        double closestDistance = double.MaxValue;
        foreach (var position in possiblePositions)
        {
            double distance = Math.Sqrt(Math.Pow(playerX - position[0], 2) + Math.Pow(playerY - position[1], 2));
            if (distance < closestDistance)
            {
                closestDistance = distance;
                bestPosition = position;
            }
        }

        return bestPosition;
    }

    static bool IsMonsterPosition(int x, int y, List<int[]> monsterPositions)
    {
        foreach (var monsterPosition in monsterPositions)
        {
            if (monsterPosition[0] >= x - 8
                && monsterPosition[0] <= x + 8
                && monsterPosition[1] >= y - 8
                && monsterPosition[1] <= y + 8)
            {
                return true;
            }
        }
        return false;
    }

}
