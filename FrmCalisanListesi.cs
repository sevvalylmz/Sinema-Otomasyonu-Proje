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

namespace SmartTicket.comV1
{
    public partial class FrmCalisanListesi : Form
    {
        public FrmCalisanListesi()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Server=.\SQLEXPRESS;Initial Catalog=SmarTicket;Integrated Security=True");

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void ListePaneli_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FrmCalisanListesi_Load(object sender, EventArgs e)
        {
            ListePaneli.Controls.Clear();
            baglanti.Open();
            string sorgu = "select * from Tbl_Calisanlar ORDER BY ADSOYAD ASC";
            SqlCommand komut = new SqlCommand(sorgu, baglanti);
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                CalisanlarListesi arac = new CalisanlarListesi();
                arac.lblId.Text = oku["ID"].ToString();
                arac.lblAdSoyad.Text = oku["ADSOYAD"].ToString();
               
                ListePaneli.Controls.Add(arac);
            }
            baglanti.Close();
        }

        private void textAramaYap_TextChanged(object sender, EventArgs e)
        {
            ListePaneli.Controls.Clear();
            baglanti.Open();
            SqlCommand ara = new SqlCommand("select * from Tbl_Calisanlar Where ADSOYAD LIKE '%" + textAramaYap.Text + "%' ORDER BY ADSOYAD ASC", baglanti);
            SqlDataReader oku = ara.ExecuteReader();
            while (oku.Read())
            {
                CalisanlarListesi arac = new CalisanlarListesi();
                arac.lblId.Text = oku["ID"].ToString();
                arac.lblAdSoyad.Text = oku["ADSOYAD"].ToString();
           
                ListePaneli.Controls.Add(arac);
            }
            baglanti.Close();
        }

        private void ListePaneli_Paint_1(object sender, PaintEventArgs e)
        {

        }

        

    }
}
