using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static app.MapAreaStruc;

namespace app
{
    public class Town
    {
        Form1 Form1_0;

        public int TownAct = 0;
        public bool Towning = true;
        public bool ForcedTowning = false;
        public bool FastTowning = false;
        public bool IsInTown = false;
        public bool TPSpawned = false;
        public bool UseLastTP = true;
        public int ScriptTownAct = 5;       //default should be 0
        public int TriedToStashCount = 0;
        public int TriedToGambleCount = 0;
        public int TriedToShopCount = 0;
        public int TriedToMercCount = 0;
        public int TriedToUseTPCount = 0;
        public int CurrentScript = 0;
        public int TriedToShopCount2 = 0;

        public uint LastUsedTPID = 0;
        public int LastUsedTPCount = 0;
        public List<uint> IgnoredTPList = new List<uint>();
        public bool FirstTown = true;

        public void SetForm1(Form1 form1_1)
        {
            Form1_0 = form1_1;
        }

        public void RunTownScript()
        {
            if (!ShouldBeInTown())
            {
                return;
            }

            Form1_0.SetGameStatus("TOWN");

            //dead leave game
            if (Form1_0.PlayerScan_0.PlayerDead || Form1_0.Potions_0.ForceLeave)
            {
                Form1_0.Potions_0.ForceLeave = true;
                Form1_0.Baal_0.SearchSameGamesAsLastOne = false;
                Form1_0.LeaveGame(false);
                return;
            }

            //item grab only -> no town
            if (Towning && CharConfig.RunItemGrabScriptOnly)
            {
                CurrentScript = 0;
                TriedToStashCount = 0;
                TriedToGambleCount = 0;
                TriedToShopCount = 0;
                TriedToShopCount2 = 0;
                TriedToMercCount = 0;
                TriedToUseTPCount = 0;
                Towning = false;
                FastTowning = false;
                ForcedTowning = false;
                UseLastTP = true;
                return;
            }

            if (!GetInTown())
            {
                Form1_0.SetGameStatus("TOWN-TP TO TOWN");
                Form1_0.Potions_0.CheckIfWeUsePotion();

                if (FastTowning && !Form1_0.Shop_0.ShouldShop())
                {
                    CurrentScript = 0;
                    TriedToStashCount = 0;
                    TriedToGambleCount = 0;
                    TriedToShopCount = 0;
                    TriedToShopCount2 = 0;
                    TriedToMercCount = 0;
                    TriedToUseTPCount = 0;
                    Towning = false;
                    FastTowning = false;
                    ForcedTowning = false;
                    UseLastTP = true;
                    return;
                }

                if (TriedToUseTPCount >= 5)
                {
                    Form1_0.method_1("NO TP FOUND NEAR WHEN TRYING TO TOWN", Color.Red);
                    Form1_0.LeaveGame(false);
                    return;
                }

                if (TPSpawned)
                {
                    //select the spawned TP
                    if (Form1_0.ObjectsStruc_0.GetObjects("TownPortal", true, IgnoredTPList))
                    //if (Form1_0.ObjectsStruc_0.GetObjects("TownPortal", Form1_0.PlayerScan_0.unitId))
                    {
                        if (Form1_0.ObjectsStruc_0.itemx != 0 && Form1_0.ObjectsStruc_0.itemy != 0)
                        {
                            Form1_0.method_1("Trying to use TP ID: " + Form1_0.ObjectsStruc_0.ObjectUnitID, Color.Red);

                            if (LastUsedTPID != Form1_0.ObjectsStruc_0.ObjectUnitID)
                            {
                                LastUsedTPID = Form1_0.ObjectsStruc_0.ObjectUnitID;
                                LastUsedTPCount = 0;
                            }
                            else
                            {
                                LastUsedTPCount++;
                                if (LastUsedTPCount >= 4)
                                {
                                    IgnoredTPList.Add(LastUsedTPID);
                                }
                            }

                            GetCorpse();
                            CurrentScript = 0;
                            Dictionary<string, int> itemScreenPos = Form1_0.GameStruc_0.World2Screen(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, Form1_0.ObjectsStruc_0.itemx, Form1_0.ObjectsStruc_0.itemy);
                            Form1_0.KeyMouse_0.MouseClicc(itemScreenPos["x"], itemScreenPos["y"] - 15);
                            Form1_0.WaitDelay(50);
                            Form1_0.Mover_0.FinishMoving();

                            //Form1_0.Mover_0.MoveToLocation(5055, 5039); //act4 only
                        }
                        else
                        {
                            TPSpawned = false;
                            TriedToUseTPCount++;
                        }
                    }
                    else
                    {
                        TPSpawned = false;
                        TriedToUseTPCount++;
                    }
                }
                else
                {
                    SpawnTP();
                }
            }
            else
            {
                //switch town
                if (CurrentScript == 0)
                {
                    if (IsInRightTown())
                    {
                        //Grab Corpse
                        if (Form1_0.ItemsStruc_0.ItemsEquiped <= 2 && FirstTown)
                        {
                            //int Tries = 0;
                            //while (Tries < 5)
                            //{
                                //Console.WriteLine("Corpse found method2");
                                Form1_0.method_1("Grab corpse #3", Color.Red);
                                Form1_0.WaitDelay(100);
                                //Clic corpse
                                FirstTown = false;
                                Dictionary<string, int> itemScreenPos = Form1_0.GameStruc_0.World2Screen(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal);
                                Form1_0.KeyMouse_0.MouseClicc(itemScreenPos["x"] - 45, itemScreenPos["y"] - 5);
                                //Form1_0.WaitDelay(100);
                                //Tries++;
                            //}
                        }

                        GetCorpse();
                        CurrentScript++;
                    }

                    if (CurrentScript == 0)
                    {
                        if (!IsInRightTown())
                        {
                            Form1_0.SetGameStatus("TOWN-SWITCH TOWN");
                            GoToWPArea();
                        }
                    }
                }

                //ID Items
                if (CurrentScript == 1)
                {
                    if (!Form1_0.InventoryStruc_0.HasUnidItemInInventory() || FastTowning)
                    {
                        CurrentScript++;
                    }

                    if (CurrentScript == 1)
                    {
                        if (Form1_0.InventoryStruc_0.HasUnidItemInInventory() && !FastTowning)
                        {
                            Form1_0.SetGameStatus("TOWN-CAIN");
                            MoveToCain();
                        }
                    }
                }

                //relive merc
                if (CurrentScript == 2)
                {
                    bool ShouldReliveMerc = false;
                    if (CharConfig.UsingMerc)
                    {
                        Form1_0.MercStruc_0.GetMercInfos();
                        ShouldReliveMerc = !Form1_0.MercStruc_0.MercAlive;
                    }

                    if (!ShouldReliveMerc || TriedToMercCount >= 3
                        || (Form1_0.PlayerScan_0.PlayerGoldInventory + Form1_0.PlayerScan_0.PlayerGoldInStash) < 75000
                        || FastTowning)
                    {
                        CurrentScript++;
                    }

                    if (CurrentScript == 2)
                    {
                        if (ShouldReliveMerc && TriedToMercCount < 3
                        && (Form1_0.PlayerScan_0.PlayerGoldInventory + Form1_0.PlayerScan_0.PlayerGoldInStash) >= 75000
                        && !FastTowning)
                        {
                            MoveToMerc();
                            TriedToMercCount++;
                        }
                    }

                }

                //stash items
                if (CurrentScript == 3)
                {
                    if (Form1_0.InventoryStruc_0.HasUnidItemInInventory() && !FastTowning)
                    {
                        //return to identify script, still contain unid item
                        CurrentScript = 1;
                    }

                    if ((!Form1_0.InventoryStruc_0.ContainStashItemInInventory() && (Form1_0.PlayerScan_0.PlayerGoldInventory < 35000))
                                || TriedToStashCount >= 6 || FastTowning)
                    {
                        CurrentScript++;
                    }

                    if (CurrentScript == 3)
                    {
                        if ((Form1_0.InventoryStruc_0.ContainStashItemInInventory() || (Form1_0.PlayerScan_0.PlayerGoldInventory >= 35000))
                                && TriedToStashCount < 6 && !FastTowning)
                        {
                            //Console.WriteLine(Form1_0.InventoryStruc_0.ContainStashItemInInventory() + "|" + (Form1_0.PlayerScan_0.PlayerGoldInventory >= 35000));

                            Form1_0.method_1("Stash: item(" + Form1_0.InventoryStruc_0.ContainStashItemInInventory() + ") | Gold(" + (Form1_0.PlayerScan_0.PlayerGoldInventory >= 35000) + ")", Color.Red);
                            Form1_0.SetGameStatus("TOWN-STASH");
                            MoveToStash(true);
                            TriedToStashCount++;
                        }
                    }
                }

                //gamble
                if (CurrentScript == 4)
                {
                    if (!Form1_0.Gamble_0.CanGamble() || TriedToGambleCount >= 3 || FastTowning)
                    {
                        CurrentScript++;
                    }

                    if (CurrentScript == 4)
                    {
                        if (Form1_0.Gamble_0.CanGamble() && TriedToGambleCount < 3 && !FastTowning)
                        {
                            TriedToStashCount = 0;
                            Form1_0.SetGameStatus("TOWN-GAMBLE");
                            MoveToGamble();
                            TriedToGambleCount++;
                        }
                    }
                }

                //buy potions,tp,etc
                if (CurrentScript == 5)
                {
                    Form1_0.ItemsStruc_0.GetItems(false);
                    if ((Form1_0.InventoryStruc_0.ContainStashItemInInventory())
                        && !FastTowning)
                    {
                        //return to stash script, still contain item
                        TriedToStashCount = 0;
                        CurrentScript = 3;
                    }
                    else
                    {
                        if (!Form1_0.Shop_0.ShouldShop() || TriedToShopCount >= 6)
                        {
                            //Console.WriteLine("town shop done");
                            CurrentScript++;
                        }

                        if (CurrentScript == 5)
                        {
                            if (Form1_0.Shop_0.ShouldShop() && TriedToShopCount < 6)
                            {
                                Form1_0.SetGameStatus("TOWN-SHOP");
                                //Console.WriteLine("town moving to shop");
                                MoveToStore();
                                TriedToShopCount++;

                                //if (FastTowning) TriedToShopCount = 6;
                            }
                        }
                    }
                }

                //check for repair
                if (CurrentScript == 6)
                {
                    if (!Form1_0.Repair_0.GetShouldRepair() || TriedToShopCount >= 12 || FastTowning)
                    {
                        CurrentScript++;
                    }

                    if (CurrentScript == 6)
                    {
                        if (Form1_0.Repair_0.GetShouldRepair() && TriedToShopCount < 12 && !FastTowning)
                        {
                            Form1_0.SetGameStatus("TOWN-REPAIR");
                            MoveToRepair();
                            TriedToShopCount++;
                        }
                    }
                }

                //end towning script
                if (CurrentScript == 7)
                {
                    Form1_0.SetGameStatus("TOWN-END");

                    if (MoveToTPOrWPSpot())
                    {
                        GetCorpse();
                        Form1_0.Stash_0.RunningScriptCount = 0;
                        CurrentScript = 0;
                        TriedToStashCount = 0;
                        TriedToGambleCount = 0;
                        TriedToShopCount = 0;
                        TriedToShopCount2 = 0;
                        TriedToMercCount = 0;
                        TriedToUseTPCount = 0;
                        Towning = false;
                        FastTowning = false;
                        ForcedTowning = false;
                        UseLastTP = true;
                    }
                }
            }
        }

