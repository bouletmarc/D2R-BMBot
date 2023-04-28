using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Remoting;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Runtime.InteropServices.ComTypes.IStream;
using static System.Windows.Forms.AxHost;

namespace app
{
    public class PlayerScan
    {
        Form1 Form1_0;

        public long PlayerPointer = 0;
        public long PlayerNamePointer = 0;
        public long actAddress = 0;
        public long mapSeedAddress = 0;
        public Int64 pathAddress = 0;
        public bool FoundPlayer = false;

        public long pAct = 0;
        public uint mapSeed = 0;
        public ushort xPos = 0;
        public ushort yPos = 0;
        public ushort xPosOffset = 0;
        public ushort yPosOffset = 0;
        public int xPosOffsetPercent = 0;
        public int yPosOffsetPercent = 0;
        public ushort xPosFinal = 0;
        public ushort yPosFinal = 0;
        public string pName = "";
        public long pRoom1Address = 0;
        public long pRoom2Address = 0;
        public long pLevelAddress = 0;
        public long levelNo = 0;
        public long pStatsListEx = 0;
        public long statPtr = 0;
        public long statCount = 0;

        public long PlayerGoldInventory = 0;
        public long PlayerGoldInStash = 0;
        public long PlayerHP = 0;
        public long PlayerMaxHP = 0;
        public long PlayerMana = 0;
        public long PlayerMaxMana = 0;
        public ushort difficulty = 0;
        public uint lastdwInitSeedHash1 = 0;
        public uint lastdwInitSeedHash2 = 0;
        public string sFile = "";
        public int PlayerSkillLeft = 0;
        public int PlayerSkillRight = 0;

        public int PlayerStrucCount = 0;
        public uint unitId = 0;
        public uint LeechPlayerUnitID = 0;
        public long LeechPlayerPointer = 0;
        public int LeechPosX = 0;
        public int LeechPosY = 0;
        public long LeechlevelNo = 0;

        public ushort xPosFinalOtherP = 0;
        public ushort yPosFinalOtherP = 0;
        public bool IsCorpse = false;
        public string pNameOther = "";
        public uint unitIdOther = 0;

        public int HPFromEquippedItems = 0;
        public int ManaFromEquippedItems = 0;
        public int VitalityFromEquippedItems = 0;
        public int EnergyFromEquippedItems = 0;
        public int HPPercentFromEquippedItems = 0;
        public int ManaPercentFromEquippedItems = 0;

        public int[] RoomExit = new int[2];

        // REQUIRED METHODS
        //[DllImport("checkmem.dll")]
        //public static extern uint get_seed(uint InitSeedHash1, uint InitSeedHash2, uint EndSeedHash1);

        public void SetForm1(Form1 form1_1)
        {
            Form1_0 = form1_1;
        }

