
namespace NodaTime.Serialization.MessagePackSpan
{
    using MessagePack;
    using MessagePack.Formatters;

    public sealed class InstantFormatter : IMessagePackFormatter<Instant>
    {
        public static readonly InstantFormatter Instance = new InstantFormatter();

        const int c_count = 2;

        public Instant Deserialize(ref MessagePackReader reader, IFormatterResolver formatterResolver)
        {
            var count = reader.ReadArrayHeader();
            if (count != c_count) { ThrowHelper.ThrowInvalidOperationException_InvalidArrayCount(); }

            var days = reader.ReadInt32();
            var nanoOfDay = reader.ReadInt64();
            return Instant.FromUntrustedDuration(new Duration(days, nanoOfDay));
        }

        public void Serialize(ref MessagePackWriter writer, ref int idx, Instant value, IFormatterResolver formatterResolver)
        {
            writer.WriteFixedArrayHeaderUnsafe(c_count, ref idx);

            var duration = value.TimeSinceEpoch;
            writer.WriteInt32(duration.FloorDays, ref idx);
            writer.WriteInt64(duration.NanosecondOfFloorDay, ref idx);
        }
    }
}