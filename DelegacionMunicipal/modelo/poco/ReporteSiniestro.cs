using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegacionMunicipal.modelo.poco
{
    public class ReporteSiniestro
    {
        private int idReporte;
        private string calle;
        private string numero;
        private string colonia;
        private int idDelegacion;
        private string username;
        private bool dictamen;

        public int IdReporte { get => idReporte; set => idReporte = value; }
        public string Calle { get => calle; set => calle = value; }
        public string Numero { get => numero; set => numero = value; }
        public string Colonia { get => colonia; set => colonia = value; }
        public int IdDelegacion { get => idDelegacion; set => idDelegacion = value; }
        public string Username { get => username; set => username = value; }
        public bool Dictamen { get => dictamen; set => dictamen = value; }


        public ReporteSiniestro()
        {

        }

        

        public override string ToString()
        {
            return "Reporte - " + idReporte;
        }
    }
}
