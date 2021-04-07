using System;
using System.Collections.Generic;
using System.Text;

namespace Innovoft.IO
{
	public class BufferColumn : IEquatable<BufferColumn>
	{
		#region Constants
		public const int DefaultCapacity = 128;
		#endregion //Constants

		#region Fields
		private int capacity;
		private byte[] buffer;
		private int count;
		private bool appended;
		#endregion //Fields

		#region Constructors
		public BufferColumn()
			: this(DefaultCapacity)
		{
		}

		public BufferColumn(int length)
		{
			this.capacity = length;
			this.buffer = new byte[length];
			this.count = 0;
		}
		#endregion //Constructors

		#region Properties
		public int Capacity => capacity;
		public byte[] Buffer => buffer;
		public int Count => count;
		public bool Appended => appended;
		#endregion //Properties

		#region Indexes
		public byte this[int offset] { get => buffer[offset]; set => buffer[offset] = value; }
		#endregion //Indexes

		#region Methods
		public override int GetHashCode()
		{
			var hash = 0;
			for (var offset = count - 1; offset >= 0; --offset)
			{
				hash ^= buffer[offset] << (offset & 0x3);
			}
			return hash;
		}

		public override bool Equals(object other)
		{
			return Equals(other as BufferColumn);
		}

		public bool Equals(BufferColumn other)
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
				if (other.buffer[offset] != this.buffer[offset])
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

		public void Append(string append, Encoding encoding)
		{
			appended = true;
			var length = encoding.GetByteCount(append);
			var required = count + length;
			if (required > capacity)
			{
				var enlargedCapacity = 2 * capacity;
				var enlarged = new byte[enlargedCapacity];
				System.Buffer.BlockCopy(buffer, 0, enlarged, 0, count);
				capacity = enlargedCapacity;
				buffer = enlarged;
			}
			encoding.GetBytes(append, 0, append.Length, buffer, count);
			count += length;
		}

		public void AppendLength(char[] append, int offset, int length, Encoding encoding)
		{
			appended = true;
			var encodedLength = encoding.GetByteCount(append, offset, length);
			var required = count + encodedLength;
			if (required > capacity)
			{
				var enlargedCapacity = 2 * capacity;
				var enlarged = new byte[enlargedCapacity];
				System.Buffer.BlockCopy(buffer, 0, enlarged, 0, count);
				capacity = enlargedCapacity;
				buffer = enlarged;
			}
			encoding.GetBytes(append, 0, append.Length, buffer, count);
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
				var enlargedCapacity = 2 * capacity;
				var enlarged = new byte[enlargedCapacity];
				System.Buffer.BlockCopy(buffer, 0, enlarged, 0, count);
				capacity = enlargedCapacity;
				buffer = enlarged;
			}
			encoding.GetBytes(append, 0, append.Length, buffer, count);
			count += encodedLength;
		}

		public void AppendLength(char[] append, int offset, int length, Encoder encoder, bool flush)
		{
			appended = true;
			var encodedLength = encoder.GetByteCount(append, offset, length, flush);
			var required = count + encodedLength;
			if (required > capacity)
			{
				var enlargedCapacity = 2 * capacity;
				var enlarged = new byte[enlargedCapacity];
				System.Buffer.BlockCopy(buffer, 0, enlarged, 0, count);
				capacity = enlargedCapacity;
				buffer = enlarged;
			}
			encoder.GetBytes(append, 0, append.Length, buffer, count, flush);
			count += encodedLength;
		}

		public void AppendLength(byte[] append, int offset, int length)
		{
			appended = true;
			var required = count + length;
			if (required > capacity)
			{
				var enlargedCapacity = 2 * capacity;
				var enlarged = new byte[enlargedCapacity];
				System.Buffer.BlockCopy(buffer, 0, enlarged, 0, count);
				capacity = enlargedCapacity;
				buffer = enlarged;
			}
			System.Buffer.BlockCopy(append, offset, buffer, count, length);
			count += length;
		}

