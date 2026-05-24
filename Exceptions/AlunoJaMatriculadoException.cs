using System;

namespace SistemaGestaoEscolar.Exceptions
{
    public class AlunoJaMatriculadoException : Exception
    {
        public AlunoJaMatriculadoException() : base("O aluno já se encontra matriculado nesta turma.") { }
        public AlunoJaMatriculadoException(string message) : base(message) { }
    }
}
