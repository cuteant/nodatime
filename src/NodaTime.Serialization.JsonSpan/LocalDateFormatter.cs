namespace NodaTime.Serialization.JsonSpan
{
    using NodaTime.Text;

    /// <summary>Formatter for local dates, using the ISO-8601 date pattern.</summary>
    public sealed class LocalDateFormatter : NodaPatternFormatterBase<LocalDate>
    {
        public static readonly LocalDateFormatter Default = new LocalDateFormatter();

        private LocalDateFormatter() : base(LocalDatePattern.Iso, CreateIsoValidator(x => x.Calendar)) { }
    }
}
