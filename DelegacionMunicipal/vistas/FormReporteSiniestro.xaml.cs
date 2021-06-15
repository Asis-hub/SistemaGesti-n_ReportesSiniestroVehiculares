using DelegacionMunicipal.conexion;
using DelegacionMunicipal.modelo.dao;
using DelegacionMunicipal.modelo.poco;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
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
        string licencia;
        Usuario usuarioConectado;
        //List<string> fotos;
        string[] fotos = new string[8];
        OpenFileDialog openFileDialog;
        public FormReporteSiniestro(Usuario usuarioConectado)
        {
            InitializeComponent();
            CargarListaConductores();
            cargarDelegaciones();
            //cargarBotones();
            this.usuarioConectado = usuarioConectado;
            cmb_Hora.SelectedIndex = 0;
            cmb_Minuto.SelectedIndex = 0;

            
            
        }

        private void CargarListaConductores()
        {
            cmb_Conductor.Items.Clear();
            cmb_Conductor.Items.Add("Conductor");
            List<Conductor> listaConductores = ConductorDAO.ConsultarConductores();
            foreach (Conductor conductor in listaConductores)
            {
                cmb_Conductor.Items.Add(conductor);
            }

            
        }

        private void CargarVehiculos()
        {
            licencia = cmb_Conductor.SelectedItem.ToString();
            cmb_Vehiculo.Items.Clear();
            cmb_Vehiculo.Items.Add("Vehiculo");
            List<Vehiculo> listaVehiculos = VehiculoDAO.ConsultarVehiculosConductor(licencia);
            foreach (Vehiculo vehiculo in listaVehiculos)
            {
                cmb_Vehiculo.Items.Add(vehiculo.NumPlaca);
            }

        }

        private void cargarDelegaciones()
        {
            cmb_delegacion.Items.Clear();
            cmb_delegacion.Items.Add("Delegacion");
            List<Delegacion> listaDelegaciones = DelegacionDAO.ConsultarDelegaciones();
            foreach (Delegacion delegacion in listaDelegaciones)
            {
                cmb_delegacion.Items.Add(delegacion.Municipio);
            }
        }



        private void btn_AgregarVehiculo_Click(object sender, RoutedEventArgs e)
        {
            
            lb_VehiculosInvolucrados.Items.Add(cmb_Vehiculo.SelectedItem.ToString());



        }

        

        private void btn_RegistrarReporte_Click(object sender, RoutedEventArgs e)
        {
            //Validar campos 
            //Validar cantidad de imagenes
            //Registrar con DAO, que retorne booleano
            if (ValidarFormulario())
            {
                ReporteSiniestro reporteSiniestro = new ReporteSiniestro();

                


                reporteSiniestro.Calle = txt_Calle.Text.ToString();
                reporteSiniestro.Numero = txt_Numero.Text.ToString();
                reporteSiniestro.Colonia = txt_Colonia.Text.ToString();
                
                reporteSiniestro.FechaHora = new DateTime(dpc_fecha.SelectedDate.Value.Year, dpc_fecha.SelectedDate.Value.Month, dpc_fecha.SelectedDate.Value.Day, int.Parse(cmb_Hora.Text), int.Parse(cmb_Minuto.Text), 0);
               
                reporteSiniestro.IdDelegacion = cmb_delegacion.SelectedIndex;
                reporteSiniestro.Username = usuarioConectado.Username;
                reporteSiniestro.Dictamen = false;
                
                reporteSiniestro.IdReporte = ReporteSiniestroDAO.RegistrarReporte(reporteSiniestro);
                

                foreach (string vehiculo in lb_VehiculosInvolucrados.Items)
                {
                    VehiculosInvolucradosDAO.InsertarVehiculo(vehiculo, reporteSiniestro.IdReporte);

                }

                int identificador = FotografiaDAO.InsertarFotografia(reporteSiniestro.IdReporte);

                Console.WriteLine(identificador.ToString());
                
              


            }


        }

        private void btn_CancelarRegistro_Click(object sender, RoutedEventArgs e)
        {
            //Cerrar ventana
        }

        private bool ValidarFormulario()
        {
            if (txt_Colonia.Text.Length == 0 || txt_Calle.Text.Length == 0 || txt_Numero.Text.Length ==0)
            {
                MessageBox.Show("Debes llenar todos los campos");
                return false;
            }

            return true;
        }

       

        private void cmb_Conductor_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            CargarVehiculos();
            

        }

        private void cargarBotones()
        {
           /* if (img1.Source !=null)
            {
                cambiarPresionado(btn_img1);

            }
            else
            {
                cambiarSinPresionar(btn_img1);
            }*/




        }

        private void cambiarPresionado(Button button)
        {
            button.Content = "Eliminar imagen";
            button.Background = Brushes.Red;
            button.Foreground = Brushes.White;
        }

        private void cambiarSinPresionar(Button button)
        {
            button.Content = "Agregar imagen";
            button.Background = Brushes.LightGray;
            button.Foreground = Brushes.Black;
        }

        private void btn_img0_Click(object sender, RoutedEventArgs e)
        {
            if (img0.Source != null)
            {
                
                fotos[0] = null;
                img0.Source = null;
                cambiarSinPresionar(btn_img0);
             
            }
            else
            {
                openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Imagenes(*.jpg) | *.jpg";
                openFileDialog.ShowDialog();
                
                fotos[0] = openFileDialog.FileName;
                
                cambiarPresionado(btn_img0);

                Uri uri = new Uri(openFileDialog.FileName);
                img0.Source = new BitmapImage(uri);

            }
            
        }

        

        private void btn_img2_Click(object sender, RoutedEventArgs e)
        {
            if (img2.Source != null)
            {
                
                fotos[1] = null;
                img2.Source = null;
                cambiarSinPresionar(btn_img2);

            }
            else
            {
                openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Imagenes(*.jpg) | *.jpg";
                openFileDialog.ShowDialog();

                fotos[1] = openFileDialog.FileName;

                cambiarPresionado(btn_img2);

                Uri uri = new Uri(openFileDialog.FileName);
                img2.Source = new BitmapImage(uri);

            }
        }

        private void cmb_Hora_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }

        private void cmb_Minuto_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }

        private void mostrarLista_Click(object sender, RoutedEventArgs e)
        {
            foreach (string ruta in fotos)
            {
                Console.WriteLine(ruta);
            }
        }
    }
}
