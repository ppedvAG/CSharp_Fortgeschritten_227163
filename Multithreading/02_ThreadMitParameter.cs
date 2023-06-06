namespace Multithreading;

internal class _02_ThreadMitParameter
{
	static void Main(string[] args)
	{
		ParameterizedThreadStart pt = new ParameterizedThreadStart(Run); //Funktionszeiger diesmal hier oben anstatt bei Thread Erstellung
		Thread t = new Thread(pt); //pt hier übergeben
		t.Start(new ThreadData(-100, 200)); //Hier Parameter übergeben

		for (int i = 0; i < 100; i++)
		{
			Console.WriteLine($"Main Thread: {i}");
		}
	}

	static void Run(object o)
	{
		if (o is ThreadData x)
		{
			for (int i = x.Start; i < x.Ende; i++)
			{
				Console.WriteLine($"Side Thread: {i}");
			}
		}
	}

	public record ThreadData(int Start, int Ende);
}
