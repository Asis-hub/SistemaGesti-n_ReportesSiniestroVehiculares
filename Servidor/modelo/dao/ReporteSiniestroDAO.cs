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
    public class ReporteSiniestroDAO
    {
        public static List<ReporteSiniestro> ConsultarReportes(string consulta)
        {
            List<ReporteSiniestro> listaReportes = new List<ReporteSiniestro>();
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
                        ReporteSiniestro reporteSiniestro = new ReporteSiniestro();
                        reporteSiniestro.IdReporte = (!dataReader.IsDBNull(0)) ? dataReader.GetInt32(0) : 0;
                        reporteSiniestro.Calle = (!dataReader.IsDBNull(1)) ? dataReader.GetString(1) : "";
                        reporteSiniestro.Numero = (!dataReader.IsDBNull(2)) ? dataReader.GetString(2) : "";
                        reporteSiniestro.Colonia = (!dataReader.IsDBNull(3)) ? dataReader.GetString(3) : "";
                        reporteSiniestro.IdDelegacion = (!dataReader.IsDBNull(4)) ? dataReader.GetInt32(4) : 0;
                        reporteSiniestro.Username = (!dataReader.IsDBNull(5)) ? dataReader.GetString(5) : "";
                        reporteSiniestro.Dictamen = (!dataReader.IsDBNull(6)) ? dataReader.GetBoolean(6) : false;
                        listaReportes.Add(reporteSiniestro);
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
            return listaReportes;
        }
    }
}
