using System.ComponentModel.DataAnnotations;
namespace CodingTrackerv2.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void IsNumberValid_NumberIsValid_ReturnsTrue()
        {
            string number = "124";

            bool isValid = Validation.isNumberValid(number);

            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void IsNumberValid_NumberIsInvalidNegative_ReturnsFalse()
        {
            string number = "-124";

            bool isValid = Validation.isNumberValid(number);

            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void IsNumberValid_NumberIsInvalidBlanksAndCharacters_ReturnsFalse()
        {
            string number = "1 2^4";

            bool isValid = Validation.isNumberValid(number);

            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void IsDateValid_DateIsValid_ReturnsTrue()
        {
            string date = "10-12-23 12:20";

            bool isValid = Validation.isDateValid(date);

            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void IsDateValid_DateIsInvalidWrongYear_ReturnsFalse()
        {
            string date = "10-12-2023 12:20";

            bool isValid = Validation.isDateValid(date);

            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void IsDateValid_DateIsInvalidWrongFormat_ReturnsFalse()
        {
            string date = "12:20 10-12-2023";

            bool isValid = Validation.isDateValid(date);

            Assert.IsFalse(isValid);
        }
    }
}