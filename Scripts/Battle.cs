using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using static MapAreaStruc;

public class Battle
{
    Form1 Form1_0;

    public int AreaX = 0;
    public int AreaY = 0;
    public bool ClearingArea = false;
    public List<long> IgnoredMobsPointer = new List<long>();
    public int ClearingSize = 0;
    public long LastMobAttackedHP = 0;
    public int AttackNotRegisteredCount = 0;
    public int MoveTryCount = 0;

    public int MaxMoveTry = 5;

    public bool FirstAttackCasted = false;
    public bool DoingBattle = false;
    public bool ClearingFullArea = false;

    public int TriedToMoveToMobsCount = 0;

    public string LastMobName = "";
    public string LastMobType = "";

    public List<Room> AllRooms_InArea = new List<Room>();
    public List<int> IgnoredRooms_InArea = new List<int>();
    public int DoingRoomIndex = 0;
    public bool LeftToRight = true;

    public int AreaIDFullyCleared = 0;

    public DateTime TimeSinceLastCast = DateTime.MaxValue;

    public void SetForm1(Form1 form1_1)
    {
        Form1_0 = form1_1;
    }

    public bool EndBossBattle()
    {
        Form1_0.KeyMouse_0.ReleaseKey(CharConfig.KeyForceMovement);
        if (!Form1_0.ItemsStruc_0.GetItems(true)) Form1_0.WaitDelay(CharConfig.EndBattleGrabDelay);
        if (!Form1_0.ItemsStruc_0.GetItems(true)) Form1_0.WaitDelay(CharConfig.EndBattleGrabDelay);
        if (!Form1_0.ItemsStruc_0.GetItems(true)) Form1_0.WaitDelay(CharConfig.EndBattleGrabDelay);
        if (!Form1_0.ItemsStruc_0.GetItems(true)) Form1_0.WaitDelay(CharConfig.EndBattleGrabDelay);
        if (!Form1_0.ItemsStruc_0.GetItems(true)) Form1_0.WaitDelay(CharConfig.EndBattleGrabDelay);
        if (!Form1_0.ItemsStruc_0.GetItems(true)) Form1_0.WaitDelay(CharConfig.EndBattleGrabDelay);
        if (!Form1_0.ItemsStruc_0.GetItems(true)) Form1_0.WaitDelay(CharConfig.EndBattleGrabDelay);
        if (!Form1_0.ItemsStruc_0.GetItems(true)) Form1_0.WaitDelay(CharConfig.EndBattleGrabDelay);
        if (!Form1_0.ItemsStruc_0.GetItems(true)) Form1_0.WaitDelay(CharConfig.EndBattleGrabDelay);
        if (!Form1_0.ItemsStruc_0.GetItems(true)) Form1_0.WaitDelay(CharConfig.EndBattleGrabDelay);

        if (CharConfig.ClearAfterBoss)
        {
            if (Form1_0.MobsStruc_0.GetMobs("", "", true, 30, new List<long>()))
            {
                Form1_0.Battle_0.DoBattleScript(30);
                return false;
            }
        }

        Form1_0.ItemsStruc_0.GrabAllItemsForGold();

        Form1_0.Battle_0.ClearingArea = false;
        Form1_0.Battle_0.DoingBattle = false;
        Form1_0.Potions_0.CanUseSkillForRegen = true;
        Form1_0.Town_0.FastTowning = false;
        Form1_0.Town_0.UseLastTP = false;

        return true;
    }

    public int[] FindBestPositionNoMobsArround(int playerX, int playerY, List<int[]> monsterPositions, int maxDisplacement)
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
                if (distance <= maxDisplacement && !IsMonsterNearPosition(x, y, monsterPositions))
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

    static bool IsMonsterNearPosition(int x, int y, List<int[]> monsterPositions)
    {
        foreach (var monsterPosition in monsterPositions)
        {
            if (monsterPosition[0] >= x - 6
                && monsterPosition[0] <= x + 6
                && monsterPosition[1] >= y - 6
                && monsterPosition[1] <= y + 6)
            {
                return true;
            }
        }
        return false;
    }

