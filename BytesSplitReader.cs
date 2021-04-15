using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace Innovoft.IO
{
	public sealed partial class BytesSplitReader : SplitReader
	{
		#region Constants
		public const byte CR = 0x0D;
		public const byte LF = 0x0A;
		public const byte Space = 0x20;
		public const byte Comma = 0x2C;
		#endregion //Constants

		#region Fields
		private int bufferOffset;
		private int bufferLength;
		#endregion //Fields

		#region Constructors
		public BytesSplitReader(Stream stream)
			: base(stream)
		{
		}
		#endregion //Constructors

		#region Methods
		public bool ReadColumns()
		{
			if (stream == null)
			{
				throw new ObjectDisposedException(nameof(BytesSplitReader));
			}
			if (bufferOffset >= bufferLength && !ReadBuffer())
			{
				return false;
			}
			while (true)
			{
				var letter = buffer[bufferOffset];
				switch (letter)
				{
				case CR:
					++bufferOffset;
					//LF
					if (bufferOffset >= bufferLength)
					{
						if (!ReadBuffer())
						{
							return true;
						}
					}
					if (buffer[bufferOffset] == LF)
					{
						++bufferOffset;
					}
					return true;

				case LF:
					++bufferOffset;
					return true;

				default:
					++bufferOffset;
					break;
				}
				if (bufferOffset >= bufferLength)
				{
					if (!ReadBuffer())
					{
						return true;
					}
				}
			}
		}

		public long Seek(long offset, SeekOrigin origin)
		{
			bufferOffset = 0;
			bufferLength = 0;
			return stream.Seek(offset, origin);
		}

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
