using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DelegacionMunicipal.conexion
{
    

    class ConectorFTP
    {
        string servidor = "ftp://maisonbleue2020.ddns.net/";
        WebClient clienteWeb = new WebClient();
        NetworkCredential credenciales = new NetworkCredential("pi", "raspberry");




    }


}
