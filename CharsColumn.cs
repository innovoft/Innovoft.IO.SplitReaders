using System;
using System.Collections.Generic;
using System.Text;

namespace Innovoft.IO
{
	public class CharsColumn : Column<char>, IEquatable<CharsColumn>
	{
		#region Constructors
		public CharsColumn()
			: base()
		{
		}

		public CharsColumn(string append)
			: base(DefaultCapacity)
		{
			Append(append);
		}

		public CharsColumn(int capacity)
			: base(capacity)
		{
		}

		public CharsColumn(int capacity, string append)
			: base(capacity)
		{
			Append(append);
		}
		#endregion //Constructors

#region Methods
#if NET5_0_OR_GREATER
		public override int GetHashCode()
		{
			return string.GetHashCode(ToReadOnlySpan());
		}
#else //NET5_0_OR_GREATER
		public override int GetHashCode()
		{
			var hash = 0;
			for (var offset = 0; ;)
			{
				//0
				if (offset >= count)
				{
					return hash;
				}
				hash ^= values[offset];
				++offset;
				//1
				if (offset >= count)
				{
					return hash;
				}
				hash ^= values[offset] << 16;
				++offset;
			}
		}
#endif //NET5_0_OR_GREATER

		public override bool Equals(object other)
		{
			return Equals(other as CharsColumn);
		}

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
				if (other.values[offset] != this.values[offset])
				{
					return false;
				}
			}
			return true;
		}

		public void Append(string append)
		{
			appended = true;
			var length = append.Length;
			var required = count + length;
			if (required > capacity)
			{
				Enlarge();
			}
			append.CopyTo(0, values, count, length);
			count += length;
		}

		public void AppendLength(string append, int offset, int length)
		{
			appended = true;
			var required = count + length;
			if (required > capacity)
			{
				Enlarge();
			}
			append.CopyTo(offset, values, count, length);
			count += length;
		}

		public void AppendEnding(string append, int offset, int ending)
		{
			appended = true;
			var length = ending - offset;
			var required = count + length;
			if (required > capacity)
			{
				Enlarge();
			}
			append.CopyTo(offset, values, count, length);
			count += length;
		}

		public void AppendLength(byte[] append, int offset, int length, Encoding encoding)
		{
			appended = true;
			var decodedLength = encoding.GetCharCount(append, offset, length);
			var required = count + decodedLength;
			if (required > capacity)
			{
				Enlarge();
			}
			encoding.GetChars(append, offset, length, values, count);
			count += decodedLength;
		}

		public void AppendEnding(byte[] append, int offset, int ending, Encoding encoding)
		{
			appended = true;
			var length = ending - offset;
			var decodedLength = encoding.GetCharCount(append, offset, length);
			var required = count + decodedLength;
			if (required > capacity)
			{
				Enlarge();
			}
			encoding.GetChars(append, offset, length, values, count);
			count += decodedLength;
		}

		public void AppendLength(byte[] append, int offset, int length, Decoder decoder)
		{
			appended = true;
			var decodedLength = decoder.GetCharCount(append, offset, length);
			var required = count + decodedLength;
			if (required > capacity)
			{
				Enlarge();
			}
			decoder.GetChars(append, offset, length, values, count);
			count += decodedLength;
		}

		public void AppendEnding(byte[] append, int offset, int ending, Decoder decoder)
		{
			appended = true;
			var length = ending - offset;
			var decodedLength = decoder.GetCharCount(append, offset, length);
			var required = count + decodedLength;
			if (required > capacity)
			{
				Enlarge();
			}
			decoder.GetChars(append, offset, length, values, count);
			count += decodedLength;
		}

#if NETSTANDARD2_1 || NET5_0_OR_GREATER
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
#else //NETSTANDARD2_1 || NET5_0_OR_GREATER
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
#endif //NETSTANDARD2_1 || NET5_0_OR_GREATER

		public sealed override string ToString()
		{
			if (appended)
			{
				return new string(values, 0, count);
			}
			else
			{
				return null;
			}
		}
#endregion //Methods
	}
}
