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
            ventanaConsultarUsuarios = new ConsultarUsuarios();
            ventanaConsultarDelegaciones = new ConsultarDelegaciones();
            ventanaConsultarReportes = new ConsultarReportes();
            ventanaSalaChat = new SalaChat();

            frame_Content.Content = ventanaConsultarUsuarios;
        }

        public MenuPrincipal(Usuario usuarioConectado)
        {
            InitializeComponent();
            this.usuarioConectado = usuarioConectado;
            ventanaConsultarUsuarios = new ConsultarUsuarios();
            ventanaConsultarDelegaciones = new ConsultarDelegaciones();
            ventanaConsultarReportes = new ConsultarReportes();
            ventanaSalaChat = new SalaChat();

            frame_Content.Content = ventanaConsultarUsuarios;
            
        }

        private void btn_Usuarios_Click(object sender, RoutedEventArgs e)
        {
            frame_Content.Content = ventanaConsultarUsuarios;
        }

        private void btn_Delegaciones_Click(object sender, RoutedEventArgs e)
        {
            frame_Content.Content = ventanaConsultarDelegaciones;
        }

        private void btn_Reportes_Click(object sender, RoutedEventArgs e)
        {
            frame_Content.Content = ventanaConsultarReportes;
        }

        private void btn_Chat_Click(object sender, RoutedEventArgs e)
        {
            frame_Content.Content = ventanaSalaChat;
        }

        private void btn_CerrarSesion_Click(object sender, RoutedEventArgs e)
        {

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
