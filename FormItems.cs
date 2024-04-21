using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace app
{
    public partial class FormItems : Form
    {
        Form1 Form1_0;

        public List<string> CurrentFlagsList = new List<string>();
        public List<string> CurrentFlagsOperator = new List<string>();

        public List<string> CurrentStatsList = new List<string>();
        public List<string> CurrentStatsOperators = new List<string>();
        public List<int> CurrentStatsValues = new List<int>();

        public string CurrentNewItemTxt = "";

        public FormItems(Form1 form1_1)
        {
            Form1_0 = form1_1;

            InitializeComponent();

            LoadSettings();

            labelCurrentPreview.Text = "";

            comboBoxFlags.Items.Clear();
            comboBoxFlags.Items.Add("identified");
            comboBoxFlags.Items.Add("socketed");
            comboBoxFlags.Items.Add("ethereal");

            comboBoxQuality.Items.Clear();
            comboBoxQuality.Items.Add("inferior");
            comboBoxQuality.Items.Add("normal");
            comboBoxQuality.Items.Add("superior");
            comboBoxQuality.Items.Add("magic");
            comboBoxQuality.Items.Add("set");
            comboBoxQuality.Items.Add("rare");
            comboBoxQuality.Items.Add("unique");
            comboBoxQuality.Items.Add("crafted");
            comboBoxQuality.Items.Add("tempered");

            comboBoxOperator.Items.Clear();
            comboBoxOperator.Items.Add("==");
            comboBoxOperator.Items.Add("<=");
            comboBoxOperator.Items.Add(">=");
            comboBoxOperator.Items.Add("!=");

            comboBoxOperatorFlag.Items.Clear();
            comboBoxOperatorFlag.Items.Add("==");
            comboBoxOperatorFlag.Items.Add("!=");

            comboBoxStats.Items.Clear();
            foreach (Enums.Attribute ThisV in Enum.GetValues(typeof(Enums.Attribute))) comboBoxStats.Items.Add(ThisV);

            comboBoxNameOrType.SelectedIndex = 0;
            SetNameOrType();
            comboBoxFlags.SelectedIndex = 0;
            comboBoxQuality.SelectedIndex = 0;
            comboBoxStats.SelectedIndex = 0;
            comboBoxOperator.SelectedIndex = 0;
            comboBoxOperatorFlag.SelectedIndex = 0;

            SetCurrentPreview();
        }

        public void SetCurrentPreview()
        {
            string PrevTxt = "";
            if (comboBoxNameOrType.SelectedIndex == 0) PrevTxt += "[Name] == " + comboBoxName.Text;
            if (comboBoxNameOrType.SelectedIndex == 1) PrevTxt += "[Type] == " + comboBoxName.Text;

            for (int i = 0; i < CurrentFlagsList.Count; i++) PrevTxt += " && [Flag] " + CurrentFlagsOperator[i] + " " + CurrentFlagsList[i];
            PrevTxt += " && [Quality] == " + comboBoxQuality.Text;

            for (int i = 0; i < CurrentStatsList.Count; i++) PrevTxt += " && [" + CurrentStatsList[i] + "] " + CurrentStatsOperators[i] + " " + CurrentStatsValues[i];

            PrevTxt += " // " + textBoxDesc.Text;
            CurrentNewItemTxt = PrevTxt;
            labelCurrentPreview.Text = PrevTxt.Replace(" ", "");
        }

        public void SetNameOrType()
        {
            comboBoxName.Items.Clear();
            if (comboBoxNameOrType.SelectedIndex == 0)
            {
                for (int i = 0; i <= 658; i++) comboBoxName.Items.Add(Form1_0.ItemsNames_0.getItemBaseName(i));
            }
            else
            {
                comboBoxName.Items.Add("helm");
                comboBoxName.Items.Add("gloves");
                comboBoxName.Items.Add("boots");
                comboBoxName.Items.Add("belt");
                comboBoxName.Items.Add("ring");
                comboBoxName.Items.Add("amulet");
                comboBoxName.Items.Add("armor");
                comboBoxName.Items.Add("circlet");
                comboBoxName.Items.Add("gold");
                comboBoxName.Items.Add("jewel");
            }
            comboBoxName.SelectedIndex = 0;
        }

        public void LoadSettings()
        {
            listViewUnique.Items.Clear();
            listViewKeys.Items.Clear();
            listViewGems.Items.Clear();
            listViewRunes.Items.Clear();
            listViewSet.Items.Clear();
            listViewNormal.Items.Clear();
            listViewRare.Items.Clear();
            listViewPotions.Items.Clear();

            listViewCubingRecipes.Items.Clear();

            int CurrI = 0;
            foreach (var ThisDir in Form1_0.ItemsAlert_0.PickItemsUnique)
            {
                string[] arr = new string[2];
                arr[0] = ThisDir.Key;
                arr[1] = Form1_0.ItemsAlert_0.PickItemsUniqueDesc[CurrI];
                ListViewItem item = new ListViewItem(arr);

                listViewUnique.Items.Add(item);
                listViewUnique.Items[listViewUnique.Items.Count - 1].Checked = ThisDir.Value;
                CurrI++;
            }
            foreach (var ThisDir in Form1_0.ItemsAlert_0.PickItemsRunesKeyGems)
            {
                if (ThisDir.Key.Contains("Key of ")
                    || ThisDir.Key.Contains("Essence")
                    || ThisDir.Key.Contains("Token")
                    || ThisDir.Key.Contains("Mephisto's Brain")
                    || ThisDir.Key.Contains("Baal's Eye")
                    || ThisDir.Key.Contains("Diablo's Horn"))
                {
                    listViewKeys.Items.Add(ThisDir.Key);
                    listViewKeys.Items[listViewKeys.Items.Count - 1].Checked = ThisDir.Value;
                }

                if (ThisDir.Key.Contains("Topaz")
                    || ThisDir.Key.Contains("Amethyst")
                    || ThisDir.Key.Contains("Sapphire")
                    || ThisDir.Key.Contains("Ruby")
                    || ThisDir.Key.Contains("Emerald")
                    || ThisDir.Key.Contains("Diamond"))
                {
                    listViewGems.Items.Add(ThisDir.Key);
                    listViewGems.Items[listViewGems.Items.Count - 1].Checked = ThisDir.Value;
                }

                if (ThisDir.Key.Contains("Rune"))
                {
                    listViewRunes.Items.Add(ThisDir.Key);
                    listViewRunes.Items[listViewRunes.Items.Count - 1].Checked = ThisDir.Value;
                }
            }
            CurrI = 0;
            foreach (var ThisDir in Form1_0.ItemsAlert_0.PickItemsSet)
            {
                string[] arr = new string[2];
                arr[0] = ThisDir.Key;
                arr[1] = Form1_0.ItemsAlert_0.PickItemsSetDesc[CurrI];
                ListViewItem item = new ListViewItem(arr);

                listViewSet.Items.Add(item);
                listViewSet.Items[listViewSet.Items.Count - 1].Checked = ThisDir.Value;
                CurrI++;
            }

            CurrI = 0;
            foreach (var ThisDir in Form1_0.ItemsAlert_0.PickItemsPotions)
            {
                string[] arr = new string[2];
                arr[0] = Regex.Replace(ThisDir.Key, @"[\d-]", string.Empty);
                arr[1] = ThisDir.Key;
                ListViewItem item = new ListViewItem(arr);

                listViewPotions.Items.Add(item);
                listViewPotions.Items[listViewPotions.Items.Count - 1].Checked = ThisDir.Value;
                CurrI++;
            }

            //###########################
            //###########################
            CurrI = 0;
            foreach (var ThisDir in Form1_0.ItemsAlert_0.PickItemsNormal_ByName)
            {
                string[] arr = new string[6];
                arr[0] = Regex.Replace(ThisDir.Key, @"[\d-]", string.Empty);
                arr[1] = "";
                arr[2] = "";
                arr[3] = "";
                arr[4] = Form1_0.ItemsAlert_0.PickItemsNormal_ByNameDesc[CurrI];
                arr[5] = ThisDir.Key;
                if (Form1_0.ItemsAlert_0.PickItemsNormal_ByName_Flags.ContainsKey(ThisDir.Key))
                {
                    string FlagsTxt = "";
                    uint TotalFlags = 0;
                    foreach (var ThisList in Form1_0.ItemsAlert_0.PickItemsNormal_ByName_Flags[ThisDir.Key]) TotalFlags += ThisList.Key;
                    Form1_0.ItemsFlags_0.calculateFlags(TotalFlags);
                    if (Form1_0.ItemsStruc_0.identified) FlagsTxt += "identified";
                    if (Form1_0.ItemsStruc_0.isSocketed)
                    {
                        if (FlagsTxt != "") FlagsTxt += ", ";
                        FlagsTxt += "socketed";
                    }
                    if (Form1_0.ItemsStruc_0.ethereal) FlagsTxt += "ethereal";

                    arr[1] = FlagsTxt;
                }
                if (Form1_0.ItemsAlert_0.PickItemsNormal_ByName_Quality.ContainsKey(ThisDir.Key))
                {
                    string ThisQ = "";
                    if (Form1_0.ItemsAlert_0.PickItemsNormal_ByName_Quality[ThisDir.Key] == 1) ThisQ = "inferior";
                    if (Form1_0.ItemsAlert_0.PickItemsNormal_ByName_Quality[ThisDir.Key] == 2) ThisQ = "normal";
                    if (Form1_0.ItemsAlert_0.PickItemsNormal_ByName_Quality[ThisDir.Key] == 3) ThisQ = "superior";
                    if (Form1_0.ItemsAlert_0.PickItemsNormal_ByName_Quality[ThisDir.Key] == 4) ThisQ = "magic";
                    if (Form1_0.ItemsAlert_0.PickItemsNormal_ByName_Quality[ThisDir.Key] == 5) ThisQ = "set";
                    if (Form1_0.ItemsAlert_0.PickItemsNormal_ByName_Quality[ThisDir.Key] == 6) ThisQ = "rare";
                    if (Form1_0.ItemsAlert_0.PickItemsNormal_ByName_Quality[ThisDir.Key] == 7) ThisQ = "unique";
                    if (Form1_0.ItemsAlert_0.PickItemsNormal_ByName_Quality[ThisDir.Key] == 8) ThisQ = "crafted";
                    if (Form1_0.ItemsAlert_0.PickItemsNormal_ByName_Quality[ThisDir.Key] == 9) ThisQ = "tempered";
                    arr[2] = ThisQ;
                }
                if (Form1_0.ItemsAlert_0.PickItemsNormal_ByName_Stats.ContainsKey(ThisDir.Key))
                {
                    foreach (var Stat in Form1_0.ItemsAlert_0.PickItemsNormal_ByName_Stats[ThisDir.Key])
                    {
                        if (arr[3] != "") arr[3] += ",";
                        arr[3] += Stat.Key + Form1_0.ItemsAlert_0.PickItemsNormal_ByName_Operators[ThisDir.Key][Stat.Key] + Stat.Value;
                    }
                }
                ListViewItem item = new ListViewItem(arr);

                ListViewGroup listViewGroup1 = new ListViewGroup("By Name");
                listViewNormal.Groups.Add(listViewGroup1);

                listViewNormal.Items.Add(item);
                listViewNormal.Items[listViewNormal.Items.Count - 1].Checked = ThisDir.Value;
                listViewNormal.Items[listViewNormal.Items.Count - 1].Group = listViewNormal.Groups[0];

                CurrI++;
            }
            //###########################
            //###########################
            CurrI = 0;
            foreach (var ThisDir in Form1_0.ItemsAlert_0.PickItemsNormal_ByType)
            {
                string[] arr = new string[6];
                arr[0] = Regex.Replace(ThisDir.Key, @"[\d-]", string.Empty);
                arr[1] = "";
                arr[2] = "";
                arr[3] = "";
                arr[4] = Form1_0.ItemsAlert_0.PickItemsNormal_ByTypeDesc[CurrI];
                arr[5] = ThisDir.Key;
                if (Form1_0.ItemsAlert_0.PickItemsNormal_ByType_Flags.ContainsKey(ThisDir.Key))
                {
                    string FlagsTxt = "";
                    uint TotalFlags = 0;
                    foreach (var ThisList in Form1_0.ItemsAlert_0.PickItemsNormal_ByType_Flags[ThisDir.Key]) TotalFlags += ThisList.Key;
                    Form1_0.ItemsFlags_0.calculateFlags(TotalFlags);
                    if (Form1_0.ItemsStruc_0.identified) FlagsTxt += "identified";
                    if (Form1_0.ItemsStruc_0.isSocketed)
                    {
                        if (FlagsTxt != "") FlagsTxt += ", ";
                        FlagsTxt += "socketed";
                    }
                    if (Form1_0.ItemsStruc_0.ethereal) FlagsTxt += "ethereal";

                    arr[1] = FlagsTxt;
                }
                if (Form1_0.ItemsAlert_0.PickItemsNormal_ByType_Quality.ContainsKey(ThisDir.Key))
                {
                    string ThisQ = "";
                    if (Form1_0.ItemsAlert_0.PickItemsNormal_ByType_Quality[ThisDir.Key] == 1) ThisQ = "inferior";
                    if (Form1_0.ItemsAlert_0.PickItemsNormal_ByType_Quality[ThisDir.Key] == 2) ThisQ = "normal";
                    if (Form1_0.ItemsAlert_0.PickItemsNormal_ByType_Quality[ThisDir.Key] == 3) ThisQ = "superior";
                    if (Form1_0.ItemsAlert_0.PickItemsNormal_ByType_Quality[ThisDir.Key] == 4) ThisQ = "magic";
                    if (Form1_0.ItemsAlert_0.PickItemsNormal_ByType_Quality[ThisDir.Key] == 5) ThisQ = "set";
                    if (Form1_0.ItemsAlert_0.PickItemsNormal_ByType_Quality[ThisDir.Key] == 6) ThisQ = "rare";
                    if (Form1_0.ItemsAlert_0.PickItemsNormal_ByType_Quality[ThisDir.Key] == 7) ThisQ = "unique";
                    if (Form1_0.ItemsAlert_0.PickItemsNormal_ByType_Quality[ThisDir.Key] == 8) ThisQ = "crafted";
                    if (Form1_0.ItemsAlert_0.PickItemsNormal_ByType_Quality[ThisDir.Key] == 9) ThisQ = "tempered";
                    arr[2] = ThisQ;
                }
                if (Form1_0.ItemsAlert_0.PickItemsNormal_ByType_Stats.ContainsKey(ThisDir.Key))
                {
                    foreach (var Stat in Form1_0.ItemsAlert_0.PickItemsNormal_ByType_Stats[ThisDir.Key])
                    {
                        if (arr[3] != "") arr[3] += ",";
                        arr[3] += Stat.Key + Form1_0.ItemsAlert_0.PickItemsNormal_ByType_Operators[ThisDir.Key][Stat.Key] + Stat.Value;
                    }
                }
                ListViewItem item = new ListViewItem(arr);

                ListViewGroup listViewGroup1 = new ListViewGroup("By Type");
                listViewNormal.Groups.Add(listViewGroup1);

                listViewNormal.Items.Add(item);
                listViewNormal.Items[listViewNormal.Items.Count - 1].Checked = ThisDir.Value;
                listViewNormal.Items[listViewNormal.Items.Count - 1].Group = listViewNormal.Groups[1];

                CurrI++;
            }
            //###########################
            //###########################


            //###########################
            //###########################
            CurrI = 0;
            foreach (var ThisDir in Form1_0.ItemsAlert_0.PickItemsRare_ByName)
            {
                string[] arr = new string[6];
                arr[0] = Regex.Replace(ThisDir.Key, @"[\d-]", string.Empty);
                arr[1] = "";
                arr[2] = "";
                arr[3] = "";
                arr[4] = Form1_0.ItemsAlert_0.PickItemsRare_ByNameDesc[CurrI];
                arr[5] = ThisDir.Key;
                if (Form1_0.ItemsAlert_0.PickItemsRare_ByName_Flags.ContainsKey(ThisDir.Key))
                {
                    string FlagsTxt = "";
                    uint TotalFlags = 0;
                    foreach (var ThisList in Form1_0.ItemsAlert_0.PickItemsRare_ByName_Flags[ThisDir.Key]) TotalFlags += ThisList.Key;
                    Form1_0.ItemsFlags_0.calculateFlags(TotalFlags);
                    if (Form1_0.ItemsStruc_0.identified) FlagsTxt += "identified";
                    if (Form1_0.ItemsStruc_0.isSocketed)
                    {
                        if (FlagsTxt != "") FlagsTxt += ", ";
                        FlagsTxt += "socketed";
                    }
                    if (Form1_0.ItemsStruc_0.ethereal) FlagsTxt += "ethereal";

                    arr[1] = FlagsTxt;
                }
                if (Form1_0.ItemsAlert_0.PickItemsRare_ByName_Quality.ContainsKey(ThisDir.Key))
                {
                    string ThisQ = "";
                    if (Form1_0.ItemsAlert_0.PickItemsRare_ByName_Quality[ThisDir.Key] == 1) ThisQ = "inferior";
                    if (Form1_0.ItemsAlert_0.PickItemsRare_ByName_Quality[ThisDir.Key] == 2) ThisQ = "normal";
                    if (Form1_0.ItemsAlert_0.PickItemsRare_ByName_Quality[ThisDir.Key] == 3) ThisQ = "superior";
                    if (Form1_0.ItemsAlert_0.PickItemsRare_ByName_Quality[ThisDir.Key] == 4) ThisQ = "magic";
                    if (Form1_0.ItemsAlert_0.PickItemsRare_ByName_Quality[ThisDir.Key] == 5) ThisQ = "set";
                    if (Form1_0.ItemsAlert_0.PickItemsRare_ByName_Quality[ThisDir.Key] == 6) ThisQ = "rare";
                    if (Form1_0.ItemsAlert_0.PickItemsRare_ByName_Quality[ThisDir.Key] == 7) ThisQ = "unique";
                    if (Form1_0.ItemsAlert_0.PickItemsRare_ByName_Quality[ThisDir.Key] == 8) ThisQ = "crafted";
                    if (Form1_0.ItemsAlert_0.PickItemsRare_ByName_Quality[ThisDir.Key] == 9) ThisQ = "tempered";
                    arr[2] = ThisQ;
                }
                if (Form1_0.ItemsAlert_0.PickItemsRare_ByName_Stats.ContainsKey(ThisDir.Key))
                {
                    foreach (var Stat in Form1_0.ItemsAlert_0.PickItemsRare_ByName_Stats[ThisDir.Key])
                    {
                        if (arr[3] != "") arr[3] += ",";
                        arr[3] += Stat.Key + Form1_0.ItemsAlert_0.PickItemsRare_ByName_Operators[ThisDir.Key][Stat.Key] + Stat.Value;
                    }
                }
                ListViewItem item = new ListViewItem(arr);

                ListViewGroup listViewGroup1 = new ListViewGroup("By Name");
                listViewRare.Groups.Add(listViewGroup1);

                listViewRare.Items.Add(item);
                listViewRare.Items[listViewRare.Items.Count - 1].Checked = ThisDir.Value;
                listViewRare.Items[listViewRare.Items.Count - 1].Group = listViewRare.Groups[0];

                CurrI++;
            }
            //###########################
            //###########################
            CurrI = 0;
            foreach (var ThisDir in Form1_0.ItemsAlert_0.PickItemsRare_ByType)
            {
                string[] arr = new string[6];
                arr[0] = Regex.Replace(ThisDir.Key, @"[\d-]", string.Empty);
                arr[1] = "";
                arr[2] = "";
                arr[3] = "";
                arr[4] = Form1_0.ItemsAlert_0.PickItemsRare_ByTypeDesc[CurrI];
                arr[5] = ThisDir.Key;
                if (Form1_0.ItemsAlert_0.PickItemsRare_ByType_Flags.ContainsKey(ThisDir.Key))
                {
                    string FlagsTxt = "";
                    uint TotalFlags = 0;
                    foreach (var ThisList in Form1_0.ItemsAlert_0.PickItemsRare_ByType_Flags[ThisDir.Key]) TotalFlags += ThisList.Key;
                    Form1_0.ItemsFlags_0.calculateFlags(TotalFlags);
                    if (Form1_0.ItemsStruc_0.identified) FlagsTxt += "identified";
                    if (Form1_0.ItemsStruc_0.isSocketed)
                    {
                        if (FlagsTxt != "") FlagsTxt += ", ";
                        FlagsTxt += "socketed";
                    }
                    if (Form1_0.ItemsStruc_0.ethereal) FlagsTxt += "ethereal";

                    arr[1] = FlagsTxt;
                }
                if (Form1_0.ItemsAlert_0.PickItemsRare_ByType_Quality.ContainsKey(ThisDir.Key))
                {
                    string ThisQ = "";
                    if (Form1_0.ItemsAlert_0.PickItemsRare_ByType_Quality[ThisDir.Key] == 1) ThisQ = "inferior";
                    if (Form1_0.ItemsAlert_0.PickItemsRare_ByType_Quality[ThisDir.Key] == 2) ThisQ = "normal";
                    if (Form1_0.ItemsAlert_0.PickItemsRare_ByType_Quality[ThisDir.Key] == 3) ThisQ = "superior";
                    if (Form1_0.ItemsAlert_0.PickItemsRare_ByType_Quality[ThisDir.Key] == 4) ThisQ = "magic";
                    if (Form1_0.ItemsAlert_0.PickItemsRare_ByType_Quality[ThisDir.Key] == 5) ThisQ = "set";
                    if (Form1_0.ItemsAlert_0.PickItemsRare_ByType_Quality[ThisDir.Key] == 6) ThisQ = "rare";
                    if (Form1_0.ItemsAlert_0.PickItemsRare_ByType_Quality[ThisDir.Key] == 7) ThisQ = "unique";
                    if (Form1_0.ItemsAlert_0.PickItemsRare_ByType_Quality[ThisDir.Key] == 8) ThisQ = "crafted";
                    if (Form1_0.ItemsAlert_0.PickItemsRare_ByType_Quality[ThisDir.Key] == 9) ThisQ = "tempered";
                    arr[2] = ThisQ;
                }
                if (Form1_0.ItemsAlert_0.PickItemsRare_ByType_Stats.ContainsKey(ThisDir.Key))
                {
                    foreach (var Stat in Form1_0.ItemsAlert_0.PickItemsRare_ByType_Stats[ThisDir.Key])
                    {
                        if (arr[3] != "") arr[3] += ",";
                        arr[3] += Stat.Key + Form1_0.ItemsAlert_0.PickItemsRare_ByType_Operators[ThisDir.Key][Stat.Key] + Stat.Value;
                    }
                }
                ListViewItem item = new ListViewItem(arr);

                ListViewGroup listViewGroup1 = new ListViewGroup("By Type");
                listViewRare.Groups.Add(listViewGroup1);

                listViewRare.Items.Add(item);
                listViewRare.Items[listViewRare.Items.Count - 1].Checked = ThisDir.Value;
                listViewRare.Items[listViewRare.Items.Count - 1].Group = listViewRare.Groups[1];

                CurrI++;
            }
            //###########################
            //###########################

            for (int i = 0; i < Form1_0.Cubing_0.CubingRecipes.Count; i++)
            {
                string[] arr = new string[1];
                arr[0] = Form1_0.Cubing_0.CubingRecipes[i];
                ListViewItem item = new ListViewItem(arr);

                listViewCubingRecipes.Items.Add(item);
                listViewCubingRecipes.Items[listViewCubingRecipes.Items.Count - 1].Checked = Form1_0.Cubing_0.CubingRecipesEnabled[i];
                CurrI++;
            }
        }

        public void SaveSettings()
        {
            foreach (var ThisDir in Form1_0.ItemsAlert_0.PickItemsUnique)
            {
                for (int i = 0; i < listViewUnique.Items.Count; i++) 
                {
                    if (ThisDir.Key == listViewUnique.Items[i].SubItems[0].ToString())
                    {
                        Form1_0.ItemsAlert_0.PickItemsUnique[ThisDir.Key] = listViewUnique.Items[i].Checked;
                    }
                }
            }
            foreach (var ThisDir in Form1_0.ItemsAlert_0.PickItemsRunesKeyGems)
            {
                if (ThisDir.Key.Contains("Key of ")
                    || ThisDir.Key.Contains("Essence")
                    || ThisDir.Key.Contains("Token")
                    || ThisDir.Key.Contains("Mephisto's Brain")
                    || ThisDir.Key.Contains("Baal's Eye")
                    || ThisDir.Key.Contains("Diablo's Horn"))
                {
                    for (int i = 0; i < listViewKeys.Items.Count; i++)
                    {
                        if (ThisDir.Key == listViewKeys.Items[i].SubItems[0].ToString())
                        {
                            Form1_0.ItemsAlert_0.PickItemsRunesKeyGems[ThisDir.Key] = listViewKeys.Items[i].Checked;
                        }
                    }
                }

                if (ThisDir.Key.Contains("Topaz")
                    || ThisDir.Key.Contains("Amethyst")
                    || ThisDir.Key.Contains("Sapphire")
                    || ThisDir.Key.Contains("Ruby")
                    || ThisDir.Key.Contains("Emerald")
                    || ThisDir.Key.Contains("Diamond"))
                {
                    for (int i = 0; i < listViewGems.Items.Count; i++)
                    {
                        if (ThisDir.Key == listViewGems.Items[i].SubItems[0].ToString())
                        {
                            Form1_0.ItemsAlert_0.PickItemsRunesKeyGems[ThisDir.Key] = listViewGems.Items[i].Checked;
                        }
                    }
                }

                if (ThisDir.Key.Contains("Rune"))
                {
                    for (int i = 0; i < listViewRunes.Items.Count; i++)
                    {
                        if (ThisDir.Key == listViewRunes.Items[i].SubItems[0].ToString())
                        {
                            Form1_0.ItemsAlert_0.PickItemsRunesKeyGems[ThisDir.Key] = listViewRunes.Items[i].Checked;
                        }
                    }
                }
            }
            foreach (var ThisDir in Form1_0.ItemsAlert_0.PickItemsSet)
            {
                for (int i = 0; i < listViewSet.Items.Count; i++)
                {
                    if (ThisDir.Key == listViewSet.Items[i].SubItems[0].ToString())
                    {
                        Form1_0.ItemsAlert_0.PickItemsSet[ThisDir.Key] = listViewSet.Items[i].Checked;
                    }
                }
            }

            foreach (var ThisDir in Form1_0.ItemsAlert_0.PickItemsPotions)
            {
                for (int i = 0; i < listViewPotions.Items.Count; i++)
                {
                    if (ThisDir.Key == listViewPotions.Items[i].SubItems[1].Text)
                    {
                        Form1_0.ItemsAlert_0.PickItemsNormal_ByName[ThisDir.Key] = listViewPotions.Items[i].Checked;
                    }
                }
            }


            //###########################
            //###########################
            foreach (var ThisDir in Form1_0.ItemsAlert_0.PickItemsNormal_ByName.ToList())
            {
                for (int i = 0; i < listViewNormal.Items.Count; i++)
                {
                    if (ThisDir.Key == listViewNormal.Items[i].SubItems[5].Text)
                    {
                        Form1_0.ItemsAlert_0.PickItemsNormal_ByName[ThisDir.Key] = listViewNormal.Items[i].Checked;
                        //Form1_0.ItemsAlert_0.PickItemsNormal_ByNameDesc[i] = listViewNormal.Items[i].SubItems[4].Text;
                    }
                }
            }
            //###########################
            //###########################
            foreach (var ThisDir in Form1_0.ItemsAlert_0.PickItemsNormal_ByType.ToList())
            {
                for (int i = 0; i < listViewNormal.Items.Count; i++)
                {
                    if (ThisDir.Key == listViewNormal.Items[i].SubItems[5].Text)
                    {
                        Form1_0.ItemsAlert_0.PickItemsNormal_ByType[ThisDir.Key] = listViewNormal.Items[i].Checked;
                        //Form1_0.ItemsAlert_0.PickItemsNormal_ByTypeDesc[i] = listViewNormal.Items[i].SubItems[4].Text;
                    }
                }
            }
            //###########################
            //###########################

            //###########################
            //###########################
            foreach (var ThisDir in Form1_0.ItemsAlert_0.PickItemsRare_ByName.ToList())
            {
                for (int i = 0; i < listViewRare.Items.Count; i++)
                {
                    if (ThisDir.Key == listViewRare.Items[i].SubItems[5].Text)
                    {
                        Form1_0.ItemsAlert_0.PickItemsRare_ByName[ThisDir.Key] = listViewRare.Items[i].Checked;
                        //Form1_0.ItemsAlert_0.PickItemsRare_ByNameDesc[i] = listViewRare.Items[i].SubItems[4].Text;
                    }
                }
            }
            //###########################
            //###########################
            foreach (var ThisDir in Form1_0.ItemsAlert_0.PickItemsRare_ByType.ToList())
            {
                for (int i = 0; i < listViewRare.Items.Count; i++)
                {
                    if (ThisDir.Key == listViewRare.Items[i].SubItems[5].Text)
                    {
                        Form1_0.ItemsAlert_0.PickItemsRare_ByType[ThisDir.Key] = listViewRare.Items[i].Checked;
                        //Form1_0.ItemsAlert_0.PickItemsRare_ByTypeDesc[i] = listViewRare.Items[i].SubItems[4].Text;
                    }
                }
            }
            //###########################
            //###########################

            for (int i = 0; i < listViewCubingRecipes.Items.Count; i++)
            {
                Form1_0.Cubing_0.CubingRecipesEnabled[i] = listViewCubingRecipes.Items[i].Checked;
            }

            Form1_0.SettingsLoader_0.SaveItemsSettings();
            Form1_0.SettingsLoader_0.SaveCubingSettings();
        }

        private void FormSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            //SaveSettings();
        }

        private void FormSettings_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            SaveSettings();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel_NewItem.Visible = true;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboBoxNameOrType_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetNameOrType();
            SetCurrentPreview();
        }

        private void comboBoxQuality_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetCurrentPreview();
        }

        private void comboBoxName_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetCurrentPreview();
        }

        private void buttonAddFlag_Click(object sender, EventArgs e)
        {
            CurrentFlagsList.Add(comboBoxFlags.Text);
            CurrentFlagsOperator.Add(comboBoxOperatorFlag.Text);

            SetCurrentPreview();
        }

        private void buttonRemoveFlag_Click(object sender, EventArgs e)
        {
            string ThisFlag = comboBoxFlags.Text;
            for (int i = 0; i < CurrentFlagsList.Count; i++)
            {
                if (CurrentFlagsList[i] == ThisFlag)
                {
                    CurrentFlagsList.RemoveAt(i);
                    CurrentFlagsOperator.RemoveAt(i);
                }
            }

            SetCurrentPreview();
        }

        private void buttonAddStat_Click(object sender, EventArgs e)
        {
            CurrentStatsList.Add(comboBoxStats.Text);
            CurrentStatsOperators.Add(comboBoxOperator.Text);
            CurrentStatsValues.Add((int) numericUpDownValue.Value);

            SetCurrentPreview();
        }

        private void buttonRemoveStat_Click(object sender, EventArgs e)
        {

            string ThisFlag = comboBoxStats.Text;
            for (int i = 0; i < CurrentStatsList.Count; i++)
            {
                if (CurrentStatsList[i] == ThisFlag)
                {
                    CurrentStatsList.RemoveAt(i);
                    CurrentStatsOperators.RemoveAt(i);
                    CurrentStatsValues.RemoveAt(i);
                }
            }

            SetCurrentPreview();
        }

        private void buttonResetStats_Click(object sender, EventArgs e)
        {
            CurrentStatsList.Clear();
            CurrentStatsOperators.Clear();
            CurrentStatsValues.Clear();

            SetCurrentPreview();
        }

        private void buttonResetFlags_Click(object sender, EventArgs e)
        {
            CurrentFlagsList.Clear();
            CurrentFlagsOperator.Clear();

            SetCurrentPreview();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            panel_NewItem.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string AllTxt = File.ReadAllText(Form1_0.SettingsLoader_0.File_ItemsSettings);
            //AllTxt += CurrentNewItemTxt + Environment.NewLine;

            if (tabControl1.SelectedIndex == 5) AllTxt = AllTxt.Replace("//###### NORMAL ITEMS\n//#######################################\n", "//###### NORMAL ITEMS\n//#######################################\n" + CurrentNewItemTxt + Environment.NewLine);
            if (tabControl1.SelectedIndex == 6) AllTxt = AllTxt.Replace("//###### RARE/MAGIC ITEMS\n//#######################################\n", "//###### RARE/MAGIC ITEMS\n//#######################################\n" + CurrentNewItemTxt + Environment.NewLine);

            File.Create(Form1_0.SettingsLoader_0.File_ItemsSettings).Dispose();
            File.WriteAllText(Form1_0.SettingsLoader_0.File_ItemsSettings, AllTxt);

            Form1_0.method_1("File saved: " + Path.GetFileName(Form1_0.SettingsLoader_0.File_ItemsSettings), Color.DarkGreen);

            Form1_0.SettingsLoader_0.AllLines = File.ReadAllLines(Form1_0.SettingsLoader_0.File_ItemsSettings);
            Form1_0.SettingsLoader_0.LoadItemsSettings();

            panel_NewItem.Visible = false;

            LoadSettings();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 5 || tabControl1.SelectedIndex == 6) buttonCreateNewItem.Visible = true;
            else buttonCreateNewItem.Visible = false;
            panel_NewItem.Visible = false;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 5)
            {
                string TxtName = "[name]==" + listViewNormal.SelectedItems[0].SubItems[0].Text;
                string TxtFlag = "";
                string TxtQuality = "";
                string TxtStats = "";

                if (listViewNormal.SelectedItems[0].SubItems[1].Text != "") TxtFlag = "[flag]==" + listViewNormal.SelectedItems[0].SubItems[1].Text;
                if (listViewNormal.SelectedItems[0].SubItems[2].Text != "") TxtQuality = "[quality]==" + listViewNormal.SelectedItems[0].SubItems[2].Text;
                if (listViewNormal.SelectedItems[0].SubItems[3].Text != "")
                {
                    TxtStats = "[name]==" + listViewNormal.SelectedItems[0].SubItems[3].Text;
                }
            }
        }

        private void listViewUnique_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
