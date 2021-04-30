using System.Windows;

namespace DireccionGeneral.vistas
{
    /// <summary>
    /// Lógica de interacción para FormDelegacion.xaml
    /// </summary>
    public partial class FormDelegacion : Window
    {
        public FormDelegacion()
        {
            InitializeComponent();
        }

        private void btn_RegistrarDelegacion_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void btn_CancelarRegistro_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

    }
}
