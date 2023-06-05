namespace DelegatesEvents;

internal class ComponentWithEvent
{
	static void Main(string[] args)
	{
		Component comp = new();
		comp.ProcessCompleted += () => Console.WriteLine("Prozess Fertig");
		comp.Progress += (x) => Console.WriteLine($"Fortschritt: {x}");
		comp.StartProcess();
	}
}
