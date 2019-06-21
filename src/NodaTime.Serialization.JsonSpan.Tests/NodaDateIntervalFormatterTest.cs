namespace NodaTime.Serialization.JsonSpan.Tests
{
    using System.Text;
    using SpanJson;
    using Xunit;
    using static NodaTime.Serialization.JsonSpan.Tests.TestHelper;

    public class NodaDateIntervalFormatterTest
    {
        [Fact]
        public void RoundTrip()
        {
            var startLocalDate = new LocalDate(2012, 1, 2);
            var endLocalDate = new LocalDate(2013, 6, 7);
            var dateInterval = new DateInterval(startLocalDate, endLocalDate);
            AssertConversionsUtf16<DateInterval, NodaExcludeNullsOriginalCaseResolver<char>>(dateInterval, "{\"Start\":\"2012-01-02\",\"End\":\"2013-06-07\"}");
            AssertConversionsUtf8<DateInterval, NodaExcludeNullsOriginalCaseResolver<byte>>(dateInterval, "{\"Start\":\"2012-01-02\",\"End\":\"2013-06-07\"}");
        }

        [Fact]
        public void RoundTrip_CamelCase()
        {
            var startLocalDate = new LocalDate(2012, 1, 2);
            var endLocalDate = new LocalDate(2013, 6, 7);
            var dateInterval = new DateInterval(startLocalDate, endLocalDate);
            AssertConversionsUtf16<DateInterval, NodaExcludeNullsCamelCaseResolver<char>>(dateInterval, "{\"start\":\"2012-01-02\",\"end\":\"2013-06-07\"}");
            AssertConversionsUtf8<DateInterval, NodaExcludeNullsCamelCaseResolver<byte>>(dateInterval, "{\"start\":\"2012-01-02\",\"end\":\"2013-06-07\"}");
        }

        [Fact]
        public void Serialize_InObject()
        {
            var startLocalDate = new LocalDate(2012, 1, 2);
            var endLocalDate = new LocalDate(2013, 6, 7);
            var dateInterval = new DateInterval(startLocalDate, endLocalDate);

            var testObject = new TestObject { Interval = dateInterval };

            var json = JsonSerializer.Generic.Utf16.Serialize<TestObject, NodaExcludeNullsOriginalCaseResolver<char>>(testObject);
            var jsonBytes = JsonSerializer.Generic.Utf8.Serialize<TestObject, NodaExcludeNullsOriginalCaseResolver<byte>>(testObject);

            var expectedJson = "{\"Interval\":{\"Start\":\"2012-01-02\",\"End\":\"2013-06-07\"}}";
            Assert.Equal(expectedJson, json);
            Assert.Equal(expectedJson, Encoding.UTF8.GetString(jsonBytes));
        }

        [Fact]
        public void Serialize_InObject_CamelCase()
        {
            var startLocalDate = new LocalDate(2012, 1, 2);
            var endLocalDate = new LocalDate(2013, 6, 7);
            var dateInterval = new DateInterval(startLocalDate, endLocalDate);

            var testObject = new TestObject { Interval = dateInterval };

            var json = JsonSerializer.Generic.Utf16.Serialize<TestObject, NodaExcludeNullsCamelCaseResolver<char>>(testObject);
            var jsonBytes = JsonSerializer.Generic.Utf8.Serialize<TestObject, NodaExcludeNullsCamelCaseResolver<byte>>(testObject);

            var expectedJson = "{\"interval\":{\"start\":\"2012-01-02\",\"end\":\"2013-06-07\"}}";
            Assert.Equal(expectedJson, json);
            Assert.Equal(expectedJson, Encoding.UTF8.GetString(jsonBytes));
        }

        [Fact]
        public void Deserialize_InObject()
        {
            var json = "{\"Interval\":{\"Start\":\"2012-01-02\",\"End\":\"2013-06-07\"}}";

            var startLocalDate = new LocalDate(2012, 1, 2);
            var endLocalDate = new LocalDate(2013, 6, 7);
            var expectedInterval = new DateInterval(startLocalDate, endLocalDate);

            var testObject = JsonSerializer.Generic.Utf16.Deserialize<TestObject, NodaExcludeNullsOriginalCaseResolver<char>>(json);
            Assert.Equal(expectedInterval, testObject.Interval);
            testObject = JsonSerializer.Generic.Utf8.Deserialize<TestObject, NodaExcludeNullsOriginalCaseResolver<byte>>(Encoding.UTF8.GetBytes(json));
            Assert.Equal(expectedInterval, testObject.Interval);
        }

        [Fact]
        public void Deserialize_InObject_CamelCase()
        {
            var json = "{\"interval\":{\"start\":\"2012-01-02\",\"end\":\"2013-06-07\"}}";

            var startLocalDate = new LocalDate(2012, 1, 2);
            var endLocalDate = new LocalDate(2013, 6, 7);
            var expectedInterval = new DateInterval(startLocalDate, endLocalDate);

            var testObject = JsonSerializer.Generic.Utf16.Deserialize<TestObject, NodaExcludeNullsCamelCaseResolver<char>>(json);
            Assert.Equal(expectedInterval, testObject.Interval);
            testObject = JsonSerializer.Generic.Utf8.Deserialize<TestObject, NodaExcludeNullsCamelCaseResolver<byte>>(Encoding.UTF8.GetBytes(json));
            Assert.Equal(expectedInterval, testObject.Interval);
        }

        public class TestObject
        {
            public DateInterval Interval { get; set; }
        }
    }
}
