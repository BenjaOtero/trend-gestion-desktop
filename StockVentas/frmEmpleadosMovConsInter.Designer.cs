namespace StockVentas
{
    partial class frmEmpleadosMovConsInter
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
            this.lstEmpleados = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimeDesde = new System.Windows.Forms.DateTimePicker();
            this.dateTimeHasta = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSalir = new System.Windows.Forms.Button();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkLiquidado = new System.Windows.Forms.CheckBox();
            this.chkTodos = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstEmpleados
            // 
            this.lstEmpleados.FormattingEnabled = true;
            this.lstEmpleados.Location = new System.Drawing.Point(9, 127);
            this.lstEmpleados.Name = "lstEmpleados";
            this.lstEmpleados.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lstEmpleados.Size = new System.Drawing.Size(315, 121);
            this.lstEmpleados.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label1.Location = new System.Drawing.Point(9, 107);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Empleados";
            // 
            // dateTimeDesde
            // 
            this.dateTimeDesde.Location = new System.Drawing.Point(92, 13);
            this.dateTimeDesde.Name = "dateTimeDesde";
            this.dateTimeDesde.Size = new System.Drawing.Size(209, 20);
            this.dateTimeDesde.TabIndex = 10;
            // 
            // dateTimeHasta
            // 
            this.dateTimeHasta.Location = new System.Drawing.Point(92, 42);
            this.dateTimeHasta.Name = "dateTimeHasta";
            this.dateTimeHasta.Size = new System.Drawing.Size(209, 20);
            this.dateTimeHasta.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label2.Location = new System.Drawing.Point(12, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Desde fecha";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label3.Location = new System.Drawing.Point(12, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Hasta fecha";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnSalir);
            this.groupBox1.Controls.Add(this.btnAceptar);
            this.groupBox1.Location = new System.Drawing.Point(9, 278);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(315, 62);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            // 
            // btnSalir
            // 
            this.btnSalir.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSalir.Location = new System.Drawing.Point(168, 19);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(135, 30);
            this.btnSalir.TabIndex = 1;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // btnAceptar
            // 
            this.btnAceptar.Location = new System.Drawing.Point(11, 19);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(135, 30);
            this.btnAceptar.TabIndex = 0;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkLiquidado);
            this.groupBox2.Controls.Add(this.dateTimeHasta);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.dateTimeDesde);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(9, 7);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(312, 94);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            // 
            // chkLiquidado
            // 
            this.chkLiquidado.AutoSize = true;
            this.chkLiquidado.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkLiquidado.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.chkLiquidado.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkLiquidado.Location = new System.Drawing.Point(11, 71);
            this.chkLiquidado.Name = "chkLiquidado";
            this.chkLiquidado.Size = new System.Drawing.Size(96, 17);
            this.chkLiquidado.TabIndex = 14;
            this.chkLiquidado.Text = "Liquidado        ";
            this.chkLiquidado.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkLiquidado.UseVisualStyleBackColor = true;
            // 
            // chkTodos
            // 
            this.chkTodos.AutoSize = true;
            this.chkTodos.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkTodos.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.chkTodos.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkTodos.Location = new System.Drawing.Point(15, 255);
            this.chkTodos.Name = "chkTodos";
            this.chkTodos.Size = new System.Drawing.Size(111, 17);
            this.chkTodos.TabIndex = 14;
            this.chkTodos.Text = "Seleccionar todos";
            this.chkTodos.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkTodos.UseVisualStyleBackColor = true;
            this.chkTodos.Click += new System.EventHandler(this.chkTodos_Click);
            // 
            // frmEmpleadosMovConsInter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(333, 350);
            this.Controls.Add(this.chkTodos);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.lstEmpleados);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmEmpleadosMovConsInter";
            this.Text = "Movimientos empleados";
            this.Load += new System.EventHandler(this.frmEmpleadosMovConsInter_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lstEmpleados;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimeDesde;
        private System.Windows.Forms.DateTimePicker dateTimeHasta;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chkLiquidado;
        private System.Windows.Forms.CheckBox chkTodos;
    }
}