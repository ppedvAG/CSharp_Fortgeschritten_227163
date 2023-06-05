namespace DelegatesEvents;

internal class Delegates
{
	public delegate void Vorstellungen(string name); //Definition vom Delegate, speichert Referenzen auf Methoden (Methodenzeiger)

	static void Main(string[] args)
	{
		Vorstellungen vorstellungen = new Vorstellungen(VorstellungDE); //Erstellung von Delegate + Initialmethode
		vorstellungen("Max"); //Delegate ausführen

		vorstellungen += new Vorstellungen(VorstellungEN); //Methode anhängen (lang)
		vorstellungen += VorstellungEN; //Methode anhängen (kurz, selber Effekt wie oben)
		vorstellungen("Lukas"); //Selbe Methode kann mehrmals angehängt werden

		vorstellungen -= VorstellungDE; //Methode abhängen
		vorstellungen -= VorstellungDE; //Keine Fehler, wenn die Methode nicht dran ist
		vorstellungen -= VorstellungDE;
		vorstellungen("Stefan");

		vorstellungen -= VorstellungEN;
		vorstellungen -= VorstellungEN; //Wenn die letzte Methode abgenommen wird, ist das Delegate null

		if (vorstellungen is not null)
			vorstellungen("Max");

		//Null Propagation: Schneller Null-Check, mit ? vor Funktionsaufruf
		vorstellungen?.Invoke("Max");

		List<int> ints = null;
		ints?.Add(1);

		foreach (Delegate dg in vorstellungen.GetInvocationList()) //Delegate anschauen
		{
            Console.WriteLine(dg.Method.Name);
        }
	}

	static void VorstellungDE(string name)
	{
		Console.WriteLine($"Hallo mein Name ist {name}");
	}

	static void VorstellungEN(string name)
	{
		Console.WriteLine($"Hello my name is {name}");
	}
}