using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.IO;
using System.Windows.Forms;
using DAL;

namespace BL
{
    public class StockMovBLL
    { 

        public static DataTable GetTabla()
        {
            DataTable tbl = DAL.StockMovDAL.GetTabla();
            return tbl;
        }

        public static DataSet CrearDatasetCons(string fechaDesde, string fechaHasta, int idLocal,
                string tipoMov, string movimiento, string articulo, string descripcion)
        {
            DataTable StockMov;
            DataTable StockMovDetalle;
            DataSet dt = DAL.StockMovDAL.CrearDatasetCons(fechaDesde, fechaHasta, idLocal, tipoMov, movimiento, articulo, descripcion);
            StockMov = dt.Tables[0].Copy();
            StockMov.TableName = "StockMov";
            StockMov.Columns.Remove("ordenar");
            StockMov.Columns.Remove("IdMSTKD");
            StockMov.Columns.Remove("IdMovMSTKD");
            StockMov.Columns.Remove("IdArticuloMSTKD");
            StockMov.Columns.Remove("DescripcionART");
            StockMov.Columns.Remove("CantidadMSTKD");
            StockMov.Columns.Remove("CompensaMSTKD");
            StockMov.Columns.Remove("OrigenMSTKD");
            StockMov.Columns.Remove("DestinoMSTKD");
            string id = string.Empty;
            foreach (DataRow row in StockMov.Rows)
            {
                if (row["IdMovMSTK"].ToString() == id)
                {
                    id = row["IdMovMSTK"].ToString();
                    row.Delete();
                }
                else
                {
                    id = row["IdMovMSTK"].ToString();
                }
            }
            StockMov.AcceptChanges();
            StockMovDetalle = dt.Tables[0].Copy();
            StockMovDetalle.TableName = "StockMovDetalle";
            StockMovDetalle.Columns.Remove("IdMovMSTK");
            StockMovDetalle.Columns.Remove("OrigenMSTK");
            StockMovDetalle.Columns.Remove("DestinoMSTK");
            StockMovDetalle.Columns.Remove("CompensaMSTK");
            DataSet dsStockMov = new DataSet();
            dsStockMov.Tables.Add(StockMov);
            dsStockMov.Tables.Add(StockMovDetalle);
            DataRelation stockMovDetalle = dsStockMov.Relations.Add(
                "StockMovDetalle", StockMov.Columns["IdMovMSTK"],
                StockMovDetalle.Columns["IdMovMSTKD"]);
            stockMovDetalle.Nested = true;
            return dsStockMov;
        }

        public static DataTable GetCompensacionesPesos(string fechaDesde, string fechaHasta, int idLocal)
        {
            DataTable tbl = new DataTable();
            tbl = DAL.StockMovDAL.GetCompensacionesPesos(fechaDesde, fechaHasta, idLocal);
            return tbl;
        }

        public static void BorrarByPK(int PK)
        {
            DAL.StockMovDAL.BorrarByPK(PK);
        }

    }
}
