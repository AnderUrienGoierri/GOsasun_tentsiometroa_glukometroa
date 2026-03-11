namespace tentsiometro_digitala
{
    partial class LeihoNagusia
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblOrduaData;
        private System.Windows.Forms.Label lblMezua;
        private System.Windows.Forms.Label lblEmaitzak;
        private System.Windows.Forms.Label lblSysDiaPul;
        private System.Windows.Forms.Button btnHasi;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer aginteTimer;
        private System.Windows.Forms.PictureBox picLogo;
        private System.Windows.Forms.PictureBox picTentsiometroa;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lblOrduaData = new System.Windows.Forms.Label();
            this.lblMezua = new System.Windows.Forms.Label();
            this.lblSysDiaPul = new System.Windows.Forms.Label();
            this.lblEmaitzak = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.btnHasi = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.aginteTimer = new System.Windows.Forms.Timer(this.components);
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.picTentsiometroa = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picTentsiometroa)).BeginInit();
            this.picTentsiometroa.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblOrduaData
            // 
            this.lblOrduaData.BackColor = System.Drawing.Color.Transparent;
            this.lblOrduaData.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.lblOrduaData.Location = new System.Drawing.Point(48, 109);
            this.lblOrduaData.Name = "lblOrduaData";
            this.lblOrduaData.Size = new System.Drawing.Size(180, 20);
            this.lblOrduaData.TabIndex = 0;
            this.lblOrduaData.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblMezua
            // 
            this.lblMezua.BackColor = System.Drawing.Color.Transparent;
            this.lblMezua.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold);
            this.lblMezua.Location = new System.Drawing.Point(59, 129);
            this.lblMezua.Name = "lblMezua";
            this.lblMezua.Size = new System.Drawing.Size(140, 38);
            this.lblMezua.TabIndex = 1;
            this.lblMezua.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblMezua.Click += new System.EventHandler(this.lblMezua_Click);
            // 
            // lblSysDiaPul
            // 
            this.lblSysDiaPul.AutoSize = true;
            this.lblSysDiaPul.BackColor = System.Drawing.Color.Transparent;
            this.lblSysDiaPul.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold);
            this.lblSysDiaPul.ForeColor = System.Drawing.Color.Black;
            this.lblSysDiaPul.Location = new System.Drawing.Point(65, 175);
            this.lblSysDiaPul.Name = "lblSysDiaPul";
            this.lblSysDiaPul.Size = new System.Drawing.Size(0, 18);
            this.lblSysDiaPul.TabIndex = 2;
            // 
            // lblEmaitzak
            // 
            this.lblEmaitzak.BackColor = System.Drawing.Color.Transparent;
            this.lblEmaitzak.Font = new System.Drawing.Font("Consolas", 24F, System.Drawing.FontStyle.Bold);
            this.lblEmaitzak.ForeColor = System.Drawing.Color.Black;
            this.lblEmaitzak.Location = new System.Drawing.Point(156, 305);
            this.lblEmaitzak.Name = "lblEmaitzak";
            this.lblEmaitzak.Size = new System.Drawing.Size(130, 90);
            this.lblEmaitzak.TabIndex = 3;
            this.lblEmaitzak.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(50, 265);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(133, 13);
            this.progressBar.TabIndex = 0;
            this.progressBar.Visible = false;
            // 
            // btnHasi
            // 
            this.btnHasi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(121)))), ((int)(((byte)(194)))), ((int)(((byte)(135)))));
            this.btnHasi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHasi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHasi.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.btnHasi.ForeColor = System.Drawing.Color.White;
            this.btnHasi.Location = new System.Drawing.Point(279, 273);
            this.btnHasi.Name = "btnHasi";
            this.btnHasi.Size = new System.Drawing.Size(53, 25);
            this.btnHasi.TabIndex = 1;
            this.btnHasi.Text = "HASI";
            this.btnHasi.UseVisualStyleBackColor = false;
            this.btnHasi.Click += new System.EventHandler(this.BtnHasi_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 50;
            this.timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // aginteTimer
            // 
            this.aginteTimer.Interval = 1000;
            this.aginteTimer.Tick += new System.EventHandler(this.AginteTimer_Tick);
            // 
            // picLogo
            // 
            this.picLogo.Location = new System.Drawing.Point(74, 61);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(300, 80);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picLogo.TabIndex = 1;
            this.picLogo.TabStop = false;
            // 
            // picTentsiometroa
            // 
            this.picTentsiometroa.Controls.Add(this.lblOrduaData);
            this.picTentsiometroa.Controls.Add(this.lblMezua);
            this.picTentsiometroa.Controls.Add(this.lblSysDiaPul);
            this.picTentsiometroa.Controls.Add(this.progressBar);
            this.picTentsiometroa.Controls.Add(this.btnHasi);
            this.picTentsiometroa.Location = new System.Drawing.Point(37, 102);
            this.picTentsiometroa.Name = "picTentsiometroa";
            this.picTentsiometroa.Size = new System.Drawing.Size(380, 480);
            this.picTentsiometroa.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picTentsiometroa.TabIndex = 0;
            this.picTentsiometroa.TabStop = false;
            this.picTentsiometroa.Click += new System.EventHandler(this.picTentsiometroa_Click);
            // 
            // LeihoNagusia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(457, 586);
            this.Controls.Add(this.picLogo);
            this.Controls.Add(this.picTentsiometroa);
            this.Controls.Add(this.lblEmaitzak);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "LeihoNagusia";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tentsiometro Digitala";
            this.Load += new System.EventHandler(this.LeihoNagusia_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picTentsiometroa)).EndInit();
            this.picTentsiometroa.ResumeLayout(false);
            this.picTentsiometroa.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
    }
}
