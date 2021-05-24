using DireccionGeneral.modelo.dao;
using DireccionGeneral.modelo.poco;
using System;
using System.Collections.Generic;
using System.Windows;

namespace DireccionGeneral.vistas
{
    /// <summary>
    /// Lógica de interacción para FormUsuario.xaml
    /// </summary>
    public partial class FormUsuario : Window
    {
        List<Cargo> cargos;
        List<Delegacion> delegaciones;
        Usuario usuarioEdicion;
        bool esNuevo;

        public FormUsuario()
        {
            InitializeComponent();
            cargos = new List<Cargo>();
            delegaciones = new List<Delegacion>();
            CargarCmb_Cargo();
            CargarCmb_Delegacion();
            esNuevo = true;
        }

        public FormUsuario(Usuario usuarioEdicion) : this()
        {
            this.usuarioEdicion = usuarioEdicion;
            esNuevo = false;
            CargarDatoUsuario();
        }

        private void CargarDatoUsuario()
        {
            txt_Usuario.Text = usuarioEdicion.Username;
            txt_Nombre.Text = usuarioEdicion.NombreCompleto;
            txt_Contraseña.Password = usuarioEdicion.Password;
            txt_ContraseñaConfirmacion.Password = usuarioEdicion.Password;
            cmb_Cargo.SelectedIndex = cargos.FindIndex(x => x.IdCargo == usuarioEdicion.IdCargo);
            cmb_Delegacion.SelectedIndex = delegaciones.FindIndex(x => x.IdDelegacion == usuarioEdicion.IdDelegacion);
        }

        private void btn_GuardarUsuario_Click(object sender, RoutedEventArgs e)
        {
            if (ValidarFormulario())
            {
                int resultado;
                Usuario usuario = new Usuario();
                usuario.Username = txt_Usuario.Text;
                usuario.Password = txt_Contraseña.Password;
                usuario.NombreCompleto = txt_Nombre.Text;

                int indiceDelegacion = cmb_Delegacion.SelectedIndex;
                usuario.IdDelegacion = delegaciones[indiceDelegacion].IdDelegacion;

                int indiceCargo = cmb_Cargo.SelectedIndex;
                usuario.IdCargo = cargos[indiceCargo].IdCargo;

                if (esNuevo)
                {
                    resultado = UsuarioDAO.RegistrarUsuario(usuario);
                }
                else
                {
                    resultado = UsuarioDAO.EditarUsuario(usuarioEdicion.Username, usuario);
                }

                if (resultado == 1)
                {
                    this.DialogResult = true; ;
                    this.Close();
                }
            }
        }

        private void btn_CancelarRegistro_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void CargarCmb_Cargo()
        {
            cargos = CargoDAO.ConsultarCargos();
            cmb_Cargo.ItemsSource = cargos;
        }

        private void CargarCmb_Delegacion()
        {
            delegaciones = DelegacionDAO.ConsultarDelegaciones();
            cmb_Delegacion.ItemsSource = delegaciones;
        }

        private bool ValidarFormulario()
        {
            if (txt_Usuario.Text.Length == 0)
                return false;
            if (txt_Nombre.Text.Length == 0)
                return false;
            if (txt_Contraseña.Password.Length == 0)
                return false;
            if (txt_ContraseñaConfirmacion.Password.Length == 0)
                return false;
            if (!(cmb_Cargo.SelectedIndex >= 0))
                return false;
            if (!(cmb_Delegacion.SelectedIndex >= 0))
                return false;
            if (!ValidarPassword())
                return false;

            return true;
        }

        private bool ValidarPassword()
        {
            if(txt_Contraseña.Password == txt_ContraseñaConfirmacion.Password)
            {
                return true;
            }
            return false;
        }
    }
}
