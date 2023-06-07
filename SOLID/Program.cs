namespace SOLID;

internal class Program
{
	static void Main(string[] args)
	{

	}

	//Ich: 3 Tage
	public void PrintPerson(IPerson p)
	{

	}
}

//Anderer Programmierer: 5 Tage
public class Person : IPerson
{
	public string Name { get; set; }
}

public interface IPerson
{

}

//2 Tage für Test Klasse
public class TestPerson : IPerson
{

}