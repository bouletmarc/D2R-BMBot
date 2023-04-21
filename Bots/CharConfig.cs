using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace app
{
    public static class CharConfig
    {

        //#######################################
        //SHORTCUT KEY TO START/STOP BOT
        //#######################################
        public const int StartStopKey = 0x65;   //numpad5 -> refer to virtual key code (vkcode)
        
        //#######################################
        //SHORTCUT KEYS SETTINGS (FOR PALADIN)
        //#######################################
        public static System.Windows.Forms.Keys KeySkillAttack = Keys.F1;               //hammer
        public static System.Windows.Forms.Keys KeySkillAura = Keys.F2;                 //concentration
        public static System.Windows.Forms.Keys KeySkillfastMoveAtTown = Keys.F4;       //vigor
        public static System.Windows.Forms.Keys KeySkillfastMoveOutsideTown = Keys.F8;  //refer to teleport if have teleport else vigor
        public static System.Windows.Forms.Keys KeySkillDefenseAura = Keys.F6;          //defiance
        public static System.Windows.Forms.Keys KeySkillCastDefense = Keys.F5;          //sacred shield
        public static System.Windows.Forms.Keys KeySkillLifeAura = Keys.F7;             //prayer

        //#######################################
        //BELT AND INVENTORY SETTINGS
        //#######################################
        public static int[] BeltPotTypeToHave = new int[4] { 0, 0, 1, 3 };  //HP-HP-MANA-FULL RV -> 0=HP, 1=MANA, 2=RV, 3=FULL RV
        public static int[] InventoryDontCheckItem = new int[40]
        {
            0, 0, 0, 0, 0, 1, 1, 1, 1, 1,   //IF EQUAL 0, WE CAN USE THIS SPOT FOR BOT
            0, 0, 0, 0, 0, 1, 1, 1, 1, 1,   //IF EQUAL 0, WE CAN USE THIS SPOT FOR BOT
            0, 0, 0, 0, 1, 1, 1, 1, 1, 1,   //IF EQUAL 0, WE CAN USE THIS SPOT FOR BOT
            0, 0, 0, 0, 1, 1, 1, 1, 1, 1    //IF EQUAL 0, WE CAN USE THIS SPOT FOR BOT
        };

        //#######################################
        //PLAYER SETTINGS
        //#######################################
        public static string PlayerCharName = "ENTER CHAR NAME HERE"; //CHAR NAME
        public static bool UseTeleport = true;
        public static int ChickenHP = 20;           //VALUE IN PERCENT
        public static int TakeHPPotUnder = 80;      //VALUE IN PERCENT
        public static int TakeRVPotUnder = 35;      //VALUE IN PERCENT
        public static int TakeManaPotUnder = 15;    //VALUE IN PERCENT

        //#######################################
        //BOT SETTINGS (USE ONLY CHAOS OR BAAL, NOT BOTH AT SAME TIME)
        //#######################################
        public static int MaxGameTime = 9;                  //9MINS -> TIME IN MINUTES
        public static bool RunChaosScript = false;          //CHAOS LEECH SCRIPT **NOT FINISHED**
        public static bool RunBaalLeechScript = true;       //BAAL LEECH SCRIPT
        public static bool RunBaalSearchGameScript = true;  //BAAL GAMES SEARCHER SCRIPT

        //#######################################
        //MERC SETTINGS
        //#######################################
        public static bool UsingMerc = true;
        public static int MercTakeHPPotUnder = 40; //NOT YET WORKING

        //#######################################
        //GAME/PC SCREEN SETTINGS
        //#######################################
        public static int ScreenX = 1920;
        public static int ScreenY = 1080;
        public static int ScreenYMenu = 180;    //REMOVE 180 PIXEL FROM THE BOTTOM SCREEN TO NOT CLIC ANY BOTTOM MENU BUTTONS



    }
}
