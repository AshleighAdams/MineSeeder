namespace MineSeed
{
    partial class Form1
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
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.btnEncode = new System.Windows.Forms.Button();
            this.tbCode = new System.Windows.Forms.TextBox();
            this.btnDecode = new System.Windows.Forms.Button();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.cbStarterKit = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "level.dat";
            this.openFileDialog.Filter = "MineCraft Levels|level.dat";
            // 
            // btnEncode
            // 
            this.btnEncode.Location = new System.Drawing.Point(96, 38);
            this.btnEncode.Name = "btnEncode";
            this.btnEncode.Size = new System.Drawing.Size(75, 23);
            this.btnEncode.TabIndex = 0;
            this.btnEncode.Text = "Load";
            this.btnEncode.UseVisualStyleBackColor = true;
            this.btnEncode.Click += new System.EventHandler(this.btnEncode_Click);
            // 
            // tbCode
            // 
            this.tbCode.Location = new System.Drawing.Point(12, 12);
            this.tbCode.Name = "tbCode";
            this.tbCode.Size = new System.Drawing.Size(320, 20);
            this.tbCode.TabIndex = 1;
            this.tbCode.Text = "[80mEdHbV6VDyciBlZ2dsb4bfmp4mdGVyQGRnZ/aJmmHKQ941]";
            // 
            // btnDecode
            // 
            this.btnDecode.Location = new System.Drawing.Point(177, 39);
            this.btnDecode.Name = "btnDecode";
            this.btnDecode.Size = new System.Drawing.Size(75, 23);
            this.btnDecode.TabIndex = 2;
            this.btnDecode.Text = "Save";
            this.btnDecode.UseVisualStyleBackColor = true;
            this.btnDecode.Click += new System.EventHandler(this.btnDecode_Click);
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.FileName = "level.dat";
            this.saveFileDialog.Filter = "MineCraft Levels|level.dat";
            // 
            // cbStarterKit
            // 
            this.cbStarterKit.AutoSize = true;
            this.cbStarterKit.Location = new System.Drawing.Point(258, 43);
            this.cbStarterKit.Name = "cbStarterKit";
            this.cbStarterKit.Size = new System.Drawing.Size(72, 17);
            this.cbStarterKit.TabIndex = 3;
            this.cbStarterKit.Text = "Starter Kit";
            this.cbStarterKit.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(342, 72);
            this.Controls.Add(this.cbStarterKit);
            this.Controls.Add(this.btnDecode);
            this.Controls.Add(this.tbCode);
            this.Controls.Add(this.btnEncode);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Form1";
            this.Text = "MineSeeder";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Button btnEncode;
        private System.Windows.Forms.TextBox tbCode;
        private System.Windows.Forms.Button btnDecode;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.CheckBox cbStarterKit;
    }
}

