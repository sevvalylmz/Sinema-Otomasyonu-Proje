using System;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SmartTicket.comV1
{
    public partial class FrmOyuncuDuzenle : Form
    {
        public FrmOyuncuDuzenle(int oyuncuId)
        {
            InitializeComponent();
            this.oyuncuId = oyuncuId;
        }

        SqlConnection baglanti = new SqlConnection(@"Server=.\SQLEXPRESS;Initial Catalog=SmarTicket;Integrated Security=True");
        int oyuncuId;

        private void FrmOyuncuDuzenle_Load(object sender, EventArgs e)
        {
            // Load player data from the database to pre-fill fields
            baglanti.Open();
            string sorgu = "SELECT * FROM Tbl_Oyuncular WHERE ID = @p1";
            SqlCommand komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@p1", oyuncuId);
            SqlDataReader oku = komut.ExecuteReader();
            if (oku.Read())
            {
                textBox1.Text = oku["ADSOYAD"].ToString();
                textBox4.Text = oku["BIYOGRAFI"].ToString();

                // Set gender based on database value
                if (oku["CINSIYET"].ToString() == "0")
                {
                    radioButton1.Checked = true;  // Erkek
                }
                else
                {
                    radioButton2.Checked = true;  // Kadın
                }

             

                // Load image
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

        private void button3_Click_1(object sender, EventArgs e)
        {
            // Update player details in the database
            if (textBox1.Text != "" && textBox4.Text != "")
            {
                string adSoyad = textBox1.Text.ToString().ToUpper();
                string cinsiyet = radioButton1.Checked ? "0" : "1";

                // Get the selected date of birth
                DateTime dogumTarihi = new DateTime((int)numericUpDown3.Value, (int)numericUpDown1.Value, (int)numericUpDown2.Value);

                // Save the updated player details
                baglanti.Open();
                SqlCommand update = new SqlCommand("UPDATE Tbl_Oyuncular SET ADSOYAD = @p1, CINSIYET = @p2, BIYOGRAFI = @p3, RESIM = @p5 WHERE ID = @p6", baglanti);
                update.Parameters.AddWithValue("@p1", adSoyad);
                update.Parameters.AddWithValue("@p2", cinsiyet);
                update.Parameters.AddWithValue("@p3", textBox4.Text);
              
                update.Parameters.AddWithValue("@p5", pictureBox3.ImageLocation); // Save the image location
                update.Parameters.AddWithValue("@p6", oyuncuId);
                update.ExecuteNonQuery();
                baglanti.Close();

                // Notify user about the success
                MessageBox.Show("Oyuncu Bilgileri Başarıyla Güncellenmiştir!");

                // Close the form
                this.Close();
            }
            else
            {
                MessageBox.Show("Lütfen Tüm Alanları Doldurduğunuzdan Emin Olun!");
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            int karakterSayisi = textBox4.Text.Length;
            int geri = 300 - karakterSayisi;
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
