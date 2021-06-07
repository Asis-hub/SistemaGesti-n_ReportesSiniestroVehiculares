using DelegacionMunicipal.conexion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegacionMunicipal.interfaz
{
    public interface ObserverChat
    {
        void MostrarMensaje(MensajeChat mensaje);
    }
}
