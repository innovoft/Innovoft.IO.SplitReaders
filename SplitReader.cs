using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
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
		protected Action dispose;
		protected Func<byte[], int, int, int> streamRead;
		protected readonly byte[] buffer;
		#endregion //Fields

		#region Constructors
		protected SplitReader()
		{
			this.buffer = new byte[length];
		}
		#endregion //Constructors

		#region Finalizer
		~SplitReader()
		{
			Dispose(disposing: false);
		}
		#endregion //Finalizer

		#region Properties
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
			if (!disposing)
			{
				return;
			}

			dispose?.Invoke();
			dispose = null;
			stream = null;
			streamRead = null;
		}
		#endregion //Dispose

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected void OpenOnly(Stream stream)
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
			this.dispose = stream.Dispose;
			this.streamRead = stream.Read;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected void OpenOnly(Stream stream, bool dispose)
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
			this.dispose = dispose ? stream.Dispose : default(Action);
			this.streamRead = stream.Read;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected void OpenOnly(Stream stream, Action dispose)
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
			this.dispose = dispose;
			this.streamRead = stream.Read;
		}
		#endregion //Methods
	}
}
