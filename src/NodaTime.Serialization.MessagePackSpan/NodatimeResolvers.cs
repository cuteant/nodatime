namespace NodaTime.Serialization.MessagePackSpan
{
    using System;
    using System.Collections.Generic;
    using MessagePack;
    using MessagePack.Formatters;

    public sealed class NodatimeResolver : IFormatterResolver
    {
        // Resolver should be singleton.
        public static readonly IFormatterResolver Instance = new NodatimeResolver();

        NodatimeResolver() { }

        // GetFormatter<T>'s get cost should be minimized so use type cache.
        public IMessagePackFormatter<T> GetFormatter<T>()
        {
            return FormatterCache<T>.formatter;
        }

        static class FormatterCache<T>
        {
            public static readonly IMessagePackFormatter<T> formatter;

            // generic's static constructor should be minimized for reduce type generation size!
            // use outer helper method.
            static FormatterCache() => formatter = (IMessagePackFormatter<T>)NodatimeResolverGetFormatterHelper.GetFormatter(typeof(T));
        }
    }

    internal static class NodatimeResolverGetFormatterHelper
    {
        static readonly Dictionary<Type, object> formatterMap = new Dictionary<Type, object>()
        {
            { typeof(AnnualDate), AnnualDateFormatter.Instance },
            { typeof(AnnualDate?), new StaticNullableFormatter<AnnualDate>(AnnualDateFormatter.Instance) },

            { typeof(DateInterval), DateIntervalFormatter.Instance },

            { typeof(Duration), DurationFormatter.Instance },
            { typeof(Duration?), new StaticNullableFormatter<Duration>(DurationFormatter.Instance) },

            { typeof(Instant), InstantFormatter.Instance },
            { typeof(Instant?), new StaticNullableFormatter<Instant>(InstantFormatter.Instance) },

            { typeof(Interval), IntervalFormatter.Instance },
            { typeof(Interval?), new StaticNullableFormatter<Interval>(IntervalFormatter.Instance) },

            { typeof(LocalDate), LocalDateFormatter.Instance },
            { typeof(LocalDate?), new StaticNullableFormatter<LocalDate>(LocalDateFormatter.Instance) },
            { typeof(LocalDateTime), LocalDateTimeFormatter.Instance },
            { typeof(LocalDateTime?), new StaticNullableFormatter<LocalDateTime>(LocalDateTimeFormatter.Instance) },
            { typeof(LocalTime), LocalTimeFormatter.Instance },
            { typeof(LocalTime?), new StaticNullableFormatter<LocalTime>(LocalTimeFormatter.Instance) },

            { typeof(Offset), OffsetFormatter.Instance },
            { typeof(Offset?), new StaticNullableFormatter<Offset>(OffsetFormatter.Instance) },
            { typeof(OffsetDate), OffsetDateFormatter.Instance },
            { typeof(OffsetDate?), new StaticNullableFormatter<OffsetDate>(OffsetDateFormatter.Instance) },
            { typeof(OffsetDateTime), OffsetDateTimeFormatter.Instance },
            { typeof(OffsetDateTime?), new StaticNullableFormatter<OffsetDateTime>(OffsetDateTimeFormatter.Instance) },
            { typeof(OffsetTime), OffsetTimeFormatter.Instance },
            { typeof(OffsetTime?), new StaticNullableFormatter<OffsetTime>(OffsetTimeFormatter.Instance) },

            { typeof(Period), PeriodFormatter.Instance },

            { typeof(ZonedDateTime), ZonedDateTimeFormatter.Instance },
            { typeof(ZonedDateTime?), new StaticNullableFormatter<ZonedDateTime>(ZonedDateTimeFormatter.Instance) },
        };

        internal static object GetFormatter(Type t)
        {
            if (formatterMap.TryGetValue(t, out var formatter))
            {
                return formatter;
            }

            // DateTimeZone
            if (typeof(DateTimeZone).IsAssignableFrom(t))
            {
                return Activator.CreateInstance(typeof(DateTimeZoneFormatter<>).MakeGenericType(t));
            }

            // If type can not get, must return null for fallback mechanism.
            return null;
        }
    }

    public sealed class BclNodatimeResolver : IFormatterResolver
    {
        // Resolver should be singleton.
        public static readonly IFormatterResolver Instance = new BclNodatimeResolver();

        BclNodatimeResolver() { }

        // GetFormatter<T>'s get cost should be minimized so use type cache.
        public IMessagePackFormatter<T> GetFormatter<T>()
        {
            return FormatterCache<T>.formatter;
        }

        static class FormatterCache<T>
        {
            public static readonly IMessagePackFormatter<T> formatter;

            // generic's static constructor should be minimized for reduce type generation size!
            // use outer helper method.
            static FormatterCache() => formatter = (IMessagePackFormatter<T>)BclNodatimeResolverGetFormatterHelper.GetFormatter(typeof(T));
        }
    }

    internal static class BclNodatimeResolverGetFormatterHelper
    {
        static readonly Dictionary<Type, object> formatterMap = new Dictionary<Type, object>()
        {
            { typeof(AnnualDate), AnnualDateFormatter.Instance },
            { typeof(AnnualDate?), new StaticNullableFormatter<AnnualDate>(AnnualDateFormatter.Instance) },

            { typeof(DateInterval), DateIntervalFormatter.Instance },

            { typeof(Duration), DurationFormatter.Instance },
            { typeof(Duration?), new StaticNullableFormatter<Duration>(DurationFormatter.Instance) },

            { typeof(Instant), InstantFormatter.Instance },
            { typeof(Instant?), new StaticNullableFormatter<Instant>(InstantFormatter.Instance) },

            { typeof(Interval), IntervalFormatter.Instance },
            { typeof(Interval?), new StaticNullableFormatter<Interval>(IntervalFormatter.Instance) },

            { typeof(LocalDate), LocalDateFormatter.Instance },
            { typeof(LocalDate?), new StaticNullableFormatter<LocalDate>(LocalDateFormatter.Instance) },
            { typeof(LocalDateTime), LocalDateTimeFormatter.Instance },
            { typeof(LocalDateTime?), new StaticNullableFormatter<LocalDateTime>(LocalDateTimeFormatter.Instance) },
            { typeof(LocalTime), LocalTimeFormatter.Instance },
            { typeof(LocalTime?), new StaticNullableFormatter<LocalTime>(LocalTimeFormatter.Instance) },

            { typeof(Offset), OffsetFormatter.Instance },
            { typeof(Offset?), new StaticNullableFormatter<Offset>(OffsetFormatter.Instance) },
            { typeof(OffsetDate), OffsetDateFormatter.Instance },
            { typeof(OffsetDate?), new StaticNullableFormatter<OffsetDate>(OffsetDateFormatter.Instance) },
            { typeof(OffsetDateTime), OffsetDateTimeFormatter.Instance },
            { typeof(OffsetDateTime?), new StaticNullableFormatter<OffsetDateTime>(OffsetDateTimeFormatter.Instance) },
            { typeof(OffsetTime), OffsetTimeFormatter.Instance },
            { typeof(OffsetTime?), new StaticNullableFormatter<OffsetTime>(OffsetTimeFormatter.Instance) },

            { typeof(Period), PeriodFormatter.Instance },

            { typeof(ZonedDateTime), BclZonedDateTimeFormatter.Instance },
            { typeof(ZonedDateTime?), new StaticNullableFormatter<ZonedDateTime>(BclZonedDateTimeFormatter.Instance) },
        };

        internal static object GetFormatter(Type t)
        {
            if (formatterMap.TryGetValue(t, out var formatter))
            {
                return formatter;
            }

            // DateTimeZone
            if (typeof(DateTimeZone).IsAssignableFrom(t))
            {
                return Activator.CreateInstance(typeof(BclDateTimeZoneFormatter<>).MakeGenericType(t));
            }

            // If type can not get, must return null for fallback mechanism.
            return null;
        }
    }

    public sealed class TzdbNodatimeResolver : IFormatterResolver
    {
        // Resolver should be singleton.
        public static readonly IFormatterResolver Instance = new TzdbNodatimeResolver();

        TzdbNodatimeResolver() { }

        // GetFormatter<T>'s get cost should be minimized so use type cache.
        public IMessagePackFormatter<T> GetFormatter<T>()
        {
            return FormatterCache<T>.formatter;
        }

        static class FormatterCache<T>
        {
            public static readonly IMessagePackFormatter<T> formatter;

            // generic's static constructor should be minimized for reduce type generation size!
            // use outer helper method.
            static FormatterCache() => formatter = (IMessagePackFormatter<T>)TzdbNodatimeResolverGetFormatterHelper.GetFormatter(typeof(T));
        }
    }

    internal static class TzdbNodatimeResolverGetFormatterHelper
    {
        static readonly Dictionary<Type, object> formatterMap = new Dictionary<Type, object>()
        {
            { typeof(AnnualDate), AnnualDateFormatter.Instance },
            { typeof(AnnualDate?), new StaticNullableFormatter<AnnualDate>(AnnualDateFormatter.Instance) },

            { typeof(DateInterval), DateIntervalFormatter.Instance },

            { typeof(Duration), DurationFormatter.Instance },
            { typeof(Duration?), new StaticNullableFormatter<Duration>(DurationFormatter.Instance) },

            { typeof(Instant), InstantFormatter.Instance },
            { typeof(Instant?), new StaticNullableFormatter<Instant>(InstantFormatter.Instance) },

            { typeof(Interval), IntervalFormatter.Instance },
            { typeof(Interval?), new StaticNullableFormatter<Interval>(IntervalFormatter.Instance) },

            { typeof(LocalDate), LocalDateFormatter.Instance },
            { typeof(LocalDate?), new StaticNullableFormatter<LocalDate>(LocalDateFormatter.Instance) },
            { typeof(LocalDateTime), LocalDateTimeFormatter.Instance },
            { typeof(LocalDateTime?), new StaticNullableFormatter<LocalDateTime>(LocalDateTimeFormatter.Instance) },
            { typeof(LocalTime), LocalTimeFormatter.Instance },
            { typeof(LocalTime?), new StaticNullableFormatter<LocalTime>(LocalTimeFormatter.Instance) },

            { typeof(Offset), OffsetFormatter.Instance },
            { typeof(Offset?), new StaticNullableFormatter<Offset>(OffsetFormatter.Instance) },
            { typeof(OffsetDate), OffsetDateFormatter.Instance },
            { typeof(OffsetDate?), new StaticNullableFormatter<OffsetDate>(OffsetDateFormatter.Instance) },
            { typeof(OffsetDateTime), OffsetDateTimeFormatter.Instance },
            { typeof(OffsetDateTime?), new StaticNullableFormatter<OffsetDateTime>(OffsetDateTimeFormatter.Instance) },
            { typeof(OffsetTime), OffsetTimeFormatter.Instance },
            { typeof(OffsetTime?), new StaticNullableFormatter<OffsetTime>(OffsetTimeFormatter.Instance) },

            { typeof(Period), PeriodFormatter.Instance },

            { typeof(ZonedDateTime), TzdbZonedDateTimeFormatter.Instance },
            { typeof(ZonedDateTime?), new StaticNullableFormatter<ZonedDateTime>(TzdbZonedDateTimeFormatter.Instance) },
        };

        internal static object GetFormatter(Type t)
        {
            if (formatterMap.TryGetValue(t, out var formatter))
            {
                return formatter;
            }

            // DateTimeZone
            if (typeof(DateTimeZone).IsAssignableFrom(t))
            {
                return Activator.CreateInstance(typeof(TzdbDateTimeZoneFormatter<>).MakeGenericType(t));
            }

            // If type can not get, must return null for fallback mechanism.
            return null;
        }
    }
}