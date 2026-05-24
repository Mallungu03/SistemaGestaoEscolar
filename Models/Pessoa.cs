using System;
using SistemaGestaoEscolar.Interfaces;
using SistemaGestaoEscolar.Enums;

namespace SistemaGestaoEscolar.Models
{
	/// <summary>
	/// Classe base abstrata para todas as pessoas do sistema.
	/// Demonstra abstração, encapsulamento e validação.
	/// </summary>
	public abstract class Pessoa : IDetalhes
	{
		#region Campos protegidos
		protected string _nome = string.Empty;
		protected DateTime _dataNascimento;
		protected Sexo _sexo;
		protected string _telefone = string.Empty;
		#endregion

		#region Propriedades
		/// <summary>
		/// Nome da pessoa. Não pode ser vazio.
		/// </summary>
		public string Nome
		{
			get => _nome;
			protected set
			{
				if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Nome não pode ser vazio.");
				_nome = value.Trim();
			}
		}

		/// <summary>
		/// Data de nascimento usada para calcular idade.
		/// </summary>
		public DateTime DataNascimento
		{
			get => _dataNascimento;
			protected set
			{
				if (value == DateTime.MinValue) throw new ArgumentException("Data de nascimento inválida.");
				_dataNascimento = value;
			}
		}

		/// <summary>
		/// Sexo da pessoa.
		/// </summary>
		public Sexo Sexo
		{
			get => _sexo;
			protected set => _sexo = value;
		}

		/// <summary>
		/// Telefone obrigatório.
		/// </summary>
		public string Telefone
		{
			get => _telefone;
			protected set
			{
				if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Telefone obrigatório.");
				_telefone = value.Trim();
			}
		}
		#endregion

		#region Construtores
		/// <summary>
		/// Construtor protegido para inicializar Pessoa.
		/// </summary>
		protected Pessoa(string nome, DateTime dataNascimento, string telefone, Sexo sexo)
		{
			Nome = nome;
			DataNascimento = dataNascimento;
			Telefone = telefone;
			Sexo = sexo;
		}
		#endregion

		#region Métodos
		/// <summary>
		/// Calcula a idade a partir da data de nascimento.
		/// </summary>
		public int CalcularIdade()
		{
			var hoje = DateTime.Today;
			var idade = hoje.Year - DataNascimento.Year;
			if (DataNascimento.Date > hoje.AddYears(-idade)) idade--;
			return idade;
		}

		/// <summary>
		/// Retorna uma string com os detalhes da pessoa.
		/// Implementação polimórfica: subclasses irão sobrescrever.
		/// </summary>
		public virtual string ObterDetalhes()
		{
			return $"Nome: {Nome} | Idade: {CalcularIdade()} | Sexo: {Sexo} | Telefone: {Telefone}";
		}
		#endregion
	}
}

