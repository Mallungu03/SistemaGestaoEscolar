using System;
using System.Collections.Generic;
using System.Linq;
using SistemaGestaoEscolar.Exceptions;

namespace SistemaGestaoEscolar.Models
{
    public class Turma
    {
        public string Codigo { get; private set; }
        public string Classe { get; private set; }
        public string Sala { get; private set; }
        public int Capacidade { get; private set; }
        private readonly List<Aluno> _alunos = new List<Aluno>();
        private readonly List<Disciplina> _disciplinas = new List<Disciplina>();

        public IReadOnlyList<Aluno> Alunos => _alunos.AsReadOnly();
        public IReadOnlyList<Disciplina> Disciplinas => _disciplinas.AsReadOnly();

        public Turma(string codigo, string classe, string sala, int capacidade)
        {
            if (string.IsNullOrWhiteSpace(codigo)) throw new ArgumentException("Código obrigatório.");
            if (string.IsNullOrWhiteSpace(classe)) throw new ArgumentException("Classe obrigatório.");
            if (capacidade <= 0) throw new ArgumentException("Capacidade deve ser maior que zero.");
            Codigo = codigo.Trim();
            Classe = classe.Trim();
            Sala = string.IsNullOrWhiteSpace(sala) ? "Não definida" : sala.Trim();
            Capacidade = capacidade;
        }

        public void AdicionarAluno(Aluno aluno)
        {
            if (aluno == null) throw new ArgumentNullException(nameof(aluno));
            if (_alunos.Any(a => a.NumeroMatricula == aluno.NumeroMatricula)) throw new AlunoJaMatriculadoException();
            if (_alunos.Count >= Capacidade) throw new TurmaLotadaException();
            _alunos.Add(aluno);
        }

        public void RemoverAluno(string numeroMatricula)
        {
            var aluno = _alunos.FirstOrDefault(a => a.NumeroMatricula == numeroMatricula);
            if (aluno != null) _alunos.Remove(aluno);
        }

        public IReadOnlyList<Aluno> ListarAlunos() => _alunos.AsReadOnly();

        public bool TemAluno(string numeroMatricula)
        {
            return _alunos.Any(a => a.NumeroMatricula == numeroMatricula);
        }

        public void AdicionarDisciplina(Disciplina disciplina)
        {
            if (disciplina == null) throw new ArgumentNullException(nameof(disciplina));
            if (_disciplinas.Any(d => d.Codigo == disciplina.Codigo)) return;
            _disciplinas.Add(disciplina);
        }

        public bool TemDisciplina(string codigoDisciplina)
        {
            return _disciplinas.Any(d => d.Codigo == codigoDisciplina);
        }

        public double MediaTurma(string codigoDisciplina)
        {
            var notas = new List<double>();
            foreach (var aluno in _alunos)
            {
                var n = aluno.ObterNotasPorDisciplina(codigoDisciplina);
                notas.AddRange(n.ConvertAll(x => x.Valor));
            }
            if (notas.Count == 0) return 0.0;
            return Math.Round(notas.Average(),2);
        }
    }
}

