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
    public class CargoDAO
    {
        public static List<Cargo> ConsultarCargos()
        {
            List<Cargo> listaCargos = new List<Cargo>();
            SocketBD socket = new SocketBD();
            string mensaje = "";
            
            Paquete paquete = new Paquete();
            paquete.TipoQuery = TipoConsulta.Select;
            paquete.TipoDominio = TipoDato.Cargo;
            paquete.Consulta = "SELECT a.idCargo, a.tipoCargo FROM dbo.cargo a";

            mensaje = JsonSerializer.Serialize(paquete);

            socket.IniciarConexion();
            socket.EnviarMensaje(mensaje);
            string respuesta = socket.RecibirMensaje();
            socket.TerminarConexion();

            if (respuesta.Length > 0)
            {
                listaCargos = (List<Cargo>)JsonSerializer.Deserialize(respuesta, typeof(List<Cargo>)); ;
            }
            return listaCargos;
        }
    }
}
