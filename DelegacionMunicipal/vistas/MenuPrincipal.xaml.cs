using DelegacionMunicipal.modelo.poco;
using System.Windows;

namespace DelegacionMunicipal.vistas
{
    /// <summary>
    /// Lógica de interacción para MenuWindow.xaml
    /// </summary>
    public partial class MenuPrincipal : Window
    {
        private ConsultarConductores consultarConductores;
        private ConsultarVehiculos consultarVehiculos;
        private ConsultarReportes consultarReportes;
        private SalaChat salaChat;

        private Usuario usuarioConectado;
        public MenuPrincipal()
        {
            InitializeComponent();
            consultarConductores = new ConsultarConductores();
            consultarVehiculos = new ConsultarVehiculos();
            consultarReportes = new ConsultarReportes();
            salaChat = new SalaChat();
            frame_Content.Content = consultarConductores;
        }
        
        public MenuPrincipal(Usuario usuarioConectado)
        {
            InitializeComponent();
            this.usuarioConectado = usuarioConectado;
            consultarConductores = new ConsultarConductores();
            consultarVehiculos = new ConsultarVehiculos();
            consultarReportes = new ConsultarReportes();
            salaChat = new SalaChat();
            frame_Content.Content = consultarConductores;
        }

        private void btn_Conductores_Click(object sender, RoutedEventArgs e)
        {
            frame_Content.Content = consultarConductores;
        }

        private void btn_Vehiculos_Click(object sender, RoutedEventArgs e)
        {
            frame_Content.Content = consultarVehiculos;
        }

        private void btn_Reportes_Click(object sender, RoutedEventArgs e)
        {
            frame_Content.Content = consultarReportes;
        }

        private void btn_Chat_Click(object sender, RoutedEventArgs e)
        {
            frame_Content.Content = salaChat;
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
