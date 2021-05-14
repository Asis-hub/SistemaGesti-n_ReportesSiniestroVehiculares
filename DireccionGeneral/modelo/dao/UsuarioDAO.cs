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
            paquete.Consulta = String.Format("SELECT a.username, a.password, a.idDelegacion , b.tipoCargo AS cargo " +
                                             "FROM dbo.usuario a INNER JOIN dbo.cargo b ON a.idCargo = b.idCargo " +
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
    }
}