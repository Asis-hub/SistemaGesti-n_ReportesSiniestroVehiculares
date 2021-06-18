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
    /// <summary>
    /// DAO para obtener, registrar y eliminar reportes de siniestro
    /// </summary>
    
    class ReporteSiniestroDAO
    {
        public static List<ReporteSiniestro> ConsultarReportes()
        {
            List<ReporteSiniestro> listaReportes = new List<ReporteSiniestro>();
            SocketBD socket = new SocketBD();

            string mensaje = "";
            Paquete paquete = new Paquete();

            paquete.Consulta = "SELECT a.idReporte, a.calle, a.numero, a.colonia, a.fechaHora, a.idDelegacion, a.username, a.dictamen, a.fechaRegistro, b.nombre, c.nombreCompleto FROM dbo.reporteSiniestro AS a INNER JOIN delegacion AS b ON a.idDelegacion = b.idDelegacion INNER JOIN usuario AS c ON a.username = c.username;";

            
            paquete.TipoDominio = TipoDato.ReportesSiniestro;
            paquete.TipoQuery = TipoConsulta.Select;

            mensaje = JsonSerializer.Serialize(paquete);

            socket.IniciarConexion();
            socket.EnviarMensaje(mensaje);
            string respuesta = socket.RecibirMensaje();
            socket.TerminarConexion();

            if (respuesta.Length > 0)
            {
                listaReportes = (List<ReporteSiniestro>)JsonSerializer.Deserialize(respuesta, typeof(List<ReporteSiniestro>)); 
                
            }


            return listaReportes;
        }

        public static List<ReporteSiniestro> BuscarReportes(int dictaminado, string idDelegacion, string fecha)
        {
            List<ReporteSiniestro> listaReportes = new List<ReporteSiniestro>();
            SocketBD socket = new SocketBD();

            string mensaje = "";
            Paquete paquete = new Paquete();


            paquete.Consulta = String.Format("SELECT a.idReporte, a.calle, a.numero, a.colonia, a.fechaHora, a.idDelegacion, a.username, a.dictamen, a.fechaRegistro, b.nombre, c.nombreCompleto FROM dbo.reporteSiniestro AS a INNER JOIN delegacion AS b ON a.idDelegacion = b.idDelegacion INNER JOIN usuario AS c ON a.username = c.username WHERE dictamen = {0} AND b.idDelegacion LIKE '{1}' AND fechaRegistro LIKE '{2}'", dictaminado, idDelegacion, fecha);
            Console.WriteLine(paquete.Consulta);
            paquete.TipoDominio = TipoDato.ReportesSiniestro;
            paquete.TipoQuery = TipoConsulta.Select;

            mensaje = JsonSerializer.Serialize(paquete);

            socket.IniciarConexion();
            socket.EnviarMensaje(mensaje);
            string respuesta = socket.RecibirMensaje();
            socket.TerminarConexion();

            if (respuesta.Length > 0)
            {
                listaReportes = (List<ReporteSiniestro>)JsonSerializer.Deserialize(respuesta, typeof(List<ReporteSiniestro>));

            }


            return listaReportes;
        }

        public static ReporteSiniestro ObtenerReporte(int idReporte)
        {
            ReporteSiniestro reporteSiniestro = new ReporteSiniestro();
            SocketBD socket = new SocketBD();

            string mensaje = "";
            Paquete paquete = new Paquete();

            paquete.Consulta = String.Format("SELECT a.idReporte, a.calle, a.numero, a.colonia, a.fechaHora, a.idDelegacion, a.username, a.dictamen, a.fechaRegistro, b.nombre, c.nombreCompleto FROM dbo.reporteSiniestro AS a INNER JOIN delegacion AS b ON a.idDelegacion = b.idDelegacion INNER JOIN usuario AS c ON a.username = c.username WHERE idReporte = {0}", idReporte); 
            paquete.TipoDominio = TipoDato.ReporteSiniestro;
            paquete.TipoQuery = TipoConsulta.Select;

            mensaje = JsonSerializer.Serialize(paquete);

            socket.IniciarConexion();
            socket.EnviarMensaje(mensaje);
            string respuesta = socket.RecibirMensaje();

            if (respuesta != null)
            {
                reporteSiniestro = (ReporteSiniestro)JsonSerializer.Deserialize(respuesta, typeof(ReporteSiniestro));
            }

            return reporteSiniestro;
        }

        public static int RegistrarReporte(ReporteSiniestro reporteSiniestro)
        {
            int resultado = 0;
            SocketBD socket = new SocketBD();
            Paquete paquete = new Paquete();

            paquete.TipoQuery = TipoConsulta.Insert;
            paquete.TipoDominio = TipoDato.ReporteSiniestro;

            
            paquete.Consulta = "insert into reporteSiniestro values ('" + reporteSiniestro.Calle + "', '"+ reporteSiniestro.Numero +"', '"+ reporteSiniestro.Colonia + "','"+ reporteSiniestro.FechaHora.ToString("yyyy-MM-dd HH:mm") + "','" + reporteSiniestro.FechaRegistro.ToString("yyyy-MM-dd HH:mm") + "'," + + reporteSiniestro.IdDelegacion + ", '" + reporteSiniestro.Username + "', '" + reporteSiniestro.Dictamen+"') Select SCOPE_IDENTITY();";

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
        
        public static int EliminarReporte(int idReporte)
        {
            int resultado = 0;

            SocketBD socket = new SocketBD();
            Paquete paquete = new Paquete();

            paquete.TipoQuery = TipoConsulta.Delete;
            paquete.TipoDominio = TipoDato.ReporteSiniestro;

            paquete.Consulta = String.Format("delete from reporteSiniestro where idReporte = " + idReporte.ToString() + ";");

            string mensaje = JsonSerializer.Serialize(paquete);
            socket.IniciarConexion();
            socket.EnviarMensaje(mensaje);
            string respuesta = socket.RecibirMensaje();
            socket.TerminarConexion();

            if (respuesta.Length >0)
            {
                resultado = int.Parse(respuesta);
                
            }


            return resultado;

        }
    }
}
