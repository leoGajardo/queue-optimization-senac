using SafePI3.Classes;
using SafePI3.UserControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//Ideia para a versão web a pessoa conseguir pegar as musicas do youtube, mas deixar uma playlist padrao caso nao houver internet para isso
//botao para ativar e desativar as vozes das pessoas


namespace SafePI3
{
    public partial class MainForm : Form
    {

        QueueManager Manager = new QueueManager();
        bool RunningQueue = false;
        public MainForm()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();

            //Queue _queue = new Queue("Teste", "A", 3, 1, 3, new List<Client>());
            //_queue.QueueUserControl.Location = new Point(30, 30);
            //this.Controls.Add(_queue.QueueUserControl);
            //Queues.Add(_queue.Name,_queue);
 
           
        }


        private void creditosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Credits CreditsForm = new Credits();
            CreditsForm.Show();
        }

        private void configuracoesToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void arquivoSetupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string FileName = "";
            FileName = Utils.Utils.PickFile("txt files (*.txt) | *.txt", "Selecionar arquivo de Setup");
            if (!String.IsNullOrWhiteSpace(FileName))
            {
                Utils.Utils.CopyFile("Setup.txt", FileName, Application.StartupPath + "\\Configs\\");
                if (!Utils.Utils.ValidateSetupFile())
                    File.Delete(Application.StartupPath + "\\Configs\\Setup.txt");
                else
                    MessageBox.Show("Arquivo Setup carregado com sucesso", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void arquivoFilaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string FileName = "";
            FileName = Utils.Utils.PickFile("txt files (*.txt) | *.txt", "Selecionar arquivo de Fila");
            if (!String.IsNullOrWhiteSpace(FileName))
            {
                Utils.Utils.CopyFile("Queue.txt", FileName, Application.StartupPath + "\\Configs\\");
                if (!Utils.Utils.ValidateQueueFile())
                    File.Delete(Application.StartupPath + "\\Configs\\Fila.txt");
                else
                    MessageBox.Show("Arquivo Setup carregado com sucesso", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void PlayButton_Click(object sender, EventArgs e)
        {
            if (RunningQueue)
            {
                RunningQueue = false;
                PlayButton.BackgroundImage = new Bitmap(Application.StartupPath + "\\Assets\\Images\\Play.png");
            }
            else
            {
                RunningQueue = true;
                PlayButton.BackgroundImage = new Bitmap(Application.StartupPath + "\\Assets\\Images\\Stop.png");
            }
            
            
        }

        private void Faster1Button_Click(object sender, EventArgs e)
        {

        }

        private void Faster3Button_Click(object sender, EventArgs e)
        {

        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (Directory.Exists(Application.StartupPath + "//Configs"))
                Directory.Delete(Application.StartupPath + "//Configs", true);
            base.OnClosing(e);
        }

        protected override void OnLoad(EventArgs e)
        {
            if(Directory.Exists(Application.StartupPath + "//Configs"))
                Directory.Delete(Application.StartupPath + "//Configs", true);
            base.OnLoad(e);
        }
    }
}
