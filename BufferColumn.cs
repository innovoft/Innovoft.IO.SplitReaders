using System;
using System.Collections.Generic;
using System.Text;

namespace Innovoft.IO
{
	public class BufferColumn
	{
		#region Fields
		private int capacity;
		private byte[] buffer;
		private int count;
		private bool appended;
		#endregion //Fields

		#region Constructors
		public BufferColumn()
			: this(128)
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
		#region Indexes

		#region Methods
		public void Clear()
		{
			count = 0;
			appended = false;
		}

		public void AppendLength(byte[] append, int offset, int length)
		{
			appended = true;
			var required = count + length;
			if (required > capacity)
			{
				var tempCapacity = 2 * capacity;
				var temp = new byte[tempCapacity];
				System.Buffer.BlockCopy(buffer, 0, temp, 0, count);
				capacity = tempCapacity;
				buffer = temp;
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
				var tempCapacity = 2 * capacity;
				var temp = new byte[tempCapacity];
				System.Buffer.BlockCopy(buffer, 0, temp, 0, count);
				capacity = tempCapacity;
				buffer = temp;
			}
			System.Buffer.BlockCopy(append, offset, buffer, count, length);
			count += length;
		}

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
