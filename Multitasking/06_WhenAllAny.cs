namespace Multitasking;

internal class _06_WhenAllAny
{
	static void Main(string[] args)
	{
		List<Task<int>> ints = new List<Task<int>>();
		for (int i = 0; i < 100; i++)
			ints.Add(Task.Run(() => i * i));

		//WhenAll: Wartet auf die Ergebnisse von beliebig vielen Tasks und erzeugt einen neuen Task
		//Dieser neue Task enthält das Ergebnis -> ContinueWith um das Ergebnis zu holen
		int[] result;
		Task<int[]> resultTask = Task.WhenAll(ints).ContinueWith(vorherigerTask => result = vorherigerTask.Result);

		Task.WhenAny(ints).ContinueWith(vorherigerTask => Console.WriteLine("Erstes Ergebnis: " + vorherigerTask.Result)); //WhenAny: Gibt das Ergebnis des Tasks zurück der zuerst fertig ist
	}
}
