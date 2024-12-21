using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SmartTicket.comV1
{
    public partial class FlmKontrol : UserControl
    {
        public FrmKontrolListeEkranı frmKontrolListeEkrani; 
        SqlConnection baglanti = new SqlConnection(@"Server=.\SQLEXPRESS;Initial Catalog=SmarTicket;Integrated Security=True");

        public FlmKontrol()
        {
            InitializeComponent();
        }

     
        private void FlmKontrol_Load(object sender, EventArgs e)
        {
            baglanti.Open();
            string sorgu = "select * from Tbl_kontrol WHERE ID=@p1";
            SqlCommand komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@p1", lblIdNo.Text);
            baglanti.Close();
        }

      
        private void button2_Click(object sender, EventArgs e)
        {
            if (baglanti.State == ConnectionState.Closed)
            {
                baglanti.Open();
            }

            string sorgu = "SELECT * FROM Tbl_Kontrol WHERE ID = @p1";
            SqlCommand komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@p1", lblIdNo.Text);

            SqlDataReader oku = komut.ExecuteReader();

            if (oku.Read())
            {
                string mesaj = "TARİH: " + oku["TARIH"].ToString() + "\n" +
                               "SAAT: " + oku["SAAT"].ToString() + "\n" +
                               "SALON: " + oku["SALONADI"].ToString() + "\n" +
                               "FILIMADI: " + oku["FILIMADI"].ToString();

                MessageBox.Show(mesaj, "Film Detayları", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            oku.Close(); 
            baglanti.Close(); 
        }

     
        private void button1_Click(object sender, EventArgs e)
        {
            // Silme işlemi için onay mesajını göster
            DialogResult confirmation = MessageBox.Show(
                "Bu kaydı silmek istediğinizden emin misiniz?",
                "Silme Onayı",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (confirmation == DialogResult.Yes)
            {
                try
                {
                  
                    if (baglanti.State == ConnectionState.Closed)
                    {
                        baglanti.Open();
                    }

                
                    SqlCommand sil = new SqlCommand("DELETE FROM Tbl_Kontrol WHERE ID=@p1", baglanti);
                    sil.Parameters.AddWithValue("@p1", lblIdNo.Text.Trim());
                    int etkilenenSatirlar = sil.ExecuteNonQuery();

                    if (etkilenenSatirlar > 0)
                    {
               
                        MessageBox.Show(
                            lblAdSoyad.Text + " Filme  ait kayıt başarıyla silinmiştir.",
                            "Başarılı",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );

                        if (frmKontrolListeEkrani != null)
                        {
                            frmKontrolListeEkrani.ListeyiGuncelle();
                        }
                    }
                    else
                    {
                     
                        MessageBox.Show(
                            "Silme işlemi başarısız. Kayıt bulunamadı.",
                            "Uyarı",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );
                    }
                }
                catch (Exception ex)
                {
               
                    MessageBox.Show(
                        "Hata: " + ex.Message,
                        "Silme İşlemi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
                finally
                {
                    
                    if (baglanti.State == ConnectionState.Open)
                    {
                        baglanti.Close();
                    }
                }
            }
            else
            {
          
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
