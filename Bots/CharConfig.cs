﻿using System;
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
        public static int StartStopKey = 0x65;   //numpad5 -> refer to virtual key code (vkcode)
        
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
        public static string DummyItemSharedStash1 = "Key";                     //PUT THIS DUMMY ITEM IN SHARED STASH1 SO ITEMS DETECTION CORRECTLY APPLY TO THIS STASH
        public static string DummyItemSharedStash2 = "Scroll of Identify";      //PUT THIS DUMMY ITEM IN SHARED STASH2 SO ITEMS DETECTION CORRECTLY APPLY TO THIS STASH
        public static string DummyItemSharedStash3 = "Scroll of Town Portal";   //PUT THIS DUMMY ITEM IN SHARED STASH3 SO ITEMS DETECTION CORRECTLY APPLY TO THIS STASH

        //#######################################
        //PLAYER SETTINGS
        //#######################################
        public static string PlayerCharName = ""; //CHAR NAME
        public static bool UseTeleport = true;
        public static int ChickenHP = 22;           //VALUE IN PERCENT
        public static int TakeHPPotUnder = 85;      //VALUE IN PERCENT
        public static int TakeRVPotUnder = 35;      //VALUE IN PERCENT
        public static int TakeManaPotUnder = 15;    //VALUE IN PERCENT
        public static int GambleAboveGoldAmount = 100000;   //IF GOLD IN STASH EXCEEP AMOUNT, WE GAMBLE FOR GOLD

        //#######################################
        //MERC SETTINGS
        //#######################################
        public static bool UsingMerc = true;
        public static int MercTakeHPPotUnder = 40; //NOT YET WORKING

        //#######################################
        //BOT SETTINGS (USE ONLY CHAOS OR BAAL, NOT BOTH AT SAME TIME)
        //#######################################
        public static int MaxGameTime = 7;                  //9MINS -> TIME IN MINUTES
        public static bool RunChaosScript = false;          //CHAOS LEECH SCRIPT **NOT FINISHED**
        public static bool RunLowerKurastScript = false;     //LOWER KURAST SCRIPT
        public static bool RunBaalLeechScript = true;       //BAAL LEECH SCRIPT
        //NOT IN GAME SCRIPT (SEARCH GAMES OR CREATE A NEW GAME)
        public static bool RunBaalSearchGameScript = true; //BAAL GAMES SEARCHER SCRIPT
        public static bool RunGameMakerScript = false;       //CREATE NEW GAME SCRIPT
        public static string GameName = "LOWERKTEST";         //SET GAME NAME, IF USING GAME MAKER
        public static string GamePass = "33";               //SET GAME PASS, IF USING GAME MAKER

        //#######################################
        //GAME/PC SCREEN SETTINGS
        //#######################################
        //public static int ScreenX = 1920;
        //public static int ScreenY = 1080;
        //public static int ScreenYMenu = 180;    //REMOVE 180 PIXEL FROM THE BOTTOM SCREEN TO NOT CLIC ANY BOTTOM MENU BUTTONS



    }
}
