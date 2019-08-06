
namespace NodaTime.Serialization.JsonSpan
{
    using System;
    using NodaTime.Text;
    using NodaTime.Utility;
    using SpanJson;

    public abstract class NodaPatternFormatterBase<T> : ICustomJsonFormatter<T>
    {
        private readonly IPattern<T> _pattern;
        private readonly Action<T> _validator;

        /// <summary>Creates a new instance with a pattern and no validator.</summary>
        /// <param name="pattern">The pattern to use for parsing and formatting.</param>
        /// <exception cref="ArgumentNullException"><paramref name="pattern"/> is null.</exception>
        public NodaPatternFormatterBase(IPattern<T> pattern) : this(pattern, null) { }

        /// <summary>Creates a new instance with a pattern and an optional validator. The validator will be called before each
        /// value is written, and may throw an exception to indicate that the value cannot be serialized.</summary>
        /// <param name="pattern">The pattern to use for parsing and formatting.</param>
        /// <param name="validator">The validator to call before writing values. May be null, indicating that no validation is required.</param>
        /// <exception cref="ArgumentNullException"><paramref name="pattern"/> is null.</exception>
        public NodaPatternFormatterBase(IPattern<T> pattern, Action<T> validator)
        {
            Preconditions.CheckNotNull(pattern, nameof(pattern));
            this._pattern = pattern;
            this._validator = validator;
        }

        public object Arguments { get; set; }

        public T Deserialize(ref JsonReader<byte> reader, IJsonFormatterResolver<byte> resolver)
        {
            string text = reader.ReadUtf8String();
            return _pattern.Parse(text).Value;
        }

        public T Deserialize(ref JsonReader<char> reader, IJsonFormatterResolver<char> resolver)
        {
            string text = reader.ReadUtf16String();
            return _pattern.Parse(text).Value;
        }

        public void Serialize(ref JsonWriter<byte> writer, T value, IJsonFormatterResolver<byte> resolver)
        {
            _validator?.Invoke(value);
            writer.WriteUtf8String(_pattern.Format(value), resolver.EscapeHandling);
        }

        public void Serialize(ref JsonWriter<char> writer, T value, IJsonFormatterResolver<char> resolver)
        {
            _validator?.Invoke(value);
            writer.WriteUtf16String(_pattern.Format(value), resolver.EscapeHandling);
        }

        protected static Action<T> CreateIsoValidator(Func<T, CalendarSystem> calendarProjection) => value =>
        {
            var calendar = calendarProjection(value);
            // We rely on CalendarSystem.Iso being a singleton here.
            if (calendar != CalendarSystem.Iso) { ThrowHelper.ThrowArgumentException_MustUseIsoCalendar<T>(); }
        };
    }
}
