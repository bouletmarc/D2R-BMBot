using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MapAreaStruc;

public class Cows
{
    Form1 Form1_0;

    public int CurrentStep = 0;
    public bool ScriptDone = false;
    public Position TristramPos = new Position { X = 0, Y = 0 };

    public bool HasWirtsLeg = false;
    public bool HadWirtsLeg = false;

    public bool HadTomeOfPortal = false;

    public void SetForm1(Form1 form1_1)
    {
        Form1_0 = form1_1;
    }

    public void ResetVars()
    {
        CurrentStep = 0;
        ScriptDone = false;
        HasWirtsLeg = false;
        HadWirtsLeg = false;
        HadTomeOfPortal = false;
    }

    public void DetectCurrentStep()
    {
        if (HadTomeOfPortal)
        {
            if ((Enums.Area)Form1_0.PlayerScan_0.levelNo == Enums.Area.MooMooFarm) CurrentStep = 1;
        }
        else
        {
            if ((Enums.Area)Form1_0.PlayerScan_0.levelNo == Enums.Area.StonyField) CurrentStep = 1;
            if ((Enums.Area)Form1_0.PlayerScan_0.levelNo == Enums.Area.Tristram) CurrentStep = 3;
        }
    }

    public void RunScriptTristam()
    {
        if (!Form1_0.Running || !Form1_0.GameStruc_0.IsInGame())
        {
            ScriptDone = true;
            return;
        }

        if (Form1_0.Town_0.GetInTown())
        {
            Form1_0.SetGameStatus("GO TO WP");
            CurrentStep = 0;

            Form1_0.Town_0.GoToWPArea(1, 2);
        }
        else
        {
            if (CurrentStep == 0)
            {
                Form1_0.SetGameStatus("DOING TRISTRAM");
                Form1_0.Battle_0.CastDefense();
                Form1_0.WaitDelay(15);

                if ((Enums.Area)Form1_0.PlayerScan_0.levelNo == Enums.Area.StonyField)
                {
                    CurrentStep++;
                }
                else
                {
                    DetectCurrentStep();
                    if (CurrentStep == 0)
                    {
                        Form1_0.Town_0.FastTowning = false;
                        Form1_0.Town_0.GoToTown();
                    }
                }
            }

            if (CurrentStep == 1)
            {
                TristramPos = Form1_0.MapAreaStruc_0.GetPositionOfObject("object", "CairnStoneAlpha", (int)Enums.Area.StonyField, new List<int>());
                if (TristramPos.X != 0 && TristramPos.Y != 0)
                {
                    Form1_0.PathFinding_0.MoveToThisPos(TristramPos);

                    CurrentStep++;
                }
                else
                {
                    Form1_0.method_1("Tristram location not detected!", Color.Red);
                    Form1_0.Town_0.FastTowning = false;
                    Form1_0.Town_0.UseLastTP = false;
                    ScriptDone = true;
                    return;
                }
            }

            if (CurrentStep == 2)
            {
                Form1_0.SetGameStatus("Tristram waiting for Tristram portal");

                Form1_0.Battle_0.DoBattleScript(5);

                if (Form1_0.ObjectsStruc_0.GetObjects("PermanentTownPortal", true, new List<uint>(), 60))
                {
                    Form1_0.Mover_0.MoveToLocation(Form1_0.ObjectsStruc_0.itemx, Form1_0.ObjectsStruc_0.itemy);

                    Position itemScreenPos = Form1_0.GameStruc_0.World2Screen(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, Form1_0.ObjectsStruc_0.itemx, Form1_0.ObjectsStruc_0.itemy);

                    Form1_0.KeyMouse_0.MouseClicc_RealPos(itemScreenPos.X, itemScreenPos.Y - 15);
                    Form1_0.WaitDelay(20);
                }

                if (Form1_0.PlayerScan_0.levelNo == (int)Enums.Area.Tristram)
                {
                    CurrentStep++;
                }
            }

            if (CurrentStep == 3)
            {
                if ((Enums.Area)Form1_0.PlayerScan_0.levelNo == Enums.Area.StonyField)
                {
                    CurrentStep--;
                    return;
                }

                Form1_0.SetGameStatus("Doing Tristram");

                Form1_0.PathFinding_0.MoveToObject("WirtCorpse");

                if (Form1_0.ObjectsStruc_0.GetObjects("WirtCorpse", true, new List<uint>()))
                {
                    if (Form1_0.Mover_0.MoveToLocation(Form1_0.ObjectsStruc_0.itemx, Form1_0.ObjectsStruc_0.itemy))
                    {
                        Form1_0.InventoryStruc_0.DumpBadItemsOnGround();

                        //repeat clic on WirtCorpse
                        int tryyy = 0;
                        while (tryyy <= 7)
                        {
                            Position itemScreenPos = Form1_0.GameStruc_0.World2Screen(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, Form1_0.ObjectsStruc_0.itemx, Form1_0.ObjectsStruc_0.itemy);

                            Form1_0.KeyMouse_0.MouseClicc_RealPos(itemScreenPos.X, itemScreenPos.Y - 15);
                            Form1_0.WaitDelay(4);
                            tryyy++;
                        }

                        CurrentStep++;
                    }
                }
            }

            if (CurrentStep == 4)
            {
                //take leg
                HasWirtsLeg = Form1_0.InventoryStruc_0.HasInventoryItemName("Wirt's Leg");
                DateTime TimeSinceTakingLeg = DateTime.Now;
                while (!HasWirtsLeg && (DateTime.Now - TimeSinceTakingLeg).TotalSeconds < 2)
                {
                    Form1_0.ItemsStruc_0.PickThisItem("Wirt's Leg");
                    Form1_0.ItemsStruc_0.GetItems(false); //get inventory
                    HasWirtsLeg = Form1_0.InventoryStruc_0.HasInventoryItemName("Wirt's Leg");
                }

                if (HasWirtsLeg)
                {
                    HadWirtsLeg = true;
                    CurrentStep = 0; //go to next script for cow
                    Form1_0.Town_0.UseLastTP = false;
                    Form1_0.Town_0.FastTowning = false;
                    Form1_0.Town_0.Towning = true;
                    Form1_0.Town_0.GoToTown();
                }
                else
                {
                    CurrentStep--; //return clicking on corpse
                }
            }
        }
    }

