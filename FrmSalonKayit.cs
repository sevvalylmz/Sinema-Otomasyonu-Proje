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
    public partial class FrmSalonKayit : Form
    {
        public FrmSalonKayit()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Server=.\SQLEXPRESS;Initial Catalog=SmarTicket;Integrated Security=True");

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textSalonAdi.Text!=""&& cbKoltukSayisi.Text!="")
            {
                baglanti.Open();
                SqlCommand kaydet = new SqlCommand("insert into Tbl_Salonlar (SALONADI,KOLTUKSAYISI) Values(@p1,@p2)",baglanti);
                kaydet.Parameters.AddWithValue("@p1", textSalonAdi.Text.ToUpper());
                kaydet.Parameters.AddWithValue("@p2", cbKoltukSayisi.Text);
                kaydet.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Salon Kaydetme İşlemi Başarılı");
                textSalonAdi.Text = "";
                cbKoltukSayisi.Text = "";
                textSalonAdi.Focus();
                listeGetir();

            }
            else
            {
                MessageBox.Show("Lütfen Bir Değer Giriniz!");
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void FrmSalonKayit_Load(object sender, EventArgs e)
        {
            listeGetir();
            kOlustur();
        }
        void kOlustur()
        {
            for (int i = 0; i < 200; i++)
            {
                cbKoltukSayisi.Items.Add(i);
            }
        }
        void listeGetir()
        {
            panelSalon.Controls.Clear();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from Tbl_Salonlar ORDER BY SALONADI ASC", baglanti);
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                SalonListesi arac = new SalonListesi();
                arac.lblSalonAdı.Text = oku["SALONADI"].ToString();
                arac.lblKoltukSayısı.Text = oku["KOLTUKSAYISI"].ToString();
                panelSalon.Controls.Add(arac);
            }
            baglanti.Close();
        }

        private void panelSalon_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
