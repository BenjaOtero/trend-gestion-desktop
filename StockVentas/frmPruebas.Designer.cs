namespace StockVentas
{
    partial class frmPruebas
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.btnCompararStock = new System.Windows.Forms.Button();
            this.txtWebRequest = new System.Windows.Forms.Button();
            this.btnUploadImagen = new System.Windows.Forms.Button();
            this.btnDownLoadImage = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.btnLeftBottom = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(129, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Actualizar códigos";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 70);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(129, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Actualizar precios";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(12, 41);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(129, 23);
            this.button3.TabIndex = 2;
            this.button3.Text = "Test AFIP 2";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // btnCompararStock
            // 
            this.btnCompararStock.Location = new System.Drawing.Point(12, 99);
            this.btnCompararStock.Name = "btnCompararStock";
            this.btnCompararStock.Size = new System.Drawing.Size(129, 23);
            this.btnCompararStock.TabIndex = 1;
            this.btnCompararStock.Text = "Comparar stock";
            this.btnCompararStock.UseVisualStyleBackColor = true;
            this.btnCompararStock.Click += new System.EventHandler(this.btnCompararStock_Click);
            // 
            // txtWebRequest
            // 
            this.txtWebRequest.Location = new System.Drawing.Point(12, 129);
            this.txtWebRequest.Name = "txtWebRequest";
            this.txtWebRequest.Size = new System.Drawing.Size(129, 23);
            this.txtWebRequest.TabIndex = 3;
            this.txtWebRequest.Text = "Añadir clientes facebook";
            this.txtWebRequest.UseVisualStyleBackColor = true;
            this.txtWebRequest.Click += new System.EventHandler(this.txtWebRequest_Click);
            // 
            // btnUploadImagen
            // 
            this.btnUploadImagen.Location = new System.Drawing.Point(12, 158);
            this.btnUploadImagen.Name = "btnUploadImagen";
            this.btnUploadImagen.Size = new System.Drawing.Size(129, 23);
            this.btnUploadImagen.TabIndex = 3;
            this.btnUploadImagen.Text = "Subir imagen mysql";
            this.btnUploadImagen.UseVisualStyleBackColor = true;
            this.btnUploadImagen.Click += new System.EventHandler(this.btnUploadImagen_Click);
            // 
            // btnDownLoadImage
            // 
            this.btnDownLoadImage.Location = new System.Drawing.Point(12, 187);
            this.btnDownLoadImage.Name = "btnDownLoadImage";
            this.btnDownLoadImage.Size = new System.Drawing.Size(129, 23);
            this.btnDownLoadImage.TabIndex = 3;
            this.btnDownLoadImage.Text = "Descargar imagen mysql";
            this.btnDownLoadImage.UseVisualStyleBackColor = true;
            this.btnDownLoadImage.Click += new System.EventHandler(this.btnDownLoadImage_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(12, 216);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(129, 226);
            this.pictureBox2.TabIndex = 4;
            this.pictureBox2.TabStop = false;
            // 
            // btnLeftBottom
            // 
            this.btnLeftBottom.Location = new System.Drawing.Point(158, 12);
            this.btnLeftBottom.Name = "btnLeftBottom";
            this.btnLeftBottom.Size = new System.Drawing.Size(129, 23);
            this.btnLeftBottom.TabIndex = 3;
            this.btnLeftBottom.Text = "Left bottom form";
            this.btnLeftBottom.UseVisualStyleBackColor = true;
            this.btnLeftBottom.Click += new System.EventHandler(this.btnLeftBottom_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(179, 78);
            this.progressBar1.MarqueeAnimationSpeed = 40;
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(268, 15);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar1.TabIndex = 5;
            this.progressBar1.UseWaitCursor = true;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // frmPruebas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(566, 483);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.btnLeftBottom);
            this.Controls.Add(this.btnDownLoadImage);
            this.Controls.Add(this.btnUploadImagen);
            this.Controls.Add(this.txtWebRequest);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.btnCompararStock);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "frmPruebas";
            this.Text = "frmPruebas";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmPruebas_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button btnCompararStock;
        private System.Windows.Forms.Button txtWebRequest;
        private System.Windows.Forms.Button btnUploadImagen;
        private System.Windows.Forms.Button btnDownLoadImage;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button btnLeftBottom;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}