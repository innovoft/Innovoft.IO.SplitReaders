﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace Innovoft.IO
{
	public sealed partial class CharsSplitReader : DecodeSplitReader
	{
		#region Constructors
		public CharsSplitReader(Stream stream)
			: base()
		{
			OpenOnly(stream);
		}

		public CharsSplitReader(Stream stream, bool dispose)
			: base()
		{
			OpenOnly(stream, dispose);
		}
		#endregion //Constructors

		#region Methods
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private new void OpenOnly(Stream stream)
		{
			base.OpenOnly(stream);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private new void OpenOnly(Stream stream, bool dispose)
		{
			base.OpenOnly(stream, dispose);
		}
		#endregion //Methods
	}
}
