using DAL;
using ENT;
using System;

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
                        WHERE  Fecha > GETDATE()
                          AND MONTH(Fecha) = MONTH(GETDATE())
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
            vSQL = @"SELECT COUNT(1) FROM SolicitudesTeletrabajo WHERE EstadoID=1 AND EmpleadoID= "+EmpleadoID;

            return cConexionBD.mObtenerDato(vSQL);
        }

        public string ObtenerCantidadNotificaciones(int EmpleadoID)
        {
            vSQL = @"SELECT COUNT(1) FROM Notificaciones WHERE Leida=0 AND EmpleadoID= " + EmpleadoID;

            return cConexionBD.mObtenerDato(vSQL);
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
    }
}
