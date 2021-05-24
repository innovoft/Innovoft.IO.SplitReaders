using System;
#if NETSTANDARD2_1 || NET5_0_OR_GREATER
using System.Buffers.Text;
#endif //NETSTANDARD2_1 || NET5_0_OR_GREATER
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Innovoft.IO
{
	public class BytesColumn : SplitColumn<byte>, IEquatable<BytesColumn>
	{
		#region Constructors
		public BytesColumn()
			: base()
		{
		}

		public BytesColumn(string append)
			: base()
		{
			Append(append, Encoding.UTF8);
		}

		public BytesColumn(string append, Encoding encoding)
			: base()
		{
			Append(append, encoding);
		}

		public BytesColumn(string append, Encoder encoder, bool flush)
			: base()
		{
			Append(append, encoder, flush);
		}

		public BytesColumn(int capacity)
			: base(capacity)
		{
		}
		#endregion //Constructors

		#region Methods
		public override int GetHashCode()
		{
			var hash = 0;
			for (var offset = 0; ; )
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
				hash ^= values[offset] << 8;
				++offset;
				//2
				if (offset >= count)
				{
					return hash;
				}
				hash ^= values[offset] << 16;
				++offset;
				//3
				if (offset >= count)
				{
					return hash;
				}
				hash ^= values[offset] << 24;
				++offset;
			}
		}

		public override bool Equals(object other)
		{
			return Equals(other as BytesColumn);
		}

		public bool Equals(BytesColumn other)
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

		public void Append(string append, Encoding encoding)
		{
			appended = true;
			var length = encoding.GetByteCount(append);
			var required = count + length;
			if (required > capacity)
			{
				Enlarge();
			}
			encoding.GetBytes(append, 0, append.Length, values, count);
			count += length;
		}

		public void Append(string append, Encoder encoder, bool flush)
		{
#if NETSTANDARD2_1 || NET5_0_OR_GREATER
			Append(append.AsSpan(), encoder, flush);
#else //NETSTANDARD2_1 || NET5_0_OR_GREATER
			AppendLength(append.ToCharArray(), 0, append.Length, encoder, flush);
#endif //NETSTANDARD2_1 || NET5_0_OR_GREATER
		}

#if NETSTANDARD2_1 || NET5_0_OR_GREATER
		public void Append(ReadOnlySpan<char> append, Encoding encoding)
		{
			appended = true;
			var length = encoding.GetByteCount(append);
			var required = count + length;
			if (required > capacity)
			{
				Enlarge();
			}
			encoding.GetBytes(append, new Span<byte>(values, count, length));
			count += length;
		}

		public void Append(ReadOnlySpan<char> append, Encoder encoder, bool flush)
		{
			appended = true;
			var length = encoder.GetByteCount(append, flush);
			var required = count + length;
			if (required > capacity)
			{
				Enlarge();
			}
			encoder.GetBytes(append, new Span<byte>(values, count, length), flush);
			count += length;
		}
#endif //NETSTANDARD2_1 || NET5_0_OR_GREATER

		public void AppendLength(string append, int offset, int length, Encoding encoding)
		{
#if NETSTANDARD2_1 || NET5_0_OR_GREATER
			appended = true;
			var encodedLength = encoding.GetByteCount(append, offset, length);
			var required = count + encodedLength;
			if (required > capacity)
			{
				Enlarge();
			}
			encoding.GetBytes(append, offset, length, values, count);
			count += length;
#else //NETSTANDARD2_1 || NET5_0_OR_GREATER
			AppendLength(append.ToCharArray(), offset, length, encoding);
#endif //NETSTANDARD2_1 || NET5_0_OR_GREATER
		}

		public void AppendEnding(string append, int offset, int ending, Encoding encoding)
		{
#if NETSTANDARD2_1 || NET5_0_OR_GREATER
			appended = true;
			var length = ending - offset;
			var encodedLength = encoding.GetByteCount(append, offset, length);
			var required = count + encodedLength;
			if (required > capacity)
			{
				Enlarge();
			}
			encoding.GetBytes(append, offset, length, values, count);
			count += length;
#else //NETSTANDARD2_1 || NET5_0_OR_GREATER
			AppendEnding(append.ToCharArray(), offset, ending, encoding);
#endif //NETSTANDARD2_1 || NET5_0_OR_GREATER
		}

		public void AppendLength(string append, int offset, int length, Encoder encoder, bool flush)
		{
#if NETSTANDARD2_1 || NET5_0_OR_GREATER
			Append(append.AsSpan(offset, length), encoder, flush);
#else //NETSTANDARD2_1 || NET5_0_OR_GREATER
			AppendLength(append.ToCharArray(), offset, length, encoder, flush);
#endif //NETSTANDARD2_1 || NET5_0_OR_GREATER
		}

		public void AppendEnding(string append, int offset, int ending, Encoder encoder, bool flush)
		{
#if NETSTANDARD2_1 || NET5_0_OR_GREATER
			var length = ending - offset;
			Append(append.AsSpan(offset, length), encoder, flush);
#else //NETSTANDARD2_1 || NET5_0_OR_GREATER
			AppendEnding(append.ToCharArray(), offset, ending, encoder, flush);
#endif //NETSTANDARD2_1 || NET5_0_OR_GREATER
		}

		public void AppendLength(char[] append, int offset, int length, Encoding encoding)
		{
			appended = true;
			var encodedLength = encoding.GetByteCount(append, offset, length);
			var required = count + encodedLength;
			if (required > capacity)
			{
				Enlarge();
			}
			encoding.GetBytes(append, 0, append.Length, values, count);
			count += encodedLength;
		}

		public void AppendEnding(char[] append, int offset, int ending, Encoding encoding)
		{
			appended = true;
			var length = ending - offset;
			var encodedLength = encoding.GetByteCount(append, offset, length);
			var required = count + encodedLength;
			if (required > capacity)
			{
				Enlarge();
			}
			encoding.GetBytes(append, 0, append.Length, values, count);
			count += encodedLength;
		}

		public void AppendLength(char[] append, int offset, int length, Encoder encoder, bool flush)
		{
			appended = true;
			var encodedLength = encoder.GetByteCount(append, offset, length, flush);
			var required = count + encodedLength;
			if (required > capacity)
			{
				Enlarge();
			}
			encoder.GetBytes(append, 0, append.Length, values, count, flush);
			count += encodedLength;
		}

		public void AppendEnding(char[] append, int offset, int ending, Encoder encoder, bool flush)
		{
			appended = true;
			var length = ending - offset;
			var encodedLength = encoder.GetByteCount(append, offset, length, flush);
			var required = count + encodedLength;
			if (required > capacity)
			{
				Enlarge();
			}
			encoder.GetBytes(append, 0, append.Length, values, count, flush);
			count += encodedLength;
		}

#if NETSTANDARD2_1 || NET5_0_OR_GREATER
		public bool ToBoolean()
		{
			if (!Utf8Parser.TryParse(ToReadOnlySpan(), out bool value, out var consumed))
			{
				throw new FormatException();
			}
			return value;
		}

		public bool ToBoolean(Encoding encoding)
		{
			return bool.Parse(ToChars(encoding));
		}

		public bool ToBoolean(Decoder decoder)
		{
			return bool.Parse(ToChars(decoder));
		}

		public DateTime ToDateTime()
		{
			if (!Utf8Parser.TryParse(ToReadOnlySpan(), out DateTime value, out var consumed))
			{
				throw new FormatException();
			}
			return value;
		}

		public DateTime ToDateTime(Encoding encoding)
		{
			return DateTime.Parse(ToChars(encoding));
		}

		public DateTime ToDateTime(Decoder decoder)
		{
			return DateTime.Parse(ToChars(decoder));
		}

		public decimal ToDecimal()
		{
			if (!Utf8Parser.TryParse(ToReadOnlySpan(), out decimal value, out var consumed))
			{
				throw new FormatException();
			}
			return value;
		}

		public decimal ToDecimal(Encoding encoding)
		{
			return decimal.Parse(ToChars(encoding));
		}

		public decimal ToDecimal(Decoder decoder)
		{
			return decimal.Parse(ToChars(decoder));
		}

		public double ToDouble()
		{
			if (!Utf8Parser.TryParse(ToReadOnlySpan(), out double value, out var consumed))
			{
				throw new FormatException();
			}
			return value;
		}

		public double ToDouble(Encoding encoding)
		{
			return double.Parse(ToChars(encoding));
		}

		public double ToDouble(Decoder decoder)
		{
			return double.Parse(ToChars(decoder));
		}

		public short ToInt16()
		{
			if (!Utf8Parser.TryParse(ToReadOnlySpan(), out short value, out var consumed))
			{
				throw new FormatException();
			}
			return value;
		}

		public short ToInt16(Encoding encoding)
		{
			return short.Parse(ToChars(encoding));
		}

		public short ToInt16(Decoder decoder)
		{
			return short.Parse(ToChars(decoder));
		}

		public int ToInt32()
		{
			if (!Utf8Parser.TryParse(ToReadOnlySpan(), out int value, out var consumed))
			{
				throw new FormatException();
			}
			return value;
		}

		public int ToInt32(Encoding encoding)
		{
			return int.Parse(ToChars(encoding));
		}

		public int ToInt32(Decoder decoder)
		{
			return int.Parse(ToChars(decoder));
		}

		public long ToInt64()
		{
			if (!Utf8Parser.TryParse(ToReadOnlySpan(), out long value, out var consumed))
			{
				throw new FormatException();
			}
			return value;
		}

		public long ToInt64(Encoding encoding)
		{
			return long.Parse(ToChars(encoding));
		}

		public long ToInt64(Decoder decoder)
		{
			return long.Parse(ToChars(decoder));
		}

		public float ToSingle()
		{
			if (!Utf8Parser.TryParse(ToReadOnlySpan(), out float value, out var consumed))
			{
				throw new FormatException();
			}
			return value;
		}

		public float ToSingle(Encoding encoding)
		{
			return float.Parse(ToChars(encoding));
		}

		public float ToSingle(Decoder decoder)
		{
			return float.Parse(ToChars(decoder));
		}

		public ushort ToUInt16()
		{
			if (!Utf8Parser.TryParse(ToReadOnlySpan(), out ushort value, out var consumed))
			{
				throw new FormatException();
			}
			return value;
		}

		public ushort ToUInt16(Encoding encoding)
		{
			return ushort.Parse(ToChars(encoding));
		}

		public ushort ToUInt16(Decoder decoder)
		{
			return ushort.Parse(ToChars(decoder));
		}

		public uint ToUInt32()
		{
			if (!Utf8Parser.TryParse(ToReadOnlySpan(), out uint value, out var consumed))
			{
				throw new FormatException();
			}
			return value;
		}

		public uint ToUInt32(Encoding encoding)
		{
			return uint.Parse(ToChars(encoding));
		}

		public uint ToUInt32(Decoder decoder)
		{
			return uint.Parse(ToChars(decoder));
		}

		public ulong ToUInt64()
		{
			if (!Utf8Parser.TryParse(ToReadOnlySpan(), out ulong value, out var consumed))
			{
				throw new FormatException();
			}
			return value;
		}

		public ulong ToUInt64(Encoding encoding)
		{
			return ulong.Parse(ToChars(encoding));
		}

		public ulong ToUInt64(Decoder decoder)
		{
			return ulong.Parse(ToChars(decoder));
		}
#else //NETSTANDARD2_1 || NET5_0_OR_GREATER
		public bool ToBoolean(Encoding encoding)
		{
			return bool.Parse(ToString(encoding));
		}

		public bool ToBoolean(Decoder decoder)
		{
			return bool.Parse(ToString(decoder));
		}

		public DateTime ToDateTime(Encoding encoding)
		{
			return DateTime.Parse(ToString(encoding));
		}

		public DateTime ToDateTime(Decoder decoder)
		{
			return DateTime.Parse(ToString(decoder));
		}

		public decimal ToDecimal(Encoding encoding)
		{
			return decimal.Parse(ToString(encoding));
		}

		public decimal ToDecimal(Decoder decoder)
		{
			return decimal.Parse(ToString(decoder));
		}

		public double ToDouble(Encoding encoding)
		{
			return double.Parse(ToString(encoding));
		}

		public double ToDouble(Decoder decoder)
		{
			return double.Parse(ToString(decoder));
		}

		public short ToInt16(Encoding encoding)
		{
			return short.Parse(ToString(encoding));
		}

		public short ToInt16(Decoder decoder)
		{
			return short.Parse(ToString(decoder));
		}

		public int ToInt32(Encoding encoding)
		{
			return int.Parse(ToString(encoding));
		}

		public int ToInt32(Decoder decoder)
		{
			return int.Parse(ToString(decoder));
		}

		public long ToInt64(Encoding encoding)
		{
			return long.Parse(ToString(encoding));
		}

		public long ToInt64(Decoder decoder)
		{
			return long.Parse(ToString(decoder));
		}

		public float ToSingle(Encoding encoding)
		{
			return float.Parse(ToString(encoding));
		}

		public float ToSingle(Decoder decoder)
		{
			return float.Parse(ToString(decoder));
		}

		public ushort ToUInt16(Encoding encoding)
		{
			return ushort.Parse(ToString(encoding));
		}

		public ushort ToUInt16(Decoder decoder)
		{
			return ushort.Parse(ToString(decoder));
		}

		public uint ToUInt32(Encoding encoding)
		{
			return uint.Parse(ToString(encoding));
		}

		public uint ToUInt32(Decoder decoder)
		{
			return uint.Parse(ToString(decoder));
		}

		public ulong ToUInt64(Encoding encoding)
		{
			return ulong.Parse(ToString(encoding));
		}

		public ulong ToUInt64(Decoder decoder)
		{
			return ulong.Parse(ToString(decoder));
		}
#endif //NETSTANDARD2_1 || NET5_0_OR_GREATER

		public sealed override string ToString()
		{
			return ToString(Encoding.UTF8);
		}

		public string ToString(Encoding encoding)
		{
			if (appended)
			{
				return encoding.GetString(values, 0, count);
			}
			else
			{
				return null;
			}
		}

		public string ToString(Decoder decoder)
		{
			if (appended)
			{
				var length = decoder.GetCharCount(values, 0, count);
				var decoded = new char[length];
				decoder.GetChars(values, 0, count, decoded, 0);
				return new string(decoded);
			}
			else
			{
				return null;
			}
		}

		public char[] ToChars()
		{
			return ToChars(Encoding.UTF8);
		}

		public char[] ToChars(Encoding encoding)
		{
			if (appended)
			{
				return encoding.GetChars(values, 0, count);
			}
			else
			{
				return null;
			}
		}

		public int ToChars(ref char[] decoded)
		{
			return ToChars(Encoding.UTF8, ref decoded);
		}

		public int ToChars(Encoding encoding, ref char[] decoded)
		{
			if (appended)
			{
				var length = encoding.GetCharCount(values, 0, count);
				if (length > decoded.Length)
				{
					Array.Resize(ref decoded, 2 * decoded.Length);
				}
				encoding.GetChars(values, 0, count, decoded, 0);
				return length;
			}
			else
			{
				return -1;
			}
		}

		public char[] ToChars(Decoder decoder)
		{
			if (appended)
			{
				var length = decoder.GetCharCount(values, 0, count);
				var decoded = new char[length];
				decoder.GetChars(values, 0, count, decoded, 0);
				return decoded;
			}
			else
			{
				return null;
			}
		}

		public int ToChars(Decoder decoder, ref char[] decoded)
		{
			if (appended)
			{
				var length = decoder.GetCharCount(values, 0, count);
				if (length > decoded.Length)
				{
					Array.Resize(ref decoded, 2 * decoded.Length);
				}
				decoder.GetChars(values, 0, count, decoded, 0);
				return length;
			}
			else
			{
				return -1;
			}
		}

		public void Write(TextWriter writer, Encoding encoding, ref char[] decoded)
		{
			var length = ToChars(encoding, ref decoded);
			if (length > 0)
			{
				writer.Write(decoded, 0, length);
			}
		}
		#endregion //Methods
	}
}
