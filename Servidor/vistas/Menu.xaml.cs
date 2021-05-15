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
        

        public Menu()
        {
            InitializeComponent();
            

        }

        private void btn_ServicioLogin_Click(object sender, RoutedEventArgs e)
        {
            if(socketLogin != null)
            {
                Console.WriteLine(socketLogin.ConexionActiva());
            }

            if (socketLogin != null && socketLogin.ConexionActiva())
            {
                socketLogin.TerminarConexion();
                btn_ServicioLogin.Content = "Encender";
            }
            else
            {
                socketLogin = new SocketLogin();
                socketLogin.IniciarConexion();
                Thread procesoLogin = new Thread(new ThreadStart(socketLogin.RecibirMensaje));
                btn_ServicioLogin.Content = "Apagar";
                procesoLogin.Start();
            }
        }

        private void ApagarServicios(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(socketLogin != null && socketLogin.ConexionActiva())
            {
                socketLogin.TerminarConexion();
            }
        }
    }
}
