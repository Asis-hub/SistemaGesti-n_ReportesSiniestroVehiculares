﻿using DelegacionMunicipal.modelo.dao;
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
            conductores = new List<Conductor>();
            CargarTablaConductores();
        }

        private void btn_RegistrarConductor_Click(object sender, RoutedEventArgs e)
        {
            AbrirFormulario(true);
        }

        private void btn_EditarConductor_Click(object sender, RoutedEventArgs e)
        {
            AbrirFormulario(false);
        }

        private void btn_EliminarConductor_Click(object sender, RoutedEventArgs e)
        {
            int indice = tbl_Conductores.SelectedIndex;
            Console.WriteLine(indice);
            if (indice >= 0)
            {
                int resultado = ConductorDAO.EliminarConductor(conductores[indice].NumeroLicencia);
                if (resultado == 1)
                {
                    CargarTablaConductores();
                }
            }
        }

        private void CargarTablaConductores()
        {
            conductores = ConductorDAO.ConsultarConductores();
            tbl_Conductores.ItemsSource = conductores;
        }

        private void AbrirFormulario(bool nuevo)
        {
            FormConductor formularioNuevoConductor;

            if (nuevo)
            {
                formularioNuevoConductor = new FormConductor();
            }
            else
            {
                int indice = tbl_Conductores.SelectedIndex;
                Conductor conductorEdicion = conductores[indice];
                formularioNuevoConductor = new FormConductor(conductorEdicion);
            }
            formularioNuevoConductor.Owner = Window.GetWindow(this);
            bool? resultado = formularioNuevoConductor.ShowDialog();
            if (resultado == true)
            {
                CargarTablaConductores();
            }
        }
    }
}
