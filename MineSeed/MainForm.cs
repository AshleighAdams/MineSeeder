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

            this._ButtonActions = new Action[5];
            this._Buttons = new Button[5];
            this._TextBoxes = new TextBox[5];
            this._WorldPaths = new string[5];
            this._Labels = new Label[5];

            this._Set(this.World1Label, this.World1Button, this.World1Code, worlds + "World1", 0);
            this._Set(this.World2Label, this.World2Button, this.World2Code, worlds + "World2", 1);
            this._Set(this.World3Label, this.World3Button, this.World3Code, worlds + "World3", 2);
            this._Set(this.World4Label, this.World4Button, this.World4Code, worlds + "World4", 3);
            this._Set(this.World5Label, this.World5Button, this.World5Code, worlds + "World5", 4);
        }

        /// <summary>
        /// Sets up a world.
        /// </summary>
        private void _Set(Label Label, Button Button, TextBox TextBox, string WorldPath, int Index)
        {
            this._Labels[Index] = Label;
            this._TextBoxes[Index] = TextBox;
            this._WorldPaths[Index] = WorldPath;
            this._Buttons[Index] = Button;
            Button.Click += delegate
            {
                this._ButtonActions[Index]();
            };
            this._Update(Index);
        }

        /// <summary>
        /// Updates information for the world at the index.
        /// </summary>
        private void _Update(int Index)
        {
            
            string path = this._WorldPaths[Index];
            TextBox textbox = this._TextBoxes[Index];
            Label label = this._Labels[Index];
            Button button = this._Buttons[Index];
            button.Enabled = true;
            textbox.Enabled = true;
            if (Directory.Exists(path) && File.Exists(path + Path.DirectorySeparatorChar + "level.dat"))
            {
                label.ForeColor = _Active;
                textbox.ReadOnly = true;
                string code = MineSeeder.Get(path + Path.DirectorySeparatorChar + "level.dat", cbRealSpawn.Checked);
                textbox.Text = code;
                button.Text = "Copy";
                this._ButtonActions[Index] = delegate
                {
                    _SetClipboard(code);
                };
            }
            else
            {
                textbox.ReadOnly = false;
                textbox.Text = "";
                label.ForeColor = _Free;
                button.Text = "Plant";
                this._ButtonActions[Index] = delegate
                {
                    if (this._Seed(textbox.Text, path))
                    {
                        button.Text = "Okay";
                        button.Enabled = false;
                        textbox.Enabled = false;
                    }
                };
            }
        }

        /// <summary>
        /// Sets the contents of the clipboard.
        /// </summary>
        private static void _SetClipboard(string Text)
        {
            try
            {
                Clipboard.SetText(Text);
            }
            catch
            {

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
            if (!MineSeed.MineSeeder.Set(Code, World + Path.DirectorySeparatorChar + "level.dat", this.StarterKit.Checked, cbSunRise.Checked))
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
                this._BrowseCode = MineSeeder.Get(this._BrowseFile, cbRealSpawn.Checked);
                this.BrowseCode.Text = this._BrowseCode;
                this.BrowseCode.Enabled = true;
                this.BrowseButton.Enabled = true;
            }
        }

        private void BrowseButton_Click(object sender, EventArgs e)
        {
            _SetClipboard(this._BrowseCode);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 5; i++)
            {
                _Update(i);
            }
        }

        private Action[] _ButtonActions;
        private string[] _WorldPaths;
        private TextBox[] _TextBoxes;
        private Label[] _Labels;
        private Button[] _Buttons;

        private string _BrowseFile;
        private string _BrowseCode;

        private static Color _Active = Color.Red;
        private static Color _Free = Color.Green;

        private string _MinecraftPath;

        private void cbRealSpawn_CheckedChanged(object sender, EventArgs e)
        {
            btnRefresh_Click(sender, e);
        }
    }
}
