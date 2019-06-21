namespace NodaTime.Serialization.JsonSpan
{
    using NodaTime.Text;

    /// <summary>Formatter for offset dates.</summary>
    public sealed class OffsetDateFormatter : NodaPatternFormatterBase<OffsetDate>
    {
        public static readonly OffsetDateFormatter Default = new OffsetDateFormatter();

        private OffsetDateFormatter() : base(OffsetDatePattern.GeneralIso, CreateIsoValidator(x => x.Calendar)) { }
    }
}
