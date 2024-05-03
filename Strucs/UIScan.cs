using System;
using System.IO;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Windows.Forms;
using System.Runtime.Remoting.Messaging;
using System.Net.NetworkInformation;

public class UIScan
{
    Form1 Form1_0;

    public bool invMenu = false;
    public bool charMenu = false;
    public bool skillSelect = false;
    public bool skillMenu = false;
    public bool npcInteract = false;
    public bool quitMenu = false;
    public bool npcShop = false;
    public bool questsMenu = false;
    public bool waypointMenu = false;
    public bool stash = false;
    public bool partyMenu = false;
    public bool mercMenu = false;
    public bool loading = false;
    public bool deadMenu = false;
    public bool tradeMenu = false;
    public bool cubeMenu = false;

    public bool leftMenu = false;
    public bool rightMenu = false;
    public bool fullMenu = false;
    public bool SetUI = false;

    public int MaxTryUIOpen = 5;
    public int MaxWaitingDelayForMenuInteractions = 10;

    public void SetForm1(Form1 form1_1)
    {
        Form1_0 = form1_1;
    }

    public void CloseAllUIMenu()
    {
        CloseAllUIMenuButThisOne("");
        /*if (GetMenuActive("invMenu")) CloseThisMenu("invMenu");
        if (GetMenuActive("questsMenu")) CloseThisMenu("questsMenu");
        if (GetMenuActive("partyMenu")) CloseThisMenu("partyMenu");
        if (GetMenuActive("mercMenu")) CloseThisMenu("mercMenu");
        if (GetMenuActive("stash")) CloseThisMenu("stash");
        if (GetMenuActive("npcInteract")) CloseThisMenu("npcInteract");
        if (GetMenuActive("npcShop")) CloseThisMenu("npcShop");
        if (GetMenuActive("tradeMenu")) CloseThisMenu("tradeMenu");
        if (GetMenuActive("cubeMenu")) CloseThisMenu("cubeMenu");
        if (GetMenuActive("quitMenu")) CloseThisMenu("quitMenu");*/
    }

    public void CloseAllUIMenuButThisOne(string NotThisUIMenu)
    {
        if (NotThisUIMenu != "invMenu" && GetMenuActive("invMenu")) CloseThisMenu("invMenu");
        if (NotThisUIMenu != "questsMenu" && GetMenuActive("questsMenu")) CloseThisMenu("questsMenu");
        if (NotThisUIMenu != "partyMenu" && GetMenuActive("partyMenu")) CloseThisMenu("partyMenu");
        if (NotThisUIMenu != "mercMenu" && GetMenuActive("mercMenu")) CloseThisMenu("mercMenu");
        if (NotThisUIMenu != "stash" && GetMenuActive("stash")) CloseThisMenu("stash");
        if (NotThisUIMenu != "npcInteract" && GetMenuActive("npcInteract")) CloseThisMenu("npcInteract");
        if (NotThisUIMenu != "npcShop" && GetMenuActive("npcShop")) CloseThisMenu("npcShop");
        if (NotThisUIMenu != "tradeMenu" && GetMenuActive("tradeMenu")) CloseThisMenu("tradeMenu");
        if (NotThisUIMenu != "cubeMenu" && GetMenuActive("cubeMenu")) CloseThisMenu("cubeMenu");
        if (NotThisUIMenu != "quitMenu" && GetMenuActive("quitMenu")) CloseThisMenu("quitMenu");
    }

    public void CloseThisMenu(string UIName)
    {
        int TryClic = 0;
        System.Windows.Forms.Keys ThisKey = GetMenuKey(UIName);
        bool ThisMenuClose = !GetMenuActive(UIName);
        while (!ThisMenuClose && TryClic < MaxTryUIOpen)
        {
            Form1_0.KeyMouse_0.PressKey(ThisKey);
            Form1_0.WaitDelay(5);
            ThisMenuClose = !GetMenuActive(UIName);
            //Application.DoEvents();
            TryClic++;
        }
    }

