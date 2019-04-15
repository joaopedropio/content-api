using ContentApi.Helpers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContentApiUnitTests.QueryHelperTests
{
    public class CreateSerachByTests
    {
        [Test]
        public void Should_ThrowException_When_TableIsInvalid()
        {
            // Arrange
            string table = null;
            string column = "NAME";
            string value = "value";

            // Assert
            Assert.Throws<ArgumentNullException>(() => QueryHelper.CreateSearchBy(table, column, value));
        }

        [Test]
        public void Should_ThrowException_When_ColumnIsInvalid()
        {
            // Arrange
            string table = "MEDIAS";
            string column = "";
            string value = "value";

            // Assert
            Assert.Throws<ArgumentNullException>(() => QueryHelper.CreateSearchBy(table, column, value));
        }

        [Test]
        public void Should_ThrowException_When_ValueIsInvalid()
        {
            // Arrange
            string table = "MEDIAS";
            string column = "NAME";
            string value = "";

            // Assert
            Assert.Throws<ArgumentNullException>(() => QueryHelper.CreateSearchBy(table, column, value));
        }

        [Test]
        public void Should_CreateSearchQuery_When_OneValueIsProvided()
        {
            // Arrange
            var expected = "SELECT ID FROM TABLE WHERE NAME LIKE '%VALUE%';";

            // Act
            var actual = QueryHelper.CreateSearchBy("TABLE", "NAME", "VALUE");

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Should_CreateSearchQuery_When_ManyValuesAreProvided()
        {
            // Arrange
            var expected = "SELECT ID FROM TABLE WHERE NAME LIKE '%JOHN%PETER%';";

            // Act
            var actual = QueryHelper.CreateSearchBy("TABLE", "NAME", "JOHN PETER");

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
