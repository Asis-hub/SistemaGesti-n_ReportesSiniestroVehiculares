using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DireccionGeneral.modelo.poco
{
    /// <summary>
    /// POCO para Registrar, Obtener, Modificar y Eliminar vehiculos de la BD
    /// </summary>
    public class Vehiculo
    {
        private string numPlaca;
        private string marca;
        private string modelo;
        private string color;
        private string numPolizaSeguro;
        private string nombreAseguradora;
        private string año;
        private string numLicenciaConducir;

        public string NumPlaca { get => numPlaca; set => numPlaca = value; }
        public string Marca { get => marca; set => marca = value; }
        public string Modelo { get => modelo; set => modelo = value; }
        public string Color { get => color; set => color = value; }
        public string NumPolizaSeguro { get => numPolizaSeguro; set => numPolizaSeguro = value; }
        public string NombreAseguradora { get => nombreAseguradora; set => nombreAseguradora = value; }
        public string Año { get => año; set => año = value; }
        public string NumLicenciaConducir { get => numLicenciaConducir; set => numLicenciaConducir = value; }

        public Vehiculo()
        {

        }

        public override string ToString()
        {
            return numPlaca + " - " + numLicenciaConducir;
        }
    }
}