        public bool IsInRightTown()
        {
            if (ScriptTownAct == 0)
            {
                return true; //perform current town operation
            }
            if (TownAct != ScriptTownAct)
            {
                return false;
            }
            return true;
        }

        public void GoToWPArea(int SelectActWPIndex = -1, int SelectWPIndex = -1)
        {
            if (TownAct == 1)
            {
                //if (Form1_0.Mover_0.MoveToLocation(4681, 4541))
                //{
                    //use wp
                    /*if (Form1_0.ObjectsStruc_0.GetObjects("WaypointPortal", false))
                    {
                        if (Form1_0.Mover_0.MoveToLocation(Form1_0.ObjectsStruc_0.itemx, Form1_0.ObjectsStruc_0.itemy))
                        {
                            if (Form1_0.ObjectsStruc_0.GetObjects("WaypointPortal", false))
                            {
                                Dictionary<string, int> itemScreenPos = Form1_0.GameStruc_0.World2Screen(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, Form1_0.ObjectsStruc_0.itemx, Form1_0.ObjectsStruc_0.itemy);
                                Form1_0.KeyMouse_0.MouseClicc(itemScreenPos["x"], itemScreenPos["y"] - 15);
                                Form1_0.Mover_0.FinishMoving();
                                if (Form1_0.UIScan_0.WaitTilUIOpen("waypointMenu"))
                                {
                                    if (SelectWPIndex == -1)
                                    {
                                        SelectTownWP();
                                    }
                                    else
                                    {
                                        SelectThisWPIndex(SelectActWPIndex, SelectWPIndex);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {*/
                        //"id":119, "type":"object", "x":84, "y":69, "name":"Waypoint",
                        Position ThisFinalPosition = Form1_0.MapAreaStruc_0.GetPositionOfObject("object", "WaypointPortal", 1 - 1, new List<int>() { });
                        if (Form1_0.Mover_0.MoveToLocation(ThisFinalPosition.X, ThisFinalPosition.Y))
                        {
                            Dictionary<string, int> itemScreenPos = Form1_0.GameStruc_0.World2Screen(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, ThisFinalPosition.X, ThisFinalPosition.Y);
                            Form1_0.KeyMouse_0.MouseClicc(itemScreenPos["x"], itemScreenPos["y"] - 15);
                            Form1_0.Mover_0.FinishMoving();
                            if (Form1_0.UIScan_0.WaitTilUIOpen("waypointMenu"))
                            {
                                if (SelectWPIndex == -1)
                                {
                                    SelectTownWP();
                                }
                                else
                                {
                                    SelectThisWPIndex(SelectActWPIndex, SelectWPIndex);
                                }
                            }
                        }
                        //Form1_0.method_1("NO WP FOUND NEAR IN TOWN", Color.OrangeRed);
                    //}
                //}
            }
            /*if (TownAct == 2)
            {
            }*/
            if (TownAct == 3)
            {
                if (Form1_0.Mover_0.MoveToLocation(5134, 5107))
                {
                    if (Form1_0.Mover_0.MoveToLocation(5154, 5056))
                    {
                        //use wp
                        /*if (Form1_0.ObjectsStruc_0.GetObjects("Act3TownWaypoint", false))
                        {
                            Dictionary<string, int> itemScreenPos = Form1_0.GameStruc_0.World2Screen(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, Form1_0.ObjectsStruc_0.itemx, Form1_0.ObjectsStruc_0.itemy);
                            Form1_0.KeyMouse_0.MouseClicc(itemScreenPos["x"], itemScreenPos["y"] - 15);
                            Form1_0.Mover_0.FinishMoving();
                            if (Form1_0.UIScan_0.WaitTilUIOpen("waypointMenu"))
                            {
                                if (SelectWPIndex == -1)
                                {
                                    SelectTownWP();
                                }
                                else
                                {
                                    SelectThisWPIndex(SelectActWPIndex, SelectWPIndex);
                                }
                            }
                        }
                        else
                        {*/
                            Position ThisFinalPosition = Form1_0.MapAreaStruc_0.GetPositionOfObject("object", "Act3TownWaypoint", 75 - 1, new List<int>() { });
                            if (Form1_0.Mover_0.MoveToLocation(ThisFinalPosition.X, ThisFinalPosition.Y))
                            {
                                Dictionary<string, int> itemScreenPos = Form1_0.GameStruc_0.World2Screen(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, ThisFinalPosition.X, ThisFinalPosition.Y);
                                Form1_0.KeyMouse_0.MouseClicc(itemScreenPos["x"], itemScreenPos["y"] - 15);
                                Form1_0.Mover_0.FinishMoving();
                                if (Form1_0.UIScan_0.WaitTilUIOpen("waypointMenu"))
                                {
                                    if (SelectWPIndex == -1)
                                    {
                                        SelectTownWP();
                                    }
                                    else
                                    {
                                        SelectThisWPIndex(SelectActWPIndex, SelectWPIndex);
                                    }
                                }
                            }
                            //Form1_0.method_1("NO WP FOUND NEAR IN TOWN", Color.OrangeRed);
                        //}
                    }
                }
            }
            if (TownAct == 4)
            {
                if (Form1_0.Mover_0.MoveToLocation(5055, 5039))
                {
                    //use wp
                    /*if (Form1_0.ObjectsStruc_0.GetObjects("PandamoniumFortressWaypoint", false))
                    {
                        Dictionary<string, int> itemScreenPos = Form1_0.GameStruc_0.World2Screen(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, Form1_0.ObjectsStruc_0.itemx, Form1_0.ObjectsStruc_0.itemy);
                        Form1_0.KeyMouse_0.MouseClicc(itemScreenPos["x"], itemScreenPos["y"] - 15);
                        Form1_0.Mover_0.FinishMoving();
                        if (Form1_0.UIScan_0.WaitTilUIOpen("waypointMenu"))
                        {
                            if (SelectWPIndex == -1)
                            {
                                SelectTownWP();
                            }
                            else
                            {
                                SelectThisWPIndex(SelectActWPIndex, SelectWPIndex);
                            }
                        }
                    }
                    else
                    {*/
                        Position ThisFinalPosition = Form1_0.MapAreaStruc_0.GetPositionOfObject("object", "PandamoniumFortressWaypoint", 103 - 1, new List<int>() { });
                        if (Form1_0.Mover_0.MoveToLocation(ThisFinalPosition.X, ThisFinalPosition.Y))
                        {
                            Dictionary<string, int> itemScreenPos = Form1_0.GameStruc_0.World2Screen(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, ThisFinalPosition.X, ThisFinalPosition.Y);
                            Form1_0.KeyMouse_0.MouseClicc(itemScreenPos["x"], itemScreenPos["y"] - 15);
                            Form1_0.Mover_0.FinishMoving();
                            if (Form1_0.UIScan_0.WaitTilUIOpen("waypointMenu"))
                            {
                                if (SelectWPIndex == -1)
                                {
                                    SelectTownWP();
                                }
                                else
                                {
                                    SelectThisWPIndex(SelectActWPIndex, SelectWPIndex);
                                }
                            }
                        }
                        //Form1_0.method_1("NO WP FOUND NEAR IN TOWN", Color.OrangeRed);
                    //}
                }
            }
            if (TownAct == 5)
            {
                //move close to stash location
                if (Form1_0.Mover_0.MoveToLocation(5117, 5065))
                {
                    //use wp
                    /*if (Form1_0.ObjectsStruc_0.GetObjects("ExpansionWaypoint", false))
                    {
                        Dictionary<string, int> itemScreenPos = Form1_0.GameStruc_0.World2Screen(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, Form1_0.ObjectsStruc_0.itemx, Form1_0.ObjectsStruc_0.itemy);
                        Form1_0.KeyMouse_0.MouseClicc(itemScreenPos["x"], itemScreenPos["y"] - 15);
                        Form1_0.Mover_0.FinishMoving();
                        if (Form1_0.UIScan_0.WaitTilUIOpen("waypointMenu"))
                        {
                            if (SelectWPIndex == -1)
                            {
                                SelectTownWP();
                            }
                            else
                            {
                                SelectThisWPIndex(SelectActWPIndex, SelectWPIndex);
                            }
                        }
                    }
                    else
                    {*/
                        Position ThisFinalPosition = Form1_0.MapAreaStruc_0.GetPositionOfObject("object", "ExpansionWaypoint", 109 - 1, new List<int>() { });
                        if (Form1_0.Mover_0.MoveToLocation(ThisFinalPosition.X, ThisFinalPosition.Y))
                        {
                            Dictionary<string, int> itemScreenPos = Form1_0.GameStruc_0.World2Screen(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, ThisFinalPosition.X, ThisFinalPosition.Y);
                            Form1_0.KeyMouse_0.MouseClicc(itemScreenPos["x"], itemScreenPos["y"] - 15);
                            Form1_0.Mover_0.FinishMoving();
                            if (Form1_0.UIScan_0.WaitTilUIOpen("waypointMenu"))
                            {
                                if (SelectWPIndex == -1)
                                {
                                    SelectTownWP();
                                }
                                else
                                {
                                    SelectThisWPIndex(SelectActWPIndex, SelectWPIndex);
                                }
                            }
                        }
                        //Form1_0.method_1("NO WP FOUND NEAR IN TOWN", Color.OrangeRed);
                    //}
                }
            }
        }

