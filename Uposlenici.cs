using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace BenzinskaPumpaVb.net
{
    public partial class Uposlenici : Form
    {
        SqlConnection veza = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='E:\Maturski rad\BenzinskaPumpaVb.net\BenzinskaPumbaDbVb.mdf';Integrated Security=True;Connect Timeout=30");
        public Uposlenici()
        {
            InitializeComponent();
            PrikaziUposlenike();
        }


        private void PrikaziUposlenike()
        {
            veza.Open();
            string SelectQuery = "SELECT * FROM Uposlenik_Tabela";

            SqlDataAdapter adapter = new SqlDataAdapter(SelectQuery, veza);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);

            DataSet ds = new DataSet();
            adapter.Fill(ds);
            PrikazUposlenika.DataSource = ds.Tables[0];




            veza.Close();
        }

        private void Sacuvajbtn_Click(object sender, EventArgs e)
        {
            if (ImeUpotb.Text == "" || BrojTeltb.Text == "" || Adresatb.Text == "" || SifraUpotb.Text == "")
                MessageBox.Show("Nedostaju informacije");
            else
            {
                try
                {

                    veza.Open();

                    SqlCommand komanda = new SqlCommand("INSERT INTO Uposlenik_Tabela VALUES('" + ImeUpotb.Text + "','" + BrojTeltb.Text + "','" + Adresatb.Text + "','" + SifraUpotb.Text + "')", veza);
                    komanda.ExecuteNonQuery();
                    MessageBox.Show("Uspješno ste sačuvali uposlenika");

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

        private void Izbrisi()
        {
            ImeUpotb.Clear();
            BrojTeltb.Clear();
            Adresatb.Clear();
            SifraUpotb.Clear();
            kljuc = 0;
        }

        private void Ocistibtn_Click(object sender, EventArgs e)
        {
            Izbrisi();
        }
        int kljuc;
        private void PrikazUposlenika_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            ImeUpotb.Text = PrikazUposlenika.SelectedRows[0].Cells[1].Value.ToString();
            BrojTeltb.Text = PrikazUposlenika.SelectedRows[0].Cells[2].Value.ToString();

            Adresatb.Text = PrikazUposlenika.SelectedRows[0].Cells[3].Value.ToString().Replace(",", ".");
            SifraUpotb.Text = PrikazUposlenika.SelectedRows[0].Cells[4].Value.ToString();
            if (ImeUpotb.Text == "")
                kljuc = 0;
            else
                kljuc = Convert.ToInt32(PrikazUposlenika.SelectedRows[0].Cells[0].Value);
        }

        private void Obrisibtn_Click(object sender, EventArgs e)
        {
            if (kljuc == 0)
                MessageBox.Show("Oznacite uposlenika kojeg želite obrisati");
            else
            {
                veza.Open();
                string query = "DELETE FROM Uposlenik_Tabela WHERE Id_Uposlenika=" + kljuc + ";";
                SqlCommand komanda = new SqlCommand(query, veza);
                komanda.ExecuteNonQuery();
                MessageBox.Show("Uspješno ste obrisali uposlenika");

                veza.Close();
                PrikaziUposlenike();
                Izbrisi();


            }
        }

        private void Uredibtn_Click(object sender, EventArgs e)
        {
            if (ImeUpotb.Text == "" || BrojTeltb.Text == "" || Adresatb.Text == "" || SifraUpotb.Text == "")
                MessageBox.Show("Označite uposlenika kojeg želite uređivati");
            else
            {
                try
                {
                    veza.Open();
                    string query = "UPDATE Uposlenik_Tabela SET Ime_Uposlenika='" + ImeUpotb.Text + "',Telefon_Uposlenika=" + BrojTeltb.Text + ",Adresa_Uposlenika='" + Adresatb.Text + "',Sifra_Uposlenika='" + SifraUpotb.Text + "'where Id_Uposlenika=" + kljuc + ";";
                    SqlCommand komanda = new SqlCommand(query, veza);
                    komanda.ExecuteNonQuery();
                    MessageBox.Show("Uspješno ste uredili proizvod");

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

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        

        private void proizvodilbl_MouseEnter(object sender, EventArgs e)
        {
            crta.Location = new Point(609, 152);
        }

        private void proizvodilbl_MouseLeave(object sender, EventArgs e)
        {
            crta.Location = new Point(448,152);
        }

        private void proizvodilbl_Click(object sender, EventArgs e)
        {
            Proizvodi proizvodi = new Proizvodi();
            proizvodi.Show();
            this.Hide();
        }

        private void Kategorijelbl_Click(object sender, EventArgs e)
        {
            Kategorije kat = new Kategorije();
            kat.Show();
            this.Hide();
        }

        private void KategorijePB_Click(object sender, EventArgs e)
        {
            Kategorije kat = new Kategorije();
            kat.Show();
            this.Hide();
        }

        private void Proizvodilbl_Click_1(object sender, EventArgs e)
        {
            Proizvodi proiz = new Proizvodi();
            proiz.Show();
            this.Hide();
        }

        private void ProizvodiPB_Click(object sender, EventArgs e)
        {
            Proizvodi proiz = new Proizvodi();
            proiz.Show();
            this.Hide();
        }

        private void Proizvodilbl_MouseEnter_1(object sender, EventArgs e)
        {
            crta.Location = new Point(557, 152);
        }

        private void Proizvodilbl_MouseLeave_1(object sender, EventArgs e)
        {
            crta.Location = new Point(396, 152);
        }

        private void ProizvodiPB_MouseEnter(object sender, EventArgs e)
        {
            crta.Location = new Point(557, 152);
        }

        private void ProizvodiPB_MouseLeave(object sender, EventArgs e)
        {
            crta.Location = new Point(396, 152);
        }

        private void Kategorijelbl_MouseEnter(object sender, EventArgs e)
        {
            crta.Location = new Point(702, 152);
        }

        private void Kategorijelbl_MouseLeave(object sender, EventArgs e)
        {
            crta.Location = new Point(396, 152);
        }

        private void KategorijePB_MouseEnter(object sender, EventArgs e)
        {
            crta.Location = new Point(702, 152);

        }

        private void KategorijePB_MouseLeave(object sender, EventArgs e)
        {
            crta.Location = new Point(396, 152);
        }
    }
}