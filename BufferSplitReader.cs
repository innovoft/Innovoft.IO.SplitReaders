using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace Innovoft.IO
{
	public sealed partial class BufferSplitReader : SplitReader
	{
		#region Constants
		public const byte CR = 0x0D;
		public const byte LF = 0x0A;
		public const byte Comma = 0x2C;
		#endregion //Constants

		#region Fields
		private int bufferOffset;
		private int bufferLength;
		#endregion //Fields

		#region Constructors
		public BufferSplitReader(Stream stream)
			: base(stream)
		{
		}
		#endregion //Constructors

		#region Methods
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool Contains(byte[] values, byte value)
		{
			for (var i = values.Length - 1; i >= 0; --i)
			{
				if (values[i] == value)
				{
					return true;
				}
			}
			return false;
		}

		private bool ReadBuffer()
		{
			bufferOffset = 0;
			bufferLength = streamRead(buffer, 0, length);
			return bufferLength > 0;
		}
		#endregion //Methods
	}
}
