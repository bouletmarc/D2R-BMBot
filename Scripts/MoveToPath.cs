using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static app.Enums;
using static app.MapAreaStruc;
using static app.MoveToPath;
using System.ComponentModel;
using static System.Windows.Forms.AxHost;
using System.Security.Cryptography;
using static System.Net.WebRequestMethods;

namespace app
{

    public class MoveToPath
    {
        Form1 Form1_0;

        public int ThisPlayerAreaID = 0;
        public Position LastMovingLocation = new Position { X = 0, Y = 0 };
        public Position ThisFinalPosition = new Position { X = 0, Y = 0 };
        //public int MoveTry = 0;

        public void SetForm1(Form1 form1_1)
        {
            Form1_0 = form1_1;
        }

        public void MoveToArea(Area ThisID)
        {
            if (Form1_0.PlayerScan_0.levelNo == 0) Form1_0.PlayerScan_0.GetPositions();

            ThisPlayerAreaID = (int)Form1_0.PlayerScan_0.levelNo;
            ThisFinalPosition = Form1_0.MapAreaStruc_0.GetPositionOfObject("exit", Form1_0.Town_0.getAreaName((int) ThisID), ThisPlayerAreaID - 1, new List<int>() { });

            //Console.WriteLine("Going to Pos: " + ThisFinalPosition.X + ", " + ThisFinalPosition.Y);
            MoveToArea((int) ThisID);
        }

        public void MoveToNPC(string NPCName)
        {
            if (Form1_0.PlayerScan_0.levelNo == 0) Form1_0.PlayerScan_0.GetPositions();

            ThisPlayerAreaID = (int)Form1_0.PlayerScan_0.levelNo;
            ThisFinalPosition = Form1_0.MapAreaStruc_0.GetPositionOfObject("npc", NPCName, ThisPlayerAreaID - 1, new List<int>() { });

            MoveToArea((int)ThisPlayerAreaID);
        }

        public void MoveToThisPos(Position ThisPositionn)
        {
            if (Form1_0.PlayerScan_0.levelNo == 0) Form1_0.PlayerScan_0.GetPositions();

            ThisPlayerAreaID = (int)Form1_0.PlayerScan_0.levelNo;
            ThisFinalPosition = ThisPositionn;

            MoveToArea((int)ThisPlayerAreaID);
        }

        public void MoveToArea(int ThisID)
        {
            ThisPlayerAreaID = (int)Form1_0.PlayerScan_0.levelNo;
            //Console.WriteLine("Going to Pos: " + ThisFinalPosition.X + ", " + ThisFinalPosition.Y);

            List<Room> path = FindShortestPath(Form1_0.MapAreaStruc_0.AllMapData[ThisPlayerAreaID - 1].Rooms, Form1_0.PlayerScan_0.xPos, Form1_0.PlayerScan_0.yPos, ThisFinalPosition.X, ThisFinalPosition.Y);
            bool IsMovingThruPath = false;
            int CurrentPathIndex = 0;
            if (path != null)
            {
                IsMovingThruPath = true;
                while (IsMovingThruPath)
                {
                    //Console.WriteLine("Pos test: " + path[CurrentPathIndex].X + ", " + path[CurrentPathIndex].Y);
                    if (Form1_0.Mover_0.MoveToLocation(path[CurrentPathIndex].X + (path[CurrentPathIndex].Width / 2), path[CurrentPathIndex].Y + (path[CurrentPathIndex].Height / 2), false, false))
                    {
                        CurrentPathIndex++;
                        if (CurrentPathIndex >= path.Count - 1)
                        {
                            IsMovingThruPath = false;
                        }
                    }
                    else
                    {
                        if (path[CurrentPathIndex].X != LastMovingLocation.X || path[CurrentPathIndex].Y != LastMovingLocation.Y)
                        {
                            LastMovingLocation.X = path[CurrentPathIndex].X;
                            LastMovingLocation.Y = path[CurrentPathIndex].Y;
                            //MoveTry = 0;
                        }
                        else
                        {
                            //MoveTry++;
                            //if (MoveTry > 5)
                            //{
                                CurrentPathIndex++;
                                if (CurrentPathIndex >= path.Count - 1)
                                {
                                    IsMovingThruPath = false;
                                }
                            //}
                        }
                    }
                }

                Form1_0.Mover_0.MoveToLocation(ThisFinalPosition.X, ThisFinalPosition.Y);

                int tryyy = 0;
                while (Form1_0.PlayerScan_0.levelNo == ThisPlayerAreaID && tryyy <= 25)
                {
                    Dictionary<string, int> itemScreenPos = Form1_0.GameStruc_0.World2Screen(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, ThisFinalPosition.X, ThisFinalPosition.Y);
                    Form1_0.KeyMouse_0.MouseClicc(itemScreenPos["x"], itemScreenPos["y"]);
                    Form1_0.PlayerScan_0.GetPositions();
                    tryyy++;
                }

                /*Console.WriteLine("Shortest path found:");
                foreach (Room room in path)
                {
                    Console.WriteLine($"Room: ({room.X}, {room.Y}) - ({room.Width}, {room.Height})");
                }*/
            }
            else
            {
                Form1_0.method_1("No path found.", Color.Red);
            }
        }


