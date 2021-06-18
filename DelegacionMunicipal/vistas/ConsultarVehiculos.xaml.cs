using DelegacionMunicipal.interfaz;
using DelegacionMunicipal.modelo.dao;
using DelegacionMunicipal.modelo.poco;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace DelegacionMunicipal.vistas
{
    /// <summary>
    /// Lógica de interacción para ConsultarVehiculos.xaml
    /// </summary>
    public partial class ConsultarVehiculos : Page, ObserverRespuesta
    {
        List<Vehiculo> vehiculos;

        public ConsultarVehiculos()
        {
            InitializeComponent();
            vehiculos = new List<Vehiculo>();
            CargarTablaVehiculos();
        }

        private void btn_RegistrarVehiculo_Click(object sender, RoutedEventArgs e)
        {
            AbrirFormulario(true, null);
        }

        private void btn_EditarVehiculo_Click(object sender, RoutedEventArgs e)
        {
            int indice = tbl_Vehiculos.SelectedIndex;
            if(indice >= 0)
            {
                Vehiculo vehiculoEdicion = vehiculos[indice];
                AbrirFormulario(false, vehiculoEdicion);
            }
            else
            {
                ActualizaInformacion("Para editar un vehículo debes seleccionarlo", "Sin selección");
            }
        }

        private void btn_EliminarVehiculo_Click(object sender, RoutedEventArgs e)
        {
            int indice = tbl_Vehiculos.SelectedIndex;
            if (indice >= 0)
            {
                Vehiculo vehiculoEliminar = vehiculos[indice];
                MessageBoxResult resultado = MessageBox.Show("¿Estás seguro de eliminar el vehículo con placas: " + vehiculoEliminar.NumPlaca + "?",
                    "Confirmar acción", MessageBoxButton.OKCancel, MessageBoxImage.Question);
                if (resultado == MessageBoxResult.OK)
                {
                    int resultadoEliminar = VehiculoDAO.EliminarVehiculo(vehiculoEliminar.NumPlaca);
                    Console.WriteLine("BOTON OK");
                    if(resultadoEliminar == -1)
                    {
                        ActualizaInformacion("No se pudo eliminar el vehículo ya que es parte de un reporte de un siniestro", "Eliminación no pudo realizarse");
                    }
                }
                else
                {
                    Console.WriteLine("BOTON CANCELAR");
                }
            }
            else
            {
                ActualizaInformacion("Para eliminar un vehículo debes seleccionarlo", "Sin selección");
            }
            CargarTablaVehiculos();
        }

        public void CargarTablaVehiculos()
        {
            vehiculos = VehiculoDAO.ConsultarVehiculos();
            tbl_Vehiculos.ItemsSource = vehiculos;
        }

        private void AbrirFormulario(bool nuevo, Vehiculo vehiculoEdicion)
        {
            FormVehiculo formularioNuevoVehiculo;

            if (nuevo)
            {
                formularioNuevoVehiculo = new FormVehiculo(this);
            }
            else
            {
                formularioNuevoVehiculo = new FormVehiculo(vehiculoEdicion, this);
            }
            formularioNuevoVehiculo.Owner = Window.GetWindow(this);
            bool? resultado = formularioNuevoVehiculo.ShowDialog();
            if (resultado == true)
            {
                CargarTablaVehiculos();
            }
        }

        public void ActualizaInformacion(string contenido, string titulo)
        {
            MessageBox.Show(contenido, titulo);
        }
    }
}
