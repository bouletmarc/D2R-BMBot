using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class HoverStruc
{
    Form1 Form1_0;

    public bool isHovered = false;
    public uint lastHoveredType = 0;
    public uint lastHoveredUnitId = 0;
    public byte[] hoverBuffer = new byte[12];

    public void SetForm1(Form1 form1_1)
    {
        Form1_0 = form1_1;
    }

    public bool IsHoveringItem(uint ThissType, uint ThissUnitId)
    {
        if (lastHoveredType == ThissType && lastHoveredUnitId == ThissUnitId)
        {
            return true;
        }
        return false;
    }

    public void GetHovering()
    {
        long hoverAddress = (long)Form1_0.BaseAddress + (long)Form1_0.offsets["hoverOffset"];
        Form1_0.Mem_0.ReadRawMemory(hoverAddress, ref hoverBuffer, 12);
        ushort TeB = BitConverter.ToUInt16(hoverBuffer, 0);
        if (TeB > 0)
        {
            isHovered = true;
        }
        else
        {
            isHovered = false;
        }
        if (isHovered)
        {
            lastHoveredType = BitConverter.ToUInt32(hoverBuffer, 0x04);
            lastHoveredUnitId = BitConverter.ToUInt32(hoverBuffer, 0x08);
        }
    }
}
