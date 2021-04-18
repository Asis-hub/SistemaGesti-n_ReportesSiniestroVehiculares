using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DireccionGeneral.Usuarios
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
