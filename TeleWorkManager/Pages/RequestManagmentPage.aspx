<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RequestManagmentPage.aspx.cs" Inherits="TeleWorkManager.Pages.RequestManagmentPage" %>

<!doctype html>
<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title>Gestión de Solicitudes</title>

        <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" />
        <link href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.3/font/bootstrap-icons.css" rel="stylesheet" />
        <link href="../CSS/RequestManagmentPage.css" rel="stylesheet" />
        <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    </head>

    <body>
        <form id="form1" runat="server">
            <div class="container-fluid p-4">
                <!-- ENCABEZADO -->
                <div class="login-card p-4">
                    <div class="d-flex justify-content-between align-items-center mb-4">
                        <div>
                            <h4 class="title mb-1">
                                <i class="bi bi-clipboard-check"></i>
                                Gestión de Solicitudes
                            </h4>

                            <small class="text-muted">
                                Administración de solicitudes de teletrabajo
                            </small>
                        </div>

                         <div class="d-flex gap-2">
                            <!-- VOLVER AL DASHBOARD -->
                            <asp:LinkButton ID="btnDashboard" runat="server" CssClass="btn btn-outline-light" OnClick="btnDashboard_Click">   
                                <i class="bi bi-house"></i> Dashboard 
                            </asp:LinkButton>

                            <!-- HISTORIAL -->
                             <asp:LinkButton ID="btnHistorial" runat="server" CssClass="btn btn-outline-light" OnClick="btnHistorial_Click">
                                <i class="bi bi-clock-history"></i> Ver historial
                            </asp:LinkButton>                            
                        </div>
                    </div>

                    <!-- FILTROS -->
                    <div class="card mb-4">
                        <div class="card-body">
                            <h6 class="mb-3">
                                <i class="bi bi-funnel"></i>
                                Filtros
                            </h6>

                            <div class="row g-3">
                                <!-- EMPLEADO -->
                                <div class="col-lg-4">
                                    <label class="form-label"> Empleado </label>

                                    <asp:DropDownList ID="ddlEmpleado" runat="server" CssClass="form-select">
                                        <asp:ListItem Text="Todos los empleados" Value="" />
                                    </asp:DropDownList>
                                </div>

                                <!-- FECHA -->
                                <div class="col-lg-3">
                                    <label class="form-label"> Fecha </label>
                                    <asp:TextBox ID="txtFecha" runat="server" TextMode="Date" CssClass="form-control"> </asp:TextBox>
                                </div>

                                <!-- ESTADO -->
                                <div class="col-lg-3">
                                    <label class="form-label"> Estado </label>

                                    <asp:DropDownList ID="ddlEstado" runat="server" CssClass="form-select">
                                        <asp:ListItem Text="Todos los estados" Value="" />
                                        <asp:ListItem Text="Pendiente" Value="Pendiente" />
                                        <asp:ListItem Text="Aprobada" Value="Aprobada" />
                                        <asp:ListItem Text="Rechazada" Value="Rechazada" />
                                    </asp:DropDownList>
                                </div>

                                <!-- BOTÓN FILTRAR -->
                                <div class="col-lg-2 d-flex align-items-end">
                                    <asp:Button ID="btnFiltrar" runat="server" Text="Filtrar" CssClass="btn btn-custom w-100" OnClick="btnFiltrar_Click" />
                                </div>
                            </div>
                        </div>
                    </div>

                    <!-- TABLA DE SOLICITUDES -->
                    <div class="card">
                        <div class="card-body">
                            <div class="d-flex justify-content-between align-items-center mb-3">
                                <h6 class="mb-0">
                                    <i class="bi bi-list-check"></i>
                                    Solicitudes
                                </h6>

                                <asp:Label ID="lblCantidadSolicitudes" runat="server" CssClass="badge bg-secondary"> </asp:Label>
                            </div>

                            <div class="table-responsive">
                                <asp:GridView
                                    ID="gvSolicitudes"
                                    runat="server"
                                    AutoGenerateColumns="False"
                                    CssClass="table table-hover align-middle"
                                    GridLines="None"
                                    EmptyDataText="No existen solicitudes para mostrar."
                                    OnRowCommand="gvSolicitudes_RowCommand">

                                    <Columns>
                                        <asp:BoundField DataField="Empleado" HeaderText="Empleado" />
                                        <asp:BoundField DataField="Fecha" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}" />
                                        <asp:BoundField DataField="Motivo" HeaderText="Motivo" />
                                        <asp:TemplateField HeaderText="Estado">
                                            <ItemTemplate>
                                                <asp:Label ID="lblEstado" runat="server" Text='<%# Eval("Estado") %>' CssClass="badge bg-warning text-dark"> </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Comentario" HeaderText="Comentario" />
                                        <asp:TemplateField HeaderText="Acciones">
                                            <ItemTemplate>
                                                <div class="d-flex gap-2">
                                                    <!-- APROBAR -->
                                                    <asp:LinkButton
                                                        ID="btnAprobar"
                                                        runat="server"
                                                        CommandName="Aprobar"
                                                        CommandArgument='<%# Eval("SolicitudID") %>'
                                                        CssClass="btn btn-sm btn-success" ToolTip="Aprobar">
                                                        <i class="bi bi-check-lg"></i>
                                                    </asp:LinkButton>

                                                    <!-- RECHAZAR -->
                                                    <asp:LinkButton
                                                        ID="btnRechazar"
                                                        runat="server"
                                                        CommandName="Rechazar"
                                                        CommandArgument='<%# Eval("SolicitudID") %>'
                                                        CssClass="btn btn-sm btn-danger"
                                                        ToolTip="Rechazar">
                                                        <i class="bi bi-x-lg"></i>
                                                    </asp:LinkButton>

                                                    <!-- COMENTARIO -->
                                                    <asp:LinkButton
                                                        ID="btnComentario"
                                                        runat="server"
                                                        CommandName="Comentario"
                                                        CommandArgument='<%# Eval("SolicitudID") %>'
                                                        CssClass="btn btn-sm btn-secondary"
                                                        ToolTip="Agregar comentario">
                                                        <i class="bi bi-chat-left-text"></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <!-- MODAL COMENTARIO -->
            <div class="modal fade" id="modalComentario" tabindex="-1" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">
                                <i class="bi bi-chat-left-text"></i>
                                Comentario
                            </h5>

                            <button type="button" class="btn-close" data-bs-dismiss="modal"> </button>
                        </div>

                        <div class="modal-body">
                            <label class="form-label">
                                Comentario
                            </label>
                            
                            <asp:TextBox ID="txtComentario" runat="server" TextMode="MultiLine" Rows="4" CssClass="form-control" placeholder="Ingrese un comentario..."> </asp:TextBox>
                        </div>

                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal"> Cancelar </button>
                            <asp:Button ID="btnGuardarComentario" runat="server" Text="Guardar comentario" CssClass="btn btn-custom" OnClick="btnGuardarComentario_Click" />
                        </div>
                    </div>
                </div>
            </div>

            <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js"></script>
        </form>
    </body>
</html>