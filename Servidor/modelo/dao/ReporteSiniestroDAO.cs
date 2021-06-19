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
    /// Clase DAO que permite el administrar los reportes de Siniestor
    /// </summary>
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
                        reporteSiniestro.FechaRegistro = (!dataReader.IsDBNull(8)) ? dataReader.GetDateTime(8) : System.DateTime.MinValue;
                        reporteSiniestro.NombreDelegacion = (!dataReader.IsDBNull(9) ? dataReader.GetString(9) : "");
                        reporteSiniestro.NombreUsuario = (!dataReader.IsDBNull(10) ? dataReader.GetString(10) : "");
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

        /// <summary>
        /// Obtiene un reporte de acuredo al id espicificado en la consulta
        /// </summary>
        /// <param name="consulta">Consulta en formato SQL</param>
        /// <returns>Reporte de Siniestro</returns>
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
                        reporteSiniestro.FechaHora = (!dataReader.IsDBNull(4)) ? dataReader.GetDateTime(4) : System.DateTime.MinValue;
                        reporteSiniestro.IdDelegacion = (!dataReader.IsDBNull(5)) ? dataReader.GetInt32(5) : 0;
                        reporteSiniestro.Username = (!dataReader.IsDBNull(6)) ? dataReader.GetString(6) : "";
                        reporteSiniestro.Dictamen = (!dataReader.IsDBNull(7)) ? dataReader.GetBoolean(7) : false;
                        reporteSiniestro.FechaRegistro = (!dataReader.IsDBNull(8)) ? dataReader.GetDateTime(8) : System.DateTime.MinValue;
                        reporteSiniestro.NombreDelegacion = (!dataReader.IsDBNull(9) ? dataReader.GetString(9) : "");
                        reporteSiniestro.NombreUsuario = (!dataReader.IsDBNull(10) ? dataReader.GetString(10) : "");
                    }
                }
            } 
            catch
            {
                Console.WriteLine();
            }
            return reporteSiniestro;
        }

        /// <summary>
        /// Registra el reporte de un siniestro
        /// </summary>
        /// <param name="consulta">Consulta en formato SQL</param>
        /// <returns>1 - registrado correctamente, -1 - Error al registrar</returns>
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

        /// <summary>
        /// Elimina un reporte de siniestro
        /// </summary>
        /// <param name="consulta"></param>
        /// <returns></returns>
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
