using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servidor.servicios
{
    public enum TipoConsulta
    {
        Select,
        Insert,
        Delete,
        Update
    }

    public enum TipoDato
    {
        Usuario,
        Delegacion,
        Vehiculo,
        Conductor,
        Municipio,
        Dictamen,
        ReporteSiniestro,
        DelegacionTipo,
        Cargo,
        Fotografia
    }

    public class Paquete
    {
        private TipoConsulta tipoQuery;
        private TipoDato tipoDominio;
        private string consulta;

        public string Consulta { get => consulta; set => consulta = value; }
        public TipoConsulta TipoQuery { get => tipoQuery; set => tipoQuery = value; }
        public TipoDato TipoDominio { get => tipoDominio; set => tipoDominio = value; }

        public Paquete()
        {

        }
    }
}
