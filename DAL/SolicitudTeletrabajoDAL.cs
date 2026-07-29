using System;
using System.Data;
using System.Data.SqlClient;
using ENT; // Importamos la capa de Entidades

namespace DAL
{
    public class SolicitudTeletrabajoDAL
    {
        // Usamos la clase de conexión que ya tenés en tu DAL
        private CConexionBD conexion = new CConexionBD();

        public bool InsertarSolicitud(SolicitudTeletrabajo solicitud)
        {
            try
            {
                using (SqlConnection con = conexion.ObtenerConexion()) // Adaptar según el método de tu CConexionBD
                {
                    string query = @"INSERT INTO SolicitudTeletrabajo (IdEmpleado, FechaInicio, FechaFin, Motivo, IdEstado) 
                                     VALUES (@IdEmpleado, @FechaInicio, @FechaFin, @Motivo, @IdEstado)";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@IdEmpleado", solicitud.IdEmpleado);
                        cmd.Parameters.AddWithValue("@FechaInicio", solicitud.FechaInicio);
                        cmd.Parameters.AddWithValue("@FechaFin", solicitud.FechaFin);
                        cmd.Parameters.AddWithValue("@Motivo", solicitud.Motivo);
                        cmd.Parameters.AddWithValue("@IdEstado", solicitud.IdEstado);

                        con.Open();
                        int filasAfectadas = cmd.ExecuteNonQuery();
                        return filasAfectadas > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar la solicitud de teletrabajo: " + ex.Message);
            }
        }
    }
}