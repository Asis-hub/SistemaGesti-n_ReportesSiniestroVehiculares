using DelegacionMunicipal.modelo.poco;
using System.Windows;

namespace DelegacionMunicipal.vistas
{
    /// <summary>
    /// Lógica de interacción para MenuWindow.xaml
    /// </summary>
    public partial class MenuPrincipal : Window
    {
        private ConsultarConductores ventanaConsultarConductores;
        private ConsultarVehiculos ventanaConsultarVehiculos;
        private ConsultarReportes ventanaConsultarReportes;
        private SalaChat ventanaSalaChat;

        private Usuario usuarioConectado;
               
        public MenuPrincipal(Usuario usuarioConectado)
        {
            InitializeComponent();
            this.usuarioConectado = usuarioConectado;
            ventanaConsultarConductores = new ConsultarConductores();
            ventanaConsultarVehiculos = new ConsultarVehiculos();
            ventanaConsultarReportes = new ConsultarReportes();
            ventanaSalaChat = new SalaChat(usuarioConectado);
            frame_Content.Content = ventanaConsultarConductores;
        }

        private void btn_Conductores_Click(object sender, RoutedEventArgs e)
        {
            frame_Content.Content = ventanaConsultarConductores;
        }

        private void btn_Vehiculos_Click(object sender, RoutedEventArgs e)
        {
            frame_Content.Content = ventanaConsultarVehiculos;
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
            ventanaSalaChat.DesconectarChat();
            InicioSesion ventanaInicioSesion = new InicioSesion();
            ventanaInicioSesion.Show();
            this.Close();
        }

        private void CerrarVentana(object sender, RoutedEventArgs e)
        {
            ventanaSalaChat.DesconectarChat();
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
