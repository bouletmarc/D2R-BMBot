using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Diagnostics;

public class Mem
{
    Form1 Form1_0;

    [DllImport("kernel32.dll")]
    public static extern bool ReadProcessMemory(int hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int dwSize, ref int lpNumberOfBytesRead);

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern bool WriteProcessMemory(int hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int nSize, ref int lpNumberOfBytesWritten);

    public void SetForm1(Form1 Form1_1)
    {
        Form1_0 = Form1_1;
    }

    Dictionary<string, int> aTypeSize = new Dictionary<string, int>
        {
            {"UChar", 1}, {"Char", 1},
            {"UShort", 2}, {"Short", 2},
            {"UInt", 4}, {"Int", 4},
            {"UFloat", 4}, {"Float", 4},
            {"Int64", 8}, {"Double", 8}
        };

    public void WriteRawMemory(IntPtr address, byte[] buffer, int writesize)
    {
        int pBytesWrite = 0;
        WriteProcessMemory((int)Form1_0.processHandle, address, buffer, writesize, ref pBytesWrite);
    }

    public void ReadMemory(IntPtr address, ref byte[] buffer, int bytes, ref int pBytesRead)
    {
        ReadProcessMemory((int)Form1_0.processHandle, address, buffer, bytes, ref pBytesRead);
    }

    public void ReadRawMemory(long address, ref byte[] buffer, int bytes = 4, params int[] aOffsets)
    {
        buffer = new byte[bytes];
        int pBytesRead = 0;
        /*if (aOffsets.Length > 0)
        {
            address = this.GetAddressFromOffsets(address, aOffsets);
        }*/

        try
        {
            ReadProcessMemory((int)Form1_0.processHandle, (IntPtr)address, buffer, bytes, ref pBytesRead);
        }
        catch
        {
            Form1_0.method_1("Couldn't read D2R process memory!", Color.Red);
        }
    }

    public string ReadMemString(long TPoint)
    {
        string name = "";
        for (int i2 = 0; i2 < 16; i2++)
        {
            if (ReadByteRaw((IntPtr)(TPoint + i2)) != 0x00)
            {
                name += ReadUCharRaw((IntPtr)(TPoint + i2));
            }
            else
            {
                break;
            }
        }
        return name;
    }

    public int ReadInt(IntPtr ThisAd)
    {
        int bytesRead = 0;
        Form1_0.bufferRead = new byte[aTypeSize["Int"]];
        ReadProcessMemory((int)Form1_0.processHandle, (IntPtr)((long)Form1_0.BaseAddress + (long)ThisAd), Form1_0.bufferRead, aTypeSize["Int"], ref bytesRead);

        if (bytesRead > 0)
        {
            return BitConverter.ToInt32(Form1_0.bufferRead, 0);
        }
        return 0;
    }

    public UInt16 ReadUInt16(IntPtr ThisAd)
    {
        int bytesRead = 0;
        Form1_0.bufferRead = new byte[aTypeSize["Short"]];
        ReadProcessMemory((int)Form1_0.processHandle, (IntPtr)((long)Form1_0.BaseAddress + (long)ThisAd), Form1_0.bufferRead, aTypeSize["Short"], ref bytesRead);

        if (bytesRead > 0)
        {
            return BitConverter.ToUInt16(Form1_0.bufferRead, 0);
        }
        return 0;
    }

    public UInt32 ReadUInt32(IntPtr ThisAd)
    {
        try
        {
            int bytesRead = 0;
            Form1_0.bufferRead = new byte[aTypeSize["Int"]];
            ReadProcessMemory((int)Form1_0.processHandle, (IntPtr)((long)Form1_0.BaseAddress + (long)ThisAd), Form1_0.bufferRead, aTypeSize["Int"], ref bytesRead);

            if (bytesRead > 0)
            {
                return BitConverter.ToUInt32(Form1_0.bufferRead, 0);
            }
        }
        catch { }
        return 0;
    }

