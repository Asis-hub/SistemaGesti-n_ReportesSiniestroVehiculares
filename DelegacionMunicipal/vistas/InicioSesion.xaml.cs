using DelegacionMunicipal.conexion;
using DelegacionMunicipal.modelo.dao;
using DelegacionMunicipal.modelo.poco;
using System.Collections.ObjectModel;
using System.Windows;

namespace DelegacionMunicipal.vistas
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class InicioSesion : Window
    {
        private SocketLogin socketLogin;

        public InicioSesion()
        {
            InitializeComponent();
            socketLogin = new SocketLogin();
            socketLogin.IniciarConexion();
            CargarCmbDelegacion();
        }

        private void btn_IniciarSesion_Click(object sender, RoutedEventArgs e)
        {
            Usuario usuarioConectado;
            string username = txt_Usuario.Text;
            string password = txt_Contrasenia.Password;
            int idDelegacion = ((Delegacion)cmb_Delegacion.SelectedItem).IdDelegacion;

            usuarioConectado = UsuarioDAO.getInicioSesion(socketLogin, username, password, idDelegacion);

            if(usuarioConectado != null)
            {
                MenuPrincipal menuWindow = new MenuPrincipal(usuarioConectado);
                menuWindow.Show();
                this.Close();
            }

            
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

        private void CargarCmbDelegacion()
        {
            ObservableCollection<Delegacion> listaDelegaciones = DelegacionDAO.ConsultarDelegaciones(socketLogin);
            cmb_Delegacion.ItemsSource = listaDelegaciones;
        }
    }
}
