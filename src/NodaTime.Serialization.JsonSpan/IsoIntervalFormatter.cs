namespace NodaTime.Serialization.JsonSpan
{
    using System.Runtime.CompilerServices;
    using NodaTime.Text;
    using SpanJson;

    /// <summary>SpanJson formatter for <see cref="Interval"/> using a compound representation. The start and
    /// end aspects of the interval are represented with separate properties, each parsed and formatted
    /// by the <see cref="Instant"/> converter for the serializer provided.</summary>   
    public sealed class IsoIntervalFormatter : ICustomJsonFormatter<Interval>
    {
        public static readonly IsoIntervalFormatter Default = new IsoIntervalFormatter();

        public object Arguments { get; set; }

        public Interval Deserialize(ref JsonReader<byte> reader, IJsonFormatterResolver<byte> resolver)
        {
            string text = reader.ReadUtf8String();
            return ParseAsIso(text);
        }

        public Interval Deserialize(ref JsonReader<char> reader, IJsonFormatterResolver<char> resolver)
        {
            string text = reader.ReadUtf16String();
            return ParseAsIso(text);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Interval ParseAsIso(string text)
        {
            int slash = text.IndexOf('/');
            if (slash == -1)
            {
                ThrowHelper.ThrowInvalidNodaDataException_IsoFormattedIntervalSlash();
            }

            string startText = text.Substring(0, slash);
            string endText = text.Substring(slash + 1);
            var pattern = InstantPattern.ExtendedIso;
            var start = startText == "" ? (Instant?)null : pattern.Parse(startText).Value;
            var end = endText == "" ? (Instant?)null : pattern.Parse(endText).Value;

            return new Interval(start, end);
        }

        public void Serialize(ref JsonWriter<byte> writer, Interval value, IJsonFormatterResolver<byte> resolver)
        {
            writer.WriteUtf8String(FormatAsIso(value));
        }

        public void Serialize(ref JsonWriter<char> writer, Interval value, IJsonFormatterResolver<char> resolver)
        {
            writer.WriteUtf16String(FormatAsIso(value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static string FormatAsIso(in Interval value)
        {
            var pattern = InstantPattern.ExtendedIso;
            return $"{(value.HasStart ? pattern.Format(value.Start) : "")}/{(value.HasEnd ? pattern.Format(value.End) : "")}";
        }
    }
}
