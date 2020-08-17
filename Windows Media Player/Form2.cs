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

        //fonction pour recuperer les noms de fichiers
        private void Form2_Load(object sender, EventArgs e)
        {
            listBox1.ValueMember = "Path";
            listBox1.DisplayMember = "FileName";
        }

        private void ouvrirMusiqueVideoToolStripMenuItem_Click(object sender, EventArgs e)
        {
        openFileDialog1.Filter = "(mp3,wav,mp4,mov,wmv,mpg)|*.mp3;*.wav;*.mp4;*.mov;*.wmv;*.mpg|all files|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                List<Global> media = new List<Global>();
                axWindowsMediaPlayer1.URL = openFileDialog1.FileName;
                string name = Path.GetFileNameWithoutExtension(openFileDialog1.FileName);
                media.Add(new Global() { FileName = name, Path = openFileDialog1.FileName });
                listBox1.DataSource = media;
            }
                
        }

        private void ouvrirUnDossierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            var pl = axWindowsMediaPlayer1.playlistCollection.newPlaylist("default");
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                string folderPath = folderBrowserDialog1.SelectedPath;

                string supportedExtensions = "*.mp3,*.wav,*.mp4,*.mov,*.wmv,*.mpg,*.png,*jpeg,*.jpg";
                List<Global> media = new List<Global>();
                
                foreach (string mediaPaths in Directory.GetFiles(folderPath, "*.*", SearchOption.AllDirectories).Where(s => supportedExtensions.Contains(Path.GetExtension(s).ToLower())))
                {
                    pl.appendItem(axWindowsMediaPlayer1.newMedia(@mediaPaths));

                    string name = Path.GetFileNameWithoutExtension(mediaPaths);
                    media.Add(new Global() { FileName = name, Path = mediaPaths });

                }
                listBox1.DataSource = media;
            }
            axWindowsMediaPlayer1.currentPlaylist = pl;
            axWindowsMediaPlayer1.Ctlcontrols.play();
        }

        //fonction pour l'affichage de la playlist crée par l'utilisateur
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Open media file
            Global file = listBox1.SelectedItem as Global;
            if (file != null)
            {
                axWindowsMediaPlayer1.URL = file.Path;
                axWindowsMediaPlayer1.Ctlcontrols.play();
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
