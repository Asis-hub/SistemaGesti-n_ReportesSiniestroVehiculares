using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servidor.servicios
{
    /// <summary>
    /// Tipos de Mensajes en la sala de chat
    /// </summary>
    public enum TipoMensaje
    {
        Conectarse,
        Desconectarse,
        Chat,
        ListaUsuarios
    }

    /// <summary>
    /// Plantilla del formato de mensajes para la sala de chat
    /// </summary>
    public class MensajeChat
    {
        private string contenido;
        private string usuario;
        private DateTime fecha;
        private TipoMensaje tipo;

        public string Contenido { get => contenido; set => contenido = value; }
        public string Usuario { get => usuario; set => usuario = value; }
        public DateTime Fecha { get => fecha; set => fecha = value; }
        public TipoMensaje Tipo { get => tipo; set => tipo = value; }
    }
}
