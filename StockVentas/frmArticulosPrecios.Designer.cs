namespace StockVentas
{
    partial class frmArticulosPrecios
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
            this.dgvDatos = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSalir = new System.Windows.Forms.Button();
            this.btnGrabar = new System.Windows.Forms.Button();
            this.grpCampos = new System.Windows.Forms.GroupBox();
            this.txtMayor = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPublico = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCosto = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.grpCampos.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvDatos
            // 
            this.dgvDatos.AllowUserToAddRows = false;
            this.dgvDatos.AllowUserToDeleteRows = false;
            this.dgvDatos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDatos.Location = new System.Drawing.Point(13, 13);
            this.dgvDatos.Name = "dgvDatos";
            this.dgvDatos.Size = new System.Drawing.Size(764, 440);
            this.dgvDatos.TabIndex = 0;
            this.dgvDatos.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvDatos_DataError);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnSalir);
            this.groupBox1.Controls.Add(this.btnGrabar);
            this.groupBox1.Location = new System.Drawing.Point(789, 163);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(207, 291);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // btnSalir
            // 
            this.btnSalir.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSalir.Location = new System.Drawing.Point(14, 59);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(179, 31);
            this.btnSalir.TabIndex = 1;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // btnGrabar
            // 
            this.btnGrabar.Location = new System.Drawing.Point(14, 19);
            this.btnGrabar.Name = "btnGrabar";
            this.btnGrabar.Size = new System.Drawing.Size(179, 31);
            this.btnGrabar.TabIndex = 0;
            this.btnGrabar.Text = "Grabar";
            this.btnGrabar.UseVisualStyleBackColor = true;
            this.btnGrabar.Click += new System.EventHandler(this.btnGrabar_Click);
            // 
            // grpCampos
            // 
            this.grpCampos.Controls.Add(this.txtMayor);
            this.grpCampos.Controls.Add(this.label3);
            this.grpCampos.Controls.Add(this.txtPublico);
            this.grpCampos.Controls.Add(this.label2);
            this.grpCampos.Controls.Add(this.txtCosto);
            this.grpCampos.Controls.Add(this.label1);
            this.grpCampos.Controls.Add(this.checkBox1);
            this.grpCampos.Location = new System.Drawing.Point(789, 7);
            this.grpCampos.Name = "grpCampos";
            this.grpCampos.Size = new System.Drawing.Size(207, 150);
            this.grpCampos.TabIndex = 0;
            this.grpCampos.TabStop = false;
            // 
            // txtMayor
            // 
            this.txtMayor.Location = new System.Drawing.Point(102, 106);
            this.txtMayor.Name = "txtMayor";
            this.txtMayor.Size = new System.Drawing.Size(94, 20);
            this.txtMayor.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 109);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Precio mayorista";
            // 
            // txtPublico
            // 
            this.txtPublico.Location = new System.Drawing.Point(102, 80);
            this.txtPublico.Name = "txtPublico";
            this.txtPublico.Size = new System.Drawing.Size(94, 20);
            this.txtPublico.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Precio al público";
            // 
            // txtCosto
            // 
            this.txtCosto.Location = new System.Drawing.Point(102, 54);
            this.txtCosto.Name = "txtCosto";
            this.txtCosto.Size = new System.Drawing.Size(94, 20);
            this.txtCosto.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Precio de costo";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(13, 19);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(183, 17);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.Text = "Seleccionar / deseleccionar todo";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // frmArticulosPrecios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnSalir;
            this.ClientSize = new System.Drawing.Size(1007, 463);
            this.Controls.Add(this.grpCampos);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dgvDatos);
            this.MaximizeBox = false;
            this.Name = "frmArticulosPrecios";
            this.Text = "Actualizar precios";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmArticulosPrecios_FormClosing);
            this.Load += new System.EventHandler(this.frmArticulosEditNews_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.grpCampos.ResumeLayout(false);
            this.grpCampos.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvDatos;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Button btnGrabar;
        private System.Windows.Forms.GroupBox grpCampos;
        private System.Windows.Forms.TextBox txtMayor;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPublico;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCosto;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBox1;

    }
}