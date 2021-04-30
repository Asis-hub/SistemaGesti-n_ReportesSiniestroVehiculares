using System.Windows;
using System.Windows.Controls;

namespace DireccionGeneral.vistas
{
    /// <summary>
    /// Lógica de interacción para ConsultarUsuarios.xaml
    /// </summary>
    public partial class ConsultarUsuarios : Page
    {
        public ConsultarUsuarios()
        {
            InitializeComponent();
        }

        private void btn_RegistrarUsuario_Click(object sender, RoutedEventArgs e)
        {
            FormUsuario formularioNuevoUsuario = new FormUsuario();
            formularioNuevoUsuario.ShowDialog();
        }

        private void btn_EditarUsuario_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_EliminarUsuario_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
