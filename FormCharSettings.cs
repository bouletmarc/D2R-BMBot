using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

public partial class FormCharSettings : Form
{
    public List<CheckBox> AvailableSlotList = new List<CheckBox>();
    public Form1 Form1_0;
    public bool CanReloadSettings = true;

    public FormCharSettings(Form1 form1_1)
    {
        Form1_0 = form1_1;
        InitializeComponent();
        this.TopMost = true;

        panelHelpKeys.Location = new Point(4, 248);
        panelHelpKeys.Visible = false;

        comboBoxAvoidImmune.SelectedIndex = 0;

        comboBoxItemsNames.Items.Clear();
        for (int i = 0; i <= 658; i++) comboBoxItemsNames.Items.Add(Form1_0.ItemsNames_0.getItemBaseName(i));

        panelGamble.Location = new Point(136, 73);

        textBoxLeftSkill.Items.Clear();
        textBoxRightSkill.Items.Clear();
        textBoxFastMoveTown.Items.Clear();
        textBoxFastMoveTeleport.Items.Clear();
        textBoxDefenseSkill.Items.Clear();
        textBoxCastDefenseSkill.Items.Clear();
        textBoxLifeSkill.Items.Clear();
        textBoxBattleOrder.Items.Clear();
        textBoxBattleCommand.Items.Clear();
        textBoxBattleCry.Items.Clear();

        comboBoxPot1.Items.Clear();
        comboBoxPot2.Items.Clear();
        comboBoxPot3.Items.Clear();
        comboBoxPot4.Items.Clear();
        string[] names = Enum.GetNames(typeof(System.Windows.Forms.Keys));
        for (int i = 0; i < names.Length; i++)
        {
            textBoxLeftSkill.Items.Add(names[i]);
            textBoxRightSkill.Items.Add(names[i]);
            textBoxFastMoveTown.Items.Add(names[i]);
            textBoxFastMoveTeleport.Items.Add(names[i]);
            textBoxDefenseSkill.Items.Add(names[i]);
            textBoxCastDefenseSkill.Items.Add(names[i]);
            textBoxLifeSkill.Items.Add(names[i]);
            textBoxBattleOrder.Items.Add(names[i]);
            textBoxBattleCommand.Items.Add(names[i]);
            textBoxBattleCry.Items.Add(names[i]);

            comboBoxPot1.Items.Add(names[i]);
            comboBoxPot2.Items.Add(names[i]);
            comboBoxPot3.Items.Add(names[i]);
            comboBoxPot4.Items.Add(names[i]);
        }

        for (int i = 0; i < 40; i++)
        {
            CheckBox CheckBox_0 = new CheckBox();
            CheckBox_0.Size = new Size(15, 15);
            AvailableSlotList.Add(CheckBox_0);
            this.groupBoxInventory.Controls.Add(this.AvailableSlotList[AvailableSlotList.Count - 1]);
        }

        int ThisXPos = 0;
        int ThixYPos = 0;
        for (int i = 0; i < 40; i++)
        {
            if (i < 10)
            {
                ThisXPos = this.Location.X + 5 + (i * 18);
                ThixYPos = 20;
            }
            if (i >= 10 && i < 20)
            {
                ThisXPos = this.Location.X + 5 + ((i - 10) * 18);
                ThixYPos = 20 + (18 * 1);
            }
            if (i >= 20 && i < 30)
            {
                ThisXPos = this.Location.X + 5 + ((i - 20) * 18);
                ThixYPos = 20 + (18 * 2);
            }
            if (i >= 30 && i < 40)
            {
                ThisXPos = this.Location.X + 5 + ((i - 30) * 18);
                ThixYPos = 20 + (18 * 3);
            }

            AvailableSlotList[i].Location = new System.Drawing.Point(ThisXPos, ThixYPos);
        }

        LoadSettings();
    }