    public bool OpenUIMenu(string UIName)
    {
        int TryClic = 0;
        System.Windows.Forms.Keys ThisKey = GetMenuKey(UIName);
        bool ThisMenuOpen = GetMenuActive(UIName);

        while (!ThisMenuOpen && TryClic < MaxTryUIOpen)
        {
            Form1_0.KeyMouse_0.PressKey(ThisKey);
            Form1_0.WaitDelay(5);
            ThisMenuOpen = GetMenuActive(UIName);
            //Application.DoEvents();
            TryClic++;
        }

        return ThisMenuOpen;
    }

    public bool CloseUIMenu(string UIName)
    {
        int TryClic = 0;
        System.Windows.Forms.Keys ThisKey = GetMenuKey(UIName);
        bool ThisMenuClose = !GetMenuActive(UIName);

        while (!ThisMenuClose && TryClic < MaxTryUIOpen)
        {
            Form1_0.KeyMouse_0.PressKey(ThisKey);
            Form1_0.WaitDelay(5);
            ThisMenuClose = !GetMenuActive(UIName);
            //Application.DoEvents();
            TryClic++;
        }

        return ThisMenuClose;
    }

    public bool WaitTilUIOpen(string UIName)
    {
        bool IsOpen = GetMenuActive(UIName);

        if (!IsOpen)
        {
            int WaitTime = 0;
            while (true)
            {
                Form1_0.WaitDelay(1);
                IsOpen = GetMenuActive(UIName);
                if (WaitTime > MaxWaitingDelayForMenuInteractions || IsOpen)
                {
                    break;
                }
                CloseAllUIMenuButThisOne(UIName);
                WaitTime++;
            }
        }

        return IsOpen;
    }

    public bool WaitTilUIClose(string UIName)
    {
        bool IsClose = !GetMenuActive(UIName);

        if (!IsClose)
        {
            int WaitTime = 0;
            while (true)
            {
                Form1_0.WaitDelay(1);
                IsClose = !GetMenuActive(UIName);
                if (WaitTime > MaxWaitingDelayForMenuInteractions || IsClose)
                {
                    break;
                }
                WaitTime++;
            }
        }

        return IsClose;
    }


    public System.Windows.Forms.Keys GetMenuKey(string UIName)
    {
        if (UIName == "invMenu")
        {
            return CharConfig.KeyOpenInventory;
        }
        if (UIName == "questsMenu")
        {
            return System.Windows.Forms.Keys.Q;
        }
        if (UIName == "partyMenu")
        {
            return System.Windows.Forms.Keys.P;
        }
        if (UIName == "mercMenu")
        {
            return System.Windows.Forms.Keys.O;
        }
        if (UIName == "quitMenu")
        {
            return System.Windows.Forms.Keys.Escape;
        }
        if (UIName == "stash")
        {
            return System.Windows.Forms.Keys.Escape; //for quiting menu only
        }
        if (UIName == "npcInteract")
        {
            return System.Windows.Forms.Keys.Escape; //for quiting menu only
        }
        if (UIName == "npcShop")
        {
            return System.Windows.Forms.Keys.Escape; //for quiting menu only
        }
        if (UIName == "tradeMenu")
        {
            return System.Windows.Forms.Keys.Escape; //for quiting menu only
        }
        if (UIName == "cubeMenu")
        {
            return System.Windows.Forms.Keys.Escape; //for quiting menu only
        }
        return System.Windows.Forms.Keys.Oemcomma;
    }

    public bool GetMenuActive(string UIName)
    {
        readUI();
        if (UIName == "invMenu")
        {
            return invMenu;
        }
        if (UIName == "questsMenu")
        {
            return questsMenu;
        }
        if (UIName == "partyMenu")
        {
            return partyMenu;
        }
        if (UIName == "mercMenu")
        {
            return mercMenu;
        }
        if (UIName == "quitMenu")
        {
            return quitMenu;
        }
        //####
        if (UIName == "npcInteract")
        {
            return npcInteract;
        }
        if (UIName == "npcShop")
        {
            return npcShop;
        }
        if (UIName == "waypointMenu")
        {
            return waypointMenu;
        }
        if (UIName == "stash")
        {
            return stash;
        }
        if (UIName == "loading")
        {
            return loading;
        }
        if (UIName == "tradeMenu")
        {
            return tradeMenu;
        }
        if (UIName == "cubeMenu")
        {
            return cubeMenu;
        }
        //####
        return false;
    }

