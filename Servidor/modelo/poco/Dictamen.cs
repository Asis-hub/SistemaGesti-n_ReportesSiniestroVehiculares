using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servidor.modelo.poco
{
    public class Dictamen
    {
        int folio;
        string descripcion;
        DateTime fechaHora;
        int idReporte;
        string username;
        private string perito;

        public Dictamen()
        {
        }

        public int Folio { get => folio; set => folio = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
        public DateTime FechaHora { get => fechaHora; set => fechaHora = value; }
        public int IdReporte { get => idReporte; set => idReporte = value; }
        public string Username { get => username; set => username = value; }
        public string Perito { get => perito; set => perito = value; }

        public override string ToString()
        {
            return Folio.ToString(); ;
        }
    }
}
