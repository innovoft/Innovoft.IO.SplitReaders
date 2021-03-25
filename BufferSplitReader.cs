using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Innovoft.IO
{
	public sealed partial class BufferSplitReader : SplitReader
	{
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
