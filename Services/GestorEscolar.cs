using System;
using System.Collections.Generic;
using System.Linq;
using SistemaGestaoEscolar.Models;
using SistemaGestaoEscolar.Exceptions;
using SistemaGestaoEscolar.Enums;

namespace SistemaGestaoEscolar.Services
{
    <Summary>
    Classe central que gerencia as operações do sistema de gestão escolar, incluindo cadastro, atribuições pedagógicas, matrículas, lançamento de notas e geração de relatórios.
    Demonstra o uso de coleções, LINQ, tratamento de exceções e organização de código em regiões.
    </ Summary>
    
    public class GestorEscolar
    {
        private readonly List<Aluno> _alunos = new List<Aluno>();
        private readonly List<Docente> _docentes = new List<Docente>();
        private readonly List<Turma> _turmas = new List<Turma>();
        private readonly List<Disciplina> _disciplinas = new List<Disciplina>();
        private readonly List<Nota> _notas = new List<Nota>();

        public IReadOnlyList<Aluno> Alunos => _alunos.AsReadOnly();
        public IReadOnlyList<Docente> Docentes => _docentes.AsReadOnly();
        public IReadOnlyList<Turma> Turmas => _turmas.AsReadOnly();
        public IReadOnlyList<Disciplina> Disciplinas => _disciplinas.AsReadOnly();
        public IReadOnlyList<Nota> Notas => _notas.AsReadOnly();

        #region Cadastro
        public Aluno CadastrarAluno(string nome, DateTime dataNascimento, string telefone, Enums.Sexo sexo, string numeroMatricula, string curso)
        {
            if (_alunos.Any(a => a.NumeroMatricula == numeroMatricula))
                throw new AlunoJaMatriculadoException("Já existe um aluno com este número de matrícula.");

            var aluno = new Aluno(nome, dataNascimento, telefone, sexo, numeroMatricula, curso);
            _alunos.Add(aluno);
            return aluno;
        }

        public Docente CadastrarDocente(string nome, DateTime dataNascimento, string telefone, Enums.Sexo sexo, string codigoFuncionario, string especialidade, double salario)
        {
            if (_docentes.Any(d => d.CodigoFuncionario == codigoFuncionario))
                throw new ArgumentException("Já existe um docente com este código.");

            var docente = new Docente(nome, dataNascimento, telefone, sexo, codigoFuncionario, especialidade, salario);
            _docentes.Add(docente);
            return docente;
        }

        public Turma CriarTurma(string codigo, string classe, string sala, int capacidade)
        {
            if (_turmas.Any(t => t.Codigo == codigo))
                throw new ArgumentException("Já existe uma turma com este código.");

            var turma = new Turma(codigo, classe, sala, capacidade);
            _turmas.Add(turma);
            return turma;
        }

        public Disciplina CriarDisciplina(string codigo, string nome, int cargaHoraria)
        {
            if (_disciplinas.Any(d => d.Codigo == codigo))
                throw new ArgumentException("Já existe uma disciplina com este código.");

            var d = new Disciplina(codigo, nome, cargaHoraria);
            _disciplinas.Add(d);
            return d;
        }
        #endregion

        #region Atribuições pedagógicas
        public void AtribuirDisciplinaATurma(string codigoTurma, string codigoDisciplina)
        {
            var turma = ObterTurmaObrigatoria(codigoTurma);
            var disciplina = ObterDisciplinaObrigatoria(codigoDisciplina);
            turma.AdicionarDisciplina(disciplina);
        }

        public void AtribuirDocenteADisciplina(string codigoDocente, string codigoDisciplina)
        {
            var docente = ObterDocenteObrigatorio(codigoDocente);
            var disciplina = ObterDisciplinaObrigatoria(codigoDisciplina);
            docente.AtribuirDisciplina(disciplina);
        }

        public void AtribuirDocenteATurmaDisciplina(string codigoDocente, string codigoTurma, string codigoDisciplina)
        {
            AtribuirDocenteADisciplina(codigoDocente, codigoDisciplina);
            AtribuirDisciplinaATurma(codigoTurma, codigoDisciplina);
            ObterDocenteObrigatorio(codigoDocente).AtribuirTurma(ObterTurmaObrigatoria(codigoTurma));
        }
        #endregion

        #region Matrícula e Notas
        public void MatricularAluno(string codigoTurma, string numeroMatricula)
        {
            var turma = ObterTurmaObrigatoria(codigoTurma);
            var aluno = ObterAlunoObrigatorio(numeroMatricula);
            turma.AdicionarAluno(aluno);
        }

        public Nota LancarNota(string codigoDisciplina, string numeroMatricula, string codigoDocente, double valor, TipoAvaliacao avaliacao, string periodoLectivo)
        {
            var disciplina = ObterDisciplinaObrigatoria(codigoDisciplina);
            var aluno = ObterAlunoObrigatorio(numeroMatricula);
            var docente = ObterDocenteObrigatorio(codigoDocente);

            if (!docente.LecionaDisciplina(codigoDisciplina))
                throw new ArgumentException("Este docente ainda não foi atribuído a esta disciplina.");

            if (!AlunoEstaMatriculadoEmTurmaComDisciplina(aluno, codigoDisciplina))
                throw new ArgumentException("O aluno não está matriculado numa turma que tenha esta disciplina.");

            var nota = docente.LancarNota(aluno, disciplina, valor, avaliacao, periodoLectivo);
            _notas.Add(nota);
            aluno.AdicionarNota(nota);
            return nota;
        }

