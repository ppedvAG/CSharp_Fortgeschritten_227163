using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;

namespace LinqErweiterungsmethoden;

internal class Program
{
	static void Main(string[] args)
	{
		#region Einfaches Linq
		List<int> ints = Enumerable.Range(1, 20).ToList();

		Console.WriteLine(ints.Average());
		Console.WriteLine(ints.Min());
		Console.WriteLine(ints.Max());
		Console.WriteLine(ints.Sum());

		Console.WriteLine(ints.First()); //Gibt das erste Element zurück, Exception wenn kein Element existiert oder wenn die Bedingung kein Element findet
		Console.WriteLine(ints.FirstOrDefault()); //Gibt das erste Element zurück, null wenn kein Element gefunden

		Console.WriteLine(ints.Last());
		Console.WriteLine(ints.LastOrDefault());

        //Console.WriteLine(ints.First(e => e % 50 == 0)); //Exception
        Console.WriteLine(ints.FirstOrDefault(e => e % 50 == 0)); //0
		#endregion

		List<Fahrzeug> fahrzeuge = new List<Fahrzeug>
		{
			new Fahrzeug(251, FahrzeugMarke.BMW),
			new Fahrzeug(274, FahrzeugMarke.BMW),
			new Fahrzeug(146, FahrzeugMarke.BMW),
			new Fahrzeug(208, FahrzeugMarke.Audi),
			new Fahrzeug(189, FahrzeugMarke.Audi),
			new Fahrzeug(133, FahrzeugMarke.VW),
			new Fahrzeug(253, FahrzeugMarke.VW),
			new Fahrzeug(304, FahrzeugMarke.BMW),
			new Fahrzeug(151, FahrzeugMarke.VW),
			new Fahrzeug(250, FahrzeugMarke.VW),
			new Fahrzeug(217, FahrzeugMarke.Audi),
			new Fahrzeug(125, FahrzeugMarke.Audi)
		};

		#region Vergleich Linq Schreibweisen
		//Alle BMWs finden
		List<Fahrzeug> bmwsForEach = new();
		foreach (Fahrzeug f in fahrzeuge)
			if (f.Marke == FahrzeugMarke.BMW)
				bmwsForEach.Add(f);

		//Standard-Linq: SQL-ähnliche Schreibweise (alt)
		List<Fahrzeug> bmwsAlt = (from f in fahrzeuge
								  where f.Marke == FahrzeugMarke.BMW
								  select f).ToList();

		//Methodenkette
		List<Fahrzeug> bmwsNeu = fahrzeuge.Where(e => e.Marke == FahrzeugMarke.BMW).ToList();
		#endregion

		#region	Linq mit Objektliste
		//Alle Fahrzeuge mit mindestens 200km/h
		fahrzeuge.Where(e => e.MaxV >= 200);

		//Alle VWs mit MaxV >= 200
		fahrzeuge.Where(e => e.MaxV >= 200 && e.Marke == FahrzeugMarke.VW);
		fahrzeuge.Where(e => e.MaxV >= 200).Where(e => e.Marke == FahrzeugMarke.VW);

		//Autos nach Marke sortieren
		fahrzeuge.OrderBy(e => e.Marke); //Originale Liste wird NICHT verändert
		fahrzeuge.OrderByDescending(e => e.Marke);

		//Autos nach Marke und danach nach Geschwindigkeit sortieren
		fahrzeuge.OrderBy(e => e.Marke).ThenBy(e => e.MaxV);
		fahrzeuge.OrderByDescending(e => e.Marke).ThenByDescending(e => e.MaxV);

		//Select: Liste umformen
		//Häufigster Anwendungfall: Einzelnes Feld extrahieren
		fahrzeuge.Select(e => e.Marke);
		fahrzeuge.Select(e => e.Marke).Distinct(); //Marken eindeutig machen
		fahrzeuge.DistinctBy(e => e.Marke); //Fahrzeuge anhand eines Kriteriums eindeutig machen
		fahrzeuge.Select(e => e.MaxV);

		//Praktisches Beispiel für Select
		string[] pfade = Directory.GetFiles(@"C:\Windows\System32");
		List<string> p = new();
		foreach (string f in pfade)
			p.Add(Path.GetFileNameWithoutExtension(f));

		List<string> p2 = Directory.GetFiles(@"C:\Windows\System32").Select(e => Path.GetFileNameWithoutExtension(e)).ToList();
        Console.WriteLine(p.SequenceEqual(p2));

		//Fahren alle unsere Autos mindestens 200km/h?
		fahrzeuge.All(e => e.MaxV >= 200);
		if (fahrzeuge.All(e => e.MaxV >= 200))
		{

		}

		//Fährt mindestens eines unserer Fahrzeuge mindestens 300km/h?
		fahrzeuge.Any(e => e.MaxV >= 300);

		fahrzeuge.Any(); //fahrzeuge.Count > 0

		//Wieviele Audis haben wir?
		fahrzeuge.Count(e => e.Marke == FahrzeugMarke.Audi);

		//Linq vereinfachen
		fahrzeuge.Select(e => e.MaxV).Average();
		fahrzeuge.Average(e => e.MaxV);

		fahrzeuge.Where(e => e.Marke == FahrzeugMarke.BMW).Count();
		fahrzeuge.Count(e => e.Marke == FahrzeugMarke.BMW);

		fahrzeuge.Where(e => e.MaxV >= 200).First();
		fahrzeuge.First(e => e.MaxV >= 200);

		//Min vs MinBy
		fahrzeuge.Min(e => e.MaxV); //Die kleinste Geschwindigkeit
		fahrzeuge.MinBy(e => e.MaxV); //Das Auto mit der kleinsten Geschwindigkeit

		fahrzeuge.Max(e => e.MaxV); //Die größte Geschwindigkeit
		fahrzeuge.MaxBy(e => e.MaxV); //Das Auto mit der größten Geschwindigkeit

		fahrzeuge.GroupBy(e => e.Marke); //Alle Objekte nach einem Kriterium gruppieren (Audi-Gruppe, BMW-Gruppe, VW-Gruppe)

		//ToDictionary: Wandelt ein IEnumerable zu einem Dictionary um
		Dictionary<FahrzeugMarke, List<Fahrzeug>> x = fahrzeuge.GroupBy(e => e.Marke).ToDictionary(e => e.Key, e => e.ToList());

		//Was ist das schnellste Fahrzeug pro Marke?
		Dictionary<FahrzeugMarke, Fahrzeug> schnellstesFahrzeug = fahrzeuge
			.GroupBy(e => e.Marke)
			.ToDictionary(e => e.Key, e => e.MaxBy(e => e.MaxV));

		foreach (var kv in schnellstesFahrzeug)
		{
            Console.WriteLine($"Das Fahrzeug hat die Marke {kv.Key} und kann maximal {kv.Value.MaxV}km/h fahren.\n");
        }

        //Aggregate: String Output anhand eines Linq Ergebnisses bauen
        Console.WriteLine
		(
			fahrzeuge
				.GroupBy(e => e.Marke)
				.ToDictionary(e => e.Key, e => e.MaxBy(e => e.MaxV))
				.Aggregate(string.Empty, (agg, element) => agg + $"Das Fahrzeug hat die Marke {element.Key} und kann maximal {element.Value.MaxV}km/h fahren.\n")
		);

		Console.WriteLine
		(
			fahrzeuge
				.GroupBy(e => e.Marke)
				.ToDictionary(e => e.Key, e => e.MaxBy(e => e.MaxV))
				.Aggregate(new StringBuilder(), (agg, element) => agg.Append($"Das Fahrzeug hat die Marke {element.Key} und kann maximal {element.Value.MaxV}km/h fahren.\n"))
				.ToString()
		);
		#endregion

		#region Erweiterungsmethoden
		int i = 327859;
		i.Quersumme();
        Console.WriteLine(32587.Quersumme());

		fahrzeuge.Shuffle();
		schnellstesFahrzeug.Shuffle();
		new int[] { 1, 2, 3 }.Shuffle();
        #endregion
    }
}

[DebuggerDisplay("Marke: {Marke}, Geschwindigkeit: {MaxV} - {typeof(Fahrzeug).FullName}")]
public record Fahrzeug(int MaxV, FahrzeugMarke Marke);

public enum FahrzeugMarke { Audi, BMW, VW }