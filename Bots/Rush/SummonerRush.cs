using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MapAreaStruc;

public class SummonerRush
{
    Form1 Form1_0;

    public int CurrentStep = 0;
    public bool ScriptDone = false;
    public Position WaitPos = new Position { X = 0, Y = 0 };


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
        Form1_0.Town_0.ScriptTownAct = 2; //set to town act 5 when running this script

        if (!Form1_0.Running || !Form1_0.GameStruc_0.IsInGame())
        {
            ScriptDone = true;
            return;
        }

        if (Form1_0.Town_0.GetInTown())
        {
            Form1_0.SetGameStatus("GO TO WP");
            CurrentStep = 0;

            Form1_0.Town_0.GoToWPArea(2, 7);
        }
        else
        {
            if (CurrentStep == 0)
            {
                Form1_0.SetGameStatus("DOING SUMMONER");
                //Form1_0.Battle_0.CastDefense();
                //Form1_0.WaitDelay(15);

                if ((Enums.Area)Form1_0.PlayerScan_0.levelNo == Enums.Area.ArcaneSanctuary)
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
                Form1_0.PathFinding_0.MoveToNPC("Summoner", 25);

                Form1_0.PlayerScan_0.GetPositions();
                WaitPos.X = Form1_0.PlayerScan_0.xPos;
                WaitPos.Y = Form1_0.PlayerScan_0.yPos;
                CurrentStep++;
            }

            if (CurrentStep == 2)
            {
                if (!Form1_0.Battle_0.DoBattleScript(25))
                {
                    Form1_0.PathFinding_0.MoveToThisPos(WaitPos);

                    Form1_0.Town_0.TPSpawned = false;
                    CurrentStep++;
                }

                Form1_0.Town_0.TPSpawned = false;
                CurrentStep++;
            }

            if (CurrentStep == 3)
            {
                Form1_0.SetGameStatus("Summoner waiting on leecher");

                if (!Form1_0.Town_0.TPSpawned) Form1_0.Town_0.SpawnTP();

                Form1_0.Battle_0.DoBattleScript(25);

                //get leecher infos
                Form1_0.PlayerScan_0.GetLeechPositions();

                if (Form1_0.PlayerScan_0.LeechlevelNo == (int)Enums.Area.ArcaneSanctuary)
                {
                    CurrentStep++;
                }
            }

            if (CurrentStep == 4)
            {
                Form1_0.Potions_0.CanUseSkillForRegen = false;
                Form1_0.SetGameStatus("KILLING SUMMONER");
                Form1_0.MobsStruc_0.DetectThisMob("getBossName", "Summoner", false, 200, new List<long>());
                if (Form1_0.MobsStruc_0.GetMobs("getBossName", "Summoner", false, 200, new List<long>()))
                {
                    if (Form1_0.MobsStruc_0.MobsHP > 0)
                    {
                        Form1_0.Battle_0.RunBattleScriptOnThisMob("getBossName", "Summoner", new List<long>());
                    }
                    else
                    {
                        Form1_0.Potions_0.CanUseSkillForRegen = true;
                        CurrentStep++;

                        /*Form1_0.ItemsStruc_0.GetItems(true);
                        Form1_0.ItemsStruc_0.GetItems(true);
                        Form1_0.ItemsStruc_0.GetItems(true);
                        Form1_0.ItemsStruc_0.GetItems(true);
                        Form1_0.ItemsStruc_0.GetItems(true);
                        Form1_0.ItemsStruc_0.GetItems(true);
                        Form1_0.ItemsStruc_0.GetItems(true);
                        Form1_0.ItemsStruc_0.GetItems(true);
                        Form1_0.ItemsStruc_0.GetItems(true);
                        Form1_0.ItemsStruc_0.GetItems(true);
                        Form1_0.ItemsStruc_0.GrabAllItemsForGold();
                        Form1_0.Potions_0.CanUseSkillForRegen = true;

                        Form1_0.Town_0.FastTowning = false;
                        ScriptDone = true;
                        return;
                        //Form1_0.LeaveGame(true);*/
                    }
                }
                else
                {
                    Form1_0.method_1("Summoner not detected!", Color.Red);

                    //baal not detected...
                    Form1_0.ItemsStruc_0.GetItems(true);
                    if (Form1_0.MobsStruc_0.GetMobs("getBossName", "Summoner", false, 200, new List<long>())) return; //redetect baal?
                    Form1_0.ItemsStruc_0.GrabAllItemsForGold();
                    if (Form1_0.MobsStruc_0.GetMobs("getBossName", "Summoner", false, 200, new List<long>())) return; //redetect baal?
                    Form1_0.Potions_0.CanUseSkillForRegen = true;

                    Form1_0.Town_0.FastTowning = false;
                    Form1_0.Town_0.UseLastTP = false;
                    ScriptDone = true;
                    return;
                    //Form1_0.LeaveGame(true);
                }
            }

            if (CurrentStep == 5)
            {
                Form1_0.SetGameStatus("Summoner waiting on leecher #2");

                Form1_0.Battle_0.DoBattleScript(50);

                //get leecher infos
                Form1_0.PlayerScan_0.GetLeechPositions();

                if (Form1_0.PlayerScan_0.LeechlevelNo != (int)Enums.Area.ArcaneSanctuary)
                {
                    Form1_0.Town_0.FastTowning = false;
                    Form1_0.Town_0.UseLastTP = false;
                    ScriptDone = true;
                    return;
                }
            }
        }
    }
}
