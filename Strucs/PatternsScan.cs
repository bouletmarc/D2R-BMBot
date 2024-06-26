using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using static Enums;

public class PatternsScan
{
    Form1 Form1_0;

    public byte[] UnitBuffer = new byte[] { };

    public Dictionary<long, bool> AllItemsPointers = new Dictionary<long, bool>();
    public Dictionary<long, bool> AllObjectsPointers = new Dictionary<long, bool>(); //->refer to all gameobjects
    public Dictionary<long, bool> AllPlayersPointers = new Dictionary<long, bool>();
    public Dictionary<long, bool> AllNPCPointers = new Dictionary<long, bool>();

    public List<long> AllPossiblePointers = new List<long>();

    public List<long> AllPossibleItemsPointers = new List<long>();
    public List<long> AllPossiblePlayerPointers = new List<long>();
    public List<long> AllPossibleObjectsPointers = new List<long>();
    public List<long> AllPossibleNPCPointers = new List<long>();

    public Dictionary<long, bool> AllScannedPointers = new Dictionary<long, bool>();

    //"04 00 00 00 ?? ?? 00 00";
    byte[] ThisCheckbytes = new byte[] { 0x04, 0x00, 0x00, 0x00, 0xff, 0xff, 0x00, 0x00 };
    public int[] DontCheckUnitIndexes = new int[] { 0, 0, 0, 0, 1, 1, 0, 0 };
    public int[] DontCheckIndexes = new int[0];

    public long StartIndexItem = long.MaxValue;
    public long StartIndexItemLast = long.MaxValue;
    public int ScanUnitsNumber = 2600;
    //public int ScanUnitsNumber = 2048;
    public int ScanUnitsNegativeOffset = 150;

    public long StartIndexItem_V2 = long.MaxValue;
    public long StartIndexItemLast_V2 = long.MaxValue;

    public long StartIndexItem_V1_Debug = long.MaxValue;
    public long StartIndexItem_V2_Debug = long.MaxValue;
    public long StartIndexItem_V3_Debug = long.MaxValue;

    public long StartIndexItem_V3 = 0x231818140B0;

    public int UnitsScanVersion = 1;
    public int MaxUnitsIncreaseCount = 5;
    public int CurrentUnitsIncreaseCount = 0;

    public void SetForm1(Form1 form1_1)
    {
        Form1_0 = form1_1;
    }

    IntPtr modulePatternScan(string Tpattern)
    {
        IntPtr ThisAddr = (IntPtr)0;

        //method_1("Patterns: " + Tpattern);
        byte[] ThisCheckbytes = StringToByteArray(Tpattern.Replace(" ", ""));
        long ThisAddrF = Search(Form1_0.buffer, ThisCheckbytes, 0, "pattern");
        if (ThisAddrF > 0)
        {
            return (IntPtr)ThisAddrF;
        }

        return ThisAddr;
    }

    public byte[] StringToByteArray(string hex)
    {
        DontCheckIndexes = new int[hex.Length / 2];

        int Inddd = 0;
        for (int i = 0; i < hex.Length; i++)
        {
            if (hex[i] == '?')
            {
                DontCheckIndexes[Inddd] = 1;
            }
            else
            {
                DontCheckIndexes[Inddd] = 0;
            }
            Inddd++;
            i++;
        }

        hex = hex.Replace("??", "00");

        return Enumerable.Range(0, hex.Length)
                         .Where(x => x % 2 == 0)
                         .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                         .ToArray();
    }

    public long Search(byte[] arrayToSearchThrough, byte[] patternToFind, long StartOffset, string SearchType)
    {
        if (patternToFind.Length > arrayToSearchThrough.Length) return -1;

        int DontCheckCount = 0;
        for (long i = StartOffset; i < arrayToSearchThrough.Length - patternToFind.Length; i++)
        {
            bool found = true;
            for (long j = 0; j < patternToFind.Length; j++)
            {
                int DontChh = 0;
                if (SearchType == "item" || SearchType == "player" || SearchType == "objects" || SearchType == "NPC")
                {
                    DontChh = DontCheckUnitIndexes[j];
                }
                else
                {
                    DontChh = DontCheckIndexes[j];
                }

                if (DontChh == 0)
                {
                    if (arrayToSearchThrough[i + j] != patternToFind[j])
                    {
                        found = false;
                        break;
                    }
                }

                //check if TxtFileNo are within range
                if (SearchType == "item")
                {
                    if (DontChh == 1)
                    {
                        //not higher than 0x02 (0x2FF)
                        if (DontCheckCount == 1 && arrayToSearchThrough[i + j] > 0x02)
                        {
                            found = false;
                            break;
                        }
                        DontCheckCount++;
                    }
                }
            }

            if (found)
            {
                return i;
            }
        }
        return -1;
    }

