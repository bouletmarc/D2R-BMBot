using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app
{
    public class LowerKurast
    {
        Form1 Form1_0;

        public int CurrentStep = 0;
        public int MapStyle = 0;
        public int WP_X = 0;
        public int WP_Y = 0;
        public List<uint> IgnoredChestList = new List<uint>();
        public bool ScriptDone = false;

        public int[] ChestPos1 = new int[2];
        public int[] ChestPos2 = new int[2];

        public void SetForm1(Form1 form1_1)
        {
            Form1_0 = form1_1;
        }

        public void ResetVars()
        {
            CurrentStep = 0;
            MapStyle = 0;
            WP_X = 0;
            WP_Y = 0;
            IgnoredChestList = new List<uint>();
            ScriptDone = false;
        }

        public void RunScript()
        {
            Form1_0.Town_0.ScriptTownAct = 5; //set to town act 5 when running this script

            if (Form1_0.Town_0.GetInTown())
            {
                Form1_0.SetGameStatus("GO TO WP");
                CurrentStep = 0;

                //close to store spot 5078, 5026
                if (Form1_0.Town_0.IsPosCloseTo(5084, 5037, 7))
                {
                    //move close to tp location
                    Form1_0.Mover_0.MoveToLocation(5103, 5029);
                }
                else
                {
                    //move close to wp location
                    if (Form1_0.Mover_0.MoveToLocation(5119, 5067))
                    {
                        //use wp
                        //if (Form1_0.ObjectsStruc_0.GetObjects("ExpansionWaypoint", false))
                        //{
                        //Dictionary<string, int> itemScreenPos = Form1_0.GameStruc_0.World2Screen(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, Form1_0.ObjectsStruc_0.itemx, Form1_0.ObjectsStruc_0.itemy);
                        Dictionary<string, int> itemScreenPos = Form1_0.GameStruc_0.World2Screen(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, 5114, 5069);
                        Form1_0.KeyMouse_0.MouseClicc(itemScreenPos["x"], itemScreenPos["y"] - 15);
                        Form1_0.Mover_0.FinishMoving();
                        if (Form1_0.UIScan_0.WaitTilUIOpen("waypointMenu"))
                        {
                            Form1_0.KeyMouse_0.MouseClicc(415, 220); //select act3
                            Form1_0.WaitDelay(50);
                            Form1_0.KeyMouse_0.MouseClicc(400, 515); //select kurast wp
                            Form1_0.UIScan_0.WaitTilUIClose("waypointMenu");
                            Form1_0.UIScan_0.WaitTilUIClose("loading");
                            Form1_0.WaitDelay(350);
                        }
                        //}
                    }
                }
            }
            else
            {
                if (CurrentStep == 0)
                {
                    Form1_0.SetGameStatus("DOING KURAST");
                    CastDefense();
                    Form1_0.WaitDelay(15);

                    if (Form1_0.PlayerScan_0.levelNo == 79)
                    {
                        CurrentStep++;
                    }
                }

                if (CurrentStep == 1)
                {
                    if (Form1_0.ObjectsStruc_0.GetObjects("Act3TownWaypoint", false))
                    {
                        WP_X = Form1_0.ObjectsStruc_0.itemx;
                        WP_Y = Form1_0.ObjectsStruc_0.itemy;

                        if (Form1_0.ObjectsStruc_0.itemx == 5814 && Form1_0.ObjectsStruc_0.itemy == 3134)
                        {
                            MapStyle = 1;
                            ChestPos1 = new int[2] { 5699, 3178 };
                            ChestPos2 = new int[2] { 5663, 3218 };
                        }
                        if (Form1_0.ObjectsStruc_0.itemx == 5334 && Form1_0.ObjectsStruc_0.itemy == 2614)
                        {
                            MapStyle = 2;
                            ChestPos1 = new int[2] { 5423, 2618 };
                            ChestPos2 = new int[2] { 5457, 2576 };
                        }
                        if (Form1_0.ObjectsStruc_0.itemx == 4454 && Form1_0.ObjectsStruc_0.itemy == 2814)
                        {
                            MapStyle = 3;
                            ChestPos1 = new int[2] { 4421, 2896 };
                            ChestPos2 = new int[2] { 4455, 2857 };
                        }
                        if (Form1_0.ObjectsStruc_0.itemx == 5054 && Form1_0.ObjectsStruc_0.itemy == 1934)
                        {
                            MapStyle = 4;
                            ChestPos1 = new int[2] { 5182, 1898 };
                            ChestPos2 = new int[2] { 5217, 1857 };
                        }
                        if (Form1_0.ObjectsStruc_0.itemx == 5014 && Form1_0.ObjectsStruc_0.itemy == 2814)
                        {
                            MapStyle = 5;
                            ChestPos1 = new int[2] { 5063, 2936 };
                            ChestPos2 = new int[2] { 5098, 2897 };
                        }
                        if (Form1_0.ObjectsStruc_0.itemx == 5094 && Form1_0.ObjectsStruc_0.itemy == 2894)
                        {
                            MapStyle = 6;
                            ChestPos1 = new int[2] { 5183, 2856 };
                            ChestPos2 = new int[2] { 5219, 2816 };
                        }
                        if (Form1_0.ObjectsStruc_0.itemx == 5094 && Form1_0.ObjectsStruc_0.itemy == 2934)
                        {
                            MapStyle = 7;
                            ChestPos1 = new int[2] { 5061, 2856 };
                            ChestPos2 = new int[2] { 5098, 2816 };
                        }
                        if (Form1_0.ObjectsStruc_0.itemx == 5614 && Form1_0.ObjectsStruc_0.itemy == 3134)
                        {
                            MapStyle = 8;
                            ChestPos1 = new int[2] { 5544, 3178 };
                            ChestPos2 = new int[2] { 5579, 3137 };
                        }
                        if (Form1_0.ObjectsStruc_0.itemx == 5094 && Form1_0.ObjectsStruc_0.itemy == 3014)
                        {
                            MapStyle = 9;
                            ChestPos1 = new int[2] { 5105, 2895 };
                            ChestPos2 = new int[2] { 5137, 2855 };
                        }
                    }

                    if (MapStyle == 0)
                    {
                        Form1_0.method_1("UNKOWN KURAST MAPSTYLE, WP POS: " + Form1_0.ObjectsStruc_0.itemx + ", " + Form1_0.ObjectsStruc_0.itemy, Color.Red);
                        if (WP_X != 0 && WP_Y != 0)
                        {
                            TakeChest();
                            TakeChest();
                            TakeChest();
                            TakeChest();
                            TakeChest();
                            TakeChest();
                            Form1_0.ItemsStruc_0.GrabAllItemsForGold();
                        }
                        CurrentStep += 2;
                    }
                    else
                    {
                        Form1_0.method_1("KURAST MAPSTYLE #" + MapStyle, Color.DarkGreen);

                        if (Form1_0.Mover_0.MoveToLocation(ChestPos1[0], ChestPos1[1]))
                        {
                            //Form1_0.WaitDelay(250);
                            TakeChest();
                            CurrentStep++;
                        }
                    }
                }

                if (CurrentStep == 2)
                {
                    if (Form1_0.Mover_0.MoveToLocation(ChestPos2[0], ChestPos2[1]))
                    {
                        //Form1_0.WaitDelay(250);
                        TakeChest();
                        CurrentStep++;
                    }
                }

                if (CurrentStep == 3)
                {
                    if (WP_X != 0 && WP_Y != 0)
                    {
                        Form1_0.ItemsStruc_0.GrabAllItemsForGold();
                        if (Form1_0.Mover_0.MoveToLocation(WP_X, WP_Y))
                        {
                            //take back wp
                            if (Form1_0.ObjectsStruc_0.GetObjects("Act3TownWaypoint", false))
                            {
                                Dictionary<string, int> itemScreenPos = Form1_0.GameStruc_0.World2Screen(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, Form1_0.ObjectsStruc_0.itemx, Form1_0.ObjectsStruc_0.itemy);
                                Form1_0.KeyMouse_0.MouseClicc(itemScreenPos["x"], itemScreenPos["y"] - 15);
                                Form1_0.Mover_0.FinishMoving();
                                if (Form1_0.UIScan_0.WaitTilUIOpen("waypointMenu"))
                                {
                                    Form1_0.Town_0.SelectTownWP();
                                    Form1_0.Town_0.Towning = true;
                                    ScriptDone = true;
                                }
                            }
                        }
                    }
                    else
                    {
                        Form1_0.KeyMouse_0.MouseClicc(Form1_0.CenterX, Form1_0.CenterY - 100);
                        Form1_0.Mover_0.FinishMoving();
                        if (Form1_0.UIScan_0.WaitTilUIOpen("waypointMenu"))
                        {
                            Form1_0.Town_0.SelectTownWP();
                            Form1_0.Town_0.Towning = true;
                            ScriptDone = true;

                            Form1_0.WaitDelay(300);
                            Form1_0.UIScan_0.CloseUIMenu("invMenu");
                        }
                    }
                }
            }
        }

        public void TakeChest()
        {
            int Tryy = 0;
            while (Form1_0.ObjectsStruc_0.GetObjects("JungleMediumChestLeft", true, IgnoredChestList, 400) && Tryy < 30)
            //while (Form1_0.ObjectsStruc_0.GetObjects("AllChests", true, IgnoredChestList, 300) && Tryy < 30)
            {
                if (Form1_0.Mover_0.MoveToLocation(Form1_0.ObjectsStruc_0.itemx, Form1_0.ObjectsStruc_0.itemy))
                {
                    Form1_0.ObjectsStruc_0.GetUnitPathData();
                    Dictionary<string, int> itemScreenPos = Form1_0.GameStruc_0.World2Screen(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, Form1_0.ObjectsStruc_0.itemx, Form1_0.ObjectsStruc_0.itemy);
                    Form1_0.KeyMouse_0.MouseClicc(itemScreenPos["x"], itemScreenPos["y"] - 15);
                    Form1_0.WaitDelay(10);
                    Form1_0.KeyMouse_0.MouseClicc(itemScreenPos["x"], itemScreenPos["y"] - 15);
                    Form1_0.WaitDelay(10);
                    Form1_0.KeyMouse_0.MouseClicc(itemScreenPos["x"], itemScreenPos["y"] - 15);
                    Form1_0.WaitDelay(10);

                    int tryy2 = 0;
                    while (Form1_0.ItemsStruc_0.GetItems(true) && tryy2 < 20)
                    {
                        Form1_0.PlayerScan_0.GetPositions();
                        Form1_0.ItemsStruc_0.GetItems(false);
                        Form1_0.Potions_0.CheckIfWeUsePotion();
                        tryy2++;
                    }
                    IgnoredChestList.Add(Form1_0.ObjectsStruc_0.ObjectUnitID);
                    Tryy++;
                }
            }
        }

        public void CastDefense()
        {
            //cast sacred shield
            Form1_0.KeyMouse_0.PressKey(CharConfig.KeySkillCastDefense);
            Form1_0.WaitDelay(5);
            Form1_0.KeyMouse_0.MouseCliccRight(Form1_0.CenterX, Form1_0.CenterY);
            Form1_0.WaitDelay(5);
        }
    }
}
