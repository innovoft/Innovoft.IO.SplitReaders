﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace Innovoft.IO
{
	public sealed partial class BytesSplitReader : SplitReader
	{
		#region Constants
		public const byte Tab = 0x09;
		public const byte CR = 0x0D;
		public const byte LF = 0x0A;
		public const byte Space = 0x20;
		public const byte Comma = 0x2C;
		public const byte Pipe = 0x7C;
		#endregion //Constants

		#region Fields
		private int bufferOffset;
		private int bufferLength;

		private long position;
		#endregion //Fields

		#region Constructors
		public BytesSplitReader()
			: base()
		{
		}

		public BytesSplitReader(Stream stream)
			: base()
		{
			OpenOnly(stream);
		}

		public BytesSplitReader(Stream stream, bool dispose)
			: base()
		{
			OpenOnly(stream, dispose);
		}

		public BytesSplitReader(Stream stream, Action dispose)
			: base()
		{
			OpenOnly(stream, dispose);
		}
		#endregion //Constructors

		#region Properties
		public long Position => position;
		#endregion //Properties

		#region Methods
		public new void OpenOnly(Stream stream)
		{
			base.OpenOnly(stream);

			bufferOffset = 0;
			bufferLength = 0;
			position = 0;
		}

		public new void OpenOnly(Stream stream, bool dispose)
		{
			base.OpenOnly(stream, dispose);

			bufferOffset = 0;
			bufferLength = 0;
			position = 0;
		}

		public new void OpenOnly(Stream stream, Action dispose)
		{
			base.OpenOnly(stream, dispose);

			bufferOffset = 0;
			bufferLength = 0;
			position = 0;
		}

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
					position += bufferOffset;
					return true;

				case LF:
					++bufferOffset;
					position += bufferOffset;
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
			position = stream.Seek(offset, origin);
			return position;
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
			position += bufferLength;
			bufferOffset = 0;
			bufferLength = streamRead(buffer, 0, length);
			return bufferLength > 0;
		}
		#endregion //Methods
	}
}
