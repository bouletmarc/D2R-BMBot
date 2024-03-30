using Newtonsoft.Json;
using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static app.Enums;
using static app.MapAreaStruc;

namespace app
{
    public class MapAreaStruc
    {
        Form1 Form1_0;

        public List<ServerLevel> AllMapData = new List<ServerLevel>();
        public int CurrentObjectIndex = 0;
        public int CurrentObjectAreaIndex = 0;

        public void SetForm1(Form1 form1_1)
        {
            Form1_0 = form1_1;

            _kooloMapPath = Application.StartupPath + @"\map.exe";
        }

        public Position GetAreaOfObject(string ObjectType, string ObjectName, List<int> IgnoreTheseIndex, int StartAreaIndexToSearch = 0, int EndAreaIndexToSearch = 1)
        {
            Position ThisPos = new Position();
            ThisPos.X = 0;
            ThisPos.Y = 0;

            if (StartAreaIndexToSearch == 0 && EndAreaIndexToSearch == 1)
            {
                EndAreaIndexToSearch = AllMapData.Count;
            }

            try
            {
                //ExitType = "exit" or "exit_area"

                CurrentObjectIndex = 0;
                CurrentObjectAreaIndex = 0;

                for (int i = StartAreaIndexToSearch; i < EndAreaIndexToSearch; i++)
                {
                    //if (AllMapData[i].Objects.Count == 0) ScanMapStruc();
                    for (int k = 0; k < AllMapData[i].Objects.Count; k++)
                    {
                        if (!AvoidThisIndex(k, IgnoreTheseIndex))
                        {
                            if (AllMapData[i].Objects[k].Type == "exit" && ObjectType == "exit")
                            {
                                if (Form1_0.Town_0.getAreaName(int.Parse(AllMapData[i].Objects[k].ID)) == ObjectName)
                                {
                                    ThisPos.X = AllMapData[i].Offset.X + AllMapData[i].Objects[k].X;
                                    ThisPos.Y = AllMapData[i].Offset.Y + AllMapData[i].Objects[k].Y;
                                    CurrentObjectIndex = k;
                                    CurrentObjectAreaIndex = i;
                                }
                            }
                            if (AllMapData[i].Objects[k].Type == "exit_area" && ObjectType == "exit_area")
                            {
                                if (Form1_0.Town_0.getAreaName(int.Parse(AllMapData[i].Objects[k].ID)) == ObjectName)
                                {
                                    ThisPos.X = AllMapData[i].Offset.X + AllMapData[i].Objects[k].X;
                                    ThisPos.Y = AllMapData[i].Offset.Y + AllMapData[i].Objects[k].Y;
                                    CurrentObjectIndex = k;
                                    CurrentObjectAreaIndex = i;
                                }
                            }
                            if (AllMapData[i].Objects[k].Type == "object" && ObjectType == "object")
                            {
                                //Console.WriteLine(Form1_0.ObjectsStruc_0.getObjectName(int.Parse(AllMapData[i].Objects[k].ID)));
                                if (Form1_0.ObjectsStruc_0.getObjectName(int.Parse(AllMapData[i].Objects[k].ID)) == ObjectName)
                                {
                                    ThisPos.X = AllMapData[i].Offset.X + AllMapData[i].Objects[k].X;
                                    ThisPos.Y = AllMapData[i].Offset.Y + AllMapData[i].Objects[k].Y;
                                    CurrentObjectIndex = k;
                                    CurrentObjectAreaIndex = i;
                                }
                            }
                            if (AllMapData[i].Objects[k].Type == "npc" && ObjectType == "npc")
                            {
                                if (Form1_0.NPCStruc_0.getNPC_ID(int.Parse(AllMapData[i].Objects[k].ID)) == ObjectName)
                                {
                                    ThisPos.X = AllMapData[i].Offset.X + AllMapData[i].Objects[k].X;
                                    ThisPos.Y = AllMapData[i].Offset.Y + AllMapData[i].Objects[k].Y;
                                    CurrentObjectIndex = k;
                                    CurrentObjectAreaIndex = i;
                                }
                            }
                        }
                    }
                }

                //Form1_0.method_1("Object: " + ExitName + " found at: "+ ThisPos.X + ", " + ThisPos.Y, Color.Red);

            }
            catch { }
            return ThisPos;
        }

