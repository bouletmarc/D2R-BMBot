using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app
{
    public class BeltStruc
    {
        Form1 Form1_0;

        public int[] BeltHaveItems = new int[16];
        public int[] BeltItemsTypes = new int[16];
        public bool MissingHPPot = false;
        public bool MissingManaPot = false;
        public bool MissingRVPot = false;
        public bool IsItemHPPotion = false;
        public bool IsItemManaPotion = false;
        public bool IsItemRVPotion = false;
        public bool IsItemFullRVPotion = false;

        public int HPQuantity = 0;
        public int ManyQuantity = 0;

        public int ForceHPPotionQty = 0;
        public int ForceMANAPotionQty = 0;
        public bool HasPotInBadSpot = false;

        public void SetForm1(Form1 form1_1)
        {
            Form1_0 = form1_1;
        }

        public bool ItemGrabPotion()
        {
            if (MissingHPPot || MissingManaPot || MissingRVPot)
            {
                ItemIsPotion();
                if (MissingHPPot && IsItemHPPotion) return true;
                if (MissingManaPot && IsItemManaPotion) return true;
                //if (MissingRVPot && IsItemRVPotion) return true;
                if (MissingRVPot && IsItemFullRVPotion) return true;
            }
            return false;
        }

        public void CheckForMissingPotions()
        {
            Form1_0.ItemsStruc_0.GetItems(false);

            MissingHPPot = false;
            MissingManaPot = false;
            MissingRVPot = false;

            for (int i = 0; i < 16; i++)
            {
                if (BeltHaveItems[i] == 0)
                {
                    int BeltIndd = ConvertBeltIndexTo4Number(i);
                    if (BeltIndd != 5)
                    {
                        int PotToHave = CharConfig.BeltPotTypeToHave[BeltIndd];
                        if (PotToHave == 0)
                        {
                            MissingHPPot = true;
                        }
                        if (PotToHave == 1)
                        {
                            MissingManaPot = true;
                        }
                        if (PotToHave == 2 || PotToHave == 3)
                        {
                            MissingRVPot = true;
                        }
                    }
                }
            }

            if (MissingHPPot)
            {
                if (ForceHPPotionQty != 0)
                {
                    if (ForceHPPotionQty == HPQuantity)
                    {
                        MissingHPPot = false; //not missing pot fix
                    }
                }
            }

            if (MissingManaPot)
            {
                if (ForceMANAPotionQty != 0)
                {
                    if (ForceMANAPotionQty == ManyQuantity)
                    {
                        MissingManaPot = false; //not missing pot fix
                    }
                }
            }

            //Console.WriteLine("Missing pots:" + MissingHPPot + "," + MissingManaPot + "," + MissingRVPot);
        }

        public int ConvertBeltIndexTo4Number(int BeltInd)
        {
            if (BeltInd == 0 || BeltInd == 4 || BeltInd == 8 || BeltInd == 12) return 0;
            if (BeltInd == 1 || BeltInd == 5 || BeltInd == 9 || BeltInd == 13) return 1;
            if (BeltInd == 2 || BeltInd == 6 || BeltInd == 10 || BeltInd == 14) return 2;
            if (BeltInd == 3 || BeltInd == 7 || BeltInd == 11 || BeltInd == 15) return 3;
            return 5;
        }

        public void AddBeltItem(bool UsePotNotInSpot)
        {
            int BufferPotType = GetPotType();
            BeltHaveItems[Form1_0.ItemsStruc_0.itemx] = 1;
            BeltItemsTypes[Form1_0.ItemsStruc_0.itemx] = BufferPotType;


            bool UsedPotion = false;
            HasPotInBadSpot = false;
            int BeltIndd = ConvertBeltIndexTo4Number(Form1_0.ItemsStruc_0.itemx);
            if (BeltIndd != 5)
            {
                int PotToHave = CharConfig.BeltPotTypeToHave[BeltIndd];
                if (PotToHave != BufferPotType)
                {
                    if (UsePotNotInSpot)
                    {
                        Form1_0.Potions_0.PressPotionKey(BeltIndd, false); //use potion, not in right spot
                        UsedPotion = true;
                    }
                    else
                    {
                        HasPotInBadSpot = true;
                    }
                }
            }

            if (!UsedPotion)
            {
                if (BufferPotType == 0)
                {
                    HPQuantity++;
                }
                if (BufferPotType == 1)
                {
                    ManyQuantity++;
                }
            }
            //Form1_0.method_1("belt pointer" + Form1_0.ItemsStruc_0.itemx + ": 0x" + Form1_0.ItemsStruc_0.ItemPointerLocation.ToString("X") + " (diff from player: 0x" + (Form1_0.PlayerScan_0.PlayerPointer - Form1_0.ItemsStruc_0.ItemPointerLocation).ToString("X") + ")");
        }

        public void ItemIsPotion()
        {
            IsItemHPPotion = false;
            IsItemManaPotion = false;
            IsItemRVPotion = false;
            IsItemFullRVPotion = false;
            if (Form1_0.ItemsStruc_0.ItemNAAME == "Super Healing Potion") IsItemHPPotion = true;
            if (Form1_0.ItemsStruc_0.ItemNAAME == "Super Mana Potion") IsItemManaPotion = true;
            if (Form1_0.ItemsStruc_0.ItemNAAME == "Rejuvenation Potion") IsItemRVPotion = true;
            if (Form1_0.ItemsStruc_0.ItemNAAME == "Full Rejuvenation Potion") IsItemFullRVPotion = true;
        }

        public int GetPotType()
        {
            if (Form1_0.ItemsStruc_0.ItemNAAME.ToLower().Contains("healing") ||
                Form1_0.ItemsStruc_0.ItemNAAME.ToLower() == "potion of life")
            {
                return 0;
            }
            if (Form1_0.ItemsStruc_0.ItemNAAME.ToLower().Contains("mana"))
            {
                return 1;
            }
            if (Form1_0.ItemsStruc_0.ItemNAAME == "Rejuvenation Potion")
            {
                return 2;
            }
            if (Form1_0.ItemsStruc_0.ItemNAAME == "Full Rejuvenation Potion")
            {
                return 3;
            }
            return -1;
        }
    }
}
