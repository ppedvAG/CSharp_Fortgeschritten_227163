namespace Sprachfeatures;

internal class Program
{
	int y = 10;

	static void Main(string[] args)
	{
		object o = null;
		if (o is int i)
		{
			//int i = (int) o;
		}

		//Wertetyp & Referenztyp

		//Wertetyp
		//struct
		//== und != werden die Werte verglichen
		//Bei Neuzuweisungen werden die Werte kopiert
		int original = 5;
		int x = original;
		original = 10;

		//Referenztyp
		//class
		//== und != werden die Speicheradressen verglichen
		//Bei Neuzuweisungen werden Referenzen angelegt
		Person p = new Person();
		Person p2 = p;
		p.Name = "Ein Name";

		Test2();
		Test2(6);

		new Person(nachname: ""); //Nur die Parameter angeben die auch benötigt werden
		new Person(null, "", DateTime.MinValue);

		string name = "lUkAs";
		string fix = char.ToUpper(name[0]) + name[1..].ToLower();

		if (o is null)
			o = 1;

		//Interpolated String ($): Gibt die Möglichkeit, Code in einen String einzubauen
		string str = "Das ist ein Text, mein Name ist " + fix;
		string interpolated = $"Das ist ein Text, mein Name ist {fix}";

		string kombi = $"Der Name ist {fix}, vorher war der Name {name}, die Zahl ist {x}";
		string kombi2 = $"Der Name ist " + fix + ", vorher war der Name " + name + ", die Zahl ist " + x;

		//Verbatim String (@): String der Escape Sequenzen ignoriert
		string pfad = "C:\\Users\\lk3\\source\\repos\\CSharp_Fortgeschritten_2023_06_05";
		string vPfad = @"C:\Users\lk3\source\repos\CSharp_Fortgeschritten_2023_06_05";
		string escape = @"\n";

		Dictionary<string, int> dict = new();
		var dict2 = new Dictionary<string, int>();

		Person p3 = new(nachname: "") { Name = "" };

		DayOfWeek day = DayOfWeek.Monday;
		switch (day)
		{
			case DayOfWeek.Monday:
			case DayOfWeek.Tuesday:
			case DayOfWeek.Wednesday:
			case DayOfWeek.Thursday:
			case DayOfWeek.Friday:
                    Console.WriteLine("Wochentag");
				break;
			case DayOfWeek.Saturday:
			case DayOfWeek.Sunday:
                    Console.WriteLine("Wochenende");
				break;
            }

		switch (day)
		{
			case > DayOfWeek.Monday and < DayOfWeek.Friday:
				Console.WriteLine("Wochentag");
				break;
			case DayOfWeek.Saturday or DayOfWeek.Sunday:
				Console.WriteLine("Wochenende");
				break;
		}

		if (o is null || o is not null)
		{

		}

		Fahrzeug f = new Fahrzeug(1, "VW");
		(int id, string m) = f;
            Console.WriteLine($"Die ID des Fahrzeugs ist {id}, die Marke des Fahrzeugs ist {m}");
        }

	public static void Test2(int x = 0)
	{

	}

	public ref int Test()
	{
		return ref y;
	}
}

public class Person : ICloneable
{
	public string Name { get; set; }

        public Person()
        {
            
        }

        public Person(string vorname = default, string nachname = default, DateTime gebDat = default)
	{

	}

	public object Clone()
	{
		//Reflection möglich
		return null;
	}
}

public interface IArbeit
{
	readonly static int Wochenstunden = 40;
}

//public class Fahrzeug
//{
//	public int MaxV { get; set; }

//	public string Marke { get; set; }

//	public Fahrzeug(int maxV, string marke)
//	{
//		MaxV = maxV;
//		Marke = marke;
//	}
//}

public record Fahrzeug(int ID, string Marke)
{
	public void Test()
	{
        Console.WriteLine();
    }
}