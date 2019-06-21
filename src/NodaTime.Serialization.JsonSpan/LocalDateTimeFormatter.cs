namespace NodaTime.Serialization.JsonSpan
{
    using NodaTime.Text;

    /// <summary>Formatter for local dates and times, using the ISO-8601 date/time pattern, extended as required to accommodate milliseconds and ticks.
    /// No time zone designator is applied.</summary>
    public sealed class LocalDateTimeFormatter : NodaPatternFormatterBase<LocalDateTime>
    {
        public static readonly LocalDateTimeFormatter Default = new LocalDateTimeFormatter();

        private LocalDateTimeFormatter() : base(LocalDateTimePattern.ExtendedIso, CreateIsoValidator(x => x.Calendar)) { }
    }
}
