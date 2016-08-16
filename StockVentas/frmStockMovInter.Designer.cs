namespace StockVentas
{
    partial class frmStockMovInter
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rdImpresora = new System.Windows.Forms.RadioButton();
            this.rdPantalla = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSalir = new System.Windows.Forms.Button();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rdTodos = new System.Windows.Forms.RadioButton();
            this.rdSalidas = new System.Windows.Forms.RadioButton();
            this.rdEntradas = new System.Windows.Forms.RadioButton();
            this.dateTimeDesde = new System.Windows.Forms.DateTimePicker();
            this.dateTimeHasta = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtParametros = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.rdDescripcion = new System.Windows.Forms.RadioButton();
            this.rdArticulo = new System.Windows.Forms.RadioButton();
            this.grpOrden = new System.Windows.Forms.GroupBox();
            this.rdDescripcionArt = new System.Windows.Forms.RadioButton();
            this.rdOrdenEntrada = new System.Windows.Forms.RadioButton();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.grpOrden.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Locales";
            // 
            // lstLocales
            // 
            this.lstLocales.FormattingEnabled = true;
            this.lstLocales.Location = new System.Drawing.Point(7, 22);
            this.lstLocales.Name = "lstLocales";
            this.lstLocales.Size = new System.Drawing.Size(319, 95);
            this.lstLocales.TabIndex = 8;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rdImpresora);
            this.groupBox2.Controls.Add(this.rdPantalla);
            this.groupBox2.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.groupBox2.Location = new System.Drawing.Point(11, 354);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(315, 56);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Vista";
            // 
            // rdImpresora
            // 
            this.rdImpresora.AutoSize = true;
            this.rdImpresora.ForeColor = System.Drawing.SystemColors.ControlText;
            this.rdImpresora.Location = new System.Drawing.Point(6, 34);
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
            this.rdPantalla.Location = new System.Drawing.Point(6, 15);
            this.rdPantalla.Name = "rdPantalla";
            this.rdPantalla.Size = new System.Drawing.Size(63, 17);
            this.rdPantalla.TabIndex = 0;
            this.rdPantalla.TabStop = true;
            this.rdPantalla.Text = "Pantalla";
            this.rdPantalla.UseVisualStyleBackColor = true;
            this.rdPantalla.Click += new System.EventHandler(this.rdPantalla_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnSalir);
            this.groupBox1.Controls.Add(this.btnAceptar);
            this.groupBox1.Location = new System.Drawing.Point(10, 482);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(316, 57);
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
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rdTodos);
            this.groupBox3.Controls.Add(this.rdSalidas);
            this.groupBox3.Controls.Add(this.rdEntradas);
            this.groupBox3.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.groupBox3.Location = new System.Drawing.Point(10, 272);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(316, 77);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Tipo de movimiento";
            // 
            // rdTodos
            // 
            this.rdTodos.AutoSize = true;
            this.rdTodos.ForeColor = System.Drawing.SystemColors.ControlText;
            this.rdTodos.Location = new System.Drawing.Point(6, 56);
            this.rdTodos.Name = "rdTodos";
            this.rdTodos.Size = new System.Drawing.Size(55, 17);
            this.rdTodos.TabIndex = 2;
            this.rdTodos.Text = "Todos";
            this.rdTodos.UseVisualStyleBackColor = true;
            // 
            // rdSalidas
            // 
            this.rdSalidas.AutoSize = true;
            this.rdSalidas.ForeColor = System.Drawing.SystemColors.ControlText;
            this.rdSalidas.Location = new System.Drawing.Point(6, 35);
            this.rdSalidas.Name = "rdSalidas";
            this.rdSalidas.Size = new System.Drawing.Size(59, 17);
            this.rdSalidas.TabIndex = 1;
            this.rdSalidas.Text = "Salidas";
            this.rdSalidas.UseVisualStyleBackColor = true;
            // 
            // rdEntradas
            // 
            this.rdEntradas.AutoSize = true;
            this.rdEntradas.Checked = true;
            this.rdEntradas.ForeColor = System.Drawing.SystemColors.ControlText;
            this.rdEntradas.Location = new System.Drawing.Point(6, 15);
            this.rdEntradas.Name = "rdEntradas";
            this.rdEntradas.Size = new System.Drawing.Size(67, 17);
            this.rdEntradas.TabIndex = 0;
            this.rdEntradas.TabStop = true;
            this.rdEntradas.Text = "Entradas";
            this.rdEntradas.UseVisualStyleBackColor = true;
            // 
            // dateTimeDesde
            // 
            this.dateTimeDesde.Location = new System.Drawing.Point(91, 127);
            this.dateTimeDesde.Name = "dateTimeDesde";
            this.dateTimeDesde.Size = new System.Drawing.Size(235, 20);
            this.dateTimeDesde.TabIndex = 10;
            // 
            // dateTimeHasta
            // 
            this.dateTimeHasta.Location = new System.Drawing.Point(91, 156);
            this.dateTimeHasta.Name = "dateTimeHasta";
            this.dateTimeHasta.Size = new System.Drawing.Size(235, 20);
            this.dateTimeHasta.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label2.Location = new System.Drawing.Point(8, 130);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Desde fecha";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label3.Location = new System.Drawing.Point(8, 158);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Hasta fecha";
            // 
            // txtParametros
            // 
            this.txtParametros.Location = new System.Drawing.Point(54, 246);
            this.txtParametros.Name = "txtParametros";
            this.txtParametros.Size = new System.Drawing.Size(272, 20);
            this.txtParametros.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label4.Location = new System.Drawing.Point(8, 249);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Buscar";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.rdDescripcion);
            this.groupBox4.Controls.Add(this.rdArticulo);
            this.groupBox4.ForeColor = System.Drawing.SystemColors.Highlight;
            this.groupBox4.Location = new System.Drawing.Point(7, 181);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(319, 56);
            this.groupBox4.TabIndex = 14;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Buscar por:";
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
            // grpOrden
            // 
            this.grpOrden.Controls.Add(this.rdDescripcionArt);
            this.grpOrden.Controls.Add(this.rdOrdenEntrada);
            this.grpOrden.ForeColor = System.Drawing.SystemColors.Highlight;
            this.grpOrden.Location = new System.Drawing.Point(10, 416);
            this.grpOrden.Name = "grpOrden";
            this.grpOrden.Size = new System.Drawing.Size(319, 60);
            this.grpOrden.TabIndex = 14;
            this.grpOrden.TabStop = false;
            this.grpOrden.Text = "Ordenar por: (solo vista impresora)";
            // 
            // rdDescripcionArt
            // 
            this.rdDescripcionArt.AutoSize = true;
            this.rdDescripcionArt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.rdDescripcionArt.Location = new System.Drawing.Point(11, 36);
            this.rdDescripcionArt.Name = "rdDescripcionArt";
            this.rdDescripcionArt.Size = new System.Drawing.Size(120, 17);
            this.rdDescripcionArt.TabIndex = 0;
            this.rdDescripcionArt.Text = "Descripcion artículo";
            this.rdDescripcionArt.UseVisualStyleBackColor = true;
            // 
            // rdOrdenEntrada
            // 
            this.rdOrdenEntrada.AutoSize = true;
            this.rdOrdenEntrada.Checked = true;
            this.rdOrdenEntrada.ForeColor = System.Drawing.SystemColors.ControlText;
            this.rdOrdenEntrada.Location = new System.Drawing.Point(11, 17);
            this.rdOrdenEntrada.Name = "rdOrdenEntrada";
            this.rdOrdenEntrada.Size = new System.Drawing.Size(108, 17);
            this.rdOrdenEntrada.TabIndex = 0;
            this.rdOrdenEntrada.TabStop = true;
            this.rdOrdenEntrada.Text = "Orden de entrada";
            this.rdOrdenEntrada.UseVisualStyleBackColor = true;
            // 
            // frmStockMovInter
            // 
            this.AcceptButton = this.btnAceptar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnSalir;
            this.ClientSize = new System.Drawing.Size(337, 545);
            this.Controls.Add(this.grpOrden);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.txtParametros);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dateTimeHasta);
            this.Controls.Add(this.dateTimeDesde);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lstLocales);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmStockMovInter";
            this.Text = "Movimientos de stock";
            this.Load += new System.EventHandler(this.frmStockMovInter_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.grpOrden.ResumeLayout(false);
            this.grpOrden.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lstLocales;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rdImpresora;
        private System.Windows.Forms.RadioButton rdPantalla;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rdTodos;
        private System.Windows.Forms.RadioButton rdSalidas;
        private System.Windows.Forms.RadioButton rdEntradas;
        private System.Windows.Forms.DateTimePicker dateTimeDesde;
        private System.Windows.Forms.DateTimePicker dateTimeHasta;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtParametros;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton rdDescripcion;
        private System.Windows.Forms.RadioButton rdArticulo;
        private System.Windows.Forms.GroupBox grpOrden;
        private System.Windows.Forms.RadioButton rdDescripcionArt;
        private System.Windows.Forms.RadioButton rdOrdenEntrada;
    }
}