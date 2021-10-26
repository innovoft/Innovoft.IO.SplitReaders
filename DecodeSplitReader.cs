using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;

namespace Innovoft.IO
{
	public abstract class DecodeSplitReader : SplitReader
	{
		#region Constants
		public const char Tab = '\t';
		public const char CR = '\r';
		public const char LF = '\n';
		public const char Space = ' ';
		public const char Comma = ',';
		#endregion //Constants

		#region Fields
		protected readonly Encoding encoding;
		protected readonly Decoder decoder;
		protected readonly Func<byte[], int, int, char[], int, int> decoderGetChars;
		protected readonly char[] decoded;
		protected int decodedOffset;
		protected int decodedLength;
		#endregion //Fields

		#region Constructors
		protected DecodeSplitReader()
			: base()
		{
			this.encoding = System.Text.Encoding.UTF8;
			this.decoder = encoding.GetDecoder();
			this.decoderGetChars = decoder.GetChars;
			this.decoded = new char[length];
		}
		#endregion //Constructors

		#region Properties
		public Encoding Encoding => encoding;
		#endregion //Properties

		#region Methods
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected new void OpenOnly(Stream stream, bool dispose)
		{
			base.OpenOnly(stream, dispose);

			decodedOffset = 0;
			decodedLength = 0;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected new void OpenOnly(Stream stream, Action dispose)
		{
			base.OpenOnly(stream, dispose);

			decodedOffset = 0;
			decodedLength = 0;
		}

		public bool ReadColumns()
		{
			if (stream == null)
			{
				throw new ObjectDisposedException(this.GetType().Name);
			}
			if (decodedOffset >= decodedLength && !ReadBuffers())
			{
				return false;
			}
			while (true)
			{
				var letter = decoded[decodedOffset];
				switch (letter)
				{
				case CR:
					++decodedOffset;
					//LF
					if (decodedOffset >= decodedLength)
					{
						if (!ReadBuffers())
						{
							return true;
						}
					}
					if (decoded[decodedOffset] == LF)
					{
						++decodedOffset;
					}
					return true;

				case LF:
					++decodedOffset;
					return true;

				default:
					++decodedOffset;
					break;
				}
				if (decodedOffset >= decodedLength)
				{
					if (!ReadBuffers())
					{
						return true;
					}
				}
			}
		}

		public long Seek(long offset, SeekOrigin origin)
		{
			decodedOffset = 0;
			decodedLength = 0;
			return stream.Seek(offset, origin);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected static bool Contains(char[] values, char value)
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

		protected bool ReadBuffers()
		{
			while (true)
			{
				var read = streamRead(buffer, 0, length);
				if (read <= 0)
				{
					return false;
				}
				decodedOffset = 0;
				decodedLength = decoderGetChars(buffer, 0, read, decoded, 0);
				if (decodedLength > 0)
				{
					return true;
				}
			}
		}
		#endregion //Methods
	}
}
