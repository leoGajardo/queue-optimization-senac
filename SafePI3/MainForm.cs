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

        QueueManager Manager;
        public bool RunningQueue = false;
        public MainForm()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
        }
        
        private void creditosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Credits CreditsForm = new Credits();
            CreditsForm.Show();
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
                //Reinicia a simulação
                RunningQueue = true;
                PlayButton.BackgroundImage = new Bitmap(Application.StartupPath + "\\Assets\\Images\\Play.png");
                if (Manager.LoadSetupFile())
                {
                    int x = 30;
                    int y = 30;
                    foreach (QueueUC item in Manager.Queues.Select(a => a.Value.QueueUserControl))
                    {
                        item.Location = new Point(x, y);
                        this.Controls.Add(item);
                        x += item.Width + 15;
                    }

                    RunningQueue = true;
                    Manager.StartQueue(Utils.TurnSpeed.Normal);
                    
                }
            }
            else
            {
                //Começa a simulação
                if (Manager.LoadSetupFile())
                {
                    int x = 30;
                    int y = 30;
                    foreach (QueueUC item in Manager.Queues.Select(a => a.Value.QueueUserControl))
                    {
                        item.Location = new Point(x, y);
                        this.Controls.Add(item);
                        x += item.Width + 15;
                    }
                    RunningQueue = true;
                    Manager.StartQueue(Utils.TurnSpeed.Normal);
                    
                }
                
            }
            
            
        }

        private void Faster1Button_Click(object sender, EventArgs e)
        {
            if (!RunningQueue)
            {
                //Começa a simulação
                if (Manager.LoadSetupFile())
                {
                    int x = 30;
                    int y = 30;
                    foreach (QueueUC item in Manager.Queues.Select(a => a.Value.QueueUserControl))
                    {
                        item.Location = new Point(x, y);
                        this.Controls.Add(item);
                        x += item.Width + 15;
                    }
                    RunningQueue = true;
                    Manager.StartQueue(Utils.TurnSpeed.Fast1);

                }
                else
                {
                    Manager.SetSpeed(Utils.TurnSpeed.Fast1);
                }

            }
                
        }

        private void Faster3Button_Click(object sender, EventArgs e)
        {
            if (!RunningQueue)
            {
                //Começa a simulação
                if (Manager.LoadSetupFile())
                {
                    int x = 30;
                    int y = 30;
                    foreach (QueueUC item in Manager.Queues.Select(a => a.Value.QueueUserControl))
                    {
                        item.Location = new Point(x, y);
                        this.Controls.Add(item);
                        x += item.Width + 15;
                    }
                    RunningQueue = true;
                    Manager.StartQueue(Utils.TurnSpeed.Fast2);

                }

            }
            else
            {
                Manager.SetSpeed(Utils.TurnSpeed.Fast2);
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            try
            {
                Manager.Dispose();
                if (Directory.Exists(Application.StartupPath + "//Configs"))
                    Directory.Delete(Application.StartupPath + "//Configs", true);
            }
            catch (Exception)
            {

            }
         
            base.OnClosing(e);
        }

        protected override void OnLoad(EventArgs e)
        {
            try
            {
                if (Directory.Exists(Application.StartupPath + "//Configs"))
                    Directory.Delete(Application.StartupPath + "//Configs", true);
                Manager = new QueueManager(this);
            }
            catch (Exception)
            {

            }

            base.OnLoad(e);
        }

        private void PauseButton_Click(object sender, EventArgs e)
        {
            Manager.PauseQueue();
            RunningQueue = false;
            PlayButton.BackgroundImage = new Bitmap(Application.StartupPath + "\\Assets\\Images\\Restart.png");
        }

        public void UpdateTurn(string _currentTurn)
        {
            CurrentTurn.Text = _currentTurn;
        }
    }
}
