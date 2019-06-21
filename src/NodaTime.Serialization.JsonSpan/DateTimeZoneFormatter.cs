namespace NodaTime.Serialization.JsonSpan
{
    using SpanJson;
    using SpanJson.Internal;

    public sealed class TzdbDateTimeZoneFormatter : DateTimeZoneFormatter
    {
        public new static readonly TzdbDateTimeZoneFormatter Default = new TzdbDateTimeZoneFormatter();

        private TzdbDateTimeZoneFormatter() : base(DateTimeZoneProviders.Tzdb) { }
    }

    public sealed class BclDateTimeZoneFormatter : DateTimeZoneFormatter
    {
        public new static readonly BclDateTimeZoneFormatter Default = new BclDateTimeZoneFormatter();

        private BclDateTimeZoneFormatter() : base(DateTimeZoneProviders.Bcl) { }
    }

    /// <summary>SpanJson formatter for <see cref="DateTimeZone"/>.</summary>
    public class DateTimeZoneFormatter : ICustomJsonFormatter<DateTimeZone>
    {
        public static readonly DateTimeZoneFormatter Default = new DateTimeZoneFormatter();

        private readonly IDateTimeZoneProvider _provider;

        private DateTimeZoneFormatter() : this(DateTimeZoneProviders.Serialization) { }

        /// <param name="provider">Provides the <see cref="DateTimeZone"/> that corresponds to each time zone ID in the JSON string.</param>
        public DateTimeZoneFormatter(IDateTimeZoneProvider provider)
        {
            this._provider = provider;
        }

        public object Arguments { get; set; }

        public DateTimeZone Deserialize(ref JsonReader<byte> reader, IJsonFormatterResolver<byte> resolver)
        {
            if (reader.ReadUtf8IsNull()) { return null; }

            var escapedVal = reader.ReadUtf8VerbatimStringSpan(out int idx);
            var timeZoneId = EscapingHelper.GetUnescapedTextFromUtf8WithCache(escapedVal, idx);
            return _provider[timeZoneId];
        }

        public DateTimeZone Deserialize(ref JsonReader<char> reader, IJsonFormatterResolver<char> resolver)
        {
            if (reader.ReadUtf16IsNull()) { return null; }

            var timeZoneId = reader.ReadUtf16String();
            return _provider[timeZoneId];
        }

        public void Serialize(ref JsonWriter<byte> writer, DateTimeZone value, IJsonFormatterResolver<byte> resolver)
        {
            if (value == null) { writer.WriteUtf8Null(); return; }

            var encodedVal = EscapingHelper.GetEncodedText(value.Id, resolver.StringEscapeHandling);
            writer.WriteUtf8String(encodedVal);
        }

        public void Serialize(ref JsonWriter<char> writer, DateTimeZone value, IJsonFormatterResolver<char> resolver)
        {
            if (value == null) { writer.WriteUtf16Null(); return; }

            var encodedVal = EscapingHelper.GetEncodedText(value.Id, resolver.StringEscapeHandling);
            writer.WriteUtf16String(encodedVal);
        }
    }
}
