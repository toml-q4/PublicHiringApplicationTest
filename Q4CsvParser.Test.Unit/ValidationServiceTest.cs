using NUnit;
using NUnit.Framework;
using Q4CsvParser.Web.Core;

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
    public class ValidationServiceTest
    {
        [Test]
        public void IsCsvFileTest()
        {
            const string validFileName = "valid_csv_filename.csv";
            const string invalidFileName = "invalid_csv_filename.bmp";

            var validationService = new ValidationService();
            Assert.IsTrue(validationService.IsCsvFile(validFileName));
            Assert.IsFalse(validationService.IsCsvFile(invalidFileName));
        }
    }
}
