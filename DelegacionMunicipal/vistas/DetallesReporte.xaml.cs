using DelegacionMunicipal.modelo.dao;
using DelegacionMunicipal.modelo.poco;
using System.Windows;

namespace DelegacionMunicipal.vistas
{
    /// <summary>
    /// Lógica de interacción para DetallesReporte.xaml
    /// </summary>
    public partial class DetallesReporte : Window
    {
        ReporteSiniestro reporteSiniestro;

        public DetallesReporte(int idReporte)
        {
            InitializeComponent();

            reporteSiniestro = ReporteSiniestroDAO.ObtenerReporte(idReporte);

            folio.Content = reporteSiniestro.IdReporte.ToString();
            calle.Content = reporteSiniestro.Calle.ToString();
            numero.Content = reporteSiniestro.Numero.ToString();
            colonia.Content = reporteSiniestro.Colonia.ToString();
            delegacion.Content = reporteSiniestro.IdDelegacion.ToString();
            usuario.Content = reporteSiniestro.Username.ToString();

            if (reporteSiniestro.Dictamen)
            {
                dictamen.Content = "Listo";
            }
            else
            {
                dictamen.Content = "Pendiente";
            }
            



            


        }
    }
}
