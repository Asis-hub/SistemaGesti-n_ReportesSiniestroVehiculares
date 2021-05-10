using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegacionMunicipal.modelo.poco
{
    class Dictamen
    {
        private int folio;
        private string descripcion;
        private DateTime fechaHora;
        private int idReporte;
        private string username;

        public int Folio { get => folio; set => folio = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
        public DateTime FechaHora { get => fechaHora; set => fechaHora = value; }
        public int IdReporte { get => idReporte; set => idReporte = value; }
        public string Username { get => username; set => username = value; }
        

        public Dictamen()
        {

        }

        public override string ToString()
        {
            return folio + " - " + username;
        }
    }
}
