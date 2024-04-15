using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Status;
using System.Diagnostics;
using System.Drawing;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;
using System.Data;
using System.Collections;
using System.Xml.Linq;
using System.Security.Cryptography;
using static System.Net.Mime.MediaTypeNames;

namespace app
{
    public class ItemsAlert
    {
        Form1 Form1_0;

        public string[] PickItemsRunesKeyGems = new string[] { };
        public string[] PickItemsUnique = new string[] { };
        public string[] PickItemsSet = new string[] { };

        public string[] PickItemsNormal = new string[] { };


        public void SetForm1(Form1 form1_1)
        {
            Form1_0 = form1_1;

            PickItemsSet = new string[]
            {
                "Lacquered Plate",      //# Tal Rasha's Guardianship
                "Death Mask",           //# Tal Rasha's Horadric Crest
                "Mesh Belt",            //# Tal Rasha's Fine Spun Cloth
                "Sacred Armor",         //# Immortal King
                "Heavy Bracers",        //# Trang-Oul's Claws
                "Winged Helm",          //# Guillaume's Face
                "Russet Armor",         //# Aldur's Advance
                "Swirling Crystal",
            };

            PickItemsUnique = new string[]
            {
                //HELMS
                "Grim Helm",     // Vampire Gaze
                "Shako",        // Harlequin Crest
                "Demonhead",    // Andariel's Visage
                "Bone Visage",   // Giant Skull
                "Spired Helm",   // Nightwing's Veil
                "Corona",       // Crown of ages
                //ARMORS
                "Serpentskin Armor",// Skin of the Vipermagi
                "Mesh Armor",    // Shaftstop
                "Sacred Armor",  // Tyrael's Might
                "Dusk Shroud",   // Ormus' Robes
                //Gloves
                "Chain Gloves",         // Chance Guards
                "Vampirebone Gloves",   // Dracul's Grasp
                "Ogre Gauntlets",       // Steelrend
                //Boots
                "Scarabshell Boots",// Sandstorm Trek
                "Boneweave Boots",  // Marrowwalk
                "War Boots",        // Gore Rider
                "Myrmidon Greaves", // Gore Rider or Shadow Dancer
                //Belts
                "War Belt",         // Thundergod's Vigor
                "Spiderweb Sash",   // Arachnid Mesh
                "Vampirefang Belt", // Nosferatu's Coil
                "Mithril Coil",     // Verdungo's Hearty Cord
                //Paladin Shields
                "Gilded Shield",    // Herald Of Zakarum
                "Zakarum Shield",   // Herald Of Zakarum
                //Sorceress Orbs
                "Swirling Crystal", // The Oculus
                "Eldritch Orb",     // Eschuta's Temper
                "Dimensional Shard",// Death's Fathom
                //Circlets
                "Tiara",            // Kira's Guardian
                "Diadem",           // Griffon's Eye
                //Necromancer Shrunken Heads
                "Hierophant Trophy",// Homunculus
                //Druid Pelts
                "Totemic Mask",     // Jalal's Mane
                //Barbarian Helms
                "Slayer Guard",     // Arreat's Face
                "Guardian Crown",   // Arreat's Face
                //Others
                "Grand Charm",      // Possible GHEED
                "Ring",             // #### ID LATER ####
                "Amulet",           // #### ID LATER ####
                "Jewel",            // #### ID LATER #### -> Rainbow Facet
            };

            PickItemsRunesKeyGems = new string[]
            {
                //"El Rune", 
                //"Eld Rune",
                //"Tir Rune",
                //"Nef Rune",
                //"Eth Rune",
                //"Ith Rune",
                //"Tal Rune",
                //"Ral Rune",
                //"Ort Rune",
                //"Thul Rune",
                //"Amn Rune",
                //"Sol Rune",
                //"Shael Rune",
                //"Dol Rune",
                "Hel Rune",
                "Io Rune",
                "Lum Rune",
                "Ko Rune",
                "Fal Rune",
                "Lem Rune",
                "Pul Rune",
                "Um Rune",
                "Mal Rune",
                "Ist Rune",
                "Gul Rune",
                "Vex Rune",
                "Ohm Rune",
                "Lo Rune",
                "Sur Rune",
                "Ber Rune",
                "Jah Rune",
                "Cham Rune",
                "Zod Rune",
                // ##### Keys ######################
                "Key of Terror",
                "Key of Hate",
                "Key of Destruction",
                // ##### Essences ######################
                "Twisted Essence Of Suffering",
                "Charged Essence Of Hatred",
                "Burning Essence Of Terror",
                "Festering Essence Of Destruction",
                "Token Of Absolution",
                // ##### Gems ######################
                "Chipped Amethyst",
                "Chipped Topaz",
                "Chipped Sapphire",
                "Chipped Ruby",
                "Chipped Emerald",
                "Chipped Diamond",

                //"Flawed Amethyst",
                //"Flawed Topaz",
                //"Flawed Sapphire",
                //"Flawed Ruby",
                //"Flawed Emerald",
                //"Flawed Diamond",

                //"Amethyst",
                //"Topaz",
                //"Sapphire",
                //"Ruby",
                //"Emerald",
                //"Diamond",

                "Flawless Amethyst",
                "Flawless Topaz",
                "Flawless Sapphire",
                "Flawless Ruby",
                "Flawless Emerald",
                "Flawless Diamond",

                "Perfect Amethyst",
                "Perfect Topaz",
                "Perfect Sapphire",
                "Perfect Ruby",
                "Perfect Emerald",
                "Perfect Diamond",
            };

            //LoadItemsList();
            //SaveList();
        }

