using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using static app.MapAreaStruc;

namespace app
{
    public class Battle
    {
        Form1 Form1_0;

        public int AreaX = 0;
        public int AreaY = 0;
        public bool ClearingArea = false;
        public List<long> IgnoredMobsPointer = new List<long>();
        public int ClearingSize = 0;
        public long LastMobAttackedHP = 0;
        public int AttackNotRegisteredCount = 0;
        public int MoveTryCount = 0;

        public int MaxAttackTry = 8; //edit this after knowing we used attack correctly
        public int MaxMoveTry = 5;

        public bool FirstAttackCasted = false;
        public bool DoingBattle = false;
        public bool ClearingFullArea = false;

        public void SetForm1(Form1 form1_1)
        {
            Form1_0 = form1_1;
        }

        public void CastDefense()
        {
            if (CharConfig.UseBO && !Form1_0.Town_0.GetInTown())
            {
                Form1_0.Potions_0.CheckIfWeUsePotion();

                Form1_0.KeyMouse_0.PressKey(Keys.W);
                Form1_0.WaitDelay(15);
                Form1_0.KeyMouse_0.PressKey(Keys.F4);
                Form1_0.WaitDelay(15);
                Form1_0.PlayerScan_0.GetPositions();

                //press W again to switch weapon again
                if (Form1_0.PlayerScan_0.RightSkill != Enums.Skill.BattleOrders)
                {
                    Form1_0.KeyMouse_0.PressKey(Keys.W);
                    Form1_0.WaitDelay(15);
                    Form1_0.KeyMouse_0.PressKey(Keys.F4);
                    Form1_0.WaitDelay(15);
                    //Form1_0.PlayerScan_0.GetPositions();
                }

                Form1_0.KeyMouse_0.MouseCliccRight(Form1_0.CenterX, Form1_0.CenterY);
                Form1_0.WaitDelay(30);

                //select battle command
                Form1_0.KeyMouse_0.MouseClicc(1025, 1025);
                Form1_0.WaitDelay(5);
                Form1_0.KeyMouse_0.MouseClicc(1025, 610);
                Form1_0.WaitDelay(5);
                Form1_0.KeyMouse_0.MouseCliccRight(Form1_0.CenterX, Form1_0.CenterY);
                Form1_0.WaitDelay(30); //60 <-
                Form1_0.Potions_0.CheckIfWeUsePotion();

                //select battle cry
                Form1_0.KeyMouse_0.MouseClicc(1025, 1025);
                Form1_0.WaitDelay(5);
                Form1_0.KeyMouse_0.MouseClicc(1165, 610);
                Form1_0.WaitDelay(5);
                Form1_0.KeyMouse_0.MouseCliccRight(Form1_0.CenterX, Form1_0.CenterY);
                Form1_0.WaitDelay(60);

                Form1_0.KeyMouse_0.PressKey(Keys.W);
                Form1_0.WaitDelay(15);
            }

            //cast sacred shield
            Form1_0.KeyMouse_0.PressKey(CharConfig.KeySkillCastDefense);
            Form1_0.WaitDelay(5);
            Form1_0.KeyMouse_0.MouseCliccRight(Form1_0.CenterX, Form1_0.CenterY);
            Form1_0.WaitDelay(35);

            //cast sacred shield
            Form1_0.KeyMouse_0.PressKey(CharConfig.KeySkillLifeAura);
            Form1_0.WaitDelay(5);
            Form1_0.KeyMouse_0.MouseCliccRight(Form1_0.CenterX, Form1_0.CenterY);
            Form1_0.WaitDelay(5);
        }

        public void ClearAreaOfMobs(int ThisX, int ThisY, int ClearSize)
        {
            AreaX = ThisX;
            AreaY = ThisY;
            IgnoredMobsPointer = new List<long>();
            ClearingSize = ClearSize;
            AttackNotRegisteredCount = 0;
            MoveTryCount = 0;
            ClearingFullArea = false;

            //ClearingArea = true;
            if (Form1_0.MobsStruc_0.GetMobs("", "", true, ClearingSize, IgnoredMobsPointer)) ClearingArea = true;
        }

        public void ClearFullAreaOfMobs()
        {
            IgnoredMobsPointer = new List<long>();
            AttackNotRegisteredCount = 0;
            MoveTryCount = 0;
            ClearingSize = 500;
            ClearingFullArea = true;

            if (Form1_0.MobsStruc_0.GetMobs("", "", true, ClearingSize, IgnoredMobsPointer)) ClearingArea = true;
        }

        public void SetBattleMoveAcceptOffset()
        {
            //if (CharConfig.RunningOnChar.ToLower().Contains("sorc")) Form1_0.Mover_0.MoveAcceptOffset = 10;
            //else Form1_0.Mover_0.MoveAcceptOffset = 4; //default
        }

