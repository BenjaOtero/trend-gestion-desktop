﻿using System;
using System.Data;
using System.Windows.Forms;

namespace StockVentas
{
    public partial class frmVentasPesosCons : Form
    {
        public string fechaDesde;
        public string fechaHasta;
        public int forma;
        DataTable tblVentasPesos;

        public frmVentasPesosCons(DataTable tblVentasPesos)
        {
            InitializeComponent();
            this.tblVentasPesos = tblVentasPesos;
        }

        private void frmVentasPesosCons_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
            System.Drawing.Icon ico = Properties.Resources.icono_app;
            this.Icon = ico;
            this.ControlBox = true;
            this.MaximizeBox = false;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            dataGridView1.DataSource = tblVentasPesos;
            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.Columns["Venta"].DefaultCellStyle.Format = "C0";
            dataGridView1.Columns["Costo"].DefaultCellStyle.Format = "C0";
            dataGridView1.Columns["Utilidad bruta"].DefaultCellStyle.Format = "C0";
            dataGridView1.Columns["Valor agregado"].DefaultCellStyle.Format = "0\\%";


            
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            return;
        }
    }
}
