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
    class FotografiaDAO
    {

        public static List<Fotografia> ObtenerFotografias(string consulta)
        {
            List<Fotografia> fotografias = new List<Fotografia>();

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

                    while (dataReader.Read())
                    {
                        Fotografia fotografia = new Fotografia();
                        fotografia.IdFotografia = (!dataReader.IsDBNull(0)) ? dataReader.GetInt32(0) : 0;
                        fotografia.IdReporte = (!dataReader.IsDBNull(1)) ? dataReader.GetInt32(1) : 0;
                        fotografias.Add(fotografia);
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


            return fotografias;
        }
    }
}