        public void SelectThisWPIndex(int ThisActIndexx, int ThisIndexx)
        {
            //select town
            if (ThisActIndexx == 1) Form1_0.KeyMouse_0.MouseClicc(235, 220);
            if (ThisActIndexx == 2) Form1_0.KeyMouse_0.MouseClicc(325, 220);
            if (ThisActIndexx == 3) Form1_0.KeyMouse_0.MouseClicc(415, 220);
            if (ThisActIndexx == 4) Form1_0.KeyMouse_0.MouseClicc(500, 220);
            if (ThisActIndexx == 5) Form1_0.KeyMouse_0.MouseClicc(585, 220);
            Form1_0.WaitDelay(50);

            //select WP from index
            Form1_0.KeyMouse_0.MouseClicc(285, 260 + (ThisIndexx * 60));
            Form1_0.UIScan_0.WaitTilUIClose("waypointMenu");
            Form1_0.UIScan_0.WaitTilUIClose("loading");
            Form1_0.WaitDelay(350);
        }

        public void SelectTownWP()
        {
            //select town
            if (ScriptTownAct == 1) Form1_0.KeyMouse_0.MouseClicc(235, 220);
            if (ScriptTownAct == 2) Form1_0.KeyMouse_0.MouseClicc(325, 220);
            if (ScriptTownAct == 3) Form1_0.KeyMouse_0.MouseClicc(415, 220);
            if (ScriptTownAct == 4) Form1_0.KeyMouse_0.MouseClicc(500, 220);
            if (ScriptTownAct == 5) Form1_0.KeyMouse_0.MouseClicc(585, 220);
            Form1_0.WaitDelay(50);

            Form1_0.KeyMouse_0.MouseClicc(285, 270); //select first wp
            Form1_0.UIScan_0.WaitTilUIClose("waypointMenu");
            Form1_0.UIScan_0.WaitTilUIClose("loading");
            Form1_0.WaitDelay(350);
        }

