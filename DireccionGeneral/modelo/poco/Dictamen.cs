using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DireccionGeneral.modelo.poco
{
    class Dictamen
    {
        int folio;
        string descripcion;
        DateTime fechaHora;
        int idReporte;
        string username;



        public Dictamen()
        {
        }

        public int Folio { get => folio; set => folio = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
        public DateTime FechaHora { get => fechaHora; set => fechaHora = value; }
        public int IdReporte { get => idReporte; set => idReporte = value; }
        public string Username { get => username; set => username = value; }

        public override string ToString()
        {
            return Folio.ToString(); ;
        }
    }
}
