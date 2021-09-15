using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace Innovoft.IO
{
	public abstract class SplitReader : IDisposable
	{
		#region Constants
		protected const int length = 4096;
		#endregion //Constants

		#region Fields
		protected Stream stream;
		protected Func<byte[], int, int, int> streamRead;
		protected readonly byte[] buffer;
		#endregion //Fields

		#region Constructors
		protected SplitReader(Stream stream)
		{
			OpenOnly(stream);
			this.buffer = new byte[length];
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
			streamRead = null;
		}
		#endregion //Dispose

		public virtual void OpenOnly(Stream stream)
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
		}
		#endregion //Methods
	}
}