        public bool ShouldBeInTown()
        {
            if (ForcedTowning) return true;
            if (GetInTown() && Towning) return true;

            bool ShouldBe = false;
            if (Form1_0.InventoryStruc_0.HasUnidItemInInventory())
            {
                ShouldBe = true;
            }
            if (Form1_0.InventoryStruc_0.ContainStashItemInInventory())
            {
                ShouldBe = true;
            }
            if (Form1_0.Shop_0.ShouldShop())
            {
                ShouldBe = true;
            }
            if (Form1_0.Repair_0.GetShouldRepair())
            {
                ShouldBe = true;
            }
            if (Form1_0.Gamble_0.CanGamble())
            {
                ShouldBe = true;
            }

            bool ShouldReliveMerc = false;
            if (CharConfig.UsingMerc)
            {
                Form1_0.MercStruc_0.GetMercInfos();
                ShouldReliveMerc = !Form1_0.MercStruc_0.MercAlive;
            }
            if (ShouldReliveMerc && (Form1_0.PlayerScan_0.PlayerGoldInventory + Form1_0.PlayerScan_0.PlayerGoldInStash) >= 75000)
            {
                ShouldBe = true;
            }
            if ((Form1_0.PlayerScan_0.PlayerGoldInventory >= 35000))
            {
                ShouldBe = true;
            }


            if (Towning && !ShouldBe)
            {
                Towning = false;
            }
            return ShouldBe;
        }

        public void CheckForNPCValidPos(string ThisNPC)
        {
            if (Form1_0.NPCStruc_0.GetNPC(ThisNPC))
            {
                FixNPCPos(Form1_0.NPCStruc_0.xPosFinal, Form1_0.NPCStruc_0.yPosFinal);
            }
            else
            {
                Form1_0.method_1(ThisNPC.ToUpper() + " NOT FOUND NEAR", Color.OrangeRed);

                if (ThisNPC == "DeckardCain" && TownAct == 4)
                {
                    Form1_0.Mover_0.MoveToLocation(5092, 5044);
                }
                if (ThisNPC == "Anya" && TownAct == 4)
                {
                    Form1_0.Mover_0.MoveToLocation(5114, 5059);
                }
                else
                {
                    MoveToStash(false);
                }
            }
        }

        public void FixNPCPos(int NPCX, int NPCY)
        {
            //detected bad NPC world position, lets visit stash to fix their pos
            if (NPCX == 0 && NPCY == 0)
            {
                MoveToStash(false);
            }
        }

        public bool MoveToTPOrWPSpot()
        {
            bool MovedCorrectly = false;
            if (TownAct == 4)
            {
                if (IsPosCloseTo(5082, 5043, 12))
                {
                    Form1_0.Mover_0.MoveToLocation(5082, 5043);
                }

                //if (Form1_0.Mover_0.MoveToLocation(5082, 5043))
                //{
                    if (Form1_0.Mover_0.MoveToLocation(5055, 5039))
                    {
                        MovedCorrectly = true;
                    }
                //}
            }
            if (TownAct == 5)
            {
                //stuck between cain and malah
                if (IsPosCloseTo(5080, 5054, 10))
                {
                    //move close to malah location
                    Form1_0.Mover_0.MoveToLocation(5078, 5026);
                }

                //stuck above stash
                if (IsPosCloseTo(5097, 5042, 4))
                {
                    //move back to TP
                    Form1_0.Mover_0.MoveToLocation(5104, 5030);
                }

                //stuck near stash
                if (IsPosCloseTo(5021, 5056, 4))
                {
                    //move back to TP
                    Form1_0.Mover_0.MoveToLocation(5108, 5060);
                }

                if (Form1_0.Mover_0.MoveToLocation(5103, 5029))
                {
                    MovedCorrectly = true;
                }
            }

            // DO OTHER ACT SCRIPT HERE ###

            if (MovedCorrectly)
            {
                if (UseLastTP)
                {
                    if (TPSpawned)
                    {
                        //use tp
                        if (Form1_0.ObjectsStruc_0.GetObjects("TownPortal", true))
                        {
                            Dictionary<string, int> itemScreenPos = Form1_0.GameStruc_0.World2Screen(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, Form1_0.ObjectsStruc_0.itemx, Form1_0.ObjectsStruc_0.itemy);
                            Form1_0.KeyMouse_0.MouseClicc(itemScreenPos["x"], itemScreenPos["y"] - 15);
                        }
                        else
                        {
                            Form1_0.method_1("NO TP FOUND NEAR IN TOWN", Color.OrangeRed);
                        }
                    }
                    /*else
                    {
                        //use wp
                        if (Form1_0.ObjectsStruc_0.GetObjects("PandamoniumFortressWaypoint"))
                        {
                            Dictionary<string, int> itemScreenPos = Form1_0.GameStruc_0.World2Screen(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, Form1_0.ObjectsStruc_0.itemx, Form1_0.ObjectsStruc_0.itemy);
                            Form1_0.MouseClicc(itemScreenPos["x"], itemScreenPos["y"] - 15);
                        }
                        else
                        {
                            Form1_0.method_1("NO TP FOUND NEAR IN TOWN");
                        }
                    }*/
                }
            }

            return MovedCorrectly;
        }

        public bool IsPosCloseTo(int TX, int TY, int Offset)
        {
            Form1_0.PlayerScan_0.GetPositions();
            if (Form1_0.PlayerScan_0.xPosFinal >= TX - Offset
                    && Form1_0.PlayerScan_0.xPosFinal <= TX + Offset
                    && Form1_0.PlayerScan_0.yPosFinal >= TY - Offset
                    && Form1_0.PlayerScan_0.yPosFinal <= TY + Offset)
            {
                return true;
            }
            return false;
        }

