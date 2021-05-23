using DireccionGeneral.modelo.dao;
using DireccionGeneral.modelo.poco;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace DireccionGeneral.vistas
{
    /// <summary>
    /// Lógica de interacción para ConsultarUsuarios.xaml
    /// </summary>
    public partial class ConsultarUsuarios : Page
    {
        List<Usuario> usuarios;

        public ConsultarUsuarios()
        {
            InitializeComponent();
            usuarios = new List<Usuario>();
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
                int resultado = UsuarioDAO.EliminarUsuario(username);
                if(resultado == 1)
                {
                    MessageBox.Show("El usuario fue eliminado exitosamente.", "Usuario eliminado");
                }
                CargarTabla();
            }
        }

        public void CargarTabla()
        {
            usuarios = UsuarioDAO.ConsultarUsuarios();
            tbl_Usuarios.ItemsSource = usuarios;
        }
    }
}
