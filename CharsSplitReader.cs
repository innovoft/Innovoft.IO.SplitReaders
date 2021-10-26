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
			OpenOnly(stream, dispose: true);
		}
		#endregion //Constructors

		#region Methods
		private new void OpenOnly(Stream stream, bool dispose)
		{
			base.OpenOnly(stream, dispose);
		}
		#endregion //Methods
	}
}
