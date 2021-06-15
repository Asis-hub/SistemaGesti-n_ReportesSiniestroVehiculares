using DelegacionMunicipal.interfaz;
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
        private ObserverRespuesta notificacion;

        public FormVehiculo(ObserverRespuesta notificacion)
        {
            InitializeComponent();
            esNuevo = true;
            conductores = new List<Conductor>();
            CargarCmb_Conductores();
            this.notificacion = notificacion;
        }

        public FormVehiculo(Vehiculo vehiculoEdicion, ObserverRespuesta notificacion) : this(notificacion)
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
                int resultado;
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
                    notificacion.ActualizaInformacion("vehiculo con placas: " + vehiculo.NumPlaca + " se registró correctamente", "Vehículo registrado");
                    this.DialogResult = true;
                    this.Close();
                }
                else if (resultado == -1)
                {
                    notificacion.ActualizaInformacion("vehiculo con placas: " + vehiculo.NumPlaca + " ya se encuentra registrado en el sistema", "Registro duplicado");
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
            if (txt_NoPlacas.Text.Length == 0 || txt_Marca.Text.Length == 0 || txt_Modelo.Text.Length == 0 || txt_Color.Text.Length == 0 || 
                txt_Anio.Text.Length == 0 || !(cmb_Conductores.SelectedIndex >= 0))
            {
                notificacion.ActualizaInformacion("Debes llenar todos los campos", "Faltan campos por llenar");
                return false;
            }
            if (txt_Poliza.Text.Length > 0 && txt_Aseguradora.Text.Length == 0)
            {
                notificacion.ActualizaInformacion("Debes ingresar el nombre de la aseguradora que proporcionó la póliza", "Falta ingresar aseguradora");
                return false;
            }
            if (txt_Aseguradora.Text.Length > 0 && txt_Poliza.Text.Length == 0)
            {
                notificacion.ActualizaInformacion("Debes ingresar el número de poliza", "Falta ingresar número de póliza");
                return false;
            }

            return true;
        }
    }
}
