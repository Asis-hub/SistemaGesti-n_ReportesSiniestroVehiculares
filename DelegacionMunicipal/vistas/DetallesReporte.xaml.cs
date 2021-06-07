using DelegacionMunicipal.modelo.dao;
using DelegacionMunicipal.modelo.poco;
using System;
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

            lbl_Folio.Content = reporteSiniestro.IdReporte.ToString();
            lbl_Calle.Content = reporteSiniestro.Calle.ToString();
            lbl_Numero.Content = reporteSiniestro.Numero.ToString();
            lbl_Colonia.Content = reporteSiniestro.Colonia.ToString();
            lbl_Delegacion.Content = reporteSiniestro.IdDelegacion.ToString();
            lbl_Usuario.Content = reporteSiniestro.Username.ToString();

            //Carga de imagenes
            //img_imagen1 

            if (reporteSiniestro.Dictamen)
            {
                Dictamen dictamen = DictamenDAO.ConsultarDictamen(reporteSiniestro.IdReporte);
                lbl_Dictamen.Content = dictamen.Folio;
                lbl_FechaDictamen.Content = dictamen.FechaHora;
                txt_DescripcionDictamen.Text = dictamen.Descripcion;
            }
            else
            {
                lbl_Dictamen.Content = "Pendiente";
            }
            



            


        }

        
    }
}
