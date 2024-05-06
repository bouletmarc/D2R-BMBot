using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MapAreaStruc;
using static Enums;

public class TerrorZones
{
    Form1 Form1_0;

    public int CurrentStep = 0;
    public bool ScriptDone = false;

    public List<Area> TerrorZonesAreas = new List<Area>();
    public int CurrentTerrorZonesIndex = 0;

    public List<int> IgnoredChestList = new List<int>();
    public bool HasTakenAnyChest = false;

    public bool DoneChestsStep = false;


    public void SetForm1(Form1 form1_1)
    {
        Form1_0 = form1_1;
    }

    public void ResetVars()
    {
        CurrentStep = 0;
        ScriptDone = false;
        TerrorZonesAreas = new List<Area>();
        CurrentTerrorZonesIndex = 0;
        IgnoredChestList = new List<int>();
        HasTakenAnyChest = false;
        DoneChestsStep = false;
    }

    public void RunScript()
    {
        if (TerrorZonesAreas.Count == 0) TerrorZonesAreas = Form1_0.GameStruc_0.GetTerrorZones();
        if (TerrorZonesAreas.Count == 0)
        {
            Form1_0.method_1("No Terror Zones Detected!", Color.Red);
            ScriptDone = true;
            return;
        }

        Form1_0.Town_0.ScriptTownAct = Form1_0.AreaScript_0.GetActFromArea(TerrorZonesAreas[CurrentTerrorZonesIndex]); //set to town act 5 when running this script

        if (!Form1_0.Running || !Form1_0.GameStruc_0.IsInGame())
        {
            ScriptDone = true;
            return;
        }

        if (Form1_0.Town_0.GetInTown())
        {
            Form1_0.SetGameStatus("GO TO WP");
            CurrentStep = 0;
            DoneChestsStep = false;

            //Console.WriteLine(TerrorZonesAreas[CurrentTerrorZonesIndex]);

            if (TerrorZonesAreas[CurrentTerrorZonesIndex] == Area.BloodMoor) Form1_0.PathFinding_0.MoveToNextArea(Enums.Area.BloodMoor);
            else if (TerrorZonesAreas[CurrentTerrorZonesIndex] == Area.SewersLevel1Act2) Form1_0.PathFinding_0.MoveToNextArea(Enums.Area.SewersLevel1Act2);
            else if (TerrorZonesAreas[CurrentTerrorZonesIndex] == Area.OuterSteppes) Form1_0.PathFinding_0.MoveToNextArea(Enums.Area.OuterSteppes);
            else if (TerrorZonesAreas[CurrentTerrorZonesIndex] == Area.BloodyFoothills) Form1_0.PathFinding_0.MoveToNextArea(Enums.Area.BloodyFoothills);
            //######################
            //ACT 1
            else if (TerrorZonesAreas[CurrentTerrorZonesIndex] == Area.DenOfEvil)
            {
                Form1_0.PathFinding_0.MoveToNextArea(Enums.Area.BloodMoor);
                Form1_0.PathFinding_0.MoveToExit(Enums.Area.DenOfEvil);
            }
            else if (TerrorZonesAreas[CurrentTerrorZonesIndex] == Area.ColdPlains) Form1_0.Town_0.GoToWPArea(1, 1);
            else if (TerrorZonesAreas[CurrentTerrorZonesIndex] == Area.CaveLevel1)
            {
                Form1_0.Town_0.GoToWPArea(1, 1);
                Form1_0.PathFinding_0.MoveToExit(Enums.Area.CaveLevel1);
            }
            else if (TerrorZonesAreas[CurrentTerrorZonesIndex] == Area.CaveLevel2)
            {
                Form1_0.Town_0.GoToWPArea(1, 1);
                Form1_0.PathFinding_0.MoveToExit(Enums.Area.CaveLevel1);
                Form1_0.PathFinding_0.MoveToExit(Enums.Area.CaveLevel2);
            }
            else if (TerrorZonesAreas[CurrentTerrorZonesIndex] == Area.StonyField) Form1_0.Town_0.GoToWPArea(1, 2);
            else if (TerrorZonesAreas[CurrentTerrorZonesIndex] == Area.DarkWood) Form1_0.Town_0.GoToWPArea(1, 3);
            else if (TerrorZonesAreas[CurrentTerrorZonesIndex] == Area.UndergroundPassageLevel1)
            {
                Form1_0.Town_0.GoToWPArea(1, 3);
                Form1_0.PathFinding_0.MoveToExit(Enums.Area.UndergroundPassageLevel1);
            }
            else if (TerrorZonesAreas[CurrentTerrorZonesIndex] == Area.UndergroundPassageLevel2)
            {
                Form1_0.Town_0.GoToWPArea(1, 3);
                Form1_0.PathFinding_0.MoveToExit(Enums.Area.UndergroundPassageLevel1);
                Form1_0.PathFinding_0.MoveToExit(Enums.Area.UndergroundPassageLevel2);
            }
            else if (TerrorZonesAreas[CurrentTerrorZonesIndex] == Area.BlackMarsh) Form1_0.Town_0.GoToWPArea(1, 4);
            else if (TerrorZonesAreas[CurrentTerrorZonesIndex] == Area.HoleLevel1)
            {
                Form1_0.Town_0.GoToWPArea(1, 4);
                Form1_0.PathFinding_0.MoveToExit(Enums.Area.HoleLevel1);
            }
            else if (TerrorZonesAreas[CurrentTerrorZonesIndex] == Area.HoleLevel2)
            {
                Form1_0.Town_0.GoToWPArea(1, 4);
                Form1_0.PathFinding_0.MoveToExit(Enums.Area.HoleLevel1);
                Form1_0.PathFinding_0.MoveToExit(Enums.Area.HoleLevel2);
            }
            else if (TerrorZonesAreas[CurrentTerrorZonesIndex] == Area.ForgottenTower)
            {
                Form1_0.Town_0.GoToWPArea(1, 4);
                Form1_0.PathFinding_0.MoveToExit(Enums.Area.ForgottenTower);
            }
            else if (TerrorZonesAreas[CurrentTerrorZonesIndex] == Area.TowerCellarLevel1)
            {
                Form1_0.Town_0.GoToWPArea(1, 4);
                Form1_0.PathFinding_0.MoveToExit(Enums.Area.ForgottenTower);
                Form1_0.PathFinding_0.MoveToExit(Enums.Area.TowerCellarLevel1);
            }
            else if (TerrorZonesAreas[CurrentTerrorZonesIndex] == Area.TowerCellarLevel2)
            {
                Form1_0.Town_0.GoToWPArea(1, 4);
                Form1_0.PathFinding_0.MoveToExit(Enums.Area.ForgottenTower);
                Form1_0.PathFinding_0.MoveToExit(Enums.Area.TowerCellarLevel1);
                Form1_0.PathFinding_0.MoveToExit(Enums.Area.TowerCellarLevel2);
            }
            else if (TerrorZonesAreas[CurrentTerrorZonesIndex] == Area.TowerCellarLevel3)
            {
                Form1_0.Town_0.GoToWPArea(1, 4);
                Form1_0.PathFinding_0.MoveToExit(Enums.Area.ForgottenTower);
                Form1_0.PathFinding_0.MoveToExit(Enums.Area.TowerCellarLevel1);
                Form1_0.PathFinding_0.MoveToExit(Enums.Area.TowerCellarLevel2);
                Form1_0.PathFinding_0.MoveToExit(Enums.Area.TowerCellarLevel3);
            }
            else if (TerrorZonesAreas[CurrentTerrorZonesIndex] == Area.TowerCellarLevel4)
            {
                Form1_0.Town_0.GoToWPArea(1, 4);
                Form1_0.PathFinding_0.MoveToExit(Enums.Area.ForgottenTower);
                Form1_0.PathFinding_0.MoveToExit(Enums.Area.TowerCellarLevel1);
                Form1_0.PathFinding_0.MoveToExit(Enums.Area.TowerCellarLevel2);
                Form1_0.PathFinding_0.MoveToExit(Enums.Area.TowerCellarLevel3);
                Form1_0.PathFinding_0.MoveToExit(Enums.Area.TowerCellarLevel4);
            }
            else if (TerrorZonesAreas[CurrentTerrorZonesIndex] == Area.TowerCellarLevel5)
            {
                Form1_0.Town_0.GoToWPArea(1, 4);
                Form1_0.PathFinding_0.MoveToExit(Enums.Area.ForgottenTower);
                Form1_0.PathFinding_0.MoveToExit(Enums.Area.TowerCellarLevel1);
                Form1_0.PathFinding_0.MoveToExit(Enums.Area.TowerCellarLevel2);
                Form1_0.PathFinding_0.MoveToExit(Enums.Area.TowerCellarLevel3);
                Form1_0.PathFinding_0.MoveToExit(Enums.Area.TowerCellarLevel4);
                Form1_0.PathFinding_0.MoveToExit(Enums.Area.TowerCellarLevel5);
            }
            else if (TerrorZonesAreas[CurrentTerrorZonesIndex] == Area.Barracks)
            {
                Form1_0.Town_0.GoToWPArea(1, 5);
                Form1_0.PathFinding_0.MoveToNextArea(Enums.Area.Barracks);
            }
            else if (TerrorZonesAreas[CurrentTerrorZonesIndex] == Area.JailLevel1) Form1_0.Town_0.GoToWPArea(1, 6);
            else if (TerrorZonesAreas[CurrentTerrorZonesIndex] == Area.JailLevel2)
            {
                Form1_0.Town_0.GoToWPArea(1, 6);
                Form1_0.PathFinding_0.MoveToExit(Enums.Area.JailLevel2);
            }
            else if (TerrorZonesAreas[CurrentTerrorZonesIndex] == Area.JailLevel3)
            {
                Form1_0.Town_0.GoToWPArea(1, 6);
                Form1_0.PathFinding_0.MoveToExit(Enums.Area.JailLevel2);
                Form1_0.PathFinding_0.MoveToExit(Enums.Area.JailLevel3);
            }
            else if (TerrorZonesAreas[CurrentTerrorZonesIndex] == Area.Cathedral)
            {
                Form1_0.Town_0.GoToWPArea(1, 7);
                Form1_0.PathFinding_0.MoveToNextArea(Enums.Area.Cathedral);
            }
            else if (TerrorZonesAreas[CurrentTerrorZonesIndex] == Area.InnerCloister) Form1_0.Town_0.GoToWPArea(1, 7);
            else if (TerrorZonesAreas[CurrentTerrorZonesIndex] == Area.CatacombsLevel1)
            {
                Form1_0.Town_0.GoToWPArea(1, 7);
                Form1_0.PathFinding_0.MoveToNextArea(Enums.Area.Cathedral);
                Form1_0.PathFinding_0.MoveToNextArea(Enums.Area.CatacombsLevel1);
            }
            else if (TerrorZonesAreas[CurrentTerrorZonesIndex] == Area.CatacombsLevel2) Form1_0.Town_0.GoToWPArea(1, 8);
            else if (TerrorZonesAreas[CurrentTerrorZonesIndex] == Area.CatacombsLevel3)
            {
                Form1_0.Town_0.GoToWPArea(1, 8);
                Form1_0.PathFinding_0.MoveToExit(Enums.Area.CatacombsLevel3);
            }
            else if (TerrorZonesAreas[CurrentTerrorZonesIndex] == Area.CatacombsLevel4)
            {
                Form1_0.Town_0.GoToWPArea(1, 8);
                Form1_0.PathFinding_0.MoveToExit(Enums.Area.CatacombsLevel3);
                Form1_0.PathFinding_0.MoveToExit(Enums.Area.CatacombsLevel4);
            }
            else if (TerrorZonesAreas[CurrentTerrorZonesIndex] == Area.BurialGrounds)
            {
                Form1_0.Town_0.GoToWPArea(1, 1);
                Form1_0.PathFinding_0.MoveToNextArea(Enums.Area.BurialGrounds);
            }
            else if (TerrorZonesAreas[CurrentTerrorZonesIndex] == Area.Crypt)
            {
                Form1_0.Town_0.GoToWPArea(1, 1);
                Form1_0.PathFinding_0.MoveToNextArea(Enums.Area.BurialGrounds);
                Form1_0.PathFinding_0.MoveToExit(Enums.Area.Crypt);
            }
            else if (TerrorZonesAreas[CurrentTerrorZonesIndex] == Area.Mausoleum)
            {
                Form1_0.Town_0.GoToWPArea(1, 1);
                Form1_0.PathFinding_0.MoveToNextArea(Enums.Area.BurialGrounds);
                Form1_0.PathFinding_0.MoveToExit(Enums.Area.Mausoleum);
            }
            //######################
            //ACT 2
            else if (TerrorZonesAreas[CurrentTerrorZonesIndex] == Area.SewersLevel2Act2) Form1_0.Town_0.GoToWPArea(2, 1);
            else if (TerrorZonesAreas[CurrentTerrorZonesIndex] == Area.SewersLevel3Act2)
            {
                Form1_0.Town_0.GoToWPArea(2, 1);
                Form1_0.PathFinding_0.MoveToExit(Enums.Area.SewersLevel3Act2);
            }
            else if (TerrorZonesAreas[CurrentTerrorZonesIndex] == Area.DryHills) Form1_0.Town_0.GoToWPArea(2, 2);
            else if (TerrorZonesAreas[CurrentTerrorZonesIndex] == Area.HallsOfTheDeadLevel1)
            {
                Form1_0.Town_0.GoToWPArea(2, 2);
                Form1_0.PathFinding_0.MoveToExit(Enums.Area.HallsOfTheDeadLevel1);
            }
            else if (TerrorZonesAreas[CurrentTerrorZonesIndex] == Area.HallsOfTheDeadLevel2) Form1_0.Town_0.GoToWPArea(2, 3);
            else if (TerrorZonesAreas[CurrentTerrorZonesIndex] == Area.HallsOfTheDeadLevel3)
            {
                Form1_0.Town_0.GoToWPArea(2, 3);
                Form1_0.PathFinding_0.MoveToExit(Enums.Area.HallsOfTheDeadLevel3);
            }
            else if (TerrorZonesAreas[CurrentTerrorZonesIndex] == Area.FarOasis) Form1_0.Town_0.GoToWPArea(2, 4);
            else if (TerrorZonesAreas[CurrentTerrorZonesIndex] == Area.LostCity) Form1_0.Town_0.GoToWPArea(2, 5);
            else if (TerrorZonesAreas[CurrentTerrorZonesIndex] == Area.ValleyOfSnakes)
            {
                Form1_0.Town_0.GoToWPArea(2, 5);
                Form1_0.PathFinding_0.MoveToNextArea(Enums.Area.ValleyOfSnakes);
            }
            else if (TerrorZonesAreas[CurrentTerrorZonesIndex] == Area.ClawViperTempleLevel1)
            {
                Form1_0.Town_0.GoToWPArea(2, 5);
                Form1_0.PathFinding_0.MoveToNextArea(Enums.Area.ValleyOfSnakes);
                Form1_0.PathFinding_0.MoveToExit(Enums.Area.ClawViperTempleLevel1);
            }
            else if (TerrorZonesAreas[CurrentTerrorZonesIndex] == Area.ClawViperTempleLevel2)
            {
                Form1_0.Town_0.GoToWPArea(2, 5);
                Form1_0.PathFinding_0.MoveToNextArea(Enums.Area.ValleyOfSnakes);
                Form1_0.PathFinding_0.MoveToExit(Enums.Area.ClawViperTempleLevel1);
                Form1_0.PathFinding_0.MoveToExit(Enums.Area.ClawViperTempleLevel2);
            }
            else if (TerrorZonesAreas[CurrentTerrorZonesIndex] == Area.ArcaneSanctuary) Form1_0.Town_0.GoToWPArea(2, 7);
            //######################
            //ACT 3
            else if (TerrorZonesAreas[CurrentTerrorZonesIndex] == Area.SpiderForest) Form1_0.Town_0.GoToWPArea(3, 1);
            else if (TerrorZonesAreas[CurrentTerrorZonesIndex] == Area.SpiderCavern)
            {
                Form1_0.Town_0.GoToWPArea(3, 1);
                Form1_0.PathFinding_0.MoveToExit(Enums.Area.SpiderCavern);
            }
            else if (TerrorZonesAreas[CurrentTerrorZonesIndex] == Area.GreatMarsh) Form1_0.Town_0.GoToWPArea(3, 2);
            else if (TerrorZonesAreas[CurrentTerrorZonesIndex] == Area.FlayerJungle) Form1_0.Town_0.GoToWPArea(3, 3);
            else if (TerrorZonesAreas[CurrentTerrorZonesIndex] == Area.FlayerDungeonLevel1)
            {
                Form1_0.Town_0.GoToWPArea(3, 3);
                Form1_0.PathFinding_0.MoveToExit(Enums.Area.FlayerDungeonLevel1);
            }
            else if (TerrorZonesAreas[CurrentTerrorZonesIndex] == Area.FlayerDungeonLevel2)
            {
                Form1_0.Town_0.GoToWPArea(3, 3);
                Form1_0.PathFinding_0.MoveToExit(Enums.Area.FlayerDungeonLevel1);
                Form1_0.PathFinding_0.MoveToExit(Enums.Area.FlayerDungeonLevel2);
            }
            else if (TerrorZonesAreas[CurrentTerrorZonesIndex] == Area.FlayerDungeonLevel3)
            {
                Form1_0.Town_0.GoToWPArea(3, 3);
                Form1_0.PathFinding_0.MoveToExit(Enums.Area.FlayerDungeonLevel1);
                Form1_0.PathFinding_0.MoveToExit(Enums.Area.FlayerDungeonLevel2);
                Form1_0.PathFinding_0.MoveToExit(Enums.Area.FlayerDungeonLevel3);
            }
            else if (TerrorZonesAreas[CurrentTerrorZonesIndex] == Area.FlayerJungle) Form1_0.Town_0.GoToWPArea(3, 5);
            else if (TerrorZonesAreas[CurrentTerrorZonesIndex] == Area.KurastBazaar) Form1_0.Town_0.GoToWPArea(3, 5);
            else if (TerrorZonesAreas[CurrentTerrorZonesIndex] == Area.RuinedTemple)
            {
                Form1_0.Town_0.GoToWPArea(3, 5);
                Form1_0.PathFinding_0.MoveToExit(Enums.Area.RuinedTemple);
            }
            else if (TerrorZonesAreas[CurrentTerrorZonesIndex] == Area.DisusedFane)
            {
                Form1_0.Town_0.GoToWPArea(3, 5);
                Form1_0.PathFinding_0.MoveToExit(Enums.Area.RuinedTemple);
                Form1_0.PathFinding_0.MoveToExit(Enums.Area.DisusedFane);
            }
            //######################
            //ACT 4
            else if (TerrorZonesAreas[CurrentTerrorZonesIndex] == Area.PlainsOfDespair)
            {
                Form1_0.PathFinding_0.MoveToNextArea(Enums.Area.OuterSteppes);
                Form1_0.PathFinding_0.MoveToNextArea(Enums.Area.PlainsOfDespair);
            }
            else if (TerrorZonesAreas[CurrentTerrorZonesIndex] == Area.RiverOfFlame) Form1_0.Town_0.GoToWPArea(4, 2);
            else if (TerrorZonesAreas[CurrentTerrorZonesIndex] == Area.CityOfTheDamned) Form1_0.Town_0.GoToWPArea(4, 1);

            //######################
            //ACT 5
            else if (TerrorZonesAreas[CurrentTerrorZonesIndex] == Area.FrigidHighlands) Form1_0.Town_0.GoToWPArea(5, 1);
            else if (TerrorZonesAreas[CurrentTerrorZonesIndex] == Area.AbAddon)
            {
                Form1_0.Town_0.GoToWPArea(5, 1);
                Form1_0.PathFinding_0.MoveToExit(Enums.Area.AbAddon);
            }
            else if (TerrorZonesAreas[CurrentTerrorZonesIndex] == Area.GlacialTrail) Form1_0.Town_0.GoToWPArea(5, 4);
            else if (TerrorZonesAreas[CurrentTerrorZonesIndex] == Area.DrifterCavern)
            {
                Form1_0.Town_0.GoToWPArea(5, 4);
                Form1_0.PathFinding_0.MoveToExit(Enums.Area.DrifterCavern);
            }
            else if (TerrorZonesAreas[CurrentTerrorZonesIndex] == Area.CrystallinePassage) Form1_0.Town_0.GoToWPArea(5, 3);
            else if (TerrorZonesAreas[CurrentTerrorZonesIndex] == Area.FrozenRiver)
            {
                Form1_0.Town_0.GoToWPArea(5, 3);
                Form1_0.PathFinding_0.MoveToExit(Enums.Area.FrozenRiver);
            }
            else if (TerrorZonesAreas[CurrentTerrorZonesIndex] == Area.ArreatPlateau) Form1_0.Town_0.GoToWPArea(5, 2);
            else if (TerrorZonesAreas[CurrentTerrorZonesIndex] == Area.PitOfAcheron)
            {
                Form1_0.Town_0.GoToWPArea(5, 2);
                Form1_0.PathFinding_0.MoveToExit(Enums.Area.PitOfAcheron);
            }
            else if (TerrorZonesAreas[CurrentTerrorZonesIndex] == Area.TheAncientsWay) Form1_0.Town_0.GoToWPArea(5, 7);
            else if (TerrorZonesAreas[CurrentTerrorZonesIndex] == Area.IcyCellar)
            {
                //Form1_0.Town_0.GoToWPArea(5, 6);
                Form1_0.Town_0.GoToWPArea(5, 7);
                Form1_0.PathFinding_0.MoveToExit(Enums.Area.IcyCellar);
            }

        }
        else
        {
            if (CurrentStep == 0)
            {
                Form1_0.SetGameStatus("DOING TERROR ZONES");
                Form1_0.Battle_0.CastDefense();
                Form1_0.WaitDelay(15);

                if ((Enums.Area)Form1_0.PlayerScan_0.levelNo == TerrorZonesAreas[CurrentTerrorZonesIndex])
                {
                    CurrentStep++;
                }
                else
                {
                    if (CurrentStep == 0)
                    {
                        Form1_0.Town_0.FastTowning = false;
                        Form1_0.Town_0.GoToTown();
                    }
                }
            }

            if (CurrentStep == 1)
            {
                if (!DoneChestsStep)
                {
                    TakeChest((int) Form1_0.PlayerScan_0.levelNo);
                    DoneChestsStep = true;
                }
                if ((Enums.Area)Form1_0.Battle_0.AreaIDFullyCleared != TerrorZonesAreas[CurrentTerrorZonesIndex])
                {
                    Form1_0.Battle_0.ClearFullAreaOfMobs();

                    if (!Form1_0.Battle_0.ClearingArea)
                    {
                        CurrentTerrorZonesIndex++;
                        if (CurrentTerrorZonesIndex > TerrorZonesAreas.Count - 1)
                        {
                            Form1_0.Town_0.FastTowning = false;
                            Form1_0.Town_0.UseLastTP = false;
                            ScriptDone = true;
                        }
                        else
                        {
                            CurrentStep = 0;
                            Form1_0.Town_0.FastTowning = false;
                            Form1_0.Town_0.GoToTown();
                            //Form1_0.PathFinding_0.MoveToExit(TerrorZonesAreas[CurrentTerrorZonesIndex], 4, true);
                        }
                    }
                }
                else
                {
                    CurrentTerrorZonesIndex++;
                    if (CurrentTerrorZonesIndex > TerrorZonesAreas.Count - 1)
                    {
                        Form1_0.Town_0.FastTowning = false;
                        Form1_0.Town_0.UseLastTP = false;
                        ScriptDone = true;
                    }
                    else
                    {
                        CurrentStep = 0;
                        Form1_0.Town_0.FastTowning = false;
                        Form1_0.Town_0.GoToTown();
                        //Form1_0.PathFinding_0.MoveToExit(TerrorZonesAreas[CurrentTerrorZonesIndex], 4, true);
                    }
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




    /*public List<List<Area>> tzAreaChain(Area firstTZ)
    {
        switch (firstTZ)
        {
            // Act 1
            case Area.BloodMoor:
                return new List<List<Area>> { new List<Area> { Area.RogueEncampment, Area.BloodMoor, Area.DenOfEvil } };
            case Area.ColdPlains:
                return new List<List<Area>> { new List<Area> { Area.ColdPlains, Area.CaveLevel1, Area.CaveLevel2 } };
            case Area.BurialGrounds:
                return new List<List<Area>>
                {
                    new List<Area> { Area.ColdPlains, Area.BurialGrounds, Area.Crypt },
                    new List<Area> { Area.ColdPlains, Area.BurialGrounds, Area.Mausoleum }
                };
            case Area.StonyField:
                return new List<List<Area>> { new List<Area> { Area.StonyField } };
            case Area.DarkWood:
                return new List<List<Area>> { new List<Area> { Area.DarkWood, Area.UndergroundPassageLevel1, Area.UndergroundPassageLevel2 } };
            case Area.BlackMarsh:
                return new List<List<Area>> { new List<Area> { Area.BlackMarsh, Area.HoleLevel1, Area.HoleLevel2 } };
            case Area.ForgottenTower:
                return new List<List<Area>> { new List<Area> { Area.BlackMarsh, Area.ForgottenTower, Area.TowerCellarLevel1, Area.TowerCellarLevel2, Area.TowerCellarLevel3, Area.TowerCellarLevel4, Area.TowerCellarLevel5 } };
            case Area.JailLevel1:
                return new List<List<Area>> { new List<Area> { Area.JailLevel1, Area.JailLevel2, Area.JailLevel3 } };
            case Area.Cathedral:
                return new List<List<Area>> { new List<Area> { Area.InnerCloister, Area.Cathedral, Area.CatacombsLevel1, Area.CatacombsLevel2, Area.CatacombsLevel3 } };
            // Act 2
            case Area.SewersLevel1Act2:
                return new List<List<Area>> { new List<Area> { Area.LutGholein, Area.SewersLevel1Act2, Area.SewersLevel2Act2, Area.SewersLevel3Act2 } };
            case Area.DryHills:
                return new List<List<Area>> { new List<Area> { Area.DryHills, Area.HallsOfTheDeadLevel1, Area.HallsOfTheDeadLevel2, Area.HallsOfTheDeadLevel3 } };
            case Area.FarOasis:
                return new List<List<Area>> { new List<Area> { Area.FarOasis } };
            case Area.LostCity:
                return new List<List<Area>> { new List<Area> { Area.LostCity, Area.ValleyOfSnakes, Area.ClawViperTempleLevel1, Area.ClawViperTempleLevel2 } };
            case Area.ArcaneSanctuary:
                return new List<List<Area>> { new List<Area> { Area.ArcaneSanctuary } };
            // Act 3
            case Area.SpiderForest:
                return new List<List<Area>> { new List<Area> { Area.SpiderForest, Area.SpiderCavern } };
            case Area.GreatMarsh:
                return new List<List<Area>> { new List<Area> { Area.GreatMarsh } };
            case Area.FlayerJungle:
                return new List<List<Area>> { new List<Area> { Area.FlayerJungle, Area.FlayerDungeonLevel1, Area.FlayerDungeonLevel2, Area.FlayerDungeonLevel3 } };
            case Area.KurastBazaar:
                return new List<List<Area>> { new List<Area> { Area.KurastBazaar, Area.RuinedTemple, Area.DisusedFane } };
            // Act 4
            case Area.OuterSteppes:
                return new List<List<Area>> { new List<Area> { Area.ThePandemoniumFortress, Area.OuterSteppes, Area.PlainsOfDespair } };
            case Area.RiverOfFlame:
                return new List<List<Area>> { new List<Area> { Area.CityOfTheDamned, Area.RiverOfFlame } };
            // Act 5
            case Area.BloodyFoothills:
                return new List<List<Area>> { new List<Area> { Area.Harrogath, Area.BloodyFoothills, Area.FrigidHighlands, Area.Abaddon } };
            case Area.GlacialTrail:
                return new List<List<Area>> { new List<Area> { Area.GlacialTrail, Area.DrifterCavern } };
            case Area.CrystallinePassage:
                return new List<List<Area>> { new List<Area> { Area.CrystallinePassage, Area.FrozenRiver } };
            case Area.ArreatPlateau:
                return new List<List<Area>> { new List<Area> { Area.ArreatPlateau, Area.PitOfAcheron } };
            case Area.TheAncientsWay:
                return new List<List<Area>> { new List<Area> { Area.TheAncientsWay, Area.IcyCellar } };
        }

        return new List<List<Area>>();
    }*/
}
