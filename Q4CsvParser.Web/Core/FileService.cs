using System;
using System.Web;
using System.IO;
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
        private const string UploadFilePath = "~/App_Data/uploads/"; //"~/App_Data/uploads/";

        /// <summary>
        /// This file takes the file from the HttpPostedFileBase and saves the file to the appData folder
        /// </summary>
        /// <param name="file"></param>
        /// <returns>The file path in the appData folder the file was saved to</returns>
        public string StoreFile(HttpPostedFileBase file)
        {
            string fileLocation=null;
            //TODO fill in your logic here            
            if (file.ContentLength > 0)
            {
                var fileName = file.FileName;
                var filePath = UploadFilePath + fileName;
                fileLocation = HttpContext.Current.Server.MapPath(filePath);
                file.SaveAs(fileLocation);
            }
            return fileLocation;
            //throw new NotImplementedException();
        }

        /// <summary>
        /// This function takes in the filePath of a csv file stored in the app data folder and return the string content
        /// of that file
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns>The contents of the file in a string</returns>
        public string ReadFile(string filePath)
        {
            //TODO fill in your logic here
            string readFileText = File.ReadAllText(filePath);
            return readFileText;

            //throw new NotImplementedException();
        }
    }
}
