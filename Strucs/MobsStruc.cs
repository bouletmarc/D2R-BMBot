using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.AxHost;

namespace app
{
    public class MobsStruc
    {
        Form1 Form1_0;
        public long MobsPointerLocation = 0;
        public string MobsName = "";
        public byte[] mobsdatastruc = new byte[144];
        public uint txtFileNo = 0;
        public int MobsHP = 0;

        public long pPathPtr = 0;
        public ushort itemx = 0;
        public ushort itemy = 0;
        public ushort xPosFinal = 0;
        public ushort yPosFinal = 0;
        public byte[] pPath = new byte[144];

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

        public void GetLastMobs()
        {
            mobsdatastruc = new byte[144];
            Form1_0.Mem_0.ReadRawMemory(MobsPointerLocation, ref mobsdatastruc, 144);
            txtFileNo = BitConverter.ToUInt32(mobsdatastruc, 4);
            GetUnitPathData();
            GetStatsAddr();
            MobsHP = GetHPFromStats();
        }

        public bool GetMobs(string MobType, string MobName, bool Nearest, int MaxMobDistance, List<long> IgnoredListPointers)
        {
            txtFileNo = 0;
            MobsName = "";
            Form1_0.PatternsScan_0.scanForUnitsPointer("NPC");
            long NearestMobPointer = 0;
            int LastDiffX = 999;
            int LastDiffY = 999;

            for (int i = 0; i < Form1_0.PatternsScan_0.AllNPCPointers.Count; i++)
            {
                MobsPointerLocation = Form1_0.PatternsScan_0.AllNPCPointers[i];
                if (MobsPointerLocation > 0 && !IsIgnored(IgnoredListPointers))
                {
                    mobsdatastruc = new byte[144];
                    Form1_0.Mem_0.ReadRawMemory(MobsPointerLocation, ref mobsdatastruc, 144);
                    txtFileNo = BitConverter.ToUInt32(mobsdatastruc, 4);
                    long pStatsListExPtr = BitConverter.ToInt64(mobsdatastruc, 0x88);

                    bool isPlayerMinion = false;
                    string playerMinion = getPlayerMinion((int) txtFileNo);
                    if (playerMinion != "")
                    {
                        isPlayerMinion = true;
                    }
                    else
                    {
                        //; is a revive
                        isPlayerMinion = ((Form1_0.Mem_0.ReadUInt32((IntPtr)(pStatsListExPtr + 0xAC8 + 0xc)) & 31) == 1);
                    }

                    if (Form1_0.NPCStruc_0.HideNPC((int) txtFileNo) == 0
                        && Form1_0.NPCStruc_0.getTownNPC((int)txtFileNo) == ""
                        && !isPlayerMinion)
                    {
                        GetUnitPathData();
                        GetStatsAddr();
                        MobsHP = GetHPFromStats();

                        //byte[] RunBuf = BitConverter.GetBytes(709);
                        //Form1_0.Mem_0.WriteRawMemory((IntPtr)(MobsPointerLocation + 0x04), RunBuf, 4);

                        if (MobsHP > 0 || (MobsHP == 0 && MobName != ""))
                        {
                            //get nearest mobs in all mobs
                            if (Nearest)
                            {
                                //Console.WriteLine("found near mob" + txtFileNo + " at: " + itemx + ", " + itemy);
                                if (xPosFinal != 0 && yPosFinal != 0)
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
                                }
                            }
                            //get the mobs by name and type
                            if (MobType == "getBossName")
                            {
                                if (getBossName((int)txtFileNo) == MobName)
                                {
                                    if (!Nearest)
                                    {
                                        return true;
                                    }
                                }
                            }
                            if (MobType == "getPlayerMinion")
                            {
                                if (getPlayerMinion((int)txtFileNo) == MobName)
                                {
                                    if (!Nearest)
                                    {
                                        return true;
                                    }
                                }
                            }
                            if (MobType == "getSuperUniqueName")
                            {
                                if (getSuperUniqueName((int)txtFileNo) == MobName)
                                {
                                    if (!Nearest)
                                    {
                                        return true;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            //load nearest mobs
            if (Nearest && NearestMobPointer != 0)
            {
                MobsPointerLocation = NearestMobPointer;
                mobsdatastruc = new byte[144];
                Form1_0.Mem_0.ReadRawMemory(MobsPointerLocation, ref mobsdatastruc, 144);
                txtFileNo = BitConverter.ToUInt32(mobsdatastruc, 4);
                GetUnitPathData();
                GetStatsAddr();
                MobsHP = GetHPFromStats();
                return true;
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
            long pStatsListExPtr = BitConverter.ToInt64(mobsdatastruc, 0x88);

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
            pPathPtr = BitConverter.ToInt64(mobsdatastruc, 0x38);
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
                Form1_0.Mem_0.ReadRawMemory(this.statPtr, ref statBuffer, (int)(this.statCount * 10));
                for (int i = 0; i < this.statCount; i++)
                {
                    int offset = i * 8;
                    short statLayer = BitConverter.ToInt16(statBuffer, offset);
                    ushort statEnum = BitConverter.ToUInt16(statBuffer, offset + 0x2);
                    int statValue = BitConverter.ToInt32(statBuffer, offset + 0x4);
                    if (statEnum == 6)
                    {
                        //byte[] RunBuf = new byte[4];
                        //Form1_0.Mem_0.WriteRawMemory((IntPtr)(this.statPtr + offset + 0x4), RunBuf, 4);
                        return statValue;
                    }
                }

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
            catch { }

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
                case 704: return "Uber Mephisto";
                case 705: return "Uber Diablo";
                case 706: return "Uber Izual";
                case 707: return "Uber Andariel";
                case 708: return "Uber Duriel";
                case 709: return "Uber Baal";
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
                // case 345: return "Council Member";
                // case 346: return "Council Member";
                // case 347: return "Council Member";
                case 362: return "Winged Death";
                case 402: return "The Smith";
                case 409: return "The Feature Creep";
                case 437: return "Bonesaw Breaker";
                case 440: return "Pindleskin";
                case 443: return "Threash Socket";
                case 449: return "Frozenstein";
                case 453: return "Megaflow Rectifier";
                case 472: return "Anodized Elite";
                case 475: return "Vinvear Molech";
                case 479: return "Siege Boss";
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
            }
            return "";
        }
    }
}
