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
        public static ObservableCollection<Delegacion> ConsultarDelegaciones(SocketLogin socketServidor)
        {
            ObservableCollection<Delegacion> listaDelegaciones = null;
            string mensaje = "";
            Paquete paquete = new Paquete();

            String consulta = "SELECT x.idDelegacion, x.idMunicipio, x.nombre, x.correo, x.codigoPostal, x.colonia, x.calle, x.numero, x.tipo FROM dbo.Delegacion x";

            paquete.Consulta = consulta;
            paquete.TipoQuery = TipoConsulta.Select;
            paquete.TipoDominio = TipoDato.Delegacion;

            mensaje = JsonSerializer.Serialize(paquete);

            socketServidor.EnviarMensaje(mensaje);
            string respuesta = socketServidor.RecibirMensaje();

            if(respuesta.Length > 0)
            {
                listaDelegaciones = (ObservableCollection<Delegacion>)JsonSerializer.Deserialize(respuesta, typeof(ObservableCollection<Delegacion>)); ;
            }
            return listaDelegaciones;
        }
    }
}
