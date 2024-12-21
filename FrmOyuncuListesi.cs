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
    public partial class FrmOyuncuListesi : Form
    {
        public FrmOyuncuListesi()
        {
            InitializeComponent();
        }
       

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        SqlConnection baglanti = new SqlConnection(@"Server=.\SQLEXPRESS;Initial Catalog=SmarTicket;Integrated Security=True");

        private void FrmOyuncuListesi_Load(object sender, EventArgs e)
        {
            ListePaneli.Controls.Clear();
            baglanti.Open();
            string sorgu = "select * from Tbl_Oyuncular ORDER BY ADSOYAD ASC";
            SqlCommand komut = new SqlCommand(sorgu, baglanti);
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                OyuncuListesi arac = new OyuncuListesi();
                arac.lblId.Text = oku["ID"].ToString();
                arac.lblAdSoyad.Text = oku["ADSOYAD"].ToString();
                arac.pbResimDetay.ImageLocation = oku["RESIM"].ToString();
                ListePaneli.Controls.Add(arac);
            }
            baglanti.Close();
        }

        private void textAramaYap_TextChanged(object sender, EventArgs e)
        {
            ListePaneli.Controls.Clear();
            baglanti.Open();
            SqlCommand ara = new SqlCommand("select * from Tbl_Oyuncular Where ADSOYAD LIKE '%" + textAramaYap.Text + "%' collate Turkish_CI_AS ORDER BY ADSOYAD ASC", baglanti);
            SqlDataReader oku = ara.ExecuteReader();
            while (oku.Read())
            {
               OyuncuListesi arac = new OyuncuListesi();
                arac.lblId.Text = oku["ID"].ToString();
                arac.lblAdSoyad.Text = oku["ADSOYAD"].ToString();

                // Orijinal fotoğrafı değiştirmiyoruz, sadece cinsiyet simgesini yüklüyoruz
                string cinsiyet = oku["CINSIYET"].ToString(); // Örneğin 0 - Erkek, 1 - Kadın

                // Cinsiyet kontrolüne göre resim ekleme
                if (cinsiyet == "0")
                {
                    // Erkek simgesi
                    arac.pbCinsiyet.ImageLocation = Path.Combine(Application.StartupPath, "resimler", "erkek.png");
                }
                else if (cinsiyet == "1")
                {
                    // Kadın simgesi
                    arac.pbCinsiyet.ImageLocation = Path.Combine(Application.StartupPath, "resimler", "kadın.png");
                }
                else
                {
                    // Varsayılan resim (Eğer cinsiyet bilgisi yoksa)
                    arac.pbCinsiyet.ImageLocation = Path.Combine(Application.StartupPath, "resimler", "default.png");
                }

                // Diğer bilgileri ekliyoruz
                arac.pbResimDetay.ImageLocation = oku["RESIM"].ToString(); // Orijinal fotoğrafı yükler

                ListePaneli.Controls.Add(arac);
            }
            baglanti.Close();
        }

        private void ListePaneli_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
