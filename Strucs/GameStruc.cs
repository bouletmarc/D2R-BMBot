using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Collections.Specialized.BitVector32;
using static System.Windows.Forms.AxHost;
using System.Threading;
using static System.Net.Mime.MediaTypeNames;

namespace app
{
    public class GameStruc
    {
        Form1 Form1_0;

        public string GameName = "";
        public string GameDifficulty = "";
        public string GameOwnerName = "";
        public List<string> AllGamesNames = new List<string>();
        public List<int> AllGamesPlayersCount = new List<int>();

        public List<string> AllPlayersNames = new List<string>();
        public int SelectedGamePlayerCount = 0;
        public int SelectedGameTime = 0;

        public List<string> AllGamesTriedNames = new List<string>();

        //[DllImport("user32.dll")]
        //public static extern long GetWindowRect(int hWnd, ref Rectangle lpRect);

        public void SetForm1(Form1 form1_1)
        {
            Form1_0 = form1_1;
        }

        public void extract()
        {
            long gameNameOffset = (long)Form1_0.BaseAddress + (long)Form1_0.offsets["AllGamesOffset"];
            byte[] unitTableBuffer = new byte[0xfff];
            Form1_0.Mem_0.ReadRawMemory(gameNameOffset, ref unitTableBuffer, unitTableBuffer.Length);

            string SavePathh = Form1_0.ThisEndPath + "DumpGameStruc";
            File.Create(SavePathh).Dispose();
            File.WriteAllBytes(SavePathh, unitTableBuffer);

            GetAllGamesNames();
        }

        public void GetAllGamesNames()
        {
            AllGamesNames = new List<string>();
            AllGamesPlayersCount = new List<int>();
            ClicTopRow();
            Form1_0.KeyMouse_0.MouseClicc(1190, 990); //clic 'salon' if not in server
            Form1_0.KeyMouse_0.MouseClicc(1415, 65);  //clic 'join game' if not in game list area
            Form1_0.KeyMouse_0.MouseClicc(1720, 210); //clic refresh
            Form1_0.WaitDelay(60);

            long gameNameOffset = (long)Form1_0.BaseAddress + (long)Form1_0.offsets["AllGamesOffset"];

            for (int i = 0; i < 40; i++)
            {
                long NameOffet = gameNameOffset + 0x08 + (i * 0x128);
                long CountOffet = gameNameOffset + 0xf8 + (i * 0x128);
                string TestName = Form1_0.Mem_0.ReadMemString(NameOffet);

                if (TestName != "")
                {
                    if (HasGameNameInList(TestName))
                    {
                        break;
                    }
                    AllGamesNames.Add(TestName);
                    AllGamesPlayersCount.Add((int) Form1_0.Mem_0.ReadByteRaw((IntPtr) CountOffet));

                    //Form1_0.method_1("Game: " + TestName + " - Players: " + ((int)Form1_0.Mem_0.ReadByteRaw((IntPtr)CountOffet)));
                }
                else
                {
                    break;
                }
            }

            //GetSelectedGameInfo();
        }

