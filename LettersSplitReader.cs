using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace Innovoft.IO
{
	public sealed partial class LettersSplitReader : IDisposable
	{
		#region Constants
		public const char CR = '\r';
		public const char LF = '\n';
		#endregion //Constants

		#region Fields
		private Stream stream;
		private Func<byte[], int, int, int> streamRead;
		private readonly Encoding encoding;
		private readonly Decoder decoder;
		private readonly Func<byte[], int, int, char[], int, int> decoderGetChars;
		private readonly int length;
		private readonly byte[] buffer;
		private readonly char[] letters;
		private int lettersOffset;
		private int lettersLength;
		#endregion //Fields

		#region Constructors
		public LettersSplitReader(Stream stream)
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
		~LettersSplitReader()
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

		private bool ReadBuffers()
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
