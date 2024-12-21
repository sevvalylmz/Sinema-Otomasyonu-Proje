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

namespace SmartTicket.comV1
{
    public partial class FrmFilmDetayEkrani2 : Form
    {
        public FrmFilmDetayEkrani2()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Server=.\SQLEXPRESS;Initial Catalog=SmarTicket;Integrated Security=True");
        public string idNo = "";

        private void FrmFilmDetayEkrani2_Load(object sender, EventArgs e)
        {
            string sorgu = "SELECT * FROM Tbl_Filmler WHERE ID=@p1";
            baglanti.Open();
            SqlCommand komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@p1", idNo);

            SqlDataReader oku = komut.ExecuteReader();
            if (oku.Read())
            {
                pictureBox3.ImageLocation = oku["AFIS"].ToString();
                lblFilmAdi.Text = oku["ADI"].ToString();
                lblFilmOzellikleri.Text = oku["OZELLIKLERI"].ToString();
                lblFilmOyuncular.Text = oku["OYUNCU"].ToString();
                lblFilmYonetmeni.Text = oku["YONETMEN"].ToString();
                lblFilmVizyon.Text = oku["TARIH"].ToString();
                lblFilmDurumu.Text = oku["DURUM"].ToString();
                lblFilmDetayı.Text = oku["DETAY"].ToString();
                lblFilmBicimi.Text = oku["BICIM"].ToString();
                lblFilmPuani.Text = oku["PUAN"].ToString(); // Film puanını yükle
                lblFilmTuru.Text = oku["TURU"].ToString();
            }
            baglanti.Close();

            // Durum bilgisini yazıya dönüştür
            if (lblFilmDurumu.Text == "1")
            {
                lblFilmDurumu.Text = "FİLM VİZYONDA";
            }
            else
            {
                lblFilmDurumu.Text = "FİLM VİZYONA GİRECEK";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
