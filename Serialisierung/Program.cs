using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace Serialisierung;

internal class Program
{
	static void Main(string[] args)
	{
		//File, Directory, Path, Environment
		string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory); //Pfad zum Desktop
		string folderPath = Path.Combine(desktop, "Test");
		string filePath = Path.Combine(folderPath, "Test.txt");

		if (!Directory.Exists(folderPath))
			Directory.CreateDirectory(folderPath);

		List<Fahrzeug> fahrzeuge = new()
		{
			new Fahrzeug(0, 251, FahrzeugMarke.BMW),
			new Fahrzeug(1, 274, FahrzeugMarke.BMW),
			new Fahrzeug(2, 146, FahrzeugMarke.BMW),
			new Fahrzeug(3, 208, FahrzeugMarke.Audi),
			new Fahrzeug(4, 189, FahrzeugMarke.Audi),
			new Fahrzeug(5, 133, FahrzeugMarke.VW),
			new Fahrzeug(6, 253, FahrzeugMarke.VW),
			new Fahrzeug(7, 304, FahrzeugMarke.BMW),
			new Fahrzeug(8, 151, FahrzeugMarke.VW),
			new PKW(9, 250, FahrzeugMarke.VW),
			new Fahrzeug(10, 217, FahrzeugMarke.Audi),
			new Fahrzeug(11, 125, FahrzeugMarke.Audi)
		};

		//SystemJson();

		//NewtonsoftJson();

