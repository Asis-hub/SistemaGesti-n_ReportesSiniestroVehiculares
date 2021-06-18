using DelegacionMunicipal.conexion;
using DelegacionMunicipal.modelo.poco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DelegacionMunicipal.modelo.dao
{
    /// <summary>
    /// DAO para obtener los tipos de delegaciones
    /// </summary>
    
    public class DelegacionTipoDAO
    {
        public static List<DelegacionTipo> ConsultarTipos()
        {
            List<DelegacionTipo> listaTiposDelegacion = new List<DelegacionTipo>();

            string mensaje = "";
            SocketBD socket = new SocketBD();
            Paquete paquete = new Paquete();
            paquete.Consulta = "SELECT idTipoDelegacion, tipoDelegacion FROM tipoDelegacion ";
            paquete.TipoQuery = TipoConsulta.Select;
            paquete.TipoDominio = TipoDato.DelegacionTipo;

            mensaje = JsonSerializer.Serialize(paquete);

            Console.WriteLine("Mensaje DAO: " + mensaje);

            socket.IniciarConexion();
            socket.EnviarMensaje(mensaje);
            string respuesta = socket.RecibirMensaje();
            socket.TerminarConexion();

            if (respuesta.Length > 0)
            {
                listaTiposDelegacion = (List<DelegacionTipo>)JsonSerializer.Deserialize(respuesta, typeof(List<DelegacionTipo>)); ;
            }

            return listaTiposDelegacion;
        }
    }
}
