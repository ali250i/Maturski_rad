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
    public partial class Kategorije : Form
    {
        SqlConnection veza = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='E:\Maturski rad\BenzinskaPumpaVb.net\BenzinskaPumbaDbVb.mdf';Integrated Security=True;Connect Timeout=30");
        public Kategorije()
        {
            InitializeComponent();
        }

        private void Ocistibtn_Click(object sender, EventArgs e)
        {
            NazivKategorijetb.Clear();
            OpisKategorijetb.Clear();
        }
        private void PrikaziUposlenike()
        {
            veza.Open();
            string SelectQuery = "SELECT * FROM Kategorija_Tabela";

            SqlDataAdapter adapter = new SqlDataAdapter(SelectQuery, veza);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);

            DataSet ds = new DataSet();
            adapter.Fill(ds);
            PrikazKategorija.DataSource = ds.Tables[0];




            veza.Close();
        }
        private void Izbrisi()
        {
            NazivKategorijetb.Clear();
            OpisKategorijetb.Clear();
            //kljuc=0;
        }

        private void Sacuvajbtn_Click(object sender, EventArgs e)
        {
            if (NazivKategorijetb.Text == "" || OpisKategorijetb.Text == "")
                MessageBox.Show("Nedostaju informacije");
            else
            {
                try
                {

                    veza.Open();

                    SqlCommand komanda = new SqlCommand("INSERT INTO Kategorija_Tabela VALUES('" + NazivKategorijetb.Text + "','" + OpisKategorijetb.Text +  "')", veza);
                    komanda.ExecuteNonQuery();
                    MessageBox.Show("Uspješno ste sačuvali kategoriju");

                    veza.Close();
                    PrikaziUposlenike();
                    Izbrisi();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Kategorije_Load(object sender, EventArgs e)
        {
            PrikaziUposlenike();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit(); 
        }

        private void Obrisibtn_Click(object sender, EventArgs e)
        {
            if (kljuc == 0)
                MessageBox.Show("Označite kategoriju koju želite obrisati");
            else
            {
                veza.Open();
                string query = "DELETE FROM Kategorija_Tabela WHERE Id_Kategorije=" + kljuc + ";";
                SqlCommand komanda = new SqlCommand(query, veza);
                komanda.ExecuteNonQuery();
                MessageBox.Show("Uspješno ste obrisali Kategoriju");

                veza.Close();
                PrikaziUposlenike();
                Izbrisi();


            }
        }
        int kljuc;
        private void PrikazKategorija_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            NazivKategorijetb.Text = PrikazKategorija.SelectedRows[0].Cells[1].Value.ToString();
            OpisKategorijetb.Text = PrikazKategorija.SelectedRows[0].Cells[2].Value.ToString();

            
            if (NazivKategorijetb.Text == "")
                kljuc = 0;
            else
                kljuc = Convert.ToInt32(PrikazKategorija.SelectedRows[0].Cells[0].Value);
        }

        private void Uredibtn_Click(object sender, EventArgs e)
        {
            if (NazivKategorijetb.Text == "" || OpisKategorijetb.Text == "")
                MessageBox.Show("Označite uposlenika kojeg želite uređivati");
            else
            {
                try
                {
                    veza.Open();
                    string query = "UPDATE Kategorija_Tabela SET Naziv_Kategorije='" + NazivKategorijetb.Text + "',Opis_Kategorije='" + OpisKategorijetb.Text +   "'where Id_Kategorije=" + kljuc + ";";
                    SqlCommand komanda = new SqlCommand(query, veza);
                    komanda.ExecuteNonQuery();
                    MessageBox.Show("Uspješno ste uredili kategoriju");

                    veza.Close();
                    PrikaziUposlenike();
                    Izbrisi();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }


            }
        }

        private void Odjavalbl_Click(object sender, EventArgs e)
        {
            Prijava prijava = new Prijava();    
            prijava.Show();
            this.Hide();
        }

        private void Odjavalbl_MouseEnter(object sender, EventArgs e)
        {
            Odjavalbl.ForeColor = Color.DeepPink;
        }

        private void Odjavalbl_MouseLeave(object sender, EventArgs e)
        {
            Odjavalbl.ForeColor = Color.Crimson;    
        }

        private void Proizvodilbl_Click(object sender, EventArgs e)
        {
            Proizvodi proiz=new Proizvodi();
            proiz.Show();
            this.Hide();
        }

        private void ProizvodiPB_Click(object sender, EventArgs e)
        {
            Proizvodi proiz = new Proizvodi();
            proiz.Show();
            this.Hide();

        }

        private void Uposlenicilbl_Click(object sender, EventArgs e)
        {
            Uposlenici up = new Uposlenici();
            up.Show();
            this.Hide();
        }

        private void UposleniciPB_Click(object sender, EventArgs e)
        {
            Uposlenici up = new Uposlenici();
            up.Show();
            this.Hide();
        }

       

        private void Proizvodilbl_MouseEnter(object sender, EventArgs e)
        {
            crta.Location = new Point(567, 152);
        }

        private void Proizvodilbl_MouseLeave(object sender, EventArgs e)
        {
            crta.Location = new Point(713, 152);
        }

        private void ProizvodiPB_MouseEnter(object sender, EventArgs e)
        {
            crta.Location = new Point(567, 152);
        }

        private void ProizvodiPB_MouseLeave(object sender, EventArgs e)
        {
            crta.Location = new Point(713, 152);
        }

        private void Uposlenicilbl_MouseEnter(object sender, EventArgs e)
        {
            crta.Location = new Point(409, 152);
        }

        private void Uposlenicilbl_MouseLeave(object sender, EventArgs e)
        {
            crta.Location = new Point(713, 152);
        }

        private void UposleniciPB_MouseEnter(object sender, EventArgs e)
        {
            crta.Location = new Point(409, 152);
        }

        private void UposleniciPB_MouseLeave(object sender, EventArgs e)
        {
            crta.Location = new Point(713, 152);
        }
    }
}
