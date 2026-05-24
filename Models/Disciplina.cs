using System;
using System.Collections.Generic;
using System.Linq;

namespace SistemaGestaoEscolar.Models
{
    public class Disciplina
    {
        public string Codigo { get; private set; }
        public string Nome { get; private set; }
        public int CargaHoraria { get; private set; }
        private readonly List<Docente> _docentes = new List<Docente>();

        public IReadOnlyList<Docente> Docentes => _docentes.AsReadOnly();

        public Disciplina(string codigo, string nome, int cargaHoraria)
        {
            if (string.IsNullOrWhiteSpace(codigo)) throw new ArgumentException("Código obrigatório.");
            if (string.IsNullOrWhiteSpace(nome)) throw new ArgumentException("Nome obrigatório.");
            if (cargaHoraria <= 0) throw new ArgumentException("Carga horária deve ser maior que zero.");
            Codigo = codigo.Trim();
            Nome = nome.Trim();
            CargaHoraria = cargaHoraria;
        }

        public string ExibirDisciplina()
        {
            return $"{Codigo} - {Nome} ({CargaHoraria}h)";
        }

        public void AtribuirDocente(Docente docente)
        {
            if (docente == null) throw new ArgumentNullException(nameof(docente));
            if (_docentes.Any(d => d.CodigoFuncionario == docente.CodigoFuncionario)) return;
            _docentes.Add(docente);
        }
    }

}

