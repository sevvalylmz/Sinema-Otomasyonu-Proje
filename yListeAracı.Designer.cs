namespace SmartTicket.comV1
{
    partial class yListeAracı
    {
        /// <summary> 
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Bileşen Tasarımcısı üretimi kod

        /// <summary> 
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblAdi = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblAdi
            // 
            this.lblAdi.AutoSize = true;
            this.lblAdi.Font = new System.Drawing.Font("Segoe UI Semibold", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblAdi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(28)))), ((int)(((byte)(43)))));
            this.lblAdi.Location = new System.Drawing.Point(2, 11);
            this.lblAdi.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblAdi.Name = "lblAdi";
            this.lblAdi.Size = new System.Drawing.Size(36, 13);
            this.lblAdi.TabIndex = 1;
            this.lblAdi.Text = "label1";
            this.lblAdi.Click += new System.EventHandler(this.lblAdi_Click);
            this.lblAdi.MouseLeave += new System.EventHandler(this.lblAdi_MouseLeave);
            this.lblAdi.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lblAdi_MouseMove);
            // 
            // yListeAracı
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.lblAdi);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "yListeAracı";
            this.Size = new System.Drawing.Size(83, 36);
            this.Load += new System.EventHandler(this.yListeAracı_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label lblAdi;
    }
}
