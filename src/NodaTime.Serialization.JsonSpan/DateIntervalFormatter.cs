namespace NodaTime.Serialization.JsonSpan
{
    using System;
    using SpanJson;

    /// <summary>SpanJson formatter for <see cref="DateInterval"/> using a compound representation. The start and
    /// end aspects of the date interval are represented with separate properties, each parsed and formatted
    /// by the <see cref="LocalDate"/> converter for the serializer provided.</summary>   
    public sealed class DateIntervalFormatter : ICustomJsonFormatter<DateInterval>
    {
        public static readonly DateIntervalFormatter Default = new DateIntervalFormatter();

        public object Arguments { get; set; }

        public DateInterval Deserialize(ref JsonReader<byte> reader, IJsonFormatterResolver<byte> resolver)
        {
            if (reader.ReadUtf8IsNull()) { return null; }

            LocalDate? startLocalDate = null;
            LocalDate? endLocalDate = null;

            reader.ReadUtf8BeginObjectOrThrow();

            var startEncodedText = resolver.GetEncodedPropertyName(nameof(DateInterval.Start));
            var endEncodedText = resolver.GetEncodedPropertyName(nameof(DateInterval.End));

            var count = 0;
            while (!reader.TryReadUtf8IsEndObjectOrValueSeparator(ref count))
            {
                var propertyName = reader.ReadUtf8VerbatimNameSpan(out _);

                if (propertyName.SequenceEqual(startEncodedText.EncodedUtf8Bytes))
                {
                    startLocalDate = LocalDateFormatter.Default.Deserialize(ref reader, resolver);
                }

                if (propertyName.SequenceEqual(endEncodedText.EncodedUtf8Bytes))
                {
                    endLocalDate = LocalDateFormatter.Default.Deserialize(ref reader, resolver);
                }
            }

            if (!startLocalDate.HasValue)
            {
                ThrowHelper.ThrowInvalidNodaDataException_DateIntervalStartDate();
            }

            if (!endLocalDate.HasValue)
            {
                ThrowHelper.ThrowInvalidNodaDataException_DateIntervalEndDate();
            }

            return new DateInterval(startLocalDate.Value, endLocalDate.Value);
        }

        public DateInterval Deserialize(ref JsonReader<char> reader, IJsonFormatterResolver<char> resolver)
        {
            if (reader.ReadUtf16IsNull()) { return null; }

            LocalDate? startLocalDate = null;
            LocalDate? endLocalDate = null;

            reader.ReadUtf16BeginObjectOrThrow();

            var startEncodedText = resolver.GetEncodedPropertyName(nameof(DateInterval.Start));
            var endEncodedText = resolver.GetEncodedPropertyName(nameof(DateInterval.End));

            var count = 0;
            while (!reader.TryReadUtf16IsEndObjectOrValueSeparator(ref count))
            {
                var propertyName = reader.ReadUtf16VerbatimNameSpan();

                if (propertyName.SequenceEqual(startEncodedText.ToString().AsSpan()))
                {
                    startLocalDate = LocalDateFormatter.Default.Deserialize(ref reader, resolver);
                }

                if (propertyName.SequenceEqual(endEncodedText.ToString().AsSpan()))
                {
                    endLocalDate = LocalDateFormatter.Default.Deserialize(ref reader, resolver);
                }
            }

            if (!startLocalDate.HasValue)
            {
                ThrowHelper.ThrowInvalidNodaDataException_DateIntervalStartDate();
            }

            if (!endLocalDate.HasValue)
            {
                ThrowHelper.ThrowInvalidNodaDataException_DateIntervalEndDate();
            }

            return new DateInterval(startLocalDate.Value, endLocalDate.Value);
        }

        public void Serialize(ref JsonWriter<byte> writer, DateInterval value, IJsonFormatterResolver<byte> resolver)
        {
            if (value == null) { writer.WriteUtf8Null(); return; }

            writer.WriteUtf8BeginObject();

            writer.WriteUtf8Name(resolver.GetEncodedPropertyName(nameof(DateInterval.Start)));
            LocalDateFormatter.Default.Serialize(ref writer, value.Start, resolver);

            writer.WriteUtf8ValueSeparator();

            writer.WriteUtf8Name(resolver.GetEncodedPropertyName(nameof(DateInterval.End)));
            LocalDateFormatter.Default.Serialize(ref writer, value.End, resolver);

            writer.WriteUtf8EndObject();
        }

        public void Serialize(ref JsonWriter<char> writer, DateInterval value, IJsonFormatterResolver<char> resolver)
        {
            if (value == null) { writer.WriteUtf16Null(); return; }

            writer.WriteUtf16BeginObject();

            writer.WriteUtf16Name(resolver.GetEncodedPropertyName(nameof(DateInterval.Start)));
            LocalDateFormatter.Default.Serialize(ref writer, value.Start, resolver);

            writer.WriteUtf16ValueSeparator();

            writer.WriteUtf16Name(resolver.GetEncodedPropertyName(nameof(DateInterval.End)));
            LocalDateFormatter.Default.Serialize(ref writer, value.End, resolver);

            writer.WriteUtf16EndObject();
        }
    }
}
