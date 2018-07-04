using System;
using System.IO;
using System.Web;
using Q4CsvParser.Contracts;

namespace Q4CsvParser.Web.Core
{
    /// <summary>
    /// This file does not need to be unit testable.
    /// Bonus Task:
    /// - Make this file unit testable using the adapter pattern for your file system interactions
    /// - Unit test this file
    /// </summary>
    public class FileService : IFileService
    {
        private const string UploadFilePath = "~/App_Data/uploads/";

        /// <summary>
        /// This file takes the file from the HttpPostedFileBase and saves the file to the appData folder
        /// </summary>
        /// <param name="file"></param>
        /// <returns>The file path in the appData folder the file was saved to</returns>
        public string StoreFile(HttpPostedFileBase file)
        {
            // TODO: Should probably not use HttpContex.Current
            var filePath = Path.Combine(HttpContext.Current.Server.MapPath(UploadFilePath), file.FileName);

            try
            {
                var fi = new FileInfo(filePath);
            }
            catch
            {
                return null;
            }

            // Overwrite existing files of the same name.
            using (var fs = File.Open(filePath, FileMode.Create))
            {
                file.InputStream.CopyTo(fs);
            }

            return filePath;
        }

        /// <summary>
        /// This function takes in the filePath of a csv file stored in the app data folder and return the string content
        /// of that file
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns>The contents of the file in a string</returns>
        public string ReadFile(string filePath)
        {
            return File.ReadAllText(filePath);
        }
    }
}
