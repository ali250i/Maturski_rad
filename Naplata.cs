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
using System.Data.SqlClient;
using DGVPrinterHelper;


namespace BenzinskaPumpaVb.net
{
    public partial class Naplata : Form
    {
      DGVPrinter stampac=new DGVPrinter();
        public Naplata()
        {
            InitializeComponent();
        }
        SqlConnection veza = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='E:\Maturski rad\BenzinskaPumpaVb.net\BenzinskaPumbaDbVb.mdf';Integrated Security=True;Connect Timeout=30");



        private void Naplata_Load(object sender, EventArgs e)
        {

            PrikaziProizvod();
            TrenutniKorisniktb.Text = Prijava.Ime_Uposlenika;
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

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        int kljuc = 0;
        int lager = 0;
        

        private void PrikazProizvoda_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewRow red = new DataGridViewRow();
            red = PrikazProizvoda.Rows[e.RowIndex];
            ImeProiztb.Text = PrikazProizvoda.SelectedRows[0].Cells[1].Value.ToString();

            Cijenatb.Text = PrikazProizvoda.SelectedRows[0].Cells[3].Value.ToString();


            if (ImeProiztb.Text == "")
                kljuc = 0;
            else
            {
                kljuc = Convert.ToInt32(PrikazProizvoda.SelectedRows[0].Cells[0].Value);
                lager = Convert.ToInt32(PrikazProizvoda.SelectedRows[0].Cells[2].Value.ToString());
            }
        }
        int i = 0;
        double Rez = 0;
        private void button2_Click(object sender, EventArgs e)
        {
            if (Cijenatb.Text == "" || Kolicinatb.Text == "" || Convert.ToInt32(Kolicinatb.Text)>lager)
                MessageBox.Show("Unesite količinu");
            else if (ImeProiztb.Text == "")
                MessageBox.Show("Označite proizvod");
            else
            {
                int rednibr = RacunPrikaz.Rows.Add();
                i++;
                double ukupno = Convert.ToDouble(Kolicinatb.Text) * Convert.ToDouble(Cijenatb.Text);
                RacunPrikaz.Rows[rednibr].Cells["Column1"].Value = i;
                RacunPrikaz.Rows[rednibr].Cells["Column2"].Value = ImeProiztb.Text;
                RacunPrikaz.Rows[rednibr].Cells["Column3"].Value = Cijenatb.Text;
                RacunPrikaz.Rows[rednibr].Cells["Column4"].Value = Kolicinatb.Text;
                RacunPrikaz.Rows[rednibr].Cells["Column5"].Value =ukupno;
                Rez += ukupno;
                string ZaPlatitti;
                ZaPlatitti = "KM: " + Rez.ToString();
                ZaPlatitilab.Text = ZaPlatitti;
                AzurirajProizvod();
                PrikaziProizvod();
                ImeProiztb.Clear();
                Kolicinatb.Clear();
                Cijenatb.Clear();
                kljuc = 0;
                lager = 0;







            }
        }

        private void Reset()
        {
            ImeProiztb.Clear();
            Kolicinatb.Clear();
            Cijenatb.Clear();
            kljuc = 0;
            lager = 0;
            ImeKupcatb.Clear();
            


        }

        private void button1_Click(object sender, EventArgs e)
        {
            Reset();
        }
        private void DodajNaRacun()
        {
            
            try
            {
                veza.Open();
                string insertQuery = "INSERT INTO Rac_Tabela VALUES('" + ImeKupcatb.Text + "',"  +Rez+ ",'" + DateTime.Today.ToString("MM-dd-yyyy") + "')";
                SqlCommand komanda = new SqlCommand(insertQuery, veza);
                komanda.ExecuteNonQuery();
                MessageBox.Show("Uspješno ste sačuvali račun");

                veza.Close();
                
                //RacunPrikaz.Rows.Clear();

               

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
           
        private void AzurirajProizvod()
        {
            int novaKolicina = lager - Convert.ToInt32(Kolicinatb.Text);
            try
            {
                veza.Open();
                string query = "UPDATE Proizvod_Tabela SET Kolicina_Proizvoda='" + novaKolicina +  "'WHERE ID_Proizvoda=" + kljuc + "";
                SqlCommand komanda = new SqlCommand(query, veza);
                komanda.ExecuteNonQuery();
                MessageBox.Show("Uspješno ste dodali proizvod na račun");

                veza.Close();
                PrikaziProizvod();
               

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
       

        private void button3_Click(object sender, EventArgs e)
        {
            if (ImeKupcatb.Text == "")
            {
                MessageBox.Show("Upišite ime kupca");
            }
            else
            {

            
            
                DodajNaRacun();
                stampac.Title = "Benzinska pumpa"+Environment.NewLine+"- Vaš račun-";
                
                stampac.SubTitle = string.Format("Datum: {0}",DateTime.Now.Date.ToString("dd-MM-yyyy")+Environment.NewLine+"Ukupno za platiti: "+Rez+"KM");
                stampac.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
                stampac.PageNumbers = true;
                
                stampac.PageNumberInHeader = false;
                stampac.PorportionalColumns = true;
                stampac.HeaderCellAlignment = StringAlignment.Near;
                stampac.Footer = "Hvala na posjeti!";
                stampac.FooterSpacing = 15;
               
                
                stampac.PrintDataGridView(RacunPrikaz);

            }



        }

        private void Odjava_Click(object sender, EventArgs e)
        {
            Prijava prijava=new Prijava();
            prijava.Show();
            this.Hide();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            ZaPlatitilab.Text = "Ukupno:";
            RacunPrikaz.Rows.Clear();
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
            Odjavalbl.ForeColor= Color.Crimson;
        }
    }
}
