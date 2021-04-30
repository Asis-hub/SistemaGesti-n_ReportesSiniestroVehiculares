using System.Windows;
using System.Windows.Controls;

namespace DireccionGeneral.vistas
{
    /// <summary>
    /// Lógica de interacción para ConsultarDelegaciones.xaml
    /// </summary>
    public partial class ConsultarDelegaciones : Page
    {
        public ConsultarDelegaciones()
        {
            InitializeComponent();
        }

        private void btn_RegistrarDelegacion_Click(object sender, RoutedEventArgs e)
        {
            FormDelegacion formularioNuevaDelegacion = new FormDelegacion();
            formularioNuevaDelegacion.ShowDialog();
        }

        private void btn_EditarDelgación_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_EliminarDelgacion_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
