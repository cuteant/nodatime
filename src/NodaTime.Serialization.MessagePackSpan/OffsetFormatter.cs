namespace NodaTime.Serialization.MessagePackSpan
{
    using MessagePack.Formatters;
    using MessagePack;

    public sealed class OffsetFormatter : IMessagePackFormatter<Offset>
    {
        public static readonly OffsetFormatter Instance = new OffsetFormatter();

        public Offset Deserialize(ref MessagePackReader reader, IFormatterResolver formatterResolver)
        {
            return Offset.FromSeconds(reader.ReadInt32());
        }

        public void Serialize(ref MessagePackWriter writer, ref int idx, Offset value, IFormatterResolver formatterResolver)
        {
            writer.WriteInt32(value.Seconds, ref idx);
        }
    }
}
