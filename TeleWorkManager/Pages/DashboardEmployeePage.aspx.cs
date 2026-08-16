using BLL;
using ENT;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TeleWorkManager.Pages
{
    public partial class DashboardEmployeePage : System.Web.UI.Page
    {
        private CDashboardEmployeeBLL  cDashboardEmployeeBLL = new CDashboardEmployeeBLL();
        private List<DateTime> fechasTeletrabajo = new List<DateTime>();

        protected void Page_Load(object sender, EventArgs e)
        {
            ObtenerDiasTeletrabajo(DateTime.Today);
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
                int mesActual = DateTime.Today.Month;
                int mesAnnio = DateTime.Today.Year;
                ddlMesHistorial.SelectedValue = mesActual.ToString();
                ddlAnioHistorial.SelectedValue = mesActual.ToString();
                ObtenerHistorialSolicitudes();
            }   
        }
        
        protected void btnFiltrarHistorial_Click(object sender, EventArgs e)
        {
            ObtenerHistorialSolicitudes();
        }

        protected void clrCalendario_VisibleMonthChanged(object sender, System.Web.UI.WebControls.MonthChangedEventArgs e)
        {
            ObtenerDiasTeletrabajo(DateTime.Today);
        }

        protected void clrCalendario_SelectionChanged(object sender, EventArgs e)
        {
            ObtenerDiasTeletrabajo(DateTime.Today);
            txtFecha.Text = clrCalendario.SelectedDate.ToString("dd/MM/yyyy");
        }
       
        protected void btnSolicitar_Click(object sender, EventArgs e)
        {
            if (txtFecha.Text==string.Empty || txtMotivo.Text==string.Empty)
            {
                ObtenerDiasTeletrabajo(DateTime.Today); 
                string script = @"
                                Swal.fire({
                                    title: 'Campos obligatorios',
                                    text: 'La fecha de teletrabajo y el motivo no pueden estar vacíos.',
                                    icon: 'warning',
                                    confirmButtonText: 'Aceptar'
                                });";

                ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
                return;
            }
            else {
                CSolicitudENT cSolicitudENT = new CSolicitudENT()
                {
                    EmpleadoID = Convert.ToInt32(Session["EmpleadoID"]),
                    FechaSolicitud = clrCalendario.SelectedDate.ToString("yyyy/M/d"),
                    Motivo = txtMotivo.Text,
                };
                if (cDashboardEmployeeBLL.ValidarSolicitarDia(cSolicitudENT)!="OK")
                {
                    string script = @"
                                Swal.fire({
                                    title: 'Validación Solicitud',
                                    text: 'Ya tienes una solicitud pendiente para ese día',
                                    icon: 'warning',
                                    confirmButtonText: 'Aceptar'
                                });";

                    ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
                    return;
                }

                if (cDashboardEmployeeBLL.CrearSolicitud(cSolicitudENT))
                {
                    ObtenerDiasTeletrabajo(DateTime.Today);
                    ObtenerCantidadSolicitudes();
                    LimpiarCampos();
                    cDashboardEmployeeBLL.CreateEmailSolicitud(Session["Nombre"].ToString(), Session["CorreoSupervisor"].ToString(),Convert.ToDateTime(cSolicitudENT.FechaSolicitud),cSolicitudENT.Motivo);
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
            ObtenerDiasTeletrabajo(DateTime.Today);
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
            if (fechasTeletrabajo.Contains(e.Day.Date))
            {
                e.Cell.BackColor = System.Drawing.Color.Green;
            }
        }

        protected void btnRptDiasTele_Click(object sender, EventArgs e)
        {
            RptDiasTele.DataSource = cDashboardEmployeeBLL.ObtenerDiasTeletrabajo(Convert.ToInt32(Session["EmpleadoID"]));
            RptDiasTele.DataBind();
            ObtenerDiasTeletrabajo(DateTime.Today);
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
            ObtenerDiasTeletrabajo(DateTime.Today);
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

        protected void btnNotificaciones_Click(object sender, EventArgs e)
        {
            RptNotificaciones.DataSource = cDashboardEmployeeBLL.ObtenerNotificaciones(Convert.ToInt32(Session["EmpleadoID"]));
            RptNotificaciones.DataBind();
            ObtenerDiasTeletrabajo(DateTime.Today);
            lblCantidadNotificaciones.Text= cDashboardEmployeeBLL.ObtenerCantidadNotificaciones(Convert.ToInt32(Session["EmpleadoID"]));

            string script = @"
                            var modal = new bootstrap.Modal(document.getElementById('ModalNotificaciones'));
                            modal.show();";
            
            ScriptManager.RegisterStartupScript(
                this,
                this.GetType(),
                "MostrarModal",
                script,
                true);
        }
        
        protected void btnReportePDF_Click(object sender, EventArgs e)
        {
            GenerarPDF();
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
        
        public void ObtenerDiasTeletrabajo(DateTime mes)
        {
            fechasTeletrabajo = cDashboardEmployeeBLL.ObtenerDiasTeletrabajo(Convert.ToInt32(Session["DepartamentoID"]), mes);
        }

        public void GenerarPDF()
        {
            List<CRptDiasTeletrabajoENT> lista= cDashboardEmployeeBLL.ObtenerDiasTeletrabajo(Convert.ToInt32(Session["EmpleadoID"]));
            using (MemoryStream ms = new MemoryStream())
            {
                Document documento = new Document(
                    PageSize.A4,
                    40,
                    40,
                    60,
                    50
                );

                PdfWriter writer = PdfWriter.GetInstance(documento, ms);

                documento.Open();

                // =====================================================
                // COLORES
                // =====================================================

                BaseColor colorPrincipal = new BaseColor(47, 62, 70);
                BaseColor colorSecundario = new BaseColor(84, 110, 122);
                BaseColor colorClaro = new BaseColor(245, 247, 248);
                BaseColor colorBlanco = BaseColor.WHITE;
                BaseColor colorTexto = new BaseColor(50, 50, 50);


                // =====================================================
                // FUENTES
                // =====================================================

                Font fuenteTitulo = FontFactory.GetFont(
                    FontFactory.HELVETICA_BOLD,
                    20,
                    colorPrincipal
                );

                Font fuenteSubtitulo = FontFactory.GetFont(
                    FontFactory.HELVETICA,
                    10,
                    colorSecundario
                );

                Font fuenteNormal = FontFactory.GetFont(
                    FontFactory.HELVETICA,
                    10,
                    colorTexto
                );

                Font fuenteNegrita = FontFactory.GetFont(
                    FontFactory.HELVETICA_BOLD,
                    10,
                    colorTexto
                );

                Font fuenteTabla = FontFactory.GetFont(
                    FontFactory.HELVETICA,
                    9,
                    colorTexto
                );

                Font fuenteTablaHeader = FontFactory.GetFont(
                    FontFactory.HELVETICA_BOLD,
                    9,
                    colorBlanco
                );


                // =====================================================
                // ENCABEZADO
                // =====================================================

                PdfPTable encabezado = new PdfPTable(2);

                encabezado.WidthPercentage = 100;

                encabezado.SetWidths(new float[]
                {
            70,
            30
                });


                PdfPCell celdaTitulo = new PdfPCell();

                celdaTitulo.Border = Rectangle.NO_BORDER;
                celdaTitulo.Padding = 0;

                Paragraph nombreSistema = new Paragraph(
                    "TeleWork Manager",
                    fuenteTitulo
                );

                Paragraph subtitulo = new Paragraph(
                    "Sistema de Gestión de Teletrabajo",
                    fuenteSubtitulo
                );

                celdaTitulo.AddElement(nombreSistema);
                celdaTitulo.AddElement(subtitulo);


                PdfPCell celdaFecha = new PdfPCell();

                celdaFecha.Border = Rectangle.NO_BORDER;
                celdaFecha.HorizontalAlignment = Element.ALIGN_RIGHT;

                Paragraph fecha = new Paragraph(
                    "FECHA DE GENERACIÓN",
                    FontFactory.GetFont(
                        FontFactory.HELVETICA_BOLD,
                        7,
                        colorSecundario
                    )
                );

                fecha.Alignment = Element.ALIGN_RIGHT;

                Paragraph fechaValor = new Paragraph(
                    DateTime.Now.ToString("dd/MM/yyyy HH:mm"),
                    fuenteNormal
                );

                fechaValor.Alignment = Element.ALIGN_RIGHT;

                celdaFecha.AddElement(fecha);
                celdaFecha.AddElement(fechaValor);


                encabezado.AddCell(celdaTitulo);
                encabezado.AddCell(celdaFecha);

                documento.Add(encabezado);


                // Línea separadora

                PdfPTable linea = new PdfPTable(1);

                linea.WidthPercentage = 100;

                PdfPCell celdaLinea = new PdfPCell();

                celdaLinea.BackgroundColor = colorPrincipal;

                celdaLinea.FixedHeight = 3;

                celdaLinea.Border = Rectangle.NO_BORDER;

                linea.AddCell(celdaLinea);

                documento.Add(linea);

                documento.Add(new Paragraph("\n"));


                // =====================================================
                // TÍTULO DEL REPORTE
                // =====================================================

                Paragraph tituloReporte = new Paragraph(
                    "Reporte de Días de Teletrabajo",
                    FontFactory.GetFont(
                        FontFactory.HELVETICA_BOLD,
                        16,
                        colorTexto
                    )
                );

                tituloReporte.SpacingAfter = 5;

                documento.Add(tituloReporte);


                Paragraph descripcion = new Paragraph(
                    "Detalle de los días programados para modalidad de teletrabajo.",
                    fuenteSubtitulo
                );

                descripcion.SpacingAfter = 15;

                documento.Add(descripcion);


                // =====================================================
                // RESUMEN
                // =====================================================

                PdfPTable resumen = new PdfPTable(2);

                resumen.WidthPercentage = 100;

                resumen.SetWidths(new float[]
                {
            50,
            50
                });


                PdfPCell totalDias = new PdfPCell();

                totalDias.BackgroundColor = colorClaro;
                totalDias.BorderColor = new BaseColor(225, 225, 225);

                totalDias.Padding = 12;

                totalDias.AddElement(
                    new Paragraph(
                        "DÍAS PROGRAMADOS",
                        FontFactory.GetFont(
                            FontFactory.HELVETICA_BOLD,
                            8,
                            colorSecundario
                        )
                    )
                );

                totalDias.AddElement(
                    new Paragraph(
                        lista.Count.ToString(),
                        FontFactory.GetFont(
                            FontFactory.HELVETICA_BOLD,
                            20,
                            colorPrincipal
                        )
                    )
                );


                PdfPCell periodo = new PdfPCell();

                periodo.BackgroundColor = colorClaro;
                periodo.BorderColor = new BaseColor(225, 225, 225);

                periodo.Padding = 12;

                periodo.AddElement(
                    new Paragraph(
                        "ESTADO DEL REPORTE",
                        FontFactory.GetFont(
                            FontFactory.HELVETICA_BOLD,
                            8,
                            colorSecundario
                        )
                    )
                );

                periodo.AddElement(
                    new Paragraph(
                        "Programación registrada",
                        fuenteNegrita
                    )
                );


                resumen.AddCell(totalDias);
                resumen.AddCell(periodo);

                documento.Add(resumen);

                documento.Add(new Paragraph("\n"));


                // =====================================================
                // TABLA PRINCIPAL
                // =====================================================

                PdfPTable tabla = new PdfPTable(3);

                tabla.WidthPercentage = 100;

                tabla.SetWidths(new float[]
                {
            10,
            30,
            60
                });


                // Encabezados

                AgregarCeldaHeader(
                    tabla,
                    "#",
                    fuenteTablaHeader,
                    colorPrincipal
                );

                AgregarCeldaHeader(
                    tabla,
                    "FECHA",
                    fuenteTablaHeader,
                    colorPrincipal
                );

                AgregarCeldaHeader(
                    tabla,
                    "OBSERVACIÓN",
                    fuenteTablaHeader,
                    colorPrincipal
                );


                // Datos

                int contador = 1;

                foreach (CRptDiasTeletrabajoENT item in lista)
                {
                    BaseColor fondo = contador % 2 == 0
                        ? colorClaro
                        : colorBlanco;


                    AgregarCelda(
                        tabla,
                        contador.ToString(),
                        fuenteTabla,
                        fondo,
                        Element.ALIGN_CENTER
                    );


                    string fechaFormateada = item.FechaTeletrabajo;

                    DateTime fechaConvertida;

                    if (DateTime.TryParse(
                        item.FechaTeletrabajo,
                        out fechaConvertida))
                    {
                        fechaFormateada =
                            fechaConvertida.ToString(
                                "dddd dd/MM/yyyy"
                            );
                    }


                    AgregarCelda(
                        tabla,
                        fechaFormateada,
                        fuenteTabla,
                        fondo,
                        Element.ALIGN_LEFT
                    );


                    AgregarCelda(
                        tabla,
                        string.IsNullOrWhiteSpace(item.Observacion)
                            ? "Sin observaciones"
                            : item.Observacion,
                        fuenteTabla,
                        fondo,
                        Element.ALIGN_LEFT
                    );


                    contador++;
                }


                documento.Add(tabla);


                // =====================================================
                // PIE DEL REPORTE
                // =====================================================

                documento.Add(new Paragraph("\n"));

                Paragraph informacion = new Paragraph(
                    "Este documento fue generado automáticamente por TeleWork Manager.",
                    FontFactory.GetFont(
                        FontFactory.HELVETICA_OBLIQUE,
                        8,
                        colorSecundario
                    )
                );

                informacion.Alignment = Element.ALIGN_CENTER;

                documento.Add(informacion);


                documento.Close();


                // =====================================================
                // DESCARGA
                // =====================================================

                HttpContext.Current.Response.Clear();

                HttpContext.Current.Response.ContentType =
                    "application/pdf";

                HttpContext.Current.Response.AddHeader(
                    "Content-Disposition",
                    "attachment;filename=Reporte_Teletrabajo.pdf"
                );

                HttpContext.Current.Response.OutputStream.Write(
                    ms.ToArray(),
                    0,
                    ms.ToArray().Length
                );

                HttpContext.Current.Response.Flush();

                HttpContext.Current.Response.End();
            }
        }
        
        private void AgregarCeldaHeader(PdfPTable tabla, string texto, Font fuente, BaseColor color)
        {
            PdfPCell celda = new PdfPCell(
                new Phrase(texto, fuente)
            );

            celda.BackgroundColor = color;
            celda.HorizontalAlignment = Element.ALIGN_CENTER;
            celda.VerticalAlignment = Element.ALIGN_MIDDLE;

            celda.PaddingTop = 8;
            celda.PaddingBottom = 8;

            celda.BorderColor = color;

            tabla.AddCell(celda);
        }
        
        private void AgregarCelda(PdfPTable tabla, string texto, Font fuente, BaseColor fondo, int alineacion)
        {
            PdfPCell celda = new PdfPCell(
                new Phrase(texto, fuente)
            );

            celda.BackgroundColor = fondo;

            celda.HorizontalAlignment = alineacion;
            celda.VerticalAlignment = Element.ALIGN_MIDDLE;

            celda.PaddingTop = 7;
            celda.PaddingBottom = 7;
            celda.PaddingLeft = 6;
            celda.PaddingRight = 6;

            celda.BorderColor = new BaseColor(225, 225, 225);

            tabla.AddCell(celda);
        }

        public void ObtenerHistorialSolicitudes()
        {
            dgv_historial.DataSource = cDashboardEmployeeBLL.obtenerHistorial(Convert.ToInt32(Session["EmpleadoID"]), Convert.ToInt32(ddlAnioHistorial.SelectedValue),Convert.ToInt32( ddlMesHistorial.SelectedValue));
            dgv_historial.DataBind();
            lblTotalSolicitudes.Text=dgv_historial.Rows.Count.ToString();
        }
        #endregion

        protected void dgv_historial_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string estado = DataBinder.Eval(e.Row.DataItem, "Estado")?.ToString();

                if (estado == "Aprobada")
                {
                    e.Row.BackColor = System.Drawing.Color.FromArgb(220, 252, 231);
                    e.Row.ForeColor = System.Drawing.Color.FromArgb(22, 101, 52);
                }
                else if (estado == "Rechazada")
                {
                    e.Row.BackColor = System.Drawing.Color.FromArgb(254, 226, 226);
                    e.Row.ForeColor = System.Drawing.Color.FromArgb(153, 27, 27);
                }
            }
        }
    }
}