using DireccionGeneral.conexion;
using DireccionGeneral.interfaz;
using DireccionGeneral.modelo.poco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DireccionGeneral.vistas
{
    /// <summary>
    /// Lógica de interacción para Chat.xaml
    /// </summary>
    public partial class SalaChat : Page, ObserverChat
    {
        private Usuario usuario;
        private List<string> listaUsuarios;


        public SalaChat(Usuario usuario)
        {
            InitializeComponent();
            this.usuario = usuario;
            listaUsuarios = new List<string>();
            SocketChat.Conectar(usuario.Username, this);
        }

        public void DesconectarChat()
        {
            SocketChat.Desconectar();
        }

        public void MostrarMensaje(MensajeChat mensaje)
        {
            this.Dispatcher.Invoke(() =>
            {
                if(mensaje.Tipo == TipoMensaje.ListaUsuarios)
                {
                    listaUsuarios = mensaje.Contenido.Split(';').ToList();
                    MostrarListaUsuarios();
                }
                else if (mensaje.Tipo == TipoMensaje.Conectarse && 
                         !listaUsuarios.Contains(mensaje.Usuario) && 
                         mensaje.Usuario != usuario.Username)
                {
                    listaUsuarios.Add(mensaje.Usuario);
                    MostrarListaUsuarios();
                }
                else if(mensaje.Tipo == TipoMensaje.Desconectarse)
                {
                    listaUsuarios.Remove(mensaje.Usuario);
                    MostrarListaUsuarios();
                }
                else if(mensaje.Tipo == TipoMensaje.Chat)
                {
                    bool esRemitente = (mensaje.Usuario == usuario.Username) ? true : false;
                    CuadroMensaje nuevoMensaje = new CuadroMensaje(mensaje, esRemitente);
                    pnl_Chat.Children.Add(nuevoMensaje);
                    scroll_Chat.ScrollToBottom();
                    txt_Mensaje.Text = "";
                }
            });
        }

        private void MostrarListaUsuarios()
        {
            pnl_Usuarios.Children.Clear();
            foreach (string usuarioChat in listaUsuarios)
            {
                Label lbl_Usuario = new Label();
                
                lbl_Usuario.Content = "> " + usuarioChat;
                if(usuarioChat == usuario.Username)
                {
                    lbl_Usuario.Foreground = Brushes.Red;
                    lbl_Usuario.Content += " (tú)";
                }
                lbl_Usuario.FontSize = 16;
                lbl_Usuario.Padding = new Thickness(5);
                pnl_Usuarios.Children.Add(lbl_Usuario);
            }

        }

        private void btn_EnviarMensaje_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (txt_Mensaje.Text.Length > 0)
            {
                MensajeChat mensaje = new MensajeChat();
                mensaje.Usuario = usuario.Username;
                mensaje.Contenido = txt_Mensaje.Text;
                mensaje.Tipo = TipoMensaje.Chat;
                mensaje.Fecha = DateTime.Now;

                string mensajeNuevo = JsonSerializer.Serialize(mensaje);
                SocketChat.EnviarMensaje(mensajeNuevo);
            }
        }
    }

    class CuadroMensaje : StackPanel
    {
        TextBlock mensaje;
        Label remitente;
        Label fecha;
        public CuadroMensaje(MensajeChat mensajeRecibido, bool esRemitente)
        {
            remitente = new Label();
            fecha = new Label();
            mensaje = new TextBlock();

            remitente.Content = mensajeRecibido.Usuario;
            fecha.Content = mensajeRecibido.Fecha;
            mensaje.Text = mensajeRecibido.Contenido;

            ConstruirCuadro(esRemitente);
        }

        private void ConstruirCuadro(bool esRemitente)
        {
            Thickness padding = new Thickness(10, 5, 10, 5);
            remitente.Padding = padding;
            remitente.HorizontalContentAlignment = HorizontalAlignment.Right;
            fecha.Padding = padding;
            mensaje.Padding = padding;
            mensaje.TextWrapping = TextWrapping.Wrap;
            mensaje.FontSize = 15;

            if (esRemitente)
            {
                this.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                Margin = new Thickness(60, 10, 20, 10);
            }
            else
            {
                this.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                Margin = new Thickness(20, 10, 60, 10);
            }

            StackPanel panel = new StackPanel();
            panel.Children.Add(fecha);
            panel.Children.Add(mensaje);
            panel.Children.Add(remitente);

            Border bordes = new Border();
            bordes.Background = Brushes.White;
            bordes.CornerRadius = new CornerRadius(10, 10, 10, 10);
            bordes.Child = panel;
            this.Children.Add(bordes);
        }
    }
}
