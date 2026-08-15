<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DashboardSupervisorPage.aspx.cs" Inherits="TeleWorkManager.Pages.DashboardSupervisorPage" %>

<!doctype html>
<html>
    <head runat="server">
        <title>Dashboard Supervisor - TeleWork Manager</title>

        <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" />
        <link href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.3/font/bootstrap-icons.css" rel="stylesheet" />
        <link href="../CSS/DashboardSupervisorPage.css" rel="stylesheet" />
        <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    </head>

    <body>
        <form id="form1" runat="server">
            <div class="container py-4">
                <!-- TITULO -->
                <div class="text-center mb-4">
                    <h1> <i class="bi bi-speedometer2"></i> TeleWork Manager </h1>
                
                    <h4><asp:Label ID="txt_dashboard" runat="server" Text=""></asp:Label></h4>

                    <div class="mt-2">
                        <a href="RequestManagmentPage.aspx" class="btn btn-custom me-2">
                            <i class="bi bi-file-earmark-text"></i>
                            Gestionar Solicitudes
                        </a>

                        <a href="MonthlySchedulePage.aspx" class="btn btn-custom me-2">
                            <i class="bi bi-calendar3"></i>
                            Programación Mensual
                        </a>

                        <a href="ProfilePage.aspx" class="btn btn-custom me-2">
                            <i class="bi-person-gear"></i>
                            Actualizar Información
                        </a>

                        <a href="LoginPage.aspx" class="btn btn-danger">
                            <i class="bi bi-box-arrow-right"></i>
                            Cerrar Sesión
                        </a>
                    </div>
                </div>

                <!-- RESUMEN -->
                <div class="row g-4 mb-4">
                    <!-- Solicitudes pendientes -->
                    <div class="col-xl-3 col-md-6">
                        <div class="card-dashboard text-center h-100">
                            <i class="bi bi-file-earmark-text icon-dashboard"></i>
                            <h5 class="letra mt-3"> Solicitudes pendientes </h5>
                            <h1 class="title">
                                <asp:Label ID="lblSolicitudesPendientes" runat="server" Text="0"> </asp:Label>
                            </h1>

                            <span class="badge bg-warning text-dark">
                                Requieren revisión
                            </span>

                            <div class="mt-3">
                                <a href="RequestManagmentPage.aspx" class="btn btn-custom btn-sm">
                                    Ver solicitudes
                                </a>
                            </div>
                        </div>
                    </div>

                    <!-- Empleados trabajando hoy -->
                    <div class="col-xl-3 col-md-6">
                        <div class="card-dashboard text-center h-100">
                            <i class="bi bi-people-fill icon-dashboard"></i>
                            <h5 class="letra mt-3"> Teletrabajando hoy </h5>
                        
                            <h1 class="title">
                                <asp:Label ID="lblEmpleadosRemotos" runat="server" Text="0"> </asp:Label>
                            </h1>

                            <span class="badge bg-success">
                                Empleados remotos
                            </span>

                            <div class="mt-3">
                                <asp:LinkButton ID="btnVerEmpleados" runat="server" CssClass="btn btn-custom btn-sm" OnClick="btnVerEmpleados_Click">
                                    Ver empleados
                                </asp:LinkButton>
                            </div>
                        </div>
                    </div>

                    <!-- Días programados -->
                    <div class="col-xl-3 col-md-6">
                        <div class="card-dashboard text-center h-100">
                            <i class="bi bi-calendar-check icon-dashboard"></i>
                            <h5 class="letra mt-3"> Días programados </h5>

                            <h1 class="title">
                                <asp:Label ID="lblDiasProgramados" runat="server" Text="0"> </asp:Label>
                            </h1>

                            <span class="badge bg-info">
                                Mes actual
                            </span>
                        </div>
                    </div>

                    <!-- Cumplimiento -->
                    <div class="col-xl-3 col-md-6">
                        <div class="card-dashboard text-center h-100">
                            <i class="bi bi-bar-chart-fill icon-dashboard"></i>

                            <h5 class="letra mt-3">
                                Cumplimiento
                            </h5>

                            <h1 class="title">
                                <asp:Label ID="lblCumplimiento" runat="server" Text="0%"> </asp:Label>
                            </h1>

                            <span class="badge bg-success">
                                Indicador mensual
                            </span>
                        </div>
                    </div>
                </div>


                <!-- CALENDARIO DEL DEPARTAMENTO -->
                <div class="login-card p-4 text-center">
                    <h4 class="title mb-4">
                        <i class="bi bi-calendar3"></i>
                        Calendario del departamento
                    </h4>

                    <div class="row">
                        <div class="col-lg-7 text-center">
                            <asp:Calendar ID="clrCalendario" runat="server" CssClass="calendar-full text-center" BackColor="White" BorderColor="Black" BorderStyle="Solid" CellSpacing="1" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Width="500px" Height="500px" NextPrevFormat="ShortMonth" OnDayRender="clrCalendario_DayRender" OnVisibleMonthChanged="clrCalendario_VisibleMonthChanged">
                                <DayHeaderStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" />
                                <DayStyle BackColor="#CCCCCC" />
                                <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="White" />
                                <OtherMonthDayStyle ForeColor="#999999" />
                                <SelectedDayStyle BackColor="#00ADB5" ForeColor="White" />
                                <TitleStyle BackColor="#00ADB5" Font-Bold="True" Font-Size="12pt" ForeColor="White" />
                            </asp:Calendar>
                        </div>
                    </div>
                </div>
            
                <!-- INDICADORES MENSUALES -->
                <div class="row g-4 mt-1">
                    <div class="col-lg-6">
                        <div class="login-card p-4">
                            <h4 class="title mb-4">
                                <i class="bi bi-graph-up"></i>
                                Indicadores mensuales
                            </h4>

                            <div class="mb-4">
                                <div class="d-flex justify-content-between">
                                    <span class="letra">
                                        Solicitudes aprobadas
                                    </span>

                                    <strong class="letra">
                                        <asp:Label ID="lblAprobadas" runat="server" Text="0"> </asp:Label>
                                    </strong>
                                </div>

                                <div class="progress mt-2">
                                    <div id="progressAprobadas" runat="server" class="progress-bar" role="progressbar" style="width:0%;">
                                </div>
                            </div>
                        </div>

                            <div class="mb-4">
                                <div class="d-flex justify-content-between">
                                    <span class="letra">
                                        Solicitudes rechazadas
                                    </span>
                                
                                    <strong class="letra">
                                        <asp:Label ID="lblRechazadas" runat="server" Text="0"> </asp:Label>  
                                    </strong>
                                </div>

                                <div class="progress mt-2">
                                    <div id="progressRechazadas" runat="server" class="progress-bar" role="progressbar" style="width:0%;"></div>
                                </div>
                            </div>
                    
                            <div class="mb-4">
                                <div class="d-flex justify-content-between">
                                    <span class="letra">
                                        Solicitudes pendientes
                                    </span>

                                    <strong class="letra">
                                        <asp:Label ID="lblPendientes" runat="server" Text="0"> </asp:Label>
                                    </strong>
                                </div>

                                <div class="progress mt-2"> 
                                    <div id="progressPendientes" runat="server" class="progress-bar" role="progressbar" style="width:0%;"></div>
                                </div>
                            </div>
                    </div>
                </div>

                    <!-- RESUMEN DEL MES -->
                    <div class="col-lg-6">
                        <div class="login-card p-4">
                            <h4 class="title mb-4">
                                <i class="bi bi-info-circle"></i>
                                Resumen del mes
                            </h4>

                            <div class="row text-center">
                                <div class="col-6 mb-4">
                                    <i class="bi bi-people icon-dashboard"></i>
                                    <h6 class="letra mt-2"> Empleados </h6>
                                
                                    <h3 class="title">
                                        <asp:Label ID="lblTotalEmpleados" runat="server" Text="0"> </asp:Label>
                                    </h3>
                                </div>

                                <div class="col-6 mb-4">
                                    <i class="bi bi-calendar-week icon-dashboard"></i>

                                    <h6 class="letra mt-2">
                                        Días programados
                                    </h6>

                                    <h3 class="title">
                                        <asp:Label ID="lblTotalDias" runat="server" Text="0"> </asp:Label>
                                    </h3>
                                </div>

                                <div class="col-6">
                                    <i class="bi bi-check-circle icon-dashboard"></i>
                            
                                    <h6 class="letra mt-2">
                                        Aprobadas
                                    </h6>

                                    <h3 class="title">
                                        <asp:Label ID="lblTotalAprobadas" runat="server" Text="0"> </asp:Label>
                                    </h3>
                                </div>

                                <div class="col-6">
                                    <i class="bi bi-x-circle icon-dashboard"></i>

                                    <h6 class="letra mt-2">
                                        Rechazadas
                                    </h6>

                                    <h3 class="title">
                                        <asp:Label ID="lblTotalRechazadas" runat="server" Text="0"> </asp:Label>
                                    </h3>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <!-- MODAL EMPLEADOS REMOTOS -->    
            <div class="modal fade" id="ModalEmpleados" tabindex="-1">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-header dark-color text-white">
                            <h5 class="modal-title">
                                <i class="bi bi-people-fill"></i>
                                Empleados trabajando remotamente hoy
                            </h5>

                            <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal"></button>
                        </div>

                        <div class="modal-body">
                            <asp:Repeater ID="RptEmpleadosRemotos" runat="server">   
                                <ItemTemplate>
                                    <div class="card mb-2">
                                        <div class="card-body">
                                            <div class="d-flex justify-content-between">
                                                <div>
                                                    <i class="bi bi-person-circle"></i>

                                                    <strong>
                                                        <%# Container.DataItem %>
                                                    </strong>
                                                </div>

                                                <span class="badge bg-success">
                                                    Teletrabajando
                                                </span>
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