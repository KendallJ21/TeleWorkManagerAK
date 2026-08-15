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

        public string ObtenerCantidadSolicitudesAprobadas(int DepartamentoID, DateTime mes)
        {
            DateTime primerDiaMes = new DateTime(mes.Year, mes.Month, 1);
            DateTime primerDiaMesSiguiente = primerDiaMes.AddMonths(1);

            vSQL = @"SELECT COUNT(1) FROM [dbo].[SolicitudesTeletrabajo] T1 
                    INNER JOIN dbo.Empleados T2 ON T1.EmpleadoID = T2.EmpleadoID                     
                    WHERE 
                        EstadoID = 'Aprobada' 
                        AND T1.FechaTeletrabajo >= '" + primerDiaMes.ToString("yyyy-MM-dd") + @"'
                        AND T1.FechaTeletrabajo < '" + primerDiaMesSiguiente.ToString("yyyy-MM-dd") + @"'
                        AND T2.DepartamentoID = " + DepartamentoID;

            return cConexionBD.mObtenerDato(vSQL);
        }

        public string ObtenerCantidadSolicitudesRechazadas( int DepartamentoID, DateTime mes)
        {
            DateTime primerDiaMes = new DateTime(mes.Year, mes.Month, 1);
            DateTime primerDiaMesSiguiente = primerDiaMes.AddMonths(1);

            vSQL = @"SELECT COUNT(1) FROM [dbo].[SolicitudesTeletrabajo] T1 
                    INNER JOIN dbo.Empleados T2 ON T1.EmpleadoID = T2.EmpleadoID 
                    WHERE 
                        EstadoID = 'Rechazada' 
                        AND T1.FechaTeletrabajo >= '" + primerDiaMes.ToString("yyyy-MM-dd") + @"'
                        AND T1.FechaTeletrabajo < '" + primerDiaMesSiguiente.ToString("yyyy-MM-dd") + @"'
                        AND T2.DepartamentoID = " + DepartamentoID;

            return cConexionBD.mObtenerDato(vSQL);
        }

        public string ObtenerCantidadSolicitudesPendientes(int DepartamentoID, DateTime mes)
        {
            DateTime primerDiaMes = new DateTime(mes.Year, mes.Month, 1);
            DateTime primerDiaMesSiguiente = primerDiaMes.AddMonths(1);

            vSQL = @"SELECT COUNT(1) FROM [dbo].[SolicitudesTeletrabajo] T1 
                    INNER JOIN dbo.Empleados T2 ON T1.EmpleadoID = T2.EmpleadoID 
                    WHERE 
                        EstadoID = 'Pendiente' 
                        AND T1.FechaTeletrabajo >= '" + primerDiaMes.ToString("yyyy-MM-dd") + @"'
                        AND T1.FechaTeletrabajo < '" + primerDiaMesSiguiente.ToString("yyyy-MM-dd") + @"'
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

        public string ObtenerCantidadDiasProgramados(int DepartamentoID, DateTime mes)
        {
            DateTime primerDiaMes = new DateTime(mes.Year, mes.Month, 1);
            DateTime primerDiaMesSiguiente = primerDiaMes.AddMonths(1);

            vSQL = @"SELECT COUNT(DISTINCT Fecha)
                        FROM dbo.ProgramacionTeletrabajo T1
                            INNER JOIN Empleados T2 ON T1.EmpleadoID = T2.EmpleadoID
                        WHERE 
                            T1.Fecha >= '" + primerDiaMes.ToString("yyyy-MM-dd") + @"'
                            AND T1.Fecha < '" + primerDiaMesSiguiente.ToString("yyyy-MM-dd") + @"'
                            AND T2.DepartamentoID = " + DepartamentoID;

            return cConexionBD.mObtenerDato(vSQL);
        }

        public string ObtenerCumplimiento(int DepartamentoID, DateTime mes)
        {
            DateTime primerDiaMes = new DateTime(mes.Year, mes.Month, 1);
            DateTime primerDiaMesSiguiente = primerDiaMes.AddMonths(1);

            vSQL = @"SELECT CAST(COUNT(CASE WHEN T1.Fecha < CAST(GETDATE() AS DATE) THEN 1 END) * 100.0 / NULLIF(COUNT(*), 0) AS DECIMAL(5,2))
                    FROM dbo.ProgramacionTeletrabajo T1
	                    INNER JOIN dbo.Empleados T2 ON T1.EmpleadoID = T2.EmpleadoID
                    WHERE
                        T1.Fecha >= '" + primerDiaMes.ToString("yyyy-MM-dd") + @"'
                        AND T1.Fecha < '" + primerDiaMesSiguiente.ToString("yyyy-MM-dd") + @"'
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

        public List<CEmployeeENT> ObtenerTeletrabajadores(int DepartamentoID, DateTime mes)
        {
            vSQL = @"SELECT DISTINCT T2.Nombre, T2.Apellido1 FROM dbo.ProgramacionTeletrabajo T1
                     INNER JOIN Empleados T2 ON T1.EmpleadoID = T2.EmpleadoID
                     WHERE 
                         T1.Fecha = '" + mes.ToString("yyyy-MM-dd") + @"'
                         AND T2.DepartamentoID = " + DepartamentoID;

            List<CEmployeeENT> cCEmployeeENT = new List<CEmployeeENT>();
            DataSet response = cConexionBD.mObtenerDatos(vSQL);

            foreach (DataTable table in response.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    CEmployeeENT c = new CEmployeeENT();

                    c.nombre = row["Nombre"].ToString();
                    c.apellido1 = row["Apellido1"].ToString();
                    cCEmployeeENT.Add(c);
                }
            }

            return cCEmployeeENT;
        }
    }
}
