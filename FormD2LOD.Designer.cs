
partial class FormD2LOD
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormD2LOD));
        this.button1 = new System.Windows.Forms.Button();
        this.label1 = new System.Windows.Forms.Label();
        this.textBox1 = new System.Windows.Forms.TextBox();
        this.label2 = new System.Windows.Forms.Label();
        this.button2 = new System.Windows.Forms.Button();
        this.label3 = new System.Windows.Forms.Label();
        this.button3 = new System.Windows.Forms.Button();
        this.label4 = new System.Windows.Forms.Label();
        this.linkLabel1 = new System.Windows.Forms.LinkLabel();
        this.label5 = new System.Windows.Forms.Label();
        this.label6 = new System.Windows.Forms.Label();
        this.linkLabel2 = new System.Windows.Forms.LinkLabel();
        this.label7 = new System.Windows.Forms.Label();
        this.SuspendLayout();
        // 
        // button1
        // 
        this.button1.Location = new System.Drawing.Point(111, 179);
        this.button1.Name = "button1";
        this.button1.Size = new System.Drawing.Size(218, 39);
        this.button1.TabIndex = 0;
        this.button1.Text = "Step #5 Create \'InstallDir\' Registry Key";
        this.button1.UseVisualStyleBackColor = true;
        this.button1.Click += new System.EventHandler(this.button1_Click);
        // 
        // label1
        // 
        this.label1.AutoSize = true;
        this.label1.Location = new System.Drawing.Point(13, 156);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(74, 13);
        this.label1.TabIndex = 1;
        this.label1.Text = "D2 LOD Path:";
        // 
        // textBox1
        // 
        this.textBox1.Location = new System.Drawing.Point(93, 153);
        this.textBox1.Name = "textBox1";
        this.textBox1.Size = new System.Drawing.Size(282, 20);
        this.textBox1.TabIndex = 2;
        // 
        // label2
        // 
        this.label2.AutoSize = true;
        this.label2.Location = new System.Drawing.Point(34, 117);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(333, 26);
        this.label2.TabIndex = 3;
        this.label2.Text = "Step#4 - Set the path where Diablo II - Lord of Destruction is installed\r\n(the ne" +
"w Diablo2 folder created on your desktop)";
        this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
        this.label2.Click += new System.EventHandler(this.label2_Click);
        // 
        // button2
        // 
        this.button2.Location = new System.Drawing.Point(111, 224);
        this.button2.Name = "button2";
        this.button2.Size = new System.Drawing.Size(218, 39);
        this.button2.TabIndex = 4;
        this.button2.Text = "Step#6 Download the 1.13C Patch";
        this.button2.UseVisualStyleBackColor = true;
        this.button2.Click += new System.EventHandler(this.button2_Click);
        // 
        // label3
        // 
        this.label3.AutoSize = true;
        this.label3.Location = new System.Drawing.Point(115, 276);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(208, 13);
        this.label3.TabIndex = 5;
        this.label3.Text = "Step#7 - Open and Install the 1.13C Patch";
        // 
        // button3
        // 
        this.button3.Location = new System.Drawing.Point(93, 23);
        this.button3.Name = "button3";
        this.button3.Size = new System.Drawing.Size(218, 39);
        this.button3.TabIndex = 6;
        this.button3.Text = "Step #1 Download Diablo II LOD Torrent";
        this.button3.UseVisualStyleBackColor = true;
        this.button3.Click += new System.EventHandler(this.button3_Click);
        // 
        // label4
        // 
        this.label4.AutoSize = true;
        this.label4.ForeColor = System.Drawing.Color.Red;
        this.label4.Location = new System.Drawing.Point(310, 36);
        this.label4.Name = "label4";
        this.label4.Size = new System.Drawing.Size(62, 13);
        this.label4.TabIndex = 7;
        this.label4.Text = "-> Require: ";
        // 
        // linkLabel1
        // 
        this.linkLabel1.AutoSize = true;
        this.linkLabel1.Location = new System.Drawing.Point(368, 36);
        this.linkLabel1.Name = "linkLabel1";
        this.linkLabel1.Size = new System.Drawing.Size(75, 13);
        this.linkLabel1.TabIndex = 8;
        this.linkLabel1.TabStop = true;
        this.linkLabel1.Text = "UTorrent Web";
        this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
        // 
        // label5
        // 
        this.label5.AutoSize = true;
        this.label5.Location = new System.Drawing.Point(34, 93);
        this.label5.Name = "label5";
        this.label5.Size = new System.Drawing.Size(307, 13);
        this.label5.TabIndex = 9;
        this.label5.Text = "Step#3 - Extract the content of Diablo II.7z to the created folder";
        // 
        // label6
        // 
        this.label6.AutoSize = true;
        this.label6.Location = new System.Drawing.Point(34, 70);
        this.label6.Name = "label6";
        this.label6.Size = new System.Drawing.Size(329, 13);
        this.label6.TabIndex = 10;
        this.label6.Text = "Step#2 - Create a folder on your desktop by exemple called \'Diablo2\'";
        // 
        // linkLabel2
        // 
        this.linkLabel2.AutoSize = true;
        this.linkLabel2.Location = new System.Drawing.Point(401, 93);
        this.linkLabel2.Name = "linkLabel2";
        this.linkLabel2.Size = new System.Drawing.Size(38, 13);
        this.linkLabel2.TabIndex = 12;
        this.linkLabel2.TabStop = true;
        this.linkLabel2.Text = "Winrar";
        this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
        // 
        // label7
        // 
        this.label7.AutoSize = true;
        this.label7.ForeColor = System.Drawing.Color.Red;
        this.label7.Location = new System.Drawing.Point(343, 93);
        this.label7.Name = "label7";
        this.label7.Size = new System.Drawing.Size(62, 13);
        this.label7.TabIndex = 11;
        this.label7.Text = "-> Require: ";
        // 
        // FormD2LOD
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = System.Drawing.SystemColors.ControlDark;
        this.ClientSize = new System.Drawing.Size(458, 300);
        this.Controls.Add(this.linkLabel2);
        this.Controls.Add(this.label7);
        this.Controls.Add(this.label6);
        this.Controls.Add(this.label5);
        this.Controls.Add(this.linkLabel1);
        this.Controls.Add(this.label4);
        this.Controls.Add(this.button3);
        this.Controls.Add(this.label3);
        this.Controls.Add(this.button2);
        this.Controls.Add(this.label2);
        this.Controls.Add(this.textBox1);
        this.Controls.Add(this.label1);
        this.Controls.Add(this.button1);
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
        this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
        this.MaximizeBox = false;
        this.Name = "FormD2LOD";
        this.Text = "D2R - BMBot - D2 LOD 1.13C Tool";
        this.Load += new System.EventHandler(this.FormD2LOD_Load);
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox textBox1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Button button2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Button button3;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.LinkLabel linkLabel1;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.LinkLabel linkLabel2;
    private System.Windows.Forms.Label label7;
}