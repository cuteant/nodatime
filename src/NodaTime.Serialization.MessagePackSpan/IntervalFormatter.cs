namespace NodaTime.Serialization.MessagePackSpan
{
    using MessagePack;
    using MessagePack.Formatters;

    public sealed class IntervalFormatter : IMessagePackFormatter<Interval>
    {
        public static readonly IntervalFormatter Instance = new IntervalFormatter();

        public Interval Deserialize(ref MessagePackReader reader, IFormatterResolver formatterResolver)
        {
            var rawCount = reader.ReadArrayHeader();

            var presence = reader.ReadByte();
            var count = 1;
            var hasStart = (presence & 1) == 1 ? true : false;
            if (hasStart) { count += 2; }
            var hasEnd = (presence & 2) == 2 ? true : false;
            if (hasEnd) { count += 2; }

            if (count != rawCount) { ThrowHelper.ThrowInvalidOperationException_InvalidArrayCount(); }

            var start = hasStart ? Instant.FromUntrustedDuration(new Duration(reader.ReadInt32(), reader.ReadInt64())) : Instant.BeforeMinValue;
            var end = hasEnd ? Instant.FromUntrustedDuration(new Duration(reader.ReadInt32(), reader.ReadInt64())) : Instant.AfterMaxValue;

            return new Interval(start, end);
        }

        public void Serialize(ref MessagePackWriter writer, ref int idx, Interval value, IFormatterResolver formatterResolver)
        {
            var count = 1;
            var hasStart = value.HasStart;
            if (hasStart) { count += 2; }
            var hasEnd = value.HasEnd;
            if (hasEnd) { count += 2; }

            writer.WriteFixedArrayHeaderUnsafe(count, ref idx);

            writer.WriteByte((byte)((hasStart ? 1 : 0) | (hasEnd ? 2 : 0)), ref idx);
            if (hasStart)
            {
                var start = value.Start;
                var duration = start.TimeSinceEpoch;
                writer.WriteInt32(duration.FloorDays, ref idx);
                writer.WriteInt64(duration.NanosecondOfFloorDay, ref idx);
            }
            if (hasEnd)
            {
                var end = value.End;
                var duration = end.TimeSinceEpoch;
                writer.WriteInt32(duration.FloorDays, ref idx);
                writer.WriteInt64(duration.NanosecondOfFloorDay, ref idx);
            }
        }
    }
}
