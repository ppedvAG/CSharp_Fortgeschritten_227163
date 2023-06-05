namespace DelegatesEvents;

internal class ActionPredicateFunc
{
	static List<int> TestList = new();

	static void Main(string[] args)
	{
		//Action, Predicate, Func: Vordefinierte Delegates die in vielen Stellen in C# eingebaut sind
		//Essentiell für die Fortgeschrittene Programmierung
		//Können alles was in dem vorherigen Kapitel steht

		//Action: Methode mit void und bis zu 16 Parametern
		Action<int, int> action = Addiere;
		action(5, 5);
		action?.Invoke(4, 5);

		DoAction(4, 7, Addiere); //Das Verhalten der Methode über den Action Parameter anpassen
		DoAction(4, 7, Subtrahiere);
		DoAction(4, 7, action);

		TestList.ForEach(Quadriere); //Hier bei ForEach die Funktion anpassen
		void Quadriere(int x) => Console.WriteLine(x * x);

		//Func: Methode mit Rückgabewert und bis zu 16 Parametern
		Func<int, int, double> func = Multipliziere;
		double d = func(4, 6); //Ergebnis der Func in eine Variable schreiben
		double? d2 = func?.Invoke(4, 6); //double?: Nullable Double (Invoke könnte null zurückgeben wenn func null ist)
		double d3 = d2 == null ? double.NaN : d2.Value;

		DoFunc(3, 4, Multipliziere);
		DoFunc(3, 4, Dividiere);
		DoFunc(3, 4, func);

		func += delegate (int x, int y) { return x + y; }; //Anonyme Methode

		func += (int x, int y) => { return x + y; }; //Kürzere Form

		func += (x, y) => { return x - y; };

		func += (x, y) => (double) x / y; //Kürzeste, häufigste Form

		//Anwendung von anonymen Funktionen
		DoAction(3, 4, (x, y) => Console.WriteLine($"Das Ergebnis ist {x + y}"));
		DoAction(3, 4, (x, y) => File.WriteAllText("Output.txt", "" + x + y));
		DoFunc(4, 7, (x, y) => (double) x % y);
		DoFunc(4, 7, (x, y) => (double) x * y);

		TestList.Where(e => e % 2 == 0);
		TestList.Find(e => e % 2 == 0);
		TestList.ForEach(e => Console.WriteLine(e));
	}

	#region Action
	static void Addiere(int x, int y) => Console.WriteLine(x + y);

	static void Subtrahiere(int x, int y) => Console.WriteLine(x - y);

	static void DoAction(int x, int y, Action<int, int> action) => action(x, y);
	#endregion

	#region Func
	static double Multipliziere(int arg1, int arg2) => arg1 * arg2;

	static double Dividiere(int arg1, int arg2) => (double) arg1 / arg2;

	static double DoFunc(int x, int y, Func<int, int, double> func) => func(x, y);
	#endregion
}