    public void PatternScan()
    {
        //SetFormat, Integer, Hex
        //; unit table
        string pattern = "48 03 C7 49 8B 8C C6";
        IntPtr patternAddress = modulePatternScan(pattern);
        IntPtr unitTable = (IntPtr)Form1_0.Mem_0.ReadInt(patternAddress + 7);
        Form1_0.offsets["unitTable"] = unitTable;
        Form1_0.method_1("UnitTable offset: 0x" + unitTable.ToString("X"), Color.Black);

        //; ui
        pattern = "40 84 ed 0f 94 05";
        patternAddress = modulePatternScan(pattern);
        IntPtr offsetBuffer = (IntPtr)Form1_0.Mem_0.ReadInt(patternAddress + 6);
        //IntPtr uiOffset = (IntPtr) (((Int64) patternAddress - (Int64) BaseAddress) + 10 + (Int64) offsetBuffer);
        IntPtr uiOffset = (IntPtr)(((Int64)patternAddress) + 10 + (Int64)offsetBuffer);
        Form1_0.offsets["uiOffset"] = uiOffset;
        Form1_0.method_1("UI offset: 0x" + uiOffset.ToString("X"), Color.Black);

        //; expansion
        //pattern = "48 8B 05 ?? ?? ?? ?? 48 8B D9 F3 0F 10 50 ??";
        pattern = "48 8B 05 ?? ?? ?? ?? 48 8B D9 F3 0F 10 50";
        patternAddress = modulePatternScan(pattern);
        offsetBuffer = (IntPtr)Form1_0.Mem_0.ReadInt(patternAddress + 3);
        //IntPtr expOffset = (IntPtr) (((Int64) patternAddress - (Int64) BaseAddress) + 7 + (Int64) offsetBuffer);
        IntPtr expOffset = (IntPtr)(((Int64)patternAddress) + 7 + (Int64)offsetBuffer);
        Form1_0.offsets["expOffset"] = expOffset;
        Form1_0.method_1("Expansion offset: 0x" + expOffset.ToString("X"), Color.Black);

        //; game data(IP and name)
        //pattern = "44 88 25 ?? ?? ?? ?? 66 44 89 25 ?? ?? ?? ??";
        pattern = "44 88 25 ?? ?? ?? ?? 66 44 89 25";
        patternAddress = modulePatternScan(pattern);
        offsetBuffer = (IntPtr)Form1_0.Mem_0.ReadInt(patternAddress + 0x3);
        //IntPtr gameDataOffset = (IntPtr) (((Int64) patternAddress - (Int64) BaseAddress) - 0x121 + (Int64) offsetBuffer);
        IntPtr gameDataOffset = (IntPtr)(((Int64)patternAddress) - 0x121 + (Int64)offsetBuffer);
        Form1_0.offsets["gameDataOffset"] = gameDataOffset;
        Form1_0.method_1("Game data offset: 0x" + gameDataOffset.ToString("X"), Color.Black);

        //; menu visibility
        pattern = "8B 05 ?? ?? ?? ?? 89 44 24 20 74 07";
        patternAddress = modulePatternScan(pattern);
        offsetBuffer = (IntPtr)Form1_0.Mem_0.ReadInt(patternAddress + 2);
        //IntPtr menuOffset = (IntPtr) (((Int64) patternAddress - (Int64) BaseAddress) + 6 + (Int64) offsetBuffer);
        IntPtr menuOffset = (IntPtr)(((Int64)patternAddress) + 6 + (Int64)offsetBuffer);
        Form1_0.offsets["menuOffset"] = menuOffset;
        Form1_0.method_1("Menu offset: 0x" + menuOffset.ToString("X"), Color.Black);

        //; last hover object
        //pattern = "C6 84 C2 ?? ?? ?? ?? ?? 48 8B 74 24 ??";
        pattern = "C6 84 C2 ?? ?? ?? ?? ?? 48 8B 74 24";
        patternAddress = modulePatternScan(pattern);
        IntPtr hoverOffset = (IntPtr)Form1_0.Mem_0.ReadInt(patternAddress + 3) - 1;
        Form1_0.offsets["hoverOffset"] = hoverOffset;
        Form1_0.method_1("Hover offset: 0x" + hoverOffset.ToString("X"), Color.Black);

        //; roster
        pattern = "02 45 33 D2 4D 8B";
        patternAddress = modulePatternScan(pattern);
        offsetBuffer = (IntPtr)Form1_0.Mem_0.ReadInt(patternAddress - 3);
        //IntPtr rosterOffset = (IntPtr) (((Int64) patternAddress - (Int64) BaseAddress) + 1 + (Int64) offsetBuffer);
        IntPtr rosterOffset = (IntPtr)(((Int64)patternAddress) + 1 + (Int64)offsetBuffer);
        Form1_0.offsets["rosterOffset"] = rosterOffset;
        Form1_0.method_1("Roster offset: 0x" + rosterOffset.ToString("X"), Color.Black);

        //#################################################################################################
        /*All games available offset: 0x2A19F10
        Selected game offset: 0x229DBD10
        Selected Char offset: 0x1E1EEF8*/
        Form1_0.offsets["AllGamesOffset"] = ((IntPtr)0x2A19F10);
        Form1_0.method_1("All games available offset: 0x" + 0x2A19F10.ToString("X"), Color.Black);

        Form1_0.offsets["GameSelectedOffset"] = ((IntPtr)0x2A28420);
        Form1_0.method_1("Selected game offset: 0x" + 0x2A28420.ToString("X"), Color.Black);

        Form1_0.offsets["SelectedChar"] = ((IntPtr)0x1E1EEF8);
        Form1_0.method_1("Selected Char offset: 0x" + 0x1E1EEF8.ToString("X"), Color.Black);

        /*
        //; all games datas
        pattern = "F8 1E 8B 9F F7 7F 00";
        patternAddress = (IntPtr)(modulePatternScan(pattern) + 16);
        //patternAddress = modulePatternScan(pattern);
        //IntPtr GameTable = (IntPtr)Form1_0.Mem_0.ReadInt(patternAddress + 6);
        Form1_0.offsets["AllGamesOffset"] = patternAddress;
        Form1_0.method_1("All games available offset: 0x" + patternAddress.ToString("X"), Color.Black);

        //; game selected data
        //pattern = "01 8D 14 51 F7 E2 42";
        pattern = "08 04 8C 9F F7 7F 00";
        patternAddress = (IntPtr)(modulePatternScan(pattern) + 16);
        //patternAddress = modulePatternScan(pattern);
        //IntPtr GameTable = (IntPtr)Form1_0.Mem_0.ReadInt(patternAddress + 7);
        //pattern = "50 E4 0F 67 F6 7F 00";
        //patternAddress = (IntPtr)(modulePatternScan(pattern) + 64);
        //IntPtr unitTable = (IntPtr)Form1_0.Mem_0.ReadInt(patternAddress + 7);
        IntPtr GameTable = patternAddress;
        //Form1_0.offsets["GameSelectedOffset"] = GameTable + 0x113;
        //Form1_0.method_1("Selected game offset: 0x" + (GameTable + 0x113).ToString("X"), Color.Black);
        Form1_0.offsets["GameSelectedOffset"] = GameTable;
        Form1_0.method_1("Selected game offset: 0x" + (GameTable).ToString("X"), Color.Black);

        //; selected char
        pattern = "D0 DA 8A 9F F7 7F 00";
        patternAddress = (IntPtr)(modulePatternScan(pattern) + 0x18);
        //patternAddress = modulePatternScan(pattern);
        //IntPtr GameTable = (IntPtr)Form1_0.Mem_0.ReadInt(patternAddress + 6);
        Form1_0.offsets["SelectedChar"] = patternAddress;
        Form1_0.method_1("Selected Char offset: 0x" + patternAddress.ToString("X"), Color.Black);*/

        //DetectFirstUnitPointer();
    }

