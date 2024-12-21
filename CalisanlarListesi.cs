using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SmartTicket.comV1
{
    public partial class CalisanlarListesi : UserControl
    {
        public CalisanlarListesi()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(@"Server=.\SQLEXPRESS;Initial Catalog=SmarTicket;Integrated Security=True");

        // Listeyi güncelleyen metod
        public void ListeyiYenile()
        {
            try
            {
                baglanti.Open();

                // ID'yi kullanarak verileri al
                string sorgu = "SELECT * FROM Tbl_Calisanlar WHERE ID = @p1";
                SqlCommand komut = new SqlCommand(sorgu, baglanti);
                komut.Parameters.AddWithValue("@p1", lblId.Text);

                SqlDataReader oku = komut.ExecuteReader();
                if (oku.Read())
                {
                    lblCinsiyet.Text = oku["CINSIYET"].ToString();
                    lblAdSoyad.Text = oku["ADSOYAD"].ToString();
                   
                }

                baglanti.Close();

                // Cinsiyet simgesini güncelle
                if (lblCinsiyet.Text == "0")
                {
                    pbCinsiyet.ImageLocation = Path.Combine(Application.StartupPath, "resimler", "erkek.png");
                }
                else
                {
                    pbCinsiyet.ImageLocation = Path.Combine(Application.StartupPath, "resimler", "kadın.png");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
            finally
            {
                if (baglanti.State == ConnectionState.Open)
                {
                    baglanti.Close();
                }
            }
        }

        // Form yüklendiğinde verileri al
        private void CalisanlarListesi_Load(object sender, EventArgs e)
        {
            ListeyiYenile(); // İlk yüklemede yenileme yap
        }

       


        private void button3_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(lblId.Text))
                {
                    MessageBox.Show("Geçerli bir ID girilmedi.");
                    return;
                }

                int ID;
                if (!int.TryParse(lblId.Text, out ID))
                {
                    MessageBox.Show("Geçerli bir ID formatı değil.");
                    return;
                }

                CalisanlarDuzenle frmDuzenle = new CalisanlarDuzenle(ID);

                // Listeyi güncellemek için event bağlama
                frmDuzenle.ListeGuncelleEvent += new CalisanlarDuzenle.ListeGuncelleHandler(ListeyiYenile);

                frmDuzenle.ShowDialog(); // Düzenleme formunu aç

                // Düzenleme sonrası listeyi yenile
                ListeyiYenile();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            // Silme işlemi için onay mesajını göster
            DialogResult confirmation = MessageBox.Show(
                "Çalışan silinsin mi?",
                "Silme Onayı",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (confirmation == DialogResult.Yes)
            {
                try
                {
                    // Veritabanı bağlantısını aç
                    if (baglanti.State == ConnectionState.Closed)
                    {
                        baglanti.Open();
                    }

                    // Silme sorgusunu çalıştır
                    SqlCommand sil = new SqlCommand("DELETE FROM Tbl_Calisanlar WHERE ID=@p1", baglanti);
                    sil.Parameters.AddWithValue("@p1", lblId.Text);
                    sil.ExecuteNonQuery();

                    // Kullanıcıya bilgilendirme mesajı göster
                    MessageBox.Show(
                        lblAdSoyad.Text + " kişisine ait kayıt başarıyla silinmiştir.",
                        "Başarılı",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    // Gerekirse formu kapat veya listeyi yenile
                    this.Hide();
                }
                catch (Exception ex)
                {
                    // Hata mesajını kullanıcıya göster
                    MessageBox.Show(
                        "Kayıt silinirken bir hata oluştu: " + ex.Message,
                        "Hata",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
                finally
                {
                    // Bağlantıyı kapat
                    if (baglanti.State == ConnectionState.Open)
                    {
                        baglanti.Close();
                    }
                }
            }
            else
            {
                // Kullanıcı "Hayır" seçtiyse bilgilendirme mesajı
                MessageBox.Show(
                    "Silme işlemi iptal edildi.",
                    "Bilgilendirme",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }

        }
    }
}
