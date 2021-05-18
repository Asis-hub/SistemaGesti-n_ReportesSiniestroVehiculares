using Servidor.modelo.db;
using Servidor.modelo.poco;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace Servidor.servicios
{
    public class SocketBD
    {
        private Socket socketServer;
        private bool encendido = false;

        public void IniciarConexion()
        {
            socketServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint direccion = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1236);
            socketServer.Bind(direccion);
            socketServer.Listen(200);
            encendido = true;
        }

        public void RecibirMensaje()
        {
            try
            {
                while (true)
                {
                    string mensaje = "";
                    Socket socketClienteRemoto = socketServer.Accept();
                    
                    //Obtener información del cliente conectado
                    IPEndPoint cliente = (IPEndPoint)socketClienteRemoto.RemoteEndPoint;
                    
                    Paquete paqueteRecibido = null;
                    
                    //Recibir mensaje
                    while (true)
                    {
                        Byte[] bytesRecibidos = new byte[socketClienteRemoto.SendBufferSize];
                        int cantidadBytes = socketClienteRemoto.Receive(bytesRecibidos, 0, bytesRecibidos.Length, 0);
                        Array.Resize(ref bytesRecibidos, cantidadBytes);


                        //Recibir Paquete
                        mensaje = Encoding.ASCII.GetString(bytesRecibidos, 0, bytesRecibidos.Length);
                        if (mensaje.IndexOf("<EOF>") > -1)
                        {
                            mensaje = mensaje.Replace("<EOF>", "");
                            paqueteRecibido = JsonSerializer.Deserialize<Paquete>(mensaje);
                            break;
                        }
                    }

                    //Se atiende la petición
                    ProcesarPaquete(socketClienteRemoto, paqueteRecibido);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        public void TerminarConexion()
        {
            socketServer.Close();
            socketServer.Dispose();
            encendido = false;
            
            Console.WriteLine("Conexión del servidor terminada");
        }

        public bool ConexionActiva()
        {
            return encendido;
        }

        
        private void ProcesarPaquete(Socket clienteRemoto, Paquete paquete)
        {
            if(paquete.TipoQuery == TipoConsulta.Select)
            {
                ProcesarSeleccion(clienteRemoto, paquete);
            }
            else if(paquete.TipoQuery == TipoConsulta.Insert)
            {
                ProcesarModificacion(clienteRemoto, paquete);
            }
            else if(paquete.TipoQuery == TipoConsulta.Delete)
            {
                ProcesarModificacion(clienteRemoto, paquete);
            }
            else if(paquete.TipoQuery == TipoConsulta.Update)
            {
                ProcesarModificacion(clienteRemoto, paquete);
            }
        }

        private void ProcesarSeleccion(Socket clienteRemoto, Paquete paquete) 
        {
            SqlConnection conexionBD = ConexionBD.GetConnection();
            string respuesta = "";
            try
            {
                if (conexionBD != null)
                {
                    SqlCommand command;
                    SqlDataReader dataReader;

                    command = new SqlCommand(paquete.Consulta, conexionBD);
                    dataReader = command.ExecuteReader();

                    //Lista de Delegaciones
                    if (paquete.TipoDominio == TipoDato.Delegacion)
                    {
                        List<Delegacion> listaDelegaciones = new List<Delegacion>();
                        while (dataReader.Read())
                        {
                            Delegacion delegacion = new Delegacion();
                            delegacion.IdDelegacion = (!dataReader.IsDBNull(0)) ? dataReader.GetInt32(0) : 0;
                            delegacion.Municipio = (!dataReader.IsDBNull(1)) ? dataReader.GetString(1) : "";
                            delegacion.Nombre = (!dataReader.IsDBNull(2)) ? dataReader.GetString(2) : "";
                            delegacion.Correo = (!dataReader.IsDBNull(3)) ? dataReader.GetString(3) : "";
                            delegacion.CodigoPostal = (!dataReader.IsDBNull(4)) ? dataReader.GetString(4) : "";
                            delegacion.Colonia = (!dataReader.IsDBNull(5)) ? dataReader.GetString(5) : "";
                            delegacion.Calle = (!dataReader.IsDBNull(6)) ? dataReader.GetString(6) : "";
                            delegacion.Numero = (!dataReader.IsDBNull(7)) ? dataReader.GetString(7) : "";
                            delegacion.Tipo = (!dataReader.IsDBNull(8)) ? dataReader.GetString(8) : "";
                            listaDelegaciones.Add(delegacion);
                        }
                        respuesta = JsonSerializer.Serialize(listaDelegaciones);
                    }

                    //Lista de Usuarios
                    if (paquete.TipoDominio == TipoDato.Usuario)
                    {
                        List<Usuario> listaUsuarios = new List<Usuario>();
                        while (dataReader.Read())
                        {
                            Usuario usuario = new Usuario();
                            usuario.Username = (!dataReader.IsDBNull(0)) ? dataReader.GetString(0) : "";
                            usuario.NombreCompleto = (!dataReader.IsDBNull(1)) ? dataReader.GetString(1) : "";
                            usuario.Password = (!dataReader.IsDBNull(2)) ? dataReader.GetString(2) : "";
                            usuario.IdDelegacion = (!dataReader.IsDBNull(3)) ? dataReader.GetInt32(3) : 0;
                            usuario.Cargo = (!dataReader.IsDBNull(4)) ? dataReader.GetString(4) : "";

                            listaUsuarios.Add(usuario);
                        }
                        respuesta = JsonSerializer.Serialize(listaUsuarios);
                    }

                    //Lista Municipios
                    if (paquete.TipoDominio == TipoDato.Municipio)
                    {
                        List<Municipio> listaMunicipios = new List<Municipio>();
                        while (dataReader.Read())
                        {
                            Municipio municipio = new Municipio();
                            municipio.IdMunicipio = (!dataReader.IsDBNull(0)) ? dataReader.GetInt32(0) : 0;
                            municipio.Nombre = (!dataReader.IsDBNull(1)) ? dataReader.GetString(1) : "";

                            listaMunicipios.Add(municipio);
                        }
                        respuesta = JsonSerializer.Serialize(listaMunicipios);
                    }

                    //Lista Tipos de Delegaciones
                    if (paquete.TipoDominio == TipoDato.DelegacionTipo)
                    {
                        List<DelegacionTipo> listaTiposDelegacion = new List<DelegacionTipo>();
                        while (dataReader.Read())
                        {
                            DelegacionTipo delegacionTipo = new DelegacionTipo();
                            delegacionTipo.IdTipoDelegacion = (!dataReader.IsDBNull(0)) ? dataReader.GetInt32(0) : 0;
                            delegacionTipo.TipoDelegacion = (!dataReader.IsDBNull(1)) ? dataReader.GetString(1) : "";

                            listaTiposDelegacion.Add(delegacionTipo);
                        }
                        respuesta = JsonSerializer.Serialize(listaTiposDelegacion);
                    }
                    dataReader.Close();
                    command.Dispose();
                }
                respuesta += "<EOF>";
                byte[] msgRespuesta = Encoding.Default.GetBytes(respuesta);
                clienteRemoto.Send(msgRespuesta, 0, msgRespuesta.Length, 0);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }finally
            {
                if (conexionBD != null)
                {
                    conexionBD.Close();
                }
            }
        }

        /*
         *  0 - No fue posible ejecutar el query.
         *  1 - El query se ejecutó con exito
         * -1 - Error al ejecutar el query
         */
        private void ProcesarModificacion(Socket clienteRemoto, Paquete paquete)
        {
            SqlConnection conexionBD = ConexionBD.GetConnection();
            int resultado = 0;
            try
            {
                if (conexionBD != null)
                {
                    try
                    {
                        if (paquete.TipoDominio == TipoDato.Usuario)
                        {
                            SqlCommand comando = new SqlCommand(paquete.Consulta, conexionBD);
                            resultado = comando.ExecuteNonQuery();
                            comando.Dispose();
                        }
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show(ex.Message, "Error de Modificación en la Base de Datos");
                        resultado = -1;
                    }
                }
                byte[] msgRespuesta = Encoding.Default.GetBytes(resultado + "<EOF>");
                clienteRemoto.Send(msgRespuesta, 0, msgRespuesta.Length, 0);
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message, "No es posible conectarse a la base de datos, Base de datos no disponible");
            }
            finally
            {
                if(conexionBD != null)
                {
                    conexionBD.Close();
                }
            }
        }
    }
}


