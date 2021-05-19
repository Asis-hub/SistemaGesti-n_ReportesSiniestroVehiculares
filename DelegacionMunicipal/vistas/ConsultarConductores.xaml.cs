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
    public partial class ConsultarConductores : Page
    {
        List<Conductor> conductores;


        public ConsultarConductores()
        {
            InitializeComponent();
            CargarTablaConductores();
        }

        private void btn_RegistrarConductor_Click(object sender, RoutedEventArgs e)
        {
            FormConductor formConductor = new FormConductor();
            formConductor.ShowDialog();

        }

        private void btn_EditarConductor_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_EliminarConductor_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CargarTablaConductores()
        {
            conductores = ConductorDAO.ConsultarConductores();
            tbl_Conductores.ItemsSource = conductores;
        }
    }
}
