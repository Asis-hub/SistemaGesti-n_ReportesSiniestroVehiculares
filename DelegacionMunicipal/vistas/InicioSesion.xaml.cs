using DelegacionMunicipal.conexion;
using DelegacionMunicipal.modelo.dao;
using DelegacionMunicipal.modelo.poco;
using System.Collections.ObjectModel;
using System;
using System.Windows;
using System.Collections.Generic;

namespace DelegacionMunicipal.vistas
{
    /// <summary>
    /// Lógica de interacción para InicioSesion.xaml
    /// </summary>
    public partial class InicioSesion : Window
    {
        public InicioSesion()
        {
            InitializeComponent();        
            CargarCmbDelegacion();
        }

        private void CargarCmbDelegacion()
        {
            cmb_Delegacion.Items.Clear();
            cmb_Delegacion.Items.Add("Delegación");
            List<Delegacion> listaDelegaciones = DelegacionDAO.GetDelegacionesLogin();
            foreach (Delegacion elemento in listaDelegaciones)
            {
                cmb_Delegacion.Items.Add(elemento);
            }
            cmb_Delegacion.SelectedIndex = 0;
        }

        private void btn_IniciarSesion_Click(object sender, RoutedEventArgs e)
        {
            string username = txt_Usuario.Text;
            string password = txt_Contrasenia.Password;

            if(username.Length > 0 && password.Length > 0 && cmb_Delegacion.SelectedIndex > 0)
            {
                Usuario usuarioConectado = null;
                int idDelegacion = ((Delegacion)cmb_Delegacion.SelectedItem).IdDelegacion;
                usuarioConectado = UsuarioDAO.getInicioSesion(username, password, idDelegacion);

                if (usuarioConectado != null)
                {
                    MenuPrincipal menuWindow = new MenuPrincipal(usuarioConectado);
                    menuWindow.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Credenciales no válidas", "Error");
                }
            }
            else
            {
                MessageBox.Show("Ingresa tus credenciales", "Campos vacios");
            } 
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
