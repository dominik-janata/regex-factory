using System;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RegexFactory.Test
{
    [TestClass]
    public class RegexBuilderFactoryTest
    {
        [TestMethod]
        public void CreateForPattern_Succeeds_ForValidPattern()
        {
            // Arrange & Act
            var regexBuilder = RegexBuilderFactory.CreateFromPattern(".*");

            // Assert
            Assert.AreEqual(".*", regexBuilder.Pattern);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CreateForPattern_Fails_ForInvalidPattern()
        {
            // Arrange, Act & Assert
            RegexBuilderFactory.CreateFromPattern(".**");
        }

        [TestMethod]
        public void CreateForRegex_Succeeds_ForValidPattern()
        {
            // Arrange & Act
            var regexBuilder = RegexBuilderFactory.CreateFromRegex(new Regex(".*"));

            // Assert
            Assert.AreEqual(".*", regexBuilder.Pattern);
        }
    }
}
