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
    /// Clase DAO que administra a los usuarios
    /// </summary>
    public class UsuarioDAO
    {
        /// <summary>
        /// Permite a un usuario ingresar al sistema
        /// </summary>
        /// <param name="consulta">Consulta en formato SQL</param>
        /// <returns>Objeto tipo usuario si se encuntra registrado</returns>
        public static Usuario getInicioSesion(string consulta)
        {
            Usuario usuario = null;
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
                        usuario = new Usuario();
                        usuario.Username = (!dataReader.IsDBNull(0)) ? dataReader.GetString(0) : "";
                        usuario.NombreCompleto = (!dataReader.IsDBNull(1)) ? dataReader.GetString(1) : "";
                        usuario.IdDelegacion = (!dataReader.IsDBNull(2)) ? dataReader.GetInt32(2) : 0;
                        usuario.IdCargo = (!dataReader.IsDBNull(3)) ? dataReader.GetInt32(3) : 0;
                        usuario.Cargo = (!dataReader.IsDBNull(4)) ? dataReader.GetString(4) : "";
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

            return usuario;
        }

        /// <summary>
        /// Obtiene lista de los usuarios registrados
        /// </summary>
        /// <param name="consulta">Consulta en formato SQL</param>
        /// <returns></returns>
        public static List<Usuario> ConsultarUsuarios(string consulta)
        {
            List<Usuario> listaUsuarios = new List<Usuario>();
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
                        Usuario usuario = new Usuario();
                        usuario.Username = (!dataReader.IsDBNull(0)) ? dataReader.GetString(0) : "";
                        usuario.NombreCompleto = (!dataReader.IsDBNull(1)) ? dataReader.GetString(1) : "";
                        usuario.Password = (!dataReader.IsDBNull(2)) ? dataReader.GetString(2) : "";
                        usuario.IdDelegacion = (!dataReader.IsDBNull(3)) ? dataReader.GetInt32(3) : 0;
                        usuario.IdCargo = (!dataReader.IsDBNull(4)) ? dataReader.GetInt32(4) : 0;
                        usuario.Cargo = (!dataReader.IsDBNull(5)) ? dataReader.GetString(5) : "";

                        listaUsuarios.Add(usuario);
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
            return listaUsuarios;
        }

        /// <summary>
        /// Registra un usuario
        /// </summary>
        /// <param name="consulta">Consulta en formato SQL</param>
        /// <returns>1 - registrado correctamente, -1 - Error al registrar</returns>
        public static int RegistrarUsuario(string consulta)
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
        /// 
        /// </summary>
        /// <param name="consulta"></param>
        /// <returns></returns>
        public static int EditarUsuario(string consulta)
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
        /// Elimina un usuario registrado
        /// </summary>
        /// <param name="consulta">Consulta en formato SQL</param>
        /// <returns></returns>
        public static int EliminarUsuario(string consulta)
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
