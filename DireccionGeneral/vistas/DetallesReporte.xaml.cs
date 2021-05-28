using DireccionGeneral.modelo.poco;
using System.Windows;

namespace DireccionGeneral.vistas
{
    /// <summary>
    /// Lógica de interacción para DetallesReporte.xaml
    /// </summary>
    public partial class DetallesReporte : Window
    {
        ReporteSiniestro reporte;
        public DetallesReporte(ReporteSiniestro reporte)
        {
            InitializeComponent();
            this.reporte = reporte;
        }
    }
}
