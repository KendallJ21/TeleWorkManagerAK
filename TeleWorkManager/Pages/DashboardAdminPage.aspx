<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DashboardAdminPage.aspx.cs" Inherits="TeleWorkManager.Pages.DashboardAdminPage" %>

<!doctype html>
<html>
    <head runat="server">
        <title>Administración - TeleWork Manager</title>

        <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" />
        <link href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.3/font/bootstrap-icons.css" rel="stylesheet" />
        <link href="../CSS/DashboardAdminPage.css" rel="stylesheet" />
        <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    </head>

    <body>
        <form id="form2" runat="server">
            <div class="container py-4">
                <!-- ================================================= -->
                <!-- ENCABEZADO -->
                <!-- ================================================= -->
                <div class="text-center mb-4">
                    <h1>
                        <i class="bi bi-gear-fill"></i>
                        TeleWork Manager
                    </h1>

                    <h4 class="text-muted">Administración del sistema</h4>

                    <div class="mt-3">
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

                <!-- ================================================= -->
                <!-- DEPARTAMENTOS -->
                <!-- ================================================= -->
                <div class="section-title">
                    <h3 class="text-black">
                        <i class="bi bi-building  text-black"></i>
                        Departamentos
                    </h3>

                    <p class="text-black">
                        Administración de departamentos y supervisores.
                    </p>
                </div>

                <div class="row g-4 mb-5">
                    <!-- Crear departamento -->
                    <div class="col-md-4">
                        <div class="admin-card">
                            <div class="admin-icon">
                                <i class="bi bi-building-add"></i>
                            </div>

                            <h5>Crear departamento </h5>

                            <p>
                                Registre un nuevo departamento dentro de la organización.
                            </p>

                            <button type="button" class="btn btn-custom w-100" data-bs-toggle="modal" data-bs-target="#modalDepartamento">
                                <i class="bi bi-plus-circle"></i>
                                Crear departamento
                            </button>
                        </div>
                    </div>

                    <!-- Modificar -->
                    <div class="col-md-4">
                        <div class="admin-card">
                            <div class="admin-icon">
                                <i class="bi bi-pencil-square"></i>
                            </div>

                            <h5>Modificar departamentos </h5>

                            <p>
                                Edite el nombre y la información
                                de los departamentos existentes.
                            </p>

                            <asp:Button ID="btnModificarDepartamento" runat="server" Text="Modificar departamentos" CssClass="btn btn-custom w-100" />
                        </div>
                    </div>

                    <!-- Supervisor -->
                    <div class="col-md-4">
                        <div class="admin-card">
                            <div class="admin-icon">
                                <i class="bi bi-person-badge"></i>
                            </div>

                            <h5>Asignar supervisor</h5>

                            <p>
                                Asigne un supervisor responsable
                                para cada departamento.
                            </p>

                            <button type="button" class="btn btn-custom w-100" data-bs-toggle="modal" data-bs-target="#modalSupervisor">
                                <i class="bi bi-person-check"></i>
                                Asignar supervisor
                            </button>
                        </div>
                    </div>
                </div>

                <!-- ================================================= -->
                <!-- POLÍTICAS -->
                <!-- ================================================= -->
                <div class="section-title">
                    <h3 class="text-black">
                        <i class="bi bi-shield-check text-black"></i>
                        Políticas de Teletrabajo
                    </h3>

                    <p class="text-black">
                        Configure las reglas que controlan las solicitudes
                        de teletrabajo.
                    </p>
                </div>

                <div class="col-12">
                    <div class="policy-card">
                        <div class="policy-icon">
                            <i class="bi bi-diagram-3"></i>
                        </div>

                        <div class="w-100">
                            <h5>Reglas especiales por departamento</h5>

                            <p>
                                Configure reglas específicas para
                                cada departamento.
                            </p>

                            <div class="row">
                                <div class="col-md-5">
                                    <label class="form-label">
                                        Departamento
                                    </label>

                                    <asp:DropDownList ID="ddlDepartamentoPolitica" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlDepartamentoPolitica_SelectedIndexChanged">
                                        <asp:ListItem> Seleccione un departamento </asp:ListItem>
                                    </asp:DropDownList>
                                </div>

                                <div class="col-md-2 d-flex align-items-end">
                                    <asp:Button ID="btnGuardarPoliticaDepartamento" runat="server" Text="Guardar" CssClass="btn btn-custom w-100" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <br />

                <div class="row g-4 mb-5">
                    <!-- Días por semana -->
                    <div class="col-md-6">
                        <div class="policy-card">
                            <div class="policy-icon">
                                <i class="bi bi-calendar-week"></i>
                            </div>

                            <div>
                                <h5>Máximo de días por semana</h5>

                                <p>
                                    Defina la cantidad máxima de días
                                    de teletrabajo permitidos semanalmente.
                                </p>

                                <div class="input-group">
                                    <asp:TextBox ID="txtMaxDiasSemana" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                    <span class="input-group-text">días</span>
                                </div>
                            </div>
                        </div>
                    </div>

                    <!-- Días por mes -->
                    <div class="col-md-6">
                        <div class="policy-card">
                            <div class="policy-icon">
                                <i class="bi bi-calendar-month"></i>
                            </div>

                            <div>
                                <h5>Máximo de días por mes</h5>

                                <p>
                                    Defina la cantidad máxima de días
                                    permitidos durante un mes.
                                </p>

                                <div class="input-group">
                                    <asp:TextBox ID="txtMaxDiasMes" runat="server" CssClass="form-control" TextMode="Number"> </asp:TextBox>
                                    <span class="input-group-text">días </span>
                                </div>
                            </div>
                        </div>
                    </div>

                    <!-- Días no permitidos -->
                    <div class="col-md-6">
                        <div class="policy-card">
                            <div class="policy-icon">
                                <i class="bi bi-calendar-x"></i>
                            </div>

                            <div class="w-100">
                                <h5>Días no permitidos</h5>

                                <p>
                                    Seleccione los días en los que
                                    no se permite teletrabajo.
                                </p>

                                <div class="row">
                                    <div class="col-6">
                                        <div class="form-check">
                                            <asp:CheckBox ID="chkLunes" runat="server" />

                                            <label>
                                                Lunes
                                            </label>
                                        </div>

                                        <div class="form-check">
                                            <asp:CheckBox ID="chkMartes" runat="server" />

                                            <label>
                                                Martes
                                            </label>
                                        </div>

                                        <div class="form-check">
                                            <asp:CheckBox ID="chkMiercoles" runat="server" />

                                            <label>
                                                Miércoles
                                            </label>
                                        </div>

                                        <div class="form-check">
                                            <asp:CheckBox ID="chkJueves" runat="server" />

                                            <label>
                                                Jueves
                                            </label>
                                        </div>
                                    </div>

                                    <div class="col-6">
                                        <div class="form-check">
                                            <asp:CheckBox ID="chkViernes" runat="server" />

                                            <label>
                                                Viernes
                                            </label>
                                        </div>

                                        <div class="form-check">
                                            <asp:CheckBox ID="chkSabado" runat="server" />

                                            <label>
                                                Sábado
                                            </label>
                                        </div>

                                        <div class="form-check">
                                            <asp:CheckBox ID="chkDomingo" runat="server" />

                                            <label>
                                                Domingo
                                            </label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <!-- Horarios -->
                    <div class="col-md-6">
                        <div class="policy-card">
                            <div class="policy-icon">
                                <i class="bi bi-clock"></i>
                            </div>

                            <div class="w-100">
                                <h5>Horarios de teletrabajo </h5>

                                <p>
                                    Defina el horario permitido para
                                    realizar teletrabajo.
                                </p>

                                <div class="row">
                                    <div class="col-6">
                                        <label class="form-label">
                                            Hora inicio
                                        </label>

                                        <asp:TextBox ID="txtHoraInicio" runat="server" CssClass="form-control" TextMode="Time"></asp:TextBox>
                                    </div>

                                    <div class="col-6">
                                        <label class="form-label">
                                            Hora final
                                        </label>

                                        <asp:TextBox ID="txtHoraFin" runat="server" CssClass="form-control" TextMode="Time"> </asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <!-- ================================================= -->
                <!-- EMPLEADOS -->
                <!-- ================================================= -->
                <div class="section-title text-black">
                    <h3 class="text-black">
                        <i class="bi bi-people-fill text-black"></i>
                        Gestión de Empleados
                    </h3>

                    <p class="text-black">
                        Administración de usuarios, departamentos
                        y responsables.
                    </p>
                </div>

                <div class="admin-table-card">
                    <div class="d-flex justify-content-between align-items-center mb-3">
                        <div>
                            <h5 class="mb-1">Empleados registrados</h5>

                            <small class="text-muted">Administre los usuarios del sistema.</small>
                        </div>

                        <button type="button" class="btn btn-custom" data-bs-toggle="modal" data-bs-target="#modalEmpleado">
                            <i class="bi bi-person-plus"></i>
                            Nuevo empleado
                        </button>
                    </div>

                    <!-- Buscador -->
                    <div class="row mb-3">
                        <div class="col-md-6">
                            <div class="input-group">
                                <span class="input-group-text">
                                    <i class="bi bi-search"></i>
                                </span>

                                <asp:TextBox ID="txtBuscarEmpleado" runat="server" CssClass="form-control" placeholder="Buscar empleado..."> </asp:TextBox>
                                <asp:Button ID="btnBuscarEmpleado" runat="server" Text="Buscar" CssClass="btn btn-custom" OnClick="btnBuscarEmpleado_Click" />
                            </div>
                        </div>
                    </div>

                    <!-- Tabla -->
                    <div class="table-responsive">
                        <asp:GridView ID="gvEmpleados" runat="server" AutoGenerateColumns="False" CssClass="table table-hover align-middle" GridLines="None">
                            <Columns>
                                <asp:BoundField DataField="EmpleadoID" HeaderText="#" />
                                <asp:BoundField DataField="Nombre" HeaderText="Empleado" />
                                <asp:BoundField DataField="Correo" HeaderText="Correo" />
                                <asp:BoundField DataField="Departamento" HeaderText="Departamento" />
                                <asp:BoundField DataField="Supervisor" HeaderText="Jefe / Supervisor" />
                                <asp:TemplateField HeaderText="Estado">
                                    <ItemTemplate>
                                        <asp:Label 
                                            ID="lblEstado" 
                                            runat="server"
                                            Text='<%# Convert.ToInt32(Eval("Activo")) == 1 ? "Activo" : "Inactivo" %>'
                                            CssClass='<%# Convert.ToInt32(Eval("Activo")) == 1 ? "badge bg-success" : "badge bg-danger" %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Acciones">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnEditar" runat="server" CssClass="btn btn-sm btn-outline-primary me-1">
                                            <i class="bi bi-pencil"></i>
                                        </asp:LinkButton>

                                        <asp:LinkButton ID="btnEstado" runat="server" CssClass="btn btn-sm btn-outline-danger">
                                            <i class="bi bi-person-x"></i>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>

            <!-- ===================================================== -->
            <!-- MODAL DEPARTAMENTO -->
            <!-- ===================================================== -->
            <div class="modal fade" id="modalDepartamento" tabindex="-1">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header dark-color text-white">
                            <h5 class="modal-title">
                                <i class="bi bi-building-add"></i>
                                Nuevo departamento
                            </h5>

                            <button type="button"
                                class="btn-close btn-close-white"
                                data-bs-dismiss="modal">
                            </button>
                        </div>

                        <div class="modal-body">
                            <label class="form-label">
                                Nombre del departamento
                            </label>

                            <asp:TextBox
                                ID="txtNombreDepartamento"
                                runat="server"
                                CssClass="form-control"
                                placeholder="Ej. Recursos Humanos">
                            </asp:TextBox>

                            <label class="form-label mt-3">
                                Descripción
                            </label>

                            <asp:TextBox
                                ID="txtDescripcionDepartamento"
                                runat="server"
                                CssClass="form-control"
                                TextMode="MultiLine"
                                Rows="3">
                            </asp:TextBox>
                        </div>

                        <div class="modal-footer">
                            <button type="button"
                                class="btn btn-secondary"
                                data-bs-dismiss="modal">
                                Cancelar
                            </button>

                            <asp:Button ID="btnCrearDepartamento" runat="server" Text="Crear departamento" CssClass="btn btn-custom" />
                        </div>
                    </div>
                </div>
            </div>

            <!-- ===================================================== -->
            <!-- MODAL SUPERVISOR -->
            <!-- ===================================================== -->
            <div class="modal fade" id="modalSupervisor" tabindex="-1">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header dark-color text-white">
                            <h5 class="modal-title">
                                <i class="bi bi-person-badge"></i>
                                Asignar supervisor
                            </h5>

                            <button type="button"
                                class="btn-close btn-close-white"
                                data-bs-dismiss="modal">
                            </button>
                        </div>

                        <div class="modal-body">
                            <label class="form-label">
                                Departamento
                            </label>

                            <asp:DropDownList ID="ddlDepartamentoSupervisor" runat="server" CssClass="form-select">
                                <asp:ListItem>
                                    Seleccione un departamento
                                </asp:ListItem>
                            </asp:DropDownList>

                            <label class="form-label mt-3">
                                Supervisor
                            </label>

                            <asp:DropDownList ID="ddlSupervisor" runat="server" CssClass="form-select">
                                <asp:ListItem>
                                    Seleccione un supervisor
                                </asp:ListItem>
                            </asp:DropDownList>
                        </div>

                        <div class="modal-footer">
                            <button type="button"
                                class="btn btn-secondary"
                                data-bs-dismiss="modal">
                                Cancelar
                            </button>

                            <asp:Button ID="btnAsignarSupervisor" runat="server" Text="Asignar supervisor" CssClass="btn btn-custom" />
                        </div>
                    </div>
                </div>
            </div>

            <!-- ===================================================== -->
            <!-- MODAL EMPLEADO -->
            <!-- ===================================================== -->
            <div class="modal fade" id="modalEmpleado" tabindex="-1">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-header dark-color text-white">
                            <h5 class="modal-title">
                                <i class="bi bi-person-plus"></i>
                                Registrar empleado
                            </h5>

                            <button type="button"
                                class="btn-close btn-close-white"
                                data-bs-dismiss="modal">
                            </button>
                        </div>

                        <div class="modal-body">
                            <div class="row g-3">
                                <div class="col-md-6">
                                    <label class="form-label">
                                        Nombre
                                    </label>

                                    <asp:TextBox
                                        ID="txtNombreEmpleado"
                                        runat="server"
                                        CssClass="form-control">
                                    </asp:TextBox>
                                </div>

                                <div class="col-md-6">
                                    <label class="form-label">
                                        Apellidos
                                    </label>

                                    <asp:TextBox
                                        ID="txtApellidosEmpleado"
                                        runat="server"
                                        CssClass="form-control">
                                    </asp:TextBox>
                                </div>

                                <div class="col-md-6">
                                    <label class="form-label">
                                        Correo electrónico
                                    </label>

                                    <asp:TextBox
                                        ID="txtCorreoEmpleado"
                                        runat="server"
                                        CssClass="form-control"
                                        TextMode="Email">
                                    </asp:TextBox>
                                </div>

                                <div class="col-md-6">
                                    <label class="form-label">
                                        Teléfono
                                    </label>

                                    <asp:TextBox
                                        ID="txtTelefonoEmpleado"
                                        runat="server"
                                        CssClass="form-control">
                                    </asp:TextBox>
                                </div>

                                <div class="col-md-6">
                                    <label class="form-label">
                                        Departamento
                                    </label>

                                    <asp:DropDownList ID="ddlDepartamentoEmpleado" runat="server" CssClass="form-select">
                                        <asp:ListItem>
                                            Seleccione un departamento
                                        </asp:ListItem>
                                    </asp:DropDownList>
                                </div>

                                <div class="col-md-6">
                                    <label class="form-label">
                                        Jefe / Supervisor
                                    </label>

                                    <asp:DropDownList ID="ddlJefeEmpleado" runat="server" CssClass="form-select">
                                        <asp:ListItem>
                                            Seleccione un supervisor
                                        </asp:ListItem>
                                    </asp:DropDownList>
                                </div>

                                <div class="col-md-6">
                                    <label class="form-label">
                                        Estado
                                    </label>

                                    <asp:DropDownList ID="ddlEstadoEmpleado" runat="server" CssClass="form-select">
                                        <asp:ListItem Value="1">
                                            Activo
                                        </asp:ListItem>

                                        <asp:ListItem Value="0">
                                            Inactivo
                                        </asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="modal-footer">
                            <button type="button"
                                class="btn btn-secondary"
                                data-bs-dismiss="modal">
                                Cancelar
                            </button>

                            <asp:Button ID="btnGuardarEmpleado" runat="server" Text="Guardar empleado" CssClass="btn btn-custom" OnClick="btnGuardarEmpleado_Click" />
                        </div>
                    </div>
                </div>
            </div>

            <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js"></script>
        </form>
    </body>
</html>