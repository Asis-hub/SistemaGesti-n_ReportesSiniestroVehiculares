using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegacionMunicipal.interfaz
{   /// <summary>
    /// Interfaz para mandar un aviso que se actualiza la información de la BD
    /// </summary>
    public interface ObserverRespuesta
    {
        void ActualizaInformacion(string contenido, String titulo);
    }
}
