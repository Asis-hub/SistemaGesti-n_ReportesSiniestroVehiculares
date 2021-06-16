using DireccionGeneral.interfaz;
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
    public partial class ConsultarReportes : Page , ObserverRespuesta
    {
        List<ReporteSiniestro> reportesSiniestro;
        Usuario usuarioConectado;
        List<Delegacion> listaDelegaciones;
        public ConsultarReportes()
        {
            InitializeComponent();
            reportesSiniestro = new List<ReporteSiniestro>();
            CargarTabla();

            listaDelegaciones = DelegacionDAO.ConsultarDelegaciones();
            cmb_Delegacion.ItemsSource = listaDelegaciones;
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
            btn_DictaminarReporte.IsEnabled = false;
            if(tbl_Reportes.Items.Count == 1)
            {
                ActualizaInformacion("Se encontró " + tbl_Reportes.Items.Count.ToString() + " resultado", "Resultado de búsqueda");
            }
            if(tbl_Reportes.Items.Count > 1)
            {
                ActualizaInformacion("Se encontraron " + tbl_Reportes.Items.Count.ToString() + " resultados", "Resultado de búsqueda");
            }
            if(tbl_Reportes.Items.Count == 0)
            {
                ActualizaInformacion("No se encontraron resultados que coincidan con los filtros de la búsqueda", "Resultado de búsqueda");
            }
        }

        private void btn_VerDetalles_Click(object sender, RoutedEventArgs e)
        {
            int seleccion = tbl_Reportes.SelectedIndex;
            if (seleccion >= 0)
            {
                ReporteSiniestro reporte = reportesSiniestro[seleccion];
                DetallesReporte ventanaDetalles = new DetallesReporte(reporte.IdReporte);
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
                    DictaminarReporte ventanaDictamen = new DictaminarReporte(usuarioConectado, idReporte, this);
                    ventanaDictamen.Owner = Window.GetWindow(this);
                    bool? resultado = ventanaDictamen.ShowDialog();
                    if(resultado == true)
                    {
                        CargarTabla();
                    }
                }
            }
        }

        private void HabilitarRegistroDictamen(object sender, SelectionChangedEventArgs e)
        {
            int seleccion = tbl_Reportes.SelectedIndex;
            if (seleccion >= 0 )
            {
                btn_DictaminarReporte.IsEnabled = reportesSiniestro[seleccion].Dictamen == false;
            }
        }

        public void ActualizaInformacion(string contenido, string titulo)
        {
            MessageBox.Show(contenido, titulo);
        }
    }
}
