using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ShopBot
{
    Form1 Form1_0;

    public int CurrentStep = 0;
    public bool ScriptDone = false;
    public int MaxShopCount = -1;
    public int CurrentShopCount = 0;
    public int ShopBotTownAct = 5;


    public void SetForm1(Form1 form1_1)
    {
        Form1_0 = form1_1;
    }

    public void ResetVars()
    {
        CurrentStep = 0;
        ScriptDone = false;
    }

    public void RunScript()
    {
        Form1_0.Town_0.ScriptTownAct = ShopBotTownAct;

        if (!Form1_0.Running || !Form1_0.GameStruc_0.IsInGame())
        {
            ScriptDone = true;
            return;
        }
        
        if (CurrentStep == 0)
        {
            Form1_0.SetGameStatus("DOING SHOPBOT");
            Form1_0.Battle_0.CastDefense();
            Form1_0.WaitDelay(15);

            if ((Enums.Area)Form1_0.PlayerScan_0.levelNo == Enums.Area.Harrogath)
            {
                CurrentStep++;
            }
            else
            {
                Form1_0.Town_0.FastTowning = false;
                Form1_0.Town_0.GoToTown();
            }
        }

        if (CurrentStep == 1)
        {
            Form1_0.SetGameStatus("TOWN-SHOPBOT");
            //Console.WriteLine("town moving to shop");
            Form1_0.Town_0.MoveToStore();
            CurrentStep++;
        }

        if (CurrentStep == 2)
        {
            Form1_0.Town_0.GoToWPArea(5, 1);
            CurrentStep++;
        }

        if (CurrentStep == 2)
        {
            Form1_0.Town_0.GoToWPArea(5, 0);

            if (MaxShopCount > 0)
            {
                CurrentShopCount++;
                if (CurrentShopCount >= MaxShopCount)
                {
                    ScriptDone = true;
                }
            }
            CurrentStep = 1;
        }
    }
}
