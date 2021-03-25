using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Innovoft.IO
{
	public sealed partial class CharsSplitReader : DecodeSplitReader
	{
		#region Constructors
		public CharsSplitReader(Stream stream)
			: base(stream)
		{
		}
		#endregion //Constructors
	}
}
