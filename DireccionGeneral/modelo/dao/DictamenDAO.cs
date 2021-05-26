﻿using DireccionGeneral.conexion;
using DireccionGeneral.modelo.poco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DireccionGeneral.modelo.dao
{
    class DictamenDAO
    {
        public static Dictamen ConsultarDictamen(int idReporte)
        {
            int IdReporte = idReporte;
            Dictamen dictamen = new Dictamen();
            SocketBD socket = new SocketBD();
            string mensaje = "";
            Paquete paquete = new Paquete();
            paquete.Consulta = String.Format("SELECT folio, descripcion, fechaHora, idReporte, username FROM dbo.dictamen WHERE idReporte = {0}", IdReporte);
            paquete.TipoDominio = TipoDato.Dictamen;
            paquete.TipoQuery = TipoConsulta.Select;

            mensaje = JsonSerializer.Serialize(paquete);

            socket.IniciarConexion();
            socket.EnviarMensaje(mensaje);
            string respuesta = socket.RecibirMensaje();
            socket.TerminarConexion();

            if (respuesta.Length > 0)
            {
                dictamen = (Dictamen)JsonSerializer.Deserialize(respuesta, typeof(Dictamen)); ;
            }

            return dictamen;
        }



    }
}
