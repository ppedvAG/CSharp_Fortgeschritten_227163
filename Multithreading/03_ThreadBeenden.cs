namespace Multithreading;

internal class _03_ThreadBeenden
{
	static void Main(string[] args)
	{
		try
		{
			Thread t = new Thread(Run);
			t.Start();

			Thread.Sleep(500);

			t.Interrupt();
		}
		catch (ThreadInterruptedException)
		{
            Console.WriteLine("Thread beendet"); //Exception kann nicht hier oben gefangen werden
        }
	}

	static void Run()
	{
		try
		{
			Thread.Sleep(1000);
		}
		catch (ThreadInterruptedException)
		{
            Console.WriteLine("Thread beendet"); //Exception muss hier unten gefangen werden
        }
	}
}
