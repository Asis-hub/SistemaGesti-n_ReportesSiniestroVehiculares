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
    class DelegacionDAO
    {
        public static List<Delegacion> GetDelegacionesLogin()
        {
            List<Delegacion> listaDelegaciones = new List<Delegacion>();
            SocketLogin socket;
            socket = new SocketLogin();
            
            string mensaje = "";
            Paquete paquete = new Paquete();

            String consulta = "SELECT a.idDelegacion, b.idMunicipio, a.nombre, a.correo, a.codigoPostal, a.calle, a.colonia, " +
                "a.numero, a.tipo FROM dbo.delegacion a INNER JOIN dbo.municipio b ON a.idMunicipio = b.idMunicipio";
            paquete.Consulta = consulta;
            paquete.TipoQuery = TipoConsulta.Select;
            paquete.TipoDominio = TipoDato.Delegacion;

            mensaje = JsonSerializer.Serialize(paquete);
            
            socket.IniciarConexion();
            socket.EnviarMensaje(mensaje);
            string respuesta = socket.RecibirMensaje();
            socket.TerminarConexion();

            if(respuesta.Length > 0)
            {
                listaDelegaciones = (List<Delegacion>)JsonSerializer.Deserialize(respuesta, typeof(List<Delegacion>)); ;
            }
            return listaDelegaciones;
        }
    }
}
