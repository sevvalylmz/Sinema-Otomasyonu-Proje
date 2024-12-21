using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SmartTicket.comV1
{
    public partial class CalisanlarDuzenle : Form
    {
        public delegate void ListeGuncelleHandler();
        public event ListeGuncelleHandler ListeGuncelleEvent;  

        private int calisanId;

        public CalisanlarDuzenle(int id)
        {
            InitializeComponent();
            calisanId = id;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();  
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (AreFieldsValid())
            {
                string adSoyad = textBox1.Text.ToUpper();  
                string cinsiyet = radioButton1.Checked ? "0" : "1";  

              
                DateTime dogumTarihi = CreateBirthDate();

                try
                {
                     
                    using (SqlConnection baglanti = new SqlConnection(@"Server=.\SQLEXPRESS;Initial Catalog=SmarTicket;Integrated Security=True"))
                    {
                        baglanti.Open();

                       
                        string sorgu = "UPDATE Tbl_Calisanlar SET ADSOYAD = @p1, KIMLIKNO = @p2, TELNO = @p3, EPOSTA = @p4, DEPARTMAN = @p5, KADI = @p6, SIFRE = @p7, ADRES = @p8, CINSIYET = @p9 WHERE ID = @p10";
                        SqlCommand update = new SqlCommand(sorgu, baglanti);

                        // Parametreleri ekle
                        AddParameters(update, adSoyad, cinsiyet);

                        // Veritabanı sorgusunu çalıştır ve etkilenen satır sayısını al
                        int rowsAffected = update.ExecuteNonQuery();

                        // Güncelleme başarılıysa
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Çalışan Bilgileri Başarıyla Güncellenmiştir!");
                            ListeGuncelleEvent?.Invoke();  // Listeyi güncelle
                            this.Close();  // Formu kapat
                        }
                        else
                        {
                            MessageBox.Show("Hiçbir kayıt güncellenemedi. Lütfen tekrar kontrol edin.");
                        }
                    }
                }
                catch (ArgumentOutOfRangeException)
                {
                    MessageBox.Show("Lütfen geçerli bir doğum tarihi giriniz! (Ay ve gün hatalı olabilir.)");
                }
                catch (SqlException sqlEx)
                {
                    MessageBox.Show("Veritabanı hatası: " + sqlEx.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Lütfen tüm bilgileri doldurduğunuzdan emin olun!");
            }
        }

        private void CalisanlarDuzenle_Load(object sender, EventArgs e)
        {
            LoadEmployeeData();  // Çalışan verisini yükle
        }

        // Çalışan verisini veritabanından yükle
        private void LoadEmployeeData()
        {
            using (SqlConnection baglanti = new SqlConnection(@"Server=.\SQLEXPRESS;Initial Catalog=SmarTicket;Integrated Security=True"))
            {
                baglanti.Open();
                string sorgu = "SELECT * FROM Tbl_Calisanlar WHERE ID = @p1";
                SqlCommand komut = new SqlCommand(sorgu, baglanti);
                komut.Parameters.AddWithValue("@p1", calisanId);

                using (SqlDataReader oku = komut.ExecuteReader())
                {
                    if (oku.Read())
                    {
                        textBox1.Text = oku["ADSOYAD"].ToString();
                        textBox3.Text = oku["KIMLIKNO"].ToString();
                        textBox5.Text = oku["TELNO"].ToString();
                        textBox6.Text = oku["EPOSTA"].ToString();
                        textBox7.Text = oku["DEPARTMAN"].ToString();
                        textBox9.Text = oku["KADI"].ToString();
                        textBox8.Text = oku["SIFRE"].ToString();
                        textBox4.Text = oku["ADRES"].ToString();

                        if (oku["CINSIYET"].ToString() == "0")
                        {
                            radioButton1.Checked = true;  
                        }
                        else
                        {
                            radioButton2.Checked = true; 
                        }
                    }
                }
            }
        }

     
        private bool AreFieldsValid()
        {
            return !string.IsNullOrWhiteSpace(textBox1.Text) &&
                   !string.IsNullOrWhiteSpace(textBox3.Text) &&
                   !string.IsNullOrWhiteSpace(textBox4.Text) &&
                   !string.IsNullOrWhiteSpace(textBox5.Text) &&
                   !string.IsNullOrWhiteSpace(textBox6.Text) &&
                   !string.IsNullOrWhiteSpace(textBox7.Text) &&
                   !string.IsNullOrWhiteSpace(textBox8.Text) &&
                   !string.IsNullOrWhiteSpace(textBox9.Text);
        }

    
        private DateTime CreateBirthDate()
        {
            int yil = (int)numericUpDown3.Value;
            int ay = (int)numericUpDown2.Value;
            int gun = (int)numericUpDown1.Value;
            return new DateTime(yil, ay, gun);
        }

     
        private void AddParameters(SqlCommand command, string adSoyad, string cinsiyet)
        {
            command.Parameters.AddWithValue("@p1", adSoyad);
            command.Parameters.AddWithValue("@p2", textBox3.Text);  
            command.Parameters.AddWithValue("@p3", textBox5.Text);  
            command.Parameters.AddWithValue("@p4", textBox6.Text);  
            command.Parameters.AddWithValue("@p5", textBox7.Text);  
            command.Parameters.AddWithValue("@p6", textBox9.Text);  
            command.Parameters.AddWithValue("@p7", textBox8.Text);  
            command.Parameters.AddWithValue("@p8", textBox4.Text);  
            command.Parameters.AddWithValue("@p9", cinsiyet);       
            command.Parameters.AddWithValue("@p10", calisanId);     
        }

       
        private void textBox1_TextChanged(object sender, EventArgs e) { }
        private void groupBox1_Enter(object sender, EventArgs e) { }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }
    }
}
