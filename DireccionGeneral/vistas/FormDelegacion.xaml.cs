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
        private List<Municipio> municipios;
        private List<DelegacionTipo> tiposDelegacion;
        private Delegacion delegacionEdicion;
        private bool esNuevo;

        public FormDelegacion()
        {
            InitializeComponent();
            esNuevo = true;
            municipios = new List<Municipio>();
            tiposDelegacion = new List<DelegacionTipo>();
            CargarCmb_Municipios();
            CargarCmb_TiposDelegacion();
        }

        public FormDelegacion(Delegacion delegacionEdicion) : this()
        {
            this.delegacionEdicion = delegacionEdicion;
            esNuevo = false;
            CargarDatosDelegacion();
        }

        private void CargarDatosDelegacion()
        {
            txt_Nombre.Text = delegacionEdicion.Nombre;
            txt_CodigoPostal.Text = delegacionEdicion.CodigoPostal;
            txt_Colonia.Text = delegacionEdicion.Colonia;
            txt_Calle.Text = delegacionEdicion.Calle;
            txt_Correo.Text = delegacionEdicion.Correo;
            txt_Numero.Text = delegacionEdicion.Numero;
            cmb_Municipio.SelectedIndex = municipios.FindIndex(x => x.IdMunicipio == delegacionEdicion.IdMunicipio);
            cmb_Tipo.SelectedIndex = tiposDelegacion.FindIndex(x => x.IdTipoDelegacion == delegacionEdicion.IdTipo);
        }

        private void btn_GuardarDelegacion_Click(object sender, RoutedEventArgs e)
        {
            if (ValidarFormulario())
            {
                Delegacion delegacion = new Delegacion();
                if (!esNuevo)
                {
                    delegacion.IdDelegacion = delegacionEdicion.IdDelegacion;
                }
                delegacion.Nombre = txt_Nombre.Text;
                delegacion.CodigoPostal =txt_CodigoPostal.Text;
                delegacion.Colonia = txt_Colonia.Text;
                delegacion.Calle = txt_Calle.Text;
                delegacion.Correo = txt_Correo.Text;
                delegacion.Numero = txt_Numero.Text;

                int indiceMunicipio = cmb_Municipio.SelectedIndex;
                delegacion.IdMunicipio = municipios[indiceMunicipio].IdMunicipio;

                int indiceTipo = cmb_Tipo.SelectedIndex;
                delegacion.IdTipo = tiposDelegacion[indiceTipo].IdTipoDelegacion;

                int resultado;

                if (esNuevo)
                {
                    resultado = DelegacionDAO.RegistrarDelegacion(delegacion);
                }
                else
                {
                    resultado = DelegacionDAO.EditarDelegacion(delegacion);
                }

                if (resultado == 1)
                {
                    this.DialogResult = true; ;
                    this.Close();
                }
            }
            
            
        }

        private void btn_CancelarRegistro_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void CargarCmb_Municipios()
        {
            municipios = MunicipioDAO.ConsultarMunicipios();
            cmb_Municipio.ItemsSource = municipios;
        }

        private void CargarCmb_TiposDelegacion()
        {
            tiposDelegacion = DelegacionTipoDAO.ConsultarTipos();
            cmb_Tipo.ItemsSource = tiposDelegacion;
        }

        private bool ValidarFormulario()
        {
            if (txt_Nombre.Text.Length == 0)
                return false;
            if (txt_CodigoPostal.Text.Length == 0)
                return false;
            if (txt_Colonia.Text.Length == 0)
                return false;
            if (txt_Calle.Text.Length == 0)
                return false;
            if (txt_Correo.Text.Length == 0)
                return false;
            if (txt_Numero.Text.Length == 0)
                return false;
            if (!(cmb_Municipio.SelectedIndex >= 0))
                return false;
            if (!(cmb_Tipo.SelectedIndex >= 0))
                return false;

            return true;
        }
    }
}
