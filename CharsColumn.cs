using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace Innovoft.IO
{
	public class CharsColumn : SplitColumn<char>, IEquatable<CharsColumn>
	{
		#region Class Methods
		public static bool TryParseObjectBool(CharsColumn parse, out object value)
		{
			return parse.TryParseObjectBool(out value);
		}

		public static bool TryParseObjectDateTime(CharsColumn parse, out object value)
		{
			return parse.TryParseObjectDateTime(out value);
		}

		public static bool TryParseObjectDecimal(CharsColumn parse, out object value)
		{
			return parse.TryParseObjectDecimal(out value);
		}

		public static bool TryParseObjectDouble(CharsColumn parse, out object value)
		{
			return parse.TryParseObjectDouble(out value);
		}

		public static bool TryParseObjectGUID(CharsColumn parse, out object value)
		{
			return parse.TryParseObjectGUID(out value);
		}

		public static bool TryParseObjectInt16(CharsColumn parse, out object value)
		{
			return parse.TryParseObjectInt16(out value);
		}

		public static bool TryParseObjectInt32(CharsColumn parse, out object value)
		{
			return parse.TryParseObjectInt32(out value);
		}

		public static bool TryParseObjectInt64(CharsColumn parse, out object value)
		{
			return parse.TryParseObjectInt64(out value);
		}

		public static bool TryParseObjectSingle(CharsColumn parse, out object value)
		{
			return parse.TryParseObjectSingle(out value);
		}

		public static bool TryParseObjectTimeSpan(CharsColumn parse, out object value)
		{
			return parse.TryParseObjectTimeSpan(out value);
		}

		public static bool TryParseObjectUInt16(CharsColumn parse, out object value)
		{
			return parse.TryParseObjectUInt16(out value);
		}

		public static bool TryParseObjectUInt32(CharsColumn parse, out object value)
		{
			return parse.TryParseObjectUInt32(out value);
		}		

		public static bool TryParseObjectUInt64(CharsColumn parse, out object value)
		{
			return parse.TryParseObjectUInt64(out value);
		}		
#endregion //Class Methods

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

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool TryParseObjectBool(out object value)
		{
			if (bool.TryParse(ToReadOnlySpan(), out var parsed))
			{
				value = parsed;
				return true;
			}
			else
			{
				value = null;
				return false;
			}
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

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool TryParseObjectDateTime(out object value)
		{
			if (DateTime.TryParse(ToReadOnlySpan(), out var parsed))
			{
				value = parsed;
				return true;
			}
			else
			{
				value = null;
				return false;
			}
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

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool TryParseObjectDecimal(out object value)
		{
			if (decimal.TryParse(ToReadOnlySpan(), out var parsed))
			{
				value = parsed;
				return true;
			}
			else
			{
				value = null;
				return false;
			}
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

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool TryParseObjectDouble(out object value)
		{
			if (double.TryParse(ToReadOnlySpan(), out var parsed))
			{
				value = parsed;
				return true;
			}
			else
			{
				value = null;
				return false;
			}
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

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool TryParseObjectGUID(out object value)
		{
			if (Guid.TryParse(ToReadOnlySpan(), out var parsed))
			{
				value = parsed;
				return true;
			}
			else
			{
				value = null;
				return false;
			}
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

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool TryParseObjectInt16(out object value)
		{
			if (short.TryParse(ToReadOnlySpan(), out var parsed))
			{
				value = parsed;
				return true;
			}
			else
			{
				value = null;
				return false;
			}
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

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool TryParseObjectInt32(out object value)
		{
			if (int.TryParse(ToReadOnlySpan(), out var parsed))
			{
				value = parsed;
				return true;
			}
			else
			{
				value = null;
				return false;
			}
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

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool TryParseObjectInt64(out object value)
		{
			if (long.TryParse(ToReadOnlySpan(), out var parsed))
			{
				value = parsed;
				return true;
			}
			else
			{
				value = null;
				return false;
			}
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

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool TryParseObjectSingle(out object value)
		{
			if (float.TryParse(ToReadOnlySpan(), out var parsed))
			{
				value = parsed;
				return true;
			}
			else
			{
				value = null;
				return false;
			}
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

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool TryParseObjectTimeSpan(out object value)
		{
			if (TimeSpan.TryParse(ToReadOnlySpan(), out var parsed))
			{
				value = parsed;
				return true;
			}
			else
			{
				value = null;
				return false;
			}
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

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool TryParseObjectUInt16(out object value)
		{
			if (ushort.TryParse(ToReadOnlySpan(), out var parsed))
			{
				value = parsed;
				return true;
			}
			else
			{
				value = null;
				return false;
			}
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

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool TryParseObjectUInt32(out object value)
		{
			if (uint.TryParse(ToReadOnlySpan(), out var parsed))
			{
				value = parsed;
				return true;
			}
			else
			{
				value = null;
				return false;
			}
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

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool TryParseObjectUInt64(out object value)
		{
			if (ulong.TryParse(ToReadOnlySpan(), out var parsed))
			{
				value = parsed;
				return true;
			}
			else
			{
				value = null;
				return false;
			}
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

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool TryParseObjectBool(out object value)
		{
			if (bool.TryParse(ToString(), out var parsed))
			{
				value = parsed;
				return true;
			}
			else
			{
				value = null;
				return false;
			}
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

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool TryParseObjectDateTime(out object value)
		{
			if (DateTime.TryParse(ToString(), out var parsed))
			{
				value = parsed;
				return true;
			}
			else
			{
				value = null;
				return false;
			}
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

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool TryParseObjectDecimal(out object value)
		{
			if (decimal.TryParse(ToString(), out var parsed))
			{
				value = parsed;
				return true;
			}
			else
			{
				value = null;
				return false;
			}
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

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool TryParseObjectDouble(out object value)
		{
			if (double.TryParse(ToString(), out var parsed))
			{
				value = parsed;
				return true;
			}
			else
			{
				value = null;
				return false;
			}
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

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool TryParseObjectGUID(out object value)
		{
			if (Guid.TryParse(ToString(), out var parsed))
			{
				value = parsed;
				return true;
			}
			else
			{
				value = null;
				return false;
			}
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

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool TryParseObjectInt16(out object value)
		{
			if (short.TryParse(ToString(), out var parsed))
			{
				value = parsed;
				return true;
			}
			else
			{
				value = null;
				return false;
			}
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

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool TryParseObjectInt32(out object value)
		{
			if (int.TryParse(ToString(), out var parsed))
			{
				value = parsed;
				return true;
			}
			else
			{
				value = null;
				return false;
			}
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

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool TryParseObjectInt64(out object value)
		{
			if (long.TryParse(ToString(), out var parsed))
			{
				value = parsed;
				return true;
			}
			else
			{
				value = null;
				return false;
			}
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

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool TryParseObjectSingle(out object value)
		{
			if (float.TryParse(ToString(), out var parsed))
			{
				value = parsed;
				return true;
			}
			else
			{
				value = null;
				return false;
			}
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

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool TryParseObjectTimeSpan(out object value)
		{
			if (TimeSpan.TryParse(ToString(), out var parsed))
			{
				value = parsed;
				return true;
			}
			else
			{
				value = null;
				return false;
			}
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

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool TryParseObjectUInt16(out object value)
		{
			if (ushort.TryParse(ToString(), out var parsed))
			{
				value = parsed;
				return true;
			}
			else
			{
				value = null;
				return false;
			}
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

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool TryParseObjectUInt32(out object value)
		{
			if (uint.TryParse(ToString(), out var parsed))
			{
				value = parsed;
				return true;
			}
			else
			{
				value = null;
				return false;
			}
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

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool TryParseObjectUInt64(out object value)
		{
			if (ulong.TryParse(ToString(), out var parsed))
			{
				value = parsed;
				return true;
			}
			else
			{
				value = null;
				return false;
			}
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
		public static bool operator ==(CharsColumn x, string y)
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

		/// <remarks>does not check for nulls</remarks>
		public static bool operator !=(CharsColumn x, string y)
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
		#endregion //Operators
	}
}
