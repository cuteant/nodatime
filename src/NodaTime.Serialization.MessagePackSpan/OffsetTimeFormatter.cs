namespace NodaTime.Serialization.MessagePackSpan
{
    using MessagePack;
    using MessagePack.Formatters;

    public sealed class OffsetTimeFormatter : IMessagePackFormatter<OffsetTime>
    {
        public static readonly OffsetTimeFormatter Instance = new OffsetTimeFormatter();

        public OffsetTime Deserialize(ref MessagePackReader reader, IFormatterResolver formatterResolver)
        {
            return new OffsetTime(reader.ReadInt64());
        }

        public void Serialize(ref MessagePackWriter writer, ref int idx, OffsetTime value, IFormatterResolver formatterResolver)
        {
            writer.WriteInt64(value.nanosecondsAndOffset, ref idx);
        }
    }
}
