namespace NodaTime.Serialization.JsonSpan
{
    using NodaTime.Text;

    /// <summary>Formatter for zoned date/times, using the given time zone provider.</summary>
    public sealed class TzdbZonedDateTimeFormatter : NodaPatternFormatterBase<ZonedDateTime>
    {
        public static readonly TzdbZonedDateTimeFormatter Default = new TzdbZonedDateTimeFormatter();

        private TzdbZonedDateTimeFormatter()
            : base(ZonedDateTimePattern.CreateWithInvariantCulture("uuuu'-'MM'-'dd'T'HH':'mm':'ss;FFFFFFFFFo<G> z",
                DateTimeZoneProviders.Tzdb), CreateIsoValidator(x => x.Calendar))
        { }
    }

    /// <summary>Formatter for zoned date/times, using the given time zone provider.</summary>
    public sealed class BclZonedDateTimeFormatter : NodaPatternFormatterBase<ZonedDateTime>
    {
        public static readonly BclZonedDateTimeFormatter Default = new BclZonedDateTimeFormatter();

        private BclZonedDateTimeFormatter()
            : base(ZonedDateTimePattern.CreateWithInvariantCulture("uuuu'-'MM'-'dd'T'HH':'mm':'ss;FFFFFFFFFo<G> z",
                DateTimeZoneProviders.Bcl), CreateIsoValidator(x => x.Calendar))
        { }
    }

    /// <summary>Formatter for zoned date/times, using the given time zone provider.</summary>
    public sealed class ZonedDateTimeFormatter : NodaPatternFormatterBase<ZonedDateTime>
    {
        public static readonly ZonedDateTimeFormatter Default = new ZonedDateTimeFormatter();

        private ZonedDateTimeFormatter() 
            : base(ZonedDateTimePattern.CreateWithInvariantCulture("uuuu'-'MM'-'dd'T'HH':'mm':'ss;FFFFFFFFFo<G> z", 
                DateTimeZoneProviders.Serialization), CreateIsoValidator(x => x.Calendar))
        { }
    }
}
