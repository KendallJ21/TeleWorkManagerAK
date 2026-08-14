using BLL;
using ENT;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TeleWorkManager.Pages
{
    public partial class RequestManagmentPage : System.Web.UI.Page
    {
        private CRequestManagmentBLL cCRequestManagmentBLL = new CRequestManagmentBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!Convert.ToBoolean(Session["LoginOK"]))
                {
                    Response.Redirect("~/Pages/LoginPage.aspx");
                }

                CargarEmpleados();
                ddlEstado.SelectedValue = "Pendiente";
                CargarSolicitudes();
            }
        }




        protected void btnFiltrar_Click(object sender, EventArgs e)
        { // Filtrar solicitudes
        }

        protected void btnDashboard_Click(object sender, EventArgs e)
        {
            Response.Redirect("DashboardSupervisorPage.aspx");
        }

        protected void btnHistorial_Click(object sender, EventArgs e) 
        { // Abrir página de historial
        } 

        protected void gvSolicitudes_RowCommand(object sender, GridViewCommandEventArgs e) 
        { 
            if (e.CommandName == "Aprobar") 
            { 
                int solicitudID = Convert.ToInt32(e.CommandArgument); // Aprobar solicitud
             } 
            
            if (e.CommandName == "Rechazar") { 
                int solicitudID = Convert.ToInt32(e.CommandArgument); // Rechazar solicitud
            } 
            
            if (e.CommandName == "Comentario") { 
                int solicitudID = Convert.ToInt32(e.CommandArgument); // Mostrar modal
            } 
        }
        
        protected void btnGuardarComentario_Click(object sender, EventArgs e)
        { 
            string comentario = txtComentario.Text; // Guardar comentario
        }

        #region Métodos
        private void CargarEmpleados()
        {
            ddlEmpleado.DataSource = cCRequestManagmentBLL.ObtenerColaboradores(Convert.ToInt32(Session["DepartamentoID"]));

            ddlEmpleado.DataTextField = "Colaborador";
            ddlEmpleado.DataValueField = "EmpleadoID";

            ddlEmpleado.DataBind();

            ddlEmpleado.Items.Insert(0,
                new ListItem("Todos los empleados", ""));
        }

        private void CargarSolicitudes()
        {
            int departamentoID = Convert.ToInt32(Session["DepartamentoID"]);

            List<CRptSolicitud> response = cCRequestManagmentBLL.ObtenerSolicitudes(departamentoID, ddlEmpleado.SelectedValue, txtFecha.Text, ddlEstado.SelectedValue);

            gvSolicitudes.DataSource = response;
            gvSolicitudes.DataBind();

            lblCantidadSolicitudes.Text = gvSolicitudes.Rows.Count + " solicitudes";
        }
        #endregion
    }
}