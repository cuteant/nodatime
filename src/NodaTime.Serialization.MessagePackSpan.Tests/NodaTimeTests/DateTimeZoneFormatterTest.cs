using MessagePack;
using NodaTime.Serialization.MessagePackSpan.Tests.Helpers;
using Xunit;

namespace NodaTime.Serialization.MessagePackSpan.Tests
{
    [Collection("ResolverCollection")]
    public class DateTimeZoneFormatterTest
    {
        [Fact]
        public void DateTimeZoneValueTest()
        {
            var dateTimeZone = DateTimeZoneProviders.Tzdb["America/Los_Angeles"];
            Assert.Equal(TestTools.Convert(dateTimeZone), dateTimeZone);
        }

        [Fact]
        public void SerializeDeserializeUtf16()
        {
            var expectedDateTimeZone = new DateTimeZoneWrapper { Id = 101, Zone = DateTimeZoneProviders.Tzdb["America/Los_Angeles"] };

            var serialized = MessagePackSerializer.Serialize(expectedDateTimeZone);
            var dateTimeZone = MessagePackSerializer.Deserialize<DateTimeZoneWrapper>(serialized);
            Assert.Equal(expectedDateTimeZone.Id, dateTimeZone.Id);
            Assert.Equal(expectedDateTimeZone.Zone, dateTimeZone.Zone);
        }

        public class DateTimeZoneWrapper
        {
            public int Id { get; set; }
            public DateTimeZone Zone { get; set; }
        }
    }
}
