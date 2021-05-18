using Servidor.modelo.db;
using Servidor.modelo.poco;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Servidor.servicios
{
    class SocketLogin
    {
        private Socket socketServer;
        private bool encendido = false;

        public void IniciarConexion()
        {
            socketServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint direccion = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1234);
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
                    Console.WriteLine("Escuchando...");
                    Socket socketClienteRemoto = socketServer.Accept();

                    //Obtener información del cliente conectado
                    IPEndPoint cliente = (IPEndPoint)socketClienteRemoto.RemoteEndPoint;
                    Console.WriteLine("Clente conectado con IP {0} en puerto {1}", cliente.Address, cliente.Port);

                    //Recibir mensaje
                    while (true)
                    {
                        Byte[] bytesRecibidos = new byte[socketClienteRemoto.SendBufferSize];
                        int datos = socketClienteRemoto.Receive(bytesRecibidos,0,bytesRecibidos.Length,0);
                        Array.Resize(ref bytesRecibidos, datos);
                        mensaje = Encoding.ASCII.GetString(bytesRecibidos, 0, datos);
                        
                        if (mensaje.IndexOf("<EOF>") > -1)
                        {
                            break;
                        }
                    }

                    //Escribir como cadena la información de socket cliente
                    mensaje = mensaje.Replace("<EOF>","");

                    //Deserializar mensaje en clase Paquete
                    Paquete paquete = JsonSerializer.Deserialize<Paquete>(mensaje);

                    //Se atiende la petición
                    ProcesarPeticion(socketClienteRemoto, paquete);
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


        private void ProcesarPeticion(Socket clienteRemoto, Paquete paquete)
        {
            SqlConnection conn = null;
            string mensaje = "";
            try
            {
                conn = ConexionBD.GetConnection();
                if (conn != null)
                {
                    SqlCommand comando;
                    SqlDataReader dataReader;

                    comando = new SqlCommand(paquete.Consulta, conn);
                    dataReader = comando.ExecuteReader();

                    if (paquete.TipoQuery == TipoConsulta.Select && paquete.TipoDominio == TipoDato.Delegacion)
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
                        mensaje = JsonSerializer.Serialize(listaDelegaciones);
                        dataReader.Close();
                        comando.Dispose();

                    }
                    else if (paquete.TipoQuery == TipoConsulta.Select && paquete.TipoDominio == TipoDato.Usuario)
                    {
                        if (dataReader.Read())
                        {
                            Usuario usuario = new Usuario();
                            usuario.Username = (!dataReader.IsDBNull(0)) ? dataReader.GetString(0) : "";
                            usuario.NombreCompleto = (!dataReader.IsDBNull(1)) ? dataReader.GetString(1) : "";
                            usuario.IdDelegacion = (!dataReader.IsDBNull(2)) ? dataReader.GetInt32(2) : 0;
                            usuario.Cargo = (!dataReader.IsDBNull(3)) ? dataReader.GetString(3) : "";

                            mensaje = JsonSerializer.Serialize(usuario);
                        }
                        dataReader.Close();
                        comando.Dispose();
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("Conexion fallida: " + e.Message);
            }
            finally
            {
                if(conn != null)
                {
                    conn.Close();
                }
            }

            //Si ourre un error solo se enviaria el mensaje <EOF>
            mensaje += "<EOF>";
            byte[] msjEnviar = Encoding.Default.GetBytes(mensaje);
            clienteRemoto.Send(msjEnviar, 0, msjEnviar.Length, 0);
            Console.WriteLine("Consulta enviada");
        }
    }
}
