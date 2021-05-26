using DireccionGeneral.modelo.poco;
using System;
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
        List<ReporteSiniestro> reportes;
        Usuario usuarioConectado;

        public ConsultarReportes()
        {
            InitializeComponent();
            reportes = new List<ReporteSiniestro>();
            CargarTabla();
        }

        public ConsultarReportes(Usuario usuario) : this()
        {
            usuarioConectado = usuario;
        }

        private void CargarTabla()
        {
            //reportes = ReportesDAO.Consultar()
            tbl_Reportes.ItemsSource = reportes;
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
            
        }
    }
}
