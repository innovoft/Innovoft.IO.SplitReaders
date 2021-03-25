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
				var actualsColumns = actuals.ToArray();
				for (var i = expecteds.Length - 1; i >= 0; --i)
				{
					Assert.AreEqual(expecteds[i], actualsColumns[i], i.ToString());
				}
			}
		}
	}
}
