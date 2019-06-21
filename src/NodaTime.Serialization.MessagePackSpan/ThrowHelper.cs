namespace NodaTime.Serialization.MessagePackSpan
{
    using System;
    using System.Runtime.CompilerServices;

    internal static class ThrowHelper
    {
        [MethodImpl(MethodImplOptions.NoInlining)]
        internal static void ThrowArgumentNullException_Provider()
        {
            throw GetException();
            static ArgumentNullException GetException()
            {
                return new ArgumentNullException("provider");
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal static void ThrowInvalidOperationException_InvalidArrayCount()
        {
            throw GetException();
            static InvalidOperationException GetException()
            {
                return new InvalidOperationException("Invalid array count");
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal static InvalidOperationException GetInvalidOperationException_LocalDate_Format(Exception ex)
        {
            return new InvalidOperationException("Invalid LocalDate format", ex);
        }
    }
}
