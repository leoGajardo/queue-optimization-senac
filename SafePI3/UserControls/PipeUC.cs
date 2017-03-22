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

    public partial class PipeUC : UserControl
    {
        System.Windows.Forms.ProgressBar ProgressBar { get; set; }
        List<System.Windows.Forms.PictureBox> ServiceDesks { get; set; }
        private int _picHeight;
        private int _picWidth;

        IPipe CurrentPipe;
        public PipeUC(IPipe pipe)
        {
            _picHeight = 50;
            _picWidth = 50;
            InitializeComponent();
            CurrentPipe = pipe;
            this.Width = (CurrentPipe.ServiceDesksQuantity * _picWidth) + 40;

            PipeName.Text = CurrentPipe.Name;
            PipeName.Left = (this.Width - PipeName.Width) / 2;

            InitializeServiceDesks();
            InitializeProgressBar();

        }
        public void UpdatePipe()
        {
            UpdateServiceDesk();
            UpdateProgressBar();
        }
        private void InitializeServiceDesks()
        {
            ServiceDesks = new List<PictureBox>(CurrentPipe.ServiceDesksQuantity);
            for (int i = 0; i < CurrentPipe.ServiceDesksQuantity; i++)
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
            ServiceDesks.Take(CurrentPipe.OperatorsQuantity).ToList().ForEach(a => a.Image = new Bitmap(Application.StartupPath + "\\Assets\\Images\\iconWorkDesk.jpg"));
            ServiceDesks.Skip(CurrentPipe.OperatorsQuantity).ToList().ForEach(a => a.Image = new Bitmap(Application.StartupPath + "\\Assets\\Images\\iconEmptyDesk.png"));
            
        }
        private void InitializeProgressBar()
        {
            ProgressBar = new ProgressBar();
            ProgressBar.Height = 180;
            ProgressBar.Width = 30;
            ProgressBar.Top = 120;
            ProgressBar.Left = (((CurrentPipe.ServiceDesksQuantity * _picWidth) + 40) / 2) - (ProgressBar.Width / 2);
            UpdateProgressBar();
            this.Controls.Add(ProgressBar);
        }
        private void UpdateProgressBar()
        {
            
        }
    }
}
