using System;
using System.Collections.Generic;
using System.Text;

namespace Innovoft.IO
{
	public sealed class CharsColumns
	{
		#region Fields
		private readonly List<CharsColumn> columns = new List<CharsColumn>();
		private readonly Queue<CharsColumn> queue = new Queue<CharsColumn>();
		#endregion //Fields

		#region Constructors
		public CharsColumns()
		{
		}
		#endregion //Constructors

		#region Properties
		public List<CharsColumn> Columns => columns;
		public int Count => columns.Count;
		#endregion //Properties

		#region Indexers
		public CharsColumn this[int i] => columns[i];
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

		public void Enqueue(CharsColumn value)
		{
			queue.Enqueue(value);
		}

		public CharsColumn Dequeue()
		{
			return queue.Count > 0 ? queue.Dequeue() : new CharsColumn();
		}

		public Action<char[], int, int> AddLength()
		{
			var column = Dequeue();
			columns.Add(column);
			return column.AppendLength;
		}

		public Action<char[], int, int> AddEnding()
		{
			var column = Dequeue();
			columns.Add(column);
			return column.AppendEnding;
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
		#endregion //Methods
	}
}
