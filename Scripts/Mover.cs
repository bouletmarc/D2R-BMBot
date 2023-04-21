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

        public void SetForm1(Form1 form1_1)
        {
            Form1_0 = form1_1;
        }

        public bool MoveToLocation(int ThisX, int ThisY)
        {
            Form1_0.PlayerScan_0.GetPositions();

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
                }
                else
                {
                    Form1_0.KeyMouse_0.PressKey(CharConfig.KeySkillfastMoveOutsideTown);
                }

                //calculate new Y clicking offset, else it will clic on bottom menu items
                if (itemScreenPos["y"] >= (CharConfig.ScreenY - CharConfig.ScreenYMenu))
                {
                    //int Sx = itemScreenPos["x"];
                    //int Sy = itemScreenPos["y"];
                    int DiffY = itemScreenPos["y"] - (CharConfig.ScreenY - CharConfig.ScreenYMenu);
                    double DiffPercent = (DiffY * 100) / itemScreenPos["y"];
                    //double DiffPercent = (DiffY * 100) / Form1_0.ScreenY;
                    itemScreenPos["x"] = (int) (itemScreenPos["x"] - ((DiffPercent * itemScreenPos["x"]) / 100));
                    itemScreenPos["y"] = (CharConfig.ScreenY - CharConfig.ScreenYMenu);
                    //Console.WriteLine("corrected pos from: " + Sx + "," + Sy + " to: " + itemScreenPos["x"] + "," + itemScreenPos["y"]);
                }

                if (!CharConfig.UseTeleport || (CharConfig.UseTeleport && Form1_0.Town_0.GetInTown()))
                {
                    Form1_0.KeyMouse_0.MouseMoveTo(itemScreenPos["x"], itemScreenPos["y"]);
                }
                if (CharConfig.UseTeleport && !Form1_0.Town_0.GetInTown())
                {
                    Form1_0.KeyMouse_0.MouseCliccRight(itemScreenPos["x"], itemScreenPos["y"]);
                    Form1_0.WaitDelay(6);
                }
                //Form1_0.WaitDelay(2);
                Application.DoEvents();
                Form1_0.PlayerScan_0.GetPositions();
                itemScreenPos = Form1_0.GameStruc_0.World2Screen(Form1_0.PlayerScan_0.xPosFinal, Form1_0.PlayerScan_0.yPosFinal, ThisX, ThisY);
                Application.DoEvents();

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
                    if (!Form1_0.GameStruc_0.IsInGame() || !Form1_0.Running)
                    {
                        return false;
                    }
                    Form1_0.ItemsStruc_0.GetItems(true);
                    Form1_0.Potions_0.CheckIfWeUsePotion();
                    if (TryMove2 == 0) Form1_0.KeyMouse_0.MouseMoveTo(CharConfig.ScreenX / 2, CharConfig.ScreenY / 2);
                    if (TryMove2 == 1) Form1_0.KeyMouse_0.MouseMoveTo(CharConfig.ScreenX / 2 - 250, CharConfig.ScreenY / 2);
                    if (TryMove2 == 2) Form1_0.KeyMouse_0.MouseMoveTo(CharConfig.ScreenX / 2 + 250, CharConfig.ScreenY / 2);
                    if (TryMove2 == 3) Form1_0.KeyMouse_0.MouseMoveTo(CharConfig.ScreenX / 2, CharConfig.ScreenY / 2 - 250);
                    if (TryMove2 == 4) Form1_0.KeyMouse_0.MouseMoveTo(CharConfig.ScreenX / 2, CharConfig.ScreenY / 2 + 250);

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
            }
        }
    }
}
