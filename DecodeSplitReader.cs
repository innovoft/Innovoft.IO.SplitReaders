using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;

namespace Innovoft.IO
{
	public abstract class DecodeSplitReader : IDisposable
	{
		#region Constants
		public const char CR = '\r';
		public const char LF = '\n';
		#endregion //Constants

		#region Fields
		protected Stream stream;
		protected Func<byte[], int, int, int> streamRead;
		protected readonly Encoding encoding;
		protected readonly Decoder decoder;
		protected readonly Func<byte[], int, int, char[], int, int> decoderGetChars;
		protected readonly int length;
		protected readonly byte[] buffer;
		protected readonly char[] letters;
		protected int lettersOffset;
		protected int lettersLength;
		#endregion //Fields

		#region Constructors
		protected DecodeSplitReader(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException(nameof(stream));
			}
			if (!stream.CanRead)
			{
				throw new ArgumentException("!stream.CanRead");
			}
			this.stream = stream;
			this.streamRead = stream.Read;
			this.encoding = System.Text.Encoding.UTF8;
			this.decoder = encoding.GetDecoder();
			this.decoderGetChars = decoder.GetChars;
			this.length = 4096;
			this.buffer = new byte[length];
			this.letters = new char[length];
			this.lettersOffset = 0;
			this.lettersLength = 0;
		}
		#endregion //Constructors

		#region Finalizer
		~DecodeSplitReader()
		{
			Dispose(disposing: false);
		}
		#endregion //Finalizer

		#region Methods
		#region Dispose
		public void Dispose()
		{
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}

		private void Dispose(bool disposing)
		{
			Interlocked.Exchange(ref stream, null)?.Dispose();
			streamRead = null;
		}
		#endregion //Dispose

		public bool ReadColumns()
		{
			if (stream == null)
			{
				throw new ObjectDisposedException(nameof(StringsSplitReader));
			}
			if (lettersOffset >= lettersLength && !ReadBuffers())
			{
				return false;
			}
			while (true)
			{
				var letter = letters[lettersOffset];
				switch (letter)
				{
				case CR:
					++lettersOffset;
					//LF
					if (lettersOffset >= lettersLength)
					{
						if (!ReadBuffers())
						{
							return true;
						}
					}
					if (letters[lettersOffset] == LF)
					{
						++lettersOffset;
					}
					return true;

				case LF:
					++lettersOffset;
					return true;

				default:
					++lettersOffset;
					break;
				}
				if (lettersOffset >= lettersLength)
				{
					if (!ReadBuffers())
					{
						return true;
					}
				}
			}
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
				lettersOffset = 0;
				lettersLength = decoderGetChars(buffer, 0, read, letters, 0);
				if (lettersLength > 0)
				{
					return true;
				}
			}
		}
		#endregion //Methods
	}
}