    //########################################################################################################################################
    //########################################################################################################################################
    //########################################################################################################################################
    //ALL VERSIONS
    public void scanForUnitsPointer(string SearchUnitsType)
    {
        if (UnitsScanVersion == 1) scanForUnitsPointerV1(SearchUnitsType);
        if (UnitsScanVersion == 2) UnitPatternScanV2(SearchUnitsType);
        if (UnitsScanVersion == 3) UnitPatternScanV3(SearchUnitsType);
    }

    public void unitPatternScan(long AtPoiinter, string SearchType)
    {
        if (IsGoodUnitPointer(AtPoiinter, SearchType))
        {
            AddPointerToList(AtPoiinter, SearchType);
        }
    }

    public bool IsGoodUnitPointer(long AtPoiinter, string SearchType)
    {
        //byte[] ThisCheckbytes = new byte[] { 0x04, 0x00, 0x00, 0x00, 0xff, 0xff, 0x00, 0x00 };
        //public int[] DontCheckUnitIndexes = new int[] { 0, 0, 0, 0, 1, 1, 0, 0 };
        long ThisAddrF = -1;
        if (UnitBuffer[0] == ThisCheckbytes[0]
            && UnitBuffer[1] == ThisCheckbytes[1]
            && UnitBuffer[2] == ThisCheckbytes[2]
            && UnitBuffer[3] == ThisCheckbytes[3]
            && UnitBuffer[6] == ThisCheckbytes[6]
            && UnitBuffer[7] == ThisCheckbytes[7])
        {
            ThisAddrF = 0;
        }

        bool GoodPlayerPointer = true;
        if (SearchType == "player")
        {
            long pathAddress = Form1_0.Mem_0.ReadInt64Raw((IntPtr)(AtPoiinter + 0x38));
            //Console.WriteLine(pathAddress);
            if (pathAddress == 0) GoodPlayerPointer = false;
        }

        if (ThisAddrF >= 0 && GoodPlayerPointer)
        {
            return true;
        }
        return false;
    }

    public int ScannedItemsCount = 0;
    public int ScannedPlayerCount = 0;
    public int ScannedObjectsCount = 0;
    public int ScannedNPCCount = 0;

