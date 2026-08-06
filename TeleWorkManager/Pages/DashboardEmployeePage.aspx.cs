using BLL;
using ENT;
using System;
using System.Globalization;
using System.Web.UI;

namespace TeleWorkManager.Pages
{
    public partial class DashboardEmployeePage : System.Web.UI.Page
    {
        private CDashboardEmployeeBLL  cDashboardEmployeeBLL = new CDashboardEmployeeBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!Convert.ToBoolean(Session["LoginOK"]))
                {
                    Response.Redirect("~/Pages/LoginPage.aspx");
                }

                lblBienvenida.Text = "Dashboard - " + Session["Nombre"].ToString();

                ObtenerDatosDíasAsignados();
                ObtenerProximoDiaTeletrabajo();
                ObtenerCantidadSolicitudes();
                ObtenerNotificaciones();

                clrCalendario.SelectedDate = DateTime.Today;
                clrCalendario.VisibleDate = DateTime.Today;
            }   
        }

        protected void clrCalendario_SelectionChanged(object sender, EventArgs e)
        {
            txtFecha.Text = clrCalendario.SelectedDate.ToString("dd/MM/yyyy");
        }
       
        protected void btnSolicitar_Click(object sender, EventArgs e)
        {
            if (txtFecha.Text==string.Empty || txtMotivo.Text==string.Empty)
            {
                string script = @"
                                Swal.fire({
                                    title: 'Campos obligatorios',
                                    text: 'La fecha de teletrabajo y el motivo no pueden estar vacíos.',
                                    icon: 'warning',
                                    confirmButtonText: 'Aceptar'
                                });";

                ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
            }
            else {
                CSolicitudENT cSolicitudENT = new CSolicitudENT()
                {
                    EmpleadoID = Convert.ToInt32(Session["EmpleadoID"]),
                    FechaSolicitud = clrCalendario.SelectedDate.ToString("yyyy/M/d"),
                    Motivo = txtMotivo.Text,
                };

                if (cDashboardEmployeeBLL.CrearSolicitud(cSolicitudENT))
                {
                    ObtenerCantidadSolicitudes();
                    LimpiarCampos();

                    string script = $@"
                                    Swal.fire({{
                                        title: 'Solicitud Teletrabajo',
                                        text: 'Hemos procesado su solicitud de forma correcta y fue notificado a su encargado de departamento',
                                        icon: 'success',
                                        confirmButtonText: 'OK'
                                    }})";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
                }
                else
                {
                    string script = @"
                                Swal.fire({
                                    title: 'Error',
                                    text: 'No fue posible procesar la solicitud de teletrabajo. Inténtelo nuevamente o contacte al administrador.',
                                    icon: 'error',
                                    confirmButtonText: 'Aceptar'
                                });";

                    ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
                }
            }
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        protected void clrCalendario_DayRender(object sender, System.Web.UI.WebControls.DayRenderEventArgs e)
        {
            DateTime hoy = DateTime.Today;

            if (e.Day.Date < hoy)
            {
                e.Day.IsSelectable = false;
                e.Cell.ForeColor = System.Drawing.Color.Gray;
                e.Cell.BackColor = System.Drawing.Color.LightGray;
            }
        }

        protected void btnRptDiasTele_Click(object sender, EventArgs e)
        {
            RptDiasTele.DataSource = cDashboardEmployeeBLL.ObtenerDiasTeletrabajo(Convert.ToInt32(Session["EmpleadoID"]));
            RptDiasTele.DataBind();

            string script = @"
                            var modal = new bootstrap.Modal(document.getElementById('ModalRptDiasTele'));
                            modal.show();";

            ScriptManager.RegisterStartupScript(
                this,
                this.GetType(),
                "MostrarModal",
                script,
                true);
        }

        protected void btnRptSolicitudes_Click(object sender, EventArgs e)
        {
            RptSolicitudes.DataSource = cDashboardEmployeeBLL.ObtenerSolicitudesPendientes(Convert.ToInt32(Session["EmpleadoID"]));
            RptSolicitudes.DataBind();

            string script = @"
                            var modal = new bootstrap.Modal(document.getElementById('ModalSolicitudes'));
                            modal.show();";

            ScriptManager.RegisterStartupScript(
                this,
                this.GetType(),
                "MostrarModal",
                script,
                true);
        }
        
        #region Métodos
        public void ObtenerDatosDíasAsignados()
        {
            CultureInfo cultura = new CultureInfo("es-CR");

            string mes = DateTime.Now.ToString("MMMM", cultura);
            mes = char.ToUpper(mes[0], cultura) + mes.Substring(1);

            lblMesAnnoTeletrabajo.Text = $"{mes}-{DateTime.Now.Year}";
            lblDiasTeletrabajo.Text = cDashboardEmployeeBLL.ObtenerDiasFaltantesTeletrabajo(Convert.ToInt32(Session["EmpleadoID"])).ToString();
        }

        public void ObtenerProximoDiaTeletrabajo()
        {
            string dia = cDashboardEmployeeBLL.ObtenerProximoDiaTeletrabajo(Convert.ToInt32(Session["EmpleadoID"])).Split('-')[0];
            int mes = Convert.ToInt32( cDashboardEmployeeBLL.ObtenerProximoDiaTeletrabajo(Convert.ToInt32(Session["EmpleadoID"])).Split('-')[1]);
            string nombreMes = new DateTime(2026, mes, 1).ToString("MMMM", new CultureInfo("es-CR"));

            nombreMes = char.ToUpper(nombreMes[0]) + nombreMes.Substring(1);
            lblProximoDia.Text = dia;
            lblProximoMes.Text = nombreMes;
        }

        public void ObtenerCantidadSolicitudes()
        {
            lblCantidadSolicitudes.Text= cDashboardEmployeeBLL.ObtenerCantidadSolicitudes(Convert.ToInt32(Session["EmpleadoID"]));   
        }

        public void ObtenerNotificaciones()
        {
            lblCantidadNotificaciones.Text = cDashboardEmployeeBLL.ObtenerCantidadNotificaciones(Convert.ToInt32(Session["EmpleadoID"]));
        }

        public void LimpiarCampos()
        {
            txtMotivo.Text = String.Empty;
            txtFecha.Text = String.Empty;
            clrCalendario.SelectedDate = DateTime.Now;
        }
        #endregion
    }
}