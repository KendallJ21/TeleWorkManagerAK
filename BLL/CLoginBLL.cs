using DAL;
using ENT;
using System;
using System.Data;
using System.IO;
using System.Security.Cryptography;
using System.Text;


namespace BLL
{
    public class CLoginBLL
    {
        private CConexionBD cConexionBD = new CConexionBD();
        private string vSQL = string.Empty;

        private static readonly string Key = "X9#mL2@vQ7!rT5$uN8&pK1^zW4*eY6hA"; // 32 caracteres
        private static readonly string IV = "B7@nQ2#xL9$pR4!Z"; // 16 caracteres

        public CLoginENT ValidarCredenciales(string email, string password)
        {
            vSQL = @"SELECT EmpleadoID,Nombre,RolID, DepartamentoID FROM Empleados WHERE UPPER(Correo)='" + email+"' AND Contrasena='"+ Encrypt(password) + "' AND Activo=1";
            CLoginENT cEmpleado= new CLoginENT();
            bool isNull = true;

            DataSet response = cConexionBD.mObtenerDatos(vSQL);
            foreach (DataTable table in response.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    isNull = false;
                    cEmpleado.EmpleadoID = Convert.ToInt32( row["EmpleadoID"].ToString());
                    cEmpleado.Nombre = row["Nombre"].ToString();
                    cEmpleado.IdRol = Convert.ToInt32( row["RolID"].ToString());
                    cEmpleado.DepartamentoID = Convert.ToInt32(row["DepartamentoID"].ToString());
                    cEmpleado.Email = email;
                    cEmpleado.Password = null;  
                }
            }

            if (isNull)
                cEmpleado = null;
            return cEmpleado;
        }

        private static string Encrypt(string texto)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(Key);
                aes.IV = Encoding.UTF8.GetBytes(IV);

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    using (StreamWriter sw = new StreamWriter(cs))
                    {
                        sw.Write(texto);
                    }

                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }
    }
}