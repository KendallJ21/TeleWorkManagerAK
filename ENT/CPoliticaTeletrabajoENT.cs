using System;

namespace ENT
{
    public class CPoliticaTeletrabajoENT
    {
        public int PoliticaID { get; set; }

        public int MaxDiasSemana { get; set; }

        public int MaxDiasMes { get; set; }

        public TimeSpan HoraInicio { get; set; }

        public TimeSpan HoraFin { get; set; }
    }
}
