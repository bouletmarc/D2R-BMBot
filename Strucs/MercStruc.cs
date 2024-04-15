using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static app.EnumsMobsNPC;
using static app.EnumsStates;
using static app.MapAreaStruc;
using static System.Windows.Forms.AxHost;

namespace app
{
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

        public bool IsMerc(int MobNameID)
        {
            return MobNameID == (int) EnumsMobsNPC.MobsNPC.Guard || MobNameID == (int) EnumsMobsNPC.MobsNPC.Act5Hireling1Hand || MobNameID == (int) EnumsMobsNPC.MobsNPC.Act5Hireling2Hand || MobNameID == (int) EnumsMobsNPC.MobsNPC.IronWolf || MobNameID == (int) EnumsMobsNPC.MobsNPC.Rogue2;
        }

        public bool GetMercInfos()
        {
            MercAlive = true;
            txtFileNo = 0;
            Form1_0.PatternsScan_0.scanForUnitsPointer("NPC");

            for (int i = 0; i < Form1_0.PatternsScan_0.AllNPCPointers.Count; i++)
            {
                MercPointerLocation = Form1_0.PatternsScan_0.AllNPCPointers[i];
                if (MercPointerLocation > 0)
                {
                    Mercdatastruc = new byte[144];
                    Form1_0.Mem_0.ReadRawMemory(MercPointerLocation, ref Mercdatastruc, 144);

                    txtFileNo = BitConverter.ToUInt32(Mercdatastruc, 4);
                    pUnitData = BitConverter.ToInt64(Mercdatastruc, 0x10);
                    mode = BitConverter.ToUInt32(Mercdatastruc, 0x0c);
                    ushort isUnique = Form1_0.Mem_0.ReadUInt16Raw((IntPtr) pUnitData + 0x18);
                    GetUnitPathData();
                    GetStatsAddr();

                    bool isPlayerMinion = false;
                    string playerMinion = Form1_0.MobsStruc_0.getPlayerMinion((int)txtFileNo);
                    if (playerMinion != "") isPlayerMinion = true;

                    //Console.WriteLine(Form1_0.NPCStruc_0.getNPC_ID((int) txtFileNo));
                    //Console.WriteLine(txtFileNo.ToString() + ", isUnique:" + isUnique + ", isPlayerMinion:" + isPlayerMinion + ", mode:" + mode + ", pos:" + xPosFinal + ", " + yPosFinal);

                    //if (IsMerc((int) txtFileNo))
                    if (isUnique == 0 && isPlayerMinion && mode != 0 && mode != 12)
                    {
                        if (xPosFinal != 0 && yPosFinal != 0)
                        {
                            SetHPFromStats();
                            Form1_0.Grid_SetInfos("Merc", MercHP.ToString() + "/" + MercMaxHP.ToString());
                            return true;
                        }
                    }
                }
            }

            Form1_0.Grid_SetInfos("Merc", "Not alive/detected");
            MercAlive = false;
            return false;
        }

        public void SetHPFromStats()
        {
            try
            {
                MercHP = 100;
                MercMaxHP = 100;

                int ThisHPStat = 0;
                Form1_0.Mem_0.ReadRawMemory(this.statPtr, ref statBuffer, (int)(this.statCount * 10));
                for (int i = (int) this.statCount - 1; i  >= 0; i--)
                {
                    int offset = i * 8;
                    //short statLayer = BitConverter.ToInt16(statBuffer, offset);
                    ushort statEnum = BitConverter.ToUInt16(statBuffer, offset + 0x2);
                    int statValue = BitConverter.ToInt32(statBuffer, offset + 0x4);
                    if (statEnum == (ushort)Enums.Attribute.Life)
                    {
                        ThisHPStat = statValue;
                        
                    }
                    if (statEnum == (ushort)Enums.Attribute.LifeMax)
                    {
                        MercMaxHP = statValue >> 8;
                    }
                }

                if (ThisHPStat <= 32768)
                {
                    MercHP = ThisHPStat / 32768 * MercMaxHP;
                }
                else
                {
                    MercHP = ThisHPStat >> 8;
                }

                //Console.WriteLine("HP:" + MercHP);

                /*Form1_0.Mem_0.ReadRawMemory(this.statExPtr, ref statBuffer, (int)(this.statExCount * 10));
                for (int i = 0; i < this.statExCount; i++)
                {
                    int offset = i * 8;
                    //short statLayer = BitConverter.ToInt16(statBuffer, offset);
                    ushort statEnum = BitConverter.ToUInt16(statBuffer, offset + 0x2);
                    int statValue = BitConverter.ToInt32(statBuffer, offset + 0x4);
                    if (statEnum == (ushort)Enums.Attribute.Life)
                    {
                        if (MercHP == 100) MercHP = statValue >> 8;
                    }
                    if (statEnum == (ushort)Enums.Attribute.LifeMax)
                    {
                        if (MercMaxHP == 100) MercMaxHP = statValue >> 8;
                    }
                }*/
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

            //string SavePathh = Form1_0.ThisEndPath + "DumpItempStatBStruc";
            //File.Create(SavePathh).Dispose();
            //File.WriteAllBytes(SavePathh, pStatB);
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

            //string SavePathh = Form1_0.ThisEndPath + "DumpItempPathStruc";
            //File.Create(SavePathh).Dispose();
            //File.WriteAllBytes(SavePathh, pPath);
        }
    }
}
