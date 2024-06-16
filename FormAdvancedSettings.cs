using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

public partial class FormAdvancedSettings : Form
{
    Form1 Form1_0;

    public FormAdvancedSettings(Form1 form1_1)
    {
        Form1_0 = form1_1;

        InitializeComponent();
        this.TopMost = true;

        LoadSettings();
    }

    public void LoadSettings()
    {
        int RowsIndex = 0;
        dataGridViewAdvanced.Rows.Clear();
        dataGridViewAdvanced.Rows.Add();
        dataGridViewAdvanced.Rows[dataGridViewAdvanced.Rows.Count - 1].Cells[0].Value = CharConfig.MaxDelayNewGame;
        dataGridViewAdvanced.Rows[RowsIndex++].HeaderCell.Value = "New Game Delay (ms*10)";
        dataGridViewAdvanced.Rows.Add();
        dataGridViewAdvanced.Rows[dataGridViewAdvanced.Rows.Count - 1].Cells[0].Value = CharConfig.WaypointEnterDelay;
        dataGridViewAdvanced.Rows[RowsIndex++].HeaderCell.Value = "Waypoint Enter Delay (ms*10)";
        dataGridViewAdvanced.Rows.Add();
        dataGridViewAdvanced.Rows[dataGridViewAdvanced.Rows.Count - 1].Cells[0].Value = CharConfig.MaxMercReliveTries;
        dataGridViewAdvanced.Rows[RowsIndex++].HeaderCell.Value = "Max Merc Relive Tries";
        dataGridViewAdvanced.Rows.Add();
        dataGridViewAdvanced.Rows[dataGridViewAdvanced.Rows.Count - 1].Cells[0].Value = CharConfig.MaxItemIDTries;
        dataGridViewAdvanced.Rows[RowsIndex++].HeaderCell.Value = "Max Item ID Tries";
        dataGridViewAdvanced.Rows.Add();
        dataGridViewAdvanced.Rows[dataGridViewAdvanced.Rows.Count - 1].Cells[0].Value = CharConfig.MaxItemGrabTries;
        dataGridViewAdvanced.Rows[RowsIndex++].HeaderCell.Value = "Max Item Grab Tries";
        dataGridViewAdvanced.Rows.Add();
        dataGridViewAdvanced.Rows[dataGridViewAdvanced.Rows.Count - 1].Cells[0].Value = CharConfig.MaxItemStashTries;
        dataGridViewAdvanced.Rows[RowsIndex++].HeaderCell.Value = "Max Item Stash Tries";
        dataGridViewAdvanced.Rows.Add();
        dataGridViewAdvanced.Rows[dataGridViewAdvanced.Rows.Count - 1].Cells[0].Value = CharConfig.StashFullTries;
        dataGridViewAdvanced.Rows[RowsIndex++].HeaderCell.Value = "Stash Full Tries";
        dataGridViewAdvanced.Rows.Add();
        dataGridViewAdvanced.Rows[dataGridViewAdvanced.Rows.Count - 1].Cells[0].Value = CharConfig.MaxShopTries;
        dataGridViewAdvanced.Rows[RowsIndex++].HeaderCell.Value = "Max Shop Tries";
        dataGridViewAdvanced.Rows.Add();
        dataGridViewAdvanced.Rows[dataGridViewAdvanced.Rows.Count - 1].Cells[0].Value = CharConfig.MaxRepairTries;
        dataGridViewAdvanced.Rows[RowsIndex++].HeaderCell.Value = "Max Repair Tries";
        dataGridViewAdvanced.Rows.Add();
        dataGridViewAdvanced.Rows[dataGridViewAdvanced.Rows.Count - 1].Cells[0].Value = CharConfig.MaxGambleTries;
        dataGridViewAdvanced.Rows[RowsIndex++].HeaderCell.Value = "Max Gamble Tries";
        dataGridViewAdvanced.Rows.Add();
        dataGridViewAdvanced.Rows[dataGridViewAdvanced.Rows.Count - 1].Cells[0].Value = CharConfig.MaxBattleAttackTries;
        dataGridViewAdvanced.Rows[RowsIndex++].HeaderCell.Value = "Max Battle Attack Tries";
        dataGridViewAdvanced.Rows.Add();
        dataGridViewAdvanced.Rows[dataGridViewAdvanced.Rows.Count - 1].Cells[0].Value = CharConfig.TakeHPPotionDelay;
        dataGridViewAdvanced.Rows[RowsIndex++].HeaderCell.Value = "Take HP Potion Delay (ms)";
        dataGridViewAdvanced.Rows.Add();
        dataGridViewAdvanced.Rows[dataGridViewAdvanced.Rows.Count - 1].Cells[0].Value = CharConfig.TakeManaPotionDelay;
        dataGridViewAdvanced.Rows[RowsIndex++].HeaderCell.Value = "Take Mana Potion Delay (ms)";
        dataGridViewAdvanced.Rows.Add();
        dataGridViewAdvanced.Rows[dataGridViewAdvanced.Rows.Count - 1].Cells[0].Value = CharConfig.OverallDelaysMultiplyer;
        dataGridViewAdvanced.Rows[RowsIndex++].HeaderCell.Value = "Overall Delays Multiplyer";
        dataGridViewAdvanced.Rows.Add();
        dataGridViewAdvanced.Rows[dataGridViewAdvanced.Rows.Count - 1].Cells[0].Value = CharConfig.EndBattleGrabDelay;
        dataGridViewAdvanced.Rows[RowsIndex++].HeaderCell.Value = "End Battle Item Grab Delay (ms*100)";
        dataGridViewAdvanced.Rows.Add();
        dataGridViewAdvanced.Rows[dataGridViewAdvanced.Rows.Count - 1].Cells[0].Value = CharConfig.MaxTimeEnterGame;
        dataGridViewAdvanced.Rows[RowsIndex++].HeaderCell.Value = "Max Search Game Time To Enter (sec)";
        dataGridViewAdvanced.Rows.Add();
        dataGridViewAdvanced.Rows[dataGridViewAdvanced.Rows.Count - 1].Cells[0].Value = CharConfig.BaalWavesCastDelay;
        dataGridViewAdvanced.Rows[RowsIndex++].HeaderCell.Value = "Baal Waves Cast Delay (sec)";
        dataGridViewAdvanced.Rows.Add();
        dataGridViewAdvanced.Rows[dataGridViewAdvanced.Rows.Count - 1].Cells[0].Value = CharConfig.ChaosWaitingSealBossDelay;
        dataGridViewAdvanced.Rows[RowsIndex++].HeaderCell.Value = "Chaos Waiting Seal Boss Delay (sec)";
        dataGridViewAdvanced.Rows.Add();
        dataGridViewAdvanced.Rows[dataGridViewAdvanced.Rows.Count - 1].Cells[0].Value = CharConfig.RecastBODelay;
        dataGridViewAdvanced.Rows[RowsIndex++].HeaderCell.Value = "Recast BO after Delay (sec)";
        dataGridViewAdvanced.Rows.Add();
        dataGridViewAdvanced.Rows[dataGridViewAdvanced.Rows.Count - 1].Cells[0].Value = CharConfig.TownSwitchAreaDelay;
        dataGridViewAdvanced.Rows[RowsIndex++].HeaderCell.Value = "Town Switch Area Delay (sec)";
        dataGridViewAdvanced.Rows.Add();
        dataGridViewAdvanced.Rows[dataGridViewAdvanced.Rows.Count - 1].Cells[0].Value = CharConfig.PublicGameTPRespawnDelay;
        dataGridViewAdvanced.Rows[RowsIndex++].HeaderCell.Value = "Public Game TP Respawn Delay (ms*10)";
        dataGridViewAdvanced.Rows.Add();
        dataGridViewAdvanced.Rows[dataGridViewAdvanced.Rows.Count - 1].Cells[0].Value = CharConfig.TPRespawnDelay;
        dataGridViewAdvanced.Rows[RowsIndex++].HeaderCell.Value = "TP Respawn Delay (ms*10)";
        dataGridViewAdvanced.Rows.Add();
        dataGridViewAdvanced.Rows[dataGridViewAdvanced.Rows.Count - 1].Cells[0].Value = CharConfig.PlayerMaxHPCheckDelay;
        dataGridViewAdvanced.Rows[RowsIndex++].HeaderCell.Value = "Player MaxHP Check Delay (ms)";
        dataGridViewAdvanced.Rows.Add();
        dataGridViewAdvanced.Rows[dataGridViewAdvanced.Rows.Count - 1].Cells[0].Value = CharConfig.LeechEnterTPDelay;
        dataGridViewAdvanced.Rows[RowsIndex++].HeaderCell.Value = "Leech Games Enter TP Delay (ms*10)";
        dataGridViewAdvanced.Rows.Add();
        dataGridViewAdvanced.Rows[dataGridViewAdvanced.Rows.Count - 1].Cells[0].Value = CharConfig.MephistoRedPortalEnterDelay;
        dataGridViewAdvanced.Rows[RowsIndex++].HeaderCell.Value = "Mephisto Ending Red Portal Delay (ms*10)";
        dataGridViewAdvanced.Rows.Add();
        dataGridViewAdvanced.Rows[dataGridViewAdvanced.Rows.Count - 1].Cells[0].Value = CharConfig.CubeItemPlaceDelay;
        dataGridViewAdvanced.Rows[RowsIndex++].HeaderCell.Value = "Cube Item Placing Delay (ms*10)";
        dataGridViewAdvanced.Rows.Add();
        dataGridViewAdvanced.Rows[dataGridViewAdvanced.Rows.Count - 1].Cells[0].Value = CharConfig.CubeItemPlaceDelay;
        dataGridViewAdvanced.Rows[RowsIndex++].HeaderCell.Value = "Create New Game Wait Delay (ms*10)";
    }

