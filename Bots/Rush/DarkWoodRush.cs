using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MapAreaStruc;

public class DarkWoodRush
{
    Form1 Form1_0;

    public int CurrentStep = 0;
    public bool ScriptDone = false;
    public Position InifussTree = new Position { X = 0, Y = 0 };


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

            Form1_0.Town_0.GoToWPArea(1, 3);
        }
        else
        {
            if (CurrentStep == 0)
            {
                Form1_0.SetGameStatus("DOING DARKWOOD");
                //Form1_0.Battle_0.CastDefense();
                //Form1_0.WaitDelay(15);

                if ((Enums.Area)Form1_0.PlayerScan_0.levelNo == Enums.Area.DarkWood)
                {
                    Form1_0.Town_0.SpawnTP();
                    Form1_0.WaitDelay(15);
                    Form1_0.Battle_0.CastDefense();
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
                InifussTree = Form1_0.MapAreaStruc_0.GetPositionOfObject("object", "InifussTree", (int)Enums.Area.DarkWood, new List<int>());
                if (InifussTree.X != 0 && InifussTree.Y != 0)
                {
                    Form1_0.PathFinding_0.MoveToThisPos(InifussTree);

                    //repeat clic on tree
                    int tryyy = 0;
                    while (tryyy <= 25)
                    {
                        Position itemScreenPos = Form1_0.GameStruc_0.World2Screen(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, InifussTree.X, InifussTree.Y);

                        Form1_0.KeyMouse_0.MouseClicc_RealPos(itemScreenPos.X, itemScreenPos.Y);
                        Form1_0.PlayerScan_0.GetPositions();
                        tryyy++;
                    }

                    CurrentStep++;
                }
                else
                {
                    Form1_0.method_1("Inifuss Tree location not detected!", Color.Red);
                    Form1_0.Town_0.UseLastTP = false;
                    Form1_0.Town_0.FastTowning = false;
                    ScriptDone = true;
                    return;
                }
            }

            if (CurrentStep == 2)
            {
                if (!Form1_0.Battle_0.DoBattleScript(25))
                {
                    Position ThisTPPos = new Position { X = InifussTree.X - 10, Y = InifussTree.Y + 5 };
                    Form1_0.PathFinding_0.MoveToThisPos(ThisTPPos);

                    Form1_0.Town_0.TPSpawned = false;

                    CurrentStep++;
                }
            }

            if (CurrentStep == 3)
            {
                Form1_0.SetGameStatus("DarkWood waiting on leecher");

                if (!Form1_0.Town_0.TPSpawned) Form1_0.Town_0.SpawnTP();

                Form1_0.Battle_0.DoBattleScript(25);

                //get leecher infos
                Form1_0.PlayerScan_0.GetLeechPositions();

                if (Form1_0.PlayerScan_0.LeechlevelNo == (int)Enums.Area.DarkWood)
                {
                    CurrentStep++;
                }
            }

            if (CurrentStep == 4)
            {
                Form1_0.SetGameStatus("DarkWood waiting on leecher #2");

                Form1_0.Battle_0.DoBattleScript(25);

                //get leecher infos
                Form1_0.PlayerScan_0.GetLeechPositions();

                if (Form1_0.PlayerScan_0.LeechlevelNo == (int)Enums.Area.RogueEncampment)
                {
                    Form1_0.Town_0.UseLastTP = false;
                    Form1_0.Town_0.FastTowning = false;
                    ScriptDone = true;
                }
            }
        }
    }
}
