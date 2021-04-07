using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Innovoft.IO
{
	public sealed class BufferColumns
	{
		#region Class Methods
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Clear(BufferColumn[] values)
		{
			foreach (var value in values)
			{
				value?.Clear();
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Clear<T>(T values)
			where T: IList<BufferColumn>
		{
			foreach (var value in values)
			{
				value?.Clear();
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Clear(IEnumerable<BufferColumn> values)
		{
			foreach (var value in values)
			{
				value?.Clear();
			}
		}
		#endregion //Class Methods

		#region Fields
		private readonly List<BufferColumn> columns = new List<BufferColumn>();
		private readonly Queue<BufferColumn> queue = new Queue<BufferColumn>();
		#endregion //Fields

		#region Constructors
		public BufferColumns()
		{
		}
		#endregion //Constructors

		#region Properties
		public List<BufferColumn> Columns => columns;
		public int Count => columns.Count;
		public Queue<BufferColumn> Queue => queue;
		public int Queued => queue.Count;
		#endregion //Properties

		#region Indexers
		public BufferColumn this[int i] => columns[i];
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

		public void Enqueue(BufferColumn value)
		{
			if (value == null)
			{
				return;
			}

			queue.Enqueue(value);
		}

		public void Enqueue(BufferColumn[] values)
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
			where T : IList<BufferColumn>
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

		public void Enqueue(IEnumerable<BufferColumn> values)
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

		public BufferColumn Dequeue()
		{
			if (queue.Count > 0)
			{
				var value = queue.Dequeue();
				value.Clear();
				return value;
			}
			else
			{
				return new BufferColumn();
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
