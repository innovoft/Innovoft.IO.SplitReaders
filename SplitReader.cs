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
		private readonly Encoding encoding;
		private readonly Decoder decoder;
		private readonly int length;
		private readonly byte[] raw;
		private int rawOffset;
		private int rawLength;
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
			this.encoding = System.Text.Encoding.UTF8;
			this.decoder = encoding.GetDecoder();
			this.length = 4096;
			this.raw = new byte[length];
			this.rawOffset = 0;
			this.rawLength = 0;
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
			throw new NotImplementedException();
		}
		#endregion //Methods
	}
}
