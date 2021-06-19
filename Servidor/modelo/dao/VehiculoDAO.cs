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
    /// Clase DAO que administra los vehiculos.Permite registrar, editar y eliminar vehiculos
    /// </summary>
    public class VehiculoDAO
    {
        /// <summary>
        /// Lista de vehiculos registrados
        /// </summary>
        /// <param name="consulta">Consulta en formato en SQL</param>
        /// <returns>Lista de vehiculos</returns>
        public static List<Vehiculo> ConsultarVehiculos(string consulta)
        {
            List<Vehiculo> listaVehiculos = new List<Vehiculo>();
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
                        Vehiculo vehiculo = new Vehiculo();
                        vehiculo.NumPlaca = (!dataReader.IsDBNull(0)) ? dataReader.GetString(0) : "";
                        vehiculo.Marca = (!dataReader.IsDBNull(1)) ? dataReader.GetString(1) : "";
                        vehiculo.Modelo = (!dataReader.IsDBNull(2)) ? dataReader.GetString(2) : "";
                        vehiculo.Color = (!dataReader.IsDBNull(3)) ? dataReader.GetString(3) : "";
                        vehiculo.NumPolizaSeguro = (!dataReader.IsDBNull(4)) ? dataReader.GetString(4) : "";
                        vehiculo.NombreAseguradora = (!dataReader.IsDBNull(5)) ? dataReader.GetString(5) : "";
                        vehiculo.Año = (!dataReader.IsDBNull(6)) ? dataReader.GetString(6) : "";
                        vehiculo.NumLicenciaConducir = (!dataReader.IsDBNull(7)) ? dataReader.GetString(7) : "";
                        listaVehiculos.Add(vehiculo);
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
            return listaVehiculos;
        }

        /// <summary>
        /// Permite registrar un nuevo vehiculo
        /// </summary>
        /// <param name="consulta"></param>
        /// <returns>1 - registrado correctamente, -1 - Error al registrar</returns>
        public static int RegistrarVehiculo(string consulta)
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

        /// <summary>
        /// Edita un vehiculo registrado
        /// </summary>
        /// <param name="consulta">Consulta en formato SQL</param>
        /// <returns>1 - editado correctamente, -1 - Error al editar</returns>
        public static int EditarVehiculo(string consulta)
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

        /// <summary>
        /// Elimina el registro de un Vehiculo
        /// </summary>
        /// <param name="consulta">Consulta en formato SQL</param>
        /// <returns>1 - eliminado correctamente, -1 - Error al eliminar</returns>
        public static int EliminarVehiculo(string consulta)
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
