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
    public partial class Proizvodi : Form
    {
        SqlConnection veza = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='E:\Maturski rad\BenzinskaPumpaVb.net\BenzinskaPumbaDbVb.mdf';Integrated Security=True;Connect Timeout=30");
        public Proizvodi()
        {
            InitializeComponent();
        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (ImeProiztb.Text == "" || Kolicinatb.Text == "" || Cijenatb.Text == "" || Kategorijecb.SelectedIndex==-1)
                MessageBox.Show("Nedostaju informacije");
            else
            {
                try
                {
                    
                    veza.Open();
                    string insertQuery = "INSERT INTO Proizvod_Tabela VALUES('" + ImeProiztb.Text + "'," + Kolicinatb.Text + "," + Cijenatb.Text + ",'" + Kategorijecb.SelectedValue.ToString() + "')";
                    SqlCommand komanda = new SqlCommand(insertQuery, veza);
                    komanda.ExecuteNonQuery();
                    MessageBox.Show("Uspješno ste sačuvali proizvod");

                    veza.Close();
                    PrikaziProizvod();
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
            ImeProiztb.Clear();
            Kolicinatb.Clear();
            Cijenatb.Clear();
            Kategorijecb.Text="";
        }
        private void button3_Click(object sender, EventArgs e)
        {
            Izbrisi();

        }

        private void PrikaziProizvod()
        {
            veza.Open();
            string SelectQuery = "SELECT * FROM Proizvod_Tabela";
            SqlCommand komanda = new SqlCommand(SelectQuery, veza);
            SqlDataAdapter adapter = new SqlDataAdapter(komanda);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            PrikazProizvoda.DataSource = ds.Tables[0];




            veza.Close();
        }
        private void Filtriraj()
        {
            veza.Open();
            string SelectQuery = "SELECT * FROM Proizvod_Tabela WHERE Kategorija_Proizvoda='" +PretragaCb.SelectedItem.ToString() + "'";
            SqlCommand komanda = new SqlCommand(SelectQuery, veza);
            SqlDataAdapter adapter = new SqlDataAdapter(komanda);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            PrikazProizvoda.DataSource = ds.Tables[0];




            veza.Close();
        }
        int kljuc = 0;
        private void PrikazProizvoda_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewRow red = new DataGridViewRow();
            red = PrikazProizvoda.Rows[e.RowIndex];
            ImeProiztb.Text = PrikazProizvoda.SelectedRows[0].Cells[1].Value.ToString();
            Kolicinatb.Text = PrikazProizvoda.SelectedRows[0].Cells[2].Value.ToString();
            
            Cijenatb.Text = PrikazProizvoda.SelectedRows[0].Cells[3].Value.ToString().Replace(",", ".");
            Kategorijecb.Text = "Označite kategoriju";//PrikazProizvoda.SelectedRows[0].Cells[4].Value.ToString();

            if (ImeProiztb.Text == "")
                kljuc = 0;
            else
                kljuc = Convert.ToInt32(PrikazProizvoda.SelectedRows[0].Cells[0].Value);
        }

        private void Proizvodi_Load(object sender, EventArgs e)
        {
            PrikaziProizvod();
            PopuniPadajuci();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (kljuc == 0)
                MessageBox.Show("Oznacite proizvod koji želite obrisati");
            else
            {
                try
                {
                    veza.Open();
                    string Query = "DELETE FROM Proizvod_Tabela WHERE Id_Proizvoda=" + kljuc + "";
                    SqlCommand komanda = new SqlCommand(Query, veza);
                    komanda.ExecuteNonQuery();
                    MessageBox.Show("Uspješno ste obrisali proizvod");

                    veza.Close();
                    PrikaziProizvod();
                    Izbrisi();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }
        private void PopuniPadajuci()
        {
            veza.Open();
            string selektovanje = "SELECT * FROM Kategorija_Tabela";
            SqlCommand komanda= new SqlCommand(selektovanje, veza);
            SqlDataAdapter adapter=new SqlDataAdapter(komanda);
            DataTable tabela=new DataTable();
            adapter.Fill(tabela);
            Kategorijecb.DataSource=tabela;
            Kategorijecb.DisplayMember = "Naziv_Kategorije";
            Kategorijecb.ValueMember = "Naziv_Kategorije";
            veza.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
           
            if (ImeProiztb.Text == "" || Kolicinatb.Text == "" || Cijenatb.Text == "" || Kategorijecb.SelectedIndex==-1)
                MessageBox.Show("Označite proizvod koji želite uređivati");
            else
            {
                try
                {
                    veza.Open();
                    string query = "UPDATE Proizvod_Tabela SET Ime_Proizvoda='" + ImeProiztb.Text + "',Kolicina_Proizvoda=" + Kolicinatb.Text + ",Cijena_Proizvoda=" + Cijenatb.Text + ",Kategorija_Proizvoda='" + Kategorijecb.SelectedValue.ToString() + "'WHERE ID_Proizvoda=" + kljuc + "";
                    SqlCommand komanda = new SqlCommand(query, veza);
                    komanda.ExecuteNonQuery();
                    MessageBox.Show("Uspješno ste uredili proizvod");

                    veza.Close();
                    PrikaziProizvod();
                    Izbrisi();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }

        private void Odjava_Click(object sender, EventArgs e)
        {
            Prijava prijava = new Prijava();
            prijava.Show();
            this.Hide();
        }

        private void PretragaCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Filtriraj();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            PrikaziProizvod();
            PretragaCb.SelectedIndex = -1;
        }

        private void Odjavalbl_Click(object sender, EventArgs e)
        {
            Prijava prijava=new Prijava();
            prijava.Show();
            this.Hide();
        }

        private void Odjavalbl_MouseEnter(object sender, EventArgs e)
        {
            Odjavalbl.ForeColor = Color.DeepPink;
        }

        private void Odjavalbl_MouseLeave(object sender, EventArgs e)
        {
            Odjavalbl.ForeColor=Color.Crimson;
        }

        private void Uposlenicilbl_MouseEnter(object sender, EventArgs e)
        {
            crta.Location = new Point(389, 152);
        }

        private void Uposlenicilbl_MouseLeave(object sender, EventArgs e)
        {
            crta.Location = new Point(545, 152);
        }

        private void Uposlenicilbl_Click(object sender, EventArgs e)
        {
           Uposlenici up=new Uposlenici();
            up.Show();
            this.Hide();
        }

        private void UposleniciPB_Click(object sender, EventArgs e)
        {
            Uposlenici up = new Uposlenici();
            up.Show();
            this.Hide();

        }

        private void Kategorijelbl_Click(object sender, EventArgs e)
        {
            Kategorije kat=new Kategorije();
            kat.Show();
            this.Hide();
        }

        private void KategorijePB_Click(object sender, EventArgs e)
        {
            Kategorije kat = new Kategorije();
            kat.Show();
            this.Hide();

        }

        private void UposleniciPB_MouseEnter(object sender, EventArgs e)
        {
            crta.Location = new Point(389, 152);

        }

        private void UposleniciPB_MouseLeave(object sender, EventArgs e)
        {
            crta.Location = new Point(545, 152);
        }

        private void Kategorijelbl_MouseEnter(object sender, EventArgs e)
        {
            crta.Location = new Point(693,152);
        }

        private void Kategorijelbl_MouseLeave(object sender, EventArgs e)
        {
            crta.Location = new Point(545, 152);
        }

        private void KategorijePB_MouseEnter(object sender, EventArgs e)
        {
            crta.Location = new Point(693, 152);
        }

        private void KategorijePB_MouseLeave(object sender, EventArgs e)
        {
            crta.Location = new Point(545, 152);
        }
    }
}
