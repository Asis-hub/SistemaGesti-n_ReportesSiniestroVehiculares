using Servidor.modelo;
using Servidor.modelo.dao;
using Servidor.modelo.dao.db;
using Servidor.modelo.poco;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Servidor.servicios
{
    /// <summary>
    /// Socket encargado de atender las peticiones de administracion de los registro de la base de datos
    /// </summary>
    public class SocketBD
    {
        private Socket socketServer;
        private string ip;
        private int puerto;
        private bool encendido = false;
        private Thread hiloRecibirMensajes;

        public bool Encendido { get => encendido; }

        public SocketBD()
        {
            ip = Properties.Settings.Default.IP;
            puerto = Properties.Settings.Default.PuertoBaseDatos;
            encendido = false;
        }

        /// <summary>
        /// Enciende el socket
        /// </summary>
        public void IniciarConexion()
        {
            socketServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint direccion = new IPEndPoint(IPAddress.Parse(ip), puerto);
            socketServer.Bind(direccion);
            socketServer.Listen(10);
            hiloRecibirMensajes = new Thread(new ThreadStart(RecibirMensaje));
            hiloRecibirMensajes.Start();
            encendido = true;
        }

        /// <summary>
        /// Envia mensaje al socket del cliente
        /// </summary>
        /// <param name="socketCliente">Cliente que realizó la petición</param>
        /// <param name="mensaje">Mensaje que se le enviará al cliente</param>
        private void EnviarMensaje(Socket socketCliente, string mensaje)
        {
            //Si oucrre un error solo se enviaria <EOF>
            mensaje += "<EOF>";
            byte[] msgRespuesta = Encoding.Default.GetBytes(mensaje);
            socketCliente.Send(msgRespuesta, 0, msgRespuesta.Length, 0);
        }

        /// <summary>
        /// Recibe los mensaje del lado del cliente
        /// </summary>
        private void RecibirMensaje()
        {
            while (encendido)
            {
                try
                {
                
                    string mensaje = "";
                    string respuesta = "";
                    Socket socketClienteRemoto = socketServer.Accept();
                    
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

                    respuesta = ProcesarPaquete(paqueteRecibido);
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

        }

        /// <summary>
        /// Apaga el la el socket
        /// </summary>
        public void TerminarConexion()
        {
            if (encendido)
            {
                socketServer.Close();
            }
        }

       
        private string ProcesarPaquete(Paquete paquete)
        {
            string respuesta = "";
            if(paquete.TipoQuery == TipoConsulta.Select)
            {
                respuesta = ProcesarSeleccion(paquete);
            }
            else if(paquete.TipoQuery == TipoConsulta.Insert)
            {
                respuesta = ProcesarInsercion(paquete);
            }
            else if(paquete.TipoQuery == TipoConsulta.Delete)
            {
                respuesta = ProcesarEliminacion(paquete);
            }
            else if(paquete.TipoQuery == TipoConsulta.Update)
            {
                respuesta = ProcesarModificacion( paquete);
            }

            return respuesta;
        }

        private string ProcesarSeleccion(Paquete paquete) 
        {
            string respuesta = "";
            
            if (paquete.TipoDominio == TipoDato.Delegacion)
            {
                List<Delegacion> listaDelegaciones = DelegacionDAO.ConsultarDelegaciones(paquete.Consulta);
                respuesta = JsonSerializer.Serialize(listaDelegaciones);
            }
            else if (paquete.TipoDominio == TipoDato.Usuario)
            {
                List<Usuario> listaUsuarios = UsuarioDAO.ConsultarUsuarios(paquete.Consulta);
                respuesta = JsonSerializer.Serialize(listaUsuarios);
            }
            else if (paquete.TipoDominio == TipoDato.Municipio)
            {
                List<Municipio> listaMunicipios = MunicipioDAO.ConsultarMunicipios(paquete.Consulta);
                respuesta = JsonSerializer.Serialize(listaMunicipios);
            }
            else if (paquete.TipoDominio == TipoDato.DelegacionTipo)
            {
                List<DelegacionTipo> listaTiposDelegacion = DelegacionTipoDAO.ConsultarTipos(paquete.Consulta);
                respuesta = JsonSerializer.Serialize(listaTiposDelegacion);
            }
            else if (paquete.TipoDominio == TipoDato.Conductor)
            {
                List<Conductor> listaConductores = ConductorDAO.ConsultarConductores(paquete.Consulta);
                respuesta = JsonSerializer.Serialize(listaConductores);
            }
            else if (paquete.TipoDominio == TipoDato.Vehiculo)
            {
                List<Vehiculo> listaVehiculos = VehiculoDAO.ConsultarVehiculos(paquete.Consulta);
                respuesta = JsonSerializer.Serialize(listaVehiculos);
            }
            else if(paquete.TipoDominio == TipoDato.Cargo)
            {
                List<Cargo> listaCargos = CargoDAO.ConsultarCargos(paquete.Consulta);
                respuesta = JsonSerializer.Serialize(listaCargos);
            }
            else if (paquete.TipoDominio == TipoDato.Dictamen)
            {
                Dictamen dictamen = DictamenDAO.ConsultarDictamenDeReporte(paquete.Consulta);
                respuesta = JsonSerializer.Serialize(dictamen);
            }
            else if (paquete.TipoDominio == TipoDato.ReportesSiniestro)
            {
                List<ReporteSiniestro> listaReporteSiniestro = ReporteSiniestroDAO.ConsultarReportes(paquete.Consulta);
                respuesta = JsonSerializer.Serialize(listaReporteSiniestro);
            }
            else if (paquete.TipoDominio == TipoDato.ReporteSiniestro)
            {
                ReporteSiniestro reporteSiniestro = ReporteSiniestroDAO.ObtenerReporte(paquete.Consulta);
                respuesta = JsonSerializer.Serialize(reporteSiniestro);
            }
            else if (paquete.TipoDominio == TipoDato.Fotografia)
            {
                List<Fotografia> fotografia = FotografiaDAO.ObtenerFotografias(paquete.Consulta);
                respuesta = JsonSerializer.Serialize(fotografia);

            }

            return respuesta;
        }

        /*
         *  0 - No fue posible ejecutar el query.
         *  1 - El query se ejecutó con exito
         * -1 - Error al ejecutar el query
         */
        private string ProcesarInsercion(Paquete paquete)
        {
            string respuesta = "";
            int resultado = 0;

            if (paquete.TipoDominio == TipoDato.Delegacion)
            {
                resultado = DelegacionDAO.RegistrarDelegacion(paquete.Consulta);
                respuesta = resultado.ToString();
            }
            else if (paquete.TipoDominio == TipoDato.Usuario)
            {
                resultado = UsuarioDAO.RegistrarUsuario(paquete.Consulta);
                respuesta = resultado.ToString();
            }
            else if (paquete.TipoDominio == TipoDato.Municipio)
            {
                //No se ocupa
            }
            else if (paquete.TipoDominio == TipoDato.DelegacionTipo)
            {
                //No se ocupa
            }
            else if (paquete.TipoDominio == TipoDato.Conductor)
            {
                resultado = ConductorDAO.RegistrarConductor(paquete.Consulta);
                respuesta = resultado.ToString();
            }
            else if (paquete.TipoDominio == TipoDato.Vehiculo)
            {
                resultado = VehiculoDAO.RegistrarVehiculo(paquete.Consulta);
                respuesta = resultado.ToString();
            }
            else if (paquete.TipoDominio == TipoDato.Cargo)
            {
                //No se ocupa
            }
            else if (paquete.TipoDominio == TipoDato.Dictamen)
            {
                resultado = DictamenDAO.RegistrarDictamen(paquete.Consulta);
                respuesta = resultado.ToString();
            }
            else if (paquete.TipoDominio == TipoDato.ReporteSiniestro)
            {
                resultado = ReporteSiniestroDAO.RegistrarReporte(paquete.Consulta);
                respuesta = resultado.ToString();

            }
            else if (paquete.TipoDominio == TipoDato.Fotografia)
            {
                resultado = FotografiaDAO.InsertarFotografia(paquete.Consulta);
                respuesta = resultado.ToString();
            }
            else if (paquete.TipoDominio == TipoDato.VehiculosInvolucrados)
            {
                resultado = VehiculosInvolucradosDAO.InsertarVehiculo(paquete.Consulta);
                respuesta = resultado.ToString();
            }

            return respuesta;
        }

        private string ProcesarModificacion(Paquete paquete)
        {
            string respuesta = "";
            int resultado = 0;

            if (paquete.TipoDominio == TipoDato.Delegacion)
            {
                resultado = DelegacionDAO.EditarDelegacion(paquete.Consulta);
                respuesta = resultado.ToString();
            }
            else if (paquete.TipoDominio == TipoDato.Usuario)
            {
                resultado = UsuarioDAO.EditarUsuario(paquete.Consulta);
                respuesta = resultado.ToString();
            }
            else if (paquete.TipoDominio == TipoDato.Conductor)
            {
                resultado = ConductorDAO.EditarConductor(paquete.Consulta);
                respuesta = resultado.ToString();
            }
            else if (paquete.TipoDominio == TipoDato.Vehiculo)
            {
                resultado = VehiculoDAO.EditarVehiculo(paquete.Consulta);
                respuesta = resultado.ToString();
            }
            else if (paquete.TipoDominio == TipoDato.Cargo)
            {
                //No se ocupa
            }
            else if (paquete.TipoDominio == TipoDato.Dictamen)
            {
                //Falta implementar. Preguntar al maestro
            }
            else if (paquete.TipoDominio == TipoDato.ReporteSiniestro)
            {
                //Falta implementar
            }
            else if (paquete.TipoDominio == TipoDato.Fotografia)
            {
                //Falta implementar FotografiaDAO
            }

            return respuesta;
        }

        private string ProcesarEliminacion(Paquete paquete)
        {
            string respuesta = "";
            int resultado = 0;

            if (paquete.TipoDominio == TipoDato.Delegacion)
            {
                resultado = DelegacionDAO.EliminarDelegacion(paquete.Consulta);
                respuesta = resultado.ToString();
            }
            else if (paquete.TipoDominio == TipoDato.Usuario)
            {
                resultado = UsuarioDAO.EliminarUsuario(paquete.Consulta);
                respuesta = resultado.ToString();
            }
            else if (paquete.TipoDominio == TipoDato.Conductor)
            {
                resultado = ConductorDAO.EliminarConductor(paquete.Consulta);
                respuesta = resultado.ToString();
            }
            else if (paquete.TipoDominio == TipoDato.Vehiculo)
            {
                resultado = VehiculoDAO.EliminarVehiculo(paquete.Consulta);
                respuesta = resultado.ToString();
            }
            else if (paquete.TipoDominio == TipoDato.Cargo)
            {
                //No se ocupa
            }
            else if (paquete.TipoDominio == TipoDato.Dictamen)
            {
                //Falta implementar. Preguntar maestro
            }
            else if (paquete.TipoDominio == TipoDato.ReporteSiniestro)
            {
                //Falta implementar ReporteSiniestroDAO
            }
            else if (paquete.TipoDominio == TipoDato.Fotografia)
            {
                resultado = ReporteSiniestroDAO.EliminarReporte(paquete.Consulta);
                respuesta = resultado.ToString();
            }

            return respuesta;
        }
    }
}


