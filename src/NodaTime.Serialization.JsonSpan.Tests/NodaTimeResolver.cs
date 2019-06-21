﻿using SpanJson;
using SpanJson.Resolvers;

namespace NodaTime.Serialization.JsonSpan.Tests
{
    public sealed class NodaIncludeNullsOriginalCaseResolver<TSymbol> : ResolverBase<TSymbol, NodaIncludeNullsOriginalCaseResolver<TSymbol>>
        where TSymbol : struct
    {
        public NodaIncludeNullsOriginalCaseResolver() : base(new SpanJsonOptions
        {
            NullOption = NullOptions.IncludeNulls,
            EnumOption = EnumOptions.String
        })
        {
            RegisterGlobalCustomFormatter<DateInterval, DateIntervalFormatter>();

            RegisterGlobalCustomFormatter<DateTimeZone, TzdbDateTimeZoneFormatter>();

            RegisterGlobalCustomFormatter<Duration, DurationFormatter>();
            RegisterGlobalCustomFormatter<Instant, InstantFormatter>();

            RegisterGlobalCustomFormatter<Interval, IntervalFormatter>();

            RegisterGlobalCustomFormatter<LocalDate, LocalDateFormatter>();
            RegisterGlobalCustomFormatter<LocalDateTime, LocalDateTimeFormatter>();
            RegisterGlobalCustomFormatter<LocalTime, LocalTimeFormatter>();
            RegisterGlobalCustomFormatter<Offset, OffsetFormatter>();
            RegisterGlobalCustomFormatter<OffsetDate, OffsetDateFormatter>();
            RegisterGlobalCustomFormatter<OffsetDateTime, OffsetDateTimeFormatter>();
            RegisterGlobalCustomFormatter<OffsetTime, OffsetTimeFormatter>();

            RegisterGlobalCustomFormatter<Period, RoundtripPeriodFormatter>();

            RegisterGlobalCustomFormatter<ZonedDateTime, TzdbZonedDateTimeFormatter>();
        }
    }

    public sealed class NodaExcludeNullsOriginalCaseResolver<TSymbol> : ResolverBase<TSymbol, NodaExcludeNullsOriginalCaseResolver<TSymbol>>
        where TSymbol : struct
    {
        public NodaExcludeNullsOriginalCaseResolver() : base(new SpanJsonOptions
        {
            NullOption = NullOptions.ExcludeNulls,
            EnumOption = EnumOptions.String
        })
        {
            RegisterGlobalCustomFormatter<DateInterval, DateIntervalFormatter>();

            RegisterGlobalCustomFormatter<DateTimeZone, TzdbDateTimeZoneFormatter>();

            RegisterGlobalCustomFormatter<Duration, DurationFormatter>();
            RegisterGlobalCustomFormatter<Instant, InstantFormatter>();

            RegisterGlobalCustomFormatter<Interval, IntervalFormatter>();

            RegisterGlobalCustomFormatter<LocalDate, LocalDateFormatter>();
            RegisterGlobalCustomFormatter<LocalDateTime, LocalDateTimeFormatter>();
            RegisterGlobalCustomFormatter<LocalTime, LocalTimeFormatter>();
            RegisterGlobalCustomFormatter<Offset, OffsetFormatter>();
            RegisterGlobalCustomFormatter<OffsetDate, OffsetDateFormatter>();
            RegisterGlobalCustomFormatter<OffsetDateTime, OffsetDateTimeFormatter>();
            RegisterGlobalCustomFormatter<OffsetTime, OffsetTimeFormatter>();

            RegisterGlobalCustomFormatter<Period, RoundtripPeriodFormatter>();

            RegisterGlobalCustomFormatter<ZonedDateTime, TzdbZonedDateTimeFormatter>();
        }
    }

