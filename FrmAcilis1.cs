using System;
using System.Windows.Forms;

namespace SmartTicket.comV1
{
    public partial class FrmAcilis1 : Form
    {
        public FrmAcilis1()
        {
            InitializeComponent();
        }

        private void FrmAcilis1_Load(object sender, EventArgs e)
        {
            
        }

        private void btnGirisYap_Click(object sender, EventArgs e)
        {
            // FrmAcilis1 formunu gizle
            this.Hide();

            // FrmAcilis formunu aç
            FrmAcilis frm = new FrmAcilis();
            frm.FormClosing += Frm_FormClosing; // FrmAcilis formu kapatıldığında FrmAcilis1 formunu yeniden göster
            frm.Show();  // FrmAcilis formunu göster
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Uygulamayı kapat
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // FrmAcilis1 formunu gizle
            this.Hide();

            // FrmBSDgirisi formunu aç
            FrmBSDgirisi frm = new FrmBSDgirisi();
            frm.FormClosing += Frm_FormClosing; // FrmBSDgirisi formu kapatıldığında FrmAcilis1 formunu yeniden göster
            frm.Show();  // FrmBSDgirisi formunu göster
        }

        // Form kapanmadan önce FrmAcilis1 formunu tekrar göster
        private void Frm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Show();  // FrmAcilis1 formunu tekrar göster
        }

       
    }
}
