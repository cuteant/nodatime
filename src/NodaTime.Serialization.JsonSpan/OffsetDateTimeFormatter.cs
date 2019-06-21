namespace NodaTime.Serialization.JsonSpan
{
    using NodaTime.Text;

    /// <summary>Formatter for offset date/times.</summary>
    public sealed class OffsetDateTimeFormatter : NodaPatternFormatterBase<OffsetDateTime>
    {
        public static readonly OffsetDateTimeFormatter Default = new OffsetDateTimeFormatter();

        private OffsetDateTimeFormatter() : base(OffsetDateTimePattern.Rfc3339, CreateIsoValidator(x => x.Calendar)) { }
    }
}