    public sealed class NodaIsoExcludeNullsOriginalCaseResolver<TSymbol> : ResolverBase<TSymbol, NodaIsoExcludeNullsOriginalCaseResolver<TSymbol>>
        where TSymbol : struct
    {
        public NodaIsoExcludeNullsOriginalCaseResolver() : base(new SpanJsonOptions
        {
            NullOption = NullOptions.ExcludeNulls,
            EnumOption = EnumOptions.String
        })
        {
            RegisterGlobalCustomFormatter<DateInterval, IsoDateIntervalFormatter>();

            RegisterGlobalCustomFormatter<DateTimeZone, TzdbDateTimeZoneFormatter>();

            RegisterGlobalCustomFormatter<Duration, DurationFormatter>();
            RegisterGlobalCustomFormatter<Instant, InstantFormatter>();

            RegisterGlobalCustomFormatter<Interval, IsoIntervalFormatter>();

            RegisterGlobalCustomFormatter<LocalDate, LocalDateFormatter>();
            RegisterGlobalCustomFormatter<LocalDateTime, LocalDateTimeFormatter>();
            RegisterGlobalCustomFormatter<LocalTime, LocalTimeFormatter>();
            RegisterGlobalCustomFormatter<Offset, OffsetFormatter>();
            RegisterGlobalCustomFormatter<OffsetDate, OffsetDateFormatter>();
            RegisterGlobalCustomFormatter<OffsetDateTime, OffsetDateTimeFormatter>();
            RegisterGlobalCustomFormatter<OffsetTime, OffsetTimeFormatter>();

            RegisterGlobalCustomFormatter<Period, NormalizingIsoPeriodFormatter>();

            RegisterGlobalCustomFormatter<ZonedDateTime, TzdbZonedDateTimeFormatter>();
        }
    }

    public sealed class NodaExcludeNullsCamelCaseResolver<TSymbol> : ResolverBase<TSymbol, NodaExcludeNullsCamelCaseResolver<TSymbol>> where TSymbol : struct
    {
        public NodaExcludeNullsCamelCaseResolver() : base(new SpanJsonOptions
        {
            NullOption = NullOptions.ExcludeNulls,
            EnumOption = EnumOptions.String,
            ExtensionDataNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        })
        {
            RegisterGlobalCustomFormatter<DateInterval, DateIntervalFormatter>();

            RegisterGlobalCustomFormatter<DateTimeZone, TzdbDateTimeZoneFormatter>();

            RegisterGlobalCustomFormatter<Duration, DurationFormatter>();
            RegisterGlobalCustomFormatter<Instant, InstantFormatter>();

            RegisterGlobalCustomFormatter<Interval, IntervalFormatter>();

            RegisterGlobalCustomFormatter<LocalDate, LocalDateFormatter>();
            RegisterGlobalCustomFormatter<LocalDateTime, LocalDateTimeFormatter>();
            RegisterGlobalCustomFormatter<LocalTime, LocalTimeFormatter>();
            RegisterGlobalCustomFormatter<Offset, OffsetFormatter>();
            RegisterGlobalCustomFormatter<OffsetDate, OffsetDateFormatter>();
            RegisterGlobalCustomFormatter<OffsetDateTime, OffsetDateTimeFormatter>();
            RegisterGlobalCustomFormatter<OffsetTime, OffsetTimeFormatter>();

            RegisterGlobalCustomFormatter<Period, RoundtripPeriodFormatter>();

            RegisterGlobalCustomFormatter<ZonedDateTime, TzdbZonedDateTimeFormatter>();
        }
    }

    public sealed class NodaIsoExcludeNullsCamelCaseResolver<TSymbol> : ResolverBase<TSymbol, NodaIsoExcludeNullsCamelCaseResolver<TSymbol>> where TSymbol : struct
    {
        public NodaIsoExcludeNullsCamelCaseResolver() : base(new SpanJsonOptions
        {
            NullOption = NullOptions.ExcludeNulls,
            EnumOption = EnumOptions.String,
            ExtensionDataNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        })
        {
            RegisterGlobalCustomFormatter<DateInterval, IsoDateIntervalFormatter>();

            RegisterGlobalCustomFormatter<DateTimeZone, TzdbDateTimeZoneFormatter>();

            RegisterGlobalCustomFormatter<Duration, DurationFormatter>();
            RegisterGlobalCustomFormatter<Instant, InstantFormatter>();

            RegisterGlobalCustomFormatter<Interval, IsoIntervalFormatter>();

            RegisterGlobalCustomFormatter<LocalDate, LocalDateFormatter>();
            RegisterGlobalCustomFormatter<LocalDateTime, LocalDateTimeFormatter>();
            RegisterGlobalCustomFormatter<LocalTime, LocalTimeFormatter>();
            RegisterGlobalCustomFormatter<Offset, OffsetFormatter>();
            RegisterGlobalCustomFormatter<OffsetDate, OffsetDateFormatter>();
            RegisterGlobalCustomFormatter<OffsetDateTime, OffsetDateTimeFormatter>();
            RegisterGlobalCustomFormatter<OffsetTime, OffsetTimeFormatter>();

            RegisterGlobalCustomFormatter<Period, NormalizingIsoPeriodFormatter>();

            RegisterGlobalCustomFormatter<ZonedDateTime, TzdbZonedDateTimeFormatter>();
        }
    }
}