        public Nota LancarNota(string codigoDisciplina, string numeroMatricula, string codigoDocente, double valor)
        {
            return LancarNota(codigoDisciplina, numeroMatricula, codigoDocente, valor, TipoAvaliacao.PrimeiraProva, "I Trimestre");
        }
        #endregion

        #region Listagens e pesquisas
        public Aluno? PesquisarAlunoPorMatricula(string numeroMatricula)
        {
            return _alunos.FirstOrDefault(a => a.NumeroMatricula == numeroMatricula);
        }

        public IEnumerable<string> GerarPauta(string codigoDisciplina)
        {
            var disciplina = ObterDisciplinaObrigatoria(codigoDisciplina);
            var pauta = new List<string>();
            foreach (var aluno in Alunos)
            {
                var media = aluno.CalcularMedia(codigoDisciplina);
                pauta.Add($"{aluno.NumeroMatricula} - {aluno.Nome} - Média: {media:0.00} - {(media>=10?"Aprovado":"Reprovado")}");
            }
            return pauta;
        }

        public IEnumerable<string> GerarPauta(string codigoTurma, string codigoDisciplina)
        {
            var turma = ObterTurmaObrigatoria(codigoTurma);
            var disciplina = ObterDisciplinaObrigatoria(codigoDisciplina);

            if (!turma.TemDisciplina(codigoDisciplina))
                throw new ArgumentException("A disciplina não está atribuída a esta turma.");

            var pauta = new List<string>
            {
                $"Pauta da Turma {turma.Codigo} - {turma.Classe} | Disciplina: {disciplina.Nome}"
            };

            foreach (var aluno in turma.Alunos.OrderBy(a => a.Nome))
            {
                var media = aluno.CalcularMedia(codigoDisciplina);
                pauta.Add($"{aluno.NumeroMatricula} - {aluno.Nome} - Média: {media:0.00} - {(media >= 10 ? "Aprovado" : "Reprovado")}");
            }

            return pauta;
        }

        public IEnumerable<Aluno> ListarAlunos() => _alunos.OrderBy(a => a.Nome);
        public IEnumerable<Docente> ListarDocentes() => _docentes.OrderBy(d => d.Nome);
        public IEnumerable<Turma> ListarTurmas() => _turmas.OrderBy(t => t.Codigo);
        public IEnumerable<Disciplina> ListarDisciplinas() => _disciplinas.OrderBy(d => d.Nome);
        #endregion

        #region Estatísticas
        public double CalcularMediaGeral()
        {
            if (_notas.Count == 0) return 0.0;
            return Math.Round(_notas.Average(n => n.Valor),2);
        }

        public IEnumerable<Aluno> RankingMelhores(int top = 5)
        {
            return _alunos.OrderByDescending(a => a.CalcularMedia()).Take(top);
        }

        public (int aprovados, int reprovados) ContagemAprovadosReprovados(string codigoDisciplina)
        {
            ObterDisciplinaObrigatoria(codigoDisciplina);
            int aprov = 0, rep = 0;
            foreach (var aluno in _alunos)
            {
                var notas = aluno.ObterNotasPorDisciplina(codigoDisciplina);
                var media = notas.Count==0?0:notas.Average(n=>n.Valor);
                if (media>=10) aprov++; else rep++;
            }
            return (aprov, rep);
        }
        #endregion

        #region Pesquisas internas
        private Aluno ObterAlunoObrigatorio(string numeroMatricula)
        {
            return _alunos.FirstOrDefault(a => a.NumeroMatricula == numeroMatricula)
                ?? throw new ArgumentException("Aluno não encontrado.");
        }

        private Docente ObterDocenteObrigatorio(string codigoDocente)
        {
            return _docentes.FirstOrDefault(dc => dc.CodigoFuncionario == codigoDocente)
                ?? throw new ArgumentException("Docente não encontrado.");
        }

        private Turma ObterTurmaObrigatoria(string codigoTurma)
        {
            return _turmas.FirstOrDefault(t => t.Codigo == codigoTurma)
                ?? throw new ArgumentException("Turma não encontrada.");
        }

        private Disciplina ObterDisciplinaObrigatoria(string codigoDisciplina)
        {
            return _disciplinas.FirstOrDefault(d => d.Codigo == codigoDisciplina)
                ?? throw new ArgumentException("Disciplina não encontrada.");
        }

        private bool AlunoEstaMatriculadoEmTurmaComDisciplina(Aluno aluno, string codigoDisciplina)
        {
            return _turmas.Any(t => t.TemAluno(aluno.NumeroMatricula) && t.TemDisciplina(codigoDisciplina));
        }
        #endregion
    }
}
