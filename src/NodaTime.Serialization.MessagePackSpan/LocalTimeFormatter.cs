namespace NodaTime.Serialization.MessagePackSpan
{
    using MessagePack;
    using MessagePack.Formatters;

    public sealed class LocalTimeFormatter : IMessagePackFormatter<LocalTime>
    {
        public static readonly LocalTimeFormatter Instance = new LocalTimeFormatter();

        public LocalTime Deserialize(ref MessagePackReader reader, IFormatterResolver formatterResolver)
        {
            return LocalTime.FromNanosecondsSinceMidnight(reader.ReadInt64());
        }

        public void Serialize(ref MessagePackWriter writer, ref int idx, LocalTime value, IFormatterResolver formatterResolver)
        {
            writer.WriteInt64(value.NanosecondOfDay, ref idx);
        }
    }
}
