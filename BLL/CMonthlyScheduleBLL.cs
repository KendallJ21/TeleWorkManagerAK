using DAL;
using ENT;
using System;
using System.Collections.Generic;
using System.Data;

namespace BLL
{
    public class CMonthlyScheduleBLL
    {
        private CConexionBD cConexionBD = new CConexionBD();
        private string vSQL = string.Empty;

        public List<CEmployeeENT> ObtenerColaboradores(int DepartamentoID)
        {
            vSQL = @"SELECT EmpleadoID, Nombre, Apellido1, Apellido2 FROM Empleados WHERE DepartamentoID = " + DepartamentoID;

            List<CEmployeeENT> cCEmployeeENT = new List<CEmployeeENT>();
            DataSet response = cConexionBD.mObtenerDatos(vSQL);

            foreach (DataTable table in response.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    CEmployeeENT c = new CEmployeeENT();

                    c.EmpleadoID = Convert.ToInt32(row["EmpleadoID"].ToString());
                    c.nombre = row["Nombre"].ToString();
                    c.apellido1 = row["Apellido1"].ToString();
                    c.apellido2 = row["Apellido2"].ToString();
                    cCEmployeeENT.Add(c);
                }
            }

            return cCEmployeeENT;
        }

        public string CargarRegla(int DepartamentoID)
        {
            vSQL = @"SELECT MaxDiasSemana FROM PoliticasTeletrabajo
                    WHERE
	                    Activo = 1
	                    AND DepartamentoID =" + DepartamentoID;

            return cConexionBD.mObtenerDato(vSQL);
        }

        public List<DateTime> ObtenerProgramacion(int empleadoID, int mes, int anio)
        {
            vSQL = @"SELECT Fecha FROM ProgramacionTeletrabajo
                    WHERE
	                    EmpleadoID = " + empleadoID + 
	                    "AND MONTH(Fecha) = " + mes +
	                    "AND YEAR(Fecha) = " + anio;

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

        public string EliminarProgramacion(int empleadoID, DateTime fecha)
        {
            try
            {
                vSQL = @"EXEC sp_EliminarProgramacion "
                    + empleadoID + ", '"
                    + fecha.ToString("yyyy-MM-dd") + "'";

                DataSet response = cConexionBD.mObtenerDatos(vSQL);

                if (response.Tables.Count > 0 && response.Tables[0].Rows.Count > 0)
                {
                    return response.Tables[0].Rows[0]["Mensaje"].ToString();
                }

                return "Programación eliminada correctamente.";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string AgregarProgramacion(int empleadoID, DateTime fecha, int SupervisorID)
        {
            string tipotrabajo = "Teletrabajo";
            string Observacion = "Trabajo remoto programado.";

            try
            {
                vSQL = @"EXEC sp_RegistrarProgramacion "
                    + empleadoID + ", '"
                    + fecha.ToString("yyyy-MM-dd") + "','"
                    + tipotrabajo + "','"
                    + Observacion + "','"
                    + SupervisorID + "'";

                DataSet response = cConexionBD.mObtenerDatos(vSQL);
                
                if (response.Tables.Count > 0 && response.Tables[0].Rows.Count > 0)
                {
                    return response.Tables[0].Rows[0]["Mensaje"].ToString();
                }

                return "Programación registrada correctamente.";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
