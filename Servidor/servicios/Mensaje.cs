using Servidor.modelo.poco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servidor.servicios
{
    public enum TipoMensaje
    {
        Ingreso,
        Salida,
        Mensaje
    }

    public class Mensaje
    {
        private string mensajeChat;
        private string fecha;
        private Usuario usuarioChat;
        private TipoMensaje tipo;

        public string MensajeChat { get => mensajeChat; set => mensajeChat = value; }
        public string Fecha { get => fecha; set => fecha = value; }
        public Usuario UsuarioChat { get => usuarioChat; set => usuarioChat = value; }
        public TipoMensaje Tipo { get => tipo; set => tipo = value; }
        
    }
}
