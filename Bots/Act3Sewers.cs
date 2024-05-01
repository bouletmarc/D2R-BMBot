using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MapAreaStruc;

public class Act3Sewers
{
    Form1 Form1_0;

    public int CurrentStep = 0;
    public List<int> IgnoredChestList = new List<int>();
    public bool ScriptDone = false;
    public bool HasTakenAnyChest = false;
    public Position ChestPos = new Position { X = 0, Y = 0 };

    public void SetForm1(Form1 form1_1)
    {
        Form1_0 = form1_1;
    }

    public void ResetVars()
    {
        CurrentStep = 0;
        IgnoredChestList = new List<int>();
        ScriptDone = false;
    }

    public void RunScript()
    {
        Form1_0.Town_0.ScriptTownAct = 3; //set to town act 5 when running this script

        if (!Form1_0.Running || !Form1_0.GameStruc_0.IsInGame())
        {
            ScriptDone = true;
            return;
        }

        if (Form1_0.Town_0.GetInTown())
        {
            Form1_0.SetGameStatus("GO TO WP");
            CurrentStep = 0;

            Form1_0.Town_0.GoToWPArea(3, 5);
        }
        else
        {
            if (CurrentStep == 0)
            {
                Form1_0.SetGameStatus("DOING A3 SEWERS");
                Form1_0.Battle_0.CastDefense();
                Form1_0.WaitDelay(15);

                if ((Enums.Area)Form1_0.PlayerScan_0.levelNo == Enums.Area.KurastBazaar)
                {
                    CurrentStep++;
                }
                else
                {
                    Form1_0.Town_0.FastTowning = false;
                    Form1_0.Town_0.GoToTown();
                }
            }

            if (CurrentStep == 1)
            {
                TakeChest(Enums.Area.KurastBazaar);
                CurrentStep++;
            }

            if (CurrentStep == 2)
            {
                //####
                if (Form1_0.PlayerScan_0.levelNo == (int)Enums.Area.SewersLevel1Act3)
                {
                    CurrentStep++;
                    return;
                }
                //####

                Form1_0.PathFinding_0.MoveToExit(Enums.Area.SewersLevel1Act3);
                CurrentStep++;
            }

            if (CurrentStep == 3)
            {
                //####
                if (Form1_0.PlayerScan_0.levelNo == (int)Enums.Area.SewersLevel2Act3)
                {
                    CurrentStep++;
                    return;
                }
                if (Form1_0.PlayerScan_0.levelNo != (int)Enums.Area.SewersLevel1Act3)
                {
                    CurrentStep--;
                    return;
                }
                //####
                TakeChest(Enums.Area.SewersLevel1Act3);
                CurrentStep++;
            }

            if (CurrentStep == 4)
            {
                //####
                if (Form1_0.PlayerScan_0.levelNo == (int)Enums.Area.SewersLevel2Act3)
                {
                    CurrentStep++;
                    return;
                }
                if (Form1_0.PlayerScan_0.levelNo != (int)Enums.Area.SewersLevel1Act3)
                {
                    CurrentStep--;
                    return;
                }
                //####

                Form1_0.PathFinding_0.MoveToExit(Enums.Area.SewersLevel2Act3);

                ChestPos = Form1_0.MapAreaStruc_0.GetPositionOfObject("object", "Act3SewerStairsToLevel3", (int)Enums.Area.SewersLevel1Act3, new List<int>());
                if (ChestPos.X != 0 && ChestPos.Y != 0)
                {
                    Form1_0.PathFinding_0.MoveToThisPos(ChestPos);

                    Form1_0.Battle_0.SetSkills();
                    Form1_0.Battle_0.CastSkillsNoMove();

                    //repeat clic on leverfor stair
                    int tryyy = 0;
                    while (tryyy <= 25)
                    {
                        Position itemScreenPos = Form1_0.GameStruc_0.World2Screen(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, ChestPos.X, ChestPos.Y);

                        Form1_0.KeyMouse_0.MouseClicc_RealPos(itemScreenPos.X, itemScreenPos.Y);
                        Form1_0.PlayerScan_0.GetPositions();
                        Form1_0.WaitDelay(2);
                        tryyy++;
                    }

                    CurrentStep++;
                }
                else
                {
                    Form1_0.method_1("Lever location not detected!", Color.Red);
                    Form1_0.Town_0.FastTowning = false;
                    Form1_0.Town_0.UseLastTP = false;
                    ScriptDone = true;
                    return;
                }

                Form1_0.PathFinding_0.MoveToExit(Enums.Area.SewersLevel2Act3);

                CurrentStep++;
            }

            if (CurrentStep == 5)
            {
                //####
                if (Form1_0.PlayerScan_0.levelNo != (int)Enums.Area.SewersLevel2Act3)
                {
                    CurrentStep--;
                    return;
                }
                //####
                TakeChest(Enums.Area.SewersLevel2Act3);
                CurrentStep++;
            }

            if (CurrentStep == 6)
            {
                Form1_0.Town_0.Towning = true;
                Form1_0.Town_0.FastTowning = false;
                Form1_0.Town_0.UseLastTP = false;
                ScriptDone = true;
            }
        }
    }

    public void TakeChest(Enums.Area ThisArea)
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

        MapAreaStruc.Position ThisChestPos = Form1_0.MapAreaStruc_0.GetPositionOfObject("object", "GoodChest", (int)ThisArea, IgnoredChestList);
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

            ThisChestPos = Form1_0.MapAreaStruc_0.GetPositionOfObject("object", "GoodChest", (int)ThisArea, IgnoredChestList);
            ChestObject = Form1_0.MapAreaStruc_0.CurrentObjectIndex;

            Tryy++;
        }

        if (!HasTakenAnyChest) Form1_0.MapAreaStruc_0.DumpMap();
    }
}
