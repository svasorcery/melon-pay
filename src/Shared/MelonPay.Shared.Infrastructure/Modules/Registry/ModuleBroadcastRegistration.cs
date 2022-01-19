namespace MelonPay.Shared.Infrastructure.Modules.Registry
{
    internal record ModuleBroadcastRegistration(
        Type ReceiverType,
        Func<object, CancellationToken, Task> Action
        )
    {
        public string Key => ReceiverType.Name;
    }
}
