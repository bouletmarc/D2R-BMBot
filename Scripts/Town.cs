using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static MapAreaStruc;

public class Town
{
    Form1 Form1_0;

    public int TownAct = 0;
    public bool Towning = true;
    public bool ForcedTowning = false;
    public bool FastTowning = false;
    public bool IsInTown = false;
    public bool TPSpawned = false;
    public bool UseLastTP = false;
    public int ScriptTownAct = 5;       //default should be 0
    public int TriedToCainCount = 0;
    public int TriedToCainCount2 = 0;
    public int TriedToStashCount = 0;
    public int TriedToGambleCount = 0;
    public int TriedToShopCount = 0;
    public int TriedToShopCount2 = 0;
    public int TriedToRepairCount = 0;
    public int TriedToMercCount = 0;
    public int TriedToUseTPCount = 0;
    public int CurrentScript = 0;
    public bool TownScriptDone = false;

    public uint LastUsedTPID = 0;
    public int LastUsedTPCount = 0;
    public List<uint> IgnoredTPList = new List<uint>();
    public List<uint> IgnoredWPList = new List<uint>();
    public bool FirstTown = true;
    public bool CainNotFoundAct1 = false;

    public void SetForm1(Form1 form1_1)
    {
        Form1_0 = form1_1;
    }

    public void StopTowningReturn()
    {
        CurrentScript = 0;
        TriedToCainCount = 0;
        TriedToCainCount2 = 0;
        TriedToStashCount = 0;
        TriedToGambleCount = 0;
        TriedToShopCount = 0;
        TriedToShopCount2 = 0;
        TriedToRepairCount = 0;
        TriedToMercCount = 0;
        TriedToUseTPCount = 0;
        Towning = false;
        TownScriptDone = true;
        //FastTowning = false;
        //ForcedTowning = false;
        //UseLastTP = true;

        if (Form1_0.PublicGame)
        {
            DateTime StartWaitingChangeArea = DateTime.Now;
            while (GetInTown() && (DateTime.Now - StartWaitingChangeArea).TotalSeconds < CharConfig.TownSwitchAreaDelay)
            {
                Form1_0.PlayerScan_0.GetPositions();
                Form1_0.overlayForm.UpdateOverlay();
                Form1_0.GameStruc_0.CheckChickenGameTime();
                Form1_0.ItemsStruc_0.GetItems(false);
            }

            if ((DateTime.Now - StartWaitingChangeArea).TotalSeconds < CharConfig.TownSwitchAreaDelay)
            {
                Form1_0.WaitDelay(CharConfig.PublicGameTPRespawnDelay);
                Form1_0.Town_0.SpawnTP();
            }

        }
    }

