using DireccionGeneral.conexion;
using DireccionGeneral.modelo.dao;
using DireccionGeneral.modelo.poco;
using System;
using System.Collections.Generic;
using System.Windows;

namespace DireccionGeneral.vistas
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

            cargarDatos();
            cargarFotos(idReporte);
        }

        private void cargarDatos()
        {

            lbl_Calle.Content = reporteSiniestro.Calle.ToString();
            lbl_Numero.Content = reporteSiniestro.Numero.ToString();
            lbl_Colonia.Content = reporteSiniestro.Colonia.ToString();
            lbl_Delegacion.Content = reporteSiniestro.NombreDelegacion;
            lbl_Usuario.Content = reporteSiniestro.NombreUsuario;

            List<Vehiculo> listaVehiculos = VehiculoDAO.ConsultarVehiculosReporte(reporteSiniestro.IdReporte);

            tbl_VehiculosInvolucrados.ItemsSource = listaVehiculos;

            if (reporteSiniestro.Dictamen)
            {
                Dictamen dictamen = DictamenDAO.ConsultarDictamenDeReporte(reporteSiniestro.IdReporte);

                

                lbl_FechaDictamen.Content = dictamen.FechaHora;
                txt_DescripcionDictamen.Text = dictamen.Descripcion;
                lbl_PeritoDictamen.Content = dictamen.Perito;
                Console.WriteLine(dictamen.Perito);
            }
            else
            {

                txt_DescripcionDictamen.Visibility = System.Windows.Visibility.Hidden;

            }
        }
        private void cargarFotos(int idReporte)
        {
            List<Fotografia> fotografias = FotografiaDAO.ObtenerFotografias(idReporte);

            //Imagen 1
            try
            {
                img_imagen1.Source = ConectorFTP.obtenerImagen(fotografias[0].IdFotografia.ToString());
            }
            catch (ArgumentOutOfRangeException)
            {

                img_imagen1.Source = ConectorFTP.obtenerImagen("NF");
            }

            //Imagen 2
            try
            {
                img_imagen2.Source = ConectorFTP.obtenerImagen(fotografias[1].IdFotografia.ToString());
            }
            catch (ArgumentOutOfRangeException)
            {

                img_imagen2.Source = ConectorFTP.obtenerImagen("NF");
            }

            //Imagen 3
            try
            {
                img_imagen3.Source = ConectorFTP.obtenerImagen(fotografias[2].IdFotografia.ToString());
            }
            catch (ArgumentOutOfRangeException)
            {

                img_imagen3.Source = ConectorFTP.obtenerImagen("NF");
            }

            //Imagen 4
            try
            {
                img_imagen4.Source = ConectorFTP.obtenerImagen(fotografias[3].IdFotografia.ToString());
            }
            catch (ArgumentOutOfRangeException)
            {

                img_imagen4.Source = ConectorFTP.obtenerImagen("NF");
            }

            //Imagen 5
            try
            {
                img_imagen5.Source = ConectorFTP.obtenerImagen(fotografias[4].IdFotografia.ToString());
            }
            catch (ArgumentOutOfRangeException)
            {

                img_imagen5.Source = ConectorFTP.obtenerImagen("NF");
            }

            //Imagen 6
            try
            {
                img_imagen6.Source = ConectorFTP.obtenerImagen(fotografias[5].IdFotografia.ToString());
            }
            catch (ArgumentOutOfRangeException)
            {

                img_imagen6.Source = ConectorFTP.obtenerImagen("NF");
            }

            //Imagen 7
            try
            {
                img_imagen7.Source = ConectorFTP.obtenerImagen(fotografias[6].IdFotografia.ToString());
            }
            catch (ArgumentOutOfRangeException)
            {

                img_imagen7.Source = ConectorFTP.obtenerImagen("NF");
            }

            //Imagen 8
            try
            {
                img_imagen8.Source = ConectorFTP.obtenerImagen(fotografias[7].IdFotografia.ToString());
            }
            catch (ArgumentOutOfRangeException)
            {

                img_imagen8.Source = ConectorFTP.obtenerImagen("NF");
            }
        }


    }
}
