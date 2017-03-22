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
        
        public Dictionary<string, Queue> Queues;
        public MainForm()
        {
            Queues = new Dictionary<string, Queue>();
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
            }

        }

        private void arquivoFilaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string FileName = "";
            FileName = Utils.Utils.PickFile("txt files (*.txt) | *.txt", "Selecionar arquivo de Fila");
            if (!String.IsNullOrWhiteSpace(FileName))
            {
                Utils.Utils.CopyFile("Queue.txt", FileName, Application.StartupPath + "\\Configs\\");
            }
        }

        private void PlayButton_Click(object sender, EventArgs e)
        {

        }

        private void Faster1Button_Click(object sender, EventArgs e)
        {

        }

        private void Faster3Button_Click(object sender, EventArgs e)
        {

        }
    }
}
