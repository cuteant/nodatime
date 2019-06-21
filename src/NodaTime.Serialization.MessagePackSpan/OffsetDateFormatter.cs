namespace NodaTime.Serialization.MessagePackSpan
{
    using MessagePack;
    using MessagePack.Formatters;

    public sealed class OffsetDateFormatter : IMessagePackFormatter<OffsetDate>
    {
        public static readonly OffsetDateFormatter Instance = new OffsetDateFormatter();

        const int c_count = 2;

        public OffsetDate Deserialize(ref MessagePackReader reader, IFormatterResolver formatterResolver)
        {
            var count = reader.ReadArrayHeader();
            if (count != c_count) { ThrowHelper.ThrowInvalidOperationException_InvalidArrayCount(); }

            var dt = LocalDateFormatter.Instance.Deserialize(ref reader, formatterResolver);
            var off = OffsetFormatter.Instance.Deserialize(ref reader, formatterResolver);

            return new OffsetDate(dt, off);
        }

        public void Serialize(ref MessagePackWriter writer, ref int idx, OffsetDate value, IFormatterResolver formatterResolver)
        {
            writer.WriteFixedArrayHeaderUnsafe(c_count, ref idx);

            LocalDateFormatter.Instance.Serialize(ref writer, ref idx, value.Date, formatterResolver);
            OffsetFormatter.Instance.Serialize(ref writer, ref idx, value.Offset, formatterResolver);
        }
    }
}
