using Servidor.modelo.dao;
using Servidor.modelo.dao.db;
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
using System.Threading;
using System.Threading.Tasks;

namespace Servidor.servicios
{
    public class SocketLogin
    {
        private Socket socketServer;
        private bool encendido;
        private Thread hiloRecibirMensajes;

        public bool Encendido { get => encendido; }

        public SocketLogin()
        {
            encendido = false;
        }

        public void IniciarConexion()
        {
            socketServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint direccion = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1234);
            socketServer.Bind(direccion);
            socketServer.Listen(10);
            hiloRecibirMensajes = new Thread(new ThreadStart(RecibirMensaje));
            hiloRecibirMensajes.Start();
            encendido = true; 
        }

        public void TerminarConexion()
        {
            if (encendido)
            {
                socketServer.Close();
            }
        }

        private void EnviarMensaje(Socket socketCliente, string respuesta)
        {
            if (Encendido)
            {
                respuesta += "<EOF>";
                byte[] msjEnviar = Encoding.Default.GetBytes(respuesta);
                socketCliente.SendBufferSize = msjEnviar.Length;
                socketCliente.Send(msjEnviar, 0, socketCliente.SendBufferSize, 0);
                Console.WriteLine("Respuesta Enviada");
            }
        }

        private void RecibirMensaje()
        {
            while (Encendido)
            {
                try
                {
                    string mensaje = "";
                    string respuesta = "";
                    Console.WriteLine("Escuchando...");
                    Socket socketClienteRemoto = socketServer.Accept();

                    //Recibir mensaje
                    Byte[] bytesRecibidos = new byte[socketClienteRemoto.SendBufferSize];
                    int datos = socketClienteRemoto.Receive(bytesRecibidos, 0, bytesRecibidos.Length, 0);
                    Array.Resize(ref bytesRecibidos, datos);
                    mensaje = Encoding.ASCII.GetString(bytesRecibidos, 0, datos);

                    if (mensaje.IndexOf("<EOF>") > -1)
                    {
                        //Escribir como cadena la información de socket cliente
                        mensaje = mensaje.Replace("<EOF>", "");

                        //Deserializar mensaje en clase Paquete
                        Paquete paquete = JsonSerializer.Deserialize<Paquete>(mensaje);

                        //Se atiende la petición
                        respuesta = ProcesarPeticion(socketClienteRemoto, paquete);
                    }

                    EnviarMensaje(socketClienteRemoto, respuesta);
                }
                catch (SocketException ex)
                {
                    if (ex.ErrorCode == 10004)
                    {
                        encendido = false;
                    }
                }
                catch (JsonException ex)
                {
                    Console.WriteLine("Error Json: " + ex.Message);
                }
            }
            socketServer.Dispose();
            Console.WriteLine("Socket Login apagado");
        }
        
        private string ProcesarPeticion(Socket clienteRemoto, Paquete paquete)
        {
            string respuesta = "";

            if (paquete.TipoQuery == TipoConsulta.Select && paquete.TipoDominio == TipoDato.Delegacion)
            {
                List<Delegacion> listaDelegaciones = DelegacionDAO.ConsultarDelegaciones(paquete.Consulta);
                respuesta = JsonSerializer.Serialize(listaDelegaciones);
            }
            else if (paquete.TipoQuery == TipoConsulta.Select && paquete.TipoDominio == TipoDato.Usuario)
            {
                Usuario usuario = UsuarioDAO.getInicioSesion(paquete.Consulta);
                if (usuario != null)
                {
                    respuesta = JsonSerializer.Serialize(usuario);
                }
            }

            return respuesta;

        }
    }
}

