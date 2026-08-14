using BLL;
using ENT;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TeleWorkManager.Pages
{
    public partial class ProfilePage : System.Web.UI.Page
    {
        private CDashboardEmployeeBLL cDashboardEmployeeBLL = new CDashboardEmployeeBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!Convert.ToBoolean(Session["LoginOK"]))
                {
                    Response.Redirect("~/Pages/LoginPage.aspx");
                }

                TraerDatosPersonales();
            }
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            ActualizarDatosPersonales();
        }

        #region Métodos
        public void TraerDatosPersonales()
        {
            List<CEmployeeENT> datos = cDashboardEmployeeBLL.TraerDatosPersonales(Convert.ToInt32(Session["EmpleadoID"]));

            if (datos.Count > 0)
            {
                CEmployeeENT empleado = datos[0];

                lblIdentificacion.Text = empleado.identificacion;
                lblNombre.Text = empleado.nombre;
                Lbl_Apellido1.Text = empleado.apellido1;
                Lbl_Apellido2.Text = empleado.apellido2;
                lblCorreo.Text = empleado.correo;
                txt_telefono.Text = empleado.telefono.ToString();               
            }

            string supervisor = cDashboardEmployeeBLL.TraerSupervisor(Convert.ToInt32(Session["EmpleadoID"]));
            string departamento = cDashboardEmployeeBLL.TraerDepartamento(Convert.ToInt32(Session["EmpleadoID"]));

            lblJefe.Text = supervisor;
            lblDepartamento.Text = departamento;

        }

        public void ActualizarDatosPersonales()
        {
            if (cDashboardEmployeeBLL.ActualizarInformacion(Convert.ToInt32(Session["EmpleadoID"]), Convert.ToInt32(txt_telefono.Text)))
            {                
                string script = $@"
                                    Swal.fire({{
                                        title: 'Actualización Información Personal',
                                        text: 'Se ha actualizado su información personal de forma exitosa',
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
                                    text: 'No fue posible actualizar su información personal. Inténtelo nuevamente o contacte al administrador.',
                                    icon: 'error',
                                    confirmButtonText: 'Aceptar'
                                });";

                ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
            }
        }
        #endregion  
    }
}