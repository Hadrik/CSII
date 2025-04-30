using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ReflectionApp
{
	class TableNameAttribute(string name) : Attribute
	{
		public string TableName { get; set; } = name;
	}
	
	class IgnoreColumnAttribute : Attribute
	{
	}
	
	[TableName("Auto")]
	public class Car
	{
		public string Color	{ get; set; }
		[IgnoreColumn]
		public int Speed { get; set; }
		public bool IsBroken { get; set; }

		public override string ToString()
		{
			return $"Color: {Color}, Speed: {Speed}, IsBroken: {IsBroken}";
		}
	}
	
	
	
	public class Program
	{
		
		private static T Read<T>(Dictionary<string, object> data)
		{
			var t = typeof(T);
			var instance = Activator.CreateInstance(t);
			foreach (var prop in t.GetProperties())
			{
				if (data.TryGetValue(prop.Name, out var value))
				{
					prop.SetValue(instance, value);
				}
			}
			return (T)instance;
		}
		
		private static string CreateInsert(Car obj)
		{
			var type = obj.GetType();
			
			var nameAttr = type.GetCustomAttribute<TableNameAttribute>();
			var name = nameAttr?.TableName ?? type.Name;
			
			var sb = new StringBuilder();
			sb.Append("INSERT INTO ");
			sb.Append(name);
			sb.Append(" (");
			foreach (var prop in type.GetProperties())
			{
				if (prop.GetCustomAttribute<IgnoreColumnAttribute>() != null)
				{
					continue;
				}
				sb.Append(prop.Name);
				sb.Append(", ");
			}
			sb.Remove(sb.Length - 2, 2);
			sb.Append(") VALUES (");
			foreach (var prop in type.GetProperties())
			{
				if (prop.GetCustomAttribute<IgnoreColumnAttribute>() != null)
				{
					continue;
				}
				var val = prop.GetValue(obj);
				if (val is string)
				{
					val = $"'{val}'";
				}
				sb.Append(val);
				sb.Append(", ");
			}
			sb.Remove(sb.Length - 2, 2);
			sb.Append(");");
			return sb.ToString();
		}
		
		public static void Main(string[] args)
		{
			var data = new Dictionary<string, object>()
			{
				{"Color", "orange"},
				{"Speed", 100},
				{"IsBroken", false}
			};

			Console.WriteLine(Read<Car>(data));

			/*
			var car = new Car()
			{
				Color = "Red",
				Speed = 100,
				IsBroken = false
			};

			Console.WriteLine(CreateInsert(car));
			*/

			/*
			string baseDir = Path.GetFullPath("../../../plugins/Debug/net8.0");
			string[] dlls = Directory.GetFiles(baseDir, "*.dll");

			// nacist vsechny knihovny
			// nacist vsechny typy do seznamu
			List<Type> typesList = [];
			foreach (var dll in dlls)
			{
				var a = Assembly.LoadFile(dll);
				Console.WriteLine($"Loaded {a.FullName}");
				typesList.AddRange(a.GetTypes());
			}

			// vybrat s kterym pracovat
			int i = int.Parse(Console.ReadLine());
			Type selectedType = typesList[i];

			var instance = Activator.CreateInstance(selectedType);

			foreach (var prop in selectedType.GetProperties())
			{
				Console.WriteLine($"Zadej {prop.Name}: ");
				var val = double.Parse(Console.ReadLine());
				prop.SetValue(instance, val);
			}

			var mi = selectedType.GetMethod("", new Type[] { });
			*/
		}
	}
}