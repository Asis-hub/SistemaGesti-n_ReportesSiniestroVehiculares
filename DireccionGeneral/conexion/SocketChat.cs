using DireccionGeneral.interfaz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace DireccionGeneral.conexion
{
    public class SocketChat
    {
        public static bool encendido = false;
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
                encendido = true;
                MensajeChat mensajeLogin = new MensajeChat();
                mensajeLogin.Usuario = usuario;
                mensajeLogin.Tipo = TipoMensaje.Conectarse;
                string msjEnviar = JsonSerializer.Serialize(mensajeLogin);

                byte[] outStream = Encoding.ASCII.GetBytes(msjEnviar);
                serverStream.Write(outStream, 0, outStream.Length);
                serverStream.Flush();

                Thread threadListen = new Thread(EscucharMensaje);
                threadListen.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void Desconectar()
        {
            if (encendido)
            {
                Console.WriteLine("Desconectando");
                MensajeChat mensajeDesconexion = new MensajeChat();
                mensajeDesconexion.Usuario = usuario;
                mensajeDesconexion.Tipo = TipoMensaje.Desconectarse;

                string msjDesconexion = JsonSerializer.Serialize(mensajeDesconexion);
                byte[] msjEnviar = Encoding.ASCII.GetBytes(msjDesconexion);

                serverStream.Write(msjEnviar, 0, msjEnviar.Length);
                serverStream.Flush();


                
                encendido = false;
            }
        }

        public static void EnviarMensaje(string mensaje)
        {
            byte[] outStream = Encoding.ASCII.GetBytes(mensaje);
            serverStream.Write(outStream, 0, outStream.Length);

            serverStream.Flush();
        }

        private static void EscucharMensaje()
        {
            while (encendido)
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
                    Console.WriteLine("Cliente: " + returnData);
                }
            }
            clientSocket.Close();
        }
    }
}
