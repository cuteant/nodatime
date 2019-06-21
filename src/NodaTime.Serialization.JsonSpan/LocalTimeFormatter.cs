namespace NodaTime.Serialization.JsonSpan
{
    using NodaTime.Text;

    /// <summary>Formatter for local times, using the ISO-8601 time pattern, extended as required to accommodate milliseconds and ticks.</summary>
    public sealed class LocalTimeFormatter : NodaPatternFormatterBase<LocalTime>
    {
        public static readonly LocalTimeFormatter Default = new LocalTimeFormatter();

        private LocalTimeFormatter() : base(LocalTimePattern.ExtendedIso) { }
    }
}
