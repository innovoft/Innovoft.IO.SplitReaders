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

#if NETSTANDARD2_1
		public Span<byte> ToSpan()
		{
			return new Span<byte>(buffer, 0, count);
		}

		public ReadOnlySpan<byte> ToReadOnlySpan()
		{
			return new ReadOnlySpan<byte>(buffer, 0, count);
		}
#endif //NETSTANDARD2_1

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
		#endregion //Methods
	}
}
