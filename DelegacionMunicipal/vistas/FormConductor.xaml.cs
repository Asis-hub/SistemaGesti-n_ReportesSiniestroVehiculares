using DelegacionMunicipal.modelo.dao;
using DelegacionMunicipal.modelo.poco;
using System;
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
            txt_FechaNacimiento.Text = conductorEdicion.FechaNacimiento.ToString();
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
                conductor.FechaNacimiento = Convert.ToDateTime(txt_FechaNacimiento.Text);



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
            if (txt_NoLicencia.Text.Length == 0)
                return false;
            if (txt_Telefono.Text.Length == 0)
                return false;
            if (txt_NombreConductor.Text.Length == 0)
                return false;
            if (txt_FechaNacimiento.Text.Length == 0)
                return false;

            return true;
        }
    }
}
