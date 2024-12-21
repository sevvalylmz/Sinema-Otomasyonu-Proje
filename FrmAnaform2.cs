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
    public partial class FrmAnaform2 : Form
    {
        public FrmAnaform2()
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
        private void FrmAnaform2_Load(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            FrmBiletOlustur frm = new FrmBiletOlustur();
            frm.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FrmBiletSorgula frm = new FrmBiletSorgula();
            frm.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FrmFilmListe2 frm = new FrmFilmListe2();
            frm.ShowDialog();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}                                                                                                                                                                                                                                                                
