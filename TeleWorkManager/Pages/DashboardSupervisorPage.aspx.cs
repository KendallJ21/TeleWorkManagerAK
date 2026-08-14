using BLL;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TeleWorkManager.Pages
{
    public partial class DashboardSupervisorPage : System.Web.UI.Page
    {
        private CDashboardSupervisorBLL cCDashboardSupervisorBLL = new CDashboardSupervisorBLL();
        List<DateTime> fechasTeletrabajo = new List<DateTime>();
        int aprobadas, rechazadas, pendientes;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!Convert.ToBoolean(Session["LoginOK"]))
                {
                    Response.Redirect("~/Pages/LoginPage.aspx");
                }

                txt_dashboard.Text = "Dashboard - " + Session["Nombre"].ToString();

                ObtenerCantidadSolicitudesAprobadas();
                ObtenerCantidadSolicitudesRechazadas();
                ObtenerCantidadSolicitudesPendientes();
                ObtenerColaboradoresDepartamento();
                ObtenerCantidadColaboradoresTeletrabajo();
                ObtenerCantidadDiasProgramados();
                ObtenerCumplimiento();
                ObtenerBarrasProgreso();
                ObtenerDiasTeletrabajo(DateTime.Today);
            }           
        }

        protected void clrCalendario_DayRender(object sender, DayRenderEventArgs e)
        {

            if (fechasTeletrabajo.Contains(e.Day.Date))
            {
                e.Cell.BackColor = System.Drawing.Color.Green;
            }
        }

        protected void clrCalendario_VisibleMonthChanged(object sender, MonthChangedEventArgs e)
        {
            DateTime nuevoMes = e.NewDate;
            ObtenerDiasTeletrabajo(nuevoMes);
        }

        #region Métodos
        public void ObtenerCantidadSolicitudesAprobadas()
        {
            aprobadas = Convert.ToInt32(cCDashboardSupervisorBLL.ObtenerCantidadSolicitudesAprobadas(Convert.ToInt32(Session["DepartamentoID"])));
            lblAprobadas.Text = aprobadas.ToString();
            lblTotalAprobadas.Text = aprobadas.ToString();
        }

        public void ObtenerCantidadSolicitudesRechazadas()
        {
            rechazadas = Convert.ToInt32(cCDashboardSupervisorBLL.ObtenerCantidadSolicitudesRechazadas(Convert.ToInt32(Session["DepartamentoID"])));
            lblRechazadas.Text = rechazadas.ToString();
            lblTotalRechazadas.Text = rechazadas.ToString();
        }

        public void ObtenerCantidadSolicitudesPendientes()
        {
            pendientes = Convert.ToInt32(cCDashboardSupervisorBLL.ObtenerCantidadSolicitudesPendientes(Convert.ToInt32(Session["DepartamentoID"])));
            lblSolicitudesPendientes.Text = pendientes.ToString();
            lblPendientes.Text = pendientes.ToString();
        }

        public void ObtenerColaboradoresDepartamento()
        {
            lblTotalEmpleados.Text = cCDashboardSupervisorBLL.ObtenerColaboradoresDepartamento(Convert.ToInt32(Session["DepartamentoID"]));
        }

        public void ObtenerCantidadColaboradoresTeletrabajo()
        {
            lblEmpleadosRemotos.Text = cCDashboardSupervisorBLL.ObtenerCantidadColaboradoresTeletrabajo(Convert.ToInt32(Session["DepartamentoID"]));
        }

        public void ObtenerCantidadDiasProgramados()
        {
            lblTotalDias.Text = cCDashboardSupervisorBLL.ObtenerCantidadDiasProgramados(Convert.ToInt32(Session["DepartamentoID"]));
            lblDiasProgramados.Text = lblTotalDias.Text;
        }

        public void ObtenerCumplimiento()
        {
            lblCumplimiento.Text = cCDashboardSupervisorBLL.ObtenerCumplimiento(Convert.ToInt32(Session["DepartamentoID"]));
        }

        protected void btnVerEmpleados_Click(object sender, EventArgs e)
        {
            RptEmpleadosRemotos.DataSource = cCDashboardSupervisorBLL.ObtenerColaboradores(Convert.ToInt32(Session["DepartamentoID"]));
            RptEmpleadosRemotos.DataBind();

            string script = @"
                            var modal = new bootstrap.Modal(document.getElementById('ModalEmpleados'));
                            modal.show();";

            ScriptManager.RegisterStartupScript(
                this,
                this.GetType(),
                "MostrarModal",
                script,
                true);
        }

        public void ObtenerBarrasProgreso()
        {
            int total = aprobadas + pendientes + rechazadas;
            int porcentajeA = (aprobadas * 100) / total;
            int porcentajeR = (rechazadas * 100) / total;
            int porcentajeP = (pendientes * 100) / total;

            progressAprobadas.Style["width"] = porcentajeA + "%";
            progressRechazadas.Style["width"] = porcentajeR + "%";
            progressPendientes.Style["width"] = porcentajeP + "%";
        }
       
        public void ObtenerDiasTeletrabajo(DateTime mes)
        {
            fechasTeletrabajo = cCDashboardSupervisorBLL.ObtenerDiasTeletrabajo(Convert.ToInt32(Session["DepartamentoID"]), mes);
        }
        #endregion  
    }
}