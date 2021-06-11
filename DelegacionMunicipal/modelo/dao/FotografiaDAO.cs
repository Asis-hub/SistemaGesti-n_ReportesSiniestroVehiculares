using DelegacionMunicipal.conexion;
using DelegacionMunicipal.modelo.poco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DelegacionMunicipal.modelo.dao
{
    class FotografiaDAO
    {

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

        public static int InsertarFotografia(int idReporte)
        {

            int respuesta = 0;
            SocketBD socket = new SocketBD();
            string mensaje = "";
            Paquete paquete = new Paquete();

            paquete.Consulta = "insert into fotografia values (" + idReporte.ToString() + ") SELECT SCOPE_IDENTITY();";

            paquete.TipoDominio = TipoDato.Fotografia;
            paquete.TipoQuery = TipoConsulta.Insert;

            mensaje = JsonSerializer.Serialize(mensaje);

            socket.IniciarConexion();
            socket.EnviarMensaje(mensaje);
            respuesta = int.Parse(socket.RecibirMensaje());
            socket.TerminarConexion();

            if (respuesta > 0)
            {
                return respuesta;
            }

            return respuesta;
        }


    }
}
