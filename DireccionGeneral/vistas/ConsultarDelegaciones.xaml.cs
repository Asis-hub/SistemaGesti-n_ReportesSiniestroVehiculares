using DireccionGeneral.modelo.dao;
using DireccionGeneral.modelo.poco;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace DireccionGeneral.vistas
{
    /// <summary>
    /// Lógica de interacción para ConsultarDelegaciones.xaml
    /// </summary>
    public partial class ConsultarDelegaciones : Page
    {
        List<Delegacion> delegaciones;

        public ConsultarDelegaciones()
        {
            InitializeComponent();
            delegaciones = new List<Delegacion>();
            CargarTabla();
        }

        private void btn_RegistrarDelegacion_Click(object sender, RoutedEventArgs e)
        {
            AbrirFormulario(true, null);
        }

        private void btn_EditarDelgación_Click(object sender, RoutedEventArgs e)
        {
            int seleccion = tbl_Delegaciones.SelectedIndex;
            if (seleccion >= 0)
            {
                AbrirFormulario(false, delegaciones[seleccion]);
            }
        }

        private void btn_EliminarDelgacion_Click(object sender, RoutedEventArgs e)
        {
            int seleccion = tbl_Delegaciones.SelectedIndex;
            if (seleccion >= 0)
            {
                int resultado = DelegacionDAO.EliminarDelegacion(delegaciones[seleccion].IdDelegacion);
                if (resultado == 1)
                {
                    CargarTabla();
                }
            }
        }

        public void CargarTabla()
        {
            delegaciones = DelegacionDAO.ConsultarDelegaciones();
            tbl_Delegaciones.ItemsSource = delegaciones;
        }

        private void AbrirFormulario(bool nuevo, Delegacion delegacion)
        {
            FormDelegacion formularioDelegacion;

            if (nuevo)
            {
                formularioDelegacion = new FormDelegacion();
            }
            else
            {
                formularioDelegacion = new FormDelegacion(delegacion);
            }
            formularioDelegacion.Owner = Window.GetWindow(this);
            bool? resultado = formularioDelegacion.ShowDialog();
            if (resultado == true)
            {
                CargarTabla();
            }
        }
    }
}
