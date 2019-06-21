using NodaTime.Serialization.MessagePackSpan.Tests.Helpers;
using Xunit;

namespace NodaTime.Serialization.MessagePackSpan.Tests
{
    [Collection("ResolverCollection")]
    public class OffsetMessagePackFormatterTest
    {
        [Fact]
        public void OffsetTest()
        {
            Offset offSet = Offset.FromHours(1);
            Assert.Equal(TestTools.Convert(offSet), offSet);
        }

        [Fact]
        public void NullableOffsetTest()
        {
            Offset? offSet = null;
            Assert.Equal(TestTools.Convert(offSet), offSet);
        }               

        [Fact]
        public void OffsetArrayTest()
        {
            Offset[] offSet = new Offset[]
                { Offset.FromHoursAndMinutes(1, 3),
                Offset.FromSeconds(80),
                Offset.FromMilliseconds(200),
                Offset.FromHours(3),
                Offset.FromNanoseconds(99)
            };
            Assert.Equal(TestTools.Convert(offSet), offSet);
        }

        [Fact]
        public void NullableOffsetArrayTest()
        {
            Offset?[] offSet = new Offset?[] {
                null,
                null,
                null,
                null,
                null
            };
            Assert.Equal(TestTools.Convert(offSet), offSet);
        }        
    }
}
