using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace app
{
    public partial class FormCharSettings : Form
    {
        public List<CheckBox> AvailableSlotList = new List<CheckBox>();
        public Form1 Form1_0;
        public bool CanReloadSettings = true;

        public FormCharSettings(Form1 form1_1)
        {
            Form1_0 = form1_1;
            InitializeComponent();

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
                    ThisXPos = groupBoxInventory.Location.X + 5 + (i * 18);
                    ThixYPos = 20;
                }
                if (i >= 10 && i < 20)
                {
                    ThisXPos = groupBoxInventory.Location.X + 5 + ((i - 10) * 18);
                    ThixYPos = 20 + (18 * 1);
                }
                if (i >= 20 && i < 30)
                {
                    ThisXPos = groupBoxInventory.Location.X + 5 + ((i - 20) * 18);
                    ThixYPos = 20 + (18 * 2);
                }
                if (i >= 30 && i < 40)
                {
                    ThisXPos = groupBoxInventory.Location.X + 5 + ((i - 30) * 18);
                    ThixYPos = 20 + (18 * 3);
                }

                AvailableSlotList[i].Location = new Point(ThisXPos, ThixYPos);
            }

            LoadSettings();
        }

        public void LoadSettings()
        {
            CanReloadSettings = false;

            if (CharConfig.RunningOnChar == "PaladinHammer") comboBoxType.SelectedIndex = 0;
            if (CharConfig.RunningOnChar == "SorceressBlizzard") comboBoxType.SelectedIndex = 1;

            textBoxLeftSkill.Text = CharConfig.KeySkillAttack.ToString();
            textBoxRightSkill.Text = CharConfig.KeySkillAura.ToString();
            textBoxFastMoveTown.Text = CharConfig.KeySkillfastMoveAtTown.ToString();
            textBoxFastMoveTeleport.Text = CharConfig.KeySkillfastMoveOutsideTown.ToString();
            textBoxDefenseSkill.Text = CharConfig.KeySkillDefenseAura.ToString();
            textBoxCastDefenseSkill.Text = CharConfig.KeySkillCastDefense.ToString();
            textBoxLifeSkill.Text = CharConfig.KeySkillLifeAura.ToString();

            checkBoxAttachRightHand.Checked = CharConfig.PlayerAttackWithRightHand;

            checkBoxUseMerc.Checked = CharConfig.UsingMerc;
            numericUpDownMercTakeHPUnder.Value = CharConfig.MercTakeHPPotUnder;

            textBoxCharName.Text = CharConfig.PlayerCharName;
            numericUpDownChickenHP.Value = CharConfig.ChickenHP;
            numericUpDownTakeHP.Value = CharConfig.TakeHPPotUnder;
            numericUpDownTakeMana.Value = CharConfig.TakeManaPotUnder;
            numericUpDownTakeRV.Value = CharConfig.TakeRVPotUnder;
            numericUpDownGambleAbove.Value = CharConfig.GambleAboveGoldAmount;
            numericUpDownGambleUntil.Value = CharConfig.GambleUntilGoldAmount;

            checkBoxUseTeleport.Checked = CharConfig.UseTeleport;

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

            CanReloadSettings = true;
        }

        public void SaveSettings()
        {
            if (comboBoxType.SelectedIndex == 0) CharConfig.RunningOnChar = "PaladinHammer";
            if (comboBoxType.SelectedIndex == 1) CharConfig.RunningOnChar = "SorceressBlizzard";

            Enum.TryParse(textBoxLeftSkill.Text, out CharConfig.KeySkillAttack);
            Enum.TryParse(textBoxRightSkill.Text, out CharConfig.KeySkillAura);
            Enum.TryParse(textBoxFastMoveTown.Text, out CharConfig.KeySkillfastMoveAtTown);
            Enum.TryParse(textBoxFastMoveTeleport.Text, out CharConfig.KeySkillfastMoveOutsideTown);
            Enum.TryParse(textBoxDefenseSkill.Text, out CharConfig.KeySkillDefenseAura);
            Enum.TryParse(textBoxCastDefenseSkill.Text, out CharConfig.KeySkillCastDefense);
            Enum.TryParse(textBoxLifeSkill.Text, out CharConfig.KeySkillLifeAura);

            CharConfig.PlayerAttackWithRightHand = checkBoxAttachRightHand.Checked;

            CharConfig.UsingMerc = checkBoxUseMerc.Checked;
            CharConfig.MercTakeHPPotUnder = (int) numericUpDownMercTakeHPUnder.Value;

            CharConfig.PlayerCharName = textBoxCharName.Text;
            CharConfig.ChickenHP = (int)numericUpDownChickenHP.Value;
            CharConfig.TakeHPPotUnder = (int)numericUpDownTakeHP.Value;
            CharConfig.TakeManaPotUnder = (int)numericUpDownTakeMana.Value;
            CharConfig.TakeRVPotUnder = (int)numericUpDownTakeRV.Value;
            CharConfig.GambleAboveGoldAmount = (int)numericUpDownGambleAbove.Value;
            CharConfig.GambleUntilGoldAmount = (int)numericUpDownGambleUntil.Value;

            CharConfig.UseTeleport = checkBoxUseTeleport.Checked;

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
                if (comboBoxType.SelectedIndex == 0) CharConfig.RunningOnChar = "PaladinHammer";
                if (comboBoxType.SelectedIndex == 1) CharConfig.RunningOnChar = "SorceressBlizzard";

                Form1_0.SettingsLoader_0.ReloadCharSettings();
                LoadSettings();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveSettings();
            Form1_0.SettingsLoader_0.SaveCharSettings();
        }
    }
}
