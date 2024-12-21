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
    public partial class FlmListesi : UserControl
    {
        public FlmListesi()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Server=.\SQLEXPRESS;Initial Catalog=SmarTicket;Integrated Security=True");


        private void button2_Click(object sender, EventArgs e)
        {
            FrmFilmDetay frm = new FrmFilmDetay();
            frm.idNo= lblIdNo.Text; 
            frm.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Onay mesajını göster
                DialogResult result = MessageBox.Show(
                    "Bu kaydı silmek istediğinize emin misiniz?",
                    "Silme Onayı",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                // Kullanıcı "Evet" butonuna bastıysa silme işlemini yap
                if (result == DialogResult.Yes)
                {
                    if (baglanti.State == ConnectionState.Closed)
                    {
                        baglanti.Open();
                    }

                    SqlCommand sil = new SqlCommand("DELETE FROM Tbl_Filmler WHERE ID=@p1", baglanti);
                    sil.Parameters.AddWithValue("@p1", lblIdNo.Text);
                    sil.ExecuteNonQuery();

                    MessageBox.Show(lblFilmAdi.Text + " adlı filme ait kayıt başarılı bir şekilde silinmiştir.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Formu gizle
                    this.Hide();
                }
                else
                {
                    // Kullanıcı "Hayır" butonuna bastıysa
                    MessageBox.Show("Silme işlemi iptal edildi.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (baglanti.State == ConnectionState.Open)
                {
                    baglanti.Close();
                }
            }

        }

        private void groupBox9_Enter(object sender, EventArgs e)
        {

        }
    }
}
