using DireccionGeneral.Delegaiciones;
using DireccionGeneral.Reportes;
using DireccionGeneral.SalaChat;
using DireccionGeneral.Usuarios;
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

namespace DireccionGeneral.Menu
{
    /// <summary>
    /// Lógica de interacción para MenuWindow.xaml
    /// </summary>
    public partial class MenuWindow : Window
    {
        private ConsultarUsuarios ventanaConsultarUsuarios;
        private ConsultarDelegaciones ventanaConsultarDelegaciones;
        private ConsultarReportes ventanaConsultarReportes;
        private Chat ventanaSalaChat;

        public MenuWindow()
        {
            InitializeComponent();
            ventanaConsultarUsuarios = new ConsultarUsuarios();
            ventanaConsultarDelegaciones = new ConsultarDelegaciones();
            ventanaConsultarReportes = new ConsultarReportes();
            ventanaSalaChat = new Chat();

            frame_Content.Content = ventanaConsultarUsuarios;
        }

        private void btn_Usuarios_Click(object sender, RoutedEventArgs e)
        {
            frame_Content.Content = ventanaConsultarUsuarios;
        }

        private void btn_Delegaciones_Click(object sender, RoutedEventArgs e)
        {
            frame_Content.Content = ventanaConsultarDelegaciones;
        }

        private void btn_Reportes_Click(object sender, RoutedEventArgs e)
        {
            frame_Content.Content = ventanaConsultarReportes;
        }

        private void btn_Chat_Click(object sender, RoutedEventArgs e)
        {
            frame_Content.Content = ventanaSalaChat;
        }

        private void btn_CerrarSesion_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
