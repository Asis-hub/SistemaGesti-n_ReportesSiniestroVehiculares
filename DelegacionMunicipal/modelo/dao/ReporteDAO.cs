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
    class ReporteDAO
    {
        //Los parametros de los métodos pueden cambiarse
        public static bool Registrar(ReporteSiniestro nuevoReporte)
        {

            return true;
        }

        public static bool Actualizar(ReporteSiniestro reporte)
        {
            return true;
        }

        public static bool Eliminar(ReporteSiniestro reporte)
        {
            return true;
        }

        public static ObservableCollection<ReporteSiniestro> BuscarReportes(SocketBD socketServidor)
        {
            ObservableCollection<ReporteSiniestro> listaReportes = null;
            
            /*
            string mensaje = "";
            Paquete paquete = new Paquete();

            String consulta = "SELECT x.idReporte, x.calle, x.numero, x.colonia, x.idDelegacion," +
                " x.username FROM dbo.ReporteSiniestro x, dbo.Delegacion y, dbo.Usuario z WHERE x.idDelegacion = y.idDelegacion AND " +
                "x.username = z.username";

            paquete.Consulta = consulta;
            paquete.TipoQuery = TipoConsulta.Select;
            paquete.TipoDominio = TipoDato.Reporte;

            mensaje = JsonSerializer.Serialize(paquete);

            socketServidor.EnviarMensaje(mensaje);
            string respuesta = socketServidor.RecibirMensaje();

            if (respuesta.Length > 0)
            {
                listaReportes = (ObservableCollection<ReporteSiniestro>)JsonSerializer.Deserialize(respuesta, typeof(ObservableCollection<Reporte>)); ;
            }
            */
            return listaReportes;
        }
    }
}
