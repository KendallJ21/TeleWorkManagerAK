using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class CConexionBD
    {
        private string cadenaConectar = "Conexion a DB";

        public void Ejecutar(string sentencia)
        {
            SqlConnection conn = new SqlConnection();

            try
            {
                conn.ConnectionString = cadenaConectar;
                conn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.Parameters.Clear();
                cmd.CommandText = sentencia;

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    throw new Exception(ex.ToString());
                }
                finally
                {
                    cmd.Connection.Close();
                    conn.Dispose();
                }
            }
            catch (SqlException)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
        }

        public string mObtenerDato(string pSql)
        {
            string vDato = String.Empty;
            SqlConnection vConec;
            SqlDataReader vReader;

            try
            {
                vConec = new SqlConnection(cadenaConectar);
                SqlCommand vComand = new SqlCommand();
                vComand.Connection = vConec;
                vComand.CommandText = pSql;
                vComand.Connection.Open();
                vReader = null;
                vReader = vComand.ExecuteReader();

                if (vReader.HasRows)
                {
                    while (vReader.Read())
                    {
                        if (!vReader.IsDBNull(0))
                        {
                            vDato = vReader[0].ToString();
                            break;
                        }
                    }
                }

                vReader.Close();
                vReader.Dispose();

                vConec.Close();
                vConec.Dispose();

                return vDato;
            }
            catch (SqlException vExcepcion)
            {
                throw vExcepcion;
            }
        }

        public DataSet mObtenerDatos(string sentencia)
        {
            DataSet vdatos = new DataSet();

            try
            {
                using (SqlConnection vconexion = new SqlConnection(cadenaConectar))
                {
                    using (SqlCommand vcomando = new SqlCommand(sentencia, vconexion))
                    {
                        using (SqlDataAdapter vData_adapter = new SqlDataAdapter(vcomando))
                        {
                            vData_adapter.Fill(vdatos);
                            return vdatos;
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
}