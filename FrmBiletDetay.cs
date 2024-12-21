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

namespace SmartTicket.comV1
{
    public partial class FrmBiletDetay : Form
    {
        public FrmBiletDetay()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Server=.\SQLEXPRESS;Initial Catalog=SmarTicket;Integrated Security=True");
        public  string biletNo = "";
        private void FrmBiletDetay_Load(object sender, EventArgs e)
        {
         
            lblBiletNo.Text = biletNo;
            lblBiletNo2.Text = biletNo;
            barkodNolustur();
            bilgiGetir();
        }
        void barkodNolustur()
        {
            Random rastgele = new Random();
            string karakterler = "012345678901234567890123456789012345678901234567890123456789";
            string kod = "";

            for (int i = 0; i < 11; i++)
            {
                kod += karakterler[rastgele.Next(karakterler.Length)];
            }
            lblBarkod1.Text = kod.ToString();
            lblBarkod1.Text= kod.ToString();
        }

            public    void bilgiGetir()
        {
            string sorgu = "select * from Tbl_Biletler where BKOD=@kod";
            baglanti.Open();
            SqlCommand getir = new SqlCommand(sorgu,baglanti);
            getir.Parameters.AddWithValue("@kod",biletNo);
            SqlDataReader oku = getir.ExecuteReader();

            if (oku.Read())
            {
                lblFilmAdi1.Text = oku["FILMADI"].ToString();
                lblFilmAdi2.Text = oku["FILMADI"].ToString() ;
                lblTelNo3.Text = oku["TELNO"].ToString();
                lblAdSoyad3.Text = oku["ADSOYAD"].ToString();
                lblBiletTuru3.Text = oku["TUR"].ToString();
                lblSalonAdi.Text = oku["SALON"].ToString();
                lblSalon3.Text = oku["SALON"].ToString();
                lblTarihSaat.Text = oku["TARIH"] + " " + oku["SAAT"].ToString();
                lblTarih3.Text = oku["TARIH"].ToString() + " " + oku["SAAT"].ToString();
                lblIslemTarihi.Text = oku["ISLEMSAATI"].ToString();
                lblKoltuk1.Text = oku["KOLTUKNO"].ToString();
                lblKoltuk2.Text = oku["KOLTUKNO"].ToString();
            }

            baglanti.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
