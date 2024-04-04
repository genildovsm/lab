using System.Collections;

public class Program
{
	public class Empresas
	{
		public int Id {get; set;}
		public string Nome {get; set;}
		public int? NumeroProcesso {get; set;}

		public Empresas(int id, string nome, int? numeroProcesso) { 
			Id = id;
			Nome = nome;
			NumeroProcesso = numeroProcesso;
		}

		public static List<Empresas> Obter ()
		{
			return new List<Empresas>()
			{
				new (1,"Sabesp",99999),
				new (3,"Americanas",null),
				new (2,"CPFL",77777),
				new (2,"CPFL",66666)
			};			
		}
	}

	public class ProcessoFisico
	{
		public int Id;
		public int Numero;
		public int Volume;
		public string Assunto;

		public ProcessoFisico(int id, int numero, int volume, string assunto)
		{
			Id = id;
			Numero = numero;
			Volume = volume;
			Assunto = assunto;
		}

		public static IEnumerable<ProcessoFisico> Obter()
		{
			yield return new ProcessoFisico(3,99999,1,"Sabesp Fisico A");
			yield return new ProcessoFisico(7,88888,1,"Sabesp Fisico B");			
			yield return new ProcessoFisico(4,77777,1,"CPFL Fisico A");
			yield return new ProcessoFisico(5,66666,1,"CPFL Fisico B");
		}
	}

	public class ViewModelProcesso
	{
		public int Id;
		public int? Numero;
		public int? Volume;
		public string? Assunto;
		public string? Midia;
		public string? NomeEmpresa;

		public ViewModelProcesso(){}

	}

	public static void Main(string[] args)
	{
		
		// var query = from e in Empresas.Obter()
		// 			join p in ProcessoFisico.Obter()
		// 			on e.NumeroProcesso equals p.Numero into juncao					
		// 			from processo in juncao.DefaultIfEmpty()
		// 			where e.Id == 2
					
		// 			select new ViewModelProcesso {
		// 				Midia = "Físico",
		// 				Numero = e.NumeroProcesso,
		// 				NomeEmpresa = e.Nome,
		// 				Assunto = processo.Assunto
		// 			};	

		var query = Empresas.Obter()
					.Where(e => e.Id == 3)
					.GroupJoin(ProcessoFisico.Obter(),
							e => e.NumeroProcesso,
							p => p.Numero,
							(e, juncao) => new { e, juncao })
					.SelectMany(x => x.juncao.DefaultIfEmpty(),
								(x, processo) => new ViewModelProcesso
								{
									Midia = "Físico",
									Numero = x.e.NumeroProcesso,
									NomeEmpresa = x.e.Nome,
									Assunto = processo?.Assunto
								});	

		foreach(var obj in query)
		{
			Console.WriteLine($"Processo Nº: {obj.Numero}\nNome Nº: {obj.NomeEmpresa}\nAssunto: {obj.Assunto}\n");
		}
		
	}
}