    public int GetUnitsScannedCount(int ThisVersion)
    {
        ScannedItemsCount = 0;
        ScannedPlayerCount = 0;
        ScannedObjectsCount = 0;
        ScannedNPCCount = 0;

        if (ThisVersion == 1) scanForUnitsPointerV1("item");
        if (ThisVersion == 2) scanForUnitsPointerV2("item");
        if (ThisVersion == 3) scanForUnitsPointerV3("item");

        int UnitsScannedCount = 0;
        long CheckThisI = StartIndexItem;
        if (ThisVersion == 2) CheckThisI = StartIndexItem_V2;
        if (ThisVersion == 3) CheckThisI = StartIndexItem_V3;

        //###############
        //###############
        bool IsDebugging = false;
        /*if (ThisVersion == 1 && StartIndexItem_V1_Debug != CheckThisI)
        {
            StartIndexItem_V1_Debug = CheckThisI;
            Form1_0.method_1("V1 Units pointer start at: 0x" + CheckThisI.ToString("X"), Color.DarkViolet);
            IsDebugging = true;
        }
        if (ThisVersion == 2 && StartIndexItem_V2_Debug != CheckThisI)
        {
            StartIndexItem_V2_Debug = CheckThisI;
            Form1_0.method_1("V2 Units pointer start at: 0x" + CheckThisI.ToString("X"), Color.DarkViolet);
            IsDebugging = true;
        }
        if (ThisVersion == 3 && StartIndexItem_V3_Debug != CheckThisI)
        {
            StartIndexItem_V3_Debug = CheckThisI;
            Form1_0.method_1("V3 Units pointer start at: 0x" + CheckThisI.ToString("X"), Color.DarkViolet);
            IsDebugging = true;
        }*/
        //###############
        //###############

        CheckThisI -= (0x48 + 0x170) * ScanUnitsNegativeOffset;  //offseting in negative here
        UnitBuffer = new byte[9];

        string SavePathh = "";
        if (IsDebugging)
        {
            //SavePathh = Form1_0.ThisEndPath + "DumpHexUnitsV" + ThisVersion + "_0x" + CheckThisI.ToString("X");
            SavePathh = Form1_0.ThisEndPath + "DumpHexUnitsV" + ThisVersion;
            File.Create(SavePathh).Dispose();
        }

        for (int i = 0; i < ScanUnitsNumber; i++)
        {
            if ((i % 2) == 1) CheckThisI += 0x48;
            else CheckThisI += 0x170;

            if (IsDebugging)
            {
                byte[] CurrentUnitBuff = new byte[(0x48 + 0x170)];
                Form1_0.Mem_0.ReadRawMemory(CheckThisI, ref CurrentUnitBuff, CurrentUnitBuff.Length);
                AppendAllBytes(SavePathh, CurrentUnitBuff);
            }

            ThisCheckbytes = new byte[] { (byte)UnitType.Item, 0x00, 0x00, 0x00, 0xff, 0xff, 0x00, 0x00 };
            Form1_0.Mem_0.ReadRawMemory(CheckThisI, ref UnitBuffer, UnitBuffer.Length);
            if (IsGoodUnitPointer(CheckThisI, "item")) { UnitsScannedCount++; ScannedItemsCount++; }

            ThisCheckbytes = new byte[] { (byte)UnitType.Player, 0x00, 0x00, 0x00, 0xff, 0xff, 0x00, 0x00 };
            Form1_0.Mem_0.ReadRawMemory(CheckThisI, ref UnitBuffer, UnitBuffer.Length);
            if (IsGoodUnitPointer(CheckThisI, "player")) { UnitsScannedCount++; ScannedPlayerCount++; }

            ThisCheckbytes = new byte[] { (byte)UnitType.GameObject, 0x00, 0x00, 0x00, 0xff, 0xff, 0x00, 0x00 };
            Form1_0.Mem_0.ReadRawMemory(CheckThisI, ref UnitBuffer, UnitBuffer.Length);
            if (IsGoodUnitPointer(CheckThisI, "objects")) { UnitsScannedCount++; ScannedObjectsCount++; }

            ThisCheckbytes = new byte[] { (byte)UnitType.NPC, 0x00, 0x00, 0x00, 0xff, 0xff, 0x00, 0x00 };
            Form1_0.Mem_0.ReadRawMemory(CheckThisI, ref UnitBuffer, UnitBuffer.Length);
            if (IsGoodUnitPointer(CheckThisI, "NPC")) { UnitsScannedCount++; ScannedNPCCount++; }
        }

        return UnitsScannedCount;
    }

    public static void AppendAllBytes(string path, byte[] bytes)
    {
        using (var stream = new FileStream(path, FileMode.Append))
        {
            stream.Write(bytes, 0, bytes.Length);
        }
    }
    //########################################################################################################################################
    //########################################################################################################################################
    //########################################################################################################################################
    //VERSION 3 UNITS SCAN
    public void scanForUnitsPointerV3(string SearchUnitsType)
    {
        UnitPatternScanV3(SearchUnitsType);
    }

