using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Innovoft.IO
{
	public sealed class CharsColumns : SplitColumns<CharsColumn, char>
	{
		#region Constants
		public const int DefaultCapacity = CharsColumn.DefaultCapacity;
		#endregion //Constants

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
		#endregion //Methods
	}
}
