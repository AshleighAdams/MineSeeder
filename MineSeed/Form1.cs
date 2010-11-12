﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MineSeed
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            

        }

        private void btnEncode_Click(object sender, EventArgs e)
        {
            openFileDialog.ShowDialog();
            string encoded = MineSeeder.Get(openFileDialog.FileName);
            tbCode.Text = encoded;
        }

        private void btnDecode_Click(object sender, EventArgs e)
        {
            MineSeeder.Set(tbCode.Text, "C:\\Users\\C0BRA\\AppData\\Roaming\\.minecraft\\saves\\World4\\level.dat");
        }
    }
}