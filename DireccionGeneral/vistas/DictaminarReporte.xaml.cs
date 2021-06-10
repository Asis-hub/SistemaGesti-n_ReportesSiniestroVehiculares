using DireccionGeneral.modelo.dao;
using DireccionGeneral.modelo.poco;
using System;
using System.Windows;

namespace DireccionGeneral.vistas
{
    /// <summary>
    /// Lógica de interacción para DictaminarReporte.xaml
    /// </summary>
    public partial class DictaminarReporte : Window
    {
        private Usuario usuario;
        private int idReporte;

        public DictaminarReporte()
        {
            InitializeComponent();
        }

        public DictaminarReporte(Usuario usuario, int idReporte) : this()
        {
            this.usuario = usuario;
            this.idReporte = idReporte;
        }

        private void btn_RegistrarDictamen_Click(object sender, RoutedEventArgs e)
        {
            if (ValidarFormulario())
            {
                Dictamen dictamen = new Dictamen();
                dictamen.Descripcion = txt_Descripcion.Text;
                dictamen.Folio = int.Parse(txt_Folio.Text);
                dictamen.FechaHora = DateTime.Now;
                dictamen.IdReporte = idReporte;
                dictamen.Username = usuario.Username;

                int resultado = DictamenDAO.RegistrarDictamen(dictamen);
                if (resultado == 1)
                {
                    this.DialogResult = true;
                    this.Close();
                }
            }
        }


        private void btn_CancelarRegistro_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private bool ValidarFormulario()
        {
            if (txt_Descripcion.Text.Length == 0)
                return false;
            if (txt_Descripcion.Text.Length > 250)
                return false;
            if (txt_Folio.Text.Length == 0)
                return false;

            try
            {
                int folio = int.Parse(txt_Folio.Text);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}

