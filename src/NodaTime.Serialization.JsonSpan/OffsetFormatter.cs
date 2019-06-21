namespace NodaTime.Serialization.JsonSpan
{
    using NodaTime.Text;

    /// <summary>Formatter for offsets.</summary>
    public sealed class OffsetFormatter : NodaPatternFormatterBase<Offset>
    {
        public static readonly OffsetFormatter Default = new OffsetFormatter();

        private OffsetFormatter() : base(OffsetPattern.GeneralInvariant) { }
    }
}
