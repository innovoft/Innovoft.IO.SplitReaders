using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using Innovoft.IO;

namespace Innovoft.IO
{
	internal static class Program
	{
		#region Main
		private static void Main(string[] args)
		{
			var errors = ParseCommandLine(args);
			if (errors != null)
			{
				PrintHelp(errors);
				return;
			}

			Prepare();
			Results();
		}
		#endregion //Main

		#region Fields
		private static string sourcePath;
		private static char separator = ',';
		private static char[] separators = new char[1] { ',', };
		#endregion //Fields

		#region Methods
		private static List<string> ParseCommandLine(string[] args)
		{
			var errors = new List<string>();
			if (args == null || args.Length == 0)
			{
				return errors;
			}

			for (var i = 0; i < args.Length; ++i)
			{
				switch (args[i])
				{
				default:
					errors.Add("Invalid Parameter: " + args[i]);
					break;

				case "-Source":
					try
					{
						sourcePath = args[++i];
					}
					catch (Exception exception)
					{
						errors.Add("Problems processing -Source " + exception.Message);
					}
					break;

				case "-Separator":
					try
					{
						var value = args[++i];
						if (value.Length != 1)
						{
							errors.Add("-Separator can only be one letter");
							break;
						}

						separators[0] = separator = value[0];
					}
					catch (Exception exception)
					{
						errors.Add("Problems processing -Separator " + exception.Message);
					}
					break;
				}
			}

			if (sourcePath == null)
			{
				errors.Add("Missing -Source");
			}

			if (errors.Count > 0)
			{
				return errors;
			}

			return null;
		}

		private static void PrintHelp(List<string> errors)
		{
			Console.WriteLine();
			Console.WriteLine("Innovoft.IO.SplitReaders.Test.Console.exe");
			Console.WriteLine(" Required");
			Console.WriteLine("  -Source Source.csv");
			Console.WriteLine(" Optional");
			Console.WriteLine("  -Separator ,");
			Console.WriteLine();

			if (errors != null && errors.Count > 0)
			{
				foreach (var error in errors)
				{
					Console.WriteLine(error);
				}
				Console.WriteLine();
				Console.WriteLine(Environment.CommandLine);
				Console.WriteLine();
			}
		}

		private static void Prepare()
		{
			Console.Title = "Innovoft.IO.SplitReaders.Test.Console";

			GC.Collect();
		}

		private static void Results()
		{
			ReadLineCompare();
		}

		private static void ReadLineCompare()
		{
			using (var lineStream = new FileStream(sourcePath, FileMode.Open, FileAccess.Read, FileShare.Read))
			using (var lineReader = new StreamReader(lineStream))
			using (var splitStream = new FileStream(sourcePath, FileMode.Open, FileAccess.Read, FileShare.Read))
			using (var splitReader = new SplitReader(splitStream))
			{
				var splits = new List<string>();
				while (true)
				{
					var line = lineReader.ReadLine();
					splits.Clear();
					var split = splitReader.ReadLine(separator, splits);
					if ((line != null) != split)
					{
						Console.WriteLine("Number of lines is not the same.");
						return;
					}
					if (line == null || !split)
					{
						return;
					}
					var columns = line.Split(separators);
					if (columns.Length != splits.Count)
					{
						Console.WriteLine("Number of columns is not the same.");
						return;
					}
					for (var i = columns.Length - 1; i >= 0; --i)
					{
						if (columns[i] != splits[i])
						{
							Console.WriteLine("A column is not the same.");
							return;
						}
					}
				}
			}
		}
		#endregion //Methods
	}
}
