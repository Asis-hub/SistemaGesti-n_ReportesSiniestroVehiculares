using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegacionMunicipal.modelo.poco
{
    class Fotografia
    {
        private int idFotografia;
        private string ruta;
        private int idReporte;

        public int IdFotografia { get => idFotografia; set => idFotografia = value; }
        public string Ruta { get => ruta; set => ruta = value; }
        public int IdReporte { get => idReporte; set => idReporte = value; }

        public Fotografia()
        {
        }

        public override string ToString()
        {
            return ruta;
        }
    }
}
