using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servidor.modelo.db
{
    public class ConexionBD
    {
        private static String SERVER = "maisonbleue2020.ddns.net";
        private static String PORT = "1433";
        private static String DATABASE = "sgsrv";
        private static String USER = "sgsrv";
        private static String PASSWORD = "14052021";
        //private static String USER = "db_sgrsv";
        //private static String PASSWORD = "01052021";

        public static SqlConnection GetConnection()
        {
            SqlConnection conexion = null;
            try
            {
                String urlConexion = String.Format("Data Source={0},{1};" +
                    "Network Library=DBMSSOCN;" +
                    "Initial Catalog={2};" +
                    "User ID={3};" +
                    "Password={4};", SERVER, PORT, DATABASE, USER, PASSWORD);
                conexion = new SqlConnection(urlConexion);
                conexion.Open();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return conexion;
        }
    }
}
