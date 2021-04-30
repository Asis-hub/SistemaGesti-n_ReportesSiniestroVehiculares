using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using DelegacionMunicipal.modelo.poco;


namespace DelegacionMunicipal.modelo.dao
{
    public class ConductorDAO
    {
        //Los parametros de los métodos pueden cambiarse
        public static bool Registrar(Conductor nuevoConductor)
        {

            return true;
        }

        public static bool Actualizar(Conductor conductor)
        {
            return true;
        }

        public static bool Eliminar(Conductor conductor)
        {
            return true;
        }

        public static Conductor buscarConductor(String parametro)
        {
            return null;
        }

    }
}
