namespace NodaTime.Serialization.MessagePackSpan
{
    using MessagePack;
    using MessagePack.Formatters;

    public sealed class DateIntervalFormatter : IMessagePackFormatter<DateInterval>
    {
        public static readonly DateIntervalFormatter Instance = new DateIntervalFormatter();

        private DateIntervalFormatter() { }

        const int c_count = 2;

        public DateInterval Deserialize(ref MessagePackReader reader, IFormatterResolver formatterResolver)
        {
            if (reader.IsNil()) { return null; }

            var count = reader.ReadArrayHeader();
            if (count != c_count) { ThrowHelper.ThrowInvalidOperationException_InvalidArrayCount(); }

            var dateFormatter = LocalDateFormatter.Instance;
            var start = dateFormatter.Deserialize(ref reader, formatterResolver);
            var end = dateFormatter.Deserialize(ref reader, formatterResolver);
            return new DateInterval(start, end);
        }

        public void Serialize(ref MessagePackWriter writer, ref int idx, DateInterval value, IFormatterResolver formatterResolver)
        {
            if (null == value) { writer.WriteNil(ref idx); return; }

            writer.WriteFixedArrayHeaderUnsafe(c_count, ref idx);

            var dateFormatter = LocalDateFormatter.Instance;
            dateFormatter.Serialize(ref writer, ref idx, value.Start, formatterResolver);
            dateFormatter.Serialize(ref writer, ref idx, value.End, formatterResolver);
        }
    }
}
