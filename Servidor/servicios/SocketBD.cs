using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Servidor.servicios
{
    public class SocketBD
    {
        private Socket socketServer;
        private bool encendido = false;

        public void IniciarConexion()
        {
            socketServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint direccion = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1235);
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

                    Paquete paqueteRecibido = null;

                    //Recibir mensaje
                    while (true)
                    {
                        Byte[] bytesRecibidos = new byte[socketClienteRemoto.SendBufferSize];
                        socketClienteRemoto.Receive(bytesRecibidos);
                        
                        //Recibir Paquete
                        mensaje = Encoding.ASCII.GetString(bytesRecibidos, 0, bytesRecibidos.Length);
                        if (mensaje.IndexOf("<EOF>") > -1)
                        {
                            mensaje = mensaje.Replace("<EOF>", "");
                            Paquete paquete = JsonSerializer.Deserialize<Paquete>(mensaje);
                            break;
                        }

                        //Recibir imágenes en el caso de un reporte
                    }

                    

                    //Se atiende la petición
                    //ProcesarPeticion(socketClienteRemoto, paquete);
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

        /*
        private void ProcesarPeticion(Socket clienteRemoto, Paquete paquete)
        {

            SqlConnection conn = ConexionBD.GetConnection();
            string mensaje = "";
            if (conn != null)
            {
                SqlCommand command;
                SqlDataReader dataReader;

                command = new SqlCommand(paquete.Consulta, conn);
                dataReader = command.ExecuteReader();

                if (paquete.TipoQuery == TipoConsulta.Select && paquete.TipoDominio == TipoDato.Delegacion)
                {
                    List<Delegacion> listaDelegaciones = new List<Delegacion>();
                    while (dataReader.Read())
                    {
                        Delegacion delegacion = new Delegacion();
                        delegacion.IdDelegacion = (!dataReader.IsDBNull(0)) ? dataReader.GetInt32(0) : 0;
                        delegacion.IdMunicipio = (!dataReader.IsDBNull(1)) ? dataReader.GetInt32(1) : 0;
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
                }

                mensaje += "<EOF>";
                Console.WriteLine("Server RES: " + mensaje);
                byte[] msjEnviar = Encoding.Default.GetBytes(mensaje);
                clienteRemoto.Send(msjEnviar, 0, msjEnviar.Length, 0);
                Console.WriteLine("Consulta enviada");
            }
            else
            {
                Console.WriteLine("Conexion fallida");
            }

        }
        */
    }
}
