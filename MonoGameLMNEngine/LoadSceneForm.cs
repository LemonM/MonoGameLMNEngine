using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using LemonParticlesSystem.Screen;
using System.Threading.Tasks;

namespace LemonParticlesSystem
{
    public partial class LoadSceneForm : Form
    {

        public string pathToScene { get; set; }

        System.Collections.Generic.Dictionary<string, string> scenesDictionary;

        public LoadSceneForm()
        {
            InitializeComponent();
            scenesDictionary = new Dictionary<string, string>();
        }

        private void formTest_Load(object sender, EventArgs e)
        {
            //listBox1.Items.Clear();
            foreach (var item in Directory.GetFiles(Screen.ScreenManager.instance.game.Content.RootDirectory + @"\Xml\Scenes"))
            {
                string name = Path.GetFileName("C:\\" + item);
                string path = item;
                scenesDictionary.Add(name, path);
                this.BeginInvoke((Action)(() => listBox1.Items.Add(name)));

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                pathToScene = scenesDictionary[listBox1.Items[listBox1.SelectedIndex].ToString()];

                Screen.ScreenManager.instance.AddScreen(new Screen.MainScreen(pathToScene));
                Close();
            }
            else
                System.Windows.Forms.MessageBox.Show("Choose scene to load!");
            
        }

        private void LoadSceneForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            listBox1.Items.Clear();
        }

        private void LoadSceneForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            listBox1.Items.Clear();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
