namespace app
{
    partial class FormCharSettings
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
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxType = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxLeftSkill = new System.Windows.Forms.TextBox();
            this.textBoxRightSkill = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxFastMoveTown = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxFastMoveTeleport = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxDefenseSkill = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxCastDefenseSkill = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxLifeSkill = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.checkBoxAttachRightHand = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.comboBoxBelt1 = new System.Windows.Forms.ComboBox();
            this.comboBoxBelt2 = new System.Windows.Forms.ComboBox();
            this.comboBoxBelt3 = new System.Windows.Forms.ComboBox();
            this.comboBoxBelt4 = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.groupBoxInventory = new System.Windows.Forms.GroupBox();
            this.label13 = new System.Windows.Forms.Label();
            this.textBoxCharName = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.checkBoxUseTeleport = new System.Windows.Forms.CheckBox();
            this.label15 = new System.Windows.Forms.Label();
            this.numericUpDownChickenHP = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownTakeHP = new System.Windows.Forms.NumericUpDown();
            this.label16 = new System.Windows.Forms.Label();
            this.numericUpDownTakeMana = new System.Windows.Forms.NumericUpDown();
            this.label17 = new System.Windows.Forms.Label();
            this.numericUpDownTakeRV = new System.Windows.Forms.NumericUpDown();
            this.label18 = new System.Windows.Forms.Label();
            this.numericUpDownGambleAbove = new System.Windows.Forms.NumericUpDown();
            this.label19 = new System.Windows.Forms.Label();
            this.numericUpDownGambleUntil = new System.Windows.Forms.NumericUpDown();
            this.label20 = new System.Windows.Forms.Label();
            this.numericUpDownKeyXPos = new System.Windows.Forms.NumericUpDown();
            this.label21 = new System.Windows.Forms.Label();
            this.numericUpDownKeyYPos = new System.Windows.Forms.NumericUpDown();
            this.checkBoxUseMerc = new System.Windows.Forms.CheckBox();
            this.numericUpDownMercTakeHPUnder = new System.Windows.Forms.NumericUpDown();
            this.label22 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownChickenHP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTakeHP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTakeMana)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTakeRV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownGambleAbove)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownGambleUntil)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownKeyXPos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownKeyYPos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMercTakeHPUnder)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(48, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Char Type:";
            // 
            // comboBoxType
            // 
            this.comboBoxType.FormattingEnabled = true;
            this.comboBoxType.Items.AddRange(new object[] {
            "Paladin Hammer",
            "Sorceress Blizzard"});
            this.comboBoxType.Location = new System.Drawing.Point(116, 9);
            this.comboBoxType.Name = "comboBoxType";
            this.comboBoxType.Size = new System.Drawing.Size(199, 21);
            this.comboBoxType.TabIndex = 1;
            this.comboBoxType.SelectedIndexChanged += new System.EventHandler(this.comboBoxType_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBoxAttachRightHand);
            this.groupBox1.Controls.Add(this.textBoxLifeSkill);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.textBoxCastDefenseSkill);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.textBoxDefenseSkill);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.textBoxFastMoveTeleport);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.textBoxFastMoveTown);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.textBoxRightSkill);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textBoxLeftSkill);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(5, 36);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(171, 207);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Skills Shortcuts Keys";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Attack Left Skill:";
            // 
            // textBoxLeftSkill
            // 
            this.textBoxLeftSkill.Location = new System.Drawing.Point(123, 46);
            this.textBoxLeftSkill.Name = "textBoxLeftSkill";
            this.textBoxLeftSkill.Size = new System.Drawing.Size(37, 20);
            this.textBoxLeftSkill.TabIndex = 4;
            // 
            // textBoxRightSkill
            // 
            this.textBoxRightSkill.Location = new System.Drawing.Point(123, 68);
            this.textBoxRightSkill.Name = "textBoxRightSkill";
            this.textBoxRightSkill.Size = new System.Drawing.Size(37, 20);
            this.textBoxRightSkill.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Attack Right Skill:";
            // 
            // textBoxFastMoveTown
            // 
            this.textBoxFastMoveTown.Location = new System.Drawing.Point(123, 90);
            this.textBoxFastMoveTown.Name = "textBoxFastMoveTown";
            this.textBoxFastMoveTown.Size = new System.Drawing.Size(37, 20);
            this.textBoxFastMoveTown.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 93);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(108, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Fast Move (In Town):";
            // 
            // textBoxFastMoveTeleport
            // 
            this.textBoxFastMoveTeleport.Location = new System.Drawing.Point(123, 112);
            this.textBoxFastMoveTeleport.Name = "textBoxFastMoveTeleport";
            this.textBoxFastMoveTeleport.Size = new System.Drawing.Size(37, 20);
            this.textBoxFastMoveTeleport.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 115);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(108, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Fast Move (Teleport):";
            // 
            // textBoxDefenseSkill
            // 
            this.textBoxDefenseSkill.Location = new System.Drawing.Point(123, 134);
            this.textBoxDefenseSkill.Name = "textBoxDefenseSkill";
            this.textBoxDefenseSkill.Size = new System.Drawing.Size(37, 20);
            this.textBoxDefenseSkill.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 137);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Defense Skill:";
            // 
            // textBoxCastDefenseSkill
            // 
            this.textBoxCastDefenseSkill.Location = new System.Drawing.Point(123, 156);
            this.textBoxCastDefenseSkill.Name = "textBoxCastDefenseSkill";
            this.textBoxCastDefenseSkill.Size = new System.Drawing.Size(37, 20);
            this.textBoxCastDefenseSkill.TabIndex = 14;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 159);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(96, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Cast Defense Skill:";
            // 
            // textBoxLifeSkill
            // 
            this.textBoxLifeSkill.Location = new System.Drawing.Point(123, 178);
            this.textBoxLifeSkill.Name = "textBoxLifeSkill";
            this.textBoxLifeSkill.Size = new System.Drawing.Size(37, 20);
            this.textBoxLifeSkill.TabIndex = 16;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(10, 181);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(49, 13);
            this.label8.TabIndex = 15;
            this.label8.Text = "Life Skill:";
            // 
            // checkBoxAttachRightHand
            // 
            this.checkBoxAttachRightHand.AutoSize = true;
            this.checkBoxAttachRightHand.Location = new System.Drawing.Point(20, 22);
            this.checkBoxAttachRightHand.Name = "checkBoxAttachRightHand";
            this.checkBoxAttachRightHand.Size = new System.Drawing.Size(136, 17);
            this.checkBoxAttachRightHand.TabIndex = 17;
            this.checkBoxAttachRightHand.Text = "Attack with Right Hand";
            this.checkBoxAttachRightHand.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.comboBoxBelt4);
            this.groupBox2.Controls.Add(this.comboBoxBelt3);
            this.groupBox2.Controls.Add(this.comboBoxBelt2);
            this.groupBox2.Controls.Add(this.comboBoxBelt1);
            this.groupBox2.Location = new System.Drawing.Point(201, 249);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(245, 64);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Belt Potions Type";
            // 
            // comboBoxBelt1
            // 
            this.comboBoxBelt1.FormattingEnabled = true;
            this.comboBoxBelt1.Items.AddRange(new object[] {
            "HP",
            "Mana",
            "RV",
            "Full RV"});
            this.comboBoxBelt1.Location = new System.Drawing.Point(6, 38);
            this.comboBoxBelt1.Name = "comboBoxBelt1";
            this.comboBoxBelt1.Size = new System.Drawing.Size(58, 21);
            this.comboBoxBelt1.TabIndex = 0;
            // 
            // comboBoxBelt2
            // 
            this.comboBoxBelt2.FormattingEnabled = true;
            this.comboBoxBelt2.Items.AddRange(new object[] {
            "HP",
            "Mana",
            "RV",
            "Full RV"});
            this.comboBoxBelt2.Location = new System.Drawing.Point(64, 38);
            this.comboBoxBelt2.Name = "comboBoxBelt2";
            this.comboBoxBelt2.Size = new System.Drawing.Size(58, 21);
            this.comboBoxBelt2.TabIndex = 1;
            // 
            // comboBoxBelt3
            // 
            this.comboBoxBelt3.FormattingEnabled = true;
            this.comboBoxBelt3.Items.AddRange(new object[] {
            "HP",
            "Mana",
            "RV",
            "Full RV"});
            this.comboBoxBelt3.Location = new System.Drawing.Point(122, 38);
            this.comboBoxBelt3.Name = "comboBoxBelt3";
            this.comboBoxBelt3.Size = new System.Drawing.Size(58, 21);
            this.comboBoxBelt3.TabIndex = 2;
            // 
            // comboBoxBelt4
            // 
            this.comboBoxBelt4.FormattingEnabled = true;
            this.comboBoxBelt4.Items.AddRange(new object[] {
            "HP",
            "Mana",
            "RV",
            "Full RV"});
            this.comboBoxBelt4.Location = new System.Drawing.Point(180, 38);
            this.comboBoxBelt4.Name = "comboBoxBelt4";
            this.comboBoxBelt4.Size = new System.Drawing.Size(58, 21);
            this.comboBoxBelt4.TabIndex = 3;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(21, 22);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(34, 13);
            this.label9.TabIndex = 4;
            this.label9.Text = "Slot1:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(78, 22);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(34, 13);
            this.label10.TabIndex = 5;
            this.label10.Text = "Slot2:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(136, 22);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(34, 13);
            this.label11.TabIndex = 6;
            this.label11.Text = "Slot3:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(193, 22);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(34, 13);
            this.label12.TabIndex = 7;
            this.label12.Text = "Slot4:";
            // 
            // groupBoxInventory
            // 
            this.groupBoxInventory.Location = new System.Drawing.Point(5, 319);
            this.groupBoxInventory.Name = "groupBoxInventory";
            this.groupBoxInventory.Size = new System.Drawing.Size(441, 101);
            this.groupBoxInventory.TabIndex = 4;
            this.groupBoxInventory.TabStop = false;
            this.groupBoxInventory.Text = "Inventory Available Slots (Checked mean this slot is NOT FREE)";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.ForeColor = System.Drawing.Color.Red;
            this.label13.Location = new System.Drawing.Point(106, 427);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(258, 52);
            this.label13.TabIndex = 5;
            this.label13.Text = "**Place theses dummy items inside the shared stash**\r\n-Key in Shared Stash1\r\n-ID " +
    "Scroll in Shared Stash2\r\n-TP Scroll in Shared Stash3";
            this.label13.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // textBoxCharName
            // 
            this.textBoxCharName.Location = new System.Drawing.Point(108, 18);
            this.textBoxCharName.Name = "textBoxCharName";
            this.textBoxCharName.Size = new System.Drawing.Size(149, 20);
            this.textBoxCharName.TabIndex = 19;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(15, 21);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(63, 13);
            this.label14.TabIndex = 18;
            this.label14.Text = "Char Name:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.numericUpDownKeyYPos);
            this.groupBox3.Controls.Add(this.numericUpDownKeyXPos);
            this.groupBox3.Controls.Add(this.label21);
            this.groupBox3.Controls.Add(this.numericUpDownGambleUntil);
            this.groupBox3.Controls.Add(this.label20);
            this.groupBox3.Controls.Add(this.numericUpDownGambleAbove);
            this.groupBox3.Controls.Add(this.label19);
            this.groupBox3.Controls.Add(this.numericUpDownTakeRV);
            this.groupBox3.Controls.Add(this.checkBoxUseTeleport);
            this.groupBox3.Controls.Add(this.label18);
            this.groupBox3.Controls.Add(this.numericUpDownTakeMana);
            this.groupBox3.Controls.Add(this.label17);
            this.groupBox3.Controls.Add(this.numericUpDownTakeHP);
            this.groupBox3.Controls.Add(this.label16);
            this.groupBox3.Controls.Add(this.numericUpDownChickenHP);
            this.groupBox3.Controls.Add(this.label15);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Controls.Add(this.textBoxCharName);
            this.groupBox3.Location = new System.Drawing.Point(182, 36);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(264, 207);
            this.groupBox3.TabIndex = 20;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Char Parameters";
            // 
            // checkBoxUseTeleport
            // 
            this.checkBoxUseTeleport.AutoSize = true;
            this.checkBoxUseTeleport.Location = new System.Drawing.Point(18, 186);
            this.checkBoxUseTeleport.Name = "checkBoxUseTeleport";
            this.checkBoxUseTeleport.Size = new System.Drawing.Size(87, 17);
            this.checkBoxUseTeleport.TabIndex = 18;
            this.checkBoxUseTeleport.Text = "Use Teleport";
            this.checkBoxUseTeleport.UseVisualStyleBackColor = true;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(15, 43);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(67, 13);
            this.label15.TabIndex = 20;
            this.label15.Text = "Chicken HP:";
            // 
            // numericUpDownChickenHP
            // 
            this.numericUpDownChickenHP.Location = new System.Drawing.Point(162, 41);
            this.numericUpDownChickenHP.Name = "numericUpDownChickenHP";
            this.numericUpDownChickenHP.Size = new System.Drawing.Size(53, 20);
            this.numericUpDownChickenHP.TabIndex = 21;
            // 
            // numericUpDownTakeHP
            // 
            this.numericUpDownTakeHP.Location = new System.Drawing.Point(162, 62);
            this.numericUpDownTakeHP.Name = "numericUpDownTakeHP";
            this.numericUpDownTakeHP.Size = new System.Drawing.Size(53, 20);
            this.numericUpDownTakeHP.TabIndex = 23;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(15, 64);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(115, 13);
            this.label16.TabIndex = 22;
            this.label16.Text = "Take HP potion under:";
            // 
            // numericUpDownTakeMana
            // 
            this.numericUpDownTakeMana.Location = new System.Drawing.Point(162, 83);
            this.numericUpDownTakeMana.Name = "numericUpDownTakeMana";
            this.numericUpDownTakeMana.Size = new System.Drawing.Size(53, 20);
            this.numericUpDownTakeMana.TabIndex = 25;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(15, 85);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(127, 13);
            this.label17.TabIndex = 24;
            this.label17.Text = "Take Mana potion under:";
            // 
            // numericUpDownTakeRV
            // 
            this.numericUpDownTakeRV.Location = new System.Drawing.Point(162, 104);
            this.numericUpDownTakeRV.Name = "numericUpDownTakeRV";
            this.numericUpDownTakeRV.Size = new System.Drawing.Size(53, 20);
            this.numericUpDownTakeRV.TabIndex = 27;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(15, 106);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(139, 13);
            this.label18.TabIndex = 26;
            this.label18.Text = "Take RV potion under (HP):";
            // 
            // numericUpDownGambleAbove
            // 
            this.numericUpDownGambleAbove.Increment = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownGambleAbove.Location = new System.Drawing.Point(162, 125);
            this.numericUpDownGambleAbove.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.numericUpDownGambleAbove.Name = "numericUpDownGambleAbove";
            this.numericUpDownGambleAbove.Size = new System.Drawing.Size(95, 20);
            this.numericUpDownGambleAbove.TabIndex = 29;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(15, 127);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(140, 13);
            this.label19.TabIndex = 28;
            this.label19.Text = "Gamble above gold amount:";
            // 
            // numericUpDownGambleUntil
            // 
            this.numericUpDownGambleUntil.Increment = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownGambleUntil.Location = new System.Drawing.Point(162, 146);
            this.numericUpDownGambleUntil.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.numericUpDownGambleUntil.Name = "numericUpDownGambleUntil";
            this.numericUpDownGambleUntil.Size = new System.Drawing.Size(95, 20);
            this.numericUpDownGambleUntil.TabIndex = 31;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(15, 148);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(129, 13);
            this.label20.TabIndex = 30;
            this.label20.Text = "Gamble until gold amount:";
            // 
            // numericUpDownKeyXPos
            // 
            this.numericUpDownKeyXPos.Location = new System.Drawing.Point(174, 167);
            this.numericUpDownKeyXPos.Name = "numericUpDownKeyXPos";
            this.numericUpDownKeyXPos.Size = new System.Drawing.Size(41, 20);
            this.numericUpDownKeyXPos.TabIndex = 33;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(15, 169);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(153, 13);
            this.label21.TabIndex = 32;
            this.label21.Text = "Keys location in Inventory (x,y):";
            // 
            // numericUpDownKeyYPos
            // 
            this.numericUpDownKeyYPos.Location = new System.Drawing.Point(216, 167);
            this.numericUpDownKeyYPos.Name = "numericUpDownKeyYPos";
            this.numericUpDownKeyYPos.Size = new System.Drawing.Size(41, 20);
            this.numericUpDownKeyYPos.TabIndex = 34;
            // 
            // checkBoxUseMerc
            // 
            this.checkBoxUseMerc.AutoSize = true;
            this.checkBoxUseMerc.Location = new System.Drawing.Point(46, 16);
            this.checkBoxUseMerc.Name = "checkBoxUseMerc";
            this.checkBoxUseMerc.Size = new System.Drawing.Size(72, 17);
            this.checkBoxUseMerc.TabIndex = 35;
            this.checkBoxUseMerc.Text = "Use Merc";
            this.checkBoxUseMerc.UseVisualStyleBackColor = true;
            // 
            // numericUpDownMercTakeHPUnder
            // 
            this.numericUpDownMercTakeHPUnder.Location = new System.Drawing.Point(128, 37);
            this.numericUpDownMercTakeHPUnder.Name = "numericUpDownMercTakeHPUnder";
            this.numericUpDownMercTakeHPUnder.Size = new System.Drawing.Size(53, 20);
            this.numericUpDownMercTakeHPUnder.TabIndex = 36;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(7, 39);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(115, 13);
            this.label22.TabIndex = 35;
            this.label22.Text = "Take HP potion under:";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.checkBoxUseMerc);
            this.groupBox4.Controls.Add(this.numericUpDownMercTakeHPUnder);
            this.groupBox4.Controls.Add(this.label22);
            this.groupBox4.Location = new System.Drawing.Point(5, 249);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(190, 64);
            this.groupBox4.TabIndex = 37;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Merc Parameters";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.Red;
            this.button1.Location = new System.Drawing.Point(364, 7);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 38;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FormCharSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(452, 487);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.groupBoxInventory);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.comboBoxType);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FormCharSettings";
            this.ShowIcon = false;
            this.Text = "D2R - BMBot - Char Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormCharSettings_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownChickenHP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTakeHP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTakeMana)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTakeRV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownGambleAbove)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownGambleUntil)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownKeyXPos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownKeyYPos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMercTakeHPUnder)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxType;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBoxFastMoveTown;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxRightSkill;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxLeftSkill;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxLifeSkill;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxCastDefenseSkill;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxDefenseSkill;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxFastMoveTeleport;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox checkBoxAttachRightHand;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox comboBoxBelt2;
        private System.Windows.Forms.ComboBox comboBoxBelt1;
        private System.Windows.Forms.ComboBox comboBoxBelt4;
        private System.Windows.Forms.ComboBox comboBoxBelt3;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox groupBoxInventory;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox textBoxCharName;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox checkBoxUseTeleport;
        private System.Windows.Forms.NumericUpDown numericUpDownTakeRV;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.NumericUpDown numericUpDownTakeMana;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.NumericUpDown numericUpDownTakeHP;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.NumericUpDown numericUpDownChickenHP;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.NumericUpDown numericUpDownGambleUntil;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.NumericUpDown numericUpDownGambleAbove;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.NumericUpDown numericUpDownKeyYPos;
        private System.Windows.Forms.NumericUpDown numericUpDownKeyXPos;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.CheckBox checkBoxUseMerc;
        private System.Windows.Forms.NumericUpDown numericUpDownMercTakeHPUnder;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button button1;
    }
}