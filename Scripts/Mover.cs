using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace app
{
    public class Mover
    {
        Form1 Form1_0;

        public int MaxMoveTry = 5;
        public int MoveAcceptOffset = 4;
        public long StartAreaBeforeMoving = 0;

        public bool AllowFastMove = false;

        public DateTime LastTimeSinceTeleport = DateTime.Now;

        public void SetForm1(Form1 form1_1)
        {
            Form1_0 = form1_1;
        }

        //This will move to a direct location -> no pathfinding
        public bool MoveToLocation(int ThisX, int ThisY, bool AllowPickingItem = false, bool AllowMoveSideWay = true)
        {
            Form1_0.UIScan_0.readUI();
            if (Form1_0.UIScan_0.leftMenu || Form1_0.UIScan_0.rightMenu) Form1_0.UIScan_0.CloseAllUIMenu();

            Form1_0.PlayerScan_0.GetPositions();
            Form1_0.overlayForm.SetMoveToPoint(new System.Drawing.Point(ThisX, ThisY));
            StartAreaBeforeMoving = Form1_0.PlayerScan_0.levelNo;
            //Form1_0.GameStruc_0.CheckChickenGameTime();

            //######
            //moving location is way to far away something might be wrong!
            if (Form1_0.PlayerScan_0.xPosFinal < (ThisX - 300)
                || Form1_0.PlayerScan_0.xPosFinal > (ThisX + 300)
                || Form1_0.PlayerScan_0.yPosFinal < (ThisY - 300)
                || Form1_0.PlayerScan_0.yPosFinal > (ThisY + 300))
            {
                return false;
            }
            if (ThisX == 0 && ThisY == 0) return false;
            //######

            //no need to move we are close already!
            if (Form1_0.PlayerScan_0.xPosFinal >= (ThisX - MoveAcceptOffset)
                && Form1_0.PlayerScan_0.xPosFinal <= (ThisX + MoveAcceptOffset)
                && Form1_0.PlayerScan_0.yPosFinal >= (ThisY - MoveAcceptOffset)
                && Form1_0.PlayerScan_0.yPosFinal <= (ThisY + MoveAcceptOffset))
            {
                Form1_0.overlayForm.ResetMoveToLocation();
                return true;
            }

            if (!Form1_0.GameStruc_0.IsInGame() || !Form1_0.Running)
            {
                Form1_0.overlayForm.ResetMoveToLocation();
                return false;
            }

            int TryMove = 0;
            int TryMove2 = 0;
            int LastX = Form1_0.PlayerScan_0.xPosFinal;
            int LastY = Form1_0.PlayerScan_0.yPosFinal;
            Dictionary<string, int> itemScreenPos = Form1_0.GameStruc_0.World2Screen(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, ThisX, ThisY);

            if (!CharConfig.UseTeleport || (CharConfig.UseTeleport && Form1_0.Town_0.GetInTown()))
            {
                Form1_0.KeyMouse_0.MouseClicHoldWithoutRelease();
                Form1_0.KeyMouse_0.PressKeyHold(System.Windows.Forms.Keys.E);
            }
            while (true)
            {
                if (Form1_0.Town_0.GetInTown())
                {
                    Form1_0.KeyMouse_0.PressKey(CharConfig.KeySkillfastMoveAtTown);
                    AllowFastMove = false;
                }
                else
                {
                    Form1_0.KeyMouse_0.PressKey(CharConfig.KeySkillfastMoveOutsideTown);
                    if (CharConfig.UseTeleport) AllowFastMove = true;
                    if (CharConfig.RunBaalLeechScript && !Form1_0.BaalLeech_0.ScriptDone) AllowFastMove = false;
                    if (CharConfig.RunLowerKurastScript && !Form1_0.LowerKurast_0.ScriptDone) AllowFastMove = true;

                    //Check if we are in close range from target destination, if we are, desactivate fast moving (eles it teleport twice)
                    if (AllowFastMove)
                    {
                        if (Form1_0.PlayerScan_0.xPosFinal >= (ThisX - 21)
                            && Form1_0.PlayerScan_0.xPosFinal <= (ThisX + 21)
                            && Form1_0.PlayerScan_0.yPosFinal >= (ThisY - 21)
                            && Form1_0.PlayerScan_0.yPosFinal <= (ThisY + 21))
                        {
                            AllowFastMove = false;
                        }
                    }
                }

                //calculate new Y clicking offset, else it will clic on bottom menu items
                if (itemScreenPos["y"] >= (Form1_0.ScreenY - Form1_0.ScreenYMenu))
                {
                    int DiffX = Form1_0.CenterX - itemScreenPos["x"];
                    itemScreenPos["x"] = (int)(itemScreenPos["x"] + (DiffX / 6));
                    itemScreenPos["y"] = (Form1_0.ScreenY - Form1_0.ScreenYMenu);
                    //Console.WriteLine("corrected pos from: " + Sx + "," + Sy + " to: " + itemScreenPos["x"] + "," + itemScreenPos["y"]);
                }

                if (!CharConfig.UseTeleport || (CharConfig.UseTeleport && Form1_0.Town_0.GetInTown()))
                {
                    Form1_0.KeyMouse_0.MouseMoveTo(itemScreenPos["x"], itemScreenPos["y"]);
                }
                if (CharConfig.UseTeleport && !Form1_0.Town_0.GetInTown())
                {
                    Form1_0.KeyMouse_0.MouseCliccRight(itemScreenPos["x"], itemScreenPos["y"]);

                    //#######
                    if (!AllowFastMove)
                    {
                        LastTimeSinceTeleport = DateTime.Now;
                        TimeSpan ThisTimeCheck = DateTime.Now - LastTimeSinceTeleport;
                        while (Form1_0.PlayerScan_0.xPosFinal == LastX && Form1_0.PlayerScan_0.yPosFinal == LastY && ThisTimeCheck.TotalMilliseconds < 200)
                        {
                            Application.DoEvents();
                            Form1_0.PlayerScan_0.GetPositions();
                            Form1_0.overlayForm.UpdateOverlay();
                            Form1_0.Potions_0.CheckIfWeUsePotion();
                            Form1_0.ItemsStruc_0.GetItems(false);
                            ThisTimeCheck = DateTime.Now - LastTimeSinceTeleport;
                        }
                    }
                    else
                    {
                        Form1_0.SetProcessingTime();
                    }
                    //#######
                }
                Form1_0.PlayerScan_0.GetPositions();
                Form1_0.overlayForm.UpdateOverlay();
                Form1_0.GameStruc_0.CheckChickenGameTime();
                if (AllowPickingItem) Form1_0.ItemsStruc_0.GetItems(true);      //#############
                Form1_0.Potions_0.CheckIfWeUsePotion();
                itemScreenPos = Form1_0.GameStruc_0.World2Screen(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, ThisX, ThisY);
                Application.DoEvents();

                //######
                //moving location is way to far away something might be wrong!
                if (Form1_0.PlayerScan_0.xPosFinal < (ThisX - 300)
                    || Form1_0.PlayerScan_0.xPosFinal > (ThisX + 300)
                    || Form1_0.PlayerScan_0.yPosFinal < (ThisY - 300)
                    || Form1_0.PlayerScan_0.yPosFinal > (ThisY + 300))
                {
                    Form1_0.KeyMouse_0.ReleaseKey(System.Windows.Forms.Keys.E);
                    return false;
                }
                if (ThisX == 0 && ThisY == 0)
                {
                    Form1_0.KeyMouse_0.ReleaseKey(System.Windows.Forms.Keys.E);
                    return false;
                }
                //######

                //not in suposed area, may have taken unwanted tp
                if (Form1_0.PlayerScan_0.levelNo < StartAreaBeforeMoving - 1
                    || Form1_0.PlayerScan_0.levelNo > StartAreaBeforeMoving + 1)
                {
                    Form1_0.overlayForm.ResetMoveToLocation();
                    Form1_0.KeyMouse_0.ReleaseKey(System.Windows.Forms.Keys.E);
                    return false;
                }

                //detect is moving
                if (Form1_0.PlayerScan_0.xPosFinal != LastX
                || Form1_0.PlayerScan_0.yPosFinal != LastY)
                {
                    TryMove = 0;
                }
                else
                {
                    TryMove++;
                }

                //break moving loop
                if (Form1_0.PlayerScan_0.xPosFinal >= (ThisX - MoveAcceptOffset)
                    && Form1_0.PlayerScan_0.xPosFinal <= (ThisX + MoveAcceptOffset)
                    && Form1_0.PlayerScan_0.yPosFinal >= (ThisY - MoveAcceptOffset)
                    && Form1_0.PlayerScan_0.yPosFinal <= (ThisY + MoveAcceptOffset))
                {
                    break;
                }

                //teleport again
                if (AllowFastMove)
                {
                    if (Form1_0.Town_0.GetInTown())
                    {
                        Form1_0.KeyMouse_0.PressKey(CharConfig.KeySkillfastMoveAtTown);
                        AllowFastMove = false;
                    }
                    else
                    {
                        Form1_0.KeyMouse_0.PressKey(CharConfig.KeySkillfastMoveOutsideTown);
                        if (CharConfig.UseTeleport) AllowFastMove = true;
                    }
                    Form1_0.KeyMouse_0.MouseCliccRight(itemScreenPos["x"], itemScreenPos["y"]);
                }

                if (TryMove >= MaxMoveTry)
                {
                    if (!AllowMoveSideWay)
                    {
                        Form1_0.overlayForm.ResetMoveToLocation();
                        Form1_0.KeyMouse_0.ReleaseKey(System.Windows.Forms.Keys.E);
                        return false;
                    }

                    if (!Form1_0.GameStruc_0.IsInGame() || !Form1_0.Running)
                    {
                        Form1_0.overlayForm.ResetMoveToLocation();
                        Form1_0.KeyMouse_0.ReleaseKey(System.Windows.Forms.Keys.E);
                        return false;
                    }
                    if (AllowPickingItem) Form1_0.ItemsStruc_0.GetItems(true);      //#############
                    Form1_0.Potions_0.CheckIfWeUsePotion();
                    if (TryMove2 == 0) Form1_0.KeyMouse_0.MouseMoveTo(Form1_0.ScreenX / 2, Form1_0.ScreenY / 2);
                    if (TryMove2 == 1) Form1_0.KeyMouse_0.MouseMoveTo(Form1_0.ScreenX / 2 - 250, Form1_0.ScreenY / 2);
                    if (TryMove2 == 2) Form1_0.KeyMouse_0.MouseMoveTo(Form1_0.ScreenX / 2 + 250, Form1_0.ScreenY / 2);
                    if (TryMove2 == 3) Form1_0.KeyMouse_0.MouseMoveTo(Form1_0.ScreenX / 2, Form1_0.ScreenY / 2 - 250);
                    if (TryMove2 == 4) Form1_0.KeyMouse_0.MouseMoveTo(Form1_0.ScreenX / 2, Form1_0.ScreenY / 2 + 250);

                    if (!CharConfig.UseTeleport || (CharConfig.UseTeleport && Form1_0.Town_0.GetInTown()))
                    {
                        Form1_0.KeyMouse_0.MouseClicRelease();
                        Form1_0.KeyMouse_0.ReleaseKey(System.Windows.Forms.Keys.E);
                        Form1_0.KeyMouse_0.MouseClicHoldWithoutRelease();
                        Form1_0.KeyMouse_0.PressKeyHold(System.Windows.Forms.Keys.E);
                    }

                    Form1_0.WaitDelay(4);

                    TryMove2++;
                    if (TryMove2 >= MaxMoveTry)
                    {
                        break;
                    }
                }

                LastX = Form1_0.PlayerScan_0.xPosFinal;
                LastY = Form1_0.PlayerScan_0.yPosFinal;
            }

            bool MovedCorrectly = false;
            if (Form1_0.PlayerScan_0.xPosFinal >= (ThisX - MoveAcceptOffset)
                    && Form1_0.PlayerScan_0.xPosFinal <= (ThisX + MoveAcceptOffset)
                    && Form1_0.PlayerScan_0.yPosFinal >= (ThisY - MoveAcceptOffset)
                    && Form1_0.PlayerScan_0.yPosFinal <= (ThisY + MoveAcceptOffset))
            {
                MovedCorrectly = true;
            }

            if (!CharConfig.UseTeleport || (CharConfig.UseTeleport && Form1_0.Town_0.GetInTown()))
            {
                Form1_0.KeyMouse_0.MouseClicRelease();
                Form1_0.KeyMouse_0.ReleaseKey(System.Windows.Forms.Keys.E);
            }
            Form1_0.KeyMouse_0.PressKey(CharConfig.KeySkillDefenseAura);

            //#######
            //finish moving
            if (MovedCorrectly && !AllowFastMove)
            {
                FinishMoving();
            }
            //#######

            Form1_0.overlayForm.ResetMoveToLocation();
            return MovedCorrectly;
        }

        public void FinishMoving()
        {
            int LastX = Form1_0.PlayerScan_0.xPosFinal;
            int LastY = Form1_0.PlayerScan_0.yPosFinal;

            Form1_0.PlayerScan_0.GetPositions();
            Form1_0.overlayForm.UpdateOverlay();
            bool IsMoving = true;
            int Triess = 0;

            while (IsMoving)
            {
                if (!Form1_0.GameStruc_0.IsInGame() || !Form1_0.Running)
                {
                    return;
                }

                if (Form1_0.PlayerScan_0.xPosFinal == LastX
                    || Form1_0.PlayerScan_0.yPosFinal == LastY)
                {
                    IsMoving = false;
                }

                Application.DoEvents();
                Form1_0.PlayerScan_0.GetPositions();
                Form1_0.Potions_0.CheckIfWeUsePotion();
                Form1_0.ItemsStruc_0.GetItems(false);
                Form1_0.overlayForm.UpdateOverlay();

                LastX = Form1_0.PlayerScan_0.xPosFinal;
                LastY = Form1_0.PlayerScan_0.yPosFinal;

                Triess++;
                if (Triess >= 20) IsMoving = false;
            }
        }

        //This will FAST move to a direct location -> no pathfinding (used for attacking mobs)
        public bool MoveToLocationAttack(int ThisX, int ThisY)
        {
            Form1_0.UIScan_0.readUI();
            if (Form1_0.UIScan_0.leftMenu || Form1_0.UIScan_0.rightMenu) Form1_0.UIScan_0.CloseAllUIMenu();

            Form1_0.PlayerScan_0.GetPositions();
            Form1_0.overlayForm.SetMoveToPoint(new System.Drawing.Point(ThisX, ThisY));
            StartAreaBeforeMoving = Form1_0.PlayerScan_0.levelNo;
            //Form1_0.GameStruc_0.CheckChickenGameTime();

            //######
            //moving location is way to far away something might be wrong!
            if (Form1_0.PlayerScan_0.xPosFinal < (ThisX - 300)
                || Form1_0.PlayerScan_0.xPosFinal > (ThisX + 300)
                || Form1_0.PlayerScan_0.yPosFinal < (ThisY - 300)
                || Form1_0.PlayerScan_0.yPosFinal > (ThisY + 300))
            {
                return false;
            }
            if (ThisX == 0 && ThisY == 0) return false;
            //######

            //no need to move we are close already!
            if (Form1_0.PlayerScan_0.xPosFinal >= (ThisX - MoveAcceptOffset)
                && Form1_0.PlayerScan_0.xPosFinal <= (ThisX + MoveAcceptOffset)
                && Form1_0.PlayerScan_0.yPosFinal >= (ThisY - MoveAcceptOffset)
                && Form1_0.PlayerScan_0.yPosFinal <= (ThisY + MoveAcceptOffset))
            {
                Form1_0.overlayForm.ResetMoveToLocation();
                return true;
            }

            if (!Form1_0.GameStruc_0.IsInGame() || !Form1_0.Running)
            {
                Form1_0.overlayForm.ResetMoveToLocation();
                return false;
            }

            Dictionary<string, int> itemScreenPos = Form1_0.GameStruc_0.World2Screen(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, ThisX, ThisY);

            if (!CharConfig.UseTeleport || (CharConfig.UseTeleport && Form1_0.Town_0.GetInTown()))
            {
                Form1_0.KeyMouse_0.MouseClicHoldWithoutRelease();
                Form1_0.KeyMouse_0.PressKeyHold(System.Windows.Forms.Keys.E);
            }
            if (Form1_0.Town_0.GetInTown()) Form1_0.KeyMouse_0.PressKey(CharConfig.KeySkillfastMoveAtTown);
            else Form1_0.KeyMouse_0.PressKey(CharConfig.KeySkillfastMoveOutsideTown);

            //calculate new Y clicking offset, else it will clic on bottom menu items
            if (itemScreenPos["y"] >= (Form1_0.ScreenY - Form1_0.ScreenYMenu))
            {
                int DiffX = Form1_0.CenterX - itemScreenPos["x"];
                itemScreenPos["x"] = (int)(itemScreenPos["x"] + (DiffX / 6));
                itemScreenPos["y"] = (Form1_0.ScreenY - Form1_0.ScreenYMenu);
                //Console.WriteLine("corrected pos from: " + Sx + "," + Sy + " to: " + itemScreenPos["x"] + "," + itemScreenPos["y"]);
            }

            Form1_0.WaitDelay(5); //wait a little bit, we just casted attack

            if (!CharConfig.UseTeleport || (CharConfig.UseTeleport && Form1_0.Town_0.GetInTown())) Form1_0.KeyMouse_0.MouseMoveTo(itemScreenPos["x"], itemScreenPos["y"]);
            if (CharConfig.UseTeleport && !Form1_0.Town_0.GetInTown()) Form1_0.KeyMouse_0.MouseCliccRightAttackMove(itemScreenPos["x"], itemScreenPos["y"]);

            //#######
            Form1_0.PlayerScan_0.GetPositions();
            Form1_0.overlayForm.UpdateOverlay();
            Form1_0.GameStruc_0.CheckChickenGameTime();
            Form1_0.Potions_0.CheckIfWeUsePotion();
            Form1_0.ItemsStruc_0.GetItems(true);
            Form1_0.SetProcessingTime();
            //Application.DoEvents();
            //#######

            //######
            //moving location is way to far away something might be wrong!
            if (Form1_0.PlayerScan_0.xPosFinal < (ThisX - 300)
                || Form1_0.PlayerScan_0.xPosFinal > (ThisX + 300)
                || Form1_0.PlayerScan_0.yPosFinal < (ThisY - 300)
                || Form1_0.PlayerScan_0.yPosFinal > (ThisY + 300))
            {
                Form1_0.KeyMouse_0.ReleaseKey(System.Windows.Forms.Keys.E);
                return false;
            }
            if (ThisX == 0 && ThisY == 0)
            {
                Form1_0.KeyMouse_0.ReleaseKey(System.Windows.Forms.Keys.E);
                return false;
            }
            //######

            //not in suposed area, may have taken unwanted tp
            if (Form1_0.PlayerScan_0.levelNo < StartAreaBeforeMoving - 1
                || Form1_0.PlayerScan_0.levelNo > StartAreaBeforeMoving + 1)
            {
                Form1_0.overlayForm.ResetMoveToLocation();
                Form1_0.KeyMouse_0.ReleaseKey(System.Windows.Forms.Keys.E);
                return false;
            }

            bool MovedCorrectly = false;
            if (Form1_0.PlayerScan_0.xPosFinal >= (ThisX - MoveAcceptOffset)
                    && Form1_0.PlayerScan_0.xPosFinal <= (ThisX + MoveAcceptOffset)
                    && Form1_0.PlayerScan_0.yPosFinal >= (ThisY - MoveAcceptOffset)
                    && Form1_0.PlayerScan_0.yPosFinal <= (ThisY + MoveAcceptOffset))
            {
                MovedCorrectly = true;
            }

            if (!CharConfig.UseTeleport || (CharConfig.UseTeleport && Form1_0.Town_0.GetInTown()))
            {
                //Form1_0.KeyMouse_0.MouseClicRelease();
                Form1_0.KeyMouse_0.ReleaseKey(System.Windows.Forms.Keys.E);
            }
            Form1_0.KeyMouse_0.PressKey(CharConfig.KeySkillDefenseAura);

            
            Form1_0.overlayForm.ResetMoveToLocation();
            return MovedCorrectly;
        }
    }
}
