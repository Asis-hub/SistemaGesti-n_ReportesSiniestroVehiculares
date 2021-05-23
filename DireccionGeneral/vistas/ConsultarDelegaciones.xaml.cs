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
            AbrirFormulario(true);
        }

        private void btn_EditarDelgación_Click(object sender, RoutedEventArgs e)
        {
            AbrirFormulario(false);
        }

        private void btn_EliminarDelgacion_Click(object sender, RoutedEventArgs e)
        {
            int indice = tbl_Delegaciones.SelectedIndex;
            if (indice >= 0)
            {
                int resultado = DelegacionDAO.EliminarDelegacion(delegaciones[indice].IdDelegacion);
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

        private void AbrirFormulario(bool nuevo)
        {
            FormDelegacion formularioNuevaDelegacion;

            if (nuevo)
            {
                formularioNuevaDelegacion = new FormDelegacion();
            }
            else
            {
                int indice = tbl_Delegaciones.SelectedIndex;
                Delegacion delegacionEdicion = delegaciones[indice];
                formularioNuevaDelegacion = new FormDelegacion(delegacionEdicion);
            }
            formularioNuevaDelegacion.Owner = Window.GetWindow(this);
            bool? resultado = formularioNuevaDelegacion.ShowDialog();
            if (resultado == true)
            {
                CargarTabla();
            }
        }
    }
}
