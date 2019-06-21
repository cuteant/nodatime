using System;
using NodaTime.Serialization.MessagePackSpan.Tests.Helpers;
using Xunit;

namespace NodaTime.Serialization.MessagePackSpan.Tests
{
    [Collection("ResolverCollection")]
    public class PeriodMessagePackFormatterTest
    {
        [Fact]
        public void PeriodTest()
        {
            Period p = Period.FromDays(1);
            Assert.Equal(TestTools.Convert(p), p);
        }

        [Fact]
        public void PeriodArrayTest()
        {
            var pp = new PeriodBuilder
            {
                Years = DateTime.UtcNow.Year,
                Months = DateTime.UtcNow.Month,
                Weeks = 4,
                Days = DateTime.UtcNow.Day,
                Hours = new DateTime().Hour,
                Minutes = DateTime.UtcNow.Minute,
                Seconds = new DateTime().Second,
                Milliseconds = DateTime.UtcNow.Millisecond,
                Ticks = DateTime.Now.Ticks,
                Nanoseconds = DateTime.UtcNow.Ticks / 100,
            }.Build();

            var pp1 = new PeriodBuilder
            {
                Years = DateTime.UtcNow.Year,
                Months = DateTime.UtcNow.Month,
                Weeks = 4,
                Days = new DateTime().Day               
            }.Build();

            Period[] p = new Period[]
                { Period.FromYears(1),
                pp,
                Period.FromDays(1),
                Period.FromNanoseconds(5),
                pp1
            };
            Assert.Equal(TestTools.Convert(p), p);
        }
    }
}
