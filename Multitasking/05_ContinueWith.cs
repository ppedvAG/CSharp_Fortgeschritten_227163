namespace Multitasking;

internal class _05_ContinueWith
{
	static void Main(string[] args)
	{
		//ContinueWith: Tasks verketten, wenn der erste Task fertig ist, können beliebig viele Folgetasks gestartet werden
		//Auf den vorherigen Task zugreifen über den Variablennamen der Action
		Task<double> t = new Task<double>(() => 
		{
			Thread.Sleep(1000);
			throw new Exception();
			return Math.Pow(4, 23);
		});
		t.ContinueWith(vorherigerTask => Console.WriteLine(vorherigerTask.Result)); //Dieser Task wird immer ausgeführt (auch bei Exception)
		t.ContinueWith(vorherigerTask => Console.WriteLine(vorherigerTask.Result), TaskContinuationOptions.OnlyOnRanToCompletion); //Dieser Task wird bei Erfolg ausgeführt
		t.ContinueWith(vorherigerTask => Console.WriteLine("Exception"), TaskContinuationOptions.OnlyOnFaulted); //Dieser Task wird bei Exceptions ausgeführt
		t.Start();

		for (int i = 0; i < 100; i++)
            Console.WriteLine(i);

		Console.ReadLine();
    }
}
