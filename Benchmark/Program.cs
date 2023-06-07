using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Order;
using BenchmarkDotNet.Running;
using System.Text;

namespace Benchmark;

internal class Program
{
	static void Main(string[] args)
	{
		BenchmarkRunner.Run<Benchmarks>();
		//Benchmarks debuggen
		//BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args, new DebugInProcessConfig());
	}
}

[MemoryDiagnoser(false)]
[RankColumn]
//[Orderer(SummaryOrderPolicy.FastestToSlowest)]
public class Benchmarks
{
	List<Fahrzeug> Fahrzeuge = new();

	[Params(10000, 50000, 100000)]
	public int Anzahl;

	[GlobalSetup]
	public void Setup()
	{
		Fahrzeuge = new();
		Random rnd = new Random();
		for (int i = 0; i < Anzahl; i++)
		{
			Fahrzeug f = new(i, rnd.Next(100, 500), (FahrzeugMarke) rnd.Next(0, 3));
			Fahrzeuge.Add(f);
		}
	}

	[GlobalCleanup]
	public void Cleanup() 
	{
		Fahrzeuge = null;
	}

	//[Benchmark]
	[IterationCount(30)]
	public void StringPlus()
	{
		string str = string.Empty;
		for (int i = 0; i < 10000; i++)
		{
			str += i;
		}
	}

	//[Benchmark]
	[IterationCount(30)]
	public void SBuilder()
	{
		StringBuilder sb = new StringBuilder();
		for (int i = 0; i < 10000; i++)
		{
			sb.Append(i);
		}
		string str = sb.ToString();
	}

	[Benchmark]
	[IterationCount(30)]
	public void ForEach()
	{
		List<Fahrzeug> bmwsForEach = new();
		foreach (Fahrzeug f in Fahrzeuge)
			if (f.Marke == FahrzeugMarke.BMW)
				bmwsForEach.Add(f);
	}

	[Benchmark]
	[IterationCount(30)]
	public void StandardLinq()
	{
		List<Fahrzeug> bmwsAlt = (from f in Fahrzeuge
								  where f.Marke == FahrzeugMarke.BMW
								  select f).ToList();
	}

	[Benchmark]
	[IterationCount(30)]
	public void Linq()
	{
		List<Fahrzeug> bmwsNeu = Fahrzeuge.Where(e => e.Marke == FahrzeugMarke.BMW).ToList();
	}
}

public class Fahrzeug
{
	public int ID;
	public int MaxGeschwindigkeit;
	public FahrzeugMarke Marke;
	public List<Sitzplatz> Sitze;

	public Fahrzeug(int id, int v, FahrzeugMarke fm)
	{
		ID = id;
		MaxGeschwindigkeit = v;
		Marke = fm;
		Sitze = new();

		//Anzahl Sitzplätze anhand der Geschwindigkeit (6: max 150km/h, 5 bis 250km/h, 4 ab 250km/h)
		int sitze = v <= 150 ? 6 : v <= 250 ? 5 : 4;

		//Sitzplätze erstellen
		for (int i = 0; i < sitze; i++)
			Sitze.Add(new Sitzplatz());

		//Sitzplätze semi-zufällig belegen damit die Übung zwischen Teilnehmern gleiche Ergebnisse liefert
		//Geschwindigkeit modulo Anzahl Sitzplätze besetzen
		for (int i = 0; i < v % (sitze + 1); i++)
			Sitze[i].IstBesetzt = true;
	}
}

public class Sitzplatz
{
	public bool IstBesetzt;
}

public enum FahrzeugMarke
{
	Audi, BMW, VW
}