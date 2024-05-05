using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.AxHost;
using static MapAreaStruc;
using static EnumsMobsNPC;
using static EnumsStates;

public class MobsStruc
{
    Form1 Form1_0;
    public long MobsPointerLocation = 0;
    public long LastMobsPointerLocation = 0;
    public string MobsName = "";
    //public byte[] mobsdatastruc = new byte[144];
    public uint txtFileNo = 0;
    public int MobsHP = 0;

    public long pPathPtr = 0;
    private ushort itemx = 0;
    private ushort itemy = 0;
    public ushort xPosFinal = 0;
    public ushort yPosFinal = 0;
    public byte[] pPath = new byte[144];

    public int MobsHPAll = 0;
    public ushort xPosFinalAll = 0;
    public ushort yPosFinalAll = 0;
    public uint statCountAll = 0;
    public long statPtrAll = 0;
    public byte[] statBufferAll = new byte[] { };
    public byte[] pPathAll = new byte[144];
    public long pPathPtrAll = 0;

    public uint statCount = 0;
    //public uint statExCount = 0;
    public long statPtr = 0;
    //public long statExPtr = 0;
    public byte[] pStatB = new byte[180];
    public byte[] statBuffer = new byte[] { };

    public byte[] CurrentPointerBytes = new byte[8];
    public Position LastMobPos = new Position { X = 0, Y = 0 };
    public uint LastMobtxtFileNo = 0;

    List<EnumsStates.State> MobsStates = new List<EnumsStates.State>();
    List<EnumsStates.State> MobsStatesAll = new List<EnumsStates.State>();

    public void SetForm1(Form1 form1_1)
    {
        Form1_0 = form1_1;
    }

    public bool IsThisMobInBound()
    {
        //return true;

        //GetUnitPathData();

        bool[,] ThisCollisionGrid = Form1_0.MapAreaStruc_0.CollisionGrid((Enums.Area)Form1_0.PlayerScan_0.levelNo);

        if (ThisCollisionGrid.GetLength(0) == 0 || ThisCollisionGrid.GetLength(1) == 0) return false;
        if (Form1_0.MapAreaStruc_0.AllMapData.Count == 0) return false;

        int ThisX = xPosFinal - Form1_0.MapAreaStruc_0.AllMapData[(int)Form1_0.PlayerScan_0.levelNo - 1].Offset.X;
        int ThisY = yPosFinal - Form1_0.MapAreaStruc_0.AllMapData[(int)Form1_0.PlayerScan_0.levelNo - 1].Offset.Y;

        //Console.WriteLine(xPosFinal + ", " + yPosFinal);

        if (ThisX < 0) return false;
        if (ThisY < 0) return false;
        if (ThisX > ThisCollisionGrid.GetLength(0) - 1) return false;
        if (ThisY > ThisCollisionGrid.GetLength(1) - 1) return false;

        //Console.WriteLine(ThisX + ", " + ThisY);

        try
        {
            if (ThisCollisionGrid[ThisX, ThisY]) return true;
        }
        catch { }

        return false;
    }

    public bool GetLastMobs()
    {
        //mobsdatastruc = new byte[144];
        //Form1_0.Mem_0.ReadRawMemory(MobsPointerLocation, ref mobsdatastruc, 144);
        //txtFileNo = BitConverter.ToUInt32(mobsdatastruc, 4);

        MobsPointerLocation = LastMobsPointerLocation;

        CurrentPointerBytes = new byte[4];
        Form1_0.Mem_0.ReadRawMemory(MobsPointerLocation + 4, ref CurrentPointerBytes, CurrentPointerBytes.Length);
        txtFileNo = BitConverter.ToUInt32(CurrentPointerBytes, 0);

        GetUnitPathData();
        GetStatsAddr();
        MobsHP = GetHPFromStats();

        if (txtFileNo != LastMobtxtFileNo)
        {
            Form1_0.method_1("Bad Last mob ID!", Color.OrangeRed);

            return false;
        }

        if (xPosFinal == 0 && yPosFinal == 0)
        {
            Form1_0.method_1("Bad Last mob positions!", Color.OrangeRed);
            xPosFinal = (ushort)LastMobPos.X;
            yPosFinal = (ushort)LastMobPos.Y;
        }
        else
        {
            LastMobPos.X = xPosFinal;
            LastMobPos.Y = yPosFinal;
        }
        return true;
    }

    public void GetThisMob(long TPointer)
    {
        MobsPointerLocation = TPointer;
        //mobsdatastruc = new byte[144];
        //Form1_0.Mem_0.ReadRawMemory(MobsPointerLocation, ref mobsdatastruc, 144);
        //txtFileNo = BitConverter.ToUInt32(mobsdatastruc, 4);

        CurrentPointerBytes = new byte[4];
        Form1_0.Mem_0.ReadRawMemory(MobsPointerLocation + 4, ref CurrentPointerBytes, CurrentPointerBytes.Length);
        txtFileNo = BitConverter.ToUInt32(CurrentPointerBytes, 0);

        GetUnitPathData();
        GetStatsAddr();
        MobsHP = GetHPFromStats();
        LastMobPos.X = xPosFinal;
        LastMobPos.Y = yPosFinal;
    }

