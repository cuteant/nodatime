namespace NodaTime.Serialization.JsonSpan.Tests
{
    using System;
    using System.Text;
    using SpanJson;
    using Xunit;

    public class NodaInstantConverterTest
    {
        [Fact]
        public void Serialize_NonNullableType()
        {
            var instant = Instant.FromUtc(2012, 1, 2, 3, 4, 5);
            var json = JsonSerializer.Generic.Utf16.Serialize<Instant, NodaExcludeNullsOriginalCaseResolver<char>>(instant);
            var jsonBytes = JsonSerializer.Generic.Utf8.Serialize<Instant, NodaExcludeNullsOriginalCaseResolver<byte>>(instant);
            var expectedJson = "\"2012-01-02T03:04:05Z\"";
            Assert.Equal(expectedJson, json);
            Assert.Equal(expectedJson, Encoding.UTF8.GetString(jsonBytes));
        }

        [Fact]
        public void Serialize_NullableType_NonNullValue()
        {
            Instant? instant = Instant.FromUtc(2012, 1, 2, 3, 4, 5);
            var json = JsonSerializer.Generic.Utf16.Serialize<Instant?, NodaExcludeNullsOriginalCaseResolver<char>>(instant);
            var jsonBytes = JsonSerializer.Generic.Utf8.Serialize<Instant?, NodaExcludeNullsOriginalCaseResolver<byte>>(instant);
            var expectedJson = "\"2012-01-02T03:04:05Z\"";
            Assert.Equal(expectedJson, json);
            Assert.Equal(expectedJson, Encoding.UTF8.GetString(jsonBytes));
        }

        [Fact]
        public void Serialize_NullableType_NullValue()
        {
            Instant? instant = null;
            var json = JsonSerializer.Generic.Utf16.Serialize<Instant?, NodaIncludeNullsOriginalCaseResolver<char>>(instant);
            var jsonBytes = JsonSerializer.Generic.Utf8.Serialize<Instant?, NodaIncludeNullsOriginalCaseResolver<byte>>(instant);
            var expectedJson = "null";
            Assert.Equal(expectedJson, json);
            Assert.Equal(expectedJson, Encoding.UTF8.GetString(jsonBytes));
        }

        [Fact]
        public void Deserialize_ToNonNullableType()
        {
            var json = "\"2012-01-02T03:04:05Z\"";
            var instant = JsonSerializer.Generic.Utf16.Deserialize<Instant, NodaExcludeNullsOriginalCaseResolver<char>>(json);
            var expectedInstant = Instant.FromUtc(2012, 1, 2, 3, 4, 5);
            Assert.Equal(expectedInstant, instant);

            instant = JsonSerializer.Generic.Utf8.Deserialize<Instant, NodaExcludeNullsOriginalCaseResolver<byte>>(Encoding.UTF8.GetBytes(json));
            Assert.Equal(expectedInstant, instant);
        }

        [Fact]
        public void Deserialize_ToNullableType_NonNullValue()
        {
            var json = "\"2012-01-02T03:04:05Z\"";
            var instant = JsonSerializer.Generic.Utf16.Deserialize<Instant?, NodaExcludeNullsOriginalCaseResolver<char>>(json);
            Instant? expectedInstant = Instant.FromUtc(2012, 1, 2, 3, 4, 5);
            Assert.Equal(expectedInstant, instant);

            instant = JsonSerializer.Generic.Utf8.Deserialize<Instant?, NodaExcludeNullsOriginalCaseResolver<byte>>(Encoding.UTF8.GetBytes(json));
            Assert.Equal(expectedInstant, instant);
        }

        [Fact]
        public void Deserialize_ToNullableType_NullValue()
        {
            var json = "null";
            var instant = JsonSerializer.Generic.Utf16.Deserialize<Instant?, NodaIncludeNullsOriginalCaseResolver<char>>(json);
            Assert.Null(instant);
            instant = JsonSerializer.Generic.Utf8.Deserialize<Instant?, NodaIncludeNullsOriginalCaseResolver<byte>>(Encoding.UTF8.GetBytes(json));
            Assert.Null(instant);
        }

        [Fact]
        public void Serialize_EquivalentToIsoDateTimeConverter()
        {
            var dateTime = new DateTime(2012, 1, 2, 3, 4, 5, DateTimeKind.Utc);
            var instant = Instant.FromDateTimeUtc(dateTime);
            var jsonDateTime = JsonSerializer.Generic.Utf16.Serialize(dateTime);
            var jsonInstant = JsonSerializer.Generic.Utf16.Serialize<Instant, NodaIncludeNullsOriginalCaseResolver<char>>(instant);
            Assert.Equal(jsonDateTime, jsonInstant);

            var jsonDateTimeBytes = JsonSerializer.Generic.Utf8.Serialize(dateTime);
            var jsonInstantBytes = JsonSerializer.Generic.Utf8.Serialize<Instant, NodaIncludeNullsOriginalCaseResolver<byte>>(instant);
            Assert.True(jsonDateTimeBytes.AsSpan().SequenceEqual(jsonInstantBytes));
        }
    }
}
