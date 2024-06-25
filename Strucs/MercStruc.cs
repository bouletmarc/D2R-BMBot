using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static EnumsMobsNPC;
using static EnumsStates;
using static MapAreaStruc;
using static System.Windows.Forms.AxHost;
using System.Windows.Forms;

public class MercStruc
{
    Form1 Form1_0;

    public long MercPointerLocation = 0;
    public byte[] Mercdatastruc = new byte[144];
    public uint txtFileNo = 0;
    public long pPathPtr = 0;
    public long pUnitData = 0;
    public uint mode = 0;
    public ushort itemx = 0;
    public ushort itemy = 0;
    public ushort xPosFinal = 0;
    public ushort yPosFinal = 0;
    public byte[] pPath = new byte[144];
    public bool MercAlive = false;
    public int MercHP = 0;
    public int MercMaxHP = 0;

    public uint MercOwnerID = 0; //set within ItemStrus (equipped item on merc)

    public uint statCount = 0;
    public uint statExCount = 0;
    public long statPtr = 0;
    public long statExPtr = 0;
    public byte[] pStatB = new byte[180];
    public byte[] statBuffer = new byte[] { };


    public void SetForm1(Form1 form1_1)
    {
        Form1_0 = form1_1;

    }

    public bool IsMerc(string MobNameID)
    {
        return MobNameID == "Guard" || MobNameID == "Act5Hireling1Hand" || MobNameID == "Act5Hireling2Hand" || MobNameID == "IronWolf" || MobNameID == "Rogue2";
    }

    public string GetName(int MobNameID)
    {
        return Enum.GetName(typeof(MonsterType), MobNameID);
    }
    public bool GetMercInfos()
    {
        int MercCount = 0;
        try
        {
            txtFileNo = 0;
            Form1_0.PatternsScan_0.scanForUnitsPointer("NPC");





            foreach (var ThisCurrentPointer in Form1_0.PatternsScan_0.AllNPCPointers)
            {
                MercPointerLocation = ThisCurrentPointer.Key;
                if (MercPointerLocation > 0)
                {
                    Mercdatastruc = new byte[144];
                    Form1_0.Mem_0.ReadRawMemory(MercPointerLocation, ref Mercdatastruc, 144);
                    var txtFileNo = Form1_0.Mem_0.ReadUIntFromBuffer(Mercdatastruc, 0x04, 4);
                    string monstername = Enum.GetName(typeof(MonsterType), txtFileNo);
                    var mercID = Form1_0.Mem_0.ReadUIntFromBuffer(Mercdatastruc, 0x08, 4);
                    if (IsMerc(monstername))
                    {

                        UIntPtr statsListExPtr = (UIntPtr)BitConverter.ToUInt64(Mercdatastruc, 0x88);

                        // Read statPtr
                        UIntPtr statPtr = (UIntPtr)Form1_0.Mem_0.ReadUInt64((UIntPtr)(statsListExPtr + 0x30));

                        // Read statCount
                        UInt64 statCount = Form1_0.Mem_0.ReadUInt64(statsListExPtr + 0x38);
                        Dictionary<Enums.Attribute, int> mercStats = Form1_0.Mem_0.GetMonsterStats((uint)statCount, statPtr);
                        if (mercStats.ContainsKey(Enums.Attribute.Life)){
                            if (mercStats[Enums.Attribute.Life] > 0) // so not match deadcorpse
                            {
                                int maxLife = mercStats[Enums.Attribute.LifeMax] >> 8;
                                double life = mercStats[Enums.Attribute.Life] >> 8;
                                if (life > 0) MercAlive = true;
                                if (mercStats[Enums.Attribute.Life] <= 32768)
                                {
                                    life = (double)mercStats[Enums.Attribute.Life] / 32768.0 * maxLife;
                                }
                                MercHP = (int)life;
                                MercMaxHP = (int)maxLife;
                                MercCount++;
                                // show % until we figure out how to show merc life + bonus life, not just base life
                                Form1_0.Grid_SetInfos("Merc", $"{100 * life / maxLife}% HP");
                                return true;
                            }
                        }
                    }
                    else
                    {
                        continue;
                    }


                }
            }
        }
        catch (Exception e)
        {
            Form1_0.method_1($"mercerror is {e}", Color.Black);
            Form1_0.WaitDelay(150);
        // couldnt get mercinfo
}

        Form1_0.Grid_SetInfos("Merc", "Not alive/detected");
        if (MercCount == 0) MercAlive = false;
        return false;
    }

    public void SetHPFromStats()
    {
        try
        {


            Form1_0.Mem_0.ReadRawMemory(this.statPtr, ref statBuffer, (int)(this.statCount * 10));
            for (int i = (int)this.statCount - 1; i >= 0; i--)
            {
                int offset = i * 8;
                //short statLayer = BitConverter.ToInt16(statBuffer, offset);
                ushort statEnum = BitConverter.ToUInt16(statBuffer, offset + 0x2);
                int statValue = BitConverter.ToInt32(statBuffer, offset + 0x4);
                if (statEnum == (ushort)Enums.Attribute.Life)
                {
                    if (MercHP >= 32768)
                    { MercHP = statValue >> 8; }
                    else
                    {
                        MercHP = statValue;
                    }


                }
                /*if (statEnum == (ushort)Enums.Attribute.LifeMax)
                {
                    MercMaxHP = statValue >> 8;
                }*/

                //Console.WriteLine(((Enums.Attribute) statEnum).ToString() + " = " + statValue);
            }

            //if (MercMaxHP < MercHP) MercMaxHP = MercHP;
            /*if (ThisHPStat <= 32768)
            {
                MercHP = ThisHPStat / 32768 * MercMaxHP;
            }
            else
            {
                MercHP = ThisHPStat >> 8;
            }*/

            //Console.WriteLine("HP:" + MercHP);
        }
        catch { }
    }

    public void GetStatsAddr()
    {
        long pStatsListExPtr = BitConverter.ToInt64(Mercdatastruc, 0x88);

        /*pStatB = new byte[180];
        Form1_0.Mem_0.ReadRawMemory(pStatsListExPtr, ref pStatB, 180);
        statPtr = BitConverter.ToInt64(pStatB, 0x30);
        statCount = BitConverter.ToUInt32(pStatB, 0x38);
        statExPtr = BitConverter.ToInt64(pStatB, 0x88);
        statExCount = BitConverter.ToUInt32(pStatB, 0x90);*/

        statPtr = Form1_0.Mem_0.ReadInt64Raw((IntPtr)(pStatsListExPtr + 0x30));
        statCount = Form1_0.Mem_0.ReadUInt32Raw((IntPtr)(pStatsListExPtr + 0x38));
        statExPtr = Form1_0.Mem_0.ReadInt64Raw((IntPtr)(pStatsListExPtr + 0x88));
        statExCount = Form1_0.Mem_0.ReadUInt32Raw((IntPtr)(pStatsListExPtr + 0x90));
    }

    public void GetUnitPathData()
    {
        pPathPtr = BitConverter.ToInt64(Mercdatastruc, 0x38);
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
    }
}