    public void UnitPatternScanV3(string SearchUnitsType)
    {
        //search
        long CheckThisI = StartIndexItem_V3;
        UnitBuffer = new byte[9];

        for (int i = 0; i < ScanUnitsNumber; i++)
        {
            if ((i % 2) == 1) CheckThisI += 0x48;
            else CheckThisI += 0x170;

            if (SearchUnitsType == "item")
            {
                ThisCheckbytes = new byte[] { (byte)UnitType.Item, 0x00, 0x00, 0x00, 0xff, 0xff, 0x00, 0x00 };
                Form1_0.Mem_0.ReadRawMemory(CheckThisI, ref UnitBuffer, UnitBuffer.Length);
                unitPatternScan(CheckThisI, "item");
            }

            if (SearchUnitsType == "player")
            {
                ThisCheckbytes = new byte[] { (byte)UnitType.Player, 0x00, 0x00, 0x00, 0xff, 0xff, 0x00, 0x00 };
                Form1_0.Mem_0.ReadRawMemory(CheckThisI, ref UnitBuffer, UnitBuffer.Length);
                unitPatternScan(CheckThisI, "player");
            }

            if (SearchUnitsType == "objects")
            {
                ThisCheckbytes = new byte[] { (byte)UnitType.GameObject, 0x00, 0x00, 0x00, 0xff, 0xff, 0x00, 0x00 };
                Form1_0.Mem_0.ReadRawMemory(CheckThisI, ref UnitBuffer, UnitBuffer.Length);
                unitPatternScan(CheckThisI, "objects");
            }

            if (SearchUnitsType == "NPC")
            {
                ThisCheckbytes = new byte[] { (byte)UnitType.NPC, 0x00, 0x00, 0x00, 0xff, 0xff, 0x00, 0x00 };
                Form1_0.Mem_0.ReadRawMemory(CheckThisI, ref UnitBuffer, UnitBuffer.Length);
                unitPatternScan(CheckThisI, "NPC");
            }
        }
    }

    //########################################################################################################################################
    //########################################################################################################################################
    //########################################################################################################################################
    //VERSION 2 UNITS SCAN
    public void scanForUnitsPointerV2(string SearchUnitsType)
    {
        UnitPatternScanV2(SearchUnitsType);
    }

    public void DetectFirstUnitPointer()
    {
        long UnitOffset = (long)Form1_0.BaseAddress + (long)Form1_0.offsets["unitTable"] + Form1_0.UnitStrucOffset;

        AllPossiblePointers = new List<long>();
        for (int i = 0; i < ((128 + 516) * 10); i += 8)
        {
            long UnitPointerLocation = Form1_0.Mem_0.ReadInt64Raw((IntPtr)(UnitOffset + i));
            if (UnitPointerLocation > 0) AllPossiblePointers.Add(UnitPointerLocation);
        }

        //set start index for searching
        for (int i = 0; i < AllPossiblePointers.Count; i++)
        {
            if (AllPossiblePointers[i] < StartIndexItem_V2) StartIndexItem_V2 = AllPossiblePointers[i];
        }

        //(byte)UnitType.Item, 0x00, 0x00, 0x00, 0xff, 0xff, 0x00, 0x00
        //1C 00 00 00 0C 00 00 00 21 00 00 00 1D 00 00 00
        //04 00 00 00 1F 00 00 00 06 00 00 00 00 00 00 00

        if (StartIndexItem_V2 == long.MaxValue) return;

        int BadCount = 0;
        long CheckThisI = StartIndexItem_V2;
        for (int i = 300; i >= 0; i--)
        {
            //if ((i % 2) == 1) CheckThisI -= 0x48;
            //else CheckThisI -= 0x170;
            CheckThisI -= (0x48 + 0x170);

            byte[] CurrentUnitBuff = new byte[8];
            Form1_0.Mem_0.ReadRawMemory(CheckThisI, ref CurrentUnitBuff, CurrentUnitBuff.Length);

            //Console.WriteLine(CurrentUnitBuff[0].ToString("X2") + " " + CurrentUnitBuff[1].ToString("X2") + " " + CurrentUnitBuff[2].ToString("X2") + " " + CurrentUnitBuff[3].ToString("X2") + " "
            //    + CurrentUnitBuff[4].ToString("X2") + " " + CurrentUnitBuff[5].ToString("X2") + " " + CurrentUnitBuff[6].ToString("X2") + " " + CurrentUnitBuff[7].ToString("X2") + " ");

            if (CurrentUnitBuff[1] == 0 && CurrentUnitBuff[2] == 0 && CurrentUnitBuff[3] == 0 && CurrentUnitBuff[6] == 0 && CurrentUnitBuff[7] == 0)
            {
                BadCount = 0;
                StartIndexItem_V2 = CheckThisI;
                //Form1_0.method_1("Possible Units pointer start at: 0x" + StartIndexItem_V2.ToString("X"), Color.DarkViolet);
            }
            else
            {
                //if (CurrentUnitBuff[6] != 0xff && CurrentUnitBuff[7] != 0xff && CurrentUnitBuff[7] != 0x80)
                //{
                BadCount++;
                if (BadCount >= 6)
                {
                    if (StartIndexItem_V2 != StartIndexItemLast_V2)
                    {
                        StartIndexItemLast_V2 = StartIndexItem_V2;
                        //ScanUnitsNumber += i;
                        Form1_0.method_1("Units pointer start at: 0x" + StartIndexItem_V2.ToString("X"), Color.Black);
                    }
                    return;
                }
                /*}
                else
                {
                    BadCount = 0;
                }*/
            }
        }
    }