        public void MoveToGamble()
        {
            bool MovedCorrectly = false;

            if (TownAct == 5)
            {
                CheckForNPCValidPos("Anya");

                //close to store spot 5078, 5026
                if (IsPosCloseTo(5078, 5026, 10))
                {
                    //move close to tp location
                    Form1_0.Mover_0.MoveToLocation(5103, 5029);
                    Form1_0.Mover_0.MoveToLocation(5114, 5059);
                }

                //stuck above stash
                if (IsPosCloseTo(5116, 5046, 4))
                {
                    //move back inbetween TP and WP location
                    Form1_0.Mover_0.MoveToLocation(5104, 5047);
                }

                //move close to store location
                //5094,5113 corner2
                if (Form1_0.Mover_0.MoveToLocation(5128, 5112)) //corner1
                {
                    //get store location
                    if (Form1_0.NPCStruc_0.GetNPC("Anya"))
                    {
                        if (Form1_0.Mover_0.MoveToLocation(Form1_0.NPCStruc_0.xPosFinal, Form1_0.NPCStruc_0.yPosFinal))
                        {
                            MovedCorrectly = true;
                        }
                    }
                }
            }

            // DO OTHER ACT SCRIPT HERE ###

            if (MovedCorrectly)
            {
                //Clic store
                Dictionary<string, int> itemScreenPos = Form1_0.GameStruc_0.World2Screen(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, Form1_0.NPCStruc_0.xPosFinal, Form1_0.NPCStruc_0.yPosFinal);
                Form1_0.KeyMouse_0.MouseClicc(itemScreenPos["x"], itemScreenPos["y"]);
                Form1_0.Mover_0.FinishMoving();
                if (Form1_0.UIScan_0.WaitTilUIOpen("npcInteract"))  //npcShop
                {
                    if (TownAct == 5)
                    {
                        Form1_0.KeyMouse_0.PressKey(System.Windows.Forms.Keys.Down); //Anya press down
                        Form1_0.KeyMouse_0.PressKey(System.Windows.Forms.Keys.Down); //Anya press down
                    }
                    Form1_0.KeyMouse_0.PressKey(System.Windows.Forms.Keys.Enter);
                    Form1_0.WaitDelay(50);
                    Form1_0.Gamble_0.RunGambleScript();
                    Form1_0.UIScan_0.CloseUIMenu("npcInteract");
                    Form1_0.UIScan_0.CloseUIMenu("npcShop");
                }
            }
        }

        public void MoveToRepair()
        {
            bool MovedCorrectly = false;
            if (TownAct == 4)
            {
                CheckForNPCValidPos("Halbu");

                //move close to store location
                if (Form1_0.Mover_0.MoveToLocation(5082, 5043))
                {
                    //get store location
                    if (Form1_0.NPCStruc_0.GetNPC("Halbu"))
                    {
                        if (Form1_0.Mover_0.MoveToLocation(Form1_0.NPCStruc_0.xPosFinal, Form1_0.NPCStruc_0.yPosFinal))
                        {
                            MovedCorrectly = true;
                        }
                    }
                }
                else
                {
                    //Form1_0.method_1("NOT MOVED CORRECTLY TO REPAIR SHOP LOCATION", Color.OrangeRed);
                }
            }

            if (TownAct == 5)
            {
                CheckForNPCValidPos("Larzuk");

                //close to store spot 5078, 5026
                if (IsPosCloseTo(5128, 5112, 20))
                {
                    //move close to corner location
                    Form1_0.Mover_0.MoveToLocation(5128, 5112);
                }

                //stuck between cain and malah
                if (IsPosCloseTo(5080, 5054, 10))
                {
                    //move close to malah location
                    Form1_0.Mover_0.MoveToLocation(5078, 5026);
                }

                //close to store spot 5078, 5026
                if (IsPosCloseTo(5078, 5026, 10))
                {
                    //move close to tp location
                    Form1_0.Mover_0.MoveToLocation(5103, 5029);
                    Form1_0.Mover_0.MoveToLocation(5114, 5059);
                }

                //move close to store location
                if (Form1_0.Mover_0.MoveToLocation(5139, 5043))
                {
                    //get store location
                    if (Form1_0.NPCStruc_0.GetNPC("Larzuk"))
                    {
                        if (Form1_0.Mover_0.MoveToLocation(Form1_0.NPCStruc_0.xPosFinal, Form1_0.NPCStruc_0.yPosFinal))
                        {
                            MovedCorrectly = true;
                        }
                    }
                }
                else
                {
                    //Form1_0.method_1("NOT MOVED CORRECTLY TO REPAIR SHOP LOCATION", Color.OrangeRed);
                }
            }

            // DO OTHER ACT SCRIPT HERE ###

            if (MovedCorrectly)
            {
                //Clic store
                Dictionary<string, int> itemScreenPos = Form1_0.GameStruc_0.World2Screen(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, Form1_0.NPCStruc_0.xPosFinal, Form1_0.NPCStruc_0.yPosFinal);
                Form1_0.KeyMouse_0.MouseClicc(itemScreenPos["x"], itemScreenPos["y"]);
                Form1_0.Mover_0.FinishMoving();
                if (Form1_0.UIScan_0.WaitTilUIOpen("npcInteract"))  //npcShop
                {
                    if (TownAct == 5)
                    {
                        Form1_0.KeyMouse_0.PressKey(System.Windows.Forms.Keys.Down); //Larzuk press down
                    }
                    Form1_0.KeyMouse_0.PressKey(System.Windows.Forms.Keys.Enter);
                    Form1_0.WaitDelay(50);
                    Form1_0.Repair_0.RunRepairScript();
                    Form1_0.UIScan_0.CloseUIMenu("npcInteract");
                    Form1_0.UIScan_0.CloseUIMenu("npcShop");
                    //Form1_0.Mover_0.MoveToLocation(5082, 5043); //#############################################
                }
                else
                {
                    //Form1_0.method_1("NPC INTERACT MENU NOT OPENED FOR REPAIR SHOP", Color.OrangeRed);
                }
            }
        }

