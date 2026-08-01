using System;
using System.Data;
using BLL;

namespace TeleWorkManager.Pages
{
    public partial class HistorialSolicitudes : System.Web.UI.Page
    {
        private SolicitudTeletrabajoBLL bll = new SolicitudTeletrabajoBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarAnios();
                CargarHistorial();
            }
        }

        private void CargarAnios()
        {
            int anioActual = DateTime.Now.Year;
            ddlAnio.Items.Add(new System.Web.UI.WebControls.ListItem("-- Todos --", "0"));
            for (int i = anioActual; i >= anioActual - 3; i--)
            {
                ddlAnio.Items.Add(new System.Web.UI.WebControls.ListItem(i.ToString(), i.ToString()));
            }
        }

        private void CargarHistorial()
        {
            int idEmpleado = 1; // ID temporal hasta integrar Login
            int? anio = ddlAnio.SelectedValue != "0" ? (int?)Convert.ToInt32(ddlAnio.SelectedValue) : null;
            int? mes = ddlMes.SelectedValue != "0" ? (int?)Convert.ToInt32(ddlMes.SelectedValue) : null;

            DataTable dt = bll.ConsultarHistorial(idEmpleado, anio, mes);
            gvHistorial.DataSource = dt;
            gvHistorial.DataBind();
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            CargarHistorial();
        }
    }
}