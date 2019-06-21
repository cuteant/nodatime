namespace NodaTime.Serialization.JsonSpan
{
    using NodaTime.Text;

    /// <summary>Formatter for offset times.</summary>
    public sealed class OffsetTimeFormatter : NodaPatternFormatterBase<OffsetTime>
    {
        public static readonly OffsetTimeFormatter Default = new OffsetTimeFormatter();

        private OffsetTimeFormatter() : base(OffsetTimePattern.ExtendedIso) { }
    }
}
