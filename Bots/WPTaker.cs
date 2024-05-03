using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MapAreaStruc;

public class WPTaker
{
    Form1 Form1_0;

    public int CurrentStep = 0;
    public bool ScriptDone = false;
    public int TriedWPCount = 0;
    public bool HasThisWP = true;
    public Enums.Area DoingThisArea = 0;

    public int CurrentAct = 1;
    public int CurrentWPIndex = 1;
    public bool AdancedIndex = false;


    public void SetForm1(Form1 form1_1)
    {
        Form1_0 = form1_1;
    }

    public void ResetVars()
    {
        TriedWPCount = 0;
        CurrentStep = 0;
        ScriptDone = false;
    }

    public void DetectCurrentStep()
    {
        /*if ((Enums.Area)Form1_0.PlayerScan_0.levelNo == Enums.Area.LostCity) CurrentStep = 1;
        if ((Enums.Area)Form1_0.PlayerScan_0.levelNo == Enums.Area.ValleyOfSnakes) CurrentStep = 2;
        if ((Enums.Area)Form1_0.PlayerScan_0.levelNo == Enums.Area.ClawViperTempleLevel1) CurrentStep = 3;
        if ((Enums.Area)Form1_0.PlayerScan_0.levelNo == Enums.Area.ClawViperTempleLevel2) CurrentStep = 4;*/
    }

    public void AdvanceScriptIndex()
    {
        //advance script by 1x index
        if ((CurrentWPIndex < 8 && CurrentAct != 4)
            || (CurrentWPIndex < 2 && CurrentAct == 4))
        {
            CurrentWPIndex++;
        }
        else
        {
            CurrentWPIndex = 1;

            //Go to next Act
            if (CurrentAct < 5)
            {
                CurrentAct++;
            }
            else
            {
                Form1_0.Town_0.FastTowning = false;
                Form1_0.Town_0.UseLastTP = false;
                ScriptDone = true;
            }
        }
    }

