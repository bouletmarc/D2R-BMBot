using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static MapAreaStruc;

public class Mover
{
    Form1 Form1_0;

    public int MaxMoveTry = 5; //default is 5
    public int MoveAcceptOffset = 4;
    public long StartAreaBeforeMoving = 0;

    public bool AllowFastMove = false;

    public DateTime LastTimeSinceTeleport = DateTime.Now;

    public void SetForm1(Form1 form1_1)
    {
        Form1_0 = form1_1;
    }

    public bool IsPositionNearOf(int ThisX, int ThisY, int Offset)
    {
        //Form1_0.PlayerScan_0.GetPositions();
        if (Form1_0.PlayerScan_0.xPosFinal >= (ThisX - Offset)
            && Form1_0.PlayerScan_0.xPosFinal <= (ThisX + Offset)
            && Form1_0.PlayerScan_0.yPosFinal >= (ThisY - Offset)
            && Form1_0.PlayerScan_0.yPosFinal <= (ThisY + Offset))
        {
            return true;
        }
        return false;
    }

    public bool MovingToInteract = false;

    //This will move to a direct location -> no pathfinding
    public bool MoveToLocation(int ThisX, int ThisY, bool AllowPickingItem = false, bool AllowMoveSideWay = true)
    {
        if (!Form1_0.Running || !Form1_0.GameStruc_0.IsInGame()) return false;

        Form1_0.UIScan_0.readUI();
        if (Form1_0.UIScan_0.leftMenu || Form1_0.UIScan_0.rightMenu && !MovingToInteract) Form1_0.UIScan_0.CloseAllUIMenu();
        //if (Form1_0.UIScan_0.leftMenu || Form1_0.UIScan_0.rightMenu) Form1_0.UIScan_0.CloseAllUIMenu();
        if (Form1_0.UIScan_0.quitMenu) Form1_0.UIScan_0.CloseUIMenu("quitMenu");

        Form1_0.PlayerScan_0.GetPositions();
        Form1_0.overlayForm.SetMoveToPoint(new System.Drawing.Point(ThisX, ThisY));
        StartAreaBeforeMoving = Form1_0.PlayerScan_0.levelNo;
        //Form1_0.GameStruc_0.CheckChickenGameTime();

        //######
        //moving location is way to far away something might be wrong!
        if (!IsPositionNearOf(ThisX, ThisY, 300)) return false;
        if (ThisX == 0 && ThisY == 0) return false;
        //######

        //no need to move we are close already!
        if (IsPositionNearOf(ThisX, ThisY, MoveAcceptOffset))
        {
            Form1_0.overlayForm.ResetMoveToLocation();
            return true;
        }

        if (!Form1_0.GameStruc_0.IsInGame() || !Form1_0.Running)
        {
            Form1_0.overlayForm.ResetMoveToLocation();
            return false;
        }

        //fix town act5 stuck near bolder
        if (Form1_0.Town_0.GetInTown() && IsPositionNearOf(5093, 5034, 2))
        {
            MoveToLocationAttack(5100, 5021);
        }

        int TryMove = 0;
        int TryMove2 = 0;
        int LastX = Form1_0.PlayerScan_0.xPosFinal;
        int LastY = Form1_0.PlayerScan_0.yPosFinal;
        Position itemScreenPos = Form1_0.GameStruc_0.World2Screen(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, ThisX, ThisY);

        if (Form1_0.Town_0.GetInTown()) Form1_0.KeyMouse_0.PressKey(CharConfig.KeySkillfastMoveAtTown);
        else Form1_0.KeyMouse_0.PressKey(CharConfig.KeySkillfastMoveOutsideTown);

        if (!CharConfig.UseTeleport || (CharConfig.UseTeleport && Form1_0.Town_0.GetInTown()))
        {
            Form1_0.KeyMouse_0.MouseMoveTo_RealPos(itemScreenPos.X, itemScreenPos.Y);
            Form1_0.KeyMouse_0.MouseClicHoldWithoutRelease();
            Form1_0.KeyMouse_0.PressKeyHold(CharConfig.KeyForceMovement);
        }
        while (true)
        {
            if (!Form1_0.Running || !Form1_0.GameStruc_0.IsInGame()) return false;

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
                    if (IsPositionNearOf(ThisX, ThisY, 21)) AllowFastMove = false;
                }
            }