        public bool ShouldKeepItem()
        {
            return ShouldPickItem(true);
            //return PickOrKeepItem(true);
        }

        public bool ShouldPickItem(bool Keeping)
        {
            if (Form1_0.ItemsStruc_0.quality == "Unique")
            {
                for (int i = 0; i < PickItemsUnique.Length; i++)
                {
                    if (Form1_0.ItemsStruc_0.ItemNAAME == PickItemsUnique[i])
                    {
                        return true;
                    }
                }
            }

            for (int i = 0; i < PickItemsRunesKeyGems.Length; i++)
            {
                if (Form1_0.ItemsStruc_0.ItemNAAME.ToLower() == PickItemsRunesKeyGems[i].ToLower())
                {
                    if (Form1_0.ItemsStruc_0.ItemNAAME.Contains("Chipped") || Form1_0.ItemsStruc_0.ItemNAAME.Contains("Flawed"))
                    //if (Form1_0.ItemsStruc_0.ItemNAAME.Contains("Chipped"))
                    {
                        //pick only chipped and flawed gems if count are bellow 2x
                        if (Form1_0.StashStruc_0.GetStashItemCount(Form1_0.ItemsStruc_0.ItemNAAME) < 2)
                        {
                            return true;
                        }
                    }
                    else
                    {
                        return true;
                    }
                }
            }

            //[Name] == GrandCharm && [Quality] == Magic # [ItemAddSkillTab] == 1
            if (Form1_0.ItemsStruc_0.ItemNAAME.ToLower() == "Small Charm".ToLower()
                //|| Form1_0.ItemsStruc_0.ItemNAAME.ToLower() == "Large Charm".ToLower()
                || Form1_0.ItemsStruc_0.ItemNAAME.ToLower() == "Grand Charm".ToLower())
            {
                if (!Form1_0.ItemsStruc_0.identified)
                {
                    return true;
                }
                else
                {
                    if (Keeping)
                    {
                        if ((Form1_0.ItemsStruc_0.ItemNAAME.ToLower() == "Grand Charm".ToLower() && Form1_0.ItemsStruc_0.IsItemHaveSameStat("AddSkillTab", 1, "=="))
                            || (Form1_0.ItemsStruc_0.ItemNAAME.ToLower() == "Small Charm".ToLower() && Form1_0.ItemsStruc_0.IsItemHaveSameStat("LifeMax", 19, ">="))
                            || (Form1_0.ItemsStruc_0.ItemNAAME.ToLower() == "Small Charm".ToLower() && Form1_0.ItemsStruc_0.IsItemHaveSameStat("PoisonMaxDamage", 50, ">="))
                            || (Form1_0.ItemsStruc_0.ItemNAAME.ToLower() == "Small Charm".ToLower() && Form1_0.ItemsStruc_0.IsItemHaveSameStat("MagicFind", 6, ">=")))
                        {
                            //Console.WriteLine("Keep charm!");
                            return true;
                        }
                        //return true;
                    }
                }
            }

            if (Form1_0.ItemsStruc_0.quality == "Set")
            {
                for (int i = 0; i < PickItemsSet.Length; i++)
                {
                    if (Form1_0.ItemsStruc_0.ItemNAAME.ToLower() == PickItemsSet[i].ToLower())
                    {
                        return true;
                    }
                }
            }

            if (Form1_0.ItemsStruc_0.quality == "Normal"
                || Form1_0.ItemsStruc_0.quality == "Superior")
            {
                //3-4os AP
                if (Form1_0.ItemsStruc_0.ItemNAAME == "Archon Plate"
                    && (Form1_0.ItemsStruc_0.numSockets == 3 || Form1_0.ItemsStruc_0.numSockets == 4))
                {
                    return true;
                }
                //4os sacred (fort base)
                if (Form1_0.ItemsStruc_0.ItemNAAME == "Sacred Armor"
                    && Form1_0.ItemsStruc_0.numSockets == 4
                    && Form1_0.ItemsStruc_0.ethereal)
                {
                    return true;
                }
                //4os eth armor 800+def
                if (IsItemThisType("armor")
                    && Form1_0.ItemsStruc_0.numSockets == 4
                    && Form1_0.ItemsStruc_0.GetValuesFromStats(Enums.Attribute.Defense) >= 800
                    && Form1_0.ItemsStruc_0.ethereal)
                {
                    return true;
                }
                //3os armor 560+def
                if (IsItemThisType("armor")
                    && Form1_0.ItemsStruc_0.numSockets == 3
                    && Form1_0.ItemsStruc_0.GetValuesFromStats(Enums.Attribute.Defense) >= 560
                    && !Form1_0.ItemsStruc_0.ethereal)
                {
                    return true;
                }

                //spirit or phoenix
                if (Form1_0.ItemsStruc_0.ItemNAAME == "Monarch"
                    && Form1_0.ItemsStruc_0.numSockets == 4)
                {
                    return true;
                }
                //
                if (Form1_0.ItemsStruc_0.ItemNAAME == "Vortex Shield"
                    && Form1_0.ItemsStruc_0.numSockets == 4
                    && Form1_0.ItemsStruc_0.ethereal)
                {
                    return true;
                }
                //zerk
                if (Form1_0.ItemsStruc_0.ItemNAAME == "Berserker Axe"
                    && Form1_0.ItemsStruc_0.numSockets == 6
                    && Form1_0.ItemsStruc_0.ethereal)
                {
                    return true;
                }
                //warpike
                if (Form1_0.ItemsStruc_0.ItemNAAME == "War Pike"
                    && (Form1_0.ItemsStruc_0.numSockets == 0 || Form1_0.ItemsStruc_0.numSockets == 5 || Form1_0.ItemsStruc_0.numSockets == 6)
                    && Form1_0.ItemsStruc_0.ethereal)
                {
                    return true;
                }
                //Ghost Spear
                if (Form1_0.ItemsStruc_0.ItemNAAME == "Ghost Spear"
                    && (Form1_0.ItemsStruc_0.numSockets == 0 || Form1_0.ItemsStruc_0.numSockets == 5 || Form1_0.ItemsStruc_0.numSockets == 6)
                    && Form1_0.ItemsStruc_0.ethereal)
                {
                    return true;
                }
                //spear
                if (IsItemThisType("spear")
                    && Form1_0.ItemsStruc_0.GetValuesFromStats(Enums.Attribute.MaxDamage) >= 200
                    && (Form1_0.ItemsStruc_0.numSockets == 0 || Form1_0.ItemsStruc_0.numSockets == 5 || Form1_0.ItemsStruc_0.numSockets == 6)
                    && Form1_0.ItemsStruc_0.ethereal)
                {
                    return true;
                }
                //# Helms
                if (Form1_0.ItemsStruc_0.ItemNAAME == "Bone Visage"
                    && Form1_0.ItemsStruc_0.numSockets == 3)
                {
                    return true;
                }
                if (Form1_0.ItemsStruc_0.ItemNAAME == "Boneweave"
                    && (Form1_0.ItemsStruc_0.numSockets == 3 || Form1_0.ItemsStruc_0.numSockets == 4))
                {
                    return true;
                }
                if (Form1_0.ItemsStruc_0.ItemNAAME == "Circlet"
                    && Form1_0.ItemsStruc_0.numSockets == 3)
                {
                    return true;
                }
                if (Form1_0.ItemsStruc_0.ItemNAAME == "Coronet"
                    && Form1_0.ItemsStruc_0.numSockets == 3)
                {
                    return true;
                }
                if (Form1_0.ItemsStruc_0.ItemNAAME == "Demonhead"
                    && Form1_0.ItemsStruc_0.numSockets == 3)
                {
                    return true;
                }
                if (Form1_0.ItemsStruc_0.ItemNAAME == "Diadem"
                    && Form1_0.ItemsStruc_0.numSockets == 3)
                {
                    return true;
                }
                //# Polearms
                if (Form1_0.ItemsStruc_0.ItemNAAME == "Colossus Voulge"
                    && Form1_0.ItemsStruc_0.numSockets == 4
                    && Form1_0.ItemsStruc_0.ethereal)
                {
                    return true;
                }
                if (Form1_0.ItemsStruc_0.ItemNAAME == "Giant Thresher"
                    && Form1_0.ItemsStruc_0.numSockets == 4
                    && Form1_0.ItemsStruc_0.ethereal)
                {
                    return true;
                }
                if (Form1_0.ItemsStruc_0.ItemNAAME == "Cryptic Axe"
                    && Form1_0.ItemsStruc_0.numSockets == 4
                    && Form1_0.ItemsStruc_0.ethereal)
                {
                    return true;
                }
                if (Form1_0.ItemsStruc_0.ItemNAAME == "Thresher"
                    && Form1_0.ItemsStruc_0.numSockets == 4
                    && Form1_0.ItemsStruc_0.ethereal)
                {
                    return true;
                }
                //polearm
                if (IsItemThisType("polearm")
                    && Form1_0.ItemsStruc_0.GetValuesFromStats(Enums.Attribute.MaxDamage) >= 200
                    && (Form1_0.ItemsStruc_0.numSockets == 0 || Form1_0.ItemsStruc_0.numSockets == 4)
                    && Form1_0.ItemsStruc_0.ethereal)
                {
                    return true;
                }
                //# Mace class
                if (Form1_0.ItemsStruc_0.ItemNAAME == "Flail"
                    && Form1_0.ItemsStruc_0.numSockets == 4
                    && !Form1_0.ItemsStruc_0.ethereal)
                {
                    return true;
                }
                /*if (Form1_0.ItemsStruc_0.ItemNAAME == "Flail"
                    && Form1_0.ItemsStruc_0.numSockets == 5
                    && !Form1_0.ItemsStruc_0.ethereal)
                {
                    return true;
                }*/
        }

            return false;
            //return PickOrKeepItem(false);
        }












