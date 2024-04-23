using System;
using System.Drawing;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace app
{
    public partial class FormSettings : Form
    {
        Form1 Form1_0;

        public FormSettings(Form1 form1_1)
        {
            Form1_0 = form1_1;

            InitializeComponent();

            listViewRush.Location = new System.Drawing.Point(listViewRunScripts.Location.X, listViewRunScripts.Location.Y);
            listViewRush.Visible = false;

            LoadSettings();
        }

        public void LoadSettings()
        {
            checkBoxShowOverlay.Checked = CharConfig.ShowOverlay;

            textBoxD2Path.Text = Form1_0.D2_LOD_113C_Path;
            numericUpDownMaxTime.Value = CharConfig.MaxGameTime;
            checkBoxRush.Checked = CharConfig.IsRushing;
            textBox1LeechName.Text = CharConfig.RushLeecherName;

            textBoxStartKey.Text = CharConfig.StartStopKey.ToString();

            int CurrI = 0;
            listViewRunScripts.Items[CurrI++].Checked = CharConfig.RunMapHackOnly;
            listViewRunScripts.Items[CurrI++].Checked = CharConfig.RunItemGrabScriptOnly;
            listViewRunScripts.Items[CurrI++].Checked = CharConfig.RunCowsScript;
            listViewRunScripts.Items[CurrI++].Checked = CharConfig.RunCountessScript;
            listViewRunScripts.Items[CurrI++].Checked = CharConfig.RunAndarielScript;
            listViewRunScripts.Items[CurrI++].Checked = CharConfig.RunSummonerScript;
            listViewRunScripts.Items[CurrI++].Checked = CharConfig.RunDurielScript;
            listViewRunScripts.Items[CurrI++].Checked = CharConfig.RunLowerKurastScript;
            listViewRunScripts.Items[CurrI++].Checked = CharConfig.RunTravincalScript;
            listViewRunScripts.Items[CurrI++].Checked = CharConfig.RunMephistoScript;
            listViewRunScripts.Items[CurrI++].Checked = CharConfig.RunChaosScript;
            listViewRunScripts.Items[CurrI++].Checked = CharConfig.RunChaosLeechScript;
            listViewRunScripts.Items[CurrI++].Checked = CharConfig.RunEldritchScript;
            listViewRunScripts.Items[CurrI++].Checked = CharConfig.RunShenkScript;
            listViewRunScripts.Items[CurrI++].Checked = CharConfig.RunPindleskinScript;
            listViewRunScripts.Items[CurrI++].Checked = CharConfig.RunNihlatakScript;
            listViewRunScripts.Items[CurrI++].Checked = CharConfig.RunBaalScript;
            listViewRunScripts.Items[CurrI++].Checked = CharConfig.RunBaalLeechScript;
            listViewRunScripts.Items[CurrI++].Checked = CharConfig.RunWPTaker;

            CurrI = 0;
            listViewRush.Items[CurrI++].Checked = CharConfig.RunDarkWoodRush;
            listViewRush.Items[CurrI++].Checked = CharConfig.RunTristramRush;
            listViewRush.Items[CurrI++].Checked = CharConfig.RunAndarielRush;
            listViewRush.Items[CurrI++].Checked = CharConfig.RunRadamentRush;
            listViewRush.Items[CurrI++].Checked = CharConfig.RunHallOfDeadRush;
            listViewRush.Items[CurrI++].Checked = CharConfig.RunFarOasisRush;
            listViewRush.Items[CurrI++].Checked = CharConfig.RunLostCityRush;
            listViewRush.Items[CurrI++].Checked = CharConfig.RunSummonerRush;
            listViewRush.Items[CurrI++].Checked = CharConfig.RunDurielRush;
            listViewRush.Items[CurrI++].Checked = CharConfig.RunKahlimEyeRush;
            listViewRush.Items[CurrI++].Checked = CharConfig.RunKahlimBrainRush;
            listViewRush.Items[CurrI++].Checked = CharConfig.RunKahlimHeartRush;
            listViewRush.Items[CurrI++].Checked = CharConfig.RunTravincalRush;
            listViewRush.Items[CurrI++].Checked = CharConfig.RunMephistoRush;
            listViewRush.Items[CurrI++].Checked = CharConfig.RunChaosRush;
            listViewRush.Items[CurrI++].Checked = CharConfig.RunAncientsRush;
            listViewRush.Items[CurrI++].Checked = CharConfig.RunBaalRush;

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
                groupBox1.Enabled = false;
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
                groupBox1.Enabled = true;
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
            CharConfig.ShowOverlay = checkBoxShowOverlay.Checked;

            Form1_0.D2_LOD_113C_Path = textBoxD2Path.Text;
            CharConfig.MaxGameTime = (int) numericUpDownMaxTime.Value;
            CharConfig.IsRushing = checkBoxRush.Checked;
            CharConfig.RushLeecherName = textBox1LeechName.Text;
            Enum.TryParse(textBoxStartKey.Text, out CharConfig.StartStopKey);

            int CurrI = 0;
            CharConfig.RunMapHackOnly = listViewRunScripts.Items[CurrI++].Checked;
            CharConfig.RunItemGrabScriptOnly = listViewRunScripts.Items[CurrI++].Checked;
            CharConfig.RunCowsScript = listViewRunScripts.Items[CurrI++].Checked;
            CharConfig.RunCountessScript = listViewRunScripts.Items[CurrI++].Checked;
            CharConfig.RunAndarielScript = listViewRunScripts.Items[CurrI++].Checked;
            CharConfig.RunSummonerScript = listViewRunScripts.Items[CurrI++].Checked;
            CharConfig.RunDurielScript = listViewRunScripts.Items[CurrI++].Checked;
            CharConfig.RunLowerKurastScript = listViewRunScripts.Items[CurrI++].Checked;
            CharConfig.RunTravincalScript = listViewRunScripts.Items[CurrI++].Checked;
            CharConfig.RunMephistoScript = listViewRunScripts.Items[CurrI++].Checked;
            CharConfig.RunChaosScript = listViewRunScripts.Items[CurrI++].Checked;
            CharConfig.RunChaosLeechScript = listViewRunScripts.Items[CurrI++].Checked;
            CharConfig.RunEldritchScript = listViewRunScripts.Items[CurrI++].Checked;
            CharConfig.RunShenkScript = listViewRunScripts.Items[CurrI++].Checked;
            CharConfig.RunPindleskinScript = listViewRunScripts.Items[CurrI++].Checked;
            CharConfig.RunNihlatakScript = listViewRunScripts.Items[CurrI++].Checked;
            CharConfig.RunBaalScript = listViewRunScripts.Items[CurrI++].Checked;
            CharConfig.RunBaalLeechScript = listViewRunScripts.Items[CurrI++].Checked;
            CharConfig.RunWPTaker = listViewRunScripts.Items[CurrI++].Checked;

            CurrI = 0;
            CharConfig.RunDarkWoodRush = listViewRush.Items[CurrI++].Checked;
            CharConfig.RunTristramRush = listViewRush.Items[CurrI++].Checked;
            CharConfig.RunAndarielRush = listViewRush.Items[CurrI++].Checked;
            CharConfig.RunRadamentRush = listViewRush.Items[CurrI++].Checked;
            CharConfig.RunHallOfDeadRush = listViewRush.Items[CurrI++].Checked;
            CharConfig.RunFarOasisRush = listViewRush.Items[CurrI++].Checked;
            CharConfig.RunLostCityRush = listViewRush.Items[CurrI++].Checked;
            CharConfig.RunSummonerRush = listViewRush.Items[CurrI++].Checked;
            CharConfig.RunDurielRush = listViewRush.Items[CurrI++].Checked;
            CharConfig.RunKahlimEyeRush = listViewRush.Items[CurrI++].Checked;
            CharConfig.RunKahlimBrainRush = listViewRush.Items[CurrI++].Checked;
            CharConfig.RunKahlimHeartRush = listViewRush.Items[CurrI++].Checked;
            CharConfig.RunTravincalRush = listViewRush.Items[CurrI++].Checked;
            CharConfig.RunMephistoRush = listViewRush.Items[CurrI++].Checked;
            CharConfig.RunChaosRush = listViewRush.Items[CurrI++].Checked;
            CharConfig.RunAncientsRush = listViewRush.Items[CurrI++].Checked;
            CharConfig.RunBaalRush = listViewRush.Items[CurrI++].Checked;

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
            //FormCharSettings FormCharSettings_0 = new FormCharSettings(Form1_0);
            //FormCharSettings_0.ShowDialog();

            SaveSettings();
            Form1_0.SettingsLoader_0.SaveCurrentSettings();
        }
    }
}
