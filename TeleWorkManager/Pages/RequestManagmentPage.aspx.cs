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

        protected void btnDashboard_Click(object sender, EventArgs e)
        {
            Response.Redirect("DashboardSupervisorPage.aspx");
        }

        protected void btnHistorial_Click(object sender, EventArgs e)
        {
            ddlEmpleado.SelectedIndex = 0;
            txtFecha.Text = "dd/mm/yyyy";
            ddlEstado.SelectedValue = "Pendiente";
            CargarSolicitudes();
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            CargarSolicitudes();
        }

        protected void gvSolicitudes_RowCommand(object sender, GridViewCommandEventArgs e) 
        { 
            if (e.CommandName == "Aprobar") 
            {
                AprobarSolicitud(Convert.ToInt32(e.CommandArgument));
            } 
            
            if (e.CommandName == "Rechazar") {
                Session["solicitudrechazo"] = Convert.ToInt32(e.CommandArgument);
                txtComentario.Text = "";

                string script = @" var modal = new bootstrap.Modal(document.getElementById('modalComentario')); modal.show();";

                ScriptManager.RegisterStartupScript(
                    this,
                    this.GetType(),
                    "MostrarModal",
                    script,
                    true);                  
            }
        }

        protected void gvSolicitudes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string estado = DataBinder.Eval(e.Row.DataItem, "Estado").ToString();

                LinkButton btnAprobar = (LinkButton)e.Row.FindControl("btnAprobar");
                LinkButton btnRechazar = (LinkButton)e.Row.FindControl("btnRechazar");

                if (estado != "Pendiente")
                {
                    btnAprobar.Visible = false;
                    btnRechazar.Visible = false;
                }
            }
        }

        protected void btnGuardarComentario_Click(object sender, EventArgs e)
        {             
            RechazarSolicitud(Convert.ToInt32(Session["solicitudrechazo"]));
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

            List<CRptSolicitudENT> response = cCRequestManagmentBLL.ObtenerSolicitudes(departamentoID, ddlEstado.SelectedValue, ddlEmpleado.SelectedValue, txtFecha.Text);

            gvSolicitudes.DataSource = response;
            gvSolicitudes.DataBind();

            lblCantidadSolicitudes.Text = gvSolicitudes.Rows.Count + " solicitudes";
        }
        
        private void AprobarSolicitud(int SolicitudID)
        {
            cCRequestManagmentBLL.AprobarSolicitud(Convert.ToInt32(Session["EmpleadoID"]), SolicitudID, txtComentario.Text);

            string empleado, correo, comentario, estado;
            DateTime fecha;

            List<CEnvioCorreoENT> datos = cCRequestManagmentBLL.ObtenerSolicitud(SolicitudID);

            CEnvioCorreoENT dato = datos[0];

            estado = "Aprobada";
            empleado = dato.Empleado;
            correo = dato.Correo;
            fecha = dato.Fecha;
            comentario = dato.Comentario;
          
            cCRequestManagmentBLL.CreateEmail(estado, empleado, correo, fecha, comentario);

            string script = @"Swal.fire({
                                    title: 'Aprobación Solicitud',
                                    text: 'Se ha aprobado la solicitud',
                                    icon: 'success',
                                    focusConfirm: false,
                                    heightAuto: false,
                                    scrollbarPadding: false,
                                    confirmButtonText: 'OK'
                                });";

            ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);

            CargarSolicitudes();
        }

        private void RechazarSolicitud(int SolicitudID)
        {
            cCRequestManagmentBLL.RechazarSolicitud(Convert.ToInt32(Session["EmpleadoID"]), SolicitudID, txtComentario.Text);

            string empleado, correo, comentario, estado;
            DateTime fecha;

            List<CEnvioCorreoENT> datos = cCRequestManagmentBLL.ObtenerSolicitud(SolicitudID);

            CEnvioCorreoENT dato = datos[0];

            estado = "Rechazada";
            empleado = dato.Empleado;
            correo = dato.Correo;
            fecha = dato.Fecha;
            comentario = dato.Comentario;

            cCRequestManagmentBLL.CreateEmail(estado, empleado, correo, fecha, comentario);

            string script = @"Swal.fire({
                                    title: 'Rechazo Solicitud',
                                    text: 'Se ha rechazado la solicitud',
                                    icon: 'success',
                                    focusConfirm: false,
                                    heightAuto: false,
                                    scrollbarPadding: false,
                                    confirmButtonText: 'OK'
                                });";

            ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);

            CargarSolicitudes();
        }
        #endregion
    }
}