    public void LoadSettings()
    {
        CanReloadSettings = false;

        if (CharConfig.RunningOnChar == "Paladin") comboBoxType.SelectedIndex = 0;
        if (CharConfig.RunningOnChar == "Sorceress") comboBoxType.SelectedIndex = 1;
        if (CharConfig.RunningOnChar == "Amazon") comboBoxType.SelectedIndex = 2;
        if (CharConfig.RunningOnChar == "Assassin") comboBoxType.SelectedIndex = 3;
        if (CharConfig.RunningOnChar == "Druid") comboBoxType.SelectedIndex = 4;
        if (CharConfig.RunningOnChar == "Barbarian") comboBoxType.SelectedIndex = 5;
        if (CharConfig.RunningOnChar == "Necromancer") comboBoxType.SelectedIndex = 6;

        textBoxLeftSkill.Text = CharConfig.KeySkillAttack.ToString();
        textBoxRightSkill.Text = CharConfig.KeySkillAura.ToString();
        textBoxFastMoveTown.Text = CharConfig.KeySkillfastMoveAtTown.ToString();
        textBoxFastMoveTeleport.Text = CharConfig.KeySkillfastMoveOutsideTown.ToString();
        textBoxDefenseSkill.Text = CharConfig.KeySkillDefenseAura.ToString();
        textBoxCastDefenseSkill.Text = CharConfig.KeySkillCastDefense.ToString();
        textBoxLifeSkill.Text = CharConfig.KeySkillLifeAura.ToString();
        textBoxBattleOrder.Text = CharConfig.KeySkillBattleOrder.ToString();
        textBoxBattleCommand.Text = CharConfig.KeySkillBattleCommand.ToString();
        textBoxBattleCry.Text = CharConfig.KeySkillBattleCry.ToString();
        comboBoxKeyOpenInventory.Text = CharConfig.KeyOpenInventory.ToString();
        comboBoxKeyForceMovement.Text = CharConfig.KeyForceMovement.ToString();
        comboBoxKeyWeaponSwap.Text = CharConfig.KeySwapWeapon.ToString();

        comboBoxPot1.Text = CharConfig.KeyPotion1.ToString();
        comboBoxPot2.Text = CharConfig.KeyPotion2.ToString();
        comboBoxPot3.Text = CharConfig.KeyPotion3.ToString();
        comboBoxPot4.Text = CharConfig.KeyPotion4.ToString();

        checkBoxAttachRightHand.Checked = CharConfig.PlayerAttackWithRightHand;

        checkBoxUseMerc.Checked = CharConfig.UsingMerc;
        checkBoxTownMercDead.Checked = CharConfig.TownIfMercDead;
        numericUpDownMercTakeHPUnder.Value = CharConfig.MercTakeHPPotUnder;

        textBoxCharName.Text = CharConfig.PlayerCharName;
        numericUpDownChickenHP.Value = CharConfig.ChickenHP;
        numericUpDownTakeHP.Value = CharConfig.TakeHPPotUnder;
        numericUpDownTakeMana.Value = CharConfig.TakeManaPotUnder;
        numericUpDownTakeRV.Value = CharConfig.TakeRVPotUnder;
        numericUpDownGambleAbove.Value = CharConfig.GambleAboveGoldAmount;
        numericUpDownGambleUntil.Value = CharConfig.GambleUntilGoldAmount;

        checkBoxUseTeleport.Checked = CharConfig.UseTeleport;
        checkBoxUseBO.Checked = CharConfig.UseBO;
        checkBoxGrabGold.Checked = CharConfig.GrabForGold;
        checkBoxIDAtShop.Checked = CharConfig.IDAtShop;
        checkBoxDClone.Checked = CharConfig.LeaveDiabloClone;
        checkBoxGamble.Checked = CharConfig.GambleGold;

        if (!checkBoxGamble.Checked)
        {
            numericUpDownGambleAbove.Enabled = false;
            numericUpDownGambleUntil.Enabled = false;
        }

        comboBoxBelt1.SelectedIndex = CharConfig.BeltPotTypeToHave[0];
        comboBoxBelt2.SelectedIndex = CharConfig.BeltPotTypeToHave[1];
        comboBoxBelt3.SelectedIndex = CharConfig.BeltPotTypeToHave[2];
        comboBoxBelt4.SelectedIndex = CharConfig.BeltPotTypeToHave[3];

        numericUpDownKeyXPos.Value = CharConfig.KeysLocationInInventory.Item1;
        numericUpDownKeyYPos.Value = CharConfig.KeysLocationInInventory.Item2;

        for (int i = 0; i < 40; i++)
        {
            if (CharConfig.InventoryDontCheckItem[i] == 0) AvailableSlotList[i].Checked = false;
            if (CharConfig.InventoryDontCheckItem[i] == 1) AvailableSlotList[i].Checked = true;
        }

        if (CharConfig.AvoidColdImmune) comboBoxAvoidImmune.SelectedIndex = 1;
        if (CharConfig.AvoidFireImmune) comboBoxAvoidImmune.SelectedIndex = 2;
        if (CharConfig.AvoidLightImmune) comboBoxAvoidImmune.SelectedIndex = 3;
        if (CharConfig.AvoidPoisonImmune) comboBoxAvoidImmune.SelectedIndex = 4;
        if (CharConfig.AvoidMagicImmune) comboBoxAvoidImmune.SelectedIndex = 5;

        checkBoxClearAfterBoss.Checked = CharConfig.ClearAfterBoss;
        checkBoxUseKeys.Checked = CharConfig.UseKeys;
        SetUsingKeys();

        listViewGambleItems.Items.Clear();
        for (int i = 0; i < CharConfig.GambleItems.Count; i++) listViewGambleItems.Items.Add(CharConfig.GambleItems[i]);

        checkBoxKillOnlySuperUnique.Checked = CharConfig.KillOnlySuperUnique;

        CanReloadSettings = true;
    }

