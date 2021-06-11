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
                    MessageBox.Show(usuario.NombreCompleto + " se registró correctamente","Usuario registrado");
                    this.DialogResult = true;
                    this.Close();
                } 
                else if (resultado == -1)
                {
                    MessageBox.Show(usuario.NombreCompleto + " ya se encuentra registrado en el sistema", "Registro duplicado");
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
            if (txt_Usuario.Text.Length == 0 || txt_Nombre.Text.Length == 0 || txt_Contraseña.Password.Length == 0 || 
                txt_ContraseñaConfirmacion.Password.Length == 0 || !(cmb_Cargo.SelectedIndex >= 0) || !(cmb_Delegacion.SelectedIndex >= 0))
            {
                MessageBox.Show("Faltan campos por llenar, favor de intentar de nuevo", "Campos faltantes", button: MessageBoxButton.OK);
                return false;
            }
            if (!ValidarPassword())
            {
                MessageBox.Show("La contraseña y su confirmación no coinciden, favor de intentar de nuevo", "Contraseñas no coinciden", button: MessageBoxButton.OK);
                return false;
            }
            if (txt_Usuario.Text.Contains(" ") || txt_Usuario.Text.Contains(" "))
            {
                MessageBox.Show("El usuario o contraseña no pueden tener espacios en blancos, favor de intentar de nuevo", "Espacios en blanco no permitidos", button: MessageBoxButton.OK);
                return false;
            }

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

        private void txt_Nombre_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            foreach (var ch in e.Text)
            {
                if (!((Char.IsLetter(ch) || Char.IsWhiteSpace(ch)))) {
                    e.Handled = true;

                    break;
                }
            }
        }

        private void txt_Usuario_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            foreach (var ch in e.Text)
            {
                if (!((Char.IsLetterOrDigit(ch)) || ch == '.' || ch == '-' || ch == '_'))
                {
                    e.Handled = true;

                    break;
                }
            }
        }

        private void txt_Contraseña_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            foreach (var ch in e.Text)
            {
                if (!((Char.IsLetterOrDigit(ch)) || ch == '.' || ch == '-' || ch == '_'))
                {
                    e.Handled = true;

                    break;
                }
            }
        }
    }
}
