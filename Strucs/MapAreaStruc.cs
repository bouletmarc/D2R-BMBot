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
using static Enums;
using static MapAreaStruc;
using System.Text.RegularExpressions;

public class MapAreaStruc
{
    Form1 Form1_0;

    public List<ServerLevel> AllMapData = new List<ServerLevel>();
    public int CurrentObjectIndex = 0;
    public int CurrentObjectAreaIndex = 0;

    public bool[,] CurrentAreaCollisionGrid = new bool[0, 0];
    public string[] MapDataLines = new string[0];
    public List<int> AllExitsIDs = new List<int>();

    public void SetForm1(Form1 form1_1)
    {
        Form1_0 = form1_1;

        _kooloMapPath = Application.StartupPath + @"\map.exe";
    }

    public Position GetAreaOfObject(string ObjectType, string ObjectName, List<int> IgnoreTheseIndex, int StartAreaIndexToSearch = 0, int EndAreaIndexToSearch = 1, bool ForOverlay = false)
    {
        Position ThisPos = new Position();
        ThisPos.X = 0;
        ThisPos.Y = 0;

        if (AllMapData.Count == 0) return ThisPos;

        if (StartAreaIndexToSearch == 0 && EndAreaIndexToSearch == 1)
        {
            EndAreaIndexToSearch = AllMapData.Count;
        }

        try
        {
            //ExitType = "exit" or "exit_area"

            if (!ForOverlay) CurrentObjectIndex = 0;
            if (!ForOverlay) CurrentObjectAreaIndex = 0;

            for (int i = StartAreaIndexToSearch; i < EndAreaIndexToSearch; i++)
            {
                if (i > AllMapData.Count - 1) ScanMapStruc();
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
                                if (!ForOverlay) CurrentObjectIndex = k;
                                if (!ForOverlay) CurrentObjectAreaIndex = i;
                            }
                        }
                        if (AllMapData[i].Objects[k].Type == "exit_area" && ObjectType == "exit_area")
                        {
                            if (Form1_0.Town_0.getAreaName(int.Parse(AllMapData[i].Objects[k].ID)) == ObjectName)
                            {
                                ThisPos.X = AllMapData[i].Offset.X + AllMapData[i].Objects[k].X;
                                ThisPos.Y = AllMapData[i].Offset.Y + AllMapData[i].Objects[k].Y;
                                if (!ForOverlay) CurrentObjectIndex = k;
                                if (!ForOverlay) CurrentObjectAreaIndex = i;
                            }
                        }
                        if (AllMapData[i].Objects[k].Type == "object" && ObjectType == "object")
                        {
                            //Console.WriteLine(Form1_0.ObjectsStruc_0.getObjectName(int.Parse(AllMapData[i].Objects[k].ID)));
                            if (Form1_0.ObjectsStruc_0.getObjectName(int.Parse(AllMapData[i].Objects[k].ID)) == ObjectName)
                            {
                                ThisPos.X = AllMapData[i].Offset.X + AllMapData[i].Objects[k].X;
                                ThisPos.Y = AllMapData[i].Offset.Y + AllMapData[i].Objects[k].Y;
                                if (!ForOverlay) CurrentObjectIndex = k;
                                if (!ForOverlay) CurrentObjectAreaIndex = i;
                            }
                        }
                        if (AllMapData[i].Objects[k].Type == "npc" && ObjectType == "npc")
                        {
                            if (Regex.Replace(((EnumsMobsNPC.MonsterType)(int.Parse(AllMapData[i].Objects[k].ID))).ToString(), @"[\d-]", string.Empty) == ObjectName)
                            {
                                ThisPos.X = AllMapData[i].Offset.X + AllMapData[i].Objects[k].X;
                                ThisPos.Y = AllMapData[i].Offset.Y + AllMapData[i].Objects[k].Y;
                                if (!ForOverlay) CurrentObjectIndex = k;
                                if (!ForOverlay) CurrentObjectAreaIndex = i;
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

    public Position GetPositionOfObject(string ObjectType, string ObjectName, int AreaID, List<int> IgnoreTheseIndex, bool IgnoreName = false, bool ForOverlay = false)
    {
        Position ThisPos = new Position();
        ThisPos.X = 0;
        ThisPos.Y = 0;

        if (AllMapData.Count == 0) return ThisPos;

        try
        {
            //ExitType = "exit" or "exit_area"

            if (!ForOverlay) CurrentObjectIndex = 0;
            if (!ForOverlay) CurrentObjectAreaIndex = 0;

            //for (int i = 0; i < AllMapData.Count; i++)
            //{
            int i = AreaID - 1;

            if (i > AllMapData.Count - 1) ScanMapStruc();
            else if (AllMapData[i].Objects.Count == 0) ScanMapStruc();

            for (int k = 0; k < AllMapData[i].Objects.Count; k++)
            {
                if (!AvoidThisIndex(k, IgnoreTheseIndex))
                {
                    if (AllMapData[i].Objects[k].Type == "exit" && ObjectType == "exit")
                    {
                        //Console.WriteLine(Form1_0.Town_0.getAreaName(int.Parse(AllMapData[i].Objects[k].ID)));
                        if ((Form1_0.Town_0.getAreaName(int.Parse(AllMapData[i].Objects[k].ID)) == ObjectName && !IgnoreName)
                            || IgnoreName)
                        {
                            ThisPos.X = AllMapData[i].Offset.X + AllMapData[i].Objects[k].X;
                            ThisPos.Y = AllMapData[i].Offset.Y + AllMapData[i].Objects[k].Y;
                            if (!ForOverlay) CurrentObjectIndex = k;
                            if (!ForOverlay) CurrentObjectAreaIndex = i;
                        }
                    }
                    if (AllMapData[i].Objects[k].Type == "exit_area" && ObjectType == "exit_area")
                    {
                        if ((Form1_0.Town_0.getAreaName(int.Parse(AllMapData[i].Objects[k].ID)) == ObjectName && !IgnoreName)
                            || IgnoreName)
                        {
                            ThisPos.X = AllMapData[i].Offset.X + AllMapData[i].Objects[k].X;
                            ThisPos.Y = AllMapData[i].Offset.Y + AllMapData[i].Objects[k].Y;
                            if (!ForOverlay) CurrentObjectIndex = k;
                            if (!ForOverlay) CurrentObjectAreaIndex = i;
                        }
                    }
                    if (AllMapData[i].Objects[k].Type == "object" && ObjectType == "object")
                    {
                        //Console.WriteLine("Object: " + Form1_0.ObjectsStruc_0.getObjectName(int.Parse(AllMapData[i].Objects[k].ID)));
                        if (ObjectName == "WaypointPortal")
                        {
                            if (Form1_0.ObjectsStruc_0.IsWaypoint(int.Parse(AllMapData[i].Objects[k].ID)))
                            {
                                ThisPos.X = AllMapData[i].Offset.X + AllMapData[i].Objects[k].X;
                                ThisPos.Y = AllMapData[i].Offset.Y + AllMapData[i].Objects[k].Y;
                                if (!ForOverlay) CurrentObjectIndex = k;
                                if (!ForOverlay) CurrentObjectAreaIndex = i;
                            }
                        }
                        else
                        {
                            if ((Form1_0.ObjectsStruc_0.getObjectName(int.Parse(AllMapData[i].Objects[k].ID)) == ObjectName && !IgnoreName)
                            || IgnoreName)
                            {
                                ThisPos.X = AllMapData[i].Offset.X + AllMapData[i].Objects[k].X;
                                ThisPos.Y = AllMapData[i].Offset.Y + AllMapData[i].Objects[k].Y;
                                if (!ForOverlay) CurrentObjectIndex = k;
                                if (!ForOverlay) CurrentObjectAreaIndex = i;
                            }
                        }
                    }
                    if (AllMapData[i].Objects[k].Type == "npc" && ObjectType == "npc")
                    {
                        //Console.WriteLine("NPC: " + ((EnumsMobsNPC.MonsterType)(int.Parse(AllMapData[i].Objects[k].ID))).ToString());
                        if ((Regex.Replace(((EnumsMobsNPC.MonsterType)(int.Parse(AllMapData[i].Objects[k].ID))).ToString(), @"[\d-]", string.Empty) == ObjectName && !IgnoreName)
                            || IgnoreName)
                        {
                            ThisPos.X = AllMapData[i].Offset.X + AllMapData[i].Objects[k].X;
                            ThisPos.Y = AllMapData[i].Offset.Y + AllMapData[i].Objects[k].Y;
                            if (!ForOverlay) CurrentObjectIndex = k;
                            if (!ForOverlay) CurrentObjectAreaIndex = i;
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

    public void DebugMapData()
    {
        Form1_0.ClearDebugMapData();
        DebuggingMapData = true;
        GetPositionOfAllObject("object", "", (int)Form1_0.PlayerScan_0.levelNo, new List<int>(), true);
        GetPositionOfAllObject("exit", "", (int)Form1_0.PlayerScan_0.levelNo, new List<int>(), true);
        GetPositionOfAllObject("npc", "", (int)Form1_0.PlayerScan_0.levelNo, new List<int>(), true);
        DebuggingMapData = false;
    }

    public bool DebuggingMapData = false;

    public List<Position> GetPositionOfAllObject(string ObjectType, string ObjectName, int AreaID, List<int> IgnoreTheseIndex, bool IgnoreName = false)
    {
        List<Position> ThisPos = new List<Position>();
        AllExitsIDs = new List<int>();

        if (AllMapData.Count == 0) return ThisPos;

        try
        {
            //ExitType = "exit" or "exit_area"

            int i = AreaID - 1;

            if (i > AllMapData.Count - 1) ScanMapStruc();
            else if (AllMapData[i].Objects.Count == 0) ScanMapStruc();

            for (int k = 0; k < AllMapData[i].Objects.Count; k++)
            {
                if (!AvoidThisIndex(k, IgnoreTheseIndex))
                {
                    if (AllMapData[i].Objects[k].Type == "exit" && ObjectType == "exit")
                    {
                        //Console.WriteLine(Form1_0.Town_0.getAreaName(int.Parse(AllMapData[i].Objects[k].ID)));
                        if ((Form1_0.Town_0.getAreaName(int.Parse(AllMapData[i].Objects[k].ID)) == ObjectName && !IgnoreName)
                            || IgnoreName)
                        {
                            ThisPos.Add(new Position { X = AllMapData[i].Offset.X + AllMapData[i].Objects[k].X, Y = AllMapData[i].Offset.Y + AllMapData[i].Objects[k].Y });
                            AllExitsIDs.Add(int.Parse(AllMapData[i].Objects[k].ID));

                            if (DebuggingMapData)
                            {
                                Form1_0.AppendTextDebugMapData("exit, ID:" + AllMapData[i].Objects[k].ID + "(" + Form1_0.Town_0.getAreaName(int.Parse(AllMapData[i].Objects[k].ID)) + ") at:" + (AllMapData[i].Offset.X + AllMapData[i].Objects[k].X) + ", " + (AllMapData[i].Offset.Y + AllMapData[i].Objects[k].Y) + Environment.NewLine);
                            }
                        }
                    }
                    if (AllMapData[i].Objects[k].Type == "exit_area" && ObjectType == "exit_area")
                    {
                        if ((Form1_0.Town_0.getAreaName(int.Parse(AllMapData[i].Objects[k].ID)) == ObjectName && !IgnoreName)
                            || IgnoreName)
                        {
                            ThisPos.Add(new Position { X = AllMapData[i].Offset.X + AllMapData[i].Objects[k].X, Y = AllMapData[i].Offset.Y + AllMapData[i].Objects[k].Y });
                            AllExitsIDs.Add(int.Parse(AllMapData[i].Objects[k].ID));

                            if (DebuggingMapData)
                            {
                                Form1_0.AppendTextDebugMapData("exit_area, ID:" + AllMapData[i].Objects[k].ID + "(" + Form1_0.Town_0.getAreaName(int.Parse(AllMapData[i].Objects[k].ID)) + ") at:" + (AllMapData[i].Offset.X + AllMapData[i].Objects[k].X) + ", " + (AllMapData[i].Offset.Y + AllMapData[i].Objects[k].Y) + Environment.NewLine);
                            }
                        }
                    }
                    if (AllMapData[i].Objects[k].Type == "object" && ObjectType == "object")
                    {
                        //Console.WriteLine("Object: " + Form1_0.ObjectsStruc_0.getObjectName(int.Parse(AllMapData[i].Objects[k].ID)));
                        if (ObjectName == "WaypointPortal")
                        {
                            if (Form1_0.ObjectsStruc_0.IsWaypoint(int.Parse(AllMapData[i].Objects[k].ID)))
                            {
                                ThisPos.Add(new Position { X = AllMapData[i].Offset.X + AllMapData[i].Objects[k].X, Y = AllMapData[i].Offset.Y + AllMapData[i].Objects[k].Y });

                                if (DebuggingMapData)
                                {
                                    Form1_0.AppendTextDebugMapData("object-waypoint, ID:" + AllMapData[i].Objects[k].ID + "(" + Form1_0.ObjectsStruc_0.getObjectName(int.Parse(AllMapData[i].Objects[k].ID)) + ") at:" + (AllMapData[i].Offset.X + AllMapData[i].Objects[k].X) + ", " + (AllMapData[i].Offset.Y + AllMapData[i].Objects[k].Y) + Environment.NewLine);
                                }
                            }
                        }
                        else
                        {
                            if ((Form1_0.ObjectsStruc_0.getObjectName(int.Parse(AllMapData[i].Objects[k].ID)) == ObjectName && !IgnoreName)
                                || IgnoreName)
                            {
                                ThisPos.Add(new Position { X = AllMapData[i].Offset.X + AllMapData[i].Objects[k].X, Y = AllMapData[i].Offset.Y + AllMapData[i].Objects[k].Y });

                                if (DebuggingMapData)
                                {
                                    Form1_0.AppendTextDebugMapData("object, ID:" + AllMapData[i].Objects[k].ID + "(" + Form1_0.ObjectsStruc_0.getObjectName(int.Parse(AllMapData[i].Objects[k].ID)) + ") at:" + (AllMapData[i].Offset.X + AllMapData[i].Objects[k].X) + ", " + (AllMapData[i].Offset.Y + AllMapData[i].Objects[k].Y) + Environment.NewLine);
                                }
                            }
                        }
                    }
                    if (AllMapData[i].Objects[k].Type == "npc" && ObjectType == "npc")
                    {
                        if ((Regex.Replace(((EnumsMobsNPC.MonsterType)(int.Parse(AllMapData[i].Objects[k].ID))).ToString(), @"[\d-]", string.Empty) == ObjectName && !IgnoreName)
                            || IgnoreName)
                        {
                            ThisPos.Add(new Position { X = AllMapData[i].Offset.X + AllMapData[i].Objects[k].X, Y = AllMapData[i].Offset.Y + AllMapData[i].Objects[k].Y });

                            if (DebuggingMapData)
                            {
                                Form1_0.AppendTextDebugMapData("npc, ID:" + AllMapData[i].Objects[k].ID + "(" + (EnumsMobsNPC.MonsterType)(int.Parse(AllMapData[i].Objects[k].ID)) + ") at:" + (AllMapData[i].Offset.X + AllMapData[i].Objects[k].X) + ", " + (AllMapData[i].Offset.Y + AllMapData[i].Objects[k].Y) + Environment.NewLine);
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

        int tryes = 0;
        while (tryes < 3)
        {
            GetMapData(Form1_0.PlayerScan_0.mapSeedValue.ToString(), (Difficulty)Form1_0.PlayerScan_0.difficulty);
            if (AllMapData.Count != 0)
            {
                tryes = 15;
                break;
            }
        }
    }

    public string _kooloMapPath;
    public string _d2LoDPath;

    public void GetMapData(string seed, Difficulty difficulty)
    {
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
            MapDataLines = stdoutLines;


            if (lvls.Count == 0)
            {
                Form1_0.method_1("Couldn't get the map data from D2 LOD 1.13C!", Color.Red);
                Form1_0.method_1("Check the file 'DumpMap.txt' for more infos", Color.Red);
                Form1_0.method_1("Retrying...", Color.Red);
            }

            /*if (process.ExitCode != 0)
            {
                throw new Exception($"Error detected fetching Map Data from Diablo II: LoD 1.13c game, please make sure you have the classic expansion game installed AND config.yaml D2LoDPath is pointing to the correct game path. Error code: {process.ExitCode}");
            }*/

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

    public bool[,] CollisionGrid(Area area)
    {
        ServerLevel level = GetLevel(area);

        int Tryess = 0;
        while (level == null && Tryess < 5)
        {
            Form1_0.MapAreaStruc_0.GetMapData(Form1_0.PlayerScan_0.mapSeedValue.ToString(), (Difficulty)Form1_0.PlayerScan_0.difficulty);
            level = GetLevel(area);
            Tryess++;
        }

        if (level == null)
        {
            Form1_0.method_1("ERROR Trying to get collision grid!", Color.Red);
            return new bool[0, 0];
        }
        if (level.Size == null)
        {
            Form1_0.method_1("ERROR Trying to get collision grid!", Color.Red);
            return new bool[0, 0];
        }

        bool[,] cg = new bool[level.Size.Width, level.Size.Height];

        for (int y = 0; y < level.Size.Height; y++)
        {
            for (int x = 0; x < level.Size.Width; x++)
            {
                cg[x, y] = false;
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
                            cg[xPos + xOffset, y] = isWalkable;
                        }
                    }
                    isWalkable = !isWalkable;
                    xPos += xs;
                }
                while (xPos < level.Size.Width)
                {
                    cg[xPos, y] = isWalkable;
                    xPos++;
                }
            }
        }

        // Lut Gholein map is a bit bugged, we should close this fake path to avoid pathing issues
        if (area == Enums.Area.LutGholein) cg[13, 210] = false;

        // Kurast Docks
        if (area == Enums.Area.KurastDocks)
        {
            cg[140, 85] = false;
            cg[141, 85] = false;
            cg[142, 85] = false;
            cg[143, 85] = false;

            cg[129, 149] = false;
            cg[130, 149] = false;
            cg[131, 149] = false;

            cg[126, 160] = false;
            cg[127, 160] = false;

            cg[125, 161] = false;
            cg[126, 161] = false;
        }

        // Harrogath
        if (area == Enums.Area.Harrogath)
        {
            cg[92, 29] = false;
            cg[93, 29] = false;
            cg[94, 29] = false;

            cg[94, 28] = false;
        }


        // Frigid
        if (area == Enums.Area.FrigidHighlands)
        {
            cg[285, 731] = false;
            cg[286, 731] = false;
            cg[287, 731] = false;
            cg[288, 731] = false;
            cg[289, 731] = false;
            cg[290, 731] = false;
            cg[291, 731] = false;
            cg[292, 731] = false;
            cg[293, 731] = false;
            cg[294, 731] = false;
            cg[295, 731] = false;
            cg[296, 731] = false;
            cg[297, 731] = false;
            cg[298, 731] = false;
            cg[299, 731] = false;
            //cg[300, 731] = false;
            cg[301, 731] = false;
            cg[302, 731] = false;
            cg[303, 731] = false;
            cg[304, 731] = false;
            cg[305, 731] = false;
            cg[306, 731] = false;
            cg[307, 731] = false;
            cg[308, 731] = false;
            cg[309, 731] = false;
            cg[310, 731] = false;
            cg[311, 731] = false;
            cg[312, 731] = false;
            cg[313, 731] = false;
            cg[314, 731] = false;
            cg[315, 731] = false;
            cg[316, 731] = false;
            cg[317, 731] = false;
            cg[318, 731] = false;
            cg[319, 731] = false;
            //cg[320, 731] = false;

            cg[285, 723] = false;
            cg[285, 724] = false;
            cg[285, 725] = false;
            cg[285, 726] = false;
            cg[285, 727] = false;
            cg[285, 728] = false;
            cg[285, 729] = false;

            cg[270, 722] = false;
            cg[271, 722] = false;
            cg[272, 722] = false;
            cg[273, 722] = false;
            cg[274, 722] = false;
            cg[275, 722] = false;
            cg[276, 722] = false;
            cg[277, 722] = false;
            cg[278, 722] = false;
            cg[279, 722] = false;
            cg[280, 722] = false;
            cg[281, 722] = false;
            cg[282, 722] = false;
            cg[283, 722] = false;
            cg[284, 722] = false;
            cg[285, 722] = false;

            cg[270, 719] = false;
            cg[270, 720] = false;

            cg[265, 718] = false;
            cg[266, 718] = false;
            cg[267, 718] = false;
            cg[268, 718] = false;
            cg[269, 718] = false;
            cg[270, 718] = false;

            cg[265, 714] = false;
            cg[265, 715] = false;
            cg[265, 716] = false;
        }

        //Forgotten Tower (Countess)
        if (area == Enums.Area.TowerCellarLevel1 || area == Enums.Area.TowerCellarLevel2 || area == Enums.Area.TowerCellarLevel3 || area == Enums.Area.TowerCellarLevel4)
        {
            cg[179, 143] = false;
            cg[180, 143] = false;
            cg[181, 143] = false;
            cg[182, 143] = false;
            cg[183, 143] = false;
            cg[184, 143] = false;

            cg[178, 142] = false;
            cg[179, 142] = false;
            cg[180, 142] = false;
            cg[181, 142] = false;
            cg[182, 142] = false;
            cg[183, 142] = false;
        }

        //Burial Ground (Crypt Door)
        if (area == Enums.Area.BurialGrounds)
        {
            Position ThisDoorPosition = Form1_0.MapAreaStruc_0.GetPositionOfObject("exit", Form1_0.Town_0.getAreaName((int)Enums.Area.Crypt), (int) Enums.Area.BurialGrounds, new List<int>() { });
            if (ThisDoorPosition.X > 0 && ThisDoorPosition.Y > 0)
            {
                ThisDoorPosition.X -= Form1_0.MapAreaStruc_0.AllMapData[((int)(Enums.Area.BurialGrounds) - 1)].Offset.X;
                ThisDoorPosition.Y -= Form1_0.MapAreaStruc_0.AllMapData[((int)(Enums.Area.BurialGrounds) - 1)].Offset.Y;
                cg[ThisDoorPosition.X, ThisDoorPosition.Y] = true;
                cg[ThisDoorPosition.X + 1, ThisDoorPosition.Y] = true;
                cg[ThisDoorPosition.X - 1, ThisDoorPosition.Y] = true;

                cg[ThisDoorPosition.X, ThisDoorPosition.Y - 1] = true;
                cg[ThisDoorPosition.X + 1, ThisDoorPosition.Y - 1] = true;
                cg[ThisDoorPosition.X - 1, ThisDoorPosition.Y - 1] = true;

                cg[ThisDoorPosition.X, ThisDoorPosition.Y + 1] = true;
                cg[ThisDoorPosition.X + 1, ThisDoorPosition.Y + 1] = true;
                cg[ThisDoorPosition.X - 1, ThisDoorPosition.Y + 1] = true;
            }
        }

        //dump data to txt file
        /*string ColisionMapTxt = "";
        for (int i = 0; i < cg.GetLength(0); i++)
        {
            for (int k = 0; k < cg.GetLength(1); k++)
            {
                if (cg[i, k]) ColisionMapTxt += "-";
                if (!cg[i, k]) ColisionMapTxt += "X";
            }
            ColisionMapTxt += Environment.NewLine;
        }
        File.Create(Form1_0.ThisEndPath + "CollisionMap.txt").Dispose();
        File.WriteAllText(Form1_0.ThisEndPath + "CollisionMap.txt", ColisionMapTxt);*/

        return cg;
        //return cg.Select(r => r.ToArray()).ToArray();
    }

    public void DumpMap()
    {
        string AddedTxt = "";
        if ((CharConfig.RunSummonerRush && !Form1_0.Summoner_0.ScriptDone)
            || (CharConfig.RunSummonerScript && !Form1_0.SummonerRush_0.ScriptDone))
        {
            AddedTxt = "NoPathSummoner";

            //dump data to txt file
            string ColisionMapTxt = "";
            bool[,] cgrid = CollisionGrid(Area.ArcaneSanctuary);
            for (int i = 0; i < cgrid.GetLength(0); i++)
            {
                for (int k = 0; k < cgrid.GetLength(1); k++)
                {
                    if (cgrid[k, i]) ColisionMapTxt += "-";
                    if (!cgrid[k, i]) ColisionMapTxt += "X";
                }
                ColisionMapTxt += Environment.NewLine;
            }
            File.Create(Form1_0.ThisEndPath + "CollisionMapSummoner.txt").Dispose();
            File.WriteAllText(Form1_0.ThisEndPath + "CollisionMapSummoner.txt", ColisionMapTxt);
        }
        else AddedTxt += Form1_0.PreviousStatus.Replace("(", "").Replace(")", "").Replace("/", "").Replace("\\", "");

        string SavePathh = Form1_0.ThisEndPath + "MapTest" + AddedTxt + ".txt";

        File.Create(SavePathh).Dispose();
        File.WriteAllLines(SavePathh, MapDataLines);
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

    public ServerLevel GetLevel(Area area)
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
        public bool[,] CollisionGrid { get; set; }
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
