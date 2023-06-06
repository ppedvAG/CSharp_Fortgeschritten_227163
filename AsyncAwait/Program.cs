using System.Diagnostics;

namespace AsyncAwait;

internal class Program
{
	static async Task Main(string[] args)
	{
		//Stopwatch sw = Stopwatch.StartNew();
		//Toast();
		//Geschirr();
		//Kaffee();
		//sw.Stop();
		//Console.WriteLine(sw.ElapsedMilliseconds); //7s

		//Stopwatch sw = Stopwatch.StartNew();
		//Task t1 = Task.Run(Toast);
		//Task t2 = Task.Run(Geschirr);
		//t2.Wait();
		//Task t3 = Task.Run(Kaffee);
		//Task.WaitAll(t1, t3);
		//sw.Stop();
		//Console.WriteLine(sw.ElapsedMilliseconds); //4s aber Wait

		//Stopwatch sw = Stopwatch.StartNew();
		//Task t1 = Task.Run(Toast);
		//Task t2 = Task.Run(Geschirr);
		//Task t3 = null;
		//t2.ContinueWith(t => t3 = Task.Run(Kaffee));
		//t1.ContinueWith(t =>
		//{
		//	sw.Stop();
		//	Console.WriteLine(sw.ElapsedMilliseconds); //4s aber Wait
		//});
		//Console.ReadKey();

		//async Methoden:
		//Enden mit Async laut Konvention, wenn sie awaiten werden sollen
		//async void: Kann selbst await verwenden, kann aber nicht awaited werden
		//async Task: Kann selbst await verwenden und awaited werden
		//async Task<T>: Kann selbst await verwenden, awaited werden und gibt ein Objekt als Ergebnis zurück
		//Wenn eine Async Methode gestartet wird, wird sie als Task gestartet
		//await: Warte darauf das dieser Task fertig ist

		//Stopwatch sw = Stopwatch.StartNew();
		//Task t1 = ToastAsync();
		//Task t2 = GeschirrAsync();
		//await t2;
		//Task t3 = KaffeeAsync();
		//await t1;
		//await t3;
		//sw.Stop();
		//Console.WriteLine(sw.ElapsedMilliseconds);

		//await bei Tasks mit Rückgabewert gibt das Objekt des Tasks zurück -> t.Result
		Stopwatch sw = Stopwatch.StartNew();
		Task<Toast> t1 = ToastObjectAsync();
		Task<Geschirr> t2 = GeschirrObjectAsync();
		Geschirr g = await t2;
		Task<Kaffee> t3 = KaffeeObjectAsync(g);
		Toast t = await t1;
		Kaffee k = await t3;
		sw.Stop();
		Console.WriteLine(sw.ElapsedMilliseconds);
		Console.ReadLine();

		//Task<Toast> t1 = ToastObjectAsync();
		//Task<Kaffee> t3 = KaffeeObjectAsync(await GeschirrObjectAsync());
		//await Task.WhenAll(t1, t3);

		//WhenAll: Warte auf mehrere Tasks, nützlich bei Tasks bei denen nicht klar ist wie lang diese dauern werden
		await Task.WhenAll(t1, t2, t3); //-> mehrere await Statements konsolidieren
		await Task.WhenAny(t1, t2, t3); //WhenAny: Warte auf den ersten Task
	}

	static void Toast()
	{
		Thread.Sleep(4000);
		Console.WriteLine("Toast fertig");
	}

	static void Geschirr()
	{
		Thread.Sleep(1500);
		Console.WriteLine("Geschirr fertig");
	}

	static void Kaffee()
	{
		Thread.Sleep(1500);
		Console.WriteLine("Kaffee fertig");
	}

	static async Task ToastAsync()
	{
		await Task.Delay(4000); //await: Warte darauf das dieser Task fertig ist
		Console.WriteLine("Toast fertig");
		//return ist nicht notwendig bei Task ohne Generic als Rückgabetyp
	}

	static async Task GeschirrAsync()
	{
		Thread.Sleep(1500);
		Console.WriteLine("Geschirr fertig");
	}

	static async Task KaffeeAsync()
	{
		Thread.Sleep(1500);
		Console.WriteLine("Kaffee fertig");
	}

	static async Task<Toast> ToastObjectAsync()
	{
		await Task.Delay(4000); //await: Warte darauf das dieser Task fertig ist
		Console.WriteLine("Toast fertig");
		return new Toast();
	}

	static async Task<Geschirr> GeschirrObjectAsync()
	{
		await Task.Delay(1500);
		Console.WriteLine("Geschirr fertig");
		return new Geschirr();
	}

	static async Task<Kaffee> KaffeeObjectAsync(Geschirr g)
	{
		await Task.Delay(1500);
		Console.WriteLine("Kaffee fertig");
		return new Kaffee();
	}
}

public class Toast { }

public class Geschirr { }

public class Kaffee { }