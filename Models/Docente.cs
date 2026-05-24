using System;
using System.Collections.Generic;
using System.Linq;
using SistemaGestaoEscolar.Enums;

namespace SistemaGestaoEscolar.Models
{
    /// <summary>
    /// Representa um docente.
    /// </summary>
    public class Docente : Pessoa
    {
        #region Propriedades
        public string CodigoFuncionario { get; private set; }
        public string Especialidade { get; private set; }
        public double Salario { get; private set; }
        private readonly List<Disciplina> _disciplinas = new List<Disciplina>();
        private readonly List<Turma> _turmas = new List<Turma>();

        public IReadOnlyList<Disciplina> Disciplinas => _disciplinas.AsReadOnly();
        public IReadOnlyList<Turma> Turmas => _turmas.AsReadOnly();
        #endregion

        #region Construtor
        public Docente(string nome, DateTime dataNascimento, string telefone, Sexo sexo, string codigoFuncionario, string especialidade, double salario)
            : base(nome, dataNascimento, telefone, sexo)
        {
            if (string.IsNullOrWhiteSpace(codigoFuncionario)) throw new ArgumentException("Código do funcionário obrigatório.");
            if (string.IsNullOrWhiteSpace(especialidade)) throw new ArgumentException("Especialidade obrigatória.");
            if (salario <= 0) throw new ArgumentException("Salário deve ser positivo.");
            CodigoFuncionario = codigoFuncionario.Trim();
            Especialidade = especialidade.Trim();
            Salario = salario;
        }
        #endregion

        #region Métodos
        /// <summary>
        /// Lança uma nota (cria e devolve uma instância de Nota).
        /// </summary>
        public void AtribuirDisciplina(Disciplina disciplina)
        {
            if (disciplina == null) throw new ArgumentNullException(nameof(disciplina));
            if (_disciplinas.Any(d => d.Codigo == disciplina.Codigo)) return;
            _disciplinas.Add(disciplina);
            disciplina.AtribuirDocente(this);
        }

        public bool LecionaDisciplina(string codigoDisciplina)
        {
            return _disciplinas.Any(d => d.Codigo == codigoDisciplina);
        }

        public void AtribuirTurma(Turma turma)
        {
            if (turma == null) throw new ArgumentNullException(nameof(turma));
            if (_turmas.Any(t => t.Codigo == turma.Codigo)) return;
            _turmas.Add(turma);
        }

        public Nota LancarNota(Aluno aluno, Disciplina disciplina, double valor, TipoAvaliacao avaliacao, string periodoLectivo)
        {
            if (aluno == null) throw new ArgumentNullException(nameof(aluno));
            if (disciplina == null) throw new ArgumentNullException(nameof(disciplina));
            var nota = new Nota(aluno, disciplina, this, valor, avaliacao, periodoLectivo);
            return nota;
        }

        public Nota LancarNota(Aluno aluno, Disciplina disciplina, double valor)
        {
            return LancarNota(aluno, disciplina, valor, TipoAvaliacao.PrimeiraProva, "I Trimestre");
        }

        public override string ObterDetalhes()
        {
            return $"Docente: {Nome} | Código: {CodigoFuncionario} | Especialidade: {Especialidade} | Salário: {Salario:0.00} | Turmas: {Turmas.Count}";
        }
        #endregion
    }
}
 

