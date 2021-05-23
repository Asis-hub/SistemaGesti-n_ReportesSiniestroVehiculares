using DelegacionMunicipal.conexion;
using DelegacionMunicipal.modelo.poco;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;


namespace DelegacionMunicipal.modelo.dao
{
    class DelegacionDAO
    {
        public static List<Delegacion> GetDelegacionesLogin()
        {
            List<Delegacion> listaDelegaciones = new List<Delegacion>();
            SocketLogin socket;
            socket = new SocketLogin();

            string mensaje = "";
            Paquete paquete = new Paquete();

            String consulta = "SELECT a.idDelegacion, c.idMunicipio, c.nombre as municipio, a.nombre, a.correo, a.codigoPostal, a.calle, a.colonia, a.numero, b.idTipoDelegacion, b.tipoDelegacion " +
                "FROM dbo.delegacion a INNER JOIN dbo.tipoDelegacion b ON a.tipo = b.idTipoDelegacion INNER JOIN dbo.municipio c ON a.idMunicipio = c.idMunicipio";
            paquete.Consulta = consulta;
            paquete.TipoQuery = TipoConsulta.Select;
            paquete.TipoDominio = TipoDato.Delegacion;

            mensaje = JsonSerializer.Serialize(paquete);

            socket.IniciarConexion();
            socket.EnviarMensaje(mensaje);
            string respuesta = socket.RecibirMensaje();
            socket.TerminarConexion();

            if (respuesta.Length > 0)
            {
                listaDelegaciones = (List<Delegacion>)JsonSerializer.Deserialize(respuesta, typeof(List<Delegacion>)); ;
            }
            return listaDelegaciones;
        }

        public static List<Delegacion> ConsultarDelegaciones()
        {
            List<Delegacion> listaDelegaciones = new List<Delegacion>();
            string mensaje = "";
            SocketBD socket = new SocketBD();
            Paquete paquete = new Paquete();
            paquete.Consulta = "SELECT a.idDelegacion, c.idMunicipio, c.nombre as municipio, a.nombre, a.correo, a.codigoPostal, a.calle, a.colonia, a.numero, b.idTipoDelegacion, b.tipoDelegacion " +
                "FROM dbo.delegacion a INNER JOIN dbo.tipoDelegacion b ON a.tipo = b.idTipoDelegacion INNER JOIN dbo.municipio c ON a.idMunicipio = c.idMunicipio";
            paquete.TipoQuery = TipoConsulta.Select;
            paquete.TipoDominio = TipoDato.Delegacion;

            mensaje = JsonSerializer.Serialize(paquete);

            Console.WriteLine("Mensaje DAO: " + mensaje);

            socket.IniciarConexion();
            socket.EnviarMensaje(mensaje);
            string respuesta = socket.RecibirMensaje();
            socket.TerminarConexion();

            if (respuesta.Length > 0)
            {
                listaDelegaciones = (List<Delegacion>)JsonSerializer.Deserialize(respuesta, typeof(List<Delegacion>)); ;
            }
            return listaDelegaciones;
        }
    }
}
