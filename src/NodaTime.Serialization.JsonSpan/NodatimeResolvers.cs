namespace NodaTime.Serialization.JsonSpan
{
    using System;
    using System.Collections.Generic;
    using SpanJson;

    public sealed class NodatimeResolver : ICustomJsonFormatterResolver
    {
        // Resolver should be singleton.
        public static readonly NodatimeResolver Instance = new NodatimeResolver();

        NodatimeResolver() { }

        public bool IsSupportedType(Type type)
        {
            if (formatterMap.ContainsKey(type)) { return true; }
            return typeof(DateTimeZone).IsAssignableFrom(type);
        }

        // GetFormatter<T>'s get cost should be minimized so use type cache.
        public ICustomJsonFormatter GetFormatter(Type type)
        {
            if (formatterMap.TryGetValue(type, out var formatter))
            {
                return formatter;
            }

            // DateTimeZone
            if (typeof(DateTimeZone).IsAssignableFrom(type))
            {
                return Activator.CreateInstance(typeof(DateTimeZoneFormatter<>).MakeGenericType(type)) as ICustomJsonFormatter;
            }

            // If type can not get, must return null for fallback mechanism.
            return null;
        }

        static readonly Dictionary<Type, ICustomJsonFormatter> formatterMap = new Dictionary<Type, ICustomJsonFormatter>()
        {
            // TODO AnnualDateFormatter
            //{ typeof(AnnualDate), AnnualDateFormatter.Default },

            { typeof(DateInterval), DateIntervalFormatter.Default },
            { typeof(Interval), IntervalFormatter.Default },

            { typeof(Duration), DurationFormatter.Default },
            { typeof(Instant), InstantFormatter.Default },

            { typeof(LocalDate), LocalDateFormatter.Default },
            { typeof(LocalDateTime), LocalDateTimeFormatter.Default },
            { typeof(LocalTime), LocalTimeFormatter.Default },

            { typeof(Offset), OffsetFormatter.Default },
            { typeof(OffsetDate), OffsetDateFormatter.Default },
            { typeof(OffsetDateTime), OffsetDateTimeFormatter.Default },
            { typeof(OffsetTime), OffsetTimeFormatter.Default },

            { typeof(Period), RoundtripPeriodFormatter.Default },

            { typeof(ZonedDateTime), ZonedDateTimeFormatter.Default },
        };
    }

    public sealed class BclNodatimeResolver : ICustomJsonFormatterResolver
    {
        // Resolver should be singleton.
        public static readonly BclNodatimeResolver Instance = new BclNodatimeResolver();

        BclNodatimeResolver() { }

        public bool IsSupportedType(Type type)
        {
            if (formatterMap.ContainsKey(type)) { return true; }
            return typeof(DateTimeZone).IsAssignableFrom(type);
        }

        // GetFormatter<T>'s get cost should be minimized so use type cache.
        public ICustomJsonFormatter GetFormatter(Type type)
        {
            if (formatterMap.TryGetValue(type, out var formatter))
            {
                return formatter;
            }

            // DateTimeZone
            if (typeof(DateTimeZone).IsAssignableFrom(type))
            {
                return Activator.CreateInstance(typeof(BclDateTimeZoneFormatter<>).MakeGenericType(type)) as ICustomJsonFormatter;
            }

            // If type can not get, must return null for fallback mechanism.
            return null;
        }

        static readonly Dictionary<Type, ICustomJsonFormatter> formatterMap = new Dictionary<Type, ICustomJsonFormatter>()
        {
            // TODO AnnualDateFormatter
            //{ typeof(AnnualDate), AnnualDateFormatter.Default },

            { typeof(DateInterval), DateIntervalFormatter.Default },
            { typeof(Interval), IntervalFormatter.Default },

            { typeof(Duration), DurationFormatter.Default },
            { typeof(Instant), InstantFormatter.Default },

            { typeof(LocalDate), LocalDateFormatter.Default },
            { typeof(LocalDateTime), LocalDateTimeFormatter.Default },
            { typeof(LocalTime), LocalTimeFormatter.Default },

            { typeof(Offset), OffsetFormatter.Default },
            { typeof(OffsetDate), OffsetDateFormatter.Default },
            { typeof(OffsetDateTime), OffsetDateTimeFormatter.Default },
            { typeof(OffsetTime), OffsetTimeFormatter.Default },

            { typeof(Period), RoundtripPeriodFormatter.Default },

            { typeof(ZonedDateTime), BclZonedDateTimeFormatter.Default },
        };
    }

    public sealed class TzdbNodatimeResolver : ICustomJsonFormatterResolver
    {
        // Resolver should be singleton.
        public static readonly TzdbNodatimeResolver Instance = new TzdbNodatimeResolver();

        TzdbNodatimeResolver() { }

        public bool IsSupportedType(Type type)
        {
            if (formatterMap.ContainsKey(type)) { return true; }
            return typeof(DateTimeZone).IsAssignableFrom(type);
        }

        // GetFormatter<T>'s get cost should be minimized so use type cache.
        public ICustomJsonFormatter GetFormatter(Type type)
        {
            if (formatterMap.TryGetValue(type, out var formatter))
            {
                return formatter;
            }

            // DateTimeZone
            if (typeof(DateTimeZone).IsAssignableFrom(type))
            {
                return Activator.CreateInstance(typeof(TzdbDateTimeZoneFormatter<>).MakeGenericType(type)) as ICustomJsonFormatter;
            }

            // If type can not get, must return null for fallback mechanism.
            return null;
        }

        static readonly Dictionary<Type, ICustomJsonFormatter> formatterMap = new Dictionary<Type, ICustomJsonFormatter>()
        {
            // TODO AnnualDateFormatter
            //{ typeof(AnnualDate), AnnualDateFormatter.Default },

            { typeof(DateInterval), DateIntervalFormatter.Default },
            { typeof(Interval), IntervalFormatter.Default },

            { typeof(Duration), DurationFormatter.Default },
            { typeof(Instant), InstantFormatter.Default },

            { typeof(LocalDate), LocalDateFormatter.Default },
            { typeof(LocalDateTime), LocalDateTimeFormatter.Default },
            { typeof(LocalTime), LocalTimeFormatter.Default },

            { typeof(Offset), OffsetFormatter.Default },
            { typeof(OffsetDate), OffsetDateFormatter.Default },
            { typeof(OffsetDateTime), OffsetDateTimeFormatter.Default },
            { typeof(OffsetTime), OffsetTimeFormatter.Default },

            { typeof(Period), RoundtripPeriodFormatter.Default },

            { typeof(ZonedDateTime), TzdbZonedDateTimeFormatter.Default },
        };
    }

    public sealed class IsoNodatimeResolver : ICustomJsonFormatterResolver
    {
        // Resolver should be singleton.
        public static readonly IsoNodatimeResolver Instance = new IsoNodatimeResolver();

        IsoNodatimeResolver() { }

        public bool IsSupportedType(Type type)
        {
            if (formatterMap.ContainsKey(type)) { return true; }
            return typeof(DateTimeZone).IsAssignableFrom(type);
        }

        // GetFormatter<T>'s get cost should be minimized so use type cache.
        public ICustomJsonFormatter GetFormatter(Type type)
        {
            if (formatterMap.TryGetValue(type, out var formatter))
            {
                return formatter;
            }

            // DateTimeZone
            if (typeof(DateTimeZone).IsAssignableFrom(type))
            {
                return Activator.CreateInstance(typeof(DateTimeZoneFormatter<>).MakeGenericType(type)) as ICustomJsonFormatter;
            }

            // If type can not get, must return null for fallback mechanism.
            return null;
        }

        static readonly Dictionary<Type, ICustomJsonFormatter> formatterMap = new Dictionary<Type, ICustomJsonFormatter>()
        {
            // TODO AnnualDateFormatter
            //{ typeof(AnnualDate), AnnualDateFormatter.Default },

            { typeof(DateInterval), IsoDateIntervalFormatter.Default },
            { typeof(Interval), IsoIntervalFormatter.Default },

            { typeof(Duration), DurationFormatter.Default },
            { typeof(Instant), InstantFormatter.Default },

            { typeof(LocalDate), LocalDateFormatter.Default },
            { typeof(LocalDateTime), LocalDateTimeFormatter.Default },
            { typeof(LocalTime), LocalTimeFormatter.Default },

            { typeof(Offset), OffsetFormatter.Default },
            { typeof(OffsetDate), OffsetDateFormatter.Default },
            { typeof(OffsetDateTime), OffsetDateTimeFormatter.Default },
            { typeof(OffsetTime), OffsetTimeFormatter.Default },

            { typeof(Period), NormalizingIsoPeriodFormatter.Default },

            { typeof(ZonedDateTime), ZonedDateTimeFormatter.Default },
        };
    }

    public sealed class BclIsoNodatimeResolver : ICustomJsonFormatterResolver
    {
        // Resolver should be singleton.
        public static readonly BclIsoNodatimeResolver Instance = new BclIsoNodatimeResolver();

        BclIsoNodatimeResolver() { }

        public bool IsSupportedType(Type type)
        {
            if (formatterMap.ContainsKey(type)) { return true; }
            return typeof(DateTimeZone).IsAssignableFrom(type);
        }

        // GetFormatter<T>'s get cost should be minimized so use type cache.
        public ICustomJsonFormatter GetFormatter(Type type)
        {
            if (formatterMap.TryGetValue(type, out var formatter))
            {
                return formatter;
            }

            // DateTimeZone
            if (typeof(DateTimeZone).IsAssignableFrom(type))
            {
                return Activator.CreateInstance(typeof(BclDateTimeZoneFormatter<>).MakeGenericType(type)) as ICustomJsonFormatter;
            }

            // If type can not get, must return null for fallback mechanism.
            return null;
        }

        static readonly Dictionary<Type, ICustomJsonFormatter> formatterMap = new Dictionary<Type, ICustomJsonFormatter>()
        {
            // TODO AnnualDateFormatter
            //{ typeof(AnnualDate), AnnualDateFormatter.Default },

            { typeof(DateInterval), IsoDateIntervalFormatter.Default },
            { typeof(Interval), IsoIntervalFormatter.Default },

            { typeof(Duration), DurationFormatter.Default },
            { typeof(Instant), InstantFormatter.Default },

            { typeof(LocalDate), LocalDateFormatter.Default },
            { typeof(LocalDateTime), LocalDateTimeFormatter.Default },
            { typeof(LocalTime), LocalTimeFormatter.Default },

            { typeof(Offset), OffsetFormatter.Default },
            { typeof(OffsetDate), OffsetDateFormatter.Default },
            { typeof(OffsetDateTime), OffsetDateTimeFormatter.Default },
            { typeof(OffsetTime), OffsetTimeFormatter.Default },

            { typeof(Period), NormalizingIsoPeriodFormatter.Default },

            { typeof(ZonedDateTime), BclZonedDateTimeFormatter.Default },
        };
    }

    public sealed class TzdbIsoNodatimeResolver : ICustomJsonFormatterResolver
    {
        // Resolver should be singleton.
        public static readonly TzdbIsoNodatimeResolver Instance = new TzdbIsoNodatimeResolver();

        TzdbIsoNodatimeResolver() { }

        public bool IsSupportedType(Type type)
        {
            if (formatterMap.ContainsKey(type)) { return true; }
            return typeof(DateTimeZone).IsAssignableFrom(type);
        }

        // GetFormatter<T>'s get cost should be minimized so use type cache.
        public ICustomJsonFormatter GetFormatter(Type type)
        {
            if (formatterMap.TryGetValue(type, out var formatter))
            {
                return formatter;
            }

            // DateTimeZone
            if (typeof(DateTimeZone).IsAssignableFrom(type))
            {
                return Activator.CreateInstance(typeof(TzdbDateTimeZoneFormatter<>).MakeGenericType(type)) as ICustomJsonFormatter;
            }

            // If type can not get, must return null for fallback mechanism.
            return null;
        }

        static readonly Dictionary<Type, ICustomJsonFormatter> formatterMap = new Dictionary<Type, ICustomJsonFormatter>()
        {
            // TODO AnnualDateFormatter
            //{ typeof(AnnualDate), AnnualDateFormatter.Default },

            { typeof(DateInterval), IsoDateIntervalFormatter.Default },
            { typeof(Interval), IsoIntervalFormatter.Default },

            { typeof(Duration), DurationFormatter.Default },
            { typeof(Instant), InstantFormatter.Default },

            { typeof(LocalDate), LocalDateFormatter.Default },
            { typeof(LocalDateTime), LocalDateTimeFormatter.Default },
            { typeof(LocalTime), LocalTimeFormatter.Default },

            { typeof(Offset), OffsetFormatter.Default },
            { typeof(OffsetDate), OffsetDateFormatter.Default },
            { typeof(OffsetDateTime), OffsetDateTimeFormatter.Default },
            { typeof(OffsetTime), OffsetTimeFormatter.Default },

            { typeof(Period), NormalizingIsoPeriodFormatter.Default },

            { typeof(ZonedDateTime), TzdbZonedDateTimeFormatter.Default },
        };
    }
}