using System;
using System.Collections.Generic;
using System.Linq;
using SistemaGestaoEscolar.Exceptions;
using SistemaGestaoEscolar.Enums;

namespace SistemaGestaoEscolar.Models
{
    /// <summary>
    /// Representa um aluno no sistema.
    /// </summary>
    public class Aluno : Pessoa
    {
        #region Propriedades
        public string NumeroMatricula { get; private set; }
        public string Curso { get; private set; }
        private readonly List<Nota> _notas = new List<Nota>();

        public IReadOnlyList<Nota> Notas => _notas.AsReadOnly();
        public double MediaFinal { get; private set; }
        #endregion

        #region Construtor
        public Aluno(string nome, DateTime dataNascimento, string telefone, Sexo sexo, string numeroMatricula, string curso)
            : base(nome, dataNascimento, telefone, sexo)
        {
            if (string.IsNullOrWhiteSpace(numeroMatricula)) throw new ArgumentException("Número de matrícula obrigatório.");
            if (string.IsNullOrWhiteSpace(curso)) throw new ArgumentException("Curso obrigatório.");
            NumeroMatricula = numeroMatricula.Trim();
            Curso = curso.Trim();
        }
        #endregion

        #region Métodos
        /// <summary>
        /// Adiciona uma nota ao aluno.
        /// </summary>
        public void AdicionarNota(Nota nota)
        {
            if (nota == null) throw new ArgumentNullException(nameof(nota));
            if (nota.Aluno != this) throw new InvalidOperationException("A nota não pertence a este aluno.");
            _notas.Add(nota);
            CalcularMedia();
        }

        /// <summary>
        /// Calcula a média final do aluno considerando todas as notas.
        /// </summary>
        public double CalcularMedia()
        {
            if (_notas.Count == 0) return 0.0;
            MediaFinal = Math.Round(_notas.Average(n => n.Valor), 2);
            return MediaFinal;
        }

        public double CalcularMedia(string codigoDisciplina)
        {
            var notas = ObterNotasPorDisciplina(codigoDisciplina);
            return notas.Count == 0 ? 0.0 : Math.Round(notas.Average(n => n.Valor), 2);
        }

        /// <summary>
        /// Verifica aprovação com média >= 10.
        /// </summary>
        public bool VerificarAprovacao()
        {
            return CalcularMedia() >= 10.0;
        }

        public bool VerificarAprovacao(string codigoDisciplina)
        {
            return CalcularMedia(codigoDisciplina) >= 10.0;
        }

        /// <summary>
        /// Retorna as notas do aluno para uma disciplina específica.
        /// </summary>
        public List<Nota> ObterNotasPorDisciplina(string codigoDisciplina)
        {
            if (string.IsNullOrWhiteSpace(codigoDisciplina)) return new List<Nota>();
            return _notas.Where(n => n.Disciplina != null && n.Disciplina.Codigo == codigoDisciplina).ToList();
        }

        /// <summary>
        /// Obter detalhes do aluno (override polimórfico).
        /// </summary>
        public override string ObterDetalhes()
        {
            return $"Aluno: {Nome} | Matrícula: {NumeroMatricula} | Curso: {Curso} | Média: {MediaFinal:0.00}";
        }
        #endregion
    }
}

