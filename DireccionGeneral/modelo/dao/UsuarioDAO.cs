using DireccionGeneral.conexion;
using DireccionGeneral.modelo.poco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DireccionGeneral.modelo.dao
{
    public class UsuarioDAO
    {
        public static Usuario getInicioSesion(string username, string password)
        {
            SocketLogin socket = new SocketLogin();
            socket.IniciarConexion();
            Usuario usuario = null;
            Paquete paquete = new Paquete();
            paquete.TipoQuery = TipoConsulta.Select;
            paquete.TipoDominio = TipoDato.Usuario;
            paquete.Consulta = String.Format("SELECT a.username, a.nombreCompleto, a.idDelegacion , b.idCargo, b.tipoCargo AS cargo " +
                                             "FROM dbo.usuario a INNER JOIN dbo.cargo b ON a.idCargo = b.idCargo  " +
                                             "WHERE a.username = '{0}' AND a.password = '{1}'",
                                             username, password);

            string mensaje = JsonSerializer.Serialize(paquete);

            socket.EnviarMensaje(mensaje);
            string respuesta = socket.RecibirMensaje();
            socket.TerminarConexion();

            if (respuesta.Length > 0)
            {
                usuario = JsonSerializer.Deserialize<Usuario>(respuesta);
            }

            return usuario;
        }

        public static List<Usuario> ConsultarUsuarios()
        {
            List<Usuario> listaUsuarios = new List<Usuario>();
            SocketBD socket = new SocketBD();
            
            Paquete paquete = new Paquete();
            paquete.TipoQuery = TipoConsulta.Select;
            paquete.TipoDominio = TipoDato.Usuario;
            paquete.Consulta = "SELECT a.username, a.nombreCompleto, a.password, a.idDelegacion, b.idCargo, b.tipoCargo FROM dbo.usuario a INNER JOIN dbo.cargo b ON a.idCargo = b.idCargo";

            string mensaje = JsonSerializer.Serialize(paquete);

            socket.IniciarConexion();
            socket.EnviarMensaje(mensaje);
            string respuesta = socket.RecibirMensaje();
            socket.TerminarConexion();

            if (respuesta.Length > 0)
            {
                listaUsuarios = (List<Usuario>)JsonSerializer.Deserialize(respuesta, typeof(List<Usuario>)); ;
            }

            return listaUsuarios;
        }

        public static int RegistrarUsuario(Usuario nuevoUsuario)
        {
            int resultado = 0;
            SocketBD socket = new SocketBD();
            Paquete paquete = new Paquete();
            paquete.TipoQuery = TipoConsulta.Insert;
            paquete.TipoDominio = TipoDato.Usuario;
            paquete.Consulta = String.Format("INSERT usuario (username, nombreCompleto, password, idDelegacion, idCargo) " +
                                             "VALUES ('{0}', '{1}', '{2}', {3}, {4})",
                                             nuevoUsuario.Username, nuevoUsuario.NombreCompleto, nuevoUsuario.Password, 
                                             nuevoUsuario.IdDelegacion, nuevoUsuario.IdCargo);
            
            string mensaje = JsonSerializer.Serialize(paquete);

            socket.IniciarConexion();
            socket.EnviarMensaje(mensaje);
            string respuesta = socket.RecibirMensaje();
            socket.TerminarConexion();
            
            resultado = int.Parse(respuesta);
            return resultado;
        }
        
        public static int EditarUsuario(Usuario usuario)
        {
            
            return 0;
        }
        
        public static int EliminarUsuario(string username)
        {
            int resultado = 0;
            SocketBD socket = new SocketBD();
            Paquete paquete = new Paquete();
            paquete.Consulta = String.Format("DELETE FROM dbo.usuario WHERE username = '{0}'", username);
            paquete.TipoDominio = TipoDato.Usuario;
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