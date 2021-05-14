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
    public class VehiculoDAO
    {

        //Los parametros de los métodos pueden cambiarse
        public static bool Registrar(Vehiculo nuevoVehiculo)
        {

            return true;
        }

        public static bool Actualizar(Vehiculo vehiculo)
        {
            return true;
        }

        public static bool Eliminar(Vehiculo vehiculo)
        {
            return true;
        }

        public static ObservableCollection<Vehiculo> BuscarVehiculos(SocketBD socketServidor)
        {
            ObservableCollection<Vehiculo> listaVehiculos = null;
            /*
            string mensaje = "";
            Paquete paquete = new Paquete();

            String consulta = "SELECT x.numPlaca, x.marca, x.modelo, x.color, x.numPolizaSeguro" +
                ", x.nombreAseguradora, x.ano, x.numLicenciaConducir FROM dbo.vehiculo x" +
                ", dbo.conductor y WHERE x.numLicenciaConducir = y.numLicenciaConducir";

            paquete.Consulta = consulta;
            paquete.TipoQuery = TipoConsulta.Select;
            paquete.TipoDominio = TipoDato.Vehiculo;

            mensaje = JsonSerializer.Serialize(paquete);

            socketServidor.EnviarMensaje(mensaje);
            string respuesta = socketServidor.RecibirMensaje();

            if (respuesta.Length > 0)
            {
                listaVehiculos = (ObservableCollection<Vehiculo>)JsonSerializer.Deserialize(respuesta, typeof(ObservableCollection<Vehiculo>)); ;
            }
            */
            return listaVehiculos;
        }
    }
}
