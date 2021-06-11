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
                        reporteSiniestro.FechaHora = (!dataReader.IsDBNull(4)) ? dataReader.GetDateTime(4) : System.DateTime.MinValue;
                        reporteSiniestro.IdDelegacion = (!dataReader.IsDBNull(5)) ? dataReader.GetInt32(5) : 0;
                        reporteSiniestro.Username = (!dataReader.IsDBNull(6)) ? dataReader.GetString(6) : "";
                        reporteSiniestro.Dictamen = (!dataReader.IsDBNull(7)) ? dataReader.GetBoolean(7) : false;
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

        public static ReporteSiniestro ObtenerReporte(string consulta)
        {
            ReporteSiniestro reporteSiniestro = new ReporteSiniestro();
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
                        reporteSiniestro.IdReporte = (!dataReader.IsDBNull(0)) ? dataReader.GetInt32(0) : 0;
                        reporteSiniestro.Calle = (!dataReader.IsDBNull(1)) ? dataReader.GetString(1) : "";
                        reporteSiniestro.Numero = (!dataReader.IsDBNull(2)) ? dataReader.GetString(2) : "";
                        reporteSiniestro.Colonia = (!dataReader.IsDBNull(3)) ? dataReader.GetString(3) : "";
                        reporteSiniestro.IdDelegacion = (!dataReader.IsDBNull(4)) ? dataReader.GetInt32(4) : 0;
                        reporteSiniestro.Username = (!dataReader.IsDBNull(5)) ? dataReader.GetString(5) : "";
                        reporteSiniestro.Dictamen = (!dataReader.IsDBNull(6)) ? dataReader.GetBoolean(6) : false;
                    }
                }
            } 
            catch
            {
                Console.WriteLine();
            }


            return reporteSiniestro;
        }

        public static int RegistrarReporte(String consulta)
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
                        //Console.WriteLine(resultado);

                        identificador = int.Parse((!resultado.IsDBNull(0) ? resultado[0].ToString() : "0"));

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

        public static int EliminarReporte(string consulta)
        {
            SqlConnection conexionDB = ConexionBD.GetConnection();
            int resultado = 0;
            try
            {
                if (conexionDB != null)
                {
                    SqlCommand comando = new SqlCommand(consulta, conexionDB);
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
                if (conexionDB != null)
                {
                    conexionDB.Close();
                }
            }

            return resultado;
        }
    }
}
