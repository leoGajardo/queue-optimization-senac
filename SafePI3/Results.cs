using SafePI3.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SafePI3
{
    public partial class Results : Form
    {
        public Results()
        {
            InitializeComponent();
        }


        public void LoadResults(double mediumTimeSystem, Dictionary<string, double> mediumPerQueue, Client MostWaitUser, Dictionary<string, double> mediumPerCombination, int TotalTurns)
        {
            Turns.Text = TotalTurns.ToString();
            ClientMostID.Text = MostWaitUser.ID.ToString();
            ClientMostTurn.Text = MostWaitUser.TurnsInSystem.ToString();
            AverageTimeInSystem.Text = mediumTimeSystem.ToString();
            int apx = 324;
            int apy = 140;

            int bpx = 471;
            int bpy = 140;
            
            foreach (var item in mediumPerQueue)
            {
                Label a = new Label();
                a.Name = item.Key + "Label";
                a.Text = item.Key;
                a.Location = new Point(apx, apy);

                Label b = new Label();
                b.Name = item.Key + "Time";
                b.Text = item.Value.ToString();
                b.Location = new Point(bpx, bpy);

                this.Controls.Add(a);
                this.Controls.Add(b);

                apy += 35;
                bpy += 35;
            }

            apx = 13;
            apy = 140;
            
            bpx = 118;
            bpy = 140;

            this.Height += mediumPerCombination.Count * 45;


            foreach (var item in mediumPerCombination)
            {
                Label a = new Label();
                a.Name = item.Key + "Label";
                a.Text = item.Key;
                a.Location = new Point(apx, apy);

                Label b = new Label();
                b.Name = item.Key + "Time";
                b.Text = item.Value.ToString();
                b.Location = new Point(bpx, bpy);

                this.Controls.Add(a);
                this.Controls.Add(b);

                apy += 35;
                bpy += 35;
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

