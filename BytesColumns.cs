using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Innovoft.IO
{
	public sealed class BytesColumns
	{
		#region Constants
		public const int DefaultCapacity = BytesColumn.DefaultCapacity;
		#endregion //Constants

		#region Class Methods
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Clear(BytesColumn[] values)
		{
			foreach (var value in values)
			{
				value?.Clear();
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Clear<T>(T values)
			where T: IList<BytesColumn>
		{
			foreach (var value in values)
			{
				value?.Clear();
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Clear(IEnumerable<BytesColumn> values)
		{
			foreach (var value in values)
			{
				value?.Clear();
			}
		}
		#endregion //Class Methods

		#region Fields
		private readonly List<BytesColumn> columns = new List<BytesColumn>();
		private readonly Queue<BytesColumn> queue = new Queue<BytesColumn>();
		private int capacity;
		#endregion //Fields

		#region Constructors
		public BytesColumns()
			: this(DefaultCapacity)
		{
		}

		public BytesColumns(int capacity)
		{
			this.capacity = capacity;
		}
		#endregion //Constructors

		#region Properties
		public List<BytesColumn> Columns => columns;
		public int Count => columns.Count;
		public Queue<BytesColumn> Queue => queue;
		public int Queued => queue.Count;
		public int Capacity { get => capacity; set => capacity = value; }
		#endregion //Properties

		#region Indexers
		public BytesColumn this[int i] => columns[i];
		#endregion //Indexers

		#region Methods
		public void Clear()
		{
			foreach (var column in columns)
			{
				queue.Enqueue(column);
			}
			columns.Clear();
		}

		public void Enqueue(BytesColumn value)
		{
			if (value == null)
			{
				return;
			}

			queue.Enqueue(value);
		}

		public void Enqueue(BytesColumn[] values)
		{
			foreach (var value in values)
			{
				if (value == null)
				{
					continue;
				}

				queue.Enqueue(value);
			}
		}

		public void Enqueue<T>(T values)
			where T : IList<BytesColumn>
		{
			foreach (var value in values)
			{
				if (value == null)
				{
					continue;
				}

				queue.Enqueue(value);
			}
		}

		public void Enqueue(IEnumerable<BytesColumn> values)
		{
			foreach (var value in values)
			{
				if (value == null)
				{
					continue;
				}

				queue.Enqueue(value);
			}
		}

		public BytesColumn Dequeue()
		{
			if (queue.Count > 0)
			{
				var value = queue.Dequeue();
				value.Clear();
				return value;
			}
			else
			{
				return new BytesColumn(capacity);
			}
		}

		public Action<byte[], int, int> AddLength()
		{
			var column = Dequeue();
			columns.Add(column);
			return column.AppendLength;
		}

		public Action<byte[], int, int> AddEnding()
		{
			var column = Dequeue();
			columns.Add(column);
			return column.AppendEnding;
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
		#endregion //Methods
	}
}
