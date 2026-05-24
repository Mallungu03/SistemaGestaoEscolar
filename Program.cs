using System;
using SistemaGestaoEscolar.Services;
using SistemaGestaoEscolar.Utils;
using System.Linq;

namespace SistemaGestaoEscolar
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var gestor = new GestorEscolar();
            while (true)
            {
                ConsoleHelper.EscreverTitulo("SISTEMA DE GESTÃO ESCOLAR");
                Console.WriteLine("1 - Cadastrar Aluno");
                Console.WriteLine("2 - Cadastrar Docente");
                Console.WriteLine("3 - Criar Turma");
                Console.WriteLine("4 - Criar Disciplina");
                Console.WriteLine("5 - Atribuir Docente a Turma/Disciplina");
                Console.WriteLine("6 - Matricular Aluno");
                Console.WriteLine("7 - Lançar Nota");
                Console.WriteLine("8 - Ver Pauta");
                Console.WriteLine("9 - Listar Alunos");
                Console.WriteLine("10 - Listar Docentes");
                Console.WriteLine("11 - Listar Turmas");
                Console.WriteLine("12 - Listar Disciplinas");
                Console.WriteLine("13 - Calcular Média Geral");
                Console.WriteLine("14 - Ranking dos melhores");
                Console.WriteLine("15 - Estatísticas de disciplina");
                Console.WriteLine("0 - Sair");
                var opc = ConsoleHelper.LerInteiro("Escolha uma opção");
                try
                {
                    switch (opc)
                    {
                        case 1:
                            CadastrarAluno(gestor);
                            break;
                        case 2:
                            CadastrarDocente(gestor);
                            break;
                        case 3:
                            CriarTurma(gestor);
                            break;
                        case 4:
                            CriarDisciplina(gestor);
                            break;
                        case 5:
                            AtribuirDocenteTurmaDisciplina(gestor);
                            break;
                        case 6:
                            MatricularAluno(gestor);
                            break;
                        case 7:
                            LancarNota(gestor);
                            break;
                        case 8:
                            VerPauta(gestor);
                            break;
                        case 9:
                            ListarAlunos(gestor);
                            break;
                        case 10:
                            ListarDocentes(gestor);
                            break;
                        case 11:
                            ListarTurmas(gestor);
                            break;
                        case 12:
                            ListarDisciplinas(gestor);
                            break;
                        case 13:
                            ConsoleHelper.EscreverSucesso($"Média Geral: {gestor.CalcularMediaGeral():0.00}");
                            ConsoleHelper.Pausar();
                            break;
                        case 14:
                            Ranking(gestor);
                            break;
                        case 15:
                            EstatisticasDisciplina(gestor);
                            break;
                        case 0:
                            return;
                        default:
                            ConsoleHelper.EscreverErro("Opção inválida.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    ConsoleHelper.EscreverErro($"Erro: {ex.Message}");
                    ConsoleHelper.Pausar();
                }
                finally
                {
                    Console.ResetColor();
                }
            }
        }

        static void CadastrarAluno(GestorEscolar gestor)
        {
            ConsoleHelper.EscreverTitulo("Cadastrar Aluno");
            var nome = ConsoleHelper.LerTexto("Nome");
            var data = ConsoleHelper.LerData("Data Nascimento");
            var telefone = ConsoleHelper.LerTexto("Telefone");
            var sexo = ConsoleHelper.LerSexo("Sexo");
            var matricula = ConsoleHelper.LerTexto("Número Matrícula");
            var curso = ConsoleHelper.LerTexto("Curso");
            gestor.CadastrarAluno(nome, data, telefone, sexo, matricula, curso);
            ConsoleHelper.EscreverSucesso("Aluno cadastrado com sucesso.");
            ConsoleHelper.Pausar();
        }

        static void CadastrarDocente(GestorEscolar gestor)
        {
            ConsoleHelper.EscreverTitulo("Cadastrar Docente");
            var nome = ConsoleHelper.LerTexto("Nome");
            var data = ConsoleHelper.LerData("Data Nascimento");
            var telefone = ConsoleHelper.LerTexto("Telefone");
            var sexo = ConsoleHelper.LerSexo("Sexo");
            var codigo = ConsoleHelper.LerTexto("Código Funcionário");
            var esp = ConsoleHelper.LerTexto("Especialidade");
            var salario = ConsoleHelper.LerDouble("Salário");
            gestor.CadastrarDocente(nome, data, telefone, sexo, codigo, esp, salario);
            ConsoleHelper.EscreverSucesso("Docente cadastrado com sucesso.");
            ConsoleHelper.Pausar();
        }

        static void CriarTurma(GestorEscolar gestor)
        {
            ConsoleHelper.EscreverTitulo("Criar Turma");
            var codigo = ConsoleHelper.LerTexto("Código");
            var classe = ConsoleHelper.LerTexto("Classe");
            var sala = ConsoleHelper.LerTexto("Sala");
            var capacidade = ConsoleHelper.LerInteiro("Capacidade");
            gestor.CriarTurma(codigo, classe, sala, capacidade);
            ConsoleHelper.EscreverSucesso("Turma criada com sucesso.");
            ConsoleHelper.Pausar();
        }

        static void CriarDisciplina(GestorEscolar gestor)
        {
            ConsoleHelper.EscreverTitulo("Criar Disciplina");
            var codigo = ConsoleHelper.LerTexto("Código");
            var nome = ConsoleHelper.LerTexto("Nome");
            var carga = ConsoleHelper.LerInteiro("Carga Horária");
            gestor.CriarDisciplina(codigo, nome, carga);
            ConsoleHelper.EscreverSucesso("Disciplina criada com sucesso.");
            ConsoleHelper.Pausar();
        }

        static void AtribuirDocenteTurmaDisciplina(GestorEscolar gestor)
        {
            ConsoleHelper.EscreverTitulo("Atribuir Docente a Turma/Disciplina");
            var codigoDocente = ConsoleHelper.LerTexto("Código Docente");
            var codigoTurma = ConsoleHelper.LerTexto("Código Turma");
            var codigoDisciplina = ConsoleHelper.LerTexto("Código Disciplina");
            gestor.AtribuirDocenteATurmaDisciplina(codigoDocente, codigoTurma, codigoDisciplina);
            ConsoleHelper.EscreverSucesso("Docente, turma e disciplina associados com sucesso.");
            ConsoleHelper.Pausar();
        }

        static void MatricularAluno(GestorEscolar gestor)
        {
            ConsoleHelper.EscreverTitulo("Matricular Aluno");
            var codigoTurma = ConsoleHelper.LerTexto("Código Turma");
            var matricula = ConsoleHelper.LerTexto("Número Matrícula");
            gestor.MatricularAluno(codigoTurma, matricula);
            ConsoleHelper.EscreverSucesso("Aluno matriculado com sucesso.");
            ConsoleHelper.Pausar();
        }

        static void LancarNota(GestorEscolar gestor)
        {
            ConsoleHelper.EscreverTitulo("Lançar Nota");
            var codigoDisc = ConsoleHelper.LerTexto("Código Disciplina");
            var matricula = ConsoleHelper.LerTexto("Número Matrícula");
            var codigoDoc = ConsoleHelper.LerTexto("Código Docente");
            var avaliacao = ConsoleHelper.LerTipoAvaliacao("Tipo de avaliação");
            var periodo = ConsoleHelper.LerTexto("Período lectivo");
            var valor = ConsoleHelper.LerDouble("Valor (0-20)");
            gestor.LancarNota(codigoDisc, matricula, codigoDoc, valor, avaliacao, periodo);
            ConsoleHelper.EscreverSucesso("Nota lançada com sucesso.");
            ConsoleHelper.Pausar();
        }

        static void VerPauta(GestorEscolar gestor)
        {
            ConsoleHelper.EscreverTitulo("Ver Pauta");
            var codigoTurma = ConsoleHelper.LerTexto("Código Turma");
            var codigoDisc = ConsoleHelper.LerTexto("Código Disciplina");
            var pauta = gestor.GerarPauta(codigoTurma, codigoDisc);
            foreach (var linha in pauta) Console.WriteLine(linha);
            ConsoleHelper.Pausar();
        }

        static void ListarAlunos(GestorEscolar gestor)
        {
            ConsoleHelper.EscreverTitulo("Listar Alunos");
            foreach (var a in gestor.ListarAlunos()) Console.WriteLine(a.ObterDetalhes());
            ConsoleHelper.Pausar();
        }

        static void ListarDocentes(GestorEscolar gestor)
        {
            ConsoleHelper.EscreverTitulo("Listar Docentes");
            foreach (var d in gestor.ListarDocentes()) Console.WriteLine(d.ObterDetalhes());
            ConsoleHelper.Pausar();
        }

        static void ListarTurmas(GestorEscolar gestor)
        {
            ConsoleHelper.EscreverTitulo("Listar Turmas");
            foreach (var turma in gestor.ListarTurmas())
            {
                Console.WriteLine($"{turma.Codigo} - {turma.Classe} | Sala: {turma.Sala} | Alunos: {turma.Alunos.Count}/{turma.Capacidade} | Disciplinas: {turma.Disciplinas.Count}");
            }
            ConsoleHelper.Pausar();
        }

        static void ListarDisciplinas(GestorEscolar gestor)
        {
            ConsoleHelper.EscreverTitulo("Listar Disciplinas");
            foreach (var disciplina in gestor.ListarDisciplinas())
            {
                var docentes = disciplina.Docentes.Count == 0
                    ? "Sem docente"
                    : string.Join(", ", disciplina.Docentes.Select(d => d.Nome));

                Console.WriteLine($"{disciplina.ExibirDisciplina()} | Docente(s): {docentes}");
            }
            ConsoleHelper.Pausar();
        }

        static void Ranking(GestorEscolar gestor)
        {
            ConsoleHelper.EscreverTitulo("Ranking dos Melhores Alunos");
            var top = gestor.RankingMelhores(10);
            int i = 1;
            foreach (var a in top)
            {
                Console.WriteLine($"{i++}. {a.Nome} - Média: {a.CalcularMedia():0.00}");
            }
            ConsoleHelper.Pausar();
        }

        static void EstatisticasDisciplina(GestorEscolar gestor)
        {
            ConsoleHelper.EscreverTitulo("Estatísticas de Disciplina");
            var codigo = ConsoleHelper.LerTexto("Código Disciplina");
            var (ap, rep) = gestor.ContagemAprovadosReprovados(codigo);
            Console.WriteLine($"Aprovados: {ap} | Reprovados: {rep}");
            var medias = gestor.ListarTurmas().Select(t => t.MediaTurma(codigo)).ToList();
            Console.WriteLine($"Média da(s) turma(s): {(medias.Count==0?0:medias.Average()):0.00}");
            ConsoleHelper.Pausar();
        }
    }
}
