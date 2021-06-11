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
    class ReporteSiniestroDAO
    {
        public static List<ReporteSiniestro> ConsultarReportes()
        {
            List<ReporteSiniestro> listaReportes = new List<ReporteSiniestro>();
            SocketBD socket = new SocketBD();

            string mensaje = "";
            Paquete paquete = new Paquete();

            paquete.Consulta = "SELECT idReporte, calle, numero, colonia, fechaHora, idDelegacion, username, dictamen from dbo.reporteSiniestro;";
            
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

            paquete.Consulta = "SELECT idReporte, calle, numero, colonia, fechaHora, idDelegacion, username, dictamen from dbo.reporteSiniestro where idReporte =" + idReporte.ToString() + ";";
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

            //paquete.Consulta = String.Format("insert into reporteSiniestro values " +
            //    "('{0}', '{1}', '{2}','{3}', {4}, '{5}', {6}) " +
            //  "SELECT SCOPE_IDENTITY();", reporteSiniestro.Calle, reporteSiniestro.Colonia, reporteSiniestro.Numero, "20120618 10:34:09 AM", reporteSiniestro.IdDelegacion, reporteSiniestro.Username, Convert.ToByte(reporteSiniestro.Dictamen));
            paquete.Consulta = "insert into reporteSiniestro values ('" + reporteSiniestro.Calle + "', '"+ reporteSiniestro.Numero +"', '"+ reporteSiniestro.Colonia + "','"+ reporteSiniestro.FechaHora.ToString("yyyyMMdd hh:mm:ss") + "',"+ reporteSiniestro.IdDelegacion + ", '" + reporteSiniestro.Username + "', '" + reporteSiniestro.Dictamen+"') Select SCOPE_IDENTITY() as identificador;";

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
