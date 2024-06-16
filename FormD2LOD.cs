using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

public partial class FormD2LOD : Form
{

    Form1 Form1_0;

    public FormD2LOD(Form1 form1_1)
    {
        Form1_0 = form1_1;

        InitializeComponent();
        this.TopMost = true;
    }

    private void button3_Click(object sender, EventArgs e)
    {
        System.Diagnostics.Process.Start("https://ia903402.us.archive.org/5/items/diablo-ii-lod/diablo-ii-lod_archive.torrent");
    }

    private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        System.Diagnostics.Process.Start("https://www.utorrent.com/web/");
    }

    private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        System.Diagnostics.Process.Start("https://www.win-rar.com/download.html");
    }

    private void button2_Click(object sender, EventArgs e)
    {
        System.Diagnostics.Process.Start("https://www.moddb.com/games/diablo-2-lod/downloads/lod-patch-113c-for-windows");
    }

    private void label2_Click(object sender, EventArgs e)
    {

    }

    private void FormD2LOD_Load(object sender, EventArgs e)
    {

    }

    private void button1_Click(object sender, EventArgs e)
    {
        CreateD2LODRegKey(textBox1.Text);
    }

    public void CreateD2LODRegKey(string InstallDir)
    {
        string keyPath = @"Software\Blizzard Entertainment\Diablo II";
        using (RegistryKey key = Registry.CurrentUser.CreateSubKey(keyPath))
        {
            if (key != null)
            {
                key.SetValue("InstallPath", InstallDir);

                Form1_0.method_1("Registry key created: " + keyPath, Color.Magenta);
                Form1_0.method_1("'InstallPath': " + InstallDir, Color.Magenta);
                Form1_0.method_1("---------------------------------", Color.DarkGreen);
                Form1_0.method_1("Ready to install the 1.13C Patch!", Color.DarkGreen);
            }
            else
            {
                Form1_0.method_1("Failed to create registry key: " + keyPath, Color.Red);
            }
        }
    }
}