        public void GetPositions()
        {
            pathAddress = Form1_0.Mem_0.ReadInt64Raw((IntPtr) (PlayerPointer + 0x38));
            xPos = Form1_0.Mem_0.ReadUInt16Raw((IntPtr) (pathAddress + 0x02));
            yPos = Form1_0.Mem_0.ReadUInt16Raw((IntPtr) (pathAddress + 0x06));
            xPosOffset = Form1_0.Mem_0.ReadUInt16Raw((IntPtr) (pathAddress + 0x00));
            yPosOffset = Form1_0.Mem_0.ReadUInt16Raw((IntPtr) (pathAddress + 0x04));
            xPosOffsetPercent = (xPosOffset / 65536); //get percentage
            yPosOffsetPercent = (yPosOffset / 65536); //get percentage
            xPosFinal = (ushort)(xPos + xPosOffsetPercent);
            yPosFinal = (ushort)(yPos + yPosOffsetPercent);

            pStatsListEx = Form1_0.Mem_0.ReadInt64Raw((IntPtr)(PlayerPointer + 0x88));
            statPtr = Form1_0.Mem_0.ReadInt64Raw((IntPtr)(pStatsListEx + 0x30));
            statCount = Form1_0.Mem_0.ReadInt32Raw((IntPtr)(pStatsListEx + 0x38));

            byte[] buffer = new byte[statCount * 8];
            Form1_0.Mem_0.ReadRawMemory(statPtr + 0x2, ref buffer, (int) (statCount * 8));

            for (int i = 0; i < statCount; i++) 
            {
                int offset = i * 8;
                ushort statEnum = BitConverter.ToUInt16(buffer, offset);
                uint statValue = BitConverter.ToUInt32(buffer, offset + 0x2);

                if (statEnum == (ushort) Enums.Attribute.Life)
                {
                    PlayerHP = statValue >> 8;
                }
                if (statEnum == (ushort) Enums.Attribute.Mana)
                {
                    PlayerMana = statValue >> 8;
                }
                if (statEnum == (ushort)Enums.Attribute.GoldInPlayer)
                {
                    PlayerGoldInventory = statValue;
                }
                if (statEnum == (ushort)Enums.Attribute.GoldInStash)
                {
                    PlayerGoldInStash = statValue;
                }
            }

            //; get the level number
            pRoom1Address = Form1_0.Mem_0.ReadInt64Raw((IntPtr)(pathAddress + 0x20));
            pRoom2Address = Form1_0.Mem_0.ReadInt64Raw((IntPtr)(pRoom1Address + 0x18));
            pLevelAddress = Form1_0.Mem_0.ReadInt64Raw((IntPtr)(pRoom2Address + 0x90));
            levelNo = Form1_0.Mem_0.ReadUInt32Raw((IntPtr)(pLevelAddress + 0x1F8));

            RoomExit[0] = Form1_0.Mem_0.ReadUInt16Raw((IntPtr)(pRoom1Address + 0x10));
            RoomExit[1] = Form1_0.Mem_0.ReadUInt16Raw((IntPtr)(pRoom1Address + 0x14));

            //; get/check for bad pointer
            if (levelNo == 0 && xPosFinal == 0 && yPosFinal == 0)
            {
                Form1_0.HasPointers = false;
            }

            //; get the difficulty
            actAddress = Form1_0.Mem_0.ReadInt64Raw((IntPtr)(PlayerPointer + 0x20));
            //mapSeedAddress = actAddress + 0x1C;
            //mapSeed = Form1_0.Mem_0.ReadUInt32Raw((IntPtr)mapSeedAddress);
            long aActUnk2 = Form1_0.Mem_0.ReadInt64Raw((IntPtr)(actAddress + 0x78));
            difficulty = Form1_0.Mem_0.ReadUInt16Raw((IntPtr)(aActUnk2 + 0x830));

            //; get the map seed
            /*long actMiscAddress = Form1_0.Mem_0.ReadInt64Raw((IntPtr)(actAddress + 0x78)); //0x0000023a64ed4780; 2449824630656
            uint dwInitSeedHash1 = Form1_0.Mem_0.ReadUInt32((IntPtr)(actMiscAddress + 0x840));
            uint dwInitSeedHash2 = Form1_0.Mem_0.ReadUInt32((IntPtr)(actMiscAddress + 0x844));
            uint dwEndSeedHash1 = Form1_0.Mem_0.ReadUInt32((IntPtr)(actMiscAddress + 0x868));

            if (dwInitSeedHash1 != lastdwInitSeedHash1 || dwInitSeedHash2 != lastdwInitSeedHash2 || mapSeed == 0)
            {
                mapSeed = calculateMapSeed(dwInitSeedHash1, dwInitSeedHash2, dwEndSeedHash1);
                lastdwInitSeedHash1 = dwInitSeedHash1;
                lastdwInitSeedHash2 = dwInitSeedHash2;
            }*/
            //; mapSeed = d2rprocess.read(actMiscAddress + 0x840, "UInt")

            //get player name
            PlayerNamePointer = Form1_0.Mem_0.ReadInt64Raw((IntPtr)(PlayerPointer + 0x10));
            pName = Form1_0.Mem_0.ReadMemString(PlayerNamePointer);
            //Form1_0.Potions_0.CheckHPAndManaMax(); //not used here

            Form1_0.Grid_SetInfos("Cords", xPosFinal + "," + yPosFinal);
            Form1_0.Grid_SetInfos("Life", PlayerHP + "/" + PlayerMaxHP);
            Form1_0.Grid_SetInfos("Mana", PlayerMana + "/" + PlayerMaxMana);
            Form1_0.Grid_SetInfos("Map Level", levelNo.ToString());
            //Form1_0.Grid_SetInfos("Room Exit", RoomExit[0].ToString() + ", " + RoomExit[1].ToString());
            //Form1_0.Grid_SetInfos("Seed", mapSeed.ToString());
            //Form1_0.Grid_SetInfos("Difficulty", difficulty.ToString());

            //Form1_0.method_1("URL: " + GetImageURL());
            //DownloadImage(GetImageURL());
        }

