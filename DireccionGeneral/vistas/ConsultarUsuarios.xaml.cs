using DireccionGeneral.modelo.dao;
using DireccionGeneral.modelo.poco;
using System;
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
            CargarTabla();
        }

        private void btn_RegistrarUsuario_Click(object sender, RoutedEventArgs e)
        {
            FormUsuario formularioNuevoUsuario = new FormUsuario();
            formularioNuevoUsuario.Owner = Window.GetWindow(this);
            formularioNuevoUsuario.ShowDialog();
        }

        private void btn_EditarUsuario_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_EliminarUsuario_Click(object sender, RoutedEventArgs e)
        {
            if(tbl_Usuarios.SelectedIndex > -1)
            {
                string username = ((Usuario)tbl_Usuarios.SelectedItem).Username;
                int resultado = UsuarioDAO.EliminararUsuario(username);
                if(resultado == 1)
                {
                    MessageBox.Show("El usuario fue eliminado exitosamente.", "Usuario eliminado");
                }
                CargarTabla();
            }
        }

        public void CargarTabla()
        {
            tbl_Usuarios.ItemsSource = UsuarioDAO.ConsultarUsuarios();
        }
    }
}