    public void readUI()
    {
        //; UI offset 0x21F89AA
        long baseAddr = (long)Form1_0.BaseAddress + (long)Form1_0.offsets["uiOffset"] - 0xa;
        byte[] buffer = new byte[32];
        Form1_0.Mem_0.ReadRawMemory(baseAddr, ref buffer, 32);

        invMenu = ByteToBool(buffer[0x01]);
        charMenu = ByteToBool(buffer[0x02]);
        skillSelect = ByteToBool(buffer[0x03]);
        skillMenu = ByteToBool(buffer[0x04]);
        //chatMenu = ByteToBool(buffer[0x05]);      //Chat box open(typing something)
        npcInteract = ByteToBool(buffer[0x08]);
        quitMenu = ByteToBool(buffer[0x09]);
        npcShop = ByteToBool(buffer[0x0B]);
        //showItemAltMenu = ByteToBool(buffer[0x0c]);
        //cashMenu = ByteToBool(buffer[0x0d]);
        questsMenu = ByteToBool(buffer[0x0E]);
        waypointMenu = ByteToBool(buffer[0x13]);
        partyMenu = ByteToBool(buffer[0x15]);
        tradeMenu = ByteToBool(buffer[0x16]);
        stash = ByteToBool(buffer[0x18]);
        cubeMenu = ByteToBool(buffer[0x19]);
        mercMenu = ByteToBool(buffer[0x1E]);

        //deadMenu = ByteToBool(buffer[0x12]);
        //loading = ByteToBool(Form1_0.Mem_0.ReadByteRaw((IntPtr) baseAddr + 0x16c));
        //loading = ByteToBool(buffer[0x16C]); 16C

        //string SavePathh = Form1_0.ThisEndPath + "DumpUIStruc";
        //File.Create(SavePathh).Dispose();
        //File.WriteAllBytes(SavePathh, buffer);

        //Form1_0.method_1("UI Open: 0x" + buffer[0x01].ToString("X"));
        //if (!SetUI)
        //{
        /*    byte[] RunBuf = new byte[1] { 0x00 };
            Form1_0.Mem_0.WriteRawMemory((IntPtr)(baseAddr + 0x0b), RunBuf, 1);

            RunBuf = new byte[1] { 0x01 };
            Form1_0.Mem_0.WriteRawMemory((IntPtr)(baseAddr + 0x18), RunBuf, 1);*/
        //SetUI = true;
        //}

        leftMenu = (questsMenu || charMenu || mercMenu || partyMenu || waypointMenu || stash);
        rightMenu = (skillMenu || invMenu);
        fullMenu = (loading || quitMenu || npcInteract);

        //Form1_0.method_1("menufull: " + fullMenu + ", left: " + leftMenu + ", right: " + rightMenu);
        Form1_0.Grid_SetInfos("Left Open", leftMenu.ToString());
        Form1_0.Grid_SetInfos("Right Open", rightMenu.ToString());
        Form1_0.Grid_SetInfos("Full Open", fullMenu.ToString());


        /*0x01	Inventory open
        0x02	Character Stat screen open
        0x03	quick skill
        0x04	skill
        0x05	Chat box open (typing something)
        0x06
        0x07
        0x08	npc menu
        0x09	Esc menu?
        0x0A	Automap is on
        0x0B	config controls
        0x0C	Shop open at NPC
        0x0D	alt show items
        0x0E	cash
        0x0F	quest
        0x10
        0x11	questlog button
        0x12	status area
        0x13	?
        0x14	waypoint
        0x15	mini panel
        0x16	party
        0x17	Trade Prompt up [ok/cancel] (player) or in Trade w/player
        0x18	msgs
        0x19	Stash is open
        0x1A	Cube is open
        0x1B
        0x1C
        0x1D
        0x1E
        0x1F	Belt show all is toggled
        0x20
        0x21	help
        0x22
        0x23
        0x24	Merc screen
        0x25	scroll of whatever*/
    }

    bool ByteToBool(byte ThB)
    {
        if (ThB >= 1)
        {
            return true;
        }
        return false;
    }
}