        public void ResetBattleMoveAcceptOffset()
        {
            //Form1_0.Mover_0.MoveAcceptOffset = 4; //default
        }

        public void RunBattleScript()
        {
            if ((Enums.Area) Form1_0.PlayerScan_0.levelNo == Enums.Area.ThroneOfDestruction)
            {
                //15096,5096
                if (Form1_0.PlayerScan_0.yPosFinal > 5096)
                {
                    DoingBattle = false;
                    FirstAttackCasted = false;
                    ResetBattleMoveAcceptOffset();
                    if (!ClearingFullArea) Form1_0.PathFinding_0.MoveToThisPos(new Position { X = AreaX, Y = AreaY });
                    //Form1_0.Mover_0.MoveToLocation(AreaX, AreaY);
                    ClearingArea = false;
                    return;
                }
            }

            if (Form1_0.MobsStruc_0.GetMobs("", "", true, ClearingSize, IgnoredMobsPointer))
            {
                DoingBattle = true;
                SetBattleMoveAcceptOffset();
                Form1_0.Mover_0.MoveAcceptOffset = 2;
                Form1_0.Mover_0.MoveToLocationAttack(Form1_0.MobsStruc_0.xPosFinal + 1, Form1_0.MobsStruc_0.yPosFinal + 1);
                Form1_0.Mover_0.MoveAcceptOffset = 4;
                ResetBattleMoveAcceptOffset();

                FirstAttackCasting();
                SetSkills();
                CastSkills();
                if (CharConfig.RunningOnChar.ToLower().Contains("paladin"))
                {
                    CastSkills();
                    CastSkills();
                }
                AttackTryCheck();
            }
            else
            {
                DoingBattle = false;
                FirstAttackCasted = false;
                ResetBattleMoveAcceptOffset();
                if (!ClearingFullArea) Form1_0.PathFinding_0.MoveToThisPos(new Position { X = AreaX, Y = AreaY });
                //Form1_0.Mover_0.MoveToLocation(AreaX, AreaY);
                ClearingArea = false;
            }
        }

        public bool DoBattleScript(int MaxDistance)
        {
            if (Form1_0.MobsStruc_0.GetMobs("", "", true, MaxDistance, new List<long>()))
            {
                DoingBattle = true;
                SetBattleMoveAcceptOffset();
                Form1_0.Mover_0.MoveAcceptOffset = 2;
                Form1_0.Mover_0.MoveToLocationAttack(Form1_0.MobsStruc_0.xPosFinal + 1, Form1_0.MobsStruc_0.yPosFinal + 1);
                Form1_0.Mover_0.MoveAcceptOffset = 4;
                ResetBattleMoveAcceptOffset();

                FirstAttackCasting();
                SetSkills();
                CastSkills();
                if (CharConfig.RunningOnChar.ToLower().Contains("paladin"))
                {
                    CastSkills();
                    CastSkills();
                }
                AttackTryCheck();
                return true;
            }

            DoingBattle = false;
            FirstAttackCasted = false;
            return false;
        }

        public void RunBattleScriptOnThisMob(string MobType, string MobName)
        {
            if (Form1_0.MobsStruc_0.GetMobs(MobType, MobName, false, 200, new List<long>()))
            {
                DoingBattle = true;
                SetBattleMoveAcceptOffset();
                Form1_0.Mover_0.MoveAcceptOffset = 2;
                Form1_0.Mover_0.MoveToLocationAttack(Form1_0.MobsStruc_0.xPosFinal + 1, Form1_0.MobsStruc_0.yPosFinal + 1);
                Form1_0.Mover_0.MoveAcceptOffset = 4;
                ResetBattleMoveAcceptOffset();

                
                FirstAttackCasting();
                SetSkills();
                CastSkills();
                if (CharConfig.RunningOnChar.ToLower().Contains("paladin"))
                {
                    CastSkills();
                    CastSkills();
                }
                AttackTryCheck();
            }
            else
            {
                DoingBattle = false;
                FirstAttackCasted = false;
            }
        }

