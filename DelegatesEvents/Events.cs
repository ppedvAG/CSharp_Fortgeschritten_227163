namespace DelegatesEvents;

internal class Events
{
	//Event: Statischer Punkt, an den Methoden angehängt werden können
	static event EventHandler TestEvent;

	static event EventHandler<int> ArgsEvent;

	static void Main(string[] args)
	{
		TestEvent += Events_TestEvent; //Events können nicht instanziert werden, Methode anhängen ohne new
		TestEvent(null, EventArgs.Empty); //Event ausführen

		ArgsEvent += Events_ArgsEvent;
		ArgsEvent(null, 34);
	}

	private static void Events_TestEvent(object sender, EventArgs e)
	{
        Console.WriteLine("Event wurde aufgerufen");
	}

	private static void Events_ArgsEvent(object sender, int e)
	{
        Console.WriteLine("Die Zahl ist " + e);
    }
}

public class TestEventArgs : EventArgs
{

}