namespace Generics;

internal class Program
{
	static void Main(string[] args)
	{
		List<int> ints = new(); //Generic: T wird nach unten übernommen (hier T = int)
		ints.Add(1); //T wird durch int ersetzt

		List<string> listStr = new();
		listStr.Add("1"); //T wird durch string ersetzt

		Dictionary<string, int> dict = new(); //Klasse mit 2 Generics: TKey -> string, TValue -> int
		dict.Add("1", 1);
	}
}

public class DataStore<T> :
	IProgress<T>, //T bei Vererbung weitergeben
	IEquatable<int> //Fixen Typ bei Vererbung übergeben
{
	private T[] data { get; set; } //T bei einem Feld oder Property

	public List<T> Data => data.ToList(); //T nach unten weitergeben

	public void Add(T item, int index) //T bei Parameter
	{
		data[index] = item;
	}

	public T Get(int index) //T als Rückgabewert
	{
		if (index < 0 || index >= data.Length)
			return default; //default: Standardwert von T (int: 0, string: null, bool: false, ...)
		return data[index];
	}

	public void Report(T value) //T kommt vom Interface
	{

	}

	public bool Equals(int other)
	{
		return true;
	}

	public MyType PrintType<MyType>(object o)
	{
        Console.WriteLine(default(MyType)); //default: Standardwert von T (int: 0, string: null, bool: false, ...)
        Console.WriteLine(typeof(MyType));
        Console.WriteLine(nameof(MyType)); //Typ als String ("int", "string", "bool", ...)

		//if (MyType is int)
		//{

		//}

		if (typeof(MyType) == typeof(int))
		{

		}

        Type t = typeof(MyType); //Type Objekt aus Generic erzeugen
		return (MyType) Convert.ChangeType(o, t); //Typ ändern mittels Generic
	}
}

public class DataStoreVererbung<T> : DataStore<T> { }