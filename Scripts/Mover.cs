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


        public bool MoveToLocation(int ThisX, int ThisY, bool AllowPickingItem = false, bool AllowMoveSideWay = true)
        {
            Form1_0.PlayerScan_0.GetPositions();
            StartAreaBeforeMoving = Form1_0.PlayerScan_0.levelNo;

            //no need to move we are close already!
            if (Form1_0.PlayerScan_0.xPosFinal >= (ThisX - MoveAcceptOffset)
                && Form1_0.PlayerScan_0.xPosFinal <= (ThisX + MoveAcceptOffset)
                && Form1_0.PlayerScan_0.yPosFinal >= (ThisY - MoveAcceptOffset)
                && Form1_0.PlayerScan_0.yPosFinal <= (ThisY + MoveAcceptOffset))
            {
                return true;
            }

            if (!Form1_0.GameStruc_0.IsInGame() || !Form1_0.Running)
            {
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
                }

                //calculate new Y clicking offset, else it will clic on bottom menu items
                if (itemScreenPos["y"] >= (Form1_0.ScreenY - Form1_0.ScreenYMenu))
                {
                    //int Sx = itemScreenPos["x"];
                    //int Sy = itemScreenPos["y"];
                    int DiffY = itemScreenPos["y"] - (Form1_0.ScreenY - Form1_0.ScreenYMenu);
                    double DiffPercent = (DiffY * 100) / itemScreenPos["y"];
                    //double DiffPercent = (DiffY * 100) / Form1_0.ScreenY;
                    itemScreenPos["x"] = (int) (itemScreenPos["x"] - ((DiffPercent * itemScreenPos["x"]) / 100));
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
                            ThisTimeCheck = DateTime.Now - LastTimeSinceTeleport;
                        }
                    }
                    //#######

                    //Form1_0.WaitDelay(10);
                }
                //Form1_0.WaitDelay(2);
                Application.DoEvents();
                Form1_0.PlayerScan_0.GetPositions();
                if (AllowPickingItem) Form1_0.ItemsStruc_0.GetItems(true);      //#############
                Form1_0.Potions_0.CheckIfWeUsePotion();
                itemScreenPos = Form1_0.GameStruc_0.World2Screen(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, ThisX, ThisY);
                Application.DoEvents();

                //not in suposed area, may have taken unwanted tp
                if (Form1_0.PlayerScan_0.levelNo < StartAreaBeforeMoving - 1
                    || Form1_0.PlayerScan_0.levelNo > StartAreaBeforeMoving + 1)
                {
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
                if (TryMove >= MaxMoveTry)
                {
                    if (!AllowMoveSideWay) return false;

                    if (!Form1_0.GameStruc_0.IsInGame() || !Form1_0.Running)
                    {
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
            if (MovedCorrectly)
            {
                FinishMoving();
            }
            //#######

            return MovedCorrectly;
        }

        public void FinishMoving()
        {
            //Form1_0.WaitDelay(5);
            Form1_0.PlayerScan_0.GetPositions();
            bool IsMoving = true;
            int LastX = Form1_0.PlayerScan_0.xPosFinal;
            int LastY = Form1_0.PlayerScan_0.yPosFinal;
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
                //Form1_0.WaitDelay(5);
                Form1_0.PlayerScan_0.GetPositions();

                LastX = Form1_0.PlayerScan_0.xPosFinal;
                LastY = Form1_0.PlayerScan_0.yPosFinal;

                Triess++;
                if (Triess >= 20) IsMoving = false;
            }
        }
    }
}
