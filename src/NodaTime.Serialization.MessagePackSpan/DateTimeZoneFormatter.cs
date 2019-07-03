namespace NodaTime.Serialization.MessagePackSpan
{
    using MessagePack;
    using MessagePack.Formatters;

    public sealed class TzdbDateTimeZoneFormatter<T> : DateTimeZoneFormatter<T> where T : DateTimeZone
    {
        public TzdbDateTimeZoneFormatter() : base(DateTimeZoneProviders.Tzdb) { }
    }

    public sealed class BclDateTimeZoneFormatter<T> : DateTimeZoneFormatter<T> where T : DateTimeZone
    {
        public BclDateTimeZoneFormatter() : base(DateTimeZoneProviders.Bcl) { }
    }

    public class DateTimeZoneFormatter<T> : IMessagePackFormatter<T> where T : DateTimeZone
    {
        private readonly IDateTimeZoneProvider _provider;

        public DateTimeZoneFormatter()
        {
            this._provider = DateTimeZoneProviders.Serialization;
        }

        /// <param name="provider">Provides the <see cref="DateTimeZone"/> that corresponds to each time zone ID in the JSON string.</param>
        public DateTimeZoneFormatter(IDateTimeZoneProvider provider)
        {
            if (null == provider) { ThrowHelper.ThrowArgumentNullException_Provider(); }
            this._provider = provider;
        }

        public T Deserialize(ref MessagePackReader reader, IFormatterResolver formatterResolver)
        {
            if (reader.IsNil()) { return null; }

            return _provider[reader.ReadStringWithCache()] as T;
        }

        public void Serialize(ref MessagePackWriter writer, ref int idx, T value, IFormatterResolver formatterResolver)
        {
            if (null == value) { writer.WriteNil(ref idx); return; }

            writer.WriteStringWithCache(value.Id, ref idx);
        }
    }
}
