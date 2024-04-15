using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app
{
    public class Eldritch
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

        public void DetectCurrentStep()
        {
            if ((Enums.Area)Form1_0.PlayerScan_0.levelNo == Enums.Area.FrigidHighlands) CurrentStep = 1;
        }

        public void RunScript()
        {
            Form1_0.Town_0.ScriptTownAct = 1; //set to town act 5 when running this script

            if (Form1_0.Town_0.GetInTown())
            {
                Form1_0.SetGameStatus("GO TO WP");
                CurrentStep = 0;

                Form1_0.Town_0.GoToWPArea(5, 1);
            }
            else
            {
                if (CurrentStep == 0)
                {
                    Form1_0.SetGameStatus("DOING ELDRITCH");
                    Form1_0.Battle_0.CastDefense();
                    Form1_0.WaitDelay(15);

                    if ((Enums.Area) Form1_0.PlayerScan_0.levelNo == Enums.Area.FrigidHighlands)
                    {
                        CurrentStep++;
                    }
                    else
                    {
                        DetectCurrentStep();
                        if (CurrentStep == 0) Form1_0.Town_0.GoToTown();
                    }
                }

                if (CurrentStep == 1)
                {

                    Form1_0.PathFinding_0.MoveToNPC("Eldritch");
                    CurrentStep++;
                }

                if (CurrentStep == 2)
                {
                    Form1_0.Potions_0.CanUseSkillForRegen = false;
                    Form1_0.SetGameStatus("KILLING ELDRITCH");
                    if (Form1_0.MobsStruc_0.GetMobs("getSuperUniqueName", "Eldritch", false, 200, new List<long>()))
                    {
                        if (Form1_0.MobsStruc_0.MobsHP > 0)
                        {
                            Form1_0.Battle_0.RunBattleScriptOnThisMob("getSuperUniqueName", "Eldritch");
                        }
                        else
                        {
                            if (!Form1_0.ItemsStruc_0.GetItems(true)) Form1_0.WaitDelay(5);
                            if (!Form1_0.ItemsStruc_0.GetItems(true)) Form1_0.WaitDelay(5);
                            if (!Form1_0.ItemsStruc_0.GetItems(true)) Form1_0.WaitDelay(5);
                            if (!Form1_0.ItemsStruc_0.GetItems(true)) Form1_0.WaitDelay(5);
                            if (!Form1_0.ItemsStruc_0.GetItems(true)) Form1_0.WaitDelay(5);
                            if (!Form1_0.ItemsStruc_0.GetItems(true)) Form1_0.WaitDelay(5);
                            if (!Form1_0.ItemsStruc_0.GetItems(true)) Form1_0.WaitDelay(5);
                            if (!Form1_0.ItemsStruc_0.GetItems(true)) Form1_0.WaitDelay(5);
                            if (!Form1_0.ItemsStruc_0.GetItems(true)) Form1_0.WaitDelay(5);
                            if (!Form1_0.ItemsStruc_0.GetItems(true)) Form1_0.WaitDelay(5);
                            Form1_0.ItemsStruc_0.GrabAllItemsForGold();
                            Form1_0.Potions_0.CanUseSkillForRegen = true;

                            Form1_0.Town_0.UseLastTP = false;
                            ScriptDone = true;
                            return;
                            //Form1_0.LeaveGame(true);
                        }
                    }
                    else
                    {
                        Form1_0.method_1("Eldritch not detected!", Color.Red);

                        //baal not detected...
                        Form1_0.ItemsStruc_0.GetItems(true);
                        if (Form1_0.MobsStruc_0.GetMobs("getSuperUniqueName", "Eldritch", false, 200, new List<long>())) return; //redetect baal?
                        Form1_0.ItemsStruc_0.GrabAllItemsForGold();
                        if (Form1_0.MobsStruc_0.GetMobs("getSuperUniqueName", "Eldritch", false, 200, new List<long>())) return; //redetect baal?
                        Form1_0.Potions_0.CanUseSkillForRegen = true;

                        Form1_0.Town_0.UseLastTP = false;
                        ScriptDone = true;
                        return;
                        //Form1_0.LeaveGame(true);
                    }
                }
            }
        }
    }
}