        public void SetMaxHPAndMana()
        {
            byte[] buffer = new byte[statCount * 8];
            Form1_0.Mem_0.ReadRawMemory(statPtr + 0x2, ref buffer, (int)(statCount * 8));

            for (int i = 0; i < statCount; i++)
            {
                int offset = i * 8;
                ushort statEnum = BitConverter.ToUInt16(buffer, offset);
                uint statValue = BitConverter.ToUInt32(buffer, offset + 0x2);

                if (statEnum == (ushort)Enums.Attribute.LifeMax)
                {
                    PlayerMaxHP = (statValue >> 8);
                }
                if (statEnum == (ushort)Enums.Attribute.ManaMax)
                {
                    PlayerMaxMana = (statValue >> 8);
                }
            }

            //Console.WriteLine("added mana percent: " + ManaPercentFromEquippedItems);
            double PercentHPAdd = Math.Ceiling(((double)HPPercentFromEquippedItems * (double)PlayerMaxHP) / 100);
            double PercentManaAdd = Math.Ceiling(((double)ManaPercentFromEquippedItems * (double)PlayerMaxMana) / 100);
            PlayerMaxHP = PlayerMaxHP + HPFromEquippedItems + (VitalityFromEquippedItems * 2) + (int)PercentHPAdd;         //might be only for paladin
            PlayerMaxMana = PlayerMaxMana + ManaFromEquippedItems + (EnergyFromEquippedItems / 2) + (int)PercentManaAdd;   //might be only for paladin

            if (PlayerHP > PlayerMaxHP) PlayerMaxHP = PlayerHP;
            if (PlayerMana > PlayerMaxMana) PlayerMaxMana = PlayerMana;
        }

        public bool HasAnyPlayerInArea(int ThisArea)
        {
            long UnitOffset = (long)Form1_0.BaseAddress + (long)Form1_0.offsets["rosterOffset"];
            long partyStruct = Form1_0.Mem_0.ReadInt64Raw((IntPtr)UnitOffset);

            for (int i = 0; i < 8; i++)
            {
                ushort RosterlevelNo = Form1_0.Mem_0.ReadUInt16Raw((IntPtr)(partyStruct + 0x5C));
                if (RosterlevelNo == ThisArea)
                {
                    return true;
                }

                partyStruct = Form1_0.Mem_0.ReadInt64Raw((IntPtr)(partyStruct + 0x148));
            }

            return false;
        }

        public void GetLeechPositions()
        {
            LeechPosX = (int) Form1_0.Mem_0.ReadUInt32Raw((IntPtr) (LeechPlayerPointer + 0x60));
            LeechPosY = (int) Form1_0.Mem_0.ReadUInt32Raw((IntPtr) (LeechPlayerPointer + 0x64));
            LeechlevelNo = Form1_0.Mem_0.ReadUInt16Raw((IntPtr)(LeechPlayerPointer + 0x5C));
            //plevel = d2rprocess.read(partyStruct + 0x58, "UShort");
            //partyId = d2rprocess.read(partyStruct + 0x5A, "UShort");
            //hostilePtr = d2rprocess.read(partyStruct + 0x70, "Int64");
        }

