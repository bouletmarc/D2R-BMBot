using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using static app.Enums;
using static app.MapAreaStruc;

namespace app
{
    public class PathFinding
    {
        // Define directions (assuming 4-direction movement)
        private int[] dx = { 1, 0, -1, 0, -1, 1, -1, 1 };
        private int[] dy = { 0, 1, 0, -1, -1, -1, 1, 1 };

        public Form1 Form1_0;

        public int TeleportAcceptSize = 20; //not implemented yet
        public int AcceptMoveOffset = 25;
        public int ThisPlayerAreaID = 0;
        public int ThisNextAreaID = 0;

        public bool IsMovingToNextArea = false;

        public Position LastMovingLocation = new Position { X = 0, Y = 0 };
        public Position ThisFinalPosition = new Position { X = 0, Y = 0 };
        public Position ThisOffsetPosition = new Position { X = 0, Y = 0 };

        public bool[,] ThisCollisionGrid = new bool[0,0];

        public bool IsMovingThruPath = false;
        public int CurrentPathIndex = 0;
        public List<Point> path = new List<Point>();

        public Position PlayerOffsetInCollisiongrid = new Position { X = 0, Y = 0 };
        public Position TargetOffsetInCollisiongrid = new Position { X = 0, Y = 0 };

        public bool CheckingForCloseToTargetPos = true;

        public void SetForm1(Form1 form1_1)
        {
            Form1_0 = form1_1;
        }

        public void CheckForBadMapData(int Thisindex)
        {
            while (Thisindex > Form1_0.MapAreaStruc_0.AllMapData.Count - 1)
            {
                Form1_0.MapAreaStruc_0.GetMapData(Form1_0.PlayerScan_0.mapSeedValue.ToString(), (Difficulty)Form1_0.PlayerScan_0.difficulty);
            }
        }

        public void DebugMapCollision()
        {
            Form1_0.ClearDebugCollision();

            ThisCollisionGrid = Form1_0.MapAreaStruc_0.CollisionGrid((Enums.Area)Form1_0.PlayerScan_0.levelNo);
            for (int i = 0; i < ThisCollisionGrid.GetLength(1); i++)
            {
                for (int k = 0; k < ThisCollisionGrid.GetLength(0); k++)
                {
                    if (ThisCollisionGrid[k, i]) Form1_0.AppendTextDebugCollision("-");
                    if (!ThisCollisionGrid[k, i]) Form1_0.AppendTextDebugCollision("X");
                }
                Form1_0.AppendTextDebugCollision(Environment.NewLine);
            }
        }

        public void MoveToNextArea(Area ThisID, int AcceptOffset = 4, bool ClearAreaOnMoving = false)
        {
            Form1_0.PlayerScan_0.GetPositions();
            IsMovingToNextArea = true;
            ThisNextAreaID = (int)ThisID;
            ThisPlayerAreaID = (int)Form1_0.PlayerScan_0.levelNo;
            ThisCollisionGrid = ExpandGrid(ThisID);
            CheckingForCloseToTargetPos = true;

            try
            {
                CheckForBadMapData(ThisPlayerAreaID - 1);

                //The Exit or Object find is only for a reference for pathing to the next AreaID, when we enter the next AreaID it will go out of the pathing loop
                //Get any 'exit' object in the next areaID for path reference (ignore object name)
                ThisFinalPosition = Form1_0.MapAreaStruc_0.GetPositionOfObject("exit", Form1_0.Town_0.getAreaName((int)ThisID), (int)ThisID, new List<int>() { }, true);

                //Get any 'object' in the next areaID for path reference in case we didn't find any exit (ignore object name)
                if (ThisFinalPosition.X == 0 && ThisFinalPosition.Y == 0) ThisFinalPosition = Form1_0.MapAreaStruc_0.GetPositionOfObject("object", Form1_0.Town_0.getAreaName((int)ThisID), (int)ThisID, new List<int>() { }, true);

                //Console.WriteLine("Going to Pos: " + ThisFinalPosition.X + ", " + ThisFinalPosition.Y);
                Form1_0.PathFinding_0.GetPathFinding(AcceptOffset, ClearAreaOnMoving);
            }
            catch { }
        }

        public void MoveToExit(Area ThisID, int AcceptOffset = 4, bool ClearAreaOnMoving = false)
        {
            Form1_0.PlayerScan_0.GetPositions();
            IsMovingToNextArea = false;
            CheckingForCloseToTargetPos = false;

            //Form1_0.method_1("ToExit " + Form1_0.Town_0.getAreaName((int)ThisID), Color.Red);
            try
            {
                ThisPlayerAreaID = (int)Form1_0.PlayerScan_0.levelNo;
                CheckForBadMapData(ThisPlayerAreaID - 1);

                ThisFinalPosition = Form1_0.MapAreaStruc_0.GetPositionOfObject("exit", Form1_0.Town_0.getAreaName((int)ThisID), ThisPlayerAreaID, new List<int>() { });
                ThisOffsetPosition = new Position { X = Form1_0.MapAreaStruc_0.AllMapData[ThisPlayerAreaID - 1].Offset.X, Y = Form1_0.MapAreaStruc_0.AllMapData[ThisPlayerAreaID - 1].Offset.Y };
                ThisCollisionGrid = Form1_0.MapAreaStruc_0.CollisionGrid((Enums.Area)Form1_0.PlayerScan_0.levelNo);

                try
                {
                    ThisCollisionGrid[ThisFinalPosition.X - ThisOffsetPosition.X, ThisFinalPosition.Y - ThisOffsetPosition.Y] = true; //make sure the exit is walkable
                }
                catch { }

                PlayerOffsetInCollisiongrid = new Position { X = 0, Y = 0 };
                TargetOffsetInCollisiongrid = new Position { X = 0, Y = 0 };

                //Console.WriteLine("Going to Pos: " + ThisFinalPosition.X + ", " + ThisFinalPosition.Y);
                Form1_0.PathFinding_0.GetPathFinding(AcceptOffset, ClearAreaOnMoving);
            }
            catch { }
        }

