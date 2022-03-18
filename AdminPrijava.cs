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
    public partial class AdminPrijava : Form
    {
        public AdminPrijava()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox_lozinka.Text == "")
                MessageBox.Show("Upišite lozinku");
            else if (textBox_lozinka.Text == "1234")
            {
                Uposlenici up = new Uposlenici();
                up.Show();
                this.Hide();
            }
            else
                MessageBox.Show("Pogrešna lozinka administratora");
        }
    }
}
