using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegacionMunicipal.modelo.poco
{
    class VehiculosInvolucrados
    {
        private string numeroPlaca;
        private int idReporte;

        public string NumeroPlaca { get => numeroPlaca; set => numeroPlaca = value; }

        public int IdReporte { get => idReporte; set => idReporte = value; }

    }
}
