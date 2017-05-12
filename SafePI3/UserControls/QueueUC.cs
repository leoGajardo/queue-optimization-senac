using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SafePI3.Interfaces;

namespace SafePI3.UserControls
{

    public partial class QueueUC : UserControl
    {
        System.Windows.Forms.ProgressBar ProgressBar { get; set; }
        List<System.Windows.Forms.PictureBox> ServiceDesks { get; set; }
        private int _picHeight;
        private int _picWidth;

        IQueue CurrentQueue;
        public QueueUC(IQueue queue)
        {
            _picHeight = 50;
            _picWidth = 50;
            InitializeComponent();
            CurrentQueue = queue;
            this.Width = (CurrentQueue.ServiceDesksQuantity * _picWidth) + 40;

            QueueName.Text = Encoding.UTF8.GetString(Encoding.UTF8.GetBytes(CurrentQueue.Label)) ?? CurrentQueue.Name;
            QueueName.Left = (this.Width - QueueName.Width) / 2;

            InitializeServiceDesks();
            InitializeProgressBar();

        }
        public void UpdateQueue()
        {
            UpdateServiceDesk();
            UpdateProgressBar();
        }
        private void InitializeServiceDesks()
        {
            ServiceDesks = new List<PictureBox>(CurrentQueue.ServiceDesksQuantity);
            for (int i = 0; i < CurrentQueue.ServiceDesksQuantity; i++)
            {
                ServiceDesks.Add(new PictureBox()
                {
                    Location = new Point((i * (_picWidth + 2)) + 20, 40),
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Width = _picWidth,
                    Height = _picHeight
                });
            }            
            UpdateServiceDesk();
            ServiceDesks.ForEach(a => this.Controls.Add(a));
        }
        private void UpdateServiceDesk()
        {
            ServiceDesks.Take(CurrentQueue.OperatorsQuantity).ToList().ForEach(a => a.Image = new Bitmap(Application.StartupPath + "\\Assets\\Images\\iconWorkDesk.jpg"));
            ServiceDesks.Skip(CurrentQueue.OperatorsQuantity).ToList().ForEach(a => a.Image = new Bitmap(Application.StartupPath + "\\Assets\\Images\\iconEmptyDesk.png"));
            
        }
        private void InitializeProgressBar()
        {
            ProgressBar = new ProgressBar();
            ProgressBar.Height = 180;
            ProgressBar.Width = 30;
            ProgressBar.Top = 120;
            ProgressBar.Left = (((CurrentQueue.ServiceDesksQuantity * _picWidth) + 40) / 2) - (ProgressBar.Width / 2);
            ProgressBar.Maximum = 200;
            ProgressBar.Step = 1;
            ProgressBar.Value = 0;

            
                        
            ProgressNumber.Top = 330;
            ProgressNumber.Left = (((CurrentQueue.ServiceDesksQuantity * _picWidth) + 40) / 2) - (ProgressBar.Width / 2);
            ProgressNumber.Text = "0";

            UpdateProgressBar();
            this.Controls.Add(ProgressBar);
        }
        private void UpdateProgressBar()
        {
            ProgressBar.Value = CurrentQueue.Clients.Count();
            ProgressNumber.Text = CurrentQueue.Clients.Count().ToString();
        }
    }
}
