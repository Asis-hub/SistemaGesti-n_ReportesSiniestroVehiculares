using DelegacionMunicipal.modelo.dao;
using DelegacionMunicipal.modelo.poco;
using System;
using System.Text.RegularExpressions;
using System.Windows;

namespace DelegacionMunicipal.vistas
{
    /// <summary>
    /// Lógica de interacción para FormConductor.xaml
    /// </summary>
    public partial class FormConductor : Window
    {
        private Conductor conductorEdicion;
        private bool esNuevo;

        public FormConductor()
        {
            InitializeComponent();
            esNuevo = true;
        }

        public FormConductor(Conductor conductorEdicion) : this()
        {
            this.conductorEdicion = conductorEdicion;
            esNuevo = false;
            CargarDatosConductor();
        }

        private void CargarDatosConductor()
        {
            txt_NoLicencia.Text = conductorEdicion.NumeroLicencia;
            txt_Telefono.Text = conductorEdicion.Celular;
            txt_NombreConductor.Text = conductorEdicion.NombreCompleto;
            dp_FechaNacimiento.SelectedDate = conductorEdicion.FechaNacimiento;
        }

        private void btn_GuardarConductor_Click(object sender, RoutedEventArgs e)
        {
            if (ValidarFormulario())
            {
                Conductor conductor = new Conductor();
                if (!esNuevo)
                {
                    conductor.NumeroLicencia = conductorEdicion.NumeroLicencia;
                }
                conductor.NumeroLicencia = txt_NoLicencia.Text;
                conductor.Celular = txt_Telefono.Text;
                conductor.NombreCompleto = txt_NombreConductor.Text;
                if (dp_FechaNacimiento.SelectedDate.HasValue)
                {
                    conductor.FechaNacimiento = dp_FechaNacimiento.SelectedDate.Value.Date;
                }

                int resultado;

                if (esNuevo)
                {
                    resultado = ConductorDAO.RegistrarConductor(conductor);
                }
                else
                {
                    resultado = ConductorDAO.EditarConductor(conductor);
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

        private bool ValidarFormulario()
        {
            if (txt_NoLicencia.Text.Length == 0 || txt_Telefono.Text.Length == 0 || txt_NombreConductor.Text.Length == 0 || !dp_FechaNacimiento.SelectedDate.HasValue)
            {
                MessageBox.Show("Debes llenar todos los campos");
                return false;
            }
            int edad = DiferenciaAnios(dp_FechaNacimiento.SelectedDate.Value, DateTime.Today);

            if (edad < 16)
            {
                MessageBox.Show("Edad no permitida, menor a la edad permitida para tener licencia");
                return false;
            }

            if(txt_Telefono.Text.Length < 10)
            {
                MessageBox.Show("El número de teléfono debe contener 10 digitos, favor de intentar de nuevo");
                return false;
            }

            return true;
        }

        private void txt_Telefono_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void txt_NombreConductor_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            foreach (var ch in e.Text)
            {
                if (!((Char.IsLetter(ch) || Char.IsWhiteSpace(ch))))
                {
                    e.Handled = true;

                    break;
                }
            }
            /*
             * Regex regex = new Regex("[^a-zA-ZáéúíóÁÉÍÓÚüÜ]");
             * e.Handled = regex.IsMatch(e.Text);
             */
        }
        /*
        private void dp_FechaNacimiento_SelectedDateChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            int edad = DiferenciaAnios(dp_FechaNacimiento.SelectedDate.Value, DateTime.Today);
            if (edad < 18)
            {
                MessageBox.Show("Edad inválida, menor a la fecha permitida");
                dp_FechaNacimiento.SelectedDate = null;
            }
        }
        */

        private int DiferenciaAnios(DateTime fechaSeleccionada, DateTime fechaActual)
        {
            return (fechaActual.Year - fechaSeleccionada.Year - 1) +
                (((fechaActual.Month > fechaSeleccionada.Month) ||
                ((fechaActual.Month == fechaSeleccionada.Month) && (fechaActual.Day >= fechaSeleccionada.Day))) ? 1 : 0);
        }
    }
}
