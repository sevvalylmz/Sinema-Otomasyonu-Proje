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
    public partial class oListeAracı : UserControl
    {
        public oListeAracı()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Server=.\SQLEXPRESS;Initial Catalog=SmarTicket;Integrated Security=True");

        private void lbloAdi_Click(object sender, EventArgs e)
        {
            if (lbloAdi.ForeColor == Color.FromArgb(17, 28, 43))
            {
                lbloAdi.ForeColor = Color.FromArgb(249, 164, 26);
                baglanti.Open();
                SqlCommand komut = new SqlCommand("insert into Tbl_Secilenler (KISI,TUR) values (@kisi, @tur)", baglanti);
                komut.Parameters.AddWithValue("@kisi", lbloAdi.Text);
                komut.Parameters.AddWithValue("tur", "OYUNCU");
                komut.ExecuteNonQuery();
                baglanti.Close();
            }
            else
            {
                lbloAdi.ForeColor = Color.FromArgb(17, 28, 43);
                baglanti.Open();
                SqlCommand komut = new SqlCommand("delete from Tbl_Secilenler where KISI=@kisi AND TUR=@tur", baglanti);
                komut.Parameters.AddWithValue("@kisi", lbloAdi.Text);
                komut.Parameters.AddWithValue("tur", "OYUNCU");
                komut.ExecuteNonQuery();
                baglanti.Close();
            }
        }

        private void lbloAdi_MouseMove(object sender, MouseEventArgs e)
        {
            lbloAdi.Font = new Font(lbloAdi.Font.Name, 9, FontStyle.Regular);
        }

        private void lbloAdi_MouseLeave(object sender, EventArgs e)
        {
            lbloAdi.Font = new Font(lbloAdi.Font.Name, 8, FontStyle.Regular);
        }

        private void oListeAracı_Load(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from Tbl_Secilenler Where KISI=@kisi AND TUR=@tur", baglanti);
            komut.Parameters.AddWithValue("@kisi", lbloAdi.Text);
            komut.Parameters.AddWithValue("@tur", "OYUNCU");
            SqlDataReader oku = komut.ExecuteReader();
            if (oku.Read())
            {
                lbloAdi.ForeColor = Color.FromArgb(249, 164, 26);
            }
            else
            {
                lbloAdi.ForeColor = Color.FromArgb(17, 28, 43);
            }
            baglanti.Close();
        }
    }
}
