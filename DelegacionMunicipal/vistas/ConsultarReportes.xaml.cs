using DelegacionMunicipal.modelo.dao;
using DelegacionMunicipal.conexion;
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

namespace DelegacionMunicipal.vistas
{
    /// <summary>
    /// Lógica de interacción para ConsultarReportes.xaml
    /// </summary>
    public partial class ConsultarReportes : Page
    {
        public ConsultarReportes()
        {
            InitializeComponent();
            ReporteDAO reporteDAO = new ReporteDAO();
            tbl_reportes.ItemsSource = reporteDAO.GetReportes();
  
            prueba.Content = reporteDAO.GetReportes().Count;

        }

        
        private void btn_VerDetalles_Click(object sender, RoutedEventArgs e)
        {
            int x = 1;
            DetallesReporte ventanaDetalles = new DetallesReporte(x);
            ventanaDetalles.ShowDialog();
        }

        private void btn_RegistrarReporte_Click(object sender, RoutedEventArgs e)
        {
            FormReporteSiniestro formReporteSiniestro = new FormReporteSiniestro();
            formReporteSiniestro.ShowDialog();
        }

        private void btn_BuscarReportes_Click(object sender, RoutedEventArgs e)
        {

        }

        private void tbl_Delegaciones_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cargarReportes()
        {

        }

        
    }
}
