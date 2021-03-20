using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace Innovoft.IO
{
	public sealed class SplitReader : IDisposable
	{
		#region Constants
		private const char CR = '\r';
		private const char LF = '\n';
		#endregion //Constants

		#region Fields
		private Stream stream;
		private Func<byte[], int, int, int> streamRead;
		private readonly Encoding encoding;
		private readonly Decoder decoder;
		private readonly Func<byte[], int, int, char[], int, int> decoderGetChars;
		private readonly int length;
		private readonly byte[] raw;
		private readonly char[] chars;
		private int charsOffset;
		private int charsLength;
		#endregion //Fields

		#region Constructors
		public SplitReader(Stream stream)
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
			this.raw = new byte[length];
			this.chars = new char[length];
			this.charsOffset = 0;
			this.charsLength = 0;
		}
		#endregion //Constructors

		#region Finalizer
		~SplitReader()
		{
			Dispose(disposing: false);
		}
		#endregion //Finalizer

		#region Properties
		public Encoding Encoding => encoding;
		#endregion //Properties

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

		public bool ReadLine(char separator, List<string> columns)
		{
			if (stream == null)
			{
				throw new ObjectDisposedException(nameof(SplitReader));
			}
			throw new NotImplementedException();
		}

		private bool ReadBuffers()
		{
			while (true)
			{
				var read = streamRead(raw, 0, length);
				if (read <= 0)
				{
					return false;
				}
				charsOffset = 0;
				charsLength = decoderGetChars(raw, 0, read, chars, 0);
				if (charsLength > 0)
				{
					return true;
				}
			}
		}
		#endregion //Methods
	}
}
