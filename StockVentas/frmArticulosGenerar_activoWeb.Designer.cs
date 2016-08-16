namespace StockVentas
{
    partial class frmArticulosGenerar
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
            this.cmbItem = new System.Windows.Forms.ComboBox();
            this.lstColores = new System.Windows.Forms.ListBox();
            this.btnProveedor = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.btnGrabar = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnColor = new System.Windows.Forms.Button();
            this.btnItem = new System.Windows.Forms.Button();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtPublico = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtCosto = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtMayor = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbProveedor = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rd2 = new System.Windows.Forms.RadioButton();
            this.rd1 = new System.Windows.Forms.RadioButton();
            this.txtDesde = new System.Windows.Forms.TextBox();
            this.txtHasta = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtDescripcionWeb = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.chkActivoWeb = new System.Windows.Forms.CheckBox();
            this.cmbGenero = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.cmbAlicuota = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbItem
            // 
            this.cmbItem.FormattingEnabled = true;
            this.cmbItem.Location = new System.Drawing.Point(126, 41);
            this.cmbItem.Name = "cmbItem";
            this.cmbItem.Size = new System.Drawing.Size(301, 21);
            this.cmbItem.TabIndex = 1;
            // 
            // lstColores
            // 
            this.lstColores.FormattingEnabled = true;
            this.lstColores.Location = new System.Drawing.Point(126, 120);
            this.lstColores.Name = "lstColores";
            this.lstColores.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lstColores.Size = new System.Drawing.Size(301, 134);
            this.lstColores.Sorted = true;
            this.lstColores.TabIndex = 4;
            // 
            // btnProveedor
            // 
            this.btnProveedor.Location = new System.Drawing.Point(16, 140);
            this.btnProveedor.Name = "btnProveedor";
            this.btnProveedor.Size = new System.Drawing.Size(129, 26);
            this.btnProveedor.TabIndex = 7;
            this.btnProveedor.Text = "Agregar proveedor";
            this.btnProveedor.UseVisualStyleBackColor = true;
            this.btnProveedor.Click += new System.EventHandler(this.btnProveedor_Click);
            // 
            // btnSalir
            // 
            this.btnSalir.Location = new System.Drawing.Point(16, 177);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(129, 26);
            this.btnSalir.TabIndex = 5;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // btnGrabar
            // 
            this.btnGrabar.Location = new System.Drawing.Point(16, 29);
            this.btnGrabar.Name = "btnGrabar";
            this.btnGrabar.Size = new System.Drawing.Size(129, 26);
            this.btnGrabar.TabIndex = 0;
            this.btnGrabar.Text = "Grabar";
            this.btnGrabar.UseVisualStyleBackColor = true;
            this.btnGrabar.Click += new System.EventHandler(this.btnGrabar_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnProveedor);
            this.groupBox2.Controls.Add(this.btnSalir);
            this.groupBox2.Controls.Add(this.btnColor);
            this.groupBox2.Controls.Add(this.btnItem);
            this.groupBox2.Controls.Add(this.btnGrabar);
            this.groupBox2.Location = new System.Drawing.Point(439, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(160, 547);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            // 
            // btnColor
            // 
            this.btnColor.Location = new System.Drawing.Point(16, 103);
            this.btnColor.Name = "btnColor";
            this.btnColor.Size = new System.Drawing.Size(129, 26);
            this.btnColor.TabIndex = 3;
            this.btnColor.Text = "Agregar color";
            this.btnColor.UseVisualStyleBackColor = true;
            this.btnColor.Click += new System.EventHandler(this.btnColor_Click);
            // 
            // btnItem
            // 
            this.btnItem.Location = new System.Drawing.Point(16, 66);
            this.btnItem.Name = "btnItem";
            this.btnItem.Size = new System.Drawing.Size(129, 26);
            this.btnItem.TabIndex = 2;
            this.btnItem.Text = "Agregar ítem";
            this.btnItem.UseVisualStyleBackColor = true;
            this.btnItem.Click += new System.EventHandler(this.btnItem_Click);
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDescripcion.Location = new System.Drawing.Point(126, 68);
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(301, 20);
            this.txtDescripcion.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label6.Location = new System.Drawing.Point(13, 70);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 15);
            this.label6.TabIndex = 1;
            this.label6.Text = "Descripción";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label3.Location = new System.Drawing.Point(13, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 15);
            this.label3.TabIndex = 3;
            this.label3.Text = "Item";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label1.Location = new System.Drawing.Point(13, 122);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 15);
            this.label1.TabIndex = 34;
            this.label1.Text = "Colores";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label2.Location = new System.Drawing.Point(13, 266);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 15);
            this.label2.TabIndex = 36;
            this.label2.Text = "Desde talle";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label4.Location = new System.Drawing.Point(13, 292);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 15);
            this.label4.TabIndex = 38;
            this.label4.Text = "Hasta talle";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label5.Location = new System.Drawing.Point(13, 344);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(85, 15);
            this.label5.TabIndex = 40;
            this.label5.Text = "Precio público";
            // 
            // txtPublico
            // 
            this.txtPublico.Location = new System.Drawing.Point(126, 338);
            this.txtPublico.Name = "txtPublico";
            this.txtPublico.Size = new System.Drawing.Size(301, 20);
            this.txtPublico.TabIndex = 8;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.CausesValidation = false;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label7.Location = new System.Drawing.Point(13, 318);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(74, 15);
            this.label7.TabIndex = 42;
            this.label7.Text = "Precio costo";
            // 
            // txtCosto
            // 
            this.txtCosto.Location = new System.Drawing.Point(126, 312);
            this.txtCosto.Name = "txtCosto";
            this.txtCosto.Size = new System.Drawing.Size(301, 20);
            this.txtCosto.TabIndex = 7;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label8.Location = new System.Drawing.Point(13, 370);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(98, 15);
            this.label8.TabIndex = 44;
            this.label8.Text = "Precio mayorista";
            // 
            // txtMayor
            // 
            this.txtMayor.Location = new System.Drawing.Point(126, 364);
            this.txtMayor.Name = "txtMayor";
            this.txtMayor.Size = new System.Drawing.Size(302, 20);
            this.txtMayor.TabIndex = 9;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label9.Location = new System.Drawing.Point(13, 422);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(63, 15);
            this.label9.TabIndex = 46;
            this.label9.Text = "Proveedor";
            // 
            // cmbProveedor
            // 
            this.cmbProveedor.FormattingEnabled = true;
            this.cmbProveedor.Location = new System.Drawing.Point(126, 417);
            this.cmbProveedor.Name = "cmbProveedor";
            this.cmbProveedor.Size = new System.Drawing.Size(302, 21);
            this.cmbProveedor.TabIndex = 11;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rd2);
            this.groupBox1.Controls.Add(this.rd1);
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.groupBox1.Location = new System.Drawing.Point(16, 484);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(413, 66);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Valor a incrementar por talle";
            // 
            // rd2
            // 
            this.rd2.AutoSize = true;
            this.rd2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.rd2.Location = new System.Drawing.Point(12, 44);
            this.rd2.Name = "rd2";
            this.rd2.Size = new System.Drawing.Size(31, 17);
            this.rd2.TabIndex = 1;
            this.rd2.TabStop = true;
            this.rd2.Text = "2";
            this.rd2.UseVisualStyleBackColor = true;
            // 
            // rd1
            // 
            this.rd1.AutoSize = true;
            this.rd1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.rd1.Location = new System.Drawing.Point(12, 21);
            this.rd1.Name = "rd1";
            this.rd1.Size = new System.Drawing.Size(31, 17);
            this.rd1.TabIndex = 0;
            this.rd1.TabStop = true;
            this.rd1.Text = "1";
            this.rd1.UseVisualStyleBackColor = true;
            // 
            // txtDesde
            // 
            this.txtDesde.Location = new System.Drawing.Point(126, 260);
            this.txtDesde.Name = "txtDesde";
            this.txtDesde.Size = new System.Drawing.Size(301, 20);
            this.txtDesde.TabIndex = 5;
            // 
            // txtHasta
            // 
            this.txtHasta.Location = new System.Drawing.Point(126, 286);
            this.txtHasta.Name = "txtHasta";
            this.txtHasta.Size = new System.Drawing.Size(301, 20);
            this.txtHasta.TabIndex = 6;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label10.Location = new System.Drawing.Point(13, 96);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(98, 15);
            this.label10.TabIndex = 48;
            this.label10.Text = "Descripción web";
            // 
            // txtDescripcionWeb
            // 
            this.txtDescripcionWeb.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDescripcionWeb.Location = new System.Drawing.Point(126, 94);
            this.txtDescripcionWeb.Name = "txtDescripcionWeb";
            this.txtDescripcionWeb.Size = new System.Drawing.Size(301, 20);
            this.txtDescripcionWeb.TabIndex = 3;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label11.Location = new System.Drawing.Point(13, 448);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(94, 15);
            this.label11.TabIndex = 34;
            this.label11.Text = "Activo en la web";
            // 
            // chkActivoWeb
            // 
            this.chkActivoWeb.AutoSize = true;
            this.chkActivoWeb.Location = new System.Drawing.Point(126, 444);
            this.chkActivoWeb.Name = "chkActivoWeb";
            this.chkActivoWeb.Size = new System.Drawing.Size(15, 14);
            this.chkActivoWeb.TabIndex = 12;
            this.chkActivoWeb.UseVisualStyleBackColor = true;
            // 
            // cmbGenero
            // 
            this.cmbGenero.FormattingEnabled = true;
            this.cmbGenero.Location = new System.Drawing.Point(126, 14);
            this.cmbGenero.Name = "cmbGenero";
            this.cmbGenero.Size = new System.Drawing.Size(301, 21);
            this.cmbGenero.TabIndex = 0;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label12.Location = new System.Drawing.Point(13, 18);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(48, 15);
            this.label12.TabIndex = 3;
            this.label12.Text = "Genero";
            // 
            // cmbAlicuota
            // 
            this.cmbAlicuota.FormattingEnabled = true;
            this.cmbAlicuota.Location = new System.Drawing.Point(126, 390);
            this.cmbAlicuota.Name = "cmbAlicuota";
            this.cmbAlicuota.Size = new System.Drawing.Size(301, 21);
            this.cmbAlicuota.TabIndex = 10;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label13.Location = new System.Drawing.Point(13, 396);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(68, 15);
            this.label13.TabIndex = 3;
            this.label13.Text = "Alícuota iva";
            // 
            // frmArticulosGenerar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(606, 562);
            this.Controls.Add(this.chkActivoWeb);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtDescripcionWeb);
            this.Controls.Add(this.txtHasta);
            this.Controls.Add(this.txtDesde);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.cmbProveedor);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtMayor);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtCosto);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtPublico);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtDescripcion);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.lstColores);
            this.Controls.Add(this.cmbGenero);
            this.Controls.Add(this.cmbAlicuota);
            this.Controls.Add(this.cmbItem);
            this.Name = "frmArticulosGenerar";
            this.Text = "Generar artículos";
            this.Activated += new System.EventHandler(this.frmArticulosGenerar_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmArticulosGenerar_FormClosing);
            this.Load += new System.EventHandler(this.frmArticulosGenerar_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbItem;
        private System.Windows.Forms.ListBox lstColores;
        private System.Windows.Forms.Button btnProveedor;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Button btnGrabar;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnColor;
        private System.Windows.Forms.Button btnItem;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtPublico;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtCosto;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtMayor;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmbProveedor;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rd2;
        private System.Windows.Forms.RadioButton rd1;
        private System.Windows.Forms.TextBox txtDesde;
        private System.Windows.Forms.TextBox txtHasta;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtDescripcionWeb;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.CheckBox chkActivoWeb;
        private System.Windows.Forms.ComboBox cmbGenero;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox cmbAlicuota;
        private System.Windows.Forms.Label label13;
    }
}