        public void MoveAway()
        {
            int MoveDistance = 5;
            //Form1_0.WaitDelay(5); //wait a little bit, we just casted attack
            if (MoveTryCount == 1)
            {
                Form1_0.Mover_0.MoveAcceptOffset = 2;
                Form1_0.Mover_0.MoveToLocationAttack(Form1_0.PlayerScan_0.xPosFinal + MoveDistance, Form1_0.PlayerScan_0.yPosFinal + MoveDistance);
                Form1_0.Mover_0.MoveAcceptOffset = 4;
            }
            if (MoveTryCount == 2)
            {
                Form1_0.Mover_0.MoveAcceptOffset = 2;
                Form1_0.Mover_0.MoveToLocationAttack(Form1_0.PlayerScan_0.xPosFinal - MoveDistance, Form1_0.PlayerScan_0.yPosFinal + MoveDistance);
                Form1_0.Mover_0.MoveAcceptOffset = 4;
            }
            if (MoveTryCount == 3)
            {
                Form1_0.Mover_0.MoveAcceptOffset = 2;
                Form1_0.Mover_0.MoveToLocationAttack(Form1_0.PlayerScan_0.xPosFinal + MoveDistance, Form1_0.PlayerScan_0.yPosFinal - MoveDistance);
                Form1_0.Mover_0.MoveAcceptOffset = 4;
            }
            if (MoveTryCount == 4)
            {
                Form1_0.Mover_0.MoveAcceptOffset = 2;
                Form1_0.Mover_0.MoveToLocationAttack(Form1_0.PlayerScan_0.xPosFinal - MoveDistance, Form1_0.PlayerScan_0.yPosFinal - MoveDistance);
                Form1_0.Mover_0.MoveAcceptOffset = 4;
            }
        }

        public void AttackTryCheck()
        {
            Form1_0.Potions_0.CheckIfWeUsePotion();
            Form1_0.MobsStruc_0.GetLastMobs();
            //long AttackedThisPointer = Form1_0.MobsStruc_0.LastMobsPointerLocation;

            //if (AttackedThisPointer == LastMobAttackedPointer)
            //{
                if (Form1_0.MobsStruc_0.MobsHP >= LastMobAttackedHP)
                {
                    AttackNotRegisteredCount++;
                    //Form1_0.method_1("Attack not registered! " + AttackNotRegisteredCount + "/" + MaxAttackTry, Color.Red);

                    if (AttackNotRegisteredCount >= MaxAttackTry)
                    {
                        AttackNotRegisteredCount = 0;
                        MoveTryCount++;
                        Form1_0.method_1("Attack not registered, moving away! " + MoveTryCount + "/" + MaxMoveTry, Color.Red);
                        MoveAway();

                        if (MoveTryCount >= MaxMoveTry)
                        {
                            MoveTryCount = 0;
                            IgnoredMobsPointer.Add(Form1_0.MobsStruc_0.LastMobsPointerLocation);
                        }
                    }
                }
                else
                {
                    //Form1_0.method_1("Attack registered! " + AttackNotRegisteredCount + "/" + MaxAttackTry, Color.DarkGreen);
                    AttackNotRegisteredCount = 0;
                    MoveTryCount = 0;
                }
            /*}
            else
            {
                AttackNotRegisteredCount = 0;
                MoveTryCount = 0;
            }*/

            //LastMobAttackedPointer = Form1_0.MobsStruc_0.LastMobsPointerLocation;
            LastMobAttackedHP = Form1_0.MobsStruc_0.MobsHP;
        }

        public void SetSkills()
        {
            Form1_0.KeyMouse_0.PressKey(CharConfig.KeySkillAttack);
            Form1_0.KeyMouse_0.PressKey(CharConfig.KeySkillAura);
        }

        public void CastSkills()
        {
            if (Form1_0.MobsStruc_0.xPosFinal != 0 && Form1_0.MobsStruc_0.yPosFinal != 0)
            {
                Dictionary<string, int> itemScreenPos = Form1_0.GameStruc_0.World2Screen(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, Form1_0.MobsStruc_0.xPosFinal, Form1_0.MobsStruc_0.yPosFinal);
                if (!CharConfig.PlayerAttackWithRightHand)
                {
                    Form1_0.KeyMouse_0.SendSHIFT_CLICK(itemScreenPos["x"], itemScreenPos["y"] - 30);
                }
                else
                {
                    Form1_0.KeyMouse_0.MouseCliccRightAttackMove(itemScreenPos["x"], itemScreenPos["y"] - 30);
                }
            }
            else
            {
                if (!CharConfig.PlayerAttackWithRightHand)
                {
                    Form1_0.KeyMouse_0.SendSHIFT_CLICK(Form1_0.CenterX, Form1_0.CenterY - 1);
                    //Form1_0.KeyMouse_0.SendSHIFT_CLICK(Form1_0.CenterX, Form1_0.CenterY);
                }
                else
                {
                    Form1_0.KeyMouse_0.MouseCliccRightAttackMove(Form1_0.CenterX, Form1_0.CenterY - 1);
                }
            }
            //Form1_0.WaitDelay(5);
            Form1_0.WaitDelay(1);
        }

        public void FirstAttackCasting()
        {
            if (!FirstAttackCasted)
            {
                if (CharConfig.RunningOnChar.ToLower().Contains("sorc"))
                {
                    Form1_0.KeyMouse_0.PressKey(CharConfig.KeySkillAttack); //select static

                    int tryes = 0;
                    while (tryes < 6)
                    {
                        CastSkills();
                        Form1_0.WaitDelay(35);
                        tryes++;
                    }
                }

                FirstAttackCasted = true;
            }
        }
    }
}