        public void MoveToNPC(string NPCName, int AcceptOffset = 4, bool ClearAreaOnMoving = false)
        {
            Form1_0.PlayerScan_0.GetPositions();
            IsMovingToNextArea = false;
            CheckingForCloseToTargetPos = false;

            //Form1_0.method_1("ToNPC " + NPCName, Color.Red);

            try
            {
                ThisPlayerAreaID = (int)Form1_0.PlayerScan_0.levelNo;
                CheckForBadMapData(ThisPlayerAreaID - 1);

                ThisFinalPosition = Form1_0.MapAreaStruc_0.GetPositionOfObject("npc", NPCName, ThisPlayerAreaID, new List<int>() { });
                ThisOffsetPosition = new Position { X = Form1_0.MapAreaStruc_0.AllMapData[ThisPlayerAreaID - 1].Offset.X, Y = Form1_0.MapAreaStruc_0.AllMapData[ThisPlayerAreaID - 1].Offset.Y };
                ThisCollisionGrid = Form1_0.MapAreaStruc_0.CollisionGrid((Enums.Area)Form1_0.PlayerScan_0.levelNo);

                PlayerOffsetInCollisiongrid = new Position { X = 0, Y = 0 };
                TargetOffsetInCollisiongrid = new Position { X = 0, Y = 0 };

                Form1_0.PathFinding_0.GetPathFinding(AcceptOffset, ClearAreaOnMoving);
            }
            catch { }
        }

        public void MoveToObject(string ObjectName, int AcceptOffset = 4, bool ClearAreaOnMoving = false)
        {
            Form1_0.PlayerScan_0.GetPositions();
            IsMovingToNextArea = false;
            CheckingForCloseToTargetPos = false;

            //Form1_0.method_1("ToObject " + ObjectName, Color.Red);

            try
            {
                ThisPlayerAreaID = (int)Form1_0.PlayerScan_0.levelNo;
                CheckForBadMapData(ThisPlayerAreaID - 1);

                ThisFinalPosition = Form1_0.MapAreaStruc_0.GetPositionOfObject("object", ObjectName, ThisPlayerAreaID, new List<int>() { });
                ThisOffsetPosition = new Position { X = Form1_0.MapAreaStruc_0.AllMapData[ThisPlayerAreaID - 1].Offset.X, Y = Form1_0.MapAreaStruc_0.AllMapData[ThisPlayerAreaID - 1].Offset.Y };
                ThisCollisionGrid = Form1_0.MapAreaStruc_0.CollisionGrid((Enums.Area)Form1_0.PlayerScan_0.levelNo);

                PlayerOffsetInCollisiongrid = new Position { X = 0, Y = 0 };
                TargetOffsetInCollisiongrid = new Position { X = 0, Y = 0 };

                Form1_0.PathFinding_0.GetPathFinding(AcceptOffset, ClearAreaOnMoving);
            }
            catch { }
        }

        public void MoveToThisPos(Position ThisPositionn, int AcceptOffset = 4, bool ClearAreaOnMoving = false)
        {
            if (Form1_0.PlayerScan_0.levelNo == 0) Form1_0.PlayerScan_0.GetPositions();
            IsMovingToNextArea = false;
            CheckingForCloseToTargetPos = true;

            //Form1_0.method_1("ToThisPos", Color.Red);

            try
            {
                ThisPlayerAreaID = (int)Form1_0.PlayerScan_0.levelNo;
                CheckForBadMapData(ThisPlayerAreaID - 1);

                ThisFinalPosition = ThisPositionn;
                ThisOffsetPosition = new Position { X = Form1_0.MapAreaStruc_0.AllMapData[ThisPlayerAreaID - 1].Offset.X, Y = Form1_0.MapAreaStruc_0.AllMapData[ThisPlayerAreaID - 1].Offset.Y };
                ThisCollisionGrid = Form1_0.MapAreaStruc_0.CollisionGrid((Enums.Area)Form1_0.PlayerScan_0.levelNo);

                PlayerOffsetInCollisiongrid = new Position { X = 0, Y = 0 };
                TargetOffsetInCollisiongrid = new Position { X = 0, Y = 0 };

                GetPathFinding(AcceptOffset, ClearAreaOnMoving);
            }
            catch { }
        }