        public Position GetPositionOfObject(string ObjectType, string ObjectName, int AreaID, List<int> IgnoreTheseIndex)
        {
            Position ThisPos = new Position();
            ThisPos.X = 0;
            ThisPos.Y = 0;

            try
            {
                //ExitType = "exit" or "exit_area"

                CurrentObjectIndex = 0;
                CurrentObjectAreaIndex = 0;

                //for (int i = 0; i < AllMapData.Count; i++)
                //{
                int i = AreaID - 1;
                if (AllMapData[i].Objects.Count == 0) ScanMapStruc();
                for (int k = 0; k < AllMapData[i].Objects.Count; k++)
                {
                    if (!AvoidThisIndex(k, IgnoreTheseIndex))
                    {
                        if (AllMapData[i].Objects[k].Type == "exit" && ObjectType == "exit")
                        {
                            if (Form1_0.Town_0.getAreaName(int.Parse(AllMapData[i].Objects[k].ID)) == ObjectName)
                            {
                                ThisPos.X = AllMapData[i].Offset.X + AllMapData[i].Objects[k].X;
                                ThisPos.Y = AllMapData[i].Offset.Y + AllMapData[i].Objects[k].Y;
                                CurrentObjectIndex = k;
                                CurrentObjectAreaIndex = i;
                            }
                        }
                        if (AllMapData[i].Objects[k].Type == "exit_area" && ObjectType == "exit_area")
                        {
                            if (Form1_0.Town_0.getAreaName(int.Parse(AllMapData[i].Objects[k].ID)) == ObjectName)
                            {
                                ThisPos.X = AllMapData[i].Offset.X + AllMapData[i].Objects[k].X;
                                ThisPos.Y = AllMapData[i].Offset.Y + AllMapData[i].Objects[k].Y;
                                CurrentObjectIndex = k;
                                CurrentObjectAreaIndex = i;
                            }
                        }
                        if (AllMapData[i].Objects[k].Type == "object" && ObjectType == "object")
                        {
                            //Console.WriteLine(Form1_0.ObjectsStruc_0.getObjectName(int.Parse(AllMapData[i].Objects[k].ID)));
                            if (Form1_0.ObjectsStruc_0.getObjectName(int.Parse(AllMapData[i].Objects[k].ID)) == ObjectName)
                            {
                                ThisPos.X = AllMapData[i].Offset.X + AllMapData[i].Objects[k].X;
                                ThisPos.Y = AllMapData[i].Offset.Y + AllMapData[i].Objects[k].Y;
                                CurrentObjectIndex = k;
                                CurrentObjectAreaIndex = i;
                            }
                        }
                        if (AllMapData[i].Objects[k].Type == "npc" && ObjectType == "npc")
                        {
                            if (Form1_0.NPCStruc_0.getNPC_ID(int.Parse(AllMapData[i].Objects[k].ID)) == ObjectName)
                            {
                                ThisPos.X = AllMapData[i].Offset.X + AllMapData[i].Objects[k].X;
                                ThisPos.Y = AllMapData[i].Offset.Y + AllMapData[i].Objects[k].Y;
                                CurrentObjectIndex = k;
                                CurrentObjectAreaIndex = i;
                            }
                        }
                    }
                }
                //}

                //Form1_0.method_1("Object: " + ExitName + " found at: "+ ThisPos.X + ", " + ThisPos.Y, Color.Red);

            }
            catch { }
            return ThisPos;
        }

