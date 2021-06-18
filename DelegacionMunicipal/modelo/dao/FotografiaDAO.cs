using DelegacionMunicipal.conexion;
using DelegacionMunicipal.modelo.dao.ftp;
using DelegacionMunicipal.modelo.poco;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DelegacionMunicipal.modelo.dao
{
    /// <summary>
    /// DAO de Fotografia para obtener e insertar fotografias
    /// </summary>
    

    class FotografiaDAO
    {
        /// <summary>
        /// Obtener lista de fotografias
        /// </summary>
        /// <param name="idReporte">identificador de reporte</param>
        /// <returns>Lista de fotografias para solicitarlas al ftp</returns>
        public static List<Fotografia> ObtenerFotografias(int idReporte)
        {
            List<Fotografia> fotografias = new List<Fotografia>();
            SocketBD socket = new SocketBD();
            
            string mensaje = "";
            Paquete paquete = new Paquete();

            paquete.Consulta = "select * from fotografia where idReporte = " + idReporte.ToString() + ";";

            paquete.TipoDominio = TipoDato.Fotografia;
            paquete.TipoQuery = TipoConsulta.Select;

            mensaje = JsonSerializer.Serialize(paquete);

            socket.IniciarConexion();
            socket.EnviarMensaje(mensaje);
            string respuesta = socket.RecibirMensaje();
            socket.TerminarConexion();

            if (respuesta.Length > 0)
            {
                fotografias = (List<Fotografia>)JsonSerializer.Deserialize(respuesta, typeof(List<Fotografia>));

            }



            return fotografias;

        }

        /// <summary>
        /// Se suben fotos de lista proporcionada
        /// </summary>
        /// <param name="listaImagenes">Se inserta la lista de rutas de las imagenes</param>
        /// <param name="idReporte">El reporte al que pertenecen las imagenes</param>
        /// <returns>Se obtiene confirmacion</returns>
        public static int InsertarFotografias(List<string> listaImagenes ,int idReporte)
        {
            int respuesta = 0;
            foreach (string rutaImagen in listaImagenes)
            {
                respuesta = 0;
                SocketBD socket = new SocketBD();
                string mensaje = "";
                Paquete paquete = new Paquete();

                paquete.Consulta = "insert into fotografia values ('" + idReporte.ToString() + "') SELECT SCOPE_IDENTITY();";

                paquete.TipoQuery = TipoConsulta.Insert;
                paquete.TipoDominio = TipoDato.Fotografia;


                mensaje = JsonSerializer.Serialize(paquete);

                socket.IniciarConexion();
                socket.EnviarMensaje(mensaje);
                mensaje = socket.RecibirMensaje();
                socket.TerminarConexion();

                if (mensaje.Length > 0)
                {
                    respuesta = int.Parse(mensaje);

                }
                if(respuesta > 0)
                {
                    SubirImagenFTP(rutaImagen, respuesta);
                }

            }
            return respuesta;
        }

        /// <summary>
        /// Metodo para subir imagenes individuales
        /// </summary>
        /// <param name="origen">Ruta de imagen a subir</param>
        /// <param name="nombre">Nombre como se llamara en la ruta de destino</param>
        /// <returns>Confirmacion</returns>
        private static bool SubirImagenFTP(string origen, int nombre)
        {
            WebClient clienteWeb = ConexionFTP.GetConnection();
            try
            {
                if (clienteWeb != null)
                {
                    string destino = ConexionFTP.SERVER + nombre + ".jpg";

                    clienteWeb.UploadFile(destino, origen);
                }
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}
