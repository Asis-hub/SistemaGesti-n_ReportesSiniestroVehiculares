using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servidor.modelo.poco
{
    class Conductor
    {
        private string numeroLicencia;
        private string celular;
        private string nombreCompleto;
        private DateTime fechaNacimiento;

        

        public Conductor()
        {
        }

        public string NumeroLicencia { get => numeroLicencia; set => numeroLicencia = value; }
        public string Celular { get => celular; set => celular = value; }
        public string NombreCompleto { get => nombreCompleto; set => nombreCompleto = value; }
        public DateTime FechaNacimiento { get => fechaNacimiento; set => fechaNacimiento = value; }

        public override string ToString()
        {
            return NumeroLicencia + " - " + NombreCompleto;
        }
    }
}
