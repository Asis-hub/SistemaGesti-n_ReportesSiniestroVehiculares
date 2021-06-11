using DelegacionMunicipal.modelo.dao;
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
        string licencia;
        Usuario usuarioConectado;
        public FormReporteSiniestro(Usuario usuarioConectado)
        {
            InitializeComponent();
            CargarListaConductores();
            cargarDelegaciones();
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
            //Agregar vehiculos involucrados que se seleccionan en combobox
            //cmb_Vehiculo
            //listaVehiculos.Add(cmb_Vehiculo.SelectedItem.ToString());
            lb_VehiculosInvolucrados.Items.Add(cmb_Vehiculo.SelectedItem.ToString());



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
            if (ValidarFormulario())
            {
                ReporteSiniestro reporteSiniestro = new ReporteSiniestro();

                


                reporteSiniestro.Calle = txt_Calle.Text.ToString();
                reporteSiniestro.Numero = txt_Numero.Text.ToString();
                reporteSiniestro.Colonia = txt_Colonia.Text.ToString();
                
                reporteSiniestro.FechaHora = new DateTime(dpc_fecha.SelectedDate.Value.Year, dpc_fecha.SelectedDate.Value.Month, dpc_fecha.SelectedDate.Value.Day, int.Parse(cmb_Hora.Text), int.Parse(cmb_Minuto.Text), 0);
               
                reporteSiniestro.IdDelegacion = cmb_delegacion.SelectedIndex;
                reporteSiniestro.Username = usuarioConectado.Username;
                reporteSiniestro.Dictamen = true;
                
                reporteSiniestro.IdReporte = ReporteSiniestroDAO.RegistrarReporte(reporteSiniestro);
                Console.WriteLine(reporteSiniestro.IdReporte);

                /*foreach (string vehiculo in lb_VehiculosInvolucrados.Items)
                {
                    VehiculosInvolucradosDAO.InsertarVehiculo(vehiculo, reporteSiniestro.IdReporte);

                }

                int x = FotografiaDAO.InsertarFotografia(reporteSiniestro.IdReporte);
                Console.WriteLine(x);*/


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

        private void ComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }

        private void cmb_Conductor_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            CargarVehiculos();
            

        }
    }
}