        public void ScanForLeecher()
        {
            long UnitOffset = (long)Form1_0.BaseAddress + (long)Form1_0.offsets["rosterOffset"];
            long partyStruct = Form1_0.Mem_0.ReadInt64Raw((IntPtr)UnitOffset);
            LeechPlayerPointer = 0;
            LeechPlayerUnitID = 0;

            for (int i = 0; i < 8; i++)
            {
                string name = Form1_0.Mem_0.ReadMemString(partyStruct);
                if (name == Form1_0.GameStruc_0.GameOwnerName)
                {
                    LeechPlayerUnitID = Form1_0.Mem_0.ReadUInt32Raw((IntPtr)(partyStruct + 0x48));
                    LeechPlayerPointer = partyStruct;
                    break;
                }
                else
                {
                    partyStruct = Form1_0.Mem_0.ReadInt64Raw((IntPtr)(partyStruct + 0x148));
                }
            }
        }

        public void scanForPlayer(bool QuickScan)   //scanning for self
        {
            FoundPlayer = false;

            int SizeArray = 0;
            int SizeIncrement = 0;
            byte[] unitTableBufferT = new byte[] { };
            if (QuickScan)
            {
                SizeArray = (128 + 516) * 8;
                SizeIncrement = 8;
                unitTableBufferT = new byte[SizeArray];
                long UnitOffset = (long)Form1_0.BaseAddress + (long)Form1_0.offsets["unitTable"] + Form1_0.UnitStrucOffset;
                Form1_0.Mem_0.ReadRawMemory(UnitOffset, ref unitTableBufferT, SizeArray);
            }
            else
            {
                Form1_0.PatternsScan_0.scanForUnitsPointer("player");
                SizeArray = Form1_0.PatternsScan_0.AllPlayersPointers.Count;
                SizeIncrement = 1;
            }

            PlayerStrucCount = 0;

            for (int i = 0; i < SizeArray; i += SizeIncrement)
            {
                long UnitPointerLocation = 0;
                if (QuickScan)
                {
                    UnitPointerLocation = BitConverter.ToInt64(unitTableBufferT, i);
                }
                else
                {
                    UnitPointerLocation = Form1_0.PatternsScan_0.AllPlayersPointers[i];
                }

                if (UnitPointerLocation > 0)
                {
                    byte[] itemdatastruc = new byte[144];
                    Form1_0.Mem_0.ReadRawMemory(UnitPointerLocation, ref itemdatastruc, 144);

                    // Do ONLY UnitType:0 && TxtFileNo:3
                    if (BitConverter.ToUInt32(itemdatastruc, 0) == 0 && BitConverter.ToUInt32(itemdatastruc, 4) == 3)
                    {
                        PlayerStrucCount++;
                        //Form1_0.method_1("PPointerLocation: 0x" + (UnitPointerLocation).ToString("X"));

                        long pUnitDataPtr = BitConverter.ToInt64(itemdatastruc, 0x10);
                        byte[] pUnitData = new byte[144];
                        Form1_0.Mem_0.ReadRawMemory(pUnitDataPtr, ref pUnitData, 144);

                        string name = "";
                        for (int i2 = 0; i2 < 16; i2++)
                        {
                            if (pUnitData[i2] != 0x00)
                            {
                                name += (char)pUnitData[i2];
                            }
                        }
                        //name = name.Replace("?", "");
                        //Form1_0.method_1("PNAME: " + name);

                        long ppath = BitConverter.ToInt64(itemdatastruc, 0x38);
                        byte[] ppathData = new byte[144];
                        Form1_0.Mem_0.ReadRawMemory(ppath, ref ppathData, 144);

                        //if posX equal not zero
                        if (BitConverter.ToInt16(ppathData, 2) != 0 && name == CharConfig.PlayerCharName)
                        {
                            PlayerPointer = UnitPointerLocation;
                            Form1_0.Grid_SetInfos("Pointer", "0x" + PlayerPointer.ToString("X"));
                            FoundPlayer = true;
                            unitId = BitConverter.ToUInt32(itemdatastruc, 0x08);
                            Form1_0.method_1("Player ID: 0x" + unitId.ToString("X"), Color.Black);

                            /*string SavePathh = Form1_0.ThisEndPath + "DumpPlayerStruc";
                            File.Create(SavePathh).Dispose();
                            File.WriteAllBytes(SavePathh, itemdatastruc);
                            SavePathh = Form1_0.ThisEndPath + "DumpPlayerUnitData";
                            File.Create(SavePathh).Dispose();
                            File.WriteAllBytes(SavePathh, pUnitData);
                            SavePathh = Form1_0.ThisEndPath + "DumpPlayerPath";
                            File.Create(SavePathh).Dispose();
                            File.WriteAllBytes(SavePathh, ppathData);*/

                            return;
                        }
                    }
                }
            }
        }

