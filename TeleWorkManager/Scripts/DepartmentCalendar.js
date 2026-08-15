document.addEventListener('DOMContentLoaded', function () {

    const calendarElement = document.getElementById('calendar');

    const calendar = new FullCalendar.Calendar(calendarElement, {

        /* ================================ */
        /* CONFIGURACIÓN */
        /* ================================ */

        locale: 'es',

        firstDay: 1,

        initialView: 'dayGridMonth',

        height: 'auto',

        editable: false,

        selectable: false,


        /* ================================ */
        /* ENCABEZADO */
        /* ================================ */

        headerToolbar: {

            left: 'prev,next today',

            center: 'title',

            right: 'dayGridMonth,timeGridWeek'

        },


        buttonText: {

            today: 'Hoy',

            month: 'Mes',

            week: 'Semana'

        },


        /* ================================ */
        /* DATOS DE PRUEBA */
        /* ================================ */

        events: [

            {
                title: 'Antony Quesada',
                start: '2026-08-14',

                extendedProps: {

                    empleado: 'Antony Quesada',

                    estado: 'Aprobada',

                    motivo: 'Trabajo remoto'

                }
            },

            {
                title: 'Carlos Bonilla',
                start: '2026-08-14',

                extendedProps: {

                    empleado: 'Carlos Bonilla',

                    estado: 'Aprobada',

                    motivo: 'Trabajo remoto'

                }
            },

            {
                title: 'Kendall Jimenez',
                start: '2026-08-18',

                extendedProps: {

                    empleado: 'Kendall Jimenez',

                    estado: 'Aprobada',

                    motivo: 'Trabajo remoto'

                }
            },

            {
                title: 'Emanuel López',
                start: '2026-08-20',

                extendedProps: {

                    empleado: 'Emanuel López',

                    estado: 'Aprobada',

                    motivo: 'Trabajo remoto'

                }
            }

        ],


        /* ================================ */
        /* CLICK EN EVENTO */
        /* ================================ */

        eventClick: function (info) {

            const empleado =
                info.event.extendedProps.empleado;

            const estado =
                info.event.extendedProps.estado;

            const motivo =
                info.event.extendedProps.motivo;


            const fecha =
                info.event.start.toLocaleDateString(
                    'es-CR'
                );


            document.getElementById(
                'detalleEmpleado'
            ).innerText = empleado;


            document.getElementById(
                'detalleFecha'
            ).innerText = fecha;


            document.getElementById(
                'detalleEstado'
            ).innerText = estado;


            document.getElementById(
                'detalleMotivo'
            ).innerText = motivo;


            const modal = new bootstrap.Modal(
                document.getElementById(
                    'modalDetalle'
                )
            );


            modal.show();

        }

    });


    calendar.render();


});