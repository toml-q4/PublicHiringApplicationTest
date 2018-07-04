using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        public CsvRow ParseRow(string line)
        {
            // As per http://edoceo.com/utilitas/csv-file-format, leading and trailing whitespace should be ignored.
            // Also need to allow double-quotes and commas within quotes.
            const char escape = '"';
            const char split = ',';
            bool escapedPrior = false;
            bool escaped = false;

            var row = new CsvRow();
            var sb = new StringBuilder();
            for (int i = 0; i < line.Length; i++)
            {
                char c = line[i];

                switch (c)
                {
                    case escape:
                        if (!escaped)
                        {
                            escaped = true;
                        }
                        else
                        {
                            if (!escapedPrior)
                            {
                                if ((i + 1 < line.Length) && line[i + 1] == escape)
                                {
                                    escapedPrior = true;
                                }
                                else
                                {
                                    escaped = false;
                                }
                            }
                            else
                            {
                                escapedPrior = false;
                                sb.Append(c);
                            }
                        }
                        break;

                    case split:
                        if (escaped)
                        {
                            sb.Append(c);
                        }
                        else
                        {
                            row.Columns.Add(new CsvColumn(sb.ToString()));
                            sb.Length = 0;
                        }
                        break;

                    default:
                        sb.Append(c);
                        break;
                }
            }

            if (sb.Length > 0)
                row.Columns.Add(new CsvColumn(sb.ToString()));

            return row;
        }
    }
}
