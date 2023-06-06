namespace Multitasking;

internal class _04_TaskExceptions
{
	static void Main(string[] args)
	{
		try
		{
			Task t1 = Task.Run(Exception1);
			Task t2 = Task.Run(Exception2);
			Task t3 = Task.Run(Exception3);

			Task.WaitAll(t1, t2, t3);
		}
		catch (AggregateException ex)
		{
			foreach (Exception e in ex.InnerExceptions)
			{
                Console.WriteLine(e.Message);
            }
		}
	}

	static void Exception1()
	{
		Thread.Sleep(1000);
		throw new DivideByZeroException();
	}

	static void Exception2()
	{
		Thread.Sleep(2000);
		throw new StackOverflowException();
	}

	static void Exception3()
	{
		Thread.Sleep(3000);
		throw new OutOfMemoryException();
	}
}
