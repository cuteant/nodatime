namespace NodaTime.Serialization.MessagePackSpan
{
    using MessagePack;
    using MessagePack.Formatters;

    public sealed class DateTimeZoneFormatter : IMessagePackFormatter<DateTimeZone>
    {
        public static readonly DateTimeZoneFormatter Instance = new DateTimeZoneFormatter();

        private readonly IDateTimeZoneProvider _provider;

        private DateTimeZoneFormatter()
        {
            this._provider = DateTimeZoneProviders.Serialization;
        }

        /// <param name="provider">Provides the <see cref="DateTimeZone"/> that corresponds to each time zone ID in the JSON string.</param>
        public DateTimeZoneFormatter(IDateTimeZoneProvider provider)
        {
            if (null == provider) { ThrowHelper.ThrowArgumentNullException_Provider(); }
            this._provider = provider;
        }

        public DateTimeZone Deserialize(ref MessagePackReader reader, IFormatterResolver formatterResolver)
        {
            if (reader.IsNil()) { return null; }

            return _provider[reader.ReadStringWithCache()];
        }

        public void Serialize(ref MessagePackWriter writer, ref int idx, DateTimeZone value, IFormatterResolver formatterResolver)
        {
            if (null == value) { writer.WriteNil(ref idx); return; }

            writer.WriteStringWithCache(value.Id, ref idx);
        }
    }
}
