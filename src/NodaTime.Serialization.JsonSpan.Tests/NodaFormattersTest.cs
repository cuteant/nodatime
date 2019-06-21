namespace NodaTime.Serialization.JsonSpan.Tests
{
    using System;
    using System.Text;
    using SpanJson;
    using Xunit;
    using static NodaTime.Serialization.JsonSpan.Tests.TestHelper;

    /// <summary>
    /// Tests for the formatters exposed in NodaFormatters.
    /// </summary>
    public class NodaFormattersTest
    {
        [Fact]
        public void OffsetFormatter()
        {
            var value = Offset.FromHoursAndMinutes(5, 30);
            var json = "\"+05:30\"";
            AssertConversionsUtf16<Offset, NodaExcludeNullsOriginalCaseResolver<char>>(value, json);
            AssertConversionsUtf8<Offset, NodaExcludeNullsOriginalCaseResolver<byte>>(value, json);
        }

        [Fact]
        public void InstantFormatter()
        {
            var value = Instant.FromUtc(2012, 1, 2, 3, 4, 5);
            var json = "\"2012-01-02T03:04:05Z\"";
            AssertConversionsUtf16<Instant, NodaExcludeNullsOriginalCaseResolver<char>>(value, json);
            AssertConversionsUtf8<Instant, NodaExcludeNullsOriginalCaseResolver<byte>>(value, json);
        }

        [Fact]
        public void InstantFormatter_EquivalentToIsoDateTimeFormatter()
        {
            var dateTime = new DateTime(2012, 1, 2, 3, 4, 5, DateTimeKind.Utc);
            var instant = Instant.FromDateTimeUtc(dateTime);

            var jsonDateTime = JsonSerializer.Generic.Utf16.Serialize(dateTime);
            var jsonInstant = JsonSerializer.Generic.Utf16.Serialize<Instant, NodaExcludeNullsOriginalCaseResolver<char>>(instant);
            Assert.Equal(jsonDateTime, jsonInstant);

            var jsonDateTimeBytes = JsonSerializer.Generic.Utf8.Serialize(dateTime);
            var jsonInstantBytes = JsonSerializer.Generic.Utf8.Serialize<Instant, NodaExcludeNullsOriginalCaseResolver<byte>>(instant);
            Assert.True(jsonDateTimeBytes.AsSpan().SequenceEqual(jsonInstantBytes));
        }

        [Fact]
        public void LocalDateFormatter()
        {
            var value = new LocalDate(2012, 1, 2, CalendarSystem.Iso);
            var json = "\"2012-01-02\"";
            AssertConversionsUtf16<LocalDate, NodaExcludeNullsOriginalCaseResolver<char>>(value, json);
            AssertConversionsUtf8<LocalDate, NodaExcludeNullsOriginalCaseResolver<byte>>(value, json);
        }

        [Fact]
        public void LocalDateFormatter_SerializeNonIso_Throws()
        {
            var localDate = new LocalDate(2012, 1, 2, CalendarSystem.Coptic);

            Assert.Throws<ArgumentException>(() => JsonSerializer.Generic.Utf16.Serialize<LocalDate, NodaExcludeNullsOriginalCaseResolver<char>>(localDate));
            Assert.Throws<ArgumentException>(() => JsonSerializer.Generic.Utf8.Serialize<LocalDate, NodaExcludeNullsOriginalCaseResolver<byte>>(localDate));
        }

        [Fact]
        public void LocalDateTimeFormatter()
        {
            var value = new LocalDateTime(2012, 1, 2, 3, 4, 5, CalendarSystem.Iso).PlusNanoseconds(123456789);
            var json = "\"2012-01-02T03:04:05.123456789\"";
            AssertConversionsUtf16<LocalDateTime, NodaExcludeNullsOriginalCaseResolver<char>>(value, json);
            AssertConversionsUtf8<LocalDateTime, NodaExcludeNullsOriginalCaseResolver<byte>>(value, json);
        }

        [Fact]
        public void LocalDateTimeFormatter_EquivalentToIsoDateTimeFormatter()
        {
            var dateTime = new DateTime(2012, 1, 2, 3, 4, 5, 6, DateTimeKind.Unspecified);
            var localDateTime = new LocalDateTime(2012, 1, 2, 3, 4, 5, 6, CalendarSystem.Iso);

            var jsonDateTime = JsonSerializer.Generic.Utf16.Serialize(dateTime);
            var jsonLocalDateTime = JsonSerializer.Generic.Utf16.Serialize<LocalDateTime, NodaExcludeNullsOriginalCaseResolver<char>>(localDateTime);
            Assert.Equal(jsonDateTime, jsonLocalDateTime);

            var jsonDateTimeBytes = JsonSerializer.Generic.Utf8.Serialize(dateTime);
            var jsonLocalDateTimeBytes = JsonSerializer.Generic.Utf8.Serialize<LocalDateTime, NodaExcludeNullsOriginalCaseResolver<byte>>(localDateTime);
            Assert.True(jsonDateTimeBytes.AsSpan().SequenceEqual(jsonLocalDateTimeBytes));
        }

        [Fact]
        public void LocalDateTimeFormatter_SerializeNonIso_Throws()
        {
            var localDateTime = new LocalDateTime(2012, 1, 2, 3, 4, 5, CalendarSystem.Coptic);

            Assert.Throws<ArgumentException>(() => JsonSerializer.Generic.Utf16.Serialize<LocalDateTime, NodaExcludeNullsOriginalCaseResolver<char>>(localDateTime));
            Assert.Throws<ArgumentException>(() => JsonSerializer.Generic.Utf8.Serialize<LocalDateTime, NodaExcludeNullsOriginalCaseResolver<byte>>(localDateTime));
        }

        [Fact]
        public void LocalTimeFormatter()
        {
            var value = LocalTime.FromHourMinuteSecondMillisecondTick(1, 2, 3, 4, 5).PlusNanoseconds(67);
            var json = "\"01:02:03.004000567\"";
            AssertConversionsUtf16<LocalTime, NodaExcludeNullsOriginalCaseResolver<char>>(value, json);
            AssertConversionsUtf8<LocalTime, NodaExcludeNullsOriginalCaseResolver<byte>>(value, json);
        }

        [Fact]
        public void RoundtripPeriodFormatter()
        {
            var value = Period.FromDays(2) + Period.FromHours(3) + Period.FromMinutes(90);
            var json = "\"P2DT3H90M\"";
            AssertConversionsUtf16<Period, NodaExcludeNullsOriginalCaseResolver<char>>(value, json);
            AssertConversionsUtf8<Period, NodaExcludeNullsOriginalCaseResolver<byte>>(value, json);
        }

        [Fact]
        public void NormalizingIsoPeriodFormatter_RequiresNormalization()
        {
            // Can't use AssertConversions here, as it doesn't round-trip
            var period = Period.FromDays(2) + Period.FromHours(3) + Period.FromMinutes(90);
            var json = JsonSerializer.Generic.Utf16.Serialize<Period, NodaIsoExcludeNullsOriginalCaseResolver<char>>(period);
            var jsonBytes = JsonSerializer.Generic.Utf8.Serialize<Period, NodaIsoExcludeNullsOriginalCaseResolver<byte>>(period);
            var expectedJson = "\"P2DT4H30M\"";
            Assert.Equal(expectedJson, json);
            Assert.Equal(expectedJson, Encoding.UTF8.GetString(jsonBytes));
        }

        [Fact]
        public void NormalizingIsoPeriodFormatter_AlreadyNormalized()
        {
            // This time we're okay as it's already a normalized value.
            var value = Period.FromDays(2) + Period.FromHours(4) + Period.FromMinutes(30);
            var json = "\"P2DT4H30M\"";
            AssertConversionsUtf16<Period, NodaIsoExcludeNullsOriginalCaseResolver<char>>(value, json);
            AssertConversionsUtf8<Period, NodaIsoExcludeNullsOriginalCaseResolver<byte>>(value, json);
        }

        [Fact]
        public void ZonedDateTimeFormatter()
        {
            // Deliberately give it an ambiguous local time, in both ways.
            var zone = DateTimeZoneProviders.Tzdb["Europe/London"];
            var earlierValue = new ZonedDateTime(new LocalDateTime(2012, 10, 28, 1, 30), zone, Offset.FromHours(1));
            var laterValue = new ZonedDateTime(new LocalDateTime(2012, 10, 28, 1, 30), zone, Offset.FromHours(0));
            var earlierJson = "\"2012-10-28T01:30:00+01 Europe/London\"";
            var laterJson = "\"2012-10-28T01:30:00Z Europe/London\"";

            AssertConversionsUtf16<ZonedDateTime, NodaExcludeNullsOriginalCaseResolver<char>>(earlierValue, earlierJson);
            AssertConversionsUtf8<ZonedDateTime, NodaExcludeNullsOriginalCaseResolver<byte>>(earlierValue, earlierJson);
            AssertConversionsUtf16<ZonedDateTime, NodaExcludeNullsOriginalCaseResolver<char>>(laterValue, laterJson);
            AssertConversionsUtf8<ZonedDateTime, NodaExcludeNullsOriginalCaseResolver<byte>>(laterValue, laterJson);
        }

        [Fact]
        public void OffsetDateTimeFormatter()
        {
            var value = new LocalDateTime(2012, 1, 2, 3, 4, 5).PlusNanoseconds(123456789).WithOffset(Offset.FromHoursAndMinutes(-1, -30));
            var json = "\"2012-01-02T03:04:05.123456789-01:30\"";
            AssertConversionsUtf16<OffsetDateTime, NodaExcludeNullsOriginalCaseResolver<char>>(value, json);
            AssertConversionsUtf8<OffsetDateTime, NodaExcludeNullsOriginalCaseResolver<byte>>(value, json);
        }

        [Fact]
        public void OffsetDateTimeFormatter_WholeHours()
        {
            // Redundantly specify the minutes, so that Javascript can parse it and it's RFC3339-compliant.
            // See issue 284 for details.
            var value = new LocalDateTime(2012, 1, 2, 3, 4, 5).PlusNanoseconds(123456789).WithOffset(Offset.FromHours(5));
            var json = "\"2012-01-02T03:04:05.123456789+05:00\"";
            AssertConversionsUtf16<OffsetDateTime, NodaExcludeNullsOriginalCaseResolver<char>>(value, json);
            AssertConversionsUtf8<OffsetDateTime, NodaExcludeNullsOriginalCaseResolver<byte>>(value, json);
        }

        [Fact]
        public void OffsetDateTimeFormatter_ZeroOffset()
        {
            // Redundantly specify the minutes, so that Javascript can parse it and it's RFC3339-compliant.
            // See issue 284 for details.
            var value = new LocalDateTime(2012, 1, 2, 3, 4, 5).PlusNanoseconds(123456789).WithOffset(Offset.Zero);
            var json = "\"2012-01-02T03:04:05.123456789Z\"";
            AssertConversionsUtf16<OffsetDateTime, NodaExcludeNullsOriginalCaseResolver<char>>(value, json);
            AssertConversionsUtf8<OffsetDateTime, NodaExcludeNullsOriginalCaseResolver<byte>>(value, json);
        }

        [Fact]
        public void Duration_WholeSeconds()
        {
            var value = Duration.FromHours(48);
            var json = "\"48:00:00\"";
            AssertConversionsUtf16<Duration, NodaExcludeNullsOriginalCaseResolver<char>>(value, json);
            AssertConversionsUtf8<Duration, NodaExcludeNullsOriginalCaseResolver<byte>>(value, json);
        }

        [Fact]
        public void Duration_FractionalSeconds()
        {
            AssertConversionsUtf16<Duration, NodaExcludeNullsOriginalCaseResolver<char>>(Duration.FromHours(48) + Duration.FromSeconds(3) + Duration.FromNanoseconds(123456789), "\"48:00:03.123456789\"");
            AssertConversionsUtf8<Duration, NodaExcludeNullsOriginalCaseResolver<byte>>(Duration.FromHours(48) + Duration.FromSeconds(3) + Duration.FromNanoseconds(123456789), "\"48:00:03.123456789\"");

            AssertConversionsUtf16<Duration, NodaExcludeNullsOriginalCaseResolver<char>>(Duration.FromHours(48) + Duration.FromSeconds(3) + Duration.FromTicks(1230000), "\"48:00:03.123\"");
            AssertConversionsUtf8<Duration, NodaExcludeNullsOriginalCaseResolver<byte>>(Duration.FromHours(48) + Duration.FromSeconds(3) + Duration.FromTicks(1230000), "\"48:00:03.123\"");

            AssertConversionsUtf16<Duration, NodaExcludeNullsOriginalCaseResolver<char>>(Duration.FromHours(48) + Duration.FromSeconds(3) + Duration.FromTicks(1234000), "\"48:00:03.1234\"");
            AssertConversionsUtf8<Duration, NodaExcludeNullsOriginalCaseResolver<byte>>(Duration.FromHours(48) + Duration.FromSeconds(3) + Duration.FromTicks(1234000), "\"48:00:03.1234\"");

            AssertConversionsUtf16<Duration, NodaExcludeNullsOriginalCaseResolver<char>>(Duration.FromHours(48) + Duration.FromSeconds(3) + Duration.FromTicks(12345), "\"48:00:03.0012345\"");
            AssertConversionsUtf8<Duration, NodaExcludeNullsOriginalCaseResolver<byte>>(Duration.FromHours(48) + Duration.FromSeconds(3) + Duration.FromTicks(12345), "\"48:00:03.0012345\"");
        }

        [Fact]
        public void Duration_MinAndMaxValues()
        {
            AssertConversionsUtf16<Duration, NodaExcludeNullsOriginalCaseResolver<char>>(Duration.FromTicks(long.MaxValue), "\"256204778:48:05.4775807\"");
            AssertConversionsUtf8<Duration, NodaExcludeNullsOriginalCaseResolver<byte>>(Duration.FromTicks(long.MaxValue), "\"256204778:48:05.4775807\"");

            AssertConversionsUtf16<Duration, NodaExcludeNullsOriginalCaseResolver<char>>(Duration.FromTicks(long.MinValue), "\"-256204778:48:05.4775808\"");
            AssertConversionsUtf8<Duration, NodaExcludeNullsOriginalCaseResolver<byte>>(Duration.FromTicks(long.MinValue), "\"-256204778:48:05.4775808\"");
        }

        /// <summary>
        /// The pre-release converter used either 3 or 7 decimal places for fractions of a second; never less.
        /// This test checks that the "new" converter (using DurationPattern) can still parse the old output.
        /// </summary>
        [Fact]
        public void Duration_ParsePartialFractionalSecondsWithTrailingZeroes()
        {
            var expected = Duration.FromHours(25) + Duration.FromMinutes(10) + Duration.FromTicks(1234000);
            var parsed = JsonSerializer.Generic.Utf16.Deserialize<Duration, NodaExcludeNullsOriginalCaseResolver<char>>("\"25:10:00.1234000\"");
            Assert.Equal(expected, parsed);
            parsed = JsonSerializer.Generic.Utf8.Deserialize<Duration, NodaExcludeNullsOriginalCaseResolver<byte>>(Encoding.UTF8.GetBytes("\"25:10:00.1234000\""));
            Assert.Equal(expected, parsed);
        }

        [Fact]
        public void OffsetDateFormatter()
        {
            var value = new LocalDate(2012, 1, 2).WithOffset(Offset.FromHoursAndMinutes(-1, -30));
            string json = "\"2012-01-02-01:30\"";
            AssertConversionsUtf16<OffsetDate, NodaExcludeNullsOriginalCaseResolver<char>>(value, json);
            AssertConversionsUtf8<OffsetDate, NodaExcludeNullsOriginalCaseResolver<byte>>(value, json);
        }

        [Fact]
        public void OffsetTimeFormattor()
        {
            var value = new LocalTime(3, 4, 5).PlusNanoseconds(123456789).WithOffset(Offset.FromHoursAndMinutes(-1, -30));
            string json = "\"03:04:05.123456789-01:30\"";
            AssertConversionsUtf16<OffsetTime, NodaExcludeNullsOriginalCaseResolver<char>>(value, json);
            AssertConversionsUtf8<OffsetTime, NodaExcludeNullsOriginalCaseResolver<byte>>(value, json);
        }
    }
}
