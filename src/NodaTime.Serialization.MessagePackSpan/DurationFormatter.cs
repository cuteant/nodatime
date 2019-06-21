namespace NodaTime.Serialization.MessagePackSpan
{
    using MessagePack;
    using MessagePack.Formatters;

    public sealed class DurationFormatter : IMessagePackFormatter<Duration>
    {
        public static readonly DurationFormatter Instance = new DurationFormatter();

        private DurationFormatter() { }

        const int c_count = 2;

        public Duration Deserialize(ref MessagePackReader reader, IFormatterResolver formatterResolver)
        {
            var count = reader.ReadArrayHeader();
            if (count != c_count) { ThrowHelper.ThrowInvalidOperationException_InvalidArrayCount(); }

            var days = reader.ReadInt32();
            var nanoOfDay = reader.ReadInt64();
            return new Duration(days, nanoOfDay);
        }

        public void Serialize(ref MessagePackWriter writer, ref int idx, Duration value, IFormatterResolver formatterResolver)
        {
            writer.WriteFixedArrayHeaderUnsafe(c_count, ref idx);

            writer.WriteInt32(value.FloorDays, ref idx);
            writer.WriteInt64(value.NanosecondOfFloorDay, ref idx);
        }
    }
}
