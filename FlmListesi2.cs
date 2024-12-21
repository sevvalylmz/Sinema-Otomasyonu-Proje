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
    public partial class FlmListesi2 : UserControl
    {
        public FlmListesi2()
        {
            InitializeComponent();
        }

        private void groupBox9_Enter(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

            FrmFilmDetayEkrani2 frm = new FrmFilmDetayEkrani2();
            frm.idNo = lblIdNo.Text;
            frm.ShowDialog();
        }
    }
}
