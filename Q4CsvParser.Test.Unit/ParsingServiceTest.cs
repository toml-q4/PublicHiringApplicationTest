﻿using Q4CsvParser.Web.Core;
using System;
using NUnit;
using NUnit.Framework;

namespace Q4CsvParser.Test.Unit
{
    /// <summary>
    /// This class should have content. 
    /// Feel free to use any testing framework you desire. (i.e. NUnit, XUnit, Microsoft built-in testing framework)
    /// You may also use a mocking framework (i.e. Moq, RhinoMock)
    /// 
    /// If you've never done unit testing before, don't worry about this section and look to complete some of the bonus mark tasks
    /// </summary>
    [TestFixture]
    public class ParsingServiceTest
    {
        [Test]
        public void ParseCsvHeaderedTest()
        {
            var rawCsv = @"
                Header1,Header2
                Row1Value1,Row1Value2
                Row2Value1,Row2Value2
            ";

            var parsingService = new ParsingService();
            var csvTable = parsingService.ParseCsv(rawCsv, true);

            Assert.NotNull(csvTable.HeaderRow);
            Assert.AreEqual(2, csvTable.HeaderRow.Columns.Count);
            Assert.AreEqual(2, csvTable.Rows.Count);
            Assert.AreEqual("HeaderValue1", csvTable.HeaderRow.Columns[0].Value);
            Assert.AreEqual("HeaderValue2", csvTable.HeaderRow.Columns[1].Value);
            Assert.AreEqual("Row1Value1", csvTable.Rows[0].Columns[0].Value);
            Assert.AreEqual("Row1Value2", csvTable.Rows[0].Columns[1].Value);
            Assert.AreEqual("Row2Value1", csvTable.Rows[1].Columns[0].Value);
            Assert.AreEqual("Row2Value2", csvTable.Rows[1].Columns[1].Value);
        }

        [Test]
        public void ParseCsvUnheaderedTest()
        {
            var rawCsv = @"
                Row1Value1,Row1Value2
                Row2Value1,Row2Value2
            ";

            var parsingService = new ParsingService();
            var csvTable = parsingService.ParseCsv(rawCsv, true);

            Assert.IsNull(csvTable.HeaderRow);
            Assert.AreEqual(2, csvTable.Rows.Count);
            Assert.AreEqual("Row1Value1", csvTable.Rows[0].Columns[0].Value);
            Assert.AreEqual("Row1Value2", csvTable.Rows[0].Columns[1].Value);
            Assert.AreEqual("Row2Value1", csvTable.Rows[1].Columns[0].Value);
            Assert.AreEqual("Row2Value2", csvTable.Rows[1].Columns[1].Value);
        }

        [Test]
        public void ParseRow()
        {
            var line = "Value1,\"Value, 2!\",Value 3";

            var parsingService = new ParsingService();
            var row = parsingService.ParseRow(line);

            Assert.AreEqual("Value1", row.Columns[0].Value);
            Assert.AreEqual("\"Value, 2!\"", row.Columns[0].Value);
            Assert.AreEqual("Value 3", row.Columns[0].Value);
        }
    }
}
