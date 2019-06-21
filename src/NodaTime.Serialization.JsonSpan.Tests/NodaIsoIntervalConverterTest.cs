namespace NodaTime.Serialization.JsonSpan.Tests
{
    using System.Text;
    using SpanJson;
    using Xunit;
    using static NodaTime.Serialization.JsonSpan.Tests.TestHelper;

    /// <summary>
    /// The same tests as NodaIntervalConverterTest, but using the ISO-based interval converter.
    /// </summary>
    public class NodaIsoIntervalConverterTest
    {
        [Fact]
        public void RoundTrip()
        {
            var startInstant = Instant.FromUtc(2012, 1, 2, 3, 4, 5) + Duration.FromMilliseconds(670);
            var endInstant = Instant.FromUtc(2013, 6, 7, 8, 9, 10) + Duration.FromNanoseconds(123456789);
            var interval = new Interval(startInstant, endInstant);
            AssertConversionsUtf16<Interval, NodaIsoExcludeNullsOriginalCaseResolver<char>>(interval, "\"2012-01-02T03:04:05.67Z/2013-06-07T08:09:10.123456789Z\"");
            AssertConversionsUtf8<Interval, NodaIsoExcludeNullsOriginalCaseResolver<byte>>(interval, "\"2012-01-02T03:04:05.67Z/2013-06-07T08:09:10.123456789Z\"");
        }

        [Fact]
        public void RoundTrip_Infinite()
        {
            var instant = Instant.FromUtc(2013, 6, 7, 8, 9, 10) + Duration.FromNanoseconds(123456789);
            AssertConversionsUtf16<Interval, NodaIsoExcludeNullsOriginalCaseResolver<char>>(new Interval(null, instant), "\"/2013-06-07T08:09:10.123456789Z\"");
            AssertConversionsUtf16<Interval, NodaIsoExcludeNullsOriginalCaseResolver<char>>(new Interval(instant, null), "\"2013-06-07T08:09:10.123456789Z/\"");
            AssertConversionsUtf16<Interval, NodaIsoExcludeNullsOriginalCaseResolver<char>>(new Interval(null, null), "\"/\"");
            AssertConversionsUtf8<Interval, NodaIsoExcludeNullsOriginalCaseResolver<byte>>(new Interval(null, instant), "\"/2013-06-07T08:09:10.123456789Z\"");
            AssertConversionsUtf8<Interval, NodaIsoExcludeNullsOriginalCaseResolver<byte>>(new Interval(instant, null), "\"2013-06-07T08:09:10.123456789Z/\"");
            AssertConversionsUtf8<Interval, NodaIsoExcludeNullsOriginalCaseResolver<byte>>(new Interval(null, null), "\"/\"");
        }

        [Fact]
        public void DeserializeComma()
        {
            // Comma is deliberate, to show that we can parse a comma decimal separator too.
            var json = "\"2012-01-02T03:04:05.670Z/2013-06-07T08:09:10,1234567Z\"";

            var startInstant = Instant.FromUtc(2012, 1, 2, 3, 4, 5) + Duration.FromMilliseconds(670);
            var endInstant = Instant.FromUtc(2013, 6, 7, 8, 9, 10) + Duration.FromTicks(1234567);
            var expectedInterval = new Interval(startInstant, endInstant);

            var interval = JsonSerializer.Generic.Utf16.Deserialize<Interval, NodaIsoExcludeNullsOriginalCaseResolver<char>>(json);
            Assert.Equal(expectedInterval, interval);

            interval = JsonSerializer.Generic.Utf8.Deserialize<Interval, NodaIsoExcludeNullsOriginalCaseResolver<byte>>(Encoding.UTF8.GetBytes(json));
            Assert.Equal(expectedInterval, interval);
        }

        [Theory]
        [InlineData("\"2012-01-02T03:04:05Z2013-06-07T08:09:10Z\"")]
        public void InvalidJson(string json)
        {
            AssertInvalidJson<Interval, NodaIsoExcludeNullsOriginalCaseResolver<char>>(json);
        }

        [Fact]
        public void Serialize_InObject()
        {
            var startInstant = Instant.FromUtc(2012, 1, 2, 3, 4, 5);
            var endInstant = Instant.FromUtc(2013, 6, 7, 8, 9, 10);
            var interval = new Interval(startInstant, endInstant);

            var testObject = new TestObject { Interval = interval };

            var json = JsonSerializer.Generic.Utf16.Serialize<TestObject, NodaIsoExcludeNullsOriginalCaseResolver<char>>(testObject);
            var jsonBytes = JsonSerializer.Generic.Utf8.Serialize<TestObject, NodaIsoExcludeNullsOriginalCaseResolver<byte>>(testObject);

            var expectedJson = "{\"Interval\":\"2012-01-02T03:04:05Z/2013-06-07T08:09:10Z\"}";
            Assert.Equal(expectedJson, json);
            Assert.Equal(expectedJson, Encoding.UTF8.GetString(jsonBytes));
        }

        [Fact]
        public void Deserialize_InObject()
        {
            var json = "{\"Interval\":\"2012-01-02T03:04:05Z/2013-06-07T08:09:10Z\"}";

            var startInstant = Instant.FromUtc(2012, 1, 2, 3, 4, 5);
            var endInstant = Instant.FromUtc(2013, 6, 7, 8, 9, 10);
            var expectedInterval = new Interval(startInstant, endInstant);

            var testObject = JsonSerializer.Generic.Utf16.Deserialize<TestObject, NodaIsoExcludeNullsOriginalCaseResolver<char>>(json);
            Assert.Equal(expectedInterval, testObject.Interval);

            testObject = JsonSerializer.Generic.Utf8.Deserialize<TestObject, NodaIsoExcludeNullsOriginalCaseResolver<byte>>(Encoding.UTF8.GetBytes(json));
            Assert.Equal(expectedInterval, testObject.Interval);
        }

        public class TestObject
        {
            public Interval Interval { get; set; }
        }
    }
}
