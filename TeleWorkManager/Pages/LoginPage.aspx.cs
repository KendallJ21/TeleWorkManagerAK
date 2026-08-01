using BLL;
using ENT;
using System;


namespace TeleWorkManager.Pages
{
    public partial class LoginPage : System.Web.UI.Page
    {
        private CLoginBLL cLoginBLL = new CLoginBLL();
        private CLoginENT Usuario = new CLoginENT();

        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["LoginOK"] = false;

        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtPassword.Text))
            {
                string script = @"Swal.fire({
                                    title: 'Inicio Sesión',
                                    text: 'Debes ingresar tu email y/o contraseña',
                                    icon: 'error',
                                    focusConfirm: false,
                                    heightAuto: false,
                                    scrollbarPadding: false
                                });";

                ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
                return;
            }

            CLoginENT userData = new CLoginENT()
            {
                Email = txtEmail.Text,
                Password = txtPassword.Text,
            };
            if (ValidarCredenciales(userData))
            {
                Session["LoginOK"] = true;
                if (Convert.ToInt32(Session["IdRol"])==3)// Valor para saber que es solo empleado
                {
                    Response.Redirect("~/Pages/DashboardEmployeePage.aspx");
                }
               
            }

        }
        #endregion

        #region Metodos

        private bool ValidarCredenciales(CLoginENT usuarioDatos)
        {
            try
            {
                Usuario = cLoginBLL.ValidarCredenciales(usuarioDatos.Email, usuarioDatos.Password);
                if (Usuario != null)
                {
                    Session["EmpleadoID"] = Usuario.EmpleadoID;
                    Session["IdRol"] = Usuario.IdRol;
                    Session["Nombre"] = Usuario.Nombre;
                    return true;
                }
                else
                {
                    string script = @"Swal.fire({
                                    title: 'Inicio Sesión',
                                    text: 'Email y/o contraseña incorrectas',
                                    icon: 'error',
                                    focusConfirm: false,
                                    heightAuto: false,
                                    scrollbarPadding: false
                                });";

                    ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
                    return false;
                }
            }
            catch (Exception ex)
            {
                string script = $@"Swal.fire({{
                                    title: 'Página Inicio Sesión',
                                    text: '{ex.Message}',
                                    icon: 'error',
                                    focusConfirm: false,
                                    heightAuto: false,
                                    scrollbarPadding: false
                                    }});";

                ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
                return false;
            }
        }

        #endregion

     
    }
}