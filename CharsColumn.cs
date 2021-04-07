using System;
using System.Collections.Generic;
using System.Text;

namespace Innovoft.IO
{
	public class CharsColumn
	{
		#region Constants
		public const int DefaultCapacity = 128;
		#endregion //Constants

		#region Fields
		private int capacity;
		private char[] letters;
		private int count;
		private bool appended;
		#endregion //Fields

		#region Constructors
		public CharsColumn()
			: this(DefaultCapacity)
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
		public bool Equals(CharsColumn other)
		{
			if (other == null)
			{
				return false;
			}
			if (other.count != this.count)
			{
				return false;
			}
			for (var offset = count - 1; offset >= 0; --offset)
			{
				if (other.letters[offset] != this.letters[offset])
				{
					return false;
				}
			}
			return true;
		}

		public void Clear()
		{
			count = 0;
			appended = false;
		}

		public void Append()
		{
			appended = true;
		}

		public void Append(string append)
		{
			appended = true;
			var length = append.Length;
			var required = count + length;
			if (required > capacity)
			{
				var enlargedCapacity = 2 * capacity;
				var enlarged = new char[enlargedCapacity];
				Array.Copy(letters, 0, enlarged, 0, count);
				capacity = enlargedCapacity;
				letters = enlarged;
			}
			append.CopyTo(0, letters, count, length);
			count += length;
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

#if NETSTANDARD2_1
		public bool ToBoolean()
		{
			return bool.Parse(ToReadOnlySpan());
		}

		public DateTime ToDateTime()
		{
			return DateTime.Parse(ToReadOnlySpan());
		}

		public decimal ToDecimal()
		{
			return decimal.Parse(ToReadOnlySpan());
		}

		public double ToDouble()
		{
			return double.Parse(ToReadOnlySpan());
		}

		public short ToInt16()
		{
			return short.Parse(ToReadOnlySpan());
		}

		public int ToInt32()
		{
			return int.Parse(ToReadOnlySpan());
		}

		public long ToInt64()
		{
			return long.Parse(ToReadOnlySpan());
		}

		public float ToSingle()
		{
			return float.Parse(ToReadOnlySpan());
		}

		public ushort ToUInt16()
		{
			return ushort.Parse(ToReadOnlySpan());
		}

		public uint ToUInt32()
		{
			return uint.Parse(ToReadOnlySpan());
		}

		public ulong ToUInt64()
		{
			return ulong.Parse(ToReadOnlySpan());
		}
#else //NETSTANDARD2_1
		public bool ToBoolean()
		{
			return bool.Parse(ToString());
		}

		public DateTime ToDateTime()
		{
			return DateTime.Parse(ToString());
		}

		public decimal ToDecimal()
		{
			return decimal.Parse(ToString());
		}

		public double ToDouble()
		{
			return double.Parse(ToString());
		}

		public short ToInt16()
		{
			return short.Parse(ToString());
		}

		public int ToInt32()
		{
			return int.Parse(ToString());
		}

		public long ToInt64()
		{
			return long.Parse(ToString());
		}

		public float ToSingle()
		{
			return float.Parse(ToString());
		}

		public ushort ToUInt16()
		{
			return ushort.Parse(ToString());
		}

		public uint ToUInt32()
		{
			return uint.Parse(ToString());
		}

		public ulong ToUInt64()
		{
			return ulong.Parse(ToString());
		}
#endif //NETSTANDARD2_1

		public sealed override string ToString()
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
