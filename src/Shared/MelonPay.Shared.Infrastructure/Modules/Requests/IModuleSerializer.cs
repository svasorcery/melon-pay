namespace MelonPay.Shared.Infrastructure.Modules.Requests
{
    internal interface IModuleSerializer
    {
        byte[] Serialize<T>(T value);
        T Deserialize<T>(byte[] value);
        object Deserialize(byte[] value, Type type);
    }
}
