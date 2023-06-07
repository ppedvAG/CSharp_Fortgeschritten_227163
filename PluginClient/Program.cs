using PluginBase;
using System.Reflection;

namespace PluginClient;

internal class Program
{
	static void Main(string[] args)
	{
		//Plugin laden
		//Pfade sollten aus einer Config kommen (z.B. Json oder Xml)
		Assembly loaded = Assembly.LoadFrom(@"C:\Users\lk3\source\repos\CSharp_Fortgeschritten_2023_06_05\PluginCalculator\bin\Debug\net7.0\PluginCalculator.dll");

		Type calcType = loaded.GetTypes().FirstOrDefault(e => e.GetInterface(typeof(IPlugin).Name) != null); //Suche die Klasse die das IPlugin Interface hat

		if (calcType != null) //Plugin gefunden
		{
			object o = Activator.CreateInstance(calcType);

			IPlugin plugin = o as IPlugin; //Objekt zu IPlugin machen für Kompatiblität

			foreach (MethodInfo mi in plugin.GetType().GetMethods())
			{
				ReflectionVisible attr;
				if (mi.GetCustomAttribute<ReflectionVisible>() != null)
				{
					attr = mi.GetCustomAttribute<ReflectionVisible>();
					Console.WriteLine(attr.Name);
				}
            }
		}
	}
}