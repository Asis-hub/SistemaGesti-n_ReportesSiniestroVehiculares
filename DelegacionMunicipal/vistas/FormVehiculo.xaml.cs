using DelegacionMunicipal.interfaz;
using DelegacionMunicipal.modelo.dao;
using DelegacionMunicipal.modelo.poco;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
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
                string numPlaca = "";
                int resultado;
                Vehiculo vehiculo = new Vehiculo();
                if (!esNuevo)
                {
                    
                    vehiculo.NumPlaca = vehiculoEdicion.NumPlaca;
                }
                numPlaca = vehiculo.NumPlaca;
                vehiculo.NumPlaca = txt_NoPlacas.Text;
                vehiculo.Marca = txt_Marca.Text;
                vehiculo.Modelo = txt_Modelo.Text;
                vehiculo.Color = txt_Color.Text;
                vehiculo.NumPolizaSeguro = txt_Poliza.Text;
                vehiculo.NombreAseguradora = txt_Aseguradora.Text;
                vehiculo.Año = txt_Anio.Text;
                vehiculo.NumLicenciaConducir = conductores[cmb_Conductores.SelectedIndex].NumeroLicencia;



                if (esNuevo)
                {
                    resultado = VehiculoDAO.RegistrarVehiculo(vehiculo);
                }
                else
                {
                    resultado = VehiculoDAO.EditarVehiculo(numPlaca,vehiculo);
                }

                if (resultado >= 1)
                {
                    if (esNuevo)
                    {
                        notificacion.ActualizaInformacion("vehiculo con placas: " + vehiculo.NumPlaca + " se registró correctamente", "Vehículo registrado");
                    }
                    else
                    {
                        notificacion.ActualizaInformacion("vehiculo con placas: " + vehiculo.NumPlaca + " se actualizó correctamente", "Vehículo actualizado");
                    }
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

        //Metodo para limitar solo texto para el txt_Marca, solo se permiten letras
        private void txt_Marca_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
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

        //Metodo para limitar solo texto para el txt_Color, solo se permiten letras

        private void txt_Color_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
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

        //Metodo para limitar solo texto para el txt_Anio, solo se permiten numeros

        private void txt_Anio_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
