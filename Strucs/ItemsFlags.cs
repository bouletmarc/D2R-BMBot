using app.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

public class ItemsFlags
{
    Form1 Form1_0;

    public void SetForm1(Form1 form1_1)
    {
        Form1_0 = form1_1;
    }

    public bool IsItemSameFlags(string ComparatorMethod, uint FlagsToCheck, uint ItemFlags)
    {
        bool Checking_identified = false;
        bool Checking_isSocketed = false;
        bool Checking_ethereal = false;
        //if ((0x00000010 & FlagsToCheck) != 0) Checking_identified = true;
        if ((0x00000800 & FlagsToCheck) != 0) Checking_isSocketed = true;
        if ((0x00400000 & FlagsToCheck) != 0) Checking_ethereal = true;

        bool item_identified = false;
        bool item_isSocketed = false;
        bool item_ethereal = false;
        //if ((0x00000010 & ItemFlags) != 0) item_identified = true;
        if ((0x00000800 & ItemFlags) != 0) item_isSocketed = true;
        if ((0x00400000 & ItemFlags) != 0) item_ethereal = true;

        bool SameFlags = true;
        if (ComparatorMethod == "==")
        {
            //if (Checking_identified && !item_identified) SameFlags = false;
            if (Checking_isSocketed && !item_isSocketed) SameFlags = false;
            if (Checking_ethereal && !item_ethereal) SameFlags = false;
        }
        if (ComparatorMethod == "!=")
        {
            //if (Checking_identified && item_identified) SameFlags = false;
            if (Checking_isSocketed && item_isSocketed) SameFlags = false;
            if (Checking_ethereal && item_ethereal) SameFlags = false;
        }

        return SameFlags;
    }

    public void calculateFlags(uint flags)
    {
        Form1_0.ItemsStruc_0.identified = false;
        Form1_0.ItemsStruc_0.isSocketed = false;
        Form1_0.ItemsStruc_0.inStore = false;
        Form1_0.ItemsStruc_0.ethereal = false;
        //Form1_0.ItemsStruc_0.inpersonalstash = false;

        /*if ((0x00000001 & flags) != 0)
        { //; IFLAG_TARGET
            Form1_0.ItemsStruc_0.inpersonalstash = true;
        }*/
        //if (0x00000002 & flags) {  //IFLAG_TARGET
        //}
        //if (0x00000004 & flags) {  //IFLAG_TARGETING
        //}
        //if (0x00000008 & flags) {  //IFLAG_TARGET
        //}
        if ((0x00000010 & flags) != 0)
        { //IFLAG_IDENTIFIED
            Form1_0.ItemsStruc_0.identified = true;
        }
        //if (0x00000020 & flags) {  //IFLAG_QUANTITY
        //}
        //if (0x00000040 & flags) {  //IFLAG_SWITCHIN
        //}
        //if (0x00000080 & flags) {  //IFLAG_SWITCHOUT
        //}
        //if (0x00000100 & flags) {  //IFLAG_BROKEN
        //}
        //if (0x00000200 & flags) {  //IFLAG_REPAIRED
        //}
        //if (0x00000400 & flags) {  //IFLAG_UNK1
        //}
        if ((0x00000800 & flags) != 0)
        {  //IFLAG_SOCKETED
            Form1_0.ItemsStruc_0.isSocketed = true;
        }
        //if (0x00001000 & flags) {  //IFLAG_NOSELL
        //}
        if ((0x00002000 & flags) != 0)
        {  //IFLAG_INSTORE
            Form1_0.ItemsStruc_0.inStore = true;
        }
        //if (0x00004000 & flags) {  //IFLAG_NOEQUIP
        //}
        //if (0x00008000 & flags) {  //IFLAG_NAMED
        //} 
        //if (0x00010000 & flags) {  //IFLAG_ISEAR
        //}
        ////if (0x00020000 & flags) { //IFLAG_STARTITEM
        //}  
        //if (0x00080000 & flags)  { //IFLAG_INIT
        //}
        if ((0x00400000 & flags) != 0)
        { //IFLAG_ETHEREAL
            Form1_0.ItemsStruc_0.ethereal = true;
        }
        //if (0x01000000 & flags) { //IFLAG_PERSONALIZED
        //}
        //if (0x02000000 & flags) { //IFLAG_LOWQUALITY
        //}
        //if (0x04000000 & flags) { //IFLAG_RUNEWORD
        //    this.runeword = true
        //}
        //if (0x08000000 & flags) { //IFLAG_ITEM
        //}
    }

    public void calculateFlagsPlayer(uint flags)
    {
        /*if ((0x00000001 & flags) != 0) //Player death = Player death
        if ((0x00000002 & flags) != 0) //Player standing outside town = Player standing outside town
        if ((0x00000004 & flags) != 0)// Player walking = Player walking
        if ((0x00000008 & flags) != 0)// Player running = Player running
        if ((0x00000010 & flags) != 0)// Player getting hit = Player getting hit
        if ((0x00000020 & flags) != 0)// Player standing in town = Player standing in town
        if ((0x00000040 & flags) != 0)// Player walking in town = Player walking in town
        if ((0x00000080 & flags) != 0)// Player attacking 1 = Player attacking 1
        if ((0x00000100 & flags) != 0)// Player attacking 2 = Player attacking 2
        if ((0x00000200 & flags) != 0)// Player blocking = Player blocking
        if ((0x00000400 & flags) != 0)// Player casting spell skill = Player casting spell skill
        if ((0x00000800 & flags) != 0)// Player throwing an item = Player throwing an item
        if ((0x00001000 & flags) != 0)// Player kicking = Player kicking
        if ((0x00002000 & flags) != 0)// Player using skill 1 = Player using skill 1
        if ((0x00004000 & flags) != 0)// Player using skill 2 = Player using skill 2
        if ((0x00008000 & flags) != 0)// Player using skill 3 = Player using skill 3
        if ((0x00010000 & flags) != 0)// Player using skill 4 = Player using skill 4
        if ((0x00020000 & flags) != 0)// Player dead = Player dead
        if ((0x00040000 & flags) != 0)// Player sequence = Player sequence
        if ((0x00080000 & flags) != 0)// Player being knocked back = Player being knocked back*/
    }


    /*NPC Mode Flags:
    0x00000001 =   0 = NPC death = NPC death
    0x00000002 =   1 = NPC standing still = NPC standing still
    0x00000004 =   2 = NPC walking = NPC walking
    0x00000008 =   3 = NPC getting hit = NPC getting hit
    0x00000010 =   4 = NPC attacking 1 = NPC attacking 1
    0x00000020 =   5 = NPC attacking 2 = NPC attacking 2
    0x00000040 =   6 = NPC blocking = NPC blocking
    0x00000080 =   7 = NPC casting spell skill = NPC casting spell skill
    0x00000100 =   8 = NPC using skill 1 = NPC using skill 1
    0x00000200 =   9 = NPC using skill 2 = NPC using skill 2
    0x00000400 =  10 = NPC using skill 3 = NPC using skill 3
    0x00000800 =  11 = NPC using skill 4 = NPC using skill 4
    0x00001000 =  12 = NPC dead = NPC dead
    0x00002000 =  13 = NPC being knocked back = NPC being knocked back
    0x00004000 =  14 = NPC sequence = NPC sequence
    0x00008000 =  15 = NPC running = NPC running*/
}
