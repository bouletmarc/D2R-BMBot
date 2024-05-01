using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MapAreaStruc;

public class TristramRush
{
    Form1 Form1_0;

    public int CurrentStep = 0;
    public bool ScriptDone = false;
    public Position TristramPos = new Position { X = 0, Y = 0 };


    public void SetForm1(Form1 form1_1)
    {
        Form1_0 = form1_1;
    }

    public void ResetVars()
    {
        CurrentStep = 0;
        ScriptDone = false;
    }

    public void DetectCurrentStep()
    {
        if ((Enums.Area)Form1_0.PlayerScan_0.levelNo == Enums.Area.StonyField) CurrentStep = 1;
        if ((Enums.Area)Form1_0.PlayerScan_0.levelNo == Enums.Area.Tristram) CurrentStep = 5;
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

            Form1_0.Town_0.GoToWPArea(1, 2);
        }
        else
        {
            if (CurrentStep == 0)
            {
                Form1_0.SetGameStatus("DOING TRISTRAM");
                //Form1_0.Battle_0.CastDefense();
                //Form1_0.WaitDelay(15);

                if ((Enums.Area)Form1_0.PlayerScan_0.levelNo == Enums.Area.StonyField)
                {
                    Form1_0.Town_0.SpawnTP();
                    Form1_0.WaitDelay(15);
                    Form1_0.Battle_0.CastDefense();
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
                Form1_0.SetGameStatus("Tristram clearing stones");
                if (!Form1_0.Battle_0.DoBattleScript(25))
                {
                    Position ThisTPPos = new Position { X = TristramPos.X - 10, Y = TristramPos.Y + 5 };
                    Form1_0.PathFinding_0.MoveToThisPos(TristramPos);

                    Form1_0.Town_0.TPSpawned = false;
                    CurrentStep++;
                }
            }

            if (CurrentStep == 3)
            {
                Form1_0.SetGameStatus("Tristram waiting on leecher");

                if (!Form1_0.Town_0.TPSpawned) Form1_0.Town_0.SpawnTP();

                Form1_0.Battle_0.DoBattleScript(25);

                //get leecher infos
                Form1_0.PlayerScan_0.GetLeechPositions();

                if (Form1_0.PlayerScan_0.LeechlevelNo == (int)Enums.Area.StonyField)
                {
                    CurrentStep++;
                }
            }

            if (CurrentStep == 4)
            {
                Form1_0.SetGameStatus("Tristram waiting for Tristram portal");

                if (Form1_0.ObjectsStruc_0.GetObjects("PermanentTownPortal", true, new List<uint>(), 60))
                {
                    Form1_0.Mover_0.MoveToLocation(Form1_0.ObjectsStruc_0.itemx, Form1_0.ObjectsStruc_0.itemy);

                    Position itemScreenPos = Form1_0.GameStruc_0.World2Screen(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, Form1_0.ObjectsStruc_0.itemx, Form1_0.ObjectsStruc_0.itemy);

                    Form1_0.KeyMouse_0.MouseClicc_RealPos(itemScreenPos.X, itemScreenPos.Y - 15);
                    Form1_0.WaitDelay(100);
                }

                if (Form1_0.PlayerScan_0.levelNo == (int)Enums.Area.Tristram)
                {
                    CurrentStep++;
                }
            }

            if (CurrentStep == 5)
            {
                if ((Enums.Area)Form1_0.PlayerScan_0.levelNo == Enums.Area.StonyField)
                {
                    CurrentStep--;
                    return;
                }

                Form1_0.SetGameStatus("Doing Tristram");

                if (Form1_0.ObjectsStruc_0.GetObjects("CainGibbet", true, new List<uint>()))
                {
                    if (Form1_0.Mover_0.MoveToLocation(Form1_0.ObjectsStruc_0.itemx, Form1_0.ObjectsStruc_0.itemy))
                    {
                        //repeat clic on tree
                        int tryyy = 0;
                        while (tryyy <= 15)
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

            if (CurrentStep == 6)
            {
                Form1_0.SetGameStatus("Clearing Tristram");

                if (!Form1_0.Battle_0.DoBattleScript(25))
                {
                    Form1_0.Town_0.FastTowning = false;
                    Form1_0.Town_0.UseLastTP = false;
                    ScriptDone = true;
                }
            }
        }
    }
}
