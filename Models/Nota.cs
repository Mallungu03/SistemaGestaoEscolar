using System;
using SistemaGestaoEscolar.Enums;
using SistemaGestaoEscolar.Exceptions;

namespace SistemaGestaoEscolar.Models
{
    public class Nota
    {
        public Aluno Aluno { get; private set; }
        public Disciplina Disciplina { get; private set; }
        public Docente Docente { get; private set; }
        public double Valor { get; private set; }
        public TipoAvaliacao Avaliacao { get; private set; }
        public string PeriodoLectivo { get; private set; }
        public DateTime DataLancamento { get; private set; }

        public Nota(Aluno aluno, Disciplina disciplina, Docente docente, double valor, TipoAvaliacao avaliacao, string periodoLectivo)
        {
            if (aluno == null) throw new ArgumentNullException(nameof(aluno));
            if (disciplina == null) throw new ArgumentNullException(nameof(disciplina));
            if (docente == null) throw new ArgumentNullException(nameof(docente));
            if (valor < 0 || valor > 20) throw new NotaInvalidaException();
            if (string.IsNullOrWhiteSpace(periodoLectivo)) throw new ArgumentException("Período lectivo obrigatório.");

            Aluno = aluno;
            Disciplina = disciplina;
            Docente = docente;
            Valor = Math.Round(valor, 2);
            Avaliacao = avaliacao;
            PeriodoLectivo = periodoLectivo.Trim();
            DataLancamento = DateTime.Now;
        }

        public Nota(Aluno aluno, Disciplina disciplina, Docente docente, double valor)
            : this(aluno, disciplina, docente, valor, TipoAvaliacao.PrimeiraProva, "I Trimestre")
        {
        }
    }
}
 