        public (LevelData, bool) LevelDataForCoords(Position p, int act)
        {
            foreach (var lvl in AllMapData)
            {
                var lvlMaxX = lvl.Offset.X + lvl.Size.Width;
                var lvlMaxY = lvl.Offset.Y + lvl.Size.Height;

                //Console.WriteLine("Act: " + act + " | LVL ID: " + lvl.ID + " " + SameAsTownAct(act, lvl.ID));
                if (SameAsTownAct(act, lvl.ID) && lvl.Offset.X <= p.X && p.X <= lvlMaxX && lvl.Offset.Y <= p.Y && p.Y <= lvlMaxY)
                {
                    return (new LevelData
                    {
                        Area = lvl.ID,
                        Name = lvl.Name,
                        Offset = new Position
                        {
                            X = lvl.Offset.X,
                            Y = lvl.Offset.Y
                        },
                        Size = new Position
                        {
                            X = lvl.Size.Width,
                            Y = lvl.Size.Height
                        },
                        CollisionGrid = CollisionGrid((Area)lvl.ID)
                    }, true);
                }
            }

            return (new LevelData(), false);
        }

        public int GetPlayerAct()
        {
            int TownAct = 0;
            if (Form1_0.PlayerScan_0.levelNo >= 1 && Form1_0.PlayerScan_0.levelNo < 40) TownAct = 1;
            if (Form1_0.PlayerScan_0.levelNo >= 40 && Form1_0.PlayerScan_0.levelNo < 75) TownAct = 2;
            if (Form1_0.PlayerScan_0.levelNo >= 75 && Form1_0.PlayerScan_0.levelNo < 103) TownAct = 3;
            if (Form1_0.PlayerScan_0.levelNo >= 103 && Form1_0.PlayerScan_0.levelNo < 109) TownAct = 4;
            if (Form1_0.PlayerScan_0.levelNo >= 109) TownAct = 5;

            return TownAct;
        }

        public bool SameAsTownAct(int ThisAct, int ThisMapID)
        {
            int TownAct = 0;
            if (ThisMapID >= 1 && ThisMapID < 40) TownAct = 1;
            if (ThisMapID >= 40 && ThisMapID < 75) TownAct = 2;
            if (ThisMapID >= 75 && ThisMapID < 103) TownAct = 3;
            if (ThisMapID >= 103 && ThisMapID < 109) TownAct = 4;
            if (ThisMapID >= 109) TownAct = 5;

            if (TownAct == ThisAct) return true;
            return false;
        }

        public bool AvoidThisIndex(int ThisIndex, List<int> AllIndexToAvoidCheck)
        {
            for (int i = 0; i < AllIndexToAvoidCheck.Count; i++)
            {
                if (AllIndexToAvoidCheck[i] == ThisIndex) return true;
            }
            return false;
        }

        public void ScanMapStruc()
        {
            _d2LoDPath = Form1_0.D2_LOD_113C_Path;

            Form1_0.method_1("Seed: " + Form1_0.PlayerScan_0.mapSeedValue.ToString(), Color.DarkBlue);
            Form1_0.method_1("Difficulty: " + ((Difficulty)Form1_0.PlayerScan_0.difficulty).ToString(), Color.DarkBlue);

            GetMapData(Form1_0.PlayerScan_0.mapSeedValue.ToString(), (Difficulty)Form1_0.PlayerScan_0.difficulty);
        }

        public string _kooloMapPath;
        public string _d2LoDPath;

