using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DireccionGeneral.interfaz
{
    public interface ObserverRespuesta
    {
        void ActualizaInformacion(string contenido, String titulo);
    }
}