        public bool TriedThisGame(string TestN)
        {
            if (AllGamesTriedNames.Count > 0)
            {
                for (int i = 0; i < AllGamesTriedNames.Count; i++)
                {
                    if (AllGamesTriedNames[i] == TestN)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool HasGameNameInList(string TestN)
        {
            if (AllGamesNames.Count > 0)
            {
                for (int i = 0; i < AllGamesNames.Count; i++)
                {
                    if (AllGamesNames[i] == TestN)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public void GetSelectedGameInfo()
        {
            //0x53F or 0x540 size
            long gameOffset = (long)Form1_0.BaseAddress + (long)Form1_0.offsets["GameSelectedOffset"];
            long PlayersNamesOffset = gameOffset + 0x138; //then 0x78 offset each others names
            AllPlayersNames = new List<string>();

            SelectedGamePlayerCount = (int)Form1_0.Mem_0.ReadByteRaw((IntPtr) (gameOffset + 0x108));
            SelectedGameTime = Form1_0.Mem_0.ReadInt32Raw((IntPtr)(gameOffset + 0xf0));
            for (int i = 0; i < SelectedGamePlayerCount; i++)
            {
                long NameOffet = PlayersNamesOffset + (i * 0x78);
                string TestName = Form1_0.Mem_0.ReadMemString(NameOffet);

                if (TestName != "")
                {
                    AllPlayersNames.Add(TestName);
                    //Form1_0.method_1("Player Name: " + TestName);
                }
            }

            //LogGameTime();
        }

        public void SelectGame(int ThisIndex, bool EnterGame)
        {
            if (EnterGame)
            {
                if (AllPlayersNames.Count > 0)
                {
                    GameOwnerName = AllPlayersNames[0];
                }
                else
                {
                    return;
                }
                if (SelectedGamePlayerCount == 8)
                {
                    return;
                }
            }

            //Form1_0.method_1("Selecting game: " + ThisIndex + ", ENTER: " + EnterGame);


            if (ThisIndex >= 0 && ThisIndex <= 13)
            {
                if (!EnterGame) ClicTopRow();
                ClicGameIndex(ThisIndex);
                if (EnterGame)
                {
                    Thread.Sleep(10);
                    ClicGameIndex(ThisIndex);
                    Thread.Sleep(10);
                    ClicGameIndex(ThisIndex);
                }
            }
            if (ThisIndex >= 14 && ThisIndex <= 26)
            {
                if (!EnterGame) ClicMidRow();
                ClicGameIndex(ThisIndex - 13);
                if (EnterGame)
                {
                    Thread.Sleep(10);
                    ClicGameIndex(ThisIndex);
                    Thread.Sleep(10);
                    ClicGameIndex(ThisIndex);
                }
            }
            if (ThisIndex >= 27 && ThisIndex <= 39)
            {
                if (!EnterGame) ClicBottomRow();
                ClicGameIndex(ThisIndex - 26);
                if (EnterGame)
                {
                    Thread.Sleep(10);
                    ClicGameIndex(ThisIndex);
                    Thread.Sleep(10);
                    ClicGameIndex(ThisIndex);
                }
            }

            Form1_0.WaitDelay(40);
        }

        public void ClicGameIndex(int ThisIndex)
        {
            //1345, 260 (+25px each games)
            Form1_0.KeyMouse_0.MouseClicc(1345, (int) (260 + (ThisIndex * 27.3)));
        }

        public void ClicTopRow()
        {
            Form1_0.KeyMouse_0.MouseClicc(1510, 270);
            Form1_0.WaitDelay(10);
        }

        public void ClicMidRow()
        {
            Form1_0.KeyMouse_0.MouseClicc(1510, 465);
            Form1_0.WaitDelay(10);
        }

        public void ClicBottomRow()
        {
            Form1_0.KeyMouse_0.MouseClicc(1510, 605);
            Form1_0.WaitDelay(10);
        }

        public void CheckChickenGameTime()
        {
            TimeSpan Checkkt = (DateTime.Now - Form1_0.GameStartedTime);
            if (Checkkt.TotalMinutes > CharConfig.MaxGameTime)
            {
                Form1_0.method_1("Leaving reason: Chicken time", Color.Red);
                Form1_0.LeaveGame();
            }
        }

        public string GetTimeNow()
        {
            DateTime ThisTimee = DateTime.Now;
            string HourTime = ThisTimee.Hour.ToString("00") + ":" + ThisTimee.Minute.ToString("00");
            string MonthTime = ThisTimee.Day + "-" + ThisTimee.Month + "-" + ThisTimee.Year;

            return HourTime + " (" + MonthTime + ")";
        }

        public void SetNewGame()
        {
            Form1_0.method_1("------------------------------------------", Color.DarkBlue);
            Form1_0.method_1("New game started: " + GetTimeNow(), Color.DarkBlue);

            Form1_0.GameStartedTime = DateTime.Now;

            long gameNameOffset = (long)Form1_0.offsets["gameDataOffset"] + 0x40;
            long gameNameAddress = (long)Form1_0.BaseAddress + gameNameOffset;
            GameName = Form1_0.Mem_0.ReadMemString(gameNameAddress);

            Form1_0.method_1("Entered game: " + GameName, Color.Black);

            AllGamesTriedNames = new List<string>();
        }

        public void LogGameTime()
        {
            TimeSpan ThisTimee = DateTime.Now - Form1_0.GameStartedTime;
            Form1_0.method_1("Game Time: " + ThisTimee.TotalMinutes.ToString("00") + ":" + ThisTimee.TotalSeconds.ToString("00") + ":" + ThisTimee.TotalMilliseconds.ToString("0"), Color.Black);
        }

        public Dictionary<string, int> World2Screen(long playerX, long playerY, long targetx, long targety)
        {
            //; scale = 27
            //double scale = Form1_0.centerModeScale * Form1_0.renderScale * 100;
            double scale = 40.8;
            long xdiff = targetx - playerX;
            long ydiff = targety - playerY;

            double angle = 0.785398; //45 deg
            double x = xdiff * Math.Cos(angle) - ydiff * Math.Sin(angle);
            double y = xdiff * Math.Sin(angle) + ydiff * Math.Cos(angle);
            int xS = (int) (Form1_0.CenterX + (x * scale));
            //int yS = (int) (Form1_0.CenterY + (y * scale * 0.5) - 10);
            int yS = (int)(Form1_0.CenterY + (y * scale * 0.5) - 30);

            Dictionary<string, int> NewDict = new Dictionary<string, int>();
            NewDict["x"] = xS;
            NewDict["y"] = yS;
            return NewDict;
        }

        public bool IsGameRunning()
        {
            Process[] ProcList = Process.GetProcessesByName("D2R");
            if (ProcList.Length == 0)
            {
                return false;
            }
            return true;
        }

        public bool IsInGame()
        {
            long baseAddress = (long)Form1_0.BaseAddress + (long)Form1_0.offsets["unitTable"] - 64;
            byte[] unitTableBuffer = new byte[1];
            Form1_0.Mem_0.ReadRawMemory(baseAddress, ref unitTableBuffer, 1);

            //Console.WriteLine(unitTableBuffer[0]);
            if (unitTableBuffer[0] == 0x01)
            {
                return true;
            }

            return false;
        }
    }
}