        public void GetMapData(string seed, Difficulty difficulty)
        {
            Console.WriteLine(_d2LoDPath);

            var procStartInfo = new ProcessStartInfo
            {
                FileName = _kooloMapPath,
                Arguments = $"{_d2LoDPath} -s {seed} -d {GetDifficultyAsNum(difficulty)}",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (var process = Process.Start(procStartInfo))
            {
                if (process == null)
                    throw new Exception("Failed to start the process.");

                var lvls = new List<ServerLevel>();
                ServerLevel currentLevel = null;

                //#########
                var stdout = process.StandardOutput.ReadToEnd();
                var stdoutLines = stdout.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var line in stdoutLines)
                {
                    try
                    {
                        //Form1_0.method_1(line, Color.Red);
                        if (JsonConvert.DeserializeObject<ServerLevel>(line) is ServerLevel lvl && !string.IsNullOrEmpty(lvl.Type) && lvl.Map.Any())
                        {
                            lvls.Add(lvl);
                        }
                    }
                    catch { }
                }
                //#########

                process.WaitForExit();


                string SavePathh = Form1_0.ThisEndPath + "DumpMap.txt";
                File.Create(SavePathh).Dispose();
                File.WriteAllLines(SavePathh, stdoutLines);

                /*if (process.ExitCode != 0)
                {
                    throw new Exception($"Error detected fetching Map Data from Diablo II: LoD 1.13c game, please make sure you have the classic expansion game installed AND config.yaml D2LoDPath is pointing to the correct game path. Error code: {process.ExitCode}");
                }*/

                //Form1_0.method_1("Count: " + lvls.Count, Color.Red);
                for (int i = 0; i < lvls.Count; i++)
                {
                    /*Form1_0.method_1("Name: " + lvls[i].Name + " (ID: " + lvls[i].ID + ")", Color.Red);
                    Form1_0.method_1("Size: " + lvls[i].Size.Width + ", " + lvls[i].Size.Height, Color.Red);
                    Form1_0.method_1("Offset: " + lvls[i].Offset.X + ", " + lvls[i].Offset.Y, Color.Red);

                    Form1_0.method_1("Map: " + lvls[i].Map.Count.ToString(), Color.Red);
                    Form1_0.method_1("Rooms: " + lvls[i].Rooms.Count.ToString(), Color.Red);*/

                    for (int k = 0; k < lvls[i].Map.Count; k++)
                    {
                        /*for (int m = 0; m < lvls[i].Map[k].Count; m++)
                        {
                            Form1_0.method_1("Map: " + lvls[i].Map[k][m].ToString(), Color.Red);
                        }*/
                        //Form1_0.method_1("Map: " + lvls[i].Map[k].Count.ToString(), Color.Red);
                    }
                    //for (int k = 0; k < lvls[i].Objects.Count; k++) Form1_0.method_1("Objects: " + lvls[i].Objects[k].Name + "(" + lvls[i].Objects[k].X + ", " + lvls[i].Objects[k].Y + ")", Color.Red);
                    for (int k = 0; k < lvls[i].Objects.Count; k++)
                    {
                        /*if (lvls[i].Objects[k].Type == "exit")
                        {
                            Form1_0.method_1("Exit: " + Form1_0.Town_0.getAreaName(int.Parse(lvls[i].Objects[k].ID)) + "(" + lvls[i].Objects[k].X + ", " + lvls[i].Objects[k].Y + ")", Color.Red);
                        }*/
                        /*if (lvls[i].Objects[k].Type == "exit_area")
                        {
                            Form1_0.method_1("Exit_area: " + Form1_0.Town_0.getAreaName(int.Parse(lvls[i].Objects[k].ID)) + "(" + lvls[i].Objects[k].X + ", " + lvls[i].Objects[k].Y + ")", Color.Red);
                        }*/
                        /*if (lvls[i].Objects[k].Type == "object")
                        {
                            Form1_0.method_1("Object: " + Form1_0.ObjectsStruc_0.getObjectName(int.Parse(lvls[i].Objects[k].ID)) + "(" + lvls[i].Objects[k].X + ", " + lvls[i].Objects[k].Y + ")", Color.Red);
                        }*/
                        /*if (lvls[i].Objects[k].Type == "npc")
                        {
                            Form1_0.method_1("NPC: " + Form1_0.Town_0.getAreaName(int.Parse(lvls[i].Objects[k].ID)) + "(" + lvls[i].Objects[k].X + ", " + lvls[i].Objects[k].Y + ")", Color.Red);
                        }*/
                    }
                    //for (int k = 0; k < lvls[i].Rooms.Count; k++) Form1_0.method_1("Rooms: " + lvls[i].Rooms[k].X + ", " + lvls[i].Rooms[k].Y + "(Size: " + lvls[i].Rooms[k].Width + ", " + lvls[i].Rooms[k].Height + ")", Color.Red);

                    //Form1_0.method_1("--------------------------", Color.Red);
                }

                AllMapData = lvls;
            }
        }

