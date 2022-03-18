using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BenzinskaPumpaVb.net
{
    public partial class Prijava : Form
    {
        SqlConnection veza = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='E:\Maturski rad\BenzinskaPumpaVb.net\BenzinskaPumbaDbVb.mdf';Integrated Security=True;Connect Timeout=30");
        public Prijava()
        {
            InitializeComponent();
        }
        public static string Ime_Uposlenika = "";
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label4_Click(object sender, EventArgs e)
        {
          AdminPrijava ap=new AdminPrijava();
            ap.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            
                veza.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("select count(*) from Uposlenik_Tabela where Ime_Uposlenika='"+textBox_ime.Text+"'and Sifra_Uposlenika='"+textBox_lozinka.Text+"'",veza);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            if (dt.Rows[0][0].ToString() == "1")
            {
                Ime_Uposlenika = textBox_ime.Text;
                Naplata naplata=new Naplata();
                naplata.Show();
                this.Hide();

               
            }
            else
            {
                MessageBox.Show("Pogrešno Korisničko ime ili Lozinka");
            }

            veza.Close();   




        }

        private void label4_MouseEnter(object sender, EventArgs e)
        {
            label4.ForeColor = Color.DeepPink;
        }

      

        private void label4_MouseLeave(object sender, EventArgs e)
        {
            label4.ForeColor = Color.Crimson;
        }
    }
}
