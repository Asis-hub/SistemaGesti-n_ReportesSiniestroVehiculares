using System.Windows;

namespace DelegacionMunicipal.vistas
{
    /// <summary>
    /// Lógica de interacción para FormVehiculo.xaml
    /// </summary>
    public partial class FormVehiculo : Window
    {
        public FormVehiculo()
        {
            InitializeComponent();
        }

        private void btn_RegistrarVehiculo_Click(object sender, RoutedEventArgs e)
        {

            this.DialogResult = true;
            this.Close();
        }

        private void btn_CancelarRegistro_Click(object sender, RoutedEventArgs e)
        {

            this.DialogResult = false;
            this.Close();
        }
    }
}
