using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app
{
    public class NPCStruc
    {
        Form1 Form1_0;
        public long NPCPointerLocation = 0;
        public byte[] NPCdatastruc = new byte[144];
        public uint txtFileNo = 0;
        public long pPathPtr = 0;
        public ushort itemx = 0;
        public ushort itemy = 0;
        public ushort xPosFinal = 0;
        public ushort yPosFinal = 0;
        public byte[] pPath = new byte[144];

        public void SetForm1(Form1 form1_1)
        {
            Form1_0 = form1_1;
        }

        public bool GetNPC(string MobName)
        {
            txtFileNo = 0;
            Form1_0.PatternsScan_0.scanForUnitsPointer("NPC");

            for (int i = 0; i < Form1_0.PatternsScan_0.AllNPCPointers.Count; i++)
            {
                NPCPointerLocation = Form1_0.PatternsScan_0.AllNPCPointers[i];
                if (NPCPointerLocation > 0)
                {
                    NPCdatastruc = new byte[144];
                    Form1_0.Mem_0.ReadRawMemory(NPCPointerLocation, ref NPCdatastruc, 144);

                    txtFileNo = BitConverter.ToUInt32(NPCdatastruc, 4);
                    GetUnitPathData();

                    //Console.WriteLine((int)txtFileNo + " at: " + xPosFinal + ", " + yPosFinal);

                    if (getTownNPC((int)txtFileNo) == MobName)
                    {
                        if (xPosFinal != 0 && yPosFinal != 0)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        public void GetUnitPathData()
        {
            pPathPtr = BitConverter.ToInt64(NPCdatastruc, 0x38);
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


        public string getTownNPC(int txtFileNo)
        {
            switch (txtFileNo)
            {
                case 146: return "DeckardCain";
                case 154: return "Charsi";
                case 147: return "Gheed";
                case 150: return "Kashya";
                case 155: return "Warriv";
                case 148: return "Akara";
                case 244: return "DeckardCain";
                case 210: return "Meshif";
                case 175: return "Warriv";
                case 199: return "Elzix";
                case 198: return "Greiz";
                case 177: return "Drognan";
                case 178: return "Fara";
                case 201: return "Jerhyn";
                case 202: return "Lysander";
                case 176: return "Atma";
                case 200: return "Geglash";
                case 331: return "Kaelan";
                case 245: return "DeckardCain";
                case 264: return "Meshif";
                case 255: return "Ormus";
                case 252: return "Asheara";
                case 254: return "Alkor";
                case 253: return "Hratli";
                case 297: return "Natalya";
                case 246: return "DeckardCain";
                case 251: return "Tyrael";
                case 338: return "DeadCorpse";
                case 367: return "Tyrael";
                case 521: return "Tyrael";
                case 257: return "Halbu";
                case 405: return "Jamella";
                case 265: return "DeckardCain";
                case 520: return "DeckardCain";
                case 512: return "Drehya";
                case 527: return "Drehya";
                case 515: return "Qual-Kehk";
                case 513: return "Malah";
                case 511: return "Larzuk";
                case 514: return "Nihlathak Town";
                case 266: return "navi";
                case 408: return "Malachai";
                case 406: return "Izual";
            }
            return "";
        }

        /*public bool IsDeadBody(int txtFileNo)
        {
            if (txtFileNo == 338)
            {
                return true;
            }
            return false;
        }*/


        // certain NPCs we don't want to see such as mercs
        public int HideNPC(int txtFileNo)
        {
            switch (txtFileNo)
            {
                case 0: return 1; //UNKOWN
                case 149: return 1; //Chicken
                case 151: return 1; //Rat
                case 152: return 1; //Rogue
                case 153: return 1; //HellMeteor
                case 157: return 1; //Bird
                case 158: return 1; //Bird2
                case 159: return 1; //Bat
                case 194: return 1; //Hadriel
                case 195: return 1; //Act2Male
                case 196: return 1; //Act2Female
                case 197: return 1; //Act2Child
                case 179: return 1; //Cow
                case 185: return 1; //Camel
                case 203: return 1; //Act2Guard
                case 204: return 1; //Act2Vendor
                case 205: return 1; //Act2Vendor2
                case 227: return 1; //Maggot
                case 268: return 1; //Bug
                case 269: return 1; //Scorpion
                // case 271: return 1; //Rogue2
                case 272: return 1; //Rogue3
                case 283: return 1; //Larva
                case 293: return 1; //Familiar
                case 294: return 1; //Act3Male
                // case 289: return 1; //ClayGolem
                // case 290: return 1; //BloodGolem
                // case 291: return 1; //IronGolem
                // case 292: return 1; //FireGolem
                case 296: return 1; //Act3Female
                case 318: return 1; //Snake
                case 319: return 1; //Parrot
                case 320: return 1; //Fish
                case 321: return 1; //EvilHole
                case 322: return 1; //EvilHole2
                case 323: return 1; //EvilHole3
                case 324: return 1; //EvilHole4
                case 325: return 1; //EvilHole5
                case 326: return 1; //FireboltTrap
                case 327: return 1; //HorzMissileTrap
                case 328: return 1; //VertMissileTrap
                case 329: return 1; //PoisonCloudTrap
                case 330: return 1; //LightningTrap
                case 332: return 1; //InvisoSpawner
                // case 338: return 1; //Guard or DEAD BODY!!!
                case 339: return 1; //MiniSpider
                case 344: return 1; //BoneWall
                case 351: return 1; //Hydra
                case 352: return 1; //Hydra2
                case 353: return 1; //Hydra3
                case 355: return 1; //SevenTombs
                // case 357: return 1; //Valkyrie
                // case 359: return 1; //IronWolf
                // case 363: return 1; //NecroSkeleton
                // case 364: return 1; //NecroMage
                case 366: return 1; //CompellingOrb},
                case 370: return 1; //SpiritMummy
                case 377: return 1; //Act2Guard4
                case 378: return 1; //Act2Guard5
                case 392: return 1; //Window
                case 393: return 1; //Window2
                case 401: return 1; //MephistoSpirit
                case 410: return 1; //WakeOfDestruction
                case 411: return 1; //ChargedBoltSentry
                case 412: return 1; //LightningSentry
                case 414: return 1; //InvisiblePet
                case 415: return 1; //InfernoSentry
                case 416: return 1; //DeathSentry
                // case 417: return 1; //ShadowWarrior
                // case 418: return 1; //ShadowMaster
                // case 419: return 1; //DruidHawk
                // case 420: return 1; //DruidSpiritWolf
                // case 421: return 1; //DruidFenris
                // case 423: return 1; //HeartOfWolverine
                // case 424: return 1; //OakSage
                // case 428: return 1; //DruidBear
                case 543: return 1; //BaalThrone
                case 567: return 1; //InjuredBarbarian
                case 568: return 1; //InjuredBarbarian2
                case 569: return 1; //InjuredBarbarian3
                case 711: return 1; //DemonHole
            }
            return 0;
        }
    }
}
