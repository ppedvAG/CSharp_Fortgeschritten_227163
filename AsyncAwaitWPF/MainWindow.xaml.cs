using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;

namespace AsyncAwaitWPF;

public partial class MainWindow : Window
{
	public MainWindow()
	{
		InitializeComponent();
	}

	private void Button_Click(object sender, RoutedEventArgs e)
	{
		Progress.Value = 0;
		for (int i = 0; i < 100; i++)
		{
			Thread.Sleep(25);
			Progress.Value++;
		} //UI Updates finden am Main Thread statt, Main Thread wird blockiert
	}

	private void Button_Click1(object sender, RoutedEventArgs e)
	{
		Task.Run(() => //UI Updates sind nicht erlaubt von Side Threads/Tasks
		{
			Dispatcher.Invoke(() => Progress.Value = 0); //Dispatcher.Invoke um UI Updates von Side Threads/Tasks zu machen
			for (int i = 0; i < 100; i++)
			{
				Thread.Sleep(25);
				Dispatcher.Invoke(() => Progress.Value++);
			}
		});
	}

	private async void Button_Click2(object sender, RoutedEventArgs e)
	{
		Progress.Value = 0;
		for (int i = 0; i < 100; i++)
		{
			await Task.Delay(25);
			Progress.Value++;
		}
	}

	private async void Button_Click3(object sender, RoutedEventArgs e)
	{
		using HttpClient client = new HttpClient();
		Task<HttpResponseMessage> responseTask = client.GetAsync(@"http://www.gutenberg.org/files/54700/54700-0.txt"); //Hier wird der Task gestartet
		TB.Text = "Text wird geladen..."; //Dinge vor dem Ergebnis machen
		HttpResponseMessage resp = await responseTask; //Warte hier auf das Ergebnis
		if (resp.IsSuccessStatusCode)
		{
			Task<string> stringTask = resp.Content.ReadAsStringAsync();
			string text = await stringTask;
			TB.Text = text;
		}
	}

	private async void Button_Click4(object sender, RoutedEventArgs e)
	{
		using HttpClient client = new HttpClient();
		Task<HttpResponseMessage> responseTask = client.GetAsync(@"http://www.gutenberg.org/files/54700/54700-0.txt"); //Hier wird der Task gestartet
		TB.Text = "Text wird geladen..."; //Dinge vor dem Ergebnis machen
		HttpResponseMessage resp = await responseTask; //Warte hier auf das Ergebnis
		if (resp.IsSuccessStatusCode)
		{
			Task<string> stringTask = resp.Content.ReadAsStringAsync();
			string text = await stringTask;

			string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory); //Pfad zum Desktop
			string folderPath = System.IO.Path.Combine(desktop, "Test");
			string filePath = System.IO.Path.Combine(folderPath, "Test");

			if (!Directory.Exists(folderPath))
				Directory.CreateDirectory(folderPath);

			ConcurrentDictionary<int, string> texte = new();
			for (int i = 0; i < 100; i++)
			{
				texte.TryAdd(i, text + text + text + text + text + text + text + text + text + text);
			}

			long foreachMS;

			Stopwatch sw = Stopwatch.StartNew();
			foreach (KeyValuePair<int, string> t in texte)
			{
				await File.WriteAllTextAsync(filePath + $"{t.Key}.txt", t.Value);
				TB.Text = $"File {t.Key} geschrieben";
				Progress.Value++;
			}
			sw.Stop();
			foreachMS = sw.ElapsedMilliseconds;

			Progress.Value = 0;
			sw.Restart();
			await Parallel.ForEachAsync(texte, (t, ct) =>
			{
				File.WriteAllText(filePath + $"{t.Key + 100}.txt", t.Value);
				Dispatcher.Invoke(() =>
				{
					TB.Text = $"File {t.Key + 100} geschrieben";
				});
				return ValueTask.CompletedTask;
			});
			sw.Stop();
			TB.Text = foreachMS.ToString() + "\n" + sw.ElapsedMilliseconds.ToString();
		}
	}
}
