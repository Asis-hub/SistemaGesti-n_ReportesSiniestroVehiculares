﻿using DelegacionMunicipal.conexion;
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
    class ReporteSiniestroDAO
    {
        //Los parametros de los métodos pueden cambiarse
       
        public static List<ReporteSiniestro> ConsultarReportes()
        {
            List<ReporteSiniestro> listaReportes = new List<ReporteSiniestro>();
            SocketBD socket = new SocketBD();

            string mensaje = "";
            Paquete paquete = new Paquete();

            paquete.Consulta = "SELECT idReporte, calle, numero, colonia, idDelegacion, username, dictamen from dbo.reporteSiniestro;";
            
            paquete.TipoDominio = TipoDato.ReporteSiniestro;
            paquete.TipoQuery = TipoConsulta.Select;

            mensaje = JsonSerializer.Serialize(paquete);

            socket.IniciarConexion();
            socket.EnviarMensaje(mensaje);
            string respuesta = socket.RecibirMensaje();
            socket.TerminarConexion();

            if (respuesta.Length > 0)
            {
                listaReportes = (List<ReporteSiniestro>)JsonSerializer.Deserialize(respuesta, typeof(List<ReporteSiniestro>)); 
            }


            return listaReportes;
        }
        
        public static ReporteSiniestro ObtenerReporte()
        {
            ReporteSiniestro reporteSiniestro = null;
            SocketBD socket = new SocketBD();

            string mensaje = "";
            Paquete paquete = new Paquete();
            paquete.Consulta = "Select idReporte, calle, numero, colonia, idDelegacion, username, dictamen";

            paquete.TipoDominio = TipoDato.ReporteSiniestro;


            return reporteSiniestro;

        }
        
    }
}
