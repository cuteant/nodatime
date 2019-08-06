namespace NodaTime.Serialization.JsonSpan.Tests
{
    using System.Text;
    using NodaTime.TimeZones;
    using SpanJson;
    using SpanJson.Internal;
    using Xunit;

    public class NodaDateTimeZoneFormatterTest
    {
        [Fact]
        public void Serialize()
        {
            var dateTimeZone = DateTimeZoneProviders.Tzdb["America/Los_Angeles"];
            var json = JsonSerializer.Generic.Utf16.Serialize<DateTimeZone, NodaExcludeNullsOriginalCaseResolver<char>>(dateTimeZone);
            var jsonBytes = JsonSerializer.Generic.Utf8.Serialize<DateTimeZone, NodaExcludeNullsOriginalCaseResolver<byte>>(dateTimeZone);
            var expectedJson = "\"America/Los_Angeles\"";
            Assert.Equal(expectedJson, json);
            Assert.Equal(expectedJson, Encoding.UTF8.GetString(jsonBytes));
        }

        [Fact]
        public void Deserialize()
        {
            var json = "\"America/Los_Angeles\"";
            var expectedDateTimeZone = DateTimeZoneProviders.Tzdb["America/Los_Angeles"];
            var dateTimeZone = JsonSerializer.Generic.Utf16.Deserialize<DateTimeZone, NodaExcludeNullsOriginalCaseResolver<char>>(json);
            Assert.Equal(expectedDateTimeZone, dateTimeZone);
            dateTimeZone = JsonSerializer.Generic.Utf8.Deserialize<DateTimeZone, NodaExcludeNullsOriginalCaseResolver<byte>>(TextEncodings.UTF8NoBOM.GetBytes(json));
            Assert.Equal(expectedDateTimeZone, dateTimeZone);
        }

        [Fact]
        public void SerializeDeserializeUtf16()
        {
            var expectedDateTimeZone = new DateTimeZoneWrapper { Id = 101, Zone = DateTimeZoneProviders.Tzdb["America/Los_Angeles"] };

            var json = JsonSerializer.Generic.Utf16.Serialize<DateTimeZoneWrapper, NodaExcludeNullsOriginalCaseResolver<char>>(expectedDateTimeZone);
            var dateTimeZone = JsonSerializer.Generic.Utf16.Deserialize<DateTimeZoneWrapper, NodaExcludeNullsOriginalCaseResolver<char>>(json);
            Assert.Equal(expectedDateTimeZone.Id, dateTimeZone.Id);
            Assert.Equal(expectedDateTimeZone.Zone, dateTimeZone.Zone);
        }

        [Fact]
        public void SerializeDeserializeUtf8()
        {
            var expectedDateTimeZone = new DateTimeZoneWrapper { Id = 101, Zone = DateTimeZoneProviders.Tzdb["America/Los_Angeles"] };

            var json = JsonSerializer.Generic.Utf8.Serialize<DateTimeZoneWrapper, NodaExcludeNullsOriginalCaseResolver<byte>>(expectedDateTimeZone);
            var dateTimeZone = JsonSerializer.Generic.Utf8.Deserialize<DateTimeZoneWrapper, NodaExcludeNullsOriginalCaseResolver<byte>>(json);
            Assert.Equal(expectedDateTimeZone.Id, dateTimeZone.Id);
            Assert.Equal(expectedDateTimeZone.Zone, dateTimeZone.Zone);
        }

        [Fact]
        public void Deserialize_TimeZoneNotFound()
        {
            var json = "\"America/DOES_NOT_EXIST\"";
            Assert.Throws<DateTimeZoneNotFoundException>(() => JsonSerializer.Generic.Utf16.Deserialize<DateTimeZone, NodaExcludeNullsOriginalCaseResolver<char>>(json));
            Assert.Throws<DateTimeZoneNotFoundException>(() => JsonSerializer.Generic.Utf8.Deserialize<DateTimeZone, NodaExcludeNullsOriginalCaseResolver<byte>>(Encoding.UTF8.GetBytes(json)));
        }

        public class DateTimeZoneWrapper
        {
            public int Id { get; set; }
            public DateTimeZone Zone { get; set; }
        }
    }
}
