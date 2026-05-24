using System;

namespace SistemaGestaoEscolar.Exceptions
{
    /// <summary>
    /// Exceção lançada quando o valor da nota está fora do intervalo válido.
    /// </summary>
    public class NotaInvalidaException : Exception
    {
        public NotaInvalidaException() : base("Valor da nota inválido. Deve estar entre 0 e 20.") { }
        public NotaInvalidaException(string message) : base(message) { }
        public NotaInvalidaException(string message, Exception inner) : base(message, inner) { }
    }
}
