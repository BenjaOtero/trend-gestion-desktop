namespace StockVentas
{
    partial class frmStockCompInter
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
            this.label1 = new System.Windows.Forms.Label();
            this.lstLocales = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSalir = new System.Windows.Forms.Button();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.dateTimeDesde = new System.Windows.Forms.DateTimePicker();
            this.dateTimeHasta = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtParametros = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.grpBuscarPor = new System.Windows.Forms.GroupBox();
            this.rdDescripcion = new System.Windows.Forms.RadioButton();
            this.rdArticulo = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rdPesos = new System.Windows.Forms.RadioButton();
            this.rdDetalle = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.grpBuscarPor.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Locales";
            // 
            // lstLocales
            // 
            this.lstLocales.FormattingEnabled = true;
            this.lstLocales.Location = new System.Drawing.Point(9, 22);
            this.lstLocales.Name = "lstLocales";
            this.lstLocales.Size = new System.Drawing.Size(319, 95);
            this.lstLocales.TabIndex = 8;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnSalir);
            this.groupBox1.Controls.Add(this.btnAceptar);
            this.groupBox1.Location = new System.Drawing.Point(9, 350);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(319, 57);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            // 
            // btnSalir
            // 
            this.btnSalir.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSalir.Location = new System.Drawing.Point(173, 17);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(130, 30);
            this.btnSalir.TabIndex = 1;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // btnAceptar
            // 
            this.btnAceptar.Location = new System.Drawing.Point(14, 17);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(130, 30);
            this.btnAceptar.TabIndex = 0;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // dateTimeDesde
            // 
            this.dateTimeDesde.Location = new System.Drawing.Point(93, 127);
            this.dateTimeDesde.Name = "dateTimeDesde";
            this.dateTimeDesde.Size = new System.Drawing.Size(235, 20);
            this.dateTimeDesde.TabIndex = 10;
            // 
            // dateTimeHasta
            // 
            this.dateTimeHasta.Location = new System.Drawing.Point(93, 156);
            this.dateTimeHasta.Name = "dateTimeHasta";
            this.dateTimeHasta.Size = new System.Drawing.Size(235, 20);
            this.dateTimeHasta.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label2.Location = new System.Drawing.Point(10, 130);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Desde fecha";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label3.Location = new System.Drawing.Point(10, 158);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Hasta fecha";
            // 
            // txtParametros
            // 
            this.txtParametros.Location = new System.Drawing.Point(56, 249);
            this.txtParametros.Name = "txtParametros";
            this.txtParametros.Size = new System.Drawing.Size(272, 20);
            this.txtParametros.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label4.Location = new System.Drawing.Point(10, 252);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Buscar";
            // 
            // grpBuscarPor
            // 
            this.grpBuscarPor.Controls.Add(this.rdDescripcion);
            this.grpBuscarPor.Controls.Add(this.rdArticulo);
            this.grpBuscarPor.ForeColor = System.Drawing.SystemColors.Highlight;
            this.grpBuscarPor.Location = new System.Drawing.Point(9, 181);
            this.grpBuscarPor.Name = "grpBuscarPor";
            this.grpBuscarPor.Size = new System.Drawing.Size(319, 56);
            this.grpBuscarPor.TabIndex = 14;
            this.grpBuscarPor.TabStop = false;
            this.grpBuscarPor.Text = "Buscar por:";
            // 
            // rdDescripcion
            // 
            this.rdDescripcion.AutoSize = true;
            this.rdDescripcion.Checked = true;
            this.rdDescripcion.ForeColor = System.Drawing.SystemColors.ControlText;
            this.rdDescripcion.Location = new System.Drawing.Point(11, 36);
            this.rdDescripcion.Name = "rdDescripcion";
            this.rdDescripcion.Size = new System.Drawing.Size(81, 17);
            this.rdDescripcion.TabIndex = 0;
            this.rdDescripcion.TabStop = true;
            this.rdDescripcion.Text = "Descripción";
            this.rdDescripcion.UseVisualStyleBackColor = true;
            // 
            // rdArticulo
            // 
            this.rdArticulo.AutoSize = true;
            this.rdArticulo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.rdArticulo.Location = new System.Drawing.Point(11, 17);
            this.rdArticulo.Name = "rdArticulo";
            this.rdArticulo.Size = new System.Drawing.Size(62, 17);
            this.rdArticulo.TabIndex = 0;
            this.rdArticulo.Text = "Artículo";
            this.rdArticulo.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rdPesos);
            this.groupBox2.Controls.Add(this.rdDetalle);
            this.groupBox2.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.groupBox2.Location = new System.Drawing.Point(9, 279);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(319, 67);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Seleccione una opción";
            // 
            // rdPesos
            // 
            this.rdPesos.AutoSize = true;
            this.rdPesos.ForeColor = System.Drawing.SystemColors.ControlText;
            this.rdPesos.Location = new System.Drawing.Point(6, 43);
            this.rdPesos.Name = "rdPesos";
            this.rdPesos.Size = new System.Drawing.Size(54, 17);
            this.rdPesos.TabIndex = 1;
            this.rdPesos.Text = "Pesos";
            this.rdPesos.UseVisualStyleBackColor = true;
            this.rdPesos.Click += new System.EventHandler(this.rdPesos_Click);
            // 
            // rdDetalle
            // 
            this.rdDetalle.AutoSize = true;
            this.rdDetalle.Checked = true;
            this.rdDetalle.ForeColor = System.Drawing.SystemColors.ControlText;
            this.rdDetalle.Location = new System.Drawing.Point(6, 20);
            this.rdDetalle.Name = "rdDetalle";
            this.rdDetalle.Size = new System.Drawing.Size(58, 17);
            this.rdDetalle.TabIndex = 0;
            this.rdDetalle.TabStop = true;
            this.rdDetalle.Text = "Detalle";
            this.rdDetalle.UseVisualStyleBackColor = true;
            this.rdDetalle.Click += new System.EventHandler(this.rdDetalle_Click);
            // 
            // frmStockCompInter
            // 
            this.AcceptButton = this.btnAceptar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnSalir;
            this.ClientSize = new System.Drawing.Size(337, 414);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.grpBuscarPor);
            this.Controls.Add(this.txtParametros);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dateTimeHasta);
            this.Controls.Add(this.dateTimeDesde);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lstLocales);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmStockCompInter";
            this.Text = "Compensaciones de stock";
            this.Load += new System.EventHandler(this.frmStockMovInter_Load);
            this.groupBox1.ResumeLayout(false);
            this.grpBuscarPor.ResumeLayout(false);
            this.grpBuscarPor.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lstLocales;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.DateTimePicker dateTimeDesde;
        private System.Windows.Forms.DateTimePicker dateTimeHasta;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtParametros;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox grpBuscarPor;
        private System.Windows.Forms.RadioButton rdDescripcion;
        private System.Windows.Forms.RadioButton rdArticulo;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rdPesos;
        private System.Windows.Forms.RadioButton rdDetalle;
    }
}