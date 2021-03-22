using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Innovoft.IO
{
	[TestClass]
	public class SplitReaderTests
	{
		[TestMethod]
		public void ReadColumnsAddTest()
		{
			const char separator = ',';
			var expecteds = new string[] { "A", "B", "C", "D", "", };
			var text = string.Join(separator.ToString(), expecteds);
			var raw = Encoding.UTF8.GetBytes(text);
			var actuals = new List<string>();
			using (var readerStream = new MemoryStream(raw))
			using (var reader = new SplitReader(readerStream))
			{
				Assert.IsTrue(reader.ReadColumnsAdd(separator, actuals), "!ReadLine");
				Assert.AreEqual(expecteds.Length, actuals.Count, "expecteds.Length != actuals.Count");
				for (var i = expecteds.Length - 1; i >= 0; --i)
				{
					Assert.AreEqual(expecteds[i], actuals[i], i.ToString());
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
				using (var readerStream = new MemoryStream(raw))
				using (var reader = new SplitReader(readerStream))
				{
					Assert.IsTrue(reader.ReadColumn(separator, i, out var actual), "!ReadColumn");
					Assert.AreEqual(expecteds[i], actual, i.ToString());
				}
			}
		}
	}
}
