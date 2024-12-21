using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartTicket.comV1
{
    public partial class FrmAnaForm : Form
    {
        public FrmAnaForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmAcilis1 frm = new FrmAcilis1();
            frm.Show(); // Modal yerine modeless olarak aç

            this.Close(); // Mevcut formu hemen kapat

        }
     public string kisiAdiSoyadi = "";

        private void FrmAnaForm_Load(object sender, EventArgs e)
        {
           // MessageBox.Show(kisiAdiSoyadi);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FrmYonetmenKayit frm = new FrmYonetmenKayit();
            frm.ShowDialog();
            
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            FrmYonetmenlerListesi frm = new FrmYonetmenlerListesi();
            frm.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FrmOyuncuKayit frm = new FrmOyuncuKayit();
            frm.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FrmOyuncuListesi frm = new FrmOyuncuListesi();
            frm.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            FrmSalonKayit frm =new FrmSalonKayit();
            frm.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            FrmFilimKayit frm=new FrmFilimKayit();
            frm.ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            FrmFilmListe frm = new FrmFilmListe();
            frm.ShowDialog();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            FrmCalisanlarKayit frm = new FrmCalisanlarKayit();
            frm.ShowDialog();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            FrmCalisanListesi frm = new FrmCalisanListesi();
            frm.ShowDialog();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            FrmFilmKontrolEkranı frm = new FrmFilmKontrolEkranı();
            frm.ShowDialog();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            FrmKontrolListeEkranı frm = new FrmKontrolListeEkranı();
            frm.ShowDialog();

        }
    }
}
