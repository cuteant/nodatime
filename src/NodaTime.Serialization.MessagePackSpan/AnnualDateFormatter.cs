namespace NodaTime.Serialization.MessagePackSpan
{
    using MessagePack;
    using MessagePack.Formatters;

    public sealed class AnnualDateFormatter : IMessagePackFormatter<AnnualDate>
    {
        public static readonly AnnualDateFormatter Instance = new AnnualDateFormatter();

        private AnnualDateFormatter() { }

        const int c_count = 2;

        public AnnualDate Deserialize(ref MessagePackReader reader, IFormatterResolver formatterResolver)
        {
            var count = reader.ReadArrayHeader();
            if (count != c_count) { ThrowHelper.ThrowInvalidOperationException_InvalidArrayCount(); }

            return new AnnualDate(reader.ReadInt32(), reader.ReadInt32());
        }

        public void Serialize(ref MessagePackWriter writer, ref int idx, AnnualDate value, IFormatterResolver formatterResolver)
        {
            writer.WriteFixedArrayHeaderUnsafe(c_count, ref idx);

            writer.WriteInt32(value.Month, ref idx);
            writer.WriteInt32(value.Day, ref idx);
        }
    }
}
