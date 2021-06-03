﻿using DelegacionMunicipal.modelo.dao;
using DelegacionMunicipal.modelo.poco;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DelegacionMunicipal.vistas
{
    /// <summary>
    /// Lógica de interacción para FormReporteSiniestro.xaml
    /// </summary>

    
    public partial class FormReporteSiniestro : Window
    {
        List<string> listaVehiculos;
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
            //cmb_Vehiculo
            listaVehiculos.Add(cmb_Vehiculo.SelectedItem.ToString());
            


        }

        private void btn_AgregarImagen_Click(object sender, RoutedEventArgs e)
        {
            //Agregar imagenes, minimo 3 y maximo 8
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "Imagenes (*.jpg)|*.jpg|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                //string filename = openFileDialog.FileName;

                //string nombre = openFileDialog.SafeFileName;

                //output.Content = openFileDialog.FileNames.Length.ToString();

                if (openFileDialog.FileNames.Length > 8 || openFileDialog.FileNames.Length < 5)
                {
                    output.Content = "Ingrese entre 5 y 8 fotos";
                    output.Foreground = Brushes.Red;

                    Uri fileUri = new Uri(openFileDialog.FileName);
                    imagen1.Source = new BitmapImage(fileUri);

                }
                else
                {
                    output.Content = "Cantidad de fotos correcta";
                    output.Foreground = Brushes.Black;

                    

                    

                }
                
                
               
                


            }


        }

        private void btn_RegistrarReporte_Click(object sender, RoutedEventArgs e)
        {
            //Validar campos 
            //Validar cantidad de imagenes
            //Registrar con DAO, que retorne booleano
            if (txt_Colonia == )
            {

            }


        }

        private void btn_CancelarRegistro_Click(object sender, RoutedEventArgs e)
        {
            //Cerrar ventana
        }
    }
}
