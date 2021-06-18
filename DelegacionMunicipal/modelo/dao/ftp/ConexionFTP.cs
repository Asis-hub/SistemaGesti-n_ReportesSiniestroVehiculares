using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DelegacionMunicipal.modelo.dao.ftp
{
    //DAO organizacion FTP
    public class ConexionFTP
    {
        public static String SERVER = "ftp://maisonbleue2020.ddns.net/sgsrv/";
        private static String USER = "pi";
        private static String PASSWORD = "raspberry";


        public static WebClient GetConnection()
        {
            WebClient clienteWeb = null;
            try
            {
                clienteWeb = new WebClient();
                NetworkCredential credenciales = new NetworkCredential(USER, PASSWORD);
                clienteWeb.Credentials = credenciales;
            }
            catch (Exception ex)
            {

            }
            return clienteWeb;
        }
    }
}
