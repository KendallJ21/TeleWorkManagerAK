<%@ Page Language="C#" AutoEventWireup="true"
    CodeBehind="DepartmentCalendarPage.aspx.cs"
    Inherits="TeleWorkManager.Pages.DepartmentCalendarPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">

    <title>Calendario del Departamento</title>

    <meta charset="utf-8" />

    <meta name="viewport"
          content="width=device-width, initial-scale=1" />

    <!-- Bootstrap -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css"
          rel="stylesheet" />

    <!-- Bootstrap Icons -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.3/font/bootstrap-icons.css"
          rel="stylesheet" />

    <!-- FullCalendar -->
    <link href="https://cdn.jsdelivr.net/npm/fullcalendar@6.1.18/index.global.min.css"
          rel="stylesheet" />

    <!-- CSS propio -->
        <link href="../CSS/DepartmentCalendarPage.css" rel="stylesheet" />
</head>

<body>

<form id="form1" runat="server">

    <div class="container-fluid calendar-container">


        <!-- ================================= -->
        <!-- ENCABEZADO -->
        <!-- ================================= -->

        <div class="calendar-header">

            <div class="header-info">

                <div class="calendar-icon">

                    <i class="bi bi-calendar3"></i>

                </div>

                <div>

                    <h2>
                        Calendario del Departamento
                    </h2>

                    <p>
                        Consulta la planificación de teletrabajo
                        de los colaboradores.
                    </p>

                </div>

            </div>


            <!-- VOLVER -->

            <asp:HyperLink
                ID="lnkVolver"
                runat="server"
                NavigateUrl="DashboardSupervisorPage.aspx"
                CssClass="btn btn-custom-dark">

                <i class="bi bi-arrow-left"></i>

                Volver al panel

            </asp:HyperLink>

        </div>


        <!-- ================================= -->
        <!-- FILTROS -->
        <!-- ================================= -->

        <div class="filter-card">

            <div class="row g-3 align-items-end">


                <!-- EQUIPO -->

                <div class="col-md-3">

                    <label class="form-label">

                        <i class="bi bi-people"></i>

                        Equipo

                    </label>

                    <asp:DropDownList
                        ID="ddlEquipo"
                        runat="server"
                        CssClass="form-select">

                        <asp:ListItem
                            Value="">
                            Todos los equipos
                        </asp:ListItem>

                    </asp:DropDownList>

                </div>


                <!-- EMPLEADO -->

                <div class="col-md-4">

                    <label class="form-label">

                        <i class="bi bi-person-search"></i>

                        Buscar empleado

                    </label>

                    <div class="input-group">

                        <span class="input-group-text">

                            <i class="bi bi-search"></i>

                        </span>

                        <asp:TextBox
                            ID="txtEmpleado"
                            runat="server"
                            CssClass="form-control"
                            placeholder="Nombre o identificación">
                        </asp:TextBox>

                    </div>

                </div>


                <!-- BUSCAR -->

                <div class="col-md-2">

                    <asp:Button
                        ID="btnBuscar"
                        runat="server"
                        Text="Buscar"
                        CssClass="btn btn-custom-dark w-100"
                         />

                </div>


                <!-- LIMPIAR -->

                <div class="col-md-2">

                    <asp:Button
                        ID="btnLimpiar"
                        runat="server"
                        Text="Limpiar"
                        CssClass="btn btn-outline-secondary w-100"
                         />

                </div>

            </div>

        </div>


        <!-- ================================= -->
        <!-- RESUMEN -->
        <!-- ================================= -->

        <div class="row g-3 summary-container">


            <div class="col-md-4">

                <div class="summary-card">

                    <div class="summary-icon employees">

                        <i class="bi bi-people-fill"></i>

                    </div>

                    <div>

                        <span>
                            Colaboradores
                        </span>

                        <strong id="totalColaboradores">
                            0
                        </strong>

                    </div>

                </div>

            </div>


            <div class="col-md-4">

                <div class="summary-card">

                    <div class="summary-icon telework">

                        <i class="bi bi-house-fill"></i>

                    </div>

                    <div>

                        <span>
                            Teletrabajo hoy
                        </span>

                        <strong id="totalTeletrabajo">
                            0
                        </strong>

                    </div>

                </div>

            </div>


            <div class="col-md-4">

                <div class="summary-card">

                    <div class="summary-icon calendar">

                        <i class="bi bi-calendar-check"></i>

                    </div>

                    <div>

                        <span>
                            Solicitudes aprobadas
                        </span>

                        <strong id="totalSolicitudes">
                            0
                        </strong>

                    </div>

                </div>

            </div>


        </div>


        <!-- ================================= -->
        <!-- LEYENDA -->
        <!-- ================================= -->

        <div class="legend-card">

            <div class="legend-item">

                <span class="legend-color telework-color"></span>

                <span>
                    Teletrabajo
                </span>

            </div>


            <div class="legend-item">

                <span class="legend-color office-color"></span>

                <span>
                    Oficina
                </span>

            </div>

        </div>


        <!-- ================================= -->
        <!-- CALENDARIO -->
        <!-- ================================= -->

        <div class="calendar-card">

            <div id="calendar"></div>

        </div>


    </div>


    <!-- ================================= -->
    <!-- MODAL DETALLE -->
    <!-- ================================= -->

    <div class="modal fade"
         id="modalDetalle"
         tabindex="-1"
         aria-hidden="true">

        <div class="modal-dialog modal-dialog-centered">

            <div class="modal-content">


                <div class="modal-header">

                    <h5 class="modal-title">

                        <i class="bi bi-house-fill"></i>

                        Detalle de teletrabajo

                    </h5>

                    <button type="button"
                            class="btn-close"
                            data-bs-dismiss="modal">
                    </button>

                </div>


                <div class="modal-body">


                    <!-- EMPLEADO -->

                    <div class="detail-row">

                        <span class="detail-label">

                            Empleado

                        </span>

                        <span
                            id="detalleEmpleado"
                            class="detail-value">

                        </span>

                    </div>


                    <!-- FECHA -->

                    <div class="detail-row">

                        <span class="detail-label">

                            Fecha

                        </span>

                        <span
                            id="detalleFecha"
                            class="detail-value">

                        </span>

                    </div>


                    <!-- ESTADO -->

                    <div class="detail-row">

                        <span class="detail-label">

                            Estado

                        </span>

                        <span
                            id="detalleEstado"
                            class="detail-value">

                        </span>

                    </div>


                    <!-- MOTIVO -->

                    <div class="detail-row">

                        <span class="detail-label">

                            Motivo

                        </span>

                        <span
                            id="detalleMotivo"
                            class="detail-value">

                        </span>

                    </div>


                </div>


                <div class="modal-footer">

                    <button type="button"
                            class="btn btn-secondary"
                            data-bs-dismiss="modal">

                        Cerrar

                    </button>

                </div>


            </div>

        </div>

    </div>


</form>


<!-- Bootstrap -->

<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js"></script>


<!-- FullCalendar -->

<script src="https://cdn.jsdelivr.net/npm/fullcalendar@6.1.18/index.global.min.js"></script>


<!-- Idioma español -->

<script src="https://cdn.jsdelivr.net/npm/fullcalendar@6.1.18/locales/es.global.min.js"></script>


<!-- JS propio -->

<script src="../Scripts/DepartmentCalendar.js"></script>

</body>

</html>