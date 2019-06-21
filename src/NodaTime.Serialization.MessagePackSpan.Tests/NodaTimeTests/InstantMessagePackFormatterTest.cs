using System;
using NodaTime.Serialization.MessagePackSpan.Tests.Helpers;
using Xunit;

namespace NodaTime.Serialization.MessagePackSpan.Tests
{
    [Collection("ResolverCollection")]
    public class InstantMessagePackFormatterTest
    {
        [Fact]
        public void InstantValueTest()
        {
            Instant inst = Instant.FromDateTimeUtc(DateTime.UtcNow);
            Assert.Equal(TestTools.Convert(inst), inst);
        }

        [Fact]
        public void NullableInstantValueTest()
        {
            Instant? inst = null;
            Assert.Equal(TestTools.Convert(inst), inst);
        }                

        [Fact]
        public void InstantArrayTest()
        {
            Instant[] inst =
                { Instant.FromDateTimeUtc(DateTime.UtcNow.AddHours(13)),
                Instant.FromDateTimeUtc(DateTime.UtcNow.AddMinutes(54)),
                Instant.FromDateTimeUtc(DateTime.UtcNow.AddYears(1)),
                Instant.FromDateTimeUtc(DateTime.UtcNow.AddSeconds(33)),
                Instant.FromDateTimeUtc(DateTime.UtcNow),
            };
            Assert.Equal(TestTools.Convert(inst), inst);
        }

        [Fact]
        public void NullableInstantArrayTest()
        {
            Instant?[] inst = new Instant?[] {
                null,
                null,
                null,
                null,
                null
            };
            Assert.Equal(TestTools.Convert(inst), inst);
        }       
    }
}
