using System;
using System.Drawing;
using System.Windows.Forms;

namespace app
{
    public partial class FormSettings : Form
    {
        Form1 Form1_0;

        public FormSettings(Form1 form1_1)
        {
            Form1_0 = form1_1;

            InitializeComponent();

            listViewRush.Location = new Point(listViewRunScripts.Location.X, listViewRunScripts.Location.Y);
            listViewRush.Visible = false;

            LoadSettings();
        }

        public void LoadSettings()
        {
            textBoxD2Path.Text = Form1_0.D2_LOD_113C_Path;
            numericUpDownMaxTime.Value = CharConfig.MaxGameTime;
            checkBoxRush.Checked = CharConfig.IsRushing;
            textBox1LeechName.Text = CharConfig.RushLeecherName;

            listViewRunScripts.Items[0].Checked = CharConfig.RunItemGrabScriptOnly;
            listViewRunScripts.Items[1].Checked = CharConfig.RunCountessScript;
            listViewRunScripts.Items[2].Checked = CharConfig.RunAndarielScript;
            listViewRunScripts.Items[3].Checked = CharConfig.RunSummonerScript;
            listViewRunScripts.Items[4].Checked = CharConfig.RunDurielScript;
            listViewRunScripts.Items[5].Checked = CharConfig.RunLowerKurastScript;
            listViewRunScripts.Items[6].Checked = CharConfig.RunTravincalScript;
            listViewRunScripts.Items[7].Checked = CharConfig.RunMephistoScript;
            listViewRunScripts.Items[8].Checked = CharConfig.RunChaosScript;
            listViewRunScripts.Items[9].Checked = CharConfig.RunPindleskinScript;
            listViewRunScripts.Items[10].Checked = CharConfig.RunBaalLeechScript;

            listViewRush.Items[0].Checked = CharConfig.RunDarkWoodRush;
            listViewRush.Items[1].Checked = CharConfig.RunTristramRush;
            listViewRush.Items[2].Checked = CharConfig.RunAndarielRush;
            listViewRush.Items[3].Checked = CharConfig.RunRadamentRush;
            listViewRush.Items[4].Checked = CharConfig.RunHallOfDeadRush;
            listViewRush.Items[5].Checked = CharConfig.RunFarOasisRush;
            listViewRush.Items[6].Checked = CharConfig.RunLostCityRush;
            listViewRush.Items[7].Checked = CharConfig.RunSummonerRush;
            listViewRush.Items[8].Checked = CharConfig.RunDurielRush;
            listViewRush.Items[9].Checked = CharConfig.RunKahlimEyeRush;
            listViewRush.Items[10].Checked = CharConfig.RunKahlimBrainRush;
            listViewRush.Items[11].Checked = CharConfig.RunKahlimHeartRush;
            listViewRush.Items[12].Checked = CharConfig.RunTravincalRush;
            listViewRush.Items[13].Checked = CharConfig.RunMephistoRush;

            if (CharConfig.RunGameMakerScript) comboBoxLobby.SelectedIndex = 0;
            if (CharConfig.RunChaosSearchGameScript) comboBoxLobby.SelectedIndex = 1;
            if (CharConfig.RunBaalSearchGameScript) comboBoxLobby.SelectedIndex = 2;

            textBoxGameName.Text = CharConfig.GameName;
            textBoxGamePass.Text = CharConfig.GamePass;

            comboBoxDifficulty.SelectedIndex = CharConfig.GameDifficulty;

            numericUpDownRunNumber.Value = Form1_0.CurrentGameNumber;

            SetCreateGameGroupbox();
            SetRushMenu();
        }

        public void SetRushMenu()
        {
            if (checkBoxRush.Checked)
            {
                comboBoxLobby.Enabled = false;
                listViewRunScripts.Visible = false;

                label1Run.Text = "Select Rush Scripts";

                textBox1LeechName.Visible = true;
                label1LeechName.Visible = true;

                label7MaxTime.Visible = false;
                numericUpDownMaxTime.Visible = false;

                listViewRush.Visible = true;
            }
            else
            {
                comboBoxLobby.Enabled = true;
                listViewRunScripts.Visible = true;

                label1Run.Text = "Select Run Scripts";

                textBox1LeechName.Visible = false;
                label1LeechName.Visible = false;

                label7MaxTime.Visible = true;
                numericUpDownMaxTime.Visible = true;

                listViewRush.Visible = false;
            }
        }

