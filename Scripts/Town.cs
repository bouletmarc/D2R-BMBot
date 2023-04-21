using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app
{
    public class Town
    {
        Form1 Form1_0;

        public int TownAct = 0;
        public bool Towning = true;
        public bool ForcedTowning = false;
        public bool IsInTown = false;
        public bool TPSpawned = false;
        public bool UseLastTP = true;
        public int ScriptTownAct = 0;
        public int TriedToShopCount = 0;
        public int TriedToMercCount = 0;

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

            if (!GetInTown())
            {
                Form1_0.Potions_0.CheckIfWeUsePotion();

                Form1_0.SetGameStatus("TOWN-TP TO TOWN");
                if (TPSpawned)
                {
                    //select the spawned TP
                    if (Form1_0.ObjectsStruc_0.GetObjects("TownPortal", true))
                    //if (Form1_0.ObjectsStruc_0.GetObjects("TownPortal", Form1_0.PlayerScan_0.unitId))
                    {
                        if (Form1_0.ObjectsStruc_0.itemx != 0 && Form1_0.ObjectsStruc_0.itemy != 0)
                        {
                            GetCorpse();
                            Dictionary<string, int> itemScreenPos = Form1_0.GameStruc_0.World2Screen(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, Form1_0.ObjectsStruc_0.itemx, Form1_0.ObjectsStruc_0.itemy);
                            Form1_0.KeyMouse_0.MouseClicc(itemScreenPos["x"], itemScreenPos["y"] - 15);
                            Form1_0.WaitDelay(50);
                            Form1_0.Mover_0.FinishMoving();
                            //Form1_0.Mover_0.MoveToLocation(5055, 5039); //act4 only
                        }
                        else
                        {
                            TPSpawned = false;
                        }
                    }
                    else
                    {
                        TPSpawned = false;
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
                if (!IsInRightTown())
                {
                    Form1_0.SetGameStatus("TOWN-SWITCH TOWN");
                    GotoTownScriptAct();
                }
                else
                {
                    //Identify items
                    if (Form1_0.InventoryStruc_0.HasUnidItemInInventory())
                    {
                        Form1_0.SetGameStatus("TOWN-CAIN");
                        MoveToCain();
                    }
                    else
                    {
                        bool ShouldReliveMerc = false;
                        if (CharConfig.UsingMerc)
                        {
                            Form1_0.MercStruc_0.GetMercInfos();
                            ShouldReliveMerc = !Form1_0.MercStruc_0.MercAlive;
                        }

                        if (ShouldReliveMerc && TriedToMercCount < 5)
                        {
                            //relive merc
                            MoveToMerc();
                            TriedToMercCount++;
                        }
                        else
                        {
                            //stash items
                            if (Form1_0.InventoryStruc_0.ContainStashItemInInventory())
                            {
                                Form1_0.SetGameStatus("TOWN-STASH");
                                MoveToStash(true);
                            }
                            else
                            {
                                //buy potions,tp,etc
                                if (Form1_0.Shop_0.ShouldShop() && TriedToShopCount < 15)
                                {
                                    Form1_0.SetGameStatus("TOWN-SHOP");
                                    MoveToStore();
                                    TriedToShopCount++;
                                }
                                else
                                {
                                    //check for repair
                                    if (Form1_0.Repair_0.GetShouldRepair() && TriedToShopCount < 20)
                                    {
                                        Form1_0.SetGameStatus("TOWN-REPAIR");
                                        MoveToRepair();
                                        TriedToShopCount++;
                                    }
                                    else
                                    {
                                        Form1_0.SetGameStatus("TOWN-END");
                                        //end towning script
                                        MoveToTPOrWPSpot();
                                        GetCorpse();
                                        TriedToShopCount = 0;
                                        TriedToMercCount = 0;
                                        Towning = false;
                                        ForcedTowning = false;
                                        UseLastTP = true;
                                    }
                                }
                            }
                        }
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

        public void GotoTownScriptAct()
        {
            if (TownAct == 1)
            {
                if (Form1_0.Mover_0.MoveToLocation(5654, 5512))
                {
                    //use wp
                    if (Form1_0.ObjectsStruc_0.GetObjects("WaypointPortal", false))
                    {
                        Dictionary<string, int> itemScreenPos = Form1_0.GameStruc_0.World2Screen(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, Form1_0.ObjectsStruc_0.itemx, Form1_0.ObjectsStruc_0.itemy);
                        Form1_0.KeyMouse_0.MouseClicc(itemScreenPos["x"], itemScreenPos["y"] - 15);
                        Form1_0.Mover_0.FinishMoving();
                        if (Form1_0.UIScan_0.WaitTilUIOpen("waypointMenu"))
                        {
                            SelectTownWP();
                        }
                        /*else
                        {
                            Form1_0.method_1("WP MENU NOT OPENED");
                        }*/
                    }
                    else
                    {
                        Form1_0.method_1("NO WP FOUND NEAR IN TOWN", Color.OrangeRed);
                    }
                }
            }
            /*if (TownAct == 2)
            {
            }
            if (TownAct == 3)
            {
            }*/
            if (TownAct == 4)
            {
                if (Form1_0.Mover_0.MoveToLocation(5055, 5039))
                {
                    //use wp
                    if (Form1_0.ObjectsStruc_0.GetObjects("PandamoniumFortressWaypoint", false))
                    {
                        Dictionary<string, int> itemScreenPos = Form1_0.GameStruc_0.World2Screen(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, Form1_0.ObjectsStruc_0.itemx, Form1_0.ObjectsStruc_0.itemy);
                        Form1_0.KeyMouse_0.MouseClicc(itemScreenPos["x"], itemScreenPos["y"] - 15);
                        Form1_0.Mover_0.FinishMoving();
                        if (Form1_0.UIScan_0.WaitTilUIOpen("waypointMenu"))
                        {
                            SelectTownWP();
                        }
                        /*else
                        {
                            Form1_0.method_1("WP MENU NOT OPENED");
                        }*/
                    }
                    else
                    {
                        Form1_0.method_1("NO WP FOUND NEAR IN TOWN", Color.OrangeRed);
                    }
                }
            }
            /*if (TownAct == 5)
            {
            }*/
        }

        public void SelectTownWP()
        {
            //select town
            if (ScriptTownAct == 2) Form1_0.KeyMouse_0.MouseClicc(325, 220);
            if (ScriptTownAct == 3) Form1_0.KeyMouse_0.MouseClicc(415, 220);
            if (ScriptTownAct == 4) Form1_0.KeyMouse_0.MouseClicc(500, 220);
            if (ScriptTownAct == 5) Form1_0.KeyMouse_0.MouseClicc(585, 220);
            Form1_0.WaitDelay(50);
            Form1_0.KeyMouse_0.MouseClicc(285, 270); //select first wp
            Form1_0.UIScan_0.WaitTilUIClose("waypointMenu");
            Form1_0.UIScan_0.WaitTilUIClose("loading");
        }

        public bool ShouldBeInTown()
        {
            if (ForcedTowning) return true;

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
                MoveToStash(false);
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

        public void MoveToTPOrWPSpot()
        {
            bool MovedCorrectly = false;
            if (TownAct == 4)
            {
                if (Form1_0.Mover_0.MoveToLocation(5082, 5043))
                {
                    if (Form1_0.Mover_0.MoveToLocation(5055, 5039))
                    {
                        MovedCorrectly = true;
                    }
                }
            }
            if (TownAct == 5)
            {
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
                CheckForNPCValidPos("Malah");

                //close to stash spot 5123, 5065
                if (Form1_0.PlayerScan_0.xPosFinal >= 5123 - 10
                    && Form1_0.PlayerScan_0.xPosFinal <= 5123 + 10
                    && Form1_0.PlayerScan_0.yPosFinal >= 5065 - 10
                    && Form1_0.PlayerScan_0.yPosFinal <= 5065 + 10)
                {
                    //move close to wp location
                    Form1_0.Mover_0.MoveToLocation(5114, 5059);
                    Form1_0.Mover_0.MoveToLocation(5103, 5029);
                }

                //move close to store location
                if (Form1_0.Mover_0.MoveToLocation(5078, 5026))
                {
                    //get store location
                    if (Form1_0.NPCStruc_0.GetNPC("Malah"))
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
                if (Form1_0.Mover_0.MoveToLocation(5023, 5039))
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
                if (Form1_0.PlayerScan_0.xPosFinal >= 5078 - 10
                    && Form1_0.PlayerScan_0.xPosFinal <= 5078 + 10
                    && Form1_0.PlayerScan_0.yPosFinal >= 5026 - 10
                    && Form1_0.PlayerScan_0.yPosFinal <= 5026 + 10)
                {
                    //move close to wp location
                    Form1_0.Mover_0.MoveToLocation(5103, 5029);
                    Form1_0.Mover_0.MoveToLocation(5114, 5059);
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
                if (Form1_0.PlayerScan_0.xPosFinal >= 5103 - 10
                    && Form1_0.PlayerScan_0.xPosFinal <= 5103 + 10
                    && Form1_0.PlayerScan_0.yPosFinal >= 5029 - 10
                    && Form1_0.PlayerScan_0.yPosFinal <= 5029 + 10)
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
                if (Form1_0.PlayerScan_0.xPosFinal >= 5103 - 10
                    && Form1_0.PlayerScan_0.xPosFinal <= 5103 + 10
                    && Form1_0.PlayerScan_0.yPosFinal >= 5029 - 10
                    && Form1_0.PlayerScan_0.yPosFinal <= 5029 + 10)
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
            //method #1
            if (Form1_0.NPCStruc_0.GetNPC("DeadCorpse"))
            {
                //Clic corpse
                Dictionary<string, int> itemScreenPos = Form1_0.GameStruc_0.World2Screen(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, Form1_0.NPCStruc_0.xPosFinal, Form1_0.NPCStruc_0.yPosFinal);
                Form1_0.KeyMouse_0.MouseClicc(itemScreenPos["x"], itemScreenPos["y"]);
            }

            //method #2
            int Tries = 0;
            while (Form1_0.PlayerScan_0.ScanForOthersPlayers(0, CharConfig.PlayerCharName, true) && Tries < 5)
            {
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
    }
}
