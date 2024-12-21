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
using System.Diagnostics.Eventing.Reader;

namespace SmartTicket.comV1
{
    public partial class FrmBiletSorgula : Form
    {
        public FrmBiletSorgula()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Server=.\SQLEXPRESS;Initial Catalog=SmarTicket;Integrated Security=True");

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSorgula_Click(object sender, EventArgs e)
        {
            if (txtBiletNo.Text != "")
            {

            }

            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from Tbl_Biletler WHERE BKOD=@p1", baglanti);
            komut.Parameters.AddWithValue("@p1", txtBiletNo.Text.ToString());
            SqlDataReader oku = komut.ExecuteReader();
            if (oku.Read())
            {
                FrmBiletDetay frm = new FrmBiletDetay();
                frm.biletNo = txtBiletNo.Text.ToString();
                txtBiletNo.Text = "";
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("KAYITLI BİLET BULUNAMADI!");
                baglanti.Close();
            }
            baglanti.Close();
        }

        private void FrmBiletSorgula_Load(object sender, EventArgs e)
        {

        }
    }
}
