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
    public partial class FrmBiletOlustur : Form
    {
        public FrmBiletOlustur()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Server=.\SQLEXPRESS;Initial Catalog=SmarTicket;Integrated Security=True");

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void groupBox8_Enter(object sender, EventArgs e)
        {

        }

        private void FrmBiletOlustur_Load(object sender, EventArgs e)
        {
            filmAdiGetir();
            biletNolustur();
        }

        void biletNolustur()
        {
            Random rastgele = new Random();
            string karakterler = "012345678901234567890123456789012345678901234567890123456789";
            string kod = "";

            for (int i = 0; i < 11; i++)
            {
                kod += karakterler[rastgele.Next(karakterler.Length)];
            }
            txtBarkod.Text = kod.ToString();
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

        private void cbTarih_SelectedIndexChanged(object sender, EventArgs e)
        {
            panelSeans.Controls.Clear();
            string saatler = "";
            string sorgu = "SELECT DISTINCT SAAT FROM Tbl_Kontrol WHERE FILIMADI=@filmadi AND TARIH=@tarih";
            baglanti.Open();
            SqlCommand komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@filmadi", cbFilmAdi.Text.ToString());
            komut.Parameters.AddWithValue("@tarih", cbTarih.Text.ToString());

            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                saatler = oku["SAAT"].ToString();
                RadioButton rnd = new RadioButton();
                rnd.Text = saatler;
                rnd.ForeColor = Color.FromArgb(249, 164, 26);
                rnd.FlatStyle = FlatStyle.Flat;
                rnd.Width = 65;
                rnd.Font = new System.Drawing.Font("Segoe UI Semibold", 10);
                rnd.CheckedChanged += new EventHandler(SeansSaatleri);
                panelSeans.Controls.Add(rnd);
            }
            baglanti.Close();
        }

        private void SeansSaatleri(object sender, EventArgs e)
        {
            foreach (RadioButton item in panelSeans.Controls)
            {
                if (item.Checked == true)
                {
                    lblSeansSec.Text = item.Text;
                    cbSalon.Items.Clear();

                    string sorgu = "SELECT DISTINCT SALONADI FROM Tbl_Kontrol WHERE FILIMADI=@filmadi AND TARIH=@tarih AND SAAT=@saat";
                    baglanti.Open();
                    SqlCommand komut = new SqlCommand(sorgu, baglanti);
                    komut.Parameters.AddWithValue("@filmadi", cbFilmAdi.Text.ToString());
                    komut.Parameters.AddWithValue("@tarih", cbTarih.Text.ToString());
                    komut.Parameters.AddWithValue("@saat", lblSeansSec.Text.ToString());

                    SqlDataReader oku = komut.ExecuteReader();
                    while (oku.Read())
                    {
                        cbSalon.Items.Add(oku["SALONADI"].ToString());
                    }
                    baglanti.Close();
                }
            }
        }

        private void cbFilmAdi_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbTarih.Items.Clear();
            string sorgu = "SELECT DISTINCT TARIH FROM Tbl_Kontrol WHERE FILIMADI=@filmadi";
            baglanti.Open();
            SqlCommand komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@filmadi", cbFilmAdi.Text.ToString());
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                cbTarih.Items.Add(oku["TARIH"].ToString());
            }
            baglanti.Close();
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }
        void biletNoSorgula()
        {
            string sorgu = "select * from Tbl_Biletler where BKOD=@no";
            baglanti.Open();
            SqlCommand komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@no", txtBarkod.Text.ToString());
            SqlDataReader oku = komut.ExecuteReader();
            if (!oku.Read())
            {
              
                kaydetMETODU();

            }
            else
            {
                biletNolustur();
                baglanti.Close();
                biletNoSorgula();
               
            }
            baglanti.Close();
        }

        void kaydetMETODU()
        {
            string sorgu = "insert into Tbl_Biletler(BKOD,ADSOYAD,TELNO,KOLTUKNO,FILMADI,TARIH,SAAT,SALON,TUR,ISLEMSAATI) Values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10)";
            baglanti.Close();
            baglanti.Open();
            SqlCommand kayit = new SqlCommand(sorgu, baglanti);
            kayit.Parameters.AddWithValue("@p1",txtBarkod.Text.ToString());
            kayit.Parameters.AddWithValue("@p2", txtAdsoyad.Text.ToUpper().ToString());
            kayit.Parameters.AddWithValue("@p3", txtTelNo.Text.ToString());
            kayit.Parameters.AddWithValue("@p4", tbkoltukno.Text.ToString());
            kayit.Parameters.AddWithValue("@p5", cbFilmAdi.Text.ToString());
            kayit.Parameters.AddWithValue("@p6", cbTarih.Text.ToString());
            kayit.Parameters.AddWithValue("@p7", lblSeansSec.Text.ToString());
            kayit.Parameters.AddWithValue("@p8", cbSalon.Text.ToString());
            kayit.Parameters.AddWithValue("@p9", cbTuru.Text.ToString());
            kayit.Parameters.AddWithValue("@p10",DateTime.Now.ToString());
            kayit.ExecuteNonQuery();
            baglanti.Close();

            string sorgu2 = "UPDATE Tbl_Kontrol SET KOLTUKLAR=@numara WHERE FILIMADI=@filmadi AND TARIH=@tarih AND SAAT=@saat AND SALONADI=@salonadi ";
            baglanti.Open();
            SqlCommand guncelle = new SqlCommand(sorgu2,baglanti);


            guncelle.Parameters.AddWithValue("@numara",lblgelenKoltuk.Text.ToString()+","+tbkoltukno.Text.ToString());
            guncelle.Parameters.AddWithValue("@filmadi", cbFilmAdi.Text.ToString());
            guncelle.Parameters.AddWithValue("@tarih", cbTarih.Text.ToString());
            guncelle.Parameters.AddWithValue("@saat", lblSeansSec.Text.ToString());
            guncelle.Parameters.AddWithValue("@salonadi", cbSalon.Text.ToString());
            guncelle.ExecuteNonQuery();
            baglanti.Close();

          
            MessageBox.Show("BİLET BAŞARILI BİR ŞEKİLDE OLUŞTURULDU!");
            salonDurumGeldi();

            lblgelenKoltuk.Text = "";
            listeGelenKoltuk.Items.Clear();
            lblBelirlenen.Items.Clear();
            tbkoltukno.Text = "";




        }

        private void btnOlustur_Click(object sender, EventArgs e)
        {
            if (!IsValidPhoneNumber(txtTelNo.Text))
            {
                MessageBox.Show("Lütfen geçerli bir telefon numarası giriniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtAdsoyad.Text != "" && txtBarkod.Text != "" && tbkoltukno.Text != "" && txtTelNo.Text != "" && cbTuru.Text != "" && cbFilmAdi.Text != "" && cbSalon.Text != "")
            {
                biletNoSorgula();
                salonDurumGeldi();
            }
            else
            {
                MessageBox.Show("Lütfen tüm alanları eksiksiz doldurunuz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
        private bool IsValidPhoneNumber(string phoneNumber)
        {
           
            string pattern = @"^[0-9]{10}$";
            return System.Text.RegularExpressions.Regex.IsMatch(phoneNumber, pattern);
        }



        void secTiklerimiz()
        {
            tbkoltukno.Text = "";
            foreach (string item in lblBelirlenen.Items)
            {
                tbkoltukno.Text += "," + item;
            }
            if (tbkoltukno.Text.Length > 1)
            {
                tbkoltukno.Text = tbkoltukno.Text.Substring(1);
            }
        }

        private void Btn_Click(object sender, EventArgs e)
        {


            Button btn = (Button)sender;
            if (btn.ForeColor==Color.Black)
            {
                MessageBox.Show("BU KOLTUK DOLUDUR!");
            }
            else
            {
                if (btn.ForeColor==Color.Yellow)
                {
                    btn.BackgroundImage = Image.FromFile(Path.Combine(Application.StartupPath, "resimler", "sarı.png"));
                    btn.BackgroundImageLayout = ImageLayout.Stretch;
                    btn.ImageAlign = ContentAlignment.MiddleLeft;
                    btn.ForeColor = Color.Blue;
                    lblBelirlenen.Items.Add(btn.Text);
                    secTiklerimiz();
                }
                else
                {
                    btn.BackgroundImage = Image.FromFile(Path.Combine(Application.StartupPath, "resimler", "sarı.png"));
                    btn.BackgroundImageLayout = ImageLayout.Stretch;
                    btn.ForeColor = Color.Yellow;
                    btn.ImageAlign = ContentAlignment.MiddleLeft;
                    btn.BackColor = Color.Transparent; 
                    btn.TextAlign = ContentAlignment.MiddleRight;
                    lblBelirlenen.Items.Remove(btn.Text);
                    secTiklerimiz();
                }
            }

           
        }

        private void cbSalon_SelectedIndexChanged(object sender, EventArgs e)
        {
            salonDurumGeldi();
        }

        void salonDurumGeldi()
        {
            string sorgu = "SELECT * FROM Tbl_Salonlar WHERE SALONADI=@salonadi";
            baglanti.Open();
            SqlCommand komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@salonadi", cbSalon.Text.ToString());

            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                lblKoltuksayisi.Text = oku["KOLTUKSAYISI"].ToString();
                if (lblgelenKoltuk.Text.Length > 2)
                {
                    lblgelenKoltuk.Text = lblgelenKoltuk.Text.Substring(2);
                }
            }
            baglanti.Close();
            koltukGetir();
            DOLDUR();
        }
        void DOLDUR()
        {
            KoltukPaneli.Controls.Clear();
            int sayi = Convert.ToInt16(lblKoltuksayisi.Text);
            for (int i = 1; i <= sayi; i++)
            {
                Button btn = new Button();

              
                if (i <= 10)
                {
                    btn.Text = "A" + i.ToString();
                    btn.Name = "A" + i.ToString();
                }
                else if (i <= 20)
                {
                    btn.Text = "B" + i.ToString();
                    btn.Name = "B" + i.ToString();
                }
                else if (i <= 30)
                {
                    btn.Text = "C" + i.ToString();
                    btn.Name = "C" + i.ToString();
                }
                else if (i <= 40)
                {
                    btn.Text = "D" + i.ToString();
                    btn.Name = "D" + i.ToString();
                }
                else if (i <= 50)
                {
                    btn.Text = "E" + i.ToString();
                    btn.Name = "E" + i.ToString();
                }
                else if (i <= 60)
                {
                    btn.Text = "F" + i.ToString();
                    btn.Name = "F" + i.ToString();
                }
                else if (i <= 70)
                {
                    btn.Text = "G" + i.ToString();
                    btn.Name = "G" + i.ToString();
                }
                else if (i <= 80)
                {
                    btn.Text = "H" + i.ToString();
                    btn.Name = "H" + i.ToString();
                }
                else if (i <= 90)
                {
                    btn.Text = "I" + i.ToString();
                    btn.Name = "I" + i.ToString();
                }
                else if (i <= 100)
                {
                    btn.Text = "İ" + i.ToString();
                    btn.Name = "i" + i.ToString();
                }
                if (i <= 110)
                {
                    btn.Text = "K" + i.ToString();
                    btn.Name = "K" + i.ToString();
                }
                else if (i <= 120)
                {
                    btn.Text = "L" + i.ToString();
                    btn.Name = "L" + i.ToString();
                }
                else if (i <= 130)
                {
                    btn.Text = "M" + i.ToString();
                    btn.Name = "M" + i.ToString();
                }
                else if (i <= 140)
                {
                    btn.Text = "N" + i.ToString();
                    btn.Name = "N" + i.ToString();
                }
                else if (i <= 150)
                {
                    btn.Text = "O" + i.ToString();
                    btn.Name = "O" + i.ToString();
                }
                else if (i <= 160)
                {
                    btn.Text = "Ö" + i.ToString();
                    btn.Name = "Ö" + i.ToString();
                }
                else if (i <= 170)
                {
                    btn.Text = "P" + i.ToString();
                    btn.Name = "P" + i.ToString();
                }
                else if (i <= 180)
                {
                    btn.Text = "R" + i.ToString();
                    btn.Name = "R" + i.ToString();
                }
                else if (i <= 190)
                {
                    btn.Text = "S" + i.ToString();
                    btn.Name = "S" + i.ToString();
                }
                else if (i <= 200)
                {
                    btn.Text = "Ş" + i.ToString();
                    btn.Name = "Ş" + i.ToString();
                }



                btn.Width = 50;
                btn.Height = 50;
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
                btn.Font = new System.Drawing.Font("Segoe UI Semibold", 12);

                btn.ForeColor = Color.Purple;
                btn.Click += Btn_Click;

             
                if (listeGelenKoltuk.Items.Contains(btn.Text))
                {
                   
                    btn.BackgroundImage = Image.FromFile(Path.Combine(Application.StartupPath, "resimler", "kırmızı.png"));
                    btn.BackgroundImageLayout = ImageLayout.Stretch;
                    btn.ForeColor = Color.Black;
                    btn.ImageAlign = ContentAlignment.MiddleLeft;
                    btn.BackColor = Color.Transparent; 
                    btn.TextAlign = ContentAlignment.MiddleRight;
                }
                else
                {
                    
                    btn.BackgroundImage = Image.FromFile(Path.Combine(Application.StartupPath, "resimler", "mavi.png"));
                    btn.BackgroundImageLayout = ImageLayout.Stretch;
                    btn.ForeColor = Color.Yellow;
                    btn.ImageAlign = ContentAlignment.MiddleLeft;
                    btn.BackColor = Color.Transparent; 
                    btn.TextAlign = ContentAlignment.MiddleRight;
                }

                KoltukPaneli.Controls.Add(btn);
            }
        }


        void koltukGetir()
        {
            lblgelenKoltuk.Text = "";
            string sorgu = "SELECT * FROM Tbl_Kontrol WHERE FILIMADI=@filmadi AND TARIH=@tarih AND SAAT=@saat AND SALONADI=@salonadi";
            baglanti.Open();
            SqlCommand komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@filmadi", cbFilmAdi.Text.ToString());
            komut.Parameters.AddWithValue("@tarih", cbTarih.Text.ToString());
            komut.Parameters.AddWithValue("@saat", lblSeansSec.Text.ToString());
            komut.Parameters.AddWithValue("@salonadi", cbSalon.Text.ToString());

            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
             
                lblgelenKoltuk.Text += " ," + oku["KOLTUKLAR"].ToString();
                if (lblgelenKoltuk.Text.Length>1)
                {
                    lblgelenKoltuk.Text = lblgelenKoltuk.Text.Substring(1);
                }
                else
                {
                    lblgelenKoltuk.Text = "";
                }
            }
            baglanti.Close();
            koltukAyirma();
        }

        void koltukAyirma()
        {
            listeGelenKoltuk.Items.Clear();
            string no = "";
            string[] sec;
            no = lblgelenKoltuk.Text.ToString();
            sec=no.Split(',');
            foreach (string bulunan in sec )
            {
                listeGelenKoltuk.Items.Add(bulunan);
            }
        }

        private void cmTuru_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            lblgelenKoltuk.Text = "";
            lblKoltuksayisi.Text = "";
            lblSeansSec.Text = "";
            lblYonetmenara.Text = "";
            txtAdsoyad.Text = "";
            txtTelNo.Text = "";
            tbkoltukno.Text = "";
            cbTuru.Text = "";
            txtBarkod.Text = "";
            cbSalon.Text = "";
            cbTarih.Text = "";
            cbFilmAdi.Text = "";
            cbSalon.Items.Clear();
            cbTarih.Items.Clear();
            cbTuru.Items.Clear();
            cbTuru.Items.Add("YETİŞKİN(215TL)");
            cbTuru.Items.Add("ÖĞRENCİ(200TL)");
            cbTuru.Items.Add("HALK GÜNÜ-YETİŞKİN(185TL)");
            cbTuru.Items.Add("HALK GÜNÜ-ÖĞRENCİ(170TL)");

            biletNolustur();
            panelSeans.Controls.Clear();
            KoltukPaneli.Controls.Clear();
            lblBelirlenen.Items.Clear();
            cbFilmAdi.Items.Clear();
            listeGelenKoltuk.Items.Clear();
            filmAdiGetir();
            txtAdsoyad.Focus();
            

           




        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}
