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
        private DateTime fechaHora;
        private DateTime fechaRegistro;
        private int idDelegacion;
        private string username;
        private bool dictamen;

        public int IdReporte { get => idReporte; set => idReporte = value; }
        public string Calle { get => calle; set => calle = value; }
        public string Numero { get => numero; set => numero = value; }
        public string Colonia { get => colonia; set => colonia = value; }
        public DateTime FechaHora { get => fechaHora; set => fechaHora = value; }
        public int IdDelegacion { get => idDelegacion; set => idDelegacion = value; }
        public string Username { get => username; set => username = value; }
        public bool Dictamen { get => dictamen; set => dictamen = value; }
        public DateTime FechaRegistro { get => fechaRegistro; set => fechaRegistro = value; }
        public string Estatus 
        {
            get
            {
                return dictamen ? "Dictaminado" : "Pendiente";
            }
        }

        

        public ReporteSiniestro()
        {

        }

        

        public override string ToString()
        {
            return "Reporte - " + idReporte;
        }
    }
}
