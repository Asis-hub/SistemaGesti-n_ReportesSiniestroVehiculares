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
        public static List<Conductor> ConsultarConductores()
        {
            List<Conductor> listaConductores = new List<Conductor>();
            string mensaje = "";
            SocketBD socket = new SocketBD();
            Paquete paquete = new Paquete();
            paquete.Consulta = "SELECT a.numeroLicenciaConducir AS numeroLicencia, a.telefonoCelular AS celular, a.nombreCompleto, a.fechaNacimiento FROM dbo.conductor a";
            paquete.TipoQuery = TipoConsulta.Select;
            paquete.TipoDominio = TipoDato.Conductor;

            mensaje = JsonSerializer.Serialize(paquete);

            Console.WriteLine("Mensaje DAO: " + mensaje);

            socket.IniciarConexion();
            socket.EnviarMensaje(mensaje);
            string respuesta = socket.RecibirMensaje();
            socket.TerminarConexion();

            if (respuesta.Length > 0)
            {
                listaConductores = (List<Conductor>)JsonSerializer.Deserialize(respuesta, typeof(List<Conductor>)); ;
            }
            return listaConductores;
        }

        //Los parametros de los métodos pueden cambiarse
        public static int RegistrarConductor(Conductor nuevoConductor)
        {
            int resultado = 0;
            SocketBD socket = new SocketBD();
            Paquete paquete = new Paquete();
            paquete.TipoQuery = TipoConsulta.Insert;
            paquete.TipoDominio = TipoDato.Conductor;
            paquete.Consulta = String.Format("INSERT conductor (numeroLicencia, celular, nombrecompleto, fechaNacimiento) " +
                                             "VALUES ('{0}', '{1}', '{2}', {3})",
                                             nuevoConductor.NumeroLicencia, nuevoConductor.Celular, nuevoConductor.NombreCompleto,
                                             nuevoConductor.FechaNacimiento);

            string mensaje = JsonSerializer.Serialize(paquete);

            socket.IniciarConexion();
            socket.EnviarMensaje(mensaje);
            string respuesta = socket.RecibirMensaje();
            socket.TerminarConexion();

            resultado = int.Parse(respuesta);
            return resultado;
        }

        public static bool Actualizar(Conductor conductor)
        {
            return true;
        }

        public static int EliminarConductor(string numeroLicencia)
        {
            int resultado = 0;
            SocketBD socket = new SocketBD();
            Paquete paquete = new Paquete();
            paquete.Consulta = String.Format("DELETE FROM dbo.conductor WHERE numeroLicenciaConducir = '{0}'", numeroLicencia);
            paquete.TipoDominio = TipoDato.Conductor;
            paquete.TipoQuery = TipoConsulta.Delete;

            string mensaje = JsonSerializer.Serialize(paquete);

            socket.IniciarConexion();
            socket.EnviarMensaje(mensaje);
            string respuesta = socket.RecibirMensaje();
            socket.TerminarConexion();

            resultado = int.Parse(respuesta);
            return resultado;
        }

    }
}
