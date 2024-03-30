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

        public bool ShopForSellingitem = false;
        public bool ShopForHP = false;
        public bool ShopForMana = false;
        public bool ShopForTP = false;
        public bool ShopForKey = false;
        public bool ShopForRegainHP = false;

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

            ShopForSellingitem = Form1_0.InventoryStruc_0.HasInventoryItems();
            ShopForHP = Form1_0.BeltStruc_0.MissingHPPot;
            ShopForMana = Form1_0.BeltStruc_0.MissingManaPot;
            ShopForTP = (Form1_0.InventoryStruc_0.HUDItems_tpscrolls <= 2);
            ShopForKey = (Form1_0.InventoryStruc_0.HUDItems_keys <= 3);
            ShopForRegainHP = Form1_0.PlayerScan_0.ShouldSeeShopForHP();

            if (Form1_0.InventoryStruc_0.HasInventoryItems()
                || Form1_0.BeltStruc_0.MissingHPPot
                || Form1_0.BeltStruc_0.MissingManaPot
                || Form1_0.InventoryStruc_0.HUDItems_tpscrolls <= 2
                || Form1_0.InventoryStruc_0.HUDItems_keys <= 3
                || Form1_0.PlayerScan_0.ShouldSeeShopForHP())
            {
                return true;
            }
            return false;
        }

        public bool PlaceItem(int PosX, int PosY)
        {
            int Tryy = 0;
            Form1_0.ItemsStruc_0.GetItems(false);
            while (Form1_0.ItemsStruc_0.ItemOnCursor && Tryy < 15)
            {
                Form1_0.KeyMouse_0.MouseClicc(PosX, PosY);
                Form1_0.WaitDelay(10);
                Form1_0.ItemsStruc_0.GetItems(false);   //get inventory again
                Tryy++;
            }
            if (Tryy >= 15)
            {
                return false;
            }
            return true;
        }

        public bool PickItem(int PosX, int PosY)
        {
            int Tryy = 0;
            Form1_0.ItemsStruc_0.GetItems(false);
            while (!Form1_0.ItemsStruc_0.ItemOnCursor && Tryy < 5)
            {
                Form1_0.KeyMouse_0.MouseClicc(PosX, PosY);
                Form1_0.WaitDelay(10);
                Form1_0.ItemsStruc_0.GetItems(false);   //get inventory again
                Tryy++;
            }
            if (Tryy >= 5)
            {
                return false;
            }
            return true;
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
            if (!Form1_0.Town_0.FastTowning)
            {
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

                                Form1_0.ItemsStruc_0.GetItems(false);   //get inventory again
                                /*PickItem(itemScreenPos["x"], itemScreenPos["y"]);
                                Form1_0.WaitDelay(10);
                                if (!PlaceItem(555, 465))
                                {
                                    Form1_0.WaitDelay(10);
                                    PlaceItem(itemScreenPos["x"], itemScreenPos["y"]);
                                }*/


                                //CTRL+Clic to send item into stash
                                Form1_0.KeyMouse_0.SendCTRL_CLICK(itemScreenPos["x"], itemScreenPos["y"]);
                                Form1_0.WaitDelay(5);
                                Form1_0.ItemsStruc_0.GetItems(false);   //get inventory again
                                PlaceItem(itemScreenPos["x"], itemScreenPos["y"]);

                                //############## OLD CODE
                                //Form1_0.KeyMouse_0.MouseClicc(itemScreenPos["x"], itemScreenPos["y"]);
                                //Form1_0.WaitDelay(5);
                                //Form1_0.KeyMouse_0.MouseClicc(555, 465);
                                //Form1_0.WaitDelay(5);
                                //Form1_0.KeyMouse_0.MouseClicc(itemScreenPos["x"], itemScreenPos["y"]);
                                //Form1_0.WaitDelay(10);
                                //Form1_0.ItemsStruc_0.GetItems(false);   //get inventory again
                                //Form1_0.SetGameStatus("TOWN-SHOP-SELL ITEMS");
                                //##############

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

                    int ShopCount = 4;
                    if (Form1_0.BeltStruc_0.ForceHPPotionQty > 0)
                    {
                        ShopCount = Form1_0.BeltStruc_0.ForceHPPotionQty - Form1_0.BeltStruc_0.HPQuantity;
                    } 
                    else
                    {
                        ShopCount = 8 - Form1_0.BeltStruc_0.HPQuantity;
                    }

                    for (int i = 0; i < ShopCount; i++)
                    {
                        //Form1_0.SendSHIFT_RIGHTCLICK(itemScreenPos["x"], itemScreenPos["y"]);
                        Form1_0.KeyMouse_0.MouseCliccRight(itemScreenPos["x"], itemScreenPos["y"]);
                        Form1_0.WaitDelay(10);
                    }

                    Form1_0.ItemsStruc_0.UsePotionNotInRightSpot = false; //dont use pot if not in correct spot
                    Form1_0.ItemsStruc_0.GetItems(false);   //get inventory

                    //####
                    if (Form1_0.InventoryStruc_0.HasInventoryItemName("Super Healing Potion") || Form1_0.BeltStruc_0.HasPotInBadSpot)
                    {
                        //Form1_0.method_1("FORCING HP POT QTY: " + Form1_0.BeltStruc_0.HPQuantity, Color.Red);
                        Form1_0.BeltStruc_0.ForceHPPotionQty = Form1_0.BeltStruc_0.HPQuantity; //reset qty in belt
                        if (Form1_0.BeltStruc_0.HasPotInBadSpot)
                        {
                            Form1_0.BeltStruc_0.ForceHPPotionQty -= 1;
                            Form1_0.ItemsStruc_0.UsePotionNotInRightSpot = true;
                            Form1_0.ItemsStruc_0.GetItems(false);   //get inventory to use pot in bad spot
                            Form1_0.ItemsStruc_0.GetItems(false);   //get inventory to use pot in bad spot
                        }
                        //Form1_0.method_1("FORCING HP POT QTY: " + Form1_0.BeltStruc_0.ForceHPPotionQty, Color.Red);
                        break;
                    }
                    //####

                    Form1_0.ItemsStruc_0.UsePotionNotInRightSpot = true;
                    Form1_0.BeltStruc_0.CheckForMissingPotions();
                }

                if (Form1_0.BeltStruc_0.HPQuantity == StartQty)
                {
                    tries++;
                }
                StartQty = Form1_0.BeltStruc_0.HPQuantity;
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

                    int ShopCount = 4;
                    if (Form1_0.BeltStruc_0.ForceHPPotionQty > 0)
                    {
                        ShopCount = Form1_0.BeltStruc_0.ForceMANAPotionQty - Form1_0.BeltStruc_0.ManyQuantity;
                    }
                    else
                    {
                        ShopCount = 8 - Form1_0.BeltStruc_0.ManyQuantity;
                    }

                    for (int i = 0; i < ShopCount; i++)
                    {
                        //Form1_0.SendSHIFT_RIGHTCLICK(itemScreenPos["x"], itemScreenPos["y"]);
                        Form1_0.KeyMouse_0.MouseCliccRight(itemScreenPos["x"], itemScreenPos["y"]);
                        Form1_0.WaitDelay(10);
                    }

                    Form1_0.ItemsStruc_0.UsePotionNotInRightSpot = false; //dont use pot if not in correct spot
                    Form1_0.ItemsStruc_0.GetItems(false);   //get inventory

                    //####
                    if (Form1_0.InventoryStruc_0.HasInventoryItemName("Super Mana Potion") || Form1_0.BeltStruc_0.HasPotInBadSpot)
                    {
                        Form1_0.BeltStruc_0.ForceMANAPotionQty = Form1_0.BeltStruc_0.ManyQuantity; //reset qty in belt
                        if (Form1_0.BeltStruc_0.HasPotInBadSpot)
                        {
                            Form1_0.BeltStruc_0.ForceMANAPotionQty -= 1;
                            Form1_0.ItemsStruc_0.UsePotionNotInRightSpot = true;
                            Form1_0.ItemsStruc_0.GetItems(false);   //get inventory to use pot in bad spot
                            Form1_0.ItemsStruc_0.GetItems(false);   //get inventory to use pot in bad spot
                        }
                        break;
                    }
                    //####

                    Form1_0.ItemsStruc_0.UsePotionNotInRightSpot = true;
                    Form1_0.BeltStruc_0.CheckForMissingPotions();
                }

                if (Form1_0.BeltStruc_0.ManyQuantity == StartQty)
                {
                    tries++;
                }
                StartQty = Form1_0.BeltStruc_0.ManyQuantity;
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

                    int ShopCount = 20 - Form1_0.InventoryStruc_0.HUDItems_tpscrolls;
                    for (int i = 0; i < ShopCount; i++)
                    {
                        //Form1_0.SendSHIFT_RIGHTCLICK(itemScreenPos["x"], itemScreenPos["y"]);
                        Form1_0.KeyMouse_0.MouseCliccRight(itemScreenPos["x"], itemScreenPos["y"]);
                        Form1_0.WaitDelay(10);
                    }
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

                    int ShopCount = 20 - Form1_0.InventoryStruc_0.HUDItems_idscrolls;
                    for (int i = 0; i < ShopCount; i++)
                    {
                        //Form1_0.SendSHIFT_RIGHTCLICK(itemScreenPos["x"], itemScreenPos["y"]);
                        Form1_0.KeyMouse_0.MouseCliccRight(itemScreenPos["x"], itemScreenPos["y"]);
                        Form1_0.WaitDelay(10);
                    }
                    Form1_0.ItemsStruc_0.GetItems(false);   //get inventory
                }

                if (Form1_0.InventoryStruc_0.HUDItems_idscrolls == StartQty)
                {
                    tries++;
                }
                StartQty = Form1_0.InventoryStruc_0.HUDItems_idscrolls;
            }

            //buy key
            Form1_0.InventoryStruc_0.VerifyKeysInventory();
            tries = 0;
            StartQty = Form1_0.InventoryStruc_0.HUDItems_keys;
            while (Form1_0.InventoryStruc_0.HUDItems_keys <= 8 && tries < 1)
            {
                Form1_0.SetGameStatus("TOWN-SHOP-BUY KEYS");
                if (!Form1_0.Running || !Form1_0.GameStruc_0.IsInGame())
                {
                    break;
                }
                if (Form1_0.ItemsStruc_0.GetShopItem("Key"))
                {
                    Dictionary<string, int> itemScreenPos = ConvertShopLocToScreenPos(Form1_0.ItemsStruc_0.itemx, Form1_0.ItemsStruc_0.itemy);

                    Form1_0.KeyMouse_0.SendSHIFT_RIGHTCLICK(itemScreenPos["x"], itemScreenPos["y"]);
                    //Form1_0.KeyMouse_0.MouseCliccRight(itemScreenPos["x"], itemScreenPos["y"]);
                    Form1_0.WaitDelay(20);
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
