using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace app
{
    public class InventoryStruc
    {

        Form1 Form1_0;

        public int[] InventoryHasItem = new int[40];
        public int[] InventoryHasUnidItem = new int[40];
        public int[] InventoryHasStashItem = new int[40];
        public int[] InventoryHasItemToID = new int[40];
        public long[] InventoryItemPointers = new long[40];
        public string[] InventoryItemNames = new string[40];
        public int[] InventoryItemQuality = new int[40];

        public int HUDItems_idscrolls = 0;
        public int HUDItems_tpscrolls = 0;
        public int HUDItems_keys = 0;

        public int HUDItems_tpscrolls_locx = 0;
        public int HUDItems_tpscrolls_locy = 0;
        public int HUDItems_idscrolls_locx = 0;
        public int HUDItems_idscrolls_locy = 0;

        public bool HasIDTome = false;

        public void SetForm1(Form1 form1_1)
        {
            Form1_0 = form1_1;

        }

        public void UseTP()
        {
            Dictionary<string, int> itemScreenPos = ConvertInventoryLocToScreenPos(HUDItems_tpscrolls_locx, HUDItems_tpscrolls_locy);
            Form1_0.KeyMouse_0.MouseCliccRight(itemScreenPos["x"], itemScreenPos["y"]);
        }

        public Dictionary<string, int> ConvertInventoryLocToScreenPos(int ThisX, int ThisY)
        {
            //starting at 1295,580 on screen for first item in inv, increment for 48px
            int xS = 1300 + (ThisX * 48);
            int yS = 580 + (ThisY * 48);

            Dictionary<string, int> NewDict = new Dictionary<string, int>();
            NewDict["x"] = xS;
            NewDict["y"] = yS;
            return NewDict;
        }

        public int ConvertXYToIndex(int ThisX, int ThisY)
        {
            return ThisX + (ThisY * 10);
        }

        public Dictionary<string, int> ConvertIndexToXY(int Thisndex)
        {
            int yS = (int) Math.Floor((double) Thisndex / 10);
            int xS = Thisndex - (yS * 10);

            Dictionary<string, int> NewDict = new Dictionary<string, int>();
            NewDict["x"] = xS;
            NewDict["y"] = yS;
            return NewDict;
        }

        public bool ContainStashItemInInventory()
        {
            bool ContainStashItem = false;
            Form1_0.ItemsStruc_0.GetItems(false);

            for (int i = 0; i < 40; i++)
            {
                if (CharConfig.InventoryDontCheckItem[i] == 0 && InventoryHasItem[i] >= 1 && InventoryHasStashItem[i] >= 1)
                {
                    ContainStashItem = true;
                }

                /*if (InventoryDontCheckItem[i] == 0 && InventoryHasItem[i] >= 1)
                {
                    Form1_0.ItemsStruc_0.GetItemAtPointer(InventoryItemPointers[i]);
                    if (Form1_0.ItemsAlert_0.ShouldKeepItem())
                    {
                        ContainStashItem = true;
                    }
                }*/
            }

            return ContainStashItem;
        }

        public void VerifyKeysInventory()
        {
            int thisindex = CharConfig.KeysLocationInInventory.Item1 + (CharConfig.KeysLocationInInventory.Item2 * 10);

            //if its not a key at the key location, relocate the item
            if (InventoryItemNames[thisindex] != "Key")
            {
                //Console.WriteLine("here");
                int ThisNewIndex = GetNextFreeSpaceInInventory();

                if (ThisNewIndex > -1)
                {
                    //remove item from this slot
                    Dictionary<string, int> itemScreenPos = Form1_0.InventoryStruc_0.ConvertIndexToXY(thisindex);
                    itemScreenPos = Form1_0.InventoryStruc_0.ConvertInventoryLocToScreenPos(itemScreenPos["x"], itemScreenPos["y"]);

                    //Form1_0.KeyMouse_0.MouseClicc(itemScreenPos["x"], itemScreenPos["y"]);
                    Form1_0.Stash_0.PickItem(itemScreenPos["x"], itemScreenPos["y"]);
                    Form1_0.WaitDelay(5);

                    //place to next free space
                    itemScreenPos = Form1_0.InventoryStruc_0.ConvertIndexToXY(ThisNewIndex);
                    itemScreenPos = Form1_0.InventoryStruc_0.ConvertInventoryLocToScreenPos(itemScreenPos["x"], itemScreenPos["y"]);

                    Form1_0.Stash_0.PlaceItem(itemScreenPos["x"], itemScreenPos["y"]);
                    Form1_0.Stash_0.PlaceItem(itemScreenPos["x"], itemScreenPos["y"]);
                }
            }

            //place all keys together
            for (int i = 0; i < 40; i++)
            {
                if (CharConfig.InventoryDontCheckItem[i] == 0 && InventoryHasItem[i] >= 1 && InventoryItemNames[i] == "Key")
                {
                    //Console.WriteLine("hereIndex:" + i);
                    //pick key item from this slot
                    Dictionary<string, int> itemScreenPos = Form1_0.InventoryStruc_0.ConvertIndexToXY(i);
                    itemScreenPos = Form1_0.InventoryStruc_0.ConvertInventoryLocToScreenPos(itemScreenPos["x"], itemScreenPos["y"]);

                    //Form1_0.KeyMouse_0.MouseClicc(itemScreenPos["x"], itemScreenPos["y"]);
                    Form1_0.Stash_0.PickItem(itemScreenPos["x"], itemScreenPos["y"]);
                    Form1_0.WaitDelay(5);

                    //place with other key
                    itemScreenPos = Form1_0.InventoryStruc_0.ConvertIndexToXY(thisindex);
                    itemScreenPos = Form1_0.InventoryStruc_0.ConvertInventoryLocToScreenPos(itemScreenPos["x"], itemScreenPos["y"]);

                    if (!Form1_0.Stash_0.PlaceItem(itemScreenPos["x"], itemScreenPos["y"]))
                    {
                        itemScreenPos = Form1_0.InventoryStruc_0.ConvertIndexToXY(i);
                        itemScreenPos = Form1_0.InventoryStruc_0.ConvertInventoryLocToScreenPos(itemScreenPos["x"], itemScreenPos["y"]);
                        
                        Form1_0.Stash_0.PlaceItem(itemScreenPos["x"], itemScreenPos["y"]);
                        Form1_0.Stash_0.PlaceItem(itemScreenPos["x"], itemScreenPos["y"]);
                    }
                }
            }
        }

        public void SetHUDItem()
        {
            if (Form1_0.ItemsStruc_0.statCount > 0)
            {
                //; get quantity
                int quantity = 1;
                //Form1_0.Mem_0.ReadRawMemory(Form1_0.ItemsStruc_0.statPtr, ref Form1_0.ItemsStruc_0.statBuffer, (int)(Form1_0.ItemsStruc_0.statCount * 10));
                for (int i = 0; i < Form1_0.ItemsStruc_0.statCount; i++)
                {
                    int offset = i * 8;
                    ushort statEnum = BitConverter.ToUInt16(Form1_0.ItemsStruc_0.statBuffer, offset);
                    int statValue = BitConverter.ToInt32(Form1_0.ItemsStruc_0.statBuffer, offset + 0x2);

                    //bad verif
                    if (statEnum == 70)
                    {
                        quantity = statValue;
                    }
                    //good verif
                    if ((statEnum == 0 && Form1_0.ItemsStruc_0.statCount == 1))
                    {
                        quantity = ((statValue >> 8) / 256);

                    }
                }

                //; 543 is key
                //; 529 is TP scroll
                //; 530 is ID scroll
                //; 518 is tome of TP
                //; 519 is tome of ID
                if (Form1_0.ItemsStruc_0.txtFileNo == 543)
                {
                    HUDItems_keys = HUDItems_keys + quantity;
                }
                else if (Form1_0.ItemsStruc_0.txtFileNo == 529)
                {
                    HUDItems_tpscrolls = HUDItems_tpscrolls + quantity;
                    HUDItems_tpscrolls_locx = Form1_0.ItemsStruc_0.itemx;
                    HUDItems_tpscrolls_locy = Form1_0.ItemsStruc_0.itemy;
                }
                else if (Form1_0.ItemsStruc_0.txtFileNo == 530)
                {
                    HUDItems_idscrolls = HUDItems_idscrolls + quantity;
                    HUDItems_idscrolls_locx = Form1_0.ItemsStruc_0.itemx;
                    HUDItems_idscrolls_locy = Form1_0.ItemsStruc_0.itemy;
                }
                else if (Form1_0.ItemsStruc_0.txtFileNo == 518)
                {
                    HUDItems_tpscrolls = HUDItems_tpscrolls + quantity;
                    HUDItems_tpscrolls_locx = Form1_0.ItemsStruc_0.itemx;
                    HUDItems_tpscrolls_locy = Form1_0.ItemsStruc_0.itemy;
                }
                else if (Form1_0.ItemsStruc_0.txtFileNo == 519)
                {
                    HasIDTome = true;
                    HUDItems_idscrolls = HUDItems_idscrolls + quantity;
                    HUDItems_idscrolls_locx = Form1_0.ItemsStruc_0.itemx;
                    HUDItems_idscrolls_locy = Form1_0.ItemsStruc_0.itemy;
                }
            }
        }

        public void DumpBadItemsOnGround()
        {
            Form1_0.UIScan_0.OpenUIMenu("invMenu");
            Form1_0.ItemsStruc_0.GetBadItemsOnCursor();
            //Form1_0.ItemsStruc_0.GetItems(false);

            //place all bad items on ground
            for (int i = 0; i < 40; i++)
            {
                //Console.WriteLine("toID: " + InventoryHasItemToID[i] + ", tostash: " + InventoryHasStashItem[i]);
                Form1_0.UIScan_0.OpenUIMenu("invMenu");
                if (CharConfig.InventoryDontCheckItem[i] == 0 && InventoryHasItem[i] >= 1 && InventoryHasItemToID[i] == 0 && InventoryHasStashItem[i] == 0)
                {
                    //pick key item from this slot
                    Dictionary<string, int> itemScreenPos = Form1_0.InventoryStruc_0.ConvertIndexToXY(i);
                    itemScreenPos = Form1_0.InventoryStruc_0.ConvertInventoryLocToScreenPos(itemScreenPos["x"], itemScreenPos["y"]);

                    if (InventoryItemNames[i].Contains("Healing") || InventoryItemNames[i].Contains("Mana") || InventoryItemNames[i].Contains("Rejuvenation"))
                    {
                        Form1_0.KeyMouse_0.MouseCliccRight(itemScreenPos["x"], itemScreenPos["y"]);
                        Form1_0.WaitDelay(10);
                    }
                    else
                    {
                        /*Form1_0.Stash_0.PickItem(itemScreenPos["x"], itemScreenPos["y"]);
                        if (!Form1_0.Stash_0.PlaceItem(Form1_0.CenterX, Form1_0.CenterY))
                        {
                            Form1_0.Stash_0.PlaceItem(itemScreenPos["x"], itemScreenPos["y"]);
                        }*/

                        Form1_0.KeyMouse_0.MouseClicc(itemScreenPos["x"], itemScreenPos["y"]);
                        Form1_0.WaitDelay(12);
                        Form1_0.ItemsStruc_0.GetItems(false);
                        //Form1_0.KeyMouse_0.MouseClicc_RealPos(Form1_0.CenterX, Form1_0.CenterY);
                        Form1_0.Stash_0.PlaceItem(Form1_0.CenterX, Form1_0.CenterY);
                        Form1_0.WaitDelay(10);
                    }
                }
            }

            Form1_0.UIScan_0.CloseUIMenu("invMenu");
        }

        public void ResetInventory()
        {
            InventoryHasItem = new int[40];
            InventoryHasItemToID = new int[40];
            InventoryHasUnidItem = new int[40];
            InventoryItemPointers = new long[40];
            InventoryItemNames = new string[40];
            InventoryItemQuality = new int[40];
            InventoryHasStashItem = new int[40];
            HUDItems_idscrolls = 0;
            HUDItems_tpscrolls = 0;
            HUDItems_keys = 0;
        }

        public void SetInventoryItem()
        {
            try
            {
                int FullIndex = ConvertXYToIndex(Form1_0.ItemsStruc_0.itemx, Form1_0.ItemsStruc_0.itemy);

                if (CharConfig.InventoryDontCheckItem[FullIndex] == 1
                    && Form1_0.ItemsStruc_0.ItemNAAME != "Key"
                    && Form1_0.ItemsStruc_0.ItemNAAME != "Tome of Town Portal"
                    && Form1_0.ItemsStruc_0.ItemNAAME != "Tome of Identify")
                {
                    return;
                }

                InventoryHasItem[FullIndex] = 1;
                InventoryItemPointers[FullIndex] = Form1_0.ItemsStruc_0.ItemPointerLocation;
                InventoryItemNames[FullIndex] = Form1_0.ItemsStruc_0.ItemNAAME;
                InventoryItemQuality[FullIndex] = (int) Form1_0.ItemsStruc_0.itemQuality;
                if (Form1_0.ItemsAlert_0.ShouldKeepItem())
                {
                    InventoryHasStashItem[FullIndex] = 1;
                }
                if (Form1_0.ItemsAlert_0.ShouldPickItem(false))
                {
                    InventoryHasItemToID[FullIndex] = 1;
                }

                if (!Form1_0.ItemsStruc_0.identified)
                {
                    InventoryHasUnidItem[FullIndex] = 1;
                }
            }
            catch { }
        }

        public bool HasUnidItemInInventory()
        {
            for (int i = 0; i < 40; i++)
            {
                if (CharConfig.InventoryDontCheckItem[i] == 0 && InventoryHasUnidItem[i] >= 1)
                {
                    return true;
                }
            }
            return false;
        }

        public int GetNextFreeSpaceInInventory()
        {
            for (int i = 0; i < 40; i++)
            {
                if (CharConfig.InventoryDontCheckItem[i] == 0 && InventoryHasItem[i] == 0)
                {
                    return i;
                }
            }
            return -1;
        }

        public bool HasInventoryItems()
        {
            for (int i = 0; i < 40; i++)
            {
                if (CharConfig.InventoryDontCheckItem[i] == 0 && InventoryHasItem[i] >= 1)
                {
                    return true;
                }
            }
            return false;
        }

        public bool HasInventoryItemName(string ItemmN)
        {
            for (int i = 0; i < 40; i++)
            {
                if (InventoryHasItem[i] >= 1)
                {
                    if (InventoryItemNames[i] == ItemmN)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool HasInventoryItemsForShop()
        {
            for (int i = 0; i < 40; i++)
            {
                if (CharConfig.RunCowsScript && !Form1_0.Cows_0.ScriptDone && Form1_0.InventoryStruc_0.InventoryItemNames[i] == "Wirt's Leg") continue;
                if (CharConfig.RunCowsScript && !Form1_0.Cows_0.ScriptDone && Form1_0.InventoryStruc_0.InventoryItemNames[i] == "Tome of Town Portal") continue;

                if (CharConfig.InventoryDontCheckItem[i] == 0 && InventoryHasItem[i] >= 1 && InventoryHasStashItem[i] == 0)
                {
                    return true;
                }
            }
            return false;
        }

        /*public bool HasInventoryItemAt(int AtIndex)
        {
            if (InventoryHasItem[AtIndex] >= 1)
            {
                return true;
            }
            return false;
        }*/
    }
}
