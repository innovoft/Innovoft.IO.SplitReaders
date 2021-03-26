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
		private int lettersOffset;
		#endregion //Fields

		#region Constructors
		public CharsColumn()
			: this(128)
		{
		}

		public CharsColumn(int length)
		{
			this.capacity = length;
			this.letters = new char[length];
			this.lettersOffset = 0;
		}
		#endregion //Constructors

		#region Properties
		public int Capacity => capacity;
		#endregion //Properties

		#region Methods
		public void AppendLength(char[] append, int offset, int length)
		{
			var size = lettersOffset + length;
			if (size > capacity)
			{
				var tempLength = 2 * capacity;
				var temp = new char[tempLength];
				Buffer.BlockCopy(letters, 0, temp, 0, lettersOffset);
				capacity = tempLength;
				letters = temp;
			}
			Array.Copy(append, offset, letters, lettersOffset, length);
			lettersOffset += length;
		}

		public override string ToString()
		{
			return new string(letters, 0, lettersOffset);
		}
		#endregion //Methods
	}
}
