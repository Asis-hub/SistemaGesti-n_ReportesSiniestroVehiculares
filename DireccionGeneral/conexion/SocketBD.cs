using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace DireccionGeneral.conexion
{
    public class SocketBD
    {
        private Socket socketCliente;
        private bool conectado = false;

        public void IniciarConexion()
        {
            try
            {
                socketCliente = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                IPEndPoint direccionConexion = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1236);
                socketCliente.Connect(direccionConexion);
                Console.WriteLine("Conectado");
                conectado = true;
            }
            catch (SocketException ex)
            {
                conectado = false;
            }
        }

        public void EnviarMensaje(string mensaje)
        {
            if (conectado)
            {
                mensaje += "<EOF>";
                byte[] msjEnviar = Encoding.Default.GetBytes(mensaje);
                socketCliente.SendBufferSize = msjEnviar.Length;

                Console.WriteLine("Send Buffer: {0} Tamaño arreglo: {1}", socketCliente.SendBufferSize, msjEnviar.Length);
                socketCliente.Send(msjEnviar, 0, socketCliente.SendBufferSize, 0);
                Console.WriteLine("Mensaje enviado");
            }
        }

        public string RecibirMensaje()
        {
            string mensaje = "";

            if (conectado)
            {
                while (true)
                {
                    Byte[] bytesRecibidos = new byte[1024];
                    int datos = socketCliente.Receive(bytesRecibidos);
                    mensaje += Encoding.ASCII.GetString(bytesRecibidos, 0, datos);
                    if (mensaje.IndexOf("<EOF>") > -1)
                    {
                        break;
                    }
                }
            }

            mensaje = mensaje.Replace("<EOF>", "");

            return mensaje;
        }

        public void TerminarConexion()
        {
            if (conectado)
            {
                socketCliente.Close();
                socketCliente.Dispose();
                Console.WriteLine("Conexión cerrada");
            }
        }

        public bool ConexionActiva()
        {
            return conectado;
        }
    }
}
