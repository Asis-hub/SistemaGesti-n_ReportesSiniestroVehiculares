using DelegacionMunicipal.modelo.dao;
using DelegacionMunicipal.modelo.poco;
using System;
using System.Collections.Generic;
using System.Windows;

namespace DelegacionMunicipal.vistas
{
    /// <summary>
    /// Lógica de interacción para FormVehiculo.xaml
    /// </summary>
    public partial class FormVehiculo : Window
    {
        private List<Conductor> conductores;
        private Vehiculo vehiculoEdicion;
        private bool esNuevo;

        public FormVehiculo()
        {
            InitializeComponent();
            esNuevo = true;
            conductores = new List<Conductor>();
            CargarCmb_Conductores();
        }

        public FormVehiculo(Vehiculo vehiculoEdicion) : this()
        {
            this.vehiculoEdicion = vehiculoEdicion;
            esNuevo = false;
            CargarDatosVehiculo();
        }

        private void CargarDatosVehiculo()
        {
            txt_NoPlacas.Text = vehiculoEdicion.NumPlaca;
            txt_Marca.Text = vehiculoEdicion.Marca;
            txt_Modelo.Text = vehiculoEdicion.Modelo;
            txt_Color.Text = vehiculoEdicion.Color;
            txt_Poliza.Text = vehiculoEdicion.NumPolizaSeguro;
            txt_Aseguradora.Text = vehiculoEdicion.NombreAseguradora;
            txt_Anio.Text = vehiculoEdicion.Año;
            cmb_Conductores.SelectedIndex = conductores.FindIndex(x => x.NumeroLicencia == vehiculoEdicion.NumLicenciaConducir);
        }

        private void btn_GuardarVehiculo_Click(object sender, RoutedEventArgs e)
        {
            if (ValidarFormulario())
            {
                Vehiculo vehiculo = new Vehiculo();
                if (!esNuevo)
                {
                    vehiculo.NumPlaca = vehiculoEdicion.NumPlaca;
                }
                vehiculo.NumPlaca = txt_NoPlacas.Text;
                vehiculo.Marca = txt_Marca.Text;
                vehiculo.Modelo = txt_Modelo.Text;
                vehiculo.Color = txt_Color.Text;
                vehiculo.NumPolizaSeguro = txt_Poliza.Text;
                vehiculo.NombreAseguradora = txt_Aseguradora.Text;
                vehiculo.Año = txt_Anio.Text;
                vehiculo.NumLicenciaConducir = cmb_Conductores.SelectedItem.ToString();


                int resultado;

                if (esNuevo)
                {
                    resultado = VehiculoDAO.RegistrarVehiculo(vehiculo);
                }
                else
                {
                    resultado = VehiculoDAO.EditarVehiculo(vehiculo);
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

        private void CargarCmb_Conductores()
        {
            conductores = ConductorDAO.ConsultarConductores();
            cmb_Conductores.ItemsSource = conductores;
        }

        private bool ValidarFormulario()
        {
            if (txt_NoPlacas.Text.Length == 0)
                return false;
            if (txt_Marca.Text.Length == 0)
                return false;
            if (txt_Modelo.Text.Length == 0)
                return false;
            if (txt_Color.Text.Length == 0)
                return false;
            if (txt_Poliza.Text.Length == 0)
                return false;
            if (txt_Aseguradora.Text.Length == 0)
                return false;
            if (txt_Anio.Text.Length == 0)
                return false;
            if (!(cmb_Conductores.SelectedIndex >= 0))
                return false;

            return true;
        }
    }
}
