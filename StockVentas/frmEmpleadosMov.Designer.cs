namespace StockVentas
{
    partial class frmEmpleadosMov
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
            this.components = new System.ComponentModel.Container();
            this.grpCampos = new System.Windows.Forms.GroupBox();
            this.chkLiquidadoEMOV = new System.Windows.Forms.CheckBox();
            this.cmbIdMovTipoEMOV = new System.Windows.Forms.ComboBox();
            this.cmbIdEmpleadoEMOV = new System.Windows.Forms.ComboBox();
            this.mskFechaEMOV = new System.Windows.Forms.MaskedTextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtImporteEMOV = new System.Windows.Forms.TextBox();
            this.txtDetalleEMOV = new System.Windows.Forms.TextBox();
            this.txtCantidadEMOV = new System.Windows.Forms.TextBox();
            this.txtIdMovEMOV = new System.Windows.Forms.TextBox();
            this.grpBotones = new System.Windows.Forms.GroupBox();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.btnGrabar = new System.Windows.Forms.Button();
            this.btnBorrar = new System.Windows.Forms.Button();
            this.btnEditar = new System.Windows.Forms.Button();
            this.btnNuevo = new System.Windows.Forms.Button();
            this.bindingNavigator1 = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.grpCampos.SuspendLayout();
            this.grpBotones.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator1)).BeginInit();
            this.bindingNavigator1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // grpCampos
            // 
            this.grpCampos.Controls.Add(this.chkLiquidadoEMOV);
            this.grpCampos.Controls.Add(this.cmbIdMovTipoEMOV);
            this.grpCampos.Controls.Add(this.cmbIdEmpleadoEMOV);
            this.grpCampos.Controls.Add(this.mskFechaEMOV);
            this.grpCampos.Controls.Add(this.label8);
            this.grpCampos.Controls.Add(this.label6);
            this.grpCampos.Controls.Add(this.label5);
            this.grpCampos.Controls.Add(this.label3);
            this.grpCampos.Controls.Add(this.label2);
            this.grpCampos.Controls.Add(this.label7);
            this.grpCampos.Controls.Add(this.label1);
            this.grpCampos.Controls.Add(this.txtImporteEMOV);
            this.grpCampos.Controls.Add(this.txtDetalleEMOV);
            this.grpCampos.Controls.Add(this.txtCantidadEMOV);
            this.grpCampos.Controls.Add(this.txtIdMovEMOV);
            this.grpCampos.Location = new System.Drawing.Point(12, 4);
            this.grpCampos.Name = "grpCampos";
            this.grpCampos.Size = new System.Drawing.Size(378, 252);
            this.grpCampos.TabIndex = 0;
            this.grpCampos.TabStop = false;
            // 
            // chkLiquidadoEMOV
            // 
            this.chkLiquidadoEMOV.AutoSize = true;
            this.chkLiquidadoEMOV.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkLiquidadoEMOV.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.chkLiquidadoEMOV.Location = new System.Drawing.Point(5, 205);
            this.chkLiquidadoEMOV.Name = "chkLiquidadoEMOV";
            this.chkLiquidadoEMOV.Size = new System.Drawing.Size(113, 17);
            this.chkLiquidadoEMOV.TabIndex = 7;
            this.chkLiquidadoEMOV.Text = "Remuneración      ";
            this.chkLiquidadoEMOV.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.chkLiquidadoEMOV.UseVisualStyleBackColor = true;
            // 
            // cmbIdMovTipoEMOV
            // 
            this.cmbIdMovTipoEMOV.FormattingEnabled = true;
            this.cmbIdMovTipoEMOV.Location = new System.Drawing.Point(104, 99);
            this.cmbIdMovTipoEMOV.Name = "cmbIdMovTipoEMOV";
            this.cmbIdMovTipoEMOV.Size = new System.Drawing.Size(255, 21);
            this.cmbIdMovTipoEMOV.TabIndex = 3;
            // 
            // cmbIdEmpleadoEMOV
            // 
            this.cmbIdEmpleadoEMOV.FormattingEnabled = true;
            this.cmbIdEmpleadoEMOV.Location = new System.Drawing.Point(104, 72);
            this.cmbIdEmpleadoEMOV.Name = "cmbIdEmpleadoEMOV";
            this.cmbIdEmpleadoEMOV.Size = new System.Drawing.Size(255, 21);
            this.cmbIdEmpleadoEMOV.TabIndex = 2;
            // 
            // mskFechaEMOV
            // 
            this.mskFechaEMOV.Location = new System.Drawing.Point(104, 45);
            this.mskFechaEMOV.Mask = "00/00/0000";
            this.mskFechaEMOV.Name = "mskFechaEMOV";
            this.mskFechaEMOV.Size = new System.Drawing.Size(255, 20);
            this.mskFechaEMOV.TabIndex = 1;
            this.mskFechaEMOV.TextMaskFormat = System.Windows.Forms.MaskFormat.IncludePromptAndLiterals;
            this.mskFechaEMOV.ValidatingType = typeof(System.DateTime);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label8.Location = new System.Drawing.Point(6, 179);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(49, 15);
            this.label8.TabIndex = 14;
            this.label8.Text = "Importe";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label6.Location = new System.Drawing.Point(6, 153);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 15);
            this.label6.TabIndex = 13;
            this.label6.Text = "Detalle";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label5.Location = new System.Drawing.Point(6, 100);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 15);
            this.label5.TabIndex = 11;
            this.label5.Text = "Tipo mov.";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label3.Location = new System.Drawing.Point(6, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 15);
            this.label3.TabIndex = 10;
            this.label3.Text = "Empleado";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label2.Location = new System.Drawing.Point(6, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 15);
            this.label2.TabIndex = 9;
            this.label2.Text = "Fecha";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label7.Location = new System.Drawing.Point(6, 127);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 15);
            this.label7.TabIndex = 12;
            this.label7.Text = "Cantidad";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label1.Location = new System.Drawing.Point(6, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(19, 15);
            this.label1.TabIndex = 8;
            this.label1.Text = "ID";
            // 
            // txtImporteEMOV
            // 
            this.txtImporteEMOV.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtImporteEMOV.Location = new System.Drawing.Point(104, 178);
            this.txtImporteEMOV.Name = "txtImporteEMOV";
            this.txtImporteEMOV.Size = new System.Drawing.Size(255, 20);
            this.txtImporteEMOV.TabIndex = 6;
            // 
            // txtDetalleEMOV
            // 
            this.txtDetalleEMOV.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDetalleEMOV.Location = new System.Drawing.Point(104, 152);
            this.txtDetalleEMOV.Name = "txtDetalleEMOV";
            this.txtDetalleEMOV.Size = new System.Drawing.Size(255, 20);
            this.txtDetalleEMOV.TabIndex = 5;
            // 
            // txtCantidadEMOV
            // 
            this.txtCantidadEMOV.Location = new System.Drawing.Point(104, 126);
            this.txtCantidadEMOV.Name = "txtCantidadEMOV";
            this.txtCantidadEMOV.Size = new System.Drawing.Size(255, 20);
            this.txtCantidadEMOV.TabIndex = 4;
            // 
            // txtIdMovEMOV
            // 
            this.txtIdMovEMOV.Location = new System.Drawing.Point(104, 18);
            this.txtIdMovEMOV.Name = "txtIdMovEMOV";
            this.txtIdMovEMOV.Size = new System.Drawing.Size(255, 20);
            this.txtIdMovEMOV.TabIndex = 0;
            // 
            // grpBotones
            // 
            this.grpBotones.Controls.Add(this.btnCancelar);
            this.grpBotones.Controls.Add(this.btnSalir);
            this.grpBotones.Controls.Add(this.btnGrabar);
            this.grpBotones.Controls.Add(this.btnBorrar);
            this.grpBotones.Controls.Add(this.btnEditar);
            this.grpBotones.Controls.Add(this.btnNuevo);
            this.grpBotones.Location = new System.Drawing.Point(399, 4);
            this.grpBotones.Name = "grpBotones";
            this.grpBotones.Size = new System.Drawing.Size(174, 252);
            this.grpBotones.TabIndex = 1;
            this.grpBotones.TabStop = false;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(13, 147);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(148, 26);
            this.btnCancelar.TabIndex = 5;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnSalir
            // 
            this.btnSalir.Location = new System.Drawing.Point(13, 179);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(148, 26);
            this.btnSalir.TabIndex = 6;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // btnGrabar
            // 
            this.btnGrabar.Location = new System.Drawing.Point(13, 115);
            this.btnGrabar.Name = "btnGrabar";
            this.btnGrabar.Size = new System.Drawing.Size(148, 26);
            this.btnGrabar.TabIndex = 4;
            this.btnGrabar.Text = "Grabar";
            this.btnGrabar.UseVisualStyleBackColor = true;
            this.btnGrabar.Click += new System.EventHandler(this.btnGrabar_Click);
            // 
            // btnBorrar
            // 
            this.btnBorrar.Location = new System.Drawing.Point(13, 83);
            this.btnBorrar.Name = "btnBorrar";
            this.btnBorrar.Size = new System.Drawing.Size(148, 26);
            this.btnBorrar.TabIndex = 3;
            this.btnBorrar.Text = "Borrar";
            this.btnBorrar.UseVisualStyleBackColor = true;
            this.btnBorrar.Click += new System.EventHandler(this.btnBorrar_Click);
            // 
            // btnEditar
            // 
            this.btnEditar.Location = new System.Drawing.Point(13, 51);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(148, 26);
            this.btnEditar.TabIndex = 2;
            this.btnEditar.Text = "Editar";
            this.btnEditar.UseVisualStyleBackColor = true;
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // btnNuevo
            // 
            this.btnNuevo.Location = new System.Drawing.Point(13, 19);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(148, 26);
            this.btnNuevo.TabIndex = 1;
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.UseVisualStyleBackColor = true;
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // bindingNavigator1
            // 
            this.bindingNavigator1.AddNewItem = null;
            this.bindingNavigator1.CountItem = this.bindingNavigatorCountItem;
            this.bindingNavigator1.DeleteItem = null;
            this.bindingNavigator1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bindingNavigator1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator2});
            this.bindingNavigator1.Location = new System.Drawing.Point(0, 269);
            this.bindingNavigator1.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.bindingNavigator1.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.bindingNavigator1.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.bindingNavigator1.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.bindingNavigator1.Name = "bindingNavigator1";
            this.bindingNavigator1.PositionItem = this.bindingNavigatorPositionItem;
            this.bindingNavigator1.Size = new System.Drawing.Size(585, 25);
            this.bindingNavigator1.TabIndex = 29;
            this.bindingNavigator1.Text = "bindingNavigator1";
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(38, 22);
            this.bindingNavigatorCountItem.Text = "de {0}";
            this.bindingNavigatorCountItem.ToolTipText = "Número total de elementos";
            // 
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveFirstItem.Text = "Mover primero";
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMovePreviousItem.Text = "Mover anterior";
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorPositionItem
            // 
            this.bindingNavigatorPositionItem.AccessibleName = "Posición";
            this.bindingNavigatorPositionItem.AutoSize = false;
            this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
            this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(50, 21);
            this.bindingNavigatorPositionItem.Text = "0";
            this.bindingNavigatorPositionItem.ToolTipText = "Posición actual";
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorMoveNextItem
            // 
            this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveNextItem.Text = "Mover siguiente";
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveLastItem.Text = "Mover último";
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // frmEmpleadosMov
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(585, 294);
            this.ControlBox = false;
            this.Controls.Add(this.bindingNavigator1);
            this.Controls.Add(this.grpCampos);
            this.Controls.Add(this.grpBotones);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmEmpleadosMov";
            this.Text = "Empleados movimientos";
            this.Activated += new System.EventHandler(this.frmEmpleadosMov_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmEmpleadosMov_FormClosing);
            this.Load += new System.EventHandler(this.frmEmpleadosMov_Load);
            this.grpCampos.ResumeLayout(false);
            this.grpCampos.PerformLayout();
            this.grpBotones.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator1)).EndInit();
            this.bindingNavigator1.ResumeLayout(false);
            this.bindingNavigator1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpCampos;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtIdMovEMOV;
        private System.Windows.Forms.GroupBox grpBotones;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Button btnGrabar;
        private System.Windows.Forms.Button btnBorrar;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.Button btnNuevo;
        private System.Windows.Forms.BindingNavigator bindingNavigator1;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.MaskedTextBox mskFechaEMOV;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDetalleEMOV;
        private System.Windows.Forms.ComboBox cmbIdMovTipoEMOV;
        private System.Windows.Forms.ComboBox cmbIdEmpleadoEMOV;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtCantidadEMOV;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtImporteEMOV;
        private System.Windows.Forms.CheckBox chkLiquidadoEMOV;
    }
}