using DireccionGeneral.conexion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DireccionGeneral.interfaz
{
    public interface ObserverChat
    {
        void MostrarMensaje(MensajeChat mensaje);
    }
}
