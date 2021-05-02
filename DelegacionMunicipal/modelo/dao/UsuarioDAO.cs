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
        public static Usuario getInicioSesion(SocketLogin socketServidor, string username, string password, int idDelegacion)
        {
            socketServidor.IniciarConexion();
            Usuario usuario = null;
            Paquete paquete = new Paquete();
            paquete.TipoQuery = TipoConsulta.Select;
            paquete.TipoDominio = TipoDato.Usuario;
            paquete.Consulta = String.Format("SELECT x.username, x.nombreCompleto, x.idDelegacion, y.tipoCargo " +
                                             "FROM dbo.usuario x, Cargo y WHERE x.idCargo = y.idCargo " +
                                             "AND x.username = '{0}' AND x.password = '{1}' AND x.idDelegacion = {2}",
                                             username, password, idDelegacion);

            Console.WriteLine("Usuario: " + paquete.Consulta);

            string mensaje = JsonSerializer.Serialize(paquete);

            socketServidor.EnviarMensaje(mensaje);
            string respuesta = socketServidor.RecibirMensaje();

            if(respuesta.Length > 0)
            {
                usuario = JsonSerializer.Deserialize<Usuario>(respuesta);
            }

            return usuario;
        }
    }
}
