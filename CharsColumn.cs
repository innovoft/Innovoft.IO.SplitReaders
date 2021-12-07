using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace Innovoft.IO
{
	public class CharsColumn : SplitColumn<char>, IEquatable<CharsColumn>
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
			if (object.ReferenceEquals(other, null))
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

		public void Append(CharsColumn append)
		{
			appended = true;
			var appendCount = append.count;
			var required = count + appendCount;
			if (required > capacity)
			{
				Enlarge();
			}
			Array.Copy(append.values, 0, values, count, appendCount);
			count += appendCount;
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

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool TryParse(out bool value)
		{
			return bool.TryParse(ToReadOnlySpan(), out value);
		}

		public DateTime ToDateTime()
		{
			return DateTime.Parse(ToReadOnlySpan());
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool TryParse(out DateTime value)
		{
			return DateTime.TryParse(ToReadOnlySpan(), out value);
		}

		public decimal ToDecimal()
		{
			return decimal.Parse(ToReadOnlySpan());
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool TryParse(out decimal value)
		{
			return decimal.TryParse(ToReadOnlySpan(), out value);
		}

		public double ToDouble()
		{
			return double.Parse(ToReadOnlySpan());
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool TryParse(out double value)
		{
			return double.TryParse(ToReadOnlySpan(), out value);
		}

		public Guid ToGUID()
		{
			return Guid.Parse(ToReadOnlySpan());
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool TryParse(out Guid value)
		{
			return Guid.TryParse(ToReadOnlySpan(), out value);
		}

		public short ToInt16()
		{
			return short.Parse(ToReadOnlySpan());
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool TryParse(out short value)
		{
			return short.TryParse(ToReadOnlySpan(), out value);
		}

		public int ToInt32()
		{
			return int.Parse(ToReadOnlySpan());
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool TryParse(out int value)
		{
			return int.TryParse(ToReadOnlySpan(), out value);
		}

		public long ToInt64()
		{
			return long.Parse(ToReadOnlySpan());
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool TryParse(out long value)
		{
			return long.TryParse(ToReadOnlySpan(), out value);
		}

		public float ToSingle()
		{
			return float.Parse(ToReadOnlySpan());
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool TryParse(out float value)
		{
			return float.TryParse(ToReadOnlySpan(), out value);
		}

		public TimeSpan ToTimeSpan()
		{
			return TimeSpan.Parse(ToReadOnlySpan());
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool TryParse(out TimeSpan value)
		{
			return TimeSpan.TryParse(ToReadOnlySpan(), out value);
		}

		public ushort ToUInt16()
		{
			return ushort.Parse(ToReadOnlySpan());
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool TryParse(out ushort value)
		{
			return ushort.TryParse(ToReadOnlySpan(), out value);
		}

		public uint ToUInt32()
		{
			return uint.Parse(ToReadOnlySpan());
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool TryParse(out uint value)
		{
			return uint.TryParse(ToReadOnlySpan(), out value);
		}

		public ulong ToUInt64()
		{
			return ulong.Parse(ToReadOnlySpan());
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool TryParse(out ulong value)
		{
			return ulong.TryParse(ToReadOnlySpan(), out value);
		}
#else //NETSTANDARD2_1 || NET5_0_OR_GREATER
		public bool ToBoolean()
		{
			return bool.Parse(ToString());
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool TryParse(out bool value)
		{
			return bool.TryParse(ToString(), out value);
		}

		public DateTime ToDateTime()
		{
			return DateTime.Parse(ToString());
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool TryParse(out DateTime value)
		{
			return DateTime.TryParse(ToString(), out value);
		}

		public decimal ToDecimal()
		{
			return decimal.Parse(ToString());
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool TryParse(out decimal value)
		{
			return decimal.TryParse(ToString(), out value);
		}

		public double ToDouble()
		{
			return double.Parse(ToString());
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool TryParse(out double value)
		{
			return double.TryParse(ToString(), out value);
		}

		public Guid ToGUID()
		{
			return Guid.Parse(ToString());
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool TryParse(out Guid value)
		{
			return Guid.TryParse(ToString(), out value);
		}

		public short ToInt16()
		{
			return short.Parse(ToString());
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool TryParse(out short value)
		{
			return short.TryParse(ToString(), out value);
		}

		public int ToInt32()
		{
			return int.Parse(ToString());
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool TryParse(out int value)
		{
			return int.TryParse(ToString(), out value);
		}

		public long ToInt64()
		{
			return long.Parse(ToString());
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool TryParse(out long value)
		{
			return long.TryParse(ToString(), out value);
		}

		public float ToSingle()
		{
			return float.Parse(ToString());
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool TryParse(out float value)
		{
			return float.TryParse(ToString(), out value);
		}

		public TimeSpan ToTimeSpan()
		{
			return TimeSpan.Parse(ToString());
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool TryParse(out TimeSpan value)
		{
			return TimeSpan.TryParse(ToString(), out value);
		}

		public ushort ToUInt16()
		{
			return ushort.Parse(ToString());
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool TryParse(out ushort value)
		{
			return ushort.TryParse(ToString(), out value);
		}

		public uint ToUInt32()
		{
			return uint.Parse(ToString());
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool TryParse(out uint value)
		{
			return uint.TryParse(ToString(), out value);
		}

		public ulong ToUInt64()
		{
			return ulong.Parse(ToString());
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool TryParse(out ulong value)
		{
			return ulong.TryParse(ToString(), out value);
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

		public void Write(TextWriter writer)
		{
			if (count > 0)
			{
				writer.Write(values, 0, count);
			}
		}

		public void WriteLine(TextWriter writer)
		{
			if (count > 0)
			{
				writer.WriteLine(values, 0, count);
			}
			else
			{
				writer.WriteLine();
			}
		}
		#endregion //Methods

		#region Operators
		/// <remarks>does not check for nulls</remarks>
		public static bool operator ==(CharsColumn x, CharsColumn y)
		{
			if (x.count != y.count)
			{
				return false;
			}
			var xv = x.values;
			var yv = y.values;
			for (var i = y.count - 1; i >= 0; --i)
			{
				if (xv[i] != yv[i])
				{
					return false;
				}
			}
			return true;
		}

		/// <remarks>does not check for nulls</remarks>
		public static bool operator ==(char[] x, CharsColumn y)
		{
			if (x.Length != y.count)
			{
				return false;
			}
			var yv = y.values;
			for (var i = y.count - 1; i >= 0; --i)
			{
				if (x[i] != yv[i])
				{
					return false;
				}
			}
			return true;
		}

		/// <remarks>does not check for nulls</remarks>
		public static bool operator ==(CharsColumn x, char[] y)
		{
			if (x.count != y.Length)
			{
				return false;
			}
			var xv = x.values;
			for (var i = x.count - 1; i >= 0; --i)
			{
				if (xv[i] != y[i])
				{
					return false;
				}
			}
			return true;
		}

		/// <remarks>does not check for nulls</remarks>
		public static bool operator ==(string x, CharsColumn y)
		{
			if (x.Length != y.count)
			{
				return false;
			}
			var yv = y.values;
			for (var i = y.count - 1; i >= 0; --i)
			{
				if (x[i] != yv[i])
				{
					return false;
				}
			}
			return true;
		}

		/// <remarks>does not check for nulls</remarks>
		public static bool operator !=(CharsColumn x, CharsColumn y)
		{
			if (x.count != y.count)
			{
				return true;
			}
			var xv = x.values;
			var yv = y.values;
			for (var i = y.count - 1; i >= 0; --i)
			{
				if (xv[i] != yv[i])
				{
					return true;
				}
			}
			return false;
		}

		/// <remarks>does not check for nulls</remarks>
		public static bool operator !=(char[] x, CharsColumn y)
		{
			if (x.Length != y.count)
			{
				return true;
			}
			var yv = y.values;
			for (var i = y.count - 1; i >= 0; --i)
			{
				if (x[i] != yv[i])
				{
					return true;
				}
			}
			return false;
		}

		/// <remarks>does not check for nulls</remarks>
		public static bool operator !=(CharsColumn x, char[] y)
		{
			if (x.count != y.Length)
			{
				return true;
			}
			var xv = x.values;
			for (var i = x.count - 1; i >= 0; --i)
			{
				if (xv[i] != y[i])
				{
					return true;
				}
			}
			return false;
		}

		/// <remarks>does not check for nulls</remarks>
		public static bool operator !=(string x, CharsColumn y)
		{
			if (x.Length != y.count)
			{
				return true;
			}
			var yv = y.values;
			for (var i = y.count - 1; i >= 0; --i)
			{
				if (x[i] != yv[i])
				{
					return true;
				}
			}
			return false;
		}
		#endregion //Operators
	}
}
