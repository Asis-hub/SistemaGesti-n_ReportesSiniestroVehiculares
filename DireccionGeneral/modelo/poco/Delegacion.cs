using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DireccionGeneral.modelo.poco
{
    public class Delegacion
    {
        private int idDelegacion;
        private int idMunicipio;
        private string municipio;
        private string nombre;
        private string correo;
        private string codigoPostal;
        private string calle;
        private string colonia;
        private string numero;
        private int idTipo;
        private string tipo;

        public int IdDelegacion { get => idDelegacion; set => idDelegacion = value; }
        public string Municipio { get => municipio; set => municipio = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Correo { get => correo; set => correo = value; }
        public string CodigoPostal { get => codigoPostal; set => codigoPostal = value; }
        public string Calle { get => calle; set => calle = value; }
        public string Colonia { get => colonia; set => colonia = value; }
        public string Numero { get => numero; set => numero = value; }
        public string Tipo { get => tipo; set => tipo = value; }
        public int IdMunicipio { get => idMunicipio; set => idMunicipio = value; }
        public int IdTipo { get => idTipo; set => idTipo = value; }

        public Delegacion()
        {

        }

        public override string ToString()
        {
            return idDelegacion + " - " + nombre;
        }
    }
}