        public List<Room> FindShortestPath(List<Room> rooms, int startX, int startY, int destX, int destY)
        {
            List<Room> path = new List<Room>();
            Queue<Room> queue = new Queue<Room>();
            HashSet<Room> visited = new HashSet<Room>();
            Dictionary<Room, Room> parent = new Dictionary<Room, Room>();

            Room startRoom = rooms.FirstOrDefault(r => r.Contain(startX, startY));
            Room destRoom = rooms.FirstOrDefault(r => r.Contain(destX, destY));

            List<Room> roomsOut = rooms;
            if (destRoom == null)
            {
                destRoom = Form1_0.MapAreaStruc_0.AllMapData[ThisPlayerAreaID - 2].Rooms.FirstOrDefault(r => r.Contain(destX, destY));
                roomsOut = Form1_0.MapAreaStruc_0.AllMapData[ThisPlayerAreaID - 2].Rooms;
            }
            if (destRoom == null)
            {
                destRoom = Form1_0.MapAreaStruc_0.AllMapData[ThisPlayerAreaID].Rooms.FirstOrDefault(r => r.Contain(destX, destY));
                roomsOut = Form1_0.MapAreaStruc_0.AllMapData[ThisPlayerAreaID].Rooms;
            }

            //Console.WriteLine("Start Room: " + startRoom.X + ", " + startRoom.Y);
            //Console.WriteLine("Dest Room: " + destRoom.X + ", " + destRoom.Y);

            if (startRoom == null || destRoom == null) return null;

            queue.Enqueue(startRoom);
            visited.Add(startRoom);
            parent[startRoom] = null;

            while (queue.Count > 0)
            {
                Room currentRoom = queue.Dequeue();
                if (currentRoom == destRoom)
                {
                    //Console.WriteLine("Path found...");
                    // Reconstruct the path
                    Room node = currentRoom;
                    while (node != null)
                    {
                        path.Insert(0, node);
                        node = parent[node];
                    }
                    return path;
                }

                foreach (Room neighbor in GetNeighbors(currentRoom, rooms, roomsOut))
                {
                    if (!visited.Contains(neighbor))
                    {
                        queue.Enqueue(neighbor);
                        visited.Add(neighbor);
                        parent[neighbor] = currentRoom;
                    }
                }
            }

            return null; // No path found
        }

        public static IEnumerable<Room> GetNeighbors(Room room, List<Room> rooms, List<Room> roomsOut)
        {
            if (rooms != roomsOut)
            {
                foreach (Room otherRoom in roomsOut)
                {
                    if (room != otherRoom && IsNeighbor(room, otherRoom))
                    {
                        yield return otherRoom;
                    }
                }

                foreach (Room otherRoom in rooms)
                {
                    if (room != otherRoom && IsNeighbor(room, otherRoom))
                    {
                        yield return otherRoom;
                    }
                }
            }
            else
            {
                foreach (Room otherRoom in rooms)
                {
                    //Console.WriteLine("Neighbor test: " + otherRoom.X + ", " + otherRoom.Y + " neighbor of: " + room.X + ", " + room.Y + " | " + IsNeighbor(room, otherRoom));

                    if (room != otherRoom && IsNeighbor(room, otherRoom))
                    {
                        //Console.WriteLine("Room: " + otherRoom.X + ", " + otherRoom.Y + " neighbor of: " + room.X + ", " + room.Y);
                        yield return otherRoom;
                    }
                }
            }
        }

        public static bool IsNeighbor(Room room1, Room room2)
        {
            //Neighbor test: 4640, 5400 neighbor of: 4640, 5440 | False
            // Check if room2 is a neighbor of room1 based on position and width
            int neighborX = room1.X - room2.Width;
            int neighborY = room1.Y - room2.Height; // Assuming rooms are aligned vertically

            bool IsNeighbor = false;
            if (room1.X < room2.X && room2.X == (room1.X + room1.Width)
                && room1.Y == room2.Y) IsNeighbor = true;
            if (room1.X > room2.X && room2.X == (room1.X - room2.Width)
                && room1.Y == room2.Y) IsNeighbor = true;

            if (room1.Y < room2.Y && room2.Y == (room1.Y + room1.Height)
                && room1.X == room2.X) IsNeighbor = true;
            if (room1.Y > room2.Y && room2.Y == (room1.Y - room2.Height)
                && room1.X == room2.X) IsNeighbor = true;

            /*if (room1.X < room2.X && room2.X == (room1.X + room1.Width)
                && room1.Y > room2.Y && room2.Y == (room1.Y - room2.Height)) IsNeighbor = true;

            if (room1.X > room2.X && room2.X == (room1.X - room2.Width)
                && room1.Y < room2.Y && room2.Y == (room1.Y + room1.Height)) IsNeighbor = true;

            if (room1.X > room2.X && room2.X == (room1.X - room2.Width)
                && room1.Y > room2.Y && room2.Y == (room1.Y - room2.Height)) IsNeighbor = true;
            */

            return IsNeighbor;
        }

    }
}