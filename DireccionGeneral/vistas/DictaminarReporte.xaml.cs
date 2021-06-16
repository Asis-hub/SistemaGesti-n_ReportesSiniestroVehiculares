using DireccionGeneral.interfaz;
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
        private ObserverRespuesta notificacion;

        public DictaminarReporte(ObserverRespuesta notificacion)
        {
            InitializeComponent();
            this.notificacion = notificacion;
        }

        public DictaminarReporte(Usuario usuario, int idReporte, ObserverRespuesta notificacion) : this(notificacion)
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
                
                dictamen.FechaHora = DateTime.Now;
                dictamen.IdReporte = idReporte;
                dictamen.Username = usuario.Username;

                int resultado = DictamenDAO.RegistrarDictamen(dictamen);
                
                if (resultado == 1)
                {
                    notificacion.ActualizaInformacion("Dictamen registrado", "Dictamen");
                    this.DialogResult = true;
                    this.Close();
                }
                else if (resultado == -1)
                {
                    notificacion.ActualizaInformacion("Error al registrar el dictamen. Verifica el folio", "Error dictamen");
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
            

            return true;
        }
    }
}

