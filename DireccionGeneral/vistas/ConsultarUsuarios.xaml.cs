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
            AbrirFormulario(true, null);
        }

        private void btn_EditarUsuario_Click(object sender, RoutedEventArgs e)
        {
            int seleccion = tbl_Usuarios.SelectedIndex;
            if (seleccion >= 0)
            {
                AbrirFormulario(false, usuarios[seleccion]);
            }
        }

        private void btn_EliminarUsuario_Click(object sender, RoutedEventArgs e)
        {
            if(tbl_Usuarios.SelectedIndex >= 0)
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

        private void AbrirFormulario(bool nuevo, Usuario usuario)
        {
            FormUsuario formularioUsuario;

            if (nuevo)
            {
                formularioUsuario = new FormUsuario();
            }
            else
            {
                formularioUsuario = new FormUsuario(usuario);
            }

            formularioUsuario.Owner = Window.GetWindow(this);
            bool? resultado = formularioUsuario.ShowDialog();

            if (resultado == true)
            {
                CargarTabla();
            }
        }

    }
}
