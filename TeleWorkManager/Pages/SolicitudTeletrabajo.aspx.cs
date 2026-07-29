using System;
using ENT;
using BLL;

namespace TeleWorkManager.Pages
{
    public partial class SolicitudTeletrabajo : System.Web.UI.Page
    {
        private SolicitudTeletrabajoBLL bll = new SolicitudTeletrabajoBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Inicializar fechas por defecto con el día de hoy
                txtFechaInicio.Text = DateTime.Now.ToString("yyyy-MM-dd");
                txtFechaFin.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
        }

        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            try
            {
                SolicitudTeletrabajo solicitud = new SolicitudTeletrabajo
                {
                    // Por ahora hardcodeamos el IdEmpleado (ejemplo: 1) hasta tener la sesión/login activa
                    IdEmpleado = 1,
                    FechaInicio = Convert.ToDateTime(txtFechaInicio.Text),
                    FechaFin = Convert.ToDateTime(txtFechaFin.Text),
                    Motivo = txtMotivo.Text.Trim()
                };

                string mensajeError;
                bool exito = bll.RegistrarSolicitud(solicitud, out mensajeError);

                if (exito)
                {
                    lblMensaje.ForeColor = System.Drawing.Color.Green;
                    lblMensaje.Text = "¡Solicitud enviada con éxito!";
                    txtMotivo.Text = string.Empty;
                }
                else
                {
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                    lblMensaje.Text = mensajeError;
                }
            }
            catch (Exception ex)
            {
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                lblMensaje.Text = "Error: " + ex.Message;
            }
        }
    }
}