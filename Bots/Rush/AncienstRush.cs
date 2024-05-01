using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MapAreaStruc;

public class AncientsRush
{
    Form1 Form1_0;

    public int CurrentStep = 0;
    public bool ScriptDone = false;
    public Position AltarPos = new Position { X = 0, Y = 0 };
    public bool KilledAnyMember = false;

    public List<long> IgnoredMembers = new List<long>();

    public void SetForm1(Form1 form1_1)
    {
        Form1_0 = form1_1;
    }

    public void ResetVars()
    {
        CurrentStep = 0;
        ScriptDone = false;
        IgnoredMembers = new List<long>();
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

            Form1_0.Town_0.GoToWPArea(5, 7);
        }
        else
        {
            if (CurrentStep == 0)
            {
                Form1_0.SetGameStatus("DOING ANCIENTS");
                //Form1_0.Battle_0.CastDefense();
                //Form1_0.WaitDelay(15);

                if ((Enums.Area)Form1_0.PlayerScan_0.levelNo == Enums.Area.TheAncientsWay)
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
                Form1_0.PathFinding_0.MoveToExit(Enums.Area.ArreatSummit, 10);
                CurrentStep++;
            }

            if (CurrentStep == 2)
            {
                Form1_0.SetGameStatus("Ancients clearing");

                if (!Form1_0.Battle_0.DoBattleScript(15))
                {
                    Form1_0.PathFinding_0.MoveToExit(Enums.Area.ArreatSummit, 10);

                    Form1_0.Town_0.TPSpawned = false;
                    CurrentStep++;
                }
            }

            if (CurrentStep == 3)
            {
                Form1_0.SetGameStatus("Ancients waiting on leecher");

                if (!Form1_0.Town_0.TPSpawned) Form1_0.Town_0.SpawnTP();

                Form1_0.Battle_0.DoBattleScript(15);

                //get leecher infos
                Form1_0.PlayerScan_0.GetLeechPositions();

                if (Form1_0.PlayerScan_0.LeechlevelNo == (int)Enums.Area.TheAncientsWay
                    || Form1_0.PlayerScan_0.LeechlevelNo == (int)Enums.Area.ArreatSummit)
                {
                    CurrentStep++;
                }
            }

            if (CurrentStep == 4)
            {
                if (Form1_0.PlayerScan_0.levelNo == (int)Enums.Area.ArreatSummit)
                {
                    CurrentStep++;
                }
                else
                {
                    Form1_0.PathFinding_0.MoveToExit(Enums.Area.ArreatSummit);
                    CurrentStep++;
                }
            }

            if (CurrentStep == 5)
            {
                if ((Enums.Area)Form1_0.PlayerScan_0.levelNo == Enums.Area.TheAncientsWay)
                {
                    CurrentStep--;
                    return;
                }

                AltarPos = Form1_0.MapAreaStruc_0.GetPositionOfObject("object", "AncientsAltar", (int)Enums.Area.ArreatSummit, new List<int>());
                if (AltarPos.X != 0 && AltarPos.Y != 0)
                {
                    Form1_0.PathFinding_0.MoveToThisPos(AltarPos);

                    //repeat clic on altar
                    int tryyy = 0;
                    while (tryyy <= 25)
                    {
                        Position itemScreenPos = Form1_0.GameStruc_0.World2Screen(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, AltarPos.X, AltarPos.Y);

                        Form1_0.KeyMouse_0.MouseClicc_RealPos(itemScreenPos.X, itemScreenPos.Y);
                        Form1_0.PlayerScan_0.GetPositions();
                        tryyy++;
                    }

                    CurrentStep++;
                }
                else
                {
                    Form1_0.method_1("Ancients Altar location not detected!", Color.Red);
                    Form1_0.Town_0.FastTowning = false;
                    ScriptDone = true;
                    Form1_0.Town_0.UseLastTP = false;
                    return;
                }
            }

            if (CurrentStep == 6)
            {
                Form1_0.Potions_0.CanUseSkillForRegen = false;
                Form1_0.SetGameStatus("KILLING ANCIENTS");
                Form1_0.MobsStruc_0.DetectThisMob("getSuperUniqueName", "Ancient Barbarian 1", false, 200, new List<long>());
                if (Form1_0.MobsStruc_0.GetMobs("getSuperUniqueName", "Ancient Barbarian 1", false, 200, IgnoredMembers))
                {
                    if (Form1_0.MobsStruc_0.MobsHP > 0)
                    {
                        Form1_0.Battle_0.RunBattleScriptOnThisMob("getSuperUniqueName", "Ancient Barbarian 1", IgnoredMembers);
                    }
                    else
                    {
                        KilledAnyMember = true;
                        IgnoredMembers.Add(Form1_0.MobsStruc_0.MobsPointerLocation);


                        if (Form1_0.MobsStruc_0.GetMobs("getSuperUniqueName", "Ancient Barbarian 2", false, 200, IgnoredMembers))
                        {
                            if (Form1_0.MobsStruc_0.MobsHP > 0)
                            {
                                Form1_0.Battle_0.RunBattleScriptOnThisMob("getSuperUniqueName", "Ancient Barbarian 2", IgnoredMembers);
                            }
                            else
                            {
                                IgnoredMembers.Add(Form1_0.MobsStruc_0.MobsPointerLocation);


                                if (Form1_0.MobsStruc_0.GetMobs("getSuperUniqueName", "Ancient Barbarian 3", false, 200, IgnoredMembers))
                                {
                                    if (Form1_0.MobsStruc_0.MobsHP > 0)
                                    {
                                        Form1_0.Battle_0.RunBattleScriptOnThisMob("getSuperUniqueName", "Ancient Barbarian 3", IgnoredMembers);
                                    }
                                    else
                                    {
                                        IgnoredMembers.Add(Form1_0.MobsStruc_0.MobsPointerLocation);

                                        //Done all killed!
                                        Form1_0.Potions_0.CanUseSkillForRegen = true;
                                        Form1_0.Town_0.UseLastTP = false;
                                        Form1_0.Town_0.FastTowning = false;
                                        ScriptDone = true;
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (!KilledAnyMember) Form1_0.method_1("Ancients Members not detected!", Color.Red);

                    Form1_0.Potions_0.CanUseSkillForRegen = true;
                    Form1_0.Town_0.UseLastTP = false;
                    Form1_0.Town_0.FastTowning = false;
                    ScriptDone = true;
                }
            }
        }
    }
}
