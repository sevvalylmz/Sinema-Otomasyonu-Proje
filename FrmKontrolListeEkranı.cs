using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SmartTicket.comV1
{
    public partial class FrmKontrolListeEkranı : Form
    {
        public FrmKontrolListeEkranı()
        {
            InitializeComponent();
        }

        // SQL bağlantısı
        SqlConnection baglanti = new SqlConnection(@"Server=.\SQLEXPRESS;Initial Catalog=SmarTicket;Integrated Security=True");

        // Listeyi güncelleyen metod
        public void ListeyiGuncelle()
        {
            try
            {
                // Paneli temizle
                ListePaneli.Controls.Clear();

                // Veritabanından tüm filmleri getir
                string sorgu = "SELECT * FROM Tbl_Kontrol ORDER BY ID ASC";
                baglanti.Open();
                SqlCommand komut = new SqlCommand(sorgu, baglanti);
                SqlDataReader oku = komut.ExecuteReader();

                // Her film için kontrol oluştur
                while (oku.Read())
                {
                    FlmKontrol arac = new FlmKontrol();
                    arac.lblAdSoyad.Text = oku["FILIMADI"].ToString();
                    arac.lblIdNo.Text = oku["ID"].ToString();
                    arac.frmKontrolListeEkrani = this; // FlmKontrol'e form referansı ekle
                    ListePaneli.Controls.Add(arac);
                }

                baglanti.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Liste güncellenirken bir hata oluştu: " + ex.Message);
            }
        }

        // Bir film silen metod
        public void SilFilm(string filmId)
        {
            try
            {
                string sorgu = "DELETE FROM Tbl_Kontrol WHERE ID = @ID";
                using (SqlCommand komut = new SqlCommand(sorgu, baglanti))
                {
                    komut.Parameters.AddWithValue("@ID", filmId);
                    baglanti.Open();
                    komut.ExecuteNonQuery();
                    baglanti.Close();
                }

                // Silme işleminden sonra listeyi güncelle
                ListeyiGuncelle();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Film silinirken bir hata oluştu: " + ex.Message);
            }
        }

        // Form yüklendiğinde listeyi güncelle
        private void FrmKontrolListeEkranı_Load(object sender, EventArgs e)
        {
            ListeyiGuncelle();
        }

        // Kapatma butonu için olay
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Arama yapılırken listeyi filtreleyen metod
        private void textAramaYap_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ListePaneli.Controls.Clear();
                string sorgu = "SELECT * FROM Tbl_Kontrol WHERE FILIMADI LIKE @FILIMADI ORDER BY FILIMADI ASC";
                SqlCommand ara = new SqlCommand(sorgu, baglanti);
                ara.Parameters.AddWithValue("@FILIMADI", "%" + textAramaYap.Text + "%");

                baglanti.Open();
                SqlDataReader oku = ara.ExecuteReader();

                // Her film için kontrol oluştur
                while (oku.Read())
                {
                    FlmKontrol arac = new FlmKontrol();
                    arac.lblIdNo.Text = oku["ID"].ToString();
                    arac.lblAdSoyad.Text = oku["FILIMADI"].ToString();
                    arac.frmKontrolListeEkrani = this; // FlmKontrol'e form referansı ekle
                    ListePaneli.Controls.Add(arac);
                }

                baglanti.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Arama sırasında bir hata oluştu: " + ex.Message);
            }
        }
    }
}
