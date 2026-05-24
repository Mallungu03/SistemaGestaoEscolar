using System;
using SistemaGestaoEscolar.Enums;

namespace SistemaGestaoEscolar.Utils
{
    public static class ConsoleHelper
    {
        public static void EscreverTitulo(string titulo)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("========================================================================");
            Console.WriteLine($"\t\t\t{titulo}");
            Console.WriteLine("========================================================================");
            Console.ResetColor();
        }

        public static void EscreverSucesso(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(msg);
            Console.ResetColor();
        }

        public static void EscreverErro(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(msg);
            Console.ResetColor();
        }

        public static string LerTexto(string prompt)
        {
            while (true)
            {
                Console.Write(prompt + ": ");
                var valor = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(valor)) return valor.Trim();
                EscreverErro("Entrada obrigatória.");
            }
        }

        public static int LerInteiro(string prompt)
        {
            while (true)
            {
                Console.Write(prompt + ": ");
                var s = Console.ReadLine();
                if (int.TryParse(s, out var v)) return v;
                EscreverErro("Entrada inválida. Introduza um número inteiro.");
            }
        }

        public static double LerDouble(string prompt)
        {
            while (true)
            {
                Console.Write(prompt + ": ");
                var s = Console.ReadLine();
                if (double.TryParse(s, out var v)) return v;
                EscreverErro("Entrada inválida. Introduza um número decimal.");
            }
        }

        public static DateTime LerData(string prompt)
        {
            while (true)
            {
                Console.Write(prompt + " (yyyy-MM-dd): ");
                var s = Console.ReadLine();
                if (DateTime.TryParse(s, out var data) && data.Date < DateTime.Today) return data;
                EscreverErro("Data inválida. Use uma data anterior à data actual.");
            }
        }

        public static Sexo LerSexo(string prompt)
        {
            while (true)
            {
                var sexo = LerTexto(prompt + " (Masculino/Feminino)").ToLowerInvariant();
                if (sexo.StartsWith("m")) return Sexo.Masculino;
                if (sexo.StartsWith("f")) return Sexo.Feminino;
                EscreverErro("Sexo inválido.");
            }
        }

        public static TipoAvaliacao LerTipoAvaliacao(string prompt)
        {
            while (true)
            {
                Console.WriteLine("1 - Primeira Prova");
                Console.WriteLine("2 - Segunda Prova");
                Console.WriteLine("3 - Trabalho");
                Console.WriteLine("4 - Exame");
                var opcao = LerInteiro(prompt);
                switch (opcao)
                {
                    case 1: return TipoAvaliacao.PrimeiraProva;
                    case 2: return TipoAvaliacao.SegundaProva;
                    case 3: return TipoAvaliacao.Trabalho;
                    case 4: return TipoAvaliacao.Exame;
                    default: EscreverErro("Tipo de avaliação inválido."); break;
                }
            }
        }

        public static void Pausar()
        {
            Console.WriteLine();
            Console.Write("Prima qualquer tecla para continuar...");
            Console.ReadKey();
        }
    }
}