        public void SetCreateGameGroupbox()
        {
            if (!checkBoxRush.Checked) groupBox1.Enabled = (comboBoxLobby.SelectedIndex == 0);
            else groupBox1.Enabled = false;
        }

        public void SaveSettings()
        {
            Form1_0.D2_LOD_113C_Path = textBoxD2Path.Text;
            CharConfig.MaxGameTime = (int) numericUpDownMaxTime.Value;
            CharConfig.IsRushing = checkBoxRush.Checked;
            CharConfig.RushLeecherName = textBox1LeechName.Text;

            CharConfig.RunItemGrabScriptOnly = listViewRunScripts.Items[0].Checked;
            CharConfig.RunCountessScript = listViewRunScripts.Items[1].Checked;
            CharConfig.RunAndarielScript = listViewRunScripts.Items[2].Checked;
            CharConfig.RunSummonerScript = listViewRunScripts.Items[3].Checked;
            CharConfig.RunDurielScript = listViewRunScripts.Items[4].Checked;
            CharConfig.RunLowerKurastScript = listViewRunScripts.Items[5].Checked;
            CharConfig.RunTravincalScript = listViewRunScripts.Items[6].Checked;
            CharConfig.RunMephistoScript = listViewRunScripts.Items[7].Checked;
            CharConfig.RunChaosScript = listViewRunScripts.Items[8].Checked;
            CharConfig.RunPindleskinScript = listViewRunScripts.Items[9].Checked;
            CharConfig.RunBaalLeechScript = listViewRunScripts.Items[10].Checked;

            CharConfig.RunDarkWoodRush = listViewRush.Items[0].Checked;
            CharConfig.RunTristramRush = listViewRush.Items[1].Checked;
            CharConfig.RunAndarielRush = listViewRush.Items[2].Checked;
            CharConfig.RunRadamentRush = listViewRush.Items[3].Checked;
            CharConfig.RunHallOfDeadRush = listViewRush.Items[4].Checked;
            CharConfig.RunFarOasisRush = listViewRush.Items[5].Checked;
            CharConfig.RunLostCityRush = listViewRush.Items[6].Checked;
            CharConfig.RunSummonerRush = listViewRush.Items[7].Checked;
            CharConfig.RunDurielRush = listViewRush.Items[8].Checked;
            CharConfig.RunKahlimEyeRush = listViewRush.Items[9].Checked;
            CharConfig.RunKahlimBrainRush = listViewRush.Items[10].Checked;
            CharConfig.RunKahlimHeartRush = listViewRush.Items[11].Checked;
            CharConfig.RunTravincalRush = listViewRush.Items[12].Checked;
            CharConfig.RunMephistoRush = listViewRush.Items[13].Checked;

            CharConfig.RunGameMakerScript = (comboBoxLobby.SelectedIndex == 0);
            CharConfig.RunChaosSearchGameScript = (comboBoxLobby.SelectedIndex == 1);
            CharConfig.RunBaalSearchGameScript = (comboBoxLobby.SelectedIndex == 2);

            CharConfig.GameName = textBoxGameName.Text;
            CharConfig.GamePass = textBoxGamePass.Text;

            CharConfig.GameDifficulty = comboBoxDifficulty.SelectedIndex;

            Form1_0.CurrentGameNumber = (int) numericUpDownRunNumber.Value;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            numericUpDownRunNumber.Value = 1;
        }

        private void FormSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveSettings();
            Form1_0.SettingsLoader_0.SaveCurrentSettings();
        }

        private void comboBoxLobby_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetCreateGameGroupbox();
        }

        private void listViewRunScripts_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void FormSettings_Load(object sender, EventArgs e)
        {

        }

        private void checkBoxRush_CheckedChanged(object sender, EventArgs e)
        {
            SetRushMenu();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormCharSettings FormCharSettings_0 = new FormCharSettings(Form1_0);
            FormCharSettings_0.ShowDialog();
        }
    }
}
