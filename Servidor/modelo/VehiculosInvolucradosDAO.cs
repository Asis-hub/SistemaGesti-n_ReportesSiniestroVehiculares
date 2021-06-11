using Servidor.modelo.dao.db;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servidor.modelo
{
    class VehiculosInvolucradosDAO
    {
        internal static int InsertarVehiculo(string consulta)
        {
            SqlConnection conexionBD = ConexionBD.GetConnection();
            int resultado = 0;
            try
            {
                if (conexionBD != null)
                {
                    SqlCommand comando = new SqlCommand(consulta, conexionBD);
                    resultado = comando.ExecuteNonQuery();
                    comando.Dispose();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                resultado = -1;
            }
            finally
            {
                if (conexionBD != null)
                {
                    conexionBD.Close();
                }

            }
            return resultado;
        }
    }
}
