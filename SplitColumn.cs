using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Innovoft.IO
{
	public class SplitColumn<T> : IEquatable<SplitColumn<T>>
		where T : IEquatable<T>
	{
		#region Constants
		public const int DefaultCapacity = 128;
		#endregion //Constants

		#region Class Methods
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Clear(SplitColumn<T>[] values)
		{
			foreach (var value in values)
			{
				value?.Clear();
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Clear(IEnumerable<SplitColumn<T>> values)
		{
			foreach (var value in values)
			{
				value?.Clear();
			}
		}
		#endregion //Class Methods

		#region Fields
		protected int capacity;
		protected T[] values;
		protected int count;
		protected bool appended;
		#endregion //Fields

		#region Constructors
		public SplitColumn()
			: this(DefaultCapacity)
		{
		}

		public SplitColumn(int capacity)
		{
			this.capacity = capacity;
			this.values = new T[capacity];
			Clear();
		}
		#endregion //Constructors

		#region Properties
		public int Capacity => capacity;
		public T[] Values => values;
		public int Count => count;
		public bool Appended => appended;
		#endregion //Properties

		#region Indexes
		public T this[int offset] { get => values[offset]; set => values[offset] = value; }
		#endregion //Indexes

		#region Methods
		public override int GetHashCode()
		{
			var hash = 0;
			for (var offset = 0; offset < count; ++offset)
			{
				hash ^= values[offset].GetHashCode();
			}
			return hash;
		}

		public override bool Equals(object other)
		{
			return Equals(other as SplitColumn<T>);
		}

		public bool Equals(SplitColumn<T> other)
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
				if (!this.values[offset].Equals(other.values[offset]))
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

		public void AppendLength(T[] append, int offset, int length)
		{
			appended = true;
			var required = count + length;
			if (required > capacity)
			{
				Enlarge();
			}
			Array.Copy(append, offset, values, count, length);
			count += length;
		}

		public void AppendEnding(T[] append, int offset, int ending)
		{
			appended = true;
			var length = ending - offset;
			var required = count + length;
			if (required > capacity)
			{
				Enlarge();
			}
			Array.Copy(append, offset, values, count, length);
			count += length;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected void Enlarge()
		{
			var enlargedCapacity = 2 * capacity;
			var enlarged = new T[enlargedCapacity];
			Array.Copy(values, 0, enlarged, 0, count);
			capacity = enlargedCapacity;
			values = enlarged;
		}

		public T[] ToArray()
		{
			if (!appended)
			{
				return null;
			}

			var value = new T[count];
			Array.Copy(values, 0, value, 0, count);
			return value;
		}

		public void Copy(T[] value, int offset)
		{
			Array.Copy(values, 0, value, offset, count);
		}

#if NETSTANDARD2_1 || NET5_0_OR_GREATER
		public Span<T> ToSpan()
		{
			return new Span<T>(values, 0, count);
		}

		public Span<T> ToSpan(int offset, int length)
		{
			return new Span<T>(values, offset, length);
		}

		public Span<T> ToSpanOffset(int offset)
		{
			return new Span<T>(values, offset, count - offset);
		}

		public static implicit operator Span<T>(SplitColumn<T> value)
		{
			return value.ToSpan();
		}

		public ReadOnlySpan<T> ToReadOnlySpan()
		{
			return new ReadOnlySpan<T>(values, 0, count);
		}

		public ReadOnlySpan<T> ToReadOnlySpan(int offset, int length)
		{
			return new ReadOnlySpan<T>(values, offset, length);
		}

		public ReadOnlySpan<T> ToReadOnlySpanOffset(int offset)
		{
			return new ReadOnlySpan<T>(values, offset, count - offset);
		}

		public static implicit operator ReadOnlySpan<T>(SplitColumn<T> value)
		{
			return value.ToReadOnlySpan();
		}

		public Memory<T> ToMemory()
		{
			return new Memory<T>(values, 0, count);
		}

		public Memory<T> ToMemory(int offset, int length)
		{
			return new Memory<T>(values, offset, length);
		}

		public Memory<T> ToMemoryOffset(int offset)
		{
			return new Memory<T>(values, offset, count - offset);
		}

		public static implicit operator Memory<T>(SplitColumn<T> value)
		{
			return value.ToMemory();
		}

		public ReadOnlyMemory<T> ToReadOnlyMemory()
		{
			return new ReadOnlyMemory<T>(values, 0, count);
		}

		public ReadOnlyMemory<T> ToReadOnlyMemory(int offset, int length)
		{
			return new ReadOnlyMemory<T>(values, offset, length);
		}

		public static implicit operator ReadOnlyMemory<T>(SplitColumn<T> value)
		{
			return value.ToReadOnlyMemory();
		}
#endif //NETSTANDARD2_1 || NET5_0_OR_GREATER
		#endregion //Methods
	}
}
