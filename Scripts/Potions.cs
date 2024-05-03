using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

    public bool ForceLeave = false;

    public DateTime LastTimeSinceUsedHPPot = DateTime.Now;
    public DateTime LastTimeSinceUsedManaPot = DateTime.Now;

    public List<int> MercHPList = new List<int>();

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

        if (Form1_0.Town_0.Towning && Form1_0.Town_0.IsInTown)
        {
            return;
        }

        if (!Form1_0.GameStruc_0.IsInGame()) return;

        Form1_0.PlayerScan_0.GetPositions();
        CheckHPAndManaMax();


        //dead leave game
        if (Form1_0.PlayerScan_0.PlayerDead || ForceLeave || Form1_0.PlayerScan_0.PlayerHP == 0)
        {
            ForceLeave = true;
            Form1_0.BaalLeech_0.SearchSameGamesAsLastOne = false;
            Form1_0.LeaveGame(false);
            Form1_0.IncreaseDeadCount();
            return;
            //Chicken();
        }

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
                //TakePotion(0);
            }
        }

        Form1_0.PlayerScan_0.GetPositions();

        //dead leave game
        if (Form1_0.PlayerScan_0.PlayerDead || ForceLeave)
        {
            ForceLeave = true;
            Form1_0.BaalLeech_0.SearchSameGamesAsLastOne = false;
            Form1_0.LeaveGame(false);
            Form1_0.IncreaseDeadCount();
            return;
            //Chicken();
        }

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
            //TakePotion(1);
        }

        //Check Merc
        if (CharConfig.UsingMerc)
        {
            Form1_0.MercStruc_0.GetMercInfos();
            if (Form1_0.MercStruc_0.MercAlive)
            {
                CheckHPMerc();
                int ThisMercHP = (int) ((Form1_0.MercStruc_0.MercHP * 100.0) / Form1_0.MercStruc_0.MercMaxHP);
                MercHPList.Add(ThisMercHP);
                if (MercHPList.Count > 10) MercHPList.RemoveAt(0);
                int MercHPAverage = 0;
                for (int i = 0; i < MercHPList.Count; i++) MercHPAverage += MercHPList[i];
                MercHPAverage = MercHPAverage / MercHPList.Count;

                if (MercHPAverage <= CharConfig.MercTakeHPPotUnder)
                {
                    //take hp pot only if we are not gaining hp already/haven't taken potion yet
                    if (MercGainingHPCount == 0)
                    {
                        TakePotion(1, true);  //merc send potion
                    }
                }
            }
            else
            {
                if (!Form1_0.Town_0.GetInTown() && CharConfig.TownIfMercDead && (Form1_0.PlayerScan_0.PlayerGoldInventory + Form1_0.PlayerScan_0.PlayerGoldInStash) >= 75000)
                {
                    Form1_0.Town_0.FastTowning = true;
                    Form1_0.Town_0.GoToTown();
                }
            }
        }
    }

    public void TakePotion(int PotToTake, bool SendToMerc = false)
    {
        //Take HP Pot
        if (PotToTake == 0)
        {
            bool UsedPot = false;
            TimeSpan ThisTimeCheck = DateTime.Now - LastTimeSinceUsedHPPot;
            if (ThisTimeCheck.TotalMilliseconds > CharConfig.TakeHPPotionDelay)
            {
                for (int i = 0; i < CharConfig.BeltPotTypeToHave.Length; i++)
                {
                    if (CharConfig.BeltPotTypeToHave[i] == 0) //Type equal 0
                    {
                        if (Form1_0.BeltStruc_0.BeltHaveItems[i] == 1)
                        {
                            PressPotionKey(i, SendToMerc);
                            UsedPot = true;
                            LastTimeSinceUsedHPPot = DateTime.Now;
                            Form1_0.BeltStruc_0.CheckForMissingPotions();
                            Form1_0.PlayerScan_0.GetPositions();
                            i = CharConfig.BeltPotTypeToHave.Length;
                        }
                    }
                }
            }
            else
            {
                UsedPot = true;
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
            TimeSpan ThisTimeCheck = DateTime.Now - LastTimeSinceUsedManaPot;
            if (ThisTimeCheck.TotalMilliseconds > CharConfig.TakeManaPotionDelay)
            {
                for (int i = 0; i < CharConfig.BeltPotTypeToHave.Length; i++)
                {
                    if (CharConfig.BeltPotTypeToHave[i] == 1) //Type equal 1
                    {
                        if (Form1_0.BeltStruc_0.BeltHaveItems[i] == 1)
                        {
                            PressPotionKey(i, SendToMerc);
                            UsedPot = true;
                            LastTimeSinceUsedManaPot = DateTime.Now;
                            Form1_0.BeltStruc_0.CheckForMissingPotions();
                            Form1_0.PlayerScan_0.GetPositions();
                            i = CharConfig.BeltPotTypeToHave.Length;
                        }
                    }
                }
            }
            else
            {
                UsedPot = true;
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
                        Form1_0.BeltStruc_0.CheckForMissingPotions();
                        Form1_0.PlayerScan_0.GetPositions();
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

        Form1_0.TotalChickenCount++;
        Form1_0.LabelChickenCount.Text = Form1_0.TotalChickenCount.ToString();
    }

    public void PressPotionKey(int i, bool SendToMerc)
    {
        if (i == 0)
        {
            if (SendToMerc) Form1_0.KeyMouse_0.PressPotionKeyMerc(CharConfig.KeyPotion1);
            else Form1_0.KeyMouse_0.PressPotionKey(CharConfig.KeyPotion1);
        }
        if (i == 1)
        {
            if (SendToMerc) Form1_0.KeyMouse_0.PressPotionKeyMerc(CharConfig.KeyPotion2);
            else Form1_0.KeyMouse_0.PressPotionKey(CharConfig.KeyPotion2);
        }
        if (i == 2)
        {
            if (SendToMerc) Form1_0.KeyMouse_0.PressPotionKeyMerc(CharConfig.KeyPotion3);
            else Form1_0.KeyMouse_0.PressPotionKey(CharConfig.KeyPotion3);
        }
        if (i == 3)
        {
            if (SendToMerc) Form1_0.KeyMouse_0.PressPotionKeyMerc(CharConfig.KeyPotion4);
            else Form1_0.KeyMouse_0.PressPotionKey(CharConfig.KeyPotion4);
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
            if (CanUseSkillForRegen && !CharConfig.RunItemGrabScriptOnly)
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
                if (CanUseSkillForRegen && !CharConfig.RunItemGrabScriptOnly)
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
            if (!CharConfig.RunItemGrabScriptOnly && !Form1_0.Battle_0.DoingBattle) Form1_0.KeyMouse_0.PressKey(CharConfig.KeySkillDefenseAura);
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
