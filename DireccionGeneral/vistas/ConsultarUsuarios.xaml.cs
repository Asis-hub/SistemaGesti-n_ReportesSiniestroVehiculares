using DireccionGeneral.interfaz;
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
    public partial class ConsultarUsuarios : Page , ObserverRespuesta
    {
        List<Usuario> usuarios;

        public ConsultarUsuarios()
        {
            InitializeComponent();
            usuarios = new List<Usuario>();
            CargarTablaUsuarios();
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
            else
            {
                ActualizaInformacion("Para editar un usuario debes seleccionarlo", "Sin selección");
            }
        }

        private void btn_EliminarUsuario_Click(object sender, RoutedEventArgs e)
        {
            int indice = tbl_Usuarios.SelectedIndex;
            if (indice >= 0)
            {
                Usuario usuarioEliminar = usuarios[indice];
                MessageBoxResult resultado = MessageBox.Show("¿Estás seguro de eliminar el usuario " + usuarioEliminar.Username +
                    " perteneciente a " + usuarioEliminar.NombreCompleto + "?",
                    "Confirmar acción", MessageBoxButton.OKCancel, MessageBoxImage.Question);
                if (resultado == MessageBoxResult.OK)
                {
                    UsuarioDAO.EliminarUsuario(usuarioEliminar.Username);
                    Console.WriteLine("BOTON OK");
                }
                else
                {
                    Console.WriteLine("BOTON CANCELAR");
                }
            }
            else
            {
                ActualizaInformacion("Para eliminar un usuario debes seleccionarlo", "Sin selección");
            }
            CargarTablaUsuarios();
        }

        public void CargarTablaUsuarios()
        {
            usuarios = UsuarioDAO.ConsultarUsuarios();
            tbl_Usuarios.ItemsSource = usuarios;
        }

        private void AbrirFormulario(bool nuevo, Usuario usuario)
        {
            FormUsuario formularioUsuario;

            if (nuevo)
            {
                formularioUsuario = new FormUsuario(this);
            }
            else
            {
                formularioUsuario = new FormUsuario(usuario, this);
            }

            formularioUsuario.Owner = Window.GetWindow(this);
            bool? resultado = formularioUsuario.ShowDialog();
            if (resultado == true)
            {
                CargarTablaUsuarios();
            }
        }
        public void ActualizaInformacion(string contenido, string titulo)
        {
            MessageBox.Show(contenido, titulo);
        }
    }
}
