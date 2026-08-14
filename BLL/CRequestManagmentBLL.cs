using DAL;
using ENT;
using System;
using System.Collections.Generic;
using System.Data;

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

        public List<CRptSolicitud> ObtenerSolicitudes(int DepartamentoID, string empleado, string fecha, string estado)
        {
            vSQL = @"SELECT SolicitudID, CONCAT(Identificacion, '-', Nombre, ' ', Apellido1, ' ', Apellido2) AS Empleado, FechaTeletrabajo AS Fecha, Motivo, ComentarioSupervisor AS Comentario, EstadoID AS Estado FROM dbo.SolicitudesTeletrabajo T1
                    INNER JOIN Empleados T2 ON T1.EmpleadoID = T2.EmpleadoID
                    WHERE
	                    T2.DepartamentoID =  " + DepartamentoID;

            List<CRptSolicitud> CRptSolicitud = new List<CRptSolicitud>();
            DataSet response = cConexionBD.mObtenerDatos(vSQL);

            foreach (DataTable table in response.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    CRptSolicitud c = new CRptSolicitud();

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
    }
}
