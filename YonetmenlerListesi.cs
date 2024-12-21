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
using System.IO;

namespace SmartTicket.comV1
{
    public partial class YonetmenlerListesi : UserControl
    {
        public YonetmenlerListesi()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Server=.\SQLEXPRESS;Initial Catalog=SmarTicket;Integrated Security=True");
        private void YonetmenlerListesi_Load(object sender, EventArgs e)
        {
            baglanti.Open();
            string sorgu = "select * from Tbl_Yonetmenler WHERE ID=@p1";
            SqlCommand komut = new SqlCommand(sorgu,baglanti);
            komut.Parameters.AddWithValue("@p1", lblId.Text);
            SqlDataReader oku = komut.ExecuteReader();
            if (oku.Read())
            {
               lblCinsiyet.Text = oku["CINSIYET"].ToString();
                lblAdSoyad.Text = oku["ADSOYAD"].ToString();
                
            }
            baglanti.Close();

            if (lblCinsiyet.Text == "0")
            {
                pbCinsiyet.ImageLocation = Path.Combine(Application.StartupPath, "resimler", "erkek.png");
            }
            else
            {
                pbCinsiyet.ImageLocation = Path.Combine(Application.StartupPath, "resimler", "kadın.png");
            }
        }
       

      

        private void button1_Click(object sender, EventArgs e)
        {
            // Silme işlemi için onay mesajını göster
            DialogResult confirmation = MessageBox.Show(
                "Yönetmen silinsin mi?",
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
                    SqlCommand sil = new SqlCommand("DELETE FROM Tbl_Yonetmenler WHERE ID=@p1", baglanti);
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

        private void pbCinsiyet_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                // Ensure lblId contains the director's ID as a string
                string yonetmenId = lblId.Text;

                // Pass the ID as a string to the FrmYonetmenDuzenle form
                FrmYonetmenDuzenle frmDuzenle = new FrmYonetmenDuzenle(yonetmenId); // Constructor expects string
                frmDuzenle.ShowDialog();

                // Optionally, refresh data after editing
                YonetmenlerListesi_Load(sender, e); // Reload the data to reflect any changes
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error opening edit form: " + ex.Message);
            }

        }
    }
}
