using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servidor.servicios
{
    /// <summary>
    /// Tipo de operaciones que se pueden realizar en la base de datos
    /// </summary>
    public enum TipoConsulta
    {
        Select,
        Insert,
        Delete,
        Update
    }

    /// <summary>
    /// Tipos de dato que se modifican en la base de datos
    /// </summary>
    public enum TipoDato
    {
        Usuario,
        Delegacion,
        Vehiculo,
        Conductor,
        Municipio,
        Dictamen,
        ReporteSiniestro,
        ReportesSiniestro,
        DelegacionTipo,
        Cargo,
        Fotografia,
        VehiculosInvolucrados
    }

    /// <summary>
    /// Plantilla del formato de Paquetes para atender las peticiones para la base de datos
    /// </summary>
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
