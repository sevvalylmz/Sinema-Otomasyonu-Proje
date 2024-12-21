namespace SmartTicket.comV1
{
    partial class YonetmenlerListesi
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(YonetmenlerListesi));
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblAdSoyad = new System.Windows.Forms.Label();
            this.lblId = new System.Windows.Forms.Label();
            this.pbResimDetay = new System.Windows.Forms.PictureBox();
            this.pbCinsiyet = new System.Windows.Forms.PictureBox();
            this.lblCinsiyet = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbResimDetay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCinsiyet)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(110)))), ((int)(((byte)(122)))));
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(5, 114);
            this.panel1.TabIndex = 0;
            // 
            // lblAdSoyad
            // 
            this.lblAdSoyad.AutoSize = true;
            this.lblAdSoyad.Font = new System.Drawing.Font("Segoe UI Semibold", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblAdSoyad.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(110)))), ((int)(((byte)(122)))));
            this.lblAdSoyad.Location = new System.Drawing.Point(141, 5);
            this.lblAdSoyad.Name = "lblAdSoyad";
            this.lblAdSoyad.Size = new System.Drawing.Size(73, 31);
            this.lblAdSoyad.TabIndex = 1;
            this.lblAdSoyad.Text = "label1";
            // 
            // lblId
            // 
            this.lblId.AutoSize = true;
            this.lblId.Font = new System.Drawing.Font("Segoe UI Semibold", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblId.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(110)))), ((int)(((byte)(122)))));
            this.lblId.Location = new System.Drawing.Point(141, 47);
            this.lblId.Name = "lblId";
            this.lblId.Size = new System.Drawing.Size(73, 31);
            this.lblId.TabIndex = 2;
            this.lblId.Text = "label1";
            this.lblId.Visible = false;
            // 
            // pbResimDetay
            // 
            this.pbResimDetay.Image = ((System.Drawing.Image)(resources.GetObject("pbResimDetay.Image")));
            this.pbResimDetay.Location = new System.Drawing.Point(11, 5);
            this.pbResimDetay.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pbResimDetay.Name = "pbResimDetay";
            this.pbResimDetay.Size = new System.Drawing.Size(85, 97);
            this.pbResimDetay.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbResimDetay.TabIndex = 3;
            this.pbResimDetay.TabStop = false;
            // 
            // pbCinsiyet
            // 
            this.pbCinsiyet.Image = ((System.Drawing.Image)(resources.GetObject("pbCinsiyet.Image")));
            this.pbCinsiyet.Location = new System.Drawing.Point(101, 11);
            this.pbCinsiyet.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pbCinsiyet.Name = "pbCinsiyet";
            this.pbCinsiyet.Size = new System.Drawing.Size(43, 25);
            this.pbCinsiyet.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbCinsiyet.TabIndex = 23;
            this.pbCinsiyet.TabStop = false;
            this.pbCinsiyet.Click += new System.EventHandler(this.pbCinsiyet_Click);
            // 
            // lblCinsiyet
            // 
            this.lblCinsiyet.AutoSize = true;
            this.lblCinsiyet.Font = new System.Drawing.Font("Segoe UI Semibold", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblCinsiyet.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(110)))), ((int)(((byte)(122)))));
            this.lblCinsiyet.Location = new System.Drawing.Point(220, 30);
            this.lblCinsiyet.Name = "lblCinsiyet";
            this.lblCinsiyet.Size = new System.Drawing.Size(73, 31);
            this.lblCinsiyet.TabIndex = 24;
            this.lblCinsiyet.Text = "label1";
            this.lblCinsiyet.Visible = false;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(110)))), ((int)(((byte)(122)))));
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Segoe UI Semibold", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(539, 65);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(93, 37);
            this.button1.TabIndex = 25;
            this.button1.Text = "SİL";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.Peru;
            this.button3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Segoe UI Semibold", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.Location = new System.Drawing.Point(362, 65);
            this.button3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(138, 37);
            this.button3.TabIndex = 26;
            this.button3.Text = "DÜZENLE";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // YonetmenlerListesi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lblCinsiyet);
            this.Controls.Add(this.pbCinsiyet);
            this.Controls.Add(this.pbResimDetay);
            this.Controls.Add(this.lblId);
            this.Controls.Add(this.lblAdSoyad);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "YonetmenlerListesi";
            this.Size = new System.Drawing.Size(666, 114);
            this.Load += new System.EventHandler(this.YonetmenlerListesi_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbResimDetay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCinsiyet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Label lblAdSoyad;
        public System.Windows.Forms.Label lblId;
        public System.Windows.Forms.PictureBox pbResimDetay;
        private System.Windows.Forms.PictureBox pbCinsiyet;
        public System.Windows.Forms.Label lblCinsiyet;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button3;
    }
}
