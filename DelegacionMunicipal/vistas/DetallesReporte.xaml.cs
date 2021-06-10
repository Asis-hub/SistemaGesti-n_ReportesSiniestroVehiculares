using DelegacionMunicipal.conexion;
using DelegacionMunicipal.modelo.dao;
using DelegacionMunicipal.modelo.poco;
using System;
using System.Collections.Generic;
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

            List<Fotografia> fotografias = FotografiaDAO.ObtenerFotografias(idReporte);


            img_imagen1.Source = ConectorFTP.obtenerImagen(fotografias[0].IdFotografia.ToString());
            img_imagen2.Source = ConectorFTP.obtenerImagen(fotografias[1].IdFotografia.ToString());
            img_imagen3.Source = ConectorFTP.obtenerImagen(fotografias[2].IdFotografia.ToString());
            img_imagen4.Source = ConectorFTP.obtenerImagen(fotografias[3].IdFotografia.ToString());
            img_imagen5.Source = ConectorFTP.obtenerImagen(fotografias[4].IdFotografia.ToString());
            img_imagen6.Source = ConectorFTP.obtenerImagen(fotografias[5].IdFotografia.ToString());
            img_imagen7.Source = ConectorFTP.obtenerImagen(fotografias[6].IdFotografia.ToString());
            img_imagen8.Source = ConectorFTP.obtenerImagen(fotografias[7].IdFotografia.ToString());

            if (reporteSiniestro.Dictamen)
            {
                /*Dictamen dictamen = DictamenDAO.ConsultarDictamen(reporteSiniestro.IdReporte);
                //lbl_Dictamen.Content = dictamen.Folio;
                lbl_FechaDictamen.Content = dictamen.FechaHora;
                txt_DescripcionDictamen.Text = dictamen.Descripcion;*/
            }
            else
            {
                lbl_Dictamen.Content = "Pendiente";
            }
            



            


        }

        
    }
}
