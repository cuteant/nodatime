namespace NodaTime.Serialization.JsonSpan
{
    using System;
    using SpanJson;

    /// <summary>SpanJson formatter for <see cref="Interval"/> using a compound representation. The start and
    /// end aspects of the interval are represented with separate properties, each parsed and formatted
    /// by the <see cref="Instant"/> converter for the serializer provided.</summary>   
    public sealed class IntervalFormatter : ICustomJsonFormatter<Interval>
    {
        public static readonly IntervalFormatter Default = new IntervalFormatter();

        public object Arguments { get; set; }

        public Interval Deserialize(ref JsonReader<byte> reader, IJsonFormatterResolver<byte> resolver)
        {
            Instant? startInstant = null;
            Instant? endInstant = null;

            reader.ReadUtf8BeginObjectOrThrow();

            var startEncodedText = resolver.GetEncodedPropertyName(nameof(Interval.Start));
            var endEncodedText = resolver.GetEncodedPropertyName(nameof(Interval.End));

            var count = 0;
            while (!reader.TryReadUtf8IsEndObjectOrValueSeparator(ref count))
            {
                var propertyName = reader.ReadUtf8VerbatimNameSpan(out _);

                if (propertyName.SequenceEqual(startEncodedText.EncodedUtf8Bytes))
                {
                    startInstant = InstantFormatter.Default.Deserialize(ref reader, resolver);
                }

                if (propertyName.SequenceEqual(endEncodedText.EncodedUtf8Bytes))
                {
                    endInstant = InstantFormatter.Default.Deserialize(ref reader, resolver);
                }
            }

            return new Interval(startInstant, endInstant);
        }

        public Interval Deserialize(ref JsonReader<char> reader, IJsonFormatterResolver<char> resolver)
        {
            Instant? startInstant = null;
            Instant? endInstant = null;

            reader.ReadUtf16BeginObjectOrThrow();

            var startEncodedText = resolver.GetEncodedPropertyName(nameof(Interval.Start));
            var endEncodedText = resolver.GetEncodedPropertyName(nameof(Interval.End));

            var count = 0;
            while (!reader.TryReadUtf16IsEndObjectOrValueSeparator(ref count))
            {
                var propertyName = reader.ReadUtf16VerbatimNameSpan();

                if (propertyName.SequenceEqual(startEncodedText.ToString().AsSpan()))
                {
                    startInstant = InstantFormatter.Default.Deserialize(ref reader, resolver);
                }

                if (propertyName.SequenceEqual(endEncodedText.ToString().AsSpan()))
                {
                    endInstant = InstantFormatter.Default.Deserialize(ref reader, resolver);
                }
            }

            return new Interval(startInstant, endInstant);
        }

        public void Serialize(ref JsonWriter<byte> writer, Interval value, IJsonFormatterResolver<byte> resolver)
        {
            writer.WriteUtf8BeginObject();

            var hasEnd = value.HasEnd;
            if (value.HasStart)
            {
                writer.WriteUtf8Name(resolver.GetEncodedPropertyName(nameof(Interval.Start)));
                InstantFormatter.Default.Serialize(ref writer, value.Start, resolver);
                if (hasEnd) { writer.WriteUtf8ValueSeparator(); }
            }

            if (hasEnd)
            {
                writer.WriteUtf8Name(resolver.GetEncodedPropertyName(nameof(Interval.End)));
                InstantFormatter.Default.Serialize(ref writer, value.End, resolver);
            }

            writer.WriteUtf8EndObject();
        }

        public void Serialize(ref JsonWriter<char> writer, Interval value, IJsonFormatterResolver<char> resolver)
        {
            writer.WriteUtf16BeginObject();

            var hasEnd = value.HasEnd;
            if (value.HasStart)
            {
                writer.WriteUtf16Name(resolver.GetEncodedPropertyName(nameof(Interval.Start)));
                InstantFormatter.Default.Serialize(ref writer, value.Start, resolver);
                if (hasEnd) { writer.WriteUtf16ValueSeparator(); }
            }

            if (hasEnd)
            {
                writer.WriteUtf16Name(resolver.GetEncodedPropertyName(nameof(Interval.End)));
                InstantFormatter.Default.Serialize(ref writer, value.End, resolver);
            }

            writer.WriteUtf16EndObject();
        }
    }
}
