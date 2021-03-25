using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Innovoft.IO
{
	public sealed partial class BufferSplitReader : SplitReader
	{
		#region Constants
		public const byte CR = 0x0D;
		public const byte LF = 0x0A;
		#endregion //Constants

		#region Fields
		private int bufferOffset;
		private int bufferLength;
		#endregion //Fields

		#region Constructors
		public BufferSplitReader(Stream stream)
			: base(stream)
		{
		}
		#endregion //Constructors

		#region Methods
		private bool ReadBuffer()
		{
			throw new NotImplementedException();
		}
		#endregion //Methods
	}
}
