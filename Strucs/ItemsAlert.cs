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
using System.Text.RegularExpressions;

namespace app
{
    public class ItemsAlert
    {
        Form1 Form1_0;

        public Dictionary<string, bool> PickItemsRunesKeyGems = new Dictionary<string, bool>();
        public Dictionary<string, bool> PickItemsUnique = new Dictionary<string, bool>();
        public Dictionary<string, bool> PickItemsSet = new Dictionary<string, bool>();
        public Dictionary<string, bool> PickItemsPotions = new Dictionary<string, bool>();

        public List<string> PickItemsUniqueDesc = new List<string>();
        public List<string> PickItemsSetDesc = new List<string>();

        public Dictionary<string, bool> PickItemsNormal_ByName = new Dictionary<string, bool>();
        public Dictionary<string, Dictionary<uint, string>> PickItemsNormal_ByName_Flags = new Dictionary<string, Dictionary<uint, string>>();
        public Dictionary<string, int> PickItemsNormal_ByName_Quality = new Dictionary<string, int>();
        public Dictionary<string, Dictionary<string, int>> PickItemsNormal_ByName_Stats = new Dictionary<string, Dictionary<string, int>>();
        public Dictionary<string, Dictionary<string, string>> PickItemsNormal_ByName_Operators = new Dictionary<string, Dictionary<string, string>>();
        public List<string> PickItemsNormal_ByNameDesc = new List<string>();

        public Dictionary<string, bool> PickItemsNormal_ByType = new Dictionary<string, bool>();
        public Dictionary<string, Dictionary<uint, string>> PickItemsNormal_ByType_Flags = new Dictionary<string, Dictionary<uint, string>>();
        public Dictionary<string, int> PickItemsNormal_ByType_Quality = new Dictionary<string, int>();
        public Dictionary<string, Dictionary<string, int>> PickItemsNormal_ByType_Stats = new Dictionary<string, Dictionary<string, int>>();
        public Dictionary<string, Dictionary<string, string>> PickItemsNormal_ByType_Operators = new Dictionary<string, Dictionary<string, string>>();
        public List<string> PickItemsNormal_ByTypeDesc = new List<string>();


        public Dictionary<string, bool> PickItemsRare_ByName = new Dictionary<string, bool>();
        public Dictionary<string, Dictionary<uint, string>> PickItemsRare_ByName_Flags = new Dictionary<string, Dictionary<uint, string>>();
        public Dictionary<string, int> PickItemsRare_ByName_Quality = new Dictionary<string, int>();
        public Dictionary<string, Dictionary<string, int>> PickItemsRare_ByName_Stats = new Dictionary<string, Dictionary<string, int>>();
        public Dictionary<string, Dictionary<string, string>> PickItemsRare_ByName_Operators = new Dictionary<string, Dictionary<string, string>>();
        public List<string> PickItemsRare_ByNameDesc = new List<string>();

        public Dictionary<string, bool> PickItemsRare_ByType = new Dictionary<string, bool>();
        public Dictionary<string, Dictionary<uint, string>> PickItemsRare_ByType_Flags = new Dictionary<string, Dictionary<uint, string>>();
        public Dictionary<string, int> PickItemsRare_ByType_Quality = new Dictionary<string, int>();
        public Dictionary<string, Dictionary<string, int>> PickItemsRare_ByType_Stats = new Dictionary<string, Dictionary<string, int>>();
        public Dictionary<string, Dictionary<string, string>> PickItemsRare_ByType_Operators = new Dictionary<string, Dictionary<string, string>>();
        public List<string> PickItemsRare_ByTypeDesc = new List<string>();

        public void SetForm1(Form1 form1_1)
        {
            Form1_0 = form1_1;
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
                foreach (var ThisDir in PickItemsUnique)
                {
                    if (Form1_0.ItemsStruc_0.ItemNAAME.ToLower().Replace(" ", "") == ThisDir.Key.ToLower().Replace(" ", "") && ThisDir.Value)
                    {
                        return true;
                    }
                }
            }