    public void RunScriptCow()
    {
        if (!Form1_0.Running || !Form1_0.GameStruc_0.IsInGame())
        {
            ScriptDone = true;
            return;
        }

        if (Form1_0.Town_0.GetInTown())
        {
            Form1_0.SetGameStatus("GO TO SHOP");
            CurrentStep = 0;

            bool HasTownPortal = Form1_0.InventoryStruc_0.HasInventoryItemName("Tome of Town Portal", true);
            if (!HasTownPortal && !HadTomeOfPortal)
            {
                //buy tome of portal in store
                Form1_0.Shop_0.ShopForTomeOfPortal = true;
                Form1_0.Town_0.MoveToStore();
            }
            else
            {
                HadTomeOfPortal = true;

                if (Form1_0.ObjectsStruc_0.GetObjects("PermanentTownPortal", true, new List<uint>()))
                {
                    Position itemScreenPos = Form1_0.GameStruc_0.World2Screen(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, Form1_0.ObjectsStruc_0.itemx, Form1_0.ObjectsStruc_0.itemy);

                    Form1_0.KeyMouse_0.MouseClicc_RealPos(itemScreenPos.X, itemScreenPos.Y - 15);
                    Form1_0.WaitDelay(100);
                }
                else
                {
                    //move to stash to create portal by cubing it
                    Form1_0.Town_0.MoveToStash(true);
                }
            }
        }
        else
        {
            if (CurrentStep == 0)
            {
                Form1_0.SetGameStatus("DOING COWS");
                Form1_0.Battle_0.CastDefense();
                Form1_0.WaitDelay(15);

                if ((Enums.Area)Form1_0.PlayerScan_0.levelNo == Enums.Area.MooMooFarm)
                {
                    CurrentStep++;
                }
                else
                {
                    DetectCurrentStep();
                    //Console.WriteLine("step shoul be 0: " + CurrentStep);
                    if (CurrentStep == 0)
                    {
                        Form1_0.Town_0.FastTowning = false;
                        Form1_0.Town_0.GoToTown();
                    }
                }
            }

            if (CurrentStep == 1)
            {
                if ((Enums.Area)Form1_0.Battle_0.AreaIDFullyCleared != Enums.Area.MooMooFarm)
                {
                    Form1_0.Battle_0.ClearFullAreaOfMobs();

                    if (!Form1_0.Battle_0.ClearingArea)
                    {
                        Form1_0.Town_0.FastTowning = false;
                        Form1_0.Town_0.UseLastTP = false;
                        ScriptDone = true;
                    }
                }
                else
                {
                    Form1_0.Town_0.FastTowning = false;
                    Form1_0.Town_0.UseLastTP = false;
                    ScriptDone = true;
                }
            }
        }
    }

    public void RunScript()
    {
        Form1_0.Town_0.ScriptTownAct = 1; //set to town act 5 when running this script

        if (!Form1_0.Running || !Form1_0.GameStruc_0.IsInGame())
        {
            ScriptDone = true;
            return;
        }

        Form1_0.ItemsStruc_0.GetItems(false); //get inventory
        HasWirtsLeg = Form1_0.InventoryStruc_0.HasInventoryItemName("Wirt's Leg", true);
        if ((Enums.Area)Form1_0.PlayerScan_0.levelNo == Enums.Area.MooMooFarm) HasWirtsLeg = true;

        if (HasWirtsLeg) HadWirtsLeg = true;

        if (HasWirtsLeg || HadWirtsLeg)
        {
            if ((Enums.Area)Form1_0.PlayerScan_0.levelNo == Enums.Area.Tristram) RunScriptTristam();
            else RunScriptCow();
        }
        else
        {
            RunScriptTristam();
        }
    }
}
