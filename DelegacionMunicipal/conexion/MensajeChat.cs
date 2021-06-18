using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegacionMunicipal.conexion
{
    //POCO mensajes chat
    public enum TipoMensaje
    {
        Conectarse,
        Desconectarse,
        Chat,
        ListaUsuarios
    }

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
