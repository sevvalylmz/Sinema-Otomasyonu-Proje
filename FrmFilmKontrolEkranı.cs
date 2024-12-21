using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace SmartTicket.comV1
{
    public partial class FrmFilmKontrolEkranı : Form
    {
        public FrmFilmKontrolEkranı()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(@"Server=.\SQLEXPRESS;Initial Catalog=SmarTicket;Integrated Security=True");

        private void FrmFilmKontrolEkranı_Load(object sender, EventArgs e)
        {
            bugununTarihi();
            filmAdiGetir();
            salonAdiGetir();
        }

        void bugununTarihi()
        {
            numericUpDown1.Value = DateTime.Today.Day;
            numericUpDown2.Value = DateTime.Today.Month;
            numericUpDown3.Value = DateTime.Today.Year;


        }
        void filmAdiGetir()
        {
            string sorgu = "select * from Tbl_Filmler ORDER BY ADI ASC ";
            baglanti.Open();
            SqlCommand komut = new SqlCommand(sorgu, baglanti);
            SqlDataReader oku = komut.ExecuteReader();

            while (oku.Read())
            {
                string gelenTarih = oku["TARIH"].ToString();
                DateTime fTarihi = Convert.ToDateTime(gelenTarih);
                DateTime bugun = DateTime.Today;

                TimeSpan timeSpan = fTarihi - bugun;
                if (timeSpan.TotalDays <= 0)
                {
                    cbFilmAdi.Items.Add(oku["ADI"].ToString());
                }
            }
            baglanti.Close();
        }
        void salonAdiGetir()
        {
            string sorgu = "select * from Tbl_Salonlar ORDER BY SALONADI ASC ";
            baglanti.Open();
            SqlCommand komut = new SqlCommand(sorgu, baglanti);
            SqlDataReader oku = komut.ExecuteReader();

            while (oku.Read())
            {

                cbSalonAdi.Items.Add(oku["SALONADI"].ToString());

            }
            baglanti.Close();





        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.Text == "TAMAMLA")
            {
                string sorgu = "select DISTINCT SAAT from Tbl_Kontrol WHERE TARIH=@tarih AND SALONADI=@salonadi";
                string tarih = numericUpDown1.Value + "-" + numericUpDown2.Value + "-" + numericUpDown3.Value;
                baglanti.Open();

                SqlCommand komut = new SqlCommand(sorgu, baglanti);
                komut.Parameters.AddWithValue("@tarih", tarih);
                komut.Parameters.AddWithValue("@salonadi", cbSalonAdi.Text.ToString());
                SqlDataReader oku = komut.ExecuteReader();
                while (oku.Read())
                {
                    cbDoluSaatler.Items.Add(oku["SAAT"].ToString());
                }
                baglanti.Close();

                seansKontrol();

                button2.Text = "OLUŞTUR";
                button2.BackColor = Color.Peru;

            }
            else
            {
                kaydet();
                temizle();
                button2.Text = "TAMAMLA";
                button2.BackColor = Color.FromArgb(84, 110, 122);
            }
        }

        void kaydet()
        {
            
            string sorgu = "insert into Tbl_Kontrol (FILIMADI,TARIH,SAAT,SALONADI) values (@filimadi,@tarih,@saat,@salonadi)";
            string tarih = numericUpDown1.Value + "-" + numericUpDown2.Value + "-" + numericUpDown3.Value;
            baglanti.Open();
            SqlCommand ekle = new SqlCommand(sorgu,baglanti);
            ekle.Parameters.AddWithValue("@filimadi",cbFilmAdi.Text);
            ekle.Parameters.AddWithValue("@tarih", tarih);
            ekle.Parameters.AddWithValue("@saat", lblSecilen.Text);
            ekle.Parameters.AddWithValue("@salonadi", cbSalonAdi.Text);
            ekle.ExecuteNonQuery();
            baglanti.Close() ;
            MessageBox.Show("SALON ATAMA İŞLEMİ GERÇEKLEŞTİRİLDİ!");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            temizle();
        }

        void temizle()
        {
            cbFilmAdi.Items.Clear();
            cbSalonAdi.Items.Clear();
            cbDoluSaatler.Items.Clear();
            lblSecilen.Text = "";
            bugununTarihi();
            filmAdiGetir();
            salonAdiGetir();
            panelSeans.Controls.Clear();
            button2.Text = "TAMAMLA";
        }


        private void SeansSaatleri(object sender, EventArgs e)
        {
            foreach (RadioButton item in panelSeans.Controls)
            {
                if (item.Checked)
                {
                    lblSecilen.Text = item.Text.ToString();
                }
            }
        }
        void seansKontrol()
        {
            panelSeans.Controls.Clear();
            for (int i = 10; i < 23; i++)
            {
                for (int j = 0; j <= 30; j += 30)
                {
                    RadioButton rnd = new RadioButton();
                    rnd.ForeColor = Color.FromArgb(249, 164, 26);
                    rnd.FlatStyle = FlatStyle.Flat;
                    rnd.Width = 65;
                    rnd.Font = new System.Drawing.Font("Segoe UI Semibold", 10);
                    rnd.CheckedChanged += new EventHandler(SeansSaatleri);
                    if (j == 0)
                    {
                        rnd.Text = i.ToString() + ":" + j.ToString() + "0";
                    }
                    else
                    {
                        rnd.Text = i.ToString() + ":" + j.ToString();
                    }
                    if (cbDoluSaatler.Items.Contains(rnd.Text))
                    {
                        rnd.Visible = false;
                    }

                    panelSeans.Controls.Add(rnd);

                }
            }

        }
    }
}
