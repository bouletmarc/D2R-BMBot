using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Enums;

public class Stash
{
    Form1 Form1_0;
    public bool StashFull = false;
    Dictionary<string, int> LastitemScreenPos = new Dictionary<string, int>();

    public int RunningScriptCount = 0;

    public bool MakingCowPortal = false;
    public int DeposingGoldCount = 0;

    public void SetForm1(Form1 form1_1)
    {
        Form1_0 = form1_1;
    }

    public void RunStashScript()
    {
        if (StashFull) return;

        Form1_0.UIScan_0.readUI();
        if (Form1_0.UIScan_0.GetMenuActive("npcInteract")) Form1_0.UIScan_0.CloseThisMenu("npcInteract");
        //if (!Form1_0.UIScan_0.leftMenu && !Form1_0.UIScan_0.rightMenu) return;

        Form1_0.WaitDelay(35);
        LastitemScreenPos = new Dictionary<string, int>();

        //move inventory into stash
        for (int i = 0; i < 40; i++)
        {
            if (!Form1_0.Running || !Form1_0.GameStruc_0.IsInGame())
            {
                break;
            }

            if ((CharConfig.InventoryDontCheckItem[i] == 0 && Form1_0.InventoryStruc_0.InventoryHasStashItem[i] >= 1)
                || (MakingCowPortal && Form1_0.InventoryStruc_0.InventoryItemNames[i] == "Tome of Town Portal"))
            {
                //################
                //GET ITEM (UNIQUE GC, GHEED, TORCH, ANNI)
                bool IsUniqueSpecial = false;
                Dictionary<string, int> itemXYPos = Form1_0.InventoryStruc_0.ConvertIndexToXY(i);
                if (Form1_0.ItemsStruc_0.GetSpecificItem(0, Form1_0.InventoryStruc_0.InventoryItemNames[i], itemXYPos["x"], itemXYPos["y"], Form1_0.PlayerScan_0.unitId, 0))
                {
                    if ((Form1_0.ItemsStruc_0.ItemNAAME == "Small Charm"
                        || Form1_0.ItemsStruc_0.ItemNAAME == "Large Charm"
                        || Form1_0.ItemsStruc_0.ItemNAAME == "Grand Charm")
                        && Form1_0.ItemsStruc_0.itemQuality == 7) //Unique
                    {
                        IsUniqueSpecial = true;
                    }
                }
                //################
                Form1_0.SetGameStatus("TOWN-STASH-ITEM:" + Form1_0.InventoryStruc_0.InventoryItemNames[i]);
                Form1_0.method_1_Items("Stashed: " + Form1_0.InventoryStruc_0.InventoryItemNames[i], Form1_0.ItemsStruc_0.GetColorFromQuality(Form1_0.InventoryStruc_0.InventoryItemQuality[i]));

                Dictionary<string, int> itemScreenPos = Form1_0.InventoryStruc_0.ConvertIndexToXY(i);
                itemScreenPos = Form1_0.InventoryStruc_0.ConvertInventoryLocToScreenPos(itemScreenPos["x"], itemScreenPos["y"]);

                int TryStashCount = 0;
                if (IsUniqueSpecial)
                {
                    TryStashCount = 1;
                    Form1_0.KeyMouse_0.MouseClicc(340, 200);   //clic shared stash1
                }
                while (true)
                {
                    int Tries = 0;
                    int MaxTries = 1;
                    while (true)
                    {
                        if (!Form1_0.Running || !Form1_0.GameStruc_0.IsInGame())
                        {
                            break;
                        }
                        Form1_0.UIScan_0.readUI();
                        if (!Form1_0.UIScan_0.leftMenu && !Form1_0.UIScan_0.rightMenu) return;

                        //CTRL+Clic to send item into stash
                        Form1_0.KeyMouse_0.SendCTRL_CLICK(itemScreenPos["x"], itemScreenPos["y"]);
                        Form1_0.WaitDelay(5);
                        Form1_0.ItemsStruc_0.GetItems(false);   //get inventory again
                        Form1_0.SetGameStatus("TOWN-STASH-ITEM:" + Form1_0.InventoryStruc_0.InventoryItemNames[i] + " (" + (Tries + 1) + "/" + MaxTries + ")");
                        PlaceItem(itemScreenPos["x"], itemScreenPos["y"]);
                        //PlaceItem(itemScreenPos["x"], itemScreenPos["y"]);

                        //item still in inventory
                        if (Form1_0.InventoryStruc_0.InventoryHasStashItem[i] >= 1)
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

                    if (!Form1_0.Running || !Form1_0.GameStruc_0.IsInGame())
                    {
                        break;
                    }
                    Form1_0.UIScan_0.readUI();
                    if (!Form1_0.UIScan_0.leftMenu && !Form1_0.UIScan_0.rightMenu) return;

                    //swap stash
                    if (Tries > MaxTries)
                    {
                        PlaceItem(itemScreenPos["x"], itemScreenPos["y"]);

                        //200-340-450-600
                        if (TryStashCount == 0)
                        {
                            Form1_0.KeyMouse_0.MouseClicc(200, 200);   //clic stash1
                        }
                        if (TryStashCount == 1)
                        {
                            Form1_0.KeyMouse_0.MouseClicc(340, 200);   //clic shared stash1
                        }
                        if (TryStashCount == 2)
                        {
                            Form1_0.KeyMouse_0.MouseClicc(450, 200);   //clic shared stash2
                        }
                        if (TryStashCount == 3)
                        {
                            Form1_0.KeyMouse_0.MouseClicc(600, 200);   //clic shared stash3
                        }
                        if (TryStashCount >= 4)
                        {
                            RunningScriptCount++;
                            if (RunningScriptCount >= CharConfig.StashFullTries) StashFull = true;
                            //StashFull = true; //##################################################
                            i = 40; //stash is full, dont try others items to stash
                            break;
                        }
                        TryStashCount++;
                        Tries = 0;
                    }
                    else
                    {
                        break;
                    }
                }


                PlaceItem(itemScreenPos["x"], itemScreenPos["y"]);
            }
        }

        if (!Form1_0.Running || !Form1_0.GameStruc_0.IsInGame())
        {
            return;
        }
        Form1_0.UIScan_0.readUI();
        if (!Form1_0.UIScan_0.leftMenu && !Form1_0.UIScan_0.rightMenu) return;

        //deposit gold
        if (DeposingGoldCount == 0) Form1_0.KeyMouse_0.MouseClicc(200, 200);   //clic stash1
        if (DeposingGoldCount == 1) Form1_0.KeyMouse_0.MouseClicc(340, 200);   //clic shared stash1
        if (DeposingGoldCount == 2) Form1_0.KeyMouse_0.MouseClicc(450, 200);   //clic shared stash2
        if (DeposingGoldCount == 3) Form1_0.KeyMouse_0.MouseClicc(600, 200);   //clic shared stash3
        DeposingGoldCount++;
        if (DeposingGoldCount > 3) DeposingGoldCount = 0;

        if (Form1_0.PlayerScan_0.PlayerGoldInventory > 0)
        {
            Form1_0.SetGameStatus("TOWN-STASH-DEPOSIT GOLD");
            Form1_0.KeyMouse_0.MouseClicc(1450, 790);  //clic deposit
            Form1_0.WaitDelay(25);
            Form1_0.KeyMouse_0.MouseClicc(820, 580);  //clic ok on deposit
            Form1_0.WaitDelay(25);
            Form1_0.PlayerScan_0.PlayerGoldInventory = 0;
        }

        //craft/cube item script here ###
        Form1_0.PlayerScan_0.GetPositions();
        Form1_0.ItemsStruc_0.GetItems(false);
        Form1_0.Cubing_0.PerformCubing();

        Form1_0.InventoryStruc_0.VerifyKeysInventory();

    }

    //Place item to cube
    public bool PlaceItemShift(int PosX, int PosY)
    {
        int Tryy = 0;
        while (Form1_0.ItemsStruc_0.ItemOnCursor && Tryy < 5)
        {
            Form1_0.KeyMouse_0.SendSHIFT_CLICK(PosX, PosY);
            Form1_0.WaitDelay(5);
            Form1_0.ItemsStruc_0.GetItems(false);   //get inventory again
            Tryy++;
        }
        if (Tryy >= 5)
        {
            return false;
        }
        return true;
    }

    public bool PlaceItem(int PosX, int PosY)
    {
        int Tryy = 0;
        while (Form1_0.ItemsStruc_0.ItemOnCursor && Tryy < 5)
        {
            Form1_0.KeyMouse_0.MouseClicc(PosX, PosY);
            Form1_0.WaitDelay(5);
            Form1_0.ItemsStruc_0.GetItems(false);   //get inventory again
            Tryy++;
        }
        if (Tryy >= 5)
        {
            return false;
        }
        return true;
    }

    public bool PickItem(int PosX, int PosY)
    {
        Form1_0.ItemsStruc_0.GetBadItemsOnCursor();

        int Tryy = 0;
        while (!Form1_0.ItemsStruc_0.ItemOnCursor && Tryy < 5)
        {
            Form1_0.KeyMouse_0.MouseClicc(PosX, PosY);
            Form1_0.WaitDelay(5);
            Form1_0.ItemsStruc_0.GetItems(false);   //get inventory again
            Tryy++;
        }
        if (Tryy >= 5)
        {
            return false;
        }
        return true;
    }
}
