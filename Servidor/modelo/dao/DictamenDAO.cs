using Servidor.modelo.dao.db;
using Servidor.modelo.poco;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servidor.modelo.dao
{
    public class DictamenDAO
    {
        public static Dictamen ConsultarDictamenDeReporte(string consulta)
        {
            Dictamen dictamen = null;
            SqlConnection conn = null;

            try
            {
                conn = ConexionBD.GetConnection();
                if (conn != null)
                {
                    SqlCommand comando;
                    SqlDataReader dataReader;
                    comando = new SqlCommand(consulta, conn);
                    dataReader = comando.ExecuteReader();
                    
                    if (dataReader.Read())
                    {
                        dictamen = new Dictamen();
                        dictamen.Folio = (!dataReader.IsDBNull(0)) ? dataReader.GetInt32(0) : 0;
                        dictamen.Descripcion = (!dataReader.IsDBNull(1)) ? dataReader.GetString(1) : "";
                        dictamen.FechaHora = (!dataReader.IsDBNull(2)) ? dataReader.GetDateTime(2) : System.DateTime.MinValue;
                        dictamen.IdReporte = (!dataReader.IsDBNull(3)) ? dataReader.GetInt32(3) : 0;
                        dictamen.Username = (!dataReader.IsDBNull(4)) ? dataReader.GetString(4) : "";
                        dictamen.Perito = (!dataReader.IsDBNull(5)) ? dataReader.GetString(5) : "";
                    }

                    dataReader.Close();
                    comando.Dispose();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return dictamen;
        }

        public static int RegistrarDictamen(string consulta)
        {
            SqlConnection conexionBD = ConexionBD.GetConnection();
            int resultado = 0;
            try
            {
                if (conexionBD != null)
                {
                    SqlCommand comando = new SqlCommand(consulta, conexionBD);
                    comando.ExecuteNonQuery();
                    comando.Dispose();
                    resultado = 1;
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
