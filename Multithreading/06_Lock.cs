namespace Multithreading;

internal class _06_Lock
{
	static int Counter = 0;

	static object LockObject = new();

	static void Main(string[] args)
	{
		for (int i = 0; i < 100; i++)
			new Thread(CounterPlus1).Start();

        Console.WriteLine(Counter);
    }

	static void CounterPlus1()
	{
		lock (LockObject) //Counter sperren damit mehrere Threads hintereinander ihre Arbeit machen können
		{
			for (int i = 0; i < 100; i++)
				Counter++;
		} //Lock wird geöffnet für den nächsten Thread

		Monitor.Enter(LockObject); //Monitor und Lock haben 1:1 den selben Code
		for (int i = 0; i < 100; i++)
			Counter++;
		Monitor.Exit(LockObject);
	}
}
