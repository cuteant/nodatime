namespace NodaTime.Serialization.JsonSpan.Tests
{
    using System.Text;
    using SpanJson;
    using Xunit;

    public class NodaIntervalConverterTest
    {
        [Fact]
        public void RoundTrip()
        {
            var startInstant = Instant.FromUtc(2012, 1, 2, 3, 4, 5) + Duration.FromMilliseconds(670);
            var endInstant = Instant.FromUtc(2013, 6, 7, 8, 9, 10) + Duration.FromNanoseconds(123456789);
            var interval = new Interval(startInstant, endInstant);
            TestHelper.AssertConversionsUtf16<Interval, NodaExcludeNullsOriginalCaseResolver<char>>(interval, "{\"Start\":\"2012-01-02T03:04:05.67Z\",\"End\":\"2013-06-07T08:09:10.123456789Z\"}");
            TestHelper.AssertConversionsUtf8<Interval, NodaExcludeNullsOriginalCaseResolver<byte>>(interval, "{\"Start\":\"2012-01-02T03:04:05.67Z\",\"End\":\"2013-06-07T08:09:10.123456789Z\"}");
        }

        [Fact]
        public void RoundTrip_Infinite()
        {
            var instant = Instant.FromUtc(2013, 6, 7, 8, 9, 10) + Duration.FromNanoseconds(123456789);
            TestHelper.AssertConversionsUtf16<Interval, NodaExcludeNullsOriginalCaseResolver<char>>(new Interval(null, instant), "{\"End\":\"2013-06-07T08:09:10.123456789Z\"}");
            TestHelper.AssertConversionsUtf16<Interval, NodaExcludeNullsOriginalCaseResolver<char>>(new Interval(instant, null), "{\"Start\":\"2013-06-07T08:09:10.123456789Z\"}");
            TestHelper.AssertConversionsUtf16<Interval, NodaExcludeNullsOriginalCaseResolver<char>>(new Interval(null, null), "{}");
            TestHelper.AssertConversionsUtf8<Interval, NodaExcludeNullsOriginalCaseResolver<byte>>(new Interval(null, instant), "{\"End\":\"2013-06-07T08:09:10.123456789Z\"}");
            TestHelper.AssertConversionsUtf8<Interval, NodaExcludeNullsOriginalCaseResolver<byte>>(new Interval(instant, null), "{\"Start\":\"2013-06-07T08:09:10.123456789Z\"}");
            TestHelper.AssertConversionsUtf8<Interval, NodaExcludeNullsOriginalCaseResolver<byte>>(new Interval(null, null), "{}");
        }

        [Fact]
        public void Serialize_InObject()
        {
            var startInstant = Instant.FromUtc(2012, 1, 2, 3, 4, 5);
            var endInstant = Instant.FromUtc(2013, 6, 7, 8, 9, 10);
            var interval = new Interval(startInstant, endInstant);

            var testObject = new TestObject { Interval = interval };

            var json = JsonSerializer.Generic.Utf16.Serialize<TestObject, NodaExcludeNullsOriginalCaseResolver<char>>(testObject);
            var jsonBytes = JsonSerializer.Generic.Utf8.Serialize<TestObject, NodaExcludeNullsOriginalCaseResolver<byte>>(testObject);

            var expectedJson = "{\"Interval\":{\"Start\":\"2012-01-02T03:04:05Z\",\"End\":\"2013-06-07T08:09:10Z\"}}";

            Assert.Equal(expectedJson, json);
            Assert.Equal(expectedJson, Encoding.UTF8.GetString(jsonBytes));
        }

        [Fact]
        public void Serialize_InObject_CamelCase()
        {
            var startInstant = Instant.FromUtc(2012, 1, 2, 3, 4, 5);
            var endInstant = Instant.FromUtc(2013, 6, 7, 8, 9, 10);
            var interval = new Interval(startInstant, endInstant);

            var testObject = new TestObject { Interval = interval };

            var json = JsonSerializer.Generic.Utf16.Serialize<TestObject, NodaExcludeNullsCamelCaseResolver<char>>(testObject);
            var jsonBytes = JsonSerializer.Generic.Utf8.Serialize<TestObject, NodaExcludeNullsCamelCaseResolver<byte>>(testObject);

            var expectedJson = "{\"interval\":{\"start\":\"2012-01-02T03:04:05Z\",\"end\":\"2013-06-07T08:09:10Z\"}}";

            Assert.Equal(expectedJson, json);
            Assert.Equal(expectedJson, Encoding.UTF8.GetString(jsonBytes));
        }

        [Fact]
        public void Deserialize_InObject()
        {
            var json = "{\"Interval\":{\"Start\":\"2012-01-02T03:04:05Z\",\"End\":\"2013-06-07T08:09:10Z\"}}";

            var startInstant = Instant.FromUtc(2012, 1, 2, 3, 4, 5);
            var endInstant = Instant.FromUtc(2013, 6, 7, 8, 9, 10);
            var expectedInterval = new Interval(startInstant, endInstant);

            var testObject = JsonSerializer.Generic.Utf16.Deserialize<TestObject, NodaExcludeNullsOriginalCaseResolver<char>>(json);
            Assert.Equal(expectedInterval, testObject.Interval);

            testObject = JsonSerializer.Generic.Utf8.Deserialize<TestObject, NodaExcludeNullsOriginalCaseResolver<byte>>(Encoding.UTF8.GetBytes(json));
            Assert.Equal(expectedInterval, testObject.Interval);
        }

        [Fact]
        public void Deserialize_InObject_CamelCase()
        {
            var json = "{\"interval\":{\"start\":\"2012-01-02T03:04:05Z\",\"end\":\"2013-06-07T08:09:10Z\"}}";

            var startInstant = Instant.FromUtc(2012, 1, 2, 3, 4, 5);
            var endInstant = Instant.FromUtc(2013, 6, 7, 8, 9, 10);
            var expectedInterval = new Interval(startInstant, endInstant);

            var testObject = JsonSerializer.Generic.Utf16.Deserialize<TestObject, NodaExcludeNullsCamelCaseResolver<char>>(json);
            Assert.Equal(expectedInterval, testObject.Interval);

            testObject = JsonSerializer.Generic.Utf8.Deserialize<TestObject, NodaExcludeNullsCamelCaseResolver<byte>>(Encoding.UTF8.GetBytes(json));
            Assert.Equal(expectedInterval, testObject.Interval);
        }

        public class TestObject
        {
            public Interval Interval { get; set; }
        }
    }
}
