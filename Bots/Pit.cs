using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Enums;
using static MapAreaStruc;

public class Pit
{
    Form1 Form1_0;

    public int CurrentStep = 0;
    public bool ScriptDone = false;
    public List<int> IgnoredChestList = new List<int>();
    public bool HasTakenAnyChest = false;


    public void SetForm1(Form1 form1_1)
    {
        Form1_0 = form1_1;
    }

    public void ResetVars()
    {
        CurrentStep = 0;
        ScriptDone = false;
        IgnoredChestList = new List<int>();
        HasTakenAnyChest = false;
    }

    public void DetectCurrentStep()
    {
        if ((Enums.Area)Form1_0.PlayerScan_0.levelNo == Enums.Area.BlackMarsh) CurrentStep = 1;
        if ((Enums.Area)Form1_0.PlayerScan_0.levelNo == Enums.Area.TamoeHighland) CurrentStep = 2;
        if ((Enums.Area)Form1_0.PlayerScan_0.levelNo == Enums.Area.PitLevel1) CurrentStep = 3;
        if ((Enums.Area)Form1_0.PlayerScan_0.levelNo == Enums.Area.PitLevel2) CurrentStep = 4;
    }

    public void RunScript()
    {
        Form1_0.Town_0.ScriptTownAct = 1; //set to town act 5 when running this script

        if (!Form1_0.Running || !Form1_0.GameStruc_0.IsInGame())
        {
            ScriptDone = true;
            return;
        }

        if (Form1_0.Town_0.GetInTown())
        {
            Form1_0.SetGameStatus("GO TO WP");
            CurrentStep = 0;

            Form1_0.Town_0.GoToWPArea(1, 4);
        }
        else
        {
            if (CurrentStep == 0)
            {
                Form1_0.SetGameStatus("DOING CRYPT");
                Form1_0.Battle_0.CastDefense();
                Form1_0.WaitDelay(15);

                if ((Enums.Area)Form1_0.PlayerScan_0.levelNo == Enums.Area.BlackMarsh)
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
                if ((Enums.Area)Form1_0.PlayerScan_0.levelNo == Enums.Area.TamoeHighland)
                {
                    CurrentStep++;
                    return;
                }

                Form1_0.PathFinding_0.MoveToNextArea(Enums.Area.TamoeHighland);
                CurrentStep++;
            }

            if (CurrentStep == 2)
            {
                //####
                if (Form1_0.PlayerScan_0.levelNo == (int)Enums.Area.PitLevel1)
                {
                    CurrentStep++;
                    return;
                }
                if ((Enums.Area)Form1_0.PlayerScan_0.levelNo == Enums.Area.BlackMarsh)
                {
                    CurrentStep--;
                    return;
                }
                //####

                Form1_0.PathFinding_0.MoveToExit(Enums.Area.PitLevel1);
                CurrentStep++;
            }

            if (CurrentStep == 3) 
            {
                //####
                if ((Enums.Area)Form1_0.PlayerScan_0.levelNo == Enums.Area.TamoeHighland)
                {
                    CurrentStep--;
                    return;
                }
                //####

                Form1_0.SetGameStatus("CLEARING PIT LVL1");
                if ((Enums.Area)Form1_0.Battle_0.AreaIDFullyCleared != Enums.Area.PitLevel1)
                {
                    Form1_0.Battle_0.ClearFullAreaOfMobs();

                    if (!Form1_0.Battle_0.ClearingArea)
                    {
                        Form1_0.PathFinding_0.MoveToExit(Enums.Area.PitLevel2);
                        CurrentStep++;
                    }
                }
                else
                {
                    Form1_0.PathFinding_0.MoveToExit(Enums.Area.PitLevel2);
                    CurrentStep++;
                }
            }

            if (CurrentStep == 4)
            {
                //####
                if (Form1_0.PlayerScan_0.levelNo == (int)Enums.Area.PitLevel1)
                {
                    Form1_0.PathFinding_0.MoveToExit(Enums.Area.PitLevel2);
                    return;
                }
                //####

                Form1_0.SetGameStatus("CLEARING PIT LVL2");
                if ((Enums.Area)Form1_0.Battle_0.AreaIDFullyCleared != Enums.Area.PitLevel2)
                {
                    Form1_0.Battle_0.ClearFullAreaOfMobs();

                    if (!Form1_0.Battle_0.ClearingArea)
                    {
                        TakeChest((int)(Enums.Area.PitLevel2));
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

    public void TakeChest(int ThisAreaa)
    {
        //JungleStashObject2
        //JungleStashObject3
        //GoodChest
        //NotSoGoodChest
        //DeadVillager1
        //DeadVillager2
        //NotSoGoodChest
        //HollowLog

        //JungleMediumChestLeft ####

        MapAreaStruc.Position ThisChestPos = Form1_0.MapAreaStruc_0.GetPositionOfObject("object", "GoodChest", ThisAreaa, IgnoredChestList);
        int ChestObject = Form1_0.MapAreaStruc_0.CurrentObjectIndex;
        int Tryy = 0;
        while (ThisChestPos.X != 0 && ThisChestPos.Y != 0 && Tryy < 30)
        {
            if (!Form1_0.Running || !Form1_0.GameStruc_0.IsInGame())
            {
                ScriptDone = true;
                return;
            }

            if (Form1_0.Mover_0.MoveToLocation(ThisChestPos.X, ThisChestPos.Y))
            {
                HasTakenAnyChest = true;

                Position itemScreenPos = Form1_0.GameStruc_0.World2Screen(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, ThisChestPos.X, ThisChestPos.Y);

                Form1_0.KeyMouse_0.MouseClicc_RealPos(itemScreenPos.X, itemScreenPos.Y - 15);
                Form1_0.WaitDelay(10);
                Form1_0.KeyMouse_0.MouseClicc_RealPos(itemScreenPos.X, itemScreenPos.Y - 15);
                Form1_0.WaitDelay(10);
                Form1_0.KeyMouse_0.MouseClicc_RealPos(itemScreenPos.X, itemScreenPos.Y - 15);
                Form1_0.WaitDelay(10);

                int tryy2 = 0;
                while (Form1_0.ItemsStruc_0.GetItems(true) && tryy2 < 20)
                {
                    Form1_0.PlayerScan_0.GetPositions();
                    Form1_0.ItemsStruc_0.GetItems(false);
                    Form1_0.Potions_0.CheckIfWeUsePotion();
                    tryy2++;
                }
                IgnoredChestList.Add(ChestObject);
            }

            ThisChestPos = Form1_0.MapAreaStruc_0.GetPositionOfObject("object", "GoodChest", ThisAreaa, IgnoredChestList);
            ChestObject = Form1_0.MapAreaStruc_0.CurrentObjectIndex;

            Tryy++;
        }

        if (!HasTakenAnyChest) Form1_0.MapAreaStruc_0.DumpMap();
    }
}
