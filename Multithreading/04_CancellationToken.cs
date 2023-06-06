namespace Multithreading;

internal class _04_CancellationToken
{
	static void Main(string[] args)
	{
		CancellationTokenSource cts = new CancellationTokenSource(); //Sender
		CancellationToken token = cts.Token; //Empfänger

		Thread t = new Thread(Run);
		t.Start(token);

		Thread.Sleep(500);

		cts.Cancel(); //Auf der Source alle Token von der Source canceln
	}

	static void Run(object o)
	{
		try
		{
			if (o is CancellationToken ct)
			{
				for (int i = 0; i < 100; i++)
				{
					ct.ThrowIfCancellationRequested();
					Thread.Sleep(25);
				}
			}
		}
		catch (OperationCanceledException)
		{
            Console.WriteLine("Thread beendet");
        }
	}
}