            if (!CharConfig.UseTeleport || (CharConfig.UseTeleport && Form1_0.Town_0.GetInTown()))
            {
                Form1_0.KeyMouse_0.MouseMoveTo_RealPos(itemScreenPos.X, itemScreenPos.Y);
            }
            if (CharConfig.UseTeleport && !Form1_0.Town_0.GetInTown())
            {
                Form1_0.KeyMouse_0.MouseCliccRight_RealPos(itemScreenPos.X, itemScreenPos.Y);

                //#######
                if (!AllowFastMove)
                {
                    LastTimeSinceTeleport = DateTime.Now;
                    TimeSpan ThisTimeCheck = DateTime.Now - LastTimeSinceTeleport;
                    while (Form1_0.PlayerScan_0.xPosFinal == LastX && Form1_0.PlayerScan_0.yPosFinal == LastY && ThisTimeCheck.TotalMilliseconds < 200)
                    {
                        if (!Form1_0.Running || !Form1_0.GameStruc_0.IsInGame()) return false;
                        Application.DoEvents();
                        Form1_0.PlayerScan_0.GetPositions();
                        Form1_0.overlayForm.UpdateOverlay();
                        Form1_0.Potions_0.CheckIfWeUsePotion();
                        Form1_0.ItemsStruc_0.GetItems(false);
                        ThisTimeCheck = DateTime.Now - LastTimeSinceTeleport;
                    }
                    if (!Form1_0.Running || !Form1_0.GameStruc_0.IsInGame()) return false;
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

            Form1_0.ItemsStruc_0.AvoidItemsOnGroundPointerList.Clear();

            //######
            //moving location is way to far away something might be wrong!
            if (!IsPositionNearOf(ThisX, ThisY, 300))
            {
                Form1_0.KeyMouse_0.ReleaseKey(CharConfig.KeyForceMovement);
                return false;
            }
            if (ThisX == 0 && ThisY == 0)
            {
                Form1_0.KeyMouse_0.ReleaseKey(CharConfig.KeyForceMovement);
                return false;
            }
            //######

            //not in suposed area, may have taken unwanted tp
            if (Form1_0.PlayerScan_0.levelNo < StartAreaBeforeMoving - 1
                || Form1_0.PlayerScan_0.levelNo > StartAreaBeforeMoving + 1)
            {
                Form1_0.overlayForm.ScanningOverlayItems = true; //try rescanning overlay if there was too much lags
                Form1_0.overlayForm.ResetMoveToLocation();
                Form1_0.KeyMouse_0.ReleaseKey(CharConfig.KeyForceMovement);
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
            if (IsPositionNearOf(ThisX, ThisY, MoveAcceptOffset)) break;

            //teleport again
            /*Form1_0.PlayerScan_0.GetPositions();
            if (AllowFastMove && !IsPositionNearOf(ThisX, ThisY, MoveAcceptOffset))
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

                itemScreenPos = Form1_0.GameStruc_0.World2Screen(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, ThisX, ThisY);

                Form1_0.KeyMouse_0.MouseCliccRight_RealPos(itemScreenPos.X, itemScreenPos.Y);
            }*/

            if (TryMove >= MaxMoveTry && (!CharConfig.UseTeleport || (CharConfig.UseTeleport && Form1_0.Town_0.GetInTown())))
            {
                if (!AllowMoveSideWay)
                {
                    Form1_0.ItemsStruc_0.AvoidItemsOnGroundPointerList.Clear();
                    Form1_0.overlayForm.ResetMoveToLocation();
                    Form1_0.KeyMouse_0.ReleaseKey(CharConfig.KeyForceMovement);
                    return false;
                }

                if (!Form1_0.GameStruc_0.IsInGame() || !Form1_0.Running)
                {
                    Form1_0.overlayForm.ResetMoveToLocation();
                    Form1_0.KeyMouse_0.ReleaseKey(CharConfig.KeyForceMovement);
                    return false;
                }
                if (AllowPickingItem) Form1_0.ItemsStruc_0.GetItems(true);      //#############
                Form1_0.Potions_0.CheckIfWeUsePotion();
                /*if (TryMove2 == 0) Form1_0.KeyMouse_0.MouseMoveTo(Form1_0.ScreenX / 2, Form1_0.ScreenY / 2);
                if (TryMove2 == 1) Form1_0.KeyMouse_0.MouseMoveTo(Form1_0.ScreenX / 2 - 250, Form1_0.ScreenY / 2);
                if (TryMove2 == 2) Form1_0.KeyMouse_0.MouseMoveTo(Form1_0.ScreenX / 2 + 250, Form1_0.ScreenY / 2);
                if (TryMove2 == 3) Form1_0.KeyMouse_0.MouseMoveTo(Form1_0.ScreenX / 2, Form1_0.ScreenY / 2 - 250);
                if (TryMove2 == 4) Form1_0.KeyMouse_0.MouseMoveTo(Form1_0.ScreenX / 2, Form1_0.ScreenY / 2 + 250);*/

                Form1_0.KeyMouse_0.MouseClicRelease();
                Form1_0.KeyMouse_0.ReleaseKey(CharConfig.KeyForceMovement);
                Form1_0.KeyMouse_0.MouseClicHoldWithoutRelease();
                Form1_0.KeyMouse_0.PressKeyHold(CharConfig.KeyForceMovement);

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
        if (!Form1_0.Running || !Form1_0.GameStruc_0.IsInGame()) return false;

        bool MovedCorrectly = false;
        if (IsPositionNearOf(ThisX, ThisY, MoveAcceptOffset)) MovedCorrectly = true;

        if (!CharConfig.UseTeleport || (CharConfig.UseTeleport && Form1_0.Town_0.GetInTown()))
        {
            Form1_0.KeyMouse_0.MouseClicRelease();
            Form1_0.KeyMouse_0.ReleaseKey(CharConfig.KeyForceMovement);
        }
        Form1_0.KeyMouse_0.PressKey(CharConfig.KeySkillDefenseAura);

        Form1_0.ItemsStruc_0.AvoidItemsOnGroundPointerList.Clear();
        Form1_0.overlayForm.ResetMoveToLocation();
        return MovedCorrectly;
    }

    //This will FAST move to a direct location -> no pathfinding (used for attacking mobs)
    public bool MoveToLocationAttack(int ThisX, int ThisY)
    {
        if (!Form1_0.Running || !Form1_0.GameStruc_0.IsInGame()) return false;

        Form1_0.UIScan_0.readUI();
        if (Form1_0.UIScan_0.leftMenu || Form1_0.UIScan_0.rightMenu) Form1_0.UIScan_0.CloseAllUIMenu();

        Form1_0.PlayerScan_0.GetPositions();
        Form1_0.overlayForm.SetMoveToPoint(new System.Drawing.Point(ThisX, ThisY));
        StartAreaBeforeMoving = Form1_0.PlayerScan_0.levelNo;
        //Form1_0.GameStruc_0.CheckChickenGameTime();

        //######
        //moving location is way to far away something might be wrong!
        Form1_0.KeyMouse_0.ReleaseKey(CharConfig.KeyForceMovement);
        if (!IsPositionNearOf(ThisX, ThisY, 300)) return false;
        if (ThisX == 0 && ThisY == 0) return false;
        //######

        //no need to move we are close already!
        if (IsPositionNearOf(ThisX, ThisY, MoveAcceptOffset))
        {
            Form1_0.overlayForm.ResetMoveToLocation();
            Form1_0.KeyMouse_0.ReleaseKey(CharConfig.KeyForceMovement);
            return true;
        }

        if (!Form1_0.GameStruc_0.IsInGame() || !Form1_0.Running)
        {
            Form1_0.overlayForm.ResetMoveToLocation();
            Form1_0.KeyMouse_0.ReleaseKey(CharConfig.KeyForceMovement);
            return false;
        }

        Position itemScreenPos = Form1_0.GameStruc_0.World2Screen(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, ThisX, ThisY);


        //if (!CharConfig.UseTeleport || (CharConfig.UseTeleport && Form1_0.Town_0.GetInTown()))
        //{
        Form1_0.KeyMouse_0.MouseMoveTo_RealPos(itemScreenPos.X, itemScreenPos.Y);
        Form1_0.KeyMouse_0.MouseClicHoldWithoutRelease();
        Form1_0.KeyMouse_0.PressKeyHold(CharConfig.KeyForceMovement);
        //}
        if (Form1_0.Town_0.GetInTown()) Form1_0.KeyMouse_0.PressKey(CharConfig.KeySkillfastMoveAtTown);
        else Form1_0.KeyMouse_0.PressKey(CharConfig.KeySkillfastMoveOutsideTown);


        Form1_0.WaitDelay(5); //wait a little bit, we just casted attack

        if (!CharConfig.UseTeleport || (CharConfig.UseTeleport && Form1_0.Town_0.GetInTown())) Form1_0.KeyMouse_0.MouseMoveTo_RealPos(itemScreenPos.X, itemScreenPos.Y);
        if (CharConfig.UseTeleport && !Form1_0.Town_0.GetInTown()) Form1_0.KeyMouse_0.MouseCliccRightAttackMove(itemScreenPos.X, itemScreenPos.Y);

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
        if (!IsPositionNearOf(ThisX, ThisY, 300))
        {
            Form1_0.KeyMouse_0.ReleaseKey(CharConfig.KeyForceMovement);
            return false;
        }
        if (ThisX == 0 && ThisY == 0)
        {
            Form1_0.KeyMouse_0.ReleaseKey(CharConfig.KeyForceMovement);
            return false;
        }
        //######

        //not in suposed area, may have taken unwanted tp
        if (Form1_0.PlayerScan_0.levelNo < StartAreaBeforeMoving - 1
            || Form1_0.PlayerScan_0.levelNo > StartAreaBeforeMoving + 1)
        {
            Form1_0.ItemsStruc_0.AvoidItemsOnGroundPointerList.Clear();
            Form1_0.overlayForm.ScanningOverlayItems = true; //try rescanning overlay if there was too much lags
            Form1_0.overlayForm.ResetMoveToLocation();
            Form1_0.KeyMouse_0.ReleaseKey(CharConfig.KeyForceMovement);
            return false;
        }

        bool MovedCorrectly = false;
        if (IsPositionNearOf(ThisX, ThisY, MoveAcceptOffset)) MovedCorrectly = true;

        //if (!CharConfig.UseTeleport || (CharConfig.UseTeleport && Form1_0.Town_0.GetInTown()))
        //{
        Form1_0.KeyMouse_0.MouseClicRelease();
        Form1_0.KeyMouse_0.ReleaseKey(CharConfig.KeyForceMovement);
        //}
        //Form1_0.KeyMouse_0.PressKey(CharConfig.KeySkillDefenseAura);

        Form1_0.ItemsStruc_0.AvoidItemsOnGroundPointerList.Clear();
        Form1_0.overlayForm.ResetMoveToLocation();
        return MovedCorrectly;
    }
}
