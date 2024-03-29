namespace app
{
    partial class FormSettings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("Item Grab ONLY");
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("Countess");
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("Andariel");
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem("Summoner");
            System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem("Duriel");
            System.Windows.Forms.ListViewItem listViewItem6 = new System.Windows.Forms.ListViewItem("Lower Kurast");
            System.Windows.Forms.ListViewItem listViewItem7 = new System.Windows.Forms.ListViewItem("Mephisto");
            System.Windows.Forms.ListViewItem listViewItem8 = new System.Windows.Forms.ListViewItem("Chaos Leech");
            System.Windows.Forms.ListViewItem listViewItem9 = new System.Windows.Forms.ListViewItem("Baal Leech");
            this.listViewRunScripts = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.numericUpDownRunNumber = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBoxDifficulty = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxGamePass = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxGameName = new System.Windows.Forms.TextBox();
            this.numericUpDownMaxTime = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.comboBoxLobby = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxD2Path = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRunNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxTime)).BeginInit();
            this.SuspendLayout();
            // 
            // listViewRunScripts
            // 
            this.listViewRunScripts.CheckBoxes = true;
            this.listViewRunScripts.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.listViewRunScripts.FullRowSelect = true;
            this.listViewRunScripts.GridLines = true;
            this.listViewRunScripts.HideSelection = false;
            listViewItem1.StateImageIndex = 0;
            listViewItem2.Checked = true;
            listViewItem2.StateImageIndex = 1;
            listViewItem3.StateImageIndex = 0;
            listViewItem4.StateImageIndex = 0;
            listViewItem5.StateImageIndex = 0;
            listViewItem6.StateImageIndex = 0;
            listViewItem7.StateImageIndex = 0;
            listViewItem8.StateImageIndex = 0;
            listViewItem9.StateImageIndex = 0;
            this.listViewRunScripts.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3,
            listViewItem4,
            listViewItem5,
            listViewItem6,
            listViewItem7,
            listViewItem8,
            listViewItem9});
            this.listViewRunScripts.Location = new System.Drawing.Point(12, 132);
            this.listViewRunScripts.Name = "listViewRunScripts";
            this.listViewRunScripts.Scrollable = false;
            this.listViewRunScripts.Size = new System.Drawing.Size(225, 157);
            this.listViewRunScripts.TabIndex = 0;
            this.listViewRunScripts.UseCompatibleStateImageBehavior = false;
            this.listViewRunScripts.View = System.Windows.Forms.View.List;
            this.listViewRunScripts.SelectedIndexChanged += new System.EventHandler(this.listViewRunScripts_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Width = 200;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(54, 108);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(134, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Select Run Scripts";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(50, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(142, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Select Lobby Script";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.numericUpDownRunNumber);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.comboBoxDifficulty);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.textBoxGamePass);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textBoxGameName);
            this.groupBox1.Location = new System.Drawing.Point(3, 328);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(244, 122);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Game Maker Settings";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(180, 65);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(54, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "Reset";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // numericUpDownRunNumber
            // 
            this.numericUpDownRunNumber.Location = new System.Drawing.Point(85, 67);
            this.numericUpDownRunNumber.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.numericUpDownRunNumber.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownRunNumber.Name = "numericUpDownRunNumber";
            this.numericUpDownRunNumber.Size = new System.Drawing.Size(89, 20);
            this.numericUpDownRunNumber.TabIndex = 7;
            this.numericUpDownRunNumber.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 69);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Run Number:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 94);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Difficulty:";
            // 
            // comboBoxDifficulty
            // 
            this.comboBoxDifficulty.FormattingEnabled = true;
            this.comboBoxDifficulty.Items.AddRange(new object[] {
            "Normal",
            "Nightmare",
            "Hell"});
            this.comboBoxDifficulty.Location = new System.Drawing.Point(85, 91);
            this.comboBoxDifficulty.Name = "comboBoxDifficulty";
            this.comboBoxDifficulty.Size = new System.Drawing.Size(149, 21);
            this.comboBoxDifficulty.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Game Pass:";
            // 
            // textBoxGamePass
            // 
            this.textBoxGamePass.Location = new System.Drawing.Point(85, 43);
            this.textBoxGamePass.Name = "textBoxGamePass";
            this.textBoxGamePass.Size = new System.Drawing.Size(149, 20);
            this.textBoxGamePass.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Game Name:";
            // 
            // textBoxGameName
            // 
            this.textBoxGameName.Location = new System.Drawing.Point(85, 17);
            this.textBoxGameName.Name = "textBoxGameName";
            this.textBoxGameName.Size = new System.Drawing.Size(149, 20);
            this.textBoxGameName.TabIndex = 0;
            // 
            // numericUpDownMaxTime
            // 
            this.numericUpDownMaxTime.Location = new System.Drawing.Point(162, 299);
            this.numericUpDownMaxTime.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.numericUpDownMaxTime.Name = "numericUpDownMaxTime";
            this.numericUpDownMaxTime.Size = new System.Drawing.Size(58, 20);
            this.numericUpDownMaxTime.TabIndex = 10;
            this.numericUpDownMaxTime.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(27, 301);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(132, 13);
            this.label7.TabIndex = 9;
            this.label7.Text = "Max Game Time (minutes):";
            // 
            // comboBoxLobby
            // 
            this.comboBoxLobby.FormattingEnabled = true;
            this.comboBoxLobby.Items.AddRange(new object[] {
            "Game Create/Maker",
            "Chaos Search (Leech)",
            "Baal Search (Leech)"});
            this.comboBoxLobby.Location = new System.Drawing.Point(12, 83);
            this.comboBoxLobby.Name = "comboBoxLobby";
            this.comboBoxLobby.Size = new System.Drawing.Size(226, 21);
            this.comboBoxLobby.TabIndex = 9;
            this.comboBoxLobby.SelectedIndexChanged += new System.EventHandler(this.comboBoxLobby_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(50, 14);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(137, 16);
            this.label8.TabIndex = 10;
            this.label8.Text = "D2 LOD 1.13C Path";
            // 
            // textBoxD2Path
            // 
            this.textBoxD2Path.Location = new System.Drawing.Point(12, 33);
            this.textBoxD2Path.Name = "textBoxD2Path";
            this.textBoxD2Path.Size = new System.Drawing.Size(225, 20);
            this.textBoxD2Path.TabIndex = 9;
            // 
            // FormSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(253, 454);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.comboBoxLobby);
            this.Controls.Add(this.textBoxD2Path);
            this.Controls.Add(this.numericUpDownMaxTime);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listViewRunScripts);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FormSettings";
            this.ShowIcon = false;
            this.Text = "D2R - BMBot - Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormSettings_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRunNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxTime)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listViewRunScripts;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBoxDifficulty;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxGamePass;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxGameName;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.NumericUpDown numericUpDownRunNumber;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numericUpDownMaxTime;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox comboBoxLobby;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxD2Path;
    }
}