    public void RunScript()
    {
        Form1_0.Town_0.ScriptTownAct = CurrentAct;

        if (!Form1_0.Running || !Form1_0.GameStruc_0.IsInGame())
        {
            ScriptDone = true;
            return;
        }

        if (Form1_0.Town_0.GetInTown())
        {
            Form1_0.SetGameStatus("GO TO WP");
            CurrentStep = 0;
            AdancedIndex = false;

            if (!HasThisWP)
            {
                Form1_0.Town_0.GoToWPArea(CurrentAct, CurrentWPIndex);

                TriedWPCount++;
                if (TriedWPCount >= 3)
                {
                    HasThisWP = false;
                    if (CurrentAct == 1 && CurrentWPIndex == 1) DoingThisArea = Enums.Area.ColdPlains;
                    if (CurrentAct == 1 && CurrentWPIndex == 2) DoingThisArea = Enums.Area.StonyField;
                    if (CurrentAct == 1 && CurrentWPIndex == 3) DoingThisArea = Enums.Area.DarkWood;
                    if (CurrentAct == 1 && CurrentWPIndex == 4) DoingThisArea = Enums.Area.BlackMarsh;
                    if (CurrentAct == 1 && CurrentWPIndex == 5) DoingThisArea = Enums.Area.OuterCloister;
                    if (CurrentAct == 1 && CurrentWPIndex == 6) DoingThisArea = Enums.Area.JailLevel1;
                    if (CurrentAct == 1 && CurrentWPIndex == 7) DoingThisArea = Enums.Area.InnerCloister;
                    if (CurrentAct == 1 && CurrentWPIndex == 8) DoingThisArea = Enums.Area.CatacombsLevel2;

                    if (CurrentAct == 2 && CurrentWPIndex == 1) DoingThisArea = Enums.Area.SewersLevel2Act2;
                    if (CurrentAct == 2 && CurrentWPIndex == 2) DoingThisArea = Enums.Area.DryHills;
                    if (CurrentAct == 2 && CurrentWPIndex == 3) DoingThisArea = Enums.Area.HallsOfTheDeadLevel2;
                    if (CurrentAct == 2 && CurrentWPIndex == 4) DoingThisArea = Enums.Area.FarOasis;
                    if (CurrentAct == 2 && CurrentWPIndex == 5) DoingThisArea = Enums.Area.LostCity;
                    if (CurrentAct == 2 && CurrentWPIndex == 6) DoingThisArea = Enums.Area.PalaceCellarLevel1;  //require cube, staff, ammy
                    if (CurrentAct == 2 && CurrentWPIndex == 7) DoingThisArea = Enums.Area.ArcaneSanctuary;
                    if (CurrentAct == 2 && CurrentWPIndex == 8) DoingThisArea = Enums.Area.CanyonOfTheMagi;     //require summoner defeated

                    if (CurrentAct == 3 && CurrentWPIndex == 1) DoingThisArea = Enums.Area.SpiderForest;
                    if (CurrentAct == 3 && CurrentWPIndex == 2) DoingThisArea = Enums.Area.GreatMarsh;
                    if (CurrentAct == 3 && CurrentWPIndex == 3) DoingThisArea = Enums.Area.FlayerJungle;
                    if (CurrentAct == 3 && CurrentWPIndex == 4) DoingThisArea = Enums.Area.LowerKurast;
                    if (CurrentAct == 3 && CurrentWPIndex == 5) DoingThisArea = Enums.Area.KurastBazaar;
                    if (CurrentAct == 3 && CurrentWPIndex == 6) DoingThisArea = Enums.Area.UpperKurast;
                    if (CurrentAct == 3 && CurrentWPIndex == 7) DoingThisArea = Enums.Area.Travincal;
                    if (CurrentAct == 3 && CurrentWPIndex == 8) DoingThisArea = Enums.Area.DuranceOfHateLevel2; //require kahlim flail

                    if (CurrentAct == 4 && CurrentWPIndex == 1) DoingThisArea = Enums.Area.CityOfTheDamned;
                    if (CurrentAct == 4 && CurrentWPIndex == 2) DoingThisArea = Enums.Area.RiverOfFlame;

                    if (CurrentAct == 5 && CurrentWPIndex == 1) DoingThisArea = Enums.Area.FrigidHighlands;
                    if (CurrentAct == 5 && CurrentWPIndex == 2) DoingThisArea = Enums.Area.ArreatPlateau;
                    if (CurrentAct == 5 && CurrentWPIndex == 3) DoingThisArea = Enums.Area.CrystallinePassage;
                    if (CurrentAct == 5 && CurrentWPIndex == 4) DoingThisArea = Enums.Area.HallsOfPain;         //Require Anya saved
                    if (CurrentAct == 5 && CurrentWPIndex == 5) DoingThisArea = Enums.Area.GlacialTrail;
                    if (CurrentAct == 5 && CurrentWPIndex == 6) DoingThisArea = Enums.Area.FrozenTundra;
                    if (CurrentAct == 5 && CurrentWPIndex == 7) DoingThisArea = Enums.Area.TheAncientsWay;
                    if (CurrentAct == 5 && CurrentWPIndex == 8) DoingThisArea = Enums.Area.TheWorldStoneKeepLevel2;

                    TriedWPCount = 0;
                }
            }
            else
            {
                if (HasThisWP)
                {
                    if (!AdancedIndex)
                    {
                        AdvanceScriptIndex();
                        AdancedIndex = true;
                    }

                    Position ThisFinalPosition = Form1_0.MapAreaStruc_0.GetPositionOfObject("object", "WaypointPortal", (int)DoingThisArea, new List<int>() { });
                    if (ThisFinalPosition.X != 0 && ThisFinalPosition.Y != 0)
                    {
                        Position itemScreenPos = Form1_0.GameStruc_0.World2Screen(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, ThisFinalPosition.X, ThisFinalPosition.Y);

                        Form1_0.KeyMouse_0.MouseClicc_RealPos(itemScreenPos.X, itemScreenPos.Y - 15);
                        if (Form1_0.UIScan_0.WaitTilUIOpen("waypointMenu"))
                        {
                            Form1_0.Town_0.SelectTownWP();
                        }
                    }
                    else
                    {
                        Form1_0.method_1("No WP found nearby", Color.OrangeRed);
                    }
                }

                if (CurrentWPIndex == 1)
                {
                    //start from town
                    if (CurrentAct == 1) Form1_0.PathFinding_0.MoveToNextArea(Enums.Area.BloodMoor);
                    if (CurrentAct == 2) Form1_0.PathFinding_0.MoveToExit(Enums.Area.SewersLevel1Act2);
                    if (CurrentAct == 3) Form1_0.PathFinding_0.MoveToNextArea(Enums.Area.SpiderForest);
                    if (CurrentAct == 4) Form1_0.PathFinding_0.MoveToNextArea(Enums.Area.OuterSteppes);
                    if (CurrentAct == 5) Form1_0.PathFinding_0.MoveToNextArea(Enums.Area.BloodyFoothills);
                }
                else
                {
                    if (CurrentAct == 2 && CurrentWPIndex == 2) Form1_0.PathFinding_0.MoveToNextArea(Enums.Area.RockyWaste);
                    else if (CurrentAct == 2 && CurrentWPIndex == 6) Form1_0.PathFinding_0.MoveToExit(Enums.Area.HaremLevel1);
                    else if (CurrentAct == 5 && CurrentWPIndex == 5) Form1_0.PathFinding_0.MoveToExit(Enums.Area.NihlathaksTemple);
                    else
                    {
                        //start from the previous wp area
                        Form1_0.Town_0.GoToWPArea(CurrentAct, CurrentWPIndex - 1);
                    }
                }
            }
        }
        else
        {
            if (CurrentStep == 0)
            {
                Form1_0.SetGameStatus("DOING WP TAKER FOR: " + DoingThisArea);
                Form1_0.Battle_0.CastDefense();
                Form1_0.WaitDelay(15);

                //Act1
                if (CurrentAct == 1 && CurrentWPIndex == 1)
                {
                    Form1_0.PathFinding_0.MoveToNextArea(Enums.Area.BloodMoor);
                    Form1_0.PathFinding_0.MoveToNextArea(Enums.Area.ColdPlains);
                }
                if (CurrentAct == 1 && CurrentWPIndex == 2) Form1_0.PathFinding_0.MoveToNextArea(Enums.Area.StonyField);
                if (CurrentAct == 1 && CurrentWPIndex == 3)
                {
                    Form1_0.PathFinding_0.MoveToExit(Enums.Area.UndergroundPassageLevel1);
                    Form1_0.PathFinding_0.MoveToExit(Enums.Area.DarkWood);
                }
                if (CurrentAct == 1 && CurrentWPIndex == 4) Form1_0.PathFinding_0.MoveToNextArea(Enums.Area.BlackMarsh);
                if (CurrentAct == 1 && CurrentWPIndex == 5)
                {
                    Form1_0.PathFinding_0.MoveToNextArea(Enums.Area.TamoeHighland);
                    Form1_0.PathFinding_0.MoveToNextArea(Enums.Area.MonasteryGate);
                    Form1_0.PathFinding_0.MoveToNextArea(Enums.Area.OuterCloister);
                }
                if (CurrentAct == 1 && CurrentWPIndex == 6) Form1_0.PathFinding_0.MoveToExit(Enums.Area.JailLevel1);
                if (CurrentAct == 1 && CurrentWPIndex == 7)
                {
                    Form1_0.PathFinding_0.MoveToExit(Enums.Area.JailLevel2);
                    Form1_0.PathFinding_0.MoveToExit(Enums.Area.JailLevel3);
                    Form1_0.PathFinding_0.MoveToExit(Enums.Area.InnerCloister);
                }
                if (CurrentAct == 1 && CurrentWPIndex == 8)
                {
                    Form1_0.PathFinding_0.MoveToNextArea(Enums.Area.Cathedral);
                    Form1_0.PathFinding_0.MoveToExit(Enums.Area.CatacombsLevel1);
                    Form1_0.PathFinding_0.MoveToExit(Enums.Area.CatacombsLevel2);
                }

                //Act2
                if (CurrentAct == 2 && CurrentWPIndex == 1) Form1_0.PathFinding_0.MoveToExit(Enums.Area.SewersLevel2Act2);
                if (CurrentAct == 2 && CurrentWPIndex == 2) Form1_0.PathFinding_0.MoveToNextArea(Enums.Area.DryHills);
                if (CurrentAct == 2 && CurrentWPIndex == 3)
                {
                    Form1_0.PathFinding_0.MoveToExit(Enums.Area.HallsOfTheDeadLevel1);
                    Form1_0.PathFinding_0.MoveToExit(Enums.Area.HallsOfTheDeadLevel2);
                }
                if (CurrentAct == 2 && CurrentWPIndex == 4) Form1_0.PathFinding_0.MoveToNextArea(Enums.Area.FarOasis);
                if (CurrentAct == 2 && CurrentWPIndex == 5) Form1_0.PathFinding_0.MoveToNextArea(Enums.Area.LostCity);
                if (CurrentAct == 2 && CurrentWPIndex == 6)
                {
                    Form1_0.PathFinding_0.MoveToExit(Enums.Area.HaremLevel2);
                    Form1_0.PathFinding_0.MoveToExit(Enums.Area.PalaceCellarLevel1);
                }
                if (CurrentAct == 2 && CurrentWPIndex == 7)
                {
                    Form1_0.PathFinding_0.MoveToExit(Enums.Area.PalaceCellarLevel2);
                    Form1_0.PathFinding_0.MoveToExit(Enums.Area.PalaceCellarLevel3);
                    Form1_0.PathFinding_0.MoveToExit(Enums.Area.ArcaneSanctuary);
                }
                if (CurrentAct == 2 && CurrentWPIndex == 8) Form1_0.PathFinding_0.MoveToExit(Enums.Area.CanyonOfTheMagi);

                //Act3
                //if (CurrentAct == 3 && CurrentWPIndex == 1) Form1_0.PathFinding_0.MoveToNextArea(Enums.Area.SpiderForest);
                if (CurrentAct == 3 && CurrentWPIndex == 2) Form1_0.PathFinding_0.MoveToNextArea(Enums.Area.GreatMarsh);
                if (CurrentAct == 3 && CurrentWPIndex == 3) Form1_0.PathFinding_0.MoveToNextArea(Enums.Area.FlayerJungle);
                if (CurrentAct == 3 && CurrentWPIndex == 4) Form1_0.PathFinding_0.MoveToNextArea(Enums.Area.LowerKurast);
                if (CurrentAct == 3 && CurrentWPIndex == 5) Form1_0.PathFinding_0.MoveToNextArea(Enums.Area.KurastBazaar);
                if (CurrentAct == 3 && CurrentWPIndex == 6) Form1_0.PathFinding_0.MoveToNextArea(Enums.Area.UpperKurast);
                if (CurrentAct == 3 && CurrentWPIndex == 7)
                {
                    Form1_0.PathFinding_0.MoveToNextArea(Enums.Area.KurastCauseway);
                    Form1_0.PathFinding_0.MoveToNextArea(Enums.Area.Travincal);
                }
                if (CurrentAct == 3 && CurrentWPIndex == 8)
                {
                    Form1_0.PathFinding_0.MoveToExit(Enums.Area.DuranceOfHateLevel1);
                    Form1_0.PathFinding_0.MoveToExit(Enums.Area.DuranceOfHateLevel2);
                }

                //Act4
                if (CurrentAct == 4 && CurrentWPIndex == 1)
                {
                    Form1_0.PathFinding_0.MoveToNextArea(Enums.Area.PlainsOfDespair);
                    Form1_0.PathFinding_0.MoveToNextArea(Enums.Area.CityOfTheDamned);
                }
                if (CurrentAct == 4 && CurrentWPIndex == 2) Form1_0.PathFinding_0.MoveToExit(Enums.Area.RiverOfFlame);

                //Act5
                if (CurrentAct == 5 && CurrentWPIndex == 1) Form1_0.PathFinding_0.MoveToNextArea(Enums.Area.FrigidHighlands);
                if (CurrentAct == 5 && CurrentWPIndex == 2) Form1_0.PathFinding_0.MoveToNextArea(Enums.Area.ArreatPlateau);
                if (CurrentAct == 5 && CurrentWPIndex == 3) Form1_0.PathFinding_0.MoveToExit(Enums.Area.CrystallinePassage);
                if (CurrentAct == 5 && CurrentWPIndex == 4) Form1_0.PathFinding_0.MoveToExit(Enums.Area.HallsOfPain);
                if (CurrentAct == 5 && CurrentWPIndex == 5) Form1_0.PathFinding_0.MoveToExit(Enums.Area.GlacialTrail);
                if (CurrentAct == 5 && CurrentWPIndex == 6) Form1_0.PathFinding_0.MoveToExit(Enums.Area.FrozenTundra);
                if (CurrentAct == 5 && CurrentWPIndex == 7) Form1_0.PathFinding_0.MoveToExit(Enums.Area.TheAncientsWay);
                if (CurrentAct == 5 && CurrentWPIndex == 8)
                {
                    Form1_0.PathFinding_0.MoveToExit(Enums.Area.ArreatSummit);
                    Form1_0.PathFinding_0.MoveToExit(Enums.Area.TheWorldStoneKeepLevel1);
                    Form1_0.PathFinding_0.MoveToExit(Enums.Area.TheWorldStoneKeepLevel2);
                }

                CurrentStep++;
            }

            if (CurrentStep == 1)
            {
                Form1_0.PathFinding_0.MoveToObject("WaypointPortal");

                Position ThisFinalPosition = Form1_0.MapAreaStruc_0.GetPositionOfObject("object", "WaypointPortal", (int)DoingThisArea, new List<int>() { });
                if (ThisFinalPosition.X != 0 && ThisFinalPosition.Y != 0)
                {
                    Position itemScreenPos = Form1_0.GameStruc_0.World2Screen(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, ThisFinalPosition.X, ThisFinalPosition.Y);

                    Form1_0.KeyMouse_0.MouseClicc_RealPos(itemScreenPos.X, itemScreenPos.Y - 15);
                    if (Form1_0.UIScan_0.WaitTilUIOpen("waypointMenu"))
                    {
                        Form1_0.Town_0.SelectTownWP();
                        CurrentStep++;
                    }
                }
                else
                {
                    Form1_0.method_1("No WP found nearby", Color.OrangeRed);
                }
                CurrentStep++;
            }

            if (CurrentStep == 2)
            {
                if (!Form1_0.Town_0.GetInTown())
                {
                    CurrentStep--;
                    return;
                }

                AdvanceScriptIndex();
            }
        }
    }
}
