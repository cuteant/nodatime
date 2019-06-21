namespace NodaTime.Serialization.MessagePackSpan.Tests.Helpers
{
    using MessagePack;

    static class TestTools
    {
        public static T Convert<T>(T value)
        {
            return MessagePackSerializer.Deserialize<T>(MessagePackSerializer.Serialize(value));
        }
    }
}