    public int GetMobsCount(uint ThisMobID)
    {
        int Count = 0;
        try
        {
            Form1_0.PatternsScan_0.scanForUnitsPointer("NPC");
            foreach (var ThisCurrentPointer in Form1_0.PatternsScan_0.AllNPCPointers)
            {
                MobsPointerLocation = ThisCurrentPointer.Key;
                if (MobsPointerLocation > 0)
                {
                    //mobsdatastruc = new byte[144];
                    //Form1_0.Mem_0.ReadRawMemory(MobsPointerLocation, ref mobsdatastruc, 144);
                    //txtFileNo = BitConverter.ToUInt32(mobsdatastruc, 4);
                    //uint FirrstName = txtFileNo;

                    CurrentPointerBytes = new byte[4];
                    Form1_0.Mem_0.ReadRawMemory(MobsPointerLocation + 4, ref CurrentPointerBytes, CurrentPointerBytes.Length);
                    uint txtFileNo2 = BitConverter.ToUInt32(CurrentPointerBytes, 0);

                    //long pStatsListExPtr = BitConverter.ToInt64(mobsdatastruc, 0x88);
                    CurrentPointerBytes = new byte[8];
                    Form1_0.Mem_0.ReadRawMemory(MobsPointerLocation + 0x88, ref CurrentPointerBytes, CurrentPointerBytes.Length);
                    long pStatsListExPtr = BitConverter.ToInt64(CurrentPointerBytes, 0);

                    if ((Form1_0.NPCStruc_0.HideNPC((int)txtFileNo2) == 0 && !DebuggingMobs) || DebuggingMobs)
                    //&& !ShouldBeIgnored(txtFileNo2))
                    {
                        if (Form1_0.NPCStruc_0.getTownNPC((int)txtFileNo2) == "")
                        {
                            bool isPlayerMinion = false;
                            if (getPlayerMinion((int)txtFileNo2) != "") isPlayerMinion = true;
                            else isPlayerMinion = ((Form1_0.Mem_0.ReadUInt32((IntPtr)(pStatsListExPtr + 0xAC8 + 0xc)) & 31) == 1); //is a revive

                            if (!isPlayerMinion)
                            {
                                MobsStatesAll = Form1_0.PlayerScan_0.GetStates(pStatsListExPtr);
                                if (!Form1_0.PlayerScan_0.HasState(EnumsStates.State.Revive, MobsStatesAll))
                                {
                                    GetUnitPathDataAll();
                                    GetStatsAddrAll();
                                    MobsHPAll = GetHPFromStatsAll();

                                    if (xPosFinalAll != 0 && yPosFinalAll != 0)
                                    {
                                        if (MobsHPAll != 0)
                                        {
                                            if (ThisMobID == 0 || (ThisMobID > 0 && ThisMobID == txtFileNo2))
                                            {
                                                Count++;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        catch
        {
            Form1_0.method_1("Couldn't 'GetMobsCount()'", Color.OrangeRed);
        }

        return Count;
    }

    public List<int> monsterIDs = new List<int>();
    public List<int> monsterTypes = new List<int>();

    public List<int[]> GetAllMobsNearby()
    {
        Form1_0.PatternsScan_0.scanForUnitsPointer("NPC");

        List<int[]> monsterPositions2 = new List<int[]>();
        monsterIDs = new List<int>();
        monsterTypes = new List<int>();

        try
        {
            foreach (var ThisCurrentPointer in Form1_0.PatternsScan_0.AllNPCPointers)
            {
                MobsPointerLocation = ThisCurrentPointer.Key;
                if (MobsPointerLocation > 0)
                {
                    //mobsdatastruc = new byte[144];
                    //Form1_0.Mem_0.ReadRawMemory(MobsPointerLocation, ref mobsdatastruc, 144);
                    //txtFileNo = BitConverter.ToUInt32(mobsdatastruc, 4);
                    //uint FirrstName = txtFileNo;

                    CurrentPointerBytes = new byte[4];
                    Form1_0.Mem_0.ReadRawMemory(MobsPointerLocation + 4, ref CurrentPointerBytes, CurrentPointerBytes.Length);
                    uint txtFileNo2 = BitConverter.ToUInt32(CurrentPointerBytes, 0);


                    CurrentPointerBytes = new byte[8];
                    Form1_0.Mem_0.ReadRawMemory(MobsPointerLocation + 0x10, ref CurrentPointerBytes, CurrentPointerBytes.Length);
                    long unitDataPtr = BitConverter.ToInt64(CurrentPointerBytes, 0);
                    byte flag = Form1_0.Mem_0.ReadByteRaw((IntPtr)(unitDataPtr + 0x1A));

                    //long pStatsListExPtr = BitConverter.ToInt64(mobsdatastruc, 0x88);
                    CurrentPointerBytes = new byte[8];
                    Form1_0.Mem_0.ReadRawMemory(MobsPointerLocation + 0x88, ref CurrentPointerBytes, CurrentPointerBytes.Length);
                    long pStatsListExPtr = BitConverter.ToInt64(CurrentPointerBytes, 0);

                    if ((Form1_0.NPCStruc_0.HideNPC((int)txtFileNo2) == 0 && !DebuggingMobs) || DebuggingMobs)
                    //&& !ShouldBeIgnored(txtFileNo2))
                    {
                        if (Form1_0.NPCStruc_0.getTownNPC((int)txtFileNo2) == "")
                        {
                            bool isPlayerMinion = false;
                            if (getPlayerMinion((int)txtFileNo2) != "") isPlayerMinion = true;
                            else isPlayerMinion = ((Form1_0.Mem_0.ReadUInt32((IntPtr)(pStatsListExPtr + 0xAC8 + 0xc)) & 31) == 1); //is a revive

                            if (!isPlayerMinion)
                            {
                                MobsStatesAll = Form1_0.PlayerScan_0.GetStates(pStatsListExPtr);
                                if (!Form1_0.PlayerScan_0.HasState(EnumsStates.State.Revive, MobsStatesAll))
                                {

                                    GetUnitPathDataAll();
                                    GetStatsAddrAll();
                                    MobsHPAll = GetHPFromStatsAll();

                                    if (DebuggingMobs)
                                    {
                                        if ((xPosFinalAll != 0 && yPosFinalAll != 0 && Form1_0.checkBoxShowOnlyValidMobs.Checked) || !Form1_0.checkBoxShowOnlyValidMobs.Checked)
                                        {
                                            Form1_0.AppendTextDebugMobs("ID:" + txtFileNo2 + "(" + (EnumsMobsNPC.MonsterType)((int)txtFileNo2) + ") at:" + xPosFinalAll + ", " + yPosFinalAll + " - HP:" + MobsHPAll + ", Type:" + ((Enums.MonsterType)GetMonsterType(flag)) + Environment.NewLine);
                                        }
                                    }

                                    if (xPosFinalAll != 0 && yPosFinalAll != 0)
                                    {
                                        if (MobsHPAll != 0)
                                        {
                                            monsterPositions2.Add(new int[2] { (int)xPosFinalAll, (int)yPosFinalAll });
                                            monsterIDs.Add((int)txtFileNo2);
                                            monsterTypes.Add(GetMonsterType(flag));
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        catch
        {
            Form1_0.method_1("Couldn't 'GetAllMobsNearby()'", Color.OrangeRed);
        }

        return monsterPositions2;
    }

    public int GetMonsterType(byte typeFlag)
    {
        switch (typeFlag)
        {
            case 10:
                return (int)Enums.MonsterType.SuperUnique;
            case 1 << 2:
                return (int)Enums.MonsterType.Champion;
            case 1 << 3:
                return (int)Enums.MonsterType.Unique;
            case 1 << 4:
                return (int)Enums.MonsterType.Minion;
            case 1 << 5:
                return (int)Enums.MonsterType.Possessed;
            case 1 << 6:
                return (int)Enums.MonsterType.Ghostly;
            case 1 << 7:
                return (int)Enums.MonsterType.Multishot;
            default:
                return (int)Enums.MonsterType.None;
        }
    }

    public bool IsImmune(Enums.StatResist resist)
    {
        if (this.statCount < 100)
        {
            Form1_0.Mem_0.ReadRawMemory(this.statPtr, ref statBuffer, (int)(this.statCount * 10));
            for (int i = 0; i < this.statCount; i++)
            {
                int offset = i * 8;
                short statLayer = BitConverter.ToInt16(statBuffer, offset);
                ushort statEnum = BitConverter.ToUInt16(statBuffer, offset + 0x2);
                int statValue = BitConverter.ToInt32(statBuffer, offset + 0x4);

                if (resist == Enums.StatResist.ColdImmune && (Enums.Attribute)statEnum == Enums.Attribute.ColdResist && statValue > 0)
                {
                    return true;
                }
                if (resist == Enums.StatResist.FireImmune && (Enums.Attribute)statEnum == Enums.Attribute.FireResist && statValue > 0)
                {
                    return true;
                }
                if (resist == Enums.StatResist.LightImmune && (Enums.Attribute)statEnum == Enums.Attribute.LightningResist && statValue > 0)
                {
                    return true;
                }
                if (resist == Enums.StatResist.PoisonImmune && (Enums.Attribute)statEnum == Enums.Attribute.PoisonResist && statValue > 0)
                {
                    return true;
                }
                if (resist == Enums.StatResist.MagicImmune && (Enums.Attribute)statEnum == Enums.Attribute.MagicResist && statValue > 0)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public void GetUnitPathDataAll()
    {
        //pPathPtr = BitConverter.ToInt64(mobsdatastruc, 0x38);
        CurrentPointerBytes = new byte[8];
        Form1_0.Mem_0.ReadRawMemory(MobsPointerLocation + 0x38, ref CurrentPointerBytes, CurrentPointerBytes.Length);
        pPathPtrAll = BitConverter.ToInt64(CurrentPointerBytes, 0);
        //pPath = new byte[144];
        pPathAll = new byte[0x08];
        Form1_0.Mem_0.ReadRawMemory(pPathPtrAll, ref pPathAll, pPathAll.Length);

        ushort itemx2 = BitConverter.ToUInt16(pPathAll, 0x02);
        ushort itemy2 = BitConverter.ToUInt16(pPathAll, 0x06);
        ushort xPosOffset = BitConverter.ToUInt16(pPathAll, 0x00);
        ushort yPosOffset = BitConverter.ToUInt16(pPathAll, 0x04);
        int xPosOffsetPercent = (xPosOffset / 65536); //get percentage
        int yPosOffsetPercent = (yPosOffset / 65536); //get percentage
        xPosFinalAll = (ushort)(itemx2 + xPosOffsetPercent);
        yPosFinalAll = (ushort)(itemy2 + yPosOffsetPercent);
    }

    public void GetStatsAddrAll()
    {
        //long pStatsListExPtr = BitConverter.ToInt64(mobsdatastruc, 0x88);
        CurrentPointerBytes = new byte[8];
        Form1_0.Mem_0.ReadRawMemory(MobsPointerLocation + 0x88, ref CurrentPointerBytes, CurrentPointerBytes.Length);
        try
        {
            long pStatsListExPtr = BitConverter.ToInt64(CurrentPointerBytes, 0);
            statPtrAll = Form1_0.Mem_0.ReadInt64Raw((IntPtr)(pStatsListExPtr + 0x30));
            statCountAll = Form1_0.Mem_0.ReadUInt32Raw((IntPtr)(pStatsListExPtr + 0x38));
        }
        catch { }
    }

    public int GetHPFromStatsAll()
    {
        try
        {
            if (this.statCountAll < 100)
            {
                Form1_0.Mem_0.ReadRawMemory(this.statPtrAll, ref statBufferAll, (int)(this.statCountAll * 10));
                for (int i = 0; i < this.statCountAll; i++)
                {
                    int offset = i * 8;
                    short statLayer = BitConverter.ToInt16(statBufferAll, offset);
                    ushort statEnum = BitConverter.ToUInt16(statBufferAll, offset + 0x2);
                    int statValue = BitConverter.ToInt32(statBufferAll, offset + 0x4);
                    if (statEnum == 6)
                    {
                        return statValue;
                    }
                }
            }
        }
        catch
        {
            Form1_0.method_1("Couldn't get Mob HP from Stats!", Color.OrangeRed);
        }
        return 0; // or some other default value
    }

    public bool DebuggingMobs = false;

    public void DetectThisMob(string MobType, string MobName, bool Nearest, int MaxMobDistance, List<long> IgnoredListPointers)
    {
        bool FoundMob = GetMobs(MobType, MobName, Nearest, MaxMobDistance, IgnoredListPointers);
        int IncreaseCount = 0;
        while (!FoundMob && IncreaseCount < 10)
        {
            Form1_0.PatternsScan_0.IncreaseV1Scanning();
            IncreaseCount++;

            FoundMob = GetMobs(MobType, MobName, Nearest, MaxMobDistance, IgnoredListPointers);
        }
    }

    public bool GetMobs(string MobType, string MobName, bool Nearest, int MaxMobDistance, List<long> IgnoredListPointers)
    {
        try
        {
            txtFileNo = 0;
            MobsName = "";
            Form1_0.PatternsScan_0.scanForUnitsPointer("NPC");
            long NearestMobPointer = 0;
            int LastDiffX = 999;
            int LastDiffY = 999;
            bool GoodMob = false;

            //Set Kill Only the Uniques Mobs
            if (CharConfig.KillOnlySuperUnique && MobType == "" && MobName == ""
                && (Enums.Area)Form1_0.PlayerScan_0.levelNo != Enums.Area.ThroneOfDestruction)
            {
                MobType = "getUniqueName";
            }

            //Set Fast Chaos Only Super Uniques
            if ((Form1_0.Chaos_0.FastChaos || Form1_0.Chaos_0.FastChaosPopingSeals)
                && MobType == "" && MobName == ""
                && (Enums.Area)Form1_0.PlayerScan_0.levelNo == Enums.Area.ChaosSanctuary)
            {
                MobType = "getSuperUniqueName";
            }

            foreach (var ThisCurrentPointer in Form1_0.PatternsScan_0.AllNPCPointers)
            {
                MobsPointerLocation = ThisCurrentPointer.Key;
                if (MobsPointerLocation > 0 && !IsIgnored(IgnoredListPointers))
                {
                    //mobsdatastruc = new byte[144];
                    //Form1_0.Mem_0.ReadRawMemory(MobsPointerLocation, ref mobsdatastruc, 144);
                    //txtFileNo = BitConverter.ToUInt32(mobsdatastruc, 4);

                    CurrentPointerBytes = new byte[4];
                    Form1_0.Mem_0.ReadRawMemory(MobsPointerLocation + 4, ref CurrentPointerBytes, CurrentPointerBytes.Length);
                    txtFileNo = BitConverter.ToUInt32(CurrentPointerBytes, 0);
                    MobsName = ((EnumsMobsNPC.MonsterType)((int)txtFileNo)).ToString();

                    //long pStatsListExPtr = BitConverter.ToInt64(mobsdatastruc, 0x88);
                    CurrentPointerBytes = new byte[8];
                    Form1_0.Mem_0.ReadRawMemory(MobsPointerLocation + 0x88, ref CurrentPointerBytes, CurrentPointerBytes.Length);
                    long pStatsListExPtr = BitConverter.ToInt64(CurrentPointerBytes, 0);

                    if ((Form1_0.NPCStruc_0.HideNPC((int)txtFileNo) == 0 && !DebuggingMobs) || DebuggingMobs)
                    //&& !ShouldBeIgnored(txtFileNo2))
                    {
                        if (Form1_0.NPCStruc_0.getTownNPC((int)txtFileNo) == "")
                        {
                            bool isPlayerMinion = false;
                            if (getPlayerMinion((int)txtFileNo) != "") isPlayerMinion = true;
                            else isPlayerMinion = ((Form1_0.Mem_0.ReadUInt32((IntPtr)(pStatsListExPtr + 0xAC8 + 0xc)) & 31) == 1); //is a revive

                            if (!isPlayerMinion)
                            {
                                MobsStates = Form1_0.PlayerScan_0.GetStates(pStatsListExPtr);
                                if (!Form1_0.PlayerScan_0.HasState(EnumsStates.State.Revive, MobsStates))
                                {
                                    GetUnitPathData();
                                    GetStatsAddr();
                                    int MobHPBuffer = GetHPFromStats();

                                    CurrentPointerBytes = new byte[8];
                                    Form1_0.Mem_0.ReadRawMemory(MobsPointerLocation + 0x10, ref CurrentPointerBytes, CurrentPointerBytes.Length);
                                    long unitDataPtr = BitConverter.ToInt64(CurrentPointerBytes, 0);
                                    byte flag = Form1_0.Mem_0.ReadByteRaw((IntPtr)(unitDataPtr + 0x1A));

                                    /*if (MobType == "getBossName"
                                        && (Enums.MonsterType)GetMonsterType(flag) != Enums.MonsterType.Champion)
                                    {
                                        continue;
                                    }

                                    if (MobType == "getSuperUniqueName"
                                        && (Enums.MonsterType)GetMonsterType(flag) != Enums.MonsterType.Champion
                                        && (Enums.MonsterType)GetMonsterType(flag) != Enums.MonsterType.SuperUnique)
                                    {
                                        continue;
                                    }*/

                                    if ((MobType == "getUniqueName" || MobType == "getSuperUniqueName" || MobType == "getBossName")
                                        && (Enums.MonsterType)GetMonsterType(flag) != Enums.MonsterType.Champion
                                        && (Enums.MonsterType)GetMonsterType(flag) != Enums.MonsterType.SuperUnique
                                        && (Enums.MonsterType)GetMonsterType(flag) != Enums.MonsterType.Unique)
                                    {
                                        continue;
                                    }

                                    //Avoid Immunes
                                    if (CharConfig.AvoidColdImmune
                                        || CharConfig.AvoidFireImmune
                                        || CharConfig.AvoidLightImmune
                                        || CharConfig.AvoidPoisonImmune
                                        || CharConfig.AvoidMagicImmune)
                                    {
                                        if (IsImmune(Enums.StatResist.ColdImmune) && CharConfig.AvoidColdImmune
                                            || IsImmune(Enums.StatResist.FireImmune) && CharConfig.AvoidFireImmune
                                            || IsImmune(Enums.StatResist.LightImmune) && CharConfig.AvoidLightImmune
                                            || IsImmune(Enums.StatResist.PoisonImmune) && CharConfig.AvoidPoisonImmune
                                            || IsImmune(Enums.StatResist.MagicImmune) && CharConfig.AvoidMagicImmune)
                                        {
                                            //Form1_0.method_1(MobName + ", isImmune", Color.Red);
                                            if (MobType != "getBossName" && MobType != "getSuperUniqueName") continue; //go to next mobs
                                        }
                                    }

                                    bool IsMobInBound = true;
                                    if (MobType != "getBossName" && MobType != "getSuperUniqueName") IsMobInBound = IsThisMobInBound();

                                    //Form1_0.method_1(MobName + ", isInBound: " + IsMobInBound + ", HP Buffer: " + MobHPBuffer, Color.Red);

                                    //Console.WriteLine("found near mob " + txtFileNo + " at: " + xPosFinal + ", " + yPosFinal + " HP:" + MobHPBuffer);
                                    if ((MobHPBuffer > 0 || (MobHPBuffer == 0 && MobName != ""))
                                        && (xPosFinal != 0 && yPosFinal != 0)//)
                                        && IsMobInBound)
                                    {
                                        if (CharConfig.LeaveDiabloClone && (EnumsMobsNPC.MonsterType)txtFileNo == EnumsMobsNPC.MonsterType.Diabloclone)
                                        {
                                            Form1_0.method_1("DClone found near, leaving game!", Color.Red);
                                            Form1_0.LeaveGame(false);
                                            Form1_0.WaitDelay(100);
                                            return false;
                                        }

                                        //get nearest mobs in all mobs
                                        if (Nearest)
                                        {
                                            int DiffXPlayer = xPosFinal - Form1_0.PlayerScan_0.xPosFinal;
                                            int DiffYPlayer = yPosFinal - Form1_0.PlayerScan_0.yPosFinal;
                                            if (DiffXPlayer < 0) DiffXPlayer = -DiffXPlayer;
                                            if (DiffYPlayer < 0) DiffYPlayer = -DiffYPlayer;

                                            if (DiffXPlayer <= LastDiffX
                                                && DiffYPlayer <= LastDiffY
                                                && DiffXPlayer <= MaxMobDistance
                                                && DiffYPlayer <= MaxMobDistance)
                                            {
                                                NearestMobPointer = MobsPointerLocation;
                                                LastDiffX = DiffXPlayer;
                                                LastDiffY = DiffYPlayer;
                                            }
                                            xPosFinal = 0;
                                            yPosFinal = 0;
                                        }

                                        if (MobType == "") GoodMob = true;

                                        //get the mobs by name and type
                                        if (MobType == "getBossName")
                                        {
                                            if (getBossName((int)txtFileNo) == MobName)
                                            {
                                                if (!Nearest)
                                                {
                                                    MobsHP = MobHPBuffer;
                                                    LastMobsPointerLocation = MobsPointerLocation;
                                                    LastMobtxtFileNo = txtFileNo;
                                                    LastMobPos.X = xPosFinal;
                                                    LastMobPos.Y = yPosFinal;
                                                    return true;
                                                }
                                                else
                                                {
                                                    GoodMob = true;
                                                }
                                            }
                                        }
                                        if (MobType == "getPlayerMinion")
                                        {
                                            if (getPlayerMinion((int)txtFileNo) == MobName)
                                            {
                                                if (!Nearest)
                                                {
                                                    MobsHP = MobHPBuffer;
                                                    LastMobsPointerLocation = MobsPointerLocation;
                                                    LastMobtxtFileNo = txtFileNo;
                                                    LastMobPos.X = xPosFinal;
                                                    LastMobPos.Y = yPosFinal;
                                                    return true;
                                                }
                                                else
                                                {
                                                    GoodMob = true;
                                                }
                                            }
                                        }
                                        if (MobType == "getSuperUniqueName" || MobType == "getUniqueName")
                                        {
                                            if ((MobName == "" && getSuperUniqueName((int)txtFileNo) != "") || (MobName != "" && getSuperUniqueName((int)txtFileNo) == MobName))
                                            {
                                                //Console.WriteLine(getSuperUniqueName((int)txtFileNo));
                                                if (!Nearest)
                                                {
                                                    MobsHP = MobHPBuffer;
                                                    LastMobsPointerLocation = MobsPointerLocation;
                                                    LastMobtxtFileNo = txtFileNo;
                                                    LastMobPos.X = xPosFinal;
                                                    LastMobPos.Y = yPosFinal;
                                                    return true;
                                                }
                                                else
                                                {
                                                    GoodMob = true;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            //Form1_0.method_1("Nearest: " + Nearest + ", NearestMobPointer: 0x" + NearestMobPointer.ToString("X") + ", GoodMob:" + GoodMob, Color.Red);

            //load nearest mobs
            if (Nearest && NearestMobPointer != 0 && GoodMob)
            {
                MobsPointerLocation = NearestMobPointer;
                //mobsdatastruc = new byte[144];
                //Form1_0.Mem_0.ReadRawMemory(MobsPointerLocation, ref mobsdatastruc, 144);
                //txtFileNo = BitConverter.ToUInt32(mobsdatastruc, 4);

                CurrentPointerBytes = new byte[4];
                Form1_0.Mem_0.ReadRawMemory(MobsPointerLocation + 4, ref CurrentPointerBytes, CurrentPointerBytes.Length);
                txtFileNo = BitConverter.ToUInt32(CurrentPointerBytes, 0);
                MobsName = ((EnumsMobsNPC.MonsterType)((int)txtFileNo)).ToString();

                GetUnitPathData();
                GetStatsAddr();
                MobsHP = GetHPFromStats();

                LastMobsPointerLocation = MobsPointerLocation;
                LastMobtxtFileNo = txtFileNo;
                LastMobPos.X = xPosFinal;
                LastMobPos.Y = yPosFinal;

                return true;
            }
        }
        catch
        {
            Form1_0.method_1("Couldn't 'GetMobs()'", Color.OrangeRed);
        }

        return false;
    }

    public bool IsIgnored(List<long> IgnoredListPointers)
    {
        if (IgnoredListPointers.Count > 0)
        {
            for (int i = 0; i < IgnoredListPointers.Count; i++)
            {
                if (IgnoredListPointers[i] == MobsPointerLocation)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public void GetStatsAddr()
    {
        //long pStatsListExPtr = BitConverter.ToInt64(mobsdatastruc, 0x88);
        CurrentPointerBytes = new byte[8];
        Form1_0.Mem_0.ReadRawMemory(MobsPointerLocation + 0x88, ref CurrentPointerBytes, CurrentPointerBytes.Length);
        try
        {
            long pStatsListExPtr = BitConverter.ToInt64(CurrentPointerBytes, 0);

            /*pStatB = new byte[180];
            Form1_0.Mem_0.ReadRawMemory(pStatsListExPtr, ref pStatB, 180);
            statPtr = BitConverter.ToInt64(pStatB, 0x30);
            statCount = BitConverter.ToUInt32(pStatB, 0x38);
            statExPtr = BitConverter.ToInt64(pStatB, 0x88);
            statExCount = BitConverter.ToUInt32(pStatB, 0x90);*/

            statPtr = Form1_0.Mem_0.ReadInt64Raw((IntPtr)(pStatsListExPtr + 0x30));
            statCount = Form1_0.Mem_0.ReadUInt32Raw((IntPtr)(pStatsListExPtr + 0x38));
            //statExPtr = Form1_0.Mem_0.ReadInt64Raw((IntPtr)(pStatsListExPtr + 0x88));
            //statExCount = Form1_0.Mem_0.ReadUInt32Raw((IntPtr)(pStatsListExPtr + 0x90));

            //string SavePathh = Form1_0.ThisEndPath + "DumpItempStatBStruc";
            //File.Create(SavePathh).Dispose();
            //File.WriteAllBytes(SavePathh, pStatB);
        }
        catch { }
    }

    public void GetUnitPathData()
    {
        //pPathPtr = BitConverter.ToInt64(mobsdatastruc, 0x38);
        CurrentPointerBytes = new byte[8];
        Form1_0.Mem_0.ReadRawMemory(MobsPointerLocation + 0x38, ref CurrentPointerBytes, CurrentPointerBytes.Length);
        pPathPtr = BitConverter.ToInt64(CurrentPointerBytes, 0);
        //pPath = new byte[144];
        pPath = new byte[0x08];
        Form1_0.Mem_0.ReadRawMemory(pPathPtr, ref pPath, pPath.Length);

        itemx = BitConverter.ToUInt16(pPath, 0x02);
        itemy = BitConverter.ToUInt16(pPath, 0x06);
        ushort xPosOffset = BitConverter.ToUInt16(pPath, 0x00);
        ushort yPosOffset = BitConverter.ToUInt16(pPath, 0x04);
        int xPosOffsetPercent = (xPosOffset / 65536); //get percentage
        int yPosOffsetPercent = (yPosOffset / 65536); //get percentage
        xPosFinal = (ushort)(itemx + xPosOffsetPercent);
        yPosFinal = (ushort)(itemy + yPosOffsetPercent);

        //string SavePathh = Form1_0.ThisEndPath + "DumpItempPathStruc";
        //File.Create(SavePathh).Dispose();
        //File.WriteAllBytes(SavePathh, pPath);
    }

    public int GetHPFromStats()
    {
        try
        {
            if (this.statCount < 100)
            {
                Form1_0.Mem_0.ReadRawMemory(this.statPtr, ref statBuffer, (int)(this.statCount * 10));
                for (int i = 0; i < this.statCount; i++)
                {
                    int offset = i * 8;
                    short statLayer = BitConverter.ToInt16(statBuffer, offset);
                    ushort statEnum = BitConverter.ToUInt16(statBuffer, offset + 0x2);
                    int statValue = BitConverter.ToInt32(statBuffer, offset + 0x4);
                    if (statEnum == 6)
                    {
                        //int NewV = 1 << 8;
                        //byte[] RunBuf = new byte[4] { 0x01, 0x00, 0x00, 0x00 };
                        //Form1_0.Mem_0.WriteRawMemory((IntPtr)(this.statPtr + offset + 0x4), RunBuf, 4);

                        return statValue;
                    }
                }
            }

            /*if (this.statExCount < 100)
            {
                Form1_0.Mem_0.ReadRawMemory(this.statExPtr, ref statBuffer, (int)(this.statExCount * 10));
                for (int i = 0; i < this.statExCount; i++)
                {
                    int offset = i * 8;
                    short statLayer = BitConverter.ToInt16(statBuffer, offset);
                    ushort statEnum = BitConverter.ToUInt16(statBuffer, offset + 0x2);
                    int statValue = BitConverter.ToInt32(statBuffer, offset + 0x4);
                    if (statEnum == 6)
                    {
                        //byte[] RunBuf = new byte[4];
                        //Form1_0.Mem_0.WriteRawMemory((IntPtr)(this.statExPtr + offset + 0x4), RunBuf, 4);
                        return statValue;
                    }
                }
            }
            else
            {
                Console.WriteLine("statExCount too long > 100: " + this.statExCount);
            }*/
        }
        catch
        {
            Form1_0.method_1("Couldn't get Mob HP from Stats!", Color.OrangeRed);
        }

        return 0; // or some other default value
    }

    public string getBossName(int txtFileNo)
    {
        switch (txtFileNo)
        {
            case 156: return "Andariel";
            case 211: return "Duriel";
            case 229: return "Radament";
            case 242: return "Mephisto";
            case 243: return "Diablo";
            case 250: return "Summoner";
            case 256: return "Izual";
            case 267: return "Bloodraven";
            case 333: return "Diabloclone";
            case 365: return "Griswold";
            case 526: return "Nihlathak";
            case 544: return "Baal";
            case 570: return "Baalclone";
            case 702: return "BaalThrone";  //543
            case 704: return "Uber Mephisto";
            case 705: return "Uber Diablo";
            case 706: return "Uber Izual";
            case 707: return "Uber Andariel";
            case 708: return "Uber Duriel";
            case 709: return "Uber Baal";
            case 803: return "Nihlathak";
        }
        return "";
    }

    public string getPlayerMinion(int txtFileNo)
    {
        switch (txtFileNo)
        {
            case 271: return "roguehire";
            case 338: return "act2hire";
            case 359: return "act3hire";
            case 560: return "act5hire1";
            case 561: return "act5hire2";
            case 289: return "ClayGolem";
            case 290: return "BloodGolem";
            case 291: return "IronGolem";
            case 292: return "FireGolem";
            case 363: return "NecroSkeleton";
            case 364: return "NecroMage";
            case 417: return "ShadowWarrior";
            case 418: return "ShadowMaster";
            case 419: return "DruidHawk";
            case 420: return "DruidSpiritWolf";
            case 421: return "DruidFenris";
            case 423: return "HeartOfWolverine";
            case 424: return "OakSage";
            case 428: return "DruidBear";
            case 357: return "Valkyrie";
                //case 359: return "IronWolf";
        }
        return "";
    }
    public string getSuperUniqueName(int txtFileNo)
    {
        switch (txtFileNo)
        {
            case 0: return "Bonebreak";
            case 5: return "Corpsefire";
            case 11: return "Pitspawn Fouldog";
            case 20: return "Rakanishu";
            case 24: return "Treehead WoodFist";
            case 31: return "Fire Eye";
            case 45: return "The Countess";
            case 47: return "Sarina the Battlemaid";
            case 62: return "Baal Subject 1";
            case 66: return "Flamespike the Crawler";
            case 75: return "Fangskin";
            case 83: return "Bloodwitch the Wild";
            case 92: return "Beetleburst";
            case 97: return "Leatherarm";
            case 103: return "Ancient Kaa the Soulless";
            case 105: return "Baal Subject 2";
            case 120: return "The Tormentor";
            case 125: return "Web Mage the Burning";
            case 129: return "Stormtree";
            case 138: return "Icehawk Riftwing";
            case 160: return "Coldcrow";
            case 276: return "Boneash";
            case 281: return "Witch Doctor Endugu";
            case 284: return "Coldworm the Burrower";
            case 299: return "Taintbreeder";
            case 306: return "Grand Vizier of Chaos";
            case 308: return "Riftwraith the Cannibal";
            case 312: return "Lord De Seis";
            case 310: return "Infector of Souls";
            case 345: return "Council Member";
            case 346: return "Council Member";
            case 347: return "Council Member";
            case 362: return "Winged Death";
            case 402: return "The Smith";
            case 409: return "The Feature Creep";
            case 437: return "Bonesaw Breaker";
            case 440: return "Pindleskin";
            case 443: return "Threash Socket";
            case 449: return "Frozenstein";
            case 453: return "Eldritch";  //Eldritch //Megaflow Rectifier
            case 472: return "Anodized Elite";
            case 475: return "Vinvear Molech";
            case 479: return "Shenk";  //Overseer //Shenk  //Siege Boss
            case 481: return "Sharp Tooth Sayer";
            case 494: return "Dac Farren";
            case 496: return "Magma Torquer";
            case 501: return "Snapchip Shatter";
            case 508: return "Axe Dweller";
            case 529: return "Eyeback Unleashed";
            case 533: return "Blaze Ripper";
            case 540: return "Ancient Barbarian 1";
            case 541: return "Ancient Barbarian 2";
            case 542: return "Ancient Barbarian 3";
            case 557: return "Baal Subject 3";
            case 558: return "Baal Subject 4";
            case 571: return "Baal Subject 5";
            case 735: return "The Cow King";
            case 736: return "Dark Elder";
            case 803: return "Nihlathak";
        }
        return "";
    }
}
