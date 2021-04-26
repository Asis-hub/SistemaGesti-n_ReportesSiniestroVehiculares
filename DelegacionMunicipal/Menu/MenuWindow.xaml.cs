using DelegacionMunicipal.Conductores;
using DelegacionMunicipal.Reportes;
using DelegacionMunicipal.SalaChat;
using DelegacionMunicipal.Vehiculos;
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
using System.Windows.Shapes;

namespace DelegacionMunicipal.Menu
{
    /// <summary>
    /// Lógica de interacción para MenuWindow.xaml
    /// </summary>
    public partial class MenuWindow : Window
    {
        private ConsultarConductores consultarConductores;
        private ConsultarVehiculos consultarVehiculos;
        private ConsultarReportes consultarReportes;
        private Chat salaChat;

        public MenuWindow()
        {
            InitializeComponent();
            consultarConductores = new ConsultarConductores();
            consultarVehiculos = new ConsultarVehiculos();
            consultarReportes = new ConsultarReportes();
            salaChat = new Chat();
            frame_Content.Content = consultarConductores;
        }

        private void btn_Conductores_Click(object sender, RoutedEventArgs e)
        {
            frame_Content.Content = consultarConductores;
        }

        private void btn_Vehiculos_Click(object sender, RoutedEventArgs e)
        {
            frame_Content.Content = consultarVehiculos;
        }

        private void btn_Reportes_Click(object sender, RoutedEventArgs e)
        {
            frame_Content.Content = consultarReportes;
        }

        private void btn_Chat_Click(object sender, RoutedEventArgs e)
        {
            frame_Content.Content = salaChat;
        }

        private void btn_CerrarSesion_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CerrarVentana(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MinimizarVentana(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
            {
                this.WindowState = WindowState.Minimized;
            }
            else
            {
                this.WindowState = WindowState.Normal;
            }
        }
    }
}
