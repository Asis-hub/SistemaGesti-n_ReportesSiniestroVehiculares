using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DireccionGeneral.modelo.dao
{   /// <summary>
    /// POCO para Registrar y Obtener Vehículos Involucrados en siniestros
    /// </summary>
    class VehiculosInvolucrados
    {
        private string numeroPlaca;
        private int idReporte;

        public string NumeroPlaca { get => numeroPlaca; set => numeroPlaca = value; }

        public int IdReporte { get => idReporte; set => idReporte = value; }
    }
}
