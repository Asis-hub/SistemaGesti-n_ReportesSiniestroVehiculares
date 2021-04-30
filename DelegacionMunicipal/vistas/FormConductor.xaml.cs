using System.Windows;

namespace DelegacionMunicipal.vistas
{
    /// <summary>
    /// Lógica de interacción para FormConductor.xaml
    /// </summary>
    public partial class FormConductor : Window
    {
        public FormConductor()
        {
            InitializeComponent();
        }

        private void btn_RegistrarConductor_Click(object sender, RoutedEventArgs e)
        {
            //
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