        /*public List<string> AllItemsNames = new List<string>();
        public List<string> AllItemsTypes = new List<string>();
        public List<string> AllItemsQuality = new List<string>();
        public List<string> AllItemsClass = new List<string>();
        public List<string> AllItemsFlags = new List<string>();
        public List<string> AllItemsStats = new List<string>();

        public List<string> AllItemsFiles = new List<string>();
        public List<string> AllPossibleClass = new List<string>();*/


        /*public bool PickOrKeepItem(bool CheckForKeeping)
        {
            for (int i = 0; i < AllItemsNames.Count; i++)
            {
                bool GoodNameOrType = false;
                bool GoodQuality = false;
                bool GoodClass = false;
                bool GoodFlags = false;
                bool GoodStats = false;

                if (AllItemsNames[i] != "")
                {
                    if (Form1_0.ItemsStruc_0.ItemNAAME.ToLower() == AllItemsNames[i].ToLower())
                    {
                        GoodNameOrType = true;
                    }
                }
                else
                {
                    if (IsItemThisType(AllItemsTypes[i].ToLower()))
                    {
                        GoodNameOrType = true;
                    }
                }

                if (AllItemsQuality[i] != "")
                {
                    if (Form1_0.ItemsStruc_0.quality.ToLower() == AllItemsQuality[i].ToLower())
                    {
                        GoodQuality = true;
                    }
                }
                else
                {
                    GoodQuality = true;
                }

                //if (AllItemsClass[i] != "")
                //{
                //    if (Form1_0.ItemsStruc_0.ItemClass.ToLower() == AllItemsClass[i].ToLower())
                //    {
                //        GoodClass = true;
                //    }
                //}
                //else
                //{
                //    GoodClass = true;
                //}

                if (AllItemsFlags[i] != "")
                {
                    if (IsSameFlags(AllItemsFlags[i].ToLower()))
                    {
                        GoodFlags = true;
                    }
                }
                else
                {
                    GoodFlags = true;
                }

                if (AllItemsStats[i] != "")
                {
                    if (Form1_0.ItemsStruc_0.identified)
                    {
                        if (IsSameStats(AllItemsStats[i].ToLower()))
                        {
                            GoodStats = true;
                        }
                    }
                    else
                    {
                        //not identified return true for picking up
                        if (!CheckForKeeping)
                        {
                            GoodStats = true;
                        }
                    }
                }
                else
                {
                    GoodStats = true;
                }


                if (GoodNameOrType && GoodQuality && GoodClass && GoodFlags && GoodStats)
                {
                    return true;
                }
            }
            return false;
        }

        public void LoadItemsList()
        {
            AllItemsFiles.Add(Application.StartupPath + @"\ItemsPicker\craft.txt");
            AllItemsFiles.Add(Application.StartupPath + @"\ItemsPicker\key.txt");
            AllItemsFiles.Add(Application.StartupPath + @"\ItemsPicker\magic_rare.txt");
            AllItemsFiles.Add(Application.StartupPath + @"\ItemsPicker\normal.txt");
            AllItemsFiles.Add(Application.StartupPath + @"\ItemsPicker\set.txt");
            AllItemsFiles.Add(Application.StartupPath + @"\ItemsPicker\unique.txt");

            for (int i = 0; i < AllItemsFiles.Count; i++)
            {
                string ThisFile = AllItemsFiles[i];
                if (File.Exists(ThisFile))
                {
                    string[] AllLines = File.ReadAllLines(ThisFile);

                    for (int k = 0; k < AllLines.Length; k++)
                    {
                        if (AllLines[k].Length > 2)
                        {
                            if (AllLines[k][0] != '/' && AllLines[k][1] != '/')
                            {
                                string CurrentLine = AllLines[k].Replace(" # ", " && ").Replace(" ", "").Replace("&&", "&");

                                //Console.WriteLine(CurrentLine);
                                if (CurrentLine.Contains("&"))
                                {
                                    string[] CommandsSplit = CurrentLine.Split('&');

                                    for (int m = 0; m < CommandsSplit.Length; m++)
                                    {
                                        //Console.WriteLine(CommandsSplit[m]);

                                        string Comparator = GetComparatorInString(CommandsSplit[m]);
                                        string[] SpitCmbAndName = SplitStringWithComparator(Comparator, CommandsSplit[m]);

                                        //############# [Name]== | [Type]==
                                        if (SpitCmbAndName.Length >= 2)
                                        {
                                            //###
                                            if (SpitCmbAndName[1].Contains("//"))
                                            {
                                                SpitCmbAndName[1] = SpitCmbAndName[1].Substring(0, SpitCmbAndName[1].IndexOf('/'));
                                            }
                                            //###

                                            if (SpitCmbAndName[0] == "[Name]")
                                            {
                                                //Console.WriteLine(SpitCmbAndName[1]);
                                                AddItemToPick();
                                                SetLastItemParameters("name", SpitCmbAndName[1]);
                                            }
                                            else if (SpitCmbAndName[0] == "[Type]")
                                            {
                                                AddItemToPick();
                                                SetLastItemParameters("type", SpitCmbAndName[1]);
                                            }

                                            //#####
                                            else if (SpitCmbAndName[0] == "[Quality]")
                                            {
                                                SpitCmbAndName[0] = SpitCmbAndName[0].Replace("[", "").Replace("]", "");
                                                SetLastItemParameters("quality", SpitCmbAndName[0] + Comparator + SpitCmbAndName[1]);
                                            }
                                            else if (SpitCmbAndName[0] == "[Class]")
                                            {
                                                SpitCmbAndName[0] = SpitCmbAndName[0].Replace("[", "").Replace("]", "");
                                                SetLastItemParameters("class", SpitCmbAndName[0] + Comparator + SpitCmbAndName[1]);
                                                //AddToListClass(SpitCmbAndName[1]);
                                            }
                                            else if (SpitCmbAndName[0] == "[Flags]" || SpitCmbAndName[0] == "[Flag]")
                                            {
                                                SpitCmbAndName[0] = SpitCmbAndName[0].Replace("[", "").Replace("]", "");
                                                SetLastItemParameters("flags", SpitCmbAndName[0] + Comparator + SpitCmbAndName[1]);
                                            }
                                            else if (SpitCmbAndName[0].Contains("[") && SpitCmbAndName[0].Contains("]"))
                                            {
                                                SpitCmbAndName[0] = SpitCmbAndName[0].Replace("[", "").Replace("]", "");
                                                SetLastItemParameters("stats", SpitCmbAndName[0] + Comparator + SpitCmbAndName[1]);

                                                //if (Form1_0.ItemsStruc_0.GetStatEnumIndex(SpitCmbAndName[0]) == -1)
                                                //{
                                                //    AddToListClass(SpitCmbAndName[0]);
                                                //}
                                            }
                                        }
                                    }
                                }
                                //###

                            }
                        }
                    }
                }
                else
                {
                    Form1_0.method_1("Cannot find items picking file: " + Path.GetFileName(ThisFile));
                }
            }
        }*/

