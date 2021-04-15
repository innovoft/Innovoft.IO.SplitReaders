using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Innovoft.IO
{
	[TestClass]
	public class BytesSplitReaderTests
	{
		[TestMethod]
		public void ReadColumnsAddFuncTest()
		{
			const byte separator = BytesSplitReader.Comma;
			var expecteds = new string[] { "A", "B", "C", "D", "", };
			var text = string.Join(((char)separator).ToString(), expecteds);
			var raw = Encoding.UTF8.GetBytes(text);
			var actuals = new BytesColumns();
			using (var readerStream = new MemoryStream(raw))
			using (var reader = new BytesSplitReader(readerStream))
			{
				Assert.IsTrue(reader.ReadColumnsLength(separator, actuals.AddLength), "!ReadColumns");
				Assert.AreEqual(expecteds.Length, actuals.Count, "expecteds.Length != actuals.Count");
				var actualsColumns = actuals.ToStringsArray(Encoding.UTF8);
				for (var i = expecteds.Length - 1; i >= 0; --i)
				{
					Assert.AreEqual(expecteds[i], actualsColumns[i], i.ToString());
				}
			}
		}

		[TestMethod]
		public void ReadColumnsAddColumnsTest()
		{
			const byte separator = BytesSplitReader.Comma;
			var expecteds = new string[] { "A", "B", "C", "D", "", };
			var text = string.Join(((char)separator).ToString(), expecteds);
			var raw = Encoding.UTF8.GetBytes(text);
			var actuals = new BytesColumns();
			using (var readerStream = new MemoryStream(raw))
			using (var reader = new BytesSplitReader(readerStream))
			{
				Assert.IsTrue(reader.ReadColumnsLength(separator, actuals), "!ReadColumns");
				Assert.AreEqual(expecteds.Length, actuals.Count, "expecteds.Length != actuals.Count");
				var actualsColumns = actuals.ToStringsArray(Encoding.UTF8);
				for (var i = expecteds.Length - 1; i >= 0; --i)
				{
					Assert.AreEqual(expecteds[i], actualsColumns[i], i.ToString());
				}
			}
		}

		[TestMethod]
		public void ReadColumnTest()
		{
			const byte separator = BytesSplitReader.Comma;
			var expecteds = new string[] { "A", "B", "C", "D", "", };
			var text = string.Join(((char)separator).ToString(), expecteds);
			var raw = Encoding.UTF8.GetBytes(text);
			for (var i = expecteds.Length - 1; i >= 0; --i)
			{
				var actual = new BytesColumn();
				using (var readerStream = new MemoryStream(raw))
				using (var reader = new BytesSplitReader(readerStream))
				{
					Assert.IsTrue(reader.ReadColumnsLength(separator, i, actual.AppendLength), "!ReadColumn");
					Assert.AreEqual(expecteds[i], actual.ToString(Encoding.UTF8), i.ToString());
				}
			}
		}

		[TestMethod]
		public void ReadColumnsSetTest()
		{
			const byte separator = BytesSplitReader.Comma;
			var expecteds = new string[] { "A", "B", "C", "D", "", };
			var text = string.Join(((char)separator).ToString(), expecteds);
			var raw = Encoding.UTF8.GetBytes(text);
			var columns = new int[expecteds.Length];
			for (var column = columns.Length - 1; column >= 0; --column)
			{
				columns[column] = column;
			}
			var actuals = new BytesColumn[expecteds.Length];
			var appends = new Action<byte[], int, int>[expecteds.Length];
			for (var i = actuals.Length - 1; i >= 0; --i)
			{
				var actual = new BytesColumn();
				actuals[i] = actual;
				appends[i] = actual.AppendLength;
			}
			using (var readerStream = new MemoryStream(raw))
			using (var reader = new BytesSplitReader(readerStream))
			{
				Assert.IsTrue(reader.ReadColumnsLength(separator, appends), "!ReadColumns");
				for (var i = expecteds.Length - 1; i >= 0; --i)
				{
					Assert.AreEqual(expecteds[i], actuals[i].ToString(Encoding.UTF8), i.ToString());
				}
			}
		}
	}
}
