using DireccionGeneral.modelo.dao;
using DireccionGeneral.modelo.poco;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace DireccionGeneral.vistas
{
    /// <summary>
    /// Lógica de interacción para ConsultarReportes.xaml
    /// </summary>
    public partial class ConsultarReportes : Page
    {
        List<ReporteSiniestro> reportesSiniestro;
        Usuario usuarioConectado;

        public ConsultarReportes()
        {
            InitializeComponent();
            reportesSiniestro = new List<ReporteSiniestro>();
            CargarTabla();
        }

        public ConsultarReportes(Usuario usuario) : this()
        {
            usuarioConectado = usuario;
        }

        private void CargarTabla()
        {
            reportesSiniestro = ReporteSiniestroDAO.ConsultarReportes();
            tbl_Reportes.ItemsSource = reportesSiniestro;
        }

        private void btn_BuscarReportes_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_VerDetalles_Click(object sender, RoutedEventArgs e)
        {
            int seleccion = tbl_Reportes.SelectedIndex;
            if (seleccion >= 0)
            {
                ReporteSiniestro reporte = reportesSiniestro[seleccion];
                DetallesReporte ventanaDetalles = new DetallesReporte(reporte);
                ventanaDetalles.ShowDialog();
            }
        }

        private void btn_DictaminarReporte_Click(object sender, RoutedEventArgs e)
        {
            int seleccion = tbl_Reportes.SelectedIndex;
            if (seleccion >= 0)
            {
                int idReporte = reportesSiniestro[seleccion].IdReporte;

                if (!reportesSiniestro[seleccion].Dictamen)
                {
                    DictaminarReporte ventanaDictamen = new DictaminarReporte(usuarioConectado, idReporte);
                    ventanaDictamen.Owner = Window.GetWindow(this);
                    ventanaDictamen.ShowDialog();
                }
            }
        }
    }
}
