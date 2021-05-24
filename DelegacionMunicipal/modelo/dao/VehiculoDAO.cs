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
        public static List<Vehiculo> ConsultarVehiculos()
        {
            List<Vehiculo> listaVehiculos = new List<Vehiculo>();
            SocketBD socket = new SocketBD();
            string mensaje = "";
            Paquete paquete = new Paquete();
            paquete.Consulta = "SELECT numeroPlaca AS numPlaca, marca, modelo, color, numeroPolizaSeguro, " +
                "nombreAseguradora, ano, numeroLicenciaConducir FROM dbo.vehiculo";
            paquete.TipoDominio = TipoDato.Vehiculo;
            paquete.TipoQuery = TipoConsulta.Select;

            mensaje = JsonSerializer.Serialize(paquete);

            socket.IniciarConexion();
            socket.EnviarMensaje(mensaje);
            string respuesta = socket.RecibirMensaje();
            socket.TerminarConexion();

            if (respuesta.Length > 0)
            {
                listaVehiculos = (List<Vehiculo>)JsonSerializer.Deserialize(respuesta, typeof(List<Vehiculo>)); ;
            }

            return listaVehiculos;
        }

        //Los parametros de los métodos pueden cambiarse
        public static int RegistrarVehiculo(Vehiculo nuevoVehiculo)
        {
            int resultado = 0;
            SocketBD socket = new SocketBD();
            Paquete paquete = new Paquete();
            paquete.TipoQuery = TipoConsulta.Insert;
            paquete.TipoDominio = TipoDato.Vehiculo;
            paquete.Consulta = String.Format("INSERT vehiculo (numeroPlaca, marca, modelo, color, numeroPolizaSeguro, nombreAseguradora, ano, numeroLicenciaConducir) " +
                                             "VALUES ('{0}', '{1}', '{2}', '{3}',  '{4}', '{5}', '{6}', {7})",
                                             nuevoVehiculo.NumPlaca, nuevoVehiculo.Marca, nuevoVehiculo.Modelo, nuevoVehiculo.Color,
                                             nuevoVehiculo.NumPolizaSeguro, nuevoVehiculo.NombreAseguradora, nuevoVehiculo.Año,
                                             nuevoVehiculo.NumLicenciaConducir);

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

        public static int EditarVehiculo(Vehiculo vehiculo)
        {
            int resultado = 0;
            SocketBD socket = new SocketBD();
            Paquete paquete = new Paquete();
            paquete.TipoQuery = TipoConsulta.Update;
            paquete.TipoDominio = TipoDato.Vehiculo;
            paquete.Consulta = String.Format("UPDATE dbo.vehiculo SET numeroPlaca='{0}', marca='{1}', color='{2}', numeroPolizaSeguro='{3}', nombreAseguradora='{4}', ano='{5}', numeroLicenciaConducir='{6}' WHERE numeroPlaca={7}",
                                             vehiculo.NumPlaca, vehiculo.Marca, vehiculo.Color, vehiculo.NumPolizaSeguro, vehiculo.Año, vehiculo.NumLicenciaConducir, vehiculo.NumPlaca);

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

        public static int EliminarVehiculo(string numeroPlaca)
        {
            int resultado = 0;
            SocketBD socket = new SocketBD();
            Paquete paquete = new Paquete();
            paquete.TipoQuery = TipoConsulta.Delete;
            paquete.TipoDominio = TipoDato.Vehiculo;
            paquete.Consulta = String.Format("DELETE FROM dbo.vehiculo WHERE numeroPlaca={0}", numeroPlaca);

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
    }
}
