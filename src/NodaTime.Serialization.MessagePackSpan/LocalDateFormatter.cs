namespace NodaTime.Serialization.MessagePackSpan
{
    using System;
    using MessagePack;
    using MessagePack.Formatters;

    public sealed class LocalDateFormatter : IMessagePackFormatter<LocalDate>
    {
        public static readonly LocalDateFormatter Instance = new LocalDateFormatter();

        const int c_count = 4;

        public LocalDate Deserialize(ref MessagePackReader reader, IFormatterResolver formatterResolver)
        {
            var count = reader.ReadArrayHeader();
            if (count != c_count) { ThrowHelper.ThrowInvalidOperationException_InvalidArrayCount(); }

            int year = reader.ReadInt32();
            int month = reader.ReadInt32();
            int day = reader.ReadInt32();
            CalendarOrdinal ordinal = (CalendarOrdinal)reader.ReadInt32();

            try
            {
                var calendar = CalendarSystem.ForOrdinal(ordinal);
                calendar.ValidateYearMonthDay(year, month, day);
                return new LocalDate(year, month, day, calendar);
            }
            catch (Exception e)
            {
                throw ThrowHelper.GetInvalidOperationException_LocalDate_Format(e);
            }
        }

        public void Serialize(ref MessagePackWriter writer, ref int idx, LocalDate value, IFormatterResolver formatterResolver)
        {
            writer.WriteFixedArrayHeaderUnsafe(c_count, ref idx);

            var yearMonthDayCalendar = value.YearMonthDayCalendar;

            writer.WriteInt32(yearMonthDayCalendar.Year, ref idx);
            writer.WriteInt32(yearMonthDayCalendar.Month, ref idx);
            writer.WriteInt32(yearMonthDayCalendar.Day, ref idx);
            writer.WriteInt32((int)yearMonthDayCalendar.CalendarOrdinal, ref idx);
        }
    }
}