    public Int32 ReadInt32(IntPtr ThisAd)
    {
        int bytesRead = 0;
        Form1_0.bufferRead = new byte[aTypeSize["Int"]];
        ReadProcessMemory((int)Form1_0.processHandle, (IntPtr)((long)Form1_0.BaseAddress + (long)ThisAd), Form1_0.bufferRead, aTypeSize["Int"], ref bytesRead);

        if (bytesRead > 0)
        {
            return BitConverter.ToInt32(Form1_0.bufferRead, 0);
        }
        return 0;
    }

    public Int64 ReadInt64(IntPtr ThisAd)
    {
        int bytesRead = 0;
        Form1_0.bufferRead = new byte[aTypeSize["Int64"]];
        ReadProcessMemory((int)Form1_0.processHandle, (IntPtr)((long)Form1_0.BaseAddress + (long)ThisAd), Form1_0.bufferRead, aTypeSize["Int64"], ref bytesRead);

        if (bytesRead > 0)
        {
            return BitConverter.ToInt64(Form1_0.bufferRead, 0);
        }
        return 0;
    }

    public unsafe UInt64 ReadUInt64(UIntPtr address)

    {
        int bytesRead = 0;
        byte[] buffer = new byte[8];
        IntPtr baseAddress = (IntPtr)address.ToPointer();
        ReadProcessMemory((int)Form1_0.processHandle, baseAddress, buffer, buffer.Length, ref bytesRead);

        if (bytesRead > 0)
        {
            return BitConverter.ToUInt64(buffer, 0);
        }
        return 0;
    }

    public uint ReadUIntFromBuffer(byte[] bytes, uint offset, int size)
    {
        if (bytes == null)
        {
            throw new ArgumentNullException(nameof(bytes));
        }

        if (offset + size > bytes.Length)
        {
            throw new ArgumentOutOfRangeException(nameof(offset), "Offset and size exceed the length of the byte array.");
        }

        return BytesToUint(bytes, offset, size);
    }

    private static uint BytesToUint(byte[] bytes, uint offset, int size)
    {
        uint result = 0;

        for (int i = 0; i < size; i++)
        {
            result |= (uint)bytes[offset + i] << (8 * i);
        }

        return result;
    }
    public char ReadUChar(IntPtr ThisAd)
    {
        int bytesRead = 0;
        Form1_0.bufferRead = new byte[aTypeSize["UChar"]];
        ReadProcessMemory((int)Form1_0.processHandle, (IntPtr)((long)Form1_0.BaseAddress + (long)ThisAd), Form1_0.bufferRead, aTypeSize["UChar"], ref bytesRead);

        if (bytesRead > 0)
        {
            return (char)Form1_0.bufferRead[0];
        }
        return '\0';
    }

    //###################################################################################################

    public byte ReadByteRaw(IntPtr ThisAd)
    {
        int bytesRead = 0;
        Form1_0.bufferRead = new byte[1];
        ReadProcessMemory((int)Form1_0.processHandle, ThisAd, Form1_0.bufferRead, 1, ref bytesRead);

        if (bytesRead > 0)
        {
            return Form1_0.bufferRead[0];
        }
        return 0;
    }

    public int ReadIntRaw(IntPtr ThisAd)
    {
        int bytesRead = 0;
        Form1_0.bufferRead = new byte[aTypeSize["Int"]];
        ReadProcessMemory((int)Form1_0.processHandle, ThisAd, Form1_0.bufferRead, aTypeSize["Int"], ref bytesRead);

        if (bytesRead > 0)
        {
            try
            {
                return BitConverter.ToInt32(Form1_0.bufferRead, 0);
            }
            catch { }
        }
        return 0;
    }

