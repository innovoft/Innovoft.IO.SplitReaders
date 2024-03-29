﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace Innovoft.IO
{
	public sealed class CharsColumns : SplitColumns<CharsColumn, char>
	{
		#region Constants
		public const int DefaultCapacity = CharsColumn.DefaultCapacity;
		#endregion //Constants

		#region Class Methods
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Clear(CharsColumns value)
		{
			value.Clear();
		}

		public static void WriteLine(TextWriter writer, IEnumerable<CharsColumn> columns, char[] separator, char[] newline)
		{
			if (columns == null)
			{
				writer.Write(newline);
				return;
			}
			using (var enumerator = columns.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					while (true)
					{
						enumerator.Current.Write(writer);
						if (!enumerator.MoveNext())
						{
							writer.Write(newline);
							return;
						}
						writer.Write(separator);
					}
				}
				else
				{
					writer.Write(newline);
					return;
				}
			}
		}

		public static void WriteLine(TextWriter writer, char[][] columns, char[] separator, char[] newline)
		{
			if (columns == null || columns.Length <= 0)
			{
				writer.Write(newline);
				return;
			}
			for (var offset = 0; ;)
			{
				var column = columns[offset];
				writer.Write(column);
				++offset;
				if (offset >= columns.Length)
				{
					writer.Write(newline);
					return;
				}
				writer.Write(separator);
			}
		}

		public static void WriteLine(TextWriter writer, IEnumerable<char[]> columns, char[] separator, char[] newline)
		{
			if (columns == null)
			{
				writer.Write(newline);
				return;
			}
			using (var enumerator = columns.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					while (true)
					{
						var column = enumerator.Current;
						writer.Write(column);
						if (!enumerator.MoveNext())
						{
							writer.Write(newline);
							return;
						}
						writer.Write(separator);
					}
				}
				else
				{
					writer.Write(newline);
					return;
				}
			}
		}
		#endregion //Class Methods

		#region Fields
		private int capacity;
		#endregion //Fields

		#region Constructors
		public CharsColumns()
			: this(DefaultCapacity)
		{
		}

		public CharsColumns(int capacity)
			: base()
		{
			this.capacity = capacity;
		}
		#endregion //Constructors

		#region Properties
		public int Capacity { get => capacity; set => capacity = value; }
		#endregion //Properties

		#region Methods
		public CharsColumn Dequeue()
		{
			if (queue.Count > 0)
			{
				var value = queue.Dequeue();
				value.Clear();
				columns.Add(value);
				return value;
			}
			else
			{
				var value = new CharsColumn(capacity);
				columns.Add(value);
				return value;
			}
		}

		public CharsColumn Remove()
		{
			if (queue.Count > 0)
			{
				var value = queue.Dequeue();
				value.Clear();
				return value;
			}
			else
			{
				var value = new CharsColumn(capacity);
				return value;
			}
		}

		public Action<char[], int, int> AddLength()
		{
			var value = Dequeue();
			return value.AppendLength;
		}

		public Action<char[], int, int> AddEnding()
		{
			var value = Dequeue();
			return value.AppendEnding;
		}

		public string ToString(char separator)
		{
			if (columns.Count <= 0)
			{
				return string.Empty;
			}
			var builder = new StringBuilder();
			for (var i = 0; ; )
			{
				var column = columns[i];
				column.ToString(builder);
				++i;
				if (i >= columns.Count)
				{
					break;
				}
				builder.Append(separator);
			}
			var builderText = builder.ToString();
			return builderText;
		}

		public string ToString(string separator)
		{
			if (columns.Count <= 0)
			{
				return string.Empty;
			}
			var builder = new StringBuilder();
			for (var i = 0; ; )
			{
				var column = columns[i];
				column.ToString(builder);
				++i;
				if (i >= columns.Count)
				{
					break;
				}
				builder.Append(separator);
			}
			var builderText = builder.ToString();
			return builderText;
		}

		public string[] ToStringsArray()
		{
			var values = new string[columns.Count];
			for (var i = values.Length - 1; i >= 0; --i)
			{
				var value = columns[i].ToString();
				values[i] = value;
			}
			return values;
		}

		public List<string> ToStringsList()
		{
			var values = new List<string>(columns.Count);
			foreach (var column in columns)
			{
				var value = column.ToString();
				values.Add(value);
			}
			return values;
		}

		public void ToStringsAdd<T>(T values)
			where T: IList<string>
		{
			foreach (var column in columns)
			{
				var value = column.ToString();
				values.Add(value);
			}
		}

		public void ToStringsAdd(Action<string> add)
		{
			foreach (var column in columns)
			{
				var value = column.ToString();
				add(value);
			}
		}

		public void Write(TextWriter writer, char separator)
		{
			if (columns.Count <= 0)
			{
				return;
			}
			for (var offset = 0; ;)
			{
				columns[offset].Write(writer);
				++offset;
				if (offset >= columns.Count)
				{
					return;
				}
				writer.Write(separator);
			}
		}

		public bool TryToString(int offset, out string value)
		{
			if (offset >= columns.Count)
			{
				value = null;
				return false;
			}
			var column = columns[offset];
			value = column.ToString();
			return true;
		}

		public string TryToString(int offset)
		{
			if (offset >= columns.Count)
			{
				return null;
			}
			var column = columns[offset];
			var value = column.ToString();
			return value;
		}

		public void Write(TextWriter writer, char[] separator)
		{
			if (columns.Count <= 0)
			{
				return;
			}
			for (var offset = 0; ;)
			{
				columns[offset].Write(writer);
				++offset;
				if (offset >= columns.Count)
				{
					return;
				}
				writer.Write(separator);
			}
		}

		public void Write(TextWriter writer, string separator)
		{
			if (columns.Count <= 0)
			{
				return;
			}
			for (var offset = 0; ; )
			{
				columns[offset].Write(writer);
				++offset;
				if (offset >= columns.Count)
				{
					return;
				}
				writer.Write(separator);
			}
		}

		public void WriteLine(TextWriter writer, char separator)
		{
			if (columns.Count <= 0)
			{
				writer.WriteLine();
				return;
			}
			for (var offset = 0; ;)
			{
				columns[offset].Write(writer);
				++offset;
				if (offset >= columns.Count)
				{
					writer.WriteLine();
					return;
				}
				writer.Write(separator);
			}
		}

		public void WriteLine(TextWriter writer, char[] separator)
		{
			if (columns.Count <= 0)
			{
				writer.WriteLine();
				return;
			}
			for (var offset = 0; ;)
			{
				columns[offset].Write(writer);
				++offset;
				if (offset >= columns.Count)
				{
					writer.WriteLine();
					return;
				}
				writer.Write(separator);
			}
		}

		public void WriteLine(TextWriter writer, string separator)
		{
			if (columns.Count <= 0)
			{
				writer.WriteLine();
				return;
			}
			for (var offset = 0; ; )
			{
				columns[offset].Write(writer);
				++offset;
				if (offset >= columns.Count)
				{
					writer.WriteLine();
					return;
				}
				writer.Write(separator);
			}
		}
		#endregion //Methods
	}
}
