using System.Collections;

namespace Sonstiges;

internal class Program
{
	static void Main(string[] args)
	{
		Wagon w1 = new();
		Wagon w2 = new();
        Console.WriteLine(w1 == w2);

		Zug z = new();
		z += w1;
		z += w2;
		z++;
		z++;
		z++;
		z++;

		foreach (Wagon w in z)
		{

		}

		z[0] = new();
		Console.WriteLine(z[10, "Rot"]);

		var x = z.Wagons.Select(e => new { e.AnzSitze, HC = e.GetHashCode() });
		Console.WriteLine(x.First().HC);
    }
}

public class Zug : IEnumerable
{
	public List<Wagon> Wagons = new();

	public IEnumerator GetEnumerator()
	{
		//List<Wagon> list = new();
		//foreach (Wagon w in Wagons)
		//	list.Add(w);
		//return list;
		return Wagons.GetEnumerator();
	}

	public static Zug operator +(Zug z, Wagon w)
	{
		z.Wagons.Add(w);
		return z;
	}

	public static Zug operator ++(Zug z)
	{
		z.Wagons.Add(new());
		return z;
	}

	public Wagon this[int index]
	{
		get => Wagons[index];
		set => Wagons[index] = value;
	}

	public Wagon this[int anz, string farbe]
	{
		get => Wagons.First(e => e.AnzSitze == anz && e.Farbe == farbe);
	}
}

public class Wagon
{
	public int AnzSitze { get; set; }

	public string Farbe { get; set; }

    public Wagon()
    {
        
    }

    public static bool operator ==(Wagon w1, Wagon w2)
	{
		return w1.AnzSitze == w2.AnzSitze && w1.Farbe == w2.Farbe;
	}

	public static bool operator !=(Wagon w1, Wagon w2)
	{
		return !(w1 == w2);
	}
}