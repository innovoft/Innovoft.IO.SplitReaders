using System;
using System.Collections.Generic;
using System.Text;

namespace Innovoft.IO
{
	public class CharsColumn : IEquatable<CharsColumn>
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

		public CharsColumn(string append)
			: this(DefaultCapacity, append)
		{
		}

		public CharsColumn(int capacity)
		{
			this.capacity = capacity;
			this.letters = new char[capacity];
			Clear();
		}

		public CharsColumn(int capacity, string append)
		{
			this.capacity = capacity;
			this.letters = new char[capacity];
			Clear();
			Append(append);
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
				hash ^= letters[offset];
				++offset;
				//1
				if (offset >= count)
				{
					return hash;
				}
				hash ^= letters[offset] << 16;
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

		public void AppendLength(string append, int offset, int length)
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
			append.CopyTo(offset, letters, count, length);
			count += length;
		}

		public void AppendEnding(string append, int offset, int ending)
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
			append.CopyTo(offset, letters, count, length);
			count += length;
		}

		public void AppendLength(byte[] append, int offset, int length, Encoding encoding)
		{
			appended = true;
			var decodedLength = encoding.GetCharCount(append, offset, length);
			var required = count + decodedLength;
			if (required > capacity)
			{
				var enlargedCapacity = 2 * capacity;
				var enlarged = new char[enlargedCapacity];
				Array.Copy(letters, 0, enlarged, 0, count);
				capacity = enlargedCapacity;
				letters = enlarged;
			}
			encoding.GetChars(append, offset, length, letters, count);
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
				var enlargedCapacity = 2 * capacity;
				var enlarged = new char[enlargedCapacity];
				Array.Copy(letters, 0, enlarged, 0, count);
				capacity = enlargedCapacity;
				letters = enlarged;
			}
			encoding.GetChars(append, offset, length, letters, count);
			count += decodedLength;
		}

		public void AppendLength(byte[] append, int offset, int length, Decoder decoder)
		{
			appended = true;
			var decodedLength = decoder.GetCharCount(append, offset, length);
			var required = count + decodedLength;
			if (required > capacity)
			{
				var enlargedCapacity = 2 * capacity;
				var enlarged = new char[enlargedCapacity];
				Array.Copy(letters, 0, enlarged, 0, count);
				capacity = enlargedCapacity;
				letters = enlarged;
			}
			decoder.GetChars(append, offset, length, letters, count);
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
				var enlargedCapacity = 2 * capacity;
				var enlarged = new char[enlargedCapacity];
				Array.Copy(letters, 0, enlarged, 0, count);
				capacity = enlargedCapacity;
				letters = enlarged;
			}
			decoder.GetChars(append, offset, length, letters, count);
			count += decodedLength;
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

#if NETSTANDARD2_1 || NET5_0_OR_GREATER
		public Span<char> ToSpan()
		{
			return new Span<char>(letters, 0, count);
		}

		public static implicit operator Span<char>(CharsColumn value)
		{
			return value.ToSpan();
		}

		public ReadOnlySpan<char> ToReadOnlySpan()
		{
			return new ReadOnlySpan<char>(letters, 0, count);
		}

		public static implicit operator ReadOnlySpan<char>(CharsColumn value)
		{
			return value.ToReadOnlySpan();
		}

		public Memory<char> ToMemory()
		{
			return new Memory<char>(letters, 0, count);
		}

		public static implicit operator Memory<char>(CharsColumn value)
		{
			return value.ToMemory();
		}

		public ReadOnlyMemory<char> ToReadOnlyMemory()
		{
			return new ReadOnlyMemory<char>(letters, 0, count);
		}
#endif //NETSTANDARD2_1 || NET5_0_OR_GREATER

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
