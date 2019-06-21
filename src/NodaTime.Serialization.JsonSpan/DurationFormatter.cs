namespace NodaTime.Serialization.JsonSpan
{
    using NodaTime.Text;

    /// <summary>Formatter for durations.</summary>
    public sealed class DurationFormatter : NodaPatternFormatterBase<Duration>
    {
        public static readonly DurationFormatter Default = new DurationFormatter();

        private DurationFormatter() : base(DurationPattern.JsonRoundtrip) { }
    }
}
