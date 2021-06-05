using Servidor.servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace Servidor.vistas
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class Menu : Window
    {
        SocketLogin socketLogin;
        SocketBD socketBD;

        public Menu()
        {
            InitializeComponent();
            socketLogin = new SocketLogin();
            socketBD = new SocketBD();
        }

        private void btn_ServicioLogin_Click(object sender, RoutedEventArgs e)
        {
            if (!socketLogin.Encendido)
            {
                socketLogin.IniciarConexion();
                btn_ServicioLogin.Content = "Apagar";
            }
            else
            {
                socketLogin.TerminarConexion();
                btn_ServicioLogin.Content = "Encender";
            }
        }

        private void btn_ServicioBaseDatos_Click(object sender, RoutedEventArgs e)
        {
            if (!socketBD.Encendido)
            {
                socketBD.IniciarConexion();
                btn_ServicioBaseDatos.Content = "Apagar";
            }
            else
            {
                socketBD.TerminarConexion();
                btn_ServicioBaseDatos.Content = "Encender";
            }
        }

        private void btn_ServicioSalaChat_Click(object sender, RoutedEventArgs e)
        {
            if (!SocketChat.Encendido)
            {
                SocketChat.IniciarConexion();
                btn_ServicioSalaChat.Content = "Apagar";
            }
            else
            {
                SocketChat.TerminarConexion();
                btn_ServicioSalaChat.Content = "Encender";
            }
        }

        private void ApagarServicios(object sender, System.ComponentModel.CancelEventArgs e)
        {
            socketLogin.TerminarConexion();
            socketBD.TerminarConexion();
            SocketChat.TerminarConexion();
        }  
    }
}