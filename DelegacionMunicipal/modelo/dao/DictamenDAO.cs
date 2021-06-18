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
    ///  DAO de Dictamen para Consultar dictamen especifico
    /// </summary>

    public class DictamenDAO
    {
        public static Dictamen ConsultarDictamen(int idReporte)
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
    }
}
