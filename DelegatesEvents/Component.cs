namespace DelegatesEvents;

internal class Component
{
	public event Action ProcessCompleted; //Action statt EventHandler (kein sender)

	public event Action<int> Progress; //Action mit Parameter (der Fortschritt)

	public void StartProcess()
	{
		for (int i = 0; i < 10; i++)
		{
			Thread.Sleep(100);
			Progress?.Invoke(i); //? hier essentiell, da der Programmierer möglicherweise keine Methode an das Event hängt
		}
		ProcessCompleted?.Invoke(); //? hier essentiell, da der Programmierer möglicherweise keine Methode an das Event hängt
	}
}
