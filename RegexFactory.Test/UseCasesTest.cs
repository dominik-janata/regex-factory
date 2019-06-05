using System;
using System.Text.RegularExpressions;
using RegexFactory.Groups;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RegexFactory.Test
{
    [TestClass]
    public class UseCasesTest
    {
        [TestMethod]
        public void AnyNonEmptyString_Test()
        {
            // Arrange & Act
            var pattern = new RegexBuilder()
                .AddGroup(CharacterGroup.Any.RepeatOnePlusTimes())
                .Pattern;

            // Assert
            Assert.AreEqual(".+", pattern);
        }

        [TestMethod]
        public void MatchingSqlIdentifier_Test()
        {
            // Arrange
            var sqlIdentifierBuilder = new RegexBuilder()
                .AddGroup(new CharacterGroup('[').MakeOptional())
                .AddGroup(CharacterGroup.CharRange('A', 'Z'))
                .AddGroup(CharacterGroup.CharRange('a', 'z').RepeatOnePlusTimes())
                .AddGroup(new CharacterGroup(']').MakeOptional());
            var builder = sqlIdentifierBuilder.Clone()
                .AddGroup(new CharacterGroup('.'))
                .Concat(sqlIdentifierBuilder);

            // Act
            var regex = builder.Regex;

            // Assert
            Assert.IsTrue(regex.IsMatch("[Schema].[Name]"));
            Assert.IsTrue(regex.IsMatch("Schema.Name"));
        }

        [TestMethod]
        public void MatchingSqlIdentifierWithGroupName_Test()
        {
            // Arrange
            var sqlIdentifierBuilder = new RegexBuilder(RegexOptions.Multiline)
                .AddGroup(new CharacterGroup('[').MakeOptional())
                .AddGroup(CharacterGroup.CharRange('A', 'Z'))
                .AddGroup(CharacterGroup.CharRange('a', 'z').RepeatOnePlusTimes())
                .AddGroup(new CharacterGroup(']').MakeOptional());
            var builder =  new RegexBuilder()
                .BeginNamedGroup("procedureName")
                .Concat(sqlIdentifierBuilder)
                .AddGroup(new CharacterGroup('.'))
                .Concat(sqlIdentifierBuilder)
                .EndNamedGroup();
            var procedureScript = "CREATE PROCEDURE [Schema].[Name]\n\tAS\n RETURN 0;";

            // Act
            var regex = builder.Regex;
            var match = regex.Match(procedureScript);

            // Assert
            Assert.AreEqual("[Schema].[Name]", match.Groups["procedureName"].Value);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ClosingWrongGroup_FailsWithInvalidOperationException()
        {
            // Arrange
            var builder = new RegexBuilder()
                .BeginNamedGroup("group1")
                .BeginNamedGroup("group2");

            // Act & Assert
            builder = builder.EndNamedGroup("group1");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GetRegex_FailsWithInvalidOperationException_IfGroupNotTerminated()
        {
            // Arrange
            var builder = new RegexBuilder()
                .BeginNamedGroup("group1")
                .AddGroup(CharacterGroup.Any);

            // Act & Assert
            var regex = builder.Regex;
        }
    }
}
