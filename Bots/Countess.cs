using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app
{
    public class Countess
    {
        Form1 Form1_0;

        public int CurrentStep = 0;
        public bool ScriptDone = false;


        public void SetForm1(Form1 form1_1)
        {
            Form1_0 = form1_1;
        }

        public void ResetVars()
        {
            CurrentStep = 0;
            ScriptDone = false;
        }

        public void RunScript()
        {
            Form1_0.Town_0.ScriptTownAct = 5; //set to town act 5 when running this script

            if (Form1_0.Town_0.GetInTown())
            {
                Form1_0.SetGameStatus("GO TO WP");
                CurrentStep = 0;

                Form1_0.Town_0.GoToWPArea(1, 4);

                //close to store spot 5078, 5026
                /*if (Form1_0.Town_0.IsPosCloseTo(5084, 5037, 7))
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
                }*/
            }
            else
            {
                if (CurrentStep == 0)
                {
                    Form1_0.SetGameStatus("DOING COUNTESS");
                    Form1_0.Battle_0.CastDefense();
                    Form1_0.WaitDelay(15);

                    if ((Enums.Area) Form1_0.PlayerScan_0.levelNo == Enums.Area.BlackMarsh)
                    {
                        CurrentStep++;
                    }
                    else
                    {
                        Form1_0.Town_0.GoToTown();
                    }
                }

                if (CurrentStep == 1)
                {
                    Form1_0.MoveToPath_0.MoveToArea(Enums.Area.ForgottenTower);
                    Form1_0.MoveToPath_0.MoveToArea(Enums.Area.TowerCellarLevel1);
                    Form1_0.MoveToPath_0.MoveToArea(Enums.Area.TowerCellarLevel2);
                    Form1_0.MoveToPath_0.MoveToArea(Enums.Area.TowerCellarLevel3);
                    Form1_0.MoveToPath_0.MoveToArea(Enums.Area.TowerCellarLevel4);
                    Form1_0.MoveToPath_0.MoveToArea(Enums.Area.TowerCellarLevel5);
                    CurrentStep++;
                }

                if (CurrentStep == 2)
                {
                    MapAreaStruc.Position ThisChestPos = Form1_0.MapAreaStruc_0.GetPositionOfObject("object", "GoodChest", 78, new List<int>());

                    //Form1_0.ItemsStruc_0.GrabAllItemsForGold();
                    if (Form1_0.Mover_0.MoveToLocation(ThisChestPos.X, ThisChestPos.Y))
                    {
                        CurrentStep++;
                    }
                }

                if (CurrentStep == 3)
                {
                    Form1_0.Potions_0.CanUseSkillForRegen = false;
                    Form1_0.SetGameStatus("KILLING COUNTESS");
                    if (Form1_0.MobsStruc_0.GetMobs("getSuperUniqueName", "The Countess", false, 200, new List<long>()))
                    {
                        if (Form1_0.MobsStruc_0.MobsHP > 0)
                        {
                            Form1_0.Battle_0.RunBattleScriptOnThisMob("getSuperUniqueName", "The Countess");
                        }
                        else
                        {
                            Form1_0.ItemsStruc_0.GetItems(true);
                            Form1_0.ItemsStruc_0.GetItems(true);
                            Form1_0.ItemsStruc_0.GetItems(true);
                            Form1_0.ItemsStruc_0.GetItems(true);
                            Form1_0.ItemsStruc_0.GetItems(true);
                            Form1_0.ItemsStruc_0.GetItems(true);
                            Form1_0.ItemsStruc_0.GetItems(true);
                            Form1_0.ItemsStruc_0.GetItems(true);
                            Form1_0.ItemsStruc_0.GetItems(true);
                            Form1_0.ItemsStruc_0.GetItems(true);
                            Form1_0.ItemsStruc_0.GrabAllItemsForGold();
                            Form1_0.Potions_0.CanUseSkillForRegen = true;

                            ScriptDone = true;
                            return;
                            //Form1_0.LeaveGame(true);
                        }
                    }
                    else
                    {
                        Form1_0.method_1("Countess not detected!", Color.Red);

                        //baal not detected...
                        Form1_0.ItemsStruc_0.GetItems(true);
                        if (Form1_0.MobsStruc_0.GetMobs("getSuperUniqueName", "The Countess", false, 200, new List<long>())) return; //redetect baal?
                        Form1_0.ItemsStruc_0.GrabAllItemsForGold();
                        if (Form1_0.MobsStruc_0.GetMobs("getSuperUniqueName", "The Countess", false, 200, new List<long>())) return; //redetect baal?
                        Form1_0.Potions_0.CanUseSkillForRegen = true;

                        ScriptDone = true;
                        return;
                        //Form1_0.LeaveGame(true);
                    }
                }
            }
        }
    }
}
