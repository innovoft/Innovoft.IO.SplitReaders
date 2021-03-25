using System;
using System.Collections.Generic;
using System.Text;

namespace Innovoft.IO
{
	internal sealed class CharsColumns
	{
		#region Fields
		private readonly List<CharsColumn> columns = new List<CharsColumn>();
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
		public Action<char[], int, int> AddLength()
		{
			var column = new CharsColumn();
			var append = new Action<char[], int, int>(column.AppendLength);
			columns.Add(column);
			return append;
		}

		public string[] ToArray()
		{
			var columns = new string[this.columns.Count];
			for (var i = columns.Length - 1; i >= 0; --i)
			{
				var column = this.columns[i].ToString();
				columns[i] = column;
			}
			return columns;
		}

		public List<string> ToList()
		{
			var columns = new List<string>(this.columns.Count);
			foreach (var column in this.columns)
			{
				var text = column.ToString();
				columns.Add(text);
			}
			return columns;
		}
		#endregion //Methods
	}
}
