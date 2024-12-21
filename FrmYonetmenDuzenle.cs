using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System;

namespace SmartTicket.comV1
{
    public partial class FrmYonetmenDuzenle : Form
    {
        public string ID { get; set; }

        public FrmYonetmenDuzenle(string yonetmenId)
        {
            InitializeComponent();
            ID = yonetmenId;
        }

        SqlConnection baglanti = new SqlConnection(@"Server=.\SQLEXPRESS;Initial Catalog=SmarTicket;Integrated Security=True");
        public string resimYolu = "";
        private string cinsiyet;
        private string bYas;

        void yasHesaplama()
        {
            try
            {
                string dogum = $"{numericUpDown1.Value}-{numericUpDown2.Value}-{numericUpDown3.Value}";
                DateTime dogumTarihi = Convert.ToDateTime(dogum);
                DateTime bugun = DateTime.Today;
                int yas = bugun.Year - dogumTarihi.Year;

                if (bugun.Month < dogumTarihi.Month || (bugun.Month == dogumTarihi.Month && bugun.Day < dogumTarihi.Day))
                    yas--;

                bYas = yas.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu: " + ex.Message);
            }
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
            int geri = 300 - karakterSayisi;
            label8.Text = geri.ToString();

            if (geri >= 20)
                label8.ForeColor = Color.Green;
            if (geri <= 20)
                label8.ForeColor = Color.Orange;
            if (geri <= 10)
                label8.ForeColor = Color.Red;
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox4.Text != "")
            {
                string adSoyad = textBox1.Text.ToString().ToUpper();
                string cinsiyet = radioButton1.Checked ? "0" : "1";
                DateTime dogumTarihi = new DateTime((int)numericUpDown3.Value, (int)numericUpDown1.Value, (int)numericUpDown2.Value);

                try
                {
                    baglanti.Open();
                    SqlCommand update = new SqlCommand(
                        "UPDATE Tbl_Yonetmenler SET ADSOYAD = @p1, CINSIYET = @p2, BIYOGRAFI = @p3, RESIM = @p5 WHERE ID = @p6",
                        baglanti
                    );

                    update.Parameters.AddWithValue("@p1", adSoyad);
                    update.Parameters.AddWithValue("@p2", cinsiyet);
                    update.Parameters.AddWithValue("@p3", textBox4.Text);

                    // Resim güncellenmesi
                    if (!string.IsNullOrEmpty(resimYolu))
                    {
                        update.Parameters.AddWithValue("@p5", resimYolu);
                    }
                    else
                    {
                        update.Parameters.AddWithValue("@p5", DBNull.Value);
                    }

                    update.Parameters.AddWithValue("@p6", ID);
                    update.ExecuteNonQuery();
                    baglanti.Close();

                    MessageBox.Show("Yönetmen Bilgileri Başarıyla Güncellenmiştir!");

                    // Formu kapatıyoruz ve ana listeyi güncellemek için DialogResult döndürüyoruz
                    this.DialogResult = DialogResult.OK;
                    this.Close(); // Formu kapatıyoruz
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata oluştu: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Lütfen Tüm Alanları Doldurduğunuzdan Emin Olun!");
            }
        }


        private void FrmYonetmenDuzenle_Load_1(object sender, EventArgs e)
        {
            baglanti.Open();
            string sorgu = "SELECT * FROM Tbl_Yonetmenler WHERE ID = @p1";
            SqlCommand komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@p1", ID);
            SqlDataReader oku = komut.ExecuteReader();
            if (oku.Read())
            {
                textBox1.Text = oku["ADSOYAD"].ToString();
                textBox4.Text = oku["BIYOGRAFI"].ToString();

                if (oku["CINSIYET"].ToString() == "0")
                {
                    radioButton1.Checked = true;
                }
                else
                {
                    radioButton2.Checked = true;
                }

                if (oku["RESIM"] != DBNull.Value)
                {
                    pictureBox3.ImageLocation = oku["RESIM"].ToString();
                }
            }
            baglanti.Close();
        }

      

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

       
    }
}
