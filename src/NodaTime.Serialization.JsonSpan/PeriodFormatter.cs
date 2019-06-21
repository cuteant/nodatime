namespace NodaTime.Serialization.JsonSpan
{
    using NodaTime.Text;

    /// <summary>Round-tripping formatter for periods. Use this when you really want to preserve information,
    /// and don't need interoperability with systems expecting ISO.</summary>
    public sealed class RoundtripPeriodFormatter : NodaPatternFormatterBase<Period>
    {
        public static readonly RoundtripPeriodFormatter Default = new RoundtripPeriodFormatter();

        private RoundtripPeriodFormatter() : base(PeriodPattern.Roundtrip) { }
    }

    /// <summary>Normalizing ISO converter for periods. Use this when you want compatibility with systems expecting
    /// ISO durations (~= Noda Time periods). However, note that Noda Time can have negative periods. Note that
    /// this converter losees information - after serialization and deserialization, "90 minutes" will become "an hour and 30 minutes".</summary>
    public sealed class NormalizingIsoPeriodFormatter : NodaPatternFormatterBase<Period>
    {
        public static readonly NormalizingIsoPeriodFormatter Default = new NormalizingIsoPeriodFormatter();

        private NormalizingIsoPeriodFormatter() : base(PeriodPattern.NormalizingIso) { }
    }
}
