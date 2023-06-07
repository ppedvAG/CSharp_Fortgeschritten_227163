using System.Reflection;

namespace Reflection;

internal class Program
{
	static void Main(string[] args)
	{
		Program p = new();
		Type pt = p.GetType(); //Typ aus Objekt entnehmen über GetType()
		Type t = typeof(Program); //Typ holen durch typeof(<Klassenname>)

		object o = Activator.CreateInstance(t); //Objekt über Typ-Object erstellen

		Convert.ChangeType(o, pt); //Typ von einem Objekt ändern ohne Cast

		///////////////////////////////

		pt.GetMethods(); //alle Methoden anschauen
		pt.GetMethod("Test").Invoke(o, null);

		pt.GetProperty("Text").SetValue(o, "Text");

		///////////////////////////////
		
		Assembly assembly = Assembly.GetExecutingAssembly(); //Das derzeitige Assembly (Projekt)

		//Externe DLL laden
		Assembly loaded = Assembly.LoadFrom(@"C:\Users\lk3\source\repos\CSharp_Fortgeschritten_2023_06_05\DelegatesEvents\bin\Debug\net7.0\DelegatesEvents.dll");

		Type compType = loaded.GetType("DelegatesEvents.Component");

		object comp = Activator.CreateInstance(compType);

		comp.GetType().GetEvent("ProcessCompleted").AddEventHandler(comp, () => Console.WriteLine("Prozess fertig"));
		comp.GetType().GetEvent("Progress").AddEventHandler(comp, (int i) => Console.WriteLine($"Fortschritt: {i}"));
		comp.GetType().GetMethod("StartProcess").Invoke(comp, null);



		Person person = new() { ID = 5, Name = "Max", Description = "Test", Vorgesetzter = new Person() { ID = 10, Name = "Test" } };
		Person p2 = person.Clone() as Person;
        Console.WriteLine(person == p2);
    }

	public string Text { get; set; }

	public void Test()
	{
        Console.WriteLine("Ein Test");
    }
}