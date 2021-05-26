using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DireccionGeneral.modelo.poco
{
    public class ReporteSiniestro
    {
        private int idReporte;
        private string calle;
        private string numero;
        private string colonia;
        private int idDelegacion;
        private string username;

        public int IdReporte { get => idReporte; set => idReporte = value; }
        public string Calle { get => calle; set => calle = value; }
        public string Numero { get => numero; set => numero = value; }
        public string Colonia { get => colonia; set => colonia = value; }
        public int IdDelegacion { get => idDelegacion; set => idDelegacion = value; }
        public string Username { get => username; set => username = value; }


        public ReporteSiniestro()
        {

        }



        public override string ToString()
        {
            return "Reporte - " + idReporte;
        }
    }
}
