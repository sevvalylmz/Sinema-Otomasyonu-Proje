using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SmartTicket.comV1
{
    public partial class FrmCalisanlarKayit : Form
    {
        public FrmCalisanlarKayit()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(@"Server=.\SQLEXPRESS;Initial Catalog=SmarTicket;Integrated Security=True");
        public string bYas = "0";
        public string cinsiyet = "0";

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Yaşı hesapla
            yasHesaplama();

            // Tüm alanların doldurulup doldurulmadığını kontrol et
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && textBox5.Text != "" && textBox6.Text != "" && textBox7.Text != "" && textBox8.Text != "" && textBox9.Text != "")
            {
                // Kimlik numarasının geçerliliğini kontrol et
                if (!KimlikNoDogruMu(textBox3.Text))
                {
                    MessageBox.Show("Geçersiz Kimlik Numarası! Lütfen 11 haneli bir kimlik numarası giriniz.");
                    return;
                }

                // E-posta doğrulama
                if (!EmailDogruMu(textBox6.Text))
                {
                    MessageBox.Show("Geçersiz E-posta! Lütfen geçerli bir e-posta adresi giriniz.");
                    return;
                }

                // Telefon numarasının geçerliliğini kontrol et
                if (!TelefonDogruMu(textBox5.Text))
                {
                    MessageBox.Show("Geçersiz Telefon Numarası! Lütfen geçerli bir telefon numarası giriniz.");
                    return;
                }

                string adSoyad = textBox1.Text.ToString().ToUpper() + " " + textBox2.Text.ToString().ToUpper();

                try
                {
                    baglanti.Open();
                    SqlCommand kayit = new SqlCommand("insert into Tbl_Calisanlar(ADSOYAD,YAS,CINSIYET,KIMLIKNO,TELNO,EPOSTA,ADRES,DEPARTMAN,KADI,SIFRE) VALUES(@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10)", baglanti);
                    kayit.Parameters.AddWithValue("@p1", adSoyad);
                    kayit.Parameters.AddWithValue("@p2", bYas); // Hesaplanan yaş
                    kayit.Parameters.AddWithValue("@p3", cinsiyet);
                    kayit.Parameters.AddWithValue("@p4", textBox3.Text.ToString());
                    kayit.Parameters.AddWithValue("@p5", textBox5.Text.ToString());
                    kayit.Parameters.AddWithValue("@p6", textBox6.Text.ToString());
                    kayit.Parameters.AddWithValue("@p7", textBox4.Text.ToString());
                    kayit.Parameters.AddWithValue("@p8", textBox7.Text.ToString());
                    kayit.Parameters.AddWithValue("@p9", textBox9.Text.ToString());
                    kayit.Parameters.AddWithValue("@p10", textBox8.Text.ToString());

                    kayit.ExecuteNonQuery();
                    MessageBox.Show("ÇALIŞAN KAYIT İŞLEMİ BAŞARILI!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
                finally
                {
                    baglanti.Close();
                }

                aracTemizle();
            }
            else
            {
                MessageBox.Show("LÜTFEN TÜM ALANLARI EKSİKSİZ BİR ŞEKİLDE DOLDURUNUZ!");
            }
        }

        // Kimlik numarasını doğrulama
        private bool KimlikNoDogruMu(string kimlikNo)
        {
            if (kimlikNo.Length != 11 || !long.TryParse(kimlikNo, out _))
            {
                return false; // Geçersiz kimlik numarası
            }
            return true;
        }

        // E-posta doğrulama
        private bool EmailDogruMu(string email)
        {
            return email.Contains("@"); // Basit @ kontrolü
        }

       
        private bool TelefonDogruMu(string telefon)
        {
            return telefon.All(char.IsDigit) && telefon.Length >= 10; // Yalnızca rakamlar ve en az 10 haneli olmalı
        }

        void yasHesaplama()
        {
            try
            {
                
                DateTime dogumTarihi = new DateTime((int)numericUpDown3.Value, (int)numericUpDown2.Value, (int)numericUpDown1.Value);
                DateTime bugun = DateTime.Today;

                
                int yas = bugun.Year - dogumTarihi.Year;
                if (dogumTarihi > bugun.AddYears(-yas))
                {
                    yas--;
                }

                bYas = yas.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Yaş hesaplama hatası: " + ex.Message);
                bYas = "0"; 
            }
        }

        void aracTemizle()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";

            numericUpDown1.Value = 1;
            numericUpDown2.Value = 1;
            numericUpDown3.Value = DateTime.Today.Year; 
            radioButton1.Checked = true;
            radioButton2.Checked = false;
            label8.Text = "500";
            cinsiyet = "0"; 
            bYas = "0"; 
            textBox1.Focus();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            cinsiyet = "0"; 
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            cinsiyet = "1"; 
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            int karakterSayisi = textBox4.Text.Length;
            int geri = 500 - karakterSayisi;
            label8.Text = geri.ToString();
            if (geri >= 20)
            {
                label8.ForeColor = Color.Green;
            }
            else if (geri <= 20)
            {
                label8.ForeColor = Color.Orange;
            }
            if (geri <= 10)
            {
                label8.ForeColor = Color.Red;
            }
        }

        private void FrmCalisanlarKayit_Load(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
