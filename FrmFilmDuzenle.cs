using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SmartTicket.comV1
{
    public partial class FrmFilmDuzenle : Form
    {
        public FrmFilmDuzenle()
        {
            InitializeComponent();
        }

        // Özellikler
        public string idNo { get; set; }
        public string FilmAdi { get; set; }
        public string FilmOzellikleri { get; set; }
        public string FilmOyuncular { get; set; }
        public string FilmYonetmeni { get; set; }
        public string FilmVizyon { get; set; }
        public string FilmDurumu { get; set; }
        public string FilmDetayi { get; set; }
        public string FilmBicimi { get; set; }
        public string FilmTuru { get; set; }
        public string FilmPuani { get; set; }

        // Veritabanı bağlantısı
        SqlConnection baglanti = new SqlConnection(@"Server=.\SQLEXPRESS;Initial Catalog=SmarTicket;Integrated Security=True");

        private void FrmFilmDuzenle_Load(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();
                string sorgu = "SELECT PUAN FROM Tbl_Filmler WHERE ID=@p1";
                SqlCommand komut = new SqlCommand(sorgu, baglanti);
                komut.Parameters.AddWithValue("@p1", idNo);

                SqlDataReader dr = komut.ExecuteReader();
                if (dr.Read())
                {
                    txtpuan.Text = dr["PUAN"].ToString(); // Puanı TextBox üzerinden göster
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (baglanti.State == System.Data.ConnectionState.Open)
                    baglanti.Close();
            }

            // Diğer bilgiler
            txtFilmAdi.Text = FilmAdi;
            txtFilmOzellikleri.Text = FilmOzellikleri;
            txtFilmOyuncular.Text = FilmOyuncular;
            txtFilmYonetmeni.Text = FilmYonetmeni;
            txtFilmVizyon.Text = FilmVizyon;
            txtFilmDurumu.Text = FilmDurumu == "1" ? "FİLM VİZYONDA" : "FİLM VİZYONA GİRECEK";
            txtFilmDetayi.Text = FilmDetayi;
            txtFilmBicimi.Text = FilmBicimi;
            txtFilmTuru.Text = FilmTuru;
        }



        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnKaydet_Click_1(object sender, EventArgs e)
        {
            try
            {
                // Veritabanını güncelle
                string sorgu = "UPDATE Tbl_Filmler SET ADI=@p1, OZELLIKLERI=@p2, OYUNCU=@p3, YONETMEN=@p4, TARIH=@p5, DURUM=@p6, DETAY=@p7, BICIM=@p8, TURU=@p9, PUAN=@p10 WHERE ID=@p11";
                baglanti.Open();
                SqlCommand komut = new SqlCommand(sorgu, baglanti);
                komut.Parameters.AddWithValue("@p1", txtFilmAdi.Text);
                komut.Parameters.AddWithValue("@p2", txtFilmOzellikleri.Text);
                komut.Parameters.AddWithValue("@p3", txtFilmOyuncular.Text);
                komut.Parameters.AddWithValue("@p4", txtFilmYonetmeni.Text);
                komut.Parameters.AddWithValue("@p5", txtFilmVizyon.Text);
                komut.Parameters.AddWithValue("@p6", txtFilmDurumu.Text == "FİLM VİZYONDA" ? "1" : "0");
                komut.Parameters.AddWithValue("@p7", txtFilmDetayi.Text);
                komut.Parameters.AddWithValue("@p8", txtFilmBicimi.Text);
                komut.Parameters.AddWithValue("@p9", txtFilmTuru.Text);
                komut.Parameters.AddWithValue("@p10", txtpuan.Text); // Puanı da kaydediyoruz
                komut.Parameters.AddWithValue("@p11", idNo);

                komut.ExecuteNonQuery();
                baglanti.Close();

                // Değişiklikleri geri bildir
                FilmAdi = txtFilmAdi.Text;
                FilmOzellikleri = txtFilmOzellikleri.Text;
                FilmOyuncular = txtFilmOyuncular.Text;
                FilmYonetmeni = txtFilmYonetmeni.Text;
                FilmVizyon = txtFilmVizyon.Text;
                FilmDurumu = txtFilmDurumu.Text == "FİLM VİZYONDA" ? "1" : "0";
                FilmDetayi = txtFilmDetayi.Text;
                FilmBicimi = txtFilmBicimi.Text;
                FilmTuru = txtFilmTuru.Text;
                FilmPuani = txtpuan.Text;

                MessageBox.Show("Film bilgileri başarıyla güncellendi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (baglanti.State == System.Data.ConnectionState.Open)
                    baglanti.Close();
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtFilmDetayi_TextChanged(object sender, EventArgs e)
        {
            int karakterSayisi = txtFilmDetayi.Text.Length;
            int geri = 500 - karakterSayisi;
            label8.Text = geri.ToString();
            if (geri >= 20)
            {
                label8.ForeColor = Color.Green;
            }
            if (geri <= 20)
            {
                label8.ForeColor = Color.Orange;
            }
            if (geri <= 10)
            {
                label8.ForeColor = Color.Red;
            }
        }
    }
}