using System.Windows;
using System.Windows.Controls;

namespace DelegacionMunicipal.vistas
{
    /// <summary>
    /// Lógica de interacción para ConsultarVehiculos.xaml
    /// </summary>
    public partial class ConsultarVehiculos : Page
    {
        public ConsultarVehiculos()
        {
            InitializeComponent();
        }

        private void btn_RegistrarVehiculo_Click(object sender, RoutedEventArgs e)
        {
            FormVehiculo formVehiculo = new FormVehiculo();
            formVehiculo.ShowDialog();
        }

        private void btn_EditarVehiculo_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_EliminarVehiculo_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
