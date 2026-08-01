﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginPage.aspx.cs" Inherits="TeleWorkManager.Pages.LoginPage" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Login - TeleWorkManager</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap-icons/font/bootstrap-icons.css" rel="stylesheet">
    <link href="../CSS/LoginPage.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
</head>
<body class="d-flex justify-content-center align-items-center vh-100">
    <form id="form1" runat="server">
        <div class="card login-card p-4" style="width:400px;">
            <h3 class="text-center title mb-4">
                <i class="bi bi-house-door-fill"></i>
               TeleWorkManager
            </h3>

            <div class="text-center mb-3">
                <img src="https://images.pexels.com/photos/4050315/pexels-photo-4050315.jpeg"
                    class="img-fluid rounded shadow"
                    style="max-width:120px;" />
            </div>

            <div class="mb-3">
                <label class="form-label letra">Correo electrónico</label>
                <div class="input-group">
                    <span class="input-group-text">
                        <i class="bi bi-envelope-fill"></i>
                    </span>
                    <asp:TextBox ID="txtEmail" runat="server"
                        CssClass="form-control"
                        TextMode="Email"
                        placeholder="ejemplo@correo.com">
                    </asp:TextBox>
                </div>
            </div>

            <div class="mb-3">
                <label class="form-label letra ">Contraseña</label>
                <div class="input-group">
                    <span class="input-group-text">
                        <i class="bi bi-lock-fill"></i>
                    </span>
                    <asp:TextBox ID="txtPassword" runat="server"
                        CssClass="form-control"
                        TextMode="Password"
                        placeholder="********">
                    </asp:TextBox>
                </div>
            </div>

            <asp:Button ID="btnLogin"
                runat="server"
                CssClass="btn btn-custom w-100"
                Text="Iniciar Sesión" OnClick="btnLogin_Click" />

            <div class="text-center mt-3 letra">
                <small>
                    <i class="bi bi-person-plus"></i>
                    ¿No tienes cuenta?
                     <a href="UserRegistrationPage.aspx">Regístrate</a>
                </small>
            </div>
        </div>
    </form>
</body>

</html>