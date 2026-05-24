# Sistema de Gestao Escolar

Aplicacao de consola em C# desenvolvida para a avaliacao pratica de Programacao Orientada a Objectos II.

O projecto implementa um sistema de gestao escolar para registar alunos, docentes, turmas, disciplinas, atribuicoes pedagogicas, notas, pautas e calculo de medias.

## Requisitos

- .NET 6 ou superior
- Terminal ou Visual Studio Code com extensao C#

O projecto nao usa base de dados externa nem pacotes NuGet de terceiros. Os dados sao mantidos em memoria com coleccoes genericas.

## Como Executar

Na pasta raiz do projecto, execute:

```bash
dotnet build
dotnet run
```

## Funcionalidades

- Cadastrar alunos
- Cadastrar docentes
- Criar turmas
- Criar disciplinas
- Atribuir docentes a turmas e disciplinas
- Matricular alunos em turmas
- Lancar notas por avaliacao e periodo lectivo
- Gerar pautas por turma e disciplina
- Calcular medias
- Verificar aprovacao ou reprovacao
- Listar alunos, docentes, turmas e disciplinas
- Consultar ranking e estatisticas

## Conceitos de POO Aplicados

- Encapsulamento
- Heranca: `Pessoa -> Aluno` e `Pessoa -> Docente`
- Polimorfismo com `ObterDetalhes()`
- Classe abstracta: `Pessoa`
- Interface: `IDetalhes`
- Agregacao e composicao
- Coleccoes genericas
- Excecoes personalizadas
- Validacao de dados
- Separacao de responsabilidades

## Estrutura

```text
Enums/        Enumeracoes do sistema
Exceptions/  Excecoes personalizadas
Interfaces/  Contratos/interfaces
Models/      Classes de dominio
Services/    Logica de negocio
Utils/       Utilitarios de consola e validacao
Docs/        Relatorio, UML e casos de teste
Program.cs   Menu e interaccao com o utilizador
```

## Teste Rapido

Um fluxo basico para demonstracao:

1. Cadastrar um aluno.
2. Cadastrar um docente.
3. Criar uma turma.
4. Criar uma disciplina.
5. Atribuir o docente a turma/disciplina.
6. Matricular o aluno.
7. Lancar uma nota valida.
8. Gerar a pauta por turma e disciplina.

