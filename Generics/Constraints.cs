﻿namespace Generics;

public class Constraints
{
	static void Main(string[] args)
	{

	}

	public void Test<T>() where T : class
	{

	}
}

public class DataStore1<T> where T : class { }

public class DataStore2<T> where T : struct { }

public class DataStore3<T> where T : Constraints { }

public class DataStore4<T> where T : IEnumerable<T> { }

public class DataStore5<T> where T : new() { } //new(): T muss einen Standardkonstruktor haben

public class DataStore6<T> where T : Enum { } //Enum: Ein Enum (kein Enumwert)

public class DataStore7<T> where T : Delegate { } //Delegate: Ein Delegate (Action, Func, EventHandler, ...)

public class DataStore8<T> where T : unmanaged { }
//https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/unmanaged-types


public class DataStore9<T> where T : class, Delegate, new() { } //Mehrere Einschränkungen auf ein Constraint

public class DataStore10<T1, T2> //Mehrere Constraints auf mehrere Generics
	where T1 : class
	where T2 : struct
{ }