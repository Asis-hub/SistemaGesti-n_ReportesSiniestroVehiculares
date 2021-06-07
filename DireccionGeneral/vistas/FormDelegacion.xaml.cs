using DireccionGeneral.modelo.dao;
using DireccionGeneral.modelo.poco;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;

namespace DireccionGeneral.vistas
{
    /// <summary>
    /// Lógica de interacción para FormDelegacion.xaml
    /// </summary>
    public partial class FormDelegacion : Window
    {
        private List<Municipio> municipios;
        private List<DelegacionTipo> tiposDelegacion;
        private Delegacion delegacionEdicion;
        private bool esNuevo;

        public FormDelegacion()
        {
            InitializeComponent();
            esNuevo = true;
            municipios = new List<Municipio>();
            tiposDelegacion = new List<DelegacionTipo>();
            CargarCmb_Municipios();
            CargarCmb_TiposDelegacion();
        }

        public FormDelegacion(Delegacion delegacionEdicion) : this()
        {
            this.delegacionEdicion = delegacionEdicion;
            esNuevo = false;
            CargarDatosDelegacion();
        }

        private void CargarDatosDelegacion()
        {
            txt_Nombre.Text = delegacionEdicion.Nombre;
            txt_CodigoPostal.Text = delegacionEdicion.CodigoPostal;
            txt_Colonia.Text = delegacionEdicion.Colonia;
            txt_Calle.Text = delegacionEdicion.Calle;
            txt_Correo.Text = delegacionEdicion.Correo;
            txt_Numero.Text = delegacionEdicion.Numero;
            cmb_Municipio.SelectedIndex = municipios.FindIndex(x => x.IdMunicipio == delegacionEdicion.IdMunicipio);
            cmb_Tipo.SelectedIndex = tiposDelegacion.FindIndex(x => x.IdTipoDelegacion == delegacionEdicion.IdTipo);
        }

        private void btn_GuardarDelegacion_Click(object sender, RoutedEventArgs e)
        {
            if (ValidarFormulario())
            {
                int resultado;
                Delegacion delegacion = new Delegacion();
                if (!esNuevo)
                {
                    delegacion.IdDelegacion = delegacionEdicion.IdDelegacion;
                }
                delegacion.Nombre = txt_Nombre.Text;
                delegacion.CodigoPostal =txt_CodigoPostal.Text;
                delegacion.Colonia = txt_Colonia.Text;
                delegacion.Calle = txt_Calle.Text;
                delegacion.Correo = txt_Correo.Text;
                delegacion.Numero = txt_Numero.Text;

                int indiceMunicipio = cmb_Municipio.SelectedIndex;
                delegacion.IdMunicipio = municipios[indiceMunicipio].IdMunicipio;

                int indiceTipo = cmb_Tipo.SelectedIndex;
                delegacion.IdTipo = tiposDelegacion[indiceTipo].IdTipoDelegacion;

                

                if (esNuevo)
                {
                    resultado = DelegacionDAO.RegistrarDelegacion(delegacion);
                }
                else
                {
                    resultado = DelegacionDAO.EditarDelegacion(delegacion);
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
            this.DialogResult = false;
            this.Close();
        }

        private void CargarCmb_Municipios()
        {
            municipios = MunicipioDAO.ConsultarMunicipios();
            cmb_Municipio.ItemsSource = municipios;
        }

        private void CargarCmb_TiposDelegacion()
        {
            tiposDelegacion = DelegacionTipoDAO.ConsultarTipos();
            cmb_Tipo.ItemsSource = tiposDelegacion;
        }

        private bool ValidarFormulario()
        {
            if (txt_Nombre.Text.Length == 0 || txt_CodigoPostal.Text.Length == 0 || txt_Colonia.Text.Length == 0 || txt_Calle.Text.Length == 0 ||
                txt_Correo.Text.Length == 0 || txt_Numero.Text.Length == 0 || !(cmb_Municipio.SelectedIndex >= 0) || !(cmb_Tipo.SelectedIndex >= 0))
            {
                MessageBox.Show("Faltan campos por llenar, favor de intentar de nuevo", "Campos faltantes", button: MessageBoxButton.OK);
                return false;
            }
            if (!validarCorreo(txt_Correo.Text))
            {
                MessageBox.Show("Correo no válido, intentar de nuevo", "Correo no válido", button: MessageBoxButton.OK);
                return false;
            }

            return true;
        }

        private void txt_Nombre_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            foreach (var ch in e.Text)
            {
                if (!((Char.IsLetter(ch) || Char.IsWhiteSpace(ch))))
                {
                    e.Handled = true;

                    break;
                }
            }
        }

        bool validarCorreo(string correo)
        {
            try
            {
                var direccion = new System.Net.Mail.MailAddress(correo);
                return direccion.Address == correo;
            }
            catch
            {
                return false;
            }
        }

        private void txt_CodigoPostal_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void txt_Calle_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            foreach (var ch in e.Text)
            {
                if (!((Char.IsLetterOrDigit(ch)) || ch == '.' || ch == '/' || ch == ',' || ch == '#'))
                {
                    e.Handled = true;

                    break;
                }
            }
        }

        private void txt_Numero_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void txt_Colonia_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^A-Za-z]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
