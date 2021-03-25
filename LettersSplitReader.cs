using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace Innovoft.IO
{
	public sealed partial class LettersSplitReader : DecodeSplitReader
	{
		#region Constructors
		public LettersSplitReader(Stream stream)
			: base(stream)
		{
		}
		#endregion //Constructors
	}
}
