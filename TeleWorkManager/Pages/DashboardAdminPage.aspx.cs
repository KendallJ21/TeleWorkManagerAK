using BLL;
using ENT;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TeleWorkManager.Pages
{
    public partial class DashboardAdminPage : System.Web.UI.Page
    {
        private CDashboardAdminBLL cCDashboardAdminBLL = new CDashboardAdminBLL();
        private List<CSupervisorENT> SupervisorENT = new List<CSupervisorENT>();
        private List<CDepartamentoENT> DepartamentoENT = new List<CDepartamentoENT>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!Convert.ToBoolean(Session["LoginOK"]))
                {
                    Response.Redirect("~/Pages/LoginPage.aspx");
                }

                CargarEmpleados();
                CargarDepartamento();
                CargarSupervisor();
                TraerPolitica();
            }
        }

        protected void btnGuardarEmpleado_Click(object sender, EventArgs e)
        {

        }

        protected void btnBuscarEmpleado_Click(object sender, EventArgs e)
        {

        }

        protected void ddlDepartamentoPolitica_SelectedIndexChanged(object sender, EventArgs e)
        {
            TraerPolitica();
        }

        #region Métodos
        public void CargarEmpleados()
        {
            gvEmpleados.DataSource = cCDashboardAdminBLL.CargarEmpleados();
            gvEmpleados.DataBind();
        }

        public void CargarDepartamento()
        {
            DepartamentoENT = cCDashboardAdminBLL.CargarDepartamento();

            ddlDepartamentoPolitica.DataSource = DepartamentoENT;
            ddlDepartamentoPolitica.DataTextField = "Nombre";
            ddlDepartamentoPolitica.DataValueField = "DepartamentoID";
            ddlDepartamentoPolitica.DataBind();

            ddlDepartamentoEmpleado.DataSource = DepartamentoENT;
            ddlDepartamentoEmpleado.DataTextField = "Nombre";
            ddlDepartamentoEmpleado.DataValueField = "DepartamentoID";
            ddlDepartamentoEmpleado.DataBind();

            ddlDepartamentoSupervisor.DataSource = DepartamentoENT;
            ddlDepartamentoSupervisor.DataTextField = "Nombre";
            ddlDepartamentoSupervisor.DataValueField = "DepartamentoID";
            ddlDepartamentoSupervisor.DataBind();
        }

        public void CargarSupervisor()
        {
            SupervisorENT = cCDashboardAdminBLL.CargarSupervisor();

            ddlSupervisor.DataSource = SupervisorENT;
            ddlSupervisor.DataTextField = "Nombre";
            ddlSupervisor.DataValueField = "SupervisorID";
            ddlSupervisor.DataBind();

            ddlJefeEmpleado.DataSource = SupervisorENT;
            ddlJefeEmpleado.DataTextField = "Nombre";
            ddlJefeEmpleado.DataValueField = "SupervisorID";
            ddlJefeEmpleado.DataBind();
        }
       
        public void TraerPolitica()
        {
            List<CPoliticaTeletrabajoENT> datos = cCDashboardAdminBLL.TraerPolitica(Convert.ToInt32(ddlDepartamentoPolitica.SelectedValue));
            int PoliticaID; 

            if (datos.Count > 0)
            {
                CPoliticaTeletrabajoENT empleado = datos[0];

                PoliticaID = empleado.PoliticaID;
                txtMaxDiasSemana.Text = empleado.MaxDiasSemana.ToString();
                txtMaxDiasMes.Text = empleado.MaxDiasMes.ToString();
                txtHoraInicio.Text = empleado.HoraInicio.ToString(@"hh\:mm\:ss");
                txtHoraFin.Text = empleado.HoraFin.ToString(@"hh\:mm\:ss");

                List<int> Dias = cCDashboardAdminBLL.TraerDiasNoPermitidos(PoliticaID);

                chkLunes.Checked = Dias.Contains(1);
                chkMartes.Checked = Dias.Contains(2);
                chkMiercoles.Checked = Dias.Contains(3);
                chkJueves.Checked = Dias.Contains(4);
                chkViernes.Checked = Dias.Contains(5);
                chkSabado.Checked = Dias.Contains(6);
                chkDomingo.Checked = Dias.Contains(7);
            }
        }
        #endregion
    }
}