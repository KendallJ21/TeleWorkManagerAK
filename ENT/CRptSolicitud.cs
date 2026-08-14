using System;

namespace ENT
{
    public class CRptSolicitud
    {
        public int SolicitudID { get; set; }
        public string Empleado { get; set; }

        public DateTime Fecha { get; set; }

        public string Motivo { get; set; }

        public string Comentario { get; set; }

        public string Estado { get; set; }
    }
}
