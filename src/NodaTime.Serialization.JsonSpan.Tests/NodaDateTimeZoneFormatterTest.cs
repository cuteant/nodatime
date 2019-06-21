namespace NodaTime.Serialization.JsonSpan.Tests
{
    using System.Text;
    using NodaTime.TimeZones;
    using SpanJson;
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
            dateTimeZone = JsonSerializer.Generic.Utf8.Deserialize<DateTimeZone, NodaExcludeNullsOriginalCaseResolver<byte>>(Encoding.UTF8.GetBytes(json));
            Assert.Equal(expectedDateTimeZone, dateTimeZone);
        }

        [Fact]
        public void Deserialize_TimeZoneNotFound()
        {
            var json = "\"America/DOES_NOT_EXIST\"";
            Assert.Throws<DateTimeZoneNotFoundException>(() => JsonSerializer.Generic.Utf16.Deserialize<DateTimeZone, NodaExcludeNullsOriginalCaseResolver<char>>(json));
            Assert.Throws<DateTimeZoneNotFoundException>(() => JsonSerializer.Generic.Utf8.Deserialize<DateTimeZone, NodaExcludeNullsOriginalCaseResolver<byte>>(Encoding.UTF8.GetBytes(json)));
        }
    }
}
