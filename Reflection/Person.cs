using System.Reflection;

namespace Reflection;

public class Person : ICloneable
{
	public int ID { get; set; }

	public string Name { get; set; }

	public string Description { get; set; }

	public Person Vorgesetzter { get; set; }

	public int HashCode => GetHashCode();

	public object Clone()
	{
		Person p = new Person();
		foreach (PropertyInfo pi in GetType().GetProperties())
		{
			if (!pi.CanWrite)
				continue;

			if (pi.GetType().GetInterface("ICloneable") == null)
				p.GetType().GetProperty(pi.Name).SetValue(p, pi.GetValue(this));
			else
				p.GetType().GetProperty(pi.Name).SetValue((p as ICloneable).Clone(), pi.GetValue(this));
		}
		return p;
	}
}
