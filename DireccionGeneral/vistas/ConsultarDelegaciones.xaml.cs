using DireccionGeneral.interfaz;
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
    public partial class ConsultarDelegaciones : Page , ObserverRespuesta
    {
        List<Delegacion> delegaciones;

        public ConsultarDelegaciones()
        {
            InitializeComponent();
            delegaciones = new List<Delegacion>();
            CargarTablaDelegaciones();
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
            else
            {
                ActualizaInformacion("Para editar una delegación debes seleccionarlo", "Sin selección");
            }
        }

        private void btn_EliminarDelgacion_Click(object sender, RoutedEventArgs e)
        {
            int indice = tbl_Delegaciones.SelectedIndex;
            if (indice >= 0)
            {
                Delegacion delegacionEliminar = delegaciones[indice];
                MessageBoxResult resultado = MessageBox.Show("¿Estás seguro de eliminar la delegación " + delegacionEliminar.Nombre + "del municipio " + delegacionEliminar.Municipio +"?",
                    "Confirmar acción", MessageBoxButton.OKCancel, MessageBoxImage.Question);
                if (resultado == MessageBoxResult.OK)
                {
                    DelegacionDAO.EliminarDelegacion(delegacionEliminar.IdDelegacion);
                    Console.WriteLine("BOTON OK");
                }
                else
                {
                    Console.WriteLine("BOTON CANCELAR");
                }
            }
            else
            {
                ActualizaInformacion("Para eliminar una delegación debes seleccionarlo", "Sin selección");
            }
            CargarTablaDelegaciones();
        }

        public void CargarTablaDelegaciones()
        {
            delegaciones = DelegacionDAO.ConsultarDelegaciones();
            tbl_Delegaciones.ItemsSource = delegaciones;
        }

        private void AbrirFormulario(bool nuevo, Delegacion delegacion)
        {
            FormDelegacion formularioDelegacion;

            if (nuevo)
            {
                formularioDelegacion = new FormDelegacion(this);
            }
            else
            {
                formularioDelegacion = new FormDelegacion(delegacion, this);
            }
            formularioDelegacion.Owner = Window.GetWindow(this);
            bool? resultado = formularioDelegacion.ShowDialog();
            if (resultado == true)
            {
                CargarTablaDelegaciones();
            }
        }
        public void ActualizaInformacion(string contenido, string titulo)
        {
            MessageBox.Show(contenido, titulo);
        }
    }
}
