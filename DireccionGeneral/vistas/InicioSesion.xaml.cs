using DireccionGeneral.modelo.dao;
using DireccionGeneral.modelo.poco;
using System.Windows;

namespace DireccionGeneral.vistas
{
    /// <summary>
    /// Lógica de interacción para InicioSesion.xaml
    /// </summary>
    public partial class InicioSesion : Window
    {
        public InicioSesion()
        {
            InitializeComponent();
        }

        private void btn_IniciarSesion_Click(object sender, RoutedEventArgs e)
        {
            string username = txt_Usuario.Text;
            string password = txt_Contrasenia.Password;

            if(username.Length > 0 && password.Length > 0)
            {
                Usuario usuarioConectado = null;
                usuarioConectado = UsuarioDAO.getInicioSesion(username, password);

                if(usuarioConectado != null)
                {
                    MenuPrincipal ventanaPrincipal = new MenuPrincipal(usuarioConectado);
                    ventanaPrincipal.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Credenciales no válidas", "Error");
                }
            }
            else
            {
                MessageBox.Show("Ingresa tus credenciales", "Campos vacios");
            }
            
        }

        private void CerrarVentana(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MinimizarVentana(object sender, RoutedEventArgs e)
        {
            if(this.WindowState == WindowState.Normal)
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
