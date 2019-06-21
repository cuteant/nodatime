namespace NodaTime.Serialization.MessagePackSpan
{
    using MessagePack;
    using MessagePack.Formatters;

    public sealed class TzdbZonedDateTimeFormatter : ZonedDateTimeFormatter
    {
        public new static readonly TzdbZonedDateTimeFormatter Instance = new TzdbZonedDateTimeFormatter();

        private TzdbZonedDateTimeFormatter() : base(DateTimeZoneProviders.Tzdb) { }
    }

    public sealed class BclZonedDateTimeFormatter : ZonedDateTimeFormatter
    {
        public new static readonly BclZonedDateTimeFormatter Instance = new BclZonedDateTimeFormatter();

        private BclZonedDateTimeFormatter() : base(DateTimeZoneProviders.Bcl) { }
    }

    public class ZonedDateTimeFormatter : IMessagePackFormatter<ZonedDateTime>
    {
        public static readonly ZonedDateTimeFormatter Instance = new ZonedDateTimeFormatter();

        const int c_count = 3;

        private readonly IDateTimeZoneProvider _provider;

        private ZonedDateTimeFormatter()
        {
            this._provider = DateTimeZoneProviders.Serialization;
        }

        /// <param name="provider">Provides the <see cref="DateTimeZone"/> that corresponds to each time zone ID in the JSON string.</param>
        public ZonedDateTimeFormatter(IDateTimeZoneProvider provider)
        {
            if (null == provider) { ThrowHelper.ThrowArgumentNullException_Provider(); }
            this._provider = provider;
        }

        public ZonedDateTime Deserialize(ref MessagePackReader reader, IFormatterResolver formatterResolver)
        {
            var count = reader.ReadArrayHeader();
            if (count != c_count) { ThrowHelper.ThrowInvalidOperationException_InvalidArrayCount(); }

            var dt = LocalDateTimeFormatter.Instance.Deserialize(ref reader, formatterResolver);
            var off = OffsetFormatter.Instance.Deserialize(ref reader, formatterResolver);
            var zoneId = reader.ReadStringWithCache();

            return new ZonedDateTime(dt, _provider[zoneId], off);
        }

        public void Serialize(ref MessagePackWriter writer, ref int idx, ZonedDateTime value, IFormatterResolver formatterResolver)
        {
            writer.WriteFixedArrayHeaderUnsafe(c_count, ref idx);

            LocalDateTimeFormatter.Instance.Serialize(ref writer, ref idx, value.LocalDateTime, formatterResolver);
            OffsetFormatter.Instance.Serialize(ref writer, ref idx, value.Offset, formatterResolver);
            writer.WriteStringWithCache(value.Zone.Id, ref idx);
        }
    }

}
