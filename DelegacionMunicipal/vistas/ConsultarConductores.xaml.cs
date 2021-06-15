using DelegacionMunicipal.interfaz;
using DelegacionMunicipal.modelo.dao;
using DelegacionMunicipal.modelo.poco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DelegacionMunicipal.vistas
{
    /// <summary>
    /// Lógica de interacción para ConsultarConductores.xaml
    /// </summary>
    public partial class ConsultarConductores : Page , ObserverRespuesta
    {
        List<Conductor> conductores;


        public ConsultarConductores()
        {
            InitializeComponent();
            conductores = new List<Conductor>();
            CargarTablaConductores();
        }

        private void btn_RegistrarConductor_Click(object sender, RoutedEventArgs e)
        {
            AbrirFormulario(true, null);
        }

        private void btn_EditarConductor_Click(object sender, RoutedEventArgs e)
        {
            int indice = tbl_Conductores.SelectedIndex;
            if(indice >= 0)
            {
                Conductor conductorEdicion = conductores[indice];
                AbrirFormulario(false, conductorEdicion);
            }
            else
            {
                ActualizaInformacion("Para editar un conductor debes seleccionarlo", "Sin selección");
            }
        }

        private void btn_EliminarConductor_Click(object sender, RoutedEventArgs e)
        {
            int indice = tbl_Conductores.SelectedIndex;
            if (indice >= 0)
            {
                Conductor conductorEliminar = conductores[indice];
                MessageBoxResult resultado = MessageBox.Show("¿Estás seguro de eliminar el conductor " + conductorEliminar.NombreCompleto + "?",
                    "Confirmar acción", MessageBoxButton.OKCancel, MessageBoxImage.Question);
                if(resultado == MessageBoxResult.OK)
                {
                    ConductorDAO.EliminarConductor(conductorEliminar.NumeroLicencia);
                    Console.WriteLine("BOTON OK");
                }
                else
                {
                    Console.WriteLine("BOTON CANCELAR");
                }
            }
            else
            {
                ActualizaInformacion("Para eliminar un conductor debes seleccionarlo", "Sin selección");
            }
            CargarTablaConductores();
        }

        private void CargarTablaConductores()
        {
            conductores = ConductorDAO.ConsultarConductores();
            tbl_Conductores.ItemsSource = conductores;
        }

        private void AbrirFormulario(bool nuevo, Conductor conductorEdicion)
        {
            FormConductor formularioNuevoConductor;

            if (nuevo)
            {
                formularioNuevoConductor = new FormConductor(this);
            }
            else
            {
                formularioNuevoConductor = new FormConductor(conductorEdicion, this);
            }
            formularioNuevoConductor.Owner = Window.GetWindow(this);
            bool? resultado = formularioNuevoConductor.ShowDialog();
            if (resultado == true)
            {
                CargarTablaConductores();
            }
        }

        public void ActualizaInformacion(string contenido, string titulo)
        {
            MessageBox.Show(contenido, titulo);
        }
    }
}
