using DelegacionMunicipal.modelo.dao;
using DelegacionMunicipal.modelo.poco;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace DelegacionMunicipal.vistas
{
    /// <summary>
    /// Lógica de interacción para ConsultarVehiculos.xaml
    /// </summary>
    public partial class ConsultarVehiculos : Page
    {
        List<Vehiculo> vehiculos;

        public ConsultarVehiculos()
        {
            InitializeComponent();
            vehiculos = new List<Vehiculo>();
            CargarTabla();
        }

        private void btn_RegistrarVehiculo_Click(object sender, RoutedEventArgs e)
        {
            AbrirFormulario(true);
        }

        private void btn_EditarVehiculo_Click(object sender, RoutedEventArgs e)
        {
            AbrirFormulario(false);
        }

        private void btn_EliminarVehiculo_Click(object sender, RoutedEventArgs e)
        {
            int indice = tbl_Vehiculos.SelectedIndex;
            if (indice >= 0)
            {
                int resultado = VehiculoDAO.EliminarVehiculo(vehiculos[indice].NumPlaca);
                if (resultado == 1)
                {
                    CargarTabla();
                }
            }
        }

        public void CargarTabla()
        {
            vehiculos = VehiculoDAO.ConsultarVehiculos();
            tbl_Vehiculos.ItemsSource = vehiculos;
        }

        private void AbrirFormulario(bool nuevo)
        {
            FormVehiculo formularioNuevoVehiculo;

            if (nuevo)
            {
                formularioNuevoVehiculo = new FormVehiculo();
            }
            else
            {
                int indice = tbl_Vehiculos.SelectedIndex;
                Vehiculo vehiculoEdicion = vehiculos[indice];
                formularioNuevoVehiculo = new FormVehiculo(vehiculoEdicion);
            }
            formularioNuevoVehiculo.Owner = Window.GetWindow(this);
            bool? resultado = formularioNuevoVehiculo.ShowDialog();
            if (resultado == true)
            {
                CargarTabla();
            }
        }
    }
}