        public (List<NPC>, List<Level>, List<ObjectS>, List<Room>) NPCsExitsAndObjects(Position areaOrigin, Area a)
        {
            var npcs = new List<NPC>();
            var exits = new List<Level>();
            var objects = new List<ObjectS>();
            var rooms = new List<Room>();

            ServerLevel level = GetLevel(a);

            foreach (var r in level.Rooms)
            {
                rooms.Add(new Room
                {
                    X = r.X,
                    Y = r.Y,
                    Width = r.Width,
                    Height = r.Height
                });
            }

            foreach (var obj in level.Objects)
            {
                switch (obj.Type)
                {
                    case "npc":
                        var n = new NPC
                        {
                            ID = obj.ID,
                            Name = obj.Name,
                            X = obj.X + areaOrigin.X,
                            Y = obj.Y + areaOrigin.Y
                        };
                        npcs.Add(n);
                        break;
                    case "exit":
                        var lvl = new Level
                        {
                            Area = int.Parse(obj.ID),
                            //X = obj.X + areaOrigin.X,
                            //Y = obj.Y + areaOrigin.Y,
                            Position = new Position
                            {
                                X = obj.X + areaOrigin.X,
                                Y = obj.Y + areaOrigin.Y
                            },
                            IsEntrance = true
                        };
                        exits.Add(lvl);
                        break;
                    case "object":
                        var o = new ObjectS
                        {
                            Name = obj.Name,
                            //Name = (object.Name)obj.ID,
                            //X = obj.X + areaOrigin.X,
                            //Y = obj.Y + areaOrigin.Y
                            Position = new Position
                            {
                                X = obj.X + areaOrigin.X,
                                Y = obj.Y + areaOrigin.Y
                            }
                        };
                        objects.Add(o);
                        break;
                }
            }

            foreach (var obj in level.Objects)
            {
                switch (obj.Type)
                {
                    case "exit_area":
                        bool found = false;
                        foreach (var exit in exits)
                        {
                            if (exit.Area == int.Parse(obj.ID))
                            {
                                exit.IsEntrance = false;
                                found = true;
                                break;
                            }
                        }

                        if (!found)
                        {
                            var lvl = new Level
                            {
                                Area = int.Parse(obj.ID),
                                //X = obj.X + areaOrigin.X,
                                //Y = obj.Y + areaOrigin.Y,
                                Position = new Position
                                {
                                    X = obj.X + areaOrigin.X,
                                    Y = obj.Y + areaOrigin.Y
                                },
                                IsEntrance = false
                            };
                            exits.Add(lvl);
                        }
                        break;
                }
            }

            return (npcs, exits, objects, rooms);
        }

        private string GetDifficultyAsNum(Difficulty df)
        {
            switch (df)
            {
                case Difficulty.Normal:
                    return "0";
                case Difficulty.Nightmare:
                    return "1";
                case Difficulty.Hell:
                    return "2";
                default:
                    return "0";
            }
        }

