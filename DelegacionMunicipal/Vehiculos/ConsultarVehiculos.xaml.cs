using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DelegacionMunicipal.Vehiculos
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
