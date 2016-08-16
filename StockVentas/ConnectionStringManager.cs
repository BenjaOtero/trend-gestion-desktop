using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace StockVentas
{
    class ConnectionStringManager
    {
        public static string GetConnectionString(string connectionStringName)
        {
            Configuration appconfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            ConnectionStringSettings connStringSettings = appconfig.ConnectionStrings.ConnectionStrings[connectionStringName];
            return connStringSettings.ConnectionString;
        }

        public static void SaveConnectionString(string connectionStringName, string connectionString)
        {
            Configuration appconfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            appconfig.ConnectionStrings.ConnectionStrings[connectionStringName].ConnectionString = connectionString;
            appconfig.Save();
        }

        public static List<string> GetConnectionStringNames()
        {
            List<string> cns = new List<string>();
            Configuration appconfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            foreach (ConnectionStringSettings cn in appconfig.ConnectionStrings.ConnectionStrings)
            {
                cns.Add(cn.Name);
            }
            return cns;
        }

        public static string GetFirstConnectionStringName()
        {
            return GetConnectionStringNames().FirstOrDefault();
        }

        public static string GetFirstConnectionString()
        {
            return GetConnectionString(GetFirstConnectionStringName());
        }

        public static string GetDatabaseName()
        {
            string cs = GetConnectionString(GetFirstConnectionStringName());
            if (cs != null)
            {
                MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder(cs);
                return builder.Database;
            }
            else
                return null;
        }

        public static string SetConnectionStringDatabaseName(string connectionString, string databaseName)
        {
            MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder(connectionString);
            builder.Database = databaseName;
            return builder.ConnectionString;
        }
    }
}
