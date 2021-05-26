using Servidor.modelo.dao;
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
            string mensaje = "";

            if (paquete.TipoQuery == TipoConsulta.Select && paquete.TipoDominio == TipoDato.Delegacion)
            {
                List<Delegacion> listaDelegaciones = DelegacionDAO.ConsultarDelegaciones(paquete.Consulta);
                mensaje = JsonSerializer.Serialize(listaDelegaciones);
            }
            else if (paquete.TipoQuery == TipoConsulta.Select && paquete.TipoDominio == TipoDato.Usuario)
            {
                Usuario usuario = UsuarioDAO.getInicioSesion(paquete.Consulta);
                if (usuario != null)
                {
                    mensaje = JsonSerializer.Serialize(usuario);
                }
            }

            mensaje += "<EOF>";
            byte[] msjEnviar = Encoding.Default.GetBytes(mensaje);
            clienteRemoto.Send(msjEnviar, 0, msjEnviar.Length, 0);
            Console.WriteLine("Consulta enviada");
        }
    }
}
