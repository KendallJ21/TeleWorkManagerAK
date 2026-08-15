using BLL;
using ENT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TeleWorkManager.Pages
{
    public partial class MonthlySchedulePage : System.Web.UI.Page
    {
        private CMonthlyScheduleBLL cCMonthlyScheduleBLL = new CMonthlyScheduleBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!Convert.ToBoolean(Session["LoginOK"]))
                {
                    Response.Redirect("~/Pages/LoginPage.aspx");
                }

                CargarAnios();

                ddlMes.SelectedValue = DateTime.Now.Month.ToString();
                ddlAnio.SelectedValue = DateTime.Now.Year.ToString();
                CalendarProgramacion.VisibleDate = DateTime.Now;

                CargarEmpleados();
                CargarRegla();
            }
        }

        protected void ddlMes_SelectedIndexChanged(object sender, EventArgs e)
        {
            int mes = Convert.ToInt32(ddlMes.SelectedValue);
            int anio = Convert.ToInt32(ddlAnio.SelectedValue);
            CalendarProgramacion.VisibleDate = new DateTime(anio, mes, 1);
        }

        protected void ddlAnio_SelectedIndexChanged(object sender, EventArgs e)
        {
            int mes = Convert.ToInt32(ddlMes.SelectedValue);
            int anio = Convert.ToInt32(ddlAnio.SelectedValue);
            CalendarProgramacion.VisibleDate = new DateTime(anio, mes, 1);
        }

        protected void btnCargar_Click(object sender, EventArgs e)
        {
            if (ddlEmpleado.SelectedValue == "0")
            {
                return;
            }

            int empleadoID = Convert.ToInt32(ddlEmpleado.SelectedValue);
            int mes = Convert.ToInt32(ddlMes.SelectedValue);
            int anio = Convert.ToInt32(ddlAnio.SelectedValue);

            CalendarProgramacion.VisibleDate = new DateTime(anio, mes, 1);

            CargarProgramacion(empleadoID, mes, anio);
        }

        protected void CalendarProgramacion_DayRender(object sender, DayRenderEventArgs e)
        {
            DateTime fecha = e.Day.Date;

            if (FechasSeleccionadas.Any(x => x.Date == fecha.Date))
            {
                e.Cell.BackColor = System.Drawing.Color.FromArgb(25, 135, 84);
                e.Cell.ForeColor = System.Drawing.Color.White;

                e.Cell.Controls.Add(
                    new LiteralControl(
                        "<br/>" +
                        "<span class='dia-teletrabajo'>" +
                        "<i class='bi bi-house-fill'></i> " +
                        "Teletrabajo" +
                        "</span>"
                    )
                );
            }
        }

        protected void CalendarProgramacion_SelectionChanged(object sender, EventArgs e)
        {
            DateTime fecha = CalendarProgramacion.SelectedDate.Date;

            if (fecha == DateTime.MinValue)
            {
                return;
            }

            if (!FechaPuedeModificar(fecha))
            {
                CalendarProgramacion.SelectedDates.Clear();

                MostrarMensaje(
                    "Fecha no modificable",
                    "No se puede modificar la programación de la semana actual ni de semanas anteriores.",
                    "warning"
                );

                return;
            }

            List<DateTime> fechas = FechasSeleccionadas;

            if (fechas.Any(x => x.Date == fecha.Date))
            {
                fechas.RemoveAll(
                    x => x.Date == fecha.Date
                );
            }
            else
            {
                if (!ValidarMaximoSemanal(fecha))
                {
                    CalendarProgramacion.SelectedDates.Clear();

                    MostrarMensaje(
                        "Límite alcanzado",
                        "El colaborador ya tiene el máximo de 3 días de teletrabajo para esta semana.",
                        "warning"
                    );

                    CalendarProgramacion.DataBind();

                    return;
                }

                fechas.Add(fecha);
            }


            FechasSeleccionadas = fechas;

            lblDiasSeleccionados.Text = FechasSeleccionadas.Count.ToString();

            CalendarProgramacion.SelectedDates.Clear();

            CalendarProgramacion.DataBind();
        }

        protected void btnPublicar_Click(object sender, EventArgs e)
        {
            int empleadoID = Convert.ToInt32(ddlEmpleado.SelectedValue);
            List<DateTime> originales = FechasOriginales;
            List<DateTime> actuales = FechasSeleccionadas;

            List<DateTime> eliminadas =
                originales
                    .Where(x =>
                        !actuales.Any(y =>
                            y.Date == x.Date))
                    .ToList();

            List<DateTime> nuevas =
                actuales
                    .Where(x =>
                        !originales.Any(y =>
                            y.Date == x.Date))
                    .ToList();

            foreach (DateTime fecha in eliminadas)
            {
                cCMonthlyScheduleBLL.EliminarProgramacion(empleadoID, fecha);
            }

            foreach (DateTime fecha in nuevas)
            {
                cCMonthlyScheduleBLL.AgregarProgramacion(empleadoID, fecha, Convert.ToInt32(Session["EmpleadoID"]));
            }

            FechasOriginales = new List<DateTime>(actuales);

            MostrarMensaje("Programación guardada", "Los cambios se guardaron correctamente.", "success");
        }

        #region Métodos
        private void CargarAnios()
        {
            ddlAnio.Items.Clear();

            int anioActual = DateTime.Now.Year;

            for (int i = anioActual - 1; i <= anioActual + 2; i++)
            {
                ddlAnio.Items.Add(
                    new ListItem(
                        i.ToString(),
                        i.ToString()
                    )
                );
            }
        }

        private void CargarEmpleados()
        {
            List<CEmployeeENT> empleados = cCMonthlyScheduleBLL.ObtenerColaboradores(Convert.ToInt32(Session["DepartamentoID"]));

            ddlEmpleado.Items.Clear();

            ddlEmpleado.Items.Add(new ListItem("Seleccione un colaborador", "0"));

            foreach (CEmployeeENT empleado in empleados)
            {
                ddlEmpleado.Items.Add(
                    new ListItem(
                        empleado.nombre + " " +
                        empleado.apellido1 + " " +
                        empleado.apellido2,

                        empleado.EmpleadoID.ToString()
                    )
                );
            }
        }

        private void CargarRegla()
        {
            string empleados = cCMonthlyScheduleBLL.CargarRegla(Convert.ToInt32(Session["DepartamentoID"]));
            Session["DiasMaximo"] = empleados;            
            lblMaxDiasSemana.Text = "Máximo permitido: " + empleados + " días por semana";
        }

        private void CargarProgramacion(int empleadoID, int mes, int anio)
        {
            List<DateTime> fechas = cCMonthlyScheduleBLL.ObtenerProgramacion(empleadoID, mes, anio);

            ViewState["FechasProgramadas"] = fechas; //.Select(x => x.ToString("yyyy-MM-dd")).ToList();

            FechasOriginales = new List<DateTime>(fechas);

            // Limpiar las selecciones nuevas
            FechasSeleccionadas = new List<DateTime>(fechas);

            lblDiasSeleccionados.Text = FechasSeleccionadas.Count.ToString();

            // Actualizar calendario
            CalendarProgramacion.DataBind();
        }

        private List<DateTime> FechasOriginales
        {
            get
            {
                if (ViewState["FechasOriginales"] == null)
                    return new List<DateTime>();

                return (List<DateTime>)
                    ViewState["FechasOriginales"];
            }
            set
            {
                ViewState["FechasOriginales"] = value;
            }
        }

        private List<DateTime> FechasSeleccionadas
        {
            get
            {
                if (ViewState["FechasSeleccionadas"] == null)
                    return new List<DateTime>();

                return (List<DateTime>)ViewState["FechasSeleccionadas"];
            }
            set
            {
                ViewState["FechasSeleccionadas"] = value;
            }
        }

        private bool ValidarMaximoSemanal(DateTime fecha)
        {
            DateTime lunes = ObtenerLunes(fecha);
            DateTime domingo = lunes.AddDays(6);

            int cantidad = 0;

            foreach (DateTime fechaSeleccionada in FechasSeleccionadas)
            {
                if (fechaSeleccionada.Date >= lunes.Date &&
                    fechaSeleccionada.Date <= domingo.Date)
                {
                    cantidad++;
                }
            }

            return cantidad < Convert.ToInt32(Session["DiasMaximo"]);
        }

        private DateTime ObtenerLunes(DateTime fecha)
        {
            int diferencia = (7 + (fecha.DayOfWeek - DayOfWeek.Monday)) % 7;

            return fecha.AddDays(-diferencia).Date;
        }

        private void MostrarMensaje(string titulo, string mensaje, string icono)
        {
            string script = $@"
                Swal.fire({{
                    title: '{titulo}',
                    text: '{mensaje}',
                    icon: '{icono}',
                    confirmButtonText: 'OK',
                    heightAuto: false,
                    scrollbarPadding: false
                }});
            ";

            ClientScript.RegisterStartupScript(
                this.GetType(),
                Guid.NewGuid().ToString(),
                script,
                true
            );
        }

        private bool FechaPuedeModificar(DateTime fecha)
        {
            DateTime hoy = DateTime.Today;

            DateTime lunesActual = ObtenerLunes(hoy);

            return fecha.Date >= lunesActual.AddDays(7).Date;
        }
        #endregion
    }
}