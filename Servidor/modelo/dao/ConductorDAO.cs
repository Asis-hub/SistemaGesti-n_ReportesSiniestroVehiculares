﻿using Servidor.modelo.dao.db;
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
    /// Clase DAO que permite administrar los conductores
    /// </summary>
    public class ConductorDAO
    {
        /// <summary>
        /// Obtiene una lista de los conductores registrados
        /// </summary>
        /// <param name="consulta">Consulta SQL</param>
        /// <returns>Lista de conductores</returns>
        public static List<Conductor> ConsultarConductores(string consulta)
        {
            List<Conductor> listaConductores = new List<Conductor>();
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
                        Conductor conductor = new Conductor();
                        conductor.NumeroLicencia = (!dataReader.IsDBNull(0)) ? dataReader.GetString(0) : "";
                        conductor.Celular = (!dataReader.IsDBNull(1)) ? dataReader.GetString(1) : "";
                        conductor.NombreCompleto = (!dataReader.IsDBNull(2)) ? dataReader.GetString(2) : "";
                        conductor.FechaNacimiento = (!dataReader.IsDBNull(3)) ? dataReader.GetDateTime(3) : System.DateTime.MinValue;
                        listaConductores.Add(conductor);
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
            return listaConductores;
        }

        /// <summary>
        /// Registrar conductor en la base de datos
        /// </summary>
        /// <param name="consulta">Consulta SQL</param>
        /// <returns>1 - registrado correctamente, -1 - Error al registrar</returns>
        public static int RegistrarConductor(string consulta)
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
        /// Modifica el registro de un conductor
        /// </summary>
        /// <param name="consulta">Consulta SQL</param>
        /// <returns>1 - actualizado correctamente, -1 - Error al actualizar</returns>
        public static int EditarConductor(string consulta)
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
        /// Elimina un coductor del registro
        /// </summary>
        /// <param name="consulta">Consulta SQL</param>
        /// <returns>1 - eliminado correctamente, -1 - Error al eliminar</returns>
        public static int EliminarConductor(string consulta)
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
