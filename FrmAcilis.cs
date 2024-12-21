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
    public partial class FrmAcilis : Form
    {
        public FrmAcilis()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(@"Server=.\SQLEXPRESS;Initial Catalog=SmarTicket;Integrated Security=True");




        private void button1_Click(object sender, EventArgs e)
        {
           this.Close();
        }

        private void btnGirisYap_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand sorgula = new SqlCommand("select * from Tbl_Kullaniciler WHERE KADI=@username AND KSIFRE=@password", baglanti);
            sorgula.Parameters.AddWithValue("@username", txtKullaniciAdi.Text);
            sorgula.Parameters.AddWithValue("@password", txtSifre.Text);
            SqlDataReader oku = sorgula.ExecuteReader();
            if (oku.Read())
            {
                //  MessageBox.Show("Giriş Başarılı!");
                FrmAnaForm frm = new FrmAnaForm();
                frm.kisiAdiSoyadi = oku["ADSOYAD"].ToString();
                frm.Show();
                this.Hide();

            }
            else
            {
                MessageBox.Show("Kullanıcı Kaydı Bululnamadı!");
            }

            baglanti.Close();

            txtKullaniciAdi.Text = "";
            txtSifre.Text = "";
            txtKullaniciAdi.Focus();

          
            //bağlantı kontrol
          //  baglanti.Open();
          //  if (baglanti.State==ConnectionState.Open)
          //  {
             //   MessageBox.Show("Başarılı");
          //  }
          //  baglanti.Close();
        }

        private void FrmAcilis_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
