<%@ Page Language="C#" AutoEventWireup="true"
    CodeBehind="DashboardEmployeePage.aspx.cs"
    Inherits="TeleWorkManager.Pages.DashboardEmployeePage" %>

<!doctype html>
<html>
    <head runat="server">
        <title>Dashboard - TeleWork Manager</title>

        <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" />
        <link href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.3/font/bootstrap-icons.css" rel="stylesheet" />
        <link href="../CSS/DashboardEmployeePage.css" rel="stylesheet" />
        <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    </head>

<body>
    <form id="form1" runat="server">
        <div class="container py-4">
            <!-- TITULO -->

            <div class="text-center mb-4">
                <h1> <i class="bi bi-house-door-fill"></i> TeleWork Manager </h1>
                <h4><asp:Label ID="lblBienvenida" runat="server" Text=""></asp:Label></h4>

                <div class="mt-2">
                    <a href="ProfilePage.aspx" class="btn btn-custom me-2">
                        <i class="bi bi-calendar3"></i>
                        Actualizar Información
                    </a>

                    <a href="LoginPage.aspx" class="btn btn-danger">
                        <i class="bi bi-box-arrow-right"></i>
                        Cerrar Sesión
                    </a>
                </div>
            </div>

            <!-- RESUMEN -->

            <div class="row g-4">
                <div class="col-md-3">
                    <div class="card-dashboard text-center">
                        <asp:LinkButton
                            ID="btnRptDiasTele"
                            runat="server"
                            CssClass="btn btn-link p-0 border-0"
                            OnClick="btnRptDiasTele_Click">
                            <i class="bi bi-house-door icon-dashboard"></i>
                        </asp:LinkButton>

                        <h5 class="letra mt-3">Días asignados</h5>

                        <h1 class="title">
                            <asp:Label ID="lblDiasTeletrabajo" runat="server" Text=""></asp:Label></h1>

                        <p class="letra">
                            <asp:Label ID="lblMesAnnoTeletrabajo" runat="server" Text=""></asp:Label>
                        </p>
                    </div>
                </div>

                <div class="col-md-3">
                    <div class="card-dashboard text-center">
                        <i class="bi bi-calendar-event icon-dashboard"></i>

                        <h5 class="letra mt-3">Próximo día</h5>

                        <h1 class="title">
                            <asp:Label ID="lblProximoDia" runat="server" Text=""></asp:Label></h1>

                        <p class="letra">
                            <asp:Label ID="lblProximoMes" runat="server" Text=""></asp:Label>
                        </p>
                    </div>
                </div>

                <div class="col-md-3">
                    <div class="card-dashboard text-center">

                        <asp:LinkButton
                            ID="btnRptSolicitudes"
                            runat="server"
                            CssClass="btn btn-link p-0 border-0"
                            OnClick="btnRptSolicitudes_Click">
                             <i class="bi bi-file-earmark-text icon-dashboard"></i>
                        </asp:LinkButton>
                        <h5 class="letra mt-3">Solicitudes</h5>

                        <h1 class="title">
                            <asp:Label ID="lblCantidadSolicitudes" runat="server" Text=""></asp:Label></h1>

                        <span class="badge bg-warning">Pendientes </span>


                    </div>
                </div>

                <div class="col-md-3">
                    <div class="card-dashboard text-center">

                        <asp:LinkButton
                            ID="btnNotificaciones"
                            runat="server"
                            CssClass="btn btn-link p-0 border-0"
                            OnClick="btnNotificaciones_Click">
                                 <i class="bi bi-bell icon-dashboard"></i>
                        </asp:LinkButton>



                        <h5 class="letra mt-3">Notificaciones</h5>

                        <h1 class="title">
                            <asp:Label ID="lblCantidadNotificaciones" runat="server" Text=""></asp:Label></h1>

                        <span class="badge bg-success">Nuevas </span>

                    </div>
                </div>
            </div>

            <br />

            <div class="login-card p-4">
                <h4 class="title mb-4">
                    <i class="bi bi-calendar-check"></i>
                    Solicitud de Teletrabajo
                </h4>

                <div class="row">
                    <!-- Calendario -->
                    <div class="col-lg-7">
                        <asp:Calendar
                            ID="clrCalendario"
                            runat="server"
                            CssClass="calendar-full"
                            BackColor="White"
                            BorderColor="Black"
                            BorderStyle="Solid"
                            CellSpacing="1"
                            Font-Names="Verdana"
                            Font-Size="9pt"
                            ForeColor="Black"
                            Width="100%"
                            Height="350px"
                            NextPrevFormat="ShortMonth" OnSelectionChanged="clrCalendario_SelectionChanged" OnDayRender="clrCalendario_DayRender">
                            <DayHeaderStyle
                                Font-Bold="True"
                                Font-Size="8pt"
                                ForeColor="#333333" />

                            <DayStyle BackColor="#CCCCCC" />

                            <NextPrevStyle
                                Font-Bold="True"
                                Font-Size="8pt"
                                ForeColor="White" />

                            <OtherMonthDayStyle ForeColor="#999999" />

                            <SelectedDayStyle
                                BackColor="#00ADB5"
                                ForeColor="White" />

                            <TitleStyle
                                BackColor="#00ADB5"
                                Font-Bold="True"
                                Font-Size="12pt"
                                ForeColor="White" />

                            <TodayDayStyle BackColor="#999999" ForeColor="White" />
                        </asp:Calendar>
                    </div>

                    <!-- Formulario -->
                    <div class="col-lg-5">
                        <label class="form-label letra mt-2">
                            Fecha seleccionada
                    
                        </label>

                        <asp:TextBox
                            ID="txtFecha"
                            runat="server"
                            CssClass="form-control mb-3"
                            ReadOnly="true">
                        </asp:TextBox>

                        <label class="form-label letra">
                            Motivo de la solicitud
                    
                        </label>

                        <asp:TextBox
                            ID="txtMotivo"
                            runat="server"
                            CssClass="form-control mb-3"
                            TextMode="MultiLine"
                            Rows="5"
                            placeholder="Ingrese el motivo de la solicitud...">
                        </asp:TextBox>
                        <div class="d-grid gap-2">
                            <asp:Button
                                ID="btnSolicitar"
                                runat="server"
                                Text="Enviar Solicitud"
                                CssClass="btn btn-custom btn-lg" OnClick="btnSolicitar_Click" />

                            <asp:Button
                                ID="btnLimpiar"
                                runat="server"
                                Text="Limpiar"
                                CssClass="btn btn-outline-light" OnClick="btnLimpiar_Click" />
                        </div>
                    </div>
                </div>
            </div>
            <br />

            <br />

            <br />
        </div>

        <div class="modal fade" id="ModalRptDiasTele" tabindex="-1">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">

                    <div class="modal-header dark-color text-white">
                        <h5 class="modal-title"><i class="bi bi-calendar-event"></i> Días de teletrabajo del mes</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
                    </div>

                    <div class="modal-body">

                        <div class="row">
                            <asp:Repeater ID="RptDiasTele" runat="server">
                                <ItemTemplate>

                                    <div class="col-md-6 mb-3">
                                        <div class='border rounded p-3 h-100 <%# Convert.ToDateTime(Eval("FechaTeletrabajo")).Date <= DateTime.Today ? "bg-success text-white" : "" %>'>

                                            <h5>Día de teletrabajo #<%# Container.ItemIndex + 1 %></h5>

                                            <strong>Fecha:</strong>
                                            <%# Convert.ToDateTime(Eval("FechaTeletrabajo"))
                        .ToString("dddd dd/MM/yyyy", new System.Globalization.CultureInfo("es-ES")) %>

                                            <br />

                                            <strong>Observación:</strong>
                                            <%# Eval("Observacion") %>
                                        </div>
                                    </div>

                                </ItemTemplate>
                            </asp:Repeater>
                        </div>

                    </div>

                </div>
            </div>
        </div>
        
        <div class="modal fade" id="ModalSolicitudes" tabindex="-1">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">

                    <div class="modal-header dark-color text-white">
                        <h5 class="modal-title">
                            <i class="bi bi-file-earmark-text"></i>
                            Solicitudes de Teletrabajo Pendientes
                        </h5>

                        <button type="button"
                            class="btn-close"
                            data-bs-dismiss="modal">
                        </button>
                    </div>

                    <div class="modal-body">

                        <div class="row">

                            <asp:Repeater ID="RptSolicitudes" runat="server">
                                <ItemTemplate>

                                    <div class="col-md-6 mb-3">

                                        <div class="card h-100  border-secondary">

                                            <div class="card-body">

                                                <h5 class="card-title dark-color-text">
                                                    <i class="bi bi-file-text"></i>
                                                    Solicitud #<%# Eval("SolicitudID") %>
                                                </h5>

                                                <hr />

                                                <p>
                                                    <strong>Fecha solicitud:</strong>
                                                    <%# Convert.ToDateTime(Eval("FechaSolicitud"))
                                                .ToString("dd/MM/yyyy") %>
                                                </p>

                                                <p>
                                                    <strong>Fecha teletrabajo:</strong>
                                                    <%# Convert.ToDateTime(Eval("FechaTeletrabajo"))
                                                .ToString("dddd dd/MM/yyyy",
                                                new System.Globalization.CultureInfo("es-ES")) %>
                                                </p>

                                                <p>
                                                    <strong>Motivo:</strong>
                                                    <%# Eval("Motivo") %>
                                                </p>

                                            </div>

                                        </div>

                                    </div>

                                </ItemTemplate>
                            </asp:Repeater>

                        </div>

                    </div>

                </div>
            </div>
        </div>

        <div class="modal fade" id="ModalNotificaciones" tabindex="-1" aria-labelledby="modalNotificacionesLabel" aria-hidden="true">
            <div class="modal-dialog modal-lg modal-dialog-scrollable">
                <div class="modal-content border-0 shadow">

                    <div class="modal-header dark-color  text-white">
                        <h5 class="modal-title" id="modalNotificacionesLabel">
                            <i class="bi bi-bell-fill me-2"></i>
                            Notificaciones
                        </h5>

                        <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal"></button>
                    </div>

                    <div class="modal-body bg-light">

                        <asp:Repeater ID="RptNotificaciones" runat="server">
                            <ItemTemplate>

                                <div class="card mb-3 border-0 shadow-sm notification-card">
                                    <div class="card-body">

                                        <div class="d-flex justify-content-between align-items-start">

                                            <div>
                                                <h6 class="fw-bold mb-1">
                                                    <i class="bi bi-info-circle-fill dark-color-text  me-2"></i>
                                                    <%# Eval("Titulo") %>
                                                </h6>

                                                <p class="mb-2 text-muted">
                                                    <%# Eval("Mensaje") %>
                                                </p>
                                            </div>

                                            <small class="text-secondary">
                                                <%# Convert.ToDateTime(Eval("Fecha")).ToString("dd/MM/yyyy HH:mm") %>
                                            </small>

                                        </div>

                                    </div>
                                </div>

                            </ItemTemplate>
                        </asp:Repeater>

                    </div>
                </div>
            </div>
        </div>

        <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js"></script>
    </form>
</body>
</html>