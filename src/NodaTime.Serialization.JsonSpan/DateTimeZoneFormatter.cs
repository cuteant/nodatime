namespace NodaTime.Serialization.JsonSpan
{
    using SpanJson;
    using SpanJson.Internal;

    public sealed class TzdbDateTimeZoneFormatter<T> : DateTimeZoneFormatter<T> where T : DateTimeZone
    {
        public new static readonly TzdbDateTimeZoneFormatter<T> Default = new TzdbDateTimeZoneFormatter<T>();

        public TzdbDateTimeZoneFormatter() : base(DateTimeZoneProviders.Tzdb) { }
    }

    public sealed class BclDateTimeZoneFormatter<T> : DateTimeZoneFormatter<T> where T : DateTimeZone
    {
        public new static readonly BclDateTimeZoneFormatter<T> Default = new BclDateTimeZoneFormatter<T>();

        public BclDateTimeZoneFormatter() : base(DateTimeZoneProviders.Bcl) { }
    }

    /// <summary>SpanJson formatter for <see cref="DateTimeZone"/>.</summary>
    public class DateTimeZoneFormatter<T> : ICustomJsonFormatter<T> where T : DateTimeZone
    {
        public static readonly DateTimeZoneFormatter<T> Default = new DateTimeZoneFormatter<T>();

        private readonly IDateTimeZoneProvider _provider;

        public DateTimeZoneFormatter() : this(DateTimeZoneProviders.Serialization) { }

        /// <param name="provider">Provides the <see cref="DateTimeZone"/> that corresponds to each time zone ID in the JSON string.</param>
        public DateTimeZoneFormatter(IDateTimeZoneProvider provider)
        {
            this._provider = provider;
        }

        public object Arguments { get; set; }

        public T Deserialize(ref JsonReader<byte> reader, IJsonFormatterResolver<byte> resolver)
        {
            if (reader.ReadUtf8IsNull()) { return null; }

            var escapedVal = reader.ReadUtf8VerbatimStringSpan(out int idx);
            var timeZoneId = EscapingHelper.GetUnescapedTextFromUtf8WithCache(escapedVal, idx);
            return _provider[timeZoneId] as T;
        }

        public T Deserialize(ref JsonReader<char> reader, IJsonFormatterResolver<char> resolver)
        {
            if (reader.ReadUtf16IsNull()) { return null; }

            var timeZoneId = reader.ReadUtf16String();
            return _provider[timeZoneId] as T;
        }

        public void Serialize(ref JsonWriter<byte> writer, T value, IJsonFormatterResolver<byte> resolver)
        {
            if (value == null) { writer.WriteUtf8Null(); return; }

            var encodedVal = EscapingHelper.GetEncodedText(value.Id, resolver.EscapeHandling);
            writer.WriteUtf8String(encodedVal);
        }

        public void Serialize(ref JsonWriter<char> writer, T value, IJsonFormatterResolver<char> resolver)
        {
            if (value == null) { writer.WriteUtf16Null(); return; }

            var encodedVal = EscapingHelper.GetEncodedText(value.Id, resolver.EscapeHandling);
            writer.WriteUtf16String(encodedVal);
        }
    }
}
