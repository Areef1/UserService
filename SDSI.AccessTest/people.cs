using Microsoft.VisualStudio.TestTools.UnitTesting;
using SDSI.Access;

namespace SDSI.AccessTest
{
    [TestClass]
    public class people
    {
        [TestMethod]
        public void GetFullName_ReturnsCorrectFullName()
        {
            // Arrange
            var person = new Person
            {
                FirstName = "John",
                LastName = "Doe"
            };

            // Act
            var fullName = person.GetFullName();

            // Assert
            Assert.AreEqual("John Doe", fullName, "The full name should be 'John Doe'.");
        }

        [TestMethod]
        public void FirstName_SetAndGet_ReturnsCorrectValue()
        {
            // Arrange
            var person = new Person();

            // Act
            person.FirstName = "Jane";

            // Assert
            Assert.AreEqual("Jane", person.FirstName, "The first name should be 'Jane'.");
        }

        [TestMethod]
        public void LastName_SetAndGet_ReturnsCorrectValue()
        {
            // Arrange
            var person = new Person();

            // Act
            person.LastName = "Smith";

            // Assert
            Assert.AreEqual("Smith", person.LastName, "The last name should be 'Smith'.");
        }
    }
}
