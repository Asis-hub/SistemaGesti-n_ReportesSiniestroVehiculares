using DelegacionMunicipal.interfaz;
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
        private ObserverRespuesta notificacion;

        public FormConductor(ObserverRespuesta notificacion)
        {
            InitializeComponent();
            this.notificacion = notificacion;
            esNuevo = true;
        }

        public FormConductor(Conductor conductorEdicion, ObserverRespuesta notificacion) : this(notificacion)
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
                int resultado;
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

                

                if (esNuevo)
                {
                    resultado = ConductorDAO.RegistrarConductor(conductor);
                }
                else
                {
                    resultado = ConductorDAO.EditarConductor(conductorEdicion.NumeroLicencia ,conductor);
                }

                if (resultado >= 1)
                {
                    if (esNuevo)
                    {
                        notificacion.ActualizaInformacion(conductor.NombreCompleto + " se registró correctamente", "Conductor registrado");
                    }
                    else
                    {
                        notificacion.ActualizaInformacion(conductor.NombreCompleto + " se actualizó correctamente", "Conductor actualizado");
                    }
                    this.DialogResult = true;
                    this.Close();
                }
                else if (resultado == -1)
                {
                    notificacion.ActualizaInformacion(conductor.NombreCompleto + " ya se encuentra registrado en el sistema", "Registro duplicado");
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
                notificacion.ActualizaInformacion("Debes llenar todos los campos", "Faltan campos por llenar");
                return false;
            }
            int edad = DiferenciaAnios(dp_FechaNacimiento.SelectedDate.Value, DateTime.Today);

            if (edad < 16)
            {
                notificacion.ActualizaInformacion("Edad no permitida, menor a la edad permitida para tener licencia", "Edad es menor a 16 años");
                return false;
            }

            if(txt_Telefono.Text.Length < 10)
            {
                notificacion.ActualizaInformacion("El número de teléfono debe contener 10 digitos, favor de intentar de nuevo", "Número de telefono no válido");
                return false;
            }

            return true;
        }


        //Metodo para limitar a numeros el txt_Telefono, solo se permiten numeros en este campo
        private void txt_Telefono_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]");
            e.Handled = regex.IsMatch(e.Text);
        }

        //Metodo para limitar a numeros el txt_NombreConductor, solo se permiten letras en este campo
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
        }

        //Metodo para obtener la edad de acuerdo a la fecha de nacimiento
        private int DiferenciaAnios(DateTime fechaSeleccionada, DateTime fechaActual)
        {
            return (fechaActual.Year - fechaSeleccionada.Year - 1) +
                (((fechaActual.Month > fechaSeleccionada.Month) ||
                ((fechaActual.Month == fechaSeleccionada.Month) && (fechaActual.Day >= fechaSeleccionada.Day))) ? 1 : 0);
        }
    }
}
