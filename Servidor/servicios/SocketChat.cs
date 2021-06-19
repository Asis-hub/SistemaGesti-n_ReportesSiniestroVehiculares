using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Servidor.servicios
{
    /// <summary>
    /// Socket encargado de administrar el envio y recepción de mensaje de la sala de chat
    /// </summary>
    public class SocketChat
    {
        public static Hashtable listaClientes = new Hashtable();
        public static int tamañoBuffer = 65537;
        private static TcpListener serverSocket;
        private static TcpClient clientSocket;
        private static string ip = Properties.Settings.Default.IP;
        private static int puerto = Properties.Settings.Default.PuertoSalaChat;
        private static bool encendido = false;

        public static bool Encendido { get => encendido; }
        
        /// <summary>
        /// Enciende el socket  
        /// </summary>
        public static void IniciarConexion()
        {
            serverSocket = new TcpListener(IPAddress.Parse(ip), puerto);
            clientSocket = default(TcpClient);
            listaClientes = new Hashtable();
            serverSocket.Start();
            Console.WriteLine("Servidor de Chat iniciado");
            encendido = true;
            Thread hiloRecibirConezion = new Thread(RecibirConexion);
            hiloRecibirConezion.Start();
        }

        /// <summary>
        /// Apaga el socket de la sala de chat
        /// </summary>
        public static void TerminarConexion()
        {
            if (encendido)
            {
                encendido = false;
                serverSocket.Stop();
            }
        }

        /// <summary>
        /// Recibe los mensaje que los clientes que se conectan a la sala de chat
        /// </summary>
        private static void RecibirConexion()
        {
            while (encendido)
            {
                string msjCliente = "";
                try
                {
                    clientSocket = serverSocket.AcceptTcpClient();
                    byte[] bytesRecibidos = new byte[tamañoBuffer];
                    NetworkStream networkStream = clientSocket.GetStream();
                    int numBytes = networkStream.Read(bytesRecibidos, 0, bytesRecibidos.Length);
                    
                    Array.Resize(ref bytesRecibidos, numBytes);
                    msjCliente = Encoding.ASCII.GetString(bytesRecibidos);
                    MensajeChat mensajeLogin = JsonSerializer.Deserialize<MensajeChat>(msjCliente); ;

                    if (listaClientes.Contains(mensajeLogin.Usuario))
                    {
                        //Usuario con una sesion abierta previamente
                        List<TcpClient> listaSockets = (List<TcpClient>)listaClientes[mensajeLogin.Usuario];
                        listaSockets.Add(clientSocket);
                        listaClientes[mensajeLogin.Usuario] = listaSockets;
                    }
                    else
                    {
                        //Usuario se conecta por primera vez
                        string usario = mensajeLogin.Usuario;
                        Console.WriteLine("Nombre cliente: " + usario);
                        List<TcpClient> listaSockets = new List<TcpClient>();
                        listaSockets.Add(clientSocket);
                        listaClientes.Add(usario, listaSockets);
                        Console.WriteLine(usario + " se unio al chat....");
                        //Notificación a todos
                        
                        NotificarClientes(mensajeLogin);
                    }
                    //Asignacion del reesponsable del socket cliente
                    ClienteRemoto clienteRemoto = new ClienteRemoto();
                    clienteRemoto.InicializarCliente(clientSocket, mensajeLogin.Usuario);
                }
                catch(SocketException ex)
                {
                    if (ex.ErrorCode == 10004)//Excepcion producida por serverSocket.Stop();
                    {
                        encendido = false;
                        Console.WriteLine(ex.Message);
                    }
                    else
                    {
                        Console.WriteLine(ex.Message);
                    }
                    
                }
                catch (JsonException ex)
                {
                    Console.WriteLine("Error formato json json");
                    Console.WriteLine(ex.Message);
                }
            }
            CerrarConexionDeUsuarios();
        }

        /// <summary>
        /// Notifica a todos los clientes conectados del mensaje que se recibió
        /// </summary>
        /// <param name="mensaje">Mensaje recibido</param>
        public static void NotificarClientes(MensajeChat mensaje)
        {
            try
            {
                foreach (DictionaryEntry item in listaClientes)
                {
                    if(!(mensaje.Usuario == (string)item.Key && mensaje.Tipo == TipoMensaje.Conectarse))
                    {
                        List<TcpClient> listaSockets = (List<TcpClient>)item.Value;
                        foreach (TcpClient clienteSocket in listaSockets)
                        {
                            NetworkStream clienteStream = clienteSocket.GetStream();
                            Byte[] bytesMensaje = new byte[tamañoBuffer];
                            String msjTodos = JsonSerializer.Serialize(mensaje);
                            bytesMensaje = Encoding.ASCII.GetBytes(msjTodos);
                            clienteStream.Write(bytesMensaje, 0, bytesMensaje.Length);
                            clienteStream.Flush();
                        }
                    }
                }
                
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error NotificarCliente");
            }
        }

        /// <summary>
        /// Cuando se apaga el socket las conexiones de los usuario conectados son cerradas
        /// </summary>
        public static void CerrarConexionDeUsuarios()
        {
            foreach (DictionaryEntry item in listaClientes)
            {
                List<TcpClient> listaSockets = (List<TcpClient>)item.Value;
                foreach (TcpClient soxketCliente in listaSockets)
                {
                    soxketCliente.Close(); 
                }
                listaSockets.Clear();
            }
            listaClientes.Clear();
        }
    }

    /// <summary>
    /// Clase a la que se le delega el control del usuario conectado
    /// </summary>
    public class ClienteRemoto
    {
        TcpClient clienteSocket;
        String nombreCliente;
        int tamañoBuffer = 65537;
        bool conectado = false;

        /// <summary>
        /// Inicia el control del usuario que se conecto a la sala de chat
        /// </summary>
        /// <param name="clientSocket">Socket del cliente que se conectó</param>
        /// <param name="nombreCliente">nombre del cliente que se conectó</param>
        public void InicializarCliente(TcpClient clientSocket, string nombreCliente)
        {
            this.clienteSocket = clientSocket;
            this.nombreCliente = nombreCliente;
            conectado = true;
            Thread hiloEscucharChat = new Thread(EscucharChat);
            hiloEscucharChat.Start();
        }

        /// <summary>
        /// Se encarga de recibir los mensaje que envie el cliente conectado y eviarselos a los demas clientes
        /// </summary>
        private void EscucharChat()
        {
            EnviarListaUsuarioConectados();
            string datoFromCliente = "";
            while (conectado && SocketChat.Encendido)
            {
                try
                {
                    byte[] byteFrom = new byte[tamañoBuffer];
                    NetworkStream networkStream = clienteSocket.GetStream();
                    int numBytes = networkStream.Read(byteFrom, 0, byteFrom.Length);
                    if(numBytes > 0)
                    {
                        Array.Resize(ref byteFrom, numBytes);
                        datoFromCliente = Encoding.ASCII.GetString(byteFrom);
                        MensajeChat mensajeRecibido = JsonSerializer.Deserialize<MensajeChat>(datoFromCliente);
                        bool notificar = true;

                        //El cliente se desconecta de la sala de chat
                        if (mensajeRecibido.Tipo == TipoMensaje.Desconectarse)
                        {
                            ((List<TcpClient>)SocketChat.listaClientes[nombreCliente]).Remove(clienteSocket);
                            notificar = false; //No se notificara si cerro una sesion y tienes más abiertas
                            if(((List<TcpClient>)SocketChat.listaClientes[nombreCliente]).Count == 0)
                            {
                                SocketChat.listaClientes.Remove(nombreCliente);
                                notificar = true;//Se notificara que cerró la última sesión
                            }
                            conectado = false;
                        }
                        if (notificar)
                        {
                            SocketChat.NotificarClientes(mensajeRecibido);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        /// <summary>
        /// Se le envia al usuario que se conecta la lista de usuarios conectados a la sala de chat
        /// </summary>
        private void EnviarListaUsuarioConectados()
        {
            List<string> listaUsuarios = SocketChat.listaClientes.Keys.OfType<string>().ToList(); ;
            string usuarios = string.Join(";", listaUsuarios);
            MensajeChat mensajeListaUsuario = new MensajeChat();
            mensajeListaUsuario.Usuario = nombreCliente;
            mensajeListaUsuario.Tipo = TipoMensaje.ListaUsuarios;
            mensajeListaUsuario.Contenido = usuarios;
            string msjEnviar = JsonSerializer.Serialize(mensajeListaUsuario);
            Byte[] bytesEnviados = Encoding.ASCII.GetBytes(msjEnviar);
            NetworkStream broadcastStream = clienteSocket.GetStream();
            broadcastStream.Write(bytesEnviados, 0, bytesEnviados.Length);
            broadcastStream.Flush();
        }
    }
}
