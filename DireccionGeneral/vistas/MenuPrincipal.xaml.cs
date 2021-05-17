using DireccionGeneral.modelo.poco;
using System.Windows;

namespace DireccionGeneral.vistas
{
    /// <summary>
    /// Lógica de interacción para MenuWindow.xaml
    /// </summary>
    public partial class MenuPrincipal : Window
    {
        private ConsultarUsuarios ventanaConsultarUsuarios;
        private ConsultarDelegaciones ventanaConsultarDelegaciones;
        private ConsultarReportes ventanaConsultarReportes;
        private SalaChat ventanaSalaChat;
        private Usuario usuarioConectado;

        public MenuPrincipal()
        {
            InitializeComponent();
            ventanaSalaChat = new SalaChat();
            frame_Content.Content = ventanaConsultarUsuarios;
        }

        public MenuPrincipal(Usuario usuarioConectado)
        {
            InitializeComponent();
            this.usuarioConectado = usuarioConectado;
            ventanaSalaChat = new SalaChat();
            ventanaConsultarUsuarios = new ConsultarUsuarios();
            frame_Content.Content = ventanaConsultarUsuarios;
        }

        private void btn_Usuarios_Click(object sender, RoutedEventArgs e)
        {
            if(ventanaConsultarUsuarios == null)
            {
                ventanaConsultarUsuarios = new ConsultarUsuarios();
            }
            frame_Content.Content = ventanaConsultarUsuarios;
        }

        private void btn_Delegaciones_Click(object sender, RoutedEventArgs e)
        {
            if(ventanaConsultarDelegaciones == null)
            {
                ventanaConsultarDelegaciones = new ConsultarDelegaciones();
            }
            frame_Content.Content = ventanaConsultarDelegaciones;
        }

        private void btn_Reportes_Click(object sender, RoutedEventArgs e)
        {
            if(ventanaConsultarReportes == null)
            {
                ventanaConsultarReportes = new ConsultarReportes();
            }
            frame_Content.Content = ventanaConsultarReportes;
        }

        private void btn_Chat_Click(object sender, RoutedEventArgs e)
        {
            frame_Content.Content = ventanaSalaChat;
        }

        private void btn_CerrarSesion_Click(object sender, RoutedEventArgs e)
        {
            InicioSesion ventanaInicioSesion = new InicioSesion();
            ventanaInicioSesion.Show();
            this.Close();
        }

        private void CerrarVentana(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MinimizarVentana(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
            {
                this.WindowState = WindowState.Minimized;
            }
            else
            {
                this.WindowState = WindowState.Normal;
            }
        }
    }
}