    public void RunTownScript()
    {
        //Console.WriteLine("Fast town: " + FastTowning);
        if (!ShouldBeInTown())
        {
            if (!FastTowning) return;
            else
            {
                if (MoveToTPOrWPSpot())
                {
                    GetCorpse();
                    Form1_0.Stash_0.RunningScriptCount = 0;
                    StopTowningReturn();
                }
            }
        }

        Form1_0.SetGameStatus("TOWN");

        //dead leave game
        if (Form1_0.PlayerScan_0.PlayerDead || Form1_0.Potions_0.ForceLeave)
        {
            Form1_0.Potions_0.ForceLeave = true;
            Form1_0.BaalLeech_0.SearchSameGamesAsLastOne = false;
            Form1_0.LeaveGame(false);
            Form1_0.IncreaseDeadCount();
            return;
        }

        Form1_0.GameStruc_0.CheckChickenGameTime();

        //item grab only -> no town
        if (Towning && CharConfig.RunItemGrabScriptOnly)
        {
            StopTowningReturn();
            return;
        }

        if (!GetInTown())
        {
            Form1_0.SetGameStatus("TOWN-TP TO TOWN");
            Form1_0.Potions_0.CheckIfWeUsePotion();

            if (FastTowning) UseLastTP = true;

            if (FastTowning && !Form1_0.Shop_0.ShouldShop())
            {
                StopTowningReturn();
                return;
            }

            if (TriedToUseTPCount >= 3)
            {
                Form1_0.method_1("No TP found nearby when trying to Town", Color.Red);
                Form1_0.LeaveGame(false);
                return;
            }

            //fix for cows script
            if ((Enums.Area)Form1_0.PlayerScan_0.levelNo == Enums.Area.MooMooFarm)
            {
                Form1_0.Cows_0.HadWirtsLeg = true;
            }

            if (TPSpawned)
            {
                int IncreaseCount = 0;
                while (!Form1_0.ObjectsStruc_0.GetObjects("TownPortal", true, IgnoredTPList, 999, CharConfig.PlayerCharName) && IncreaseCount < 10)
                {
                    Form1_0.PatternsScan_0.IncreaseV1Scanning();
                    IncreaseCount++;
                }

                //select the spawned TP
                if (Form1_0.ObjectsStruc_0.GetObjects("TownPortal", true, IgnoredTPList, 999, CharConfig.PlayerCharName))
                //if (Form1_0.ObjectsStruc_0.GetObjects("TownPortal", Form1_0.PlayerScan_0.unitId))
                {
                    if (Form1_0.ObjectsStruc_0.itemx != 0 && Form1_0.ObjectsStruc_0.itemy != 0)
                    {
                        //if (Form1_0.Mover_0.MoveToLocation(Form1_0.ObjectsStruc_0.itemx, Form1_0.ObjectsStruc_0.itemy))
                        //{
                        Form1_0.method_1("Trying to use TP ID: " + Form1_0.ObjectsStruc_0.ObjectUnitID, Color.OrangeRed);

                        if (LastUsedTPID != Form1_0.ObjectsStruc_0.ObjectUnitID)
                        {
                            LastUsedTPID = Form1_0.ObjectsStruc_0.ObjectUnitID;
                            LastUsedTPCount = 0;
                        }
                        else
                        {
                            Form1_0.Mover_0.MoveToLocation(Form1_0.ObjectsStruc_0.itemx + (LastUsedTPCount * 2), Form1_0.ObjectsStruc_0.itemy + (LastUsedTPCount * 2));

                            LastUsedTPCount++;
                            if (LastUsedTPCount >= 4)
                            {
                                IgnoredTPList.Add(LastUsedTPID);
                            }
                        }

                        GetCorpse();
                        CurrentScript = 0;
                        Position itemScreenPos = Form1_0.GameStruc_0.World2Screen(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, Form1_0.ObjectsStruc_0.itemx, Form1_0.ObjectsStruc_0.itemy);
                        Form1_0.WaitDelay(50);
                        Form1_0.KeyMouse_0.PressKeyHold(CharConfig.KeyForceMovement);
                        Form1_0.KeyMouse_0.MouseClicc_RealPos(itemScreenPos.X, itemScreenPos.Y - 15);
                        Form1_0.KeyMouse_0.ReleaseKey(CharConfig.KeyForceMovement);
                        Form1_0.WaitDelay(50);
                        //}
                    }
                    else
                    {
                        TPSpawned = false;
                        TriedToUseTPCount++;
                        if (TriedToUseTPCount >= 3) IgnoredTPList.Clear(); //try to clear again the ignored tp list!
                    }
                }
                else
                {
                    if (TriedToUseTPCount == 3 || TriedToUseTPCount == 4)
                    {
                        Form1_0.method_1("Trying to use Unkown TP ID!", Color.OrangeRed);

                        CurrentScript = 0;
                        Position itemScreenPos = Form1_0.GameStruc_0.World2Screen(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, Form1_0.PlayerScan_0.xPosFinal - 2, Form1_0.PlayerScan_0.yPosFinal);

                        Form1_0.KeyMouse_0.PressKeyHold(CharConfig.KeyForceMovement);
                        Form1_0.KeyMouse_0.MouseClicc_RealPos(itemScreenPos.X, itemScreenPos.Y - 15);
                        Form1_0.KeyMouse_0.ReleaseKey(CharConfig.KeyForceMovement);
                        Form1_0.WaitDelay(50);

                        TPSpawned = false;
                        TriedToUseTPCount++;
                        if (TriedToUseTPCount >= 3) IgnoredTPList.Clear(); //try to clear again the ignored tp list!
                    }
                    else
                    {
                        TPSpawned = false;
                        TriedToUseTPCount++;
                        if (TriedToUseTPCount >= 3) IgnoredTPList.Clear(); //try to clear again the ignored tp list!
                    }
                }
            }
            else
            {
                SpawnTP(true);
                Form1_0.WaitDelay(CharConfig.TPRespawnDelay);
            }
        }
        else
        {
            Form1_0.Battle_0.DoingBattle = false;
            Form1_0.Battle_0.ClearingArea = false;
            Form1_0.Battle_0.MoveTryCount = 0;

            //Console.WriteLine("town... " + CurrentScript);

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
                        Form1_0.WaitDelay(150);
                        //Clic corpse
                        FirstTown = false;
                        Position itemScreenPos = Form1_0.GameStruc_0.World2Screen(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal);

                        Form1_0.KeyMouse_0.MouseClicc_RealPos(itemScreenPos.X - 45, itemScreenPos.Y - 5);
                        //Form1_0.WaitDelay(100);
                        //Tries++;
                        //}
                    }

                    GetCorpse();
                    CurrentScript++;
                    return;
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

            bool AlreadyGoneToShop = false;

            //ID Items
            if (CurrentScript == 1)
            {
                if (!Form1_0.InventoryStruc_0.HasUnidItemInInventory() || (FastTowning && Form1_0.ItemsStruc_0.TriesToPickItemCount < CharConfig.MaxItemGrabTries))
                {
                    CurrentScript++;
                    return;
                }

                if (TriedToCainCount2 >= CharConfig.MaxItemIDTries)
                {
                    CurrentScript++;
                    return;
                }

                //Go see Cain if we cannot ID at shop
                if (CharConfig.IDAtShop && Form1_0.Shop_0.HasUnidItem && TriedToCainCount2 < CharConfig.MaxItemIDTries)
                {
                    Form1_0.SetGameStatus("TOWN-CAIN (" + (TriedToCainCount2 + 1) + "/" + CharConfig.MaxItemIDTries + ")");
                    MoveToCain();
                    AlreadyGoneToShop = false;
                    TriedToCainCount = CharConfig.MaxItemIDTries;
                    TriedToCainCount2++;
                    return;
                }

                if (CurrentScript == 1)
                {
                    if (TriedToCainCount >= CharConfig.MaxItemIDTries)
                    {
                        if (CharConfig.IDAtShop) Form1_0.Shop_0.HasUnidItem = true;
                        else
                        {
                            TriedToCainCount = CharConfig.MaxItemIDTries;
                            TriedToCainCount2 = CharConfig.MaxItemIDTries;
                            CurrentScript++;
                        }
                        return;
                    }

                    if (Form1_0.InventoryStruc_0.HasUnidItemInInventory() && TriedToCainCount < CharConfig.MaxItemIDTries)
                    {
                        if (!CharConfig.IDAtShop)
                        {
                            Form1_0.SetGameStatus("TOWN-CAIN (" + (TriedToCainCount + 1) + "/" + CharConfig.MaxItemIDTries + ")");
                            MoveToCain();
                            TriedToCainCount++;
                        }
                        else
                        {
                            AlreadyGoneToShop = true;
                            Form1_0.SetGameStatus("TOWN-SHOP (IDENTIFY ITEMS) (" + (TriedToCainCount + 1) + "/" + CharConfig.MaxItemIDTries + ")");
                            MoveToStore();
                            TriedToCainCount++;
                        }
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

                if (!ShouldReliveMerc || TriedToMercCount >= CharConfig.MaxMercReliveTries
                    || (Form1_0.PlayerScan_0.PlayerGoldInventory + Form1_0.PlayerScan_0.PlayerGoldInStash) < 75000)
                {
                    CurrentScript++;
                    return;
                }

                if (CurrentScript == 2)
                {
                    if (ShouldReliveMerc && TriedToMercCount < CharConfig.MaxMercReliveTries
                        && (Form1_0.PlayerScan_0.PlayerGoldInventory + Form1_0.PlayerScan_0.PlayerGoldInStash) >= 75000)
                    {
                        Form1_0.SetGameStatus("TOWN-MERC (" + (TriedToMercCount + 1) + "/" + CharConfig.MaxMercReliveTries + ")");
                        MoveToMerc();
                        TriedToMercCount++;
                    }
                    else
                    {
                        TriedToMercCount++;
                    }
                }

            }

            //stash items
            if (CurrentScript == 3)
            {
                if (Form1_0.InventoryStruc_0.HasUnidItemInInventory()
                    && (!FastTowning || (FastTowning && Form1_0.ItemsStruc_0.TriesToPickItemCount >= CharConfig.MaxItemGrabTries))
                    && TriedToCainCount2 < CharConfig.MaxItemIDTries
                    && TriedToCainCount < CharConfig.MaxItemIDTries)
                {
                    //return to identify script, still contain unid item
                    CurrentScript = 1;
                    Form1_0.ItemsStruc_0.TriesToPickItemCount = -1;
                    return;
                }

                if ((!Form1_0.InventoryStruc_0.ContainStashItemInInventory() && (Form1_0.PlayerScan_0.PlayerGoldInventory < 35000))
                            || TriedToStashCount >= CharConfig.MaxItemStashTries || (FastTowning && Form1_0.ItemsStruc_0.TriesToPickItemCount < CharConfig.MaxItemGrabTries && Form1_0.ItemsStruc_0.TriesToPickItemCount >= 0))
                {
                    CurrentScript++;
                    return;
                }

                if (CurrentScript == 3)
                {
                    if ((Form1_0.InventoryStruc_0.ContainStashItemInInventory() || (Form1_0.PlayerScan_0.PlayerGoldInventory >= 35000)) && TriedToStashCount < CharConfig.MaxItemStashTries)
                    {
                        string DescTxt = "";
                        if (Form1_0.InventoryStruc_0.ContainStashItemInInventory()) DescTxt += " (ITEM)";
                        if ((Form1_0.PlayerScan_0.PlayerGoldInventory >= 35000)) DescTxt += " (GOLD)";
                        Form1_0.SetGameStatus("TOWN-STASH" + DescTxt + " (" + (TriedToStashCount + 1) + "/" + CharConfig.MaxItemStashTries + ")");
                        MoveToStash(true);
                        TriedToStashCount++;
                    }
                }
            }

            //gamble
            if (CurrentScript == 4)
            {
                if (!CharConfig.GambleGold)
                {
                    CurrentScript++;
                    return;
                }
                if (!Form1_0.Gamble_0.CanGamble() || TriedToGambleCount >= CharConfig.MaxGambleTries || FastTowning)
                {
                    CurrentScript++;
                    return;
                }

                if (CurrentScript == 4)
                {
                    if (Form1_0.Gamble_0.CanGamble() && TriedToGambleCount < CharConfig.MaxGambleTries && !FastTowning)
                    {
                        TriedToStashCount = 0;
                        Form1_0.SetGameStatus("TOWN-GAMBLE (" + (TriedToGambleCount + 1) + "/" + CharConfig.MaxGambleTries + ")");
                        MoveToGamble();
                        TriedToGambleCount++;
                    }
                }
            }

            //buy potions,tp,etc
            if (CurrentScript == 5)
            {
                if (CharConfig.IDAtShop && AlreadyGoneToShop)
                {
                    CurrentScript++;
                    return;
                }

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
                    if (!Form1_0.Shop_0.ShouldShop() || TriedToShopCount >= CharConfig.MaxShopTries)
                    {
                        //Console.WriteLine("town shop done");
                        CurrentScript++;
                        return;
                    }

                    if (CurrentScript == 5)
                    {
                        if (Form1_0.Shop_0.ShouldShop() && TriedToShopCount < CharConfig.MaxShopTries)
                        {
                            string DescTxt = "";
                            if (Form1_0.Shop_0.ShopForSellingitem) DescTxt += " (SELL)";
                            if (Form1_0.Shop_0.ShopForHP) DescTxt += " (HP)";
                            if (Form1_0.Shop_0.ShopForMana) DescTxt += " (MANA)";
                            if (Form1_0.Shop_0.ShopForTP) DescTxt += " (TP)";
                            if (Form1_0.Shop_0.ShopForKey) DescTxt += " (KEYS)";
                            if (Form1_0.Shop_0.ShopForRegainHP) DescTxt += " (REGEN HP)";

                            Form1_0.SetGameStatus("TOWN-SHOP" + DescTxt + " (" + (TriedToShopCount + 1) + "/" + CharConfig.MaxShopTries + ")");
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
                if (!Form1_0.Repair_0.GetShouldRepair() || TriedToRepairCount >= CharConfig.MaxRepairTries || FastTowning)
                {
                    CurrentScript++;
                    return;
                }

                if (CurrentScript == 6)
                {
                    if (Form1_0.Repair_0.GetShouldRepair() && TriedToRepairCount < CharConfig.MaxRepairTries && !FastTowning)
                    {
                        Form1_0.SetGameStatus("TOWN-REPAIR" + " (" + (TriedToRepairCount + 1) + "/" + CharConfig.MaxRepairTries + ")");
                        MoveToRepair();
                        TriedToRepairCount++;
                    }
                }
            }

            //end towning script
            if (CurrentScript == 7)
            {
                Form1_0.SetGameStatus("TOWN-END");

                if (FastTowning)
                {
                    if (MoveToTPOrWPSpot())
                    {
                        GetCorpse();
                        Form1_0.Stash_0.RunningScriptCount = 0;
                        StopTowningReturn();
                    }
                }
                else
                {
                    GetCorpse();
                    Form1_0.Stash_0.RunningScriptCount = 0;
                    StopTowningReturn();
                }
            }
        }
    }

    public void LoadFirstTownAct()
    {
        GetInTown();
        ScriptTownAct = TownAct;
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
            Form1_0.Mover_0.MovingToInteract = true;
            Form1_0.PathFinding_0.MoveToObject("WaypointPortal");
            Form1_0.Mover_0.MovingToInteract = false;
            Position ThisFinalPosition = Form1_0.MapAreaStruc_0.GetPositionOfObject("object", "WaypointPortal", (int)Enums.Area.RogueEncampment, new List<int>() { });
            if (ThisFinalPosition.X != 0 && ThisFinalPosition.Y != 0)
            {
                Position itemScreenPos = Form1_0.GameStruc_0.World2Screen(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, ThisFinalPosition.X, ThisFinalPosition.Y);

                Form1_0.KeyMouse_0.MouseClicc_RealPos(itemScreenPos.X, itemScreenPos.Y - 15);
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
            {
                Form1_0.method_1("No WP found nearby in Town", Color.OrangeRed);
                IgnoredWPList.Clear();
            }
        }
        if (TownAct == 2)
        {
            Form1_0.Mover_0.MovingToInteract = true;
            Form1_0.PathFinding_0.MoveToObject("Act2Waypoint");
            Form1_0.Mover_0.MovingToInteract = false;
            Position ThisFinalPosition = Form1_0.MapAreaStruc_0.GetPositionOfObject("object", "Act2Waypoint", (int)Enums.Area.LutGholein, new List<int>() { });
            if (ThisFinalPosition.X != 0 && ThisFinalPosition.Y != 0)
            {
                Position itemScreenPos = Form1_0.GameStruc_0.World2Screen(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, ThisFinalPosition.X, ThisFinalPosition.Y);

                Form1_0.KeyMouse_0.MouseClicc_RealPos(itemScreenPos.X, itemScreenPos.Y - 15);
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
            {
                Form1_0.method_1("No WP found nearby in Town", Color.OrangeRed);
                IgnoredWPList.Clear();
            }
        }
        if (TownAct == 3)
        {
            Form1_0.Mover_0.MovingToInteract = true;
            Form1_0.PathFinding_0.MoveToObject("Act3TownWaypoint");
            Form1_0.Mover_0.MovingToInteract = false;
            Position ThisFinalPosition = Form1_0.MapAreaStruc_0.GetPositionOfObject("object", "Act3TownWaypoint", (int)Enums.Area.KurastDocks, new List<int>() { });
            if (ThisFinalPosition.X != 0 && ThisFinalPosition.Y != 0)
            {
                Position itemScreenPos = Form1_0.GameStruc_0.World2Screen(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, ThisFinalPosition.X, ThisFinalPosition.Y);

                Form1_0.KeyMouse_0.MouseClicc_RealPos(itemScreenPos.X, itemScreenPos.Y - 15);
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
            {
                Form1_0.method_1("No WP found nearby in Town", Color.OrangeRed);
                IgnoredWPList.Clear();
            }
        }
        if (TownAct == 4)
        {
            Form1_0.Mover_0.MovingToInteract = true;
            Form1_0.PathFinding_0.MoveToObject("PandamoniumFortressWaypoint");
            Form1_0.Mover_0.MovingToInteract = false;
            Position ThisFinalPosition = Form1_0.MapAreaStruc_0.GetPositionOfObject("object", "PandamoniumFortressWaypoint", (int)Enums.Area.ThePandemoniumFortress, new List<int>() { });
            if (ThisFinalPosition.X != 0 && ThisFinalPosition.Y != 0)
            {
                Position itemScreenPos = Form1_0.GameStruc_0.World2Screen(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, ThisFinalPosition.X, ThisFinalPosition.Y);

                Form1_0.KeyMouse_0.MouseClicc_RealPos(itemScreenPos.X, itemScreenPos.Y - 15);
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
            {
                Form1_0.method_1("No WP found nearby in Town", Color.OrangeRed);
                IgnoredWPList.Clear();
            }
        }
        if (TownAct == 5)
        {
            Form1_0.Mover_0.MovingToInteract = true;
            Form1_0.PathFinding_0.MoveToObject("ExpansionWaypoint");
            Form1_0.Mover_0.MovingToInteract = false;
            Position ThisFinalPosition = Form1_0.MapAreaStruc_0.GetPositionOfObject("object", "ExpansionWaypoint", (int)Enums.Area.Harrogath, new List<int>() { });
            if (ThisFinalPosition.X != 0 && ThisFinalPosition.Y != 0)
            {
                Position itemScreenPos = Form1_0.GameStruc_0.World2Screen(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, ThisFinalPosition.X, ThisFinalPosition.Y);

                Form1_0.KeyMouse_0.MouseClicc_RealPos(itemScreenPos.X, itemScreenPos.Y - 15);
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
            {
                Form1_0.method_1("No WP found nearby in Town", Color.OrangeRed);
                IgnoredWPList.Clear();
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
        Form1_0.WaitDelay(CharConfig.WaypointEnterDelay);
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
        Form1_0.WaitDelay(CharConfig.WaypointEnterDelay);
    }

    public bool ShouldBeInTown()
    {
        if (ForcedTowning) return true;
        if (GetInTown() && Towning) return true;

        bool ShouldBe = false;
        if (Form1_0.InventoryStruc_0.HasUnidItemInInventory() && !FastTowning) ShouldBe = true;
        if (Form1_0.InventoryStruc_0.ContainStashItemInInventory() && !FastTowning) ShouldBe = true;
        if (Form1_0.ItemsStruc_0.TriesToPickItemCount >= CharConfig.MaxItemGrabTries)
        {
            if (Form1_0.InventoryStruc_0.HasUnidItemInInventory()) ShouldBe = true;
            if (Form1_0.InventoryStruc_0.ContainStashItemInInventory()) ShouldBe = true;
        }
        if (Form1_0.Shop_0.ShouldShop()) ShouldBe = true;
        if (Form1_0.Repair_0.GetShouldRepair()) ShouldBe = true;
        if (Form1_0.Gamble_0.CanGamble() && !FastTowning && TownAct == 5) ShouldBe = true;

        bool ShouldReliveMerc = false;
        if (CharConfig.UsingMerc)
        {
            Form1_0.MercStruc_0.GetMercInfos();
            ShouldReliveMerc = !Form1_0.MercStruc_0.MercAlive;
        }
        if (ShouldReliveMerc && (Form1_0.PlayerScan_0.PlayerGoldInventory + Form1_0.PlayerScan_0.PlayerGoldInStash) >= 75000) ShouldBe = true;
        if ((Form1_0.PlayerScan_0.PlayerGoldInventory >= 35000)) ShouldBe = true;


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
            Form1_0.method_1(ThisNPC.ToUpper() + " not found nearby", Color.OrangeRed);

            if (ThisNPC == "DeckardCain" && TownAct == 1)
            {
                Form1_0.PathFinding_0.MoveToNPC("Akara");
            }
            if (ThisNPC == "DeckardCain" && TownAct == 4)
            {
                Form1_0.PathFinding_0.MoveToThisPos(new Position { X = 5092, Y = 5044 });
            }
            if (ThisNPC == "Anya" && TownAct == 5)
            {
                Form1_0.PathFinding_0.MoveToThisPos(new Position { X = 5114, Y = 5059 });
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

        if (TownAct == 1)
        {
            Position ThisFinalPosition = Form1_0.MapAreaStruc_0.GetPositionOfObject("object", "RogueBonfire", (int)Enums.Area.RogueEncampment, new List<int>() { });
            if (ThisFinalPosition.X != 0 && ThisFinalPosition.Y != 0)
            {
                ThisFinalPosition.X = ThisFinalPosition.X + 5;
                ThisFinalPosition.Y = ThisFinalPosition.Y - 5;
                Form1_0.PathFinding_0.MoveToThisPos(ThisFinalPosition);
                MovedCorrectly = true;
            }
            else
            {
                Form1_0.method_1("No RogueBonfire found nearby in Town", Color.OrangeRed);
            }
        }
        if (TownAct == 2)
        {
            Form1_0.PathFinding_0.MoveToThisPos(new Position { X = 5150, Y = 5056 });
            MovedCorrectly = true;
        }
        if (TownAct == 3)
        {
            Position ThisFinalPosition = Form1_0.MapAreaStruc_0.GetPositionOfObject("object", "Bank", (int)Enums.Area.KurastDocks, new List<int>() { });
            if (ThisFinalPosition.X != 0 && ThisFinalPosition.Y != 0)
            {
                Form1_0.PathFinding_0.MoveToThisPos(ThisFinalPosition);
                MovedCorrectly = true;
            }
            else
            {
                Form1_0.method_1("No TP/WP spot (Stash) found nearby in Town", Color.OrangeRed);
            }
        }

        if (TownAct == 4)
        {
            Position ThisFinalPosition = Form1_0.MapAreaStruc_0.GetPositionOfObject("object", "Bank", (int)Enums.Area.ThePandemoniumFortress, new List<int>() { });
            if (ThisFinalPosition.X != 0 && ThisFinalPosition.Y != 0)
            {
                Form1_0.PathFinding_0.MoveToThisPos(ThisFinalPosition);
                MovedCorrectly = true;
            }
            else
            {
                Form1_0.method_1("No TP/WP spot (Stash) found nearby in Town", Color.OrangeRed);
            }
        }
        if (TownAct == 5)
        {
            Form1_0.PathFinding_0.MoveToThisPos(new Position { X = 5103, Y = 5029 });
            MovedCorrectly = true;
        }

        if (MovedCorrectly)
        {
            if (UseLastTP)
            {
                if (TPSpawned)
                {
                    //use tp
                    if (Form1_0.ObjectsStruc_0.GetObjects("TownPortal", true, new List<uint>(), 999, CharConfig.PlayerCharName))
                    {
                        Form1_0.PathFinding_0.MoveToThisPos(new Position { X = Form1_0.ObjectsStruc_0.itemx, Y = Form1_0.ObjectsStruc_0.itemy });
                        Form1_0.WaitDelay(15);

                        int tries = 0;
                        while (tries < 5)
                        {
                            Position itemScreenPos = Form1_0.GameStruc_0.World2Screen(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, Form1_0.ObjectsStruc_0.itemx, Form1_0.ObjectsStruc_0.itemy);

                            Form1_0.KeyMouse_0.MouseClicc_RealPos(itemScreenPos.X, itemScreenPos.Y - 15);
                            Form1_0.WaitDelay(10);
                            Form1_0.PlayerScan_0.GetPositions();
                            tries++;
                        }
                        Form1_0.WaitDelay(10);
                    }
                    else
                    {
                        Form1_0.method_1("No TP found nearby in Town", Color.OrangeRed);
                    }
                }
                /*else
                {
                    //use wp
                    if (Form1_0.ObjectsStruc_0.GetObjects("PandamoniumFortressWaypoint"))
                    {
                        Position itemScreenPos = Form1_0.GameStruc_0.World2Screen(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, Form1_0.ObjectsStruc_0.itemx, Form1_0.ObjectsStruc_0.itemy);
                                    itemScreenPos = Form1_0.Mover_0.FixMousePositionWithScreenSize(itemScreenPos);
                        Form1_0.MouseClicc(itemScreenPos.X, itemScreenPos.Y - 15);
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

        //MISSING TOWN ACT HERE -> DONT GAMBLE IN OTHER TOWN ACT
        if (TownAct != 5)
        {
            TriedToGambleCount = CharConfig.MaxGambleTries + 5;
            return;
        }

        if (TownAct == 5)
        {
            CheckForNPCValidPos("Anya");
            //Form1_0.PathFinding_0.MoveToNPC("Anya");  //not found
            Form1_0.PathFinding_0.MoveToThisPos(new Position { X = 5103, Y = 5115 });
            Form1_0.NPCStruc_0.GetNPC("Anya");
            MovedCorrectly = true;
        }

        if (MovedCorrectly)
        {
            //Clic store
            Position itemScreenPos = Form1_0.GameStruc_0.World2Screen(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, Form1_0.NPCStruc_0.xPosFinal, Form1_0.NPCStruc_0.yPosFinal);

            Form1_0.KeyMouse_0.MouseClicc_RealPos(itemScreenPos.X, itemScreenPos.Y);
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

        if (TownAct == 1)
        {
            //4985,6108 stash
            //4954,6095 charsi
            Position ThisFinalPosition = Form1_0.MapAreaStruc_0.GetPositionOfObject("object", "Bank", (int)Form1_0.PlayerScan_0.levelNo, new List<int>() { });
            CheckForNPCValidPos("Charsi");
            //Form1_0.PathFinding_0.MoveToNPC("Charsi");  //not found
            Form1_0.PathFinding_0.MoveToThisPos(new Position { X = ThisFinalPosition.X - 31, Y = ThisFinalPosition.Y - 13 });
            Form1_0.NPCStruc_0.GetNPC("Charsi");
            MovedCorrectly = true;
        }
        if (TownAct == 2)
        {
            CheckForNPCValidPos("Fara");
            //Form1_0.PathFinding_0.MoveToNPC("Fara");  //not found
            Form1_0.PathFinding_0.MoveToThisPos(new Position { X = 5115, Y = 5080 });
            Form1_0.NPCStruc_0.GetNPC("Fara");
            MovedCorrectly = true;
        }
        if (TownAct == 3)
        {
            CheckForNPCValidPos("Hratli");
            //Form1_0.PathFinding_0.MoveToNPC("Hratli");  //not found
            Form1_0.PathFinding_0.MoveToThisPos(new Position { X = 5219, Y = 5035 });
            Form1_0.NPCStruc_0.GetNPC("Hratli");
            MovedCorrectly = true;
        }

        if (TownAct == 4)
        {
            CheckForNPCValidPos("Halbu");
            //Form1_0.PathFinding_0.MoveToNPC("Halbu");  //not found
            Form1_0.PathFinding_0.MoveToThisPos(new Position { X = 5085, Y = 5022 });
            Form1_0.NPCStruc_0.GetNPC("Halbu");
            MovedCorrectly = true;
        }

        if (TownAct == 5)
        {
            CheckForNPCValidPos("Larzuk");
            //Form1_0.PathFinding_0.MoveToNPC("Larzuk");  //not found
            Form1_0.PathFinding_0.MoveToThisPos(new Position { X = 5145, Y = 5041 });
            Form1_0.NPCStruc_0.GetNPC("Larzuk");
            MovedCorrectly = true;
        }

        if (MovedCorrectly)
        {
            //Clic store
            Position itemScreenPos = Form1_0.GameStruc_0.World2Screen(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, Form1_0.NPCStruc_0.xPosFinal, Form1_0.NPCStruc_0.yPosFinal);

            Form1_0.KeyMouse_0.MouseClicc_RealPos(itemScreenPos.X, itemScreenPos.Y);
            if (Form1_0.UIScan_0.WaitTilUIOpen("npcInteract"))  //npcShop
            {
                if (TownAct != 4)
                {
                    Form1_0.KeyMouse_0.PressKey(System.Windows.Forms.Keys.Down); //Larzuk press down
                }
                Form1_0.KeyMouse_0.PressKey(System.Windows.Forms.Keys.Enter);
                Form1_0.WaitDelay(50);
                Form1_0.Repair_0.RunRepairScript();
                Form1_0.UIScan_0.CloseUIMenu("npcInteract");
                Form1_0.UIScan_0.CloseUIMenu("npcShop");
            }
        }
    }

    public void MoveToStore()
    {
        bool MovedCorrectly = false;

        if (TownAct == 1)
        {
            CheckForNPCValidPos("Akara");
            Form1_0.PathFinding_0.MoveToNPC("Akara");
            Form1_0.NPCStruc_0.GetNPC("Akara");
            MovedCorrectly = true;
        }
        if (TownAct == 2)
        {
            if (!Form1_0.Shop_0.ShopForSellingitem
                && !Form1_0.Shop_0.ShopForHP
                && !Form1_0.Shop_0.ShopForMana
                && !Form1_0.Shop_0.ShopForTP
                && !Form1_0.Shop_0.ShopForKey
                && Form1_0.Shop_0.ShopForRegainHP)
            {
                //Act2 Drognan doesn't regen HP, if we are going to shop only for regen HP, then go see Fara in Act2
                CheckForNPCValidPos("Fara");
                Form1_0.PathFinding_0.MoveToNPC("Fara");
                Form1_0.NPCStruc_0.GetNPC("Fara");
                MovedCorrectly = true;
            }
            else
            {
                CheckForNPCValidPos("Drognan");
                Form1_0.PathFinding_0.MoveToNPC("Drognan");
                Form1_0.NPCStruc_0.GetNPC("Drognan");
                MovedCorrectly = true;
            }

        }
        if (TownAct == 3)
        {
            CheckForNPCValidPos("Ormus");
            Form1_0.PathFinding_0.MoveToNPC("Ormus");
            Form1_0.NPCStruc_0.GetNPC("Ormus");
            MovedCorrectly = true;
        }

        if (TownAct == 4)
        {
            CheckForNPCValidPos("Jamella");
            Form1_0.PathFinding_0.MoveToNPC("Jamella");
            Form1_0.NPCStruc_0.GetNPC("Jamella");
            MovedCorrectly = true;
        }

        if (TownAct == 5)
        {
            CheckForNPCValidPos("Malah");
            Form1_0.PathFinding_0.MoveToNPC("Malah");
            //Form1_0.NPCStruc_0.GetNPC("Malah");
            MovedCorrectly = true;
        }

        if (MovedCorrectly)
        {
            //Clic store
            Position itemScreenPos = Form1_0.GameStruc_0.World2Screen(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, Form1_0.NPCStruc_0.xPosFinal, Form1_0.NPCStruc_0.yPosFinal);

            Form1_0.KeyMouse_0.MouseClicc_RealPos(itemScreenPos.X, itemScreenPos.Y);
            if (Form1_0.UIScan_0.WaitTilUIOpen("npcInteract"))  //npcShop
            {
                if (TownAct != 4)
                {
                    Form1_0.KeyMouse_0.PressKey(System.Windows.Forms.Keys.Down);    //press down if not in Act4
                }
                Form1_0.KeyMouse_0.PressKey(System.Windows.Forms.Keys.Enter);
                Form1_0.WaitDelay(50);
                Form1_0.Shop_0.RunShopScript();
                Form1_0.UIScan_0.CloseUIMenu("npcInteract");
                Form1_0.UIScan_0.CloseUIMenu("npcShop");

                Form1_0.Shop_0.PlaceItem(Form1_0.CenterX, Form1_0.CenterY, true);
            }
        }
    }

    public void MoveToStash(bool RunScript)
    {
        bool MovedCorrectly = false;
        //MISSING TOWN ACT HERE
        if (TownAct == 1)
        {
            Form1_0.PathFinding_0.MoveToObject("Bank");
            MovedCorrectly = true;
        }
        if (TownAct == 2)
        {
            Form1_0.PathFinding_0.MoveToObject("Bank");
            MovedCorrectly = true;
        }
        if (TownAct == 3)
        {
            Form1_0.PathFinding_0.MoveToObject("Bank");
            MovedCorrectly = true;
        }

        if (TownAct == 4)
        {
            Form1_0.PathFinding_0.MoveToObject("Bank");
            MovedCorrectly = true;
        }

        if (TownAct == 5)
        {
            Form1_0.PathFinding_0.MoveToObject("Bank");
            MovedCorrectly = true;
        }

        if (MovedCorrectly)
        {
            //get stash location
            Position itemScreenPos = new Position { X = 0, Y = 0 };
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
                    Form1_0.method_1("Changed Stash pos to: " + Form1_0.ObjectsStruc_0.itemx + ", " + Form1_0.ObjectsStruc_0.itemy, Color.BlueViolet);
                    itemScreenPos = Form1_0.GameStruc_0.World2Screen(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, Form1_0.ObjectsStruc_0.itemx, Form1_0.ObjectsStruc_0.itemy);

                    HasPosForStash = true;
                }
                else
                {
                    Form1_0.method_1("Stash not found nearby in Town", Color.OrangeRed);
                    if (TownAct == 1)
                    {
                        Form1_0.PathFinding_0.MoveToNPC("Akara");
                    }
                    if (TownAct == 2)
                    {
                        Form1_0.PathFinding_0.MoveToNPC("Greiz");
                    }
                    if (TownAct == 3)
                    {
                        Form1_0.PathFinding_0.MoveToNPC("Asheara");
                    }
                    if (TownAct == 4)
                    {
                        Form1_0.PathFinding_0.MoveToThisPos(new Position { X = 5092, Y = 5044 });
                    }
                }

            }
            if (HasPosForStash)
            {
                //Clic stash
                Form1_0.KeyMouse_0.MouseClicc_RealPos(itemScreenPos.X, itemScreenPos.Y);
                if (Form1_0.UIScan_0.WaitTilUIOpen("stash"))
                {
                    if (RunScript)
                    {
                        Form1_0.Stash_0.RunStashScript();
                    }
                    Form1_0.UIScan_0.CloseUIMenu("stash");
                }
            }
        }
    }

    public void MoveToCain()
    {
        CheckForNPCValidPos("DeckardCain");
        bool MovedCorrectly = false;

        if (TownAct == 1)
        {
            if (!CainNotFoundAct1)
            {
                Form1_0.PathFinding_0.MoveToNPC("DeckardCain");
                MovedCorrectly = true;
            }
            else
            {
                //go to town act5 for cain
                ScriptTownAct = 5;
            }
        }
        if (TownAct == 2)
        {
            Form1_0.PathFinding_0.MoveToNPC("DeckardCain");
            MovedCorrectly = true;
        }
        if (TownAct == 3)
        {
            Form1_0.PathFinding_0.MoveToNPC("DeckardCain");
            MovedCorrectly = true;
        }

        if (TownAct == 4)
        {
            Form1_0.PathFinding_0.MoveToNPC("DeckardCain");
            MovedCorrectly = true;
        }

        if (TownAct == 5)
        {
            Form1_0.PathFinding_0.MoveToNPC("DeckardCain");
            MovedCorrectly = true;
        }

        if (MovedCorrectly)
        {
            if (!Form1_0.NPCStruc_0.GetNPC("DeckardCain"))
            {
                if (TownAct == 1 && !CainNotFoundAct1)
                {
                    CainNotFoundAct1 = true;

                    //go to town act5 for cain
                    ScriptTownAct = 5;
                }
            }

            //Clic cain
            Position itemScreenPos = Form1_0.GameStruc_0.World2Screen(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, Form1_0.NPCStruc_0.xPosFinal, Form1_0.NPCStruc_0.yPosFinal);

            Form1_0.KeyMouse_0.MouseClicc_RealPos(itemScreenPos.X, itemScreenPos.Y);
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
        }
    }

    public void MoveToMerc()
    {
        bool MovedCorrectly = false;

        /*if (TownAct == 1)
        {
            CheckForNPCValidPos("Kashya");
            Form1_0.PathFinding_0.MoveToNPC("Kashya");
            Form1_0.NPCStruc_0.GetNPC("Kashya");
            MovedCorrectly = true;
        }*/
        if (TownAct == 2)
        {
            CheckForNPCValidPos("Greiz");
            Form1_0.PathFinding_0.MoveToNPC("Greiz");
            Form1_0.NPCStruc_0.GetNPC("Greiz");
            MovedCorrectly = true;
        }
        if (TownAct == 3)
        {
            CheckForNPCValidPos("Asheara");
            Form1_0.PathFinding_0.MoveToNPC("Asheara");
            Form1_0.NPCStruc_0.GetNPC("Asheara");
            MovedCorrectly = true;
        }

        if (TownAct == 4)
        {
            CheckForNPCValidPos("Tyrael");
            Form1_0.PathFinding_0.MoveToNPC("Tyrael");
            Form1_0.NPCStruc_0.GetNPC("Tyrael");
            MovedCorrectly = true;
        }

        if (TownAct == 5)
        {
            CheckForNPCValidPos("QualKehk");
            Form1_0.PathFinding_0.MoveToNPC("QualKehk");
            Form1_0.NPCStruc_0.GetNPC("QualKehk");
            MovedCorrectly = true;
        }

        if (MovedCorrectly)
        {
            //Clic merc NPC
            Position itemScreenPos = Form1_0.GameStruc_0.World2Screen(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, Form1_0.NPCStruc_0.xPosFinal, Form1_0.NPCStruc_0.yPosFinal);

            Form1_0.KeyMouse_0.MouseClicc_RealPos(itemScreenPos.X, itemScreenPos.Y);
            if (Form1_0.UIScan_0.WaitTilUIOpen("npcInteract"))
            {
                Form1_0.KeyMouse_0.PressKey(System.Windows.Forms.Keys.Down);
                if (TownAct == 4)
                {
                    Form1_0.KeyMouse_0.PressKey(System.Windows.Forms.Keys.Down); //Tyrael press down
                }
                Form1_0.KeyMouse_0.PressKey(System.Windows.Forms.Keys.Enter);

                //wait til its done
                Form1_0.UIScan_0.WaitTilUIClose("npcInteract");
                Form1_0.UIScan_0.CloseUIMenu("npcInteract");
            }
        }
    }

    public void FixBaalNearRedPortal()
    {
        //fix when close to RedPortal in Baal
        if (Form1_0.PlayerScan_0.xPosFinal >= (15090 - 4)
            && Form1_0.PlayerScan_0.xPosFinal <= (15090 + 4)
            && Form1_0.PlayerScan_0.yPosFinal >= (5008 - 4)
            && Form1_0.PlayerScan_0.yPosFinal <= (5008 + 4)
            && Form1_0.PlayerScan_0.levelNo == 131)
        {
            Form1_0.PathFinding_0.MoveToThisPos(new Position { X = 15090 + 5, Y = 5008 + 15 });
        }
    }

    public void SpawnTP(bool EnterTP = false)
    {
        FixBaalNearRedPortal();

        int IncreaseCount = 0;
        while (Form1_0.InventoryStruc_0.HUDItems_tpscrolls == 0 && IncreaseCount < 10)
        {
            Form1_0.PatternsScan_0.IncreaseV1Scanning();
            IncreaseCount++;
            Form1_0.ItemsStruc_0.GetItems(false);
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

            if (EnterTP)
            {
                Towning = true;
                ForcedTowning = true;
            }
        }
        else
        {
            if (EnterTP)
            {
                Form1_0.method_1("Leaving because TP quantity equal 0, cannot spawn a TP and go to Town!", Color.Red);
                Form1_0.LeaveGame(false);
            }
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
            Position itemScreenPos = Form1_0.GameStruc_0.World2Screen(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, Form1_0.NPCStruc_0.xPosFinal, Form1_0.NPCStruc_0.yPosFinal);

            Form1_0.KeyMouse_0.MouseClicc_RealPos(itemScreenPos.X, itemScreenPos.Y);
        }

        //method #2
        int Tries = 0;
        while (Form1_0.PlayerScan_0.ScanForOthersPlayers(0, CharConfig.PlayerCharName, true) && Tries < 5)
        {
            //Console.WriteLine("Corpse found method2");
            Form1_0.method_1("Grab corpse #2", Color.Red);
            //Clic corpse
            Position itemScreenPos = Form1_0.GameStruc_0.World2Screen(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, Form1_0.PlayerScan_0.xPosFinalOtherP, Form1_0.PlayerScan_0.yPosFinalOtherP);

            Form1_0.KeyMouse_0.MouseClicc_RealPos(itemScreenPos.X, itemScreenPos.Y);
            Form1_0.WaitDelay(100);
            Form1_0.PlayerScan_0.GetPositions();
            Tries++;
        }
    }

    public void GoToTown()
    {
        //script to spawn tp and move to town quickly (no potion and no hp)
        if (!GetInTown())
        {
            SpawnTP(true);
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
