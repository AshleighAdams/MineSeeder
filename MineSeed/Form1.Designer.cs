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
            this.SuspendLayout();
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "level.dat";
            this.openFileDialog.Filter = "MineCraft Levels|level.dat";
            // 
            // btnEncode
            // 
            this.btnEncode.Location = new System.Drawing.Point(213, 38);
            this.btnEncode.Name = "btnEncode";
            this.btnEncode.Size = new System.Drawing.Size(75, 23);
            this.btnEncode.TabIndex = 0;
            this.btnEncode.Text = "Encode";
            this.btnEncode.UseVisualStyleBackColor = true;
            this.btnEncode.Click += new System.EventHandler(this.btnEncode_Click);
            // 
            // tbCode
            // 
            this.tbCode.Location = new System.Drawing.Point(12, 12);
            this.tbCode.Name = "tbCode";
            this.tbCode.Size = new System.Drawing.Size(357, 20);
            this.tbCode.TabIndex = 1;
            this.tbCode.Text = "[Y3NT5/qeKVa1twJlZ2dsb+0gZWEzdGVy4pqYmKUalmSzDVU2]";
            // 
            // btnDecode
            // 
            this.btnDecode.Location = new System.Drawing.Point(294, 38);
            this.btnDecode.Name = "btnDecode";
            this.btnDecode.Size = new System.Drawing.Size(75, 23);
            this.btnDecode.TabIndex = 2;
            this.btnDecode.Text = "Decode";
            this.btnDecode.UseVisualStyleBackColor = true;
            this.btnDecode.Click += new System.EventHandler(this.btnDecode_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(381, 72);
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
    }
}

