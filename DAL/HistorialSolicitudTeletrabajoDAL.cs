using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using ENT;

namespace DAL
{
    public partial class SolicitudTeletrabajoDAL
    {
        public DataTable ObtenerHistorial(int idEmpleado, int? anio, int? mes)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection con = conexion.ObtenerConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_ObtenerHistorialTeletrabajo", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdEmpleado", idEmpleado);
                        cmd.Parameters.AddWithValue("@Anio", (object)anio ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Mes", (object)mes ?? DBNull.Value);

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el historial: " + ex.Message);
            }
            return dt;
        }
    }
}