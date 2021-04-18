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

namespace DireccionGeneral.Delegaiciones
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