            foreach (var ThisDir in PickItemsRunesKeyGems)
            {
                if (Form1_0.ItemsStruc_0.ItemNAAME.ToLower().Replace(" ", "") == ThisDir.Key.ToLower().Replace(" ", "") && ThisDir.Value)
                {
                    if (Form1_0.ItemsStruc_0.ItemNAAME.Contains("Chipped") || Form1_0.ItemsStruc_0.ItemNAAME.Contains("Flawed")
                        || Form1_0.ItemsStruc_0.ItemNAAME == "Topaz"
                        || Form1_0.ItemsStruc_0.ItemNAAME == "Amethyst"
                        || Form1_0.ItemsStruc_0.ItemNAAME == "Sapphire"
                        || Form1_0.ItemsStruc_0.ItemNAAME == "Emerald"
                        || Form1_0.ItemsStruc_0.ItemNAAME == "Ruby"
                        || Form1_0.ItemsStruc_0.ItemNAAME == "Diamond")
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

            /*if (Form1_0.ItemsStruc_0.ItemNAAME.ToLower() == "Small Charm".ToLower()
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
                            || (Form1_0.ItemsStruc_0.ItemNAAME.ToLower() == "Small Charm".ToLower() && Form1_0.ItemsStruc_0.IsItemHaveSameStat("MagicFind", 6, ">="))
                            || (Form1_0.ItemsStruc_0.ItemNAAME.ToLower() == "Small Charm".ToLower() && Form1_0.ItemsStruc_0.IsItemHaveSameStat("FireResist", 5, ">=") && Form1_0.ItemsStruc_0.IsItemHaveSameStat("LightningResist", 5, ">=") && Form1_0.ItemsStruc_0.IsItemHaveSameStat("ColdResist", 5, ">=") && Form1_0.ItemsStruc_0.IsItemHaveSameStat("PoisonResist", 5, ">=")))
                        {
                            //Console.WriteLine("Keep charm!");
                            return true;
                        }
                        //return true;
                    }
                }
            }*/

            if (Form1_0.ItemsStruc_0.quality == "Set")
            {
                foreach (var ThisDir in PickItemsSet)
                {
                    if (Form1_0.ItemsStruc_0.ItemNAAME.ToLower().Replace(" ", "") == ThisDir.Key.ToLower().Replace(" ", "") && ThisDir.Value)
                    {
                        return true;
                    }
                }
            }


            //###############
            foreach (var ThisDir in PickItemsNormal_ByName)
            {
                if (Form1_0.ItemsStruc_0.ItemNAAME.ToLower().Replace(" ", "") == Regex.Replace(ThisDir.Key.ToLower().Replace(" ", ""), @"[\d-]", string.Empty) && ThisDir.Value)
                {
                    bool SameFlags = true;
                    bool SameQuality = true;
                    bool SameStats = true;

                    if (PickItemsNormal_ByName_Flags.ContainsKey(ThisDir.Key))
                    {
                        bool BufferId = Form1_0.ItemsStruc_0.identified;
                        bool BufferSock = Form1_0.ItemsStruc_0.isSocketed;
                        bool BufferEth = Form1_0.ItemsStruc_0.ethereal;

                        uint TotalFlags = 0;
                        foreach (var ThisList in PickItemsNormal_ByName_Flags[ThisDir.Key]) TotalFlags += ThisList.Key;
                        Form1_0.ItemsFlags_0.calculateFlags(TotalFlags);

                        //might have issue here with flags
                        if (BufferId != Form1_0.ItemsStruc_0.identified
                            && BufferSock != Form1_0.ItemsStruc_0.isSocketed
                            && BufferEth != Form1_0.ItemsStruc_0.ethereal)
                        {
                            SameFlags = false;
                        }
                        //Console.WriteLine(Form1_0.ItemsStruc_0.ItemNAAME + ":" + SameFlags);
                    }
                    if (PickItemsNormal_ByName_Quality.ContainsKey(ThisDir.Key))
                    {
                        if (Form1_0.ItemsStruc_0.quality != Form1_0.ItemsStruc_0.getQuality(PickItemsNormal_ByName_Quality[ThisDir.Key])) SameQuality = false;
                    }
                    if (PickItemsNormal_ByName_Stats.ContainsKey(ThisDir.Key))
                    {
                        foreach (var ThisDir2 in PickItemsNormal_ByName_Stats[ThisDir.Key])
                        {
                            //Console.WriteLine(Form1_0.ItemsStruc_0.ItemNAAME + ":" + ThisDir2.Key + "=" + ThisDir2.Value);
                            if (!Form1_0.ItemsStruc_0.IsItemHaveSameStatMultiCheck(ThisDir2.Key, ThisDir2.Value, PickItemsNormal_ByName_Operators[ThisDir.Key][ThisDir2.Key])) SameStats = false;
                        }
                    }


                    if (SameFlags && SameQuality && SameStats) return true;
                }
            }
            //###############
            foreach (var ThisDir in PickItemsNormal_ByType)
            {
                if (IsItemThisType(Regex.Replace(ThisDir.Key.ToLower().Replace(" ", ""), @"[\d-]", string.Empty)) && ThisDir.Value)
                {
                    bool SameFlags = true;
                    bool SameQuality = true;
                    bool SameStats = true;

                    if (PickItemsNormal_ByType_Flags.ContainsKey(ThisDir.Key))
                    {
                        bool BufferId = Form1_0.ItemsStruc_0.identified;
                        bool BufferSock = Form1_0.ItemsStruc_0.isSocketed;
                        bool BufferEth = Form1_0.ItemsStruc_0.ethereal;

                        uint TotalFlags = 0;
                        foreach (var ThisList in PickItemsNormal_ByType_Flags[ThisDir.Key]) TotalFlags += ThisList.Key;
                        Form1_0.ItemsFlags_0.calculateFlags(TotalFlags);

                        //might have issue here with flags
                        if (BufferId != Form1_0.ItemsStruc_0.identified
                            && BufferSock != Form1_0.ItemsStruc_0.isSocketed
                            && BufferEth != Form1_0.ItemsStruc_0.ethereal)
                        {
                            SameFlags = false;
                        }
                    }
                    if (PickItemsNormal_ByType_Quality.ContainsKey(ThisDir.Key))
                    {
                        if (Form1_0.ItemsStruc_0.quality != Form1_0.ItemsStruc_0.getQuality(PickItemsNormal_ByType_Quality[ThisDir.Key])) SameQuality = false;
                    }
                    if (PickItemsNormal_ByType_Stats.ContainsKey(ThisDir.Key))
                    {
                        foreach (var ThisDir2 in PickItemsNormal_ByType_Stats[ThisDir.Key])
                        {
                            if (!Form1_0.ItemsStruc_0.IsItemHaveSameStatMultiCheck(ThisDir2.Key, ThisDir2.Value, PickItemsNormal_ByType_Operators[ThisDir.Key][ThisDir2.Key])) SameStats = false;
                        }
                    }

                    if (SameFlags && SameQuality && SameStats) return true;
                }
            }
            //###############

            //###############
            foreach (var ThisDir in PickItemsRare_ByName)
            {
                if (Form1_0.ItemsStruc_0.ItemNAAME.ToLower().Replace(" ", "") == Regex.Replace(ThisDir.Key.ToLower().Replace(" ", ""), @"[\d-]", string.Empty) && ThisDir.Value)
                {
                    bool SameFlags = true;
                    bool SameQuality = true;
                    bool SameStats = true;

                    if (PickItemsRare_ByName_Flags.ContainsKey(ThisDir.Key))
                    {
                        bool BufferId = Form1_0.ItemsStruc_0.identified;
                        bool BufferSock = Form1_0.ItemsStruc_0.isSocketed;
                        bool BufferEth = Form1_0.ItemsStruc_0.ethereal;

                        uint TotalFlags = 0;
                        foreach (var ThisList in PickItemsRare_ByName_Flags[ThisDir.Key]) TotalFlags += ThisList.Key;
                        Form1_0.ItemsFlags_0.calculateFlags(TotalFlags);

                        //might have issue here with flags
                        if (BufferId != Form1_0.ItemsStruc_0.identified
                            && BufferSock != Form1_0.ItemsStruc_0.isSocketed
                            && BufferEth != Form1_0.ItemsStruc_0.ethereal)
                        {
                            SameFlags = false;
                        }
                    }
                    if (PickItemsRare_ByName_Quality.ContainsKey(ThisDir.Key))
                    {
                        if (Form1_0.ItemsStruc_0.quality != Form1_0.ItemsStruc_0.getQuality(PickItemsRare_ByName_Quality[ThisDir.Key])) SameQuality = false;
                    }
                    if (PickItemsRare_ByName_Stats.ContainsKey(ThisDir.Key))
                    {
                        foreach (var ThisDir2 in PickItemsRare_ByName_Stats[ThisDir.Key])
                        {
                            if (!Form1_0.ItemsStruc_0.IsItemHaveSameStatMultiCheck(ThisDir2.Key, ThisDir2.Value, PickItemsRare_ByName_Operators[ThisDir.Key][ThisDir2.Key])) SameStats = false;
                        }
                    }

                    if (SameFlags && SameQuality && SameStats) return true;
                }
            }
            //###############
            foreach (var ThisDir in PickItemsRare_ByType)
            {
                if (IsItemThisType(Regex.Replace(ThisDir.Key.ToLower().Replace(" ", ""), @"[\d-]", string.Empty)) && ThisDir.Value)
                {
                    bool SameFlags = true;
                    bool SameQuality = true;
                    bool SameStats = true;

                    if (PickItemsRare_ByType_Flags.ContainsKey(ThisDir.Key))
                    {
                        bool BufferId = Form1_0.ItemsStruc_0.identified;
                        bool BufferSock = Form1_0.ItemsStruc_0.isSocketed;
                        bool BufferEth = Form1_0.ItemsStruc_0.ethereal;

                        uint TotalFlags = 0;
                        foreach(var ThisList in PickItemsRare_ByType_Flags[ThisDir.Key]) TotalFlags += ThisList.Key;
                        Form1_0.ItemsFlags_0.calculateFlags(TotalFlags);

                        //might have issue here with flags
                        if (BufferId != Form1_0.ItemsStruc_0.identified
                            && BufferSock != Form1_0.ItemsStruc_0.isSocketed
                            && BufferEth != Form1_0.ItemsStruc_0.ethereal)
                        {
                            SameFlags = false;
                        }
                    }
                    if (PickItemsRare_ByType_Quality.ContainsKey(ThisDir.Key))
                    {
                        if (Form1_0.ItemsStruc_0.quality != Form1_0.ItemsStruc_0.getQuality(PickItemsRare_ByType_Quality[ThisDir.Key])) SameQuality = false;
                    }
                    if (PickItemsRare_ByType_Stats.ContainsKey(ThisDir.Key))
                    {
                        foreach (var ThisDir2 in PickItemsRare_ByType_Stats[ThisDir.Key])
                        {
                            if (!Form1_0.ItemsStruc_0.IsItemHaveSameStatMultiCheck(ThisDir2.Key, ThisDir2.Value, PickItemsRare_ByType_Operators[ThisDir.Key][ThisDir2.Key])) SameStats = false;
                        }
                    }

                    if (SameFlags && SameQuality && SameStats) return true;
                }
            }
            //###############


            /*if (Form1_0.ItemsStruc_0.quality == "Normal"
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
            }*/

            return false;
            //return PickOrKeepItem(false);
        }

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

        public string GetItemTypeText()
        {
            return "[Type] == " + GetItemType();
        }

        public string GetItemType()
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
                return "helm";
            }
            if (Form1_0.ItemsStruc_0.ItemNAAME.Contains("Gloves")
                || Form1_0.ItemsStruc_0.ItemNAAME.Contains("Gauntlets")
                || Form1_0.ItemsStruc_0.ItemNAAME == "Heavy Bracers"
                || Form1_0.ItemsStruc_0.ItemNAAME == "Vambraces"
                || Form1_0.ItemsStruc_0.ItemNAAME == "Bramble Mitts")
            {
                return "gloves";
            }
            if (Form1_0.ItemsStruc_0.ItemNAAME.Contains("Boots")
                || Form1_0.ItemsStruc_0.ItemNAAME.Contains("Greaves"))
            {
                return "boots";
            }
            if (Form1_0.ItemsStruc_0.ItemNAAME.Contains("Belt")
                || Form1_0.ItemsStruc_0.ItemNAAME.Contains("Sash")
                || Form1_0.ItemsStruc_0.ItemNAAME == "Mithril Coil"
                || Form1_0.ItemsStruc_0.ItemNAAME == "Colossus Girdle")
            {
                return "belt";
            }
            if (Form1_0.ItemsStruc_0.ItemNAAME == "Ring") return "ring";
            if (Form1_0.ItemsStruc_0.ItemNAAME == "Amulet") return "amulet";
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
                    return "armor";
                }
            }
            catch { }
            if (Form1_0.ItemsStruc_0.ItemNAAME == "Circlet"
                || Form1_0.ItemsStruc_0.ItemNAAME == "Tiara"
                || Form1_0.ItemsStruc_0.ItemNAAME == "Diadem")
            {
                return "circlet";
            }
            if (Form1_0.ItemsStruc_0.ItemNAAME == "Gold") return "gold";
            if (Form1_0.ItemsStruc_0.ItemNAAME == "Jewel") return "jewel";
            return "";
        }

    }
}
