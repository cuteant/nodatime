namespace NodaTime.Serialization.JsonSpan
{
    using NodaTime.Text;

    /// <summary>Formatter for instants, using the ISO-8601 date/time pattern, extended as required to accommodate milliseconds and ticks, and
    /// specifying 'Z' at the end to show it's effectively in UTC.</summary>
    public sealed class InstantFormatter : NodaPatternFormatterBase<Instant>
    {
        public static readonly InstantFormatter Default = new InstantFormatter();

        private InstantFormatter() : base(InstantPattern.ExtendedIso) { }
    }
}
