using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace Innovoft.IO
{
	public sealed class SplitReader : IDisposable
	{
		#region Fields
		private Stream stream;
		private byte[] raw;
		private int rawOffset;
		private int rawLength;
		private char[] chars;
		private int charsOffset;
		private int charsLength;
		#endregion //Fields

		#region Constructors
		public SplitReader(Stream stream)
		{
			this.stream = stream;
		}
		#endregion //Constructors

		#region Finalizer
		~SplitReader()
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
		}
		#endregion //Dispose

		private int ReadBuffers(char separator, List<string> columns)
		{
			if (stream == null)
			{
				throw new ObjectDisposedException(nameof(SplitReader));
			}
			throw new NotImplementedException();
		}
		#endregion //Methods
	}
}