    public void SaveSettings()
    {
        int RowsIndex = 0;
        CharConfig.MaxDelayNewGame = int.Parse(dataGridViewAdvanced.Rows[RowsIndex++].Cells[0].Value.ToString());
        CharConfig.WaypointEnterDelay = int.Parse(dataGridViewAdvanced.Rows[RowsIndex++].Cells[0].Value.ToString());
        CharConfig.MaxMercReliveTries = int.Parse(dataGridViewAdvanced.Rows[RowsIndex++].Cells[0].Value.ToString());
        CharConfig.MaxItemIDTries = int.Parse(dataGridViewAdvanced.Rows[RowsIndex++].Cells[0].Value.ToString());
        CharConfig.MaxItemGrabTries = int.Parse(dataGridViewAdvanced.Rows[RowsIndex++].Cells[0].Value.ToString());
        CharConfig.MaxItemStashTries = int.Parse(dataGridViewAdvanced.Rows[RowsIndex++].Cells[0].Value.ToString());
        CharConfig.StashFullTries = int.Parse(dataGridViewAdvanced.Rows[RowsIndex++].Cells[0].Value.ToString());
        CharConfig.MaxShopTries = int.Parse(dataGridViewAdvanced.Rows[RowsIndex++].Cells[0].Value.ToString());
        CharConfig.MaxRepairTries = int.Parse(dataGridViewAdvanced.Rows[RowsIndex++].Cells[0].Value.ToString());
        CharConfig.MaxGambleTries = int.Parse(dataGridViewAdvanced.Rows[RowsIndex++].Cells[0].Value.ToString());
        CharConfig.MaxBattleAttackTries = int.Parse(dataGridViewAdvanced.Rows[RowsIndex++].Cells[0].Value.ToString());
        CharConfig.TakeHPPotionDelay = int.Parse(dataGridViewAdvanced.Rows[RowsIndex++].Cells[0].Value.ToString());
        CharConfig.TakeManaPotionDelay = int.Parse(dataGridViewAdvanced.Rows[RowsIndex++].Cells[0].Value.ToString());
        CharConfig.OverallDelaysMultiplyer = double.Parse(dataGridViewAdvanced.Rows[RowsIndex++].Cells[0].Value.ToString(), System.Globalization.CultureInfo.InvariantCulture);
        CharConfig.EndBattleGrabDelay = int.Parse(dataGridViewAdvanced.Rows[RowsIndex++].Cells[0].Value.ToString());
        CharConfig.MaxTimeEnterGame = int.Parse(dataGridViewAdvanced.Rows[RowsIndex++].Cells[0].Value.ToString());
        CharConfig.BaalWavesCastDelay = int.Parse(dataGridViewAdvanced.Rows[RowsIndex++].Cells[0].Value.ToString());
        CharConfig.ChaosWaitingSealBossDelay = int.Parse(dataGridViewAdvanced.Rows[RowsIndex++].Cells[0].Value.ToString());
        CharConfig.RecastBODelay = int.Parse(dataGridViewAdvanced.Rows[RowsIndex++].Cells[0].Value.ToString());
        CharConfig.TownSwitchAreaDelay = int.Parse(dataGridViewAdvanced.Rows[RowsIndex++].Cells[0].Value.ToString());
        CharConfig.PublicGameTPRespawnDelay = int.Parse(dataGridViewAdvanced.Rows[RowsIndex++].Cells[0].Value.ToString());
        CharConfig.TPRespawnDelay = int.Parse(dataGridViewAdvanced.Rows[RowsIndex++].Cells[0].Value.ToString());
        CharConfig.PlayerMaxHPCheckDelay = int.Parse(dataGridViewAdvanced.Rows[RowsIndex++].Cells[0].Value.ToString());
        CharConfig.LeechEnterTPDelay = int.Parse(dataGridViewAdvanced.Rows[RowsIndex++].Cells[0].Value.ToString());
        CharConfig.MephistoRedPortalEnterDelay = int.Parse(dataGridViewAdvanced.Rows[RowsIndex++].Cells[0].Value.ToString());
        CharConfig.CubeItemPlaceDelay = int.Parse(dataGridViewAdvanced.Rows[RowsIndex++].Cells[0].Value.ToString());
        CharConfig.CreateGameWaitDelay = int.Parse(dataGridViewAdvanced.Rows[RowsIndex++].Cells[0].Value.ToString());
    }

    private void FormAdvancedSettings_FormClosing(object sender, FormClosingEventArgs e)
    {
        SaveSettings();
    }

    private void button2_Click(object sender, EventArgs e)
    {
        SaveSettings();
    }

    private void dataGridViewAdvanced_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {

    }

    private void label20_Click(object sender, EventArgs e)
    {

    }
}
