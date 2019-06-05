using RegexFactory.Groups;
using RegexFactory.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RegexFactory.Test.Extensions
{
    [TestClass]
    public class RegexBuilderGroupOperationsExtensionsTest
    {
        [TestMethod]
        public void SimpleTest()
        {
            // Arrange
            var builder = new RegexBuilder().AddGroup(CharacterGroup.Any.RepeatOnePlusTimes());
            var wrapped = builder.WrapInNamedGroup("theName");
            var regex = wrapped.Regex;

            // Act
            var match = regex.Match("abc");

            // Assert
            Assert.AreEqual(match.Groups["theName"].Value, "abc");

        }
    }
}
