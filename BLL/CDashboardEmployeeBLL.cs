using DAL;
using ENT;
using System;
using System.Collections.Generic;
using System.Data;

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
    }
}