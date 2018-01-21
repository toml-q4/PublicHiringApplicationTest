using System;
using Q4CsvParser.Contracts;
using Q4CsvParser.Domain;
using System.Collections;
using System.Collections.Generic;

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
            CsvTable parsedFileData = new CsvTable();
            //TODO fill in your logic here
            if (containsHeader)
            {
                CsvRow headerRow = new CsvRow();
                List<CsvColumn> cols = new List<CsvColumn>();
                string[] strCols = (fileContent.Split('\n')[0]).Split('\t');
                foreach (string s in strCols)
                {
                    cols.Add(new CsvColumn(s));
                }
                headerRow.Columns = cols;                
                parsedFileData.HeaderRow = headerRow;
            }
            else
                parsedFileData.HeaderRow = null;

            //Rows
            List<CsvRow> rows = new List<CsvRow>();
            string[] strRows = fileContent.Split('\n');
            foreach (string str in strRows)
            {
                CsvRow newRow = new CsvRow();
                List<CsvColumn> cols = new List<CsvColumn>();
                string[] strCols = (str.Split('\t'));
                foreach (string s in strCols)
                {
                    cols.Add(new CsvColumn(s));
                }
                newRow.Columns = cols;
                rows.Add(newRow);
            }
            parsedFileData.Rows = rows;

            return parsedFileData;
            //throw new NotImplementedException();
        }
    }
}
