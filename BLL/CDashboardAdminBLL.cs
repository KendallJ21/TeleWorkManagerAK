using DAL;
using ENT;
using System;
using System.Collections.Generic;
using System.Data;

namespace BLL
{
    public class CDashboardAdminBLL
    {
        private CConexionBD cConexionBD = new CConexionBD();
        private string vSQL = string.Empty;

        public List<CEmployeeAdminENT> CargarEmpleados()
        {
            vSQL = @"SELECT T1.EmpleadoID, T1.Nombre, T1.Correo, T2.Nombre AS Departamento, T3.Nombre AS Supervisor, T1.Activo 
                    FROM Empleados T1
	                    INNER JOIN Departamentos T2
		                    ON T1.DepartamentoID = T2.DepartamentoID
	                    INNER JOIN Empleados T3
		                    ON T2.SupervisorID = T3.EmpleadoID";

            List<CEmployeeAdminENT> cCEmployeeAdminENT = new List<CEmployeeAdminENT>();
            DataSet response = cConexionBD.mObtenerDatos(vSQL);

            foreach (DataTable table in response.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    CEmployeeAdminENT c = new CEmployeeAdminENT();

                    c.EmpleadoID = Convert.ToInt32(row["EmpleadoID"].ToString());
                    c.Nombre = row["Nombre"].ToString();
                    c.Correo = row["Correo"].ToString();
                    c.Departamento = row["Departamento"].ToString();
                    c.Supervisor = row["Supervisor"].ToString();
                    c.Activo = Convert.ToInt32(row["Activo"]);
                    cCEmployeeAdminENT.Add(c);
                }
            }

            return cCEmployeeAdminENT;
        }

        public List<CSupervisorENT> CargarSupervisor()
        {
            vSQL = @"SELECT EmpleadoID, Nombre
                    FROM Empleados
                    WHERE RolID = 2";

            List<CSupervisorENT> cCSupervisorENT = new List<CSupervisorENT>();
            DataSet response = cConexionBD.mObtenerDatos(vSQL);

            foreach (DataTable table in response.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    CSupervisorENT c = new CSupervisorENT();

                    c.SupervisorID = Convert.ToInt32(row["EmpleadoID"].ToString());
                    c.Nombre = row["Nombre"].ToString();
                    cCSupervisorENT.Add(c);
                }
            }

            return cCSupervisorENT;
        }

        public List<CDepartamentoENT> CargarDepartamento()
        {
            vSQL = @"SELECT DepartamentoID, Nombre
                FROM Departamentos
                WHERE Activo = 1";

            List<CDepartamentoENT> cCDepartamentoENT = new List<CDepartamentoENT>();
            DataSet response = cConexionBD.mObtenerDatos(vSQL);

            foreach (DataTable table in response.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    CDepartamentoENT c = new CDepartamentoENT();

                    c.DepartamentoID = Convert.ToInt32(row["DepartamentoID"].ToString());
                    c.Nombre = row["Nombre"].ToString();
                    cCDepartamentoENT.Add(c);
                }
            }

            return cCDepartamentoENT;
        }

        public List<CPoliticaTeletrabajoENT> TraerPolitica(int DepartamentoID)
        {
            vSQL = @"SELECT PoliticaID, MaxDiasSemana, MaxDiasMes, HoraInicio, HoraFin
                    FROM PoliticasTeletrabajo 
                    WHERE
	                    Activo = 1
	                    AND DepartamentoID = " + DepartamentoID;

            List<CPoliticaTeletrabajoENT> cCPoliticaTeletrabajoENT = new List<CPoliticaTeletrabajoENT>();
            DataSet response = cConexionBD.mObtenerDatos(vSQL);

            foreach (DataTable table in response.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    CPoliticaTeletrabajoENT c = new CPoliticaTeletrabajoENT();

                    c.PoliticaID = Convert.ToInt32(row["PoliticaID"].ToString());
                    c.MaxDiasSemana = Convert.ToInt32(row["MaxDiasSemana"].ToString());
                    c.MaxDiasMes = Convert.ToInt32(row["MaxDiasMes"].ToString());
                    c.HoraInicio = (TimeSpan)row["HoraInicio"];
                    c.HoraFin = (TimeSpan)row["HoraFin"];
                    cCPoliticaTeletrabajoENT.Add(c);
                }
            }

            return cCPoliticaTeletrabajoENT;
        }

        public List<int> TraerDiasNoPermitidos(int PoliticaID)
        {
            vSQL = @"SELECT DiaID FROM PoliticaDiasNoPermitidos
                    WHERE PoliticaID = " + PoliticaID;

            List<int> Dias = new List<int>();
            DataSet response = cConexionBD.mObtenerDatos(vSQL);

            foreach (DataTable table in response.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    int c = new int();

                    c = Convert.ToInt32(row["DiaID"].ToString());
                    Dias.Add(c);
                }
            }

            return Dias;
        }

    }
}