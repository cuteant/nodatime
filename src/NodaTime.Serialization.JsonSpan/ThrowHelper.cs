namespace NodaTime.Serialization.JsonSpan
{
    using System;
    using System.Runtime.CompilerServices;
    using NodaTime.Utility;

    internal static class ThrowHelper
    {
        [MethodImpl(MethodImplOptions.NoInlining)]
        internal static void ThrowArgumentException_MustUseIsoCalendar<T>()
        {
            throw GetException();
            static ArgumentException GetException()
            {
                return new ArgumentException($"Values of type {typeof(T).Name} must (currently) use the ISO calendar in order to be serialized.");
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal static void ThrowInvalidNodaDataException_IsoFormattedIntervalSlash()
        {
            throw GetException();
            static InvalidNodaDataException GetException()
            {
                return new InvalidNodaDataException("Expected ISO-8601-formatted interval; slash was missing.");
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal static void ThrowInvalidNodaDataException_DateIntervalStartDate()
        {
            throw GetException();
            static InvalidNodaDataException GetException()
            {
                return new InvalidNodaDataException("Expected date interval; start date was missing.");
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal static void ThrowInvalidNodaDataException_DateIntervalEndDate()
        {
            throw GetException();
            static InvalidNodaDataException GetException()
            {
                return new InvalidNodaDataException("Expected date interval; end date was missing.");
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal static void ThrowInvalidNodaDataException_IsoFormattedDateIntervalSlash()
        {
            throw GetException();
            static InvalidNodaDataException GetException()
            {
                return new InvalidNodaDataException("Expected ISO-8601-formatted date interval; slash was missing.");
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal static void ThrowInvalidNodaDataException_IsoFormattedDateIntervalStartDate()
        {
            throw GetException();
            static InvalidNodaDataException GetException()
            {
                return new InvalidNodaDataException("Expected ISO-8601-formatted date interval; start date was missing.");
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal static void ThrowInvalidNodaDataException_IsoFormattedDateIntervalEndDate()
        {
            throw GetException();
            static InvalidNodaDataException GetException()
            {
                return new InvalidNodaDataException("Expected ISO-8601-formatted date interval; end date was missing.");
            }
        }
    }
}
