using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Innovoft.IO
{
	public sealed class BytesColumns : SplitColumns<BytesColumn, byte>
	{
		#region Constants
		public const int DefaultCapacity = BytesColumn.DefaultCapacity;
		#endregion //Constants

		#region Class Methods
		[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static void Clear(BytesColumns value)
		{
			value.Clear();
		}

		[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static void WriteLine(Stream writer, BytesColumns columns, byte[] separator, byte[] newline)
		{
			columns.WriteLine(writer, separator, newline);
		}

		public static void WriteLine(Stream writer, IEnumerable<BytesColumn> columns, byte[] separator, byte[] newline)
		{
			if (columns == null)
			{
				writer.Write(newline, 0, newline.Length);
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
							writer.Write(newline, 0, newline.Length);
							return;
						}
						writer.Write(separator, 0, separator.Length);
					}
				}
				else
				{
					writer.Write(newline, 0, newline.Length);
					return;
				}
			}
		}

		public static void WriteLine(Stream writer, byte[][] columns, byte[] separator, byte[] newline)
		{
			if (columns == null || columns.Length <= 0)
			{
				writer.Write(newline, 0, newline.Length);
				return;
			}
			for (var offset = 0; ;)
			{
				var column = columns[offset];
				if (column != null)
				{
					writer.Write(column, 0, column.Length);
				}
				++offset;
				if (offset >= columns.Length)
				{
					writer.Write(newline, 0, newline.Length);
					return;
				}
				writer.Write(separator, 0, separator.Length);
			}
		}

		public static void WriteLine(Stream writer, IEnumerable<byte[]> columns, byte[] separator, byte[] newline)
		{
			if (columns == null)
			{
				writer.Write(newline, 0, newline.Length);
				return;
			}
			using (var enumerator = columns.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					while (true)
					{
						var column = enumerator.Current;
						if (column != null)
						{
							writer.Write(column, 0, column.Length);
						}
						if (!enumerator.MoveNext())
						{
							writer.Write(newline, 0, newline.Length);
							return;
						}
						writer.Write(separator, 0, separator.Length);
					}
				}
				else
				{
					writer.Write(newline, 0, newline.Length);
					return;
				}
			}
		}
		#endregion //Class Methods

		#region Fields
		private int capacity;
		#endregion //Fields

		#region Constructors
		public BytesColumns()
			: this(DefaultCapacity)
		{
		}

		public BytesColumns(int capacity)
			: base()
		{
			this.capacity = capacity;
		}
		#endregion //Constructors

		#region Properties
		public int Capacity { get => capacity; set => capacity = value; }
		#endregion //Properties

		#region Methods
		public BytesColumn Dequeue()
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
				var value = new BytesColumn(capacity);
				columns.Add(value);
				return value;
			}
		}

		public BytesColumn Remove()
		{
			if (queue.Count > 0)
			{
				var value = queue.Dequeue();
				value.Clear();
				return value;
			}
			else
			{
				var value = new BytesColumn(capacity);
				return value;
			}
		}

		public Action<byte[], int, int> AddLength()
		{
			var value = Dequeue();
			return value.AppendLength;
		}

		public Action<byte[], int, int> AddEnding()
		{
			var value = Dequeue();
			return value.AppendEnding;
		}

		public string ToString(char separator)
		{
			return ToString(Encoding.UTF8, separator);
		}

		public string ToString(Encoding encoding, char separator)
		{
			if (columns.Count <= 0)
			{
				return string.Empty;
			}
			var builder = new StringBuilder();
			for (var i = 0; ; )
			{
				var column = columns[i];
				var columnText = column.ToString(encoding);
				builder.Append(columnText);
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

		public string ToString(Decoder decoder, char separator)
		{
			if (columns.Count <= 0)
			{
				return string.Empty;
			}
			var builder = new StringBuilder();
			for (var i = 0; ; )
			{
				var column = columns[i];
				var columnText = column.ToString(decoder);
				builder.Append(columnText);
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

		public string ToString(char separator, ref char[] decoded)
		{
			return ToString(Encoding.UTF8, separator, ref decoded);
		}

		public string ToString(Encoding encoding, char separator, ref char[] decoded)
		{
			if (columns.Count <= 0)
			{
				return string.Empty;
			}
			var builder = new StringBuilder();
			for (var i = 0; ; )
			{
				var column = columns[i];
				var length = column.ToChars(encoding, ref decoded);
				builder.Append(decoded, 0, length);
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

		public string ToString(Decoder decoder, char separator, ref char[] decoded)
		{
			if (columns.Count <= 0)
			{
				return string.Empty;
			}
			var builder = new StringBuilder();
			for (var i = 0; ; )
			{
				var column = columns[i];
				var length = column.ToChars(decoder, ref decoded);
				builder.Append(decoded, 0, length);
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
			return ToString(Encoding.UTF8, separator);
		}

		public string ToString(Encoding encoding, string separator)
		{
			if (columns.Count <= 0)
			{
				return string.Empty;
			}
			var builder = new StringBuilder();
			for (var i = 0; ; )
			{
				var column = columns[i];
				var columnText = column.ToString(encoding);
				builder.Append(columnText);
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

		public string ToString(Decoder decoder, string separator)
		{
			if (columns.Count <= 0)
			{
				return string.Empty;
			}
			var builder = new StringBuilder();
			for (var i = 0; ; )
			{
				var column = columns[i];
				var columnText = column.ToString(decoder);
				builder.Append(columnText);
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

		public string ToString(string separator, ref char[] decoded)
		{
			return ToString(Encoding.UTF8, separator, ref decoded);
		}

		public string ToString(Encoding encoding, string separator, ref char[] decoded)
		{
			if (columns.Count <= 0)
			{
				return string.Empty;
			}
			var builder = new StringBuilder();
			for (var i = 0; ; )
			{
				var column = columns[i];
				var length = column.ToChars(encoding, ref decoded);
				builder.Append(decoded, 0, length);
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

		public string ToString(Decoder decoder, string separator, ref char[] decoded)
		{
			if (columns.Count <= 0)
			{
				return string.Empty;
			}
			var builder = new StringBuilder();
			for (var i = 0; ; )
			{
				var column = columns[i];
				var length = column.ToChars(decoder, ref decoded);
				builder.Append(decoded, 0, length);
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
			return ToStringsArray(Encoding.UTF8);
		}

		public string[] ToStringsArray(Encoding encoding)
		{
			var values = new string[columns.Count];
			for (var i = values.Length - 1; i >= 0; --i)
			{
				var value = columns[i].ToString(encoding);
				values[i] = value;
			}
			return values;
		}

		public string[] ToStringsArray(Decoder decoder)
		{
			var values = new string[columns.Count];
			for (var i = values.Length - 1; i >= 0; --i)
			{
				var value = columns[i].ToString(decoder);
				values[i] = value;
			}
			return values;
		}

		public List<string> ToStringsList()
		{
			return ToStringsList(Encoding.UTF8);
		}

		public List<string> ToStringsList(Encoding encoding)
		{
			var values = new List<string>(columns.Count);
			foreach (var column in columns)
			{
				var value = column.ToString(encoding);
				values.Add(value);
			}
			return values;
		}

		public List<string> ToStringsList(Decoder decoder)
		{
			var values = new List<string>(columns.Count);
			foreach (var column in columns)
			{
				var value = column.ToString(decoder);
				values.Add(value);
			}
			return values;
		}

		public void ToStringsAdd<T>(T values)
			where T: IList<string>
		{
			ToStringsAdd(values, Encoding.UTF8);
		}

		public void ToStringsAdd<T>(T values, Encoding encoding)
			where T: IList<string>
		{
			foreach (var column in columns)
			{
				var value = column.ToString(encoding);
				values.Add(value);
			}
		}

		public void ToStringsAdd<T>(T values, Decoder decoder)
			where T: IList<string>
		{
			foreach (var column in columns)
			{
				var value = column.ToString(decoder);
				values.Add(value);
			}
		}

		public void ToStringsAdd(Action<string> add)
		{
			ToStringsAdd(Encoding.UTF8, add);
		}

		public void ToStringsAdd(Encoding encoding, Action<string> add)
		{
			foreach (var column in columns)
			{
				var value = column.ToString(encoding);
				add(value);
			}
		}

		public void ToStringsAdd(Decoder decoder, Action<string> add)
		{
			foreach (var column in columns)
			{
				var value = column.ToString(decoder);
				add(value);
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

		public void Write(TextWriter writer, char separator, ref char[] decoded)
		{
			if (columns.Count <= 0)
			{
				return;
			}
			for (var offset = 0; ;)
			{
				columns[offset].Write(writer, ref decoded);
				++offset;
				if (offset >= columns.Count)
				{
					return;
				}
				writer.Write(separator);
			}
		}

		public void Write(TextWriter writer, string separator, ref char[] decoded)
		{
			if (columns.Count <= 0)
			{
				return;
			}
			for (var offset = 0; ;)
			{
				columns[offset].Write(writer, ref decoded);
				++offset;
				if (offset >= columns.Count)
				{
					return;
				}
				writer.Write(separator);
			}
		}

		public void Write(TextWriter writer, char separator, Encoding encoding, ref char[] decoded)
		{
			if (columns.Count <= 0)
			{
				return;
			}
			for (var offset = 0; ;)
			{
				columns[offset].Write(writer, encoding, ref decoded);
				++offset;
				if (offset >= columns.Count)
				{
					return;
				}
				writer.Write(separator);
			}
		}

		public void Write(TextWriter writer, string separator, Encoding encoding, ref char[] decoded)
		{
			if (columns.Count <= 0)
			{
				return;
			}
			for (var offset = 0; ;)
			{
				columns[offset].Write(writer, encoding, ref decoded);
				++offset;
				if (offset >= columns.Count)
				{
					return;
				}
				writer.Write(separator);
			}
		}

		public void Write(TextWriter writer, char separator, Decoder decoder, ref char[] decoded)
		{
			if (columns.Count <= 0)
			{
				return;
			}
			for (var offset = 0; ;)
			{
				columns[offset].Write(writer, decoder, ref decoded);
				++offset;
				if (offset >= columns.Count)
				{
					return;
				}
				writer.Write(separator);
			}
		}

		public void Write(TextWriter writer, string separator, Decoder decoder, ref char[] decoded)
		{
			if (columns.Count <= 0)
			{
				return;
			}
			for (var offset = 0; ;)
			{
				columns[offset].Write(writer, decoder, ref decoded);
				++offset;
				if (offset >= columns.Count)
				{
					return;
				}
				writer.Write(separator);
			}
		}

		public void WriteLine(TextWriter writer, char separator, ref char[] decoded)
		{
			if (columns.Count <= 0)
			{
				writer.WriteLine();
				return;
			}
			for (var offset = 0; ;)
			{
				columns[offset].Write(writer, ref decoded);
				++offset;
				if (offset >= columns.Count)
				{
					writer.WriteLine();
					return;
				}
				writer.Write(separator);
			}
		}

		public void WriteLine(TextWriter writer, string separator, ref char[] decoded)
		{
			if (columns.Count <= 0)
			{
				writer.WriteLine();
				return;
			}
			for (var offset = 0; ;)
			{
				columns[offset].Write(writer, ref decoded);
				++offset;
				if (offset >= columns.Count)
				{
					writer.WriteLine();
					return;
				}
				writer.Write(separator);
			}
		}

		public void WriteLine(TextWriter writer, char separator, Encoding encoding, ref char[] decoded)
		{
			if (columns.Count <= 0)
			{
				writer.WriteLine();
				return;
			}
			for (var offset = 0; ;)
			{
				columns[offset].Write(writer, encoding, ref decoded);
				++offset;
				if (offset >= columns.Count)
				{
					writer.WriteLine();
					return;
				}
				writer.Write(separator);
			}
		}

		public void WriteLine(TextWriter writer, string separator, Encoding encoding, ref char[] decoded)
		{
			if (columns.Count <= 0)
			{
				writer.WriteLine();
				return;
			}
			for (var offset = 0; ;)
			{
				columns[offset].Write(writer, encoding, ref decoded);
				++offset;
				if (offset >= columns.Count)
				{
					writer.WriteLine();
					return;
				}
				writer.Write(separator);
			}
		}

		public void WriteLine(TextWriter writer, char separator, Decoder decoder, ref char[] decoded)
		{
			if (columns.Count <= 0)
			{
				writer.WriteLine();
				return;
			}
			for (var offset = 0; ;)
			{
				columns[offset].Write(writer, decoder, ref decoded);
				++offset;
				if (offset >= columns.Count)
				{
					writer.WriteLine();
					return;
				}
				writer.Write(separator);
			}
		}

		public void WriteLine(TextWriter writer, string separator, Decoder decoder, ref char[] decoded)
		{
			if (columns.Count <= 0)
			{
				writer.WriteLine();
				return;
			}
			for (var offset = 0; ;)
			{
				columns[offset].Write(writer, decoder, ref decoded);
				++offset;
				if (offset >= columns.Count)
				{
					writer.WriteLine();
					return;
				}
				writer.Write(separator);
			}
		}

		public void Write(Stream writer, byte separator)
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
				writer.WriteByte(separator);
			}
		}

		public void Write(Stream writer, byte[] separator)
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
				writer.Write(separator, 0, separator.Length);
			}
		}

		public void WriteLine(Stream writer, byte separator, byte[] newline)
		{
			if (columns.Count <= 0)
			{
				writer.Write(newline, 0, newline.Length);
				return;
			}
			for (var offset = 0; ;)
			{
				columns[offset].Write(writer);
				++offset;
				if (offset >= columns.Count)
				{
					writer.Write(newline, 0, newline.Length);
					return;
				}
				writer.WriteByte(separator);
			}
		}

		public void WriteLine(Stream writer, byte[] separator, byte[] newline)
		{
			if (columns.Count <= 0)
			{
				writer.Write(newline, 0, newline.Length);
				return;
			}
			for (var offset = 0; ;)
			{
				columns[offset].Write(writer);
				++offset;
				if (offset >= columns.Count)
				{
					writer.Write(newline, 0, newline.Length);
					return;
				}
				writer.Write(separator, 0, separator.Length);
			}
		}
		#endregion //Methods
	}
}
