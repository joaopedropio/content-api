using ContentApi.Helpers;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace ContentApiUnitTests.QueryHelperTests
{
    public class CreateInsertQueryTests
    {
        [Test]
        public void Should_ThrowException_When_InputIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => QueryHelper.CreateInsertQuery(null, null));
        }

        [Test]
        public void Should_ThrowException_When_InputIsEmpty()
        {
            Assert.Throws<ArgumentNullException>(() => QueryHelper.CreateInsertQuery(string.Empty, new List<KeyValuePair<string, string>>()));
        }

        [Test]
        public void Should_ThrowException_When_InvalidColumnNameIsProvided()
        {
            var table = "TABELA";
            var columns = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("", "VALUE")
            };

            Assert.Throws<ArgumentNullException>(() => QueryHelper.CreateInsertQuery(table, columns));
        }

        [Test]
        public void Should_ThrowException_When_InvalidColumnValueIsProvided()
        {
            var table = "TABELA";
            var columns = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("NAME", "")
            };

            Assert.Throws<ArgumentNullException>(() => QueryHelper.CreateInsertQuery(table, columns));
        }

        [Test]
        public void Should_CreatreQueryString_When_ValidInputIsProvided()
        {
            var table = "PERSON";
            var columns = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("NAME", "Arnold Schwarzenegger"),
                new KeyValuePair<string, string>("BIRTHDAY", "1947-07-30 00:00:00")
            };
            var expected = "INSERT INTO PERSON (NAME,BIRTHDAY) VALUES ('Arnold Schwarzenegger','1947-07-30 00:00:00');";

            var result = QueryHelper.CreateInsertQuery(table, columns);

            Assert.AreEqual(expected, result);
        }
    }
}
