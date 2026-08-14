<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProfilePage.aspx.cs" Inherits="TeleWorkManager.Pages.ProfilePage" %>

<!DOCTYPE html>
<html>
    <head runat="server">
        <title>Dashboard Supervisor - TeleWork Manager</title>

        <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" />
        <link href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.3/font/bootstrap-icons.css" rel="stylesheet" />
        <link href="../CSS/ProfilePage.css" rel="stylesheet" />
        <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    </head>

    <body>  
        <form id="form1" runat="server">
            <div class="container profile-container">
                <!-- ENCABEZADO -->
                <div class="profile-header">
                    <div class="profile-icon">
                        <i class="bi bi-person-fill"></i>
                    </div>
                    
                    <div>
                        <h2>Mi Perfil</h2>
                        <p> Consulta y actualiza tu información personal. </p>
                    </div>

                    <a href="DashboardEmployeePage.aspx" class="btn btn-custom-dark ms-auto">
                        <i class="bi bi-arrow-left"></i>
                        Volver al panel
                    </a>
                </div>

                <div class="row g-4">
                    <!-- INFORMACIÓN PERSONAL -->
                    <div class="col-lg-8">
                        <div class="profile-card">
                            <div class="card-header-custom">
                                <div>
                                    <i class="bi bi-person"></i>
                                    <span> Información personal </span>
                                </div>
                            </div>

                            <div class="card-body-custom">
                                <div class="row g-4">
                                    <!-- IDENTIFICACIÓN -->
                                    <div class="col-md-6">
                                        <label> Identificación </label>

                                        <div class="info-field"> 
                                            <i class="bi bi-card-text"></i>
                                            <asp:Label ID="lblIdentificacion" runat="server" Text="1-2345-6789"> </asp:Label>
                                        </div>
                                    </div>

                                    <!-- NOMBRE -->
                                    <div class="col-md-6">
                                        <label> Nombre completo </label>

                                        <div class="info-field">
                                            <i class="bi bi-person"></i>
                                            <asp:Label ID="lblNombre" runat="server" Text="Antony Quesada Brenes"></asp:Label>
                                        </div>
                                    </div>
                                
                                    <!-- APELLIDO 1 -->
                                    <div class="col-md-6">
                                        <label> Primer Apellido </label>

                                        <div class="info-field">
                                            <i class="bi bi-person"></i>
                                            <asp:Label ID="Lbl_Apellido1" runat="server" Text="Antony Quesada Brenes"></asp:Label>
                                        </div>
                                    </div>
                                
                                     <!-- APELLIDO 2 -->
                                    <div class="col-md-6">
                                        <label> Segundo Apellido </label>

                                        <div class="info-field">
                                            <i class="bi bi-person"></i>
                                            <asp:Label ID="Lbl_Apellido2" runat="server" Text="Antony Quesada Brenes"></asp:Label>
                                        </div>
                                    </div>

                                    <!-- CORREO -->
                                    <div class="col-md-6">
                                        <label> Correo electrónico </label>

                                        <div class="info-field">
                                            <i class="bi bi-envelope"></i>
                                            <asp:Label ID="lblCorreo" runat="server" Text="correo@empresa.com"> </asp:Label>
                                        </div>
                                    </div>

                                    <!-- TELÉFONO -->
                                    <div class="col-md-6">
                                        <label> Teléfono </label>

                                        <div class="info-field">
                                            <i class="bi bi-telephone"></i>

                                            <asp:TextBox
                                                ID="txt_telefono" runat="server" CssClass="input-profile" placeholder="Ingrese su teléfono"> 
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <!-- BOTÓN EDITAR -->
                                <div class="card-actions">
                                    <asp:Button ID="btnEditar" runat="server" Text="Actualizar información" CssClass="btn btn-custom-dark" OnClick="btnEditar_Click" />
                                </div>
                            </div>
                        </div>

                        <!-- INFORMACIÓN LABORAL -->
                        <div class="profile-card mt-4">
                            <div class="card-header-custom">
                                <div>
                                    <i class="bi bi-building"></i>
                                    <span> Información laboral </span>
                                </div>
                            </div>

                            <div class="card-body-custom">
                                <div class="row g-4">
                                    <!-- DEPARTAMENTO -->
                                    <div class="col-md-6">
                                        <label> Departamento </label>

                                        <div class="info-field">
                                            <i class="bi bi-diagram-3"></i>
                                            <asp:Label ID="lblDepartamento" runat="server" Text="Tecnologías de Información"> </asp:Label>
                                        </div>
                                    </div>

                                    <!-- JEFE -->
                                    <div class="col-md-6">
                                        <label> Jefe asignado </label>

                                        <div class="info-field">
                                            <i class="bi bi-person-badge"></i>
                                            <asp:Label ID="lblJefe" runat="server" Text="Supervisor General"> </asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <!-- SEGURIDAD -->
                    <div class="col-lg-4">
                        <div class="security-card">
                            <div class="security-icon">
                                <i class="bi bi-shield-lock"></i>
                            </div>
                    
                            <h4> Seguridad </h4>

                            <p>
                                Mantén segura tu cuenta actualizando
                                periódicamente tu contraseña.
                            </p>
                    
                            <a href="#" class="btn btn-custom-dark" onclick="enviarCorreoCambio()";>
                                <i class="bi bi-key"></i>
                                Cambiar Contraseña
                            </a>
                        </div>

                        <!-- ESTADO -->
                        <div class="status-card mt-4">
                            <div class="status-icon">
                                <i class="bi bi-check-circle"></i>
                            </div>

                            <div>
                                <strong> Cuenta activa </strong>
                                <p> Tu cuenta se encuentra activa. </p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </form>

        <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js"></script>
        <script src="../Scripts/Profile.js"></script>
    </body>
</html>

<script>
    function enviarCorreoCambio() {
        Swal.fire({
            title: 'Correo enviado',
            text: 'Se ha enviado un correo electrónico con las instrucciones para cambiar su contraseña. Revise su bandeja de entrada y, si no lo encuentra, verifique la carpeta de correo no deseado.',
            icon: 'success',
            confirmButtonText: 'Aceptar'
        });
    }
</script>