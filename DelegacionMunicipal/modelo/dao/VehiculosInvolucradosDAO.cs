using DelegacionMunicipal.conexion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DelegacionMunicipal.modelo.dao
{
    class VehiculosInvolucradosDAO
    {

        public static int InsertarVehiculos(string numeroPlaca, int idReporte)
        {
            int resultado = 0;
            SocketBD socket = new SocketBD();
            Paquete paquete = new Paquete();

            paquete.TipoQuery = TipoConsulta.Insert;
            paquete.TipoDominio = TipoDato.VehiculosInvolucrados;

            paquete.Consulta = String.Format("insert into vehiculosInvolucrados values ('uvj4064', " + idReporte.ToString() + ");");

            string mensaje = JsonSerializer.Serialize(paquete);
            socket.IniciarConexion();
            socket.EnviarMensaje(mensaje);
            string respuesta = socket.RecibirMensaje();
            socket.TerminarConexion();

            if (respuesta.Length>0)
            {
                resultado = int.Parse(respuesta);
            }


            return resultado;
        }
    }
}
