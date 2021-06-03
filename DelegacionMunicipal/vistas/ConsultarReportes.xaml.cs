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
using DelegacionMunicipal.modelo.poco;

namespace DelegacionMunicipal.vistas
{
    /// <summary>
    /// Lógica de interacción para ConsultarReportes.xaml
    /// </summary>
    public partial class ConsultarReportes : Page
    {
        List<ReporteSiniestro> reportesSiniestro;
        public ConsultarReportes()
        {
            InitializeComponent();
            
            reportesSiniestro = new List<ReporteSiniestro>();


            cargarTabla();

            ReporteSiniestro reporteSiniestro = ReporteSiniestroDAO.ObtenerReporte(2);

            Console.WriteLine(reporteSiniestro.Username.ToString());

            

        }

        
        private void btn_VerDetalles_Click(object sender, RoutedEventArgs e)
        {
            int seleccion = tbl_Reportes.SelectedIndex;
            if(seleccion >= 0)
            {
                ReporteSiniestro reporte = reportesSiniestro[seleccion];
                DetallesReporte ventanaDetalles = new DetallesReporte(3);
                ventanaDetalles.ShowDialog();
            }
        }

        private void btn_RegistrarReporte_Click(object sender, RoutedEventArgs e)
        {
            FormReporteSiniestro formReporteSiniestro = new FormReporteSiniestro();
            formReporteSiniestro.ShowDialog();
        }

        private void btn_BuscarReportes_Click(object sender, RoutedEventArgs e)
        {

        }

        
        private void cargarTabla()
        {
            reportesSiniestro = ReporteSiniestroDAO.ConsultarReportes();
            btn_BuscarReportes.Content = reportesSiniestro.Count;
            tbl_Reportes.ItemsSource = reportesSiniestro;
          
        }

        
    }
}
