using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Innovoft.IO
{
	public abstract class SplitColumns<TColumn, TValue>
		where TColumn: SplitColumn<TValue> where TValue: IEquatable<TValue>
	{
		#region Class Methods
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Clear(TColumn[] values)
		{
			foreach (var value in values)
			{
				value?.Clear();
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Clear<T>(T values)
			where T : IList<TColumn>
		{
			foreach (var value in values)
			{
				value?.Clear();
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Clear(IEnumerable<TColumn> values)
		{
			foreach (var value in values)
			{
				value?.Clear();
			}
		}
		#endregion //Class Methods

		#region Fields
		protected readonly List<TColumn> columns = new List<TColumn>();
		protected readonly Queue<TColumn> queue = new Queue<TColumn>();
		#endregion //Fields

		#region Constructors
		protected SplitColumns()
		{
		}
		#endregion //Constructors

		#region Properties
		public List<TColumn> Columns => columns;
		public int Count => columns.Count;
		public Queue<TColumn> Queue => queue;
		public int Queued => queue.Count;
		#endregion //Properties

		#region Indexers
		public TColumn this[int offset] => columns[offset];
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

		public bool TryGetColumn(int offset, out TColumn column)
		{
			if (offset < columns.Count)
			{
				column = columns[offset];
				return true;
			}
			else
			{
				column = null;
				return false;
			}
		}

		public void Enqueue(TColumn value)
		{
			if (value == null)
			{
				return;
			}

			queue.Enqueue(value);
		}

		public void Enqueue(TColumn[] values)
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
			where T : IList<TColumn>
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

		public void Enqueue(IEnumerable<TColumn> values)
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

		public TValue[][] ToArrays()
		{
			var values = new TValue[columns.Count][];
			for (var i = values.Length - 1; i >= 0; --i)
			{
				values[i] = columns[i].ToArray();
			}
			return values;
		}
		#endregion //Methods
	}
}
