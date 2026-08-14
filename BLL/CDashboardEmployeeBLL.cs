using DAL;
using ENT;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Net.Mail;

namespace BLL
{
    public class CDashboardEmployeeBLL
    {
        private CConexionBD cConexionBD = new CConexionBD();
        private string vSQL = string.Empty;

        public int ObtenerDiasFaltantesTeletrabajo(int EmpleadoID)
        {
            vSQL = @"SELECT COUNT(*) dias
                        FROM ProgramacionTeletrabajo
                        WHERE MONTH(Fecha) = MONTH(GETDATE())
                          AND YEAR(Fecha) = YEAR(GETDATE())
                          AND EmpleadoID = " + EmpleadoID;

            return Convert.ToInt32(cConexionBD.mObtenerDato(vSQL));
        }

        public string ObtenerProximoDiaTeletrabajo(int EmpleadoID)
        {
            vSQL = @"SELECT TOP 1 CAST(DAY(Fecha) AS VARCHAR(2)) + '-' + CAST(MONTH(Fecha) AS VARCHAR(2)) AS FechaFormateada
                    FROM ProgramacionTeletrabajo
                    WHERE Fecha > GETDATE()
                      AND MONTH(Fecha) = MONTH(GETDATE())
                      AND YEAR(Fecha) = YEAR(GETDATE())
                      AND EmpleadoID = "+EmpleadoID+" ORDER BY Fecha ASC";

            return cConexionBD.mObtenerDato(vSQL);
        }

        public string ObtenerCantidadSolicitudes(int EmpleadoID)
        {
            vSQL = @"SELECT COUNT(1) FROM SolicitudesTeletrabajo WHERE EstadoID='Pendiente' AND EmpleadoID= "+EmpleadoID;

            return cConexionBD.mObtenerDato(vSQL);
        }

        public string ObtenerCantidadNotificaciones(int EmpleadoID)
        {
            vSQL = @"SELECT COUNT(1) FROM Notificaciones WHERE Leida=0 AND EmpleadoID= " + EmpleadoID;

            return cConexionBD.mObtenerDato(vSQL);
        }

        public List<CRptDiasTeletrabajoENT> ObtenerDiasTeletrabajo(int EmpleadoID)
        {
            vSQL = @"SELECT
                        Fecha AS FechaTeletrabajo,
                        Observacion
                    FROM ProgramacionTeletrabajo
                    WHERE YEAR(Fecha) = YEAR(GETDATE())
                      AND MONTH(Fecha) = MONTH(GETDATE())
                      AND EmpleadoID="+EmpleadoID+ " ORDER BY Fecha ASC";

            List< CRptDiasTeletrabajoENT> cRptDiasTeletrabajo = new List<CRptDiasTeletrabajoENT>();
            DataSet response = cConexionBD.mObtenerDatos(vSQL);

            foreach (DataTable table in response.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    CRptDiasTeletrabajoENT c = new CRptDiasTeletrabajoENT();

                    c.FechaTeletrabajo = row["FechaTeletrabajo"].ToString();
                    c.Observacion = row["Observacion"].ToString();
                    cRptDiasTeletrabajo.Add(c);
                }
            }
        
            return cRptDiasTeletrabajo;
        }

        public List<CNotificacionesENT> ObtenerNotificaciones(int EmpleadoID)
        {
            vSQL = @"SELECT [NotificacionID]
                      ,[Titulo]
                      ,[Mensaje]
                      ,[Fecha]
                  FROM [dbo].[Notificaciones] 
                  WHERE EmpleadoID=" + EmpleadoID + " ORDER BY Fecha DESC";

            List<CNotificacionesENT> cNotificaciones = new List<CNotificacionesENT>();
            DataSet response = cConexionBD.mObtenerDatos(vSQL);

            foreach (DataTable table in response.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    CNotificacionesENT c = new CNotificacionesENT();

                    c.NotificacionID = Convert.ToInt32(row["NotificacionID"]);
                    c.Titulo = row["Titulo"].ToString();
                    c.Mensaje = row["Mensaje"].ToString();
                    c.Fecha = Convert.ToDateTime(row["Fecha"]);
                    cNotificaciones.Add(c);
                }
            }

            vSQL = @"Update [Notificaciones] SET Leida=1 WHERE EmpleadoID="+EmpleadoID+" AND Leida=0";
            cConexionBD.Ejecutar(vSQL);

