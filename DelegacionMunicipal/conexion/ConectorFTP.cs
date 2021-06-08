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
    

    class ConectorFTP
    {
        
        

        public static BitmapImage obtenerImagen(string name)
        {
            string servidor = "ftp://maisonbleue2020.ddns.net/";
            WebClient clienteWeb = new WebClient();
            NetworkCredential credenciales = new NetworkCredential("pi", "raspberry");
            clienteWeb.Credentials = credenciales;

            MemoryStream memoria = new MemoryStream(clienteWeb.DownloadData(servidor + name));

            BitmapImage imagen = new BitmapImage();
            imagen.BeginInit();
            imagen.StreamSource = memoria;
            imagen.EndInit();

            return imagen;
            

        }


    }


}
