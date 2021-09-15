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
			: base()
		{
			OpenOnly(stream, streamDispose: true);
		}
		#endregion //Constructors

		#region Methods
		private new void OpenOnly(Stream stream, bool streamDispose)
		{
			base.OpenOnly(stream, streamDispose);
		}
		#endregion //Methods
	}
}
