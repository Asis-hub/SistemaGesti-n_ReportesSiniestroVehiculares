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
    public class DelegacionDAO
    {
        public static List<Delegacion> ConsultarDelegaciones(string consulta)
        {
            List<Delegacion> listaDelegaciones = new List<Delegacion>();
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
                        Delegacion delegacion = new Delegacion();
                        delegacion.IdDelegacion = (!dataReader.IsDBNull(0)) ? dataReader.GetInt32(0) : 0;
                        delegacion.IdMunicipio = (!dataReader.IsDBNull(1)) ? dataReader.GetInt32(1) : 0;
                        delegacion.Municipio = (!dataReader.IsDBNull(2)) ? dataReader.GetString(2) : "";
                        delegacion.Nombre = (!dataReader.IsDBNull(3)) ? dataReader.GetString(3) : "";
                        delegacion.Correo = (!dataReader.IsDBNull(4)) ? dataReader.GetString(4) : "";
                        delegacion.CodigoPostal = (!dataReader.IsDBNull(5)) ? dataReader.GetString(5) : "";
                        delegacion.Colonia = (!dataReader.IsDBNull(6)) ? dataReader.GetString(6) : "";
                        delegacion.Calle = (!dataReader.IsDBNull(7)) ? dataReader.GetString(7) : "";
                        delegacion.Numero = (!dataReader.IsDBNull(8)) ? dataReader.GetString(8) : "";
                        delegacion.IdTipo = (!dataReader.IsDBNull(9)) ? dataReader.GetInt32(9) : 0;
                        delegacion.Tipo = (!dataReader.IsDBNull(10)) ? dataReader.GetString(10) : "";
                        listaDelegaciones.Add(delegacion);
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
            return listaDelegaciones;
        }

        public static int RegistrarDelegacion(string consulta)
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

        public static int EditarDelegacion(string consulta)
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

        public static int EliminarDelegacion(string consulta)
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
