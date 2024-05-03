using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class AreaScript
{
    Form1 Form1_0;

    public void SetForm1(Form1 form1_1)
    {
        Form1_0 = form1_1;
    }

    public int GetActFromArea(Enums.Area ThisArea)
    {
        int TownAct = 1;
        if ((int)ThisArea >= 1 && (int)ThisArea < 40) TownAct = 1;
        if ((int)ThisArea >= 40 && (int)ThisArea < 75) TownAct = 2;
        if ((int)ThisArea >= 75 && (int)ThisArea < 103) TownAct = 3;
        if ((int)ThisArea >= 103 && (int)ThisArea < 109) TownAct = 4;
        if ((int)ThisArea >= 109) TownAct = 5;

        return TownAct;
    }

    public bool IsThisTZAreaInSameAct(int DoingTZAreaAct, Enums.Area ThisTZArea)
    {
        return (GetActFromArea(ThisTZArea) == DoingTZAreaAct);
    }
}
