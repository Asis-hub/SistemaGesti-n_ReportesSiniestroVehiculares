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
    /// Clase DAO que permite consultar el catalogo de tipos de delegación
    /// </summary>
    public class DelegacionTipoDAO
    {
        /// <summary>
        /// Obtiene el catalogo de tipo de delegaciones
        /// </summary>
        /// <param name="consulta">Consulta en formato SQL</param>
        /// <returns></returns>
        public static List<DelegacionTipo> ConsultarTipos(string consulta)
        {
            List<DelegacionTipo> listaTiposDelegacion = new List<DelegacionTipo>();
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
                        DelegacionTipo delegacionTipo = new DelegacionTipo();
                        delegacionTipo.IdTipoDelegacion = (!dataReader.IsDBNull(0)) ? dataReader.GetInt32(0) : 0;
                        delegacionTipo.TipoDelegacion = (!dataReader.IsDBNull(1)) ? dataReader.GetString(1) : "";

                        listaTiposDelegacion.Add(delegacionTipo);
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
            return listaTiposDelegacion;
        }
    }
}
