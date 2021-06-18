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
    //DAO para Consultar conductores, Registrar conductor, Editar Conductor, Eliminar Conductor
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

        public static int RegistrarConductor(Conductor conductor)
        {
            int resultado = 0;
            SocketBD socket = new SocketBD();
            Paquete paquete = new Paquete();
            paquete.TipoQuery = TipoConsulta.Insert;
            paquete.TipoDominio = TipoDato.Conductor;
            paquete.Consulta = String.Format("INSERT INTO dbo.conductor (numeroLicenciaConducir, telefonoCelular, nombreCompleto, fechaNacimiento) " +
                                             "VALUES ('{0}', '{1}', '{2}', '{3}')",
                                             conductor.NumeroLicencia, conductor.Celular, conductor.NombreCompleto, conductor.FechaNacimiento.ToString("yyyy-MM-dd"));
            Console.WriteLine(paquete.Consulta);
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

        public static int EditarConductor(string numeroLicencia, Conductor conductor)
        {
            int resultado = 0;
            SocketBD socket = new SocketBD();
            Paquete paquete = new Paquete();
            paquete.TipoQuery = TipoConsulta.Update;
            paquete.TipoDominio = TipoDato.Conductor;
            paquete.Consulta = String.Format("UPDATE dbo.conductor SET numeroLicenciaConducir='{0}', telefonoCelular='{1}', nombreCompleto='{2}', fechaNacimiento='{3}' WHERE numeroLicenciaConducir='{4}'",
                                             conductor.NumeroLicencia, conductor.Celular, conductor.NombreCompleto, conductor.FechaNacimiento.ToString("yyyy-MM-dd"), numeroLicencia);

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

        public static int EliminarConductor(string numeroLicencia)
        {
            int resultado = 0;
            SocketBD socket = new SocketBD();
            Paquete paquete = new Paquete();
            paquete.TipoQuery = TipoConsulta.Delete;
            paquete.TipoDominio = TipoDato.Conductor;
            paquete.Consulta = String.Format("DELETE FROM dbo.conductor WHERE numeroLicenciaConducir='{0}'", numeroLicencia);
            Console.WriteLine(paquete.Consulta);
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
