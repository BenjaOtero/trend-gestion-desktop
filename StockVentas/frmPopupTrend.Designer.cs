namespace StockVentas
{
    partial class frmPopupTrend
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
            this.pictureBoxBoton = new System.Windows.Forms.PictureBox();
            this.pictureBoxPromo = new System.Windows.Forms.PictureBox();
            this.btnInfo = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBoton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPromo)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxBoton
            // 
            this.pictureBoxBoton.Location = new System.Drawing.Point(372, 12);
            this.pictureBoxBoton.Name = "pictureBoxBoton";
            this.pictureBoxBoton.Size = new System.Drawing.Size(16, 14);
            this.pictureBoxBoton.TabIndex = 3;
            this.pictureBoxBoton.TabStop = false;
            this.pictureBoxBoton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBoxBoton_MouseDown);
            this.pictureBoxBoton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBoxBoton_MouseUp);
            // 
            // pictureBoxPromo
            // 
            this.pictureBoxPromo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxPromo.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxPromo.Name = "pictureBoxPromo";
            this.pictureBoxPromo.Size = new System.Drawing.Size(400, 200);
            this.pictureBoxPromo.TabIndex = 4;
            this.pictureBoxPromo.TabStop = false;
            // 
            // btnInfo
            // 
            this.btnInfo.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInfo.Location = new System.Drawing.Point(143, 145);
            this.btnInfo.Name = "btnInfo";
            this.btnInfo.Size = new System.Drawing.Size(115, 31);
            this.btnInfo.TabIndex = 5;
            this.btnInfo.Text = "Mas info . . .";
            this.btnInfo.UseVisualStyleBackColor = false;
            this.btnInfo.Click += new System.EventHandler(this.btnInfo_Click);
            // 
            // frmPopupTrend
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 200);
            this.ControlBox = false;
            this.Controls.Add(this.btnInfo);
            this.Controls.Add(this.pictureBoxBoton);
            this.Controls.Add(this.pictureBoxPromo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmPopupTrend";
            this.ShowInTaskbar = false;
            this.Deactivate += new System.EventHandler(this.frmPopupTrend_Deactivate);
            this.Load += new System.EventHandler(this.frmPopupTrend_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBoton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPromo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxBoton;
        private System.Windows.Forms.PictureBox pictureBoxPromo;
        private System.Windows.Forms.Button btnInfo;
    }
}