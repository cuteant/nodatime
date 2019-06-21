using System.Text;
using NodaTime.Utility;
using SpanJson;
using Xunit;

namespace NodaTime.Serialization.JsonSpan.Tests
{
    internal static class TestHelper
    {
        internal static void AssertConversionsUtf16<T, TResolver>(T value, string expectedJson)
            where TResolver : IJsonFormatterResolver<char, TResolver>, new()
        {
            var actualJson = JsonSerializer.Generic.Utf16.Serialize<T, TResolver>(value);
            Assert.Equal(expectedJson, actualJson);

            var deserializedValue = JsonSerializer.Generic.Utf16.Deserialize<T, TResolver>(actualJson);
            Assert.Equal(value, deserializedValue);
        }

        internal static void AssertConversionsUtf8<T, TResolver>(T value, string expectedJson)
            where TResolver : IJsonFormatterResolver<byte, TResolver>, new()
        {
            var actualJson = JsonSerializer.Generic.Utf8.Serialize<T, TResolver>(value);
            Assert.Equal(expectedJson, Encoding.UTF8.GetString(actualJson));

            var deserializedValue = JsonSerializer.Generic.Utf8.Deserialize<T, TResolver>(actualJson);
            Assert.Equal(value, deserializedValue);
        }

        internal static void AssertInvalidJson<T, TResolver>(string json)
            where TResolver : IJsonFormatterResolver<char, TResolver>, new()
        {
            Assert.Throws<InvalidNodaDataException>(() => JsonSerializer.Generic.Utf16.Deserialize<T, TResolver>(json));
        }
    }
}
