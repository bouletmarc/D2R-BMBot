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

        public Position GetBestAttackLocation(Position ThisAttackPos)
        {
            Position ReturnPos = new Position { X = ThisAttackPos.X, Y = ThisAttackPos.Y};
            int ChoosenAttackLocation = 0; //0=Down, 1=Right, 2=Left, 3=Up

            bool[,] ThisCollisionGrid = Form1_0.MapAreaStruc_0.CollisionGrid((Enums.Area)Form1_0.PlayerScan_0.levelNo);

            if (ThisCollisionGrid.GetLength(0) == 0 || ThisCollisionGrid.GetLength(1) == 0) return ReturnPos;
            if (Form1_0.MapAreaStruc_0.AllMapData.Count == 0) return ReturnPos;

            int ThisX = ThisAttackPos.X - Form1_0.MapAreaStruc_0.AllMapData[(int)Form1_0.PlayerScan_0.levelNo - 1].Offset.X;
            int ThisY = ThisAttackPos.Y - Form1_0.MapAreaStruc_0.AllMapData[(int)Form1_0.PlayerScan_0.levelNo - 1].Offset.Y;

            if (ThisX < 0) return ReturnPos;
            if (ThisY < 0) return ReturnPos;
            if (ThisX > ThisCollisionGrid.GetLength(0) - 1) return ReturnPos;
            if (ThisY > ThisCollisionGrid.GetLength(1) - 1) return ReturnPos;

            try
            {
                bool AttackPosFound = false;
                while (!AttackPosFound)
                {
                    //check boundary for attacking the mobs from down position
                    if (ChoosenAttackLocation == 0)
                    {
                        //#####
                        //Check Validity
                        bool IsValid = true;
                        if (ThisX < 2) IsValid = false;
                        if (ThisY < 2) IsValid = false;
                        if (ThisX > ThisCollisionGrid.GetLength(0) - 1) IsValid = false;
                        if (ThisY > ThisCollisionGrid.GetLength(1) - 1) IsValid = false;
                        //#####

                        if (ThisCollisionGrid[ThisX, ThisY]
                            && ThisCollisionGrid[ThisX - 1, ThisY]
                            && ThisCollisionGrid[ThisX - 2, ThisY]
                            && ThisCollisionGrid[ThisX - 1, ThisY - 1]
                            && ThisCollisionGrid[ThisX - 2, ThisY - 1]
                            && ThisCollisionGrid[ThisX - 1, ThisY - 2]
                            && IsValid) 
                        {
                            //Form1_0.method_1("Attack from Bottom!", Color.OrangeRed);
                            AttackPosFound = true;
                            ChoosenAttackLocation = 0; //Attack from Bottom
                            ReturnPos = new Position { X = ThisX + Form1_0.MapAreaStruc_0.AllMapData[(int)Form1_0.PlayerScan_0.levelNo - 1].Offset.X, Y = ThisY + Form1_0.MapAreaStruc_0.AllMapData[(int)Form1_0.PlayerScan_0.levelNo - 1].Offset.Y };
                        }
                        else
                        {
                            //change attack location to right
                            ThisX += 4;
                            ThisY -= 2;

                            ChoosenAttackLocation++;
                        }
                    }

                    //check boundary for attacking the mobs from Right position
                    if (ChoosenAttackLocation == 1)
                    {
                        //#####
                        //Check Validity
                        bool IsValid = true;
                        if (ThisX < 2) IsValid = false;
                        if (ThisY < 0) IsValid = false;
                        if (ThisX > ThisCollisionGrid.GetLength(0) - 1) IsValid = false;
                        if (ThisY > ThisCollisionGrid.GetLength(1) - 1) IsValid = false;
                        //#####

                        if (ThisCollisionGrid[ThisX, ThisY]
                            && ThisCollisionGrid[ThisX - 1, ThisY]
                            && ThisCollisionGrid[ThisX - 2, ThisY]
                            && IsValid)
                        {
                            //Form1_0.method_1("Attack from Right!", Color.OrangeRed);
                            AttackPosFound = true;
                            ChoosenAttackLocation = 1; //Attack from Right
                            ReturnPos = new Position { X = ThisX + Form1_0.MapAreaStruc_0.AllMapData[(int)Form1_0.PlayerScan_0.levelNo - 1].Offset.X, Y = ThisY + Form1_0.MapAreaStruc_0.AllMapData[(int)Form1_0.PlayerScan_0.levelNo - 1].Offset.Y };
                        }
                        else
                        {
                            //change attack location to left
                            ThisX -= 7;

                            ChoosenAttackLocation++;
                        }
                    }

                    //check boundary for attacking the mobs from Left position
                    if (ChoosenAttackLocation == 2)
                    {
                        //#####
                        //Check Validity
                        bool IsValid = true;
                        if (ThisX < 1) IsValid = false;
                        if (ThisY < 1) IsValid = false;
                        if (ThisX > ThisCollisionGrid.GetLength(0) - 3) IsValid = false;
                        if (ThisY > ThisCollisionGrid.GetLength(1) - 1) IsValid = false;
                        //#####

                        if (ThisCollisionGrid[ThisX, ThisY]
                            && ThisCollisionGrid[ThisX - 1, ThisY]
                            && ThisCollisionGrid[ThisX + 1, ThisY]
                            && ThisCollisionGrid[ThisX + 2, ThisY]
                            && ThisCollisionGrid[ThisX, ThisY - 1]
                            && ThisCollisionGrid[ThisX + 1, ThisY - 1]
                            && IsValid)
                        {
                            //Form1_0.method_1("Attack from Left!", Color.OrangeRed);
                            AttackPosFound = true;
                            ChoosenAttackLocation = 2; //Attack from Left
                            ReturnPos = new Position { X = ThisX + Form1_0.MapAreaStruc_0.AllMapData[(int)Form1_0.PlayerScan_0.levelNo - 1].Offset.X, Y = ThisY + Form1_0.MapAreaStruc_0.AllMapData[(int)Form1_0.PlayerScan_0.levelNo - 1].Offset.Y };
                        }
                        else
                        {
                            //change attack location to top
                            ThisX += 3;
                            ThisY -= 5;

                            ChoosenAttackLocation++;
                        }
                    }

                    //check boundary for attacking the mobs from Up position (NOT RECOMMENDED FOR HAMMER)
                    if (ChoosenAttackLocation == 3)
                    {
                        //#####
                        //Check Validity
                        bool IsValid = true;
                        if (ThisX < 1) IsValid = false;
                        if (ThisY < 1) IsValid = false;
                        if (ThisX > ThisCollisionGrid.GetLength(0) - 2) IsValid = false;
                        if (ThisY > ThisCollisionGrid.GetLength(1) - 2) IsValid = false;
                        //#####

                        if (ThisCollisionGrid[ThisX, ThisY]
                            && ThisCollisionGrid[ThisX - 1, ThisY]
                            && ThisCollisionGrid[ThisX + 1, ThisY]
                            && ThisCollisionGrid[ThisX, ThisY - 1]
                            && ThisCollisionGrid[ThisX, ThisY + 1]
                            && IsValid)
                        {
                            //Form1_0.method_1("Attack from Top!", Color.OrangeRed);
                            AttackPosFound = true;
                            ChoosenAttackLocation = 3; //Attack from Top
                            ReturnPos = new Position { X = ThisX + Form1_0.MapAreaStruc_0.AllMapData[(int)Form1_0.PlayerScan_0.levelNo - 1].Offset.X, Y = ThisY + Form1_0.MapAreaStruc_0.AllMapData[(int)Form1_0.PlayerScan_0.levelNo - 1].Offset.Y };
                        }
                        else
                        {
                            Form1_0.method_1("No Attack pos found!", Color.Red);
                            //no atack pos found??
                            AttackPosFound = true;
                            ChoosenAttackLocation++; //return attack pos = 4 (for error)
                            ReturnPos = new Position { X = 0, Y = 0 };
                        }
                    }
                }

            }
            catch { }

            return ReturnPos;
        }

        public void CastDefense()
        {
            if (CharConfig.UseBO && !Form1_0.Town_0.GetInTown())
            {
                Form1_0.Potions_0.CheckIfWeUsePotion();

                Form1_0.KeyMouse_0.PressKey(Keys.W);
                Form1_0.WaitDelay(15);
                //Form1_0.KeyMouse_0.PressKey(Keys.F4);
                //Form1_0.WaitDelay(15);
                Form1_0.KeyMouse_0.MouseClicc(1025, 1025);
                Form1_0.WaitDelay(5);
                Form1_0.KeyMouse_0.MouseClicc(1095, 610);
                Form1_0.WaitDelay(5);
                Form1_0.PlayerScan_0.GetPositions();

                //press W again to switch weapon again
                if (Form1_0.PlayerScan_0.RightSkill != Enums.Skill.BattleOrders)
                {
                    Form1_0.KeyMouse_0.PressKey(Keys.W);
                    Form1_0.WaitDelay(15);
                    //Form1_0.KeyMouse_0.PressKey(Keys.F4);
                    //Form1_0.WaitDelay(15);
                    Form1_0.KeyMouse_0.MouseClicc(1025, 1025);
                    Form1_0.WaitDelay(5);
                    Form1_0.KeyMouse_0.MouseClicc(1095, 610);
                    Form1_0.WaitDelay(5);
                    Form1_0.PlayerScan_0.GetPositions();
                }

                Form1_0.KeyMouse_0.MouseCliccRight_RealPos(Form1_0.CenterX, Form1_0.CenterY);
                Form1_0.WaitDelay(30);

                //select battle command
                Form1_0.KeyMouse_0.MouseClicc(1025, 1025);
                Form1_0.WaitDelay(5);
                Form1_0.KeyMouse_0.MouseClicc(1025, 610);
                Form1_0.WaitDelay(5);
                Form1_0.KeyMouse_0.MouseCliccRight_RealPos(Form1_0.CenterX, Form1_0.CenterY);
                Form1_0.WaitDelay(30); //60 <-
                Form1_0.Potions_0.CheckIfWeUsePotion();

                //select battle cry
                Form1_0.KeyMouse_0.MouseClicc(1025, 1025);
                Form1_0.WaitDelay(5);
                Form1_0.KeyMouse_0.MouseClicc(1165, 610);
                Form1_0.WaitDelay(5);
                Form1_0.KeyMouse_0.MouseCliccRight_RealPos(Form1_0.CenterX, Form1_0.CenterY);
                Form1_0.WaitDelay(60);

                Form1_0.KeyMouse_0.PressKey(Keys.W);
                Form1_0.WaitDelay(15);
                Form1_0.PlayerScan_0.GetPositions();
            }

            //press W again to switch weapon again
            if (Form1_0.PlayerScan_0.RightSkill == Enums.Skill.BattleCry 
                || Form1_0.PlayerScan_0.RightSkill == Enums.Skill.BattleOrders
                || Form1_0.PlayerScan_0.RightSkill == Enums.Skill.BattleCommand)
            {
                Form1_0.KeyMouse_0.PressKey(Keys.W);
                Form1_0.WaitDelay(15);
                Form1_0.PlayerScan_0.GetPositions();
            }

            //cast sacred shield
            Form1_0.KeyMouse_0.PressKey(CharConfig.KeySkillCastDefense);
            Form1_0.WaitDelay(5);
            Form1_0.KeyMouse_0.MouseCliccRight_RealPos(Form1_0.CenterX, Form1_0.CenterY);
            Form1_0.WaitDelay(35);

            //cast sacred shield
            Form1_0.KeyMouse_0.PressKey(CharConfig.KeySkillLifeAura);
            Form1_0.WaitDelay(5);
            Form1_0.KeyMouse_0.MouseCliccRight_RealPos(Form1_0.CenterX, Form1_0.CenterY);
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
                Position ThisAttackPos = GetBestAttackLocation(new Position { X = Form1_0.MobsStruc_0.xPosFinal + 1, Y = Form1_0.MobsStruc_0.yPosFinal + 5 });
                if (ThisAttackPos.X != 0 && ThisAttackPos.Y != 0) Form1_0.Mover_0.MoveToLocationAttack(ThisAttackPos.X, ThisAttackPos.Y);
                //Form1_0.Mover_0.MoveToLocationAttack(Form1_0.MobsStruc_0.xPosFinal - 1, Form1_0.MobsStruc_0.yPosFinal + 2);
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
                Position ThisAttackPos = GetBestAttackLocation(new Position { X = Form1_0.MobsStruc_0.xPosFinal + 1, Y = Form1_0.MobsStruc_0.yPosFinal + 5 });
                if (ThisAttackPos.X != 0 && ThisAttackPos.Y != 0) Form1_0.Mover_0.MoveToLocationAttack(ThisAttackPos.X, ThisAttackPos.Y);
                //Form1_0.Mover_0.MoveToLocationAttack(Form1_0.MobsStruc_0.xPosFinal - 1, Form1_0.MobsStruc_0.yPosFinal + 2);
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
                Position ThisAttackPos = GetBestAttackLocation(new Position { X = Form1_0.MobsStruc_0.xPosFinal + 1, Y = Form1_0.MobsStruc_0.yPosFinal + 5 });
                if (ThisAttackPos.X != 0 && ThisAttackPos.Y != 0) Form1_0.Mover_0.MoveToLocationAttack(ThisAttackPos.X, ThisAttackPos.Y);
                //Form1_0.Mover_0.MoveToLocationAttack(Form1_0.MobsStruc_0.xPosFinal - 1, Form1_0.MobsStruc_0.yPosFinal + 2);
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
                    //Form1_0.method_1("Attack not registered! " + AttackNotRegisteredCount + "/" + MaxAttackTry, Color.OrangeRed);

                    if (AttackNotRegisteredCount >= MaxAttackTry)
                    {
                        AttackNotRegisteredCount = 0;
                        MoveTryCount++;
                        Form1_0.method_1("Attack not registered, moving away! " + MoveTryCount + "/" + MaxMoveTry, Color.OrangeRed);
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
                Form1_0.PlayerScan_0.GetPositions();
                Dictionary<string, int> itemScreenPos = Form1_0.GameStruc_0.World2Screen(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, Form1_0.MobsStruc_0.xPosFinal, Form1_0.MobsStruc_0.yPosFinal);
                if (!CharConfig.PlayerAttackWithRightHand)
                {
                    Form1_0.KeyMouse_0.SendSHIFT_CLICK_ATTACK(itemScreenPos["x"], itemScreenPos["y"] - 30);
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
                    Form1_0.KeyMouse_0.SendSHIFT_CLICK_ATTACK(Form1_0.CenterX, Form1_0.CenterY - 1);
                }
                else
                {
                    Form1_0.KeyMouse_0.MouseCliccRightAttackMove(Form1_0.CenterX, Form1_0.CenterY - 1);
                }
            }
            //Form1_0.WaitDelay(5);
            //Form1_0.WaitDelay(1);
        }

        public void CastSkillsNoMove()
        {
            if (Form1_0.MobsStruc_0.xPosFinal != 0 && Form1_0.MobsStruc_0.yPosFinal != 0)
            {
                Form1_0.PlayerScan_0.GetPositions();
                Dictionary<string, int> itemScreenPos = Form1_0.GameStruc_0.World2Screen(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, Form1_0.MobsStruc_0.xPosFinal, Form1_0.MobsStruc_0.yPosFinal);
                if (!CharConfig.PlayerAttackWithRightHand)
                {
                    //Form1_0.KeyMouse_0.SendSHIFT_CLICK_ATTACK(itemScreenPos["x"], itemScreenPos["y"] - 30);
                    Form1_0.KeyMouse_0.SendSHIFT_CLICK_ATTACK_CAST_NO_MOVE(itemScreenPos["x"], itemScreenPos["y"] - 30);
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
                    //Form1_0.KeyMouse_0.SendSHIFT_CLICK_ATTACK(Form1_0.CenterX, Form1_0.CenterY - 1);
                    Form1_0.KeyMouse_0.SendSHIFT_CLICK_ATTACK_CAST_NO_MOVE(Form1_0.CenterX, Form1_0.CenterY - 1);
                }
                else
                {
                    Form1_0.KeyMouse_0.MouseCliccRightAttackMove(Form1_0.CenterX, Form1_0.CenterY - 1);
                }
            }
            //Form1_0.WaitDelay(5);
            //Form1_0.WaitDelay(1);
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
