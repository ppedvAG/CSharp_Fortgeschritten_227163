using System.Collections.Concurrent;

namespace Multithreading;

internal class _07_ConcurrentCollections
{
	static void Main(string[] args)
	{
		ConcurrentBag<int> ints = new ConcurrentBag<int>(); //Thread-/Task sichere Liste
		ints.Add(1);

		ConcurrentDictionary<string, int> dict = new(); //Thread-/Task sicheres Dictionary
		dict.TryAdd("a", 1); //Add -> TryAdd
	}
}
