using System;
using System.Drawing;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace app
{
    public partial class FormItems : Form
    {
        Form1 Form1_0;

        public FormItems(Form1 form1_1)
        {
            Form1_0 = form1_1;

            InitializeComponent();

            LoadSettings();
        }

        public void LoadSettings()
        {
            listViewUnique.Items.Clear();
            listViewKeys.Items.Clear();
            listViewGems.Items.Clear();
            listViewRunes.Items.Clear();
            listViewSet.Items.Clear();

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

        }

        public void SaveSettings()
        {
            foreach (var ThisDir in Form1_0.ItemsAlert_0.PickItemsUnique)
            {
                for (int i = 0; i < listViewUnique.Items.Count; i++) 
                {
                    if (ThisDir.Key == listViewUnique.Items[i].ToString())
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
                        if (ThisDir.Key == listViewKeys.Items[i].ToString())
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
                        if (ThisDir.Key == listViewGems.Items[i].ToString())
                        {
                            Form1_0.ItemsAlert_0.PickItemsRunesKeyGems[ThisDir.Key] = listViewGems.Items[i].Checked;
                        }
                    }
                }

                if (ThisDir.Key.Contains("Rune"))
                {
                    for (int i = 0; i < listViewRunes.Items.Count; i++)
                    {
                        if (ThisDir.Key == listViewRunes.Items[i].ToString())
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
                    if (ThisDir.Key == listViewSet.Items[i].ToString())
                    {
                        Form1_0.ItemsAlert_0.PickItemsSet[ThisDir.Key] = listViewSet.Items[i].Checked;
                    }
                }
            }

            Form1_0.SettingsLoader_0.SaveItemsSettings();
        }

        private void FormSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveSettings();
        }

        private void FormSettings_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            SaveSettings();
        }
    }
}
