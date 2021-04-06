using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Innovoft.IO
{
	[TestClass]
	public class CharsSplitReaderTests
	{
		[TestMethod]
		public void ReadColumnsAddTest()
		{
			const char separator = ',';
			var expecteds = new string[] { "A", "B", "C", "D", "", };
			var text = string.Join(separator.ToString(), expecteds);
			var raw = Encoding.UTF8.GetBytes(text);
			var actuals = new CharsColumns();
			using (var readerStream = new MemoryStream(raw))
			using (var reader = new CharsSplitReader(readerStream))
			{
				Assert.IsTrue(reader.ReadColumnsLength(separator, actuals.AddLength), "!ReadColumns");
				Assert.AreEqual(expecteds.Length, actuals.Count, "expecteds.Length != actuals.Count");
				var actualsColumns = actuals.ToStringsArray();
				for (var i = expecteds.Length - 1; i >= 0; --i)
				{
					Assert.AreEqual(expecteds[i], actualsColumns[i], i.ToString());
				}
			}
		}

		[TestMethod]
		public void ReadColumnTest()
		{
			const char separator = ',';
			var expecteds = new string[] { "A", "B", "C", "D", "", };
			var text = string.Join(separator.ToString(), expecteds);
			var raw = Encoding.UTF8.GetBytes(text);
			for (var i = expecteds.Length - 1; i >= 0; --i)
			{
				var actual = new CharsColumn();
				using (var readerStream = new MemoryStream(raw))
				using (var reader = new CharsSplitReader(readerStream))
				{
					Assert.IsTrue(reader.ReadColumnsLength(separator, i, actual.AppendLength), "!ReadColumn");
					Assert.AreEqual(expecteds[i], actual.ToString(), i.ToString());
				}
			}
		}

		[TestMethod]
		public void ReadColumnsSetTest()
		{
			const char separator = ',';
			var expecteds = new string[] { "A", "B", "C", "D", "", };
			var text = string.Join(separator.ToString(), expecteds);
			var raw = Encoding.UTF8.GetBytes(text);
			var columns = new int[expecteds.Length];
			for (var column = columns.Length - 1; column >= 0; --column)
			{
				columns[column] = column;
			}
			var actuals = new CharsColumn[expecteds.Length];
			var appends = new Action<char[], int, int>[expecteds.Length];
			for (var i = actuals.Length - 1; i >= 0; --i)
			{
				var actual = new CharsColumn();
				actuals[i] = actual;
				appends[i] = actual.AppendLength;
			}
			using (var readerStream = new MemoryStream(raw))
			using (var reader = new CharsSplitReader(readerStream))
			{
				Assert.IsTrue(reader.ReadColumnsLength(separator, appends), "!ReadColumns");
				for (var i = expecteds.Length - 1; i >= 0; --i)
				{
					Assert.AreEqual(expecteds[i], actuals[i].ToString(), i.ToString());
				}
			}
		}
	}
}
