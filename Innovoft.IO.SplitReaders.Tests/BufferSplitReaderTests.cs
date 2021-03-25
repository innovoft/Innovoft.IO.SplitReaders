using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Innovoft.IO
{
	[TestClass]
	public class BufferSplitReaderTests
	{
		[TestMethod]
		public void ReadColumnsAddTest()
		{
			const byte separator = BufferSplitReader.Comma;
			var expecteds = new string[] { "A", "B", "C", "D", "", };
			var text = string.Join(((char)separator).ToString(), expecteds);
			var raw = Encoding.UTF8.GetBytes(text);
			var actuals = new BufferColumns();
			using (var readerStream = new MemoryStream(raw))
			using (var reader = new BufferSplitReader(readerStream))
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

		[TestMethod]
		public void ReadColumnTest()
		{
			const byte separator = BufferSplitReader.Comma;
			var expecteds = new string[] { "A", "B", "C", "D", "", };
			var text = string.Join(((char)separator).ToString(), expecteds);
			var raw = Encoding.UTF8.GetBytes(text);
			for (var i = expecteds.Length - 1; i >= 0; --i)
			{
				var actual = new BufferColumn(Encoding.UTF8);
				using (var readerStream = new MemoryStream(raw))
				using (var reader = new BufferSplitReader(readerStream))
				{
					Assert.IsTrue(reader.ReadColumnsLength(separator, i, actual.AppendLength), "!ReadColumn");
					Assert.AreEqual(expecteds[i], actual.ToString(), i.ToString());
				}
			}
		}
	}
}
