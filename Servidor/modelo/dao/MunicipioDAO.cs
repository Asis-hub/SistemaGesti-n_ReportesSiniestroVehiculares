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
    /// Clase DAO que administra el Catálogo de municipios
    /// </summary>
    public class MunicipioDAO
    {
        /// <summary>
        /// Obtiene la lista de municipios registrados
        /// </summary>
        /// <param name="consulta">Consulta en formato SQL</param>
        /// <returns>Lista de Municipios</returns>
        public static List<Municipio> ConsultarMunicipios(string consulta)
        {
            List<Municipio> listaMunicipios = new List<Municipio>();
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
                        Municipio municipio = new Municipio();
                        municipio.IdMunicipio = (!dataReader.IsDBNull(0)) ? dataReader.GetInt32(0) : 0;
                        municipio.Nombre = (!dataReader.IsDBNull(1)) ? dataReader.GetString(1) : "";

                        listaMunicipios.Add(municipio);
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
            return listaMunicipios;
        }
    }
}