    public UInt16 ReadUInt16Raw(IntPtr ThisAd)
    {
        int bytesRead = 0;
        Form1_0.bufferRead = new byte[aTypeSize["Short"]];
        ReadProcessMemory((int)Form1_0.processHandle, ThisAd, Form1_0.bufferRead, aTypeSize["Short"], ref bytesRead);

        if (bytesRead > 0)
        {
            try
            {
                return BitConverter.ToUInt16(Form1_0.bufferRead, 0);
            }
            catch { }
        }
        return 0;
    }

    public UInt32 ReadUInt32Raw(IntPtr ThisAd)
    {
        int bytesRead = 0;
        Form1_0.bufferRead = new byte[aTypeSize["Int"]];
        ReadProcessMemory((int)Form1_0.processHandle, ThisAd, Form1_0.bufferRead, aTypeSize["Int"], ref bytesRead);

        if (bytesRead > 0)
        {
            try
            {
                return BitConverter.ToUInt32(Form1_0.bufferRead, 0);
            }
            catch { }
        }
        return 0;
    }

    public Int32 ReadInt32Raw(IntPtr ThisAd)
    {
        int bytesRead = 0;
        Form1_0.bufferRead = new byte[aTypeSize["Int"]];
        ReadProcessMemory((int)Form1_0.processHandle, ThisAd, Form1_0.bufferRead, aTypeSize["Int"], ref bytesRead);

        if (bytesRead > 0)
        {
            try
            {
                return BitConverter.ToInt32(Form1_0.bufferRead, 0);
            }
            catch { }
        }
        return 0;
    }

    public Int64 ReadInt64Raw(IntPtr ThisAd)
    {
        int bytesRead = 0;
        Form1_0.bufferRead = new byte[aTypeSize["Int64"]];
        ReadProcessMemory((int)Form1_0.processHandle, ThisAd, Form1_0.bufferRead, aTypeSize["Int64"], ref bytesRead);

        if (bytesRead > 0)
        {
            try
            {
                return BitConverter.ToInt64(Form1_0.bufferRead, 0);
            }
            catch { }
        }
        return 0;
    }

    public char ReadUCharRaw(IntPtr ThisAd)
    {
        int bytesRead = 0;
        Form1_0.bufferRead = new byte[aTypeSize["UChar"]];
        ReadProcessMemory((int)Form1_0.processHandle, ThisAd, Form1_0.bufferRead, aTypeSize["UChar"], ref bytesRead);

        if (bytesRead > 0)
        {
            return (char)Form1_0.bufferRead[0];
        }
        return '\0';
    }

    public byte[] ReadBytesFromMemory(IntPtr address, int size)
    {
        byte[] buffer = new byte[size];
        int bytesRead = 0;
        ReadProcessMemory((int)Form1_0.processHandle, address, buffer, size, ref bytesRead);
        return buffer;
    }

    public unsafe byte[] ReadBytesFromMemory(UIntPtr address, uint size)
    {
        byte[] buffer = new byte[size];
        int bytesRead = 0;
        IntPtr baseAddress = (IntPtr)address.ToPointer();

        ReadProcessMemory((int)Form1_0.processHandle, baseAddress, buffer, (int)size, ref bytesRead);
        return buffer;
    }


    public Dictionary<Enums.Attribute, int> GetMonsterStats(uint statCount, UIntPtr statPtr)
    {
        Dictionary<Enums.Attribute, int> stats = new Dictionary<Enums.Attribute, int>();

        if (statCount > 0)
        {
            byte[] statBuffer = ReadBytesFromMemory(statPtr + 0x2, (statCount * 8));

            for (int i = 0; i < statCount; i++)
            {
                uint offset = (uint)(i * 8);
                ushort statEnum = (ushort)ReadUIntFromBuffer(statBuffer, offset, 2); // Uint16 in Go
                uint statValue = ReadUIntFromBuffer(statBuffer, offset + 0x2, 4); // Uint32 in Go

                if (Enum.IsDefined(typeof(Enums.Attribute), (int)statEnum))
                {
                    stats[(Enums.Attribute)(int)statEnum] = (int)statValue;
                }
            }
        }

        return stats;
    }
}
