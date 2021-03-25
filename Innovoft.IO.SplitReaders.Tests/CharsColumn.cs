using System;
using System.Collections.Generic;
using System.Text;

namespace Innovoft.IO
{
	internal sealed class CharsColumn
	{
		#region Fields
		private int lettersLength;
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
			this.lettersLength = length;
			this.letters = new char[length];
			this.lettersOffset = 0;
		}
		#endregion //Constructors

		#region Methods
		public void AppendLength(char[] append, int offset, int length)
		{
			var size = lettersOffset + length;
			if (size > lettersLength)
			{
				var tempLength = 2 * lettersLength;
				var temp = new char[tempLength];
				Buffer.BlockCopy(letters, 0, temp, 0, lettersOffset);
				lettersLength = tempLength;
				letters = temp;
			}
			Buffer.BlockCopy(append, offset, letters, lettersOffset, length);
			lettersOffset += length;
		}

		public override string ToString()
		{
			return new string(letters, 0, lettersOffset);
		}
		#endregion //Methods
	}
}