    public Position GetBestAttackLocation(Position ThisAttackPos)
    {
        Position ReturnPos = new Position { X = ThisAttackPos.X, Y = ThisAttackPos.Y };
        int ChoosenAttackLocation = 0; //0=Down, 1=Right, 2=Left, 3=Up

        bool[,] ThisCollisionGrid = Form1_0.MapAreaStruc_0.CollisionGrid((Enums.Area)Form1_0.PlayerScan_0.levelNo);

        if (ThisCollisionGrid.GetLength(0) == 0 || ThisCollisionGrid.GetLength(1) == 0) return ReturnPos;
        if (Form1_0.MapAreaStruc_0.AllMapData.Count == 0) return ReturnPos;

        int ThisX = ThisAttackPos.X - Form1_0.MapAreaStruc_0.AllMapData[(int)Form1_0.PlayerScan_0.levelNo - 1].Offset.X;
        int ThisY = ThisAttackPos.Y - Form1_0.MapAreaStruc_0.AllMapData[(int)Form1_0.PlayerScan_0.levelNo - 1].Offset.Y;

        if (ThisX < 0) return ReturnPos;
        if (ThisY < 0) return ReturnPos;
        if (ThisX > ThisCollisionGrid.GetLength(0) - 1) return ReturnPos;
        if (ThisY > ThisCollisionGrid.GetLength(1) - 1) return ReturnPos;

        try
        {
            bool AttackPosFound = false;
            while (!AttackPosFound)
            {
                //check boundary for attacking the mobs from down position
                if (ChoosenAttackLocation == 0)
                {
                    //#####
                    //Check Validity
                    bool IsValid = true;
                    if (ThisX < 2) IsValid = false;
                    if (ThisY < 2) IsValid = false;
                    if (ThisX > ThisCollisionGrid.GetLength(0) - 1) IsValid = false;
                    if (ThisY > ThisCollisionGrid.GetLength(1) - 1) IsValid = false;
                    //#####

                    if (ThisCollisionGrid[ThisX, ThisY]
                        && ThisCollisionGrid[ThisX - 1, ThisY]
                        && ThisCollisionGrid[ThisX - 2, ThisY]
                        && ThisCollisionGrid[ThisX - 1, ThisY - 1]
                        && ThisCollisionGrid[ThisX - 2, ThisY - 1]
                        && ThisCollisionGrid[ThisX - 1, ThisY - 2]
                        && ThisCollisionGrid[ThisX - 1, ThisY - 3]
                        && ThisCollisionGrid[ThisX - 1, ThisY - 4]
                        && IsValid)
                    {
                        //Form1_0.method_1("Attack from Bottom!", Color.OrangeRed);
                        AttackPosFound = true;
                        ChoosenAttackLocation = 0; //Attack from Bottom
                        ReturnPos = new Position { X = ThisX + Form1_0.MapAreaStruc_0.AllMapData[(int)Form1_0.PlayerScan_0.levelNo - 1].Offset.X, Y = ThisY + Form1_0.MapAreaStruc_0.AllMapData[(int)Form1_0.PlayerScan_0.levelNo - 1].Offset.Y };
                    }
                    else
                    {
                        //change attack location to right
                        ThisX += 4;
                        ThisY -= 2;

                        ChoosenAttackLocation++;
                    }
                }

                //check boundary for attacking the mobs from Right position
                if (ChoosenAttackLocation == 1)
                {
                    //#####
                    //Check Validity
                    bool IsValid = true;
                    if (ThisX < 2) IsValid = false;
                    if (ThisY < 0) IsValid = false;
                    if (ThisX > ThisCollisionGrid.GetLength(0) - 1) IsValid = false;
                    if (ThisY > ThisCollisionGrid.GetLength(1) - 1) IsValid = false;
                    //#####

                    if (ThisCollisionGrid[ThisX, ThisY]
                        && ThisCollisionGrid[ThisX - 1, ThisY]
                        && ThisCollisionGrid[ThisX - 2, ThisY]
                        && IsValid)
                    {
                        //Form1_0.method_1("Attack from Right!", Color.OrangeRed);
                        AttackPosFound = true;
                        ChoosenAttackLocation = 1; //Attack from Right
                        ReturnPos = new Position { X = ThisX + Form1_0.MapAreaStruc_0.AllMapData[(int)Form1_0.PlayerScan_0.levelNo - 1].Offset.X, Y = ThisY + Form1_0.MapAreaStruc_0.AllMapData[(int)Form1_0.PlayerScan_0.levelNo - 1].Offset.Y };
                    }
                    else
                    {
                        //change attack location to left
                        ThisX -= 7;

                        ChoosenAttackLocation++;
                    }
                }

                //check boundary for attacking the mobs from Left position
                if (ChoosenAttackLocation == 2)
                {
                    //#####
                    //Check Validity
                    bool IsValid = true;
                    if (ThisX < 1) IsValid = false;
                    if (ThisY < 1) IsValid = false;
                    if (ThisX > ThisCollisionGrid.GetLength(0) - 3) IsValid = false;
                    if (ThisY > ThisCollisionGrid.GetLength(1) - 1) IsValid = false;
                    //#####

                    if (ThisCollisionGrid[ThisX, ThisY]
                        && ThisCollisionGrid[ThisX - 1, ThisY]
                        && ThisCollisionGrid[ThisX + 1, ThisY]
                        && ThisCollisionGrid[ThisX + 2, ThisY]
                        && ThisCollisionGrid[ThisX, ThisY - 1]
                        && ThisCollisionGrid[ThisX + 1, ThisY - 1]
                        && IsValid)
                    {
                        //Form1_0.method_1("Attack from Left!", Color.OrangeRed);
                        AttackPosFound = true;
                        ChoosenAttackLocation = 2; //Attack from Left
                        ReturnPos = new Position { X = ThisX + Form1_0.MapAreaStruc_0.AllMapData[(int)Form1_0.PlayerScan_0.levelNo - 1].Offset.X, Y = ThisY + Form1_0.MapAreaStruc_0.AllMapData[(int)Form1_0.PlayerScan_0.levelNo - 1].Offset.Y };
                    }
                    else
                    {
                        //change attack location to top
                        ThisX += 3;
                        ThisY -= 5;

                        ChoosenAttackLocation++;
                    }
                }

                //check boundary for attacking the mobs from Up position (NOT RECOMMENDED FOR HAMMER)
                if (ChoosenAttackLocation == 3)
                {
                    //#####
                    //Check Validity
                    bool IsValid = true;
                    if (ThisX < 1) IsValid = false;
                    if (ThisY < 1) IsValid = false;
                    if (ThisX > ThisCollisionGrid.GetLength(0) - 2) IsValid = false;
                    if (ThisY > ThisCollisionGrid.GetLength(1) - 2) IsValid = false;
                    //#####

                    if (ThisCollisionGrid[ThisX, ThisY]
                        && ThisCollisionGrid[ThisX - 1, ThisY]
                        && ThisCollisionGrid[ThisX + 1, ThisY]
                        && ThisCollisionGrid[ThisX, ThisY - 1]
                        && ThisCollisionGrid[ThisX, ThisY + 1]
                        && IsValid)
                    {
                        //Form1_0.method_1("Attack from Top!", Color.OrangeRed);
                        AttackPosFound = true;
                        ChoosenAttackLocation = 3; //Attack from Top
                        ReturnPos = new Position { X = ThisX + Form1_0.MapAreaStruc_0.AllMapData[(int)Form1_0.PlayerScan_0.levelNo - 1].Offset.X, Y = ThisY + Form1_0.MapAreaStruc_0.AllMapData[(int)Form1_0.PlayerScan_0.levelNo - 1].Offset.Y };
                    }
                    else
                    {
                        Form1_0.method_1("No Attack pos found!", Color.Red);
                        //no atack pos found??
                        AttackPosFound = true;
                        ChoosenAttackLocation++; //return attack pos = 4 (for error)
                        ReturnPos = new Position { X = ThisAttackPos.X, Y = ThisAttackPos.Y };
                        //ReturnPos = new Position { X = 0, Y = 0 };
                    }
                }
            }

        }
        catch { }

        return ReturnPos;
    }