		public void AppendEnding(byte[] append, int offset, int ending)
		{
			appended = true;
			var length = ending - offset;
			var required = count + length;
			if (required > capacity)
			{
				var enlargedCapacity = 2 * capacity;
				var enlarged = new byte[enlargedCapacity];
				System.Buffer.BlockCopy(buffer, 0, enlarged, 0, count);
				capacity = enlargedCapacity;
				buffer = enlarged;
			}
			System.Buffer.BlockCopy(append, offset, buffer, count, length);
			count += length;
		}

#if NETSTANDARD2_1 || NET5_0_OR_GREATER
		public Span<byte> ToSpan()
		{
			return new Span<byte>(buffer, 0, count);
		}

		public ReadOnlySpan<byte> ToReadOnlySpan()
		{
			return new ReadOnlySpan<byte>(buffer, 0, count);
		}
#endif //NETSTANDARD2_1 || NET5_0_OR_GREATER

#if NETSTANDARD2_1 || NET5_0_OR_GREATER
		public bool ToBoolean(Encoding encoding)
		{
			return bool.Parse(ToChars(encoding));
		}

		public bool ToBoolean(Decoder decoder)
		{
			return bool.Parse(ToChars(decoder));
		}

		public DateTime ToDateTime(Encoding encoding)
		{
			return DateTime.Parse(ToChars(encoding));
		}

		public DateTime ToDateTime(Decoder decoder)
		{
			return DateTime.Parse(ToChars(decoder));
		}

		public decimal ToDecimal(Encoding encoding)
		{
			return decimal.Parse(ToChars(encoding));
		}

		public decimal ToDecimal(Decoder decoder)
		{
			return decimal.Parse(ToChars(decoder));
		}

		public double ToDouble(Encoding encoding)
		{
			return double.Parse(ToChars(encoding));
		}

		public double ToDouble(Decoder decoder)
		{
			return double.Parse(ToChars(decoder));
		}

		public short ToInt16(Encoding encoding)
		{
			return short.Parse(ToChars(encoding));
		}

		public short ToInt16(Decoder decoder)
		{
			return short.Parse(ToChars(decoder));
		}

		public int ToInt32(Encoding encoding)
		{
			return int.Parse(ToChars(encoding));
		}

		public int ToInt32(Decoder decoder)
		{
			return int.Parse(ToChars(decoder));
		}

		public long ToInt64(Encoding encoding)
		{
			return long.Parse(ToChars(encoding));
		}

		public long ToInt64(Decoder decoder)
		{
			return long.Parse(ToChars(decoder));
		}

		public float ToSingle(Encoding encoding)
		{
			return float.Parse(ToChars(encoding));
		}

		public float ToSingle(Decoder decoder)
		{
			return float.Parse(ToChars(decoder));
		}

		public ushort ToUInt16(Encoding encoding)
		{
			return ushort.Parse(ToChars(encoding));
		}

		public ushort ToUInt16(Decoder decoder)
		{
			return ushort.Parse(ToChars(decoder));
		}

		public uint ToUInt32(Encoding encoding)
		{
			return uint.Parse(ToChars(encoding));
		}

		public uint ToUInt32(Decoder decoder)
		{
			return uint.Parse(ToChars(decoder));
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

		public string ToString(Encoding encoding)
		{
			if (appended)
			{
				return encoding.GetString(buffer, 0, count);
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
				var length = decoder.GetCharCount(buffer, 0, count);
				var decoded = new char[length];
				decoder.GetChars(buffer, 0, count, decoded, 0);
				return new string(decoded);
			}
			else
			{
				return null;
			}
		}

		public char[] ToChars(Encoding encoding)
		{
			if (appended)
			{
				return encoding.GetChars(buffer, 0, count);
			}
			else
			{
				return null;
			}
		}

		public char[] ToChars(Decoder decoder)
		{
			if (appended)
			{
				var length = decoder.GetCharCount(buffer, 0, count);
				var decoded = new char[length];
				decoder.GetChars(buffer, 0, count, decoded, 0);
				return decoded;
			}
			else
			{
				return null;
			}
		}
		#endregion //Methods
	}
}
