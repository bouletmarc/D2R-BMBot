using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace app
{
    public class Potions
    {
        Form1 Form1_0;

        public int SameHPCount = 0;
        public int GainingHPCount = 0;
        public long PlayerHPLast = 0;

        public int SameMANACount = 0;
        public int GainingMANACount = 0;
        public long PlayerMANALast = 0;

        public int MercGainingHPCount = 0;
        public long MercHPLast = 0;
        public int MercSameHPCount = 0;
        public bool CanUseSkillForRegen = true;

        public void SetForm1(Form1 form1_1)
        {
            Form1_0 = form1_1;
        }

        public void CheckIfWeUsePotion()
        {
            if (Form1_0.PlayerScan_0.PlayerMaxHP == 0 || Form1_0.PlayerScan_0.PlayerMaxMana == 0)
            {
                return;
            }

            if (Form1_0.Town_0.Towning)
            {
                return;
            }

            Form1_0.PlayerScan_0.GetPositions();
            CheckHPAndManaMax();

            //take hp
            if (((Form1_0.PlayerScan_0.PlayerHP * 100) / Form1_0.PlayerScan_0.PlayerMaxHP) <= CharConfig.TakeHPPotUnder)
            {
                if (((Form1_0.PlayerScan_0.PlayerHP * 100) / Form1_0.PlayerScan_0.PlayerMaxHP) <= CharConfig.TakeRVPotUnder)
                {
                    TakePotion(2);
                }
                else
                {
                    //take hp pot only if we are not gaining hp already/haven't taken potion yet
                    if (GainingHPCount == 0)
                    {
                        TakePotion(0);
                    }
                }
            }

            Form1_0.PlayerScan_0.GetPositions();

            //chicken
            if (((Form1_0.PlayerScan_0.PlayerHP * 100) / Form1_0.PlayerScan_0.PlayerMaxHP) < CharConfig.ChickenHP)
            {
                if (!Form1_0.Town_0.GetInTown())
                {
                    Chicken();
                    return;
                }
            }

            //take mana
            if (((Form1_0.PlayerScan_0.PlayerMana * 100) / Form1_0.PlayerScan_0.PlayerMaxMana) <= CharConfig.TakeManaPotUnder)
            {
                //take mana pot only if we are not gaining mana already/haven't taken potion yet
                if (GainingMANACount == 0)
                {
                    TakePotion(1);
                }
            }

            //Check Merc
            /*if (Form1_0.UsingMerc)
            {
                Form1_0.MercStruc_0.GetMercInfos();
                if (Form1_0.MercStruc_0.MercAlive)
                {
                    CheckHPMerc();
                    if (((Form1_0.MercStruc_0.MercHP * 100) / Form1_0.MercStruc_0.MercMaxHP) <= MercTakeHPPotUnder)
                    {
                        //take hp pot only if we are not gaining hp already/haven't taken potion yet
                        if (MercGainingHPCount == 0)
                        {
                            TakePotion(1, true);  //merc send potion
                        }
                    }
                }
            }*/
        }

        public void TakePotion(int PotToTake, bool SendToMerc = false)
        {
            //Take HP Pot
            if (PotToTake == 0)
            {
                bool UsedPot = false;
                for (int i = 0; i < CharConfig.BeltPotTypeToHave.Length; i++)
                {
                    if (CharConfig.BeltPotTypeToHave[i] == 0) //Type equal 0
                    {
                        if (Form1_0.BeltStruc_0.BeltHaveItems[i] == 1)
                        {
                            PressPotionKey(i, SendToMerc);
                            UsedPot = true;
                            i = CharConfig.BeltPotTypeToHave.Length;
                        }
                    }
                }
                if (!UsedPot)
                {
                    Form1_0.Town_0.FastTowning = true;
                    Form1_0.Town_0.GoToTown();
                    return;
                }
            }
            //Take Mana Pot
            if (PotToTake == 1)
            {
                bool UsedPot = false;
                for (int i = 0; i < CharConfig.BeltPotTypeToHave.Length; i++)
                {
                    if (CharConfig.BeltPotTypeToHave[i] == 1) //Type equal 1
                    {
                        if (Form1_0.BeltStruc_0.BeltHaveItems[i] == 1)
                        {
                            PressPotionKey(i, SendToMerc);
                            UsedPot = true;
                            i = CharConfig.BeltPotTypeToHave.Length;
                        }
                    }
                }
                if (!UsedPot)
                {
                    Form1_0.Town_0.FastTowning = true;
                    Form1_0.Town_0.GoToTown();
                    return;
                }
            }
            //Take RV Pot
            if (PotToTake == 2)
            {
                bool UsedPot = false;
                for (int i = 0; i < CharConfig.BeltPotTypeToHave.Length; i++)
                {
                    if (CharConfig.BeltPotTypeToHave[i] == 2 || CharConfig.BeltPotTypeToHave[i] == 3) //Type equal 2 or 3
                    {
                        if (Form1_0.BeltStruc_0.BeltHaveItems[i] == 1)
                        {
                            PressPotionKey(i, SendToMerc);
                            UsedPot = true;
                            i = CharConfig.BeltPotTypeToHave.Length;
                        }
                    }
                }
                if (!UsedPot)
                {
                    Form1_0.Town_0.FastTowning = true;
                    Form1_0.Town_0.GoToTown();
                    return;
                }
            }

            Form1_0.ItemsStruc_0.GetItems(false);
        }

        public void Chicken()
        {
            Form1_0.method_1("Leaving reason: Chicken HP", Color.Red);
            Form1_0.LeaveGame(false);
        }

        public void PressPotionKey(int i, bool SendToMerc)
        {
            if (i == 0)
            {
                if (SendToMerc) Form1_0.KeyMouse_0.PressSHIFT();
                Form1_0.KeyMouse_0.PressKey(System.Windows.Forms.Keys.D1);
                if (SendToMerc) Form1_0.KeyMouse_0.ReleaseSHIFT();
            }
            if (i == 1)
            {
                if (SendToMerc) Form1_0.KeyMouse_0.PressSHIFT();
                Form1_0.KeyMouse_0.PressKey(System.Windows.Forms.Keys.D2);
                if (SendToMerc) Form1_0.KeyMouse_0.ReleaseSHIFT();
            }
            if (i == 2)
            {
                if (SendToMerc) Form1_0.KeyMouse_0.PressSHIFT();
                Form1_0.KeyMouse_0.PressKey(System.Windows.Forms.Keys.D3);
                if (SendToMerc) Form1_0.KeyMouse_0.ReleaseSHIFT();
            }
            if (i == 3)
            {
                if (SendToMerc) Form1_0.KeyMouse_0.PressSHIFT();
                Form1_0.KeyMouse_0.PressKey(System.Windows.Forms.Keys.D4);
                if (SendToMerc) Form1_0.KeyMouse_0.ReleaseSHIFT();
            }
        }

        public void CheckHPMerc()
        {
            if (Form1_0.MercStruc_0.MercHP < MercHPLast)
            {
                MercGainingHPCount = 0;
                MercSameHPCount = 0;
            }
            if (Form1_0.MercStruc_0.MercHP >= MercHPLast)
            {
                if (MercGainingHPCount < 250)
                {
                    MercGainingHPCount++;
                }
                if (Form1_0.MercStruc_0.MercHP == MercHPLast)
                {
                    if (MercSameHPCount < 45)
                    {
                        MercSameHPCount++;
                    }
                }
                else
                {
                    MercSameHPCount = 0;
                }

                //Set Higher HP
                if (Form1_0.MercStruc_0.MercHP > Form1_0.MercStruc_0.MercMaxHP)
                {
                    Form1_0.MercStruc_0.MercMaxHP = Form1_0.MercStruc_0.MercHP;
                }
            }
            //Set Lower HP
            if (Form1_0.MercStruc_0.MercHP == MercHPLast && MercSameHPCount >= 45)
            {
                if (Form1_0.MercStruc_0.MercHP < Form1_0.MercStruc_0.MercMaxHP)
                {
                    Form1_0.MercStruc_0.MercMaxHP = Form1_0.MercStruc_0.MercHP;
                }
            }
        }

        public void CheckHPAndManaMax()
        {
            //if (PlayerHP > PlayerMaxHP) PlayerMaxHP = PlayerHP;
            //if (PlayerMana > PlayerMaxMana) PlayerMaxMana = PlayerMana;

            //############################
            if (Form1_0.PlayerScan_0.PlayerHP < PlayerHPLast)
            {
                if (CanUseSkillForRegen)
                {
                    Form1_0.KeyMouse_0.PressKey(CharConfig.KeySkillLifeAura);
                }
                GainingHPCount = 0;
                SameHPCount = 0;
            }
            if (Form1_0.PlayerScan_0.PlayerHP >= PlayerHPLast)
            {
                if (GainingHPCount < 250)
                {
                    GainingHPCount++;
                }
                if (Form1_0.PlayerScan_0.PlayerHP == PlayerHPLast)
                {
                    if (SameHPCount < 45)
                    {
                        SameHPCount++;
                    }
                }
                else
                {
                    if (CanUseSkillForRegen)
                    {
                        Form1_0.KeyMouse_0.PressKey(CharConfig.KeySkillLifeAura);
                    }
                    SameHPCount = 0;
                }

                //Set Higher HP
                if (Form1_0.PlayerScan_0.PlayerHP > Form1_0.PlayerScan_0.PlayerMaxHP)
                {
                    Form1_0.PlayerScan_0.PlayerMaxHP = Form1_0.PlayerScan_0.PlayerHP;
                }
            }
            //Set Lower HP
            if (Form1_0.PlayerScan_0.PlayerHP == PlayerHPLast && SameHPCount >= 45)
            {
                Form1_0.KeyMouse_0.PressKey(CharConfig.KeySkillDefenseAura);
                if (Form1_0.PlayerScan_0.PlayerHP < Form1_0.PlayerScan_0.PlayerMaxHP)
                {
                    Form1_0.PlayerScan_0.PlayerMaxHP = Form1_0.PlayerScan_0.PlayerHP;
                }
            }
            //############################
            if (Form1_0.PlayerScan_0.PlayerMana < PlayerMANALast)
            {
                GainingMANACount = 0;
                SameMANACount = 0;
            }
            if (Form1_0.PlayerScan_0.PlayerMana >= PlayerMANALast)
            {
                if (GainingMANACount < 250)
                {
                    GainingMANACount++;
                }
                if (Form1_0.PlayerScan_0.PlayerMana == PlayerMANALast)
                {
                    if (SameMANACount < 55)
                    {
                        SameMANACount++;
                    }
                }
                else
                {
                    SameMANACount = 0;
                }

                //Set Higher Mana
                if (Form1_0.PlayerScan_0.PlayerMana > Form1_0.PlayerScan_0.PlayerMaxMana)
                {
                    Form1_0.PlayerScan_0.PlayerMaxMana = Form1_0.PlayerScan_0.PlayerMana;
                }
            }
            //Set Lower Mana
            if (Form1_0.PlayerScan_0.PlayerMana == PlayerMANALast && SameMANACount >= 55)
            {
                if (Form1_0.PlayerScan_0.PlayerMana < Form1_0.PlayerScan_0.PlayerMaxMana)
                {
                    Form1_0.PlayerScan_0.PlayerMaxMana = Form1_0.PlayerScan_0.PlayerMana;
                }
            }
            //############################

            PlayerHPLast = Form1_0.PlayerScan_0.PlayerHP;
            PlayerMANALast = Form1_0.PlayerScan_0.PlayerMana;
        }
    }
}