    public void SaveSettings()
    {
        if (comboBoxType.SelectedIndex == 0) CharConfig.RunningOnChar = "Paladin";
        if (comboBoxType.SelectedIndex == 1) CharConfig.RunningOnChar = "Sorceress";
        if (comboBoxType.SelectedIndex == 2) CharConfig.RunningOnChar = "Amazon";
        if (comboBoxType.SelectedIndex == 3) CharConfig.RunningOnChar = "Assassin";
        if (comboBoxType.SelectedIndex == 4) CharConfig.RunningOnChar = "Druid";
        if (comboBoxType.SelectedIndex == 5) CharConfig.RunningOnChar = "Barbarian";
        if (comboBoxType.SelectedIndex == 6) CharConfig.RunningOnChar = "Necromancer";


        Enum.TryParse(textBoxLeftSkill.Text, out CharConfig.KeySkillAttack);
        Enum.TryParse(textBoxRightSkill.Text, out CharConfig.KeySkillAura);
        Enum.TryParse(textBoxFastMoveTown.Text, out CharConfig.KeySkillfastMoveAtTown);
        Enum.TryParse(textBoxFastMoveTeleport.Text, out CharConfig.KeySkillfastMoveOutsideTown);
        Enum.TryParse(textBoxDefenseSkill.Text, out CharConfig.KeySkillDefenseAura);
        Enum.TryParse(textBoxCastDefenseSkill.Text, out CharConfig.KeySkillCastDefense);
        Enum.TryParse(textBoxLifeSkill.Text, out CharConfig.KeySkillLifeAura);
        Enum.TryParse(textBoxBattleOrder.Text, out CharConfig.KeySkillBattleOrder);
        Enum.TryParse(textBoxBattleCommand.Text, out CharConfig.KeySkillBattleCommand);
        Enum.TryParse(textBoxBattleCry.Text, out CharConfig.KeySkillBattleCry);
        Enum.TryParse(comboBoxKeyOpenInventory.Text, out CharConfig.KeyOpenInventory);
        Enum.TryParse(comboBoxKeyForceMovement.Text, out CharConfig.KeyForceMovement);
        Enum.TryParse(comboBoxKeyWeaponSwap.Text, out CharConfig.KeySwapWeapon);

        Enum.TryParse(comboBoxPot1.Text, out CharConfig.KeyPotion1);
        Enum.TryParse(comboBoxPot2.Text, out CharConfig.KeyPotion2);
        Enum.TryParse(comboBoxPot3.Text, out CharConfig.KeyPotion3);
        Enum.TryParse(comboBoxPot4.Text, out CharConfig.KeyPotion4);

        CharConfig.PlayerAttackWithRightHand = checkBoxAttachRightHand.Checked;

        CharConfig.UsingMerc = checkBoxUseMerc.Checked;
        CharConfig.TownIfMercDead = checkBoxTownMercDead.Checked;
        CharConfig.MercTakeHPPotUnder = (int)numericUpDownMercTakeHPUnder.Value;

        CharConfig.PlayerCharName = textBoxCharName.Text;
        CharConfig.ChickenHP = (int)numericUpDownChickenHP.Value;
        CharConfig.TakeHPPotUnder = (int)numericUpDownTakeHP.Value;
        CharConfig.TakeManaPotUnder = (int)numericUpDownTakeMana.Value;
        CharConfig.TakeRVPotUnder = (int)numericUpDownTakeRV.Value;
        CharConfig.GambleAboveGoldAmount = (int)numericUpDownGambleAbove.Value;
        CharConfig.GambleUntilGoldAmount = (int)numericUpDownGambleUntil.Value;

        CharConfig.UseTeleport = checkBoxUseTeleport.Checked;
        CharConfig.UseBO = checkBoxUseBO.Checked;
        CharConfig.GrabForGold = checkBoxGrabGold.Checked;
        CharConfig.IDAtShop = checkBoxIDAtShop.Checked;
        CharConfig.LeaveDiabloClone = checkBoxDClone.Checked;
        CharConfig.GambleGold = checkBoxGamble.Checked;

        CharConfig.BeltPotTypeToHave[0] = comboBoxBelt1.SelectedIndex;
        CharConfig.BeltPotTypeToHave[1] = comboBoxBelt2.SelectedIndex;
        CharConfig.BeltPotTypeToHave[2] = comboBoxBelt3.SelectedIndex;
        CharConfig.BeltPotTypeToHave[3] = comboBoxBelt4.SelectedIndex;

        CharConfig.KeysLocationInInventory.Item1 = (int)numericUpDownKeyXPos.Value;
        CharConfig.KeysLocationInInventory.Item2 = (int)numericUpDownKeyYPos.Value;

        for (int i = 0; i < 40; i++)
        {
            if (!AvailableSlotList[i].Checked) CharConfig.InventoryDontCheckItem[i] = 0;
            if (AvailableSlotList[i].Checked) CharConfig.InventoryDontCheckItem[i] = 1;
        }

        if (comboBoxAvoidImmune.SelectedIndex == 0)
        {
            CharConfig.AvoidColdImmune = false;
            CharConfig.AvoidFireImmune = false;
            CharConfig.AvoidLightImmune = false;
            CharConfig.AvoidPoisonImmune = false;
            CharConfig.AvoidMagicImmune = false;
        }
        else if (comboBoxAvoidImmune.SelectedIndex == 1)
        {
            CharConfig.AvoidColdImmune = true;
            CharConfig.AvoidFireImmune = false;
            CharConfig.AvoidLightImmune = false;
            CharConfig.AvoidPoisonImmune = false;
            CharConfig.AvoidMagicImmune = false;
        }
        else if (comboBoxAvoidImmune.SelectedIndex == 2)
        {
            CharConfig.AvoidColdImmune = false;
            CharConfig.AvoidFireImmune = true;
            CharConfig.AvoidLightImmune = false;
            CharConfig.AvoidPoisonImmune = false;
            CharConfig.AvoidMagicImmune = false;
        }
        else if (comboBoxAvoidImmune.SelectedIndex == 3)
        {
            CharConfig.AvoidColdImmune = false;
            CharConfig.AvoidFireImmune = false;
            CharConfig.AvoidLightImmune = true;
            CharConfig.AvoidPoisonImmune = false;
            CharConfig.AvoidMagicImmune = false;
        }
        else if (comboBoxAvoidImmune.SelectedIndex == 4)
        {
            CharConfig.AvoidColdImmune = false;
            CharConfig.AvoidFireImmune = false;
            CharConfig.AvoidLightImmune = false;
            CharConfig.AvoidPoisonImmune = true;
            CharConfig.AvoidMagicImmune = false;
        }
        else if (comboBoxAvoidImmune.SelectedIndex == 5)
        {
            CharConfig.AvoidColdImmune = false;
            CharConfig.AvoidFireImmune = false;
            CharConfig.AvoidLightImmune = false;
            CharConfig.AvoidPoisonImmune = false;
            CharConfig.AvoidMagicImmune = true;
        }

        CharConfig.ClearAfterBoss = checkBoxClearAfterBoss.Checked;
        CharConfig.UseKeys = checkBoxUseKeys.Checked;

        CharConfig.GambleItems.Clear();
        for (int i = 0; i < listViewGambleItems.Items.Count; i++) CharConfig.GambleItems.Add(listViewGambleItems.Items[i].Text);

        CharConfig.KillOnlySuperUnique = checkBoxKillOnlySuperUnique.Checked;
    }

