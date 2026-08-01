using System.Data;
using DAL;

namespace BLL
{
    public partial class SolicitudTeletrabajoBLL
    {
        public DataTable ConsultarHistorial(int idEmpleado, int? anio, int? mes)
        {
            
            return dal.ObtenerHistorial(idEmpleado, anio, mes);
        }
    }
}