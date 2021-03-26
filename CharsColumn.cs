using System;
using System.Collections.Generic;
using System.Text;

namespace Innovoft.IO
{
	public class CharsColumn
	{
		#region Fields
		private int capacity;
		private char[] letters;
		private int count;
		#endregion //Fields

		#region Constructors
		public CharsColumn()
			: this(128)
		{
		}

		public CharsColumn(int capacity)
		{
			this.capacity = capacity;
			this.letters = new char[capacity];
			this.count = 0;
		}
		#endregion //Constructors

		#region Properties
		public int Capacity => capacity;
		public int Count => count;
		#endregion //Properties

		#region Methods
		public void AppendLength(char[] append, int offset, int length)
		{
			var size = count + length;
			if (size > capacity)
			{
				var tempLength = 2 * capacity;
				var temp = new char[tempLength];
				Buffer.BlockCopy(letters, 0, temp, 0, count);
				capacity = tempLength;
				letters = temp;
			}
			Array.Copy(append, offset, letters, count, length);
			count += length;
		}

		public override string ToString()
		{
			return new string(letters, 0, count);
		}
		#endregion //Methods
	}
}
