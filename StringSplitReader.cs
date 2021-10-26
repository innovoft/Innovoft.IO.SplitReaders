using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Innovoft.IO
{
	public sealed partial class StringSplitReader : DecodeSplitReader
	{
		#region Fields
		private readonly StringBuilder builder = new StringBuilder();
		#endregion //Fields

		#region Constructors
		public StringSplitReader(Stream stream)
			: base()
		{
			OpenOnly(stream, dispose: true);
		}
		#endregion //Constructors

		#region Methods
		private new void OpenOnly(Stream stream, bool dispose)
		{
			base.OpenOnly(stream, dispose);

			builder.Clear();
		}
		#endregion //Methods
	}
}
