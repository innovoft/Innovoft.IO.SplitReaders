using System;
using System.Collections.Generic;
using System.Text;

namespace Innovoft.IO
{
	internal sealed class BufferColumn
	{
		#region Fields
		private Encoding encoding;
		private int buffersLength;
		private byte[] buffer;
		private int bufferOffset;
		#endregion //Fields

		#region Constructors
		public BufferColumn(Encoding encoding)
			: this(encoding, 128)
		{
		}

		public BufferColumn(Encoding encoding, int length)
		{
			this.encoding = encoding;
			this.buffersLength = length;
			this.buffer = new byte[length];
			this.bufferOffset = 0;
		}
		#endregion //Constructors

		#region Methods
		public void AppendLength(byte[] append, int offset, int length)
		{
			var size = bufferOffset + length;
			if (size > buffersLength)
			{
				var tempLength = 2 * buffersLength;
				var temp = new byte[tempLength];
				Buffer.BlockCopy(buffer, 0, temp, 0, bufferOffset);
				buffersLength = tempLength;
				buffer = temp;
			}
			Buffer.BlockCopy(append, offset, buffer, bufferOffset, length);
			bufferOffset += length;
		}

		public override string ToString()
		{
			return encoding.GetString(buffer, 0, bufferOffset);
		}
		#endregion //Methods
	}
}
