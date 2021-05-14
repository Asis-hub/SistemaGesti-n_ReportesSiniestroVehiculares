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
    class MunicipioDAO
    {
        //Los parametros de los métodos pueden cambiarse
        public static bool Registrar(Municipio nuevoMunicipio)
        {

            return true;
        }

        public static bool Actualizar(Municipio municipio)
        {
            return true;
        }

        public static bool Eliminar(Municipio municipio)
        {
            return true;
        }

        public static ObservableCollection<Municipio> BuscarMunicipios(SocketBD socketServidor)
        {
            ObservableCollection<Municipio> listaMunicipios = null;
            /*
            string mensaje = "";
            Paquete paquete = new Paquete();

            String consulta = "SELECT x.idMunicipio, x.nombre, x.idDelegacion FROM dbo.Municipio x, dbo.Delegacion y WHERE " +
                "x.idDelegacion = y.idDelegacion";

            paquete.Consulta = consulta;
            paquete.TipoQuery = TipoConsulta.Select;
            paquete.TipoDominio = TipoDato.Municipio;

            mensaje = JsonSerializer.Serialize(paquete);

            socketServidor.EnviarMensaje(mensaje);
            string respuesta = socketServidor.RecibirMensaje();

            if (respuesta.Length > 0)
            {
                listaMunicipios = (ObservableCollection<Municipio>)JsonSerializer.Deserialize(respuesta, typeof(ObservableCollection<Municipio>)); ;
            }
            */
            return listaMunicipios;
        }
    }
}
