using DelegacionMunicipal.interfaz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace DelegacionMunicipal.conexion
{
    public class SocketChat
    {
        public static bool conectado = false;
        private static string usuario;
        private static ObserverChat notificacionChat;
        private static TcpClient clientSocket;
        private static NetworkStream serverStream;

        public static void Conectar(string usuarioNuevo, ObserverChat notificacion)
        {
            try
            {
                usuario = usuarioNuevo;
                notificacionChat = notificacion;
                clientSocket = new TcpClient();
                serverStream = default(NetworkStream);
                clientSocket.Connect(IPAddress.Parse("127.0.0.1"), 1238);
                serverStream = clientSocket.GetStream();
                conectado = true;
                MensajeChat mensajeLogin = new MensajeChat();
                mensajeLogin.Usuario = usuario;
                mensajeLogin.Tipo = TipoMensaje.Conectarse;
                string msjEnviar = JsonSerializer.Serialize(mensajeLogin);

                byte[] outStream = Encoding.ASCII.GetBytes(msjEnviar);
                serverStream.Write(outStream, 0, outStream.Length);
                serverStream.Flush();

                Thread threadListen = new Thread(RecibirMensaje);
                threadListen.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void Desconectar()
        {
            if (conectado)
            {
                MensajeChat mensajeDesconexion = new MensajeChat();
                mensajeDesconexion.Usuario = usuario;
                mensajeDesconexion.Tipo = TipoMensaje.Desconectarse;

                string msjDesconexion = JsonSerializer.Serialize(mensajeDesconexion);
                byte[] msjEnviar = Encoding.ASCII.GetBytes(msjDesconexion);

                serverStream.Write(msjEnviar, 0, msjEnviar.Length);
                serverStream.Flush();
                conectado = false;
            }
        }

        public static void EnviarMensaje(string mensaje)
        {
            if (conectado)
            {
                try
                {
                    byte[] outStream = Encoding.ASCII.GetBytes(mensaje);
                    serverStream.Write(outStream, 0, outStream.Length);
                    serverStream.Flush();
                }
                catch (Exception ex)
                {
                    //El servidor apago el servicio
                    clientSocket.Close();
                }
            }
        }

        private static void RecibirMensaje()
        {
            while (conectado)
            {
                string returnData = "";
                try
                {
                    serverStream = clientSocket.GetStream();
                    byte[] inStream = new byte[65537];
                    int noBytes = serverStream.Read(inStream, 0, inStream.Length);
                    Array.Resize(ref inStream, noBytes);
                    returnData = Encoding.ASCII.GetString(inStream);
                    MensajeChat mensajeRecibido = JsonSerializer.Deserialize<MensajeChat>(returnData);

                    notificacionChat.MostrarMensaje(mensajeRecibido);

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Cliente Chat: " + ex.Message);
                    conectado = false;
                }
            }
            if (conectado)
            {
                clientSocket.Close();
            }

        }
    }
}
