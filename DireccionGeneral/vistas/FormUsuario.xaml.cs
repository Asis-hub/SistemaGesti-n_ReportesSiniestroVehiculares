using System.Windows;

namespace DireccionGeneral.vistas
{
    /// <summary>
    /// Lógica de interacción para FormUsuario.xaml
    /// </summary>
    public partial class FormUsuario : Window
    {
        public FormUsuario()
        {
            InitializeComponent();
        }

        private void btn_RegistrarUsuario_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void btn_CancelarRegistro_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }
    }
}
