using System.Windows;
using System.Windows.Controls;

namespace DireccionGeneral.vistas
{
    /// <summary>
    /// Lógica de interacción para ConsultarReportes.xaml
    /// </summary>
    public partial class ConsultarReportes : Page
    {
        public ConsultarReportes()
        {
            InitializeComponent();
        }

        private void btn_BuscarReportes_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_VerDetalles_Click(object sender, RoutedEventArgs e)
        {
            DetallesReporte ventanaDetalles = new DetallesReporte();
            ventanaDetalles.ShowDialog();

        }

        private void btn_DictaminarReporte_Click(object sender, RoutedEventArgs e)
        {
            DictaminarReporte ventanaDictaminarReporte = new DictaminarReporte();
            ventanaDictaminarReporte.ShowDialog();
        }
    }
}
