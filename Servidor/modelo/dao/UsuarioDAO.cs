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
    public class UsuarioDAO
    {
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
