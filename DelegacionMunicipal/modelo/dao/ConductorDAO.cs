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
    public class ConductorDAO
    {
        //Los parametros de los métodos pueden cambiarse
        public static bool Registrar(Conductor nuevoConductor)
        {

            return true;
        }

        public static bool Actualizar(Conductor conductor)
        {
            return true;
        }

        public static bool Eliminar(Conductor conductor)
        {
            return true;
        }

        public static ObservableCollection<Conductor> BuscarConductores(SocketLogin socketServidor)
        {
            
            ObservableCollection<Conductor> listaConductores = null;
            
            /*string mensaje = "";
            Paquete paquete = new Paquete();

            String consulta = "SELECT x.numLicenciaConducir, x.telCelular, x.nombreCompleto, x.fechaNacimiento FROM dbo.conductor x";

            paquete.Consulta = consulta;
            paquete.TipoQuery = TipoConsulta.Select;
            paquete.TipoDominio = TipoDato.Delegacion;

            mensaje = JsonSerializer.Serialize(paquete);

            socketServidor.EnviarMensaje(mensaje);
            string respuesta = socketServidor.RecibirMensaje();

            if (respuesta.Length > 0)
            {
                listaConductores = (ObservableCollection<Conductor>)JsonSerializer.Deserialize(respuesta, typeof(ObservableCollection<Delegacion>)); ;
            }
            */
             return listaConductores;
        }
    }
}