    public void CastDefense()
    {
        if (CharConfig.UseBO && !Form1_0.Town_0.GetInTown())
        {
            Form1_0.Potions_0.CheckIfWeUsePotion();

            Form1_0.KeyMouse_0.PressKey(CharConfig.KeySwapWeapon);
            Form1_0.WaitDelay(15);
            //Form1_0.KeyMouse_0.PressKey(CharConfig.KeySkillBattleOrder);
            Form1_0.KeyMouse_0.PressKey(CharConfig.KeySkillBattleCommand);
            Form1_0.WaitDelay(10);
            /*Form1_0.KeyMouse_0.MouseClicc(1025, 1025);
            Form1_0.WaitDelay(5);
            Form1_0.KeyMouse_0.MouseClicc(1095, 610);
            Form1_0.WaitDelay(5);*/
            Form1_0.PlayerScan_0.GetPositions();

            //press W again to switch weapon again
            //if (Form1_0.PlayerScan_0.RightSkill != Enums.Skill.BattleOrders)
            if (Form1_0.PlayerScan_0.RightSkill != Enums.Skill.BattleCommand)
            {
                Form1_0.KeyMouse_0.PressKey(CharConfig.KeySwapWeapon);
                Form1_0.WaitDelay(15);
                //Form1_0.KeyMouse_0.PressKey(CharConfig.KeySkillBattleOrder);
                Form1_0.KeyMouse_0.PressKey(CharConfig.KeySkillBattleCommand);
                Form1_0.WaitDelay(10);
                /*Form1_0.KeyMouse_0.MouseClicc(1025, 1025);
                Form1_0.WaitDelay(5);
                Form1_0.KeyMouse_0.MouseClicc(1095, 610);
                Form1_0.WaitDelay(5);*/
                Form1_0.PlayerScan_0.GetPositions();
            }

            Form1_0.KeyMouse_0.MouseCliccRight_RealPos(Form1_0.CenterX, Form1_0.CenterY);
            Form1_0.WaitDelay(35);

            //select battle command
            //Form1_0.KeyMouse_0.PressKey(CharConfig.KeySkillBattleCommand);
            Form1_0.KeyMouse_0.PressKey(CharConfig.KeySkillBattleOrder);
            Form1_0.WaitDelay(10);
            /*Form1_0.KeyMouse_0.MouseClicc(1025, 1025);
            Form1_0.WaitDelay(5);
            Form1_0.KeyMouse_0.MouseClicc(1025, 610);
            Form1_0.WaitDelay(5);*/
            Form1_0.KeyMouse_0.MouseCliccRight_RealPos(Form1_0.CenterX, Form1_0.CenterY);
            Form1_0.WaitDelay(35); //60 <-
            Form1_0.Potions_0.CheckIfWeUsePotion();

            //select battle cry
            Form1_0.KeyMouse_0.PressKey(CharConfig.KeySkillBattleCry);
            Form1_0.WaitDelay(10);
            /*Form1_0.KeyMouse_0.MouseClicc(1025, 1025);
            Form1_0.WaitDelay(5);
            Form1_0.KeyMouse_0.MouseClicc(1165, 610);
            Form1_0.WaitDelay(5);*/
            Form1_0.KeyMouse_0.MouseCliccRight_RealPos(Form1_0.CenterX, Form1_0.CenterY);
            Form1_0.WaitDelay(60);

            Form1_0.KeyMouse_0.PressKey(CharConfig.KeySwapWeapon);
            Form1_0.WaitDelay(15);
            Form1_0.PlayerScan_0.GetPositions();
        }

        //press W again to switch weapon again
        if (Form1_0.PlayerScan_0.RightSkill == Enums.Skill.BattleCry
            || Form1_0.PlayerScan_0.RightSkill == Enums.Skill.BattleOrders
            || Form1_0.PlayerScan_0.RightSkill == Enums.Skill.BattleCommand)
        {
            Form1_0.KeyMouse_0.PressKey(CharConfig.KeySwapWeapon);
            Form1_0.WaitDelay(15);
            Form1_0.PlayerScan_0.GetPositions();
        }

        //cast sacred shield
        Form1_0.KeyMouse_0.PressKey(CharConfig.KeySkillCastDefense);
        Form1_0.WaitDelay(5);
        Form1_0.KeyMouse_0.MouseCliccRight_RealPos(Form1_0.CenterX, Form1_0.CenterY);
        Form1_0.WaitDelay(35);

        //cast sacred shield
        Form1_0.KeyMouse_0.PressKey(CharConfig.KeySkillLifeAura);
        Form1_0.WaitDelay(5);
        Form1_0.KeyMouse_0.MouseCliccRight_RealPos(Form1_0.CenterX, Form1_0.CenterY);
        Form1_0.WaitDelay(5);

        TimeSinceLastCast = DateTime.Now;
    }

    public bool ClearAreaOfMobs(int ThisX, int ThisY, int ClearSize)
    {
        AreaX = ThisX;
        AreaY = ThisY;
        IgnoredMobsPointer = new List<long>();
        ClearingSize = ClearSize;
        AttackNotRegisteredCount = 0;
        MoveTryCount = 0;
        //ClearingFullArea = false;

        //ClearingArea = true;
        if (Form1_0.MobsStruc_0.GetMobs("", "", true, ClearingSize, IgnoredMobsPointer))
        {
            ClearingArea = true;
            return true;
        }
        return false;
    }

