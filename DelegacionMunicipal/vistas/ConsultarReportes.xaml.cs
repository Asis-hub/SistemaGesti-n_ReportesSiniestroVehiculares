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
using System.Data.SqlClient;

namespace DelegacionMunicipal.vistas
{
    /// <summary>
    /// Lógica de interacción para ConsultarReportes.xaml
    /// </summary>
    public partial class ConsultarReportes : Page
    {
        List<ReporteSiniestro> reportesSiniestro;
        List<Delegacion> listaDelegaciones;
        Usuario usuarioConectado;
        public ConsultarReportes(Usuario usuarioConectado)
        {
            InitializeComponent();
            this.usuarioConectado = usuarioConectado;
            reportesSiniestro = new List<ReporteSiniestro>();
            listaDelegaciones = new List<Delegacion>();
            CargarTabla();
            listaDelegaciones = DelegacionDAO.ConsultarDelegaciones();
            cmb_Delegacion.ItemsSource = listaDelegaciones;
        }

        
        private void btn_VerDetalles_Click(object sender, RoutedEventArgs e)
        {
            int seleccion = tbl_Reportes.SelectedIndex;
            if(seleccion >= 0)
            {
                ReporteSiniestro reporte = reportesSiniestro[seleccion];
                DetallesReporte ventanaDetalles = new DetallesReporte(reporte.IdReporte);
                ventanaDetalles.ShowDialog();
            }
        }

        private void btn_RegistrarReporte_Click(object sender, RoutedEventArgs e)
        {
            FormReporteSiniestro formReporteSiniestro = new FormReporteSiniestro(usuarioConectado);
            bool? resultado = formReporteSiniestro.ShowDialog();
            if (resultado == true)
            {
                CargarTabla();
            }

        }

        private void btn_BuscarReportes_Click(object sender, RoutedEventArgs e)
        {
            int dictaminado = 0;
            string fecha = "";
            string delegacion = "";

            if (dpck_Fecha.SelectedDate != null)
            {
                
                fecha = dpck_Fecha.SelectedDate.Value.ToString("yyyy-MM-dd");

            }
            else
            {
                fecha = "%";

            }

            if (rdb_Dictaminado.IsChecked == true)
            {
                dictaminado = 1;
            }
            else if (rdb_Pendiente.IsChecked == true)
            {
                dictaminado = 0;
            }

            int seleccion = cmb_Delegacion.SelectedIndex;

            if (seleccion >= 0)
            {
                delegacion = listaDelegaciones[seleccion].IdDelegacion.ToString();
            }
            else
            {
                delegacion = "%";
            }

            reportesSiniestro = ReporteSiniestroDAO.BuscarReportes(dictaminado, delegacion, fecha);
            
            tbl_Reportes.ItemsSource = reportesSiniestro;

            dpck_Fecha.SelectedDate = null;
            rdb_Pendiente.IsChecked = true;
            cmb_Delegacion.SelectedIndex = -1;

            //Notificacion de resultados, observer respuesta

        }

        private void CargarTabla()
        {
            reportesSiniestro = ReporteSiniestroDAO.ConsultarReportes();
            tbl_Reportes.ItemsSource = reportesSiniestro;
        }

        private void CargarCcmb_Delegacion()
        {
            listaDelegaciones = DelegacionDAO.ConsultarDelegaciones();
            cmb_Delegacion.ItemsSource = listaDelegaciones;
        }
    }
}
