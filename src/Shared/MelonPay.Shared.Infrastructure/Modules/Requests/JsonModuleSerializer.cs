using System.Text.Json;

namespace MelonPay.Shared.Infrastructure.Modules.Requests
{
    internal sealed class JsonModuleSerializer : IModuleSerializer
    {
        private static readonly JsonSerializerOptions SerializerOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public byte[] Serialize<T>(T value)
            => JsonSerializer.SerializeToUtf8Bytes(value, SerializerOptions);

        public T Deserialize<T>(byte[] value)
            => JsonSerializer.Deserialize<T>(value, SerializerOptions);

        public object Deserialize(byte[] value, Type type)
            => JsonSerializer.Deserialize(value, type, SerializerOptions);
    }
}
