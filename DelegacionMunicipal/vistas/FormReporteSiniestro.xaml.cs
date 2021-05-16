using System.Windows;

namespace DelegacionMunicipal.vistas
{
    /// <summary>
    /// Lógica de interacción para FormReporteSiniestro.xaml
    /// </summary>
    public partial class FormReporteSiniestro : Window
    {
        public FormReporteSiniestro()
        {
            InitializeComponent();
        }

        private void btn_AgregarVehiculo_Click(object sender, RoutedEventArgs e)
        {
            //Agregar vehiculos involucrados que se seleccionan en combobox

        }

        private void btn_AgregarImagen_Click(object sender, RoutedEventArgs e)
        {
            //Agregar imagenes, minimo 3 y maximo 8

        }

        private void btn_RegistrarReporte_Click(object sender, RoutedEventArgs e)
        {
            //Validar campos 
            //Validar cantidad de imagenes
            //Registrar con DAO, que retorne booleano



        }

        private void btn_CancelarRegistro_Click(object sender, RoutedEventArgs e)
        {
            //Cerrar ventana
        }
    }
}