        public void MoveToStore()
        {
            bool MovedCorrectly = false;
            if (TownAct == 4)
            {
                CheckForNPCValidPos("Jamella");

                //move close to store location
                if (Form1_0.Mover_0.MoveToLocation(5082, 5043))
                {
                    //get store location
                    if (Form1_0.NPCStruc_0.GetNPC("Jamella"))
                    {
                        if (Form1_0.Mover_0.MoveToLocation(Form1_0.NPCStruc_0.xPosFinal, Form1_0.NPCStruc_0.yPosFinal))
                        {
                            MovedCorrectly = true;
                        }
                    }
                }
                else
                {
                    //Form1_0.method_1("NOT MOVED CORRECTLY TO SHOP LOCATION", Color.OrangeRed);
                }
            }

            if (TownAct == 5)
            {
                //CheckForNPCValidPos("Malah");

                //close to store spot 5078, 5026
                if (IsPosCloseTo(5128, 5112, 20))
                {
                    //move close to corner location
                    Form1_0.Mover_0.MoveToLocation(5128, 5112);
                }

                //close to stash spot 5123, 5065
                if (IsPosCloseTo(5123, 5065, 10))
                {
                    //move close to wp location
                    Form1_0.Mover_0.MoveToLocation(5114, 5059);
                    Form1_0.Mover_0.MoveToLocation(5103, 5029);
                    GetCorpse();
                }

                //stuck above stash
                if (IsPosCloseTo(5097, 5042, 6))
                {
                    //move back to TP
                    Form1_0.Mover_0.MoveToLocation(5104, 5030);
                }

                //stuck near stash
                if (IsPosCloseTo(5021, 5056, 6))
                {
                    //move back to TP
                    Form1_0.Mover_0.MoveToLocation(5108, 5060);
                }

                //move close to store location
                if (Form1_0.Mover_0.MoveToLocation(5078, 5026))
                {
                    //get store location
                    if (Form1_0.NPCStruc_0.GetNPC("Malah"))
                    {
                        if (Form1_0.NPCStruc_0.xPosFinal != 0 && Form1_0.NPCStruc_0.yPosFinal != 0)
                        {
                            if (Form1_0.Mover_0.MoveToLocation(Form1_0.NPCStruc_0.xPosFinal, Form1_0.NPCStruc_0.yPosFinal))
                            {
                                MovedCorrectly = true;
                            }
                        }
                        else
                        {
                            TriedToShopCount2++;
                            TriedToShopCount--;

                            if (TriedToShopCount2 > 50)
                            {
                                CheckForNPCValidPos("Malah");
                                TriedToShopCount++;
                            }
                        }
                    }
                    else
                    {
                        TriedToShopCount2++;
                        TriedToShopCount--;

                        if (TriedToShopCount2 > 50)
                        {
                            CheckForNPCValidPos("Malah");
                            TriedToShopCount++;
                        }
                    }
                }
                else
                {
                    //Form1_0.method_1("NOT MOVED CORRECTLY TO SHOP LOCATION", Color.OrangeRed);
                }
            }

            // DO OTHER ACT SCRIPT HERE ###

            if (MovedCorrectly)
            {
                //Clic store
                Dictionary<string, int> itemScreenPos = Form1_0.GameStruc_0.World2Screen(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, Form1_0.NPCStruc_0.xPosFinal, Form1_0.NPCStruc_0.yPosFinal);
                Form1_0.KeyMouse_0.MouseClicc(itemScreenPos["x"], itemScreenPos["y"]);
                Form1_0.Mover_0.FinishMoving();
                if (Form1_0.UIScan_0.WaitTilUIOpen("npcInteract"))  //npcShop
                {
                    if (TownAct == 5)
                    {
                        Form1_0.KeyMouse_0.PressKey(System.Windows.Forms.Keys.Down);    //press down with Malah
                    }
                    Form1_0.KeyMouse_0.PressKey(System.Windows.Forms.Keys.Enter);
                    Form1_0.WaitDelay(50);
                    Form1_0.Shop_0.RunShopScript();
                    Form1_0.UIScan_0.CloseUIMenu("npcInteract");
                    Form1_0.UIScan_0.CloseUIMenu("npcShop");
                    //Form1_0.Mover_0.MoveToLocation(5082, 5043); //#############################################
                }
                else
                {
                    //Form1_0.method_1("NPC INTERACT MENU NOT OPENED FOR SHOP", Color.OrangeRed);
                }
            }
        }

        public void MoveToStash(bool RunScript)
        {
            bool MovedCorrectly = false;
            if (TownAct == 4)
            {
                //move close to stash location
                if (Form1_0.Mover_0.MoveToLocation(5021, 5034))
                {
                    MovedCorrectly = true;
                }
                else
                {
                    //Form1_0.method_1("NOT MOVED CORRECTLY TO STASH LOCATION", Color.OrangeRed);
                }
            }

            if (TownAct == 5)
            {
                //close to store spot 5078, 5026
                if (IsPosCloseTo(5078, 5026, 10))
                {
                    //move close to tp location
                    Form1_0.Mover_0.MoveToLocation(5103, 5029);
                    Form1_0.Mover_0.MoveToLocation(5114, 5059);
                }

                //close to store spot 5078, 5026
                if (IsPosCloseTo(5128, 5112, 20))
                {
                    //move close to corner location
                    Form1_0.Mover_0.MoveToLocation(5128, 5112);
                }

                //stuck above stash
                if (IsPosCloseTo(5116, 5046, 6))
                {
                    //move back inbetween TP and WP location
                    Form1_0.Mover_0.MoveToLocation(5104, 5047);
                }

                //move close to stash location
                if (Form1_0.Mover_0.MoveToLocation(5123, 5065))
                {
                    MovedCorrectly = true;
                }
                else
                {
                    //Form1_0.method_1("NOT MOVED CORRECTLY TO STASH LOCATION", Color.OrangeRed);
                }
            }

            // DO OTHER ACT SCRIPT HERE ###

            if (MovedCorrectly)
            {
                //get stash location

                Dictionary<string, int> itemScreenPos = new Dictionary<string, int>();
                bool HasPosForStash = false;
                if (TownAct == 5)
                {
                    itemScreenPos = Form1_0.GameStruc_0.World2Screen(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, 5124, 5057);
                    HasPosForStash = true;
                }
                else
                {
                    if (Form1_0.ObjectsStruc_0.GetObjects("Bank", true))
                    {
                        Form1_0.method_1("CHANGE STASH POS TO: " + Form1_0.ObjectsStruc_0.itemx + ", " + Form1_0.ObjectsStruc_0.itemy, Color.BlueViolet);
                        itemScreenPos = Form1_0.GameStruc_0.World2Screen(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, Form1_0.ObjectsStruc_0.itemx, Form1_0.ObjectsStruc_0.itemy);
                        HasPosForStash = true;
                    }
                    else
                    {
                        Form1_0.method_1("STASH NOT FOUND NEAR", Color.OrangeRed);
                        if (TownAct == 4)
                        {
                            Form1_0.Mover_0.MoveToLocation(5092, 5044);
                        }
                    }

                }
                if (HasPosForStash)
                {
                    //Clic stash
                    Form1_0.KeyMouse_0.MouseClicc(itemScreenPos["x"], itemScreenPos["y"]);
                    Form1_0.Mover_0.FinishMoving();
                    if (Form1_0.UIScan_0.WaitTilUIOpen("stash"))
                    {
                        if (RunScript)
                        {
                            Form1_0.Stash_0.RunStashScript();
                        }
                        Form1_0.UIScan_0.CloseUIMenu("stash");
                    }
                    else
                    {
                        //Form1_0.method_1("STASH MENU NOT OPENED", Color.OrangeRed);
                    }
                }
            }
        }

