using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace DelegacionMunicipal.conexion
{
    /// <summary>
    /// Clase para conectar con el servidor FTP
    /// Exclusivamente archivos con extension .jpg
    /// Se asigna en un Bitmap
    /// </summary>

    class ConectorFTP
    {
        
        
        //Descarga 
        public static BitmapImage obtenerImagen(string name)
        {
            string servidor = "ftp://maisonbleue2020.ddns.net/sgsrv/";
            // Se utiliza un cliente web para recibir la imagen desde un URI (Uniform resource identifier)
            WebClient clienteWeb = new WebClient();
            NetworkCredential credenciales = new NetworkCredential("pi", "raspberry");
            clienteWeb.Credentials = credenciales;
            // Se instancia un flujo de memoeria  para recibir la imagen como un arreglo de bytes
            MemoryStream memoria = new MemoryStream(clienteWeb.DownloadData(servidor + name + ".jpg"));

            BitmapImage imagen = new BitmapImage();
            imagen.BeginInit();
            imagen.StreamSource = memoria;
            imagen.EndInit();

            return imagen;
            

        }

        public static void insertarFoto(string nombre, string identificador)
        {

            string servidor = "ftp://maisonbleue2020.ddns.net/sgsrv/";
            // Se utiliza un cliente web para enviar la imagen
            WebClient clienteWeb = new WebClient();
            NetworkCredential credenciales = new NetworkCredential("pi", "raspberry");
            clienteWeb.Credentials = credenciales;
            //Se envia con el metodo UploadFile, se proporciona la ruta de procedencia y la de destino en el servidor
            string ruta = nombre;
            string destino = servidor + identificador + ".jpg";
            clienteWeb.UploadFile(destino, ruta);
            

        }


    }


}
