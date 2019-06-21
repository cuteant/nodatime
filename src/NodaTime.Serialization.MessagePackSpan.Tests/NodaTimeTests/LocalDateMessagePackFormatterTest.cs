using System;
using NodaTime.Serialization.MessagePackSpan.Tests.Helpers;
using Xunit;

namespace NodaTime.Serialization.MessagePackSpan.Tests
{
    [Collection("ResolverCollection")]
    public class LocalDateMessagePackFormatterTest
    {   
        [Fact]
        public void LocalDateTest()
        {
            LocalDate ld = LocalDate.FromDateTime(DateTime.Now);      
            Assert.Equal(TestTools.Convert(ld), ld);
        }

        [Fact]
        public void NullableLocalDateTest()
        {
            LocalDate? ld = null;
            Assert.Equal(TestTools.Convert(ld), ld);
        }
                
        [Fact]
        public void LocalDateArrayTest()
        {
            LocalDate[] ld =
                { LocalDate.FromDateTime(DateTime.Now),
                LocalDate.FromDateTime(new DateTime()),
                LocalDate.FromDateTime(DateTime.Now.AddTicks(500)),
                LocalDate.FromDateTime(DateTime.Now.AddMonths(10)),
                LocalDate.FromDateTime(new DateTime(2010,10,10))
            };
            Assert.Equal(TestTools.Convert(ld), ld);
        }

        [Fact]
        public void NullableLocalDateArrayTest()
        {
            LocalDate?[] ld = new LocalDate?[] {
                null,
                null,
                null,
                null,
                null
            };
            Assert.Equal(TestTools.Convert(ld), ld);
        }        
    }
}
