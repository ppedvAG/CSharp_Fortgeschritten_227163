using PluginBase;

namespace PluginCalculator;

public class Calculator : IPlugin
{
	public string Name => "Calculator Plugin";

	public string Description => "Ein einfacher Rechner";

	public string Version => "1.0";

	public string Author => "Lukas Kern";

	[ReflectionVisible("Die Addiere Methode")]
	public double Addiere(double x, double y) => x + y;

	[ReflectionVisible("Die Subtrahiere Methode")]
	public double Subtrahiere(double x, double y) => x - y;
}