using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Data.SqlClient;

namespace SmartTicket.comV1
{
    public partial class FrmFilimKayit : Form
    {
        public FrmFilimKayit()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Server=.\SQLEXPRESS;Initial Catalog=SmarTicket;Integrated Security=True");

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            verileriSil();
            
        }

        void verileriSil()
        {


            baglanti.Open();
            SqlCommand komut = new SqlCommand("delete from Tbl_Secilenler", baglanti);

            komut.ExecuteNonQuery();
            baglanti.Close(); 
           


        }



        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            label2.Text = "1";
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            label2.Text = "2";
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            label2.Text = "3";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            label2.Text = "4";
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            label2.Text = "5";
        }

        private void radioButton10_CheckedChanged(object sender, EventArgs e)
        {
            label2.Text = "6";
        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            label2.Text = "7";
        }

        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {
            label2.Text = "8";
        }

        private void radioButton9_CheckedChanged(object sender, EventArgs e)
        {
            label2.Text = "9";
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            label2.Text = "10";
        }

        private void groupBox8_Enter(object sender, EventArgs e)
        {

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

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            int karakterSayisi = textBox2.Text.Length;
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

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void FrmFilimKayit_Load(object sender, EventArgs e)
        {

           
          

            yListesiGetir();
            oListesiGetir();
            bugununTarihi();
            textBox1.Focus();
           

        }

        void bugununTarihi()
        {
            numericUpDown1.Value = DateTime.Today.Day;
            numericUpDown2.Value = DateTime.Today.Month;
            numericUpDown3.Value = DateTime.Today.Year;


        }
        void yListesiGetir()
        {
            string sorgu = "select * from Tbl_Yonetmenler  ORDER BY ADSOYAD ASC";
            flowLayoutPanel1.Controls.Clear();
            baglanti.Open();
            SqlCommand komut = new SqlCommand(sorgu, baglanti);

            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                yListeAracı arac = new yListeAracı();

                arac.lblAdi.Text = oku["ADSOYAD"].ToString();
                flowLayoutPanel1.Controls.Add(arac);
            }
            baglanti.Close();

        }
        void oListesiGetir()
        {
            string sorgu = "select * from Tbl_Oyuncular  ORDER BY ADSOYAD ASC";
            flowLayoutPanel2.Controls.Clear();
            baglanti.Open();
            SqlCommand ara = new SqlCommand(sorgu, baglanti);

            SqlDataReader oku = ara.ExecuteReader();
            while (oku.Read())
            {
                oListeAracı arac = new oListeAracı();
                arac.lbloAdi.Text = oku["ADSOYAD"].ToString();
                flowLayoutPanel2.Controls.Add(arac);
            }
            baglanti.Close();
        }

        private void textBox4_MouseMove(object sender, MouseEventArgs e)
        {
            lblYonetmenara.Visible = true;
        }

        private void textBox4_MouseLeave(object sender, EventArgs e)
        {
            lblYonetmenara.Visible = false;
        }

        private void textBox3_MouseMove(object sender, MouseEventArgs e)
        {
            lblOyuncuara.Visible = true;
        }

        private void textBox3_MouseLeave(object sender, EventArgs e)
        {
            lblOyuncuara.Visible = false;
        }

        private void lblYonetmenara_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            oyuncuAra();
        }
        void oyuncuAra()
        {
            string sorgu = "select * from Tbl_Oyuncular Where ADSOYAD LIKE '%" + textBox3.Text + "%'collate Turkish_CI_AS ORDER BY ADSOYAD ASC";
            flowLayoutPanel2.Controls.Clear();
            baglanti.Open();
            SqlCommand komut = new SqlCommand(sorgu, baglanti);
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                oListeAracı arac = new oListeAracı();
                arac.lbloAdi.Text = oku["ADSOYAD"].ToString();
                flowLayoutPanel2.Controls.Add(arac);
            }
            baglanti.Close();

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            yonetmenAra();
        }
        void yonetmenAra()
        {
            string sorgu = "select * from Tbl_Yonetmenler Where ADSOYAD LIKE '%" + textBox4.Text + "%'collate Turkish_CI_AS ORDER BY ADSOYAD ASC";
            flowLayoutPanel1.Controls.Clear();
            baglanti.Open();
            SqlCommand komut = new SqlCommand(sorgu, baglanti);
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                oListeAracı arac = new oListeAracı();
                arac.lbloAdi.Text = oku["ADSOYAD"].ToString();
                flowLayoutPanel1.Controls.Add(arac);
            }
            baglanti.Close();
        }

        private void lblAksiyon_Click(object sender, EventArgs e)
        {
            if (lblAksiyon.ForeColor == Color.FromArgb(84, 110, 122))
            {
                lblAksiyon.ForeColor = Color.FromArgb(205, 133, 63);
            }
            else
            {
                lblAksiyon.ForeColor = Color.FromArgb(84, 110, 122);

            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

            if (label4.ForeColor == Color.FromArgb(84, 110, 122))
            {
                label4.ForeColor = Color.FromArgb(205, 133, 63);
            }
            else
            {
                label4.ForeColor = Color.FromArgb(84, 110, 122);

            }
        }

        private void lblKorku_Click(object sender, EventArgs e)
        {

            if (lblKorku.ForeColor == Color.FromArgb(84, 110, 122))
            {
                lblKorku.ForeColor = Color.FromArgb(205, 133, 63);
            }
            else
            {
                lblKorku.ForeColor = Color.FromArgb(84, 110, 122);

            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

            if (label3.ForeColor == Color.FromArgb(84, 110, 122))
            {
                label3.ForeColor = Color.FromArgb(205, 133, 63);
            }
            else
            {
                label3.ForeColor = Color.FromArgb(84, 110, 122);

            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

            if (label6.ForeColor == Color.FromArgb(84, 110, 122))
            {
                label6.ForeColor = Color.FromArgb(205, 133, 63);
            }
            else
            {
                label6.ForeColor = Color.FromArgb(84, 110, 122);

            }
        }

        private void label7_Click(object sender, EventArgs e)
        {

            if (label7.ForeColor == Color.FromArgb(84, 110, 122))
            {
                label7.ForeColor = Color.FromArgb(205, 133, 63);
            }
            else
            {
                label7.ForeColor = Color.FromArgb(84, 110, 122);

            }
        }

        private void label20_Click(object sender, EventArgs e)
        {
            if (label20.ForeColor == Color.FromArgb(84, 110, 122))
            {
                label20.ForeColor = Color.FromArgb(205, 133, 63);
            }
            else
            {
                label20.ForeColor = Color.FromArgb(84, 110, 122);

            }
        }

        private void label18_Click(object sender, EventArgs e)
        {
            if (label18.ForeColor == Color.FromArgb(84, 110, 122))
            {
                label18.ForeColor = Color.FromArgb(205, 133, 63);
            }
            else
            {
                label18.ForeColor = Color.FromArgb(84, 110, 122);

            }
        }

        private void label19_Click(object sender, EventArgs e)
        {
            if (label19.ForeColor == Color.FromArgb(84, 110, 122))
            {
                label19.ForeColor = Color.FromArgb(205, 133, 63);
            }
            else
            {
                label19.ForeColor = Color.FromArgb(84, 110, 122);

            }
        }





        private void label14_Click(object sender, EventArgs e)
        {
            if (label14.ForeColor == Color.FromArgb(84, 110, 122))
            {
                label14.ForeColor = Color.FromArgb(205, 133, 63);
            }
            else
            {
                label14.ForeColor = Color.FromArgb(84, 110, 122);

            }
        }

        private void label13_Click(object sender, EventArgs e)
        {
            if (label13.ForeColor == Color.FromArgb(84, 110, 122))
            {
                label13.ForeColor = Color.FromArgb(205, 133, 63);
            }
            else
            {
                label13.ForeColor = Color.FromArgb(84, 110, 122);

            }
        }



        private void label12_Click(object sender, EventArgs e)
        {
            if (label12.ForeColor == Color.FromArgb(84, 110, 122))
            {
                label12.ForeColor = Color.FromArgb(205, 133, 63);
            }
            else
            {
                label12.ForeColor = Color.FromArgb(84, 110, 122);

            }
        }

        private void label10_Click(object sender, EventArgs e)
        {
            if (label10.ForeColor == Color.FromArgb(84, 110, 122))
            {
                label10.ForeColor = Color.FromArgb(205, 133, 63);
            }
            else
            {
                label10.ForeColor = Color.FromArgb(84, 110, 122);

            }
        }

        private void label11_Click(object sender, EventArgs e)
        {
            if (label11.ForeColor == Color.FromArgb(84, 110, 122))
            {
                label11.ForeColor = Color.FromArgb(205, 133, 63);
            }
            else
            {
                label11.ForeColor = Color.FromArgb(84, 110, 122);

            }
        }

        private void label9_Click(object sender, EventArgs e)
        {
            if (label9.ForeColor == Color.FromArgb(84, 110, 122))
            {
                label9.ForeColor = Color.FromArgb(205, 133, 63);
            }
            else
            {
                label9.ForeColor = Color.FromArgb(84, 110, 122);

            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            vizyonTarihiHesapla();
        }
        string vTarihi = "";
        string durum = "0";
        void vizyonTarihiHesapla()
        {
            vTarihi = numericUpDown1.Value + "-" + numericUpDown2.Value + "-" + numericUpDown3.Value;
            DateTime dVTarih = Convert.ToDateTime(vTarihi);
            DateTime bugunTarihi = DateTime.Today;

            TimeSpan tSpan = dVTarih - bugunTarihi;
            if (tSpan.TotalDays < 0)
            {
                MessageBox.Show("GEÇMİŞ TARİHLERE AİT FİLİM EKLENMESİ YAPILAMAZ!");
                bugununTarihi();
            }
            else
            {
                if (tSpan.TotalDays == 0)
                {
                    durum = "1";
                    MessageBox.Show(textBox1.Text.ToUpper() +  " FİLMİ BUGÜN VİZYONDA ");

                }
                else
                {
                    durum = "0";
                    MessageBox.Show(textBox1.Text.ToUpper() + " " + tSpan.TotalDays.ToString() + " GÜN SONRA VİZYONA GİRECEKTİR! ");

                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTarih.Text = DateTime.Now.ToShortDateString();
            lblSaat.Text = DateTime.Now.ToShortTimeString();

        }
        string yonetmen = "";
        string oyuncu = "";

        void secilenYonetmen()
        {
            yonetmen = "";
            string sorgu = "select * from Tbl_Secilenler Where TUR='YÖNETMEN'";
            baglanti.Open();
            SqlCommand komut = new SqlCommand(sorgu, baglanti);
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                yonetmen += " ," + oku["KISI"].ToString();
            }

            baglanti.Close();
        }
        void secilenOyuncu()
        {
            oyuncu = "";
            string sorgu = "select * from Tbl_Secilenler Where TUR='OYUNCU'";
            baglanti.Open();
            SqlCommand komut = new SqlCommand(sorgu, baglanti);
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                oyuncu += " ," + oku["KISI"].ToString();
            }

            baglanti.Close();
        }

        void temizlemeMetodu()
        {
            var currentWindowState = this.WindowState;
            this.Controls.Clear();
            this.InitializeComponent();
            verileriSil();
            textBox1.Focus();
            yListesiGetir();
            oListesiGetir();
            bugununTarihi();
            this.WindowState = FormWindowState.Normal;
           



        }

        private void button3_Click(object sender, EventArgs e)
        {
             
            secilenYonetmen();
            secilenOyuncu();
            tur();
            ozellik();
            bicim();
             

            if (textBox1.Text != "" && textBox2.Text != "" && yonetmen != "" && oyuncu != "" && resimYolu != "" && vTarihi != "" && secilenBicim != "" && secilenOzellik != "" && secilenTur != "")
            {

                string sorgu = "insert into Tbl_Filmler (ADI,TURU,OZELLIKLERI,BICIM,YONETMEN,OYUNCU,DETAY,PUAN,AFIS,TARIH,DURUM) VALUES (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11)";
                baglanti.Open();
                SqlCommand komut = new SqlCommand(sorgu, baglanti);

                komut.Parameters.AddWithValue("@p1", textBox1.Text.ToUpper());
                if (secilenTur.Length > 2)
                {
                    komut.Parameters.AddWithValue("@p2", secilenTur.Substring(2));
                }
                else
                {
                    komut.Parameters.AddWithValue("@p2", secilenTur);
                }


                if (secilenOzellik.Length > 2)
                {
                    komut.Parameters.AddWithValue("@p3", secilenOzellik.Substring(2));
                }
                else
                {
                    komut.Parameters.AddWithValue("@p3", secilenOzellik);
                }

                if (secilenBicim.Length > 2)
                {
                    komut.Parameters.AddWithValue("@p4", secilenBicim.Substring(2));
                }
                else
                {
                    komut.Parameters.AddWithValue("@p4", secilenBicim);
                }


                if (yonetmen.Length > 2)
                {
                    komut.Parameters.AddWithValue("@p5", yonetmen.Substring(2));
                }
                else
                {
                    komut.Parameters.AddWithValue("@p5", yonetmen);
                }

                if (oyuncu.Length > 2)
                {
                    komut.Parameters.AddWithValue("@p6", oyuncu.Substring(2));
                }
                else
                {
                    komut.Parameters.AddWithValue("@p6", oyuncu);
                }
                
                komut.Parameters.AddWithValue("@p7", textBox2.Text.ToUpper());
                komut.Parameters.AddWithValue("@p8", label2.Text);
                komut.Parameters.AddWithValue("@p9", resimYolu);
                komut.Parameters.AddWithValue("@p10", vTarihi);
                komut.Parameters.AddWithValue("@p11", durum);
                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("FİLM KAYDI BAŞARILI!");
                this.WindowState = FormWindowState.Normal;
                temizlemeMetodu();
            }

            else
            {
                MessageBox.Show("Lütfen Tüm Alanları Doldurunuz!");
            }
        }

            string secilenTur = "";
            string secilenOzellik = "";
            string secilenBicim = "";

            void tur()
            {

                foreach (Control arac in groupBox2.Controls)
                {
                    if (arac is Label)
                    {
                        if (arac.ForeColor == Color.FromArgb(84, 110, 122))
                        {

                        }
                        else
                        {
                            secilenTur += " ," + arac.Text.ToString();
                        }
                    }
                }
            }
            void ozellik()
            {

                foreach (Control arac in groupBox3.Controls)
                {
                    if (arac is Label)
                    {
                        if (arac.ForeColor == Color.FromArgb(84, 110, 122))
                        {

                        }
                        else
                        {
                            secilenOzellik += " ," + arac.Text.ToString();
                        }
                    }
                }

            }
            void bicim()
            {

                foreach (Control arac in groupBox4.Controls)
                {
                    if (arac is Label)
                    {
                        if (arac.ForeColor == Color.FromArgb(84, 110, 122))
                        {

                        }
                        else
                        {
                            secilenBicim += " ," + arac.Text.ToString();
                        }
                    }
                }

            }

        private void label22_Click(object sender, EventArgs e)
        {
            if (label22.ForeColor == Color.FromArgb(84, 110, 122))
            {
                label22.ForeColor = Color.FromArgb(205, 133, 63);
            }
            else
            {
                label22.ForeColor = Color.FromArgb(84, 110, 122);

            }
        }

        private void groupBox7_Enter(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label24_Click(object sender, EventArgs e)
        {
            if (label24.ForeColor == Color.FromArgb(84, 110, 122))
            {
                label24.ForeColor = Color.FromArgb(205, 133, 63);
            }
            else
            {
                label24.ForeColor = Color.FromArgb(84, 110, 122);

            }
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }
    }
    }