        public bool ScanForOthersPlayers(long ThisUnitID, string ThisPlayerName, bool GetCorpseOnly)
        {
            //this can be used to get self corpse??

            Form1_0.PatternsScan_0.scanForUnitsPointer("player");
            for (int i = 0; i < Form1_0.PatternsScan_0.AllPlayersPointers.Count; i++)
            {
                long UnitPointerLocation = Form1_0.PatternsScan_0.AllPlayersPointers[i];
                if (UnitPointerLocation > 0)
                {
                    byte[] itemdatastruc = new byte[0x98];
                    Form1_0.Mem_0.ReadRawMemory(UnitPointerLocation, ref itemdatastruc, itemdatastruc.Length);


                    long pInventory = BitConverter.ToInt64(itemdatastruc, 0x90);
                    if (pInventory > 0)
                    {
                        unitIdOther = BitConverter.ToUInt32(itemdatastruc, 0x08);

                        long OtherpathAddress = Form1_0.Mem_0.ReadInt64Raw((IntPtr)(PlayerPointer + 0x38));
                        long OtherxPos = Form1_0.Mem_0.ReadUInt16Raw((IntPtr)(OtherpathAddress + 0x02));
                        long OtheryPos = Form1_0.Mem_0.ReadUInt16Raw((IntPtr)(OtherpathAddress + 0x06));
                        long OtherxPosOffset = Form1_0.Mem_0.ReadUInt16Raw((IntPtr)(OtherpathAddress + 0x00));
                        long OtheryPosOffset = Form1_0.Mem_0.ReadUInt16Raw((IntPtr)(OtherpathAddress + 0x04));
                        long OtherxPosOffsetPercent = (OtherxPosOffset / 65536); //get percentage
                        long OtheryPosOffsetPercent = (OtheryPosOffset / 65536); //get percentage
                        xPosFinalOtherP = (ushort)(OtherxPos + OtherxPosOffsetPercent);
                        yPosFinalOtherP = (ushort)(OtheryPos + OtheryPosOffsetPercent);

                        long pUnitDataPtr = BitConverter.ToInt64(itemdatastruc, 0x10);
                        byte[] pUnitData = new byte[144];
                        Form1_0.Mem_0.ReadRawMemory(pUnitDataPtr, ref pUnitData, 144);

                        string name = "";
                        for (int i2 = 0; i2 < 16; i2++)
                        {
                            if (pUnitData[i2] != 0x00)
                            {
                                name += (char)pUnitData[i2];
                            }
                            else
                            {
                                break;
                            }
                        }
                        pNameOther = name;
                        IsCorpse = false;
                        if (Form1_0.Mem_0.ReadByteRaw((IntPtr)(PlayerPointer + 0x1A6)) == 1)
                        {
                            IsCorpse = true;
                        }
                        if (xPosFinalOtherP > 1 && yPosFinalOtherP > 1)
                        {
                            if (unitIdOther != 0)
                            {
                                if (unitIdOther == ThisUnitID)
                                {
                                    if (!GetCorpseOnly || (GetCorpseOnly && IsCorpse))
                                    {
                                        return true;
                                    }
                                }
                            }
                            if (ThisPlayerName != "")
                            {
                                if (pNameOther == ThisPlayerName)
                                {
                                    if (!GetCorpseOnly || (GetCorpseOnly && IsCorpse))
                                    {
                                        return true;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return false;
        }

        public void GetHPAndManaOnThisEquippedItem()
        {
            int AddedHP = 0;
            int AddedMana = 0;
            int AddedVit = 0;
            int AddedEnergy = 0;
            int AddedHPPercent = 0;
            int AddedManaPercent = 0;

            if (Form1_0.ItemsStruc_0.statCount > 0)
            {
                for (int i = 0; i < Form1_0.ItemsStruc_0.statCount; i++)
                {
                    int offset = i * 8;
                    //short statLayer = BitConverter.ToInt16(Form1_0.ItemsStruc_0.statBuffer, offset);
                    ushort statEnum = BitConverter.ToUInt16(Form1_0.ItemsStruc_0.statBuffer, offset + 0x2);
                    int statValue = BitConverter.ToInt32(Form1_0.ItemsStruc_0.statBuffer, offset + 0x4);

                    if (statEnum == (ushort)Enums.Attribute.Vitality)
                    {
                        AddedVit += statValue;
                        AddedHP += statValue;
                    }
                    if (statEnum == (ushort)Enums.Attribute.LifeMax)
                    {
                        AddedHP += (statValue >> 8);
                    }
                    if (statEnum == (ushort)Enums.Attribute.MaxHPPercent)
                    {
                        AddedHPPercent += statValue;
                    }
                    if (statEnum == (ushort)Enums.Attribute.Energy)
                    {
                        AddedEnergy += statValue;
                        AddedMana += statValue;
                    }
                    if (statEnum == (ushort)Enums.Attribute.ManaMax)
                    {
                        AddedMana += (statValue >> 8);
                    }
                    if (statEnum == (ushort)Enums.Attribute.MaxManaPercent)
                    {
                        AddedManaPercent += statValue;
                    }
                }
            }

            if (Form1_0.ItemsStruc_0.statExCount > 0)
            {
                for (int i = 0; i < Form1_0.ItemsStruc_0.statExCount; i++)
                {
                    int offset = i * 8;
                    //short statLayer = BitConverter.ToInt16(Form1_0.ItemsStruc_0.statBufferEx, offset);
                    ushort statEnum = BitConverter.ToUInt16(Form1_0.ItemsStruc_0.statBufferEx, offset + 0x2);
                    int statValue = BitConverter.ToInt32(Form1_0.ItemsStruc_0.statBufferEx, offset + 0x4);

                    if (statEnum == (ushort)Enums.Attribute.Vitality)
                    {
                        AddedVit += statValue;
                        AddedHP += statValue;
                    }
                    if (statEnum == (ushort)Enums.Attribute.LifeMax)
                    {
                        AddedHP += (statValue >> 8);
                    }
                    if (statEnum == (ushort)Enums.Attribute.MaxHPPercent)
                    {
                        AddedHPPercent += statValue;
                    }
                    if (statEnum == (ushort)Enums.Attribute.Energy)
                    {
                        AddedEnergy += statValue;
                        AddedMana += statValue;
                    }
                    if (statEnum == (ushort)Enums.Attribute.ManaMax)
                    {
                        AddedMana += (statValue >> 8);
                    }
                    if (statEnum == (ushort)Enums.Attribute.MaxManaPercent)
                    {
                        AddedManaPercent += statValue;
                    }
                }
            }

            HPFromEquippedItems += AddedHP;
            ManaFromEquippedItems += AddedMana;
            VitalityFromEquippedItems += AddedVit;
            EnergyFromEquippedItems += AddedEnergy;
            HPPercentFromEquippedItems += AddedHPPercent;
            ManaPercentFromEquippedItems += AddedManaPercent;
        }



        //#####################################################################
        //#####################################################################
        //#####################################################################

        /*public void SetPlayerPos(ushort atX, ushort atY)
        {
            byte[] bytesposx = BitConverter.GetBytes(atX);
            byte[] bytesposy = BitConverter.GetBytes(atY);
            Form1_0.Mem_0.WriteRawMemory((IntPtr)(pathAddress + 0x02), bytesposx, 2);
            Form1_0.Mem_0.WriteRawMemory((IntPtr)(pathAddress + 0x06), bytesposy, 2);
            Thread.Sleep(1);
        }

        public uint calculateMapSeed(uint InitSeedHash1, uint InitSeedHash2, uint EndSeedHash1)
        {
            uint mapSeedTest = get_seed(InitSeedHash1, InitSeedHash2, EndSeedHash1);
            if (mapSeedTest == 0)
            {
                Form1_0.method_1("ERRROR: YOU HAVE AN ERROR DEECRYPTING THE MAP SEED, YOUR MAPS WILL EITHER NOT APPEAR OR NOT LINE UP", Color.Red);
            }
            return mapSeedTest;
        }*/

        public string GetImageURL()
        {
            string baseUrl = "http://localhost:3002";
            double wallThickness = 1.2;
            int serverScale = 3;
            string imageUrl = baseUrl + "/v1/map/" + mapSeed + "/" + difficulty + "/" + levelNo + "/image?wallthickness=" + wallThickness;
            imageUrl = imageUrl + "&rotate=true&showTextLabels=false";
            imageUrl = imageUrl + "&padding=0";  //+ settings["padding"];
            imageUrl = imageUrl + "&noStitch=true";  //+ settings["padding"];
            imageUrl = imageUrl + "&edge=true";
            /*if (settings["showPathFinding"])
            {
                if (pathStart && pathEnd)
                {
                    imageUrl = imageUrl + "&pathFinding=true";
                    imageUrl = imageUrl + "&pathStart=" + pathStart;
                    imageUrl = imageUrl + "&pathEnd=" + pathEnd;
                    imageUrl = imageUrl + "&pathColour=" + settings["pathFindingColour"];
                }
            }*/
            imageUrl = imageUrl + "&serverScale=" + serverScale;
            imageUrl = imageUrl.Replace(",", ".");
            return imageUrl;
        }

        public string getFileName()
        {
            return Form1_0.ThisEndPath + "\\maps\\" + this.mapSeed + "_" + this.difficulty + "_" + this.levelNo + ".png";
        }

        //delegate int ReadDelegate(IntPtr pIStream, IntPtr buffer, uint cb, out IntPtr pcbRead);
        //ReadDelegate Read = (ReadDelegate)Marshal.GetDelegateForFunctionPointer(Marshal.ReadIntPtr(Marshal.GetComInterfaceForObject(pIStream, typeof(IStream)), 0), typeof(ReadDelegate));
        //IntPtr pcbRead;

        public void DownloadImage(string imageUrl)
        {
            int tries = 0;
            while (tries < 5)
            {
                tries++;
                try
                {
                    dynamic whr = Activator.CreateInstance(Type.GetTypeFromProgID("WinHttp.WinHttpRequest.5.1"));
                    whr.SetTimeouts(45000, 45000, 45000, 45000);
                    whr.Open("GET", imageUrl, true);
                    whr.Send();
                    whr.WaitForResponse();
                    dynamic vStream = whr.ResponseStream;
                    Console.WriteLine("PASSED");
                    string sFile2 = getFileName();
                    if (vStream.GetType().Equals(typeof(System.Runtime.InteropServices.UnknownWrapper)))
                    {
                        Stream pIStream = vStream.QueryInterface(Type.GetTypeFromCLSID(new Guid("0000000c-0000-0000-C000-000000000046")));
                        using (FileStream oFile = new FileStream(sFile, FileMode.Create))
                        {
                            byte[] buffer = new byte[8192];
                            var br = new BinaryReader(pIStream);
                            buffer = br.ReadBytes((int)br.BaseStream.Length);
                            //buffer = br.ReadBytes((int)buffer.Length);
                            oFile.Write(buffer, 0, buffer.Length);
                        }
                        Marshal.ReleaseComObject(pIStream);
                    }
                    this.sFile = sFile2;
                    //this.ParseHeaders(whr.GetAllResponseHeaders());

                    if (!string.IsNullOrEmpty(whr.GetAllResponseHeaders()))
                    {
                        break; // don't retry if headers found
                    }
                }
                catch (Exception e)
                {
                    Form1_0.method_1("ERRROR:" + e.Message, Color.Red);
                }
            }
        }
    }
}
