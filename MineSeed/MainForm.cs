using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace MineSeed
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            string path = this.MinecraftPath;
            string worlds = path + Path.DirectorySeparatorChar;
            this._Set(this.World1Label, this.World1Button, this.World1Code, worlds + "World1");
            this._Set(this.World2Label, this.World2Button, this.World2Code, worlds + "World2");
            this._Set(this.World3Label, this.World3Button, this.World3Code, worlds + "World3");
            this._Set(this.World4Label, this.World4Button, this.World4Code, worlds + "World4");
            this._Set(this.World5Label, this.World5Button, this.World5Code, worlds + "World5");
        }

        /// <summary>
        /// Sets up a world.
        /// </summary>
        private void _Set(Label Label, Button Button, TextBox TextBox, string WorldPath)
        {
            if (Directory.Exists(WorldPath) && File.Exists(WorldPath + Path.DirectorySeparatorChar + "level.dat"))
            {
                Label.ForeColor = _Active;
                TextBox.ReadOnly = true;
                string code = MineSeeder.Get(WorldPath + Path.DirectorySeparatorChar + "level.dat");
                TextBox.Text = code;
                Button.Text = "Copy";
                Button.Click += delegate
                {
                    Clipboard.SetText(code);
                };
            }
            else
            {
                Label.ForeColor = _Free;
                Button.Text = "Load";
                Button.Click += delegate
                {
                    if (this._Seed(TextBox.Text, WorldPath))
                    {
                        Button.Text = "Okay";
                        Button.Enabled = false;
                        TextBox.Enabled = false;
                    }
                };
            }
        }

        /// <summary>
        /// Loads a code into a file.
        /// </summary>
        private bool _Seed(string Code, string World)
        {
            if (!Directory.Exists(World))
            {
                Directory.CreateDirectory(World);
            }
            if (!MineSeed.MineSeeder.Set(Code, World + Path.DirectorySeparatorChar + "level.dat", this.StarterKit.Checked))
            {
                Directory.Delete(World);
                MessageBox.Show("The code you inputted is not valid... Don't worry, It'll be okay", "Oh No");
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Gets the path to minecraft level files.
        /// </summary>
        public string MinecraftPath
        {
            get
            {
                if (this._MinecraftPath == null)
                {
                    // Look it up in config
                    if (File.Exists("minecraftpath.txt"))
                    {
                        return this._MinecraftPath = File.ReadAllText("minecraftpath.txt");
                    }

                    // Guess it's in appdata
                    this._MinecraftPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + 
                        Path.DirectorySeparatorChar + ".minecraft" + Path.DirectorySeparatorChar + "saves";
                    if (Directory.Exists(this._MinecraftPath))
                    {
                        return this._MinecraftPath;
                    }


                    // No idea yet, ask the user.
                    if (MessageBox.Show(
                        "MineSeeder can't find your minecraft folder (where your saves are stored). Can you give it some help? You'll only " +
                        "need to do this once.", "Oh no", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        FolderBrowserDialog fbd = new FolderBrowserDialog();
                        if (fbd.ShowDialog() == DialogResult.OK)
                        {
                            string folder = fbd.SelectedPath;
                            File.WriteAllText("minecraftpath.txt", folder);
                            return this._MinecraftPath = folder;
                        }
                        else
                        {
                            this.Close();
                        }
                    }
                    else
                    {
                        this.Close();
                    }
                }
                return this._MinecraftPath;
            }
        }

        private void BrowseLabel_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = this.MinecraftPath;
            ofd.Filter = "Minecraft Level|level.dat";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                this._BrowseFile = ofd.FileName;
                this._BrowseCode = MineSeeder.Get(this._BrowseFile);
                this.BrowseCode.Text = this._BrowseCode;
                this.BrowseCode.Enabled = true;
                this.BrowseButton.Enabled = true;
            }
        }

        private void BrowseButton_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(this._BrowseCode);
        }

        private string _BrowseFile;
        private string _BrowseCode;

        private static Color _Active = Color.Red;
        private static Color _Free = Color.Green;

        private string _MinecraftPath;

    }
}
