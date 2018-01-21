using System;
using System.Linq;
using Q4CsvParser.Contracts;

namespace Q4CsvParser.Web.Core
{
    /// <summary>
    /// This file must be unit tested
    /// </summary>
    public class ValidationService : IValidationService
    {
        /// <summary>
        /// Takes in a file name and determines whether it is a csv file or not.
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public bool IsCsvFile(string filename)
        {
            bool IsCsvFile = false;
            //TODO fill in your logic here
            if (filename.Contains(".csv"))
                IsCsvFile = true;

            return IsCsvFile;

            //throw new NotImplementedException();
        }
    }
}
