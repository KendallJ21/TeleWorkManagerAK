<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MonthlySchedulePage.aspx.cs" Inherits="TeleWorkManager.Pages.MonthlySchedulePage" %>

<!doctype html>
<html>
<head runat="server">
    <title>Dashboard - TeleWork Manager</title>

    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.3/font/bootstrap-icons.css" rel="stylesheet" />
    <link href="../CSS/MonthlySchedulePage.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
</head>

<body>
    <form id="form1" runat="server">
        <div class="container-fluid schedule-container">
            <!-- ================================= -->
            <!-- ENCABEZADO -->
            <!-- ================================= -->
            <div class="schedule-header">
                <div class="header-info">
                    <div class="schedule-icon">
                        <i class="bi bi-calendar-plus"></i>
                    </div>
                <div>

                <h2> Programación Mensual </h2>

                <p> Asigna y administra el teletrabajo de los colaboradores. </p>
            </div>
        </div>

                <asp:HyperLink ID="lnkVolver" runat="server" NavigateUrl="DashboardSupervisorPage.aspx" CssClass="btn btn-custom-dark">
                <i class="bi bi-arrow-left"></i>
                Volver al panel
            </asp:HyperLink>
            </div>
        
            <!-- ================================= -->
            <!-- CONFIGURACIÓN -->
            <!-- ================================= -->
            <div class="configuration-card">
                <div class="row g-3 align-items-end">
                    <!-- MES -->
                    <div class="col-md-3">
                        <label class="form-label">
                            <i class="bi bi-calendar3"></i>
                            Mes
                        </label>

                        <asp:DropDownList ID="ddlMes" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlMes_SelectedIndexChanged">
                            <asp:ListItem Value="1"> Enero </asp:ListItem>
                            <asp:ListItem Value="2"> Febrero </asp:ListItem>
                            <asp:ListItem Value="3"> Marzo </asp:ListItem>
                            <asp:ListItem Value="4"> Abril </asp:ListItem>
                            <asp:ListItem Value="5"> Mayo </asp:ListItem>
                            <asp:ListItem Value="6"> Junio </asp:ListItem>
                            <asp:ListItem Value="7"> Julio </asp:ListItem>
                            <asp:ListItem Value="8"> Agosto </asp:ListItem>
                            <asp:ListItem Value="9"> Septiembre </asp:ListItem>
                            <asp:ListItem Value="10"> Octubre </asp:ListItem>
                            <asp:ListItem Value="11"> Noviembre </asp:ListItem>
                            <asp:ListItem Value="12"> Diciembre </asp:ListItem>
                        </asp:DropDownList>
                    </div>

                    <!-- AÑO -->
                    <div class="col-md-2">
                        <label class="form-label">
                            Año
                        </label>

                        <asp:DropDownList ID="ddlAnio" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlAnio_SelectedIndexChanged"> </asp:DropDownList>
                    </div>

                    <!-- EMPLEADO -->
                    <div class="col-md-4">
                        <label class="form-label">
                            <i class="bi bi-person"></i>
                            Colaborador
                        </label>

                        <asp:DropDownList ID="ddlEmpleado" runat="server" CssClass="form-select"> </asp:DropDownList>
                    </div>

                    <!-- BOTÓN -->
                    <div class="col-md-3">
                        <asp:Button ID="btnCargar" runat="server" Text="Cargar programación" CssClass="btn btn-custom-dark w-100" OnClick="btnCargar_Click" />
                    </div>
                </div>
            </div>

            <!-- ================================= -->
            <!-- INFORMACIÓN -->
            <!-- ================================= -->
            <div class="info-card">
                <div>
                    <i class="bi bi-info-circle"></i>
                    Selecciona los días en los que el colaborador
                    realizará teletrabajo.
                </div>
                
                <div>
                    <asp:Label ID="lblMaxDiasSemana" runat="server" Text="Máximo permitido: 3 días por semana" CssClass="fw-bold"> </asp:Label>                
                </div>
            </div>

            <!-- ================================= -->
            <!-- CALENDARIO -->
            <!-- ================================= -->
            <div class="calendar-card">
                <asp:Calendar ID="CalendarProgramacion" runat="server" CssClass="calendar-programacion" Width="100%" Height="600px" OnDayRender="CalendarProgramacion_DayRender" OnSelectionChanged="CalendarProgramacion_SelectionChanged">
                    <TitleStyle BackColor="#222831" ForeColor="White" Font-Bold="true" Height="50px" />
                    <DayHeaderStyle BackColor="#f1f3f5" ForeColor="#222831" Font-Bold="true" Height="40px" />
                    <DayStyle BackColor="White" ForeColor="#343a40" Font-Size="10pt" />
                    <SelectedDayStyle BackColor="#198754" ForeColor="White" Font-Bold="true" />
                    <TodayDayStyle BackColor="#e9ecef" ForeColor="#222831" Font-Bold="true" />
                </asp:Calendar>
            </div>

            <!-- ================================= -->
            <!-- RESUMEN -->
            <!-- ================================= -->
            <div class="summary-card">
                <div>
                    <i class="bi bi-calendar-check"></i>
                    Días seleccionados:

                    <strong>
                        <asp:Label ID="lblDiasSeleccionados" runat="server" Text="0"> </asp:Label>
                    </strong>
                </div>
            
                <div class="action-container">
                    <asp:Button ID="btnPublicar" runat="server" Text="Publicar programación" CssClass="btn btn-success" OnClick="btnPublicar_Click" />
                </div>
            </div>            
        </div>
    </form>
</body>
</html>