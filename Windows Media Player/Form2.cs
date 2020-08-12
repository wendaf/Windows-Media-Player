using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Windows_Media_Player
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void ouvrirMusiqueVideoToolStripMenuItem_Click(object sender, EventArgs e)
        {
        openFileDialog1.Filter = "(mp3,wav,mp4,mov,wmv,mpg)|*.mp3;*.wav;*.mp4;*.mov;*.wmv;*.mpg|all files|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.Hide();
                Form1 f1 = new Form1();
                f1.AxWindowsMediaPlayer1.URL = openFileDialog1.FileName;
                f1.Show();
            }
                
        }

        private void ouvrirUnDossierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                string folderPath = folderBrowserDialog1.SelectedPath;

                string supportedExtensions = "*.mp3,*.wav,*.mp4,*.mov,*.wmv,*.mpg";
                foreach (string mediaPaths in Directory.GetFiles(folderPath, "*.*", SearchOption.AllDirectories).Where(s => supportedExtensions.Contains(Path.GetExtension(s).ToLower())))
                {
                    Global._media.Add(mediaPaths);
                }
                this.Hide();
                Form1 f1 = new Form1();
                /// create playlist
                f1.AxWindowsMediaPlayer1.currentPlaylist = f1.AxWindowsMediaPlayer1.newPlaylist("default", "");
                foreach (string fn in Global._media)
                {   ////add playlist from the selected files by the OpenFileDialog
                    f1.AxWindowsMediaPlayer1.currentPlaylist.appendItem(f1.AxWindowsMediaPlayer1.newMedia(fn));
                }
                f1.AxWindowsMediaPlayer1.Ctlcontrols.play();////play
                f1.Show();
            }
        }

        private void quitterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (System.Windows.Forms.Application.MessageLoop)
            {
                // WinForms app
                System.Windows.Forms.Application.Exit();
            }
            else
            {
                // Console app
                System.Environment.Exit(1);
            }
        }
    }
}