        public void MoveToCain()
        {
            CheckForNPCValidPos("DeckardCain");
            bool MovedCorrectly = false;

            if (TownAct == 4)
            {
                //move close to cain location
                if (Form1_0.Mover_0.MoveToLocation(5029, 5037))
                {
                    //get cain location
                    if (Form1_0.NPCStruc_0.GetNPC("DeckardCain"))
                    {
                        if (Form1_0.Mover_0.MoveToLocation(Form1_0.NPCStruc_0.xPosFinal, Form1_0.NPCStruc_0.yPosFinal))
                        {
                            MovedCorrectly = true;
                        }
                    }
                }
                else
                {
                    //Form1_0.method_1("NOT MOVED CORRECTLY TO CAIN LOCATION", Color.OrangeRed);
                }
            }

            if (TownAct == 5)
            {
                //close to wp spot 5103, 5029
                if (IsPosCloseTo(5103, 5029, 10))
                {
                    //move close to stash location
                    Form1_0.Mover_0.MoveToLocation(5114, 5059);
                }

                //move close to cain location
                if (Form1_0.Mover_0.MoveToLocation(5086, 5082))
                {
                    //get cain location
                    if (Form1_0.NPCStruc_0.GetNPC("DeckardCain"))
                    {
                        if (Form1_0.Mover_0.MoveToLocation(Form1_0.NPCStruc_0.xPosFinal, Form1_0.NPCStruc_0.yPosFinal))
                        {
                            MovedCorrectly = true;
                        }
                    }
                }
                else
                {
                    //Form1_0.method_1("NOT MOVED CORRECTLY TO CAIN LOCATION", Color.OrangeRed);
                }
            }

            // DO OTHER ACT SCRIPT HERE ###

            if (MovedCorrectly)
            {
                //Clic cain
                Dictionary<string, int> itemScreenPos = Form1_0.GameStruc_0.World2Screen(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, Form1_0.NPCStruc_0.xPosFinal, Form1_0.NPCStruc_0.yPosFinal);
                Form1_0.KeyMouse_0.MouseClicc(itemScreenPos["x"], itemScreenPos["y"]);
                Form1_0.Mover_0.FinishMoving();
                if (Form1_0.UIScan_0.WaitTilUIOpen("npcInteract"))
                {
                    //Clic Identify items (get cain pos again) - 227 offset y
                    Form1_0.KeyMouse_0.PressKey(System.Windows.Forms.Keys.Down);
                    Form1_0.KeyMouse_0.PressKey(System.Windows.Forms.Keys.Enter);

                    //wait til its done
                    if (!Form1_0.UIScan_0.WaitTilUIClose("npcInteract"))
                    {
                        //Form1_0.method_1("ITEMS DIDN'T IDENTIFIED, RETRYING...", Color.Black);
                        Form1_0.KeyMouse_0.PressKey(System.Windows.Forms.Keys.Enter);
                    }
                    Form1_0.ItemsStruc_0.GetItems(false);
                }
                else
                {
                    //Form1_0.method_1("NPC INTERACT MENU NOT OPENED FOR CAIN", Color.OrangeRed);
                }
            }
        }

        public void MoveToMerc()
        {
            bool MovedCorrectly = false;

            if (TownAct == 4)
            {
                CheckForNPCValidPos("Tyrael");

                //move close to cain location
                if (Form1_0.Mover_0.MoveToLocation(5029, 5037))
                {
                    //get cain location
                    if (Form1_0.NPCStruc_0.GetNPC("Tyrael"))
                    {
                        if (Form1_0.Mover_0.MoveToLocation(Form1_0.NPCStruc_0.xPosFinal, Form1_0.NPCStruc_0.yPosFinal))
                        {
                            MovedCorrectly = true;
                        }
                    }
                }
                else
                {
                    //Form1_0.method_1("NOT MOVED CORRECTLY TO TYRAEL LOCATION", Color.OrangeRed);
                }
            }

            if (TownAct == 5)
            {
                CheckForNPCValidPos("Qual-Kehk");

                //close to wp spot 5103, 5029
                if (IsPosCloseTo(5103, 5029, 10))
                {
                    //move close to stash location
                    Form1_0.Mover_0.MoveToLocation(5114, 5059);
                }

                //move close to cain location
                if (Form1_0.Mover_0.MoveToLocation(5086, 5082))
                {
                    //get cain location
                    if (Form1_0.NPCStruc_0.GetNPC("Qual-Kehk"))
                    {
                        if (Form1_0.Mover_0.MoveToLocation(Form1_0.NPCStruc_0.xPosFinal, Form1_0.NPCStruc_0.yPosFinal))
                        {
                            MovedCorrectly = true;
                        }
                    }
                }
                else
                {
                    //Form1_0.method_1("NOT MOVED CORRECTLY TO QUAL-KEHK LOCATION", Color.OrangeRed);
                }
            }

            // DO OTHER ACT SCRIPT HERE ###

            if (MovedCorrectly)
            {
                //Clic merc NPC
                Dictionary<string, int> itemScreenPos = Form1_0.GameStruc_0.World2Screen(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, Form1_0.NPCStruc_0.xPosFinal, Form1_0.NPCStruc_0.yPosFinal);
                Form1_0.KeyMouse_0.MouseClicc(itemScreenPos["x"], itemScreenPos["y"]);
                Form1_0.Mover_0.FinishMoving();
                if (Form1_0.UIScan_0.WaitTilUIOpen("npcInteract"))
                {
                    Form1_0.KeyMouse_0.PressKey(System.Windows.Forms.Keys.Down);
                    Form1_0.KeyMouse_0.PressKey(System.Windows.Forms.Keys.Enter);

                    //wait til its done
                    Form1_0.UIScan_0.WaitTilUIClose("npcInteract");
                    Form1_0.UIScan_0.CloseUIMenu("npcInteract");
                }
                else
                {
                    //Form1_0.method_1("NPC INTERACT MENU NOT OPENED FOR CAIN", Color.OrangeRed);
                }
            }
        }

        public void SpawnTP()
        {
            //fix when close to RedPortal in Baal
            if (Form1_0.PlayerScan_0.xPosFinal >= (15090 - 4)
                && Form1_0.PlayerScan_0.xPosFinal <= (15090 + 4)
                && Form1_0.PlayerScan_0.yPosFinal >= (5008 - 4)
                && Form1_0.PlayerScan_0.yPosFinal <= (5008 + 4)
                && Form1_0.PlayerScan_0.levelNo == 131)
            {
                Form1_0.Mover_0.MoveToLocation(15090, 5008 + 15);
            }

            //has tp
            if (Form1_0.InventoryStruc_0.HUDItems_tpscrolls > 0)
            {
                // open inv
                Form1_0.UIScan_0.OpenUIMenu("invMenu");
                //use tp in inventory
                Form1_0.InventoryStruc_0.UseTP();
                TPSpawned = true;
                //close inv
                Form1_0.UIScan_0.CloseUIMenu("invMenu");
                Form1_0.WaitDelay(50); //100 default
                Towning = true;
                ForcedTowning = true;
            }
        }

        public void GetCorpse()
        {
            if (Form1_0.ItemsStruc_0.ItemsEquiped > 2) return;

            //method #1
            if (Form1_0.NPCStruc_0.GetNPC("DeadCorpse"))
            {
                //Console.WriteLine("Corpse found method1");
                Form1_0.method_1("Grab corpse #1", Color.Red);
                //Clic corpse
                Dictionary<string, int> itemScreenPos = Form1_0.GameStruc_0.World2Screen(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, Form1_0.NPCStruc_0.xPosFinal, Form1_0.NPCStruc_0.yPosFinal);
                Form1_0.KeyMouse_0.MouseClicc(itemScreenPos["x"], itemScreenPos["y"]);
            }

            //method #2
            int Tries = 0;
            while (Form1_0.PlayerScan_0.ScanForOthersPlayers(0, CharConfig.PlayerCharName, true) && Tries < 5)
            {
                //Console.WriteLine("Corpse found method2");
                Form1_0.method_1("Grab corpse #2", Color.Red);
                //Clic corpse
                Dictionary<string, int> itemScreenPos = Form1_0.GameStruc_0.World2Screen(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, Form1_0.PlayerScan_0.xPosFinalOtherP, Form1_0.PlayerScan_0.yPosFinalOtherP);
                Form1_0.KeyMouse_0.MouseClicc(itemScreenPos["x"], itemScreenPos["y"]);
                Form1_0.WaitDelay(100);
                Tries++;
            }
        }

        public void GoToTown()
        {
            //script to spawn tp and move to town quickly (no potion and no hp)
            if (!GetInTown())
            {
                SpawnTP();
            }
        }

