using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MapAreaStruc;

public class Travincal
{
    Form1 Form1_0;

    public int CurrentStep = 0;
    public bool ScriptDone = false;
    public Position OrbPos = new Position { X = 0, Y = 0 };
    public List<long> IgnoredCouncilMembers = new List<long>();
    public bool KilledAnyMember = false;


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

        if (!Form1_0.Running || !Form1_0.GameStruc_0.IsInGame())
        {
            ScriptDone = true;
            return;
        }

        if (Form1_0.Town_0.GetInTown())
        {
            Form1_0.SetGameStatus("GO TO WP");
            CurrentStep = 0;

            Form1_0.Town_0.GoToWPArea(3, 7);
        }
        else
        {
            if (CurrentStep == 0)
            {
                Form1_0.SetGameStatus("DOING TRAVINCAL");
                Form1_0.Battle_0.CastDefense();
                Form1_0.WaitDelay(15);

                if ((Enums.Area)Form1_0.PlayerScan_0.levelNo == Enums.Area.Travincal)
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
                OrbPos = Form1_0.MapAreaStruc_0.GetPositionOfObject("object", "CompellingOrb", (int)Enums.Area.Travincal, new List<int>());
                if (OrbPos.X != 0 && OrbPos.Y != 0)
                {
                    Form1_0.PathFinding_0.MoveToThisPos(OrbPos);

                    CurrentStep++;
                }
                else
                {
                    Form1_0.method_1("Kahlim Orb location not detected!", Color.Red);
                    Form1_0.Town_0.FastTowning = false;
                    Form1_0.Town_0.UseLastTP = false;
                    ScriptDone = true;
                    return;
                }
            }

            if (CurrentStep == 2)
            {
                Form1_0.Potions_0.CanUseSkillForRegen = false;
                Form1_0.SetGameStatus("KILLING TRAVINCAL COUNCIL");
                Form1_0.MobsStruc_0.DetectThisMob("getSuperUniqueName", "Council Member", false, 200, new List<long>());
                if (Form1_0.MobsStruc_0.GetMobs("getSuperUniqueName", "Council Member", false, 200, IgnoredCouncilMembers))
                {
                    if (Form1_0.MobsStruc_0.MobsHP > 0)
                    {
                        Form1_0.Battle_0.RunBattleScriptOnThisMob("getSuperUniqueName", "Council Member", IgnoredCouncilMembers);
                    }
                    else
                    {
                        KilledAnyMember = true;
                        IgnoredCouncilMembers.Add(Form1_0.MobsStruc_0.MobsPointerLocation);
                    }
                }
                else
                {
                    if (!KilledAnyMember)
                    {
                        Form1_0.method_1("Council Members not detected!", Color.Red);

                        //baal not detected...
                        Form1_0.ItemsStruc_0.GetItems(true);
                        if (Form1_0.MobsStruc_0.GetMobs("getSuperUniqueName", "Council Member", false, 200, new List<long>())) return; //redetect baal?
                        Form1_0.ItemsStruc_0.GrabAllItemsForGold();
                        if (Form1_0.MobsStruc_0.GetMobs("getSuperUniqueName", "Council Member", false, 200, new List<long>())) return; //redetect baal?
                        Form1_0.Potions_0.CanUseSkillForRegen = true;

                        if (Form1_0.Battle_0.EndBossBattle()) ScriptDone = true;
                        return;
                    }
                    else
                    {
                        if (Form1_0.Battle_0.EndBossBattle()) ScriptDone = true;
                        return;
                    }

                }
            }
        }
    }
}
