using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BenzinskaPumpaVb.net
{
    public partial class Ucitavanje : Form
    {
        public Ucitavanje()
        {
            InitializeComponent();
        }
        int pocetak = 0;



        private void Ucitavanje_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            pocetak += 2;
            Procentualno.Text = ProgressBar.Value + "%";
            ProgressBar.Value = pocetak;

            if (ProgressBar.Value == 100)
            {
                ProgressBar.Value = 0;
                timer1.Stop();

                Prijava prijava = new Prijava();
                prijava.Show();
                this.Hide();



            }
        }
    }
}