        public bool GetInTown()
        {
            TownAct = 0;
            if (Form1_0.PlayerScan_0.levelNo >= 1 && Form1_0.PlayerScan_0.levelNo < 40) TownAct = 1;
            if (Form1_0.PlayerScan_0.levelNo >= 40 && Form1_0.PlayerScan_0.levelNo < 75) TownAct = 2;
            if (Form1_0.PlayerScan_0.levelNo >= 75 && Form1_0.PlayerScan_0.levelNo < 103) TownAct = 3;
            if (Form1_0.PlayerScan_0.levelNo >= 103 && Form1_0.PlayerScan_0.levelNo < 109) TownAct = 4;
            if (Form1_0.PlayerScan_0.levelNo >= 109) TownAct = 5;


            IsInTown = false;
            if (Form1_0.PlayerScan_0.levelNo == 1       //act1
                || Form1_0.PlayerScan_0.levelNo == 40   //act2
                || Form1_0.PlayerScan_0.levelNo == 75   //act3
                || Form1_0.PlayerScan_0.levelNo == 103  //act4
                || Form1_0.PlayerScan_0.levelNo == 109) //act5
            {
                IsInTown = true;
            }
            return IsInTown;
        }


        public string getAreaName(int areaNum)
        {
            switch (areaNum)
            {
                case 1: return "Rogue Encampment";
                case 2: return "Blood Moor";
                case 3: return "Cold Plains";
                case 4: return "Stony Field";
                case 5: return "Dark Wood";
                case 6: return "Black Marsh";
                case 7: return "Tamoe Highland";
                case 8: return "Den of Evil";
                case 9: return "Cave Level 1";
                case 10: return "Underground Passage Level 1";
                case 11: return "Hole Level 1";
                case 12: return "Pit Level 1";
                case 13: return "Cave Level 2";
                case 14: return "Underground Passage Level 2";
                case 15: return "Hole Level 2";
                case 16: return "Pit Level 2";
                case 17: return "Burial Grounds";
                case 18: return "Crypt";
                case 19: return "Mausoleum";
                case 20: return "Forgotten Tower";
                case 21: return "Tower Cellar Level 1";
                case 22: return "Tower Cellar Level 2";
                case 23: return "Tower Cellar Level 3";
                case 24: return "Tower Cellar Level 4";
                case 25: return "Tower Cellar Level 5";
                case 26: return "Monastery Gate";
                case 27: return "Outer Cloister";
                case 28: return "Barracks";
                case 29: return "Jail Level 1";
                case 30: return "Jail Level 2";
                case 31: return "Jail Level 3";
                case 32: return "Inner Cloister";
                case 33: return "Cathedral";
                case 34: return "Catacombs Level 1";
                case 35: return "Catacombs Level 2";
                case 36: return "Catacombs Level 3";
                case 37: return "Catacombs Level 4";
                case 38: return "Tristram";
                case 39: return "Moo Moo Farm";
                case 40: return "Lut Gholein";
                case 41: return "Rocky Waste";
                case 42: return "Dry Hills";
                case 43: return "Far Oasis";
                case 44: return "Lost City";
                case 45: return "Valley of Snakes";
                case 46: return "Canyon of the Magi";
                case 47: return "Sewers Level 1";
                case 48: return "Sewers Level 2";
                case 49: return "Sewers Level 3";
                case 50: return "Harem Level 1";
                case 51: return "Harem Level 2";
                case 52: return "Palace Cellar Level 1";
                case 53: return "Palace Cellar Level 2";
                case 54: return "Palace Cellar Level 3";
                case 55: return "Stony Tomb Level 1";
                case 56: return "Halls of the Dead Level 1";
                case 57: return "Halls of the Dead Level 2";
                case 58: return "Claw Viper Temple Level 1";
                case 59: return "Stony Tomb Level 2";
                case 60: return "Halls of the Dead Level 3";
                case 61: return "Claw Viper Temple Level 2";
                case 62: return "Maggot Lair Level 1";
                case 63: return "Maggot Lair Level 2";
                case 64: return "Maggot Lair Level 3";
                case 65: return "Ancient Tunnels";
                case 66: return "Tal Rasha's Tomb #1";
                case 67: return "Tal Rasha's Tomb #2";
                case 68: return "Tal Rasha's Tomb #3";
                case 69: return "Tal Rasha's Tomb #4";
                case 70: return "Tal Rasha's Tomb #5";
                case 71: return "Tal Rasha's Tomb #6";
                case 72: return "Tal Rasha's Tomb #7";
                case 73: return "Duriel's Lair";
                case 74: return "Arcane Sanctuary";
                case 75: return "Kurast Docktown";
                case 76: return "Spider Forest";
                case 77: return "Great Marsh";
                case 78: return "Flayer Jungle";
                case 79: return "Lower Kurast";
                case 80: return "Kurast Bazaar";
                case 81: return "Upper Kurast";
                case 82: return "Kurast Causeway";
                case 83: return "Travincal";
                case 84: return "Arachnid Lair";
                case 85: return "Spider Cavern";
                case 86: return "Swampy Pit Level 1";
                case 87: return "Swampy Pit Level 2";
                case 88: return "Flayer Dungeon Level 1";
                case 89: return "Flayer Dungeon Level 2";
                case 90: return "Swampy Pit Level 3";
                case 91: return "Flayer Dungeon Level 3";
                case 92: return "Sewers Level 1";
                case 93: return "Sewers Level 2";
                case 94: return "Ruined Temple";
                case 95: return "Disused Fane";
                case 96: return "Forgotten Reliquary";
                case 97: return "Forgotten Temple";
                case 98: return "Ruined Fane";
                case 99: return "Disused Reliquary";
                case 100: return "Durance of Hate Level 1";
                case 101: return "Durance of Hate Level 2";
                case 102: return "Durance of Hate Level 3";
                case 103: return "Pandemonium Fortress";
                case 104: return "Outer Steppes";
                case 105: return "Plains of Despair";
                case 106: return "City of the Damned";
                case 107: return "River of Flame";
                case 108: return "Chaos Sanctuary";
                case 109: return "Harrogath";
                case 110: return "Bloody Foothills";
                case 111: return "Frigid Highlands";
                case 112: return "Arreat Plateau";
                case 113: return "Crystalline Passage";
                case 114: return "Frozen River";
                case 115: return "Glacial Trail";
                case 116: return "Drifter Cavern";
                case 117: return "Frozen Tundra";
                case 118: return "Ancients' Way";
                case 119: return "Icy Cellar";
                case 120: return "Arreat Summit";
                case 121: return "Nihlathaks Temple";
                case 122: return "Halls of Anguish";
                case 123: return "Halls of Death's Calling";
                case 124: return "Halls of Vaught";
                case 125: return "Abaddon";
                case 126: return "Pit of Acheron";
                case 127: return "Infernal Pit";
                case 128: return "Worldstone Keep Level 1";
                case 129: return "Worldstone Keep Level 2";
                case 130: return "Worldstone Keep Level 3";
                case 131: return "Throne of Destruction";
                case 132: return "Worldstone Chamber";
                case 133: return "Pandemonium Run 1";
                case 134: return "Pandemonium Run 2";
                case 135: return "Pandemonium Run 3";
                case 136: return "Tristram";
            }

            return "";
        }
    }
}
