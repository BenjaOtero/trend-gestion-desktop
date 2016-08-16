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
using Entities;

namespace BL
{
    public class TransaccionesBLL
    {

        public static void GrabarStockMovimientos(DataSet dtStockMov)
        {
            MySqlTransaction tr = null;
            MySqlConnection SqlConnection1 = DALBase.GetConnection();
            SqlConnection1.Open();
            tr = SqlConnection1.BeginTransaction();
            DAL.StockMovDAL.GrabarDB(dtStockMov, SqlConnection1, tr);
            DAL.StockMovDetalleDAL.GrabarDB(dtStockMov, SqlConnection1, tr);
            tr.Commit();
            SqlConnection1.Close();
        }

        public static void GrabarStockMovimientos(DataSet dtStockMov, ref int? codigoError)
        {
            MySqlTransaction tr = null;
            try
            {
                MySqlConnection SqlConnection1 = DALBase.GetConnection();
                SqlConnection1.Open();
                tr = SqlConnection1.BeginTransaction();
                DAL.StockMovDAL.GrabarDB(dtStockMov, SqlConnection1, tr);
                DAL.StockMovDetalleDAL.GrabarDB(dtStockMov, SqlConnection1, tr);
                tr.Commit();
                SqlConnection1.Close();
            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 1042: //Unable to connect to any of the specified MySQL hosts.
                        //  dtStockMov.RejectChanges();
                        codigoError = 1042;
                        break;
                    case 0: // Procedure or function cannot be found in database 
                        //    dtStockMov.RejectChanges();
                        codigoError = ex.Number;
                        break;
                    default:
                        dtStockMov.RejectChanges();
                        if (tr != null)
                        {
                            tr.Rollback();
                        }
                        codigoError = ex.Number;
                        break;
                }
            }
            catch (TimeoutException)
            {
                codigoError = 8953; // El número 8953 lo asigné al azar
            }
            catch (NullReferenceException)
            {
                codigoError = 8954; // El número 8954 lo asigné al azar
            }
            catch (Exception)
            {
                codigoError = 8955; // El número 8955 lo asigné al azar
            }
        }

        public static void GrabarVentas(DataSet dtVentas, ref int? codigoError)
        {
            MySqlTransaction tr = null;
            try
            {
                MySqlConnection SqlConnection1 = DALBase.GetConnection();
                SqlConnection1.Open();
                tr = SqlConnection1.BeginTransaction();
                DAL.VentasDAL.GrabarDB(dtVentas, SqlConnection1, tr);
                DAL.VentasDetalleDAL.GrabarDB(dtVentas, SqlConnection1, tr);
                tr.Commit();
                SqlConnection1.Close();
            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 1042: //Unable to connect to any of the specified MySQL hosts.
                        dtVentas.RejectChanges();
                        codigoError = 1042;
                        break;
                    case 0: // Procedure or function cannot be found in database 
                        dtVentas.RejectChanges();
                        codigoError = ex.Number;
                        break;
                    default:
                        dtVentas.RejectChanges();
                        if (tr != null)
                        {
                            tr.Rollback();
                        }
                        codigoError = ex.Number;
                        break;
                }
            }
            catch (TimeoutException)
            {
                codigoError = 8953; // El número 8953 lo asigné al azar
            }
            catch (NullReferenceException)
            {
                codigoError = 8954; // El número 8954 lo asigné al azar
            }
            catch (Exception)
            {
                codigoError = 8955; // El número 8955 lo asigné al azar
            }
        }

        public static void GrabarArticulosAgrupar(DataTable tblStock, DataTable tblArticulos, ref int? codigoError)
        {
            MySqlTransaction tr = null;
            try
            {
                MySqlConnection SqlConnection1 = DALBase.GetConnection();
                SqlConnection1.Open();
                tr = SqlConnection1.BeginTransaction();
                DAL.ArticulosDAL.BorrarArticulosAgrupar(tblArticulos, SqlConnection1, tr);
                DAL.StockDAL.Update(tblStock, SqlConnection1, tr);
                tr.Commit();
                SqlConnection1.Close();
            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 1042: //Unable to connect to any of the specified MySQL hosts.
                        tblStock.RejectChanges();
                        tblArticulos.RejectChanges();
                        codigoError = 1042;
                        break;
                    case 0: // Procedure or function cannot be found in database 
                        tblStock.RejectChanges();
                        tblArticulos.RejectChanges();
                        codigoError = ex.Number;
                        break;
                    default:
                        tblStock.RejectChanges();
                        tblArticulos.RejectChanges();
                        if (tr != null)
                        {
                            tr.Rollback();
                        }
                        codigoError = ex.Number;
                        break;
                }
            }
            catch (TimeoutException)
            {
                codigoError = 8953; // El número 8953 lo asigné al azar
            }
            catch (NullReferenceException)
            {
                codigoError = 8954; // El número 8954 lo asigné al azar
            }
            catch (Exception)
            {
                codigoError = 8955; // El número 8955 lo asigné al azar
            }
        }

    }
}