    public void UnitPatternScanV2(string SearchUnitsType)
    {
        if (StartIndexItem_V2 == long.MaxValue) DetectFirstUnitPointer();

        //search
        long CheckThisI = StartIndexItem_V2;
        UnitBuffer = new byte[9];
        CheckThisI -= (0x48 + 0x170) * ScanUnitsNegativeOffset;  //offseting in negative here

        for (int i = 0; i < ScanUnitsNumber; i++)
        {
            if ((i % 2) == 1) CheckThisI += 0x48;
            else CheckThisI += 0x170;

            if (SearchUnitsType == "item")
            {
                ThisCheckbytes = new byte[] { (byte)UnitType.Item, 0x00, 0x00, 0x00, 0xff, 0xff, 0x00, 0x00 };
                Form1_0.Mem_0.ReadRawMemory(CheckThisI, ref UnitBuffer, UnitBuffer.Length);
                unitPatternScan(CheckThisI, "item");
            }

            if (SearchUnitsType == "player")
            {
                ThisCheckbytes = new byte[] { (byte)UnitType.Player, 0x00, 0x00, 0x00, 0xff, 0xff, 0x00, 0x00 };
                Form1_0.Mem_0.ReadRawMemory(CheckThisI, ref UnitBuffer, UnitBuffer.Length);
                unitPatternScan(CheckThisI, "player");
            }

            if (SearchUnitsType == "objects")
            {
                ThisCheckbytes = new byte[] { (byte)UnitType.GameObject, 0x00, 0x00, 0x00, 0xff, 0xff, 0x00, 0x00 };
                Form1_0.Mem_0.ReadRawMemory(CheckThisI, ref UnitBuffer, UnitBuffer.Length);
                unitPatternScan(CheckThisI, "objects");
            }

            if (SearchUnitsType == "NPC")
            {
                ThisCheckbytes = new byte[] { (byte)UnitType.NPC, 0x00, 0x00, 0x00, 0xff, 0xff, 0x00, 0x00 };
                Form1_0.Mem_0.ReadRawMemory(CheckThisI, ref UnitBuffer, UnitBuffer.Length);
                unitPatternScan(CheckThisI, "NPC");
            }
        }
    }

    
    //########################################################################################################################################
    //########################################################################################################################################
    //########################################################################################################################################
    //VERSION 1 UNITS SCAN
    public void IncreaseV1Scanning()
    {
        if (CurrentUnitsIncreaseCount < MaxUnitsIncreaseCount)
        {
            ScanUnitsNumber += 100;
            ScanUnitsNegativeOffset += 25;
            Form1_0.method_1("Units Scan have increased trying to detect more Units, but glitch may happend too!", Color.OrangeRed);
            CurrentUnitsIncreaseCount++;
        }
    }

    public void ResetV1Scanning()
    {
        ScanUnitsNumber = 2600;
        ScanUnitsNegativeOffset = 150;
        CurrentUnitsIncreaseCount = 0;
    }

    public void scanForUnitsPointerV1(string SearchUnitsType)
    {
        long UnitOffset = 0;
        try
        {
            UnitOffset = (long)Form1_0.BaseAddress + (long)Form1_0.offsets["unitTable"] + Form1_0.UnitStrucOffset;
        }
        catch
        {
            return;
        }

        if (SearchUnitsType == "item") AllPossibleItemsPointers = new List<long>();
        if (SearchUnitsType == "player") AllPossiblePlayerPointers = new List<long>();
        if (SearchUnitsType == "objects") AllPossibleObjectsPointers = new List<long>();
        if (SearchUnitsType == "NPC") AllPossibleNPCPointers = new List<long>();

        for (int i = 0; i < ((128 + 516) * 10); i += 8)
        {
            long UnitPointerLocation = Form1_0.Mem_0.ReadInt64Raw((IntPtr)(UnitOffset + i));
            if (UnitPointerLocation > 0)
            {
                uint UnitTypeT = Form1_0.Mem_0.ReadUInt32Raw((IntPtr)(UnitPointerLocation));
                uint TxtFileNoT = Form1_0.Mem_0.ReadUInt32Raw((IntPtr)(UnitPointerLocation + 4));

                // Do ONLY UnitType:4 && TxtFileNo:!3
                if (SearchUnitsType == "item")
                {
                    if (UnitTypeT == (int)UnitType.Item && TxtFileNoT != 3)
                    {
                        AllPossibleItemsPointers.Add(UnitPointerLocation);
                    }
                }
                if (SearchUnitsType == "player")
                {
                    if (UnitTypeT == (int)UnitType.Player)
                    {
                        AllPossiblePlayerPointers.Add(UnitPointerLocation);
                    }
                }
                if (SearchUnitsType == "objects")
                {
                    if (UnitTypeT == (int)UnitType.GameObject)
                    {
                        AllPossibleObjectsPointers.Add(UnitPointerLocation);
                    }
                }
                if (SearchUnitsType == "NPC")
                {
                    if (UnitTypeT == (int)UnitType.NPC)
                    {
                        AllPossibleNPCPointers.Add(UnitPointerLocation);
                    }
                }
            }
        }

        checkForMissingPointers(SearchUnitsType);
        UnitPatternScanV1(SearchUnitsType);
    }

