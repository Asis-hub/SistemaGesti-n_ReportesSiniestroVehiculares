using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegacionMunicipal.modelo.poco
{
    public class Conductor
    {
        private string numeroLicencia;
        private string celular;
        private string nombreCompleto;
        private string fechaNacimiento;

        public string NumeroLicencia { get => numeroLicencia; set => numeroLicencia = value; }
        public string Celular { get => celular; set => celular = value; }
        public string NombreCompleto { get => nombreCompleto; set => nombreCompleto = value; }
        public string FechaNacimiento { get => fechaNacimiento; set => fechaNacimiento = value; }
    }
}
