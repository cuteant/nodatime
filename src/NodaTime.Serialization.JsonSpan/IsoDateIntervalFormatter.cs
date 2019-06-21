namespace NodaTime.Serialization.JsonSpan
{
    using System.Runtime.CompilerServices;
    using NodaTime.Text;
    using SpanJson;

    /// <summary>SpanJson formatter for <see cref="DateInterval"/>.</summary>   
    public sealed class IsoDateIntervalFormatter : ICustomJsonFormatter<DateInterval>
    {
        public static readonly IsoDateIntervalFormatter Default = new IsoDateIntervalFormatter();

        public object Arguments { get; set; }

        public DateInterval Deserialize(ref JsonReader<byte> reader, IJsonFormatterResolver<byte> resolver)
        {
            if (reader.ReadUtf8IsNull()) { return null; }

            string text = reader.ReadUtf8String();
            return ParseAsIso(text);
        }

        public DateInterval Deserialize(ref JsonReader<char> reader, IJsonFormatterResolver<char> resolver)
        {
            if (reader.ReadUtf16IsNull()) { return null; }

            string text = reader.ReadUtf16String();
            return ParseAsIso(text);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static DateInterval ParseAsIso(string text)
        {
            int slash = text.IndexOf('/');
            if (slash == -1)
            {
                ThrowHelper.ThrowInvalidNodaDataException_IsoFormattedDateIntervalSlash();
            }

            string startText = text.Substring(0, slash);
            if (startText == "")
            {
                ThrowHelper.ThrowInvalidNodaDataException_IsoFormattedDateIntervalStartDate();
            }

            string endText = text.Substring(slash + 1);
            if (endText == "")
            {
                ThrowHelper.ThrowInvalidNodaDataException_IsoFormattedDateIntervalEndDate();
            }

            var pattern = LocalDatePattern.Iso;
            var start = pattern.Parse(startText).Value;
            var end = pattern.Parse(endText).Value;

            return new DateInterval(start, end);
        }

        public void Serialize(ref JsonWriter<byte> writer, DateInterval value, IJsonFormatterResolver<byte> resolver)
        {
            if (value == null) { writer.WriteUtf8Null(); return; }

            writer.WriteUtf8String(FormatAsIso(value));
        }

        public void Serialize(ref JsonWriter<char> writer, DateInterval value, IJsonFormatterResolver<char> resolver)
        {
            if (value == null) { writer.WriteUtf16Null(); return; }

            writer.WriteUtf16String(FormatAsIso(value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static string FormatAsIso(DateInterval value)
        {
            var pattern = LocalDatePattern.Iso;
            return $"{pattern.Format(value.Start)}/{pattern.Format(value.End)}";
        }
    }
}
