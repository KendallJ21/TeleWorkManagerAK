using DAL;
using ENT;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Net.Mail;

namespace BLL
{
    public class CRequestManagmentBLL
    {
        private CConexionBD cConexionBD = new CConexionBD();
        private string vSQL = string.Empty;

        public List<CEmployeeENT> ObtenerColaboradores(int DepartamentoID)
        {
            vSQL = @"SELECT EmpleadoID, Identificacion, Nombre, Apellido1, Apellido2, CONCAT(Identificacion, '-', Nombre, ' ', Apellido1, ' ', Apellido2) AS Colaborador FROM Empleados
                    WHERE        
                        DepartamentoID = " + DepartamentoID;

            List<CEmployeeENT> cEmployee = new List<CEmployeeENT>();
            DataSet response = cConexionBD.mObtenerDatos(vSQL);

            foreach (DataTable table in response.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    CEmployeeENT c = new CEmployeeENT();

                    c.EmpleadoID = Convert.ToInt32(row["EmpleadoID"]);
                    c.identificacion = row["identificacion"].ToString(); 
                    c.nombre = row["nombre"].ToString();
                    c.apellido1 = row["Apellido1"].ToString();
                    c.apellido2 = row["Apellido2"].ToString();
                    c.Colaborador = row["Colaborador"].ToString();

                    cEmployee.Add(c);
                }
            }

            return cEmployee;
        }

        public List<CRptSolicitudENT> ObtenerSolicitudes(int DepartamentoID, string estado, string empleado, string fecha)
        {
            vSQL = @"SELECT SolicitudID, CONCAT(Identificacion, '-', Nombre, ' ', Apellido1, ' ', Apellido2) AS Empleado, FechaTeletrabajo AS Fecha, Motivo, ComentarioSupervisor AS Comentario, EstadoID AS Estado FROM dbo.SolicitudesTeletrabajo T1
                    INNER JOIN Empleados T2 ON T1.EmpleadoID = T2.EmpleadoID
                    WHERE
	                    T2.DepartamentoID =  " + DepartamentoID;

            if (!string.IsNullOrEmpty(estado))
            {
                vSQL += "AND T1.EstadoID = '" + estado + "'";
            }

            if (!string.IsNullOrEmpty(empleado))
            {
                vSQL += " AND T1.EmpleadoID = " + empleado;
            }

            if (fecha != "dd/mm/yyyy" && !string.IsNullOrEmpty(fecha))
            {
                vSQL += " AND CONVERT(date, T1.FechaTeletrabajo) = '" + fecha + "'";
            }

            List<CRptSolicitudENT> CRptSolicitud = new List<CRptSolicitudENT>();
            DataSet response = cConexionBD.mObtenerDatos(vSQL);

            foreach (DataTable table in response.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    CRptSolicitudENT c = new CRptSolicitudENT();

                    c.SolicitudID = Convert.ToInt32(row["SolicitudID"]);
                    c.Empleado = row["Empleado"].ToString();
                    c.Fecha = Convert.ToDateTime(row["Fecha"]);
                    c.Motivo = row["Motivo"].ToString();
                    c.Comentario = row["Comentario"].ToString();
                    c.Estado = row["Estado"].ToString();

                    CRptSolicitud.Add(c);
                }
            }

