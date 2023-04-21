using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app
{
    public class Shop
    {
        Form1 Form1_0;

        public bool FirstShopping = true;
        Dictionary<string, int> LastitemScreenPos = new Dictionary<string, int>();

        public void SetForm1(Form1 form1_1)
        {
            Form1_0 = form1_1;
        }

        public Dictionary<string, int> ConvertShopLocToScreenPos(int ThisX, int ThisY)
        {
            //starting at 1295,580 on screen for first item in inv, increment for 48px
            int xS = 185 + (ThisX * 48);
            int yS = 245 + (ThisY * 48);

            Dictionary<string, int> NewDict = new Dictionary<string, int>();
            NewDict["x"] = xS;
            NewDict["y"] = yS;
            return NewDict;
        }

        public bool ShouldShop()
        {
            Form1_0.ItemsStruc_0.GetItems(false);   //get inventory
            Form1_0.BeltStruc_0.CheckForMissingPotions();

            if (Form1_0.InventoryStruc_0.HasInventoryItems()
                || Form1_0.BeltStruc_0.MissingHPPot
                || Form1_0.BeltStruc_0.MissingManaPot
                || Form1_0.InventoryStruc_0.HUDItems_tpscrolls <= 2)
            {
                return true;
            }
            return false;
        }

        public void RunShopScript()
        {
            if (FirstShopping)
            {
                Form1_0.BeltStruc_0.ForceHPPotionQty = 0;
                Form1_0.BeltStruc_0.ForceMANAPotionQty = 0;

                FirstShopping = false;
            }

            LastitemScreenPos = new Dictionary<string, int>();

            //sell items
            if (Form1_0.InventoryStruc_0.HasInventoryItems())
            {
                Form1_0.SetGameStatus("TOWN-SHOP-SELL ITEMS");
                for (int i = 0; i < 40; i++)
                {
                    if (CharConfig.InventoryDontCheckItem[i] == 0 && Form1_0.InventoryStruc_0.InventoryHasItem[i] >= 1)
                    {
                        Dictionary<string, int> itemScreenPos = Form1_0.InventoryStruc_0.ConvertIndexToXY(i);
                        itemScreenPos = Form1_0.InventoryStruc_0.ConvertInventoryLocToScreenPos(itemScreenPos["x"], itemScreenPos["y"]);

                        int Tries = 0;
                        int MaxTries = 1;
                        while (true)
                        {
                            if (!Form1_0.Running || !Form1_0.GameStruc_0.IsInGame())
                            {
                                break;
                            }
                            /*if (Form1_0.ItemsStruc_0.ItemOnCursor)
                            {
                                Form1_0.KeyMouse_0.MouseClicc(LastitemScreenPos["x"], LastitemScreenPos["y"]);
                                Form1_0.WaitDelay(20);
                                Form1_0.ItemsStruc_0.GetItems(false);   //get inventory again
                                Form1_0.SetGameStatus("TOWN-SHOP-SELL ITEMS");
                            }*/
                            Form1_0.KeyMouse_0.MouseClicc(itemScreenPos["x"], itemScreenPos["y"]);
                            Form1_0.WaitDelay(15);
                            Form1_0.KeyMouse_0.MouseClicc(555, 465);

                            Form1_0.WaitDelay(50);
                            Form1_0.ItemsStruc_0.GetItems(false);   //get inventory again
                            Form1_0.SetGameStatus("TOWN-SHOP-SELL ITEMS");

                            //item still in inventory
                            if (Form1_0.InventoryStruc_0.InventoryHasItem[i] >= 1)
                            {
                                if (Tries > MaxTries)
                                {
                                    break;
                                }
                                Tries++;
                            }
                            else
                            {
                                while (Form1_0.ItemsStruc_0.ItemOnCursor)
                                {
                                    Form1_0.KeyMouse_0.MouseClicc(555, 465);
                                    Form1_0.WaitDelay(50);
                                    Form1_0.ItemsStruc_0.GetItems(false);   //get inventory again
                                    Form1_0.SetGameStatus("TOWN-SHOP-SELL ITEMS");
                                }
                                /*if (Form1_0.ItemsStruc_0.ItemOnCursor)
                                {
                                    Form1_0.KeyMouse_0.MouseClicc(LastitemScreenPos["x"], LastitemScreenPos["y"]);
                                    Form1_0.WaitDelay(20);
                                    Form1_0.ItemsStruc_0.GetItems(false);   //get inventory again
                                    Form1_0.SetGameStatus("TOWN-SHOP-SELL ITEMS");
                                }*/
                                break;
                            }
                        }

                        LastitemScreenPos = itemScreenPos;

                        if (Tries > MaxTries)
                        {
                            Form1_0.method_1("DIDNT SELL ITEM CORRECTLY!", Color.OrangeRed);
                            break;
                        }
                    }

                    if (!Form1_0.Running || !Form1_0.GameStruc_0.IsInGame())
                    {
                        break;
                    }
                }
            }

            if (!Form1_0.Running || !Form1_0.GameStruc_0.IsInGame())
            {
                return;
            }

            //Form1_0.method_1("MISS HP: " + Form1_0.BeltStruc_0.MissingHPPot);
            //Form1_0.method_1("MISS MANA: " + Form1_0.BeltStruc_0.MissingManaPot);
            //Form1_0.method_1("TP QTY: " + Form1_0.InventoryStruc_0.HUDItems_tpscrolls);
            //Form1_0.method_1("ID QTY: " + Form1_0.InventoryStruc_0.HUDItems_idscrolls);

            //buy potions
            int tries = 0;
            int StartQty = Form1_0.BeltStruc_0.HPQuantity;
            while (Form1_0.BeltStruc_0.MissingHPPot && tries < 1)
            {
                Form1_0.SetGameStatus("TOWN-SHOP-BUY HP POTIONS");
                if (!Form1_0.Running || !Form1_0.GameStruc_0.IsInGame())
                {
                    break;
                }
                if (Form1_0.ItemsStruc_0.GetShopItem("Super Healing Potion"))
                {
                    Dictionary<string, int> itemScreenPos = ConvertShopLocToScreenPos(Form1_0.ItemsStruc_0.itemx, Form1_0.ItemsStruc_0.itemy);

                    //Form1_0.SendSHIFT_RIGHTCLICK(itemScreenPos["x"], itemScreenPos["y"]);
                    Form1_0.KeyMouse_0.MouseCliccRight(itemScreenPos["x"], itemScreenPos["y"]);
                    Form1_0.WaitDelay(40);
                    Form1_0.ItemsStruc_0.UsePotionNotInRightSpot = false; //dont use pot if not in correct spot
                    Form1_0.ItemsStruc_0.GetItems(false);   //get inventory
                    Form1_0.ItemsStruc_0.UsePotionNotInRightSpot = true;
                    Form1_0.BeltStruc_0.CheckForMissingPotions();
                }

                if (Form1_0.BeltStruc_0.HPQuantity == StartQty)
                {
                    tries++;
                }
                StartQty = Form1_0.BeltStruc_0.HPQuantity;
            }

            if (Form1_0.InventoryStruc_0.HasInventoryItemName("Super Healing Potion") || Form1_0.BeltStruc_0.HasPotInBadSpot)
            {
                Form1_0.BeltStruc_0.ForceHPPotionQty = Form1_0.BeltStruc_0.HPQuantity; //reset qty in belt
                if (Form1_0.BeltStruc_0.HasPotInBadSpot)
                {
                    Form1_0.ItemsStruc_0.GetItems(false);   //get inventory to use pot in bad spot
                }
            }

            //buy mana
            tries = 0;
            StartQty = Form1_0.BeltStruc_0.ManyQuantity;
            while (Form1_0.BeltStruc_0.MissingManaPot && tries < 1)
            {
                Form1_0.SetGameStatus("TOWN-SHOP-BUY MANA POTIONS");
                if (!Form1_0.Running || !Form1_0.GameStruc_0.IsInGame())
                {
                    break;
                }
                if (Form1_0.ItemsStruc_0.GetShopItem("Super Mana Potion"))
                {
                    Dictionary<string, int> itemScreenPos = ConvertShopLocToScreenPos(Form1_0.ItemsStruc_0.itemx, Form1_0.ItemsStruc_0.itemy);

                    //Form1_0.SendSHIFT_RIGHTCLICK(itemScreenPos["x"], itemScreenPos["y"]);
                    Form1_0.KeyMouse_0.MouseCliccRight(itemScreenPos["x"], itemScreenPos["y"]);
                    Form1_0.WaitDelay(40);
                    Form1_0.ItemsStruc_0.UsePotionNotInRightSpot = false; //dont use pot if not in correct spot
                    Form1_0.ItemsStruc_0.GetItems(false);   //get inventory
                    Form1_0.ItemsStruc_0.UsePotionNotInRightSpot = true;
                    Form1_0.BeltStruc_0.CheckForMissingPotions();
                }

                if (Form1_0.BeltStruc_0.ManyQuantity == StartQty)
                {
                    tries++;
                }
                StartQty = Form1_0.BeltStruc_0.ManyQuantity;
            }

            if (Form1_0.InventoryStruc_0.HasInventoryItemName("Super Mana Potion") || Form1_0.BeltStruc_0.HasPotInBadSpot)
            {
                Form1_0.BeltStruc_0.ForceMANAPotionQty = Form1_0.BeltStruc_0.ManyQuantity; //reset qty in belt
                if (Form1_0.BeltStruc_0.HasPotInBadSpot)
                {
                    Form1_0.ItemsStruc_0.GetItems(false);   //get inventory to use pot in bad spot
                }
            }

            //buy tp
            tries = 0;
            StartQty = Form1_0.InventoryStruc_0.HUDItems_tpscrolls;
            while (Form1_0.InventoryStruc_0.HUDItems_tpscrolls < 20 && tries < 1)
            {
                Form1_0.SetGameStatus("TOWN-SHOP-BUY TP'S");
                if (!Form1_0.Running || !Form1_0.GameStruc_0.IsInGame())
                {
                    break;
                }
                if (Form1_0.ItemsStruc_0.GetShopItem("Scroll of Town Portal"))
                {
                    Dictionary<string, int> itemScreenPos = ConvertShopLocToScreenPos(Form1_0.ItemsStruc_0.itemx, Form1_0.ItemsStruc_0.itemy);

                    //Form1_0.SendSHIFT_RIGHTCLICK(itemScreenPos["x"], itemScreenPos["y"]);
                    Form1_0.KeyMouse_0.MouseCliccRight(itemScreenPos["x"], itemScreenPos["y"]);
                    Form1_0.WaitDelay(40);
                    Form1_0.ItemsStruc_0.GetItems(false);   //get inventory
                }

                if (Form1_0.InventoryStruc_0.HUDItems_tpscrolls == StartQty)
                {
                    tries++;
                }
                StartQty = Form1_0.InventoryStruc_0.HUDItems_tpscrolls;
            }

            //buy id
            tries = 0;
            StartQty = Form1_0.InventoryStruc_0.HUDItems_idscrolls;
            while (Form1_0.InventoryStruc_0.HUDItems_idscrolls < 20 && tries < 1)
            {
                Form1_0.SetGameStatus("TOWN-SHOP-BUY ID'S");
                if (!Form1_0.Running || !Form1_0.GameStruc_0.IsInGame())
                {
                    break;
                }
                if (Form1_0.ItemsStruc_0.GetShopItem("Scroll of Identify"))
                {
                    Dictionary<string, int> itemScreenPos = ConvertShopLocToScreenPos(Form1_0.ItemsStruc_0.itemx, Form1_0.ItemsStruc_0.itemy);

                    //Form1_0.SendSHIFT_RIGHTCLICK(itemScreenPos["x"], itemScreenPos["y"]);
                    Form1_0.KeyMouse_0.MouseCliccRight(itemScreenPos["x"], itemScreenPos["y"]);
                    Form1_0.WaitDelay(40);
                    Form1_0.ItemsStruc_0.GetItems(false);   //get inventory
                }

                if (Form1_0.InventoryStruc_0.HUDItems_idscrolls == StartQty)
                {
                    tries++;
                }
                StartQty = Form1_0.InventoryStruc_0.HUDItems_idscrolls;
            }

            //buy key
            tries = 0;
            StartQty = Form1_0.InventoryStruc_0.HUDItems_keys;
            while (Form1_0.InventoryStruc_0.HUDItems_keys < 12 && tries < 1)
            {
                Form1_0.SetGameStatus("TOWN-SHOP-BUY KEYS");
                if (!Form1_0.Running || !Form1_0.GameStruc_0.IsInGame())
                {
                    break;
                }
                if (Form1_0.ItemsStruc_0.GetShopItem("Key"))
                {
                    Dictionary<string, int> itemScreenPos = ConvertShopLocToScreenPos(Form1_0.ItemsStruc_0.itemx, Form1_0.ItemsStruc_0.itemy);

                    //Form1_0.SendSHIFT_RIGHTCLICK(itemScreenPos["x"], itemScreenPos["y"]);
                    Form1_0.KeyMouse_0.MouseCliccRight(itemScreenPos["x"], itemScreenPos["y"]);
                    Form1_0.WaitDelay(40);
                    Form1_0.ItemsStruc_0.GetItems(false);   //get inventory
                }

                if (Form1_0.InventoryStruc_0.HUDItems_keys == StartQty)
                {
                    tries++;
                }
                StartQty = Form1_0.InventoryStruc_0.HUDItems_keys;
            }
        }
    }
}
