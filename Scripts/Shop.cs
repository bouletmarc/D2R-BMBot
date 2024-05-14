using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Enums;

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

    public bool ShopForTomeOfPortal = false; //cows level portal making

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
        if (Form1_0.InventoryStruc_0.HasInventoryItemName("Wirt's Leg")
            && !Form1_0.InventoryStruc_0.HasInventoryItemName("Tome of Town Portal", true))
        {
            ShopForTomeOfPortal = true;
            return true;
        }

        Form1_0.ItemsStruc_0.GetItems(false);   //get inventory
        Form1_0.BeltStruc_0.CheckForMissingPotions();

        ShopForSellingitem = Form1_0.InventoryStruc_0.HasInventoryItemsForShop();
        ShopForHP = Form1_0.BeltStruc_0.MissingHPPot;
        ShopForMana = Form1_0.BeltStruc_0.MissingManaPot;
        ShopForTP = (Form1_0.InventoryStruc_0.HUDItems_tpscrolls <= 2);
        ShopForKey = (Form1_0.InventoryStruc_0.HUDItems_keys <= 3) && CharConfig.UseKeys;
        ShopForRegainHP = Form1_0.PlayerScan_0.ShouldSeeShopForHP();

        if (Form1_0.InventoryStruc_0.HasInventoryItemsForShop()
            || Form1_0.BeltStruc_0.MissingHPPot
            || Form1_0.BeltStruc_0.MissingManaPot
            || Form1_0.InventoryStruc_0.HUDItems_tpscrolls <= 2
            || (Form1_0.InventoryStruc_0.HUDItems_keys <= 3 && CharConfig.UseKeys)
            || Form1_0.PlayerScan_0.ShouldSeeShopForHP())
        {
            return true;
        }
        return false;
    }

    public bool PlaceItem(int PosX, int PosY, bool ForceBadDetection = false)
    {
        int Tryy = 0;
        Form1_0.ItemsStruc_0.GetItems(false);
        while (Form1_0.ItemsStruc_0.ItemOnCursor && Tryy < 15)
        {
            Form1_0.KeyMouse_0.MouseClicc(PosX, PosY);
            Form1_0.WaitDelay(10);
            Form1_0.ItemsStruc_0.GetItems(false);   //get inventory again
            Tryy++;

            if (Tryy == 5 && ForceBadDetection)
            {
                Form1_0.ItemsStruc_0.GetBadItemsOnCursor();
                Tryy = 10;
            }
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

    public bool HasUnidItem = false;

    public void RunShopScript()
    {
        if (FirstShopping)
        {
            Form1_0.BeltStruc_0.ForceHPPotionQty = 0;
            Form1_0.BeltStruc_0.ForceMANAPotionQty = 0;

            FirstShopping = false;
        }
        HasUnidItem = false;

        if (Form1_0.InventoryStruc_0.HasInventoryItemName("Wirt's Leg")) ShopForTomeOfPortal = true;

        LastitemScreenPos = new Dictionary<string, int>();

        if (CharConfig.IDAtShop)
        {
            int tries2 = 0;
            int LastItemIdentified = 999;
            while (Form1_0.InventoryStruc_0.HasUnidItemInInventory() && tries2 < 2)
            {
                Form1_0.SetGameStatus("TOWN-SHOP-ID ITEMS");
                Form1_0.SetProcessingTime();
                if (!Form1_0.Running || !Form1_0.GameStruc_0.IsInGame())
                {
                    break;
                }
                if (Form1_0.ItemsStruc_0.GetShopItem("Scroll of Identify"))
                {
                    Dictionary<string, int> itemScreenPos = ConvertShopLocToScreenPos(Form1_0.ItemsStruc_0.itemx, Form1_0.ItemsStruc_0.itemy);

                    Form1_0.KeyMouse_0.MouseCliccRight(itemScreenPos["x"], itemScreenPos["y"]);
                    Form1_0.WaitDelay(20);
                    Form1_0.ItemsStruc_0.GetItems(false);   //get inventory
                }

                bool IdentifiedItem = false;
                for (int i = 0; i < 40; i++)
                {
                    if (Form1_0.InventoryStruc_0.InventoryItemNames[i] == "Scroll of Identify")
                    {
                        Dictionary<string, int> itemScreenPos = Form1_0.InventoryStruc_0.ConvertIndexToXY(i);
                        itemScreenPos = Form1_0.InventoryStruc_0.ConvertInventoryLocToScreenPos(itemScreenPos["x"], itemScreenPos["y"]);
                        Form1_0.KeyMouse_0.MouseCliccRight(itemScreenPos["x"], itemScreenPos["y"]);
                        Form1_0.WaitDelay(20);

                        for (int k = 0; k < 40; k++)
                        {
                            if (Form1_0.InventoryStruc_0.InventoryHasUnidItem[k] == 1 && CharConfig.InventoryDontCheckItem[i] == 0)
                            {
                                itemScreenPos = Form1_0.InventoryStruc_0.ConvertIndexToXY(k);
                                itemScreenPos = Form1_0.InventoryStruc_0.ConvertInventoryLocToScreenPos(itemScreenPos["x"], itemScreenPos["y"]);
                                Form1_0.KeyMouse_0.MouseClicc(itemScreenPos["x"], itemScreenPos["y"]);
                                Form1_0.WaitDelay(100);
                                PlaceItem(itemScreenPos["x"], itemScreenPos["y"]);
                                IdentifiedItem = true;
                                Form1_0.ItemsStruc_0.GetItems(false);   //get inventory again

                                //#########################
                                //try selling this bad item
                                if (k == LastItemIdentified && Form1_0.InventoryStruc_0.InventoryHasStashItem[i] == 0)
                                {
                                    Form1_0.KeyMouse_0.SendCTRL_CLICK(itemScreenPos["x"], itemScreenPos["y"]);
                                    Form1_0.WaitDelay(5);
                                    Form1_0.ItemsStruc_0.GetItems(false);   //get inventory again
                                    if (Form1_0.ItemsStruc_0.ItemOnCursor)
                                    {
                                        PlaceItem(itemScreenPos["x"], itemScreenPos["y"]);
                                        IdentifiedItem = false;
                                    }
                                }
                                LastItemIdentified = k;
                                //#########################
                                break;
                            }
                        }

                        break;
                    }
                }
                Form1_0.ItemsStruc_0.GetItems(false);   //get inventory

                if (!IdentifiedItem) tries2++;
                else tries2 = 0;
            }

            Form1_0.ItemsStruc_0.GetItems(false);   //get inventory
            if (Form1_0.InventoryStruc_0.HasUnidItemInInventory()) HasUnidItem = true;
            Form1_0.ItemsStruc_0.GetItems(false);   //get inventory
        }

        //sell items
        //if (!Form1_0.Town_0.FastTowning)
        //{
        if (Form1_0.InventoryStruc_0.HasInventoryItems())
        {
            Form1_0.SetGameStatus("TOWN-SHOP-SELL ITEMS");
            for (int i = 0; i < 40; i++)
            {
                if (CharConfig.InventoryDontCheckItem[i] == 1) continue;
                if (Form1_0.InventoryStruc_0.InventoryHasItem[i] == 0) continue;
                if (ShopForTomeOfPortal && Form1_0.InventoryStruc_0.InventoryItemNames[i] == "Wirt's Leg") continue;

                //Console.WriteLine("HasStashItem:" + Form1_0.InventoryStruc_0.InventoryHasStashItem[i] + ", HasUnidItem:" + Form1_0.InventoryStruc_0.InventoryHasUnidItem[i]);

                if (Form1_0.InventoryStruc_0.InventoryHasStashItem[i] == 0
                    && Form1_0.InventoryStruc_0.InventoryHasUnidItem[i] == 0)
                {
                    //################
                    //GET ITEM SOLD INFOS
                    string SoldTxt = "";
                    Color ThisCol = Color.Black;
                    Dictionary<string, int> itemXYPos = Form1_0.InventoryStruc_0.ConvertIndexToXY(i);
                    if (Form1_0.ItemsStruc_0.GetSpecificItem(0, Form1_0.InventoryStruc_0.InventoryItemNames[i], itemXYPos["x"], itemXYPos["y"], Form1_0.PlayerScan_0.unitId, 0, true))
                    {
                        string ItemT = "";// Form1_0.ItemsAlert_0.GetItemTypeText();
                        if (Form1_0.ItemsAlert_0.GetItemTypeText() != "") ItemT = " && " + Form1_0.ItemsAlert_0.GetItemTypeText();
                        SoldTxt = "Sold Item:" + Form1_0.ItemsStruc_0.ItemNAAME + " (ID:" + Form1_0.ItemsStruc_0.txtFileNo + ")" + ItemT + " && " + Form1_0.ItemsStruc_0.GetQualityTextString() + " && " + Form1_0.ItemsStruc_0.GetAllFlagsFromItem() + " && " + Form1_0.ItemsStruc_0.GetAllValuesFromStats() + Form1_0.ItemsStruc_0.GetItemsStashInfosTxt();
                        ThisCol = Form1_0.ItemsStruc_0.GetColorFromQuality((int)Form1_0.ItemsStruc_0.itemQuality);
                        if (Form1_0.ItemsAlert_0.ShouldKeepItem())
                        {
                            continue;
                        }
                    }
                    //Form1_0.ItemsViewer_0.TakeItemPicture();
                    //################

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

                        //CTRL+Clic to send item into stash
                        //Form1_0.KeyMouse_0.MouseMoveTo(itemScreenPos["x"], itemScreenPos["y"]);
                        //Form1_0.ItemsViewer_0.TakeItemPicture();
                        Form1_0.KeyMouse_0.SendCTRL_CLICK(itemScreenPos["x"], itemScreenPos["y"]);
                        Form1_0.WaitDelay(5);
                        Form1_0.ItemsStruc_0.GetItems(false);   //get inventory again
                        PlaceItem(itemScreenPos["x"], itemScreenPos["y"]);

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
                            if (SoldTxt != "")
                            {
                                Form1_0.method_1_SoldItems(SoldTxt, ThisCol);
                                //Form1_0.ItemsViewer_0.AddBufferPicture("Sold");
                            }
                            break;
                        }
                    }

                    LastitemScreenPos = itemScreenPos;

                    if (Tries > MaxTries)
                    {
                        Form1_0.method_1("Item didn't sell correctly!", Color.OrangeRed);
                        break;
                    }
                }

                if (!Form1_0.Running || !Form1_0.GameStruc_0.IsInGame())
                {
                    break;
                }
            }
        }
        //}

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

        string BuyingThisPotion = "Super Healing Potion";
        if (!Form1_0.ItemsStruc_0.GetShopItem("Super Healing Potion") && BuyingThisPotion == "Super Healing Potion") BuyingThisPotion = "Greater Healing Potion";
        if (!Form1_0.ItemsStruc_0.GetShopItem("Greater Healing Potion") && BuyingThisPotion == "Greater Healing Potion") BuyingThisPotion = "Healing Potion";
        if (!Form1_0.ItemsStruc_0.GetShopItem("Healing Potion") && BuyingThisPotion == "Healing Potion") BuyingThisPotion = "Light Healing Potion";
        if (!Form1_0.ItemsStruc_0.GetShopItem("Light Healing Potion") && BuyingThisPotion == "Light Healing Potion") BuyingThisPotion = "Minor Healing Potion";
        if (!Form1_0.ItemsStruc_0.GetShopItem("Minor Healing Potion") && BuyingThisPotion == "Minor Healing Potion") BuyingThisPotion = "Potion of Life";

        while (Form1_0.BeltStruc_0.MissingHPPot && tries < 2)
        {
            Form1_0.SetGameStatus("TOWN-SHOP-BUY HP POTIONS");
            if (!Form1_0.Running || !Form1_0.GameStruc_0.IsInGame())
            {
                break;
            }
            if (Form1_0.ItemsStruc_0.GetShopItem(BuyingThisPotion))
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
                if (Form1_0.InventoryStruc_0.HasInventoryItemName(BuyingThisPotion) || Form1_0.BeltStruc_0.HasPotInBadSpot)
                {
                    int IncreaseCount = 0;
                    while (Form1_0.BeltStruc_0.HPQuantity != Form1_0.BeltStruc_0.ForceHPPotionQty && IncreaseCount < 15)
                    {
                        Form1_0.PatternsScan_0.IncreaseV1Scanning();
                        IncreaseCount++;
                        Form1_0.ItemsStruc_0.GetItems(false);
                    }

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

        BuyingThisPotion = "Super Mana Potion";
        if (!Form1_0.ItemsStruc_0.GetShopItem("Super Mana Potion") && BuyingThisPotion == "Super Mana Potion") BuyingThisPotion = "Greater Mana Potion";
        if (!Form1_0.ItemsStruc_0.GetShopItem("Greater Mana Potion") && BuyingThisPotion == "Greater Mana Potion") BuyingThisPotion = "Mana Potion";
        if (!Form1_0.ItemsStruc_0.GetShopItem("Mana Potion") && BuyingThisPotion == "Mana Potion") BuyingThisPotion = "Light Mana Potion";
        if (!Form1_0.ItemsStruc_0.GetShopItem("Light Mana Potion") && BuyingThisPotion == "Light Mana Potion") BuyingThisPotion = "Minor Mana Potion";

        while (Form1_0.BeltStruc_0.MissingManaPot && tries < 2)
        {
            Form1_0.SetGameStatus("TOWN-SHOP-BUY MANA POTIONS");
            if (!Form1_0.Running || !Form1_0.GameStruc_0.IsInGame())
            {
                break;
            }
            if (Form1_0.ItemsStruc_0.GetShopItem(BuyingThisPotion))
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
                if (Form1_0.InventoryStruc_0.HasInventoryItemName(BuyingThisPotion) || Form1_0.BeltStruc_0.HasPotInBadSpot)
                {
                    int IncreaseCount = 0;
                    while (Form1_0.BeltStruc_0.ManyQuantity != Form1_0.BeltStruc_0.ForceMANAPotionQty && IncreaseCount < 10)
                    {
                        Form1_0.PatternsScan_0.IncreaseV1Scanning();
                        IncreaseCount++;
                        Form1_0.ItemsStruc_0.GetItems(false);
                    }

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
        if (Form1_0.InventoryStruc_0.HasIDTome)
        {
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
        }

        //buy key
        if (CharConfig.UseKeys)
        {
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

                    //Buy keys again to fill inventory
                    /*if (StartQty == 0)
                    {
                        Form1_0.KeyMouse_0.SendSHIFT_RIGHTCLICK(itemScreenPos["x"], itemScreenPos["y"]);
                        //Form1_0.KeyMouse_0.MouseCliccRight(itemScreenPos["x"], itemScreenPos["y"]);
                        Form1_0.WaitDelay(20);
                        Form1_0.ItemsStruc_0.GetItems(false);   //get inventory
                    }*/
                }

                if (Form1_0.InventoryStruc_0.HUDItems_keys == StartQty)
                {
                    tries++;
                }
                StartQty = Form1_0.InventoryStruc_0.HUDItems_keys;
            }
        }

        //buy tome of portal for cows level
        if (ShopForTomeOfPortal)
        {
            bool HasTownPortal = Form1_0.InventoryStruc_0.HasInventoryItemName("Tome of Town Portal", true);
            tries = 0;
            while (!HasTownPortal && tries < 1)
            {
                Form1_0.SetGameStatus("TOWN-SHOP-BUY TOME PORTAL");
                if (!Form1_0.Running || !Form1_0.GameStruc_0.IsInGame())
                {
                    break;
                }
                if (Form1_0.ItemsStruc_0.GetShopItem("Tome of Town Portal"))
                {
                    Dictionary<string, int> itemScreenPos = ConvertShopLocToScreenPos(Form1_0.ItemsStruc_0.itemx, Form1_0.ItemsStruc_0.itemy);

                    Form1_0.KeyMouse_0.MouseClicc(itemScreenPos["x"], itemScreenPos["y"]);
                    Form1_0.WaitDelay(20);
                    Form1_0.KeyMouse_0.MouseClicc(itemScreenPos["x"], itemScreenPos["y"] + 15);
                    Form1_0.WaitDelay(10);
                    Form1_0.ItemsStruc_0.GetItems(false);   //get inventory
                }

                HasTownPortal = Form1_0.InventoryStruc_0.HasInventoryItemName("Tome of Town Portal", true);
                if (!HasTownPortal) tries++;
            }

            if (HasTownPortal) ShopForTomeOfPortal = false;
        }

        //ShopBot
        if (CharConfig.RunShopBotScript && !Form1_0.ShopBot_0.ScriptDone && Form1_0.ShopBot_0.CurrentStep > 0)
        {
            Form1_0.ItemsStruc_0.ShopBotGetPurchaseItems();
        }
        //Form1_0.ItemsStruc_0.ShopBotGetPurchaseItems();
    }
}