    public void SetUsingKeys()
    {
        if (checkBoxUseKeys.Checked)
        {
            numericUpDownKeyXPos.Enabled = true;
            numericUpDownKeyYPos.Enabled = true;
            linkLabel1.Enabled = true;
        }
        else
        {
            numericUpDownKeyXPos.Enabled = false;
            numericUpDownKeyYPos.Enabled = false;
            linkLabel1.Enabled = false;
        }
    }

    private void FormCharSettings_FormClosing(object sender, FormClosingEventArgs e)
    {
        SaveSettings();
        Form1_0.SettingsLoader_0.SaveCharSettings();
    }

    private void comboBoxType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (CanReloadSettings)
        {
            if (comboBoxType.SelectedIndex == 0) CharConfig.RunningOnChar = "Paladin";
            if (comboBoxType.SelectedIndex == 1) CharConfig.RunningOnChar = "Sorceress";
            if (comboBoxType.SelectedIndex == 2) CharConfig.RunningOnChar = "Amazon";
            if (comboBoxType.SelectedIndex == 3) CharConfig.RunningOnChar = "Assassin";
            if (comboBoxType.SelectedIndex == 4) CharConfig.RunningOnChar = "Druid";
            if (comboBoxType.SelectedIndex == 5) CharConfig.RunningOnChar = "Barbarian";
            if (comboBoxType.SelectedIndex == 6) CharConfig.RunningOnChar = "Necromancer";

            Form1_0.SettingsLoader_0.ReloadCharSettings();
            LoadSettings();
        }
    }

    private void button1_Click(object sender, EventArgs e)
    {
        SaveSettings();
        Form1_0.SettingsLoader_0.SaveCharSettings();
    }

    private void checkBoxGamble_CheckedChanged(object sender, EventArgs e)
    {
        if (!checkBoxGamble.Checked)
        {
            numericUpDownGambleAbove.Enabled = false;
            numericUpDownGambleUntil.Enabled = false;
            buttonGambleSettings.Enabled = false;
        }
        else
        {
            numericUpDownGambleAbove.Enabled = true;
            numericUpDownGambleUntil.Enabled = true;
            buttonGambleSettings.Enabled = true;
        }
    }

    private void buttonReload_Click(object sender, EventArgs e)
    {
        DialogResult result = openFileDialog1.ShowDialog();
        if (result == DialogResult.OK)
        {
            Form1_0.SettingsLoader_0.ReloadCharSettingsFromThisFile(openFileDialog1.FileName);
            LoadSettings();
            Application.DoEvents();
        }
    }

    private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        panelHelpKeys.Visible = true;
    }

    private void button2_Click(object sender, EventArgs e)
    {
        panelHelpKeys.Visible = false;
    }

    private void checkBoxUseKeys_CheckedChanged(object sender, EventArgs e)
    {
        SetUsingKeys();
    }

    private void buttonGambleAddItem_Click(object sender, EventArgs e)
    {
        listViewGambleItems.Items.Add(comboBoxItemsNames.Text);
    }

    private void buttonGambleClearItems_Click(object sender, EventArgs e)
    {
        listViewGambleItems.Items.Clear();
    }

    private void button4_Click(object sender, EventArgs e)
    {
        panelGamble.Visible = false;
    }

    private void button3_Click(object sender, EventArgs e)
    {
        panelGamble.Visible = true;
    }

    private void buttonSaveAsChar_Click(object sender, EventArgs e)
    {
        SaveSettings();
        Form1_0.SettingsLoader_0.SaveCharSettings();

        string Fname = "";
        if (CharConfig.RunningOnChar == "Paladin") Fname = "Paladin_" + CharConfig.PlayerCharName + ".txt";
        if (CharConfig.RunningOnChar == "Sorceress") Fname = "Sorceress_" + CharConfig.PlayerCharName + ".txt";
        if(CharConfig.RunningOnChar == "Amazon") Fname = "Amazon_" + CharConfig.PlayerCharName + ".txt";
        if(CharConfig.RunningOnChar == "Assassin") Fname = "Assassin_" + CharConfig.PlayerCharName + ".txt";
        if(CharConfig.RunningOnChar == "Druid") Fname = "Druid_" + CharConfig.PlayerCharName + ".txt";
        if(CharConfig.RunningOnChar == "Barbarian") Fname = "Barbarian_" + CharConfig.PlayerCharName + ".txt";
        if(CharConfig.RunningOnChar == "Necromancer") Fname = "Necromancer_" + CharConfig.PlayerCharName + ".txt";
        saveFileDialog1.FileName = Fname;

        DialogResult result = saveFileDialog1.ShowDialog();
        if (result == DialogResult.OK)
        {
            File.Create(saveFileDialog1.FileName).Dispose();
            if (CharConfig.RunningOnChar == "Paladin") File.WriteAllBytes(saveFileDialog1.FileName, File.ReadAllBytes(Form1_0.SettingsLoader_0.File_Paladin));
            else if (CharConfig.RunningOnChar == "Sorceress") File.WriteAllBytes(saveFileDialog1.FileName, File.ReadAllBytes(Form1_0.SettingsLoader_0.File_Sorceress));
            else if (CharConfig.RunningOnChar == "Amazon") File.WriteAllBytes(saveFileDialog1.FileName, File.ReadAllBytes(Form1_0.SettingsLoader_0.File_Amazon));
            else if (CharConfig.RunningOnChar == "Assassin") File.WriteAllBytes(saveFileDialog1.FileName, File.ReadAllBytes(Form1_0.SettingsLoader_0.File_Assassin));
            else if (CharConfig.RunningOnChar == "Druid") File.WriteAllBytes(saveFileDialog1.FileName, File.ReadAllBytes(Form1_0.SettingsLoader_0.File_Druid));
            else if (CharConfig.RunningOnChar == "Barbarian") File.WriteAllBytes(saveFileDialog1.FileName, File.ReadAllBytes(Form1_0.SettingsLoader_0.File_Barbarian));
            else if (CharConfig.RunningOnChar == "Necromancer") File.WriteAllBytes(saveFileDialog1.FileName, File.ReadAllBytes(Form1_0.SettingsLoader_0.File_Necromancer));
        }
    }

    private void groupBoxInventory_Enter(object sender, EventArgs e)
    {

    }

    private void groupBox3_Enter(object sender, EventArgs e)
    {

    }

    private void textBoxLeftSkill_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
