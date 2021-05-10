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
    class DictamenDAO
    {
        //Los parametros de los métodos pueden cambiarse
        public static bool Registrar(Dictamen nuevoDictamen)
        {

            return true;
        }

        public static bool Actualizar(Dictamen dictamen)
        {
            return true;
        }

        public static bool Eliminar(Dictamen dictamen)
        {
            return true;
        }

        public static ObservableCollection<Dictamen> BuscarDictamenes(SocketBD socketServidor)
        {
            ObservableCollection<Dictamen> listaDictamenes = null;
            string mensaje = "";
            Paquete paquete = new Paquete();

            String consulta = "SELECT x.Folio, x.Descripcion, x.FechaHora, x.IdReporte, x.Username FROM dbo.Dictamen x, " +
                "dbo.ReporteSiniestro y, dbo.Usuario z WHERE x.IdReporte = y.IdReporte AND x.Username = z.Username";

            paquete.Consulta = consulta;
            paquete.TipoQuery = TipoConsulta.Select;
            paquete.TipoDominio = TipoDato.Dictamen;

            mensaje = JsonSerializer.Serialize(paquete);

            socketServidor.EnviarMensaje(mensaje);
            string respuesta = socketServidor.RecibirMensaje();

            if (respuesta.Length > 0)
            {
                listaDictamenes = (ObservableCollection<Dictamen>)JsonSerializer.Deserialize(respuesta, typeof(ObservableCollection<Dictamen>)); ;
            }
            return listaDictamenes;
        }
    }
}
