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

        public LoadSceneForm()
        {
            InitializeComponent();
        }

        private void formTest_Load(object sender, EventArgs e)
        {
            foreach (var item in Directory.GetFiles(Screen.ScreenManager.instance.game.Content.RootDirectory + @"\Xml\Scenes"))
                listBox1.Items.Add(item);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                pathToScene = listBox1.Items[listBox1.SelectedIndex].ToString();

                Screen.ScreenManager.instance.AddScreen(new Screen.MainScreen(pathToScene));
                Close();
            }
            else
                System.Windows.Forms.MessageBox.Show("Choose scene to load!");
            
        }
    }
}