		//XML();
	}

	static void SystemJson()
	{
		////File, Directory, Path, Environment
		//string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory); //Pfad zum Desktop
		//string folderPath = Path.Combine(desktop, "Test");
		//string filePath = Path.Combine(folderPath, "Test.txt");

		//if (!Directory.Exists(folderPath))
		//	Directory.CreateDirectory(folderPath);

		//List<Fahrzeug> fahrzeuge = new()
		//{
		//	new Fahrzeug(0, 251, FahrzeugMarke.BMW),
		//	new Fahrzeug(1, 274, FahrzeugMarke.BMW),
		//	new Fahrzeug(2, 146, FahrzeugMarke.BMW),
		//	new Fahrzeug(3, 208, FahrzeugMarke.Audi),
		//	new Fahrzeug(4, 189, FahrzeugMarke.Audi),
		//	new Fahrzeug(5, 133, FahrzeugMarke.VW),
		//	new Fahrzeug(6, 253, FahrzeugMarke.VW),
		//	new Fahrzeug(7, 304, FahrzeugMarke.BMW),
		//	new Fahrzeug(8, 151, FahrzeugMarke.VW),
		//	new PKW(9, 250, FahrzeugMarke.VW),
		//	new Fahrzeug(10, 217, FahrzeugMarke.Audi),
		//	new Fahrzeug(11, 125, FahrzeugMarke.Audi)
		//};

		//JsonSerializerOptions options = new(); //Einstellungen beim (De-)Serialisieren
		//options.WriteIndented = true; //Json schön schreiben
		//options.ReferenceHandler = ReferenceHandler.IgnoreCycles; //Zirkelbezüge ignorieren

		////WICHTIG: Einstellungen übergeben
		//string json = JsonSerializer.Serialize(fahrzeuge, options);
		//File.WriteAllText(filePath, json);

		//string readJson = File.ReadAllText(filePath);
		//List<Fahrzeug> readFzg = JsonSerializer.Deserialize<List<Fahrzeug>>(readJson, options);

		//////////////////////////////////////////////////////////////

		////Json per Hand verarbeiten
		//JsonDocument doc = JsonDocument.Parse(readJson); //Das Json einlesen und zu einem JsonDocument konvertieren
		//foreach (JsonElement element in doc.RootElement.EnumerateArray()) //Das gesamte Dokument zu einzelnen Elementen umwandeln und durchgehen
		//{
		//	Console.WriteLine(element.GetProperty("ID").GetInt32()); //Auf einzelne Felder zugreifen und zu den entsprechenden Typen konvertieren
		//	Console.WriteLine(element.GetProperty("MaxV").GetInt32());
		//	Console.WriteLine((FahrzeugMarke) element.GetProperty("Marke").GetInt32());
		//	Console.WriteLine();
		//}
	}

	static void NewtonsoftJson()
	{
		////File, Directory, Path, Environment
		//string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory); //Pfad zum Desktop
		//string folderPath = Path.Combine(desktop, "Test");
		//string filePath = Path.Combine(folderPath, "Test.txt");

		//if (!Directory.Exists(folderPath))
		//	Directory.CreateDirectory(folderPath);

		//List<Fahrzeug> fahrzeuge = new()
		//{
		//	new Fahrzeug(0, 251, FahrzeugMarke.BMW),
		//	new Fahrzeug(1, 274, FahrzeugMarke.BMW),
		//	new Fahrzeug(2, 146, FahrzeugMarke.BMW),
		//	new Fahrzeug(3, 208, FahrzeugMarke.Audi),
		//	new Fahrzeug(4, 189, FahrzeugMarke.Audi),
		//	new Fahrzeug(5, 133, FahrzeugMarke.VW),
		//	new Fahrzeug(6, 253, FahrzeugMarke.VW),
		//	new Fahrzeug(7, 304, FahrzeugMarke.BMW),
		//	new Fahrzeug(8, 151, FahrzeugMarke.VW),
		//	new PKW(9, 250, FahrzeugMarke.VW),
		//	new Fahrzeug(10, 217, FahrzeugMarke.Audi),
		//	new Fahrzeug(11, 125, FahrzeugMarke.Audi)
		//};

		//JsonSerializerSettings options = new(); //Einstellungen beim (De-)Serialisieren
		//options.Formatting = Formatting.Indented; //Json schön schreiben
		//options.ReferenceLoopHandling = ReferenceLoopHandling.Ignore; //Zirkelbezüge ignorieren
		//options.TypeNameHandling = TypeNameHandling.Objects; //Vererbungen serialisieren

		////WICHTIG: Einstellungen übergeben
		//string json = JsonConvert.SerializeObject(fahrzeuge, options);
		//File.WriteAllText(filePath, json);

		//string readJson = File.ReadAllText(filePath);
		//List<Fahrzeug> readFzg = JsonConvert.DeserializeObject<List<Fahrzeug>>(readJson, options);

		////////////////////////////////////////////////////////////////

		////Json per Hand verarbeiten

		//JToken doc = JToken.Parse(readJson); //Dokument einlesen
		//foreach (JToken token in doc)
		//{
		//	Console.WriteLine(token["ID"].Value<int>());
		//	Console.WriteLine(token["MaxV"].Value<int>());
		//	Console.WriteLine((FahrzeugMarke) token["Marke"].Value<int>());
		//	Console.WriteLine();
		//}
	}

	static void XML()
	{
		//File, Directory, Path, Environment
		string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory); //Pfad zum Desktop
		string folderPath = Path.Combine(desktop, "Test");
		string filePath = Path.Combine(folderPath, "Test.txt");

		if (!Directory.Exists(folderPath))
			Directory.CreateDirectory(folderPath);

		List<Fahrzeug> fahrzeuge = new()
		{
			new Fahrzeug(0, 251, FahrzeugMarke.BMW),
			new Fahrzeug(1, 274, FahrzeugMarke.BMW),
			new Fahrzeug(2, 146, FahrzeugMarke.BMW),
			new Fahrzeug(3, 208, FahrzeugMarke.Audi),
			new Fahrzeug(4, 189, FahrzeugMarke.Audi),
			new Fahrzeug(5, 133, FahrzeugMarke.VW),
			new Fahrzeug(6, 253, FahrzeugMarke.VW),
			new Fahrzeug(7, 304, FahrzeugMarke.BMW),
			new Fahrzeug(8, 151, FahrzeugMarke.VW),
			new PKW(9, 250, FahrzeugMarke.VW),
			new Fahrzeug(10, 217, FahrzeugMarke.Audi),
			new Fahrzeug(11, 125, FahrzeugMarke.Audi)
		};

		XmlSerializer xml = new XmlSerializer(fahrzeuge.GetType());
		using (StreamWriter sw = new StreamWriter(filePath))
			xml.Serialize(sw, fahrzeuge);

		using (StreamReader sr = new StreamReader(filePath))
		{
			List<Fahrzeug> readFzg = xml.Deserialize(sr) as List<Fahrzeug>;
		}

		//////////////////////////////////////////////////////////////

		//XML per Hand verarbeiten
		XmlDocument doc = new XmlDocument();
		doc.Load(filePath);

		foreach (XmlNode node in doc.DocumentElement.ChildNodes)
		{
			Console.WriteLine(node["ID"].InnerText);
			Console.WriteLine(node["MaxV"].InnerText);
			Console.WriteLine(node["Marke"].InnerText);
			Console.WriteLine();
			//Console.WriteLine(node.Attributes); //Attribute auslesen
		}
	}
}

//Vererbung ermöglichen mit System.Text.Json
//[JsonDerivedType(typeof(Fahrzeug), "F")]
//[JsonDerivedType(typeof(PKW), "P")]

//Vererbung ermöglichen bei XML
[XmlInclude(typeof(Fahrzeug))]
[XmlInclude(typeof(PKW))]
public class Fahrzeug
{
	//System.Text.Json Attribute
	//[JsonIgnore]
	//[JsonPropertyName("Identifier")] //Feld umbenennen
	public int ID { get; set; }

	//Newtonsoft.Json Attribute
	//[JsonIgnore]
	//[JsonProperty("Maximalgeschwindigkeit")]
	public int MaxV { get; set; }

	//XML Attribute
	//[XmlIgnore]
	//[XmlAttribute]
	public FahrzeugMarke Marke { get; set; }

	public Fahrzeug(int id, int maxV, FahrzeugMarke marke)
	{
		ID = id;
		MaxV = maxV;
		Marke = marke;
	}

    public Fahrzeug()
    {
        
    }
}

public class PKW : Fahrzeug
{
	public PKW(int id, int maxV, FahrzeugMarke marke) : base(id, maxV, marke)
	{
	}

    public PKW()
    {
        
    }
}

public enum FahrzeugMarke
{
	Audi, BMW, VW
}