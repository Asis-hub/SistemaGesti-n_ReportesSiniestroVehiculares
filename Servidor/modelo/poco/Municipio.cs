using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servidor.modelo.poco
{
    public class Municipio
    {
        private int idMunicipio;
        private string nombre;

        public int IdMunicipio { get => idMunicipio; set => idMunicipio = value; }
        public string Nombre { get => nombre; set => nombre = value; }

        public Municipio()
        {

        }

        public override string ToString()
        {
            return idMunicipio + " - " + nombre;
        }
    }
}