            return CRptSolicitud;
        }

        public bool AprobarSolicitud(int Empleado, int SolicitudID, string comentario)
        {
            try
            {
                vSQL = @"EXEC sp_AprobarSolicitud "
                    + SolicitudID + ", '"
                    + Empleado + "','"
                    + comentario + "'";

                cConexionBD.Ejecutar(vSQL);
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public bool RechazarSolicitud(int Empleado, int SolicitudID, string comentario)
        {
            try
            {
                vSQL = @"EXEC sp_RechazarSolicitud "
                    + SolicitudID + ", '"
                    + Empleado + "','"
                    + comentario + "'";

                cConexionBD.Ejecutar(vSQL);
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public List<CEnvioCorreoENT> ObtenerSolicitud(int SolicitudID)
        {
            vSQL = @"SELECT
	                  CONCAT(Identificacion, '-', Nombre, ' ', Apellido1, ' ', Apellido2) AS Empleado
	                , T2.Correo
	                , T1.FechaTeletrabajo AS FechaSolicitud
	                , T1.ComentarioSupervisor
                FROM SolicitudesTeletrabajo T1
	                INNER JOIN Empleados T2
		                ON T1.EmpleadoID = T2.EmpleadoID
                WHERE SolicitudID = " + SolicitudID;

            List<CEnvioCorreoENT> cCEnvioCorreoENT = new List<CEnvioCorreoENT>();
            DataSet response = cConexionBD.mObtenerDatos(vSQL);

            foreach (DataTable table in response.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    CEnvioCorreoENT c = new CEnvioCorreoENT();

                    c.Empleado = row["Empleado"].ToString();
                    c.Correo = row["Correo"].ToString();
                    c.Fecha = Convert.ToDateTime(row["FechaSolicitud"]);
                    c.Comentario = row["ComentarioSupervisor"].ToString();

                    cCEnvioCorreoENT.Add(c);
                }
            }

            return cCEnvioCorreoENT;
        }

        public void CreateEmail(string estado, string nombreEmpleado, string correoEmpleado, DateTime fechaSolicitud, string comentario)
        {
            MailMessage correo = new MailMessage();

            correo.From = new MailAddress("quesadabrenesantony@gmail.com");
            correo.To.Add(correoEmpleado);

            bool aprobada = estado == "Aprobada";

            correo.Subject = aprobada
                ? "Solicitud de teletrabajo aprobada"
                : "Solicitud de teletrabajo rechazada";

            correo.IsBodyHtml = true;

            string titulo = aprobada
                ? "¡Tu solicitud de teletrabajo ha sido aprobada!"
                : "Tu solicitud de teletrabajo ha sido rechazada";

            string mensaje = aprobada
                ? "Te informamos que tu solicitud de teletrabajo ha sido aprobada por tu supervisor."
                : "Te informamos que tu solicitud de teletrabajo ha sido rechazada por tu supervisor.";

            string color = aprobada ? "#198754" : "#dc3545";

            correo.Body = @"
                <meta charset='UTF-8'>

                <div style='font-family: Arial, sans-serif; background-color:#f5f5f5; padding:20px;'>
                    <div style='max-width:500px; margin:auto; background:white; border-radius:10px; padding:30px; text-align:center; box-shadow:0 4px 10px rgba(0,0,0,0.1);'>
                        <h2 style='color:" + color + @";'>
                            " + titulo + @"
                        </h2>

                        <p style='color:#555; font-size:16px;'>
                            Hola " + nombreEmpleado + @",
                        </p>

                        <p style='color:#555; font-size:16px;'>
                            " + mensaje + @"
                        </p>

                        <p style='color:#333; font-size:18px; margin:20px 0;'>
                            Fecha de teletrabajo:
                        </p>

                        <p style='font-size:24px; font-weight:bold; color:" + color + @";'>
                            " + fechaSolicitud.ToString("dd/MM/yyyy") + @"
                        </p>

                        " + (!string.IsNullOrEmpty(comentario) ? @"
                        <p style='color:#333; font-size:16px; margin-top:20px;'>
                            <strong>Comentario del supervisor:</strong>
                        </p>

                        <p style='color:#555; font-size:15px; background:#f8f8f8; padding:15px; border-radius:5px;'>
                            " + comentario + @"
                        </p>
                        " : "") + @"
                
                        <hr style='margin:30px 0;'>
                            <p style='font-size:12px; color:#aaa;'>
                            © 2026 Sistema de Gestión de Teletrabajo
                        </p>
                    </div>
                </div>";

            correo.BodyEncoding = System.Text.Encoding.UTF8;
            correo.SubjectEncoding = System.Text.Encoding.UTF8;

            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);

            smtp.Credentials = new NetworkCredential(
                "quesadabrenesantony@gmail.com",
                "opnr bldz cpfs vzjz"
            );

            smtp.EnableSsl = true;

            smtp.Send(correo);
        }
    }
}