            return cNotificaciones;
        }

        public List<CSolicitudesENT> ObtenerSolicitudesPendientes(int EmpleadoID)
        {
            vSQL = @"SELECT 
                          [SolicitudID],
                          [FechaSolicitud],
                          [FechaTeletrabajo],
                          [Motivo]
                    FROM [dbo].[SolicitudesTeletrabajo]
                    WHERE EstadoID = 'Pendiente'
                      AND YEAR(FechaSolicitud) = YEAR(GETDATE())
                      AND MONTH(FechaSolicitud) = MONTH(GETDATE())
                      AND EmpleadoID = "+EmpleadoID;

            List<CSolicitudesENT> cSolicitudes = new List<CSolicitudesENT>();
            DataSet response = cConexionBD.mObtenerDatos(vSQL);

            foreach (DataTable table in response.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    CSolicitudesENT c = new CSolicitudesENT();

                    c.SolicitudID = Convert.ToInt32(row["SolicitudID"]);
                    c.FechaSolicitud = Convert.ToDateTime(row["FechaSolicitud"]);
                    c.FechaTeletrabajo = Convert.ToDateTime(row["FechaTeletrabajo"]);
                    c.Motivo = row["Motivo"].ToString();
                    cSolicitudes.Add(c);
                }
            }

            return cSolicitudes;
        }

        public bool CrearSolicitud(CSolicitudENT cSolicitudENT)
        {
            try
            {
                vSQL = @"EXEC sp_InsertarSolicitudTeletrabajo "
                    + cSolicitudENT.EmpleadoID + ", '"
                    + cSolicitudENT.FechaSolicitud + "','"
                    + cSolicitudENT.Motivo+"'";

                cConexionBD.Ejecutar(vSQL);
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public List<CEmployeeENT> TraerDatosPersonales(int EmpleadoID)
        {
            vSQL = @"SELECT Identificacion, Nombre, Apellido1, Apellido2, Correo, Telefono
                    FROM  dbo.Empleados
                    WHERE
	                    EmpleadoID =" + EmpleadoID;

            List<CEmployeeENT> cCEmployeeENT = new List<CEmployeeENT>();
            DataSet response = cConexionBD.mObtenerDatos(vSQL);

            foreach (DataTable table in response.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    CEmployeeENT c = new CEmployeeENT();

                    c.identificacion = row["Identificacion"].ToString();
                    c.nombre = row["Nombre"].ToString();
                    c.apellido1 = row["Apellido1"].ToString();
                    c.apellido2 = row["Apellido2"].ToString();
                    c.correo = row["Correo"].ToString();
                    c.telefono = Convert.ToInt32(row["Telefono"].ToString());
                    cCEmployeeENT.Add(c);
                }
            }

            return cCEmployeeENT;
        }

        public string TraerSupervisor(int EmpleadoID)
        {
            vSQL = @"SELECT CONCAT(T3.Nombre, ' ', T3.Apellido1, ' ', T3.Apellido2) AS Supervisor
                FROM dbo.Empleados T1
                INNER JOIN dbo.Departamentos T2
                    ON T1.DepartamentoID = T2.DepartamentoID
                INNER JOIN dbo.Empleados T3
                    ON T2.SupervisorID = T3.EmpleadoID
                WHERE
                    T1.EmpleadoID = " + EmpleadoID;

            return cConexionBD.mObtenerDato(vSQL);
        }

        public string TraerDepartamento(int EmpleadoID)
        {
            vSQL = @"SELECT T2.Nombre
                    FROM dbo.Empleados T1
                    INNER JOIN dbo.Departamentos T2
                        ON T1.DepartamentoID = T2.DepartamentoID
                    WHERE
                        T1.EmpleadoID =" + EmpleadoID;

            return cConexionBD.mObtenerDato(vSQL);
        }

        public bool ActualizarInformacion(int EmpleadoID, int telefono)
        {
            try
            {
                vSQL = @"UPDATE Empleados
                            SET Telefono = " + telefono +
                            "WHERE EmpleadoID = " + EmpleadoID;

                cConexionBD.Ejecutar(vSQL);
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }

        }
        public string ValidarSolicitarDia(CSolicitudENT cSolicitudENT)
        {
            vSQL = @"SELECT COUNT(1)
                      FROM [dbo].[SolicitudesTeletrabajo]
                      WHERE EmpleadoID="+cSolicitudENT.EmpleadoID+" and EstadoID='Pendiente' and FechaTeletrabajo='"+cSolicitudENT.FechaSolicitud+"'";

             
            string res=cConexionBD.mObtenerDato(vSQL);
            if (!res.Equals("0"))
            {
                return "Ya tienes una solicitud pendiente para ese día";
            }
            else
            {
                return "OK";
            }
        
        }

        public List<DateTime> ObtenerDiasTeletrabajo(int EmpleadoID, DateTime mes)
        {
            DateTime primerDiaMes = new DateTime(mes.Year, mes.Month, 1);
            DateTime primerDiaMesSiguiente = primerDiaMes.AddMonths(1);

            vSQL = @"SELECT DISTINCT T1.Fecha FROM dbo.ProgramacionTeletrabajo T1
                     INNER JOIN Empleados T2 ON T1.EmpleadoID = T2.EmpleadoID
                     WHERE 
                         T1.Fecha >= '" + primerDiaMes.ToString("yyyy-MM-dd") + @"'
                         AND T1.Fecha < '" + primerDiaMesSiguiente.ToString("yyyy-MM-dd") + @"'
                         AND T2.EmpleadoID = " + EmpleadoID;

            List<DateTime> fechastrabajo = new List<DateTime>();
            DataSet response = cConexionBD.mObtenerDatos(vSQL);

            foreach (DataTable table in response.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    DateTime fecha = Convert.ToDateTime(row["Fecha"]);
                    fechastrabajo.Add(fecha.Date);
                }
            }

            return fechastrabajo;
        }
        public void CreateEmailSolicitud(
            string nombreEmpleado,
            string correoSupervisor,
            DateTime fechaSolicitud,
            string motivo)
        {
            MailMessage correo = new MailMessage();

            correo.From = new MailAddress("quesadabrenesantony@gmail.com");
            correo.To.Add(correoSupervisor);

            correo.Subject = "Nueva solicitud de teletrabajo";

            correo.IsBodyHtml = true;

            correo.Body = @"
        <meta charset='UTF-8'>

        <div style='font-family: Arial, sans-serif; background-color:#f5f5f5; padding:20px;'>

            <div style='max-width:500px; margin:auto; background:white; 
                        border-radius:10px; padding:30px; text-align:center; 
                        box-shadow:0 4px 10px rgba(0,0,0,0.1);'>

                <h2 style='color:#0d6efd;'>
                    Nueva solicitud de teletrabajo
                </h2>

                <p style='color:#555; font-size:16px;'>
                    Se ha registrado una nueva solicitud de teletrabajo 
                    que requiere de su revisión.
                </p>

                <div style='background:#f8f9fa; padding:20px; 
                            border-radius:8px; margin-top:20px; text-align:left;'>

                    <p style='color:#333; font-size:15px;'>
                        <strong>Empleado:</strong><br>
                        " + nombreEmpleado + @"
                    </p>

                    <p style='color:#333; font-size:15px;'>
                        <strong>Fecha de teletrabajo:</strong><br>
                        " + fechaSolicitud.ToString("dd/MM/yyyy") + @"
                    </p>

                    <p style='color:#333; font-size:15px;'>
                        <strong>Motivo:</strong><br>
                        " + motivo + @"
                    </p>

                    <p style='color:#333; font-size:15px;'>
                        <strong>Estado:</strong><br>
                        <span style='color:#ffc107; font-weight:bold;'>
                            Pendiente de aprobación
                        </span>
                    </p>

                </div>

                <p style='color:#555; font-size:14px; margin-top:25px;'>
                    Por favor, ingrese al sistema para revisar 
                    y gestionar esta solicitud.
                </p>

                <hr style='margin:30px 0;'>

                <p style='font-size:12px; color:#aaa;'>
                    © 2026 Sistema de Gestión de Teletrabajo
                </p>

            </div>

        </div>";

            correo.BodyEncoding = System.Text.Encoding.UTF8;
            correo.SubjectEncoding = System.Text.Encoding.UTF8;

            SmtpClient smtp = new SmtpClient(
                "smtp.gmail.com",
                587
            );

            smtp.Credentials = new NetworkCredential(
                "quesadabrenesantony@gmail.com",
                "opnr bldz cpfs vzjz"
            );

            smtp.EnableSsl = true;

            smtp.Send(correo);
        }
       


    }

}