        public void GetPathFinding(int AcceptOffset = 4, bool ClearAreaOnMoving = false)
        {
            Point startPos = new Point(Form1_0.PlayerScan_0.xPos - ThisOffsetPosition.X, Form1_0.PlayerScan_0.yPos - ThisOffsetPosition.Y);
            Point targetPos = new Point(ThisFinalPosition.X - ThisOffsetPosition.X, ThisFinalPosition.Y - ThisOffsetPosition.Y);
            //Point startPos = new Point(115, 579);
            //Point targetPos = new Point(41, 429);

            startPos.X += PlayerOffsetInCollisiongrid.X;
            startPos.Y += PlayerOffsetInCollisiongrid.Y;

            targetPos.X += TargetOffsetInCollisiongrid.X;
            targetPos.Y += TargetOffsetInCollisiongrid.Y;

            /*if (targetPos.X < 0 || targetPos.Y < 0)
            {
                Form1_0.method_1("ERROR Target pos: " + targetPos.X + ", " + targetPos.Y, Color.Red);
                ThisFinalPosition = new Position { X = ThisFinalPosition.X + ThisOffsetPosition.X, Y = ThisFinalPosition.Y + ThisOffsetPosition.Y };
                targetPos = new Point(ThisFinalPosition.X - ThisOffsetPosition.X, ThisFinalPosition.Y - ThisOffsetPosition.Y);
            }*/

            //no need to move we are close already!
            if (CheckingForCloseToTargetPos)
            {
                if (startPos.X >= (targetPos.X - Form1_0.Mover_0.MoveAcceptOffset)
                    && startPos.X <= (targetPos.X + Form1_0.Mover_0.MoveAcceptOffset)
                    && startPos.Y >= (targetPos.Y - Form1_0.Mover_0.MoveAcceptOffset)
                    && startPos.Y <= (targetPos.Y + Form1_0.Mover_0.MoveAcceptOffset))
                {
                    return;
                }
            }

            //Form1_0.method_1("Start pos: " + startPos.X + ", " + startPos.Y, Color.Red);
            //Form1_0.method_1("Target pos: " + targetPos.X + ", " + targetPos.Y, Color.Red);

            /*if (ThisOffsetPosition.X == 0 && ThisOffsetPosition.Y == 0)
            {
                Form1_0.method_1("Offsets are bad!", Color.Red);
            }*/
            if (targetPos.X <= 0 || targetPos.Y <= 0)
            {
                Form1_0.method_1("Target pos are bad: " + targetPos.X + ", " + targetPos.Y, Color.OrangeRed);
                return;
            }

            path = FindPath(startPos, targetPos);
            if (path == null)
            {
                //dump data to txt file
                /*string ColisionMapTxt = "";
                for (int i = 0; i < ThisCollisionGrid.GetLength(1); i++)
                {
                    for (int k = 0; k < ThisCollisionGrid.GetLength(0); k++)
                    {
                        if (ThisCollisionGrid[k, i]) ColisionMapTxt += "-";
                        if (!ThisCollisionGrid[k, i]) ColisionMapTxt += "X";
                    }
                    ColisionMapTxt += Environment.NewLine;
                }
                File.Create(Form1_0.ThisEndPath + "CollisionMap.txt").Dispose();
                File.WriteAllText(Form1_0.ThisEndPath + "CollisionMap.txt", ColisionMapTxt);*/

                Form1_0.method_1("No path found.", Color.Red);
                //Form1_0.MapAreaStruc_0.DumpMap();
                Form1_0.GoToNextScript();
                return;
            }

            //################################################
            //Shorten the path so we don't go at each single unit
            int ThisOffsetToUse = AcceptMoveOffset;
            if (Form1_0.Town_0.IsInTown) ThisOffsetToUse = 5;
            else if (!CharConfig.UseTeleport) ThisOffsetToUse = 5;

            List<Point> pathShortened = new List<Point>();
            int LastPathAdded = 0;
            for (int i = 0; i < path.Count; i += ThisOffsetToUse)
            {
                pathShortened.Add(path[i]);
                LastPathAdded = i;
            }
            if (LastPathAdded != path.Count - 1) pathShortened.Add(path[path.Count - 1]);
            path = pathShortened;
            //################################################

            IsMovingThruPath = false;
            CurrentPathIndex = 0;
            int BadPathIndexount = 0;
            if (path != null)
            {
                IsMovingThruPath = true;
                while (IsMovingThruPath)
                {
                    Form1_0.PlayerScan_0.GetPositions();
                    ThisPlayerAreaID = (int)Form1_0.PlayerScan_0.levelNo;

                    //we are close to accept offset, stop moving to path
                    if (IsCloseToLocation(ThisFinalPosition, AcceptOffset))
                    {
                        IsMovingThruPath = false;
                        break;
                    }

                    //we are within the next area, stop moving to path
                    if (IsMovingToNextArea)
                    {
                        if (ThisPlayerAreaID == ThisNextAreaID)
                        {
                            path = null;
                            CurrentPathIndex = 0;
                            IsMovingThruPath = false;
                            break;
                        }
                    }

                    if (CurrentPathIndex > path.Count - 1)
                    {
                        IsMovingThruPath = false;
                        break;
                    }

                    DetectCloserPathPositions();

                    if (CurrentPathIndex < path.Count - 1)
                    {
                        Form1_0.Mover_0.MoveAcceptOffset = 7;
                    }
                    else
                    {
                        Form1_0.Mover_0.MoveAcceptOffset = 4;
                    }

                    //Console.WriteLine("Pos test: " + path[CurrentPathIndex].X + ", " + path[CurrentPathIndex].Y);
                    if (Form1_0.Mover_0.MoveToLocation(path[CurrentPathIndex].X + ThisOffsetPosition.X - PlayerOffsetInCollisiongrid.X, path[CurrentPathIndex].Y + ThisOffsetPosition.Y - PlayerOffsetInCollisiongrid.Y, false, false))
                    {
                        BadPathIndexount = 0;
                        CurrentPathIndex++;
                        Form1_0.overlayForm.SetPathPoints(path, CurrentPathIndex, ThisOffsetPosition, PlayerOffsetInCollisiongrid);
                        if (CurrentPathIndex >= path.Count - 1)
                        {
                            IsMovingThruPath = false;
                        }
                        else
                        {
                            if (ClearAreaOnMoving)
                            {
                                //Form1_0.method_1("Clearing area of mobs...", Color.Red);
                                Form1_0.Battle_0.ClearAreaOfMobs(path[CurrentPathIndex].X + ThisOffsetPosition.X - PlayerOffsetInCollisiongrid.X, path[CurrentPathIndex].Y + ThisOffsetPosition.Y - PlayerOffsetInCollisiongrid.Y, AcceptMoveOffset + 2);

                                //stop moving thru the pathfinding, we are clearing the area of mobs
                                if (Form1_0.Battle_0.ClearingArea)
                                {
                                    IsMovingToNextArea = true;
                                    IsMovingThruPath = false;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (path[CurrentPathIndex].X != LastMovingLocation.X || path[CurrentPathIndex].Y != LastMovingLocation.Y)
                        {
                            LastMovingLocation.X = path[CurrentPathIndex].X;
                            LastMovingLocation.Y = path[CurrentPathIndex].Y;
                        }
                        else
                        {
                            BadPathIndexount++;
                            CurrentPathIndex++;
                            Form1_0.overlayForm.SetPathPoints(path, CurrentPathIndex, ThisOffsetPosition, PlayerOffsetInCollisiongrid);
                            if (BadPathIndexount >= 3)
                            {
                                IsMovingThruPath = false;
                            }
                        }
                    }
                }

                if (!IsMovingToNextArea)
                {
                    if (AcceptOffset == 4) Form1_0.Mover_0.MoveToLocation(ThisFinalPosition.X - PlayerOffsetInCollisiongrid.X, ThisFinalPosition.Y - PlayerOffsetInCollisiongrid.Y);

                    //int tryyy = 0;
                    //while (Form1_0.PlayerScan_0.levelNo == ThisPlayerAreaID && tryyy <= 25)
                    //{
                    Dictionary<string, int> itemScreenPos = Form1_0.GameStruc_0.World2Screen(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, ThisFinalPosition.X - PlayerOffsetInCollisiongrid.X, ThisFinalPosition.Y - PlayerOffsetInCollisiongrid.Y);
                    Form1_0.KeyMouse_0.MouseClicc(itemScreenPos["x"], itemScreenPos["y"]);
                    Form1_0.PlayerScan_0.GetPositions();
                    //tryyy++;
                    //}
                }
            }
            else
            {
                Form1_0.method_1("No path found.", Color.Red);
                Form1_0.GoToNextScript();
            }
        }

        public void DetectCloserPathPositions()
        {
            if (IsMovingThruPath)
            {
                int DistX = Form1_0.PlayerScan_0.xPos - path[CurrentPathIndex].X;
                int DistY = Form1_0.PlayerScan_0.yPos - path[CurrentPathIndex].Y;
                if (DistX < 0) DistX = -DistX;
                if (DistY < 0) DistY = -DistY;

                int CheckIndexxx = CurrentPathIndex + 1;
                int CheckCount = 0;
                while (CheckIndexxx < path.Count - 1 && CheckCount < 1)
                {
                    int DistNextX = Form1_0.PlayerScan_0.xPos - path[CheckIndexxx].X;
                    int DistNextY = Form1_0.PlayerScan_0.yPos - path[CheckIndexxx].Y;
                    if (DistNextX < 0) DistNextX = -DistNextX;
                    if (DistNextY < 0) DistNextY = -DistNextY;

                    if (DistNextX < DistX && DistNextY < DistY)
                    {
                        CurrentPathIndex++; //increase pathing index, we are closer to the next destination than the current one!
                        Form1_0.overlayForm.SetPathPoints(path, CurrentPathIndex, ThisOffsetPosition, PlayerOffsetInCollisiongrid);
                    }
                    CheckIndexxx++;
                    CheckCount++;
                }
            }
        }

        public bool[,] ExpandGrid(Enums.Area ThisNewArea)
        {
            bool[,] CurrentAreaGrid = Form1_0.MapAreaStruc_0.CollisionGrid((Enums.Area)Form1_0.PlayerScan_0.levelNo);
            bool[,] NextAreaGrid = Form1_0.MapAreaStruc_0.CollisionGrid(ThisNewArea);
            bool[,] ExpendedGrid = new bool[0,0];

            //Console.WriteLine("CurrentArea: " + CurrentAreaGrid.GetLength(0) + ", " + CurrentAreaGrid.GetLength(1));
            //Console.WriteLine("NextArea: " + NextAreaGrid.GetLength(0) + ", " + NextAreaGrid.GetLength(1));
            PlayerOffsetInCollisiongrid = new Position { X = 0, Y = 0 };
            TargetOffsetInCollisiongrid = new Position { X = 0, Y = 0 };

            if (Form1_0.MapAreaStruc_0.AllMapData[(int)ThisNewArea - 1].Offset.Y == Form1_0.MapAreaStruc_0.AllMapData[ThisPlayerAreaID - 1].Offset.Y + Form1_0.MapAreaStruc_0.AllMapData[ThisPlayerAreaID - 1].Size.Height)
            {
                //Expend Bottom
                int NewSizeX = 0;
                int NewOffsetXUp = 0;
                int NewOffsetXBottom = 0;
                if (Form1_0.MapAreaStruc_0.AllMapData[(int)ThisNewArea - 1].Size.Width > Form1_0.MapAreaStruc_0.AllMapData[ThisPlayerAreaID - 1].Size.Width) NewSizeX = Form1_0.MapAreaStruc_0.AllMapData[(int)ThisNewArea - 1].Size.Width;
                else if (Form1_0.MapAreaStruc_0.AllMapData[ThisPlayerAreaID - 1].Size.Width >= Form1_0.MapAreaStruc_0.AllMapData[(int)ThisNewArea - 1].Size.Width) NewSizeX = Form1_0.MapAreaStruc_0.AllMapData[ThisPlayerAreaID - 1].Size.Width;
                //#####
                if (Form1_0.MapAreaStruc_0.AllMapData[ThisPlayerAreaID - 1].Offset.X > Form1_0.MapAreaStruc_0.AllMapData[(int)ThisNewArea - 1].Offset.X)
                {
                    NewOffsetXUp = 0;
                    NewOffsetXBottom = Form1_0.MapAreaStruc_0.AllMapData[ThisPlayerAreaID - 1].Offset.X - Form1_0.MapAreaStruc_0.AllMapData[(int)ThisNewArea - 1].Offset.X;
                    NewSizeX += NewOffsetXBottom;
                    PlayerOffsetInCollisiongrid.X = NewOffsetXBottom;
                    TargetOffsetInCollisiongrid.X = NewOffsetXBottom;

                }
                if (Form1_0.MapAreaStruc_0.AllMapData[ThisPlayerAreaID - 1].Offset.X < Form1_0.MapAreaStruc_0.AllMapData[(int)ThisNewArea - 1].Offset.X)
                {
                    NewOffsetXUp = Form1_0.MapAreaStruc_0.AllMapData[(int)ThisNewArea - 1].Offset.X - Form1_0.MapAreaStruc_0.AllMapData[ThisPlayerAreaID - 1].Offset.X;
                    NewOffsetXBottom = 0;
                    NewSizeX += NewOffsetXUp;
                    PlayerOffsetInCollisiongrid.X = NewOffsetXUp;
                    TargetOffsetInCollisiongrid.X = NewOffsetXUp;
                }
                //#####

                int NewSizeY = Form1_0.MapAreaStruc_0.AllMapData[(int)ThisNewArea - 1].Size.Height + Form1_0.MapAreaStruc_0.AllMapData[ThisPlayerAreaID - 1].Size.Height;
                
                ExpendedGrid = new bool[NewSizeX, NewSizeY];
                //Console.WriteLine("ExpendedSize: " + NewSizeX + ", " + NewSizeY);

                //Merge Both CollisionGrid into one
                for (int i = 0; i < CurrentAreaGrid.GetLength(0); i++)
                {
                    for (int k = 0; k < CurrentAreaGrid.GetLength(1); k++)
                    {
                        ExpendedGrid[i + NewOffsetXUp, k] = CurrentAreaGrid[i, k];
                    }
                }

                for (int i = 0; i < NextAreaGrid.GetLength(0); i++)
                {
                    for (int k = 0; k < NextAreaGrid.GetLength(1); k++)
                    {
                        ExpendedGrid[i + NewOffsetXBottom, k + Form1_0.MapAreaStruc_0.AllMapData[ThisPlayerAreaID - 1].Size.Height] = NextAreaGrid[i, k];
                    }
                }

                //Set New OffsetPosition
                ThisOffsetPosition = new Position { X = Form1_0.MapAreaStruc_0.AllMapData[ThisPlayerAreaID - 1].Offset.X, Y = Form1_0.MapAreaStruc_0.AllMapData[ThisPlayerAreaID - 1].Offset.Y };
            }
            if (Form1_0.MapAreaStruc_0.AllMapData[(int)ThisNewArea - 1].Offset.Y == Form1_0.MapAreaStruc_0.AllMapData[ThisPlayerAreaID - 1].Offset.Y - Form1_0.MapAreaStruc_0.AllMapData[(int)ThisNewArea - 1].Size.Height)
            {
                //Expend Up
                int NewSizeX = 0;
                int NewOffsetXUp = 0;
                int NewOffsetXBottom = 0;
                if (Form1_0.MapAreaStruc_0.AllMapData[(int)ThisNewArea - 1].Size.Width > Form1_0.MapAreaStruc_0.AllMapData[ThisPlayerAreaID - 1].Size.Width) NewSizeX = Form1_0.MapAreaStruc_0.AllMapData[(int)ThisNewArea - 1].Size.Width;
                else if (Form1_0.MapAreaStruc_0.AllMapData[ThisPlayerAreaID - 1].Size.Width >= Form1_0.MapAreaStruc_0.AllMapData[(int)ThisNewArea - 1].Size.Width) NewSizeX = Form1_0.MapAreaStruc_0.AllMapData[ThisPlayerAreaID - 1].Size.Width;
                //#####
                if (Form1_0.MapAreaStruc_0.AllMapData[ThisPlayerAreaID - 1].Offset.X > Form1_0.MapAreaStruc_0.AllMapData[(int)ThisNewArea - 1].Offset.X)
                {
                    NewOffsetXUp = 0;
                    NewOffsetXBottom = Form1_0.MapAreaStruc_0.AllMapData[ThisPlayerAreaID - 1].Offset.X - Form1_0.MapAreaStruc_0.AllMapData[(int)ThisNewArea - 1].Offset.X;
                    NewSizeX += NewOffsetXBottom;
                    PlayerOffsetInCollisiongrid.X = NewOffsetXBottom;
                    TargetOffsetInCollisiongrid.X = NewOffsetXBottom;
                }
                if (Form1_0.MapAreaStruc_0.AllMapData[ThisPlayerAreaID - 1].Offset.X < Form1_0.MapAreaStruc_0.AllMapData[(int)ThisNewArea - 1].Offset.X)
                {
                    NewOffsetXUp = Form1_0.MapAreaStruc_0.AllMapData[(int)ThisNewArea - 1].Offset.X - Form1_0.MapAreaStruc_0.AllMapData[ThisPlayerAreaID - 1].Offset.X;
                    NewOffsetXBottom = 0;
                    NewSizeX += NewOffsetXUp;
                    PlayerOffsetInCollisiongrid.X = NewOffsetXUp;
                    TargetOffsetInCollisiongrid.X = NewOffsetXUp;
                }
                //#####

                int NewSizeY = Form1_0.MapAreaStruc_0.AllMapData[(int)ThisNewArea - 1].Size.Height + Form1_0.MapAreaStruc_0.AllMapData[ThisPlayerAreaID - 1].Size.Height;

                ExpendedGrid = new bool[NewSizeX, NewSizeY];
                //Console.WriteLine("ExpendedSize: " + NewSizeX + ", " + NewSizeY);

                //Merge Both CollisionGrid into one
                for (int i = 0; i < CurrentAreaGrid.GetLength(0); i++)
                {
                    for (int k = 0; k < CurrentAreaGrid.GetLength(1); k++)
                    {
                        ExpendedGrid[i + NewOffsetXBottom, k + Form1_0.MapAreaStruc_0.AllMapData[(int)ThisNewArea - 1].Size.Height] = CurrentAreaGrid[i, k];
                    }
                }

                for (int i = 0; i < NextAreaGrid.GetLength(0); i++)
                {
                    for (int k = 0; k < NextAreaGrid.GetLength(1); k++)
                    {
                        ExpendedGrid[i + NewOffsetXUp, k] = NextAreaGrid[i, k];
                    }
                }

                //Set New OffsetPosition
                ThisOffsetPosition = new Position { X = Form1_0.MapAreaStruc_0.AllMapData[(int)ThisNewArea - 1].Offset.X, Y = Form1_0.MapAreaStruc_0.AllMapData[(int)ThisNewArea - 1].Offset.Y };
            }
            if (Form1_0.MapAreaStruc_0.AllMapData[(int)ThisNewArea - 1].Offset.X == Form1_0.MapAreaStruc_0.AllMapData[ThisPlayerAreaID - 1].Offset.X + Form1_0.MapAreaStruc_0.AllMapData[ThisPlayerAreaID - 1].Size.Width)
            {
                //Expend Right
                int NewSizeX = Form1_0.MapAreaStruc_0.AllMapData[(int)ThisNewArea - 1].Size.Width + Form1_0.MapAreaStruc_0.AllMapData[ThisPlayerAreaID - 1].Size.Width;

                int NewSizeY = 0;
                int NewOffsetYLeft = 0;
                int NewOffsetYRight = 0;
                if (Form1_0.MapAreaStruc_0.AllMapData[(int)ThisNewArea - 1].Size.Height > Form1_0.MapAreaStruc_0.AllMapData[ThisPlayerAreaID - 1].Size.Height) NewSizeY = Form1_0.MapAreaStruc_0.AllMapData[(int)ThisNewArea - 1].Size.Height;
                else if (Form1_0.MapAreaStruc_0.AllMapData[ThisPlayerAreaID - 1].Size.Height >= Form1_0.MapAreaStruc_0.AllMapData[(int)ThisNewArea - 1].Size.Height) NewSizeY = Form1_0.MapAreaStruc_0.AllMapData[ThisPlayerAreaID - 1].Size.Height;
                //#####
                if (Form1_0.MapAreaStruc_0.AllMapData[ThisPlayerAreaID - 1].Offset.Y > Form1_0.MapAreaStruc_0.AllMapData[(int)ThisNewArea - 1].Offset.Y)
                {
                    NewOffsetYLeft = 0;
                    NewOffsetYRight = Form1_0.MapAreaStruc_0.AllMapData[ThisPlayerAreaID - 1].Offset.Y - Form1_0.MapAreaStruc_0.AllMapData[(int)ThisNewArea - 1].Offset.Y;
                    NewSizeY += NewOffsetYRight;
                    PlayerOffsetInCollisiongrid.Y = NewOffsetYRight;
                    TargetOffsetInCollisiongrid.Y = NewOffsetYRight;

                }
                if (Form1_0.MapAreaStruc_0.AllMapData[ThisPlayerAreaID - 1].Offset.Y < Form1_0.MapAreaStruc_0.AllMapData[(int)ThisNewArea - 1].Offset.Y)
                {
                    NewOffsetYLeft = Form1_0.MapAreaStruc_0.AllMapData[(int)ThisNewArea - 1].Offset.Y - Form1_0.MapAreaStruc_0.AllMapData[ThisPlayerAreaID - 1].Offset.Y;
                    NewOffsetYRight = 0;
                    NewSizeY += NewOffsetYLeft;
                    PlayerOffsetInCollisiongrid.Y = NewOffsetYLeft;
                    TargetOffsetInCollisiongrid.Y = NewOffsetYLeft;
                }
                //#####

                ExpendedGrid = new bool[NewSizeX, NewSizeY];
                //Console.WriteLine("ExpendedSize: " + NewSizeX + ", " + NewSizeY);

                //Merge Both CollisionGrid into one
                for (int i = 0; i < CurrentAreaGrid.GetLength(0); i++)
                {
                    for (int k = 0; k < CurrentAreaGrid.GetLength(1); k++)
                    {
                        ExpendedGrid[i, k + NewOffsetYLeft] = CurrentAreaGrid[i, k];
                    }
                }

                for (int i = 0; i < NextAreaGrid.GetLength(0); i++)
                {
                    for (int k = 0; k < NextAreaGrid.GetLength(1); k++)
                    {
                        ExpendedGrid[i + Form1_0.MapAreaStruc_0.AllMapData[ThisPlayerAreaID - 1].Size.Width, k + NewOffsetYRight] = NextAreaGrid[i, k];
                    }
                }

                //Set New OffsetPosition
                ThisOffsetPosition = new Position { X = Form1_0.MapAreaStruc_0.AllMapData[ThisPlayerAreaID - 1].Offset.X, Y = Form1_0.MapAreaStruc_0.AllMapData[ThisPlayerAreaID - 1].Offset.Y };
            }
            if (Form1_0.MapAreaStruc_0.AllMapData[(int)ThisNewArea - 1].Offset.X == Form1_0.MapAreaStruc_0.AllMapData[ThisPlayerAreaID - 1].Offset.X - Form1_0.MapAreaStruc_0.AllMapData[(int)ThisNewArea - 1].Size.Width)
            {
                //Expend Left
                int NewSizeX = Form1_0.MapAreaStruc_0.AllMapData[(int)ThisNewArea - 1].Size.Width + Form1_0.MapAreaStruc_0.AllMapData[ThisPlayerAreaID - 1].Size.Width;

                int NewSizeY = 0;
                int NewOffsetYLeft = 0;
                int NewOffsetYRight = 0;
                if (Form1_0.MapAreaStruc_0.AllMapData[(int)ThisNewArea - 1].Size.Height > Form1_0.MapAreaStruc_0.AllMapData[ThisPlayerAreaID - 1].Size.Height) NewSizeY = Form1_0.MapAreaStruc_0.AllMapData[(int)ThisNewArea - 1].Size.Height;
                else if (Form1_0.MapAreaStruc_0.AllMapData[ThisPlayerAreaID - 1].Size.Height >= Form1_0.MapAreaStruc_0.AllMapData[(int)ThisNewArea - 1].Size.Height) NewSizeY = Form1_0.MapAreaStruc_0.AllMapData[ThisPlayerAreaID - 1].Size.Height;
                //#####
                if (Form1_0.MapAreaStruc_0.AllMapData[ThisPlayerAreaID - 1].Offset.Y > Form1_0.MapAreaStruc_0.AllMapData[(int)ThisNewArea - 1].Offset.Y)
                {
                    NewOffsetYLeft = 0;
                    NewOffsetYRight = Form1_0.MapAreaStruc_0.AllMapData[ThisPlayerAreaID - 1].Offset.Y - Form1_0.MapAreaStruc_0.AllMapData[(int)ThisNewArea - 1].Offset.Y;
                    NewSizeY += NewOffsetYRight;
                    PlayerOffsetInCollisiongrid.Y = NewOffsetYRight;
                    TargetOffsetInCollisiongrid.Y = NewOffsetYRight;

                }
                if (Form1_0.MapAreaStruc_0.AllMapData[ThisPlayerAreaID - 1].Offset.Y < Form1_0.MapAreaStruc_0.AllMapData[(int)ThisNewArea - 1].Offset.Y)
                {
                    NewOffsetYLeft = Form1_0.MapAreaStruc_0.AllMapData[(int)ThisNewArea - 1].Offset.Y - Form1_0.MapAreaStruc_0.AllMapData[ThisPlayerAreaID - 1].Offset.Y;
                    NewOffsetYRight = 0;
                    NewSizeY += NewOffsetYLeft;
                    PlayerOffsetInCollisiongrid.Y = NewOffsetYLeft;
                    TargetOffsetInCollisiongrid.Y = NewOffsetYLeft;
                }
                //#####

                ExpendedGrid = new bool[NewSizeX, NewSizeY];
                //Console.WriteLine("ExpendedSize: " + NewSizeX + ", " + NewSizeY);

                //Merge Both CollisionGrid into one
                for (int i = 0; i < CurrentAreaGrid.GetLength(0); i++)
                {
                    for (int k = 0; k < CurrentAreaGrid.GetLength(1); k++)
                    {
                        ExpendedGrid[i + Form1_0.MapAreaStruc_0.AllMapData[(int)ThisNewArea - 1].Size.Width, k + NewOffsetYRight] = CurrentAreaGrid[i, k];
                    }
                }

                for (int i = 0; i < NextAreaGrid.GetLength(0); i++)
                {
                    for (int k = 0; k < NextAreaGrid.GetLength(1); k++)
                    {
                        ExpendedGrid[i, k + NewOffsetYLeft] = NextAreaGrid[i, k];
                    }
                }

                //Set New OffsetPosition
                ThisOffsetPosition = new Position { X = Form1_0.MapAreaStruc_0.AllMapData[(int)ThisNewArea - 1].Offset.X, Y = Form1_0.MapAreaStruc_0.AllMapData[(int)ThisNewArea - 1].Offset.Y };
            }

            //dump data to txt file
            /*string ColisionMapTxt = "";
            for (int i = 0; i < ExpendedGrid.GetLength(1); i++)
            {
                for (int k = 0; k < ExpendedGrid.GetLength(0); k++)
                {
                    if (ExpendedGrid[k, i]) ColisionMapTxt += "-";
                    if (!ExpendedGrid[k, i]) ColisionMapTxt += "X";
                }
                ColisionMapTxt += Environment.NewLine;
            }
            File.Create(Form1_0.ThisEndPath + "CollisionMap.txt").Dispose();
            File.WriteAllText(Form1_0.ThisEndPath + "CollisionMap.txt", ColisionMapTxt);*/

            return ExpendedGrid;
        }

        public List<Point> FindPath(Point startPos, Point targetPos)
        {
            int width = ThisCollisionGrid.GetLength(0);
            int height = ThisCollisionGrid.GetLength(1);

            // Add some padding to the origin/destination, sometimes when the origin or destination are close to a non-walkable
            // area, pather is not able to calculate the path, so we add some padding around origin/dest to avoid this
            // If character can not teleport if apply this hacky thing it will try to kill monsters across walls
            /*if (CharConfig.UseTeleport && !Form1_0.Town_0.IsInTown)
            {
                for (int i = -3; i < 4; i++)
                {
                    for (int k = -3; k < 4; k++)
                    {
                        if (i == 0 && k == 0) continue;
                        ThisCollisionGrid[i + targetPos.X, k + targetPos.Y] = true;
                    }
                }
                for (int i = -3; i < 4; i++)
                {
                    for (int k = -3; k < 4; k++)
                    {
                        if (i == 0 && k == 0) continue;
                        ThisCollisionGrid[i + startPos.X, k + startPos.Y] = true;
                    }
                }
            }*/

            bool[,] closedSet = new bool[width, height];
            List<Node> openSet = new List<Node>();

            openSet.Add(new Node(startPos, null, 0, Heuristic(startPos, targetPos)));

            try
            {
                while (openSet.Any())
                {
                    Node current = openSet.OrderBy(node => node.F).First();

                    if (current.Position.Equals(targetPos)) return ReconstructPath(current);

                    openSet.Remove(current);
                    closedSet[current.Position.X, current.Position.Y] = true;

                    for (int p = 0; p < 8; p++) // Assuming 4-direction movement
                    {
                        int nx = current.Position.X + dx[p];
                        int ny = current.Position.Y + dy[p];

                        if (nx >= 0 && nx < width && ny >= 0 && ny < height)
                        {
                            if (nx < ThisCollisionGrid.GetLength(0) && ny < ThisCollisionGrid.GetLength(1)
                                && nx < closedSet.GetLength(0) && ny < closedSet.GetLength(1))
                            {
                                // Check if teleportation is possible
                                /*bool canTeleport = CanTeleport(current.Position, new Point(nx, ny));
                                if (canTeleport)
                                {
                                    Point teleportTarget = new Point(nx, ny);
                                    double teleportCost = Heuristic(current.Position, teleportTarget);
                                    double tentativeG = current.G + teleportCost;

                                    Node teleportNode = new Node(teleportTarget, current, tentativeG, Heuristic(teleportTarget, targetPos));
                                    openSet.Add(teleportNode);
                                    continue; // Skip regular movement check
                                }*/

                                /*if (!ThisCollisionGrid[nx, ny])
                                {
                                    bool canTeleportTo = CanTeleport(new Point(nx, ny));
                                    if (canTeleportTo)
                                    {
                                        ThisCollisionGrid[nx,ny] = true;
                                        nx = TeleportPoint.X;
                                        ny = TeleportPoint.Y;

                                        //#########################################
                                        /*Point teleportTarget = new Point(nx, ny);
                                        //if (closedSet[nx, ny]) continue; //#####

                                        double teleportCost = Heuristic(current.Position, teleportTarget);
                                        double tentativeG = current.G + teleportCost;

                                        Node teleportNode = new Node(teleportTarget, current, tentativeG, Heuristic(teleportTarget, targetPos));
                                        openSet.Add(teleportNode);
                                        continue; // Skip regular movement check*/
                                //#########################################
                                /*
                                        //if (ny >= 338 && ny <= 348) Console.WriteLine("Can tel from: " + current.Position.X + ", " + current.Position.Y + " to: " + nx + ", " + ny);
                                    }
                                }*/

                                //if (ThisCollisionGrid[nx, ny] && !closedSet[nx, ny])
                                if (ThisCollisionGrid[nx, ny])
                                {
                                    Point neighborPos = new Point(nx, ny);
                                    //Point neighborPos = new Point(nxTeleport, nyTeleport);

                                    if (closedSet[nx, ny]) continue;
                                    //if (closedSet[nxTeleport, nyTeleport]) continue;

                                    double tentativeG = current.G + 1;
                                    if (p > 3) tentativeG += 1;

                                    Node neighbor = openSet.FirstOrDefault(node => node.Position.Equals(neighborPos));
                                    if (neighbor == null || tentativeG < neighbor.G)
                                    {
                                        if (neighbor == null)
                                        {
                                            neighbor = new Node(neighborPos);
                                            openSet.Add(neighbor);
                                        }

                                        neighbor.Parent = current;
                                        neighbor.G = tentativeG;
                                        neighbor.H = Heuristic(neighborPos, targetPos);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
                Form1_0.method_1("Issue with PathFinding CollisionGrid 'Indexes'!", Color.Red);
            }

            // No path found
            return null;
        }

        public bool IsCloseToLocation(Position ThissP, int AcceptOffset)
        {
            bool MovedCorrectly = false;
            if (Form1_0.PlayerScan_0.xPosFinal >= (ThissP.X - AcceptOffset)
                    && Form1_0.PlayerScan_0.xPosFinal <= (ThissP.X + AcceptOffset)
                    && Form1_0.PlayerScan_0.yPosFinal >= (ThissP.Y - AcceptOffset)
                    && Form1_0.PlayerScan_0.yPosFinal <= (ThissP.Y + AcceptOffset))
            {
                MovedCorrectly = true;
            }
            return MovedCorrectly;
        }

        public Point TeleportPoint = new Point(0, 0);

        private bool CanTeleport(Point next)
        {
            /*for (int dx = -TeleportAcceptSize; dx <= TeleportAcceptSize; dx++)
            {
                for (int dy = -TeleportAcceptSize; dy <= TeleportAcceptSize; dy++)
                {
                    int nx = current.X + dx;
                    int ny = current.Y + dy;

                    if (nx >= 0 && nx < ThisCollisionGrid.GetLength(0) && ny >= 0 && ny < ThisCollisionGrid.GetLength(1))
                    {
                        double distance = Math.Sqrt(dx * dx + dy * dy);
                        if (distance <= TeleportAcceptSize && ThisCollisionGrid[nx, ny])
                        {
                            TeleportPoint.X = nx;
                            TeleportPoint.Y = ny;
                            return true;
                        }
                    }
                }
            }
            return false;*/

            for (int dir = 0; dir < 8; dir++)
            {
                for (int i = TeleportAcceptSize - 1; i > 0; i--)
                {
                    int XAdder = 0;
                    int YAdder = 0;
                    if (dir == 0) XAdder = i;
                    if (dir == 1) XAdder = -i;
                    if (dir == 2) YAdder = i;
                    if (dir == 3) YAdder = -i;
                    if (dir == 4) { XAdder = i; YAdder = i; }
                    if (dir == 5) { XAdder = -i; YAdder = -i; }
                    if (dir == 6) { XAdder = i; YAdder = -i; }
                    if (dir == 7) { XAdder = -i; YAdder = i; }

                    if (ThisCollisionGrid[next.X + XAdder, next.Y + YAdder])
                    {
                        // Check if the target point is walkable and within teleportSize range
                        //return IsWalkable(next) && Math.Abs(next.X - current.X) <= TeleportAcceptSize && Math.Abs(next.Y - current.Y) <= TeleportAcceptSize;
                        //return IsWalkable(next) && (Math.Abs(next.X - current.X) <= TeleportAcceptSize || Math.Abs(next.Y - current.Y) <= TeleportAcceptSize);

                        //double distance = Math.Sqrt(Math.Pow(next.X - current.X, 2) + Math.Pow(next.Y - current.Y, 2));
                        //return IsWalkable(next) && distance <= TeleportAcceptSize;

                        TeleportPoint.X = next.X + XAdder;
                        TeleportPoint.Y = next.Y + YAdder;
                        return true;
                        //return IsWalkable(next);
                    }
                }
            }
            return false;
        }

        private bool IsWalkable(Point point)
        {
            return point.X >= 0 && point.X < ThisCollisionGrid.GetLength(0) && point.Y >= 0 && point.Y < ThisCollisionGrid.GetLength(1) && ThisCollisionGrid[point.X, point.Y];
        }

        private double Heuristic(Point a, Point b)
        {
            return Math.Sqrt(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2));
        }

        private List<Point> ReconstructPath(Node node)
        {
            List<Point> path = new List<Point>();

            while (node != null)
            {
                path.Add(node.Position);
                node = node.Parent;
            }

            path.Reverse();
            return path;
        }

        private class Node
        {
            public Point Position { get; }
            public Node Parent { get; set; }
            public double G { get; set; } // g score: cost from start to current
            public double H { get; set; } // h score: estimated cost from current to target
            public double F => G + H; // f score: total estimated cost

            public Node(Point position, Node parent = null, double g = 0, double h = 0)
            {
                Position = position;
                Parent = parent;
                G = g;
                H = h;
            }
        }

        public struct Point
        {
            public int X { get; set; }
            public int Y { get; set; }

            public Point(int x, int y)
            {
                X = x;
                Y = y;
            }

            public override bool Equals(object obj)
            {
                if (!(obj is Point))
                    return false;

                var other = (Point)obj;
                return X == other.X && Y == other.Y;
            }

            public override int GetHashCode()
            {
                return X.GetHashCode() ^ Y.GetHashCode();
            }
        }
    }
    
}
