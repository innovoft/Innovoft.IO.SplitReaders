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
		#endregion //Methods
	}
}
