using System;
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

            LoadSettings();
        }

        public void LoadSettings()
        {
            textBoxD2Path.Text = Form1_0.D2_LOD_113C_Path;
            numericUpDownMaxTime.Value = CharConfig.MaxGameTime;

            listViewRunScripts.Items[0].Checked = CharConfig.RunItemGrabScriptOnly;
            listViewRunScripts.Items[1].Checked = CharConfig.RunCountessScript;
            listViewRunScripts.Items[2].Checked = CharConfig.RunAndarielScript;
            listViewRunScripts.Items[3].Checked = CharConfig.RunSummonerScript;
            listViewRunScripts.Items[4].Checked = CharConfig.RunDurielScript;
            listViewRunScripts.Items[5].Checked = CharConfig.RunLowerKurastScript;
            listViewRunScripts.Items[6].Checked = CharConfig.RunMephistoScript;
            listViewRunScripts.Items[7].Checked = CharConfig.RunChaosScript;
            listViewRunScripts.Items[8].Checked = CharConfig.RunBaalLeechScript;

            if (CharConfig.RunGameMakerScript) comboBoxLobby.SelectedIndex = 0;
            if (CharConfig.RunChaosSearchGameScript) comboBoxLobby.SelectedIndex = 1;
            if (CharConfig.RunBaalSearchGameScript) comboBoxLobby.SelectedIndex = 2;

            textBoxGameName.Text = CharConfig.GameName;
            textBoxGamePass.Text = CharConfig.GamePass;

            comboBoxDifficulty.SelectedIndex = CharConfig.GameDifficulty;

            numericUpDownRunNumber.Value = Form1_0.CurrentGameNumber;

            SetCreateGameGroupbox();
        }

        public void SetCreateGameGroupbox()
        {
            groupBox1.Enabled = (comboBoxLobby.SelectedIndex == 0);
        }

        public void SaveSettings()
        {
            Form1_0.D2_LOD_113C_Path = textBoxD2Path.Text;
            CharConfig.MaxGameTime = (int) numericUpDownMaxTime.Value;

            CharConfig.RunItemGrabScriptOnly = listViewRunScripts.Items[0].Checked;
            CharConfig.RunCountessScript = listViewRunScripts.Items[1].Checked;
            CharConfig.RunAndarielScript = listViewRunScripts.Items[2].Checked;
            CharConfig.RunSummonerScript = listViewRunScripts.Items[3].Checked;
            CharConfig.RunDurielScript = listViewRunScripts.Items[4].Checked;
            CharConfig.RunLowerKurastScript = listViewRunScripts.Items[5].Checked;
            CharConfig.RunMephistoScript = listViewRunScripts.Items[6].Checked;
            CharConfig.RunChaosScript = listViewRunScripts.Items[7].Checked;
            CharConfig.RunBaalLeechScript = listViewRunScripts.Items[8].Checked;

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
    }
}
