﻿using DelegacionMunicipal.conexion;
using DelegacionMunicipal.modelo.dao.ftp;
using DelegacionMunicipal.modelo.poco;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DelegacionMunicipal.modelo.dao
{
    class FotografiaDAO
    {

        public static List<Fotografia> ObtenerFotografias(int idReporte)
        {
            List<Fotografia> fotografias = new List<Fotografia>();
            SocketBD socket = new SocketBD();
            
            string mensaje = "";
            Paquete paquete = new Paquete();

            paquete.Consulta = "select * from fotografia where idReporte = " + idReporte.ToString() + ";";

            paquete.TipoDominio = TipoDato.Fotografia;
            paquete.TipoQuery = TipoConsulta.Select;

            mensaje = JsonSerializer.Serialize(paquete);

            socket.IniciarConexion();
            socket.EnviarMensaje(mensaje);
            string respuesta = socket.RecibirMensaje();
            socket.TerminarConexion();

            if (respuesta.Length > 0)
            {
                fotografias = (List<Fotografia>)JsonSerializer.Deserialize(respuesta, typeof(List<Fotografia>));

            }



            return fotografias;

        }

        public static int InsertarFotografias(List<string> listaImagenes ,int idReporte)
        {
            int respuesta = 0;
            foreach (string rutaImagen in listaImagenes)
            {
                respuesta = 0;
                SocketBD socket = new SocketBD();
                string mensaje = "";
                Paquete paquete = new Paquete();

                paquete.Consulta = "insert into fotografia values ('" + idReporte.ToString() + "') SELECT SCOPE_IDENTITY();";

                paquete.TipoQuery = TipoConsulta.Insert;
                paquete.TipoDominio = TipoDato.Fotografia;


                mensaje = JsonSerializer.Serialize(paquete);

                socket.IniciarConexion();
                socket.EnviarMensaje(mensaje);
                mensaje = socket.RecibirMensaje();
                socket.TerminarConexion();

                if (mensaje.Length > 0)
                {
                    respuesta = int.Parse(mensaje);

                }
                if(respuesta > 0)
                {
                    SubirImagenFTP(rutaImagen, respuesta);
                }

            }
            return respuesta;
        }

        private static bool SubirImagenFTP(string origen, int nombre)
        {
            WebClient clienteWeb = ConexionFTP.GetConnection();
            try
            {
                if (clienteWeb != null)
                {
                    string destino = ConexionFTP.SERVER + nombre + ".jpg";

                    clienteWeb.UploadFile(destino, origen);
                }
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}
