
partial class FormAdvancedSettings
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAdvancedSettings));
            this.dataGridViewAdvanced = new System.Windows.Forms.DataGridView();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label20 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAdvanced)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewAdvanced
            // 
            this.dataGridViewAdvanced.AllowUserToAddRows = false;
            this.dataGridViewAdvanced.AllowUserToDeleteRows = false;
            this.dataGridViewAdvanced.AllowUserToResizeRows = false;
            this.dataGridViewAdvanced.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewAdvanced.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewAdvanced.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column2});
            this.dataGridViewAdvanced.EnableHeadersVisualStyles = false;
            this.dataGridViewAdvanced.Location = new System.Drawing.Point(12, 37);
            this.dataGridViewAdvanced.MultiSelect = false;
            this.dataGridViewAdvanced.Name = "dataGridViewAdvanced";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewAdvanced.RowHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewAdvanced.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dataGridViewAdvanced.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridViewAdvanced.Size = new System.Drawing.Size(329, 212);
            this.dataGridViewAdvanced.TabIndex = 15;
            this.dataGridViewAdvanced.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewAdvanced_CellContentClick);
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Value";
            this.Column2.Name = "Column2";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.ForeColor = System.Drawing.Color.Blue;
            this.label20.Location = new System.Drawing.Point(76, 6);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(193, 20);
            this.label20.TabIndex = 14;
            this.label20.Text = "Advanced Bot Settings";
            this.label20.Click += new System.EventHandler(this.label20_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button2.Image = global::app.Properties.Resources.Save;
            this.button2.Location = new System.Drawing.Point(293, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(48, 29);
            this.button2.TabIndex = 16;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // FormAdvancedSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(352, 256);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.dataGridViewAdvanced);
            this.Controls.Add(this.label20);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormAdvancedSettings";
            this.Text = "BMBot - Advanced Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormAdvancedSettings_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAdvanced)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.DataGridView dataGridViewAdvanced;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
    private System.Windows.Forms.Label label20;
    private System.Windows.Forms.Button button2;
}