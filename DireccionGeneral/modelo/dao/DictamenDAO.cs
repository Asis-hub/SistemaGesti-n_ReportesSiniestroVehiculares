using DireccionGeneral.conexion;
using DireccionGeneral.modelo.poco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DireccionGeneral.modelo.dao
{
    public class DictamenDAO
    {
        
        public static Dictamen ConsultarDictamenDeReporte(int idReporte)
        {
            int IdReporte = idReporte;
            Dictamen dictamen = new Dictamen();
            SocketBD socket = new SocketBD();
            string mensaje = "";
            Paquete paquete = new Paquete();
            paquete.Consulta = String.Format("SELECT DISTINCT a.folio, a.descripcion, a.fechaHora, a.idReporte, a.username, b.nombreCompleto FROM dbo.dictamen AS a INNER JOIN dbo.usuario AS b ON a.username = b.username WHERE a.idReporte = {0}", IdReporte);
            paquete.TipoDominio = TipoDato.Dictamen;
            paquete.TipoQuery = TipoConsulta.Select;

            mensaje = JsonSerializer.Serialize(paquete);

            socket.IniciarConexion();
            socket.EnviarMensaje(mensaje);
            string respuesta = socket.RecibirMensaje();
            socket.TerminarConexion();

            if (respuesta.Length > 0)
            {
                dictamen = (Dictamen)JsonSerializer.Deserialize(respuesta, typeof(Dictamen)); ;
            }

            return dictamen;
        }

        public static int RegistrarDictamen(Dictamen nuevoDictamen)
        {
            int resultado = 0;
            Dictamen dictamen = new Dictamen();
            SocketBD socket = new SocketBD();
            string mensaje = "";
            Paquete paquete = new Paquete();
            paquete.Consulta = String.Format("INSERT INTO dbo.dictamen (descripcion, fechaHora, idReporte, username) VALUES ('{0}', '{1}', {2}, '{3}')",
                                             nuevoDictamen.Descripcion, nuevoDictamen.FechaHora.ToString("yyyy-MM-dd HH:mm"), nuevoDictamen.IdReporte, nuevoDictamen.Username);
            paquete.TipoDominio = TipoDato.Dictamen;
            paquete.TipoQuery = TipoConsulta.Insert;
            Console.WriteLine(paquete.Consulta);
            mensaje = JsonSerializer.Serialize(paquete);

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
