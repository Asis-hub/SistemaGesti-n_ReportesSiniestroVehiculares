using DelegacionMunicipal.modelo.dao;
using DelegacionMunicipal.modelo.poco;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Windows;

namespace DelegacionMunicipal.vistas
{
    /// <summary>
    /// Lógica de interacción para FormReporteSiniestro.xaml
    /// </summary>
    public partial class FormReporteSiniestro : Window
    {
        public FormReporteSiniestro()
        {
            InitializeComponent();
            CargarListaVehiculos();
        }

        private void CargarListaVehiculos()
        {
            cmb_Conductor.Items.Clear();
            cmb_Conductor.Items.Add("Conductor");
            List<Conductor> listaConductores = ConductorDAO.ConsultarConductores();
            foreach (Conductor elemento in listaConductores)
            {
                cmb_Conductor.Items.Add(elemento);
            }
            cmb_Conductor.SelectedIndex = 0;
            
        }

        private void btn_AgregarVehiculo_Click(object sender, RoutedEventArgs e)
        {
            //Agregar vehiculos involucrados que se seleccionan en combobox

        }

        private void btn_AgregarImagen_Click(object sender, RoutedEventArgs e)
        {
            //Agregar imagenes, minimo 3 y maximo 8
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Imagenes (*.jpg)|*.jpg|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                string filename = openFileDialog.FileName;
              
                string nombre = openFileDialog.SafeFileName;
                
                /*foreach (string filename in openFileDialog.FileNames)
                {
                    
                }*/

                


            }


        }

        private void btn_RegistrarReporte_Click(object sender, RoutedEventArgs e)
        {
            //Validar campos 
            //Validar cantidad de imagenes
            //Registrar con DAO, que retorne booleano



        }

        private void btn_CancelarRegistro_Click(object sender, RoutedEventArgs e)
        {
            //Cerrar ventana
        }
    }
}
