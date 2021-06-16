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
        private Usuario usuarioConectado;
        private List<Vehiculo> listaVehiculosInvolucrados;
        private List<Vehiculo> listaVehiculos;
        private List<Delegacion> listaDelegaciones;
        private List<string> listaImagenes;

        
        //List<string> fotos;
        string[] fotos = new string[8];
        OpenFileDialog openFileDialog;
        public FormReporteSiniestro(Usuario usuarioConectado)
        {
            InitializeComponent();
            this.usuarioConectado = usuarioConectado;
            listaVehiculosInvolucrados = new List<Vehiculo>();
            listaDelegaciones = new List<Delegacion>();
            listaVehiculosInvolucrados = new List<Vehiculo>();
            listaImagenes = new List<string>();
            dpc_fecha.DisplayDateEnd = DateTime.Now;

            CargarListaConductores();
            cargarDelegaciones();
            //cargarBotones();
            
            cmb_Hora.SelectedIndex = 0;
            cmb_Minuto.SelectedIndex = 0;
        }

        private void CargarListaConductores()
        {
            cmb_Conductor.Items.Clear();
            List<Conductor> listaConductores = ConductorDAO.ConsultarConductores();
            cmb_Conductor.ItemsSource = listaConductores;
        }

        private void CargarVehiculos()
        {
            if(cmb_Conductor.SelectedIndex >= 0)
            {
                string licencia = ((Conductor)cmb_Conductor.SelectedItem).NumeroLicencia;
                listaVehiculos = VehiculoDAO.ConsultarVehiculosConductor(licencia);
                cmb_Vehiculo.ItemsSource = listaVehiculos;
            }
        }

        private void cargarDelegaciones()
        {
            listaDelegaciones = DelegacionDAO.ConsultarDelegaciones();
            cmb_delegacion.ItemsSource = listaDelegaciones;
        }

        private void btn_AgregarVehiculo_Click(object sender, RoutedEventArgs e)
        {
            int seleccion = cmb_Vehiculo.SelectedIndex;
            if(seleccion >= 0)
            {
                int resultado = listaVehiculosInvolucrados.FindIndex(x => x.NumPlaca == listaVehiculos[seleccion].NumPlaca);
                if(resultado == -1)
                {
                    listaVehiculosInvolucrados.Add(listaVehiculos[seleccion]);
                    tbl_VehiculosInvolucrados.ItemsSource = null;
                    tbl_VehiculosInvolucrados.ItemsSource = listaVehiculosInvolucrados;
                }
            }
        }

        private void btn_RemoverVehiculo_Click(object sender, RoutedEventArgs e)
        {
            int seleccion = tbl_VehiculosInvolucrados.SelectedIndex;
            if (seleccion >= 0)
            {
                listaVehiculosInvolucrados.RemoveAt(seleccion);
                tbl_VehiculosInvolucrados.ItemsSource = null;
                tbl_VehiculosInvolucrados.ItemsSource = listaVehiculosInvolucrados;
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

                reporteSiniestro.Calle = txt_Calle.Text;
                reporteSiniestro.Numero = txt_Numero.Text;
                reporteSiniestro.Colonia = txt_Colonia.Text;
                reporteSiniestro.FechaHora = new DateTime(dpc_fecha.SelectedDate.Value.Year, dpc_fecha.SelectedDate.Value.Month, dpc_fecha.SelectedDate.Value.Day, int.Parse(cmb_Hora.Text), int.Parse(cmb_Minuto.Text), 0);
                reporteSiniestro.FechaRegistro = DateTime.Now;
                reporteSiniestro.IdDelegacion = listaDelegaciones[cmb_delegacion.SelectedIndex].IdDelegacion;
                reporteSiniestro.Username = usuarioConectado.Username;
                reporteSiniestro.Dictamen = false;
                reporteSiniestro.IdReporte = ReporteSiniestroDAO.RegistrarReporte(reporteSiniestro);
                
                if (reporteSiniestro.IdReporte > 0)
                {
                    foreach (Vehiculo vehiculoInvolucrado in listaVehiculosInvolucrados)
                    {
                        VehiculosInvolucradosDAO.InsertarVehiculo(vehiculoInvolucrado.NumPlaca, reporteSiniestro.IdReporte);
                    }
                    listaImagenes.Clear();
                    foreach (ContenedorImagen imagenSeleccionada in pnl_Imagenes.Children)
                    {
                        listaImagenes.Add(imagenSeleccionada.RutaImagen);
                    }
                    FotografiaDAO.InsertarFotografias(listaImagenes, reporteSiniestro.IdReporte);

                    //notificacion
                    this.DialogResult = true;
                    this.Close();
                }
            }
        }

        private void btn_CancelarRegistro_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private bool ValidarFormulario()
        {
            bool esValido = true;

            if (txt_Colonia.Text.Length == 0 || txt_Calle.Text.Length == 0 || txt_Numero.Text.Length ==0 || !(cmb_delegacion.SelectedIndex >= 0))
            {
                MessageBox.Show("Debes llenar todos los campos");
                esValido = false;
            }
            else if (pnl_Imagenes.Children.Count < 3)//Validacion del minimo de imagenes
            {
                //notificacionç
                esValido = false;
            }
            else if (listaVehiculosInvolucrados.Count < 1)//no son suficientes vehiculos
            {
                //notificacion
                esValido = false;
            }

            return esValido;
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

        

        private void cmb_Hora_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }

        private void cmb_Minuto_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }


        private void btn_SeleccionImagenes_Click(object sender, RoutedEventArgs e)
        {
            openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Imagenes(*.jpg) | *.jpg";
            openFileDialog.Multiselect = true;
            if (pnl_Imagenes.Children.Count < 8)
            {
                openFileDialog.ShowDialog();
                int cantidadImagenes = openFileDialog.FileNames.Length;

                for (int indice = 0; indice < cantidadImagenes && pnl_Imagenes.Children.Count < 8; indice++)
                {
                    ContenedorImagen imagenSeleccionada = new ContenedorImagen(openFileDialog.FileNames[indice], pnl_Imagenes);
                    pnl_Imagenes.Children.Add(imagenSeleccionada);
                }
            }
        }

        

    }

    public class ContenedorImagen : StackPanel
    {
        private string rutaImagen;
        private Image imagen;
        private Button btn_Eliminar;
        private StackPanel contenedorPadre;

        public ContenedorImagen(string rutaImagen, StackPanel contenedorPadre)
        {
            this.rutaImagen = rutaImagen;
            this.contenedorPadre = contenedorPadre;
            CargarComponente();
        }

        public string RutaImagen { get => rutaImagen;}

        private void CargarComponente()
        {
            imagen = new Image();
            imagen.Source = new BitmapImage(new Uri(RutaImagen));
            imagen.Height = 180;
            

            btn_Eliminar = new Button();
            btn_Eliminar.Content = "Quitar";
            btn_Eliminar.HorizontalAlignment = HorizontalAlignment.Right;
            btn_Eliminar.Click += new RoutedEventHandler(EliminarImagen);

            this.Children.Add(btn_Eliminar);
            this.Children.Add(imagen);
        }

        private void EliminarImagen(object sender, RoutedEventArgs e)
        {
            contenedorPadre.Children.Remove(this);
        }
    }

}
