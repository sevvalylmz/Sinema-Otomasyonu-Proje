using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SmartTicket.comV1
{
    public partial class FrmFilmDetay : Form
    {
        public FrmFilmDetay()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(@"Server=.\SQLEXPRESS;Initial Catalog=SmarTicket;Integrated Security=True");
        public string idNo = "";

        private void FrmFilmDetay_Load(object sender, EventArgs e)
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
                lblFilmPuani.Text = oku["PUAN"].ToString(); 
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

        private void btnDuzenle_Click(object sender, EventArgs e)
        {
            FrmFilmDuzenle duzenleForm = new FrmFilmDuzenle
            {
                idNo = this.idNo,
                FilmAdi = lblFilmAdi.Text,
                FilmOzellikleri = lblFilmOzellikleri.Text,
                FilmOyuncular = lblFilmOyuncular.Text,
                FilmYonetmeni = lblFilmYonetmeni.Text,
                FilmVizyon = lblFilmVizyon.Text,
                FilmDurumu = lblFilmDurumu.Text == "FİLM VİZYONDA" ? "1" : "0",
                FilmDetayi = lblFilmDetayı.Text,
                FilmBicimi = lblFilmBicimi.Text,
                FilmTuru = lblFilmTuru.Text,
                FilmPuani = lblFilmPuani.Text // Film puanını aktar
            };

            // Düzenleme formunu göster
            if (duzenleForm.ShowDialog() == DialogResult.OK)
            {
                // Düzenleme sonrası verileri güncelle
                lblFilmAdi.Text = duzenleForm.FilmAdi;
                lblFilmOzellikleri.Text = duzenleForm.FilmOzellikleri;
                lblFilmOyuncular.Text = duzenleForm.FilmOyuncular;
                lblFilmYonetmeni.Text = duzenleForm.FilmYonetmeni;
                lblFilmVizyon.Text = duzenleForm.FilmVizyon;
                lblFilmDurumu.Text = duzenleForm.FilmDurumu == "1" ? "FİLM VİZYONDA" : "FİLM VİZYONA GİRECEK";
                lblFilmDetayı.Text = duzenleForm.FilmDetayi;
                lblFilmBicimi.Text = duzenleForm.FilmBicimi;
                lblFilmTuru.Text = duzenleForm.FilmTuru;
                lblFilmPuani.Text = duzenleForm.FilmPuani; // Puanı güncelle
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void groupBox11_Enter(object sender, EventArgs e)
        {

        }
    }
}