        public List<List<bool>> CollisionGrid(Area area)
        {
            ServerLevel level = GetLevel(area);

            List<List<bool>> cg = new List<List<bool>>();

            for (int y = 0; y < level.Size.Height; y++)
            {
                List<bool> row = new List<bool>();
                for (int x = 0; x < level.Size.Width; x++)
                {
                    row.Add(false);
                }

                // Documentation about how this works: https://github.com/blacha/diablo2/tree/master/packages/map
                if (level.Map.Count > y)
                {
                    List<int> mapRow = level.Map[y];
                    bool isWalkable = false;
                    int xPos = 0;
                    foreach (int xs in mapRow)
                    {
                        if (xs != 0)
                        {
                            for (int xOffset = 0; xOffset < xs; xOffset++)
                            {
                                row[xPos + xOffset] = isWalkable;
                            }
                        }
                        isWalkable = !isWalkable;
                        xPos += xs;
                    }
                    while (xPos < row.Count)
                    {
                        row[xPos] = isWalkable;
                        xPos++;
                    }
                }

                cg.Add(row);
            }

            return cg;
            //return cg.Select(r => r.ToArray()).ToArray();
        }

        public Position Origin(Area area)
        {
            var level = GetLevel(area);

            return new Position
            {
                X = level.Offset.X,
                Y = level.Offset.Y
            };
        }

        public (LevelData, bool) GetLevelData(Area id)
        {
            foreach (var lvl in AllMapData)
            {
                if (lvl.ID == (int)id)
                {
                    return (new LevelData
                    {
                        Area = lvl.ID,
                        Name = lvl.Name,
                        Offset = new Position
                        {
                            X = lvl.Offset.X,
                            Y = lvl.Offset.Y
                        },
                        Size = new Position
                        {
                            X = lvl.Size.Width,
                            Y = lvl.Size.Height
                        },
                        CollisionGrid = CollisionGrid((Area)lvl.ID)
                    }, true);
                }
            }

            return (new LevelData(), false);
        }

        private ServerLevel GetLevel(Area area)
        {
            foreach (var level in AllMapData)
            {
                if (level.ID == (int)area)
                {
                    return level;
                }
            }

            return new ServerLevel();
        }

        public class ServerLevel
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public Size Size { get; set; }
            public Offset Offset { get; set; }
            public List<List<int>> Map { get; set; }
            public List<Room> Rooms { get; set; }
            public List<MapObject> Objects { get; set; }
            public string Type { get; set; }
        }

        public class Offset
        {
            public int X { get; set; }
            public int Y { get; set; }
        }

        public class Size
        {
            public int Width { get; set; }
            public int Height { get; set; }
        }

        public class Room
        {
            public int Area { get; set; }
            public int X { get; set; }
            public int Y { get; set; }
            public int Width { get; set; }
            public int Height { get; set; }
            public bool Contain(int x, int y)
            {
                return x >= X && x < X + Width && y >= Y && y < Y + Height;
            }
        }

        public class MapObject
        {
            public string Type { get; set; }
            public string ID { get; set; }
            public string Name { get; set; }
            public int X { get; set; }
            public int Y { get; set; }
        }

        public class Position
        {
            public int X { get; set; }
            public int Y { get; set; }
        }

        public class LevelData
        {
            public int Area { get; set; }
            public string Name { get; set; }
            public Position Offset { get; set; }
            public Position Size { get; set; }
            public List<List<bool>> CollisionGrid { get; set; }
        }

        public class Level
        {
            public int Area { get; set; }
            public string Name { get; set; }
            //public int X { get; set; }
            //public int Y { get; set; }
            public Position Position { get; set; }
            public bool IsEntrance { get; set; }
        }

        public class NPC
        {
            public string ID { get; set; }
            public string Name { get; set; }
            public int X { get; set; }
            public int Y { get; set; }
        }

        public class ObjectS
        {
            public string ID { get; set; }
            public string Name { get; set; }
            //public int X { get; set; }
            //public int Y { get; set; }
            public Position Position { get; set; }
        }

        public enum ObjectName
        {
            // Define your enum members here
        }
    }
}
