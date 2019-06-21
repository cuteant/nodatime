namespace NodaTime.Serialization.JsonSpan.Tests
{
    using System.Text;
    using SpanJson;
    using Xunit;
    using static NodaTime.Serialization.JsonSpan.Tests.TestHelper;

    /// <summary>
    /// The same tests as NodaDateIntervalConverterTest, but using the ISO-based interval converter.
    /// </summary>
    public class NodaIsoDateIntervalConverterTest
    {
        [Fact]
        public void RoundTrip()
        {
            var startLocalDate = new LocalDate(2012, 1, 2);
            var endLocalDate = new LocalDate(2013, 6, 7);
            var dateInterval = new DateInterval(startLocalDate, endLocalDate);
            AssertConversionsUtf16<DateInterval, NodaIsoExcludeNullsOriginalCaseResolver<char>>(dateInterval, "\"2012-01-02/2013-06-07\"");
            AssertConversionsUtf8<DateInterval, NodaIsoExcludeNullsOriginalCaseResolver<byte>>(dateInterval, "\"2012-01-02/2013-06-07\"");
        }

        [Theory]
        [InlineData("\"2012-01-022013-06-07\"")]
        public void InvalidJson(string json)
        {
            AssertInvalidJson<DateInterval, NodaIsoExcludeNullsOriginalCaseResolver<char>>(json);
        }

        [Fact]
        public void Serialize_InObject()
        {
            var startLocalDate = new LocalDate(2012, 1, 2);
            var endLocalDate = new LocalDate(2013, 6, 7);
            var dateInterval = new DateInterval(startLocalDate, endLocalDate);

            var testObject = new TestObject { Interval = dateInterval };

            var json = JsonSerializer.Generic.Utf16.Serialize<TestObject, NodaIsoExcludeNullsOriginalCaseResolver<char>>(testObject);
            var jsonBytes = JsonSerializer.Generic.Utf8.Serialize<TestObject, NodaIsoExcludeNullsOriginalCaseResolver<byte>>(testObject);

            var expectedJson = "{\"Interval\":\"2012-01-02/2013-06-07\"}";
            Assert.Equal(expectedJson, json);
            Assert.Equal(expectedJson, Encoding.UTF8.GetString(jsonBytes));
        }

        [Fact]
        public void Deserialize_InObject()
        {
            var json = "{\"Interval\":\"2012-01-02/2013-06-07\"}";

            var startLocalDate = new LocalDate(2012, 1, 2);
            var endLocalDate = new LocalDate(2013, 6, 7);
            var expectedInterval = new DateInterval(startLocalDate, endLocalDate);

            var testObject = JsonSerializer.Generic.Utf16.Deserialize<TestObject, NodaIsoExcludeNullsOriginalCaseResolver<char>>(json);
            Assert.Equal(expectedInterval, testObject.Interval);

            testObject = JsonSerializer.Generic.Utf8.Deserialize<TestObject, NodaIsoExcludeNullsOriginalCaseResolver<byte>>(Encoding.UTF8.GetBytes(json));
            Assert.Equal(expectedInterval, testObject.Interval);
        }

        public class TestObject
        {
            public DateInterval Interval { get; set; }
        }
    }
}