    public void UnitPatternScanV1(string SearchUnitsType)
    {
        if (SearchUnitsType == "item") AllItemsPointers = new Dictionary<long, bool>();
        if (SearchUnitsType == "player") AllPlayersPointers = new Dictionary<long, bool>();
        if (SearchUnitsType == "objects") AllObjectsPointers = new Dictionary<long, bool>();
        if (SearchUnitsType == "NPC") AllNPCPointers = new Dictionary<long, bool>();

        StartIndexItem = long.MaxValue;

        //set start index for searching
        if (SearchUnitsType == "item")
        {
            for (int i = 0; i < AllPossibleItemsPointers.Count; i++)
            {
                if (AllPossibleItemsPointers[i] < StartIndexItem) StartIndexItem = AllPossibleItemsPointers[i];
            }
        }
        if (SearchUnitsType == "player")
        {
            for (int i = 0; i < AllPossiblePlayerPointers.Count; i++)
            {
                if (AllPossiblePlayerPointers[i] < StartIndexItem) StartIndexItem = AllPossiblePlayerPointers[i];
            }
        }
        if (SearchUnitsType == "objects")
        {
            for (int i = 0; i < AllPossibleObjectsPointers.Count; i++)
            {
                if (AllPossibleObjectsPointers[i] < StartIndexItem) StartIndexItem = AllPossibleObjectsPointers[i];
            }
        }
        if (SearchUnitsType == "NPC")
        {
            for (int i = 0; i < AllPossibleNPCPointers.Count; i++)
            {
                if (AllPossibleNPCPointers[i] < StartIndexItem) StartIndexItem = AllPossibleNPCPointers[i];
            }
        }

        //######
        if (StartIndexItem < StartIndexItemLast)
        {
            //0xD87F7298
            //0x31F38
            //0x4830
            if (StartIndexItemLast != long.MaxValue)
            {
                long DiffVal = (StartIndexItemLast - StartIndexItem);
                int UnitNumberDiff = (int)(DiffVal) / (0x48 + 0x170);
                if (DiffVal < 0xFFFFF)  //here
                {
                    ScanUnitsNumber += UnitNumberDiff;
                    Form1_0.method_1("Units start diff: 0x" + (DiffVal).ToString("X") + ", scann for: " + ScanUnitsNumber + " +" + UnitNumberDiff, Color.DarkOrchid);
                }
                else
                {
                    StartIndexItem = StartIndexItemLast; //correct the pointer
                                                         //Form1_0.method_1("BAD Item start diff: 0x" + (DiffVal).ToString("X") + ", scann for: " + ScanUnitsNumber + " +" + UnitNumberDiff, Color.Red);
                }
            }
            StartIndexItemLast = StartIndexItem;
        }
        //######

        //search
        long CheckThisI = StartIndexItem;
        UnitBuffer = new byte[9];
        CheckThisI -= (0x48 + 0x170) * ScanUnitsNegativeOffset;  //offseting in negative here

        //Console.WriteLine("Unit start: 0x" + CheckThisI.ToString("X"));
        //Console.WriteLine("Unit end: 0x" + (CheckThisI + ((0x48 * ScanUnitsNumber / 2)) + ((0x170 * ScanUnitsNumber / 2))).ToString("X"));
        AllScannedPointers = new Dictionary<long, bool>();
        for (int i = 0; i < ScanUnitsNumber; i++)
        {
            if ((i % 2) == 1) CheckThisI += 0x48;
            else CheckThisI += 0x170;

            AllScannedPointers.Add(CheckThisI, true);

            if (SearchUnitsType == "item")
            {
                ThisCheckbytes = new byte[] { (byte)UnitType.Item, 0x00, 0x00, 0x00, 0xff, 0xff, 0x00, 0x00 };
                Form1_0.Mem_0.ReadRawMemory(CheckThisI, ref UnitBuffer, UnitBuffer.Length);
                unitPatternScan(CheckThisI, "item");
            }

            if (SearchUnitsType == "player")
            {
                ThisCheckbytes = new byte[] { (byte)UnitType.Player, 0x00, 0x00, 0x00, 0xff, 0xff, 0x00, 0x00 };
                Form1_0.Mem_0.ReadRawMemory(CheckThisI, ref UnitBuffer, UnitBuffer.Length);
                unitPatternScan(CheckThisI, "player");
            }

            if (SearchUnitsType == "objects")
            {
                ThisCheckbytes = new byte[] { (byte)UnitType.GameObject, 0x00, 0x00, 0x00, 0xff, 0xff, 0x00, 0x00 };
                Form1_0.Mem_0.ReadRawMemory(CheckThisI, ref UnitBuffer, UnitBuffer.Length);
                unitPatternScan(CheckThisI, "objects");
            }

            if (SearchUnitsType == "NPC")
            {
                ThisCheckbytes = new byte[] { (byte)UnitType.NPC, 0x00, 0x00, 0x00, 0xff, 0xff, 0x00, 0x00 };
                Form1_0.Mem_0.ReadRawMemory(CheckThisI, ref UnitBuffer, UnitBuffer.Length);
                unitPatternScan(CheckThisI, "NPC");
            }
        }

        if (SearchUnitsType == "item")
        {
            foreach (var ThisCurrentPointer in AllItemsPointers) 
            {
                CheckThisI = ThisCurrentPointer.Key;

                if (!AllScannedPointers.ContainsKey(CheckThisI))
                {
                    //AllScannedPointers.Add(CheckThisI);
                    //Form1_0.method_1("Missed item pointer: " + CheckThisI.ToString("X"), Color.Red);

                    ThisCheckbytes = new byte[] { (byte)UnitType.Item, 0x00, 0x00, 0x00, 0xff, 0xff, 0x00, 0x00 };
                    Form1_0.Mem_0.ReadRawMemory(CheckThisI, ref UnitBuffer, UnitBuffer.Length);
                    unitPatternScan(CheckThisI, "item");
                }
            }
        }
        if (SearchUnitsType == "player")
        {
            foreach (var ThisCurrentPointer in AllPlayersPointers)
            {
                CheckThisI = ThisCurrentPointer.Key;

                if (!AllScannedPointers.ContainsKey(CheckThisI))
                {
                    //AllScannedPointers.Add(CheckThisI);
                    //Form1_0.method_1("Missed player pointer: " + CheckThisI.ToString("X"), Color.Red);

                    ThisCheckbytes = new byte[] { (byte)UnitType.Player, 0x00, 0x00, 0x00, 0xff, 0xff, 0x00, 0x00 };
                    Form1_0.Mem_0.ReadRawMemory(CheckThisI, ref UnitBuffer, UnitBuffer.Length);
                    unitPatternScan(CheckThisI, "player");
                }
            }
        }
        if (SearchUnitsType == "objects")
        {
            foreach (var ThisCurrentPointer in AllObjectsPointers)
            {
                CheckThisI = ThisCurrentPointer.Key;

                if (!AllScannedPointers.ContainsKey(CheckThisI))
                {
                    //AllScannedPointers.Add(CheckThisI);
                    //Form1_0.method_1("Missed object pointer: " + CheckThisI.ToString("X"), Color.Red);

                    ThisCheckbytes = new byte[] { (byte)UnitType.GameObject, 0x00, 0x00, 0x00, 0xff, 0xff, 0x00, 0x00 };
                    Form1_0.Mem_0.ReadRawMemory(CheckThisI, ref UnitBuffer, UnitBuffer.Length);
                    unitPatternScan(CheckThisI, "objects");
                }
            }
        }
        if (SearchUnitsType == "NPC")
        {
            foreach (var ThisCurrentPointer in AllNPCPointers)
            {
                CheckThisI = ThisCurrentPointer.Key;

                if (!AllScannedPointers.ContainsKey(CheckThisI))
                {
                    //AllScannedPointers.Add(CheckThisI);
                    //Form1_0.method_1("Missed npc pointer: " + CheckThisI.ToString("X"), Color.Red);

                    ThisCheckbytes = new byte[] { (byte)UnitType.NPC, 0x00, 0x00, 0x00, 0xff, 0xff, 0x00, 0x00 };
                    Form1_0.Mem_0.ReadRawMemory(CheckThisI, ref UnitBuffer, UnitBuffer.Length);
                    unitPatternScan(CheckThisI, "NPC");
                }
            }
        }
    }

