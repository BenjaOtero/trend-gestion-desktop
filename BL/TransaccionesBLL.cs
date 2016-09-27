using System;
using System.Data;
using MySql.Data.MySqlClient;
using DAL;

namespace BL
{
    public class TransaccionesBLL
    {

        public static void GrabarStockMovimientos(DataSet dtStockMov)
        {
          /*  MySqlTransaction tr = null;
            MySqlConnection SqlConnection1 = DALBase.GetConnection();
            tr = SqlConnection1.BeginTransaction();
            DataTable tblStock = dtStockMov.Tables[0];
            DataTable tblStockDetalle = dtStockMov.Tables[1];
        reintetarMov:
            try
            {           
                DAL.StockMovDAL.GrabarDB(dtStockMov, SqlConnection1, tr);
            }
            catch (MySqlException ex)
            {
                if (ex.Number == 1062) // clave principal duplicada
                {
                    Random rand = new Random();
                    int clave = rand.Next(1, 2000000000);
                    tblStock.Rows[0][0] = clave;
                    foreach (DataRow row in tblStockDetalle.Rows)
                    {
                        row["IdMovMSTKD"] = clave;
                    }
                    goto reintetarMov;
                }
                else
                {
                    tr.Rollback();
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                throw new Exception();
            }
        reintetarDetalle:
        try
        {
            DAL.StockMovDetalleDAL.GrabarDB(dtStockMov, SqlConnection1, tr);
        }
        catch (MySqlException ex)
        {
            if (ex.Number == 1062) // clave principal duplicada
            {
                Random rand = new Random();
                int clave;
                foreach (DataRow row in tblStockDetalle.Rows)
                {
                    clave = rand.Next(1, 2000000000);
                    row["IdMSTKD"] = clave;
                }
                goto reintetarDetalle;
            }
            else
            {
                tr.Rollback();
                throw new Exception();
            }
        }
        catch (Exception)
        {
            throw new Exception();
        }
            tr.Commit();
            SqlConnection1.Close();*/
        }

        public static void GrabarArticulosAgrupar(DataTable tblStock, DataTable tblArticulos, ref int? codigoError)
        {
            MySqlTransaction tr = null;
            try
            {
                MySqlConnection SqlConnection1 = DALBase.GetConnection();                
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
