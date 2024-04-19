namespace app
{
    partial class FormItems
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
            this.listViewUnique = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.listViewKeys = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.listViewGems = new System.Windows.Forms.ListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.listViewRunes = new System.Windows.Forms.ListView();
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.listViewSet = new System.Windows.Forms.ListView();
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.tabPage7.SuspendLayout();
            this.SuspendLayout();
            // 
            // listViewUnique
            // 
            this.listViewUnique.CheckBoxes = true;
            this.listViewUnique.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader10});
            this.listViewUnique.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewUnique.FullRowSelect = true;
            this.listViewUnique.GridLines = true;
            this.listViewUnique.HideSelection = false;
            this.listViewUnique.Location = new System.Drawing.Point(3, 3);
            this.listViewUnique.Name = "listViewUnique";
            this.listViewUnique.Size = new System.Drawing.Size(349, 329);
            this.listViewUnique.TabIndex = 0;
            this.listViewUnique.UseCompatibleStateImageBehavior = false;
            this.listViewUnique.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Item Name";
            this.columnHeader1.Width = 163;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "Description";
            this.columnHeader10.Width = 163;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Controls.Add(this.tabPage6);
            this.tabControl1.Controls.Add(this.tabPage7);
            this.tabControl1.Location = new System.Drawing.Point(3, 11);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(363, 361);
            this.tabControl1.TabIndex = 15;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.listViewUnique);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(355, 335);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Unique";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.listViewKeys);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(355, 335);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Keys";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // listViewKeys
            // 
            this.listViewKeys.CheckBoxes = true;
            this.listViewKeys.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2,
            this.columnHeader9});
            this.listViewKeys.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewKeys.FullRowSelect = true;
            this.listViewKeys.GridLines = true;
            this.listViewKeys.HideSelection = false;
            this.listViewKeys.Location = new System.Drawing.Point(3, 3);
            this.listViewKeys.Name = "listViewKeys";
            this.listViewKeys.Size = new System.Drawing.Size(349, 329);
            this.listViewKeys.TabIndex = 1;
            this.listViewKeys.UseCompatibleStateImageBehavior = false;
            this.listViewKeys.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Item Name";
            this.columnHeader2.Width = 193;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "Description";
            this.columnHeader9.Width = 133;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.listViewGems);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(355, 335);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Gems";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // listViewGems
            // 
            this.listViewGems.CheckBoxes = true;
            this.listViewGems.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader8});
            this.listViewGems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewGems.FullRowSelect = true;
            this.listViewGems.GridLines = true;
            this.listViewGems.HideSelection = false;
            this.listViewGems.Location = new System.Drawing.Point(0, 0);
            this.listViewGems.Name = "listViewGems";
            this.listViewGems.Size = new System.Drawing.Size(355, 335);
            this.listViewGems.TabIndex = 1;
            this.listViewGems.UseCompatibleStateImageBehavior = false;
            this.listViewGems.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Item Name";
            this.columnHeader3.Width = 163;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Description";
            this.columnHeader8.Width = 163;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.listViewRunes);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(355, 335);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Runes";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // listViewRunes
            // 
            this.listViewRunes.CheckBoxes = true;
            this.listViewRunes.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader4,
            this.columnHeader7});
            this.listViewRunes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewRunes.FullRowSelect = true;
            this.listViewRunes.GridLines = true;
            this.listViewRunes.HideSelection = false;
            this.listViewRunes.Location = new System.Drawing.Point(0, 0);
            this.listViewRunes.Name = "listViewRunes";
            this.listViewRunes.Size = new System.Drawing.Size(355, 335);
            this.listViewRunes.TabIndex = 1;
            this.listViewRunes.UseCompatibleStateImageBehavior = false;
            this.listViewRunes.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Item Name";
            this.columnHeader4.Width = 163;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Description";
            this.columnHeader7.Width = 163;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.listViewSet);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(355, 335);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Set";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // listViewSet
            // 
            this.listViewSet.CheckBoxes = true;
            this.listViewSet.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader5,
            this.columnHeader6});
            this.listViewSet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewSet.FullRowSelect = true;
            this.listViewSet.GridLines = true;
            this.listViewSet.HideSelection = false;
            this.listViewSet.Location = new System.Drawing.Point(0, 0);
            this.listViewSet.Name = "listViewSet";
            this.listViewSet.Size = new System.Drawing.Size(355, 335);
            this.listViewSet.TabIndex = 1;
            this.listViewSet.UseCompatibleStateImageBehavior = false;
            this.listViewSet.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Item Name";
            this.columnHeader5.Width = 163;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Description";
            this.columnHeader6.Width = 163;
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.label1);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Size = new System.Drawing.Size(355, 335);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "Normal";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(44, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(264, 50);
            this.label1.TabIndex = 0;
            this.label1.Text = "MISSING NORMAL\r\nITEM PICKIT SETTINGS";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.ForeColor = System.Drawing.Color.Red;
            this.button3.Location = new System.Drawing.Point(322, 4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(44, 23);
            this.button3.TabIndex = 16;
            this.button3.Text = "Save";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // tabPage7
            // 
            this.tabPage7.Controls.Add(this.label2);
            this.tabPage7.Location = new System.Drawing.Point(4, 22);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Size = new System.Drawing.Size(355, 335);
            this.tabPage7.TabIndex = 6;
            this.tabPage7.Text = "Rare";
            this.tabPage7.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(44, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(264, 50);
            this.label2.TabIndex = 1;
            this.label2.Text = "MISSING RARE\r\nITEM PICKIT SETTINGS";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // FormItems
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(370, 375);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FormItems";
            this.ShowIcon = false;
            this.Text = "D2R - BMBot - Items";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormSettings_FormClosing);
            this.Load += new System.EventHandler(this.FormSettings_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.tabPage6.ResumeLayout(false);
            this.tabPage6.PerformLayout();
            this.tabPage7.ResumeLayout(false);
            this.tabPage7.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listViewUnique;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ListView listViewKeys;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.ListView listViewGems;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.ListView listViewRunes;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.ListView listViewSet;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPage7;
        private System.Windows.Forms.Label label2;
    }
}