    public void checkForMissingPointers(string SearchUnitsType)
    {
        if (SearchUnitsType == "item")
        {
            for (int i = 0; i < AllPossibleItemsPointers.Count; i++) AddPointerToList(AllPossibleItemsPointers[i], "item");
        }
        if (SearchUnitsType == "player")
        {
            for (int i = 0; i < AllPossiblePlayerPointers.Count; i++) AddPointerToList(AllPossiblePlayerPointers[i], "player");
        }
        if (SearchUnitsType == "objects")
        {
            for (int i = 0; i < AllPossibleObjectsPointers.Count; i++) AddPointerToList(AllPossibleObjectsPointers[i], "objects");
        }
        if (SearchUnitsType == "NPC")
        {
            for (int i = 0; i < AllPossibleNPCPointers.Count; i++) AddPointerToList(AllPossibleNPCPointers[i], "NPC");
        }
    }

    public void AddPointerToList(long TPoiinter, string SearchType)
    {
        if (SearchType == "item")
        {
            if (AllItemsPointers.Count > 0)
            {
                if (AllItemsPointers.ContainsKey(TPoiinter)) return;
                AllItemsPointers.Add(TPoiinter, true);
            }
            else
            {
                AllItemsPointers.Add(TPoiinter, true);
            }
        }

        if (SearchType == "player")
        {
            if (AllPlayersPointers.Count > 0)
            {
                if (AllPlayersPointers.ContainsKey(TPoiinter)) return;
                AllPlayersPointers.Add(TPoiinter, true);
            }
            else
            {
                AllPlayersPointers.Add(TPoiinter, true);
            }
        }

        if (SearchType == "objects")
        {
            if (AllObjectsPointers.Count > 0)
            {
                if (AllObjectsPointers.ContainsKey(TPoiinter)) return;
                AllObjectsPointers.Add(TPoiinter, true);
            }
            else
            {
                AllObjectsPointers.Add(TPoiinter, true);
            }
        }

        if (SearchType == "NPC")
        {
            if (AllNPCPointers.Count > 0)
            {
                if (AllNPCPointers.ContainsKey(TPoiinter)) return;
                AllNPCPointers.Add(TPoiinter, true);
            }
            else
            {
                AllNPCPointers.Add(TPoiinter, true);
            }
        }
    }
}
