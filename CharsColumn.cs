﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Innovoft.IO
{
	public class CharsColumn
	{
		#region Fields
		private int capacity;
		private char[] letters;
		private int count;
		private bool appended;
		#endregion //Fields

		#region Constructors
		public CharsColumn()
			: this(128)
		{
		}

		public CharsColumn(int capacity)
		{
			this.capacity = capacity;
			this.letters = new char[capacity];
			Clear();
		}
		#endregion //Constructors

		#region Properties
		public int Capacity => capacity;
		public char[] Letters => letters;
		public int Count => count;
		public bool Appended => appended;
		#endregion //Properties

		#region Indexes
		public char this[int offset] { get => letters[offset]; set => letters[offset] = value; }
		#endregion //Indexes

		#region Methods
		public void Clear()
		{
			count = 0;
			appended = false;
		}

		public void AppendNothig()
		{
			appended = true;
		}

		public void AppendLength(char[] append, int offset, int length)
		{
			appended = true;
			var required = count + length;
			if (required > capacity)
			{
				var enlargedCapacity = 2 * capacity;
				var enlarged = new char[enlargedCapacity];
				Array.Copy(letters, 0, enlarged, 0, count);
				capacity = enlargedCapacity;
				letters = enlarged;
			}
			Array.Copy(append, offset, letters, count, length);
			count += length;
		}

		public void AppendEnding(char[] append, int offset, int ending)
		{
			appended = true;
			var length = ending - offset;
			var required = count + length;
			if (required > capacity)
			{
				var enlargedCapacity = 2 * capacity;
				var enlarged = new char[enlargedCapacity];
				Array.Copy(letters, 0, enlarged, 0, count);
				capacity = enlargedCapacity;
				letters = enlarged;
			}
			Array.Copy(append, offset, letters, count, length);
			count += length;
		}

#if NETSTANDARD2_1
		public Span<char> ToSpan()
		{
			return new Span<char>(letters, 0, count);
		}

		public ReadOnlySpan<char> ToReadOnlySpan()
		{
			return new ReadOnlySpan<char>(letters, 0, count);
		}
#endif //NETSTANDARD2_1

		public override string ToString()
		{
			if (appended)
			{
				return new string(letters, 0, count);
			}
			else
			{
				return null;
			}
		}
#endregion //Methods
	}
}
