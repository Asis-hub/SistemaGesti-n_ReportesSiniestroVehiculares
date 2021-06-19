using DireccionGeneral.conexion;
using DireccionGeneral.modelo.poco;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;


namespace DireccionGeneral.modelo.dao
{   /// <summary>
    /// DAO para Consultar delegaciones, Registrar Delegación, Editar Delegación, Eliminar Delegación
    /// </summary>
    public class DelegacionDAO
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

        public static int RegistrarDelegacion(Delegacion delegacion)
        {
            int resultado = 0;
            SocketBD socket = new SocketBD();
            Paquete paquete = new Paquete();
            paquete.TipoQuery = TipoConsulta.Insert;
            paquete.TipoDominio = TipoDato.Delegacion;
            paquete.Consulta = String.Format("INSERT INTO dbo.delegacion (nombre, correo, codigoPostal, calle, colonia, numero, tipo, idMunicipio) " +
                                             "VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', {6}, {7})",
                                             delegacion.Nombre, delegacion.Correo, delegacion.CodigoPostal, delegacion.Calle, delegacion.Colonia, 
                                             delegacion.Numero, delegacion.IdTipo, delegacion.IdMunicipio);
            Console.WriteLine(paquete.Consulta);
            string mensaje = JsonSerializer.Serialize(paquete);

            socket.IniciarConexion();
            socket.EnviarMensaje(mensaje);
            string respuesta = socket.RecibirMensaje();
            socket.TerminarConexion();

            if (respuesta.Length > 0)
            {
                resultado = int.Parse(respuesta);
            }
            
            return resultado;
        }

        public static int EditarDelegacion(Delegacion delegacion)
        {
            int resultado = 0;
            SocketBD socket = new SocketBD();
            Paquete paquete = new Paquete();
            paquete.TipoQuery = TipoConsulta.Update;
            paquete.TipoDominio = TipoDato.Delegacion;
            paquete.Consulta = String.Format("UPDATE dbo.delegacion SET nombre='{0}', codigoPostal='{1}', correo='{2}', calle='{3}', " +
                                             "colonia='{4}', numero='{5}', tipo={6}, idMunicipio={7} WHERE idDelegacion={8}",
                                             delegacion.Nombre, delegacion.CodigoPostal, delegacion.Correo, delegacion.Calle,
                                             delegacion.Colonia, delegacion.Numero, delegacion.IdTipo, delegacion.IdMunicipio, delegacion.IdDelegacion);

            string mensaje = JsonSerializer.Serialize(paquete);

            socket.IniciarConexion();
            socket.EnviarMensaje(mensaje);
            string respuesta = socket.RecibirMensaje();
            socket.TerminarConexion();

            if (respuesta.Length > 0)
            {
                resultado = int.Parse(respuesta);
            }

            return resultado;
        }

        public static int EliminarDelegacion(int idDelegacion)
        {
            int resultado = 0;
            SocketBD socket = new SocketBD();
            Paquete paquete = new Paquete();
            paquete.TipoQuery = TipoConsulta.Delete;
            paquete.TipoDominio = TipoDato.Delegacion;
            paquete.Consulta = String.Format("DELETE FROM dbo.delegacion WHERE idDelegacion={0}", idDelegacion);

            string mensaje = JsonSerializer.Serialize(paquete);

            socket.IniciarConexion();
            socket.EnviarMensaje(mensaje);
            string respuesta = socket.RecibirMensaje();
            socket.TerminarConexion();

            if (respuesta.Length > 0)
            {
                resultado = int.Parse(respuesta);
            }

            return resultado;
        }
    }
}