    public void ClearFullAreaOfMobs()
    {
        IgnoredMobsPointer = new List<long>();
        AttackNotRegisteredCount = 0;
        MoveTryCount = 0;
        ClearingSize = 500;
        ClearingFullArea = true;
        DoingRoomIndex = 0;

        AllRooms_InArea = Form1_0.MapAreaStruc_0.AllMapData[(int)(Form1_0.PlayerScan_0.levelNo - 1)].Rooms;

        //if (Form1_0.MobsStruc_0.GetMobs("", "", true, ClearingSize, IgnoredMobsPointer)) ClearingArea = true;
        ClearingArea = true;
    }

    public void SetBattleMoveAcceptOffset()
    {
        //if (CharConfig.RunningOnChar.ToLower().Contains("sorc")) Form1_0.Mover_0.MoveAcceptOffset = 10;
        //else Form1_0.Mover_0.MoveAcceptOffset = 4; //default
    }

    public void ResetBattleMoveAcceptOffset()
    {
        //Form1_0.Mover_0.MoveAcceptOffset = 4; //default
    }

    public bool IsIncludedInList(List<int> IgnoredIDList, int ThisID)
    {
        if (IgnoredIDList != null)
        {
            if (IgnoredIDList.Count > 0)
            {
                for (int i = 0; i < IgnoredIDList.Count; i++)
                {
                    if (IgnoredIDList[i] == ThisID)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    public void RemoveCurrentRoomFromClearing()
    {
        //List<Room> AllRooms = Form1_0.MapAreaStruc_0.AllMapData[(int)(Form1_0.PlayerScan_0.levelNo - 1)].Rooms;
        int LastRoomXIndex = 0;
        for (int i = 1; i < AllRooms_InArea.Count; i++)
        {
            if (AllRooms_InArea[i].X == AllRooms_InArea[0].X) break;
            LastRoomXIndex++;
        }

        //Remove the Rooms we just done clearing
        List<int> RemovingRoomsAt = new List<int>();
        for (int i = 0; i < AllRooms_InArea.Count; i++)
        {
            if (Form1_0.PlayerScan_0.xPosFinal >= AllRooms_InArea[i].X && Form1_0.PlayerScan_0.xPosFinal <= AllRooms_InArea[i].X + AllRooms_InArea[i].Width
                && Form1_0.PlayerScan_0.yPosFinal >= AllRooms_InArea[i].Y && Form1_0.PlayerScan_0.yPosFinal <= AllRooms_InArea[i].Y + AllRooms_InArea[i].Height)
            {
                DoingRoomIndex = i;
                RemovingRoomsAt.Add(i + LastRoomXIndex);
                RemovingRoomsAt.Add(i + 1);
                RemovingRoomsAt.Add(i);
                RemovingRoomsAt.Add(i - 1);
                RemovingRoomsAt.Add(i - LastRoomXIndex);
                break;
            }
        }

        for (int i = 0; i < RemovingRoomsAt.Count; i++)
        {
            if (RemovingRoomsAt[i] < AllRooms_InArea.Count)
            {
                try
                {
                    if (!IsIncludedInList(IgnoredRooms_InArea, RemovingRoomsAt[i]))
                    {
                        IgnoredRooms_InArea.Add(RemovingRoomsAt[i]);
                        //Form1_0.method_1("Removed Room: " + RemovingRoomsAt[i] + ", remaining: " + (AllRooms_InArea.Count - IgnoredRooms_InArea.Count), Color.Red);
                    }
                }
                catch { }
            }
        }
    }

    public void RunBattleScript()
    {
        if ((Enums.Area)Form1_0.PlayerScan_0.levelNo == Enums.Area.ThroneOfDestruction)
        {
            //15096,5096
            if (Form1_0.PlayerScan_0.yPosFinal > 5096)
            {
                DoingBattle = false;
                FirstAttackCasted = false;
                ResetBattleMoveAcceptOffset();
                if (!ClearingFullArea) Form1_0.PathFinding_0.MoveToThisPos(new Position { X = AreaX, Y = AreaY });
                //Form1_0.Mover_0.MoveToLocation(AreaX, AreaY);
                ClearingArea = false;
                Form1_0.KeyMouse_0.ReleaseKey(CharConfig.KeyForceMovement);
                return;
            }
        }

        if (Form1_0.MobsStruc_0.GetMobs("", "", true, ClearingSize, IgnoredMobsPointer))
        {
            if (CharConfig.RunBaalScript && !Form1_0.Baal_0.ScriptDone && Form1_0.MobsStruc_0.MobsName == "BaalSubject5") Form1_0.Baal_0.Wave5Detected = true;
            if (CharConfig.RunBaalScript && !Form1_0.Baal_0.ScriptDone && (Enums.Area) Form1_0.PlayerScan_0.levelNo == Enums.Area.ThroneOfDestruction) Form1_0.Baal_0.TimeSinceLastWaveDone = DateTime.MaxValue;

            DoingBattle = true;
            SetBattleMoveAcceptOffset();
            Form1_0.Mover_0.MoveAcceptOffset = 2;
            Position ThisAttackPos = GetBestAttackLocation(new Position { X = Form1_0.MobsStruc_0.xPosFinal + 1, Y = Form1_0.MobsStruc_0.yPosFinal + 5 });
            if (ThisAttackPos.X != 0 && ThisAttackPos.Y != 0)
            {
                if (!Form1_0.Mover_0.MoveToLocationAttack(ThisAttackPos.X, ThisAttackPos.Y))
                {
                    TriedToMoveToMobsCount++;
                    if (TriedToMoveToMobsCount >= 2)
                    {
                        ThisAttackPos = ResetMovePostionInBetween(ThisAttackPos);
                        Form1_0.Mover_0.MoveToLocationAttack(ThisAttackPos.X, ThisAttackPos.Y);
                        TriedToMoveToMobsCount = 0;
                    }
                }
                Form1_0.KeyMouse_0.ReleaseKey(CharConfig.KeyForceMovement);
            }
            //Form1_0.Mover_0.MoveToLocationAttack(Form1_0.MobsStruc_0.xPosFinal - 1, Form1_0.MobsStruc_0.yPosFinal + 2);
            Form1_0.Mover_0.MoveAcceptOffset = 4;
            ResetBattleMoveAcceptOffset();

            FirstAttackCasting();
            SetSkills();
            CastSkills();
            if (CharConfig.RunningOnChar == "Paladin")
            {
                CastSkills();
                CastSkills();
            }
            AttackTryCheck();

            if (ClearingFullArea && IgnoredRooms_InArea.Count < AllRooms_InArea.Count)
            {
                //Remove the Rooms we just done clearing
                RemoveCurrentRoomFromClearing();
            }
        }
        else
        {
            if (ClearingFullArea && (AllRooms_InArea.Count - IgnoredRooms_InArea.Count) > 0)
            {
                if ((DateTime.Now - TimeSinceLastCast).TotalSeconds > CharConfig.RecastBODelay)
                {
                    CastDefense();
                }
                //"x":25320, "y":6100, "width":40, "height":40

                //Remove the Rooms we just done clearing
                RemoveCurrentRoomFromClearing();

                if (DoingRoomIndex > 0) DoingRoomIndex--;
                while (IsIncludedInList(IgnoredRooms_InArea, DoingRoomIndex)) DoingRoomIndex--;
                if (DoingRoomIndex < 0)
                {
                    DoingRoomIndex = 0;
                    while (IsIncludedInList(IgnoredRooms_InArea, DoingRoomIndex)) DoingRoomIndex++;
                }
                //if (DoingRoomIndex > AllRooms_InArea.Count - 1) DoingRoomIndex = AllRooms_InArea.Count - 1;
                if (DoingRoomIndex > AllRooms_InArea.Count - 1)
                {
                    Form1_0.KeyMouse_0.ReleaseKey(CharConfig.KeyForceMovement);
                    Form1_0.MobsStruc_0.xPosFinal = 0;
                    Form1_0.MobsStruc_0.yPosFinal = 0;
                    //if (CharConfig.RunBaalScript && !Form1_0.Baal_0.ScriptDone && Form1_0.Baal_0.Wave5Detected) Form1_0.Baal_0.Wave5Cleared = true;
                    TriedToMoveToMobsCount = 0;
                    DoingBattle = false;
                    FirstAttackCasted = false;
                    ResetBattleMoveAcceptOffset();
                    if (!ClearingFullArea) Form1_0.PathFinding_0.MoveToThisPos(new Position { X = AreaX, Y = AreaY });
                    //Form1_0.Mover_0.MoveToLocation(AreaX, AreaY);
                    ClearingArea = false;
                    AreaIDFullyCleared = (int) Form1_0.PlayerScan_0.levelNo;
                    return;
                }

                //Go to next room
                bool[,] ThisCollisionGrid = Form1_0.MapAreaStruc_0.CollisionGrid((Enums.Area)Form1_0.PlayerScan_0.levelNo);
                int RoomStartX = AllRooms_InArea[DoingRoomIndex].X - Form1_0.MapAreaStruc_0.AllMapData[(int)(Form1_0.PlayerScan_0.levelNo - 1)].Offset.X;
                int RoomStartY = AllRooms_InArea[DoingRoomIndex].Y - Form1_0.MapAreaStruc_0.AllMapData[(int)(Form1_0.PlayerScan_0.levelNo - 1)].Offset.Y;
                int RoomSizeX = AllRooms_InArea[DoingRoomIndex].Width;
                int RoomSizeY = AllRooms_InArea[DoingRoomIndex].Height;

                Position MovingToPos = new Position { X = AllRooms_InArea[DoingRoomIndex].X, Y = AllRooms_InArea[DoingRoomIndex].Y };
                bool FoundWalkablePath = false;
                //Form1_0.method_1("Check:" + RoomStartX + ", " + RoomStartY, Color.Red);
                //Form1_0.method_1("Check size:" + RoomSizeX + ", " + RoomSizeY, Color.Red);
                for (int i = RoomStartX; i < RoomStartX + RoomSizeX; i++)
                {
                    for (int k = RoomStartY; k < RoomStartY + RoomSizeY; k++)
                    {
                        if (ThisCollisionGrid[i, k])
                        {
                            FoundWalkablePath = true;
                            MovingToPos = new Position { X = i + Form1_0.MapAreaStruc_0.AllMapData[(int)(Form1_0.PlayerScan_0.levelNo - 1)].Offset.X, Y = k + Form1_0.MapAreaStruc_0.AllMapData[(int)(Form1_0.PlayerScan_0.levelNo - 1)].Offset.Y };
                        }
                    }
                }
                if (FoundWalkablePath)
                {
                    //Form1_0.PathFinding_0.MoveToThisPos(MovingToPos);
                    Form1_0.PathFinding_0.MoveToThisPos(MovingToPos, 4, true);
                }
                else
                {
                    if (!IsIncludedInList(IgnoredRooms_InArea, DoingRoomIndex)) IgnoredRooms_InArea.Add(DoingRoomIndex);
                }
            }
            else
            {
                Form1_0.KeyMouse_0.ReleaseKey(CharConfig.KeyForceMovement);
                Form1_0.MobsStruc_0.xPosFinal = 0;
                Form1_0.MobsStruc_0.yPosFinal = 0;
                //if (CharConfig.RunBaalScript && !Form1_0.Baal_0.ScriptDone && Form1_0.Baal_0.Wave5Detected) Form1_0.Baal_0.Wave5Cleared = true;
                TriedToMoveToMobsCount = 0;
                DoingBattle = false;
                FirstAttackCasted = false;
                ResetBattleMoveAcceptOffset();
                if (!ClearingFullArea) Form1_0.PathFinding_0.MoveToThisPos(new Position { X = AreaX, Y = AreaY });
                //Form1_0.Mover_0.MoveToLocation(AreaX, AreaY);
                ClearingArea = false;
                AreaIDFullyCleared = (int)Form1_0.PlayerScan_0.levelNo;
            }
        }
    }

    public bool DoBattleScript(int MaxDistance)
    {
        if (Form1_0.MobsStruc_0.GetMobs("", "", true, MaxDistance, new List<long>()))
        {
            if (CharConfig.RunBaalScript && !Form1_0.Baal_0.ScriptDone && Form1_0.MobsStruc_0.MobsName == "BaalSubject5") Form1_0.Baal_0.Wave5Detected = true;
            if (CharConfig.RunBaalScript && !Form1_0.Baal_0.ScriptDone && (Enums.Area)Form1_0.PlayerScan_0.levelNo == Enums.Area.ThroneOfDestruction) Form1_0.Baal_0.TimeSinceLastWaveDone = DateTime.MaxValue;
            DoingBattle = true;
            SetBattleMoveAcceptOffset();
            Form1_0.Mover_0.MoveAcceptOffset = 2;
            Position ThisAttackPos = GetBestAttackLocation(new Position { X = Form1_0.MobsStruc_0.xPosFinal + 1, Y = Form1_0.MobsStruc_0.yPosFinal + 5 });
            if (ThisAttackPos.X != 0 && ThisAttackPos.Y != 0)
            {
                if (!Form1_0.Mover_0.MoveToLocationAttack(ThisAttackPos.X, ThisAttackPos.Y))
                {
                    TriedToMoveToMobsCount++;
                    if (TriedToMoveToMobsCount >= 2)
                    {
                        ThisAttackPos = ResetMovePostionInBetween(ThisAttackPos);
                        Form1_0.Mover_0.MoveToLocationAttack(ThisAttackPos.X, ThisAttackPos.Y);
                        TriedToMoveToMobsCount = 0;
                    }
                }
                Form1_0.KeyMouse_0.ReleaseKey(CharConfig.KeyForceMovement);
            }
            //Form1_0.Mover_0.MoveToLocationAttack(Form1_0.MobsStruc_0.xPosFinal - 1, Form1_0.MobsStruc_0.yPosFinal + 2);
            Form1_0.Mover_0.MoveAcceptOffset = 4;
            ResetBattleMoveAcceptOffset();

            FirstAttackCasting();
            SetSkills();
            CastSkills();
            if (CharConfig.RunningOnChar == "Paladin")
            {
                CastSkills();
                CastSkills();
            }
            AttackTryCheck();
            return true;
        }

        Form1_0.KeyMouse_0.ReleaseKey(CharConfig.KeyForceMovement);
        Form1_0.MobsStruc_0.xPosFinal = 0;
        Form1_0.MobsStruc_0.yPosFinal = 0;
        //if (CharConfig.RunBaalScript && !Form1_0.Baal_0.ScriptDone && Form1_0.Baal_0.Wave5Detected) Form1_0.Baal_0.Wave5Cleared = true;
        TriedToMoveToMobsCount = 0;
        DoingBattle = false;
        FirstAttackCasted = false;
        return false;
    }

    public void RunBattleScriptOnLastMob(List<long> IgnoredIDList)
    {
        IgnoredMobsPointer = IgnoredIDList;
        if (Form1_0.MobsStruc_0.GetMobs(LastMobType, LastMobName, false, 200, IgnoredIDList))
        {
            if (CharConfig.RunBaalScript && !Form1_0.Baal_0.ScriptDone && Form1_0.MobsStruc_0.MobsName == "BaalSubject5") Form1_0.Baal_0.Wave5Detected = true;
            if (CharConfig.RunBaalScript && !Form1_0.Baal_0.ScriptDone && (Enums.Area)Form1_0.PlayerScan_0.levelNo == Enums.Area.ThroneOfDestruction) Form1_0.Baal_0.TimeSinceLastWaveDone = DateTime.MaxValue;
            if (Form1_0.MobsStruc_0.MobsHP > 0)
            {
                DoingBattle = true;
                SetBattleMoveAcceptOffset();
                Form1_0.Mover_0.MoveAcceptOffset = 2;
                Position ThisAttackPos = GetBestAttackLocation(new Position { X = Form1_0.MobsStruc_0.xPosFinal + 1, Y = Form1_0.MobsStruc_0.yPosFinal + 5 });
                if (ThisAttackPos.X != 0 && ThisAttackPos.Y != 0)
                {
                    if (!Form1_0.Mover_0.MoveToLocationAttack(ThisAttackPos.X, ThisAttackPos.Y))
                    {
                        TriedToMoveToMobsCount++;
                        if (TriedToMoveToMobsCount >= 2)
                        {
                            ThisAttackPos = ResetMovePostionInBetween(ThisAttackPos);
                            Form1_0.Mover_0.MoveToLocationAttack(ThisAttackPos.X, ThisAttackPos.Y);
                            TriedToMoveToMobsCount = 0;
                        }
                    }
                    Form1_0.KeyMouse_0.ReleaseKey(CharConfig.KeyForceMovement);
                }
                //Form1_0.Mover_0.MoveToLocationAttack(Form1_0.MobsStruc_0.xPosFinal - 1, Form1_0.MobsStruc_0.yPosFinal + 2);
                Form1_0.Mover_0.MoveAcceptOffset = 4;
                ResetBattleMoveAcceptOffset();


                FirstAttackCasting();
                SetSkills();
                CastSkills();
                if (CharConfig.RunningOnChar == "Paladin")
                {
                    CastSkills();
                    CastSkills();
                }
                AttackTryCheck();
            }
            else
            {
                //LastMobType = "";
                //LastMobName = "";
                Form1_0.MobsStruc_0.xPosFinal = 0;
                Form1_0.MobsStruc_0.yPosFinal = 0;
                //if (CharConfig.RunBaalScript && !Form1_0.Baal_0.ScriptDone && Form1_0.Baal_0.Wave5Detected) Form1_0.Baal_0.Wave5Cleared = true;
                TriedToMoveToMobsCount = 0;
                DoingBattle = false;
                FirstAttackCasted = false;
                Form1_0.KeyMouse_0.ReleaseKey(CharConfig.KeyForceMovement);
            }
        }
        else
        {
            //LastMobType = "";
            //LastMobName = "";
            Form1_0.MobsStruc_0.xPosFinal = 0;
            Form1_0.MobsStruc_0.yPosFinal = 0;
            //if (CharConfig.RunBaalScript && !Form1_0.Baal_0.ScriptDone && Form1_0.Baal_0.Wave5Detected) Form1_0.Baal_0.Wave5Cleared = true;
            TriedToMoveToMobsCount = 0;
            DoingBattle = false;
            FirstAttackCasted = false;
            Form1_0.KeyMouse_0.ReleaseKey(CharConfig.KeyForceMovement);
        }
    }

    public void RunBattleScriptOnThisMob(string MobType, string MobName, List<long> IgnoredIDList)
    {
        LastMobType = MobType;
        LastMobName = MobName;
        RunBattleScriptOnLastMob(IgnoredIDList);
    }

    public Position ResetMovePostionInBetween(Position ThisPos)
    {
        Position ReturnPos = new Position { };
        ReturnPos.X = 0;
        ReturnPos.Y = 0;

        if (ThisPos.X >= Form1_0.PlayerScan_0.xPosFinal) ReturnPos.X = ThisPos.X - ((ThisPos.X - Form1_0.PlayerScan_0.xPosFinal) / 2);
        if (ThisPos.Y >= Form1_0.PlayerScan_0.yPosFinal) ReturnPos.Y = ThisPos.Y - ((ThisPos.Y - Form1_0.PlayerScan_0.yPosFinal) / 2);
        if (ThisPos.X < Form1_0.PlayerScan_0.xPosFinal) ReturnPos.X = ThisPos.X + ((Form1_0.PlayerScan_0.xPosFinal - ThisPos.X) / 2);
        if (ThisPos.Y < Form1_0.PlayerScan_0.yPosFinal) ReturnPos.Y = ThisPos.Y + ((Form1_0.PlayerScan_0.yPosFinal - ThisPos.Y) / 2);

        return ReturnPos;
    }

    public void MoveAway()
    {
        int MoveDistance = 5;
        //Form1_0.WaitDelay(5); //wait a little bit, we just casted attack
        if (MoveTryCount == 1)
        {
            Form1_0.Mover_0.MoveAcceptOffset = 2;
            Form1_0.Mover_0.MoveToLocationAttack(Form1_0.PlayerScan_0.xPosFinal + MoveDistance, Form1_0.PlayerScan_0.yPosFinal + MoveDistance);
            Form1_0.Mover_0.MoveAcceptOffset = 4;
        }
        if (MoveTryCount == 2)
        {
            Form1_0.Mover_0.MoveAcceptOffset = 2;
            Form1_0.Mover_0.MoveToLocationAttack(Form1_0.PlayerScan_0.xPosFinal - MoveDistance, Form1_0.PlayerScan_0.yPosFinal + MoveDistance);
            Form1_0.Mover_0.MoveAcceptOffset = 4;
        }
        if (MoveTryCount == 3)
        {
            Form1_0.Mover_0.MoveAcceptOffset = 2;
            Form1_0.Mover_0.MoveToLocationAttack(Form1_0.PlayerScan_0.xPosFinal + MoveDistance, Form1_0.PlayerScan_0.yPosFinal - MoveDistance);
            Form1_0.Mover_0.MoveAcceptOffset = 4;
        }
        if (MoveTryCount == 4)
        {
            Form1_0.Mover_0.MoveAcceptOffset = 2;
            Form1_0.Mover_0.MoveToLocationAttack(Form1_0.PlayerScan_0.xPosFinal - MoveDistance, Form1_0.PlayerScan_0.yPosFinal - MoveDistance);
            Form1_0.Mover_0.MoveAcceptOffset = 4;
        }
    }

    public void AttackTryCheck()
    {
        Form1_0.Potions_0.CheckIfWeUsePotion();
        Form1_0.MobsStruc_0.GetLastMobs();
        //long AttackedThisPointer = Form1_0.MobsStruc_0.LastMobsPointerLocation;

        //if (AttackedThisPointer == LastMobAttackedPointer)
        //{
        if (Form1_0.MobsStruc_0.MobsHP >= LastMobAttackedHP)
        {
            AttackNotRegisteredCount++;
            //Form1_0.method_1("Attack not registered! " + AttackNotRegisteredCount + "/" + MaxAttackTry, Color.OrangeRed);

            if (AttackNotRegisteredCount >= CharConfig.MaxBattleAttackTries)
            {
                AttackNotRegisteredCount = 0;
                MoveTryCount++;
                Form1_0.method_1("Attack not registered, moving away! " + MoveTryCount + "/" + MaxMoveTry, Color.OrangeRed);
                MoveAway();

                if (MoveTryCount >= MaxMoveTry)
                {
                    MoveTryCount = 0;
                    IgnoredMobsPointer.Add(Form1_0.MobsStruc_0.LastMobsPointerLocation);
                }
            }
        }
        else
        {
            //Form1_0.method_1("Attack registered! " + AttackNotRegisteredCount + "/" + MaxAttackTry, Color.DarkGreen);
            AttackNotRegisteredCount = 0;
            MoveTryCount = 0;
        }
        /*}
        else
        {
            AttackNotRegisteredCount = 0;
            MoveTryCount = 0;
        }*/

        //LastMobAttackedPointer = Form1_0.MobsStruc_0.LastMobsPointerLocation;
        LastMobAttackedHP = Form1_0.MobsStruc_0.MobsHP;
    }

    public void SetSkills()
    {
        Form1_0.KeyMouse_0.PressKey(CharConfig.KeySkillAttack);
        Form1_0.KeyMouse_0.PressKey(CharConfig.KeySkillAura);
    }

    public void CastSkills()
    {
        Form1_0.KeyMouse_0.ReleaseKey(CharConfig.KeyForceMovement);
        if (Form1_0.MobsStruc_0.xPosFinal != 0 && Form1_0.MobsStruc_0.yPosFinal != 0)
        {
            Form1_0.PlayerScan_0.GetPositions();
            Position itemScreenPos = Form1_0.GameStruc_0.World2Screen(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, Form1_0.MobsStruc_0.xPosFinal, Form1_0.MobsStruc_0.yPosFinal);
            if (!CharConfig.PlayerAttackWithRightHand)
            {
                Form1_0.KeyMouse_0.SendSHIFT_CLICK_ATTACK(itemScreenPos.X, itemScreenPos.Y - 30);
            }
            else
            {
                Form1_0.KeyMouse_0.MouseCliccRightAttackMove(itemScreenPos.X, itemScreenPos.Y - 30);
            }
        }
        else
        {
            if (!CharConfig.PlayerAttackWithRightHand)
            {
                Form1_0.KeyMouse_0.SendSHIFT_CLICK_ATTACK(Form1_0.CenterX, Form1_0.CenterY - 1);
            }
            else
            {
                Form1_0.KeyMouse_0.MouseCliccRightAttackMove(Form1_0.CenterX, Form1_0.CenterY - 1);
            }
        }
        Form1_0.KeyMouse_0.ReleaseKey(CharConfig.KeyForceMovement);
        //Form1_0.WaitDelay(5);
        //Form1_0.WaitDelay(1);
    }

    public void CastSkillsNoMove()
    {
        if (Form1_0.MobsStruc_0.xPosFinal != 0 && Form1_0.MobsStruc_0.yPosFinal != 0)
        {
            Form1_0.PlayerScan_0.GetPositions();
            Position itemScreenPos = Form1_0.GameStruc_0.World2Screen(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, Form1_0.MobsStruc_0.xPosFinal, Form1_0.MobsStruc_0.yPosFinal);
            if (!CharConfig.PlayerAttackWithRightHand)
            {
                //Form1_0.KeyMouse_0.SendSHIFT_CLICK_ATTACK(itemScreenPos.X, itemScreenPos.Y - 30);
                Form1_0.KeyMouse_0.SendSHIFT_CLICK_ATTACK_CAST_NO_MOVE(itemScreenPos.X, itemScreenPos.Y - 30);
            }
            else
            {
                Form1_0.KeyMouse_0.MouseCliccRightAttackMove(itemScreenPos.X, itemScreenPos.Y - 30);
            }
        }
        else
        {
            if (!CharConfig.PlayerAttackWithRightHand)
            {
                //Form1_0.KeyMouse_0.SendSHIFT_CLICK_ATTACK(Form1_0.CenterX, Form1_0.CenterY - 1);
                Form1_0.KeyMouse_0.SendSHIFT_CLICK_ATTACK_CAST_NO_MOVE(Form1_0.CenterX, Form1_0.CenterY - 1);
            }
            else
            {
                Form1_0.KeyMouse_0.MouseCliccRightAttackMove(Form1_0.CenterX, Form1_0.CenterY - 1);
            }
        }
        //Form1_0.WaitDelay(5);
        //Form1_0.WaitDelay(1);
    }

    public void FirstAttackCasting()
    {
        /*if (CharConfig.RunningOnChar == "Necromancer")
        {
            Form1_0.KeyMouse_0.PressKey(CharConfig.KeySkillAttack); //select teeth

            int tryes = 0;
            while (tryes < 6)
            {
                CastSkills();
                Form1_0.WaitDelay(35);
                tryes++;
            }
        }

        if (CharConfig.RunningOnChar == "Druid")
        {
            Form1_0.KeyMouse_0.PressKey(CharConfig.KeySkillAttack); //select twister

            int tryes = 0;
            while (tryes < 6)
            {
                CastSkills();
                Form1_0.WaitDelay(35);
                tryes++;
            }
        }

        if (!FirstAttackCasted)
        {
            if (CharConfig.RunningOnChar == "Barbarian")
            {
                Form1_0.KeyMouse_0.PressKey(CharConfig.KeySkillAttack); //select leap

                int tryes = 0;
                while (tryes < 6)
                {
                    CastSkills();
                    Form1_0.WaitDelay(35);
                    tryes++;
                }
            }

            if (CharConfig.RunningOnChar == "Assassin")
            {
                Form1_0.KeyMouse_0.PressKey(CharConfig.KeySkillAttack); //select shock web

                int tryes = 0;
                while (tryes < 6)
                {
                    CastSkills();
                    Form1_0.WaitDelay(35);
                    tryes++;
                }
            }

            if (CharConfig.RunningOnChar == "Amazon")
            {
                Form1_0.KeyMouse_0.PressKey(CharConfig.KeySkillAttack); //select charged strike

                int tryes = 0;
                while (tryes < 6)
                {
                    CastSkills();
                    Form1_0.WaitDelay(35);
                    tryes++;
                }
            }

            if (CharConfig.RunningOnChar == "Sorceress")
            {
                Form1_0.KeyMouse_0.PressKey(CharConfig.KeySkillAttack); //select static

                int tryes = 0;
                while (tryes < 6)
                {
                    CastSkills();
                    Form1_0.WaitDelay(35);
                    tryes++;
                }
            }

            FirstAttackCasted = true;
        }*/
    }
}
