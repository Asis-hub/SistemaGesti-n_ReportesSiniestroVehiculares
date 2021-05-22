using DelegacionMunicipal.conexion;
using DelegacionMunicipal.modelo.poco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DelegacionMunicipal.modelo.dao
{
    public class UsuarioDAO
    {
        public static Usuario getInicioSesion(string username, string password, int idDelegacion)
        {
            SocketLogin socket = new SocketLogin();
            
            Usuario usuario = null;
            Paquete paquete = new Paquete();
            paquete.TipoQuery = TipoConsulta.Select;
            paquete.TipoDominio = TipoDato.Usuario;
            paquete.Consulta = String.Format("SELECT a.username, a.nombreCompleto, a.idDelegacion , b.idCargo, b.tipoCargo AS cargo " +
                                             "FROM dbo.usuario a INNER JOIN dbo.cargo b ON a.idCargo = b.idCargo " +
                                             "WHERE a.username = '{0}' AND a.password = '{1}' AND a.idDelegacion = '{2}'",
                                             username, password, idDelegacion);

            string mensaje = JsonSerializer.Serialize(paquete);
            
            socket.IniciarConexion();
            socket.EnviarMensaje(mensaje);
            string respuesta = socket.RecibirMensaje();
            socket.TerminarConexion();

            if(respuesta.Length > 0)
            {
                usuario = JsonSerializer.Deserialize<Usuario>(respuesta);
            }

            return usuario;
        }
    }
}
