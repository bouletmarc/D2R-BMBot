using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        public long LastMobAttackedPointer = 0;
        public long LastMobAttackedHP = 0;
        public int AttackNotRegisteredCount = 0;
        public int MoveTryCount = 0;

        public int MaxAttackTry = 10; //edit this after knowing we used attack correctly
        public int MaxMoveTry = 5;

        public void SetForm1(Form1 form1_1)
        {
            Form1_0 = form1_1;
        }

        public void CastDefense()
        {
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
            ClearingArea = true;
        }

        public void RunBattleScript()
        {
            if (Form1_0.MobsStruc_0.GetMobs("", "", true, ClearingSize, IgnoredMobsPointer))
            {
                if (MoveTryCount > 0)
                {
                    MoveAway();
                }
                if (Form1_0.Mover_0.MoveToLocation(Form1_0.MobsStruc_0.xPosFinal + 2, Form1_0.MobsStruc_0.yPosFinal + 2))
                {
                    SetSkills();
                }
                Form1_0.MobsStruc_0.GetLastMobs();
                CastSkills();
                AttackTryCheck();
            }
            else
            {
                Form1_0.Mover_0.MoveToLocation(AreaX, AreaY);
                ClearingArea = false;
            }
        }

        public bool DoBattleScript(int MaxDistance)
        {
            if (Form1_0.MobsStruc_0.GetMobs("", "", true, MaxDistance, new List<long>()))
            {
                if (MoveTryCount > 0)
                {
                    MoveAway();
                }
                if (Form1_0.Mover_0.MoveToLocation(Form1_0.MobsStruc_0.xPosFinal + 2, Form1_0.MobsStruc_0.yPosFinal + 2))
                {
                    SetSkills();
                }
                Form1_0.MobsStruc_0.GetLastMobs();
                CastSkills();
                AttackTryCheck();
                return true;
            }
            return false;
        }

        public void RunBattleScriptOnThisMob(string MobType, string MobName)
        {
            if (Form1_0.MobsStruc_0.GetMobs(MobType, MobName, false, 99, new List<long>()))
            {
                if (MoveTryCount > 0)
                {
                    MoveAway();
                }
                if (Form1_0.Mover_0.MoveToLocation(Form1_0.MobsStruc_0.xPosFinal + 2, Form1_0.MobsStruc_0.yPosFinal + 2))
                {
                    SetSkills();
                }
                Form1_0.MobsStruc_0.GetLastMobs();
                CastSkills();
                AttackTryCheck();
            }
        }

        public void MoveAway()
        {
            int MoveDistance = 7;
            if (MoveTryCount == 1)
            {
                Form1_0.Mover_0.MoveToLocation(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal + MoveDistance);
            }
            if (MoveTryCount == 2)
            {
                Form1_0.Mover_0.MoveToLocation(Form1_0.PlayerScan_0.xPosFinal - MoveDistance, Form1_0.PlayerScan_0.yPosFinal + MoveDistance);
            }
            if (MoveTryCount == 3)
            {
                Form1_0.Mover_0.MoveToLocation(Form1_0.PlayerScan_0.xPosFinal + MoveDistance, Form1_0.PlayerScan_0.yPosFinal - MoveDistance);
            }
            if (MoveTryCount == 4)
            {
                Form1_0.Mover_0.MoveToLocation(Form1_0.PlayerScan_0.xPosFinal - MoveDistance, Form1_0.PlayerScan_0.yPosFinal - MoveDistance);
            }
        }

        public void AttackTryCheck()
        {
            long AttackedThisPointer = Form1_0.MobsStruc_0.MobsPointerLocation;

            if (AttackedThisPointer == LastMobAttackedPointer)
            {
                if (Form1_0.MobsStruc_0.MobsHP == LastMobAttackedHP)
                {
                    AttackNotRegisteredCount++;

                    if (AttackNotRegisteredCount >= MaxAttackTry)
                    {
                        MoveTryCount++;

                        if (MoveTryCount >= MaxMoveTry)
                        {
                            IgnoredMobsPointer.Add(Form1_0.MobsStruc_0.MobsPointerLocation);
                        }
                    }
                }
                else
                {
                    AttackNotRegisteredCount = 0;
                    MoveTryCount = 0;
                }
            }
            else
            {
                AttackNotRegisteredCount = 0;
                MoveTryCount = 0;
            }

            LastMobAttackedPointer = Form1_0.MobsStruc_0.MobsPointerLocation;
            LastMobAttackedHP = Form1_0.MobsStruc_0.MobsHP;
        }

        public void SetSkills()
        {
            Form1_0.KeyMouse_0.PressKey(CharConfig.KeySkillAttack);
            if (CharConfig.PlayerAttackWithRightHand) Form1_0.KeyMouse_0.PressKey(CharConfig.KeySkillAura);
        }

        public void CastSkills()
        {
            Dictionary<string, int> itemScreenPos = Form1_0.GameStruc_0.World2Screen(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, Form1_0.MobsStruc_0.xPosFinal, Form1_0.MobsStruc_0.yPosFinal);
            if (!CharConfig.PlayerAttackWithRightHand)
            {
                Form1_0.KeyMouse_0.MouseClicc(itemScreenPos["x"], itemScreenPos["y"]);
            }
            else
            {
                Form1_0.KeyMouse_0.MouseCliccRight(itemScreenPos["x"], itemScreenPos["y"]);
            }
        }
    }
}
