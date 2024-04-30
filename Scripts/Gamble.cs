using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app
{
    public class Gamble
    {
        Form1 Form1_0;

        public int GambleType = 0;

        public void SetForm1(Form1 form1_1)
        {
            Form1_0 = form1_1;
        }

        public bool CanGamble()
        {
            if (GambleType == 0)
            {
                 return (Form1_0.PlayerScan_0.PlayerGoldInStash >= CharConfig.GambleAboveGoldAmount + 50000); //gamble ring
            }
            if (GambleType >= 1)
            {
                return (Form1_0.PlayerScan_0.PlayerGoldInStash >= CharConfig.GambleAboveGoldAmount + 63000); //gamble ammy
            }
            return false;
        }

        public bool CanStillGamble()
        {
            if (GambleType == 0)
            {
                return (Form1_0.PlayerScan_0.PlayerGoldInStash >= CharConfig.GambleUntilGoldAmount + 50000); //gamble ring
            }
            if (GambleType >= 1)
            {
                return (Form1_0.PlayerScan_0.PlayerGoldInStash >= CharConfig.GambleUntilGoldAmount + 63000); //gamble ammy
            }
            return false;
        }

        public void RunGambleScript()
        {
            int tries = 0;
            bool Gambling = true;
            GambleType = 0;
            while (Gambling && tries < 3)
            {
                int ThisStartCount = Form1_0.ItemsStruc_0.ItemsInInventory;

                string GambleThisItem = CharConfig.GambleItems[GambleType];
                if (Form1_0.ItemsStruc_0.GetShopItem(GambleThisItem, true))
                {
                    Dictionary<string, int> itemScreenPos = Form1_0.Shop_0.ConvertShopLocToScreenPos(Form1_0.ItemsStruc_0.itemx, Form1_0.ItemsStruc_0.itemy);
                    Form1_0.KeyMouse_0.MouseCliccRight(itemScreenPos["x"], itemScreenPos["y"]);
                    Form1_0.WaitDelay(100);
                    Form1_0.PlayerScan_0.GetPositions();
                    Form1_0.ItemsStruc_0.GetItems(false);   //get inventory
                }

                GambleType++;
                if (GambleType > CharConfig.GambleItems.Count - 1) GambleType = 0;

                Gambling = CanStillGamble();

                if (Form1_0.ItemsStruc_0.ItemsInInventory == ThisStartCount) tries++;
                ThisStartCount = Form1_0.ItemsStruc_0.ItemsInInventory;
            }
        }
    }
}
