using System;
using System.Linq;
using Q4CsvParser.Contracts;
using Q4CsvParser.Domain;

namespace Q4CsvParser.Web.Core
{
    /// <summary>
    /// This file must be unit tested.
    /// </summary>
    public class ParsingService : IParsingService
    {
        /// <summary>
        /// Accepts a string with the contents of the csv file in it and should return a parsed csv file.
        /// </summary>
        /// <param name="fileContent"></param>
        /// <param name="containsHeader"></param>
        /// <returns></returns>
        public CsvTable ParseCsv(string fileContent, bool containsHeader)
        {
            try
            {
                var table = new CsvTable();

                string[] lines = fileContent.Split(
                    new[] { "\r\n", "\r", "\n" },
                    StringSplitOptions.RemoveEmptyEntries
                );

                for (int i = 0; i < lines.Length; i++)
                {
                    var row = lines[i];

                    if (containsHeader && i == 0)
                    {
                        table.HeaderRow = ParseRow(row);
                    }
                    else
                    {
                        table.Rows.Add(ParseRow(row));
                    }

                }

                return table;
            }
            catch
            {
                // Row parsing failures should bubble up to here.
                return null;
            }
        }

        /// <summary>
        /// Accepts a single-line string with the contents of the CSV row.
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public CsvRow ParseRow(string rowText)
        {
            var row = new CsvRow();
            row.Columns.AddRange(rowText.Split(',').Select(x => new CsvColumn(x)));
            return row;
        }
    }
}
