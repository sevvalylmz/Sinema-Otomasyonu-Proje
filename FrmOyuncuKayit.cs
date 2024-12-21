using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace SmartTicket.comV1
{
    public partial class FrmOyuncuKayit : Form
    {
        public FrmOyuncuKayit()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Server=.\SQLEXPRESS;Initial Catalog=SmarTicket;Integrated Security=True");

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public string resimYolu = "";

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "RESİM SEÇME EKRANI";
            ofd.Filter = "PNG | *.png | JPG-JPEG | *.jpg;*.jpeg  | All Files | *.*";
            ofd.FilterIndex = 3;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pictureBox3.Image = new Bitmap(ofd.FileName);
                resimYolu = ofd.FileName.ToString();
            }
        }
        public string cinsiyet = "0";
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            cinsiyet = "0";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            cinsiyet = "1";
        }
        public string bYas = "00";

        void yasHesaplama()
        {
            string dogum = numericUpDown1.Value.ToString() + "-" + numericUpDown2.Value.ToString() + "-" + numericUpDown3.Value.ToString();
            // MessageBox.Show(dogum);
            DateTime dogumTarihi = Convert.ToDateTime(dogum);
            DateTime bugun = DateTime.Today;
            int yas = bugun.Year - dogumTarihi.Year;
           // MessageBox.Show(yas.ToString());



            bYas = yas.ToString();
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

        private void button3_Click(object sender, EventArgs e)
        {
            yasHesaplama();
            if (textBox1.Text != "" && textBox2.Text != "" && textBox4.Text != "" && resimYolu != "")
            {

                string adSoyad = textBox1.Text.ToString().ToUpper() + " " + textBox2.Text.ToString().ToUpper();

                baglanti.Open();
                SqlCommand kayit = new SqlCommand("insert into TBL_Oyuncular(ADSOYAD,CINSIYET,YAS,BIYOGRAFI,RESIM) VALUES(@p1,@p2,@p3,@p4,@p5)", baglanti);
                kayit.Parameters.AddWithValue("@p1", adSoyad);
                kayit.Parameters.AddWithValue("@p2", cinsiyet);
                kayit.Parameters.AddWithValue("@p3", bYas);
                kayit.Parameters.AddWithValue("@p4", textBox4.Text.ToString());
                kayit.Parameters.AddWithValue("@p5", resimYolu);
                kayit.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Oyuncu KAYIT İŞLEMİ BAŞARILI!");
                aracTemizle();
            }
            else
            {
                MessageBox.Show("LÜTFEN TÜM ALANLARI EKSİKSİZ BİR ŞEKİLDE DOLDURUNUZ!");
            }

        }
        void aracTemizle()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox4.Text = "";
            numericUpDown1.Value = 1;
            numericUpDown2.Value = 1;
            numericUpDown3.Value = 2024;
            radioButton1.Checked = true;
            radioButton2.Checked = false;
            label8.Text = "300";
            cinsiyet = "0";
            bYas = "00";
            textBox1.Focus();
            pictureBox3.ImageLocation = Path.Combine(Application.StartupPath, "resimler", "icon-of-no-camera-use-or-no-photo-sign-vector.jpg");



        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void FrmOyuncuKayit_Load(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }
    }
}
