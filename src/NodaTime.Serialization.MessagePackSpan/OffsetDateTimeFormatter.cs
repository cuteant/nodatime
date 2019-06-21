namespace NodaTime.Serialization.MessagePackSpan
{
    using MessagePack;
    using MessagePack.Formatters;

    public sealed class OffsetDateTimeFormatter : IMessagePackFormatter<OffsetDateTime>
    {
        public static readonly OffsetDateTimeFormatter Instance = new OffsetDateTimeFormatter();

        const int c_count = 2;

        public OffsetDateTime Deserialize(ref MessagePackReader reader, IFormatterResolver formatterResolver)
        {
            var count = reader.ReadArrayHeader();
            if (count != c_count) { ThrowHelper.ThrowInvalidOperationException_InvalidArrayCount(); }

            var dt = LocalDateTimeFormatter.Instance.Deserialize(ref reader, formatterResolver);
            var off = OffsetFormatter.Instance.Deserialize(ref reader, formatterResolver);

            return new OffsetDateTime(dt, off);
        }

        public void Serialize(ref MessagePackWriter writer, ref int idx, OffsetDateTime value, IFormatterResolver formatterResolver)
        {
            writer.WriteFixedArrayHeaderUnsafe(c_count, ref idx);

            LocalDateTimeFormatter.Instance.Serialize(ref writer, ref idx, value.LocalDateTime, formatterResolver);
            OffsetFormatter.Instance.Serialize(ref writer, ref idx, value.Offset, formatterResolver);
        }
    }
}
