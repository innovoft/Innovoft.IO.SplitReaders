using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;

namespace Innovoft.IO
{
	public sealed partial class SplitReader : IDisposable
	{
		#region Constants
		public const char CR = '\r';
		public const char LF = '\n';
		#endregion //Constants

		#region Fields
		private Stream stream;
		private Func<byte[], int, int, int> streamRead;
		private readonly Encoding encoding;
		private readonly Decoder decoder;
		private readonly Func<byte[], int, int, char[], int, int> decoderGetChars;
		private readonly int length;
		private readonly byte[] buffer;
		private readonly char[] letters;
		private int lettersOffset;
		private int lettersLength;
		private readonly StringBuilder builder = new StringBuilder();
		#endregion //Fields

		#region Constructors
		public SplitReader(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException(nameof(stream));
			}
			if (!stream.CanRead)
			{
				throw new ArgumentException("!stream.CanRead");
			}
			this.stream = stream;
			this.streamRead = stream.Read;
			this.encoding = System.Text.Encoding.UTF8;
			this.decoder = encoding.GetDecoder();
			this.decoderGetChars = decoder.GetChars;
			this.length = 4096;
			this.buffer = new byte[length];
			this.letters = new char[length];
			this.lettersOffset = 0;
			this.lettersLength = 0;
		}
		#endregion //Constructors

		#region Finalizer
		~SplitReader()
		{
			Dispose(disposing: false);
		}
		#endregion //Finalizer

		#region Properties
		public Encoding Encoding => encoding;
		#endregion //Properties

		#region Methods
		#region Dispose
		public void Dispose()
		{
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}

		private void Dispose(bool disposing)
		{
			Interlocked.Exchange(ref stream, null)?.Dispose();
			streamRead = null;
		}
		#endregion //Dispose

		public bool ReadColumn(char separator, int column, out string value)
		{
			if (stream == null)
			{
				throw new ObjectDisposedException(nameof(SplitReader));
			}
			if (lettersOffset >= lettersLength && !ReadBuffers())
			{
				value = null;
				return false;
			}
			var offset = lettersOffset;
			var building = false;
			string columnValue = null;
			while (true)
			{
				var letter = letters[lettersOffset];
				switch (letter)
				{
				case CR:
					if (column == 0)
					{
						if (building)
						{
							builder.Append(letters, offset, lettersOffset - offset);
							columnValue = builder.ToString();
							builder.Clear();
						}
						else
						{
							columnValue = new string(letters, offset, lettersOffset - offset);
						}
					}
					++lettersOffset;
					//LF
					if (lettersOffset >= lettersLength)
					{
						if (!ReadBuffers())
						{
							value = columnValue;
							return true;
						}
					}
					if (letters[lettersOffset] == LF)
					{
						++lettersOffset;
					}
					value = columnValue;
					return true;

				case LF:
					if (column == 0)
					{
						if (building)
						{
							builder.Append(letters, offset, lettersOffset - offset);
							columnValue = builder.ToString();
							builder.Clear();
							++lettersOffset;
							value = columnValue;
							return true;
						}
						else
						{
							columnValue = new string(letters, offset, lettersOffset - offset);
							++lettersOffset;
							value = columnValue;
							return true;
						}
					}
					else
					{
						++lettersOffset;
						value = columnValue;
						return true;
					}

				default:
					if (letter == separator)
					{
						if (column == 0)
						{
							if (building)
							{
								builder.Append(letters, offset, lettersOffset - offset);
								columnValue = builder.ToString();
								builder.Clear();
								building = false;
								offset = ++lettersOffset;
								--column;
								break;
							}
							else
							{
								columnValue = new string(letters, offset, lettersOffset - offset);
								offset = ++lettersOffset;
								--column;
								break;
							}
						}
						else
						{
							offset = ++lettersOffset;
							--column;
							break;
						}
					}
					else
					{
						++lettersOffset;
					}
					break;
				}
				if (lettersOffset >= lettersLength)
				{
					if (column == 0)
					{
						if (offset <= lettersOffset)
						{
							builder.Append(letters, offset, lettersOffset - offset);
							building = true;
						}
					}
					if (!ReadBuffers())
					{
						if (building)
						{
							columnValue = builder.ToString();
							builder.Clear();
						}
						value = columnValue;
						return true;
					}
					offset = 0;
				}
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool Contains(char[] values, char value)
		{
			for (var i = values.Length - 1; i >= 0; --i)
			{
				if (values[i] == value)
				{
					return true;
				}
			}
			return false;
		}

		private bool ReadBuffers()
		{
			while (true)
			{
				var read = streamRead(buffer, 0, length);
				if (read <= 0)
				{
					return false;
				}
				lettersOffset = 0;
				lettersLength = decoderGetChars(buffer, 0, read, letters, 0);
				if (lettersLength > 0)
				{
					return true;
				}
			}
		}
		#endregion //Methods
	}
}
