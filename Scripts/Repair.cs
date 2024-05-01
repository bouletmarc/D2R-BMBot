using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Repair
{
    Form1 Form1_0;
    public bool ShouldRepair = false;

    public void SetForm1(Form1 form1_1)
    {
        Form1_0 = form1_1;
    }

    public void RunRepairScript()
    {
        int tries = 0;
        bool Repairing = true;
        while (Repairing && tries < 3)
        {
            Form1_0.KeyMouse_0.MouseClicc(585, 775);  //clic full repair button
            Form1_0.WaitDelay(40);
            Repairing = GetShouldRepair();
            tries++;
        }
    }

    public bool GetShouldRepair()
    {
        Form1_0.ItemsStruc_0.GetItems(false);   //get inventory
        return ShouldRepair;
    }

    public void GetDurabilityOnThisEquippedItem()
    {
        int durability = 1;
        int Maxdurability = 1;

        if (Form1_0.ItemsStruc_0.statCount > 0)
        {
            //; get durability
            //Form1_0.Mem_0.ReadRawMemory(Form1_0.ItemsStruc_0.statPtr, ref Form1_0.ItemsStruc_0.statBuffer, (int)(Form1_0.ItemsStruc_0.statCount * 10));
            for (int i = 0; i < Form1_0.ItemsStruc_0.statCount; i++)
            {
                int offset = i * 8;
                //short statLayer = BitConverter.ToInt16(Form1_0.ItemsStruc_0.statBuffer, offset);
                ushort statEnum = BitConverter.ToUInt16(Form1_0.ItemsStruc_0.statBuffer, offset + 0x2);
                int statValue = BitConverter.ToInt32(Form1_0.ItemsStruc_0.statBuffer, offset + 0x4);
                //ushort statEnum = BitConverter.ToUInt16(Form1_0.ItemsStruc_0.statBuffer, offset);
                //int statValue = BitConverter.ToInt32(Form1_0.ItemsStruc_0.statBuffer, offset + 0x2);

                if (statEnum == (ushort)Enums.Attribute.Durability)
                {
                    durability = statValue;
                }
                if (statEnum == (ushort)Enums.Attribute.MaxDurability)
                {
                    Maxdurability = statValue;
                }
            }
        }

        if (durability == 1 && Maxdurability == 1)
        {
            if (Form1_0.ItemsStruc_0.statExCount > 0)
            {
                //; get durability
                //Form1_0.Mem_0.ReadRawMemory(Form1_0.ItemsStruc_0.statExPtr, ref Form1_0.ItemsStruc_0.statBuffer, (int)(Form1_0.ItemsStruc_0.statExCount * 10));
                for (int i = 0; i < Form1_0.ItemsStruc_0.statExCount; i++)
                {
                    int offset = i * 8;
                    //short statLayer = BitConverter.ToInt16(Form1_0.ItemsStruc_0.statBufferEx, offset);
                    ushort statEnum = BitConverter.ToUInt16(Form1_0.ItemsStruc_0.statBufferEx, offset + 0x2);
                    int statValue = BitConverter.ToInt32(Form1_0.ItemsStruc_0.statBufferEx, offset + 0x4);
                    //ushort statEnum = BitConverter.ToUInt16(Form1_0.ItemsStruc_0.statBufferEx, offset);
                    //int statValue = BitConverter.ToInt32(Form1_0.ItemsStruc_0.statBufferEx, offset + 0x2);

                    if (statEnum == (ushort)Enums.Attribute.Durability)
                    {
                        durability = statValue;
                    }
                    if (statEnum == (ushort)Enums.Attribute.MaxDurability)
                    {
                        Maxdurability = statValue;
                    }
                }
            }
        }

        int DurabilityPercent = (durability * 100) / Maxdurability;
        if (DurabilityPercent < 25)
        {
            ShouldRepair = true;
        }
    }
}
