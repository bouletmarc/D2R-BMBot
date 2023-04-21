using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace app
{
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

            ReadProcessMemory((int) Form1_0.processHandle, (IntPtr)address, buffer, bytes, ref pBytesRead);
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
            int bytesRead = 0;
            Form1_0.bufferRead = new byte[aTypeSize["Int"]];
            ReadProcessMemory((int)Form1_0.processHandle, (IntPtr)((long)Form1_0.BaseAddress + (long)ThisAd), Form1_0.bufferRead, aTypeSize["Int"], ref bytesRead);

            if (bytesRead > 0)
            {
                return BitConverter.ToUInt32(Form1_0.bufferRead, 0);
            }
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

        public char ReadUChar(IntPtr ThisAd)
        {
            int bytesRead = 0;
            Form1_0.bufferRead = new byte[aTypeSize["UChar"]];
            ReadProcessMemory((int)Form1_0.processHandle, (IntPtr)((long)Form1_0.BaseAddress + (long)ThisAd), Form1_0.bufferRead, aTypeSize["UChar"], ref bytesRead);

            if (bytesRead > 0)
            {
                return (char) Form1_0.bufferRead[0];
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
                return BitConverter.ToInt32(Form1_0.bufferRead, 0);
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
                return BitConverter.ToUInt16(Form1_0.bufferRead, 0);
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
                return BitConverter.ToUInt32(Form1_0.bufferRead, 0);
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
                return BitConverter.ToInt32(Form1_0.bufferRead, 0);
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
    }
}