        public bool IsItemThisType(string ItemTypee)
        {
            if (ItemTypee == "helm")
            {
                if (Form1_0.ItemsStruc_0.ItemNAAME.Contains("Mask")
                    || Form1_0.ItemsStruc_0.ItemNAAME.Contains("Helm")
                    || Form1_0.ItemsStruc_0.ItemNAAME.Contains("Crown")
                    || Form1_0.ItemsStruc_0.ItemNAAME == "Demonhead"
                    || Form1_0.ItemsStruc_0.ItemNAAME == "Cap"
                    || Form1_0.ItemsStruc_0.ItemNAAME == "Basinet"
                    || Form1_0.ItemsStruc_0.ItemNAAME == "Bone Visage"
                    || Form1_0.ItemsStruc_0.ItemNAAME == "Shako"
                    || Form1_0.ItemsStruc_0.ItemNAAME == "War Hat"
                    || Form1_0.ItemsStruc_0.ItemNAAME == "Sallet"
                    || Form1_0.ItemsStruc_0.ItemNAAME == "Casque"
                    || Form1_0.ItemsStruc_0.ItemNAAME == "Armet"
                    || Form1_0.ItemsStruc_0.ItemNAAME == "Skull Cap"
                    || Form1_0.ItemsStruc_0.ItemNAAME == "Hydraskull"
                    || Form1_0.ItemsStruc_0.ItemNAAME == "Giant Conch"
                    || Form1_0.ItemsStruc_0.ItemNAAME == "Diadem"
                    || Form1_0.ItemsStruc_0.ItemNAAME == "Tiara"
                    || Form1_0.ItemsStruc_0.ItemNAAME == "Circlet")
                {
                    return true;
                }
            }
            if (ItemTypee == "gloves")
            {
                if (Form1_0.ItemsStruc_0.ItemNAAME.Contains("Gloves")
                    || Form1_0.ItemsStruc_0.ItemNAAME.Contains("Gauntlets")
                    || Form1_0.ItemsStruc_0.ItemNAAME == "Heavy Bracers"
                    || Form1_0.ItemsStruc_0.ItemNAAME == "Vambraces"
                    || Form1_0.ItemsStruc_0.ItemNAAME == "Bramble Mitts")
                {
                    return true;
                }
            }
            if (ItemTypee == "boots")
            {
                if (Form1_0.ItemsStruc_0.ItemNAAME.Contains("Boots")
                    || Form1_0.ItemsStruc_0.ItemNAAME.Contains("Greaves"))
                {
                    return true;
                }
            }
            if (ItemTypee == "belt")
            {
                if (Form1_0.ItemsStruc_0.ItemNAAME.Contains("Belt")
                    || Form1_0.ItemsStruc_0.ItemNAAME.Contains("Sash")
                    || Form1_0.ItemsStruc_0.ItemNAAME == "Mithril Coil"
                    || Form1_0.ItemsStruc_0.ItemNAAME == "Colossus Girdle")
                {
                    return true;
                }
            }
            if (ItemTypee == "ring")
            {
                if (Form1_0.ItemsStruc_0.ItemNAAME == "Ring") return true;
            }
            if (ItemTypee == "amulet")
            {
                if (Form1_0.ItemsStruc_0.ItemNAAME == "Amulet") return true;
            }
            if (ItemTypee == "armor")
            {
                try
                {
                    if (Form1_0.ItemsStruc_0.ItemNAAME.Contains("Plate")
                        || Form1_0.ItemsStruc_0.ItemNAAME.Contains("Armor")
                        || Form1_0.ItemsStruc_0.ItemNAAME.Contains("Skin")
                        || Form1_0.ItemsStruc_0.ItemNAAME.Contains("Mail")
                        || Form1_0.ItemsStruc_0.ItemNAAME.Contains("Coat")
                        || Form1_0.ItemsStruc_0.ItemNAAME.Contains("Shell")
                        || Form1_0.ItemsStruc_0.ItemNAAME == "Cuirass"
                        || Form1_0.ItemsStruc_0.ItemNAAME == "Dusk Shroud"
                        || Form1_0.ItemsStruc_0.ItemNAAME == "Wire Fleece"
                        || Form1_0.ItemsStruc_0.ItemNAAME == "Studded Leather"
                        || Form1_0.ItemsStruc_0.ItemNAAME == "Great Hauberk"
                        || Form1_0.ItemsStruc_0.ItemNAAME == "Boneweave"
                        || Form1_0.ItemsStruc_0.ItemNAAME == "Wyrmhide"
                        || Form1_0.ItemsStruc_0.ItemNAAME == "Scarab Husk"
                        || Form1_0.ItemsStruc_0.ItemNAAME == "Boneweave")
                    {
                        return true;
                    }
                }
                catch { }
            }
            if (ItemTypee == "circlet")
            {
                if (Form1_0.ItemsStruc_0.ItemNAAME == "Circlet"
                    || Form1_0.ItemsStruc_0.ItemNAAME == "Tiara"
                    || Form1_0.ItemsStruc_0.ItemNAAME == "Diadem")
                {
                    return true;
                }
            }
            if (ItemTypee == "gold")
            {
                if (Form1_0.ItemsStruc_0.ItemNAAME == "Gold") return true;
            }
            if (ItemTypee == "jewel")
            {
                if (Form1_0.ItemsStruc_0.ItemNAAME == "Jewel") return true;
            }
            return false;
        }

