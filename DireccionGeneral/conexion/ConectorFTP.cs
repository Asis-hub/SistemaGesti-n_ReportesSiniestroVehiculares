using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace DireccionGeneral.conexion
{
    /// <summary>
    /// ConectorFTP es una clase que tiene un método de clase BitmapImage para cargar imagenes utilizando XAML
    /// </summary>
    class ConectorFTP
    {
        public static BitmapImage obtenerImagen(string name)
        {
            string servidor = "ftp://maisonbleue2020.ddns.net/sgsrv/";
            // Se utiliza un cliente web para recibir las imagenes desde un URI (Uniform Resource Identifier)
            WebClient clienteWeb = new WebClient();
            NetworkCredential credenciales = new NetworkCredential("pi", "raspberry");
            clienteWeb.Credentials = credenciales;
            /// Se instancia un flujo de memoria para enviar o recibir las imagenes como un arreglo de byte
            MemoryStream memoria = new MemoryStream(clienteWeb.DownloadData(servidor + name + ".jpg"));

            BitmapImage imagen = new BitmapImage();
            imagen.BeginInit();
            imagen.StreamSource = memoria;
            imagen.EndInit();

            return imagen;


        }
    }
}
