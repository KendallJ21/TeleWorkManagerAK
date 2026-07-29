using System;
using ENT;
using DAL;

namespace BLL
{
    public class SolicitudTeletrabajoBLL
    {
        private SolicitudTeletrabajoDAL dal = new SolicitudTeletrabajoDAL();

        public bool RegistrarSolicitud(SolicitudTeletrabajo solicitud, out string mensajeError)
        {
            mensajeError = string.Empty;

            // Validaciones de Negocio
            if (string.IsNullOrWhiteSpace(solicitud.Motivo))
            {
                mensajeError = "Debe ingresar un motivo para la solicitud.";
                return false;
            }

            if (solicitud.FechaInicio.Date < DateTime.Now.Date)
            {
                mensajeError = "La fecha de inicio no puede ser menor a la fecha actual.";
                return false;
            }

            if (solicitud.FechaFin.Date < solicitud.FechaInicio.Date)
            {
                mensajeError = "La fecha fin no puede ser anterior a la fecha de inicio.";
                return false;
            }

            // Si todo está correcto, asignamos estado por defecto (1 = Pendiente)
            solicitud.IdEstado = 1;

            return dal.InsertarSolicitud(solicitud);
        }
    }
}