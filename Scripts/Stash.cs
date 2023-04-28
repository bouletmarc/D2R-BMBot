using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static app.Enums;

namespace app
{
    public class Stash
    {
        Form1 Form1_0;
        public bool StashFull = false;
        Dictionary<string, int> LastitemScreenPos = new Dictionary<string, int>();

        public void SetForm1(Form1 form1_1)
        {
            Form1_0 = form1_1;
        }

        public void RunStashScript()
        {
            if (StashFull) return;

            Form1_0.WaitDelay(35);
            LastitemScreenPos = new Dictionary<string, int>();

            //move inventory into stash
            for (int i = 0; i < 40; i++)
            {
                if (!Form1_0.Running || !Form1_0.GameStruc_0.IsInGame())
                {
                    break;
                }

                if (CharConfig.InventoryDontCheckItem[i] == 0 && Form1_0.InventoryStruc_0.InventoryHasStashItem[i] >= 1)
                {
                    Form1_0.SetGameStatus("TOWN-STASH-ITEM:" + Form1_0.InventoryStruc_0.InventoryItemNames[i]);
                    Form1_0.method_1_Items("Stashed: " + Form1_0.InventoryStruc_0.InventoryItemNames[i], Color.DarkTurquoise);

                    Dictionary<string, int> itemScreenPos = Form1_0.InventoryStruc_0.ConvertIndexToXY(i);
                    itemScreenPos = Form1_0.InventoryStruc_0.ConvertInventoryLocToScreenPos(itemScreenPos["x"], itemScreenPos["y"]);

                    int TryStashCount = 0;
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
                            //CTRL+Clic to send item into stash
                            Form1_0.KeyMouse_0.SendCTRL_CLICK(itemScreenPos["x"], itemScreenPos["y"]);
                            Form1_0.WaitDelay(5);
                            Form1_0.ItemsStruc_0.GetItems(false);   //get inventory again
                            Form1_0.SetGameStatus("TOWN-STASH-ITEM:" + Form1_0.InventoryStruc_0.InventoryItemNames[i]);
                            PlaceItem(itemScreenPos["x"], itemScreenPos["y"]);

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

                        //swap stash
                        if (Tries > MaxTries)
                        {
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
                                //StashFull = true; //##################################################
                                i = 40; //stash is full, dont try others items to stash
                                break;
                            }
                            TryStashCount++;
                        }
                        else
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

            //deposit gold
            Form1_0.SetGameStatus("TOWN-STASH-DEPOSIT GOLD");
            Form1_0.KeyMouse_0.MouseClicc(1450, 790);  //clic deposit
            Form1_0.WaitDelay(25);
            Form1_0.KeyMouse_0.MouseClicc(820, 580);  //clic ok on deposit
            Form1_0.WaitDelay(25);

            //craft/cube item script here ###
            Form1_0.PlayerScan_0.GetPositions();
            Form1_0.ItemsStruc_0.GetItems(false);
            Form1_0.Cubing_0.PerformCubing();
        }

        public bool PlaceItem(int PosX, int PosY)
        {
            int Tryy = 0;
            while (Form1_0.ItemsStruc_0.ItemOnCursor && Tryy < 5)
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

        public bool PickItem(int PosX, int PosY)
        {
            int Tryy = 0;
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
    }
}
