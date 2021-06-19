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
    /// <summary>
    /// Clase DAO que administra el registro de fotografias
    /// </summary>
    class FotografiaDAO
    {
        /// <summary>
        /// Obtiene lista de Fotografias
        /// </summary>
        /// <param name="consulta">Consulta en formato SQL</param>
        /// <returns>Lista de fotografias de un reporte</returns>
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

        /// <summary>
        /// Guarda el registro de una fotografia de un reporte
        /// </summary>
        /// <param name="consulta">Consulta en formato SQL</param>
        /// <returns>1 - registrado correctamente, -1 - Error al registrar</returns>
        public static int InsertarFotografia(string consulta)
        {
            SqlConnection conexionDB = ConexionBD.GetConnection();
            int identificador = 0;
            
            SqlDataReader resultado;
            try
            {
                if (conexionDB != null)
                {
                    SqlCommand comando = new SqlCommand(consulta, conexionDB);
                    resultado = comando.ExecuteReader();

                    if (resultado.Read())
                    {
                        identificador = int.Parse(!resultado.IsDBNull(0) ? resultado[0].ToString() : "0");
                    }
                    comando.Dispose();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                identificador = 0;
            }
            finally
            {
                if (conexionDB != null)
                {
                    conexionDB.Close();
                }
            }
            return identificador;
        }
    }
}