        /*public void AddToListClass(string thisnnnn)
        {
            //Only output 1 class -> 'elite'
            if (thisnnnn != "")
            {
                if (!AllPossibleClass.Contains(thisnnnn))
                {
                    AllPossibleClass.Add(thisnnnn);
                }
            }
        }

        public void SaveList()
        {
            string[] Alli = new string[AllPossibleClass.Count];
            for (int i = 0; i < AllPossibleClass.Count; i++) Alli[i] = AllPossibleClass[i];

            string SavePathh = Form1_0.ThisEndPath + "DumpItemStatsNameList.txt";
            File.Create(SavePathh).Dispose();
            File.WriteAllLines(SavePathh, Alli);
        }

        public void SetLastItemParameters(string ThisParam, string Desc)
        {
            if (ThisParam == "name") AllItemsNames[AllItemsNames.Count - 1] = Desc;
            if (ThisParam == "type") AllItemsTypes[AllItemsTypes.Count - 1] = Desc;
            if (ThisParam == "quality") AllItemsQuality[AllItemsQuality.Count - 1] = Desc;
            if (ThisParam == "class") AllItemsClass[AllItemsClass.Count - 1] = Desc;
            if (ThisParam == "flags") AllItemsFlags[AllItemsFlags.Count - 1] = Desc;
            if (ThisParam == "stats")
            {
                if (AllItemsStats[AllItemsStats.Count - 1] != "") AllItemsStats[AllItemsStats.Count - 1] += ",";
                AllItemsStats[AllItemsStats.Count - 1] += Desc;
            }
        }

        public void AddItemToPick()
        {
            AllItemsNames.Add("");
            AllItemsTypes.Add("");
            AllItemsQuality.Add("");
            AllItemsClass.Add("");
            AllItemsFlags.Add("");
            AllItemsStats.Add("");
        }

        public string[] SplitStringWithComparator(string Compa, string ThisLinee)
        {
            string[] Spitteed = new string[] { };
            if (Compa == "==")
            {
                Spitteed = ThisLinee.Replace("==", "=").Split('=');
            }
            if (Compa == "<=")
            {
                Spitteed = ThisLinee.Replace("<=", "=").Split('=');
            }
            if (Compa == ">=")
            {
                Spitteed = ThisLinee.Replace(">=", "=").Split('=');
            }
            if (Compa == "!=")
            {
                Spitteed = ThisLinee.Replace("!=", "=").Split('=');
            }
            if (Compa == "<")
            {
                Spitteed = ThisLinee.Replace("<", "=").Split('=');
            }
            if (Compa == ">")
            {
                Spitteed = ThisLinee.Replace("<", "=").Split('=');
            }
            return Spitteed;
        }

        public string GetComparatorInString(string ThisC)
        {
            string Comparator = "";
            if (ThisC.Contains("=="))
            {
                Comparator = "==";
            }
            if (ThisC.Contains("<="))
            {
                Comparator = "<=";
            }
            if (ThisC.Contains(">="))
            {
                Comparator = ">=";
            }
            if (ThisC.Contains("!="))
            {
                Comparator = "!=";
            }
            if (ThisC.Contains("<") && !ThisC.Contains("<="))
            {
                Comparator = "<";
            }
            if (ThisC.Contains(">") && !ThisC.Contains(">="))
            {
                Comparator = ">";
            }
            return Comparator;
        }

        public bool IsSameStats(string ThisStats)
        {
            bool SameStats = false;
            List<string> StatListCheck = new List<string>();
            List<string> StatValuesCheck = new List<string>();
            List<string> StatParamsCheck = new List<string>();
            if (ThisStats.Contains(","))
            {
                string[] StatsSplit = ThisStats.Split(',');
                for (int i = 0; i < StatsSplit.Length; i++)
                {
                    string Comparator = GetComparatorInString(StatsSplit[i]);
                    string[] SplitCmbAndName = SplitStringWithComparator(Comparator, StatsSplit[i]);

                    StatListCheck.Add(SplitCmbAndName[0]);
                    StatValuesCheck.Add(SplitCmbAndName[1]);
                    StatParamsCheck.Add(Comparator);
                }
            }
            else
            {
                string Comparator = GetComparatorInString(ThisStats);
                string[] SplitCmbAndName = SplitStringWithComparator(Comparator, ThisStats);

                StatListCheck.Add(SplitCmbAndName[0]);
                StatValuesCheck.Add(SplitCmbAndName[1]);
                StatParamsCheck.Add(Comparator);
            }

            //compare stats
            for (int i = 0; i < StatListCheck.Count; i++)
            {
                try
                {
                    string CheckName = StatListCheck[i];
                    int CheckValue = int.Parse(StatValuesCheck[i]);
                    string CheckComparator = StatParamsCheck[i];

                    if (!CheckName.Contains("+"))
                    {
                        SameStats = Form1_0.ItemsStruc_0.IsItemHaveSameStat(CheckName, CheckValue, CheckComparator);
                    }
                    else
                    {
                        //multi stats comparaison
                        string[] AllCheckNames = CheckName.Split('+');
                        SameStats = Form1_0.ItemsStruc_0.IsItemHaveSameStatMulti(AllCheckNames, CheckValue, CheckComparator);
                    }
                }
                catch
                {
                    Form1_0.method_1("Something wrong with stat: " + StatListCheck[i] + StatParamsCheck[i] + StatValuesCheck[i]);
                }
            }

            return SameStats;
        }

        public bool IsSameFlags(string ThisFlags)
        {
            bool SameFlags = false;

            //identified
            if (ThisFlags.Contains("==identified") && Form1_0.ItemsStruc_0.identified)
            {
                SameFlags = true;
            }
            if (ThisFlags.Contains("!=identified") && !Form1_0.ItemsStruc_0.identified)
            {
                SameFlags = true;
            }
            //etheral
            if (ThisFlags.Contains("==ethereal") && Form1_0.ItemsStruc_0.ethereal)
            {
                SameFlags = true;
            }
            if (ThisFlags.Contains("!=ethereal") && !Form1_0.ItemsStruc_0.ethereal)
            {
                SameFlags = true;
            }

            return SameFlags;
        }*/

    }
}
