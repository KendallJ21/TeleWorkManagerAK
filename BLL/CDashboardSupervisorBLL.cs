using DAL;
using ENT;
using System;
using System.Collections.Generic;
using System.Data;

namespace BLL
{
    public class CDashboardSupervisorBLL
    {
        private CConexionBD cConexionBD = new CConexionBD();
        private string vSQL = string.Empty;

        public string ObtenerCantidadSolicitudesAprobadas(int DepartamentoID)
        {
            vSQL = @"SELECT COUNT(1) FROM [dbo].[SolicitudesTeletrabajo] T1 
                    INNER JOIN dbo.Empleados T2 ON T1.EmpleadoID = T2.EmpleadoID                     
                    WHERE 
                        EstadoID = 'Aprobada' 
                        AND MONTH(T1.FechaTeletrabajo) = MONTH(GETDATE())
                        AND YEAR(T1.FechaTeletrabajo) = YEAR(GETDATE())
                        AND T2.DepartamentoID = " + DepartamentoID;

            return cConexionBD.mObtenerDato(vSQL);
        }

        public string ObtenerCantidadSolicitudesRechazadas( int DepartamentoID)
        {
            vSQL = @"SELECT COUNT(1) FROM [dbo].[SolicitudesTeletrabajo] T1 
                    INNER JOIN dbo.Empleados T2 ON T1.EmpleadoID = T2.EmpleadoID 
                    WHERE 
                        EstadoID = 'Rechazada' 
                        AND MONTH(T1.FechaTeletrabajo) = MONTH(GETDATE())
                        AND YEAR(T1.FechaTeletrabajo) = YEAR(GETDATE())
                        AND T2.DepartamentoID = " + DepartamentoID;

            return cConexionBD.mObtenerDato(vSQL);
        }

        public string ObtenerCantidadSolicitudesPendientes(int DepartamentoID)
        {
            vSQL = @"SELECT COUNT(1) FROM [dbo].[SolicitudesTeletrabajo] T1 
                    INNER JOIN dbo.Empleados T2 ON T1.EmpleadoID = T2.EmpleadoID 
                    WHERE 
                        EstadoID = 'Pendiente' 
                        AND MONTH(T1.FechaTeletrabajo) = MONTH(GETDATE())
                        AND YEAR(T1.FechaTeletrabajo) = YEAR(GETDATE())
                        AND T2.DepartamentoID = " + DepartamentoID;

            return cConexionBD.mObtenerDato(vSQL);

        }

        public string ObtenerColaboradoresDepartamento(int DepartamentoID)
        {
            vSQL = @"SELECT COUNT(1) FROM dbo.Empleados WHERE DepartamentoID = " + DepartamentoID;

            return cConexionBD.mObtenerDato(vSQL);
        }

        public string ObtenerCantidadColaboradoresTeletrabajo(int DepartamentoID)
        {
            vSQL = @"SELECT COUNT(1) FROM dbo.ProgramacionTeletrabajo T1
	                    INNER JOIN Empleados T2 ON T1.EmpleadoID = T2.EmpleadoID
                        WHERE Fecha = GETDATE() AND T2.DepartamentoID = " + DepartamentoID;

            return cConexionBD.mObtenerDato(vSQL);
        }

        public string ObtenerCantidadDiasProgramados(int DepartamentoID)
        {
            vSQL = @"SELECT COUNT(1)
                        FROM dbo.ProgramacionTeletrabajo T1
                            INNER JOIN Empleados T2 ON T1.EmpleadoID = T2.EmpleadoID
                        WHERE 
                            T1.Fecha >= DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()), 0)
                              AND T1.Fecha < DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()) + 1, 0)
                              AND T2.DepartamentoID = " + DepartamentoID;

            return cConexionBD.mObtenerDato(vSQL);
        }

        public string ObtenerCumplimiento(int DepartamentoID)
        {
            vSQL = @"SELECT CAST(COUNT(CASE WHEN T1.Fecha < CAST(GETDATE() AS DATE) THEN 1 END) * 100.0 / NULLIF(COUNT(*), 0) AS DECIMAL(5,2))
                    FROM dbo.ProgramacionTeletrabajo T1
	                    INNER JOIN dbo.Empleados T2 ON T1.EmpleadoID = T2.EmpleadoID
                    WHERE
                        T1.Fecha >= DATEFROMPARTS(YEAR(GETDATE()), MONTH(GETDATE()), 1)
                        AND T1.Fecha < DATEADD(MONTH, 1, DATEFROMPARTS(YEAR(GETDATE()), MONTH(GETDATE()), 1))
                        AND T2.DepartamentoID =" + DepartamentoID;

            return cConexionBD.mObtenerDato(vSQL);
        }

        public List<DateTime> ObtenerDiasTeletrabajo(int DepartamentoID, DateTime mes)
        {
            DateTime primerDiaMes = new DateTime(mes.Year, mes.Month, 1);
            DateTime primerDiaMesSiguiente = primerDiaMes.AddMonths(1);

            vSQL = @"SELECT DISTINCT T1.Fecha FROM dbo.ProgramacionTeletrabajo T1
                     INNER JOIN Empleados T2 ON T1.EmpleadoID = T2.EmpleadoID
                     WHERE 
                         T1.Fecha >= '" + primerDiaMes.ToString("yyyy-MM-dd") + @"'
                         AND T1.Fecha < '" + primerDiaMesSiguiente.ToString("yyyy-MM-dd") + @"'
                         AND T2.DepartamentoID = " + DepartamentoID;

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

        public List<string> ObtenerColaboradores(int DepartamentoID)
        {
            vSQL = @"SELECT DISTINCT CONCAT(Identificacion, '-', Nombre, ' ', Apellido1, ' ', Apellido2) AS Colaborador
                    FROM dbo.ProgramacionTeletrabajo T1
                        INNER JOIN Empleados T2 ON T1.EmpleadoID = T2.EmpleadoID
                    WHERE 
                        T1.Fecha = GETDATE()
                        AND T2.DepartamentoID = " + DepartamentoID;

            List<string> Colaboradores = new List<string>();
            DataSet response = cConexionBD.mObtenerDatos(vSQL);

            foreach (DataTable table in response.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    string Empleado;
 
                    Empleado = row["Colaborador"].ToString();
                    Colaboradores.Add(Empleado);
                }
            }

            return Colaboradores;
        }
    }
}
