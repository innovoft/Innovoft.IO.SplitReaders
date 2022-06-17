using System;
#if NETSTANDARD2_1 || NET5_0_OR_GREATER
using System.Buffers.Text;
#endif //NETSTANDARD2_1 || NET5_0_OR_GREATER
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace Innovoft.IO
{
	public class BytesColumn : SplitColumn<byte>, IEquatable<BytesColumn>
	{
		#region Class Methods
		public static bool TryParseObjectBool(BytesColumn parse, out object value)
		{
			return parse.TryParseObjectBool(out value);
		}

		public static bool TryParseObjectDateTime(BytesColumn parse, out object value)
		{
			return parse.TryParseObjectDateTime(out value);
		}

		public static bool TryParseObjectDecimal(BytesColumn parse, out object value)
		{
			return parse.TryParseObjectDecimal(out value);
		}

		public static bool TryParseObjectDouble(BytesColumn parse, out object value)
		{
			return parse.TryParseObjectDouble(out value);
		}

		public static bool TryParseObjectGUID(BytesColumn parse, out object value)
		{
			return parse.TryParseObjectGUID(out value);
		}

		public static bool TryParseObjectInt16(BytesColumn parse, out object value)
		{
			return parse.TryParseObjectInt16(out value);
		}

		public static bool TryParseObjectInt32(BytesColumn parse, out object value)
		{
			return parse.TryParseObjectInt32(out value);
		}

		public static bool TryParseObjectInt64(BytesColumn parse, out object value)
		{
			return parse.TryParseObjectInt64(out value);
		}

		public static bool TryParseObjectSingle(BytesColumn parse, out object value)
		{
			return parse.TryParseObjectSingle(out value);
		}

		public static bool TryParseObjectTimeSpan(BytesColumn parse, out object value)
		{
			return parse.TryParseObjectTimeSpan(out value);
		}

		public static bool TryParseObjectUInt16(BytesColumn parse, out object value)
		{
			return parse.TryParseObjectUInt16(out value);
		}

		public static bool TryParseObjectUInt32(BytesColumn parse, out object value)
		{
			return parse.TryParseObjectUInt32(out value);
		}

		public static bool TryParseObjectUInt64(BytesColumn parse, out object value)
		{
			return parse.TryParseObjectUInt64(out value);
		}

		public static bool TryParseObjectString(BytesColumn parse, out object value)
		{
			return parse.TryParseObjectString(out value);
		}

		public static char[] ToChars(byte[] values)
		{
			return ToChars(values, Encoding.UTF8);
		}

		public static char[] ToChars(byte[] values, Encoding encoding)
		{
			return encoding.GetChars(values, 0, values.Length);
		}

		public static int ToChars(byte[] values, ref char[] decoded)
		{
			return ToChars(values, Encoding.UTF8, ref decoded);
		}

		public static int ToChars(byte[] values, Encoding encoding, ref char[] decoded)
		{
			var count = values.Length;
			var length = encoding.GetCharCount(values, 0, count);
			if (length > decoded.Length)
			{
				Array.Resize(ref decoded, 2 * decoded.Length);
			}
			encoding.GetChars(values, 0, count, decoded, 0);
			return length;
		}

		public static char[] ToChars(byte[] values, Decoder decoder)
		{
			var count = values.Length;
			var length = decoder.GetCharCount(values, 0, count);
			var decoded = new char[length];
			decoder.GetChars(values, 0, count, decoded, 0);
			return decoded;
		}

		public static int ToChars(byte[] values, Decoder decoder, ref char[] decoded)
		{
			var count = values.Length;
			var length = decoder.GetCharCount(values, 0, count);
			if (length > decoded.Length)
			{
				Array.Resize(ref decoded, 2 * decoded.Length);
			}
			decoder.GetChars(values, 0, count, decoded, 0);
			return length;
		}

		public static void Write(byte[] values, TextWriter writer, ref char[] decoded)
		{
			if (values.Length > 0)
			{
				var length = ToChars(values, ref decoded);
				writer.Write(decoded, 0, length);
			}
		}

		public static void Write(byte[] values, TextWriter writer, Encoding encoding, ref char[] decoded)
		{
			if (values.Length > 0)
			{
				var length = ToChars(values, encoding, ref decoded);
				writer.Write(decoded, 0, length);
			}
		}

		public static void Write(byte[] values, TextWriter writer, Decoder decoder, ref char[] decoded)
		{
			if (values.Length > 0)
			{
				var length = ToChars(values, decoder, ref decoded);
				writer.Write(decoded, 0, length);
			}
		}

		public static void WriteLine(byte[] values, TextWriter writer, ref char[] decoded)
		{
			if (values.Length > 0)
			{
				var length = ToChars(values, ref decoded);
				writer.WriteLine(decoded, 0, length);
			}
			else
			{
				writer.WriteLine();
			}
		}

		public static void WriteLine(byte[] values, TextWriter writer, Encoding encoding, ref char[] decoded)
		{
			if (values.Length > 0)
			{
				var length = ToChars(values, encoding, ref decoded);
				writer.WriteLine(decoded, 0, length);
			}
			else
			{
				writer.WriteLine();
			}
		}

		public static void WriteLine(byte[] values, TextWriter writer, Decoder decoder, ref char[] decoded)
		{
			if (values.Length > 0)
			{
				var length = ToChars(values, decoder, ref decoded);
				writer.WriteLine(decoded, 0, length);
			}
			else
			{
				writer.WriteLine();
			}
		}
		#endregion //Class Methods

		#region Constructors
		public BytesColumn()
			: base()
		{
		}

		public BytesColumn(string append)
			: base()
		{
			Append(append);
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

		public void Append(BytesColumn append)
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

		public void Append(string append)
		{
			Append(append, Encoding.UTF8);
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

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool TryParse(out bool value)
		{
			return Utf8Parser.TryParse(ToReadOnlySpan(), out value, out var consumed);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool TryParse(int offset, out bool value)
		{
			return Utf8Parser.TryParse(ToReadOnlySpan(offset), out value, out var consumed);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool TryParseObjectBool(out object value)
		{
			if (Utf8Parser.TryParse(ToReadOnlySpan(), out bool parsed, out var consumed))
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

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool TryParseObjectBool(int offset, out object value)
		{
			if (Utf8Parser.TryParse(ToReadOnlySpan(offset), out bool parsed, out var consumed))
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

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool TryParse(out DateTime value)
		{
			return Utf8Parser.TryParse(ToReadOnlySpan(), out value, out var consumed);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool TryParse(int offset, out DateTime value)
		{
			return Utf8Parser.TryParse(ToReadOnlySpan(offset), out value, out var consumed);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool TryParseObjectDateTime(out object value)
		{
			if (Utf8Parser.TryParse(ToReadOnlySpan(), out DateTime parsed, out var consumed))
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

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool TryParseObjectDateTime(int offset, out object value)
		{
			if (Utf8Parser.TryParse(ToReadOnlySpan(offset), out DateTime parsed, out var consumed))
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

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool TryParse(out decimal value)
		{
			return Utf8Parser.TryParse(ToReadOnlySpan(), out value, out var consumed);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool TryParse(int offset, out decimal value)
		{
			return Utf8Parser.TryParse(ToReadOnlySpan(offset), out value, out var consumed);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool TryParseObjectDecimal(out object value)
		{
			if (Utf8Parser.TryParse(ToReadOnlySpan(), out decimal parsed, out var consumed))
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

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool TryParseObjectDecimal(int offset, out object value)
		{
			if (Utf8Parser.TryParse(ToReadOnlySpan(offset), out decimal parsed, out var consumed))
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

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool TryParse(out double value)
		{
			return Utf8Parser.TryParse(ToReadOnlySpan(), out value, out var consumed);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool TryParse(int offset, out double value)
		{
			return Utf8Parser.TryParse(ToReadOnlySpan(offset), out value, out var consumed);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool TryParseObjectDouble(out object value)
		{
			if (Utf8Parser.TryParse(ToReadOnlySpan(), out double parsed, out var consumed))
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

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool TryParseObjectDouble(int offset, out object value)
		{
			if (Utf8Parser.TryParse(ToReadOnlySpan(offset), out double parsed, out var consumed))
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
			if (!Utf8Parser.TryParse(ToReadOnlySpan(), out Guid value, out var consumed))
			{
				throw new FormatException();
			}
			return value;
		}

		public Guid ToGUID(Encoding encoding)
		{
			return Guid.Parse(ToChars(encoding));
		}

		public Guid ToGUID(Decoder decoder)
		{
			return Guid.Parse(ToChars(decoder));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool TryParse(out Guid value)
		{
			return Utf8Parser.TryParse(ToReadOnlySpan(), out value, out var consumed);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool TryParseObjectGUID(out object value)
		{
			if (Utf8Parser.TryParse(ToReadOnlySpan(), out Guid parsed, out var consumed))
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

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool TryParse(out short value)
		{
			return Utf8Parser.TryParse(ToReadOnlySpan(), out value, out var consumed);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool TryParseObjectInt16(out object value)
		{
			if (Utf8Parser.TryParse(ToReadOnlySpan(), out short parsed, out var consumed))
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

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool TryParse(out int value)
		{
			return Utf8Parser.TryParse(ToReadOnlySpan(), out value, out var consumed);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool TryParseObjectInt32(out object value)
		{
			if (Utf8Parser.TryParse(ToReadOnlySpan(), out int parsed, out var consumed))
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

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool TryParse(out long value)
		{
			return Utf8Parser.TryParse(ToReadOnlySpan(), out value, out var consumed);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool TryParseObjectInt64(out object value)
		{
			if (Utf8Parser.TryParse(ToReadOnlySpan(), out long parsed, out var consumed))
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

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool TryParse(out float value)
		{
			return Utf8Parser.TryParse(ToReadOnlySpan(), out value, out var consumed);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool TryParseObjectSingle(out object value)
		{
			if (Utf8Parser.TryParse(ToReadOnlySpan(), out float parsed, out var consumed))
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
			if (!Utf8Parser.TryParse(ToReadOnlySpan(), out TimeSpan value, out var consumed))
			{
				throw new FormatException();
			}
			return value;
		}

		public TimeSpan ToTimeSpan(Encoding encoding)
		{
			return TimeSpan.Parse(ToChars(encoding));
		}

		public TimeSpan ToTimeSpan(Decoder decoder)
		{
			return TimeSpan.Parse(ToChars(decoder));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool TryParse(out TimeSpan value)
		{
			return Utf8Parser.TryParse(ToReadOnlySpan(), out value, out var consumed);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool TryParseObjectTimeSpan(out object value)
		{
			if (Utf8Parser.TryParse(ToReadOnlySpan(), out TimeSpan parsed, out var consumed))
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

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool TryParse(out ushort value)
		{
			return Utf8Parser.TryParse(ToReadOnlySpan(), out value, out var consumed);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool TryParseObjectUInt16(out object value)
		{
			if (Utf8Parser.TryParse(ToReadOnlySpan(), out ushort parsed, out var consumed))
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

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool TryParse(out uint value)
		{
			return Utf8Parser.TryParse(ToReadOnlySpan(), out value, out var consumed);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool TryParseObjectUInt32(out object value)
		{
			if (Utf8Parser.TryParse(ToReadOnlySpan(), out uint parsed, out var consumed))
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

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool TryParse(out ulong value)
		{
			return Utf8Parser.TryParse(ToReadOnlySpan(), out value, out var consumed);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool TryParseObjectUInt64(out object value)
		{
			if (Utf8Parser.TryParse(ToReadOnlySpan(), out ulong parsed, out var consumed))
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

		public bool ToBoolean(Encoding encoding)
		{
			return bool.Parse(ToString(encoding));
		}

		public bool ToBoolean(Decoder decoder)
		{
			return bool.Parse(ToString(decoder));
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

		public DateTime ToDateTime(Encoding encoding)
		{
			return DateTime.Parse(ToString(encoding));
		}

		public DateTime ToDateTime(Decoder decoder)
		{
			return DateTime.Parse(ToString(decoder));
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

		public decimal ToDecimal(Encoding encoding)
		{
			return decimal.Parse(ToString(encoding));
		}

		public decimal ToDecimal(Decoder decoder)
		{
			return decimal.Parse(ToString(decoder));
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

		public double ToDouble(Encoding encoding)
		{
			return double.Parse(ToString(encoding));
		}

		public double ToDouble(Decoder decoder)
		{
			return double.Parse(ToString(decoder));
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

		public Guid ToGUID(Encoding encoding)
		{
			return Guid.Parse(ToString(encoding));
		}

		public Guid ToGUID(Decoder decoder)
		{
			return Guid.Parse(ToString(decoder));
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

		public short ToInt16(Encoding encoding)
		{
			return short.Parse(ToString(encoding));
		}

		public short ToInt16(Decoder decoder)
		{
			return short.Parse(ToString(decoder));
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

		public int ToInt32(Encoding encoding)
		{
			return int.Parse(ToString(encoding));
		}

		public int ToInt32(Decoder decoder)
		{
			return int.Parse(ToString(decoder));
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

		public long ToInt64(Encoding encoding)
		{
			return long.Parse(ToString(encoding));
		}

		public long ToInt64(Decoder decoder)
		{
			return long.Parse(ToString(decoder));
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

		public float ToSingle(Encoding encoding)
		{
			return float.Parse(ToString(encoding));
		}

		public float ToSingle(Decoder decoder)
		{
			return float.Parse(ToString(decoder));
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

		public TimeSpan ToTimeSpan(Encoding encoding)
		{
			return TimeSpan.Parse(ToString(encoding));
		}

		public TimeSpan ToTimeSpan(Decoder decoder)
		{
			return TimeSpan.Parse(ToString(decoder));
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

		public ushort ToUInt16(Encoding encoding)
		{
			return ushort.Parse(ToString(encoding));
		}

		public ushort ToUInt16(Decoder decoder)
		{
			return ushort.Parse(ToString(decoder));
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

		public uint ToUInt32(Encoding encoding)
		{
			return uint.Parse(ToString(encoding));
		}

		public uint ToUInt32(Decoder decoder)
		{
			return uint.Parse(ToString(decoder));
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

		public ulong ToUInt64(Encoding encoding)
		{
			return ulong.Parse(ToString(encoding));
		}

		public ulong ToUInt64(Decoder decoder)
		{
			return ulong.Parse(ToString(decoder));
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

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool TryParse(out string value)
		{
			value = ToString();
			return true;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool TryParseObjectString(out object value)
		{
			value = ToString();
			return true;
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

#if NETSTANDARD2_1 || NET5_0_OR_GREATER
		public ReadOnlySpan<char> ToCharsRead(ref char[] decoded)
		{
			return ToCharsRead(Encoding.UTF8, ref decoded);
		}

		public ReadOnlySpan<char> ToCharsRead(Encoding encoding, ref char[] decoded)
		{
			if (appended)
			{
				var length = encoding.GetCharCount(values, 0, count);
				if (length > decoded.Length)
				{
					Array.Resize(ref decoded, 2 * decoded.Length);
				}
				encoding.GetChars(values, 0, count, decoded, 0);
				return new ReadOnlySpan<char>(decoded, 0, length);
			}
			else
			{
				return new ReadOnlySpan<char>(null, 0, -1);
			}
		}
#endif //NETSTANDARD2_1 || NET5_0_OR_GREATER

		public void Write(TextWriter writer, ref char[] decoded)
		{
			if (count > 0)
			{
				var length = ToChars(ref decoded);
				writer.Write(decoded, 0, length);
			}
		}

		public void Write(TextWriter writer, Encoding encoding, ref char[] decoded)
		{
			if (count > 0)
			{
				var length = ToChars(encoding, ref decoded);
				writer.Write(decoded, 0, length);
			}
		}

		public void Write(TextWriter writer, Decoder decoder, ref char[] decoded)
		{
			if (count > 0)
			{
				var length = ToChars(decoder, ref decoded);
				writer.Write(decoded, 0, length);
			}
		}

		public void WriteLine(TextWriter writer, ref char[] decoded)
		{
			if (count > 0)
			{
				var length = ToChars(ref decoded);
				writer.WriteLine(decoded, 0, length);
			}
			else
			{
				writer.WriteLine();
			}
		}

		public void WriteLine(TextWriter writer, Encoding encoding, ref char[] decoded)
		{
			if (count > 0)
			{
				var length = ToChars(encoding, ref decoded);
				writer.WriteLine(decoded, 0, length);
			}
			else
			{
				writer.WriteLine();
			}
		}

		public void WriteLine(TextWriter writer, Decoder decoder, ref char[] decoded)
		{
			if (count > 0)
			{
				var length = ToChars(decoder, ref decoded);
				writer.WriteLine(decoded, 0, length);
			}
			else
			{
				writer.WriteLine();
			}
		}

		public void Write(Stream writer)
		{
			if (count > 0)
			{
				writer.Write(values, 0, count);
			}
		}

		public void WriteLine(Stream writer, byte[] newline)
		{
			if (count > 0)
			{
				writer.Write(values, 0, count);
			}
			writer.Write(newline, 0, newline.Length);
		}
		#endregion //Methods

		#region Operators
		/// <remarks>does not check for nulls</remarks>
		public static bool operator ==(BytesColumn x, BytesColumn y)
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
		public static bool operator ==(byte[] x, BytesColumn y)
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
		public static bool operator ==(BytesColumn x, byte[] y)
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
		public static bool operator !=(BytesColumn x, BytesColumn y)
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
		public static bool operator !=(byte[] x, BytesColumn y)
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
		public static bool operator !=(BytesColumn x, byte[] y)
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
