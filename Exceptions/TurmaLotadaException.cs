using System;

namespace SistemaGestaoEscolar.Exceptions
{
    public class TurmaLotadaException : Exception
    {
        public TurmaLotadaException() : base("A turma atingiu a sua capacidade máxima.") { }
        public TurmaLotadaException(string message) : base(message) { }
    }
}
