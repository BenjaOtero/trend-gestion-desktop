namespace StockVentas
{
    partial class frmStockInter
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSalir = new System.Windows.Forms.Button();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rdImpresora = new System.Windows.Forms.RadioButton();
            this.rdPantalla = new System.Windows.Forms.RadioButton();
            this.lstLocales = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chkActivo = new System.Windows.Forms.CheckBox();
            this.txtParametros = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.rdArticulo = new System.Windows.Forms.RadioButton();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbProveedor = new System.Windows.Forms.ComboBox();
            this.cmbGenero = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnSalir);
            this.groupBox1.Controls.Add(this.btnAceptar);
            this.groupBox1.Location = new System.Drawing.Point(11, 395);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(314, 65);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // btnSalir
            // 
            this.btnSalir.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSalir.Location = new System.Drawing.Point(169, 19);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(130, 30);
            this.btnSalir.TabIndex = 1;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // btnAceptar
            // 
            this.btnAceptar.Location = new System.Drawing.Point(15, 19);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(130, 30);
            this.btnAceptar.TabIndex = 0;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rdImpresora);
            this.groupBox2.Controls.Add(this.rdPantalla);
            this.groupBox2.ForeColor = System.Drawing.SystemColors.Highlight;
            this.groupBox2.Location = new System.Drawing.Point(12, 330);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(313, 60);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Seleccione una opción";
            // 
            // rdImpresora
            // 
            this.rdImpresora.AutoSize = true;
            this.rdImpresora.ForeColor = System.Drawing.SystemColors.ControlText;
            this.rdImpresora.Location = new System.Drawing.Point(6, 36);
            this.rdImpresora.Name = "rdImpresora";
            this.rdImpresora.Size = new System.Drawing.Size(71, 17);
            this.rdImpresora.TabIndex = 1;
            this.rdImpresora.Text = "Impresora";
            this.rdImpresora.UseVisualStyleBackColor = true;
            this.rdImpresora.Click += new System.EventHandler(this.rdImpresora_Click);
            // 
            // rdPantalla
            // 
            this.rdPantalla.AutoSize = true;
            this.rdPantalla.Checked = true;
            this.rdPantalla.ForeColor = System.Drawing.SystemColors.ControlText;
            this.rdPantalla.Location = new System.Drawing.Point(6, 16);
            this.rdPantalla.Name = "rdPantalla";
            this.rdPantalla.Size = new System.Drawing.Size(63, 17);
            this.rdPantalla.TabIndex = 0;
            this.rdPantalla.TabStop = true;
            this.rdPantalla.Text = "Pantalla";
            this.rdPantalla.UseVisualStyleBackColor = true;
            this.rdPantalla.Click += new System.EventHandler(this.rdPantalla_Click);
            // 
            // lstLocales
            // 
            this.lstLocales.FormattingEnabled = true;
            this.lstLocales.Location = new System.Drawing.Point(12, 216);
            this.lstLocales.Name = "lstLocales";
            this.lstLocales.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lstLocales.Size = new System.Drawing.Size(313, 108);
            this.lstLocales.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label1.Location = new System.Drawing.Point(12, 202);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Locales";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.chkActivo);
            this.groupBox3.Controls.Add(this.cmbGenero);
            this.groupBox3.Controls.Add(this.txtParametros);
            this.groupBox3.Controls.Add(this.cmbProveedor);
            this.groupBox3.ForeColor = System.Drawing.SystemColors.Highlight;
            this.groupBox3.Location = new System.Drawing.Point(15, 74);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(310, 124);
            this.groupBox3.TabIndex = 19;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Buscar";
            // 
            // chkActivo
            // 
            this.chkActivo.AutoSize = true;
            this.chkActivo.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkActivo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.chkActivo.Location = new System.Drawing.Point(2, 98);
            this.chkActivo.Name = "chkActivo";
            this.chkActivo.Size = new System.Drawing.Size(105, 17);
            this.chkActivo.TabIndex = 8;
            this.chkActivo.Text = "Activo en la web";
            this.chkActivo.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.chkActivo.UseVisualStyleBackColor = true;
            // 
            // txtParametros
            // 
            this.txtParametros.Location = new System.Drawing.Point(6, 18);
            this.txtParametros.Name = "txtParametros";
            this.txtParametros.Size = new System.Drawing.Size(288, 20);
            this.txtParametros.TabIndex = 7;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.radioButton2);
            this.groupBox4.Controls.Add(this.rdArticulo);
            this.groupBox4.ForeColor = System.Drawing.SystemColors.Highlight;
            this.groupBox4.Location = new System.Drawing.Point(15, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(310, 57);
            this.groupBox4.TabIndex = 18;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Buscar por:";
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Checked = true;
            this.radioButton2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.radioButton2.Location = new System.Drawing.Point(14, 33);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(81, 17);
            this.radioButton2.TabIndex = 0;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Descripción";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // rdArticulo
            // 
            this.rdArticulo.AutoSize = true;
            this.rdArticulo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.rdArticulo.Location = new System.Drawing.Point(14, 15);
            this.rdArticulo.Name = "rdArticulo";
            this.rdArticulo.Size = new System.Drawing.Size(62, 17);
            this.rdArticulo.TabIndex = 0;
            this.rdArticulo.Text = "Artículo";
            this.rdArticulo.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label9.Location = new System.Drawing.Point(4, 74);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(56, 13);
            this.label9.TabIndex = 48;
            this.label9.Text = "Proveedor";
            // 
            // cmbProveedor
            // 
            this.cmbProveedor.FormattingEnabled = true;
            this.cmbProveedor.Location = new System.Drawing.Point(75, 71);
            this.cmbProveedor.Name = "cmbProveedor";
            this.cmbProveedor.Size = new System.Drawing.Size(220, 21);
            this.cmbProveedor.TabIndex = 47;
            // 
            // cmbGenero
            // 
            this.cmbGenero.FormattingEnabled = true;
            this.cmbGenero.Location = new System.Drawing.Point(75, 44);
            this.cmbGenero.Name = "cmbGenero";
            this.cmbGenero.Size = new System.Drawing.Size(220, 21);
            this.cmbGenero.TabIndex = 47;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(6, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 48;
            this.label2.Text = "Género";
            // 
            // frmStockInter
            // 
            this.AcceptButton = this.btnAceptar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnSalir;
            this.ClientSize = new System.Drawing.Size(334, 468);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lstLocales);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmStockInter";
            this.Text = "Stock de artículos";
            this.Load += new System.EventHandler(this.frmStockMovInter_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rdImpresora;
        private System.Windows.Forms.RadioButton rdPantalla;
        private System.Windows.Forms.ListBox lstLocales;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtParametros;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton rdArticulo;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmbProveedor;
        private System.Windows.Forms.CheckBox chkActivo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbGenero;

    }
}