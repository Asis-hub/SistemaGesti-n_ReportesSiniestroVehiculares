using DireccionGeneral.modelo.dao;
using DireccionGeneral.modelo.poco;
using System.Collections.Generic;
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
            CargarCmb_Municipios();
            CargarCmb_TiposDelegacion();
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

        private void CargarCmb_Municipios()
        {
            cmb_Municipio.Items.Add("Seleccionar municipio");
            cmb_Municipio.SelectedIndex = 0;
            List<Municipio> listaMunicipios = MunicipioDAO.ConsultarMunicipios();
            foreach (Municipio elemento in listaMunicipios)
            {
                cmb_Municipio.Items.Add(elemento);
            }
            
        }

        private void CargarCmb_TiposDelegacion()
        {
            cmb_Tipo.Items.Add("Seleccionar Tipo");
            cmb_Tipo.SelectedIndex = 0;
            List<DelegacionTipo> listaTipoDelegacion = DelegacionTipoDAO.ConsultarTipos();
            foreach (DelegacionTipo elemento in listaTipoDelegacion)
            {
                cmb_Tipo.Items.Add(elemento);
            }
            
        }

    }
}
