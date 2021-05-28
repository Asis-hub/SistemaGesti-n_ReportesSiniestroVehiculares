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
                    SqlCommand comando;
                    SqlDataReader dataReader;

                    comando = new SqlCommand(paquete.Consulta, conexionBD);
                    dataReader = comando.ExecuteReader();

                    //Lista de Delegaciones
                    if (paquete.TipoDominio == TipoDato.Delegacion)
                    {
                        List<Delegacion> listaDelegaciones = new List<Delegacion>();
                        while (dataReader.Read())
                        {
                            Delegacion delegacion = new Delegacion();
                            delegacion.IdDelegacion = (!dataReader.IsDBNull(0)) ? dataReader.GetInt32(0) : 0;
                            delegacion.IdMunicipio = (!dataReader.IsDBNull(1)) ? dataReader.GetInt32(1) : 0;
                            delegacion.Municipio = (!dataReader.IsDBNull(2)) ? dataReader.GetString(2) : "";
                            delegacion.Nombre = (!dataReader.IsDBNull(3)) ? dataReader.GetString(3) : "";
                            delegacion.Correo = (!dataReader.IsDBNull(4)) ? dataReader.GetString(4) : "";
                            delegacion.CodigoPostal = (!dataReader.IsDBNull(5)) ? dataReader.GetString(5) : "";
                            delegacion.Colonia = (!dataReader.IsDBNull(6)) ? dataReader.GetString(6) : "";
                            delegacion.Calle = (!dataReader.IsDBNull(7)) ? dataReader.GetString(7) : "";
                            delegacion.Numero = (!dataReader.IsDBNull(8)) ? dataReader.GetString(8) : "";
                            delegacion.IdTipo = (!dataReader.IsDBNull(9)) ? dataReader.GetInt32(9) : 0;
                            delegacion.Tipo = (!dataReader.IsDBNull(10)) ? dataReader.GetString(10) : "";
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
                            usuario.IdCargo = (!dataReader.IsDBNull(4)) ? dataReader.GetInt32(4) : 0;
                            usuario.Cargo = (!dataReader.IsDBNull(5)) ? dataReader.GetString(5) : "";

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

                    //Lista de Conductores
                    if (paquete.TipoDominio == TipoDato.Conductor)
                    {
                        List<Conductor> listaConductores = new List<Conductor>();
                        while (dataReader.Read())
                        {
                            Conductor conductor = new Conductor();
                            conductor.NumeroLicencia = (!dataReader.IsDBNull(0)) ? dataReader.GetString(0) : "";
                            conductor.Celular = (!dataReader.IsDBNull(1)) ? dataReader.GetString(1) : "";
                            conductor.NombreCompleto = (!dataReader.IsDBNull(2)) ? dataReader.GetString(2) : "";
                            conductor.FechaNacimiento = (!dataReader.IsDBNull(3)) ? dataReader.GetDateTime(3) : System.DateTime.MinValue;
                            listaConductores.Add(conductor);
                        }
                        respuesta = JsonSerializer.Serialize(listaConductores);
                    }

                    //Lista de Vehiculos
                    if (paquete.TipoDominio == TipoDato.Vehiculo)
                    {
                        List<Vehiculo> listaVehiculos = new List<Vehiculo>();
                        while (dataReader.Read())
                        {
                            Vehiculo vehiculo = new Vehiculo();
                            vehiculo.NumPlaca = (!dataReader.IsDBNull(0)) ? dataReader.GetString(0) : "";
                            vehiculo.Marca = (!dataReader.IsDBNull(1)) ? dataReader.GetString(1) : "";
                            vehiculo.Modelo = (!dataReader.IsDBNull(2)) ? dataReader.GetString(2) : "";
                            vehiculo.Color = (!dataReader.IsDBNull(3)) ? dataReader.GetString(3) : "";
                            vehiculo.NumPolizaSeguro = (!dataReader.IsDBNull(4)) ? dataReader.GetString(4) : "";
                            vehiculo.NombreAseguradora = (!dataReader.IsDBNull(5)) ? dataReader.GetString(5) : "";
                            vehiculo.Año = (!dataReader.IsDBNull(6)) ? dataReader.GetString(6) : "";
                            vehiculo.NumLicenciaConducir = (!dataReader.IsDBNull(7)) ? dataReader.GetString(7) : "";
                            listaVehiculos.Add(vehiculo);
                        }
                        respuesta = JsonSerializer.Serialize(listaVehiculos);
                    }

                    //Lista Cargos
                    if(paquete.TipoDominio == TipoDato.Cargo)
                    {
                        List<Cargo> listaCargos = new List<Cargo>();
                        while (dataReader.Read())
                        {
                            Cargo cargo = new Cargo();
                            cargo.IdCargo = (!dataReader.IsDBNull(0)) ? dataReader.GetInt32(0) : 0;
                            cargo.TipoCargo = (!dataReader.IsDBNull(1)) ? dataReader.GetString(1) : "";
                            listaCargos.Add(cargo);
                        }

                        respuesta = JsonSerializer.Serialize(listaCargos);
                    }

                    //Dictamen de Reporte (Dirección General)
                    if (paquete.TipoDominio == TipoDato.Dictamen)
                    {
                        Dictamen dictamen = new Dictamen();
                        while (dataReader.Read())
                        {
                            dictamen.Folio = (!dataReader.IsDBNull(0)) ? dataReader.GetInt32(0) : 0;
                            dictamen.Descripcion = (!dataReader.IsDBNull(1)) ? dataReader.GetString(1) : "";
                            dictamen.FechaHora = (!dataReader.IsDBNull(2)) ? dataReader.GetDateTime(2) : System.DateTime.MinValue;
                            dictamen.IdReporte = (!dataReader.IsDBNull(3)) ? dataReader.GetInt32(3) : 0;
                            dictamen.Username = (!dataReader.IsDBNull(4)) ? dataReader.GetString(4) : "";
                        }

                        respuesta = JsonSerializer.Serialize(dictamen);
                    }

                    //Reporte
                    if (paquete.TipoDominio == TipoDato.ReporteSiniestro)
                    {
                        List<ReporteSiniestro> listaReporteSiniestro = new List<ReporteSiniestro>();
                        while (dataReader.Read())
                        {
                            ReporteSiniestro reporteSiniestro = new ReporteSiniestro();
                            reporteSiniestro.IdReporte = (!dataReader.IsDBNull(0)) ? dataReader.GetInt32(0) : 0;
                            reporteSiniestro.Calle = (!dataReader.IsDBNull(1)) ? dataReader.GetString(1) : "";
                            reporteSiniestro.Numero = (!dataReader.IsDBNull(2)) ? dataReader.GetString(2) : "";
                            reporteSiniestro.Colonia = (!dataReader.IsDBNull(3)) ? dataReader.GetString(3) : "";
                            reporteSiniestro.IdDelegacion = (!dataReader.IsDBNull(4)) ? dataReader.GetInt32(4) : 0;
                            reporteSiniestro.Username = (!dataReader.IsDBNull(5)) ? dataReader.GetString(5) : "";
                            listaReporteSiniestro.Add(reporteSiniestro);
                        }
                        respuesta = JsonSerializer.Serialize(listaReporteSiniestro);
                    }

                    dataReader.Close();
                    comando.Dispose();
                }
                
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

            //Si oucrre un error solo se enviaria <EOF>
            respuesta += "<EOF>";
            byte[] msgRespuesta = Encoding.Default.GetBytes(respuesta);
            clienteRemoto.Send(msgRespuesta, 0, msgRespuesta.Length, 0);
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
                            SqlCommand comando = new SqlCommand(paquete.Consulta, conexionBD);
                            resultado = comando.ExecuteNonQuery();
                            comando.Dispose();
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show(ex.Message, "Error de Modificación en la Base de Datos");
                        resultado = -1;
                    }
                }
                
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

            byte[] msgRespuesta = Encoding.Default.GetBytes(resultado + "<EOF>");
            clienteRemoto.Send(msgRespuesta, 0, msgRespuesta.Length, 0);
        }
    }
}


