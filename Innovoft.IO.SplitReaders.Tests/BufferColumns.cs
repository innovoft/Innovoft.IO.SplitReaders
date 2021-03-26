using System;
using System.Collections.Generic;
using System.Text;

namespace Innovoft.IO
{
	internal sealed class BufferColumns
	{
		#region Fields
		private readonly List<BufferColumn> columns = new List<BufferColumn>();
		#endregion //Fields

		#region Constructors
		public BufferColumns()
		{
		}
		#endregion //Constructors

		#region Properties
		public List<BufferColumn> Columns => columns;
		public int Count => columns.Count;
		#endregion //Properties

		#region Indexers
		public BufferColumn this[int i] => columns[i];
		#endregion //Indexers

		#region Methods
		public Action<byte[], int, int> AddLength()
		{
			var column = new BufferColumn();
			var append = new Action<byte[], int, int>(column.AppendLength);
			columns.Add(column);
			return append;
		}

		public string[] ToArray(Encoding encoding)
		{
			var columns = new string[this.columns.Count];
			for (var i = columns.Length - 1; i >= 0; --i)
			{
				var column = this.columns[i].ToString(encoding);
				columns[i] = column;
			}
			return columns;
		}

		public List<string> ToList(Encoding encoding)
		{
			var columns = new List<string>(this.columns.Count);
			foreach (var column in this.columns)
			{
				var text = column.ToString(encoding);
				columns.Add(text);
			}
			return columns;
		}
		#endregion //Methods
	}
}
