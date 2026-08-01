using System;

namespace ENT
{
    public class SolicitudTeletrabajo
    {
        public int IdSolicitud { get; set; }
        public int IdEmpleado { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string Motivo { get; set; }
        public int IdEstado { get; set; }
        public string NombreEstado { get; set; } // Útil para mostrar en pantalla/tablas
        public DateTime FechaCreacion { get; set; }
    }
}