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

public partial class FormItems : Form
{
    Form1 Form1_0;

    public List<string> CurrentFlagsList = new List<string>();
    public List<string> CurrentFlagsOperator = new List<string>();

    public List<string> CurrentStatsList = new List<string>();
    public List<string> CurrentStatsOperators = new List<string>();
    public List<int> CurrentStatsValues = new List<int>();

    public string CurrentNewItemTxt = "";

    public bool SettingsChanged = false;

    public FormItems(Form1 form1_1)
    {
        Form1_0 = form1_1;

        InitializeComponent();

        LoadSettings();

        labelCurrentPreview.Text = "";
        labelItemNameEdit.Text = "";
        panelItemQty.Visible = false;
        panelItemQty.Location = new Point(175, 16);

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
        listViewSuperior.Items.Clear();
        listViewRare.Items.Clear();
        listViewMagic.Items.Clear();
        listViewPotions.Items.Clear();

        listViewCubingRecipes.Items.Clear();

        foreach (var ThisDir in Form1_0.ItemsAlert_0.PickItemsRunesKeyGems)
        {
            if (ThisDir.Key.Contains("Keyof")
                || ThisDir.Key.Contains("Essence")
                || ThisDir.Key.Contains("Token")
                || ThisDir.Key.Contains("Mephisto'sBrain")
                || ThisDir.Key.Contains("Baal'sEye")
                || ThisDir.Key.Contains("Diablo'sHorn"))
            {
                string[] arr = new string[2];
                arr[0] = ThisDir.Key;
                arr[1] = "";

                int ThisQty = Form1_0.ItemsAlert_0.PickItemsRunesKeyGems_Quantity[ThisDir.Key];
                if (ThisQty > 0) arr[1] = ThisQty.ToString();

                ListViewItem item = new ListViewItem(arr);
                listViewKeys.Items.Add(item);
                listViewKeys.Items[listViewKeys.Items.Count - 1].Checked = ThisDir.Value;
            }

            if (ThisDir.Key.Contains("Topaz")
                || ThisDir.Key.Contains("Amethyst")
                || ThisDir.Key.Contains("Sapphire")
                || ThisDir.Key.Contains("Ruby")
                || ThisDir.Key.Contains("Emerald")
                || ThisDir.Key.Contains("Diamond"))
            {
                string[] arr = new string[2];
                arr[0] = ThisDir.Key;
                arr[1] = "";

                int ThisQty = Form1_0.ItemsAlert_0.PickItemsRunesKeyGems_Quantity[ThisDir.Key];
                if (ThisQty > 0) arr[1] = ThisQty.ToString();

                ListViewItem item = new ListViewItem(arr);
                listViewGems.Items.Add(item);
                listViewGems.Items[listViewGems.Items.Count - 1].Checked = ThisDir.Value;
            }

            if (ThisDir.Key.Contains("Rune"))
            {
                string[] arr = new string[2];
                arr[0] = ThisDir.Key;
                arr[1] = "";

                int ThisQty = Form1_0.ItemsAlert_0.PickItemsRunesKeyGems_Quantity[ThisDir.Key];
                if (ThisQty > 0) arr[1] = ThisQty.ToString();

                ListViewItem item = new ListViewItem(arr);
                listViewRunes.Items.Add(item);
                listViewRunes.Items[listViewRunes.Items.Count - 1].Checked = ThisDir.Value;
            }
        }

        foreach (var ThisDir in Form1_0.ItemsAlert_0.PickItemsPotions)
        {
            string[] arr = new string[2];
            arr[0] = Regex.Replace(ThisDir.Key, @"[\d-]", string.Empty);
            arr[1] = "";// ThisDir.Key;
            ListViewItem item = new ListViewItem(arr);

            listViewPotions.Items.Add(item);
            listViewPotions.Items[listViewPotions.Items.Count - 1].Checked = ThisDir.Value;
        }

        //###########################
        //###########################
        foreach (var ThisDir in Form1_0.ItemsAlert_0.PickItemsNormal_ByName)
        {
            string[] arr = new string[6];
            arr[0] = Regex.Replace(ThisDir.Key, @"[\d-]", string.Empty);
            arr[1] = "";
            arr[2] = "";
            arr[3] = "";
            arr[4] = Form1_0.ItemsAlert_0.PickItemsNormal_ByNameDesc[ThisDir.Key];
            arr[5] = ThisDir.Key;
            if (Form1_0.ItemsAlert_0.PickItemsNormal_ByName_Flags.ContainsKey(ThisDir.Key))
            {
                string FlagsTxt = "";
                foreach (var ThisList in Form1_0.ItemsAlert_0.PickItemsNormal_ByName_Flags[ThisDir.Key])
                {
                    if (FlagsTxt != "") FlagsTxt += ", ";
                    FlagsTxt += ThisList.Value;
                    if ((0x00000010 & ThisList.Key) != 0) FlagsTxt += "identified";
                    if ((0x00000800 & ThisList.Key) != 0) FlagsTxt += "socketed";
                    if ((0x00400000 & ThisList.Key) != 0) FlagsTxt += "ethereal";
                }

                arr[1] = FlagsTxt;
            }
            string ThisQ = "";
            if (Form1_0.ItemsAlert_0.PickItemsNormal_ByName_Quality.ContainsKey(ThisDir.Key))
            {
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
            if (ThisQ == "normal")
            {
                listViewNormal.Groups.Add(listViewGroup1);
                listViewNormal.Items.Add(item);
                listViewNormal.Items[listViewNormal.Items.Count - 1].Checked = ThisDir.Value;
                listViewNormal.Items[listViewNormal.Items.Count - 1].Group = listViewNormal.Groups[0];
            }
            else if (ThisQ == "superior")
            {
                listViewSuperior.Groups.Add(listViewGroup1);
                listViewSuperior.Items.Add(item);
                listViewSuperior.Items[listViewSuperior.Items.Count - 1].Checked = ThisDir.Value;
                listViewSuperior.Items[listViewSuperior.Items.Count - 1].Group = listViewSuperior.Groups[0];
            }
            else if (ThisQ == "rare")
            {
                listViewRare.Groups.Add(listViewGroup1);
                listViewRare.Items.Add(item);
                listViewRare.Items[listViewRare.Items.Count - 1].Checked = ThisDir.Value;
                listViewRare.Items[listViewRare.Items.Count - 1].Group = listViewRare.Groups[0];
            }
            else if (ThisQ == "magic")
            {
                listViewMagic.Groups.Add(listViewGroup1);
                listViewMagic.Items.Add(item);
                listViewMagic.Items[listViewMagic.Items.Count - 1].Checked = ThisDir.Value;
                listViewMagic.Items[listViewMagic.Items.Count - 1].Group = listViewMagic.Groups[0];
            }
            else if (ThisQ == "unique")
            {
                listViewUnique.Groups.Add(listViewGroup1);
                listViewUnique.Items.Add(item);
                listViewUnique.Items[listViewUnique.Items.Count - 1].Checked = ThisDir.Value;
                listViewUnique.Items[listViewUnique.Items.Count - 1].Group = listViewUnique.Groups[0];
            }
            else if (ThisQ == "set")
            {
                listViewSet.Groups.Add(listViewGroup1);
                listViewSet.Items.Add(item);
                listViewSet.Items[listViewSet.Items.Count - 1].Checked = ThisDir.Value;
                listViewSet.Items[listViewSet.Items.Count - 1].Group = listViewSet.Groups[0];
            }
            else
            {
                listViewNormal.Groups.Add(listViewGroup1);
                listViewNormal.Items.Add(item);
                listViewNormal.Items[listViewNormal.Items.Count - 1].Checked = ThisDir.Value;
                listViewNormal.Items[listViewNormal.Items.Count - 1].Group = listViewNormal.Groups[0];
            }
        }
        //###########################
        //###########################
        foreach (var ThisDir in Form1_0.ItemsAlert_0.PickItemsNormal_ByType)
        {
            string[] arr = new string[6];
            arr[0] = Regex.Replace(ThisDir.Key, @"[\d-]", string.Empty);
            arr[1] = "";
            arr[2] = "";
            arr[3] = "";
            arr[4] = Form1_0.ItemsAlert_0.PickItemsNormal_ByTypeDesc[ThisDir.Key];
            arr[5] = ThisDir.Key;
            if (Form1_0.ItemsAlert_0.PickItemsNormal_ByType_Flags.ContainsKey(ThisDir.Key))
            {
                string FlagsTxt = "";
                foreach (var ThisList in Form1_0.ItemsAlert_0.PickItemsNormal_ByType_Flags[ThisDir.Key])
                {
                    if (FlagsTxt != "") FlagsTxt += ", ";
                    FlagsTxt += ThisList.Value;
                    if ((0x00000010 & ThisList.Key) != 0) FlagsTxt += "identified";
                    if ((0x00000800 & ThisList.Key) != 0) FlagsTxt += "socketed";
                    if ((0x00400000 & ThisList.Key) != 0) FlagsTxt += "ethereal";
                }

                arr[1] = FlagsTxt;
            }
            string ThisQ = "";
            if (Form1_0.ItemsAlert_0.PickItemsNormal_ByType_Quality.ContainsKey(ThisDir.Key))
            {
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
            if (ThisQ == "normal")
            {
                listViewNormal.Groups.Add(listViewGroup1);
                listViewNormal.Items.Add(item);
                listViewNormal.Items[listViewNormal.Items.Count - 1].Checked = ThisDir.Value;
                listViewNormal.Items[listViewNormal.Items.Count - 1].Group = listViewNormal.Groups[1];
            }
            else if (ThisQ == "superior")
            {
                listViewSuperior.Groups.Add(listViewGroup1);
                listViewSuperior.Items.Add(item);
                listViewSuperior.Items[listViewSuperior.Items.Count - 1].Checked = ThisDir.Value;
                listViewSuperior.Items[listViewSuperior.Items.Count - 1].Group = listViewSuperior.Groups[1];
            }
            else if (ThisQ == "rare")
            {
                listViewRare.Groups.Add(listViewGroup1);
                listViewRare.Items.Add(item);
                listViewRare.Items[listViewRare.Items.Count - 1].Checked = ThisDir.Value;
                listViewRare.Items[listViewRare.Items.Count - 1].Group = listViewRare.Groups[1];
            }
            else if (ThisQ == "magic")
            {
                listViewMagic.Groups.Add(listViewGroup1);
                listViewMagic.Items.Add(item);
                listViewMagic.Items[listViewMagic.Items.Count - 1].Checked = ThisDir.Value;
                listViewMagic.Items[listViewMagic.Items.Count - 1].Group = listViewMagic.Groups[1];
            }
            else if (ThisQ == "unique")
            {
                listViewUnique.Groups.Add(listViewGroup1);
                listViewUnique.Items.Add(item);
                listViewUnique.Items[listViewUnique.Items.Count - 1].Checked = ThisDir.Value;
                listViewUnique.Items[listViewUnique.Items.Count - 1].Group = listViewUnique.Groups[1];
            }
            else if (ThisQ == "set")
            {
                listViewSet.Groups.Add(listViewGroup1);
                listViewSet.Items.Add(item);
                listViewSet.Items[listViewSet.Items.Count - 1].Checked = ThisDir.Value;
                listViewSet.Items[listViewSet.Items.Count - 1].Group = listViewSet.Groups[1];
            }
            else
            {
                listViewNormal.Groups.Add(listViewGroup1);
                listViewNormal.Items.Add(item);
                listViewNormal.Items[listViewNormal.Items.Count - 1].Checked = ThisDir.Value;
                listViewNormal.Items[listViewNormal.Items.Count - 1].Group = listViewNormal.Groups[1];
            }
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
        }
    }

    public void SaveSettings()
    {
        for (int i = 0; i < listViewKeys.Items.Count; i++)
        {
            foreach (var ThisDir in Form1_0.ItemsAlert_0.PickItemsRunesKeyGems)
            {
                if (ThisDir.Key.Contains("Keyof")
                    || ThisDir.Key.Contains("Essence")
                    || ThisDir.Key.Contains("Token")
                    || ThisDir.Key.Contains("Mephisto'sBrain")
                    || ThisDir.Key.Contains("Baal'sEye")
                    || ThisDir.Key.Contains("Diablo'sHorn"))
                {
                    if (ThisDir.Key == listViewKeys.Items[i].SubItems[0].Text.ToString())
                    {
                        Form1_0.ItemsAlert_0.PickItemsRunesKeyGems[ThisDir.Key] = listViewKeys.Items[i].Checked;
                        int ThisQty = 0;
                        if (listViewKeys.Items[i].SubItems[1].Text != "") ThisQty = int.Parse(listViewKeys.Items[i].SubItems[1].Text);
                        Form1_0.ItemsAlert_0.PickItemsRunesKeyGems_Quantity[ThisDir.Key] = ThisQty;
                        break;
                    }
                }
            }
        }

        for (int i = 0; i < listViewGems.Items.Count; i++)
        {
            foreach (var ThisDir in Form1_0.ItemsAlert_0.PickItemsRunesKeyGems)
            {
                if (ThisDir.Key.Contains("Topaz")
                    || ThisDir.Key.Contains("Amethyst")
                    || ThisDir.Key.Contains("Sapphire")
                    || ThisDir.Key.Contains("Ruby")
                    || ThisDir.Key.Contains("Emerald")
                    || ThisDir.Key.Contains("Diamond"))
                {
                    if (ThisDir.Key == listViewGems.Items[i].SubItems[0].Text.ToString())
                    {
                        Form1_0.ItemsAlert_0.PickItemsRunesKeyGems[ThisDir.Key] = listViewGems.Items[i].Checked;
                        int ThisQty = 0;
                        if (listViewGems.Items[i].SubItems[1].Text != "") ThisQty = int.Parse(listViewGems.Items[i].SubItems[1].Text);
                        Form1_0.ItemsAlert_0.PickItemsRunesKeyGems_Quantity[ThisDir.Key] = ThisQty;
                        break;
                    }
                }
            }
        }

        for (int i = 0; i < listViewRunes.Items.Count; i++)
        {
            foreach (var ThisDir in Form1_0.ItemsAlert_0.PickItemsRunesKeyGems)
            {
                if (ThisDir.Key.Contains("Rune"))
                {
                    if (ThisDir.Key == listViewRunes.Items[i].SubItems[0].Text.ToString())
                    {
                        Form1_0.ItemsAlert_0.PickItemsRunesKeyGems[ThisDir.Key] = listViewRunes.Items[i].Checked;
                        int ThisQty = 0;
                        if (listViewRunes.Items[i].SubItems[1].Text != "") ThisQty = int.Parse(listViewRunes.Items[i].SubItems[1].Text);
                        Form1_0.ItemsAlert_0.PickItemsRunesKeyGems_Quantity[ThisDir.Key] = ThisQty;
                        break;
                    }
                }
            }
        }

        for (int i = 0; i < listViewPotions.Items.Count; i++)
        {
            foreach (var ThisDir in Form1_0.ItemsAlert_0.PickItemsPotions)
            {
                if (ThisDir.Key == listViewPotions.Items[i].SubItems[0].Text.ToString())
                {
                    Form1_0.ItemsAlert_0.PickItemsPotions[ThisDir.Key] = listViewPotions.Items[i].Checked;
                    break;
                }
            }
        }

        //###########################
        //###########################
        foreach (var ThisDir in Form1_0.ItemsAlert_0.PickItemsNormal_ByName.ToList())
        {
            for (int i = 0; i < listViewUnique.Items.Count; i++)
            {
                if (ThisDir.Key == listViewUnique.Items[i].SubItems[5].Text)
                {
                    Form1_0.ItemsAlert_0.PickItemsNormal_ByName[ThisDir.Key] = listViewUnique.Items[i].Checked;
                }
            }
            for (int i = 0; i < listViewSet.Items.Count; i++)
            {
                if (ThisDir.Key == listViewSet.Items[i].SubItems[5].Text)
                {
                    Form1_0.ItemsAlert_0.PickItemsNormal_ByName[ThisDir.Key] = listViewSet.Items[i].Checked;
                }
            }
            for (int i = 0; i < listViewNormal.Items.Count; i++)
            {
                if (ThisDir.Key == listViewNormal.Items[i].SubItems[5].Text)
                {
                    Form1_0.ItemsAlert_0.PickItemsNormal_ByName[ThisDir.Key] = listViewNormal.Items[i].Checked;
                }
            }
            for (int i = 0; i < listViewNormal.Items.Count; i++)
            {
                if (ThisDir.Key == listViewNormal.Items[i].SubItems[5].Text)
                {
                    Form1_0.ItemsAlert_0.PickItemsNormal_ByName[ThisDir.Key] = listViewNormal.Items[i].Checked;
                }
            }
            for (int i = 0; i < listViewSuperior.Items.Count; i++)
            {
                if (ThisDir.Key == listViewSuperior.Items[i].SubItems[5].Text)
                {
                    Form1_0.ItemsAlert_0.PickItemsNormal_ByName[ThisDir.Key] = listViewSuperior.Items[i].Checked;
                }
            }
            for (int i = 0; i < listViewRare.Items.Count; i++)
            {
                if (ThisDir.Key == listViewRare.Items[i].SubItems[5].Text)
                {
                    Form1_0.ItemsAlert_0.PickItemsNormal_ByName[ThisDir.Key] = listViewRare.Items[i].Checked;
                }
            }
            for (int i = 0; i < listViewMagic.Items.Count; i++)
            {
                if (ThisDir.Key == listViewMagic.Items[i].SubItems[5].Text)
                {
                    Form1_0.ItemsAlert_0.PickItemsNormal_ByName[ThisDir.Key] = listViewMagic.Items[i].Checked;
                }
            }
        }
        //###########################
        //###########################
        foreach (var ThisDir in Form1_0.ItemsAlert_0.PickItemsNormal_ByType.ToList())
        {
            for (int i = 0; i < listViewUnique.Items.Count; i++)
            {
                if (ThisDir.Key == listViewUnique.Items[i].SubItems[5].Text)
                {
                    Form1_0.ItemsAlert_0.PickItemsNormal_ByType[ThisDir.Key] = listViewUnique.Items[i].Checked;
                }
            }
            for (int i = 0; i < listViewSet.Items.Count; i++)
            {
                if (ThisDir.Key == listViewSet.Items[i].SubItems[5].Text)
                {
                    Form1_0.ItemsAlert_0.PickItemsNormal_ByType[ThisDir.Key] = listViewSet.Items[i].Checked;
                }
            }
            for (int i = 0; i < listViewNormal.Items.Count; i++)
            {
                if (ThisDir.Key == listViewNormal.Items[i].SubItems[5].Text)
                {
                    Form1_0.ItemsAlert_0.PickItemsNormal_ByType[ThisDir.Key] = listViewNormal.Items[i].Checked;
                }
            }
            for (int i = 0; i < listViewSuperior.Items.Count; i++)
            {
                if (ThisDir.Key == listViewSuperior.Items[i].SubItems[5].Text)
                {
                    Form1_0.ItemsAlert_0.PickItemsNormal_ByType[ThisDir.Key] = listViewSuperior.Items[i].Checked;
                }
            }
            for (int i = 0; i < listViewRare.Items.Count; i++)
            {
                if (ThisDir.Key == listViewRare.Items[i].SubItems[5].Text)
                {
                    Form1_0.ItemsAlert_0.PickItemsNormal_ByType[ThisDir.Key] = listViewRare.Items[i].Checked;
                }
            }
            for (int i = 0; i < listViewMagic.Items.Count; i++)
            {
                if (ThisDir.Key == listViewMagic.Items[i].SubItems[5].Text)
                {
                    Form1_0.ItemsAlert_0.PickItemsNormal_ByType[ThisDir.Key] = listViewMagic.Items[i].Checked;
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

        SettingsChanged = false;
    }

    private void FormSettings_FormClosing(object sender, FormClosingEventArgs e)
    {
        if (SettingsChanged)
        {
            DialogResult result = MessageBox.Show("Do you want to save items settings?", "Unsaved changes!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes) SaveSettings();
        }
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
        CurrentStatsValues.Add((int)numericUpDownValue.Value);

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
        AllTxt = AllTxt.Replace("//###### CUSTOM CREATED ITEMS\n//#######################################\n", "//###### CUSTOM CREATED ITEMS\n//#######################################\n" + CurrentNewItemTxt + Environment.NewLine);

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
        //panel_NewItem.Visible = false;
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

    private void button4_Click(object sender, EventArgs e)
    {
        DialogResult result = openFileDialog1.ShowDialog();
        if (result == DialogResult.OK)
        {
            Form1_0.SettingsLoader_0.LoadThisFileSettings(openFileDialog1.FileName);
            LoadSettings();
            Application.DoEvents();
        }
    }

    private void listViewUnique_Click(object sender, EventArgs e)
    {
        SettingsChanged = true;
    }

    public int DoingItemQty = 0;

    private void listViewKeys_DoubleClick(object sender, EventArgs e)
    {
        if (listViewKeys.SelectedItems.Count > 0)
        {
            listViewKeys.SelectedItems[0].Checked = !listViewKeys.SelectedItems[0].Checked;
            DoingItemQty = 0;
            labelItemNameEdit.Text = listViewKeys.SelectedItems[0].SubItems[0].Text;
            int ThisQty = 0;
            if (listViewKeys.SelectedItems[0].SubItems[1].Text != "") ThisQty = int.Parse(listViewKeys.SelectedItems[0].SubItems[1].Text);
            numericUpDownItemMaxQty.Value = ThisQty;

            panelItemQty.Visible = true;
        }
    }

    private void button5_Click(object sender, EventArgs e)
    {
        if (DoingItemQty == 0) listViewKeys.SelectedItems[0].SubItems[1].Text = numericUpDownItemMaxQty.Value.ToString();
        if (DoingItemQty == 1) listViewGems.SelectedItems[0].SubItems[1].Text = numericUpDownItemMaxQty.Value.ToString();
        if (DoingItemQty == 2) listViewRunes.SelectedItems[0].SubItems[1].Text = numericUpDownItemMaxQty.Value.ToString();
        panelItemQty.Visible = false;
    }

    private void listViewGems_DoubleClick(object sender, EventArgs e)
    {
        if (listViewGems.SelectedItems.Count > 0)
        {
            listViewGems.SelectedItems[0].Checked = !listViewGems.SelectedItems[0].Checked;
            DoingItemQty = 1;
            labelItemNameEdit.Text = listViewGems.SelectedItems[0].SubItems[0].Text;
            int ThisQty = 0;
            if (listViewGems.SelectedItems[0].SubItems[1].Text != "") ThisQty = int.Parse(listViewGems.SelectedItems[0].SubItems[1].Text);
            numericUpDownItemMaxQty.Value = ThisQty;

            panelItemQty.Visible = true;
        }
    }

    private void listViewRunes_DoubleClick(object sender, EventArgs e)
    {
        if (listViewRunes.SelectedItems.Count > 0)
        {
            listViewRunes.SelectedItems[0].Checked = !listViewRunes.SelectedItems[0].Checked;
            DoingItemQty = 2;
            labelItemNameEdit.Text = listViewRunes.SelectedItems[0].SubItems[0].Text;
            int ThisQty = 0;
            if (listViewRunes.SelectedItems[0].SubItems[1].Text != "") ThisQty = int.Parse(listViewRunes.SelectedItems[0].SubItems[1].Text);
            numericUpDownItemMaxQty.Value = ThisQty;

            panelItemQty.Visible = true;
        }
    }
}
