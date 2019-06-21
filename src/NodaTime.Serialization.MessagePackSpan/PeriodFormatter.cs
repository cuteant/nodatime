namespace NodaTime.Serialization.MessagePackSpan
{
    using MessagePack;
    using MessagePack.Formatters;
    using NodaTime;
    using NodaTime.Text;

    public sealed class PeriodFormatter : IMessagePackFormatter<Period>
    {
        public static readonly PeriodFormatter Instance = new PeriodFormatter();

        const int c_count = 10;

        public Period Deserialize(ref MessagePackReader reader, IFormatterResolver formatterResolver)
        {
            if (reader.IsNil()) { return null; }

            var count = reader.ReadArrayHeader();
            if (count != c_count) { ThrowHelper.ThrowInvalidOperationException_InvalidArrayCount(); }

            var years = reader.ReadInt32();
            var months = reader.ReadInt32();
            var weeks = reader.ReadInt32();
            var days = reader.ReadInt32();
            var hours = reader.ReadInt64();
            var minutes = reader.ReadInt64();
            var seconds = reader.ReadInt64();
            var milliseconds = reader.ReadInt64();
            var ticks = reader.ReadInt64();
            var nano = reader.ReadInt64();

            return new PeriodBuilder()
            {
                Years = years,
                Months = months,
                Weeks = weeks,
                Days = days,
                Hours = hours,
                Minutes = minutes,
                Seconds = seconds,
                Milliseconds = milliseconds,
                Ticks = ticks,
                Nanoseconds = nano,
            }.Build();
        }

        public void Serialize(ref MessagePackWriter writer, ref int idx, Period value, IFormatterResolver formatterResolver)
        {
            if (value == null) { writer.WriteNil(ref idx); return; }

            writer.WriteFixedArrayHeaderUnsafe(c_count, ref idx);
            writer.WriteInt32(value.Years, ref idx);
            writer.WriteInt32(value.Months, ref idx);
            writer.WriteInt32(value.Weeks, ref idx);
            writer.WriteInt32(value.Days, ref idx);
            writer.WriteInt64(value.Hours, ref idx);
            writer.WriteInt64(value.Minutes, ref idx);
            writer.WriteInt64(value.Seconds, ref idx);
            writer.WriteInt64(value.Milliseconds, ref idx);
            writer.WriteInt64(value.Ticks, ref idx);
            writer.WriteInt64(value.Nanoseconds, ref idx);
        }
    }
}