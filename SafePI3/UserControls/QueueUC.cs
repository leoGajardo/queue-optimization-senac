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

            QueueName.Text = CurrentQueue.Name;
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
            UpdateProgressBar();
            this.Controls.Add(ProgressBar);
        }
        private void UpdateProgressBar()
        {
            
        }
    }
}
